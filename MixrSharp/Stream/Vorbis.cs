using System.Text;
using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Vorbis : AudioStream
{
    public unsafe Vorbis(string path)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(path);
        fixed (byte* pBytes = bytes)
            mxStreamLoadVorbis((sbyte*) pBytes, out Stream);
    }
}