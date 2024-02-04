// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class ThrowHelper
{
    /// <summary>
    /// Throws a new <see cref="InvalidOperationException"/>.
    /// "The {<paramref name="targetType"/>} constructor should never be used."
    /// </summary>
    /// <param name="targetType">The target type whose constructor should never be used.</param>
    /// <exception cref="InvalidOperationException">
    /// Always thrown new created with next message:
    /// "The {<paramref name="targetType"/>} constructor should never be used."
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowThisConstructorShouldNeverBeUsed(Type targetType)
        => new InvalidOperationException($"The {targetType} constructor should never be used.")
        .Throw();
}
