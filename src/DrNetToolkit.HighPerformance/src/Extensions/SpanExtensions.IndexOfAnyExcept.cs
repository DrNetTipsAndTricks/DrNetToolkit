// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if !NET7_0_OR_GREATER
using DrNetToolkit.HighPerformance.Dangerous;
#endif

namespace DrNetToolkit.HighPerformance;

public static partial class SpanExtensions
{
    /// <summary>Searches for the first index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
    public static int IndexOfAnyExcept<T>(this Span<T> span, T value) where T : IEquatable<T>? =>
        IndexOfAnyExcept((ReadOnlySpan<T>)span, value);


    /// <summary>Searches for the first index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value)
        where T : IEquatable<T>?
    {
#if NET7_0_OR_GREATER
        return MemoryExtensions.IndexOfAnyExcept(span, value);
#else

        return DangerousSpanHelpers.IndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif
    }

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value)
        where T : IEquatable<T>?
        => LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value);

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value)
        where T : IEquatable<T>?
    {
#if NET7_0_OR_GREATER
        return MemoryExtensions.LastIndexOfAnyExcept(span, value);
#else

        return DangerousSpanHelpers.LastIndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif
    }
}

