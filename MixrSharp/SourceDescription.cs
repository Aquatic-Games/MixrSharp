using System.Runtime.InteropServices;

namespace MixrSharp;

[StructLayout(LayoutKind.Explicit)]
public struct SourceDescription
{
    [FieldOffset(0)] public SourceType Type;

    [FieldOffset(4)] public AudioFormat Format;

    [FieldOffset(16)] public AdpcmDescription Adpcm;

    public SourceDescription(SourceType type, AudioFormat format)
    {
        Type = type;
        Format = format;
    }
}