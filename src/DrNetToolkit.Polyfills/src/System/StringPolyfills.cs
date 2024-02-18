// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;

namespace System;

/// <summary>
/// <see cref="string"/> polyfills.
/// </summary>
public static class StringPolyfills
{
    /// <summary>
    /// Returns a reference to the first element of the String. If the string is <see langword="null"/>, an access will
    /// throw a <see cref="NullReferenceException"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
    public static unsafe ref readonly char GetPinnableReference(string text)
        => ref text.GetPinnableReference();
#else
    public static unsafe ref readonly char GetPinnableReference(this string text)
    {
        fixed (char* p = text)
            return ref Unsafe.AsRef<char>(p);
    }
#endif
}
