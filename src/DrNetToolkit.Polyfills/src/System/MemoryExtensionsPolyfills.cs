// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;

namespace System;

/// <summary>
/// Extension methods for <see cref="Span{T}"/>, <see cref="Memory{T}"/>, and friends.
/// </summary>
public static partial class MemoryExtensionsPolyfills
{
#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Creates a new span over the portion of the target array.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<T> AsSpan<T>(this T[]? array, Index startIndex)
        => MemoryExtensionsImpls.AsSpan(array, startIndex);
#endif
}
#endif
