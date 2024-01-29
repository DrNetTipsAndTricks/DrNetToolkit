// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance;

[StackTraceHidden]
public static partial class ThrowHelper
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
        => throw new ArgumentException(
            $"Cannot use type '{targetType}'. Only value types without pointers or references are supported.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowArgumentNullException(string parameterName)
        => throw new ArgumentNullException(parameterName);

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(Type sourceType, Type targetType)
        => throw new InvalidCastException(
            $"Can't cast the instance of type '{sourceType}' to an instance of the type `{targetType}`");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowThisConstructorShouldNeverBeUsed(Type targetType)
        => throw new InvalidOperationException(
            $"The {targetType} constructor should never be used.");

    [MethodImpl(MethodImplOptions.NoInlining)]
    [DoesNotReturn]
    public static void ThrowUnreachableException()
#if NET7_0_OR_GREATER
        => throw new UnreachableException();
#else
        => throw new InvalidOperationException();
#endif
}
