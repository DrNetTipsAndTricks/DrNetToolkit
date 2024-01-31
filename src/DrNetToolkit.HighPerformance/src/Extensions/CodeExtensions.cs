// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{

#pragma warning disable IDE0060 // Remove unused parameter

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Discard<T>(this T value)
    { }

#pragma warning restore IDE0060 // Remove unused parameter

}
