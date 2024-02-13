// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Polyfills.Impls.Hidden;

/// <summary>
/// Implementations of string methods.
/// </summary>
public static class StringImplsHidden
{
    /// <summary>
    /// Length of null terminated UTF-16 string.
    /// </summary>
    /// <param name="ptr">Null terminated UTF-16 string.</param>
    /// <returns>Length of null terminated UTF-16 string.</returns>
    [CLSCompliant(false)]
    public static unsafe int wcslen(char* ptr) => SpanHelpersImplsHidden.IndexOfNullCharacter(ptr);

    /// <summary>
    /// Length of null terminated UTF-8 string.
    /// </summary>
    /// <param name="ptr">Null terminated UTF-8 string.</param>
    /// <returns>Length of null terminated UTF-8 string.</returns>
    [CLSCompliant(false)]
    public static unsafe int strlen(byte* ptr) => SpanHelpersImplsHidden.IndexOfNullByte(ptr);
}
