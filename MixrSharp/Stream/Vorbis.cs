using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Vorbis : AudioStream
{
    public Vorbis(string path)
    {
        mxStreamLoadVorbis(path, out Stream);
    }
}