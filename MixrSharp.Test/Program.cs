using System;
using System.IO;
using System.Threading;
using MixrSharp;
using MixrSharp.Devices;
using static MixrSharp.MixrNative;

ReadOnlySpan<byte> path = @"C:\Users\ollie\Music\DEADLOCK.wav"u8;
nint stream;
AudioFormat format;
byte[] data;

unsafe
{
    fixed (byte* pPath = path)
        mxStreamLoadWav((sbyte*) pPath, out stream);

    format = mxStreamGetFormat(stream);

    nuint dataLength;
    mxStreamGetPCM(stream, null, &dataLength);

    data = new byte[dataLength];
    fixed (byte* pData = data)
        mxStreamGetPCM(stream, pData, &dataLength);
    
    mxDestroyStream(stream);
}

Device device = new SdlDevice(48000);
Context context = device.Context;
//context.MasterVolume = 0.1f;

AudioBuffer buffer = context.CreateBuffer(format, data);

AudioSource source = context.CreateSource();
source.SubmitBuffer(buffer);
//source.Speed = 0.85;
//source.Volume = 0.5f;
source.Looping = true;
source.Play();

while (true)
{
    Thread.Sleep(1000);
}

device.Dispose();
