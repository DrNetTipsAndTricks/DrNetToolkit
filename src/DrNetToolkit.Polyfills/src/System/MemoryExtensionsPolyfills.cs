// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Polyfills.Hidden;
using DrNetToolkit.Polyfills.Internals;

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

}

#endif
