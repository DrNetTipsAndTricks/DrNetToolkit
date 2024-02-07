// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Runtime;

namespace DrNetToolkit.HighPerformance;

/// <summary>
/// Helper methods for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/> structures.
/// </summary>
public static partial class SpanExtensions
{
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

    public static Span<T> DangerousSlice<T>(this Span<T> span, int start)
        => MemoryHelpers.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), span.Length - start);

    public static ReadOnlySpan<T> DangerousSlice<T>(this ReadOnlySpan<T> span, int start)
        => MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start),
            span.Length - start);

    public static Span<T> DangerousSlice<T>(this Span<T> span, int start, int length)
        => MemoryHelpers.CreateSpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), length);

    public static ReadOnlySpan<T> DangerousSlice<T>(this ReadOnlySpan<T> span, int start, int length)
        => MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), start), length);

}

