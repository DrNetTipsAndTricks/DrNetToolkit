// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#define TARGET_64BIT

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using DrNetToolkit.Polyfills.Internals;

namespace DrNetToolkit.Polyfills.Hidden;

/// <summary>
/// Implementations of <see cref="RuntimeHelpers"/> hidden methods.
/// </summary>
public static partial class RuntimeHelpersHidden
{
    /// <summary>
    /// Indicates whether the specified type is a reference type or a value type that contains references.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// <see langword="true"/> if the given type is reference type or value type that contains references; otherwise,
    /// <see langword="false"/>.
    /// </returns>
#if NET6_0_OR_GREATER
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
#endif
    public static bool IsReferenceOrContainsReferences(Type type)
        => TypeInfo.IsReferenceOrContainsReferences(type);

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
        => TypeInfo.IsBitwiseEquatable(typeof(T));

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
        => TypeInfo.IsBitwiseEquatable(type);

    /// <summary>
    /// Returns true iff the object has a component size;
    /// i.e., is variable length like System.String or Array.
    /// Callers are required to keep obj alive
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>true iff the object has a component size.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool ObjectHasComponentSize(object obj)
    {
        return GetMethodTable(obj)->HasComponentSize;
    }

    /// <summary>
    /// GetRawData
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>ref to object data.</returns>
    public static ref byte GetRawData(object obj) =>
        ref Unsafe.As<RawData>(obj).Data;

    /// <summary>
    /// Given an object reference, returns its MethodTable*.
    ///
    /// WARNING: The caller has to ensure that MethodTable* does not get unloaded. The most robust way to achieve this
    /// is by using GC.KeepAlive on the object that the MethodTable* was fetched from, e.g.:
    ///
    /// MethodTable* pMT = GetMethodTable(o);
    ///
    /// ... work with pMT ...
    ///
    /// GC.KeepAlive(o);
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe MethodTable* GetMethodTable(object obj)
    {
        // The body of this function will be replaced by the EE with unsafe code
        // See getILIntrinsicImplementationForRuntimeHelpers for how this happens.

        return (MethodTable*)Unsafe.Add(ref Unsafe.As<byte, IntPtr>(ref GetRawData(obj)), -1);
    }
}

/// <summary>
/// Helper class to assist with unsafe pinning of arbitrary objects.
/// It's used by VM code.
/// </summary>
[NonVersionable] // This only applies to field layout
public sealed class RawData
{
    /// <summary>
    /// DataOffset
    /// </summary>
    public byte Data;
}

/// <summary>
/// CLR arrays are laid out in memory as follows (multidimensional array bounds are optional):
/// [ sync block || pMethodTable || num components || MD array bounds || array data .. ]
///                 ^               ^                 ^                  ^ returned reference
///                 |               |                 \-- ref Unsafe.As{RawArrayData}(array).Data
///                 \-- array       \-- ref Unsafe.As{RawData}(array).Data
/// The BaseSize of an array includes all the fields before the array data, including the sync block and method table.
/// The reference to RawData.Data points at the number of components, skipping over these two pointer-sized fields.
/// </summary>
[CLSCompliant(false)]
[NonVersionable] // This only applies to field layout
public sealed class RawArrayData
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public uint Length; // Array._numComponents padded to IntPtr
#if TARGET_64BIT
    public uint Padding;
#endif
    public byte Data;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
/// Subset of src\vm\methodtable.h
/// </summary>
[CLSCompliant(false)]
[StructLayout(LayoutKind.Explicit)]
public unsafe struct MethodTable
{
    /// <summary>
    /// The low WORD of the first field is the component size for array and string types.
    /// </summary>
    [FieldOffset(0)]
    public ushort ComponentSize;

    /// <summary>
    /// The flags for the current method table (only for not array or string types).
    /// </summary>
    [FieldOffset(0)]
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE1006 // Naming Styles
    private uint Flags;
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore IDE0044 // Add readonly modifier

    /// <summary>
    /// The base size of the type (used when allocating an instance on the heap).
    /// </summary>
    [FieldOffset(4)]
    public uint BaseSize;

    // See additional native members in methodtable.h, not needed here yet.
    // 0x8: m_dwFlags2 (additional flags and token in upper 24 bits)
    // 0xC: m_wNumVirtuals

    /// <summary>
    /// The number of interfaces implemented by the current type.
    /// </summary>
    [FieldOffset(0x0E)]
    public ushort InterfaceCount;

    // For DEBUG builds, there is a conditional field here (see methodtable.h again).
    // 0x10: debug_m_szClassName (display name of the class, for the debugger)

    /// <summary>
    /// A pointer to the parent method table for the current one.
    /// </summary>
    [FieldOffset(ParentMethodTableOffset)]
    public MethodTable* ParentMethodTable;

    // Additional conditional fields (see methodtable.h).
    // m_pModule
    // m_pAuxiliaryData
    // union {
    //   m_pEEClass (pointer to the EE class)
    //   m_pCanonMT (pointer to the canonical method table)
    // }

    /// <summary>
    /// This element type handle is in a union with additional info or a pointer to the interface map.
    /// Which one is used is based on the specific method table being in used (so this field is not
    /// always guaranteed to actually be a pointer to a type handle for the element type of this type).
    /// </summary>
    [FieldOffset(ElementTypeOffset)]
    public void* ElementType;

    /// <summary>
    /// This interface map used to list out the set of interfaces. Only meaningful if InterfaceCount is non-zero.
    /// </summary>
    [FieldOffset(InterfaceMapOffset)]
    public MethodTable** InterfaceMap;

    // WFLAGS_LOW_ENUM
#pragma warning disable IDE1006 // Naming Styles
    private const uint enum_flag_GenericsMask = 0x00000030;
    private const uint enum_flag_GenericsMask_NonGeneric = 0x00000000; // no instantiation
    private const uint enum_flag_GenericsMask_GenericInst = 0x00000010; // regular instantiation, e.g. List<String>
    private const uint enum_flag_GenericsMask_SharedInst = 0x00000020; // shared instantiation, e.g. List<__Canon> or List<MyValueType<__Canon>>
    private const uint enum_flag_GenericsMask_TypicalInst = 0x00000030; // the type instantiated at its formal parameters, e.g. List<T>
    private const uint enum_flag_HasDefaultCtor = 0x00000200;
    private const uint enum_flag_IsByRefLike = 0x00001000;

    // WFLAGS_HIGH_ENUM
    private const uint enum_flag_ContainsPointers = 0x01000000;
    private const uint enum_flag_HasComponentSize = 0x80000000;
    private const uint enum_flag_HasTypeEquivalence = 0x02000000;
    private const uint enum_flag_Category_Mask = 0x000F0000;
    private const uint enum_flag_Category_ValueType = 0x00040000;
    private const uint enum_flag_Category_Nullable = 0x00050000;
    private const uint enum_flag_Category_PrimitiveValueType = 0x00060000; // sub-category of ValueType, Enum or primitive value type
    private const uint enum_flag_Category_TruePrimitive = 0x00070000; // sub-category of ValueType, Primitive (ELEMENT_TYPE_I, etc.)
    private const uint enum_flag_Category_ValueType_Mask = 0x000C0000;
    private const uint enum_flag_Category_Interface = 0x000C0000;
    // Types that require non-trivial interface cast have this bit set in the category
    private const uint enum_flag_NonTrivialInterfaceCast = 0x00080000 // enum_flag_Category_Array
                                                         | 0x40000000 // enum_flag_ComObject
                                                         | 0x00400000 // enum_flag_ICastable;
                                                         | 0x10000000 // enum_flag_IDynamicInterfaceCastable;
                                                         | 0x00040000; // enum_flag_Category_ValueType

    private const int DebugClassNamePtr = // adjust for debug_m_szClassName
#if DEBUG
#if TARGET_64BIT
            8
#else
        4
#endif
#else
            0
#endif
        ;
#pragma warning restore IDE1006 // Naming Styles

    private const int ParentMethodTableOffset = 0x10 + DebugClassNamePtr;

#if TARGET_64BIT
        private const int ElementTypeOffset = 0x30 + DebugClassNamePtr;
#else
    private const int ElementTypeOffset = 0x20 + DebugClassNamePtr;
#endif

#if TARGET_64BIT
        private const int InterfaceMapOffset = 0x38 + DebugClassNamePtr;
#else
    private const int InterfaceMapOffset = 0x24 + DebugClassNamePtr;
#endif

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE0251 // Make member 'readonly'
    public bool HasComponentSize => (Flags & enum_flag_HasComponentSize) != 0;

    public bool ContainsGCPointers => (Flags & enum_flag_ContainsPointers) != 0;

    public bool NonTrivialInterfaceCast => (Flags & enum_flag_NonTrivialInterfaceCast) != 0;

    public bool HasTypeEquivalence => (Flags & enum_flag_HasTypeEquivalence) != 0;

    internal static bool AreSameType(MethodTable* mt1, MethodTable* mt2) => mt1 == mt2;

    public bool HasDefaultConstructor => (Flags & (enum_flag_HasComponentSize | enum_flag_HasDefaultCtor)) == enum_flag_HasDefaultCtor;

    public bool IsMultiDimensionalArray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(HasComponentSize);
            // See comment on RawArrayData for details
            return BaseSize > (uint)(3 * sizeof(IntPtr));
        }
    }

    // Returns rank of multi-dimensional array rank, 0 for sz arrays
    public int MultiDimensionalArrayRank
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Debug.Assert(HasComponentSize);
            // See comment on RawArrayData for details
            return (int)((BaseSize - (uint)(3 * sizeof(IntPtr))) / (uint)(2 * sizeof(int)));
        }
    }

    public bool IsInterface => (Flags & enum_flag_Category_Mask) == enum_flag_Category_Interface;

    public bool IsValueType => (Flags & enum_flag_Category_ValueType_Mask) == enum_flag_Category_ValueType;

    public bool IsNullable => (Flags & enum_flag_Category_Mask) == enum_flag_Category_Nullable;

    public bool IsByRefLike => (Flags & (enum_flag_HasComponentSize | enum_flag_IsByRefLike)) == enum_flag_IsByRefLike;

    // Warning! UNLIKE the similarly named Reflection api, this method also returns "true" for Enums.
    public bool IsPrimitive => (Flags & enum_flag_Category_Mask) is enum_flag_Category_PrimitiveValueType or enum_flag_Category_TruePrimitive;

    public bool HasInstantiation => (Flags & enum_flag_HasComponentSize) == 0 && (Flags & enum_flag_GenericsMask) != enum_flag_GenericsMask_NonGeneric;

    public bool IsGenericTypeDefinition => (Flags & (enum_flag_HasComponentSize | enum_flag_GenericsMask)) == enum_flag_GenericsMask_TypicalInst;

    public bool IsConstructedGenericType
    {
        get
        {
            uint genericsFlags = Flags & (enum_flag_HasComponentSize | enum_flag_GenericsMask);
            return genericsFlags == enum_flag_GenericsMask_GenericInst || genericsFlags == enum_flag_GenericsMask_SharedInst;
        }
    }
#pragma warning restore IDE0251 // Make member 'readonly'
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    /// <summary>
    /// Gets a <see cref="TypeHandle"/> for the element type of the current type.
    /// </summary>
    /// <remarks>This method should only be called when the current <see cref="MethodTable"/> instance represents an array or string type.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TypeHandle GetArrayElementTypeHandle()
    {
        Debug.Assert(HasComponentSize);

        return new(ElementType);
    }

    //[MethodImpl(MethodImplOptions.InternalCall)]
    //public extern uint GetNumInstanceFieldBytes();
}

/// <summary>
/// A type handle, which can wrap either a pointer to a <c>TypeDesc</c> or to a <see cref="MethodTable"/>.
/// </summary>
#pragma warning disable IDE0250 // Make struct 'readonly'
[CLSCompliant(false)]
public unsafe struct TypeHandle
{
    // Subset of src\vm\typehandle.h

    /// <summary>
    /// The address of the current type handle object.
    /// </summary>
#pragma warning disable IDE1006 // Naming Styles
    private readonly void* m_asTAddr;
#pragma warning restore IDE1006 // Naming Styles


    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET8_0_OR_GREATER
//#pragma warning disable IDE0290 // Use primary constructor
#endif
    public TypeHandle(void* tAddr)
#if NET8_0_OR_GREATER
//#pragma warning restore IDE0290 // Use primary constructor
#endif
    {
        m_asTAddr = tAddr;
    }

    /// <summary>
    /// Gets whether the current instance wraps a <see langword="null"/> pointer.
    /// </summary>
#pragma warning disable IDE0251 // Make member 'readonly'
    public bool IsNull => m_asTAddr is null;
#pragma warning restore IDE0251 // Make member 'readonly'

    /// <summary>
    /// Gets whether or not this <see cref="TypeHandle"/> wraps a <c>TypeDesc</c> pointer.
    /// Only if this returns <see langword="false"/> it is safe to call <see cref="AsMethodTable"/>.
    /// </summary>
    public bool IsTypeDesc
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable IDE0251 // Make member 'readonly'
        get => ((nint)m_asTAddr & 2) != 0;
#pragma warning restore IDE0251 // Make member 'readonly'
    }

    /// <summary>
    /// Gets the <see cref="MethodTable"/> pointer wrapped by the current instance.
    /// </summary>
    /// <remarks>This is only safe to call if <see cref="IsTypeDesc"/> returned <see langword="false"/>.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodTable* AsMethodTable()
    {
        Debug.Assert(!IsTypeDesc);

        return (MethodTable*)m_asTAddr;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static TypeHandle TypeHandleOf<T>()
    //{
    //    return new TypeHandle((void*)RuntimeTypeHandle.ToIntPtr(typeof(T).TypeHandle));
    //}
}
#pragma warning restore IDE0250 // Make struct 'readonly'
