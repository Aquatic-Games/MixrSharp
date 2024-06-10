using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

string[] files =
{
    @"C:\Users\ollie\Music\High Times - Singles 1992-2006\01 - When You Gonna Learn_.wav",
    @"C:\Users\ollie\Music\High Times - Singles 1992-2006\02 - Too Young to Die (Remastered).wav",
    @"C:\Users\ollie\Music\High Times - Singles 1992-2006\11 - Canned Heat (Remastered).wav"
};

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

AudioSource source = context.CreateSource();

foreach (string file in files)
{
    using Wav wav = new Wav(file);
    AudioBuffer buffer = context.CreateBuffer(wav.Format, wav.GetPcm());
    source.SubmitBuffer(buffer);
}

//source.ClearBuffers();

//source.SubmitBuffer(buffer);
//source.Speed = 10;
//source.Volume = 0.5f;
//source.Looping = true;
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
