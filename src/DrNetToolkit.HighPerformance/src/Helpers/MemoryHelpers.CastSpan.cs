// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using DrNetToolkit.Diagnostics;

namespace DrNetToolkit.Runtime;

public static partial class MemoryHelpers
{
    /// <summary>
    /// Casts a Span of one primitive type <typeparamref name="TFrom"/> to another primitive type
    /// <typeparamref name="TTo"/>. These types may not contain pointers or references. This is checked at runtime in
    /// order to preserve type safety.
    /// </summary>
    /// <remarks>
    /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other
    /// means.
    /// </remarks>
    /// <param name="span">The source slice, of type <typeparamref name="TFrom"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="TFrom"/> or <typeparamref name="TTo"/> contains pointers.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span)
    {
        Guard.IsNotReferenceAndNotContainsReferences<TFrom>(nameof(span));
        Guard.IsNotReferenceAndNotContainsReferences<TTo>(nameof(TTo));

        // Use unsigned integers - unsigned division by constant (especially by power of 2)
        // and checked casts are faster and smaller.
        uint fromSize = (uint)Unsafe.SizeOf<TFrom>();
        uint toSize = (uint)Unsafe.SizeOf<TTo>();
        uint fromLength = (uint)span.Length;
        int toLength;
        if (fromSize == toSize)
        {
            // Special case for same size types - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
            // should be optimized to just `length` but the JIT doesn't do that today.
            toLength = (int)fromLength;
        }
        else if (fromSize == 1)
        {
            // Special case for byte sized TFrom - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
            // becomes `(ulong)fromLength / (ulong)toSize` but the JIT can't narrow it down to `int`
            // and can't eliminate the checked cast. This also avoids a 32 bit specific issue,
            // the JIT can't eliminate long multiply by 1.
            toLength = (int)(fromLength / toSize);
        }
        else
        {
            // Ensure that casts are done in such a way that the JIT is able to "see"
            // the uint->ulong casts and the multiply together so that on 32 bit targets
            // 32x32to64 multiplication is used.
            ulong toLengthUInt64 = (ulong)fromLength * (ulong)fromSize / (ulong)toSize;
            toLength = checked((int)toLengthUInt64);
        }

        return CreateSpan(ref Unsafe.As<TFrom, TTo>(ref MemoryMarshal.GetReference(span)), toLength);
    }

    /// <summary>
    /// Casts a ReadOnlySpan of one primitive type <typeparamref name="TFrom"/>? to another primitive type
    /// <typeparamref name="TTo"/>. These types may not contain pointers or references. This is checked at runtime in
    /// order to preserve type safety.
    /// </summary>
    /// <remarks>
    /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other
    /// means.
    /// </remarks>
    /// <param name="span">The source slice, of type <typeparamref name="TFrom"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="TFrom"/> or <typeparamref name="TTo"/> contains pointers.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span)
    {
        Guard.IsNotReferenceAndNotContainsReferences<TFrom>(nameof(span));
        Guard.IsNotReferenceAndNotContainsReferences<TTo>(nameof(TTo));

        // Use unsigned integers - unsigned division by constant (especially by power of 2)
        // and checked casts are faster and smaller.
        uint fromSize = (uint)Unsafe.SizeOf<TFrom>();
        uint toSize = (uint)Unsafe.SizeOf<TTo>();
        uint fromLength = (uint)span.Length;
        int toLength;
        if (fromSize == toSize)
        {
            // Special case for same size types - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
            // should be optimized to just `length` but the JIT doesn't do that today.
            toLength = (int)fromLength;
        }
        else if (fromSize == 1)
        {
            // Special case for byte sized TFrom - `(ulong)fromLength * (ulong)fromSize / (ulong)toSize`
            // becomes `(ulong)fromLength / (ulong)toSize` but the JIT can't narrow it down to `int`
            // and can't eliminate the checked cast. This also avoids a 32 bit specific issue,
            // the JIT can't eliminate long multiply by 1.
            toLength = (int)(fromLength / toSize);
        }
        else
        {
            // Ensure that casts are done in such a way that the JIT is able to "see"
            // the uint->ulong casts and the multiply together so that on 32 bit targets
            // 32x32to64 multiplication is used.
            ulong toLengthUInt64 = (ulong)fromLength * (ulong)fromSize / (ulong)toSize;
            toLength = checked((int)toLengthUInt64);
        }

        return CreateReadOnlySpan(ref Unsafe.As<TFrom, TTo>(ref MemoryMarshal.GetReference(span)), toLength);
    }
}
