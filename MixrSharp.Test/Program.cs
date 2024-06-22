using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

using Wav wav = new Wav(@"C:\Users\ollie\Documents\Audacity\test.wav");

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

BufferDescription description = new BufferDescription(PcmType.Pcm, wav.Format);

if (wav.IsAdpcm)
{
    description.Type = PcmType.Adpcm;
    description.Adpcm = new AdpcmDescription(wav.AdpcmInfo.ChunkSize);
}

AudioBuffer buffer = context.CreateBuffer(description, wav.GetPcm());

AudioSource source = context.CreateSource();
source.SubmitBuffer(buffer);

//source.ClearBuffers();

//source.SubmitBuffer(buffer);
//source.Speed = 1.15;
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
