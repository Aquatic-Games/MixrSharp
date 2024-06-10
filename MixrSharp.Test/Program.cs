using System.IO;
using System.Threading;
using MixrSharp;
using static MixrSharp.MixrNative;

byte[] data = File.ReadAllBytes(@"C:\Users\ollie\Music\TESTFILES\Feeling-16bitshort.raw");

unsafe
{
    mxCreateSDLDevice(48000, 512, out nint device);
    mxDeviceGetContext(device, out nint context);

    AudioFormat format = new AudioFormat(DataType.I16, 44100, Channels.Stereo);

    nuint buffer;
    fixed (byte* pData = data)
        buffer = mxContextCreateBuffer(context, &format, pData, (nuint) data.Length);

    nuint source = mxContextCreateSource(context);
    mxSourceSubmitBuffer(context, source, buffer);
    mxSourcePlay(context, source);
        
    while (true)
    {
        Thread.Sleep(1000);
    }

    mxDestroyDevice(device);
}