﻿// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, TOut>
    (
        this T1 v1,
        Func<T1, TOut> function
    )
    {
        return function(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, TOut>
    (
        this T1 v1, T2 v2,
        Func<T1, T2, TOut> function
    )
    {
        return function(v1, v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, TOut>
    (
        this T1 v1, T2 v2, T3 v3,
        Func<T1, T2, T3, TOut> function
    )
    {
        return function(v1, v2, v3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4,
        Func<T1, T2, T3, T4, TOut> function
    )
    {
        return function(v1, v2, v3, v4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5,
        Func<T1, T2, T3, T4, T5, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6,
        Func<T1, T2, T3, T4, T5, T6, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7,
        Func<T1, T2, T3, T4, T5, T6, T7, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TOut Fluent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>
    (
        this T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, T16 v16,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut> function
    )
    {
        return function(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
    }
}