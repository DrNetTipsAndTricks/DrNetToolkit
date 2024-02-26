// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Polyfills.Hidden;
using DrNetToolkit.Polyfills.Internals;


#if NET8_0_OR_GREATER
    using CompositeFormat = System.Text.CompositeFormat;
#else
using CompositeFormat = System.Text.CompositeFormatPolyfills;
#endif

namespace System;

/// <summary>
/// Extension methods for <see cref="Span{T}"/>, <see cref="Memory{T}"/>, and friends.
/// </summary>
public static partial class MemoryExtensionsPolyfills
{
    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(T[]? array, Index startIndex)
        => MemoryExtensions.AsSpan(array, startIndex);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Index startIndex)
    {
        if (array == null)
        {
            if (!startIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

            return default;
        }

        if (typeof(T).IsValueType() && array.GetType() != typeof(T[]))
            ThrowHelper.ThrowArrayTypeMismatchException();

        int actualIndex = startIndex.GetOffset(array.Length);
        if ((uint)actualIndex > (uint)array.Length)
            ThrowHelper.ThrowArgumentOutOfRangeException();

        return MemoryMarshalPolyfills.CreateSpan(ref Unsafe.Add(ref MemoryMarshalPolyfills.GetArrayDataReference(array),
            (nint)(uint)actualIndex /* force zero-extension */), array.Length - actualIndex);
    }
#endif

    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(T[]? array, Range range)
        => MemoryExtensions.AsSpan(array, range);
#else
    //
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Range range)
    {
        if (array == null)
        {
            Index startIndex = range.Start;
            Index endIndex = range.End;

            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

            return default;
        }

        if (!typeof(T).IsValueType() && array.GetType() != typeof(T[]))
            ThrowHelper.ThrowArrayTypeMismatchException();

        (int start, int length) = range.GetOffsetAndLength(array.Length);
        return MemoryMarshalPolyfills.CreateSpan(ref Unsafe.Add(
            ref MemoryMarshalPolyfills.GetArrayDataReference(array), (nint)(uint)start /* force zero-extension */),
            length);
    }
#endif

    /// <summary>Creates a new <see cref="ReadOnlySpan{Char}"/> over a portion of the target string from a specified position to the end of the string.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="startIndex">The index at which to begin this slice.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than 0 or greater than <paramref name="text"/>.Length.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> AsSpan(string? text, Index startIndex)
        => MemoryExtensions.AsSpan(text, startIndex);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> AsSpan(this string? text, Index startIndex)
    {
        if (text is null)
        {
            if (!startIndex.Equals(Index.Start))
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex);
            }

            return default;
        }

        int actualIndex = startIndex.GetOffset(text.Length);
        if ((uint)actualIndex > (uint)text.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex);
        }

        return MemoryMarshalPolyfills.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()),
            (nint)(uint)actualIndex /* force zero-extension */), text.Length - actualIndex);
    }
#endif

    /// <summary>Creates a new <see cref="ReadOnlySpan{Char}"/> over a portion of a target string using the range start and end indexes.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="range">The range which has start and end indexes to use for slicing the string.</param>
    /// <exception cref="ArgumentNullException"><paramref name="text"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="range"/>'s start or end index is not within the bounds of the string.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="range"/>'s start index is greater than its end index.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> AsSpan(string? text, Range range)
        => MemoryExtensions.AsSpan(text, range);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> AsSpan(this string? text, Range range)
    {
        if (text is null)
        {
            Index startIndex = range.Start;
            Index endIndex = range.End;

            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
            }

            return default;
        }

        (int start, int length) = range.GetOffsetAndLength(text.Length);
        return MemoryMarshalPolyfills.CreateSpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()),
            (nint)(uint)start /* force zero-extension */), length);
    }
#endif

    /// <summary>Creates a new <see cref="ReadOnlyMemory{T}"/> over the portion of the target string.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="startIndex">The index at which to begin this slice.</param>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<char> AsMemory(string? text, Index startIndex)
        => MemoryExtensions.AsMemory(text, startIndex);
#else
    //
    public static ReadOnlyMemory<char> AsMemory(this string? text, Index startIndex)
    {
        if (text == null)
        {
            if (!startIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);

            return default;
        }

        int actualIndex = startIndex.GetOffset(text.Length);
        return MemoryExtensions.AsMemory(text, actualIndex);
    }
#endif

    /// <summary>Creates a new <see cref="ReadOnlyMemory{T}"/> over the portion of the target string.</summary>
    /// <param name="text">The target string.</param>
    /// <param name="range">The range used to indicate the start and length of the sliced string.</param>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlyMemory<char> AsMemory(string? text, Range range)
        => MemoryExtensions.AsMemory(text, range);
#else
    //
    public static ReadOnlyMemory<char> AsMemory(this string? text, Range range)
    {
        if (text == null)
        {
            Index startIndex = range.Start;
            Index endIndex = range.End;

            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);

            return default;
        }

        (int start, int length) = range.GetOffsetAndLength(text.Length);
        return MemoryExtensions.AsMemory(text, start, length);
    }
#endif

    /// <inheritdoc cref="Contains{T}(ReadOnlySpan{T}, T)"/>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.Contains(span, value);
#elif NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(Span<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => SpanHelpersHidden.Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>
    /// Searches for the specified value and returns true if found. If not found, returns false. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.Contains(span, value);
#elif NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool Contains<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => SpanHelpersHidden.Contains(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <inheritdoc cref="ContainsAny{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAny((ReadOnlySpan<T>)span, value0, value1);
#endif

    /// <inheritdoc cref="ContainsAny{T}(ReadOnlySpan{T}, T, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAny((ReadOnlySpan<T>)span, value0, value1, value2);
#endif

    /// <inheritdoc cref="ContainsAny{T}(ReadOnlySpan{T}, ReadOnlySpan{T})"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAny((ReadOnlySpan<T>)span, values);
#endif

    /// <inheritdoc cref="ContainsAnyExcept{T}(ReadOnlySpan{T}, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAnyExcept((ReadOnlySpan<T>)span, value);
#endif

    /// <inheritdoc cref="ContainsAnyExcept{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAnyExcept((ReadOnlySpan<T>)span, value0, value1);
#endif

    /// <inheritdoc cref="ContainsAnyExcept{T}(ReadOnlySpan{T}, T, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAnyExcept((ReadOnlySpan<T>)span, value0, value1, value2);
#endif

    /// <inheritdoc cref="ContainsAnyExcept{T}(ReadOnlySpan{T}, ReadOnlySpan{T})"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.ContainsAnyExcept((ReadOnlySpan<T>)span, values);
#endif

    /// <inheritdoc cref="ContainsAnyInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.ContainsAnyInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.ContainsAnyInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <inheritdoc cref="ContainsAnyExceptInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExceptInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.ContainsAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExceptInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.ContainsAnyExceptInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <summary>
    /// Searches for any occurrence of the specified <paramref name="value0"/> or <paramref name="value1"/>, and returns true if found. If not found, returns false.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny<T>(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAny(span, value0, value1) >= 0;
#endif

    /// <summary>
    /// Searches for any occurrence of the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>, and returns true if found. If not found, returns false.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
    /// <param name="value2">One of the values to search for.</param>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAny(span, value0, value1, value2) >= 0;
#endif

    /// <summary>
    /// Searches for any occurrence of any of the specified <paramref name="values"/> and returns true if found. If not found, returns false.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The set of values to search for.</param>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAny(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAny(span, values) >= 0;
#endif

    /// <summary>
    /// Searches for any value other than the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// True if any value other than <paramref name="value"/> is present in the span.
    /// If all of the values are <paramref name="value"/>, returns false.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept(span, value) >= 0;
#endif

    /// <summary>
    /// Searches for any value other than the specified <paramref name="value0"/> or <paramref name="value1"/>.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid.</param>
    /// <returns>
    /// True if any value other than <paramref name="value0"/> and <paramref name="value1"/> is present in the span.
    /// If all of the values are <paramref name="value0"/> or <paramref name="value1"/>, returns false.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept(span, value0, value1) >= 0;
#endif

    /// <summary>
    /// Searches for any value other than the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid.</param>
    /// <param name="value2">A value to avoid.</param>
    /// <returns>
    /// True if any value other than <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/> is present in the span.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>, returns false.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept(span, value0, value1, value2) >= 0;
#endif

    /// <summary>
    /// Searches for any value other than the specified <paramref name="values"/>.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The values to avoid.</param>
    /// <returns>
    /// True if any value other than those in <paramref name="values"/> is present in the span.
    /// If all of the values are in <paramref name="values"/>, returns false.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.ContainsAnyExcept(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept(span, values) >= 0;
#endif

    /// <summary>
    /// Searches for any value in the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive, and returns true if found. If not found, returns false.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the range for which to search.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the range for which to search.</param>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.ContainsAnyInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.IndexOfAnyInRange(span, lowInclusive, highInclusive) >= 0;
#endif

    /// <summary>
    /// Searches for any value outside of the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the excluded range.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the excluded range.</param>
    /// <returns>
    /// True if any value other than those in the specified range is present in the span.
    /// If all of the values are inside of the specified range, returns false.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExceptInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.ContainsAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsAnyExceptInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.IndexOfAnyExceptInRange(span, lowInclusive, highInclusive) >= 0;
#endif

    /// <summary>
    /// Searches for the specified value and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOf(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>
    /// Searches for the specified sequence and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOf(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(value), value.Length);
#endif

    /// <summary>
    /// Searches for the specified value and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOf(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>
    /// Searches for the specified sequence and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
#if !NET6_0_OR_GREATER || NET10_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOf(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOf(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(value), value.Length);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept((ReadOnlySpan<T>)span, value);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value0"/> and <paramref name="value1"/>.
    /// If all of the values are <paramref name="value0"/> or <paramref name="value1"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1, value2);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="values"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The values to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than those in <paramref name="values"/>.
    /// If all of the values are in <paramref name="values"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.IndexOfAnyExcept((ReadOnlySpan<T>)span, values);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => SpanHelpersHidden.IndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value0"/> and <paramref name="value1"/>.
    /// If all of the values are <paramref name="value0"/> or <paramref name="value1"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => SpanHelpersHidden.IndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => SpanHelpersHidden.IndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>Searches for the first index of any value other than the specified <paramref name="values"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The values to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than those in <paramref name="values"/>.
    /// If all of the values are in <paramref name="values"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAnyExcept<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAnyExcept(span, values);
#else
    //
    public static unsafe int IndexOfAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        switch (values.Length)
        {
            case 0:
                // If the span is empty, we want to return -1.
                // If the span is non-empty, we want to return the index of the first char that's not in the empty set,
                // which is every character, and so the first char in the span.
                return span.IsEmpty ? -1 : 0;

            case 1:
                return IndexOfAnyExcept(span, values[0]);

            case 2:
                return IndexOfAnyExcept(span, values[0], values[1]);

            case 3:
                return IndexOfAnyExcept(span, values[0], values[1], values[2]);

            case 4:
                return MemoryExtensionsHidden.IndexOfAnyExcept(span, values[0], values[1], values[2], values[3]);

            default:
                for (int i = 0; i < span.Length; i++)
                {
                    if (!Contains(values, span[i]))
                    {
                        return i;
                    }
                }

                return -1;
        }
    }
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, value);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.LastIndexOfAnyExcept<T>((ReadOnlySpan<T>)span, value);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value0"/> and <paramref name="value1"/>.
    /// If all of the values are <paramref name="value0"/> or <paramref name="value1"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, value0, value1);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, value0, value1, value2);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.LastIndexOfAnyExcept((ReadOnlySpan<T>)span, value0, value1, value2);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="values"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The values to avoid.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than those in <paramref name="values"/>.
    /// If all of the values are in <paramref name="values"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, values);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExcept<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.LastIndexOfAnyExcept((ReadOnlySpan<T>)span, values);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">A value to avoid.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value"/>.
    /// If all of the values are <paramref name="value"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => SpanHelpersHidden.LastIndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value0"/> or <paramref name="value1"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value0"/> and <paramref name="value1"/>.
    /// If all of the values are <paramref name="value0"/> or <paramref name="value1"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => SpanHelpersHidden.LastIndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="value0"/>, <paramref name="value1"/>, or <paramref name="value2"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">A value to avoid.</param>
    /// <param name="value1">A value to avoid</param>
    /// <param name="value2">A value to avoid</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value other than <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>.
    /// If all of the values are <paramref name="value0"/>, <paramref name="value1"/>, and <paramref name="value2"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept<T>(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => SpanHelpersHidden.LastIndexOfAnyExcept(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>Searches for the last index of any value other than the specified <paramref name="values"/>.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The values to avoid.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value other than those in <paramref name="values"/>.
    /// If all of the values are in <paramref name="values"/>, returns -1.
    /// </returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAnyExcept(span, values);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAnyExcept<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
    {
        switch (values.Length)
        {
            case 0:
                // If the span is empty, we want to return -1.
                // If the span is non-empty, we want to return the index of the last char that's not in the empty set,
                // which is every character, and so the last char in the span.
                // Either way, we want to return span.Length - 1.
                return span.Length - 1;

            case 1:
                return LastIndexOfAnyExcept(span, values[0]);

            case 2:
                return LastIndexOfAnyExcept(span, values[0], values[1]);

            case 3:
                return LastIndexOfAnyExcept(span, values[0], values[1], values[2]);

            case 4:
                return MemoryExtensionsHidden.LastIndexOfAnyExcept(span, values[0], values[1], values[2], values[3]);

            default:
                for (int i = span.Length - 1; i >= 0; i--)
                {
                    if (!MemoryExtensionsPolyfills.Contains(values, span[i]))
                    {
                        return i;
                    }
                }

                return -1;
        }
    }
#endif

    /// <inheritdoc cref="IndexOfAnyInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.IndexOfAnyInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.IndexOfAnyInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <summary>Searches for the first index of any value in the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the range for which to search.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the range for which to search.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value in the specified range.
    /// If all of the values are outside of the specified range, returns -1.
    /// </returns>
#if NET8_0_OR_GREATER
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.IndexOfAnyInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
    {
        if (lowInclusive is null || highInclusive is null)
        {
            ThrowNullLowHighInclusive(lowInclusive, highInclusive);
        }

        return SpanHelpersHidden.IndexOfAnyInRange(ref MemoryMarshal.GetReference(span), lowInclusive, highInclusive, span.Length);
    }
#endif

    /// <inheritdoc cref="IndexOfAnyExceptInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExceptInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.IndexOfAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExceptInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.IndexOfAnyExceptInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <summary>Searches for the first index of any value outside of the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the excluded range.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the excluded range.</param>
    /// <returns>
    /// The index in the span of the first occurrence of any value outside of the specified range.
    /// If all of the values are inside of the specified range, returns -1.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExceptInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.IndexOfAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAnyExceptInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
    {
        if (lowInclusive is null || highInclusive is null)
        {
            ThrowNullLowHighInclusive(lowInclusive, highInclusive);
        }

        return SpanHelpersHidden.IndexOfAnyExceptInRange(ref MemoryMarshal.GetReference(span), lowInclusive, highInclusive, span.Length);
    }
#endif

    /// <inheritdoc cref="LastIndexOfAnyInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.LastIndexOfAnyInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.LastIndexOfAnyInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <summary>Searches for the last index of any value in the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the range for which to search.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the range for which to search.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value in the specified range.
    /// If all of the values are outside of the specified range, returns -1.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.LastIndexOfAnyInRange<T>(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
    {
        if (lowInclusive is null || highInclusive is null)
        {
            ThrowNullLowHighInclusive(lowInclusive, highInclusive);
        }

        return SpanHelpersHidden.LastIndexOfAnyInRange(ref MemoryMarshal.GetReference(span), lowInclusive, highInclusive, span.Length);
    }
#endif

    /// <inheritdoc cref="LastIndexOfAnyExceptInRange{T}(ReadOnlySpan{T}, T, T)"/>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExceptInRange<T>(Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.LastIndexOfAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExceptInRange<T>(this Span<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensionsPolyfills.LastIndexOfAnyExceptInRange((ReadOnlySpan<T>)span, lowInclusive, highInclusive);
#endif

    /// <summary>Searches for the last index of any value outside of the range between <paramref name="lowInclusive"/> and <paramref name="highInclusive"/>, inclusive.</summary>
    /// <typeparam name="T">The type of the span and values.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="lowInclusive">A lower bound, inclusive, of the excluded range.</param>
    /// <param name="highInclusive">A upper bound, inclusive, of the excluded range.</param>
    /// <returns>
    /// The index in the span of the last occurrence of any value outside of the specified range.
    /// If all of the values are inside of the specified range, returns -1.
    /// </returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExceptInRange<T>(ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
        => MemoryExtensions.LastIndexOfAnyExceptInRange(span, lowInclusive, highInclusive);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAnyExceptInRange<T>(this ReadOnlySpan<T> span, T lowInclusive, T highInclusive) where T : IComparable<T>
    {
        if (lowInclusive is null || highInclusive is null)
        {
            ThrowNullLowHighInclusive(lowInclusive, highInclusive);
        }

        return SpanHelpersHidden.LastIndexOfAnyExceptInRange(ref MemoryMarshal.GetReference(span), lowInclusive, highInclusive, span.Length);
    }
#endif

#if !NET8_0_OR_GREATER
    /// <summary>Throws an <see cref="ArgumentNullException"/> for <paramref name="lowInclusive"/> or <paramref name="highInclusive"/> being null.</summary>
    [DoesNotReturn]
    private static void ThrowNullLowHighInclusive<T>(T? lowInclusive, T? highInclusive)
    {
        Debug.Assert(lowInclusive is null || highInclusive is null);
        throw new ArgumentNullException(lowInclusive is null ? nameof(lowInclusive) : nameof(highInclusive));
    }
#endif

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using IEquatable{T}.Equals(T).
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(Span<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>?
        => MemoryExtensions.SequenceEqual<T>(span, other);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(Span<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>? // There should be no this here!
    {
        int length = span.Length;
        int otherLength = other.Length;

        return length == otherLength && SpanHelpersHidden.SequenceEqual(ref MemoryMarshal.GetReference(span), ref MemoryMarshal.GetReference(other), length);
    }
#endif

    /// <summary>
    /// Determines the relative order of the sequences being compared by comparing the elements using IComparable{T}.CompareTo(T).
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SequenceCompareTo<T>(Span<T> span, ReadOnlySpan<T> other) where T : IComparable<T>?
        => MemoryExtensions.SequenceCompareTo<T>(span, other);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SequenceCompareTo<T>(Span<T> span, ReadOnlySpan<T> other) where T : IComparable<T>? // There should be no this here!
    {
        // Can't use IsBitwiseEquatable<T>() below because that only tells us about
        // equality checks, not about CompareTo checks.

        if (typeof(T) == typeof(byte))
            return SpanHelpersHidden.SequenceCompareTo(
                ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(span)),
                span.Length,
                ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(other)),
                other.Length);

        if (typeof(T) == typeof(char))
            return SpanHelpersHidden.SequenceCompareTo(
                ref Unsafe.As<T, char>(ref MemoryMarshal.GetReference(span)),
                span.Length,
                ref Unsafe.As<T, char>(ref MemoryMarshal.GetReference(other)),
                other.Length);

        return SpanHelpersHidden.SequenceCompareTo(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(other), other.Length);
    }
#endif

    /// <summary>
    /// Searches for the specified value and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOf<T>(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>
    /// Searches for the specified sequence and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.IndexOf<T>(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(value), value.Length);
#endif

    /// <summary>
    /// Searches for the specified value and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOf<T>(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>
    /// Searches for the specified sequence and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOf<T>(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOf(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(value), value.Length);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
    /// <param name="value2">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The set of values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAny<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int IndexOfAny<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>? // There should be no this here!
        => MemoryExtensionsPolyfills.IndexOfAny((ReadOnlySpan<T>)span, values);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
    /// <param name="value2">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>
    /// Searches for the first index of any of the specified values similar to calling IndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The set of values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.IndexOfAny<T>(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.IndexOfAny(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(values), values.Length);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(Span<T> span, T value0, T value1) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
    /// <param name="value2">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The set of values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAny<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LastIndexOfAny<T>(Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>? // There should be no this here!
        => MemoryExtensionsPolyfills.LastIndexOfAny((ReadOnlySpan<T>)span, values);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, value0, value1);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, span.Length);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value0">One of the values to search for.</param>
    /// <param name="value1">One of the values to search for.</param>
    /// <param name="value2">One of the values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, value0, value1, value2);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOfAny(ref MemoryMarshal.GetReference(span), value0, value1, value2, span.Length);
#endif

    /// <summary>
    /// Searches for the last index of any of the specified values similar to calling LastIndexOf several times with the logical OR operator. If not found, returns -1.
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="values">The set of values to search for.</param>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>?
        => MemoryExtensions.LastIndexOfAny(span, values);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOfAny<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>? // There should be no this here!
        => SpanHelpersHidden.LastIndexOfAny(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(values), values.Length);
#endif

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using IEquatable{T}.Equals(T).
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>?
        => MemoryExtensions.SequenceEqual(span, other);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IEquatable<T>? // There should be no this here!
    {
        int length = span.Length;
        int otherLength = other.Length;

        return length == otherLength && SpanHelpersHidden.SequenceEqual(ref MemoryMarshal.GetReference(span), ref MemoryMarshal.GetReference(other), length);
    }
#endif

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using an <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing elements, or null to use the default <see cref="IEqualityComparer{T}"/> for the type of an element.</param>
    /// <returns>true if the two sequences are equal; otherwise, false.</returns>
#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SequenceEqual<T>(Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer = null)
        => MemoryExtensions.SequenceEqual(span, other, comparer);
#else
    public static bool SequenceEqual<T>(this Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer = null)
        => SequenceEqual((ReadOnlySpan<T>)span, other, comparer);
#endif

    /// <summary>
    /// Determines whether two sequences are equal by comparing the elements using an <see cref="IEqualityComparer{T}"/>.
    /// </summary>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing elements, or null to use the default <see cref="IEqualityComparer{T}"/> for the type of an element.</param>
    /// <returns>true if the two sequences are equal; otherwise, false.</returns>
#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer = null)
        => MemoryExtensions.SequenceEqual(span, other, comparer);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool SequenceEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer = null)
    {
        // If the spans differ in length, they're not equal.
        if (span.Length != other.Length)
        {
            return false;
        }

        if (typeof(T).IsValueType())
        {
            if (comparer is null || comparer == EqualityComparer<T>.Default)
            {
                // Otherwise, compare each element using EqualityComparer<T>.Default.Equals in a way that will enable it to de virtualize.
                for (int i = 0; i < span.Length; i++)
                {
                    if (!EqualityComparer<T>.Default.Equals(span[i], other[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        // Use the comparer to compare each element.
        comparer ??= EqualityComparer<T>.Default;
        for (int i = 0; i < span.Length; i++)
        {
            if (!comparer.Equals(span[i], other[i]))
            {
                return false;
            }
        }

        return true;
    }
#endif

    /// <summary>
    /// Determines the relative order of the sequences being compared by comparing the elements using IComparable{T}.CompareTo(T).
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int SequenceCompareTo<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IComparable<T>?
        => MemoryExtensions.SequenceCompareTo(span, other);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int SequenceCompareTo<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : IComparable<T>? // There should be no this here!
    {
        // Can't use IsBitwiseEquatable<T>() below because that only tells us about
        // equality checks, not about CompareTo checks.

        if (typeof(T) == typeof(byte))
            return SpanHelpersHidden.SequenceCompareTo(
                ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(span)),
                span.Length,
                ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(other)),
                other.Length);

        if (typeof(T) == typeof(char))
            return SpanHelpersHidden.SequenceCompareTo(
                ref Unsafe.As<T, char>(ref MemoryMarshal.GetReference(span)),
                span.Length,
                ref Unsafe.As<T, char>(ref MemoryMarshal.GetReference(other)),
                other.Length);

        return SpanHelpersHidden.SequenceCompareTo(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(other), other.Length);
    }
#endif

    /// <summary>
    /// Determines whether the specified sequence appears at the start of the span.
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool StartsWith<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.StartsWith(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool StartsWith<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
    {
        int valueLength = value.Length;
        return valueLength <= span.Length && SpanHelpersHidden.SequenceEqual(ref MemoryMarshal.GetReference(span),
            ref MemoryMarshal.GetReference(value), valueLength);
    }
#endif

    /// <summary>
    /// Determines whether the specified sequence appears at the start of the span.
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool StartsWith<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.StartsWith(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool StartsWith<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
    {
        int valueLength = value.Length;
        return valueLength <= span.Length && SpanHelpersHidden.SequenceEqual(ref MemoryMarshal.GetReference(span),
            ref MemoryMarshal.GetReference(value), valueLength);
    }
#endif

    /// <summary>
    /// Determines whether the specified sequence appears at the end of the span.
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool EndsWith<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.EndsWith<T>(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool EndsWith<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
    {
        int spanLength = span.Length;
        int valueLength = value.Length;

        return valueLength <= spanLength &&
            SpanHelpersHidden.SequenceEqual(
                ref Unsafe.Add(ref MemoryMarshal.GetReference(span), (nint)(uint)(spanLength - valueLength) /* force zero-extension */),
                ref MemoryMarshal.GetReference(value),
                valueLength);
    }
#endif

    /// <summary>
    /// Determines whether the specified sequence appears at the end of the span.
    /// </summary>
#if !NET6_0
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool EndsWith<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.EndsWith(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool EndsWith<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // There should be no this here!
    {
        int spanLength = span.Length;
        int valueLength = value.Length;

        return valueLength <= spanLength &&
            SpanHelpersHidden.SequenceEqual(
                ref Unsafe.Add(ref MemoryMarshal.GetReference(span), (nint)(uint)(spanLength - valueLength) /* force zero-extension */),
                ref MemoryMarshal.GetReference(value),
                valueLength);
    }
#endif

    /// <summary>
    /// Creates a new Span over the portion of the target array beginning
    /// at 'startIndex' and ending at the end of the segment.
    /// </summary>
    /// <param name="segment">The target array.</param>
    /// <param name="startIndex">The index at which to begin the Span.</param>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(ArraySegment<T> segment, Index startIndex)
        => MemoryExtensions.AsSpan(segment, startIndex);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Index startIndex)
    {
        int actualIndex = startIndex.GetOffset(segment.Count);
        return MemoryExtensions.AsSpan(segment, actualIndex);
    }
#endif

    /// <summary>
    /// Creates a new Span over the portion of the target array using the range start and end indexes
    /// </summary>
    /// <param name="segment">The target array.</param>
    /// <param name="range">The range which has start and end indexes to use for slicing the array.</param>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(ArraySegment<T> segment, Range range)
        => MemoryExtensions.AsSpan(segment, range);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this ArraySegment<T> segment, Range range)
    {
        (int start, int length) = range.GetOffsetAndLength(segment.Count);
        return new Span<T>(segment.Array, segment.Offset + start, length);
    }
#endif

    /// <summary>
    /// Creates a new memory over the portion of the target array starting from
    /// 'startIndex' to the end of the array.
    /// </summary>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> AsMemory<T>(T[]? array, Index startIndex)
        => MemoryExtensions.AsMemory(array, startIndex);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> AsMemory<T>(this T[]? array, Index startIndex)
    {
        if (array == null)
        {
            if (!startIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

            return default;
        }

        int actualIndex = startIndex.GetOffset(array.Length);
        return MemoryExtensions.AsMemory(array, actualIndex);
    }
#endif

    /// <summary>
    /// Creates a new memory over the portion of the target array beginning at inclusive start index of the range
    /// and ending at the exclusive end index of the range.
    /// </summary>
#if NETSTANDARD2_1_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> AsMemory<T>(T[]? array, Range range)
        => MemoryExtensions.AsMemory<T>(array, range);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Memory<T> AsMemory<T>(this T[]? array, Range range)
    {
        if (array == null)
        {
            Index startIndex = range.Start;
            Index endIndex = range.End;
            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);

            return default;
        }

        (int start, int length) = range.GetOffsetAndLength(array.Length);
        return new Memory<T>(array, start, length);
    }
#endif

    /// <summary>
    /// Sorts the elements in the entire <see cref="Span{T}" /> using the <see cref="IComparable{T}" /> implementation
    /// of each element of the <see cref= "Span{T}" />
    /// </summary>
    /// <typeparam name="T">The type of the elements of the span.</typeparam>
    /// <param name="span">The <see cref="Span{T}"/> to sort.</param>
    /// <exception cref="InvalidOperationException">
    /// One or more elements in <paramref name="span"/> do not implement the <see cref="IComparable{T}" /> interface.
    /// </exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T>(Span<T> span)
        => MemoryExtensions.Sort(span);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T>(this Span<T> span)
        => Sort(span, (IComparer<T>?)null);
#endif

    /// <summary>
    /// Sorts the elements in the entire <see cref="Span{T}" /> using the <typeparamref name="TComparer" />.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the span.</typeparam>
    /// <typeparam name="TComparer">The type of the comparer to use to compare elements.</typeparam>
    /// <param name="span">The <see cref="Span{T}"/> to sort.</param>
    /// <param name="comparer">
    /// The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to
    /// use the <see cref="IComparable{T}"/> interface implementation of each element.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="comparer"/> is null, and one or more elements in <paramref name="span"/> do not
    /// implement the <see cref="IComparable{T}" /> interface.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The implementation of <paramref name="comparer"/> caused an error during the sort.
    /// </exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T, TComparer>(Span<T> span, TComparer comparer) where TComparer : IComparer<T>?
        => MemoryExtensions.Sort(span, comparer);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T, TComparer>(this Span<T> span, TComparer comparer) where TComparer : IComparer<T>?
    {
        if (span.Length > 1)
        {
            if (comparer is null)
                ArraySortHelperHidden<T>.Sort(span, Comparer<T>.Default.Compare);
            else
                ArraySortHelperHidden<T>.Sort(span, comparer.Compare);
        }
    }
#endif

    /// <summary>
    /// Sorts the elements in the entire <see cref="Span{T}" /> using the specified <see cref="Comparison{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the span.</typeparam>
    /// <param name="span">The <see cref="Span{T}"/> to sort.</param>
    /// <param name="comparison">The <see cref="Comparison{T}"/> to use when comparing elements.</param>
    /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is null.</exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T>(Span<T> span, Comparison<T> comparison)
        => MemoryExtensions.Sort(span, comparison);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<T>(this Span<T> span, Comparison<T> comparison)
    {
        if (comparison == null)
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);

        if (span.Length > 1)
        {
            ArraySortHelperHidden<T>.Sort(span, comparison!);
        }
    }
#endif

    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items)
    /// based on the keys in the first <see cref="Span{TKey}" /> using the <see cref="IComparable{T}" />
    /// implementation of each key.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements of the key span.</typeparam>
    /// <typeparam name="TValue">The type of the elements of the items span.</typeparam>
    /// <param name="keys">The span that contains the keys to sort.</param>
    /// <param name="items">The span that contains the items that correspond to the keys in <paramref name="keys"/>.</param>
    /// <exception cref="ArgumentException">
    /// The length of <paramref name="keys"/> isn't equal to the length of <paramref name="items"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// One or more elements in <paramref name="keys"/> do not implement the <see cref="IComparable{T}" /> interface.
    /// </exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue>(Span<TKey> keys, Span<TValue> items)
        => MemoryExtensions.Sort(keys, items);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue>(this Span<TKey> keys, Span<TValue> items)
        => Sort(keys, items, (IComparer<TKey>?)null);
#endif

    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items)
    /// based on the keys in the first <see cref="Span{TKey}" /> using the specified comparer.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements of the key span.</typeparam>
    /// <typeparam name="TValue">The type of the elements of the items span.</typeparam>
    /// <typeparam name="TComparer">The type of the comparer to use to compare elements.</typeparam>
    /// <param name="keys">The span that contains the keys to sort.</param>
    /// <param name="items">The span that contains the items that correspond to the keys in <paramref name="keys"/>.</param>
    /// <param name="comparer">
    /// The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to
    /// use the <see cref="IComparable{T}"/> interface implementation of each element.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The length of <paramref name="keys"/> isn't equal to the length of <paramref name="items"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="comparer"/> is null, and one or more elements in <paramref name="keys"/> do not
    /// implement the <see cref="IComparable{T}" /> interface.
    /// </exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue, TComparer>(Span<TKey> keys, Span<TValue> items, TComparer comparer) where TComparer : IComparer<TKey>?
        => MemoryExtensions.Sort(keys, items, comparer);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue, TComparer>(this Span<TKey> keys, Span<TValue> items, TComparer comparer) where TComparer : IComparer<TKey>?
    {
        if (keys.Length != items.Length)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_SpansMustHaveSameLength);

        if (keys.Length > 1)
        {
            ArraySortHelperHidden<TKey, TValue>.Sort(keys, items, comparer);
        }
    }
#endif

    /// <summary>
    /// Sorts a pair of spans (one containing the keys and the other containing the corresponding items)
    /// based on the keys in the first <see cref="Span{TKey}" /> using the specified comparison.
    /// </summary>
    /// <typeparam name="TKey">The type of the elements of the key span.</typeparam>
    /// <typeparam name="TValue">The type of the elements of the items span.</typeparam>
    /// <param name="keys">The span that contains the keys to sort.</param>
    /// <param name="items">The span that contains the items that correspond to the keys in <paramref name="keys"/>.</param>
    /// <param name="comparison">The <see cref="Comparison{T}"/> to use when comparing elements.</param>
    /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is null.</exception>
    /// <exception cref="ArgumentException">
    /// The length of <paramref name="keys"/> isn't equal to the length of <paramref name="items"/>.
    /// </exception>
#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue>(Span<TKey> keys, Span<TValue> items, Comparison<TKey> comparison)
        => MemoryExtensions.Sort(keys, items, comparison);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Sort<TKey, TValue>(this Span<TKey> keys, Span<TValue> items, Comparison<TKey> comparison)
    {
        if (comparison == null)
            ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
        if (keys.Length != items.Length)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_SpansMustHaveSameLength);

        if (keys.Length > 1)
        {
            ArraySortHelperHidden<TKey, TValue>.Sort(keys, items, new ComparisonComparerHidden<TKey>(comparison));
        }
    }
#endif

    /// <summary>
    /// Replaces all occurrences of <paramref name="oldValue"/> with <paramref name="newValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the span.</typeparam>
    /// <param name="span">The span in which the elements should be replaced.</param>
    /// <param name="oldValue">The value to be replaced with <paramref name="newValue"/>.</param>
    /// <param name="newValue">The value to replace all occurrences of <paramref name="oldValue"/>.</param>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void Replace<T>(Span<T> span, T oldValue, T newValue) where T : IEquatable<T>?
        => MemoryExtensions.Replace(span, oldValue, newValue);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void Replace<T>(this Span<T> span, T oldValue, T newValue) where T : IEquatable<T>?
    {
        ref T src2 = ref MemoryMarshal.GetReference(span);
        SpanHelpersHidden.Replace(ref src2, ref src2, oldValue, newValue, span.Length);
    }
#endif

    /// <summary>
    /// Copies <paramref name="source"/> to <paramref name="destination"/>, replacing all occurrences of <paramref name="oldValue"/> with <paramref name="newValue"/>.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="source">The span to copy.</param>
    /// <param name="destination">The span into which the copied and replaced values should be written.</param>
    /// <param name="oldValue">The value to be replaced with <paramref name="newValue"/>.</param>
    /// <param name="newValue">The value to replace all occurrences of <paramref name="oldValue"/>.</param>
    /// <exception cref="ArgumentException">The <paramref name="destination"/> span was shorter than the <paramref name="source"/> span.</exception>
    /// <exception cref="ArgumentException">The <paramref name="source"/> and <paramref name="destination"/> were overlapping but not referring to the same starting location.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void Replace<T>(ReadOnlySpan<T> source, Span<T> destination, T oldValue, T newValue) where T : IEquatable<T>?
        => MemoryExtensions.Replace(source, destination, oldValue, newValue);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void Replace<T>(this ReadOnlySpan<T> source, Span<T> destination, T oldValue, T newValue) where T : IEquatable<T>?
    {
        int length = source.Length;
        if (length == 0)
        {
            return;
        }

        if (length > destination.Length)
        {
            ThrowHelper.ThrowArgumentException_DestinationTooShort();
        }

        ref T src = ref MemoryMarshal.GetReference(source);
        ref T dst = ref MemoryMarshal.GetReference(destination);

        nint byteOffset = Unsafe.ByteOffset(ref src, ref dst);
#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        if (byteOffset != 0 &&
            ((nuint)byteOffset < (nuint)((nint)source.Length * sizeof(T)) ||
             (nuint)byteOffset > (nuint)(-((nint)destination.Length * sizeof(T)))))
        {
            ThrowHelper.ThrowArgumentException(ExceptionResource.InvalidOperation_SpanOverlappedOperation);
        }
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

        SpanHelpersHidden.Replace(ref src, ref dst, oldValue, newValue, length);
    }
#endif

    /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <returns>The length of the common prefix shared by the two spans.  If there's no shared prefix, 0 is returned.</returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(Span<T> span, ReadOnlySpan<T> other)
        => MemoryExtensions.CommonPrefixLength(span, other);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other)
        => MemoryExtensionsPolyfills.CommonPrefixLength((ReadOnlySpan<T>)span, other);
#endif

    /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing elements, or null to use the default <see cref="IEqualityComparer{T}"/> for the type of an element.</param>
    /// <returns>The length of the common prefix shared by the two spans.  If there's no shared prefix, 0 is returned.</returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        => MemoryExtensions.CommonPrefixLength(span, other, comparer);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(this Span<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        => MemoryExtensionsPolyfills.CommonPrefixLength((ReadOnlySpan<T>)span, other, comparer);
#endif

    /// <summary>Finds the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
    /// <typeparam name="T">The type of the elements in the spans.</typeparam>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <returns>The length of the common prefix shared by the two spans.  If there's no shared prefix, 0 is returned.</returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int CommonPrefixLength<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other)
        => MemoryExtensions.CommonPrefixLength(span, other);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
    {
        // Shrink one of the spans if necessary to ensure they're both the same length. We can then iterate until
        // the Length of one of them and at least have bounds checks removed from that one.
        MemoryExtensionsHidden.SliceLongerSpanToMatchShorterLength(ref span, ref other);

        // Find the first element pairwise that is not equal, and return its index as the length
        // of the sequence before it that matches.
        for (int i = 0; i < span.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(span[i], other[i]))
            {
                return i;
            }
        }

        return span.Length;
    }
#endif

    /// <summary>Determines the length of any common prefix shared between <paramref name="span"/> and <paramref name="other"/>.</summary>
    /// <typeparam name="T">The type of the elements in the sequences.</typeparam>
    /// <param name="span">The first sequence to compare.</param>
    /// <param name="other">The second sequence to compare.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing elements, or null to use the default <see cref="IEqualityComparer{T}"/> for the type of an element.</param>
    /// <returns>The length of the common prefix shared by the two spans.  If there's no shared prefix, 0 is returned.</returns>
#if NET7_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
        => MemoryExtensions.CommonPrefixLength(span, other, comparer);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CommonPrefixLength<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, IEqualityComparer<T>? comparer)
    {
        // If the comparer is null or the default, and T is a value type, we want to use EqualityComparer<T>.Default.Equals
        // directly to enable devirtualization.  The non-comparer overload already does so, so just use it.
        if (typeof(T).IsValueType() && (comparer is null || comparer == EqualityComparer<T>.Default))
        {
            return CommonPrefixLength(span, other);
        }

        // Shrink one of the spans if necessary to ensure they're both the same length. We can then iterate until
        // the Length of one of them and at least have bounds checks removed from that one.
        MemoryExtensionsHidden.SliceLongerSpanToMatchShorterLength(ref span, ref other);

        // Ensure we have a comparer, then compare the spans.
        comparer ??= EqualityComparer<T>.Default;
        for (int i = 0; i < span.Length; i++)
        {
            if (!comparer.Equals(span[i], other[i]))
            {
                return i;
            }
        }

        return span.Length;
    }
#endif

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating the <paramref name="destination"/> span
    /// with <see cref="Range"/> instances representing the regions between the separators.
    /// </summary>
    /// <param name="source">The source span to parse.</param>
    /// <param name="destination">The destination span into which the resulting ranges are written.</param>
    /// <param name="separator">A character that delimits the regions in this instance.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim whitespace and include empty ranges.</param>
    /// <returns>The number of ranges written into <paramref name="destination"/>.</returns>
    /// <remarks>
    /// <para>
    /// Delimiter characters are not included in the elements of the returned array.
    /// </para>
    /// <para>
    /// If the <paramref name="destination"/> span is empty, or if the <paramref name="options"/> specifies <see cref="StringSplitOptions.RemoveEmptyEntries"/> and <paramref name="source"/> is empty,
    /// or if <paramref name="options"/> specifies both <see cref="StringSplitOptions.RemoveEmptyEntries"/> and StringSplitOptions.TrimEntries and the <paramref name="source"/> is
    /// entirely whitespace, no ranges are written to the destination.
    /// </para>
    /// <para>
    /// If the span does not contain <paramref name="separator"/>, or if <paramref name="destination"/>'s length is 1, a single range will be output containing the entire <paramref name="source"/>,
    /// subject to the processing implied by <paramref name="options"/>.
    /// </para>
    /// <para>
    /// If there are more regions in <paramref name="source"/> than will fit in <paramref name="destination"/>, the first <paramref name="destination"/> length minus 1 ranges are
    /// stored in <paramref name="destination"/>, and a range for the remainder of <paramref name="source"/> is stored in <paramref name="destination"/>.
    /// </para>
    /// </remarks>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Split(ReadOnlySpan<char> source, Span<Range> destination, char separator, StringSplitOptions options = StringSplitOptions.None)
        => MemoryExtensions.Split(source, destination, separator, options);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, char separator, StringSplitOptions options = StringSplitOptions.None)
    {
        StringHidden.CheckStringSplitOptions(options);

        return MemoryExtensionsHidden.SplitCore(source, destination, MemoryMarshalPolyfills.CreateReadOnlySpan(ref separator, 1), default, isAny: true, options);
    }
#endif

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for the specified <paramref name="separator"/>, populating the <paramref name="destination"/> span
    /// with <see cref="Range"/> instances representing the regions between the separators.
    /// </summary>
    /// <param name="source">The source span to parse.</param>
    /// <param name="destination">The destination span into which the resulting ranges are written.</param>
    /// <param name="separator">A character that delimits the regions in this instance.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim whitespace and include empty ranges.</param>
    /// <returns>The number of ranges written into <paramref name="destination"/>.</returns>
    /// <remarks>
    /// <para>
    /// Delimiter characters are not included in the elements of the returned array.
    /// </para>
    /// <para>
    /// If the <paramref name="destination"/> span is empty, or if the <paramref name="options"/> specifies <see cref="StringSplitOptions.RemoveEmptyEntries"/> and <paramref name="source"/> is empty,
    /// or if <paramref name="options"/> specifies both <see cref="StringSplitOptions.RemoveEmptyEntries"/> and StringSplitOptions.TrimEntries and the <paramref name="source"/> is
    /// entirely whitespace, no ranges are written to the destination.
    /// </para>
    /// <para>
    /// If the span does not contain <paramref name="separator"/>, or if <paramref name="destination"/>'s length is 1, a single range will be output containing the entire <paramref name="source"/>,
    /// subject to the processing implied by <paramref name="options"/>.
    /// </para>
    /// <para>
    /// If there are more regions in <paramref name="source"/> than will fit in <paramref name="destination"/>, the first <paramref name="destination"/> length minus 1 ranges are
    /// stored in <paramref name="destination"/>, and a range for the remainder of <paramref name="source"/> is stored in <paramref name="destination"/>.
    /// </para>
    /// </remarks>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Split(ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None)
        => MemoryExtensions.Split(source, destination, separator, options);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Split(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separator, StringSplitOptions options = StringSplitOptions.None)
    {
        StringHidden.CheckStringSplitOptions(options);

        // If the separator is an empty string, the whole input is considered the sole range.
        if (separator.IsEmpty)
        {
            if (!destination.IsEmpty)
            {
                int startInclusive = 0, endExclusive = source.Length;

#if NET5_0_OR_GREATER
                if ((options & StringSplitOptions.TrimEntries) != 0)
                {
                    (startInclusive, endExclusive) = MemoryExtensionsHidden.TrimSplitEntry(source, startInclusive, endExclusive);
                }
#endif

                if (startInclusive != endExclusive || (options & StringSplitOptions.RemoveEmptyEntries) == 0)
                {
                    destination[0] = startInclusive..endExclusive;
                    return 1;
                }
            }

            return 0;
        }

        return MemoryExtensionsHidden.SplitCore(source, destination, separator, default, isAny: false, options);
    }
#endif

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>, populating the <paramref name="destination"/> span
    /// with <see cref="Range"/> instances representing the regions between the separators.
    /// </summary>
    /// <param name="source">The source span to parse.</param>
    /// <param name="destination">The destination span into which the resulting ranges are written.</param>
    /// <param name="separators">Any number of characters that may delimit the regions in this instance. If empty, all Unicode whitespace characters are used as the separators.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim whitespace and include empty ranges.</param>
    /// <returns>The number of ranges written into <paramref name="destination"/>.</returns>
    /// <remarks>
    /// <para>
    /// Delimiter characters are not included in the elements of the returned array.
    /// </para>
    /// <para>
    /// If the <paramref name="destination"/> span is empty, or if the <paramref name="options"/> specifies <see cref="StringSplitOptions.RemoveEmptyEntries"/> and <paramref name="source"/> is empty,
    /// or if <paramref name="options"/> specifies both <see cref="StringSplitOptions.RemoveEmptyEntries"/> and StringSplitOptions.TrimEntries and the <paramref name="source"/> is
    /// entirely whitespace, no ranges are written to the destination.
    /// </para>
    /// <para>
    /// If the span does not contain any of the <paramref name="separators"/>, or if <paramref name="destination"/>'s length is 1, a single range will be output containing the entire <paramref name="source"/>,
    /// subject to the processing implied by <paramref name="options"/>.
    /// </para>
    /// <para>
    /// If there are more regions in <paramref name="source"/> than will fit in <paramref name="destination"/>, the first <paramref name="destination"/> length minus 1 ranges are
    /// stored in <paramref name="destination"/>, and a range for the remainder of <paramref name="source"/> is stored in <paramref name="destination"/>.
    /// </para>
    /// </remarks>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SplitAny(ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separators, StringSplitOptions options = StringSplitOptions.None)
        => MemoryExtensions.SplitAny(source, destination, separators, options);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<char> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        StringHidden.CheckStringSplitOptions(options);

#if NET5_0_OR_GREATER
        // If the separators list is empty, whitespace is used as separators.  In that case, we want to ignore TrimEntries if specified,
        // since TrimEntries also impacts whitespace.  The TrimEntries flag must be left intact if we are constrained by count because we need to process last substring.
        if (separators.IsEmpty && destination.Length > source.Length)
        {
            options &= ~StringSplitOptions.TrimEntries;
        }
#endif

        return MemoryExtensionsHidden.SplitCore(source, destination, separators, default, isAny: true, options);
    }
#endif

    /// <summary>
    /// Parses the source <see cref="ReadOnlySpan{Char}"/> for one of the specified <paramref name="separators"/>, populating the <paramref name="destination"/> span
    /// with <see cref="Range"/> instances representing the regions between the separators.
    /// </summary>
    /// <param name="source">The source span to parse.</param>
    /// <param name="destination">The destination span into which the resulting ranges are written.</param>
    /// <param name="separators">Any number of strings that may delimit the regions in this instance.  If empty, all Unicode whitespace characters are used as the separators.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim whitespace and include empty ranges.</param>
    /// <returns>The number of ranges written into <paramref name="destination"/>.</returns>
    /// <remarks>
    /// <para>
    /// Delimiter characters are not included in the elements of the returned array.
    /// </para>
    /// <para>
    /// If the <paramref name="destination"/> span is empty, or if the <paramref name="options"/> specifies <see cref="StringSplitOptions.RemoveEmptyEntries"/> and <paramref name="source"/> is empty,
    /// or if <paramref name="options"/> specifies both <see cref="StringSplitOptions.RemoveEmptyEntries"/> and StringSplitOptions.TrimEntries and the <paramref name="source"/> is
    /// entirely whitespace, no ranges are written to the destination.
    /// </para>
    /// <para>
    /// If the span does not contain any of the <paramref name="separators"/>, or if <paramref name="destination"/>'s length is 1, a single range will be output containing the entire <paramref name="source"/>,
    /// subject to the processing implied by <paramref name="options"/>.
    /// </para>
    /// <para>
    /// If there are more regions in <paramref name="source"/> than will fit in <paramref name="destination"/>, the first <paramref name="destination"/> length minus 1 ranges are
    /// stored in <paramref name="destination"/>, and a range for the remainder of <paramref name="source"/> is stored in <paramref name="destination"/>.
    /// </para>
    /// </remarks>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SplitAny(ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<string> separators, StringSplitOptions options = StringSplitOptions.None)
        => MemoryExtensions.SplitAny(source, destination, separators, options);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SplitAny(this ReadOnlySpan<char> source, Span<Range> destination, ReadOnlySpan<string> separators, StringSplitOptions options = StringSplitOptions.None)
    {
        StringHidden.CheckStringSplitOptions(options);

#if NET5_0_OR_GREATER
        // If the separators list is empty, whitespace is used as separators.  In that case, we want to ignore TrimEntries if specified,
        // since TrimEntries also impacts whitespace.  The TrimEntries flag must be left intact if we are constrained by count because we need to process last substring.
        if (separators.IsEmpty && destination.Length > source.Length)
        {
            options &= ~StringSplitOptions.TrimEntries;
        }
#endif

        return MemoryExtensionsHidden.SplitCore(source, destination, default, separators!, isAny: true, options);
    }
#endif

    /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value for which to search.</param>
    /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.Count(span, value);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(this Span<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.Count((ReadOnlySpan<T>)span, value);
#endif

    /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value for which to search.</param>
    /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => MemoryExtensions.Count(span, value);
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>?
        => SpanHelpersHidden.Count(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

    /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value for which to search.</param>
    /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.Count(span, value);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(this Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensionsPolyfills.Count((ReadOnlySpan<T>)span, value);
#endif

    /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value for which to search.</param>
    /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
        => MemoryExtensions.Count(span, value);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Count<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>?
    {
        switch (value.Length)
        {
            case 0:
                return 0;

            case 1:
                return Count(span, value[0]);

            default:
                int count = 0;

                int pos;
                while ((pos = MemoryExtensionsPolyfills.IndexOf(span, value)) >= 0)
                {
                    span = span.Slice(pos + value.Length);
                    count++;
                }

                return count;
        }
    }
#endif

    /// <summary>Writes the specified interpolated string to the character span.</summary>
    /// <param name="destination">The span to which the interpolated string should be formatted.</param>
    /// <param name="handler">The interpolated string.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <returns>true if the entire interpolated string could be formatted successfully; otherwise, false.</returns>
#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite(Span<char> destination, [InterpolatedStringHandlerArgument(nameof(destination))] ref MemoryExtensions.TryWriteInterpolatedStringHandler handler, out int charsWritten)
        => MemoryExtensions.TryWrite(destination, ref handler, out charsWritten);
#else
    // no [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable IDE0060 // Remove unused parameter
    public static bool TryWrite(this Span<char> destination, [InterpolatedStringHandlerArgument(nameof(destination))] ref TryWriteInterpolatedStringHandler handler, out int charsWritten)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        // The span argument isn't used directly in the method; rather, it'll be used by the compiler to create the handler.
        // We could validate here that span == handler._destination, but that doesn't seem necessary.
        if (handler._success)
        {
            charsWritten = handler._pos;
            return true;
        }

        charsWritten = 0;
        return false;
    }
#endif

    /// <summary>Writes the specified interpolated string to the character span.</summary>
    /// <param name="destination">The span to which the interpolated string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="handler">The interpolated string.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <returns>true if the entire interpolated string could be formatted successfully; otherwise, false.</returns>
#if NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite(Span<char> destination, IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(destination), nameof(provider))] ref MemoryExtensions.TryWriteInterpolatedStringHandler handler, out int charsWritten)
        => MemoryExtensions.TryWrite(destination, provider, ref handler, out charsWritten);
#else
    // no [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable IDE0060 // Remove unused parameter
    public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, [InterpolatedStringHandlerArgument(nameof(destination), nameof(provider))] ref TryWriteInterpolatedStringHandler handler, out int charsWritten)
#pragma warning restore IDE0060 // Remove unused parameter
        // The provider is passed to the handler by the compiler, so the actual implementation of the method
        // is the same as the non-provider overload.
        => MemoryExtensionsPolyfills.TryWrite(destination, ref handler, out charsWritten);
#endif

    /// <summary>
    /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
    /// with the string representation of the corresponding arguments.
    /// </summary>
    /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
    /// <param name="destination">The span to which the string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A <see cref="CompositeFormat"/>.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <param name="arg0">The first object to format.</param>
    /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
    /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite<TArg0>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0)
        => MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static bool TryWrite<TArg0>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0)
    {
        ArgumentNullExceptionPolyfills.ThrowIfNull(format);
        format.ValidateNumberOfArgs(1);
        return MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, arg0, 0, 0, default);
    }
#endif

    /// <summary>
    /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
    /// with the string representation of the corresponding arguments.
    /// </summary>
    /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
    /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
    /// <param name="destination">The span to which the string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A <see cref="CompositeFormat"/>.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <param name="arg0">The first object to format.</param>
    /// <param name="arg1">The second object to format.</param>
    /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
    /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite<TArg0, TArg1>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1)
        => MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0, arg1);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static bool TryWrite<TArg0, TArg1>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1)
    {
        ArgumentNullExceptionPolyfills.ThrowIfNull(format);
        format.ValidateNumberOfArgs(2);
        return MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, arg0, arg1, 0, default);
    }
#endif

    /// <summary>
    /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
    /// with the string representation of the corresponding arguments.
    /// </summary>
    /// <typeparam name="TArg0">The type of the first object to format.</typeparam>
    /// <typeparam name="TArg1">The type of the second object to format.</typeparam>
    /// <typeparam name="TArg2">The type of the third object to format.</typeparam>
    /// <param name="destination">The span to which the string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A <see cref="CompositeFormat"/>.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <param name="arg0">The first object to format.</param>
    /// <param name="arg1">The second object to format.</param>
    /// <param name="arg2">The third object to format.</param>
    /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
    /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite<TArg0, TArg1, TArg2>(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1, TArg2 arg2)
        => MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, arg0, arg1, arg2);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static bool TryWrite<TArg0, TArg1, TArg2>(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        ArgumentNullExceptionPolyfills.ThrowIfNull(format);
        format.ValidateNumberOfArgs(3);
        return MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, arg0, arg1, arg2, default);
    }
#endif

    /// <summary>
    /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
    /// with the string representation of the corresponding arguments.
    /// </summary>
    /// <param name="destination">The span to which the string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A <see cref="CompositeFormat"/>.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <param name="args">An array of objects to format.</param>
    /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="args"/> is null.</exception>
    /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params object?[] args)
        => MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, args);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, params object?[] args)
    {
        ArgumentNullExceptionPolyfills.ThrowIfNull(format);
        ArgumentNullExceptionPolyfills.ThrowIfNull(args);
        return MemoryExtensionsPolyfills.TryWrite(destination, provider, format, out charsWritten, (ReadOnlySpan<object?>)args);
    }
#endif

    /// <summary>
    /// Writes the <see cref="CompositeFormat"/> string to the character span, substituting the format item or items
    /// with the string representation of the corresponding arguments.
    /// </summary>
    /// <param name="destination">The span to which the string should be formatted.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A <see cref="CompositeFormat"/>.</param>
    /// <param name="charsWritten">The number of characters written to the span.</param>
    /// <param name="args">A span of objects to format.</param>
    /// <returns><see langword="true"/> if the entire interpolated string could be formatted successfully; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="format"/> is null.</exception>
    /// <exception cref="FormatException">The index of a format item is greater than or equal to the number of supplied arguments.</exception>
#if NET8_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryWrite(Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, ReadOnlySpan<object?> args)
        => MemoryExtensions.TryWrite(destination, provider, format, out charsWritten, args);
#else
    // No [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [CLSCompliant(false)]
    public static bool TryWrite(this Span<char> destination, IFormatProvider? provider, CompositeFormat format, out int charsWritten, ReadOnlySpan<object?> args)
    {
        ArgumentNullExceptionPolyfills.ThrowIfNull(format);
        format.ValidateNumberOfArgs(args.Length);
        return args.Length switch
        {
            0 => MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, 0, 0, 0, args),
            1 => MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, args[0], 0, 0, args),
            2 => MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, args[0], args[1], 0, args),
            _ => MemoryExtensionsHidden.TryWrite(destination, provider, format, out charsWritten, args[0], args[1], args[2], args),
        };
    }
#endif

    /// <summary>Provides a handler used by the language compiler to format interpolated strings into character spans.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [InterpolatedStringHandler]
    public ref struct TryWriteInterpolatedStringHandler
    {
        // Implementation note:
        // As this type is only intended to be targeted by the compiler, public APIs eschew argument validation logic
        // in a variety of places, e.g. allowing a null input when one isn't expected to produce a NullReferenceException rather
        // than an ArgumentNullException.

        /// <summary>The destination buffer.</summary>
        private readonly Span<char> _destination;
        /// <summary>Optional provider to pass to IFormattable.ToString or ISpanFormattable.TryFormat calls.</summary>
        private readonly IFormatProvider? _provider;
        /// <summary>The number of characters written to <see cref="_destination"/>.</summary>
        internal int _pos;
        /// <summary>true if all formatting operations have succeeded; otherwise, false.</summary>
        internal bool _success;
        /// <summary>Whether <see cref="_provider"/> provides an ICustomFormatter.</summary>
        /// <remarks>
        /// Custom formatters are very rare.  We want to support them, but it's ok if we make them more expensive
        /// in order to make them as pay-for-play as possible.  So, we avoid adding another reference type field
        /// to reduce the size of the handler and to reduce required zero'ing, by only storing whether the provider
        /// provides a formatter, rather than actually storing the formatter.  This in turn means, if there is a
        /// formatter, we pay for the extra interface call on each AppendFormatted that needs it.
        /// </remarks>
        private readonly bool _hasCustomFormatter;

        /// <summary>Creates a handler used to write an interpolated string into a <see cref="Span{Char}"/>.</summary>
        /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
        /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
        /// <param name="destination">The destination buffer.</param>
        /// <param name="shouldAppend">Upon return, true if the destination may be long enough to support the formatting, or false if it won't be.</param>
        /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
#pragma warning disable IDE0060 // Remove unused parameter
        public TryWriteInterpolatedStringHandler(int literalLength, int formattedCount, Span<char> destination, out bool shouldAppend)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            _destination = destination;
            _provider = null;
            _pos = 0;
            _success = shouldAppend = destination.Length >= literalLength;
            _hasCustomFormatter = false;
        }

        /// <summary>Creates a handler used to write an interpolated string into a <see cref="Span{Char}"/>.</summary>
        /// <param name="literalLength">The number of constant characters outside of interpolation expressions in the interpolated string.</param>
        /// <param name="formattedCount">The number of interpolation expressions in the interpolated string.</param>
        /// <param name="destination">The destination buffer.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="shouldAppend">Upon return, true if the destination may be long enough to support the formatting, or false if it won't be.</param>
        /// <remarks>This is intended to be called only by compiler-generated code. Arguments are not validated as they'd otherwise be for members intended to be used directly.</remarks>
#pragma warning disable IDE0060 // Remove unused parameter
        public TryWriteInterpolatedStringHandler(int literalLength, int formattedCount, Span<char> destination, IFormatProvider? provider, out bool shouldAppend)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            _destination = destination;
            _provider = provider;
            _pos = 0;
            _success = shouldAppend = destination.Length >= literalLength;
            _hasCustomFormatter = provider is not null && DefaultInterpolatedStringHandlerHidden.HasCustomFormatter(provider);
        }

        /// <summary>Writes the specified string to the handler.</summary>
        /// <param name="value">The string to write.</param>
        /// <returns>true if the value could be formatted to the span; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AppendLiteral(string value)
        {
            if (value.TryCopyTo(_destination.Slice(_pos)))
            {
                _pos += value.Length;
                return true;
            }

            return Fail();
        }

        #region AppendFormatted
        // Design note:
        // This provides the same set of overloads and semantics as DefaultInterpolatedStringHandler.

        #region AppendFormatted T
        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        public bool AppendFormatted<T>(T value)
        {
            // This method could delegate to AppendFormatted with a null format, but explicitly passing
            // default as the format to TryFormat helps to improve code quality in some cases when TryFormat is inlined,
            // e.g. for Int32 it enables the JIT to eliminate code in the inlined method based on a length check on the format.

            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                return AppendCustomFormatter(value, format: null);
            }

            // Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
            // derives from the former.  For value types, it won't matter as the type checks devolve into
            // JIT-time constants.  For reference types, they're more likely to implement IFormattable
            // than they are to implement ISpanFormattable: if they don't implement either, we save an
            // interface check over first checking for ISpanFormattable and then for IFormattable, and
            // if it only implements IFormattable, we come out even: only if it implements both do we
            // end up paying for an extra interface check.
            string? s;
#pragma warning disable IDE0038 // Use pattern matching
            if (value is IFormattable)
            {
                // If the value can format itself directly into our buffer, do so.

                if (typeof(T).IsEnum())
                {
#if NET8_0_OR_GREATER
                    if (EnumHidden.TryFormatUnconstrained(value, _destination.Slice(_pos), out int charsWritten))
                    {
                        _pos += charsWritten;
                        return true;
                    }
#endif

                    return Fail();
                }

#if NET6_0_OR_GREATER
                if (value is ISpanFormattable)
                {
                    if (((ISpanFormattable)value).TryFormat(_destination.Slice(_pos), out int charsWritten, default, _provider)) // constrained call avoiding boxing for value types
                    {
                        _pos += charsWritten;
                        return true;
                    }

                    return Fail();
                }
#endif

                s = ((IFormattable)value).ToString(format: null, _provider); // constrained call avoiding boxing for value types
            }
            else
            {
                s = value?.ToString();
            }
#pragma warning restore IDE0038 // Use pattern matching

            return s is null || AppendLiteral(s);
        }

        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format string.</param>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        public bool AppendFormatted<T>(T value, string? format)
        {
            // If there's a custom formatter, always use it.
            if (_hasCustomFormatter)
            {
                return AppendCustomFormatter(value, format);
            }

            // Check first for IFormattable, even though we'll prefer to use ISpanFormattable, as the latter
            // derives from the former.  For value types, it won't matter as the type checks devolve into
            // JIT-time constants.  For reference types, they're more likely to implement IFormattable
            // than they are to implement ISpanFormattable: if they don't implement either, we save an
            // interface check over first checking for ISpanFormattable and then for IFormattable, and
            // if it only implements IFormattable, we come out even: only if it implements both do we
            // end up paying for an extra interface check.
            string? s;
#pragma warning disable IDE0038 // Use pattern matching
            if (value is IFormattable)
            {
                // If the value can format itself directly into our buffer, do so.

                if (typeof(T).IsEnum())
                {
                    if (EnumHidden.TryFormatUnconstrained(value, _destination.Slice(_pos), out int charsWritten, format.AsSpan()))
                    {
                        _pos += charsWritten;
                        return true;
                    }

                    return Fail();
                }

#if NET6_0_OR_GREATER
                if (value is ISpanFormattable)
                {
                    if (((ISpanFormattable)value).TryFormat(_destination.Slice(_pos), out int charsWritten, format, _provider)) // constrained call avoiding boxing for value types
                    {
                        _pos += charsWritten;
                        return true;
                    }

                    return Fail();
                }
#endif

                s = ((IFormattable)value).ToString(format, _provider); // constrained call avoiding boxing for value types
            }
            else
            {
                s = value?.ToString();
            }
#pragma warning restore IDE0038 // Use pattern matching

            return s is null || AppendLiteral(s);
        }

        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        public bool AppendFormatted<T>(T value, int alignment)
        {
            int startingPos = _pos;
            if (AppendFormatted(value))
            {
                return alignment == 0 || TryAppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }

            return Fail();
        }

        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format string.</param>
        /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        public bool AppendFormatted<T>(T value, int alignment, string? format)
        {
            int startingPos = _pos;
            if (AppendFormatted(value, format))
            {
                return alignment == 0 || TryAppendOrInsertAlignmentIfNeeded(startingPos, alignment);
            }

            return Fail();
        }
#endregion

        #region AppendFormatted ReadOnlySpan<char>
        /// <summary>Writes the specified character span to the handler.</summary>
        /// <param name="value">The span to write.</param>
        public bool AppendFormatted(scoped ReadOnlySpan<char> value)
        {
            // Fast path for when the value fits in the current buffer
            if (value.TryCopyTo(_destination.Slice(_pos)))
            {
                _pos += value.Length;
                return true;
            }

            return Fail();
        }

        /// <summary>Writes the specified string of chars to the handler.</summary>
        /// <param name="value">The span to write.</param>
        /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        /// <param name="format">The format string.</param>
#pragma warning disable IDE0060 // Remove unused parameter
        public bool AppendFormatted(scoped ReadOnlySpan<char> value, int alignment = 0, string? format = null)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            bool leftAlign = false;
            if (alignment < 0)
            {
                leftAlign = true;
                alignment = -alignment;
            }

            int paddingRequired = alignment - value.Length;
            if (paddingRequired <= 0)
            {
                // The value is as large or larger than the required amount of padding,
                // so just write the value.
                return AppendFormatted(value);
            }

            // Write the value along with the appropriate padding.
            Debug.Assert(alignment > value.Length);
            if (alignment <= _destination.Length - _pos)
            {
                if (leftAlign)
                {
                    value.CopyTo(_destination.Slice(_pos));
                    _pos += value.Length;
                    _destination.Slice(_pos, paddingRequired).Fill(' ');
                    _pos += paddingRequired;
                }
                else
                {
                    _destination.Slice(_pos, paddingRequired).Fill(' ');
                    _pos += paddingRequired;
                    value.CopyTo(_destination.Slice(_pos));
                    _pos += value.Length;
                }

                return true;
            }

            return Fail();
        }
        #endregion

        #region AppendFormatted string
        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        public bool AppendFormatted(string? value)
        {
            if (_hasCustomFormatter)
            {
                return AppendCustomFormatter(value, format: null);
            }

            if (value is null)
            {
                return true;
            }

            if (value.TryCopyTo(_destination.Slice(_pos)))
            {
                _pos += value.Length;
                return true;
            }

            return Fail();
        }

        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        /// <param name="format">The format string.</param>
        public bool AppendFormatted(string? value, int alignment = 0, string? format = null) =>
            // Format is meaningless for strings and doesn't make sense for someone to specify.  We have the overload
            // simply to disambiguate between ROS<char> and object, just in case someone does specify a format, as
            // string is implicitly convertible to both. Just delegate to the T-based implementation.
            AppendFormatted<string?>(value, alignment, format);
        #endregion

        #region AppendFormatted object
        /// <summary>Writes the specified value to the handler.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="alignment">Minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        /// <param name="format">The format string.</param>
        public bool AppendFormatted(object? value, int alignment = 0, string? format = null) =>
            // This overload is expected to be used rarely, only if either a) something strongly typed as object is
            // formatted with both an alignment and a format, or b) the compiler is unable to target type to T. It
            // exists purely to help make cases from (b) compile. Just delegate to the T-based implementation.
            AppendFormatted<object?>(value, alignment, format);
        #endregion
#endregion

        /// <summary>Formats the value using the custom formatter from the provider.</summary>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format string.</param>
        /// <typeparam name="T">The type of the value to write.</typeparam>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool AppendCustomFormatter<T>(T value, string? format)
        {
            // This case is very rare, but we need to handle it prior to the other checks in case
            // a provider was used that supplied an ICustomFormatter which wanted to intercept the particular value.
            // We do the cast here rather than in the ctor, even though this could be executed multiple times per
            // formatting, to make the cast pay for play.
            Debug.Assert(_hasCustomFormatter);
            Debug.Assert(_provider != null);

            ICustomFormatter? formatter = (ICustomFormatter?)_provider!.GetFormat(typeof(ICustomFormatter));
            Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

            if (formatter is not null && formatter.Format(format, value, _provider) is string customFormatted)
            {
                return AppendLiteral(customFormatted);
            }

            return true;
        }

        /// <summary>Handles adding any padding required for aligning a formatted value in an interpolation expression.</summary>
        /// <param name="startingPos">The position at which the written value started.</param>
        /// <param name="alignment">Non-zero minimum number of characters that should be written for this value.  If the value is negative, it indicates left-aligned and the required minimum is the absolute value.</param>
        private bool TryAppendOrInsertAlignmentIfNeeded(int startingPos, int alignment)
        {
            Debug.Assert(startingPos >= 0 && startingPos <= _pos);
            Debug.Assert(alignment != 0);

            int charsWritten = _pos - startingPos;

            bool leftAlign = false;
            if (alignment < 0)
            {
                leftAlign = true;
                alignment = -alignment;
            }

            int paddingNeeded = alignment - charsWritten;
            if (paddingNeeded <= 0)
            {
                return true;
            }

            if (paddingNeeded <= _destination.Length - _pos)
            {
                if (leftAlign)
                {
                    _destination.Slice(_pos, paddingNeeded).Fill(' ');
                }
                else
                {
                    _destination.Slice(startingPos, charsWritten).CopyTo(_destination.Slice(startingPos + paddingNeeded));
                    _destination.Slice(startingPos, paddingNeeded).Fill(' ');
                }

                _pos += paddingNeeded;
                return true;
            }

            return Fail();
        }

        /// <summary>Marks formatting as having failed and returns false.</summary>
        private bool Fail()
        {
            _success = false;
            return false;
        }
    }
}

#endif
