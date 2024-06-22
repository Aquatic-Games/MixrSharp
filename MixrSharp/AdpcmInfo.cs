namespace MixrSharp;

public struct AdpcmInfo
{
    public AdpcmType Type;
    
    public ulong ChunkSize;

    public AdpcmInfo(AdpcmType type, ulong chunkSize)
    {
        Type = type;
        ChunkSize = chunkSize;
    }
}