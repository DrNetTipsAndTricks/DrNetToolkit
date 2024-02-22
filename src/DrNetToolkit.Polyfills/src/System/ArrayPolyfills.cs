// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace System;

/// <summary>
/// <see cref="Array"/> polyfills.
/// </summary>
public static class ArrayPolyfills
{
    /// <summary>
    ///  This is the threshold where Introspective sort switches to Insertion sort.
    ///  Empirically, 16 seems to speed up most cases without slowing down others, at least for integers.
    ///  Large value types may benefit from a smaller number.
    /// </summary>
    internal const int IntrosortSizeThreshold = 16;

}
