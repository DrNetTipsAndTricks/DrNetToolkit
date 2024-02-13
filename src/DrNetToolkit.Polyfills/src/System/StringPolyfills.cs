// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Impls;

namespace System;

/// <summary>
/// String polyfills.
/// </summary>
public static class StringPolyfills
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// Returns a reference to the first element of the String. If the string is null, an access will throw a NullReferenceException.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref readonly char GetPinnableReference(this string text)
        => ref StringImpls.GetPinnableReference(text);
#endif

}
