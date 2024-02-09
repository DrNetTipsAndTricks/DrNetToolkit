// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

#if NETCOREAPP3_0_OR_GREATER
using System.Text;
#endif

#if NETSTANDARD2_1_OR_GREATER
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace DrNetToolkit.Runtime;

/// <summary>
/// Low-level utility methods and polyfills for .NET Standard 2.0.
/// </summary>
public static class RuntimeHelpers
{
    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsReferenceOrContainsReferences<T>()
    {
#if NETSTANDARD2_1_OR_GREATER
        return RTHelpers.IsReferenceOrContainsReferences<T>();
#else
        return TypeInfo<T>.IsReferenceOrContainsReferences;
#endif
    }

    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool IsReferenceOrContainsReferences(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
        DynamicallyAccessedMemberTypes.NonPublicFields)]
        Type type)
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
#pragma warning disable IL2072 // Target parameter argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The return value of the source method does not have matching annotations.
            if (IsReferenceOrContainsReferences(field.FieldType))
            {
                return true;
            }
#pragma warning restore IL2072 // Target parameter argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The return value of the source method does not have matching annotations.
        }

        return false;
    }

    /// <summary>
    /// Indicates whether the specified type is bitwise equatable (memcmp can be used for equality checking).
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>
    /// <see langword="true"/> if given type is bitwise equatable (memcmp can be used for equality checking).
    /// </returns>
    /// <remarks>
    /// Only use the result of this for Equals() comparison, not for CompareTo() comparison.
    /// </remarks>
    public static bool IsBitwiseEquatable<T>()
        => TypeInfo<T>.IsBitwiseEquatable;

    /// <summary>
    /// Indicates whether the specified type is bitwise equatable (memcmp can be used for equality checking).
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if given type is bitwise equatable (memcmp can be used for equality checking).
    /// </returns>
    /// <remarks>
    /// Only use the result of this for Equals() comparison, not for CompareTo() comparison.
    /// </remarks>
    public static bool IsBitwiseEquatable(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type)
    {
        // Ideally we could detect automatically whether a type is trivially equatable
        // (i.e., its operator == could be implemented via memcmp). But for now we'll
        // do the simple thing and hardcode the list of types we know fulfill this contract.
        // n.b. This doesn't imply that the type's CompareTo method can be memcmp-implemented,
        // as a method like CompareTo may need to take a type's signedness into account.

        if (!type.IsValueType)
        {
            if (type.IsPointer)
                return true;

            return false;
        }

        // Common case, for primitive types
        if (type.IsPrimitive)
        {
            if (type == typeof(bool) ||
                type == typeof(byte) || type == typeof(sbyte) ||
                type == typeof(char) ||
                type == typeof(ushort) || type == typeof(short) ||
                type == typeof(uint) || type == typeof(int) ||
                type == typeof(ulong) || type == typeof(ulong) ||
                type == typeof(UIntPtr) || type == typeof(IntPtr))
            {
                return true;
            }

            return false;
        }

        if (type.IsEnum)
            return true;

#if NETCOREAPP3_0_OR_GREATER
        if (type == typeof(Rune))
            return true;
#endif

        return false;

        //switch (elementType.UnderlyingType.Category)
        //{
        //    case TypeFlags.Boolean:
        //    case TypeFlags.Byte:
        //    case TypeFlags.SByte:
        //    case TypeFlags.Char:
        //    case TypeFlags.UInt16:
        //    case TypeFlags.Int16:
        //    case TypeFlags.UInt32:
        //    case TypeFlags.Int32:
        //    case TypeFlags.UInt64:
        //    case TypeFlags.Int64:
        //    case TypeFlags.IntPtr:
        //    case TypeFlags.UIntPtr:
        //        result = true;
        //        break;
        //    default:
        //        result = false;
        //        if (elementType is MetadataType mdType)
        //        {
        //            if (mdType.Module == mdType.Context.SystemModule &&
        //                mdType.Namespace == "System.Text" &&
        //                mdType.Name == "Rune")
        //            {
        //                result = true;
        //            }
        //            else if (mdType.IsValueType)
        //            {
        //                bool? equatable = ComparerIntrinsics.ImplementsIEquatable(mdType.GetTypeDefinition());
        //                if (equatable.HasValue && !equatable.Value)
        //                {
        //                    // Value type that can use memcmp and that doesn't override object.Equals or implement IEquatable<T>.Equals.
        //                    MethodDesc objectEquals = mdType.Context.GetWellKnownType(WellKnownType.Object).GetMethod("Equals", null);
        //                    result =
        //                        mdType.FindVirtualFunctionTargetMethodOnObjectType(objectEquals).OwningType != mdType &&
        //                        ComparerIntrinsics.CanCompareValueTypeBits(mdType, objectEquals);
        //                }
        //            }
        //        }
        //        break;
        //}
    }
}
