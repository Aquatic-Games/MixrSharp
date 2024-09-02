using System.Text;
using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Mp3 : AudioStream
{
    public unsafe Mp3(string path)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(path);
        fixed (byte* pBytes = bytes)
            mxStreamLoadMp3((sbyte*) pBytes, out Stream);
    }
}