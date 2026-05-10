using System;
using Silk.NET.SDL;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public sealed unsafe class Device : IDisposable
{
    private readonly Sdl _sdl;
    private readonly uint _audioDevice;
    private readonly Context _context;

    public Context Context => _context;

    public Device(uint sampleRate)
    {
        _sdl = Sdl.GetApi();
        if (_sdl.Init(Sdl.InitAudio) < 0)
            throw new Exception($"Failed to initialize SDL: {_sdl.GetErrorS()}");

        AudioSpec spec = new()
        {
            Channels = 2,
            Format = Sdl.AudioF32,
            Freq = (int) sampleRate,
            Samples = 512,
            Callback = new PfnAudioCallback(AudioCallback)
        };

        _audioDevice = _sdl.OpenAudioDevice((byte*) null, 0, &spec, null, 0);
        if (_audioDevice == 0)
            throw new Exception($"Failed to open audio device: {_sdl.GetErrorS()}");

        _context = new Context(sampleRate);
        _sdl.PauseAudioDevice(_audioDevice, 0);
    }

    public void Dispose()
    {
        _sdl.CloseAudioDevice(_audioDevice);
        _context.Dispose();
        _sdl.Dispose();
    }
    
    private void AudioCallback(void* arg0, byte* arg1, int arg2)
    {
        float* floatBuf = (float*) arg1;
        int bufLen = arg2 / 4;
        _context.MixToStereoF32Buffer(new Span<float>(floatBuf, bufLen));
    }
}