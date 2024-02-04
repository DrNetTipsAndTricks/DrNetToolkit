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
    /// Throws a new <see cref="ArgumentException"/>.
    /// "Cannot use type '{<paramref name="targetType"/>}'. Only value types without pointers or references are
    /// supported."
    /// </summary>
    /// <param name="targetType">The target type with pointers or references.</param>
    /// <exception cref="ArgumentException">
    /// Always thrown new created with next message:
    /// "Cannot use type '{<paramref name="targetType"/>}'. Only value types without pointers or references are
    /// supported."
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
        => new ArgumentException(
            $"Cannot use type '{targetType}'. Only value types without pointers or references are supported.")
        .Throw();
}
