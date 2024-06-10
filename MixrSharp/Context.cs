using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public class Context : IDisposable
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
    
    public void Dispose()
    {
        mxDestroyContext(_context);
    }
}