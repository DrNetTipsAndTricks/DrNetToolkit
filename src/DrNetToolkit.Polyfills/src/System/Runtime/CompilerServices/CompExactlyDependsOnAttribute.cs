// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace System.Runtime.CompilerServices;

/// <summary>
/// Use this attribute to indicate that a function should only be compiled into a Ready2Run
/// binary if the associated type will always have a well defined value for its IsSupported property
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, AllowMultiple = true, Inherited = false)]
#if MONO
    [Conditional("unnecessary")] // Mono doesn't use Ready2Run so we can remove this attribute to reduce size
#endif
public sealed class CompExactlyDependsOnAttribute(Type intrinsicsTypeUsedInHelperFunction) : Attribute
{
    /// <summary>
    /// Intrinsics type. It will always have a well defined value for its IsSupported property
    /// </summary>
    public Type IntrinsicsTypeUsedInHelperFunction { get; } = intrinsicsTypeUsedInHelperFunction;
}
