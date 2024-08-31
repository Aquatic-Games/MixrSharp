using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

if (args.Length < 1)
{
    Console.WriteLine("Please give a filename as the argument.");
    return;
}

Vorbis stream = new Vorbis(args[0]);

Device device = new SdlDevice(48000);
Context context = device.Context;

byte[] buffer = new byte[stream.Format.SampleRate];

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

ulong totalBytes = 0;

source.BufferFinished += () =>
{
    Task.Run(() =>
    {
        ulong numBytes = stream.GetBuffer(buffer);
        totalBytes += numBytes;

        // Console.WriteLine($"Buffer returned {numBytes} bytes.");

        if (numBytes == 0)
            return;

        buffers[currentBuffer].Update(buffer);
        source.SubmitBuffer(buffers[currentBuffer]);

        currentBuffer++;
        if (currentBuffer >= buffers.Length)
            currentBuffer = 0;
    });
};

//source.Speed = 2;
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

    fmtDivisor *= fmt.Channels;
    
    ulong totalSamples = (totalBytes / (ulong) fmtDivisor) + source.Position;
    ulong currentSecond = totalSamples / fmt.SampleRate;
    
    Console.WriteLine($"{currentSecond / 60:00}:{currentSecond % 60:00}");
    Thread.Sleep(1000);
}

device.Dispose();
stream.Dispose();