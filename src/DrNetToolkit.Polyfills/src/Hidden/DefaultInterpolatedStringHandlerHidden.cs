// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System;

namespace DrNetToolkit.Polyfills.Hidden;

#if NET6_0_OR_GREATER
/// <summary>
/// <see cref="DefaultInterpolatedStringHandler"/> hidden methods.
/// </summary>
#else
/// <summary>
/// DefaultInterpolatedStringHandler hidden methods.
/// </summary>
#endif
public static class DefaultInterpolatedStringHandlerHidden
{
    /// <summary>Gets whether the provider provides a custom formatter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)] // only used in a few hot path call sites
    public static bool HasCustomFormatter(IFormatProvider provider)
    {
        Debug.Assert(provider is not null);
        Debug.Assert(provider is not CultureInfo || provider.GetFormat(typeof(ICustomFormatter)) is null, "Expected CultureInfo to not provide a custom formatter");
        return
            provider!.GetType() != typeof(CultureInfo) && // optimization to avoid GetFormat in the majority case
            provider.GetFormat(typeof(ICustomFormatter)) != null;
    }
}
