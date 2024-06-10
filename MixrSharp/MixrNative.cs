using System.Runtime.InteropServices;

namespace MixrSharp;

public static unsafe class MixrNative
{
    public const string DllName = "libmixr";

    [DllImport(DllName)]
    public static extern void mxCreateContext(uint sampleRate, out nint context);

    [DllImport(DllName)]
    public static extern void mxDestroyContext(nint context);

    [DllImport(DllName)]
    public static extern void mxCreateSDLDevice(uint sampleRate, ushort periodSize, out nint device);

    [DllImport(DllName)]
    public static extern void mxDeviceGetContext(nint device, out nint context);

    [DllImport(DllName)]
    public static extern void mxDestroyDevice(nint device);

    [DllImport(DllName)]
    public static extern nuint mxContextCreateBuffer(nint context, AudioFormat* format, byte* data, nuint dataLength);

    [DllImport(DllName)]
    public static extern nuint mxContextCreateSource(nint context);

    [DllImport(DllName)]
    public static extern void mxSourceSubmitBuffer(nint context, nuint source, nuint buffer);

    [DllImport(DllName)]
    public static extern void mxSourcePlay(nint context, nuint source);
}