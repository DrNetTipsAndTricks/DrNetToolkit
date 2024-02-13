// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace System.Runtime.InteropServices;

#if !NETSTANDARD1_1_OR_GREATER

/// <summary>
/// Indicates that data should be marshaled from the caller to the callee, but not back to the caller.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
public sealed class InAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InAttribute"/> class.
    /// </summary>
    public InAttribute()
    { }
}

#endif
