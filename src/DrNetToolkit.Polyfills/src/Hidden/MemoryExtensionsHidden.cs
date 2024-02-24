// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Diagnostics;
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

    /// <summary>Determines if one span is longer than the other, and slices the longer one to match the length of the shorter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SliceLongerSpanToMatchShorterLength<T>(ref ReadOnlySpan<T> span, ref ReadOnlySpan<T> other)
    {
        if (other.Length > span.Length)
        {
            other = other.Slice(0, span.Length);
        }
        else if (span.Length > other.Length)
        {
            span = span.Slice(0, other.Length);
        }
        Debug.Assert(span.Length == other.Length);
    }

    /// <summary>Core implementation for all of the Split{Any}AsRanges methods.</summary>
    /// <param name="source">The source span to parse.</param>
    /// <param name="destination">The destination span into which the resulting ranges are written.</param>
    /// <param name="separatorOrSeparators">Either a single separator (one or more characters in length) or multiple individual 1-character separators.</param>
    /// <param name="stringSeparators">Strings to use as separators instead of <paramref name="separatorOrSeparators"/>.</param>
    /// <param name="isAny">true if the separators are a set; false if <paramref name="separatorOrSeparators"/> should be treated as a single separator.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim whitespace and include empty ranges.</param>
    /// <returns>The number of ranges written into <paramref name="destination"/>.</returns>
    /// <remarks>This implementation matches the various quirks of string.Split.</remarks>
    public static int SplitCore(
        ReadOnlySpan<char> source, Span<Range> destination,
        ReadOnlySpan<char> separatorOrSeparators, ReadOnlySpan<string?> stringSeparators, bool isAny,
        StringSplitOptions options)
    {
        // If the destination is empty, there's nothing to do.
        if (destination.IsEmpty)
        {
            return 0;
        }

        bool keepEmptyEntries = (options & StringSplitOptions.RemoveEmptyEntries) == 0;
#if NET5_0_OR_GREATER
        bool trimEntries = (options & StringSplitOptions.TrimEntries) != 0;
#else
        bool trimEntries = false;
#endif

        // If the input is empty, then we either return an empty range as the sole range, or if empty entries
        // are to be removed, we return nothing.
        if (source.Length == 0)
        {
            if (keepEmptyEntries)
            {
                destination[0] = default;
                return 1;
            }

            return 0;
        }

        int startInclusive = 0, endExclusive;

        // If the destination has only one slot, then we need to return the whole input, subject to the options.
        if (destination.Length == 1)
        {
            endExclusive = source.Length;
            if (trimEntries)
            {
                (startInclusive, endExclusive) = TrimSplitEntry(source, startInclusive, endExclusive);
            }

            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                destination[0] = startInclusive..endExclusive;
                return 1;
            }

            return 0;
        }

        scoped ValueListBuilderHidden<int> separatorList = new ValueListBuilderHidden<int>(stackalloc int[StringHidden.StackallocIntBufferSizeLimit]);
        scoped ValueListBuilderHidden<int> lengthList = default;

        int separatorLength;
        int rangeCount = 0;
        if (!stringSeparators.IsEmpty)
        {
            lengthList = new ValueListBuilderHidden<int>(stackalloc int[StringHidden.StackallocIntBufferSizeLimit]);
            StringHidden.MakeSeparatorListAny(source, stringSeparators, ref separatorList, ref lengthList);
            separatorLength = -1; // Will be set on each iteration of the loop
        }
        else if (isAny)
        {
            StringHidden.MakeSeparatorListAny(source, separatorOrSeparators, ref separatorList);
            separatorLength = 1;
        }
        else
        {
            StringHidden.MakeSeparatorList(source, separatorOrSeparators, ref separatorList);
            separatorLength = separatorOrSeparators.Length;
        }

        // Try to fill in all but the last slot in the destination.  The last slot is reserved for whatever remains
        // after the last discovered separator. If the options specify that empty entries are to be removed, then we
        // need to skip past all of those here as well, including any that occur at the beginning of the last entry,
        // which is why we enter the loop if remove empty entries is set, even if we've already added enough entries.
        int separatorIndex = 0;
        Span<Range> destinationMinusOne = destination.Slice(0, destination.Length - 1);
        while (separatorIndex < separatorList.Length && (rangeCount < destinationMinusOne.Length || !keepEmptyEntries))
        {
            endExclusive = separatorList[separatorIndex];
            if (separatorIndex < lengthList.Length)
            {
                separatorLength = lengthList[separatorIndex];
            }
            separatorIndex++;

            // Trim off whitespace from the start and end of the range.
            int untrimmedEndExclusive = endExclusive;
            if (trimEntries)
            {
                (startInclusive, endExclusive) = TrimSplitEntry(source, startInclusive, endExclusive);
            }

            // If the range is not empty or we're not ignoring empty ranges, store it.
            Debug.Assert(startInclusive <= endExclusive);
            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                // If we're not keeping empty entries, we may have entered the loop even if we'd
                // already written enough ranges.  Now that we know this entry isn't empty, we
                // need to validate there's still room remaining.
                if ((uint)rangeCount >= (uint)destinationMinusOne.Length)
                {
                    break;
                }

                destinationMinusOne[rangeCount] = startInclusive..endExclusive;
                rangeCount++;
            }

            // Reset to be just past the separator, and loop around to go again.
            startInclusive = untrimmedEndExclusive + separatorLength;
        }

        separatorList.Dispose();
        lengthList.Dispose();

        // Either we found at least destination.Length - 1 ranges or we didn't find any more separators.
        // If we still have a last destination slot available and there's anything left in the source,
        // put a range for the remainder of the source into the destination.
        if ((uint)rangeCount < (uint)destination.Length)
        {
            endExclusive = source.Length;
            if (trimEntries)
            {
                (startInclusive, endExclusive) = TrimSplitEntry(source, startInclusive, endExclusive);
            }

            if (startInclusive != endExclusive || keepEmptyEntries)
            {
                destination[rangeCount] = startInclusive..endExclusive;
                rangeCount++;
            }
        }

        // Return how many ranges were written.
        return rangeCount;
    }

    /// <summary>Updates the starting and ending markers for a range to exclude whitespace.</summary>
    public static (int StartInclusive, int EndExclusive) TrimSplitEntry(ReadOnlySpan<char> source, int startInclusive, int endExclusive)
    {
        while (startInclusive < endExclusive && char.IsWhiteSpace(source[startInclusive]))
        {
            startInclusive++;
        }

        while (endExclusive > startInclusive && char.IsWhiteSpace(source[endExclusive - 1]))
        {
            endExclusive--;
        }

        return (startInclusive, endExclusive);
    }
}

#endif
