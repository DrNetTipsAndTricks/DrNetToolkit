// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using DrNetToolkit.Polyfills.Internals;

namespace System.Runtime.CompilerServices;

/// <summary>
/// Polyfills of <see cref="RuntimeHelpers"/>.
/// </summary>
public static class RuntimeHelpersPolyfills
{
    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1_OR_GREATER
    public static bool IsReferenceOrContainsReferences<T>()
        => RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#else
    public static bool IsReferenceOrContainsReferences<T>()
        => TypeInfo.IsReferenceOrContainsReferences(typeof(T));
#endif
}
