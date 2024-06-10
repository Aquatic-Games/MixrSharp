using System.IO;
using System.Threading;
using MixrSharp;
using MixrSharp.Devices;

byte[] data = File.ReadAllBytes(@"C:\Users\ollie\Music\TESTFILES\Feeling-16bitshort.raw");

Device device = new SdlDevice(48000);
Context context = device.Context;

while (true)
{
    Thread.Sleep(1000);
}

device.Dispose();
