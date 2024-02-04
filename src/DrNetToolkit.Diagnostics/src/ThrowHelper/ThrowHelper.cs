// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
public static partial class ThrowHelper
{
    /// <summary>
    /// Throws the specified <paramref name="exception"/>. Method will never return under any circumstance.
    /// </summary>
    /// <param name="exception">The exception that will be thrown.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void Throw(this Exception exception)
        => throw exception;

    /// <summary>
    /// Throws the specified <paramref name="exception"/>. Method will not return if the <paramref name="flag"/> is
    /// <see langword="true"/>.
    /// </summary>
    /// <param name="exception">
    /// The exception that will be thrown if the <paramref name="flag"/> is <see langword="true"/>.
    /// </param>
    /// <param name="flag">The flag indicates when the method will throw the <paramref name="exception"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    public static void ThrowIfTrue(this Exception exception, [DoesNotReturnIf(true)] bool flag)
    {
        if (flag) Throw(exception);
    }

    /// <summary>
    /// Throws the specified <paramref name="exception"/>. Method will not return if the <paramref name="flag"/> is
    /// <see langword="false"/>.
    /// </summary>
    /// <param name="exception">
    /// The exception that will be thrown if the <paramref name="flag"/> is <see langword="false"/>.
    /// </param>
    /// <param name="flag">The flag indicates when the method will throw the <paramref name="exception"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    public static void ThrowIfFalse(this Exception exception, [DoesNotReturnIf(false)] bool flag)
    {
        if (!flag) Throw(exception);
    }
}
