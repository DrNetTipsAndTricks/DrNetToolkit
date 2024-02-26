// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Internals;

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// Implementations of <see cref="string"/> hidden methods.
/// </summary>
public static class StringHidden
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public const int StackallocIntBufferSizeLimit = 128;
    public const int StackallocCharBufferSizeLimit = 256;

    /// <summary>
    /// Length of null terminated UTF-16 string.
    /// </summary>
    /// <param name="ptr">Null terminated UTF-16 string.</param>
    /// <returns>Length of null terminated UTF-16 string.</returns>
    [CLSCompliant(false)]
    public static unsafe int wcslen(char* ptr) => SpanHelpersHidden.IndexOfNullCharacter(ptr);

    /// <summary>
    /// Length of null terminated UTF-8 string.
    /// </summary>
    /// <param name="ptr">Null terminated UTF-8 string.</param>
    /// <returns>Length of null terminated UTF-8 string.</returns>
    [CLSCompliant(false)]
    public static unsafe int strlen(byte* ptr) => SpanHelpersHidden.IndexOfNullByte(ptr);

    public static void CheckStringSplitOptions(StringSplitOptions options)
    {
        const StringSplitOptions AllValidFlags = StringSplitOptions.RemoveEmptyEntries
#if NET5_0_OR_GREATER
            | StringSplitOptions.TrimEntries
#endif
            ;

        if ((options & ~AllValidFlags) != 0)
        {
            // at least one invalid flag was set
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidFlag, ExceptionArgument.options);
        }
    }

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Uses ValueListBuilder to create list that holds indexes of separators in string.
    /// </summary>
    /// <param name="source">The source to parse.</param>
    /// <param name="separators"><see cref="ReadOnlySpan{T}"/> of separator chars</param>
    /// <param name="sepListBuilder"><see cref="ValueListBuilderHidden{T}"/> to store indexes</param>
    public static void MakeSeparatorListAny(ReadOnlySpan<char> source, ReadOnlySpan<char> separators, ref ValueListBuilderHidden<int> sepListBuilder)
    {
        // Special-case no separators to mean any whitespace is a separator.
        if (separators.Length == 0)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (char.IsWhiteSpace(source[i]))
                {
                    sepListBuilder.Append(i);
                }
            }
        }

        // Special-case the common cases of 1, 2, and 3 separators, with manual comparisons against each separator.
        else if (true || separators.Length <= 3)
        {
            char sep0, sep1, sep2;
            sep0 = separators[0];
            sep1 = separators.Length > 1 ? separators[1] : sep0;
            sep2 = separators.Length > 2 ? separators[2] : sep1;
#if false && NET7_0_OR_GREATER
            if (Vector128.IsHardwareAccelerated && source.Length >= Vector128<ushort>.Count * 2)
            {
                MakeSeparatorListVectorized(source, ref sepListBuilder, sep0, sep1, sep2);
                return;
            }
#endif

            for (int i = 0; i < source.Length; i++)
            {
                char c = source[i];
                if (c == sep0 || c == sep1 || c == sep2)
                {
                    sepListBuilder.Append(i);
                }
            }
        }

#if false
        // Handle > 3 separators with a probabilistic map, ala IndexOfAny.
        // This optimizes for chars being unlikely to match a separator.
        else
        {
            unsafe
            {
                var map = new ProbabilisticMap(separators);
                ref uint charMap = ref Unsafe.As<ProbabilisticMap, uint>(ref map);

                for (int i = 0; i < source.Length; i++)
                {
                    if (ProbabilisticMap.Contains(ref charMap, separators, source[i]))
                    {
                        sepListBuilder.Append(i);
                    }
                }
            }
        }
#endif
    }

    /// <summary>
    /// Uses ValueListBuilder to create list that holds indexes of separators in string.
    /// </summary>
    /// <param name="source">The source to parse.</param>
    /// <param name="separator">separator string</param>
    /// <param name="sepListBuilder"><see cref="ValueListBuilderHidden{T}"/> to store indexes</param>
    internal static void MakeSeparatorList(ReadOnlySpan<char> source, ReadOnlySpan<char> separator, ref ValueListBuilderHidden<int> sepListBuilder)
    {
        Debug.Assert(!separator.IsEmpty, "Empty separator");

        int i = 0;
        while (!source.IsEmpty)
        {
            int index = source.IndexOf(separator);
            if (index < 0)
            {
                break;
            }

            i += index;
            sepListBuilder.Append(i);

            i += separator.Length;
            source = source.Slice(index + separator.Length);
        }
    }

    /// <summary>
    /// Uses ValueListBuilder to create list that holds indexes of separators in string and list that holds length of separator strings.
    /// </summary>
    /// <param name="source">The source to parse.</param>
    /// <param name="separators">separator strings</param>
    /// <param name="sepListBuilder"><see cref="ValueListBuilderHidden{T}"/> for separator indexes</param>
    /// <param name="lengthListBuilder"><see cref="ValueListBuilderHidden{T}"/> for separator length values</param>
    public static void MakeSeparatorListAny(ReadOnlySpan<char> source, ReadOnlySpan<string?> separators, ref ValueListBuilderHidden<int> sepListBuilder, ref ValueListBuilderHidden<int> lengthListBuilder)
    {
        Debug.Assert(!separators.IsEmpty, "Zero separators");

        for (int i = 0; i < source.Length; i++)
        {
            for (int j = 0; j < separators.Length; j++)
            {
                string? separator = separators[j];
                if (string.IsNullOrEmpty(separator))
                {
                    continue;
                }
                int currentSepLength = separator!.Length;
                if (source[i] == separator[0] && currentSepLength <= source.Length - i)
                {
                    if (currentSepLength == 1 || source.Slice(i, currentSepLength).SequenceEqual(separator.AsSpan()))
                    {
                        sepListBuilder.Append(i);
                        lengthListBuilder.Append(currentSepLength);
                        i += currentSepLength - 1;
                        break;
                    }
                }
            }
        }
    }
#endif

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
