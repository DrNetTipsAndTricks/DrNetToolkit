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
    /// <exception>Always thrown the specified <paramref name="exception"/>.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static void Throw(this Exception exception)
        => throw exception;

    /// <summary>
    /// Throws the specified <paramref name="exception"/>. Function will never return under any circumstance.
    /// </summary>
    /// <typeparam name="T">The type of the expected value to be returned.</typeparam>
    /// <param name="exception">The exception that will be thrown.</param>
    /// <exception>Always thrown the specified <paramref name="exception"/>.</exception>
    /// <remarks>
    /// This function can be used where the compiler expects a value of type <typeparamref name="T"/> and the
    /// <seealso cref="Throw(Exception)"/> method cannot be used due to a code compilation error.
    /// </remarks>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static T Throw<T>(this Exception exception)
        => throw exception;
}
