// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace System;

/// <summary>
/// <see cref="char"/> polyfills.
/// </summary>
public static class CharPolyfills
{
    /// <summary>Indicates whether a character is categorized as an ASCII digit.</summary>
    /// <param name="c">The character to evaluate.</param>
    /// <returns>true if <paramref name="c"/> is an ASCII digit; otherwise, false.</returns>
    /// <remarks>
    /// This determines whether the character is in the range '0' through '9', inclusive.
    /// </remarks>
    ///
#if NET7_0_OR_GREATER
    public static bool IsAsciiDigit(char c) => char.IsAsciiDigit(c);
#else
    public static bool IsAsciiDigit(char c) => CharPolyfills.IsBetween(c, '0', '9');
#endif

    /// <summary>Indicates whether a character is within the specified inclusive range.</summary>
    /// <param name="c">The character to evaluate.</param>
    /// <param name="minInclusive">The lower bound, inclusive.</param>
    /// <param name="maxInclusive">The upper bound, inclusive.</param>
    /// <returns>true if <paramref name="c"/> is within the specified range; otherwise, false.</returns>
    /// <remarks>
    /// The method does not validate that <paramref name="maxInclusive"/> is greater than or equal
    /// to <paramref name="minInclusive"/>.  If <paramref name="maxInclusive"/> is less than
    /// <paramref name="minInclusive"/>, the behavior is undefined.
    /// </remarks>
#if NET7_0_OR_GREATER
    public static bool IsBetween(char c, char minInclusive, char maxInclusive)
    => char.IsBetween(c, minInclusive, maxInclusive);
#else
    public static bool IsBetween(char c, char minInclusive, char maxInclusive) =>
        (uint)(c - minInclusive) <= (uint)(maxInclusive - minInclusive);
#endif

}
