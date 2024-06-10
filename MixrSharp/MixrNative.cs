using System.Runtime.InteropServices;

namespace MixrSharp;

public static class MixrNative
{
    public const string DllName = "libmixr";

    [DllImport(DllName)]
    public static extern void mxCreateContext(uint sampleRate, out nint context);

    [DllImport(DllName)]
    public static extern void mxDestroyContext(nint context);
}