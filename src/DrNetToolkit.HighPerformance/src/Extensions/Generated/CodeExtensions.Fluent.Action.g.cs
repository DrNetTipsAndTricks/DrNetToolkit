// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{
    /// <summary>
    /// Fluent styled function to call <see cref="Action"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
    /// <param name="value">The value that will be returned.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Fluent<T>(this T value, Action action)
    {
        action();
        return value;
    }

    /// <summary>
    /// Fluent styled function to call <see cref="Action{T}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="value"/> value.</typeparam>
    /// <param name="value">The value that will be passed to <paramref name="action"/>.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Fluent<T>(this T value, Action<T> action)
    {
        action(value);
        return value;
    }


    /// <summary>
    /// Fluent styled function to call <see cref="Action{T1, T2}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <typeparam name="T12">The type of <paramref name="v12"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="action"/> at position 12.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <typeparam name="T12">The type of <paramref name="v12"/> value.</typeparam>
    /// <typeparam name="T13">The type of <paramref name="v13"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="action"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="action"/> at position 13.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <typeparam name="T12">The type of <paramref name="v12"/> value.</typeparam>
    /// <typeparam name="T13">The type of <paramref name="v13"/> value.</typeparam>
    /// <typeparam name="T14">The type of <paramref name="v14"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="action"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="action"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="action"/> at position 14.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <typeparam name="T12">The type of <paramref name="v12"/> value.</typeparam>
    /// <typeparam name="T13">The type of <paramref name="v13"/> value.</typeparam>
    /// <typeparam name="T14">The type of <paramref name="v14"/> value.</typeparam>
    /// <typeparam name="T15">The type of <paramref name="v15"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="action"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="action"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="action"/> at position 14.</param>
    /// <param name="v15">The value that will be passed to <paramref name="action"/> at position 15.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
    /// Fluent styled function to call <see cref="Action{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="T9">The type of <paramref name="v9"/> value.</typeparam>
    /// <typeparam name="T10">The type of <paramref name="v10"/> value.</typeparam>
    /// <typeparam name="T11">The type of <paramref name="v11"/> value.</typeparam>
    /// <typeparam name="T12">The type of <paramref name="v12"/> value.</typeparam>
    /// <typeparam name="T13">The type of <paramref name="v13"/> value.</typeparam>
    /// <typeparam name="T14">The type of <paramref name="v14"/> value.</typeparam>
    /// <typeparam name="T15">The type of <paramref name="v15"/> value.</typeparam>
    /// <typeparam name="T16">The type of <paramref name="v16"/> value.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="action"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="action"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="action"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="action"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="action"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="action"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="action"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="action"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="action"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="action"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="action"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="action"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="action"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="action"/> at position 14.</param>
    /// <param name="v15">The value that will be passed to <paramref name="action"/> at position 15.</param>
    /// <param name="v16">The value that will be passed to <paramref name="action"/> at position 16.</param>
    /// <param name="action">The action that will be called.</param>
    /// <returns>A <paramref name="v1"/> value.</returns>
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
