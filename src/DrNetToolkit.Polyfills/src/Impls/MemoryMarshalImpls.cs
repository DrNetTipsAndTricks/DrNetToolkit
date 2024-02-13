// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DrNetToolkit.Polyfills.Impls.Hidden;
using DrNetToolkit.Polyfills.Internals;

namespace DrNetToolkit.Polyfills.Impls;

#if NETSTANDARD1_1_OR_GREATER
/// <summary>
/// Implementations of <see cref="MemoryMarshal"/> methods.
/// </summary>
#else
/// <summary>
/// Implementations of MemoryMarshal methods.
/// </summary>
#endif
public static class MemoryMarshalImpls
{
    /// <summary>
    /// Returns a reference to the 0th element of <paramref name="array"/>. If the array is empty, returns a reference to where the 0th element
    /// would have been stored. Such a reference may be used for pinning but must never be dereferenced.
    /// </summary>
    /// <exception cref="NullReferenceException"><paramref name="array"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// This method does not perform array variance checks. The caller must manually perform any array variance checks
    /// if the caller wishes to write to the returned reference.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET5_0_OR_GREATER
    public static ref T GetArrayDataReference<T>(T[] array)
        => ref MemoryMarshal.GetArrayDataReference(array);
#else
    public static ref T GetArrayDataReference<T>(T[] array)
        => ref Unsafe.AsRef(in array[0]);
#endif

    /// <summary>
    /// Returns a reference to the 0th element of <paramref name="array"/>. If the array is empty, returns a reference to where the 0th element
    /// would have been stored. Such a reference may be used for pinning but must never be dereferenced.
    /// </summary>
    /// <exception cref="NullReferenceException"><paramref name="array"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// The caller must manually reinterpret the returned <em>ref byte</em> as a ref to the array's underlying elemental type,
    /// perhaps utilizing an API such as <em>System.Runtime.CompilerServices.Unsafe.As</em> to assist with the reinterpretation.
    /// This technique does not perform array variance checks. The caller must manually perform any array variance checks
    /// if the caller wishes to write to the returned reference.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
    public static ref byte GetArrayDataReference(Array array)
        => ref MemoryMarshal.GetArrayDataReference(array);
#else
    public static unsafe ref byte GetArrayDataReference(Array array)
    {
        // If needed, we can save one or two instructions per call by marking this method as intrinsic and asking the JIT
        // to special-case arrays of known type and dimension.

        // See comment on RawArrayData (in RuntimeHelpers.CoreCLR.cs) for details
        return ref Unsafe.AddByteOffset(ref Unsafe.As<RawData>(array).Data,
            (IntPtr)RuntimeHelpersImplsHidden.GetMethodTable(array)->BaseSize - (2 * sizeof(IntPtr)));
    }
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Casts a Span of one primitive type <typeparamref name="T"/> to Span of bytes.
    /// That type may not contain pointers or references. This is checked at runtime in order to preserve type safety.
    /// </summary>
    /// <param name="span">The source slice, of type <typeparamref name="T"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="T"/> contains pointers.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Thrown if the Length property of the new Span would exceed int.MaxValue.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<byte> AsBytes<T>(Span<T> span)
    {
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<T>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));

#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        return CreateSpan(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(span)),
            checked(span.Length * sizeof(T)));
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
    }
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Casts a ReadOnlySpan of one primitive type <typeparamref name="T"/> to ReadOnlySpan of bytes.
    /// That type may not contain pointers or references. This is checked at runtime in order to preserve type safety.
    /// </summary>
    /// <param name="span">The source slice, of type <typeparamref name="T"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="T"/> contains pointers.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Thrown if the Length property of the new Span would exceed int.MaxValue.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span)
    {
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<T>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));

#pragma warning disable CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
        return CreateReadOnlySpan(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(span)),
            checked(span.Length * sizeof(T)));
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type
    }
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>Creates a <see cref="Memory{T}"/> from a <see cref="ReadOnlyMemory{T}"/>.</summary>
    /// <param name="memory">The <see cref="ReadOnlyMemory{T}"/>.</param>
    /// <returns>A <see cref="Memory{T}"/> representing the same memory as the <see cref="ReadOnlyMemory{T}"/>, but writable.</returns>
    /// <remarks>
    /// <see cref="AsMemory{T}(ReadOnlyMemory{T})"/> must be used with extreme caution.  <see cref="ReadOnlyMemory{T}"/> is used
    /// to represent immutable data and other memory that is not meant to be written to; <see cref="Memory{T}"/> instances created
    /// by <see cref="AsMemory{T}(ReadOnlyMemory{T})"/> should not be written to.  The method exists to enable variables typed
    /// as <see cref="Memory{T}"/> but only used for reading to store a <see cref="ReadOnlyMemory{T}"/>.
    /// </remarks>
#if NETCOREAPP2_1_OR_GREATER
    public static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory) =>
        MemoryMarshal.AsMemory(memory);
#else
    public static Memory<T> AsMemory<T>(ReadOnlyMemory<T> memory) =>
        Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
#endif
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Casts a Span of one primitive type <typeparamref name="TFrom"/> to another primitive type <typeparamref name="TTo"/>.
    /// These types may not contain pointers or references. This is checked at runtime in order to preserve type safety.
    /// </summary>
    /// <remarks>
    /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other means.
    /// </remarks>
    /// <param name="span">The source slice, of type <typeparamref name="TFrom"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="TFrom"/> or <typeparamref name="TTo"/> contains pointers.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span)
    {
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<TFrom>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<TTo>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));

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
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Casts a ReadOnlySpan of one primitive type <typeparamref name="TFrom"/> to another primitive type <typeparamref name="TTo"/>.
    /// These types may not contain pointers or references. This is checked at runtime in order to preserve type safety.
    /// </summary>
    /// <remarks>
    /// Supported only for platforms that support misaligned memory access or when the memory block is aligned by other means.
    /// </remarks>
    /// <param name="span">The source slice, of type <typeparamref name="TFrom"/>.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <typeparamref name="TFrom"/> or <typeparamref name="TTo"/> contains pointers.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span)
        where TFrom : struct
        where TTo : struct
    {
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<TFrom>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
        if (RuntimeHelpersImpls.IsReferenceOrContainsReferences<TTo>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));

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

        return CreateReadOnlySpan(ref Unsafe.As<TFrom, TTo>(ref MemoryMarshal.GetReference(span)),
            toLength);
    }
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Creates a new span over a portion of a regular managed object. This can be useful
    /// if part of a managed object represents a "fixed array." This is dangerous because the
    /// <paramref name="length"/> is not checked.
    /// </summary>
    /// <param name="reference">A reference to data.</param>
    /// <param name="length">The number of <typeparamref name="T"/> elements the memory contains.</param>
    /// <returns>A span representing the specified reference and length.</returns>
    /// <remarks>
    /// This method should be used with caution. It is dangerous because the length argument is not checked.
    /// Even though the ref is annotated as scoped, it will be stored into the returned span, and the lifetime
    /// of the returned span will not be validated for safety, even by span-aware languages.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER
    public static Span<T> CreateSpan<T>(scoped ref T reference, int length)
        => MemoryMarshal.CreateSpan(ref reference, length);
#else
    public static unsafe Span<T> CreateSpan<T>(scoped ref T reference, int length)
        => new (Unsafe.AsPointer(ref reference), length);
#endif
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>
    /// Creates a new read-only span over a portion of a regular managed object. This can be useful
    /// if part of a managed object represents a "fixed array." This is dangerous because the
    /// <paramref name="length"/> is not checked.
    /// </summary>
    /// <param name="reference">A reference to data.</param>
    /// <param name="length">The number of <typeparamref name="T"/> elements the memory contains.</param>
    /// <returns>A read-only span representing the specified reference and length.</returns>
    /// <remarks>
    /// This method should be used with caution. It is dangerous because the length argument is not checked.
    /// Even though the ref is annotated as scoped, it will be stored into the returned span, and the lifetime
    /// of the returned span will not be validated for safety, even by span-aware languages.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER
    public static ReadOnlySpan<T> CreateReadOnlySpan<T>(scoped ref readonly T reference, int length)
        => MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in reference), length);
#else
    public static unsafe ReadOnlySpan<T> CreateReadOnlySpan<T>(scoped ref readonly T reference, int length)
        => new(Unsafe.AsPointer(ref Unsafe.AsRef(in reference)), length);
#endif
#endif

#if NETSTANDARD1_1_OR_GREATER
    /// <summary>Creates a new read-only span for a null-terminated string.</summary>
    /// <param name="value">The pointer to the null-terminated string of characters.</param>
    /// <returns>A read-only span representing the specified null-terminated string, or an empty span if the pointer is null.</returns>
    /// <remarks>The returned span does not include the null terminator.</remarks>
    /// <exception cref="ArgumentException">The string is longer than <see cref="int.MaxValue"/>.</exception>
    [CLSCompliant(false)]
#if NET6_0_OR_GREATER
    public static unsafe ReadOnlySpan<char> CreateReadOnlySpanFromNullTerminated(char* value)
        => MemoryMarshal.CreateReadOnlySpanFromNullTerminated(value);
#else
    public static unsafe ReadOnlySpan<char> CreateReadOnlySpanFromNullTerminated(char* value)
        => value != null ? new ReadOnlySpan<char>(value, StringImplsHidden.wcslen(value)) : default;
#endif
#endif

}
