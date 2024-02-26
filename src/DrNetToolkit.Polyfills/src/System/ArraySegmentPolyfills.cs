// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace System;

/// <summary>
/// <see cref="ArraySegment{T}"/> polyfills.
/// </summary>
/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
public static class ArraySegmentPolyfills<T>
{
    /// <summary>
    /// Represents the empty array segment.This field is read-only.
    /// </summary>
#pragma warning disable IDE0300 // Simplify collection initialization
#if NETSTANDARD2_0_OR_GREATER
#pragma warning disable CA1825 // Avoid zero-length array allocations
#endif
    public static ArraySegment<T> Empty { get; } = new(new T[0]);
#if NETSTANDARD2_0_OR_GREATER
#pragma warning restore CA1825 // Avoid zero-length array allocations
#endif
#pragma warning restore IDE0300 // Simplify collection initialization
}
