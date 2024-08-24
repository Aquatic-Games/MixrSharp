using System;
using System.Diagnostics;
using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

using Flac stream = new Flac(@"C:\Users\ollie\Music\Music\Various Artists\NOW Millennium- 2000 - 2001 (Disc 2)\08 Steal My Sunshine (Single Version).flac");

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

SourceDescription description = new SourceDescription(SourceType.Pcm, stream.Format);

/*if (stream.IsAdpcm)
{
    description.Type = SourceType.Adpcm;
    description.Adpcm = new AdpcmDescription(stream.AdpcmInfo.ChunkSize);
}*/

Stopwatch sw = Stopwatch.StartNew();
AudioBuffer buffer = context.CreateBuffer(stream.GetPcm());
Console.WriteLine(sw.Elapsed);
sw.Stop();

AudioSource source = context.CreateSource(description);
source.SubmitBuffer(buffer);

//source.ClearBuffers();

//source.SubmitBuffer(buffer);
//source.Speed = 2;
//source.Volume = 0.5f;
//source.Looping = true;
//source.Panning = -1.0f;
//source.SetChannelVolumes(-1, 1);
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
