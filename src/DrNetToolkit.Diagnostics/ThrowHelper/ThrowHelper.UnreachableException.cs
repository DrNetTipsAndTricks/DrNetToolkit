// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class ThrowHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowUnreachableException()
#if NET7_0_OR_GREATER
        => new UnreachableException().Throw();
#else
        => new InvalidOperationException().Throw();
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowUnreachableException(string? message)
#if NET7_0_OR_GREATER
        => new UnreachableException(message).Throw();
#else
        => new InvalidOperationException(message).Throw();
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DoesNotReturn]
    public static void ThrowUnreachableException(string? message, Exception? innerException)
#if NET7_0_OR_GREATER
        => new UnreachableException(message, innerException).Throw();
#else
        => new InvalidOperationException(message, innerException).Throw();
#endif
}
