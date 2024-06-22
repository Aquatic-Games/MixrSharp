namespace MixrSharp;

public struct AdpcmDescription
{
    public ulong ChunkSize;

    public AdpcmDescription(ulong chunkSize)
    {
        ChunkSize = chunkSize;
    }
}