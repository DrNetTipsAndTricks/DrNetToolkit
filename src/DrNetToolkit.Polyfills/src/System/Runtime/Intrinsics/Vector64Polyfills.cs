// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

# if NETCOREAPP3_0_OR_GREATER

using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics;

/// <summary>
/// <see cref="Vector64{T}"/> polyfills.
/// </summary>
public static class Vector64Polyfills
{
    /// <summary>Computes the ones-complement of a vector.</summary>
    /// <typeparam name="T">The type of the elements in the vector.</typeparam>
    /// <param name="vector">The vector whose ones-complement is to be computed.</param>
    /// <returns>A vector whose elements are the ones-complement of the corresponding elements in <paramref name="vector" />.</returns>
    /// <exception cref="NotSupportedException">The type of <paramref name="vector" /> (<typeparamref name="T" />) is not supported.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET7_0_OR_GREATER
    public static Vector64<T> OnesComplement<T>(Vector64<T> vector) where T : struct
        => Vector64.OnesComplement(vector);
#else
    public static Vector64<T> OnesComplement<T>(this Vector64<T> vector) where T : struct
    {
        Unsafe.SkipInit(out Vector64<T> result);
        Unsafe.As<Vector64<T>, ulong>(ref result) = ~Unsafe.As<Vector64<T>, ulong>(ref vector);

        return result;
    }
#endif
}

#endif
