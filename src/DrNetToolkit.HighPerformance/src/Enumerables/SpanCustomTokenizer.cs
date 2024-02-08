// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Enumerables;

/// <summary>
/// Helper class for <see cref="SpanCustomTokenizer{T}"/> structure.
/// </summary>
public static class SpanCustomTokenizer
{
    /// <summary>
    /// Splits the source span into a token and a non-tokenized part.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <paramref name="source"/> span.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> instance to be tokenized.</param>
    /// <returns>
    /// A tuple of (`TokenRange`, `NonTokenizedRange`).
    /// 
    /// The `TokenRange` is a tuple of two indexes (int `Start`, int `End`) that represents the next token in the
    /// <paramref name="source"/> span.
    /// 
    /// If the next token is not found, the delegate must return a TokenRange where `End` is less than `Start`.
    /// 
    /// The NonTokenizedRange is a tuple of two indices (int `Start`, int `End`) that represents the remaining part of
    /// the <paramref name="source"/> span after or before the found token.
    /// </returns>
    public delegate ((int Start, int End) TokenRange, (int Start, int End) NonTokenizedRange)
        NextToken<T>(ReadOnlySpan<T> source);

    /// <summary>
    /// Defines a delegate for trimming operations that remove leading and trailing elements from a span of elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <paramref name="span"/>.</typeparam>
    /// <param name="span">The readonly span to trim.</param>
    /// <returns>
    /// A tuple of two indices (int `Start`, int `End`) that represent the first inclusive and last exclusive elements
    /// of the trimmed <paramref name="span"/> part.
    /// </returns>
    public delegate (int Start, int End) Trimmer<T>(ReadOnlySpan<T> span);

    /// <summary>
    /// Creates the necessary functions for the <see cref="SpanCustomTokenizer{T}"/> to operate with the specified
    /// parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in a span.</typeparam>
    /// <param name="options">Defines the behavior for handling empty tokens and trimming tokens.</param>
    /// <param name="separator">An array of delimiter items.</param>
    /// <returns>
    /// A tuple with (<see cref="NextToken{T}"/> `nextToken`, <see cref="Trimmer{T}"/>? `trimmer`) necessary to make
    /// the <see cref="SpanCustomTokenizer{T}"/> work with the specified parameters.
    /// A tuple containing the <see cref="NextToken{T}"/> function `nextToken` and an optional <see cref="Trimmer{T}"/>
    /// function `trimmer`, which are required for the <see cref="SpanCustomTokenizer{T}"/> to operate with the
    /// specified parameters.
    /// </returns>
    public static (NextToken<T> nextToken, Trimmer<T>? trimmer)
        CreateTokenizerFunctions<T>(StringSplitOptions options, params T[] separator)
            where T : IEquatable<T>
    {
        return (CreateNextTokenFunc(options, separator), CreateTrimmer<T>(options));
    }

    #region NextTokenFunc

    /// <summary>
    /// Creates a <see cref="NextToken{T}"/> function for the <see cref="SpanCustomTokenizer{T}"/> to operate with the
    /// specified parameters.
    /// </summary>
    /// <typeparam name="T">The type of elements in a span.</typeparam>
    /// <param name="options">Defines the behavior for handling empty tokens and trimming tokens.</param>
    /// <param name="separator">An array of delimiter items.</param>
    /// <returns>
    /// A <see cref="NextToken{T}"/> function necessary to make the <see cref="SpanCustomTokenizer{T}"/> work with the
    /// specified parameters.
    /// </returns>
    /// <exception cref="NotImplementedException">When the required function is not yet implemented.</exception>
    public static NextToken<T> CreateNextTokenFunc<T>(StringSplitOptions options, params T[] separator)
        where T : IEquatable<T>
    {
        if (!options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
        {
            if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
            {
#if NET8_0_OR_GREATER
                SearchValues<char> searchValues = SearchValues.Create(Unsafe.As<T[], char[]>(ref separator));
                NextToken<char> func = (ReadOnlySpan<char> source) => NextTokenForwardSkipEmpty(source, searchValues);
                return Unsafe.As<NextToken<char>, NextToken<T>>(ref func);
#endif
            }

            if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
            {
#if NET8_0_OR_GREATER
                SearchValues<byte> searchValues = SearchValues.Create(Unsafe.As<T[], byte[]>(ref separator));
                NextToken<byte> func = (ReadOnlySpan<byte> source) => NextTokenForwardSkipEmpty(source, searchValues);
                return Unsafe.As<NextToken<byte>, NextToken<T>>(ref func);
#endif
            }
        }
        else
        {
            if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
            {
#if NET8_0_OR_GREATER
                SearchValues<char> searchValues = SearchValues.Create(Unsafe.As<T[], char[]>(ref separator));
                NextToken<char> func = (ReadOnlySpan<char> source) => NextTokenForwardNotSkipEmpty(source, searchValues);
                return Unsafe.As<NextToken<char>, NextToken<T>>(ref func);
#endif
            }

            if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
            {
#if NET8_0_OR_GREATER
                SearchValues<byte> searchValues = SearchValues.Create(Unsafe.As<T[], byte[]>(ref separator));
                NextToken<byte> func = (ReadOnlySpan<byte> source) => NextTokenForwardNotSkipEmpty(source, searchValues);
                return Unsafe.As<NextToken<byte>, NextToken<T>>(ref func);
#endif
            }
        }

        throw new NotImplementedException();
    }

#if NET8_0_OR_GREATER

    /// <summary>
    /// Looks for the next token in the source.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/> instance.</param>
    /// <param name="separator">A <see cref="SearchValues{T}"/> of delimiting items.</param>
    /// <returns>
    /// A tuple containing (A Range for the founded token, Range for untokenized part of source).
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ((int Start, int End) Token, (int Start, int End) Untokenized)
        NextTokenForwardSkipEmpty<T>(ReadOnlySpan<T> source, SearchValues<T> separator)
            where T : IEquatable<T>
    {
        int start = source.IndexOfAnyExcept(separator);
        if (start < 0)
        {
            return ((source.Length, source.Length), (source.Length, source.Length));
        }

        ReadOnlySpan<T> source2 = source.Slice(start + 1);
        int end = source2.IndexOfAny(separator);
        if (end < 0)
        {
            return ((start, source.Length), (source.Length, source.Length));
        }

        return ((start, start + end), (start + end + 1, source.Length));
    }

    /// <summary>
    /// Looks for the next token in the source.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
    /// <param name="source">The source <see cref="ReadOnlySpan{T}"/> instance.</param>
    /// <param name="separator">A <see cref="SearchValues{T}"/> of delimiting items.</param>
    /// <returns>
    /// A tuple containing (A Range for the founded token, Range for untokenized part of source).
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ((int Start, int End) Token, (int Start, int End) Untokenized)
        NextTokenForwardNotSkipEmpty<T>(ReadOnlySpan<T> source, SearchValues<T> separator)
            where T : IEquatable<T>
    {
        int index = source.IndexOfAny(separator);
        if (index < 0)
        {
            return ((0, source.Length), (source.Length, source.Length));
        }

        return ((0, index), (index + 1, source.Length));
    }

#endif

    #endregion

    #region Trimmer

    /// <summary>
    /// Creates a <see cref="Trimmer{T}"/> function necessary to make the <see cref="SpanCustomTokenizer{T}"/> to trim tokens.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/> source.</typeparam>
    /// <param name="options">Specifies whether to remove empty tokens and trim tokens.</param>
    /// <returns>A <see cref="Trimmer{T}"/> function necessary to make the <see cref="SpanCustomTokenizer{T}"/> to trim tokens.</returns>
    /// <exception cref="NotImplementedException">When the required function is not yet implemented.</exception>
    public static Trimmer<T>? CreateTrimmer<T>(StringSplitOptions options)
        where T : IEquatable<T?>
#if NET5_0_OR_GREATER
    {
        if (!options.HasFlag(StringSplitOptions.TrimEntries))
            return null;

        if (typeof(T) == typeof(char))
        {
            Trimmer<char> func = Trim;
            return Unsafe.As<Trimmer<char>, Trimmer<T>>(ref func);
        }

        {
            Trimmer<T> func = TrimDefault<T>;
            return func;
        }

        throw new NotImplementedException();
    }
#else
        => null;
#endif

    /// <summary>
    /// Removes all leading and trailing whitespace characters from a read-only character span.
    /// </summary>
    /// <param name="span">The readonly span to trim.</param>
    /// <returns>
    /// A tuple of two indices (int `Start`, int `End`) that represent the first inclusive and last exclusive elements
    /// of the trimmed <paramref name="span"/> part.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int Start, int End) Trim(ReadOnlySpan<char> span)
    {
        ReadOnlySpan<char> trimmed = span.Trim();
        if (trimmed.IsEmpty)
            return (span.Length, span.Length);

        int start = (int)Unsafe.ByteOffset(
                ref Unsafe.AsRef(in span.GetPinnableReference()),
                ref Unsafe.AsRef(in trimmed.GetPinnableReference()))
            / sizeof(char);

        return (start, start + trimmed.Length);
    }

    /// <summary>
    /// Removes all leading and trailing default values from a read-only span.
    /// </summary>
    /// <param name="span">The readonly span to trim.</param>
    /// <returns>
    /// A tuple of two indices (int `Start`, int `End`) that represent the first inclusive and last exclusive elements
    /// of the trimmed <paramref name="span"/> part.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (int Start, int End) TrimDefault<T>(ReadOnlySpan<T?> span)
        where T : IEquatable<T?>
    {
        int start = span.IndexOf(default(T));
        if (start < 0)
            return (span.Length, span.Length);

        int end = span.IndexOf(default(T));
        return (start, end + 1);
    }

    #endregion
}
