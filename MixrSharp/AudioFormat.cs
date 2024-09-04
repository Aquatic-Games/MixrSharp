using System;

namespace MixrSharp;

public struct AudioFormat
{
    public DataType DataType;
    public uint SampleRate;
    public byte Channels;

    public byte BitsPerSample => DataType switch
    {
        DataType.I8 => 8,
        DataType.U8 => 8,
        DataType.I16 => 16,
        DataType.I32 => 32,
        DataType.F32 => 32,
        _ => throw new ArgumentOutOfRangeException()
    };

    public byte BytesPerSample => (byte) (BitsPerSample / 8);

    public AudioFormat(DataType dataType, uint sampleRate, byte channels)
    {
        DataType = dataType;
        SampleRate = sampleRate;
        Channels = channels;
    }
}