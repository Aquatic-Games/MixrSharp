using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Mp3 : AudioStream
{
    public Mp3(string path)
    {
        mxStreamLoadMp3(path, out Stream);
    }
}