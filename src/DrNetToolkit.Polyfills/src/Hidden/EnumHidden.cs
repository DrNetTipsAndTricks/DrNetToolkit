// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.


using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// <see cref="Enum"/> hidden methods.
/// </summary>
public static class EnumHidden
{
#if NETSTANDARD1_1_OR_GREATER

    private delegate bool TryFormat<TEnum>(TEnum value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default);

#if NET8_0_OR_GREATER
    /// <summary>Tries to format the value of the enumerated type instance into the provided span of characters.</summary>
    /// <remarks>
    /// This is same as the implementation for <see cref="Enum.TryFormat"/>. It is separated out as <see cref="Enum.TryFormat"/> has constrains on the TEnum,
    /// and we internally want to use this method in cases where we dynamically validate a generic T is an enum rather than T implementing
    /// those constraints. It's a manual copy/paste right now to avoid pressure on the JIT's inlining mechanisms.
    /// </remarks>
#else
    /// <summary>Tries to format the value of the enumerated type instance into the provided span of characters.</summary>
    /// <remarks>
    /// This is same as the implementation for Enum.TryFormat. It is separated out as Enum.TryFormat has constrains on the TEnum,
    /// and we internally want to use this method in cases where we dynamically validate a generic T is an enum rather than T implementing
    /// those constraints. It's a manual copy/paste right now to avoid pressure on the JIT's inlining mechanisms.
    /// </remarks>
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)] // format is most frequently a constant, and we want it exposed to the implementation; this should be inlined automatically, anyway
    public static bool TryFormatUnconstrained<TEnum>(TEnum value, Span<char> destination, out int charsWritten, [StringSyntax(StringSyntaxAttribute.EnumFormat)] ReadOnlySpan<char> format = default)
    {
        try
        {
            string outStr = Enum.Format(typeof(Enum), value!, format.ToString());
            if (outStr.TryCopyTo(destination))
            {
                charsWritten = outStr.Length;
                return true;
            }
            else
            {
                charsWritten = 0;
                return false;
            }
        }
        catch (InvalidOperationException)
        {
            charsWritten = 0;
            return false;
        }
        catch (FormatException)
        {
            charsWritten = 0;
            return false;
        }
    }
#endif
}
