// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using DrNetToolkit.Diagnostics;

namespace DrNetToolkit.HighPerformance.Boxing.Base;

/// <summary>
/// A <see langword="class"/> that represents a boxed <typeparamref name="T"/> value on the managed heap. This is a
/// "shadow" type that can be used in place of a non-generic <see cref="object"/> reference to a boxed value type, to
/// make the code more expressive and reduce the chances of errors.
/// 
/// Consider this example:
/// <code>
/// object obj = 42;
///
/// // Manual, error prone unboxing
/// int sum = (int)obj + 1;
/// </code>
/// 
/// In this example, it is not possible to know in advance what type is actually being boxed in a given
/// <see cref="object"/> instance, making the code less robust at build time. The <see cref="BoxBase{T}"/> type can be
/// used as a drop-in replacement in this case, like so:
/// <code>
/// Box&lt;int> box = 42;
///
/// // Build-time validation, automatic unboxing
/// int sum = box.Value + 1;
/// </code>
/// 
/// This type can also be useful when dealing with large custom value types that are also boxed, as it allows to
/// retrieve a mutable reference to the boxing value. This means that a given boxed value can be mutated in-place,
/// instead of having to allocate a new updated boxed instance.
/// </summary>
/// <typeparam name="T">The type of boxed value. Where <typeparamref name="T"/> : <see langword="struct"/>.</typeparam>
/// <remarks>
/// The <see cref="BoxBase{T}"/> class is never actually instantiated. Here we are just boxing the input
/// <typeparamref name="T"/> value, and then reinterpreting that object reference as a <see cref="BoxBase{T}"/>
/// reference. As such, the <see cref="BoxBase{T}"/> class is really only used as an interface to access the contents
/// of a boxed <typeparamref name="T"/> value. This also makes it so that additional methods like
/// <see cref="object.ToString()"/> or <see cref="object.GetHashCode()"/> will automatically be referenced from the
/// method table of the boxed <typeparamref name="T"/> value, meaning that they don't need to manually be implemented
/// in the <see cref="BoxBase{T}"/> type. For instance, boxing a float and calling ToString() on it directly, on its
/// boxed object or on a <see cref="BoxBase{T}"/> instance retrieved from it will produce the same result in all
/// cases.
/// </remarks>
public abstract class BoxBase<T>
    where T : struct
{
    // Boxed value types in the CLR are represented in memory as simple objects that store the method table of the
    // corresponding T value type being boxed, and then the data of the value being boxed:
    // [ sync block || pMethodTable || boxed T value ]
    //                 ^               ^
    //                 |               \-- Unsafe.Unbox<T>(Box<T>)
    //                 \-- Box<T> reference
    // For more info, see: https://mattwarren.org/2017/08/02/A-look-at-the-internals-of-boxing-in-the-CLR/.
    // Note that there might be some padding before the actual data representing the boxed value, which might depend on
    // both the runtime and the exact CPU architecture.
    //
    // This is automatically handled by the unbox !!T instruction in IL, which unboxes a given value type T and returns
    // a reference to its boxed data.

    /// <summary>
    /// Always thrown <see cref="InvalidOperationException"/> when this constructor is called (eg. from reflection).
    /// </summary>
    /// <remarks>
    /// This constructor is never used, it is only declared in order to mark it with the <see langword="protected"/>
    /// visibility modifier and prevent direct use.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Always thrown when this constructor is used (eg. from reflection).
    /// </exception>
    protected BoxBase()
        => ThrowHelper.ThrowThisConstructorShouldNeverBeUsed(typeof(BoxBase<T>));

#pragma warning disable CS0649 // Field 'BoxBase<T>._value' is never assigned to, and will always have its default value
    private readonly T _value; // used for fast unboxing
#pragma warning restore CS0649 // Field 'BoxBase<T>._value' is never assigned to, and will always have its default value

    /// <summary>
    /// Boxed <typeparamref name="T"/> value in this box.
    /// </summary>
    ///
    [DebuggerDisplay("{_value}")]
    public T Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _value;
    }

    /// <summary>Gets a readonly reference to the boxed <typeparamref name="T"/> value from this box.</summary>
    /// <returns>A readonly reference to the boxed <typeparamref name="T"/> value from this box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref readonly T GetReference() => ref Unsafe.AsRef(in _value);

    /// <summary>Gets a reference to the boxed <typeparamref name="T"/> value from this box.</summary>
    /// <returns>A reference to the boxed <typeparamref name="T"/> value from this box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T DangerousGetReference() => ref Unsafe.AsRef(in _value);

    /// <summary>
    /// Implicitly gets the <typeparamref name="T"/> value from a given <see cref="BoxBase{T}"/> boxed instance.
    /// </summary>
    /// <param name="box">The <see cref="BoxBase{T}"/> boxed instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator T(BoxBase<T> box) => box._value;

    /// <summary>
    /// Implicitly gets the <typeparamref name="T"/>? value from a given <see cref="BoxBase{T}"/>? boxed instance.
    /// </summary>
    /// <param name="box">The <see cref="BoxBase{T}"/>? boxed value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(box))]
    public static implicit operator T?(BoxBase<T>? box) => box?._value;
}
