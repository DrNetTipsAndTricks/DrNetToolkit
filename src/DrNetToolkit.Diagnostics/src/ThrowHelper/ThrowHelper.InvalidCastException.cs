// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class ThrowHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCastException()
        => new InvalidCastException().Throw();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(string? message)
        => new InvalidCastException(message).Throw();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(string? message, Exception? innerException)
        => new InvalidCastException(message, innerException).Throw();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(Type sourceType, Type targetType)
        => ThrowInvalidCastException(
            $"Can't cast the instance of type '{sourceType}' to an instance of the type `{targetType}`");
}
