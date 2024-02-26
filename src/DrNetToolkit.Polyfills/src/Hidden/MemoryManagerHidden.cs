// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// Implementations of <see cref="MemoryManager{T}"/> hidden methods.
/// </summary>
///
public static class MemoryManagerHidden
{
    /// <summary>
    /// Try to get array segment.
    /// </summary>
    /// <typeparam name="T">The type of items in the memory buffer managed by this memory manager.</typeparam>
    /// <param name="memoryManager">The memory manager.</param>
    /// <param name="segment">Array segment.</param>
    /// <returns>true memory manager return array segment</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetArray<T>(MemoryManager<T> memoryManager, out ArraySegment<T> segment)
        => RawMemoryManager<T>.TryGetArray(memoryManager, out segment);
}

/// <summary>
/// Manager of <see cref="Memory{T}"/> that provides the implementation.
/// </summary>
/// <typeparam name="T">The type of items in the memory buffer managed by this memory manager.</typeparam>
public abstract class RawMemoryManager<T> : MemoryManager<T>
{
    /// <summary>
    /// Try to get array segment.
    /// </summary>
    /// <param name="memoryManager">The memory manager.</param>
    /// <param name="segment">Array segment.</param>
    /// <returns>true memory manager return array segment</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetArray(MemoryManager<T> memoryManager, out ArraySegment<T> segment)
        => Unsafe.As<MemoryManager<T>, RawMemoryManager<T>>(ref memoryManager).TryGetArray(out segment);
}
#endif
