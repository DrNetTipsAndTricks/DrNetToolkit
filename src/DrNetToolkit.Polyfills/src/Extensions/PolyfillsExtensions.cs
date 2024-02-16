// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DrNetToolkit.Polyfills;

///// <summary>
///// Polyfills Extensions methods for Index and Range classes.
///// </summary>
//public static partial class PolyfillsExtensions
//{
//#if !NETSTANDARD2_1_OR_GREATER
//    /// <summary>
//    /// Creates a new span over the portion of the target array.
//    /// </summary>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static Span<T> AsSpan<T>(this T[]? array, Range range)
//    {
//        if (array == null)
//        {
//            Index startIndex = range.Start;
//            Index endIndex = range.End;

//            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
//                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.array);

//            return default;
//        }

//#if NETSTANDARD2_0_OR_GREATER
//        if (!typeof(T).IsValueType && array!.GetType() != typeof(T[]))
//#else
//        if (array!.GetType() != typeof(T[]))
//#endif
//            ThrowHelper.ThrowArrayTypeMismatchException();

//        (int start, int length) = range.GetOffsetAndLength(array.Length);
//        return MemoryHelpers.CreateSpan(ref Unsafe.Add(ref MemoryHelpers.GetArrayDataReference(array), (nint)(uint)start /* force zero-extension */), length);
//    }
//#endif

//    /// <summary>Creates a new <see cref="ReadOnlySpan{Char}"/> over a portion of the target string from a specified position to the end of the string.</summary>
//    /// <param name="text">The target string.</param>
//    /// <param name="startIndex">The index at which to begin this slice.</param>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> is less than 0 or greater than <paramref name="text"/>.Length.</exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ReadOnlySpan<char> AsSpan(this string? text, Index startIndex)
//    {
//        if (text is null)
//        {
//            if (!startIndex.Equals(Index.Start))
//            {
//                ThrowHelper.ThrowArgumentOutOfRangeException(ThrowHelper.ExceptionArgument.startIndex);
//            }

//            return default;
//        }

//        int actualIndex = startIndex.GetOffset(text.Length);
//        if ((uint)actualIndex > (uint)text.Length)
//        {
//            ThrowHelper.ThrowArgumentOutOfRangeException(ThrowHelper.ExceptionArgument.startIndex);
//        }

//#if NETSTANDARD2_1_OR_GREATER
//        return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()), (nint)(uint)actualIndex /* force zero-extension */), text.Length - actualIndex);
//#else
//        return MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()), (nint)(uint)actualIndex /* force zero-extension */), text.Length - actualIndex);
//#endif
//    }

//    /// <summary>Creates a new <see cref="ReadOnlySpan{Char}"/> over a portion of a target string using the range start and end indexes.</summary>
//    /// <param name="text">The target string.</param>
//    /// <param name="range">The range which has start and end indexes to use for slicing the string.</param>
//    /// <exception cref="ArgumentNullException"><paramref name="text"/> is null.</exception>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="range"/>'s start or end index is not within the bounds of the string.</exception>
//    /// <exception cref="ArgumentOutOfRangeException"><paramref name="range"/>'s start index is greater than its end index.</exception>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static ReadOnlySpan<char> AsSpan(this string? text, Range range)
//    {
//        if (text is null)
//        {
//            Index startIndex = range.Start;
//            Index endIndex = range.End;

//            if (!startIndex.Equals(Index.Start) || !endIndex.Equals(Index.Start))
//            {
//                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.text);
//            }

//            return default;
//        }

//        (int start, int length) = range.GetOffsetAndLength(text.Length);
//#if NETSTANDARD2_1_OR_GREATER
//        return MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()), (nint)(uint)start /* force zero-extension */), length);
//#else
//        return MemoryHelpers.CreateReadOnlySpan(ref Unsafe.Add(ref Unsafe.AsRef(in text.GetPinnableReference()), (nint)(uint)start /* force zero-extension */), length);
//#endif
//    }

//    /// <summary>Creates a new <see cref="ReadOnlyMemory{T}"/> over the portion of the target string.</summary>
//    /// <param name="text">The target string.</param>
//    /// <param name="startIndex">The index at which to begin this slice.</param>
//    public static ReadOnlyMemory<char> AsMemory(this string? text, Index startIndex)
//    {
//        if (text == null)
//        {
//            if (!startIndex.Equals(Index.Start))
//                ThrowHelper.ThrowArgumentNullException(ThrowHelper.ExceptionArgument.text);

//            return default;
//        }

//        int actualIndex = startIndex.GetOffset(text.Length);
//        if ((uint)actualIndex > (uint)text.Length)
//            ThrowHelper.ThrowArgumentOutOfRangeException();

//        return new ReadOnlyMemory<char>(text, actualIndex, text.Length - actualIndex);
//    }

//#endif
//}
