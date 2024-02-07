// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

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
    public static readonly bool IsReferenceOrContainsReferences =
#if NETSTANDARD2_1_OR_GREATER
        RTHelpers.IsReferenceOrContainsReferences<T>();
#else
        RuntimeHelpers.IsReferenceOrContainsReferences(typeof(T));
#endif
}
