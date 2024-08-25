using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public class AudioBuffer : IDisposable
{
    private readonly nint _context;
    private bool _isDisposed;
    
    public readonly nuint ID;

    internal AudioBuffer(nuint id, nint context)
    {
        ID = id;
        _context = context;
    }

    ~AudioBuffer()
    {
        Dispose();
    }

    public void Update<T>(T[] data) where T : unmanaged
        => Update(new ReadOnlySpan<T>(data));
    
    public unsafe void Update<T>(in ReadOnlySpan<T> data) where T : unmanaged
    {
        fixed (void* pData = data)
            mxContextUpdateBuffer(_context, ID, (byte*) pData, (nuint) (data.Length / sizeof(T)));
    }
    
    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;
        
        mxContextDestroyBuffer(_context, ID);
    }
}