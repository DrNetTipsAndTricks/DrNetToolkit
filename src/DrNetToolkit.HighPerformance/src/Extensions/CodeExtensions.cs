// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

/// <summary>
/// Provides methods that allow you to writing of beautiful, compact code and bypass some limitations of the C#
/// language.
/// </summary>
public static partial class CodeExtensions
{

#pragma warning disable IDE0060 // Remove unused parameter

    /// <summary>
    /// A discard method that allows you to ignore any value. It is fluent style method which is similar to using
    /// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards#a-standalone-discard">
    /// a standalone discard</see>.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="value"/> to discard.</typeparam>
    /// <param name="value">The value to discard.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Discard<T>(this T value)
    { }

#pragma warning restore IDE0060 // Remove unused parameter

}
