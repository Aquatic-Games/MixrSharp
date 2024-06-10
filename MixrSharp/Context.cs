using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public class Context : IDisposable
{
    private nint _context;

    public Context(uint sampleRate)
    {
        mxCreateContext(sampleRate, out _context);
    }

    public Context(IntPtr context)
    {
        _context = context;
    }

    public void Dispose()
    {
        mxDestroyContext(_context);
    }
}