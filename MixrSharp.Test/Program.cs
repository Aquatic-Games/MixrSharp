using static MixrSharp.MixrNative;

mxCreateContext(48000, out nint context);

mxDestroyContext(context);