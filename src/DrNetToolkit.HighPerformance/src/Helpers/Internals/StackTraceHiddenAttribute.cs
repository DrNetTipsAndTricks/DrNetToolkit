// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if !NET7_0_OR_GREATER

namespace System.Diagnostics;

/// <summary>
/// Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in StackTrace.ToString()
/// and Exception.StackTrace
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Struct, Inherited = false)]
internal sealed class StackTraceHiddenAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StackTraceHiddenAttribute"/> class.
    /// </summary>
    public StackTraceHiddenAttribute() { }
}

#endif
