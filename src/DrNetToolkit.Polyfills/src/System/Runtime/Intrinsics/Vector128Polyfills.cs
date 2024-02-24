// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

# if NETCOREAPP3_0_OR_GREATER

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using DrNetToolkit.Polyfills.Hidden;

namespace System.Runtime.Intrinsics;

/// <summary>
/// <see cref="Vector128{T}"/> polyfills.
/// </summary>
public static class Vector128Polyfills
{
    /// <summary>Creates a new <see cref="Vector128{T}" /> instance from two <see cref="Vector64{T}" /> instances.</summary>
    /// <typeparam name="T">The type of the elements in the vector.</typeparam>
    /// <param name="lower">The value that the lower 64-bits will be initialized to.</param>
    /// <param name="upper">The value that the upper 64-bits will be initialized to.</param>
    /// <returns>A new <see cref="Vector128{T}" /> initialized from <paramref name="lower" /> and <paramref name="upper" />.</returns>
    /// <exception cref="NotSupportedException">The type of <paramref name="lower" /> and <paramref name="upper" /> (<typeparamref name="T" />) is not supported.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
    public static Vector128<T> Create<T>(Vector64<T> lower, Vector64<T> upper) where T : struct
        => Vector128.Create(lower, upper);
#else
    public static Vector128<T> Create<T>(Vector64<T> lower, Vector64<T> upper) where T : struct
    {
        if (AdvSimd.IsSupported)
        {
            return lower.ToVector128Unsafe().WithUpper(upper);
        }
        else
        {
            Unsafe.SkipInit(out Vector128<T> result);

            Vector128Hidden.SetLowerUnsafe(result, lower);
            Vector128Hidden.SetUpperUnsafe(result, lower);

            return result;
        }
    }
#endif

    /// <summary>Computes the ones-complement of a vector.</summary>
    /// <typeparam name="T">The type of the elements in the vector.</typeparam>
    /// <param name="vector">The vector whose ones-complement is to be computed.</param>
    /// <returns>A vector whose elements are the ones-complement of the corresponding elements in <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException">The type of <paramref name="vector" /> (<typeparamref name="T" />) is not supported.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET7_0_OR_GREATER
    public static Vector128<T> OnesComplement<T>(Vector128<T> vector) where T : struct
        => Vector128.OnesComplement(vector);
#else
    public static Vector128<T> OnesComplement<T>(this Vector128<T> vector) where T : struct
        => Vector128Polyfills.Create(
            vector.GetLower().OnesComplement(),
            vector.GetUpper().OnesComplement()
        );
#endif
}

#endif
