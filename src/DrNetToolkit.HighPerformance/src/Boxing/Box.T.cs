// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using DrNetToolkit.HighPerformance.Boxing.Base;

namespace DrNetToolkit.HighPerformance.Boxing;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CA1067 // Override Object.Equals(object) when implementing IEquatable<T>

/// <inheritdoc/>
public sealed class Box<T> : BoxBase<T>, IEquatable<Box<T>?>, IComparable<Box<T>?>
    where T : struct
{
    /// <inheritdoc/>
    private Box() : base() { }

    /// <summary>
    /// Implicitly creates a new boxed <paramref name="value"/> instance and cast it to the box.
    /// </summary>
    /// <param name="value">The value to box into the box.</param>
    /// <returns>The created boxed value, casted to the box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Box<T>(T value)
        => Box.DangerousAsBox<T>(value);

    /// <summary>
    /// Indicates whether the current boxed value is equal to another boxed value.
    /// </summary>
    /// <param name="other">A boxed value to compare with this boxed value.</param>
    /// <returns><see langword="true"/> if the current boxed value is equal to the other boxed value OR both boxes are
    /// <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Box<T>? other)
        => other is not null && EqualityComparer<T>.Default.Equals(this, other);

    /// <summary>
    /// Indicates whether the one boxed value is equal to another boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with <paramref name="left"/> boxed value.</param>
    /// <returns><see langword="true"/> if the current boxed value is equal to the other boxed value OR both boxes are
    /// <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Box<T>? left, Box<T>? right)
        => left is null ? right is null : left.Equals(right);

    /// <summary>
    /// Indicates whether the one boxed value is not equal to another boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with <paramref name="left"/> boxed value.</param>
    /// <returns>
    /// <see langword="true"/> if the current boxed value is not equal to the other boxed value OR not both
    /// boxes are <see langword="null"/>; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Box<T>? left, Box<T>? right)
        => !(left == right);

    /// <summary>
    /// Compares this boxed value with another boxed value and returns an integer that indicates whether the this boxed
    /// value precedes, follows, or occurs in the same position in the sort order as the other boxed value.
    /// </summary>
    /// <param name="other">A boxed value to compare with this boxed value.</param>
    /// <returns>
    /// A value that indicates the relative order of the boxed values being compared. The return value has these
    /// meanings:
    /// <list type="table">
    ///     <listheader>
    ///         <term>Return Value</term>
    ///         <description>Meaning</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Less than zero</term>
    ///         <description>This boxed value precedes <paramref name="other"/> in the sort order.</description>
    ///     </item>
    ///     <item>
    ///         <term>Zero</term>
    ///         <description> This boxed value occurs in the same position in the sort order as <paramref name="other"/>.</description>
    ///     </item>
    ///     <item>
    ///         <term> Greater than zero</term>
    ///         <description>This boxed value follows <paramref name="other"/> in the sort order.</description>
    ///     </item>
    /// </list>
    /// </returns>
    public int CompareTo(Box<T>? other)
        => other is null ? 1 : Comparer<T>.Default.Compare(this, other);

    /// <summary>
    /// Indicates whether the <paramref name="left"/> boxed value is less then the <paramref name="right"/> boxed
    /// value.
    /// </summary>
    /// <param name="left">A boxed value to compare with the <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with the <paramref name="left"/> boxed value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> boxed value is less than the <paramref name="right"/>
    /// boxed value; <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Box<T>? left, Box<T>? right)
        => left is null ? right is not null : left.CompareTo(right) < 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> boxed value is greater then the <paramref name="right"/> boxed
    /// value.
    /// </summary>
    /// <param name="left">A boxed value to compare with the <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with the <paramref name="left"/> boxed value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> boxed value is greater than the <paramref name="right"/>
    /// boxed value; <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Box<T>? left, Box<T>? right)
        => left is not null && left.CompareTo(right) > 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> boxed value is less then or equal to the <paramref name="right"/>
    /// boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with the the <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with the the <paramref name="left"/> boxed value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> boxed value is less than or equal to the
    /// <paramref name="right"/> boxed value; <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Box<T>? left, Box<T>? right)
        => left is null || left.CompareTo(right) <= 0;

    /// <summary>
    /// Indicates whether the <paramref name="left"/> boxed value is greater then or equal to the
    /// <paramref name="right"/> boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with the <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with the <paramref name="left"/> boxed value.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> boxed value is greater than or equal to the 
    /// <paramref name="right"/> boxed value; <see langword="false"/> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Box<T>? left, Box<T>? right)
        => left is null ? right is null : left.CompareTo(right) >= 0;
}
