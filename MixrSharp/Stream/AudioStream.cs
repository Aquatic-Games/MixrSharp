﻿using System;
using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public abstract class AudioStream : IDisposable
{
    protected nint Stream;

    public AudioFormat Format => mxStreamGetFormat(Stream);
    
    public ulong PcmLengthInBytes => mxStreamGetPCMLengthInBytes(Stream);

    public unsafe ulong GetBuffer(Span<byte> buffer)
    {
        fixed (byte* pBuffer = buffer)
            return mxStreamGetBuffer(Stream, pBuffer, (nuint) buffer.Length);
    }

    public void Restart()
        => mxStreamRestart(Stream);

    public unsafe byte[] GetPcm()
    {
        nuint dataLength;
        mxStreamGetPCM(Stream, null, &dataLength);

        byte[] data = new byte[dataLength];
        fixed (byte* pData = data)
            mxStreamGetPCM(Stream, pData, &dataLength);

        return data;
    }
    
    public void Dispose()
    {
        mxDestroyStream(Stream);
    }
}