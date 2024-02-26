// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace DrNetToolkit.Polyfills.Internals;

/// <summary>
/// Retrieves and caches type information.
/// </summary>
internal sealed class TypeInfo(Type type)
{
    //private Type Type { get; } = type;

    #region IsEnum

#if !NETSTANDARD2_0_OR_GREATER
    public static bool IsEnum(Type type) => GetOrCreateTypeInfo(type).IsEnumProp;

    private bool? _isEnum;
    private bool IsEnumProp { get => _isEnum ??= type.IsSubclassOf(typeof(Enum)); }
#endif

    #endregion

    #region IsPrimitive

#if !NETSTANDARD2_0_OR_GREATER
    public static bool IsPrimitive(Type type) => GetOrCreateTypeInfo(type).IsPrimitiveProp;

    private bool? _isPrimitive;
    private bool IsPrimitiveProp { get => _isPrimitive ??= IsPrimitiveImpl(type); }

    /// <summary>
    /// Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static bool IsPrimitiveImpl(Type type)
        => type == typeof(bool) ||
            type == typeof(byte) || type == typeof(sbyte) ||
            type == typeof(short) || type == typeof(ushort) ||
            type == typeof(int) || type == typeof(uint) ||
            type == typeof(long) || type == typeof(ulong) ||
            type == typeof(nint) || type == typeof(nuint) ||
            type == typeof(char) ||
            type == typeof(double) || type == typeof(float);
#endif

    #endregion

    #region IsValueType

#if !NETSTANDARD2_0_OR_GREATER
    public static bool IsValueType(Type type) => GetOrCreateTypeInfo(type).IsValueTypeProp;

    private bool? _isValueType;
    private bool IsValueTypeProp { get => _isValueType ??= type.IsSubclassOf(typeof(ValueType)); }
#endif

    #endregion

    #region IsSubclassOf

#if !NETSTANDARD2_0_OR_GREATER
    public static bool IsSubclassOf(Type left, Type right)
        => GetOrCreateTypeInfo(left).GetOrCreateOtherTypeInfo(left, right).IsSubclassOf;
#endif

    #endregion

    #region IsReferenceOrContainsReferences

#if NET6_0_OR_GREATER
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
#endif
    public static bool IsReferenceOrContainsReferences(Type type)
        => GetOrCreateTypeInfo(type).IsReferenceOrContainsReferencesProp;

    private bool? _isReferenceOrContainsReferences;
    private bool IsReferenceOrContainsReferencesProp
    {
#if NET6_0_OR_GREATER
        [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
#endif
        get => _isReferenceOrContainsReferences ??= IsReferenceOrContainsReferencesImpl(type);
    }

#if NET6_0_OR_GREATER
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
#endif
    private static bool IsReferenceOrContainsReferencesImpl(Type type)
    {
        // Check for value types
        if (type.IsValueType())
        {
            if (type.IsPointer)
                return false;
            return true;
        }

        // Check if the type is Nullable<T>
        if (Nullable.GetUnderlyingType(type) is Type nullableType)
            type = nullableType;

        // Common case, for primitive types
        if (type.IsPrimitive() || type.IsEnum())
            return false;

        // Complex struct, recursively inspect all fields
#if NETSTANDARD2_0_OR_GREATER
        foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
#else
        foreach (FieldInfo field in type.GetTypeInfo().DeclaredFields)
#endif
            if (IsReferenceOrContainsReferencesImpl(field.FieldType))
                return true;

        return false;
    }

    #endregion

    #region IsBitwiseEquatable

    public static bool IsBitwiseEquatable(Type type) => GetOrCreateTypeInfo(type).IsBitwiseEquatableProp;

    private bool? _isBitwiseEquatable;
    private bool IsBitwiseEquatableProp
    {
        get => _isBitwiseEquatable ??= IsBitwiseEquatableImpl(type);
    }

    private static bool IsBitwiseEquatableImpl(Type type)
    {
        // Ideally we could detect automatically whether a type is trivially equatable
        // (i.e., its operator == could be implemented via memcmp). But for now we'll
        // do the simple thing and hardcode the list of types we know fulfill this contract.
        // n.b. This doesn't imply that the type's CompareTo method can be memcmp-implemented,
        // as a method like CompareTo may need to take a type's signedness into account.

        if (!type.IsValueType())
        {
            if (type.IsPointer)
                return true;

            return false;
        }

        if (Nullable.GetUnderlyingType(type) is Type nullableType)
            type = nullableType;

        // Common case, for primitive types
        if (type.IsPrimitive())
        {
            if (type == typeof(bool)    ||
                type == typeof(byte)    || type == typeof(sbyte) ||
                type == typeof(char)    ||
                type == typeof(ushort)  || type == typeof(short) ||
                type == typeof(uint)    || type == typeof(int)   ||
                type == typeof(ulong)   || type == typeof(ulong) ||
                type == typeof(UIntPtr) || type == typeof(IntPtr))
            {
                return true;
            }

            return false;
        }

        if (type.IsEnum())
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

    #endregion

    #region TypeInfoCache

    private static readonly Dictionary<Type, TypeInfo> s_typeInfoCache = [];

    private static TypeInfo GetOrCreateTypeInfo(Type type)
    {
        if (s_typeInfoCache.TryGetValue(type, out TypeInfo? typeInfo))
            return typeInfo;

        typeInfo = new TypeInfo(type);
        lock (s_typeInfoCache)
        {
            if (s_typeInfoCache.TryGetValue(type, out TypeInfo? typeInfo2))
                return typeInfo2;
            s_typeInfoCache.Add(type, typeInfo);
        }
        return typeInfo;
    }

    #endregion

    #region OtherTypeInfo
#if !NETSTANDARD2_0_OR_GREATER

    private Dictionary<Type, OtherTypeInfo>? _otherTypeInfoCache;

    private OtherTypeInfo GetOrCreateOtherTypeInfo(Type type, Type other)
    {
        _otherTypeInfoCache ??= [];

        if (_otherTypeInfoCache.TryGetValue(other, out OtherTypeInfo otherTypeInfo))
            return otherTypeInfo;

        otherTypeInfo = new OtherTypeInfo(type, other);
        lock (_otherTypeInfoCache)
        {
            if (_otherTypeInfoCache.TryGetValue(other, out OtherTypeInfo otherTypeInfo2))
                return otherTypeInfo2;
            _otherTypeInfoCache.Add(other, otherTypeInfo);
        }
        return otherTypeInfo;
    }

#if !NETSTANDARD2_0_OR_GREATER
    /// <summary>
    /// Retrieves and caches type information.
    /// </summary>
    private sealed class OtherTypeInfo(Type type, Type other)
    {
        #region IsSubclassOf

#if !NETSTANDARD2_0_OR_GREATER
        private bool? _isSubclassOf;
        public bool IsSubclassOf { get => _isSubclassOf ??= IsSubclassOfImpl(type, other); }

        private static bool IsSubclassOfImpl(Type type, Type other)
        {
            while (type != null)
            {
                type = type.GetTypeInfo().BaseType;
                if (type == other)
                    return true;
            }
            return false;
        }
#endif

        #endregion
    }
#endif

#endif
    #endregion
}
