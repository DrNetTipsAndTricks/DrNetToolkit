// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD2_1_OR_GREATER

using System;
using System.Runtime.InteropServices;

namespace DrNetToolkit.HighPerformance;

/// <summary>
/// Helper methods for <see cref="Span{T}"/> and <see cref="ReadOnlySpan{T}"/> structures.
/// </summary>
public static partial class SpanExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="span"></param>
    /// <returns></returns>
    public static ReadOnlySpan<T> AsReadOnlySpan<T>(this Span<T> span)
        => span;

    public static Span<T> DangerousAsSpan<T>(this ReadOnlySpan<T> span)
        => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), span.Length);
}

#endif
