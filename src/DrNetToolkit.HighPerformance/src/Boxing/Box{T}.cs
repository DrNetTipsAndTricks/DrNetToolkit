// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace CommunityToolkit.HighPerformance;

using System.Runtime.CompilerServices;

/// <inheritdoc/>
public sealed class Box<T> : BoxBase<T> where T : struct
{
    /// <inheritdoc/>
    private Box() : base() { }

    /// <summary>Casts the given boxed <typeparamref name="T"/> value to the box.</summary>
    /// <param name="obj">The boxed <typeparamref name="T"/> value to cast.</param>
    /// <returns>The original boxed <typeparamref name="T"/> value, casted to the box.</returns>
    /// <exception cref="InvalidCastException">
    /// Thrown when a cast from an invalid <see cref="object"/> is attempted.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Box<T> CastFrom(object obj) => CastFrom<Box<T>>(obj);

    /// <summary>Try casting the given boxed <typeparamref name="T"/> value to the box.</summary>
    /// <param name="obj">The boxed <typeparamref name="T"/> value to cast.</param>
    /// <returns>
    /// <see langword="null"/> if given object is not boxed <typeparamref name="T"/> value, otherwise the original
    /// boxed <typeparamref name="T"/> value, casted to the box.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Box<T>? TryCastFrom(object obj) => TryCastFrom<Box<T>>(obj);

    /// <summary>Casts the given boxed <typeparamref name="T"/> value to the box.</summary>
    /// <param name="obj">The boxed <typeparamref name="T"/> value to cast.</param>
    /// <returns>
    /// The original boxed <typeparamref name="T"/> value, casted to the box.
    /// </returns>
    /// <remarks>
    /// This method doesn't check the actual type of <paramref name="obj"/>, so it is responsibility of the caller
    /// to ensure it actually represents a boxed <typeparamref name="T"/> value and not some other instance.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Box<T> DangerousCastFrom(object obj) => DangerousCastFrom<Box<T>>(obj);

    /// <summary>
    /// Implicitly creates a new <see cref="BoxBase{T}"/> instance from a given <typeparamref name="T"/> value.
    /// </summary>
    /// <param name="value">The input <typeparamref name="T"/> value to wrap.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Box<T>(T value) => DangerousCastFrom<Box<T>>(value);
}
