// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#if NETSTANDARD2_1_OR_GREATER
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RTHelpers = DrNetToolkit.Runtime.RuntimeHelpers;
#endif 

namespace DrNetToolkit.Diagnostics;

public static partial class Guard
{
    /// <summary>
    /// Asserts that the specified type is not a reference type and not a value type that contains references.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <exception cref="ArgumentException">
    /// Thrown if the <typeparamref name="T"/> type is a reference type or a value type that contains references.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    public static void IsNotReferenceAndNotContainsReferences<T>(string name = "")
    {
        if (RTHelpers.IsReferenceOrContainsReferences<T>())
            ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T), name);
    }
}
