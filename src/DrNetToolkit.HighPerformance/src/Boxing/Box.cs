// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DrNetToolkit.HighPerformance.Protected.Boxing;

namespace DrNetToolkit.HighPerformance.Boxing;

/// <inheritdoc/>
public static class Box
{
    /// <summary>Creates a new boxed <paramref name="value"/> instance and cast it to the box.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <param name="value">The value to box into the box.</param>
    /// <returns>The created boxed value, casted to the box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Box<T> ToBox<T>(this T value)
        where T : struct
        => DangerousAsBox<T>(value);

    /// <summary>Casts the given boxed value to the box.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>The original boxed value, casted to the box.</returns>
    /// <exception cref="InvalidCastException">
    /// Thrown when type of the given <paramref name="obj"/> is not <typeparamref name="T"/> and <paramref name="obj"/>
    /// can not be casted to the box.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(obj))]
    public static Box<T> AsBox<T>(this object obj)
        where T : struct
        => BoxBase.AsBox<T, Box<T>>(obj);

    /// <summary>Try casting the given boxed value to the box.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>
    /// The original boxed value, casted to the box OR <see langword="null"/> when type of the given
    /// <paramref name="obj"/> is not <typeparamref name="T"/> and <paramref name="obj"/> can not be casted to the box.
    /// </returns>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Box<T>? TryAsBox<T>(this object obj)
        where T : struct
        => BoxBase.TryAsBox<T, Box<T>>(obj);

    /// <summary>Casts the given boxed value to the box.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>The original boxed value, casted to the box.</returns>
    /// <remarks>
    /// This method doesn't check the actual type of <paramref name="obj"/>, so it is responsibility of the caller
    /// to ensure it actually represents a boxed <typeparamref name="T"/> value and not some other instance.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(obj))]
    public static Box<T>? DangerousAsBox<T>(this object? obj)
        where T : struct
        => BoxBase.DangerousAsBox<T, Box<T>>(obj);
}
