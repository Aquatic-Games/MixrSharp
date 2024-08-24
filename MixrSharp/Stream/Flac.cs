using System.Text;
using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Flac : AudioStream
{
    public unsafe Flac(string path)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(path);
        fixed (byte* pBytes = bytes)
            mxStreamLoadFlac((sbyte*) pBytes, out Stream);
    }
}