// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.Polyfills.Internals;

internal static partial class SR
{
    /// <summary>Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.</summary>
    internal static string @Arg_BogusIComparer => "Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.";

    /// <summary>The string must be null-terminated.</summary>
    internal static string @Arg_MustBeNullTerminatedString => "The string must be null-terminated.";

    /// <summary>Cannot use type '{0}'. Only value types without pointers or references are supported.</summary>
    internal static string @Argument_InvalidTypeWithPointersNotSupported => "Cannot use type '{0}'. Only value types without pointers or references are supported.";

    /// <summary>Failed to compare two elements in the array.</summary>
    internal static string @InvalidOperation_IComparerFailed => "Failed to compare two elements in the array.";
}
