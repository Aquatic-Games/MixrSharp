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
    public static extern nuint mxContextCreateBuffer(nint context, byte* data, nuint dataLength);

    [DllImport(DllName)]
    public static extern nuint mxContextCreateSource(nint context, SourceDescription* description);

    [DllImport(DllName)]
    public static extern void mxContextSetMasterVolume(nint context, float volume);

    [DllImport(DllName)]
    public static extern void mxSourceSubmitBuffer(nint context, nuint source, nuint buffer);
    
    [DllImport(DllName)]
    public static extern void mxSourceClearBuffers(nint context, nuint source);

    [DllImport(DllName)]
    public static extern void mxSourcePlay(nint context, nuint source);
    
    [DllImport(DllName)]
    public static extern void mxSourcePause(nint context, nuint source);

    [DllImport(DllName)]
    public static extern void mxSourceStop(nint context, nuint source);

    [DllImport(DllName)]
    public static extern void mxSourceSetSpeed(nint context, nuint source, double speed);
    
    [DllImport(DllName)]
    public static extern void mxSourceSetVolume(nint context, nuint source, float volume);
    
    [DllImport(DllName)]
    public static extern void mxSourceSetLooping(nint context, nuint source, bool looping);
    
    [DllImport(DllName)]
    public static extern void mxSourceSetPanning(nint context, nuint source, float panning);

    [DllImport(DllName)]
    public static extern void mxSourceSetChannelVolumes(nint context, nuint source, float volumeL, float volumeR);

    [DllImport(DllName)]
    public static extern AudioFormat mxStreamGetFormat(nint stream);

    [DllImport(DllName)]
    public static extern void mxStreamGetPCM(nint stream, byte* data, nuint* dataLength);

    [DllImport(DllName)]
    public static extern void mxDestroyStream(nint stream);

    [DllImport(DllName)]
    public static extern void mxStreamLoadWav(sbyte* path, out nint audioStream);

    [DllImport(DllName)]
    public static extern bool mxWavIsADPCM(nint stream);

    [DllImport(DllName)]
    public static extern AdpcmInfo mxWavGetADPCMInfo(nint stream);
}