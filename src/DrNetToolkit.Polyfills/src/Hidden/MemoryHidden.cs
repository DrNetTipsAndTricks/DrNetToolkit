// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace DrNetToolkit.Polyfills.Hidden;

///// <summary>
///// Implementations of <see cref="Memory{T}"/> and <see cref="ReadOnlyMemory{T}"/> methods.
///// </summary>
//public static class MemoryHidden
//{
//    /// <summary>
//    /// The highest order bit of _index is used to discern whether _object is a pre-pinned array.
//    /// (_index &lt; 0) => _object is a pre-pinned array, so Pin() will not allocate a new GCHandle
//    ///       (else) => Pin() needs to allocate a new GCHandle to pin the object.
//    /// </summary>
//    public const int RemoveFlagsBitMask = 0x7FFFFFFF;

//    /// <summary>Gets the state of the memory as individual fields.</summary>
//    /// <param name="memory">The memory.</param>
//    /// <param name="start">The offset.</param>
//    /// <param name="length">The count.</param>
//    /// <returns>The object.</returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static object? GetObjectStartLength<T>(Memory<T> memory, out int start, out int length)
//    {
//        ref RawMemory<T> rawMemory = ref Unsafe.As<Memory<T>, RawMemory<T>>(ref memory);
//        start = rawMemory._index;
//        length = rawMemory._length;
//        return rawMemory._object;
//    }

//    /// <summary>Gets the state of the memory as individual fields.</summary>
//    /// <param name="memory">The memory.</param>
//    /// <param name="start">The offset.</param>
//    /// <param name="length">The count.</param>
//    /// <returns>The object.</returns>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static object? GetObjectStartLength<T>(ReadOnlyMemory<T> memory, out int start, out int length)
//    {
//        ref RawMemory<T> rawMemory = ref Unsafe.As<ReadOnlyMemory<T>, RawMemory<T>>(ref memory);
//        start = rawMemory._index;
//        length = rawMemory._length;
//        return rawMemory._object;
//    }
//}

///// <summary>
///// Represents <see cref="Memory{T}"/>.
///// </summary>
//[CLSCompliant(false)]
//[NonVersionable] // This only applies to field layout
//public readonly struct RawMemory<T>
//{
//    // NOTE: With the current implementation, Memory<T> and ReadOnlyMemory<T> must have the same layout,
//    // as code uses Unsafe.As to cast between them.

//    // The highest order bit of _index is used to discern whether _object is a pre-pinned array.
//    // (_index < 0) => _object is a pre-pinned array, so Pin() will not allocate a new GCHandle
//    //       (else) => Pin() needs to allocate a new GCHandle to pin the object.
//#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
//    public readonly object? _object;
//    public readonly int _index;
//    public readonly int _length;
//#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

//    /// <summary>Creates a new memory over the existing object, start, and length. No validation is performed.</summary>
//    /// <param name="obj">The target object.</param>
//    /// <param name="start">The index at which to begin the memory.</param>
//    /// <param name="length">The number of items in the memory.</param>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public RawMemory(object? obj, int start, int length)
//    {
//        // No validation performed in release builds; caller must provide any necessary validation.

//        // 'obj is T[]' below also handles things like int[] <-> uint[] being convertible
//        Debug.Assert((obj == null)
//            || (typeof(T) == typeof(char) && obj is string)
//            || (obj is T[])
//            || (obj is MemoryManager<T>));

//        _object = obj;
//        _index = start;
//        _length = length;
//    }
//}
#endif
