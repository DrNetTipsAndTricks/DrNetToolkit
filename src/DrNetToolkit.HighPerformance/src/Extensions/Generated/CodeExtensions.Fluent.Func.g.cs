// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{
    /// <summary>
    /// Fluent styled function to call <see cref="Func{TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="value"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="value">The value that will be discarded.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T, TResult>(this T value, Func<TResult> function)
        => function();

    /// <summary>
    /// Fluent styled function to call <see cref="Func{T, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="value">The value that will be passed to <paramref name="function"/>.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T, TResult>(this T value, Func<T, TResult> function)
        => function(value);
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, TResult>
    (
        this T1 v1, T2 v2,
        Func<T1, T2, TResult> function
    )
    {
        return function(v1, v2);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, TResult>
    (
        this T1 v1, T2 v2, T3 v3,
        Func<T1, T2, T3, TResult> function
    )
    {
        return function(v1, v2, v3);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4,
        Func<T1, T2, T3, T4, TResult> function
    )
    {
        return function(v1, v2, v3, v4);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5,
        Func<T1, T2, T3, T4, T5, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6,
        Func<T1, T2, T3, T4, T5, T6, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7,
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> in fluent styled code.
    /// </summary>
    /// <typeparam name="T1">The type of <paramref name="v1"/> value.</typeparam>
    /// <typeparam name="T2">The type of <paramref name="v2"/> value.</typeparam>
    /// <typeparam name="T3">The type of <paramref name="v3"/> value.</typeparam>
    /// <typeparam name="T4">The type of <paramref name="v4"/> value.</typeparam>
    /// <typeparam name="T5">The type of <paramref name="v5"/> value.</typeparam>
    /// <typeparam name="T6">The type of <paramref name="v6"/> value.</typeparam>
    /// <typeparam name="T7">The type of <paramref name="v7"/> value.</typeparam>
    /// <typeparam name="T8">The type of <paramref name="v8"/> value.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="function"/> at position 12.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="function"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="function"/> at position 13.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="function"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="function"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="function"/> at position 14.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="function"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="function"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="function"/> at position 14.</param>
    /// <param name="v15">The value that will be passed to <paramref name="function"/> at position 15.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
    }
    
    /// <summary>
    /// Fluent styled function to call <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> in fluent styled code.
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
    /// <typeparam name="TResult">The type of the return value of the <paramref name="function"/>.</typeparam>
    /// <param name="v1">The value that will be passed to <paramref name="function"/> at position 1.</param>
    /// <param name="v2">The value that will be passed to <paramref name="function"/> at position 2.</param>
    /// <param name="v3">The value that will be passed to <paramref name="function"/> at position 3.</param>
    /// <param name="v4">The value that will be passed to <paramref name="function"/> at position 4.</param>
    /// <param name="v5">The value that will be passed to <paramref name="function"/> at position 5.</param>
    /// <param name="v6">The value that will be passed to <paramref name="function"/> at position 6.</param>
    /// <param name="v7">The value that will be passed to <paramref name="function"/> at position 7.</param>
    /// <param name="v8">The value that will be passed to <paramref name="function"/> at position 8.</param>
    /// <param name="v9">The value that will be passed to <paramref name="function"/> at position 9.</param>
    /// <param name="v10">The value that will be passed to <paramref name="function"/> at position 10.</param>
    /// <param name="v11">The value that will be passed to <paramref name="function"/> at position 11.</param>
    /// <param name="v12">The value that will be passed to <paramref name="function"/> at position 12.</param>
    /// <param name="v13">The value that will be passed to <paramref name="function"/> at position 13.</param>
    /// <param name="v14">The value that will be passed to <paramref name="function"/> at position 14.</param>
    /// <param name="v15">The value that will be passed to <paramref name="function"/> at position 15.</param>
    /// <param name="v16">The value that will be passed to <paramref name="function"/> at position 16.</param>
    /// <param name="function">The function that will be called.</param>
    /// <returns>The return value of <paramref name="function"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, T16 v16,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
    }
}
