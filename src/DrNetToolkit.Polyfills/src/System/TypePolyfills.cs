// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Internals;

namespace System;

/// <summary>
/// <see cref="Type"/> polyfills.
/// </summary>
public static class TypePolyfills
{
    /// <summary>
    /// Gets a value indicating whether the current <paramref name="type"/> represents an enumeration.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the current <paramref name="type"/> represents an enumeration; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0_OR_GREATER
    public static bool IsEnum(this Type type) => type.IsEnum;
#else
    public static bool IsEnum(this Type type) => TypeInfo.IsEnum(type);
#endif

    /// <summary>
    /// Gets a value indicating whether the <paramref name="type"/> is one of the primitive types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="type"/> is one of the primitive types; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0_OR_GREATER
    public static bool IsPrimitive(this Type type) => type.IsPrimitive;
#else
    public static bool IsPrimitive(this Type type) => TypeInfo.IsPrimitive(type);
#endif

    /// <summary>
    /// Gets a value indicating whether the <paramref name="type"/> is a value type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="type"/> is a value type; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0_OR_GREATER
    public static bool IsValueType(this Type type) => type.IsValueType;
#else
    public static bool IsValueType(this Type type) => TypeInfo.IsValueType(type);
#endif

    /// <summary>
    /// Determines whether the <paramref name="left"/> type derives from the <paramref name="right"/> type.
    /// </summary>
    /// <param name="left">The left type.</param>
    /// <param name="right">The right type.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> type derives from the <paramref name="right"/> type;
    /// otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if
    /// <paramref name="left"/> type and the <paramref name="right"/> type are equal.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_0_OR_GREATER
    public static bool IsSubclassOf(this Type left, Type right) => left.IsSubclassOf(right);
#else
    public static bool IsSubclassOf(this Type left, Type right) => TypeInfo.IsSubclassOf(left, right);
#endif
}
