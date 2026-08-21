using Avalonia;
using Avalonia.Media.Imaging;
using AvaloniaFramework.Threading;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AvaloniaFramework.Imaging;

/// <summary>
/// Shrinks a picked photo to the size the app actually stores, before it reaches the image store.
/// </summary>
/// <remarks>
/// <para>
/// A photo off a phone camera is a dozen megapixels and several megabytes. An app rarely draws one
/// larger than a full-screen viewer, and anything that copies the store — a backup archive, a sync —
/// carries every byte, so keeping the camera's original costs disk and transfer for detail nobody
/// sees.
/// </para>
/// <para>
/// Reduce at the point a photo is picked, not where it is written. This needs Avalonia's codecs, so
/// it cannot live in a UI-free storage or data layer; running it in the picker means every caller
/// gets the reduction with no change to any view model, and the store keeps writing whatever stream
/// it is handed.
/// </para>
/// </remarks>
public static class PhotoDownscaler
{
    /// <summary>
    /// The longest edge, in pixels, a stored photo may have.
    /// </summary>
    /// <remarks>
    /// The full-screen viewer is the largest consumer. At 1280 it still has more pixels than a
    /// phone screen can show, so the reduction is invisible where it matters and roughly an order
    /// of magnitude off the file size.
    /// </remarks>
    public const int MaxStoredEdge = 1280;

    /// <summary>
    /// JPEG quality for the re-encode. High enough that the compression is not visible on a
    /// photograph, low enough that the file is a fraction of a camera original.
    /// </summary>
    private const int JpegQuality = 85;

    /// <summary>
    /// Reduces a picked photo, returning the bytes to store and the extension they need.
    /// </summary>
    /// <param name="source">The picked file's contents. Not disposed here.</param>
    /// <param name="sourceExtension">The picked file's own extension, including the dot.</param>
    /// <returns>
    /// A rewound stream the caller owns, and its extension. When the photo is already small enough
    /// and upright the original bytes come back untouched, so no re-encode loss is introduced.
    /// </returns>
    /// <remarks>
    /// The copy and the decode — the expensive half, hundreds of milliseconds for a camera
    /// original — run on the thread pool, so picking a large photo no longer freezes the screen.
    /// Only the rotate-and-scale render stays on the calling (UI) thread: it draws through a
    /// <see cref="RenderTargetBitmap"/>, and by then the pixels are already down to stored size.
    /// </remarks>
    public static async Task<(Stream Content, string Extension)> ReduceAsync(Stream source, string sourceExtension)
    {
        // Buffered because the work needs three passes — orientation, decode, and the decision to
        // hand the original back — and a picker stream is often forward-only.
        var buffered = new MemoryStream();

        Bitmap decoded;
        int orientation;

        try
        {
            (decoded, orientation) = await Task.Run(() =>
            {
                source.CopyTo(buffered);
                buffered.Position = 0;

                var exif = ExifOrientation.Read(buffered);
                buffered.Position = 0;

                // Off the UI thread on purpose, like PhotoCache's decodes: nothing here touches a
                // visual, and this is where the hundreds of milliseconds go.
                return (new Bitmap(buffered), exif);
            }).WithSync();
        }
        catch (ArgumentException)
        {
            // An image Avalonia cannot decode — the same case PhotoCache swallows when drawing.
            // Storing the original unchanged is the safe answer: the app already tolerates a photo
            // it cannot render, and refusing the pick outright is a worse outcome than a big file.
            buffered.Position = 0;
            return (buffered, sourceExtension);
        }
        catch (IOException)
        {
            buffered.Position = 0;
            return (buffered, sourceExtension);
        }

        using (decoded)
        {
            var upright = PhotoCache.TransformFor(orientation, decoded.PixelSize).Size;
            var longest = Math.Max(upright.Width, upright.Height);

            if (longest <= MaxStoredEdge && orientation == ExifOrientation.Normal)
            {
                // Already small and already the right way up. Re-encoding would only lose quality
                // and, for a PNG, could make the file larger.
                buffered.Position = 0;
                return (buffered, sourceExtension);
            }

            try
            {
                var reduced = Encode(decoded, orientation, upright, longest);
                await buffered.DisposeAsync().WithSync();
                return (reduced, ".jpg");
            }
            catch (IOException)
            {
                buffered.Position = 0;
                return (buffered, sourceExtension);
            }
        }
    }

    /// <summary>
    /// Draws the photo upright and scaled, then encodes it as JPEG. UI thread only: the drawing
    /// goes through a <see cref="RenderTargetBitmap"/>.
    /// </summary>
    private static MemoryStream Encode(Bitmap decoded, int orientation, PixelSize upright, int longest)
    {
        var scale = longest <= MaxStoredEdge ? 1d : (double)MaxStoredEdge / longest;
        var target = new PixelSize(
            Math.Max(1, (int)Math.Round(upright.Width * scale)),
            Math.Max(1, (int)Math.Round(upright.Height * scale)));

        // The EXIF rotation is baked into the pixels here and the tag is dropped with the
        // re-encode, so PhotoCache reads Normal from the stored file and draws it as-is. Leaving
        // the tag off is the point: a rotated-but-tagged file would be turned twice.
        var transform = PhotoCache.TransformFor(orientation, decoded.PixelSize).Transform
            * Matrix.CreateScale(scale, scale);

        using var canvas = new RenderTargetBitmap(target);

        using (var context = canvas.CreateDrawingContext())
        using (context.PushTransform(transform))
        {
            context.DrawImage(decoded, new Rect(0, 0, decoded.PixelSize.Width, decoded.PixelSize.Height));
        }

        var output = new MemoryStream();
        canvas.Save(output, new JpegBitmapEncoderOptions { Quality = JpegQuality });
        output.Position = 0;
        return output;
    }
}