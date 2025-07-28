using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public sealed class Device : IDisposable
{
    private readonly nint _devicePointer;

    public Context Context
    {
        get
        {
            mxDeviceGetContext(_devicePointer, out nint context);
            return new Context(context);
        }
    }

    public Device(uint sampleRate)
    {
        mxCreateDevice(sampleRate, out _devicePointer);
    }
    
    public void Dispose()
    {
        mxDestroyDevice(_devicePointer);
    }
}