// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Polyfills.Hidden;

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
#endif

namespace System.Numerics;

#if NETCOREAPP3_0_OR_GREATER
/// <summary>
/// <see cref="BitOperations"/> polyfills.
/// Utility methods for intrinsic bit-twiddling operations.
/// The methods use hardware intrinsics when available on the underlying platform,
/// otherwise they use optimized software fallbacks.
/// </summary>
#else
/// BitOperations polyfills.
/// Utility methods for intrinsic bit-twiddling operations.
/// The methods use hardware intrinsics when available on the underlying platform,
/// otherwise they use optimized software fallbacks.
#endif
public static class BitOperationsPolyfills
{
    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(int value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(int value) => (value & (value - 1)) == 0 && value > 0;
#endif

    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(uint value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(uint value) => (value & (value - 1)) == 0 && value != 0;
#endif

    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(long value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(long value) => (value & (value - 1)) == 0 && value > 0;
#endif

    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(ulong value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(ulong value) => (value & (value - 1)) == 0 && value != 0;
#endif

    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(nint value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(nint value) => (value & (value - 1)) == 0 && value > 0;
#endif

    /// <summary>
    /// Evaluate whether a given integral value is a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static bool IsPow2(nuint value) => BitOperations.IsPow2(value);
#else
    public static bool IsPow2(nuint value) => (value & (value - 1)) == 0 && value != 0;
#endif

    /// <summary>Round the given integral value up to a power of 2.</summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
    /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static uint RoundUpToPowerOf2(uint value) => BitOperations.RoundUpToPowerOf2(value);
#else
    public static uint RoundUpToPowerOf2(uint value)
    {
        // Based on https://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
        --value;
        value |= value >> 1;
        value |= value >> 2;
        value |= value >> 4;
        value |= value >> 8;
        value |= value >> 16;
        return value + 1;
    }
#endif

    /// <summary>
    /// Round the given integral value up to a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
    /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static ulong RoundUpToPowerOf2(ulong value) => BitOperations.RoundUpToPowerOf2(value);
#else
    public static ulong RoundUpToPowerOf2(ulong value)
    {
        // Based on https://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
        --value;
        value |= value >> 1;
        value |= value >> 2;
        value |= value >> 4;
        value |= value >> 8;
        value |= value >> 16;
        value |= value >> 32;
        return value + 1;
    }
#endif

    /// <summary>
    /// Round the given integral value up to a power of 2.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The smallest power of 2 which is greater than or equal to <paramref name="value"/>.
    /// If <paramref name="value"/> is 0 or the result overflows, returns 0.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static unsafe nuint RoundUpToPowerOf2(nuint value)
    {
        //#if TARGET_64BIT
        //        return (nuint)RoundUpToPowerOf2((ulong)value);
        //#else
        //        return (nuint)RoundUpToPowerOf2((uint)value);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return (nuint)RoundUpToPowerOf2((ulong)value);
        else
            return (nuint)RoundUpToPowerOf2((uint)value);
    }

    /// <summary>
    /// Returns the integer (floor) log of the specified value, base 2.
    /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int Log2(uint value)
        => BitOperations.Log2(value);
#else
    public static int Log2(uint value)
    {
        // The 0->0 contract is fulfilled by setting the LSB to 1.
        // Log(1) is 0, and setting the LSB for values > 1 does not change the log2 result.
        value |= 1;

        // Fallback contract is 0->0
        return BitOperationsHidden.Log2SoftwareFallback(value);
    }
#endif

    /// <summary>
    /// Returns the integer (floor) log of the specified value, base 2.
    /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int Log2(ulong value)
        => BitOperations.Log2(value);
#else
    public static int Log2(ulong value)
    {
        value |= 1;

        uint hi = (uint)(value >> 32);

        if (hi == 0)
        {
            return Log2((uint)value);
        }

        return 32 + Log2(hi);
    }
#endif

    /// <summary>
    /// Returns the integer (floor) log of the specified value, base 2.
    /// Note that by convention, input value 0 returns 0 since log(0) is undefined.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int Log2(nuint value)
        => BitOperations.Log2(value);
#else
    public static unsafe int Log2(nuint value)
    {
        //#if TARGET_64BIT
        //        return Log2((ulong)value);
        //#else
        //        return Log2((uint)value);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return Log2((ulong)value);
        else
            return Log2((uint)value);
    }
#endif

    /// <summary>
    /// Returns the population count (number of bits set) of a mask.
    /// Similar in behavior to the x86 instruction POPCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int PopCount(uint value)
        => BitOperations.PopCount(value);
#else
    public static int PopCount(uint value)
    {
        return SoftwareFallback(value);

        static int SoftwareFallback(uint value)
        {
            const uint c1 = 0x_55555555u;
            const uint c2 = 0x_33333333u;
            const uint c3 = 0x_0F0F0F0Fu;
            const uint c4 = 0x_01010101u;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 24;

            return (int)value;
        }
    }
#endif

    /// <summary>
    /// Returns the population count (number of bits set) of a mask.
    /// Similar in behavior to the x86 instruction POPCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int PopCount(ulong value)
        => BitOperations.PopCount(value);
#else
    public static unsafe int PopCount(ulong value)
    {
        //#if TARGET_32BIT
        if (sizeof(nuint) == sizeof(uint))
            return PopCount((uint)value) // lo
                + PopCount((uint)(value >> 32)); // hi
                                                 //#else
        else
            return SoftwareFallback(value);

        static int SoftwareFallback(ulong value)
        {
            const ulong c1 = 0x_55555555_55555555ul;
            const ulong c2 = 0x_33333333_33333333ul;
            const ulong c3 = 0x_0F0F0F0F_0F0F0F0Ful;
            const ulong c4 = 0x_01010101_01010101ul;

            value -= (value >> 1) & c1;
            value = (value & c2) + ((value >> 2) & c2);
            value = (((value + (value >> 4)) & c3) * c4) >> 56;

            return (int)value;
        }
        //#endif
    }
#endif

    /// <summary>
    /// Returns the population count (number of bits set) of a mask.
    /// Similar in behavior to the x86 instruction POPCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int PopCount(nuint value)
        => BitOperations.PopCount(value);
#else
    public static unsafe int PopCount(nuint value)
    {
        //#if TARGET_64BIT
        //        return PopCount((ulong)value);
        //#else
        //        return PopCount((uint)value);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return PopCount((ulong)value);
        else
            return PopCount((uint)value);
    }
#endif

    /// <summary>
    /// Count the number of trailing zero bits in an integer value.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(int value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static int TrailingZeroCount(int value)
        => BitOperationsPolyfills.TrailingZeroCount((uint)value);
#endif

    /// <summary>
    /// Count the number of trailing zero bits in an integer value.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(uint value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static int TrailingZeroCount(uint value)
    {
        // Unguarded fallback contract is 0->0, BSF contract is 0->undefined
        if (value == 0)
        {
            return 32;
        }

        // uint.MaxValue >> 27 is always in range [0 - 31] so we use Unsafe.AddByteOffset to avoid bounds check
        return Unsafe.AddByteOffset(
            // Using deBruijn sequence, k=2, n=5 (2^5=32) : 0b_0000_0111_0111_1100_1011_0101_0011_0001u
#if NETSTANDARD1_1_OR_GREATER
            ref MemoryMarshal.GetReference(BitOperationsHidden.TrailingZeroCountDeBruijn),
#else
            ref MemoryMarshalPolyfills.GetArrayDataReference(BitOperationsHidden.TrailingZeroCountDeBruijn),
#endif
            // uint|long -> IntPtr cast on 32-bit platforms does expensive overflow checks not needed here
            (IntPtr)(int)(((value & (uint)-(int)value) * 0x077CB531u) >> 27)); // Multi-cast mitigates redundant conv.u8
    }
#endif

    /// <summary>
    /// Count the number of trailing zero bits in a mask.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(long value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static int TrailingZeroCount(long value)
        => BitOperationsPolyfills.TrailingZeroCount((ulong)value);
#endif

    /// <summary>
    /// Count the number of trailing zero bits in a mask.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(ulong value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static int TrailingZeroCount(ulong value)
    {
        uint lo = (uint)value;

        if (lo == 0)
        {
            return 32 + TrailingZeroCount((uint)(value >> 32));
        }

        return TrailingZeroCount(lo);
    }
#endif

    /// <summary>
    /// Count the number of trailing zero bits in a mask.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(nint value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static unsafe int TrailingZeroCount(nint value)
    {
        //#if TARGET_64BIT
        //        return TrailingZeroCount((ulong)(nuint)value);
        //#else
        //        return TrailingZeroCount((uint)(nuint)value);
        //#endif
        if (sizeof(nint) == sizeof(ulong))
            return TrailingZeroCount((ulong)(nuint)value);
        else
            return TrailingZeroCount((uint)(nuint)value);
    }
#endif

    /// <summary>
    /// Count the number of trailing zero bits in a mask.
    /// Similar in behavior to the x86 instruction TZCNT.
    /// </summary>
    /// <param name="value">The value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static int TrailingZeroCount(nuint value)
        => BitOperations.TrailingZeroCount(value);
#else
    public static unsafe int TrailingZeroCount(nuint value)
    {
        //#if TARGET_64BIT
        //        return TrailingZeroCount((ulong)value);
        //#else
        //        return TrailingZeroCount((uint)value);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return TrailingZeroCount((ulong)value);
        else
            return TrailingZeroCount((uint)value);
    }
#endif

    /// <summary>
    /// Rotates the specified value left by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static uint RotateLeft(uint value, int offset)
        => BitOperations.RotateLeft(value, offset);
#else
    public static uint RotateLeft(uint value, int offset)
        => (value << offset) | (value >> (32 - offset));
#endif

    /// <summary>
    /// Rotates the specified value left by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static ulong RotateLeft(ulong value, int offset)
        => BitOperations.RotateLeft(value, offset);
#else
    public static ulong RotateLeft(ulong value, int offset)
        => (value << offset) | (value >> (64 - offset));
#endif

    /// <summary>
    /// Rotates the specified value left by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROL.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..31] is treated as congruent mod 32 on a 32-bit process,
    /// and any value outside the range [0..63] is treated as congruent mod 64 on a 64-bit process.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NET7_0_OR_GREATER
    public static nuint RotateLeft(nuint value, int offset)
        => BitOperations.RotateLeft(value, offset);
#else
    public static unsafe nuint RotateLeft(nuint value, int offset)
    {
        //#if TARGET_64BIT
        //        return (nuint)RotateLeft((ulong)value, offset);
        //#else
        //        return (nuint)RotateLeft((uint)value, offset);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return (nuint)RotateLeft((ulong)value, offset);
        else
            return (nuint)RotateLeft((uint)value, offset);
    }
#endif

    /// <summary>
    /// Rotates the specified value right by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROR.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..31] is treated as congruent mod 32.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static uint RotateRight(uint value, int offset)
        => BitOperations.RotateRight(value, offset);
#else
    public static uint RotateRight(uint value, int offset)
        => (value >> offset) | (value << (32 - offset));
#endif

    /// <summary>
    /// Rotates the specified value right by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROR.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NETCOREAPP3_0_OR_GREATER
    public static ulong RotateRight(ulong value, int offset)
        => BitOperations.RotateRight(value, offset);
#else
    public static ulong RotateRight(ulong value, int offset)
        => (value >> offset) | (value << (64 - offset));
#endif

    /// <summary>
    /// Rotates the specified value right by the specified number of bits.
    /// Similar in behavior to the x86 instruction ROR.
    /// </summary>
    /// <param name="value">The value to rotate.</param>
    /// <param name="offset">The number of bits to rotate by.
    /// Any value outside the range [0..31] is treated as congruent mod 32 on a 32-bit process,
    /// and any value outside the range [0..63] is treated as congruent mod 64 on a 64-bit process.</param>
    /// <returns>The rotated value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
#if NET7_0_OR_GREATER
    public static nuint RotateRight(nuint value, int offset)
        => BitOperations.RotateRight(value, offset);
#else
    public static unsafe nuint RotateRight(nuint value, int offset)
    {
        //#if TARGET_64BIT
        //        return (nuint)RotateRight((ulong)value, offset);
        //#else
        //        return (nuint)RotateRight((uint)value, offset);
        //#endif
        if (sizeof(nuint) == sizeof(ulong))
            return (nuint)RotateRight((ulong)value, offset);
        else
            return (nuint)RotateRight((uint)value, offset);
    }
#endif

    /// <summary>
    /// Accumulates the CRC (Cyclic redundancy check) checksum.
    /// </summary>
    /// <param name="crc">The base value to calculate checksum on</param>
    /// <param name="data">The data for which to compute the checksum</param>
    /// <returns>The CRC-checksum</returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
    public static uint Crc32C(uint crc, byte data)
        => BitOperations.Crc32C(crc, data);
#else
    public static uint Crc32C(uint crc, byte data)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Sse42.IsSupported)
        {
            return Sse42.Crc32(crc, data);
        }
#endif

#if NET5_0_OR_GREATER
        if (Crc32.IsSupported)
        {
            return Crc32.ComputeCrc32C(crc, data);
        }
#endif

        return BitOperationsHidden.Crc32Fallback.Crc32C(crc, data);
    }
#endif

    /// <summary>
    /// Accumulates the CRC (Cyclic redundancy check) checksum.
    /// </summary>
    /// <param name="crc">The base value to calculate checksum on</param>
    /// <param name="data">The data for which to compute the checksum</param>
    /// <returns>The CRC-checksum</returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
    public static uint Crc32C(uint crc, ushort data)
        => BitOperations.Crc32C(crc, data);
#else
    public static uint Crc32C(uint crc, ushort data)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Sse42.IsSupported)
        {
            return Sse42.Crc32(crc, data);
        }
#endif

#if NET5_0_OR_GREATER
        if (Crc32.IsSupported)
        {
            return Crc32.ComputeCrc32C(crc, data);
        }
#endif

        return BitOperationsHidden.Crc32Fallback.Crc32C(crc, data);
    }
#endif

    /// <summary>
    /// Accumulates the CRC (Cyclic redundancy check) checksum.
    /// </summary>
    /// <param name="crc">The base value to calculate checksum on</param>
    /// <param name="data">The data for which to compute the checksum</param>
    /// <returns>The CRC-checksum</returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
    public static uint Crc32C(uint crc, uint data)
        => BitOperations.Crc32C(crc, data);
#else
    public static uint Crc32C(uint crc, uint data)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Sse42.IsSupported)
        {
            return Sse42.Crc32(crc, data);
        }
#endif

#if NET5_0_OR_GREATER
        if (Crc32.IsSupported)
        {
            return Crc32.ComputeCrc32C(crc, data);
        }
#endif

        return BitOperationsHidden.Crc32Fallback.Crc32C(crc, data);
    }
#endif

        /// <summary>
        /// Accumulates the CRC (Cyclic redundancy check) checksum.
        /// </summary>
        /// <param name="crc">The base value to calculate checksum on</param>
        /// <param name="data">The data for which to compute the checksum</param>
        /// <returns>The CRC-checksum</returns>
        [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
    public static uint Crc32C(uint crc, ulong data)
        => BitOperations.Crc32C(crc, data);
#else
    public static uint Crc32C(uint crc, ulong data)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Sse42.X64.IsSupported)
        {
            // This intrinsic returns a 64-bit register with the upper 32-bits set to 0.
            return (uint)Sse42.X64.Crc32(crc, data);
        }
#endif

#if NET5_0_OR_GREATER
        if (Crc32.Arm64.IsSupported)
        {
            return Crc32.Arm64.ComputeCrc32C(crc, data);
        }
#endif

        return Crc32C(Crc32C(crc, (uint)(data)), (uint)(data >> 32));
    }
#endif

}
