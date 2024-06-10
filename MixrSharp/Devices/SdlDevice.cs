using static MixrSharp.MixrNative;

namespace MixrSharp.Devices;

public class SdlDevice : Device
{
    public SdlDevice(uint sampleRate, ushort periodSize = 512)
    {
        mxCreateSDLDevice(sampleRate, periodSize, out DevicePointer);
    }
}