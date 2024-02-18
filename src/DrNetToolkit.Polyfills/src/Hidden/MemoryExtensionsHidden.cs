// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// Implementations of <see cref="MemoryExtensions"/> hidden methods.
/// </summary>
public static partial class MemoryExtensionsHidden
{
    /// <summary>
    /// Searches for the first index of any value other than the specified <paramref name="value0"/>,
    /// <paramref name="value1"/>, <paramref name="value2"/>, or <paramref name="value3"/>.
    /// </summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <param name="value3">A value to avoid</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value0"/>,
    /// <paramref name="value1"/>, <paramref name="value2"/>, and <paramref name="value3"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, <paramref name="value2"/>, and
    /// <paramref name="value3"/>, returns -1.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2, T value3) where T : IEquatable<T>?
        => SpanHelpersHidden.IndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, value2, value3, span.Length);

    /// <summary>
    /// Searches for the last index of any value other than the specified <paramref name="value0"/>,
    /// <paramref name="value1"/>, <paramref name="value2"/>, or <paramref name="value3"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <param name="value3">A value to avoid</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value0"/>,
    /// <paramref name="value1"/>, <paramref name="value2"/>, and <paramref name="value3"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, <paramref name="value2"/>, and
    /// <paramref name="value3"/>, returns -1.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2, T value3) where T : IEquatable<T>?
        => SpanHelpersHidden.LastIndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, value2, value3, span.Length);
}

#endif
