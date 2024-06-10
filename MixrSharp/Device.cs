using System;
using static MixrSharp.MixrNative;

namespace MixrSharp;

public abstract class Device : IDisposable
{
    protected nint DevicePointer;

    public Context Context
    {
        get
        {
            mxDeviceGetContext(DevicePointer, out nint context);
            return new Context(context);
        }
    }
    
    public virtual void Dispose()
    {
        mxDestroyDevice(DevicePointer);
    }
}