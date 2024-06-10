﻿using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public struct AudioSource : IDisposable
{
    private readonly nint _context;
    
    public readonly nuint ID;

    internal AudioSource(UIntPtr id, IntPtr context)
    {
        ID = id;
        _context = context;
    }

    public void SubmitBuffer(AudioBuffer buffer)
    {
        mxSourceSubmitBuffer(_context, ID, buffer.ID);
    }

    public void Play()
    {
        mxSourcePlay(_context, ID);
    }

    public void Dispose()
    {
        
    }
}