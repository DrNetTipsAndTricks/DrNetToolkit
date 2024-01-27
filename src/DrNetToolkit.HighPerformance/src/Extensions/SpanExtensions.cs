// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace DrNetToolkit.HighPerformance;

#if NETSTANDARD2_1_OR_GREATER

public static partial class SpanExtensions
{
    public static Span<T> DangerousAsSpan<T>(this ReadOnlySpan<T> span)
        => MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(span), span.Length);
}

#endif
