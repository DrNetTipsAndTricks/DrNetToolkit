// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DrNetToolkit.HighPerformance;

/// <summary>
/// Helper methods for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/> structures.
/// </summary>
public static partial class SpanExtensions
{
    #region AsSpan

    /// <summary>
    /// Cast a <see cref="Span{T}"/> to a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="span">The span to cast to a <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> that corresponds to specified <paramref name="span"/>.</returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this Span<T> span)
        => span;

    /// <summary>
    /// Cast a <see cref="ReadOnlySpan{T}"/> to a <see cref="Span{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The readonly span to cast to a <see cref="Span{T}"/>.</param>
    /// <returns>A <see cref="Span{T}"/> that corresponds to specified readonly <paramref name="span"/>.</returns>
    public static Span<T> DangerousAsSpan<T>(this ReadOnlySpan<T> span)
        => MemoryHelpers.CreateSpan(ref MemoryMarshal.GetReference(span), span.Length);

    #endregion

    #region DangerousSlice

    /// <summary>
    /// Forms a slice out of the current span that begins at a specified index.
    /// </summary>
    /// <typeparam name="T">The type of items in the <paramref name="span"/>.</typeparam>
    /// <param name="span">The span to slice.</param>
    /// <param name="start">The index at which to begin the slice.</param>
    /// <returns>
    /// A span that consists of all elements of the current <paramref name="span"/> from <paramref name="start"/> to
    /// the end of the <paramref name="span"/>.
    /// </returns>
    public static Span<T> DangerousSlice<T>(this Span<T> span, int start)
        => MemoryHelpers.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), span.Length - start);

    /// <summary>
    /// Forms a slice out of the current readonly span that begins at a specified index.
    /// </summary>
    /// <typeparam name="T">The type of items in the readonly <paramref name="span"/>.</typeparam>
    /// <param name="span">The readonly span to slice.</param>
    /// <param name="start">The index at which to begin the slice.</param>
    /// <returns>
    /// A readonly span that consists of all elements of the current readonly <paramref name="span"/> from
    /// <paramref name="start"/> to the end of the <paramref name="span"/>.
    /// </returns>
    public static ReadOnlySpan<T> DangerousSlice<T>(this ReadOnlySpan<T> span, int start)
        => MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start),
            span.Length - start);

    /// <summary>
    /// Forms a slice out of the current span starting at a specified index for a specified length.
    /// </summary>
    /// <typeparam name="T">The type of items in the <paramref name="span"/>.</typeparam>
    /// <param name="span">The span to slice.</param>
    /// <param name="start">The index at which to begin the slice.</param>
    /// <param name="length">The desired length for the slice.</param>
    /// <returns>
    /// A span that consists of <paramref name="length"/> elements from the current <paramref name="span"/> starting at
    /// <paramref name="start"/>.
    /// </returns>
    public static Span<T> DangerousSlice<T>(this Span<T> span, int start, int length)
        => MemoryHelpers.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), length);

    /// <summary>
    /// Forms a slice out of the current readonly span starting at a specified index for a specified length.
    /// </summary>
    /// <typeparam name="T">The type of items in the readonly <paramref name="span"/>.</typeparam>
    /// <param name="span">The readonly span to slice.</param>
    /// <param name="start">The index at which to begin the slice.</param>
    /// <param name="length">The desired length for the slice.</param>
    /// <returns>
    /// A readonly span that consists of <paramref name="length"/> elements from the current readonly
    /// <paramref name="span"/> starting at <paramref name="start"/>.
    /// </returns>
    public static ReadOnlySpan<T> DangerousSlice<T>(this ReadOnlySpan<T> span, int start, int length)
        => MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), length);

    #endregion
}

