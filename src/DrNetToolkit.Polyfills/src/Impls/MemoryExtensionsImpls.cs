// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Polyfills.Internals;

namespace DrNetToolkit.Polyfills.Impls;

/// <summary>
/// Implementations of <see cref="MemoryExtensions"/> methods.
/// </summary>
public static partial class MemoryExtensionsImpls
{
    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER
    public static Span<T> AsSpan<T>(T[]? array, Index startIndex)
        => MemoryExtensions.AsSpan(array, startIndex);
#else
    public static Span<T> AsSpan<T>(T[]? array, Index startIndex)
    {
        if (array == null)
        {
            if (!startIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.array);

            return default;
        }

        if (typeof(T).IsValueType() && array.GetType() != typeof(T[]))
            ThrowHelper.ThrowArrayTypeMismatchException();

        int actualIndex = startIndex.GetOffset(array.Length);
        if ((uint)actualIndex > (uint)array.Length)
            ThrowHelper.ThrowArgumentOutOfRangeException();

        return MemoryMarshalPolyfills.CreateSpan(
            ref Unsafe.Add(ref MemoryMarshalPolyfills.GetArrayDataReference(array),
            (nint)(uint)actualIndex /* force zero-extension */), array.Length - actualIndex);
    }
#endif

    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Range range)
    {
        if (array == null)
        {
            Index startIndex = range.Start;
            Index endIndex = range.End;

            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.array);

            return default;
        }

        if (!typeof(T).IsValueType() && array.GetType() != typeof(T[]))
            ThrowHelper.ThrowArrayTypeMismatchException();

        (int start, int length) = range.GetOffsetAndLength(array.Length);
#if NETSTANDARD2_1_OR_GREATER
        return MemoryMarshal.CreateSpan(ref Unsafe.Add(ref MemoryMarshalImpls.GetArrayDataReference(array),
            (nint)(uint)start /* force zero-extension */), length);
#else
        return MemoryMarshalPolyfills.CreateSpan(ref Unsafe.Add(ref MemoryMarshalImpls.GetArrayDataReference(array),
            (nint)(uint)start /* force zero-extension */), length);
#endif
    }
}
#endif
