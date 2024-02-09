// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{
    /// <summary>
    /// Calls <see cref="Func{TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="value">Value to discard.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T, TResult>(this T value, Func<TResult> function)
        => function();

    /// <summary>
    /// Calls <see cref="Func{T, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="value">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TResult Fluent<T, TResult>(this T value, Func<T, TResult> function)
        => function(value);
    
    /// <summary>
    /// Calls <see cref="Func{T1, T2, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="v12">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/> parameter.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="v12">Value for <paramref name="function"/>.</param>
    /// <param name="v13">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/> parameter.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/> parameter.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="v12">Value for <paramref name="function"/>.</param>
    /// <param name="v13">Value for <paramref name="function"/>.</param>
    /// <param name="v14">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/> parameter.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/> parameter.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/> parameter.</typeparam>
    /// <typeparam name="T15">Type of <paramref name="v15"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="v12">Value for <paramref name="function"/>.</param>
    /// <param name="v13">Value for <paramref name="function"/>.</param>
    /// <param name="v14">Value for <paramref name="function"/>.</param>
    /// <param name="v15">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
    /// Calls <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult}"/> in fluent code.
    /// </summary>
    /// <typeparam name="T1">Type of <paramref name="v1"/> parameter.</typeparam>
    /// <typeparam name="T2">Type of <paramref name="v2"/> parameter.</typeparam>
    /// <typeparam name="T3">Type of <paramref name="v3"/> parameter.</typeparam>
    /// <typeparam name="T4">Type of <paramref name="v4"/> parameter.</typeparam>
    /// <typeparam name="T5">Type of <paramref name="v5"/> parameter.</typeparam>
    /// <typeparam name="T6">Type of <paramref name="v6"/> parameter.</typeparam>
    /// <typeparam name="T7">Type of <paramref name="v7"/> parameter.</typeparam>
    /// <typeparam name="T8">Type of <paramref name="v8"/> parameter.</typeparam>
    /// <typeparam name="T9">Type of <paramref name="v9"/> parameter.</typeparam>
    /// <typeparam name="T10">Type of <paramref name="v10"/> parameter.</typeparam>
    /// <typeparam name="T11">Type of <paramref name="v11"/> parameter.</typeparam>
    /// <typeparam name="T12">Type of <paramref name="v12"/> parameter.</typeparam>
    /// <typeparam name="T13">Type of <paramref name="v13"/> parameter.</typeparam>
    /// <typeparam name="T14">Type of <paramref name="v14"/> parameter.</typeparam>
    /// <typeparam name="T15">Type of <paramref name="v15"/> parameter.</typeparam>
    /// <typeparam name="T16">Type of <paramref name="v16"/> parameter.</typeparam>
    /// <typeparam name="TResult">Return type of <paramref name="function"/>.</typeparam>
    /// <param name="v1">Value for <paramref name="function"/>.</param>
    /// <param name="v2">Value for <paramref name="function"/>.</param>
    /// <param name="v3">Value for <paramref name="function"/>.</param>
    /// <param name="v4">Value for <paramref name="function"/>.</param>
    /// <param name="v5">Value for <paramref name="function"/>.</param>
    /// <param name="v6">Value for <paramref name="function"/>.</param>
    /// <param name="v7">Value for <paramref name="function"/>.</param>
    /// <param name="v8">Value for <paramref name="function"/>.</param>
    /// <param name="v9">Value for <paramref name="function"/>.</param>
    /// <param name="v10">Value for <paramref name="function"/>.</param>
    /// <param name="v11">Value for <paramref name="function"/>.</param>
    /// <param name="v12">Value for <paramref name="function"/>.</param>
    /// <param name="v13">Value for <paramref name="function"/>.</param>
    /// <param name="v14">Value for <paramref name="function"/>.</param>
    /// <param name="v15">Value for <paramref name="function"/>.</param>
    /// <param name="v16">Value for <paramref name="function"/>.</param>
    /// <param name="function">Function to call.</param>
    /// <returns><paramref name="function"/> result.</returns>
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
