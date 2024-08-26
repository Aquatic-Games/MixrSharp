using System;
using System.Runtime.InteropServices;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public class AudioSource : IDisposable
{
    public event OnBufferFinished BufferFinished = delegate { };
    
    private readonly nint _context;
    private GCHandle _cbHandle;
    private bool _isDisposed;
    
    public readonly nuint ID;

    public double Speed
    {
        get => mxSourceGetSpeed(_context, ID);
        set => mxSourceSetSpeed(_context, ID, value);
    }

    public float Volume
    {
        get => mxSourceGetVolume(_context, ID);
        set => mxSourceSetVolume(_context, ID, value);
    }

    public bool Looping
    {
        get => mxSourceGetLooping(_context, ID);
        set => mxSourceSetLooping(_context, ID, value);
    }

    public float Panning
    {
        get => mxSourceGetPanning(_context, ID);
        set => mxSourceSetPanning(_context, ID, value);
    }

    public (float l, float r) ChannelVolumes
    {
        get
        {
            unsafe
            {
                float l, r;
                mxSourceGetChannelVolumes(_context, ID, &l, &r);

                return (l, r);
            }
        }
        
        set => mxSourceSetChannelVolumes(_context, ID, value.l, value.r);
    }

    public SourceState State => mxSourceGetState(_context, ID);

    public ulong Position => mxSourceGetPositionSamples(_context, ID);

    public double PositionSecs => mxSourceGetPositionSeconds(_context, ID);

    internal unsafe AudioSource(UIntPtr id, IntPtr context)
    {
        ID = id;
        _context = context;

        SourceBufferFinishedCallback cb = BufferFinishedCallback;
        _cbHandle = GCHandle.Alloc(cb);

        mxSourceSetBufferFinishedCallback(_context, id,
            (delegate*<void*, void>) Marshal.GetFunctionPointerForDelegate(cb), null);
    }

    ~AudioSource()
    {
        Dispose();
    }

    public void SubmitBuffer(AudioBuffer buffer)
    {
        mxSourceSubmitBuffer(_context, ID, buffer.ID);
    }

    public void ClearBuffers()
    {
        mxSourceClearBuffers(_context, ID);
    }

    public void Play()
    {
        mxSourcePlay(_context, ID);
    }

    public void Pause()
    {
        mxSourcePause(_context, ID);
    }

    public void Stop()
    {
        mxSourceStop(_context, ID);
    }
    
    private unsafe void BufferFinishedCallback(void* userData)
    {
        BufferFinished();
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        BufferFinished = null;
        _cbHandle.Free();
        mxContextDestroySource(_context, ID);
    }

    public delegate void OnBufferFinished();
}