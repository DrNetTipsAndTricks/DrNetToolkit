// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance;

/// <summary>
/// Extension methods for the <see cref="Nullable{T}"/> structure.
/// </summary>
public static class NullableExtensions
{

#if !NET7_0_OR_GREATER
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Retrieves a readonly reference to the location in the <see cref="Nullable{T}"/> instance where the
    /// <see cref="Nullable{T}.Value"/> is stored.
    /// </summary>
    /// <typeparam name="T">The underlying value type of the <see cref="Nullable{T}"/> generic type.</typeparam>
    /// <param name="nullable">The readonly reference to the input <see cref="Nullable{T}"/> value.</param>
    /// <returns>
    /// A readonly reference to the location where the instance's <see cref="Nullable{T}.Value"/> is stored. If the
    /// instance's <see cref="Nullable{T}.HasValue"/> is false, the current value at that location may be the default
    /// value.
    /// </returns>
    /// <seealso cref="Nullable.GetValueRefOrDefaultRef{T}(in T?)"/>
#if !NET7_0_OR_GREATER
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly T GetValueRef<T>(this in T? nullable)
        where T : struct
#if NET7_0_OR_GREATER
        => ref Nullable.GetValueRefOrDefaultRef(in nullable);
#else
        => ref Unsafe.As<T?, RawNullableData<T>>(ref Unsafe.AsRef(in nullable)).Value;
#endif

#if !NET7_0_OR_GREATER
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Retrieves a reference to the location in the <see cref="Nullable{T}"/> instance where the
    /// <see cref="Nullable{T}.Value"/> is stored.
    /// </summary>
    /// <typeparam name="T">The underlying value type of the <see cref="Nullable{T}"/> generic type.</typeparam>
    /// <param name="nullable">The readonly reference to the input <see cref="Nullable{T}"/> value.</param>
    /// <returns>
    /// A reference to the location where the instance's <see cref="Nullable{T}.Value"/> is stored. If the instance's
    /// <see cref="Nullable{T}.HasValue"/> is false, the current value at that location may be the default value.
    /// </returns>
    /// <seealso cref="Nullable.GetValueRefOrDefaultRef{T}(in T?)"/>
#if !NET7_0_OR_GREATER
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T DangerousGetValueRef<T>(in T? nullable)
        where T : struct
#if NET7_0_OR_GREATER
        => ref Unsafe.AsRef(in Nullable.GetValueRefOrDefaultRef(in nullable));
#else
        => ref Unsafe.As<T?, RawNullableData<T>>(ref Unsafe.AsRef(in nullable)).Value;
#endif


#if !NET7_0_OR_GREATER
    /// <summary>
    /// Mapping type that reflects the internal layout of the <see cref="Nullable{T}"/> type.
    /// See https://github.com/dotnet/runtime/blob/master/src/libraries/System.Private.CoreLib/src/System/Nullable.cs.
    /// </summary>
    /// <typeparam name="T">The value type wrapped by the current instance.</typeparam>
    private struct RawNullableData<T>
        where T : struct
    {
#pragma warning disable CS0649 // Unassigned fields
        public bool HasValue;
        public T Value;
#pragma warning restore CS0649
    }
#endif
}
