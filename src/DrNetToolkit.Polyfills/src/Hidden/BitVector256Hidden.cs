// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Polyfills.Hidden;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public unsafe struct BitVector256Hidden
{
    private fixed uint _values[8];

    public readonly BitVector256Hidden CreateInverse()
    {
        BitVector256Hidden inverse = default;

        for (int i = 0; i < 8; i++)
        {
            inverse._values[i] = ~_values[i];
        }

        return inverse;
    }

    public void Set(int c)
    {
        Debug.Assert(c < 256);
        uint offset = (uint)(c >> 5);
        uint significantBit = 1u << c;
        _values[offset] |= significantBit;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains128(char c) =>
        c < 128 && ContainsUnchecked(c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains256(char c) =>
        c < 256 && ContainsUnchecked(c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Contains(byte b) =>
        ContainsUnchecked(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private readonly bool ContainsUnchecked(int b)
    {
        Debug.Assert(b < 256);
        uint offset = (uint)(b >> 5);
        uint significantBit = 1u << b;
        return (_values[offset] & significantBit) != 0;
    }

    public readonly char[] GetCharValues()
    {
        var chars = new List<char>();
        for (int i = 0; i < 256; i++)
        {
            if (ContainsUnchecked(i))
            {
                chars.Add((char)i);
            }
        }
        return chars.ToArray();
    }

    public readonly byte[] GetByteValues()
    {
        var bytes = new List<byte>();
        for (int i = 0; i < 256; i++)
        {
            if (ContainsUnchecked(i))
            {
                bytes.Add((byte)i);
            }
        }
        return bytes.ToArray();
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
