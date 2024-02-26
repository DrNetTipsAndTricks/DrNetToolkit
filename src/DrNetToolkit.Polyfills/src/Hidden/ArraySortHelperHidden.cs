// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

#if NETSTANDARD1_1_OR_GREATER

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DrNetToolkit.Polyfills.Internals;

namespace DrNetToolkit.Polyfills.Hidden;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE0180 // Use tuple to swap values

#region ArraySortHelper for single arrays

public static partial class ArraySortHelperHidden<T>
{
    public static void Sort(Span<T> keys, Comparison<T> comparer)
    {
        Debug.Assert(comparer != null, "Check the arguments in the caller!");

        // Add a try block here to detect bogus comparisons
        try
        {
            IntrospectiveSort(keys, comparer!);
        }
        catch (IndexOutOfRangeException)
        {
            ThrowHelper.ThrowArgumentException_BadComparer(comparer);
        }
        catch (Exception e)
        {
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
        }
    }

    public static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
    {
        Debug.Assert(array != null, "Check the arguments in the caller!");
        Debug.Assert(index >= 0 && length >= 0 && (array!.Length - index >= length), "Check the arguments in the caller!");

        int lo = index;
        int hi = index + length - 1;
        while (lo <= hi)
        {
            int i = lo + ((hi - lo) >> 1);
            int order = comparer.Compare(array![i], value);

            if (order == 0) return i;
            if (order < 0)
            {
                lo = i + 1;
            }
            else
            {
                hi = i - 1;
            }
        }

        return ~lo;
    }

    private static void SwapIfGreater(Span<T> keys, Comparison<T> comparer, int i, int j)
    {
        Debug.Assert(i != j);

        if (comparer(keys[i], keys[j]) > 0)
        {
            T key = keys[i];
            keys[i] = keys[j];
            keys[j] = key;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap(Span<T> a, int i, int j)
    {
        Debug.Assert(i != j);

        T t = a[i];
        a[i] = a[j];
        a[j] = t;
    }

    private static void IntrospectiveSort(Span<T> keys, Comparison<T> comparer)
    {
        Debug.Assert(comparer != null);

        if (keys.Length > 1)
        {
            IntroSort(keys, 2 * (BitOperationsPolyfills.Log2((uint)keys.Length) + 1), comparer!);
        }
    }

    // IntroSort is recursive; block it from being inlined into itself as
    // this is currently not profitable.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void IntroSort(Span<T> keys, int depthLimit, Comparison<T> comparer)
    {
        Debug.Assert(!keys.IsEmpty);
        Debug.Assert(depthLimit >= 0);
        Debug.Assert(comparer != null);

        int partitionSize = keys.Length;
        while (partitionSize > 1)
        {
            if (partitionSize <= ArrayHidden.IntrosortSizeThreshold)
            {

                if (partitionSize == 2)
                {
                    SwapIfGreater(keys, comparer!, 0, 1);
                    return;
                }

                if (partitionSize == 3)
                {
                    SwapIfGreater(keys, comparer!, 0, 1);
                    SwapIfGreater(keys, comparer!, 0, 2);
                    SwapIfGreater(keys, comparer!, 1, 2);
                    return;
                }

                InsertionSort(keys.Slice(0, partitionSize), comparer!);
                return;
            }

            if (depthLimit == 0)
            {
                HeapSort(keys.Slice(0, partitionSize), comparer!);
                return;
            }
            depthLimit--;

            int p = PickPivotAndPartition(keys.Slice(0, partitionSize), comparer!);

            // Note we've already partitioned around the pivot and do not have to move the pivot again.
            IntroSort(keys[(p + 1)..partitionSize], depthLimit, comparer!);
            partitionSize = p;
        }
    }

    private static int PickPivotAndPartition(Span<T> keys, Comparison<T> comparer)
    {
        Debug.Assert(keys.Length >= ArrayHidden.IntrosortSizeThreshold);
        Debug.Assert(comparer != null);

        int hi = keys.Length - 1;

        // Compute median-of-three.  But also partition them, since we've done the comparison.
        int middle = hi >> 1;

        // Sort lo, mid and hi appropriately, then pick mid as the pivot.
        SwapIfGreater(keys, comparer!, 0, middle);  // swap the low with the mid point
        SwapIfGreater(keys, comparer!, 0, hi);   // swap the low with the high
        SwapIfGreater(keys, comparer!, middle, hi); // swap the middle with the high

        T pivot = keys[middle];
        Swap(keys, middle, hi - 1);
        int left = 0, right = hi - 1;  // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.

        while (left < right)
        {
            while (comparer!(keys[++left], pivot) < 0) ;
            while (comparer(pivot, keys[--right]) < 0) ;

            if (left >= right)
                break;

            Swap(keys, left, right);
        }

        // Put pivot in the right location.
        if (left != hi - 1)
        {
            Swap(keys, left, hi - 1);
        }
        return left;
    }

    private static void HeapSort(Span<T> keys, Comparison<T> comparer)
    {
        Debug.Assert(comparer != null);
        Debug.Assert(!keys.IsEmpty);

        int n = keys.Length;
        for (int i = n >> 1; i >= 1; i--)
        {
            DownHeap(keys, i, n, comparer!);
        }

        for (int i = n; i > 1; i--)
        {
            Swap(keys, 0, i - 1);
            DownHeap(keys, 1, i - 1, comparer!);
        }
    }

    private static void DownHeap(Span<T> keys, int i, int n, Comparison<T> comparer)
    {
        Debug.Assert(comparer != null);

        T d = keys[i - 1];
        while (i <= n >> 1)
        {
            int child = 2 * i;
            if (child < n && comparer!(keys[child - 1], keys[child]) < 0)
            {
                child++;
            }

            if (!(comparer!(d, keys[child - 1]) < 0))
                break;

            keys[i - 1] = keys[child - 1];
            i = child;
        }

        keys[i - 1] = d;
    }

    private static void InsertionSort(Span<T> keys, Comparison<T> comparer)
    {
        for (int i = 0; i < keys.Length - 1; i++)
        {
            T t = keys[i + 1];

            int j = i;
            while (j >= 0 && comparer(t, keys[j]) < 0)
            {
                keys[j + 1] = keys[j];
                j--;
            }

            keys[j + 1] = t;
        }
    }

}

#endregion

#region ArraySortHelper for paired key and value arrays

public static partial class ArraySortHelperHidden<TKey, TValue>
{
    public static void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey>? comparer)
    {
        // Add a try block here to detect IComparers (or their
        // underlying IComparable, etc) that are bogus.
        try
        {
            IntrospectiveSort(keys, values, comparer ?? Comparer<TKey>.Default);
        }
        catch (IndexOutOfRangeException)
        {
            ThrowHelper.ThrowArgumentException_BadComparer(comparer);
        }
        catch (Exception e)
        {
            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
        }
    }

    private static void SwapIfGreaterWithValues(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer, int i, int j)
    {
        Debug.Assert(comparer != null);
        Debug.Assert(0 <= i && i < keys.Length && i < values.Length);
        Debug.Assert(0 <= j && j < keys.Length && j < values.Length);
        Debug.Assert(i != j);

        if (comparer!.Compare(keys[i], keys[j]) > 0)
        {
            TKey key = keys[i];
            keys[i] = keys[j];
            keys[j] = key;

            TValue value = values[i];
            values[i] = values[j];
            values[j] = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap(Span<TKey> keys, Span<TValue> values, int i, int j)
    {
        Debug.Assert(i != j);

        TKey k = keys[i];
        keys[i] = keys[j];
        keys[j] = k;

        TValue v = values[i];
        values[i] = values[j];
        values[j] = v;
    }

    internal static void IntrospectiveSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
    {
        Debug.Assert(comparer != null);
        Debug.Assert(keys.Length == values.Length);

        if (keys.Length > 1)
        {
            IntroSort(keys, values, 2 * (BitOperationsPolyfills.Log2((uint)keys.Length) + 1), comparer!);
        }
    }

    private static void IntroSort(Span<TKey> keys, Span<TValue> values, int depthLimit, IComparer<TKey> comparer)
    {
        Debug.Assert(!keys.IsEmpty);
        Debug.Assert(values.Length == keys.Length);
        Debug.Assert(depthLimit >= 0);
        Debug.Assert(comparer != null);

        int partitionSize = keys.Length;
        while (partitionSize > 1)
        {
            if (partitionSize <= ArrayHidden.IntrosortSizeThreshold)
            {

                if (partitionSize == 2)
                {
                    SwapIfGreaterWithValues(keys, values, comparer!, 0, 1);
                    return;
                }

                if (partitionSize == 3)
                {
                    SwapIfGreaterWithValues(keys, values, comparer!, 0, 1);
                    SwapIfGreaterWithValues(keys, values, comparer!, 0, 2);
                    SwapIfGreaterWithValues(keys, values, comparer!, 1, 2);
                    return;
                }

                InsertionSort(keys.Slice(0, partitionSize), values.Slice(0, partitionSize), comparer!);
                return;
            }

            if (depthLimit == 0)
            {
                HeapSort(keys.Slice(0, partitionSize), values.Slice(0, partitionSize), comparer!);
                return;
            }
            depthLimit--;

            int p = PickPivotAndPartition(keys.Slice(0, partitionSize), values.Slice(0, partitionSize), comparer!);

            // Note we've already partitioned around the pivot and do not have to move the pivot again.
            IntroSort(keys[(p + 1)..partitionSize], values[(p + 1)..partitionSize], depthLimit, comparer!);
            partitionSize = p;
        }
    }

    private static int PickPivotAndPartition(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
    {
        Debug.Assert(keys.Length >= ArrayHidden.IntrosortSizeThreshold);
        Debug.Assert(comparer != null);

        int hi = keys.Length - 1;

        // Compute median-of-three.  But also partition them, since we've done the comparison.
        int middle = hi >> 1;

        // Sort lo, mid and hi appropriately, then pick mid as the pivot.
        SwapIfGreaterWithValues(keys, values, comparer!, 0, middle);  // swap the low with the mid point
        SwapIfGreaterWithValues(keys, values, comparer!, 0, hi);   // swap the low with the high
        SwapIfGreaterWithValues(keys, values, comparer!, middle, hi); // swap the middle with the high

        TKey pivot = keys[middle];
        Swap(keys, values, middle, hi - 1);
        int left = 0, right = hi - 1;  // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.

        while (left < right)
        {
            while (comparer!.Compare(keys[++left], pivot) < 0) ;
            while (comparer.Compare(pivot, keys[--right]) < 0) ;

            if (left >= right)
                break;

            Swap(keys, values, left, right);
        }

        // Put pivot in the right location.
        if (left != hi - 1)
        {
            Swap(keys, values, left, hi - 1);
        }
        return left;
    }

    private static void HeapSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
    {
        Debug.Assert(comparer != null);
        Debug.Assert(!keys.IsEmpty);

        int n = keys.Length;
        for (int i = n >> 1; i >= 1; i--)
        {
            DownHeap(keys, values, i, n, comparer!);
        }

        for (int i = n; i > 1; i--)
        {
            Swap(keys, values, 0, i - 1);
            DownHeap(keys, values, 1, i - 1, comparer!);
        }
    }

    private static void DownHeap(Span<TKey> keys, Span<TValue> values, int i, int n, IComparer<TKey> comparer)
    {
        Debug.Assert(comparer != null);

        TKey d = keys[i - 1];
        TValue dValue = values[i - 1];

        while (i <= n >> 1)
        {
            int child = 2 * i;
            if (child < n && comparer!.Compare(keys[child - 1], keys[child]) < 0)
            {
                child++;
            }

            if (!(comparer!.Compare(d, keys[child - 1]) < 0))
                break;

            keys[i - 1] = keys[child - 1];
            values[i - 1] = values[child - 1];
            i = child;
        }

        keys[i - 1] = d;
        values[i - 1] = dValue;
    }

    private static void InsertionSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
    {
        Debug.Assert(comparer != null);

        for (int i = 0; i < keys.Length - 1; i++)
        {
            TKey t = keys[i + 1];
            TValue tValue = values[i + 1];

            int j = i;
            while (j >= 0 && comparer!.Compare(t, keys[j]) < 0)
            {
                keys[j + 1] = keys[j];
                values[j + 1] = values[j];
                j--;
            }

            keys[j + 1] = t;
            values[j + 1] = tValue;
        }
    }
}

#endregion

/// <summary>Helper methods for use in array/span sorting routines.</summary>
public static class SortUtilities
{
    public static int MoveNansToFront<TKey, TValue>(Span<TKey> keys, Span<TValue> values) where TKey : notnull
    {
        Debug.Assert(typeof(TKey) == typeof(double) || typeof(TKey) == typeof(float));

        int left = 0;

        for (int i = 0; i < keys.Length; i++)
        {
            if ((typeof(TKey) == typeof(double) && double.IsNaN((double)(object)keys[i])) ||
                (typeof(TKey) == typeof(float) && float.IsNaN((float)(object)keys[i])) ||
#if NET5_0_OR_GREATER
                (typeof(TKey) == typeof(Half) && Half.IsNaN((Half)(object)keys[i])) ||
#endif
                false
                )
            {
                TKey temp = keys[left];
                keys[left] = keys[i];
                keys[i] = temp;

                if ((uint)i < (uint)values.Length) // check to see if we have values
                {
                    TValue tempValue = values[left];
                    values[left] = values[i];
                    values[i] = tempValue;
                }

                left++;
            }
        }

        return left;
    }
}

#pragma warning restore IDE0180 // Use tuple to swap values
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#endif
