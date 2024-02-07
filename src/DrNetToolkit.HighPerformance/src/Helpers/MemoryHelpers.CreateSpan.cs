// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;

#if NETSTANDARD2_1_OR_GREATER
using System.Runtime.InteropServices;
#else
using System.Runtime.CompilerServices;
#endif

namespace DrNetToolkit.Runtime;

public static partial class MemoryHelpers
{
    /// <summary>
    /// Creates a new span over a portion of a regular managed object.
    /// </summary>
    /// <typeparam name="T">The type of the data items.</typeparam>
    /// <param name="reference">A reference to data.</param>
    /// <param name="length">The number of T elements that reference contains.</param>
    /// <returns>A span.</returns>
    public static unsafe Span<T> CreateSpan<T>(ref T reference, int length)
#if NETSTANDARD2_1_OR_GREATER
        => MemoryMarshal.CreateSpan(ref reference, length);
#else
        => new Span<T>(Unsafe.AsPointer(ref reference), length);
#endif

    /// <summary>
    /// Creates a new readonly span over a portion of a regular managed object.
    /// </summary>
    /// <typeparam name="T">The type of the data items.</typeparam>
    /// <param name="reference">A reference to data.</param>
    /// <param name="length">The number of T elements that reference contains.</param>
    /// <returns>A readonly span.</returns>
    public static unsafe ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
#if NETSTANDARD2_1_OR_GREATER
        => MemoryMarshal.CreateReadOnlySpan(ref reference, length);
#else
        => new ReadOnlySpan<T>(Unsafe.AsPointer(ref reference), length);
#endif
}
