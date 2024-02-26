// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.


using System;

namespace DrNetToolkit.Polyfills.Hidden;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public static class Crc32ReflectedTableHidden
{
    [CLSCompliant(false)]
    public static uint[] Generate(uint reflectedPolynomial)
    {
        uint[] table = new uint[256];

        for (int i = 0; i < 256; i++)
        {
            uint val = (uint)i;

            for (int j = 0; j < 8; j++)
            {
                if ((val & 0b0000_0001) == 0)
                {
                    val >>= 1;
                }
                else
                {
                    val = (val >> 1) ^ reflectedPolynomial;
                }
            }

            table[i] = val;
        }

        return table;
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
