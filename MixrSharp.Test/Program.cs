using System.IO;
using System.Threading;
using MixrSharp;
using MixrSharp.Devices;

byte[] data = File.ReadAllBytes(@"C:\Users\ollie\Music\TESTFILES\Feeling-16bitshort.raw");

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

AudioFormat format = new AudioFormat(DataType.I16, 44100, Channels.Stereo);

AudioBuffer buffer = context.CreateBuffer(format, data);

AudioSource source = context.CreateSource();
source.SubmitBuffer(buffer);
//source.Speed = 0.85;
//source.Volume = 0.5f;
source.Looping = true;
source.Play();

while (true)
{
    Thread.Sleep(1000);
}

device.Dispose();
