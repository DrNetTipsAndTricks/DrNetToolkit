// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class ThrowHelper
{
    /// <summary>
    /// Throws a new <see cref="InvalidCastException"/>.
    /// </summary>
    /// <exception cref="InvalidCastException">Always thrown new created with no parameters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowInvalidCastException()
        => new InvalidCastException().Throw();

    /// <summary>
    /// Throws a new <see cref="InvalidCastException"/>.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="InvalidCastException">Always thrown new created with the specified parameter.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(string? message)
        => new InvalidCastException(message).Throw();

    /// <summary>
    /// Throws a new <see cref="InvalidCastException"/>.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> is not null,
    /// the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    /// <exception cref="InvalidCastException">Always thrown new created with the specified parameters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(string? message, Exception? innerException)
        => new InvalidCastException(message, innerException).Throw();

    /// <summary>
    /// Throws a new <see cref="InvalidCastException"/>.
    /// "Can't cast the instance of type '{<paramref name="sourceType"/>}' to an instance of the type
    /// `{<paramref name="targetType"/>}`.".
    /// </summary>
    /// <param name="sourceType">The type of a source instance.</param>
    /// <param name="targetType">The target type to cast from a source instance.</param>
    /// <exception cref="InvalidCastException">
    /// Always thrown new created with next message:
    /// "Can't cast the instance of type '{<paramref name="sourceType"/>}' to an instance of the type
    /// `{<paramref name="targetType"/>}`.".
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowInvalidCastException(Type sourceType, Type targetType)
        => ThrowInvalidCastException(
            $"Can't cast the instance of type '{sourceType}' to an instance of the type `{targetType}`.");
}
