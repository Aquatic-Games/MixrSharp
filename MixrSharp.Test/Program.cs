using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MixrSharp;
using MixrSharp.Stream;

if (args.Length < 1)
{
    Console.WriteLine("Please give a filename as the argument.");
    return;
}

AudioStream stream = new Mp3(args[0]);
Console.WriteLine(stream.LengthInSamples / stream.Format.SampleRate);

Device device = new Device(44100);
Context context = device.Context;

byte[] buffer = new byte[48000];

stream.GetBuffer(buffer);
AudioBuffer buffer1 = context.CreateBuffer(buffer);

stream.GetBuffer(buffer);
AudioBuffer buffer2 = context.CreateBuffer(buffer);

AudioBuffer[] buffers = [buffer1, buffer2];
int currentBuffer = 0;

SourceDescription description = new SourceDescription(SourceType.Pcm, stream.Format);

AudioSource source = context.CreateSource(description);
source.SubmitBuffer(buffer1);
source.SubmitBuffer(buffer2);

source.StateChanged += state => Console.WriteLine(state);

source.BufferFinished += () =>
{
    Task.Run(() =>
    {
        ulong numBytes = stream.GetBuffer(buffer);
        
        //if (numBytes == 0)
        //    return;

        if (numBytes == 0)
        {
            stream.SeekToSample(2168470);
            return;
        }

        /*const ulong loopEnd = 4233600;

        if (stream.PositionInSamples > loopEnd)
        {
            Console.WriteLine(stream.PositionInSamples - loopEnd);
            ulong samplesToChop = stream.PositionInSamples - loopEnd;
            int bytesToChop = (int) samplesToChop * stream.Format.BytesPerSample * stream.Format.Channels;
            stream.SeekToSample(650916);
            buffer = buffer[..^bytesToChop];
        }*/

        buffers[currentBuffer].Update(buffer);
        source.SubmitBuffer(buffers[currentBuffer]);

        currentBuffer++;
        if (currentBuffer >= buffers.Length)
            currentBuffer = 0;
    });
};

//source.Speed = 4;
source.Play();

Console.WriteLine(source.Speed);
Console.WriteLine(source.Volume);
Console.WriteLine(source.Looping);
Console.WriteLine(source.Panning);
Console.WriteLine(source.ChannelVolumes);

while (source.State == SourceState.Playing)
{
    AudioFormat fmt = stream.Format;

    int fmtDivisor = fmt.DataType switch
    {
        DataType.U8 => 1,
        DataType.I16 => 2,
        DataType.I32 => 4,
        DataType.F32 => 4,
        _ => throw new ArgumentOutOfRangeException()
    };
    
    ulong totalSamples = stream.PositionInSamples + source.Position;
    ulong currentSecond = totalSamples / fmt.SampleRate;
    
    Console.WriteLine($"{currentSecond / 60:00}:{currentSecond % 60:00}");
    Thread.Sleep(1000);
}

device.Dispose();
stream.Dispose();