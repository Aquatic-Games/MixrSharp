namespace MixrSharp;

public struct AudioFormat
{
    public DataType DataType;
    public uint SampleRate;
    public byte Channels;

    public AudioFormat(DataType dataType, uint sampleRate, byte channels)
    {
        DataType = dataType;
        SampleRate = sampleRate;
        Channels = channels;
    }
}