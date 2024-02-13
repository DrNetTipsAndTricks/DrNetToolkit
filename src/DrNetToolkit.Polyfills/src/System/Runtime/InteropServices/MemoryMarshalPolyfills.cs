// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;

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
}
