// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class Guard
{
    /// <summary>
    /// Asserts that the <paramref name="sourceType"/> is <typeparamref name="TTarget"/> type.
    /// </summary>
    /// <typeparam name="TTarget">The target type.</typeparam>
    /// <param name="sourceType">The source type.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the <paramref name="sourceType"/> is not <typeparamref name="TTarget"/> type.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    public static void CastToTheSameType<TTarget>(Type sourceType)
    {
        if (sourceType != typeof(TTarget))
            ThrowHelper.ThrowInvalidCastException(sourceType, typeof(TTarget));
    }
}
