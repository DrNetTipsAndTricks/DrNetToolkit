// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;

namespace System;

/// <summary>
/// Polyfills of <see cref="RuntimeHelpers"/>.
/// </summary>
public static class RuntimeHelpersPolyfills
{
#if !NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsReferenceOrContainsReferences<T>()
        => RuntimeHelpersImpls.IsReferenceOrContainsReferences<T>();
#endif
}
