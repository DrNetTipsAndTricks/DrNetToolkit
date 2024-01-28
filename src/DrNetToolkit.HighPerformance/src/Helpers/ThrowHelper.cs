// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DrNetToolkit.HighPerformance.ThrowHelpers;

[StackTraceHidden]
public static partial class ThrowHelper
{
    [DoesNotReturn]
    public static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
        => throw new ArgumentException(
            $"Cannot use type '{targetType}'. Only value types without pointers or references are supported.");

    public static void ThrowArgumentNullException(string parameterName)
        => throw new ArgumentNullException(parameterName);

    [DoesNotReturn]
    public static void ThrowInvalidCastException(Type sourceType, Type targetType)
        => throw new InvalidCastException(
            $"Can't cast the instance of type '{sourceType}' to an instance of the type `{targetType}`");

    [DoesNotReturn]
    public static void ThrowThisConstructorShouldNeverBeUsed(Type targetType)
        => throw new InvalidOperationException(
            $"The {targetType} constructor should never be used.");

}
