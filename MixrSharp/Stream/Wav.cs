using static MixrSharp.MixrNative;

namespace MixrSharp.Stream;

public class Wav : AudioStream
{
    public bool IsAdpcm => mxWavIsADPCM(Stream);

    public AdpcmInfo AdpcmInfo => mxWavGetADPCMInfo(Stream);
    
    public Wav(string path)
    {
        mxStreamLoadWav(path, out Stream);
    }
}