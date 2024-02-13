// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls.Hidden;

namespace DrNetToolkit.Polyfills.Internals;

/// <summary>
/// Retrieves and caches type information.
/// </summary>
/// <typeparam name="T">The type.</typeparam>
internal static class TypeInfo<T>
{
    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool IsReferenceOrContainsReferences
    {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RuntimeHelpers.IsReferenceOrContainsReferences<T>();
    }
#else
    public static bool IsReferenceOrContainsReferences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = RuntimeHelpersImplsHidden.IsReferenceOrContainsReferences(typeof(T));
#endif

    /// <summary>
    /// Indicates whether the specified given type is bitwise equatable (memcmp can be used for equality checking).
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if given type is bitwise equatable (memcmp can be used for equality checking).
    /// </returns>
    /// <remarks>
    /// Only use the result of this for Equals() comparison, not for CompareTo() comparison.
    /// </remarks>
    public static bool IsBitwiseEquatable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = RuntimeHelpersImplsHidden.IsBitwiseEquatable(typeof(T));
}
