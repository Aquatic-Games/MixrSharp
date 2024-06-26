﻿using System.Runtime.InteropServices;

namespace MixrSharp;

[StructLayout(LayoutKind.Explicit)]
public struct BufferDescription
{
    [FieldOffset(0)] public PcmType Type;

    [FieldOffset(4)] public AudioFormat Format;

    [FieldOffset(16)] public AdpcmDescription Adpcm;

    public BufferDescription(PcmType type, AudioFormat format)
    {
        Type = type;
        Format = format;
    }
}