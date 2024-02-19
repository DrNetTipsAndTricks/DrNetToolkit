// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

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
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.array);

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
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.array);

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
                ThrowHelper.ThrowArgumentOutOfRangeException(ThrowHelper.ExceptionArgument.startIndex);
            }

            return default;
        }

        int actualIndex = startIndex.GetOffset(text.Length);
        if ((uint)actualIndex > (uint)text.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(ThrowHelper.ExceptionArgument.startIndex);
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
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.text);
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
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.text);

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
                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.text);

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
    public static unsafe bool Contains<T>(Span<T> span, T value) where T : IEquatable<T>? // It should be without this here!
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
    public static unsafe bool Contains<T>(ReadOnlySpan<T> span, T value) where T : IEquatable<T>? // It should be without this here!
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

#if NET6_0
    /// <summary>
    /// Searches for the specified value and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, T value) where T : IEquatable<T>? // It should be without this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

#if NET6_0
    /// <summary>
    /// Searches for the specified sequence and returns the index of its first occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int IndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // It should be without this here!
        => SpanHelpersHidden.IndexOf(ref MemoryMarshal.GetReference(span), span.Length, ref MemoryMarshal.GetReference(value), value.Length);
#endif

#if NET6_0
    /// <summary>
    /// Searches for the specified value and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The value to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, T value) where T : IEquatable<T>? // It should be without this here!
        => SpanHelpersHidden.LastIndexOf(ref MemoryMarshal.GetReference(span), value, span.Length);
#endif

#if NET6_0
    /// <summary>
    /// Searches for the specified sequence and returns the index of its last occurrence. If not found, returns -1. Values are compared using IEquatable{T}.Equals(T).
    /// </summary>
    /// <param name="span">The span to search.</param>
    /// <param name="value">The sequence to search for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int LastIndexOf<T>(Span<T> span, ReadOnlySpan<T> value) where T : IEquatable<T>? // It should be without this here!
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
    //
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
    //
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
}

#endif
