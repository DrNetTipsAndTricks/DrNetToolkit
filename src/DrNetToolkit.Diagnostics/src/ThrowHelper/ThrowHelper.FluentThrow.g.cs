// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DrNetToolkit.Diagnostics;

public static partial class ThrowHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<TOut>
    (
        Action throwAction
    )
    {
        throwAction();
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, TOut>
    (
        T1 v1, 
        Action<T1> throwAction
    )
    {
        throwAction(v1);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, TOut>
    (
        T1 v1, T2 v2, 
        Action<T1, T2> throwAction
    )
    {
        throwAction(v1, v2);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, TOut>
    (
        T1 v1, T2 v2, T3 v3, 
        Action<T1, T2, T3> throwAction
    )
    {
        throwAction(v1, v2, v3);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, 
        Action<T1, T2, T3, T4> throwAction
    )
    {
        throwAction(v1, v2, v3, v4);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, 
        Action<T1, T2, T3, T4, T5> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, 
        Action<T1, T2, T3, T4, T5, T6> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, 
        Action<T1, T2, T3, T4, T5, T6, T7> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StackTraceHidden]
    [DoesNotReturn]
    public static TOut FluentThrow<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TOut>
    (
        T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8, T9 v9, T10 v10, T11 v11, T12 v12, T13 v13, T14 v14, T15 v15, T16 v16, 
        Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> throwAction
    )
    {
        throwAction(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16);
#if NET7_0_OR_GREATER
        throw new UnreachableException();
#else
        throw new InvalidOperationException();
#endif
    }
}
