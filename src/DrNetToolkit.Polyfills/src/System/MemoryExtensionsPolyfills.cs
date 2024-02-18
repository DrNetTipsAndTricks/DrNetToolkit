// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;

namespace System;

/// <summary>
/// Extension methods for <see cref="Span{T}"/>, <see cref="Memory{T}"/>, and friends.
/// </summary>
public static partial class MemoryExtensionsPolyfills
{
#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Index startIndex)
        => MemoryExtensionsImpls.AsSpan(array, startIndex);
#endif

#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Range range)
        => MemoryExtensionsImpls.AsSpan(array, range);
#endif

#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>Creates a new <see cref="ReadOnlyMemory{T}"/> over the portion of the target string.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="startIndex">The index at which to begin this slice.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<char> AsMemory(this string? text, Index startIndex)
        => MemoryExtensionsImpls.AsMemory(text, startIndex);
#endif

#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>Creates a new <see cref="ReadOnlyMemory{T}"/> over the portion of the target string.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="range">The range used to indicate the start and length of the sliced string.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<char> AsMemory(this string? text, Range range)
        => MemoryExtensionsImpls.AsMemory(text, range);
#endif

#if !NET7_0_OR_GREATER
    /// <inheritdoc cref="Contains{T}(ReadOnlySpan{T}, T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsImpls.Contains(span, value);
#endif

#if !NET7_0_OR_GREATER
    /// <summary>
    /// Searches for the specified value and returns true if found. If not found, returns false. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsImpls.Contains(span, value);
#endif

}

#endif
