// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Runtime;

/// <summary>
/// Low-level utility methods and polyfills for .NET Standard 2.0.
/// </summary>
public static class RuntimeHelpers
{
    /// <summary>
    /// Returns a value that indicates whether the specified type is a reference type or a value type that contains
    /// references.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsReferenceOrContainsReferences<T>()
        => TypeInfo<T>.IsReferenceOrContainsReferences;

    /// <summary>
    /// Returns a value that indicates whether the specified type is a reference type or a value type that contains
    /// references.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsReferenceOrContainsReferences(Type type)
    {
        // Common case, for primitive types
        if (type.IsPrimitive)
        {
            return false;
        }

        // Explicitly check for pointer types first
        if (type.IsPointer)
        {
            return false;
        }

        // Check for value types (this has to be after checking for pointers)
        if (!type.IsValueType)
        {
            return true;
        }

        // Check if the type is Nullable<T>
        if (Nullable.GetUnderlyingType(type) is Type nullableType)
        {
            type = nullableType;
        }

        if (type.IsEnum)
        {
            return false;
        }

        // Complex struct, recursively inspect all fields
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (IsReferenceOrContainsReferences(field.FieldType))
            {
                return true;
            }
        }

        return false;
    }
}
