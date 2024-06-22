using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public unsafe class Context : IDisposable
{
    private readonly nint _context;

    public float MasterVolume
    {
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

    public AudioBuffer CreateBuffer<T>(BufferDescription description, T[] data) where T : unmanaged
        => CreateBuffer(description, new ReadOnlySpan<T>(data));
    
    public AudioBuffer CreateBuffer<T>(BufferDescription description, in ReadOnlySpan<T> data) where T : unmanaged
    {
        nuint buffer;
        fixed (T* pData = data)
            buffer = mxContextCreateBuffer(_context, &description, (byte*) pData, (nuint) (data.Length / sizeof(T)));

        return new AudioBuffer(buffer, _context);
    }

    public AudioSource CreateSource()
    {
        nuint source = mxContextCreateSource(_context);

        return new AudioSource(source, _context);
    }
    
    public void Dispose()
    {
        mxDestroyContext(_context);
    }
}