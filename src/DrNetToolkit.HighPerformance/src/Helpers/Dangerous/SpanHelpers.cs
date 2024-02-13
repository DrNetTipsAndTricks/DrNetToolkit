// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Dangerous;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Provides low level methods for <see cref="Span{T}"/>, <see cref="ReadOnlySpan{T}"/>.
/// </summary>
public static partial class DangerousSpanHelpers
{
    public static int IndexOfAnyExcept<T>(ref T searchSpace, T value0, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        EqualityComparer<T> comparer  = EqualityComparer<T>.Default;
        for (int i = 0; i < length; i++)
        {
            if (!comparer.Equals(Unsafe.Add(ref searchSpace, i), value0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExcept<T>(ref T searchSpace, T value0, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        for (int i = length - 1; i >= 0; i--)
        {
            if (!comparer.Equals(Unsafe.Add(ref searchSpace, i), value0))
            {
                return i;
            }
        }

        return -1;
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
