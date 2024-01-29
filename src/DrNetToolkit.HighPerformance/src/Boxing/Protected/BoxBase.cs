// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Boxing.Protected;

public static class BoxBase
{

    /// <summary>Casts the given boxed value to the box of specified type.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <typeparam name="TBox">
    /// The type of the box to which the boxed value will be cast to. Where <typeparamref name="TBox"/> : 
    /// <see cref="BoxBase<T>"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>The original boxed value, casted to the box of specified type.</returns>
    /// <exception cref="InvalidCastException">
    /// Thrown when type of the given <paramref name="obj"/> is not <typeparamref name="T"/> and <paramref name="obj"/>
    /// can not be casted to the box of specified type.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(obj))]
    public static TBox AsBox<T, TBox>(object obj)
        where T : struct
        where TBox : BoxBase<T>
    {
        if (obj.GetType() != typeof(T))
            ThrowHelper.ThrowInvalidCastException(obj.GetType(), typeof(T));

        return Unsafe.As<TBox>(obj);
    }

    /// <summary>Try casting the given boxed value to the box of specified type.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <typeparam name="TBox">
    /// The type of the box to which the boxed value will be cast to. Where <typeparamref name="TBox"/> : 
    /// <see cref="BoxBase<T>"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>
    /// The original boxed value, casted to the box of specified type OR <see langword="null"/> when type of the given
    /// <paramref name="obj"/> is not <typeparamref name="T"/> and <paramref name="obj"/> can not be casted to the box
    /// of specified type.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TBox? TryAsBox<T, TBox>(object obj)
        where T : struct
        where TBox : BoxBase<T>
        => obj.GetType() == typeof(T) ? Unsafe.As<TBox>(obj) : null;

    /// <summary>Casts the given boxed value to the box of specified type.</summary>
    /// <typeparam name="T">
    /// The type of the given boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.
    /// </typeparam>
    /// <typeparam name="TBox">
    /// The type of the box to which the boxed value will be cast to. Where <typeparamref name="TBox"/> : 
    /// <see cref="BoxBase<T>"/>.
    /// </typeparam>
    /// <param name="obj">The boxed value to cast.</param>
    /// <returns>The original boxed value, casted to the box of specified type.</returns>
    /// <remarks>
    /// This method doesn't check the actual type of <paramref name="obj"/>, so it is responsibility of the caller
    /// to ensure it actually represents a boxed <typeparamref name="T"/> value and not some other instance.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(obj))]
    public static TBox? DangerousAsBox<T, TBox>(object? obj)
        where T : struct
        where TBox : BoxBase<T>
        => Unsafe.As<TBox>(obj);
}
