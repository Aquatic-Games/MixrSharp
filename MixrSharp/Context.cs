using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public unsafe class Context : IDisposable
{
    private readonly nint _context;

    public float MasterVolume
    {
        get => mxContextGetMasterVolume(_context);
        set => mxContextSetMasterVolume(_context, value);
    }

    public Context(uint sampleRate)
    {
        mxCreateContext(sampleRate, out _context);
    }

    internal Context(nint context)
    {
        _context = context;
    }

    public AudioBuffer CreateBuffer<T>(T[] data) where T : unmanaged
        => CreateBuffer(new ReadOnlySpan<T>(data));
    
    public AudioBuffer CreateBuffer<T>(in ReadOnlySpan<T> data) where T : unmanaged
    {
        nuint buffer;
        fixed (T* pData = data)
            buffer = mxContextCreateBuffer(_context, (byte*) pData, (nuint) (data.Length / sizeof(T)));

        return new AudioBuffer(buffer, _context);
    }

    public AudioSource CreateSource(SourceDescription description)
    {
        nuint source = mxContextCreateSource(_context, &description);

        return new AudioSource(source, _context);
    }

    public void MixToStereoF32Buffer(Span<float> buffer)
    {
        fixed (float* pBuffer = buffer)
            mxContextMixToStereoF32Buffer(_context, pBuffer, (nuint) buffer.Length);
    }
    
    public void Dispose()
    {
        mxDestroyContext(_context);
    }
}