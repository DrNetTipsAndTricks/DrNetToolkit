// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace System;

/// <summary>
/// ArraySegment polyfills.
/// </summary>
/// <typeparam name="T">The type of the elements in the array segment.</typeparam>
public static class ArraySegmentPolyfills<T>
{
#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Represents the empty array segment.This field is read-only.
    /// </summary>
#pragma warning disable IDE0300 // Simplify collection initialization
    public static ArraySegment<T> Empty { get; } = new(new T[0]);
#pragma warning restore IDE0300 // Simplify collection initialization
#endif
}
