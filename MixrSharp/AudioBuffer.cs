using System;

namespace MixrSharp;

public struct AudioBuffer : IDisposable
{
    private readonly nint _context;
    
    public readonly nuint ID;

    internal AudioBuffer(nuint id, nint context)
    {
        ID = id;
        _context = context;
    }
    
    public void Dispose()
    {
        
    }
}