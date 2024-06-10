using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

using Wav wav = new Wav(@"C:\Users\ollie\Documents\Audacity\18 Ghetto Heaven.wav");

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

AudioBuffer buffer = context.CreateBuffer(wav.Format, wav.GetPcm());

AudioSource source = context.CreateSource();
source.SubmitBuffer(buffer);
//source.Speed = 0.85;
//source.Volume = 0.5f;
source.Looping = true;
//source.Panning = -1.0f;
source.Play();

bool test = false;

while (true)
{
    Thread.Sleep(1000);
    
    /*source.Stop();
    source.Play();*/
    
    /*if (test)
        source.Play();
    else
        source.Pause();

    test = !test;*/
}

device.Dispose();
