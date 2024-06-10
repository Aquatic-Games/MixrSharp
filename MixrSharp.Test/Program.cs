using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

using Wav wav = new Wav(@"C:\Users\ollie\Music\High Times - Singles 1992-2006\09 - High Times (Remastered).wav");

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

AudioBuffer buffer = context.CreateBuffer(wav.Format, wav.GetPcm());

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
