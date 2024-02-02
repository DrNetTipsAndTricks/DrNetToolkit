// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

#pragma warning disable CS8763 // A method marked [DoesNotReturn] should not return.

public static partial class ThrowHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
        => new ArgumentException(
            $"Cannot use type '{targetType}'. Only value types without pointers or references are supported.")
        .Throw();
}
