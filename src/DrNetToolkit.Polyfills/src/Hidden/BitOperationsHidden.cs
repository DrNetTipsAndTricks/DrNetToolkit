// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DrNetToolkit.Polyfills.Hidden;

#if NETCOREAPP3_0_OR_GREATER
/// <summary>
/// Implementations of <see cref="BitOperations"/> hidden methods.
/// Utility methods for intrinsic bit-twiddling operations.
/// The methods use hardware intrinsics when available on the underlying platform,
/// otherwise they use optimized software fallbacks.
/// </summary>
#else
/// Implementations of BitOperations hidden methods.
/// Utility methods for intrinsic bit-twiddling operations.
/// The methods use hardware intrinsics when available on the underlying platform,
/// otherwise they use optimized software fallbacks.
#endif
public static class BitOperationsHidden
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

#if NETSTANDARD1_1_OR_GREATER
    public static ReadOnlySpan<byte> TrailingZeroCountDeBruijn => // 32
#else
    internal static byte[] TrailingZeroCountDeBruijn => // 32
#endif
    [
        00, 01, 28, 02, 29, 14, 24, 03,
        30, 22, 20, 15, 25, 17, 04, 08,
        31, 27, 13, 23, 21, 19, 16, 07,
        26, 12, 18, 06, 11, 05, 10, 09
    ];


#if NETSTANDARD1_1_OR_GREATER
    public static ReadOnlySpan<byte> Log2DeBruijn => // 32
#else
    private static byte[] Log2DeBruijn => // 32
#endif
    [
        00, 09, 01, 10, 13, 21, 02, 29,
        11, 14, 16, 18, 22, 25, 03, 30,
        08, 12, 20, 28, 15, 17, 24, 07,
        19, 27, 23, 06, 26, 05, 04, 31
    ];

    /// <summary>
    /// Returns the integer (floor) log of the specified value, base 2.
    /// Note that by convention, input value 0 returns 0 since Log(0) is undefined.
    /// Does not directly use any hardware intrinsics, nor does it incur branching.
    /// </summary>
    /// <param name="value">The value.</param>
    [CLSCompliant(false)]
    public static int Log2SoftwareFallback(uint value)
    {
        // No AggressiveInlining due to large method size
        // Has conventional contract 0->0 (Log(0) is undefined)

        // Fill trailing zeros with ones, eg 00010010 becomes 00011111
        value |= value >> 01;
        value |= value >> 02;
        value |= value >> 04;
        value |= value >> 08;
        value |= value >> 16;

        // uint.MaxValue >> 27 is always in range [0 - 31] so we use Unsafe.AddByteOffset to avoid bounds check
        return Unsafe.AddByteOffset(
            // Using deBruijn sequence, k=2, n=5 (2^5=32) : 0b_0000_0111_1100_0100_1010_1100_1101_1101u
#if NETSTANDARD1_1_OR_GREATER
            ref MemoryMarshal.GetReference(Log2DeBruijn),
#else
            ref MemoryMarshalPolyfills.GetArrayDataReference(Log2DeBruijn),
#endif
            // uint|long -> IntPtr cast on 32-bit platforms does expensive overflow checks not needed here
            (IntPtr)(int)((value * 0x07C4ACDDu) >> 27));
    }

    /// <summary>Returns the integer (ceiling) log of the specified value, base 2.</summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static int Log2Ceiling(uint value)
    {
        int result = BitOperationsPolyfills.Log2(value);
        if (BitOperationsPolyfills.PopCount(value) != 1)
        {
            result++;
        }
        return result;
    }

    public static class Crc32Fallback
    {
        // CRC-32 transition table.
        // While this implementation is based on the Castagnoli CRC-32 polynomial (CRC-32C),
        // x32 + x28 + x27 + x26 + x25 + x23 + x22 + x20 + x19 + x18 + x14 + x13 + x11 + x10 + x9 + x8 + x6 + x0,
        // this version uses reflected bit ordering, so 0x1EDC6F41 becomes 0x82F63B78u.
        // This is computed lazily so as to avoid increasing the assembly size for data that's
        // only needed on a fallback path.
        private static readonly uint[] s_crcTable = Crc32ReflectedTableHidden.Generate(0x82F63B78u);

        [CLSCompliant(false)]
        public static uint Crc32C(uint crc, byte data)
        {
            ref uint lookupTable = ref MemoryMarshalPolyfills.GetArrayDataReference(s_crcTable);
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ data)) ^ (crc >> 8);

            return crc;
        }

        [CLSCompliant(false)]
        public static uint Crc32C(uint crc, ushort data)
        {
            ref uint lookupTable = ref MemoryMarshalPolyfills.GetArrayDataReference(s_crcTable);

            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ data)) ^ (crc >> 8);

            return crc;
        }

        [CLSCompliant(false)]
        public static uint Crc32C(uint crc, uint data)
        {
            ref uint lookupTable = ref MemoryMarshalPolyfills.GetArrayDataReference(s_crcTable);
            return Crc32CCore(ref lookupTable, crc, data);
        }

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Crc32CCore(ref uint lookupTable, uint crc, uint data)
        {
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
            crc = Unsafe.Add(ref lookupTable, (nint)(byte)(crc ^ data)) ^ (crc >> 8);

            return crc;
        }
    }

    /// <summary>
    /// Reset the lowest significant bit in the given value
    /// </summary>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ResetLowestSetBit(uint value)
    {
        // It's lowered to BLSR on x86
        return value & (value - 1);
    }

    /// <summary>
    /// Reset specific bit in the given value
    /// Reset the lowest significant bit in the given value
    /// </summary>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ResetLowestSetBit(ulong value)
    {
        // It's lowered to BLSR on x86
        return value & (value - 1);
    }

    /// <summary>
    /// Flip the bit at a specific position in a given value.
    /// Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="index">The zero-based index of the bit to flip.
    /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
    /// <returns>The new value.</returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint FlipBit(uint value, int index)
    {
        return value ^ (1u << index);
    }

    /// <summary>
    /// Flip the bit at a specific position in a given value.
    /// Similar in behavior to the x86 instruction BTC (Bit Test and Complement).
    /// /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="index">The zero-based index of the bit to flip.
    /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
    /// <returns>The new value.</returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong FlipBit(ulong value, int index)
    {
        return value ^ (1ul << index);
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
