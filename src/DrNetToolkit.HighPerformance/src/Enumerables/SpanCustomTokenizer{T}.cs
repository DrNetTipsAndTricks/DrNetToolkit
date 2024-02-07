// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using static DrNetToolkit.HighPerformance.Enumerables.SpanCustomTokenizer;

#if NETSTANDARD2_1_OR_GREATER
using Range = System.Range;
#else
using Range = (int Start, int End);
#endif

namespace DrNetToolkit.HighPerformance.Enumerables;

/// <summary>
/// <see langword="ref"/> <see langword="struct"/> that tokenizes a given <see cref="Span{T}"/> or <see cref="ReadOnlySpan{T}"/> source.
/// It should be used directly within a <see langword="foreach"/> loop.
/// It use <see cref="NextToken{T}"/> delegate to enumerate all tokens in a source.
/// It use <see cref="Trimmer{T}"/> delegate to trim tokens.
/// It satisfies to <see href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement">foreach statement pattern</see>.
/// </summary>
/// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/> source.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="SpanCustomTokenizer{T}"/> struct.
/// </remarks>
/// <param name="source">The source <see cref="ReadOnlySpan{T}"/> instance.</param>
/// <param name="nextToken">The <see cref="NextToken{T}"/> delegate to get next token in the <paramref name="source"/>.</param>
/// <param name="trimmer">The <see cref="Trimmer{T}"/>  delegate to trim current token.</param>
/// <param name="skipEmpty">The flag to skip empty tokens.</param>
public ref struct SpanCustomTokenizer<T>(ReadOnlySpan<T> source, NextToken<T> nextToken, Trimmer<T>? trimmer = null,
    bool skipEmpty = false)
{
    /// <summary>
    /// Not yet tokenized part of a source.
    /// </summary>
    public ReadOnlySpan<T> UnTokenizedPart
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable IDE0251 // Make member 'readonly'
        private set;
#pragma warning restore IDE0251 // Make member 'readonly'
    } = source;

    // Offset of the untokenized source relative to the original source
    private int _unTokenizedPartOffset = 0;

    // Range of the current token in the original source.
    private (int Start, int End) _tokenRange = default;

    // The delegate to get next token in the source.
    private readonly NextToken<T> _nextToken = nextToken;

    // The delegate to trim current token.
    private readonly Trimmer<T>? _trimmer = trimmer;

    // The flag to skip empty tokens.
    private readonly bool _skipEmpty = skipEmpty;

    /// <summary>
    /// Implements the duck-typed <see cref="IEnumerable{T}.GetEnumerator"/> method.
    /// </summary>
    /// <returns>An <see cref="SpanCustomTokenizer{T}"/> instance targeting <see cref="ReadOnlySpan{T}"/> source.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SpanCustomTokenizer<T> GetEnumerator() => this;

    /// <summary>
    /// Implements the duck-typed <see cref="System.Collections.IEnumerator.MoveNext"/> method.
    /// </summary>
    /// <returns><see langword="true"/> whether a new element is available, <see langword="false"/> otherwise</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
    SkipEmpty:
        ((int Start, int End) token, (int Start, int End) nonTokenized) = _nextToken(UnTokenizedPart);
        Debug.Assert(nonTokenized.Start >= 0 && nonTokenized.Start <= UnTokenizedPart.Length,
                $"SpanCustomTokenizer.NextToken return Untokenized.Start {nonTokenized.Start} out of range [0, {UnTokenizedPart.Length}]");
        Debug.Assert(nonTokenized.End >= nonTokenized.Start && nonTokenized.End <= UnTokenizedPart.Length,
                $"SpanCustomTokenizer.NextToken return Untokenized.End {nonTokenized.End} out of range [{nonTokenized.Start}, {UnTokenizedPart.Length}]");
        if (token.End < token.Start)
        {
            _tokenRange.Start = token.Start + _unTokenizedPartOffset;
            _tokenRange.End = token.End + _unTokenizedPartOffset;
            UnTokenizedPart = UnTokenizedPart.Slice(token.Start, token.End);
            return false;
        }
        Debug.Assert(token.Start >= 0 && token.Start <= UnTokenizedPart.Length,
                $"SpanCustomTokenizer.NextToken return Token.Start {token.Start} out of range [0, {UnTokenizedPart.Length}]");
        Debug.Assert(token.End >= token.Start && token.End <= UnTokenizedPart.Length,
                $"SpanCustomTokenizer.NextToken return Token.End {token.End} out of range [{token.Start}, {UnTokenizedPart.Length}]");

        int tokenLen = token.End - token.Start;
        if (tokenLen > 0 && _trimmer is not null)
        {
            (token.Start, token.End) = _trimmer(UnTokenizedPart.Slice(token.Start, tokenLen));
            Debug.Assert(token.Start >= 0 && token.Start <= tokenLen,
                $"SpanCustomTokenizer.TrimFunc return Start {token.Start} out of range [0, {tokenLen}].");
            Debug.Assert(token.End >= token.Start && token.End <= tokenLen,
                $"SpanCustomTokenizer.TrimFunc return End {token.End} out of range [{token.Start}, {tokenLen}].");
            tokenLen = token.End - token.Start;
        }

        if (tokenLen <= 0 && _skipEmpty)
        {
            UnTokenizedPart = UnTokenizedPart.Slice(nonTokenized.Start, nonTokenized.End - nonTokenized.Start);
            _unTokenizedPartOffset += nonTokenized.Start;
            goto SkipEmpty;
        }

        _tokenRange = (token.Start + _unTokenizedPartOffset, token.End + _unTokenizedPartOffset);
        UnTokenizedPart = UnTokenizedPart.Slice(nonTokenized.Start, nonTokenized.End - nonTokenized.Start);
        _unTokenizedPartOffset += nonTokenized.Start;
        return true;
    }

    /// <summary>
    /// Gets the duck-typed <see cref="IEnumerator{T}.Current"/> property.
    /// </summary>
    public readonly Range Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER
        get => new(_tokenRange.Start, _tokenRange.End);
#else
        get => _tokenRange;
#endif
    }
}
