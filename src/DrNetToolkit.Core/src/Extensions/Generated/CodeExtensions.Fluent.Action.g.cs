// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Coding;

public static partial class CodeExtensions
{
    /// <summary>
    /// Calls <see cref="Action"/> in fluent code.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/>.</typeparam>
    /// <param name="value">Value to return.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Fluent<T>(this T value, Action action)
    {
        action();
        return value;
    }

    /// <summary>
    /// Calls <see cref="Action{T}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/>.</typeparam>
    /// <param name="value">Value for <paramref name="action"/> and return.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="value"/>.</returns>    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Fluent<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }


    /// <summary>
    /// Calls <see cref="Action{T1, T2}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2>
    (
        this T1 v1, T2 v2,
        Action<T1, T2> action
    )
    {
        action(v1, v2);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3>
    (
        this T1 v1, T2 v2, T3 v3,
        Action<T1, T2, T3> action
    )
    {
        action(v1, v2, v3);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4,
        Action<T1, T2, T3, T4> action
    )
    {
        action(v1, v2, v3, v4);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5,
        Action<T1, T2, T3, T4, T5> action
    )
    {
        action(v1, v2, v3, v4, v5);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6,
        Action<T1, T2, T3, T4, T5, T6> action
    )
    {
        action(v1, v2, v3, v4, v5, v6);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7,
        Action<T1, T2, T3, T4, T5, T6, T7> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8,
        Action<T1, T2, T3, T4, T5, T6, T7, T8> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="v12">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/>.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="v12">Value for <paramref name="action"/>.</param>
    /// <param name="v13">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/>.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/>.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="v12">Value for <paramref name="action"/>.</param>
    /// <param name="v13">Value for <paramref name="action"/>.</param>
    /// <param name="v14">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/>.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/>.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/>.</typeparam>
    /// <typeparam name="T15">Type of <paramref name="v15"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="v12">Value for <paramref name="action"/>.</param>
    /// <param name="v13">Value for <paramref name="action"/>.</param>
    /// <param name="v14">Value for <paramref name="action"/>.</param>
    /// <param name="v15">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
        return v1;
    }

    /// <summary>
    /// Calls <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/>.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/>.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/>.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/>.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/>.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/>.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/>.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/>.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/>.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/>.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/>.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/>.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/>.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/>.</typeparam>
    /// <typeparam name="T15">Type of <paramref name="v15"/>.</typeparam>
    /// <typeparam name="T16">Type of <paramref name="v16"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="action"/> and return.</param>
    /// <param name="v2">Value for <paramref name="action"/>.</param>
    /// <param name="v3">Value for <paramref name="action"/>.</param>
    /// <param name="v4">Value for <paramref name="action"/>.</param>
    /// <param name="v5">Value for <paramref name="action"/>.</param>
    /// <param name="v6">Value for <paramref name="action"/>.</param>
    /// <param name="v7">Value for <paramref name="action"/>.</param>
    /// <param name="v8">Value for <paramref name="action"/>.</param>
    /// <param name="v9">Value for <paramref name="action"/>.</param>
    /// <param name="v10">Value for <paramref name="action"/>.</param>
    /// <param name="v11">Value for <paramref name="action"/>.</param>
    /// <param name="v12">Value for <paramref name="action"/>.</param>
    /// <param name="v13">Value for <paramref name="action"/>.</param>
    /// <param name="v14">Value for <paramref name="action"/>.</param>
    /// <param name="v15">Value for <paramref name="action"/>.</param>
    /// <param name="v16">Value for <paramref name="action"/>.</param>
    /// <param name="action">Action to call.</param>
    /// <returns><paramref name="v1"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, T16 v16,
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action
    )
    {
        action(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
        return v1;
    }
}
