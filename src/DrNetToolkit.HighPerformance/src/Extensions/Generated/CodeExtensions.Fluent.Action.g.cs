﻿// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.HighPerformance.Coding;

public static partial class CodeExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T1 Fluent<T1>
    (
        this T1 v1,
        Action<T1> action
    )
    {
        action(v1);
        return v1;
    }


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