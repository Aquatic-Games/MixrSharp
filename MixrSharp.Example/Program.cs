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

const bool useStreaming = true;

// Create an MP3 stream. You can also create FLAC, WAV, and Vorbis.
AudioStream stream = new Mp3(args[0]);
Console.WriteLine(stream.LengthInSamples / stream.Format.SampleRate);

// Create our device and get its context.
Device device = new Device(44100);
Context context = device.Context;

AudioSource source;

// ------------------------------------- BASIC USAGE -------------------------------------
// This shows how to load data into a source.
// This is the easiest method, but I advise you use the streaming route below.

if (!useStreaming)
{
    // Get the song data as a full buffer of PCM.
    byte[] songData = stream.GetPcm();
    AudioBuffer buffer = context.CreateBuffer(songData);
    source = context.CreateSource(new SourceDescription(SourceType.Pcm, stream.Format));
    source.SubmitBuffer(buffer);
}

// ------------------------------------- STREAMING -------------------------------------

else
{
    // The song buffer used for streaming.
    byte[] buffer = new byte[48000];

    // Streaming setup.
    // Load part of the stream into our buffer, then create a mixr-owned buffer to get the data.
    stream.GetBuffer(buffer);
    AudioBuffer buffer1 = context.CreateBuffer(buffer);

    // Create a second buffer.
    stream.GetBuffer(buffer);
    AudioBuffer buffer2 = context.CreateBuffer(buffer);

    AudioBuffer[] buffers = [buffer1, buffer2];
    int currentBuffer = 0;

    SourceDescription description = new SourceDescription(SourceType.Pcm, stream.Format);

    // Create a source from our two buffers.
    source = context.CreateSource(description);
    source.SubmitBuffer(buffer1);
    source.SubmitBuffer(buffer2);

    // A callback that is called when the source's state changed.
    source.StateChanged += state => Console.WriteLine(state);

    // A callback that is called when the source's current buffer has been used up.
    source.BufferFinished += () =>
    {
        Task.Run(() =>
        {
            // Fill the song buffer with some more of the stream.
            ulong numBytes = stream.GetBuffer(buffer);

            // Exit if there is nothing left in the stream (it is finished)
            if (numBytes == 0)
            {
                return;
            }

            // Update the current audio buffer and submit it back to the source.
            buffers[currentBuffer].Update(buffer);
            source.SubmitBuffer(buffers[currentBuffer]);

            // Alternate between buffers to ensure a fresh supply of data.
            currentBuffer++;
            if (currentBuffer >= buffers.Length)
                currentBuffer = 0;
        });
    };
}

// Set some parameters, you don't need to set these, this is just an example.
source.Speed = 1;
source.Volume = 1;
source.Play();

Console.WriteLine(source.Speed);
Console.WriteLine(source.Volume);
Console.WriteLine(source.Looping);
Console.WriteLine(source.Panning);
Console.WriteLine(source.ChannelVolumes);

// Continue running until the song is finished.
while (source.State == SourceState.Playing)
{
    AudioFormat fmt = stream.Format;
    
    ulong totalSamples = stream.PositionInSamples + source.Position;
    ulong currentSecond = totalSamples / fmt.SampleRate;
    
    Console.WriteLine($"{currentSecond / 60:00}:{currentSecond % 60:00}");
    Thread.Sleep(1000);
}

device.Dispose();
stream.Dispose();