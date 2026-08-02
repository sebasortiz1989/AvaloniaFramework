using System;

namespace AvaloniaFramework.Presentation.View.Screen;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class SizeAttribute : Attribute
{
    private readonly int size;

    public SizeAttribute(int size)
    {
        this.size = size;
    }

    public int Offset { get; set; }

    public int Size
    {
        get { return size; }
    }
}