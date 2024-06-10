﻿using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public unsafe class Context : IDisposable
{
    private readonly nint _context;

    public Context(uint sampleRate)
    {
        mxCreateContext(sampleRate, out _context);
    }

    internal Context(nint context)
    {
        _context = context;
    }

    public AudioBuffer CreateBuffer<T>(AudioFormat format, T[] data) where T : unmanaged
        => CreateBuffer(format, new ReadOnlySpan<T>(data));
    
    public AudioBuffer CreateBuffer<T>(AudioFormat format, in ReadOnlySpan<T> data) where T : unmanaged
    {
        nuint buffer;
        fixed (T* pData = data)
            buffer = mxContextCreateBuffer(_context, &format, (byte*) pData, (nuint) (data.Length / sizeof(T)));

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