// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.Polyfills.Internals;

internal static partial class SR
{
    /// <summary>The string must be null-terminated.</summary>
    internal static string @Arg_MustBeNullTerminatedString => "The string must be null-terminated.";

    /// <summary>Cannot use type '{0}'. Only value types without pointers or references are supported.</summary>
    internal static string @Argument_InvalidTypeWithPointersNotSupported => "Cannot use type '{0}'. Only value types without pointers or references are supported.";
}
