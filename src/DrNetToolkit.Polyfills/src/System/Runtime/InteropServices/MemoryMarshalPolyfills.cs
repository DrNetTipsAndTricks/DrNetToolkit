// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;
using DrNetToolkit.Polyfills.Internals;

namespace System.Runtime.InteropServices;

#if NETSTANDARD1_1_OR_GREATER
/// <summary>
/// <see cref="MemoryMarshal"/> polyfills.
/// </summary>
#else
/// <summary>
/// MemoryMarshal polyfills.
/// </summary>
#endif
public static class MemoryMarshalPolyfills
{
#if !NET5_0_OR_GREATER
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
    public static ref T GetArrayDataReference<T>(T[] array)
        => ref MemoryMarshalImpls.GetArrayDataReference(array);
#endif

#if !NET6_0_OR_GREATER
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
    public static unsafe ref byte GetArrayDataReference(Array array)
        => ref MemoryMarshalImpls.GetArrayDataReference(array);
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
        => MemoryMarshalImpls.AsBytes(span);
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
        => MemoryMarshalImpls.AsBytes(span);
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
        => MemoryMarshalImpls.Cast<TFrom, TTo>(span);
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
        => MemoryMarshalImpls.Cast<TFrom, TTo>(span);
#endif

#if NETSTANDARD1_1_OR_GREATER
#if !NETSTANDARD2_1_OR_GREATER
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
    public static unsafe Span<T> CreateSpan<T>(scoped ref T reference, int length)
        => MemoryMarshalImpls.CreateSpan(ref reference, length);
#endif
#endif

#if NETSTANDARD1_1_OR_GREATER
#if !NETSTANDARD2_1_OR_GREATER
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
    public static ReadOnlySpan<T> CreateReadOnlySpan<T>(scoped ref readonly T reference, int length)
        => MemoryMarshalImpls.CreateReadOnlySpan(in reference, length);
#endif
#endif

#if NETSTANDARD1_1_OR_GREATER
#if !NET6_0_OR_GREATER
    /// <summary>Creates a new read-only span for a null-terminated string.</summary>
    /// <param name="value">The pointer to the null-terminated string of characters.</param>
    /// <returns>A read-only span representing the specified null-terminated string, or an empty span if the pointer is null.</returns>
    /// <remarks>The returned span does not include the null terminator.</remarks>
    /// <exception cref="ArgumentException">The string is longer than <see cref="int.MaxValue"/>.</exception>
    [CLSCompliant(false)]
    public static unsafe ReadOnlySpan<char> CreateReadOnlySpanFromNullTerminated(char* value)
        => MemoryMarshalImpls.CreateReadOnlySpanFromNullTerminated(value);
#endif
#endif
}
