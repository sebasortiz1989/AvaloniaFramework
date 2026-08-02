using System.IO;
using System.Threading.Tasks;

namespace AvaloniaFramework.Presentation.View;

public interface OpenShapeFileDialog
{
    Task<(Stream Shp, Stream Dbf, string NameOfFile)> OpenShapeFileAsync();
}