// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

# if NETCOREAPP3_0_OR_GREATER

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// <see cref="Vector128{T}"/> hidden methods.
/// </summary>
public static class Vector128Hidden
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetLowerUnsafe<T>(in Vector128<T> vector, Vector64<T> value) where T : struct
    {
        Unsafe.As<Vector128<T>, Vector64<T>>(ref Unsafe.AsRef(in vector)) = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUpperUnsafe<T>(in Vector128<T> vector, Vector64<T> value) where T : struct
    {
        Unsafe.Add(ref Unsafe.As<Vector128<T>, Vector64<T>>(ref Unsafe.AsRef(in vector)), 1) = value;
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

#endif
