using System.Text;
using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Wav : AudioStream
{
    public unsafe Wav(string path)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(path);
        fixed (byte* pBytes = bytes)
            mxStreamLoadWav((sbyte*) pBytes, out Stream);
    }
}