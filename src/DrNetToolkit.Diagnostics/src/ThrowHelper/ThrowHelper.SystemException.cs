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
    /// Throws a new <see cref="SystemException"/>.
    /// </summary>
    /// <exception cref="SystemException">Always thrown new created with no parameters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowSystemException()
        => new SystemException().Throw();

    /// <summary>
    /// Throws a new <see cref="SystemException"/>.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="SystemException">Always thrown new created with the specified parameter.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowSystemException(string? message)
        => new SystemException(message).Throw();

    /// <summary>
    /// Throws a new <see cref="SystemException"/>.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> is not null,
    /// the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    /// <exception cref="SystemException">Always thrown new created with the specified parameters.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void ThrowSystemException(string? message, Exception? innerException)
        => new SystemException(message, innerException).Throw();
}
