// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.HighPerformance.Boxing;

using DrNetToolkit.HighPerformance.Internal.Boxing;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()

/// <inheritdoc/>
public sealed class Box<T> : BoxBase<T>, IEquatable<Box<T>>
    where T : struct
{
    /// <inheritdoc/>
    private Box() : base() { }

    /// <summary>
    /// Indicates whether the current boxed value is equal to another boxed value.
    /// </summary>
    /// <param name="other">A boxed value to compare with this boxed value.</param>
    /// <returns><see langword="true"/> if the current boxed value is equal to the other boxed value OR both boxes are
    /// <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Box<T>? other)
        => other is not null && EqualityComparer<T>.Default.Equals(this, other);

    /// <summary>
    /// Implicitly creates a new boxed <paramref name="value"/> instance and cast it to the box.
    /// </summary>
    /// <param name="value">The value to box into the box.</param>
    /// <returns>The created boxed value, casted to the box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Box<T>(T value)
        => Box.DangerousAsBox<T>(value);

    /// <summary>
    /// Indicates whether the one boxed value is equal to another boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with <paramref name="left"/> boxed value.</param>
    /// <returns><see langword="true"/> if the current boxed value is equal to the other boxed value OR both boxes are
    /// <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Box<T> left, Box<T> right)
        => left.Equals(right);

    /// <summary>
    /// Indicates whether the one boxed value is not equal to another boxed value.
    /// </summary>
    /// <param name="left">A boxed value to compare with <paramref name="right"/> boxed value.</param>
    /// <param name="right">A boxed value to compare with <paramref name="left"/> boxed value.</param>
    /// <returns><see langword="true"/> if the current boxed value is not equal to the other boxed value OR not both
    /// boxes are <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Box<T> left, Box<T> right)
        => !(left == right);
}
