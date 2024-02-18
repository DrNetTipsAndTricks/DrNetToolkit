// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using System;
using System.Runtime.InteropServices;

namespace DrNetToolkit.Polyfills.Hidden;

#if NETSTANDARD1_1_OR_GREATER
/// <summary>
/// Implementations of <see cref="MemoryMarshal"/> hidden methods.
/// </summary>
#else
/// <summary>
/// Implementations of MemoryMarshal methods.
/// </summary>
#endif
public static class MemoryMarshalHidden
{
#if NETSTANDARD1_1_OR_GREATER

    /// <summary>
    /// Returns a reference to the 0th element of the Span. If the Span is empty, returns a reference to fake non-null pointer. Such a reference can be used
    /// for pinning but must never be dereferenced. This is useful for interop with methods that do not accept null pointers for zero-sized buffers.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref T GetNonNullPinnableReference<T>(Span<T> span)
        => ref (span.Length != 0) ? ref Unsafe.AsRef(in MemoryMarshal.GetReference(span)) : ref Unsafe.AsRef<T>((void*)1);
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Returns a reference to the 0th element of the ReadOnlySpan. If the ReadOnlySpan is empty, returns a reference to fake non-null pointer. Such a reference
    /// can be used for pinning but must never be dereferenced. This is useful for interop with methods that do not accept null pointers for zero-sized buffers.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
        => ref (span.Length != 0) ? ref Unsafe.AsRef(in MemoryMarshal.GetReference(span)) : ref Unsafe.AsRef<T>((void*)1);
#endif
}
