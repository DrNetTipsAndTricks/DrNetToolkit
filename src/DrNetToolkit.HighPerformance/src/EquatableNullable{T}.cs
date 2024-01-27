// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance;

/// <summary>
/// Represents a value type that can be assigned <see langword="null"/> and supports <see cref="IEquatable{T}"/> and
/// <see cref="IComparable{T}"/> interfaces.
/// 
/// Actually, it is a wrapper structure for the <see cref="Nullable{T}"/> structure which additionally support of
/// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
/// 
/// The most useful extension methods for <see cref="Span{T}"/> or <see cref="ReadOnlySpan{T}"/> structure require that
/// elements support an <see cref="IEquatable{T}"/> an/or an <see cref="IComparable{T}"/> interfaces, but the
/// <see cref="Nullable{T}"/> structure does not have them. Therefore, many extension methods for spans does not
/// support nullable value types. At the  same time, ordinary arrays perfectly support nullable value types.
/// 
/// To support many extension methods for spans with nullable value elements, you can use this wrapper structure to
/// cast spans with <see cref="Nullable{T}"/> values to spans with <see cref="EquatableNullable{T}"/> values. After
/// this, many extension methods for spans become available.
/// </summary>
/// <typeparam name="T">The underlying value type of the <see cref="EquatableNullable{T}"/> structure.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="EquatableNullable{T}"/> structure to the specified
/// <see cref="Nullable{T}"/> value.
/// </remarks>
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct EquatableNullable<T>(T? value)
    : IEquatable<EquatableNullable<T>>, IComparable<EquatableNullable<T>>
    where T : struct
{
    /// <summary>
    /// Gets the wrapped <see cref="Nullable{T}"/> value.
    /// </summary>
    /// <returns>
    /// The <see cref="Nullable{T}"/> value of the current <see cref="EquatableNullable{T}"/> structure.
    /// </returns>
    public readonly T? NullableValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    } = value;

    ///// <summary>
    ///// Gets a value indicating whether the current <see cref="EquatableNullable{T}"/> object has a valid value of its
    ///// underlying type.
    ///// </summary>
    ///// <returns>
    ///// <see langword="true"/> if the current <see cref="EquatableNullable{T}"/> structure has a value;
    ///// <see langword="false"/> if the current <see cref="EquatableNullable{T}"/> structure has no value.
    ///// </returns>
    ///// <seealso cref="Nullable{T}.HasValue"/>
    //public readonly bool HasValue
    //{
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    get => NullableValue.HasValue;
    //}

    ///// <summary>
    ///// Gets the value of the current <see cref="EquatableNullable{T}"/> structure if it has been assigned a valid
    ///// underlying value.
    ///// </summary>
    ///// <returns>
    ///// The value of the current <see cref="EquatableNullable{T}"/> structure if the <see cref="HasValue"/> property is
    ///// <see langword="true"/>. An exception is thrown if the <see cref="HasValue"/> property is
    ///// <see langword="false"/>.
    ///// </returns>
    ///// <exception cref="InvalidOperationException">
    ///// The <see cref="HasValue"/> property is <see langword="false"/>.
    ///// </exception>
    ///// <seealso cref="Nullable{T}.Value"/>
    //public readonly T Value
    //{
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    get => NullableValue!.Value;
    //}

    /// <summary>
    /// Retrieves the value of the current <see cref="EquatableNullable{T}"/> structure, or the default value of the
    /// underlying type.
    /// </summary>
    /// <returns>
    /// The value of the <see cref="Value"/> property if the <see cref="HasValue"/> property is <see langword="true"/>;
    /// otherwise, the default value of the underlying type.
    /// </returns>
    /// <remarks>
    /// The <see cref="GetValueOrDefault"/> method returns a value even if the <see cref="HasValue"/> property is
    /// <see langword="false"/> (unlike the <see cref="Value"/> property, which throws an exception). If the
    /// <see cref="HasValue"/> property is <see langword="false"/>, the method returns the default value of the
    /// underlying type.
    /// </remarks>
    /// <seealso cref="GetValueOrDefault(T)"/>
    /// <seealso cref="Nullable{T}.GetValueOrDefault()"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValueOrDefault()
        => NullableValue.GetValueOrDefault();

    /// <summary>
    /// Retrieves the value of the current <see cref="EquatableNullable{T}"/> structure, or the specified default
    /// value.
    /// </summary>
    /// <param name="defaultValue">
    /// A value to return if the <see cref="HasValue"/> property is <see langword="false"/>.
    /// </param>
    /// <returns>
    /// The value of the <see cref="Value"/> property if the <see cref="HasValue"/> property is <see langword="true"/>;
    /// otherwise, the <paramref name="defaultValue"/> parameter.
    /// </returns>
    /// <remarks>
    /// The <see cref="GetValueOrDefault"/> method returns a value even if the <see cref="HasValue"/> property is
    /// <see langword="false"/> (unlike the <see cref="Value"/> property, which throws an exception). If the
    /// <see cref="HasValue"/> property is <see langword="false"/>, the method returns the
    /// <paramref name="defaultValue"/> parameter.
    /// </remarks>
    /// <seealso cref="GetValueOrDefault()"/>
    /// <seealso cref="Nullable{T}.GetValueOrDefault(T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetValueOrDefault(T defaultValue)
        => NullableValue.GetValueOrDefault(defaultValue);

    /// <summary>
    /// Indicates whether the current <see cref="EquatableNullable{T}"/> structure is equal to a specified object.
    /// </summary>
    /// <param name="other">An object.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="other"/> parameter is equal to the current
    /// <see cref="EquatableNullable{T}"/> structure; otherwise, <see langword="false"/>.
    ///
    /// See <see cref="Nullable{T}.Equals(object)"/> method for description how equality is defined for the compared
    /// values.
    /// </returns>
    /// <seealso cref="Nullable{T}.Equals(object)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? other)
        => NullableValue.Equals(other);

    /// <summary>
    /// Retrieves the hash code of the value returned by the <see cref="Value"/> property.
    /// </summary>
    /// <returns>
    /// The hash code of the value returned by the <see cref="Value"/> property if the <see cref="HasValue"/> property
    /// is <see langword="true"/>, or zero if the <see cref="HasValue"/> property is <see langword="false"/>.
    /// </returns>
    /// <seealso cref="Nullable{T}.GetHashCode"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => NullableValue.GetHashCode();

    /// <summary>
    /// Returns the text representation of the value returned by the <see cref="Value"/> property.
    /// </summary>
    /// <returns>
    /// The text representation of the value returned by the <see cref="Value"/> if the <see cref="HasValue"/> property
    /// is <see langword="true"/>, or an empty string ("") if the <see cref="HasValue"/> property is
    /// <see langword="false"/>.
    /// </returns>
    /// <seealso cref="Nullable{T}.ToString"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string? ToString()
        => NullableValue.ToString();

    /// <summary>
    /// Creates a new <see cref="EquatableNullable{T}"/> initialized to a specified value.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// A <see cref="EquatableNullable{T}"/> structure whose <see cref="NullableValue"/> property is initialized with
    /// the <paramref name="value"/> parameter.
    /// </returns>
    /// <remarks>
    /// The <see cref="Value"/> property of the new <see cref="EquatableNullable{T}"/> value is initialized to the
    /// <paramref name="value"/> parameter and the <see cref="HasValue"/> property is initialized to
    /// <see langword="true"/>.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator EquatableNullable<T>(T value)
        => new(value);

    /// <summary>
    /// Creates a new <see cref="EquatableNullable{T}"/> initialized to a specified value.
    /// </summary>
    /// <param name="value">A nullable value.</param>
    /// <returns>
    /// A <see cref="EquatableNullable{T}"/> structure whose <see cref="Value"/> property is initialized with
    /// the <paramref name="value"/> parameter.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator EquatableNullable<T>(T? value)
        => new(value);

    /// <summary>
    /// Conversion of a <see cref="EquatableNullable{T}"/> instance to its underlying <see cref="Nullable<T>"/> value.
    /// </summary>
    /// <param name="value">A value instance.</param>
    /// <returns>
    /// The value of the <see cref="NullableValue"/> property for the <paramref name="value"/> parameter.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T?(EquatableNullable<T> value)
        => value.NullableValue;

    /// <summary>
    /// Defines an explicit conversion of a <see cref="EquatableNullable{T}"/> instance to its underlying value.
    /// </summary>
    /// <param name="value">A value instance.</param>
    /// <returns>
    /// The value of the <see cref="Value"/> property for the <paramref name="value"/> parameter.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator T(EquatableNullable<T> value)
        => value.NullableValue!.Value;

    /// <summary>
    /// Indicates whether this value is equal to another value.
    /// </summary>
    /// <param name="other">The other value to compare with this value.</param>
    /// <returns>
    /// <see langword="true"/> if this value is equal to the <paramref name="other"/> value; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(EquatableNullable<T> other)
        => EqualityComparer<T?>.Default.Equals(NullableValue, other.NullableValue);

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is equal to the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> value is equal to the <paramref name="right"/> value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(EquatableNullable<T> left, EquatableNullable<T> right)
        => EqualityComparer<T?>.Default.Equals(left.NullableValue, right.NullableValue);

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is not equal to the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> value is not equal to the <paramref name="right"/> value;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(EquatableNullable<T> left, EquatableNullable<T> right)
        => !EqualityComparer<T?>.Default.Equals(left.NullableValue, right.NullableValue);

    /// <summary>
    /// Compares this value with another value and returns an integer that indicates whether the this value precedes, 
    /// follows, or occurs in the same position in the sort order as the other value.
    /// </summary>
    /// <param name="other">The other value to compare with this value.</param>
    /// <returns>
    /// A value that indicates the relative order of the values being compared. The return value has these meanings:
    /// <list type="table">
    ///     <listheader>
    ///         <term>Return Value</term>
    ///         <description>Meaning</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Less than zero</term>
    ///         <description>This value precedes <paramref name="other"/> in the sort order.</description>
    ///     </item>
    ///     <item>
    ///         <term>Zero</term>
    ///         <description>This value occurs in the same position in the sort order as <paramref name="other"/>.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description>This value follows <paramref name="other"/> in the sort order.</description>
    ///     </item>
    /// </list>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(EquatableNullable<T> other)
        => Comparer<T?>.Default.Compare(NullableValue, other.NullableValue);

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is less than the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> value is less than the <paramref name="right"/> value;
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator <(EquatableNullable<T> left, EquatableNullable<T> right)
        => Comparer<T?>.Default.Compare(left, right) < 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is greater than the <paramref name="right"/> value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> value is greater than the <paramref name="right"/> value;
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator >(EquatableNullable<T> left, EquatableNullable<T> right)
        => Comparer<T?>.Default.Compare(left, right) > 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is less than or equal to the <paramref name="right"/>
    /// value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> value is less than or equal to the
    /// <paramref name="right"/> value; <see langword="false"/> otherwise.
    public static bool operator <=(EquatableNullable<T> left, EquatableNullable<T> right)
        => Comparer<T?>.Default.Compare(left, right) <= 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> value is greater than or equal to the <paramref name="right"/> 
    /// value.
    /// </summary>
    /// <param name="left">A left value to compare with the <paramref name="right"/> value.</param>
    /// <param name="right">A right value to compare with the <paramref name="left"/> value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> value is greater than or equal to the
    /// <paramref name="right"/> value; <see langword="false"/> otherwise.
    /// </returns>
    public static bool operator >=(EquatableNullable<T> left, EquatableNullable<T> right)
        => Comparer<T?>.Default.Compare(left, right) >= 0;
}

public static partial class SpanExtensions
{
#if NETSTANDARD2_1_OR_GREATER

    /// <summary>
    /// Creates a <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces. After this, many 
    /// <see cref="MemoryExtensions"/> methods for spans become available.
    /// </summary>
    /// <typeparam name="T">The underlying value type of <see cref="source"/> elements.</typeparam>
    /// <param name="source">A source with <see cref="Nullable{T}"/> elements.</param>
    /// <returns>
    /// A <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
    /// </returns>
    public static Span<EquatableNullable<T>> AsEquatable<T>(this Span<T?> source)
        where T : struct
        => MemoryMarshaling.Cast<T, EquatableNullable<T>>(source);

    /// <summary>
    /// Creates a <see cref="ReadOnlySpan{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces. After this, many 
    /// <see cref="MemoryExtensions"/> methods for spans become available.
    /// </summary>
    /// <typeparam name="T">The underlying value type of <see cref="source"/> elements.</typeparam>
    /// <param name="source">A source with <see cref="Nullable{T}"/> elements.</param>
    /// <returns>
    /// A <see cref="ReadOnlySpan{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
    /// </returns>
    public static ReadOnlySpan<EquatableNullable<T>> AsEquatable<T>(this ReadOnlySpan<T?> source)
        where T : struct
        => MemoryMarshaling.Cast<T, EquatableNullable<T>>(source);

    /// <summary>
    /// Creates a <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces. After this, many 
    /// <see cref="MemoryExtensions"/> methods for spans become available.
    /// </summary>
    /// <typeparam name="T">The underlying value type of <see cref="source"/> elements.</typeparam>
    /// <param name="source">A source with <see cref="Nullable{T}"/> elements.</param>
    /// <returns>
    /// A <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
    /// </returns>
    public static Span<T?> AsNullable<T>(this Span<EquatableNullable<T>> source)
        where T : struct
        => MemoryMarshaling.CastToNullable<EquatableNullable<T>, T>(source);


    /// <summary>
    /// Creates a <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces. After this, many 
    /// <see cref="MemoryExtensions"/> methods for spans become available.
    /// </summary>
    /// <typeparam name="T">The underlying value type of <see cref="source"/> elements.</typeparam>
    /// <param name="source">A source with <see cref="Nullable{T}"/> elements.</param>
    /// <returns>
    /// A <see cref="Span{T}"/> with <see cref="EquatableNullable{T}"/> elements that support the 
    /// <see cref="IEquatable{T}"/> and <see cref="IComparable{T}"/> interfaces.
    /// </returns>
    public static ReadOnlySpan<T?> AsNullable<T>(this ReadOnlySpan<EquatableNullable<T>> source)
        where T : struct
        => MemoryMarshaling.CastToNullable<EquatableNullable<T>, T>(source);

#endif
}
