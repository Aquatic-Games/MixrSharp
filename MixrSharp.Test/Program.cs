using System;
using System.Diagnostics;
using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using MixrSharp.Stream;

if (args.Length < 1)
{
    Console.WriteLine("Please give a filename as the argument.");
    return;
}

Flac stream = new Flac(args[0]);

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
};

source.Play();

while (source.State == SourceState.Playing)
{
    AudioFormat fmt = stream.Format;
    ulong totalSamples = (totalBytes / 4) + source.Position;
    ulong currentSecond = totalSamples / fmt.SampleRate;
    
    Console.WriteLine($"{currentSecond / 60:00}:{currentSecond % 60:00}");
    Thread.Sleep(1000);
}

device.Dispose();
stream.Dispose();