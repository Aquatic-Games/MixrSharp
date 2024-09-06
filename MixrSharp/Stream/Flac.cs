using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Flac : AudioStream
{
    public Flac(string path)
    {
        mxStreamLoadFlac(path, out Stream);
    }
}