// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;

#if NETSTANDARD2_1_OR_GREATER
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace DrNetToolkit.Runtime;

/// <summary>
/// Retrieves and caches type information.
/// </summary>
/// <typeparam name="T">The type.</typeparam>
public static class TypeInfo<T>
{
    /// <summary>
    /// Returns a value that indicates whether the specified type is a reference type or a value type that contains
    /// references.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
#if NETSTANDARD2_1_OR_GREATER
    public static bool IsReferenceOrContainsReferences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => RTHelpers.IsReferenceOrContainsReferences<T>();
    }
#else
    public static bool IsReferenceOrContainsReferences
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = RuntimeHelpers.IsReferenceOrContainsReferences(typeof(T));
#endif
}
