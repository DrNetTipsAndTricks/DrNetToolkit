// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

namespace DrNetToolkit.Polyfills.Hidden;

public static partial class SpanHelpersHidden // .T
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

    public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>?
    {
        Debug.Assert(searchSpaceLength >= 0);
        Debug.Assert(valueLength >= 0);

        if (valueLength == 0)
            return 0;  // A zero-length sequence is always treated as "found" at the start of the search space.

        T valueHead = value;
        ref T valueTail = ref Unsafe.Add(ref value, 1);
        int valueTailLength = valueLength - 1;

        int index = 0;
        while (true)
        {
            Debug.Assert(0 <= index && index <= searchSpaceLength); // Ensures no deceptive underflows in the computation of "remainingSearchSpaceLength".
            int remainingSearchSpaceLength = searchSpaceLength - index - valueTailLength;
            if (remainingSearchSpaceLength <= 0)
                break;  // The unsearched portion is now shorter than the sequence we're looking for. So it can't be there.

            // Do a quick search for the first element of "value".
            int relativeIndex = IndexOf(ref Unsafe.Add(ref searchSpace, index), valueHead, remainingSearchSpaceLength);
            if (relativeIndex < 0)
                break;
            index += relativeIndex;

            // Found the first element of "value". See if the tail matches.
            if (SequenceEqual(ref Unsafe.Add(ref searchSpace, index + 1), ref valueTail, valueTailLength))
                return index;  // The tail matched. Return a successful find.

            index++;
        }
        return -1;
    }

    public static unsafe bool Contains<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>?
    {
        Debug.Assert(length >= 0);

        nint index = 0; // Use nint for arithmetic to avoid unnecessary 64->32->64 truncations

        if (default(T) != null || (object?)value != null)
        {
            Debug.Assert(value is not null);

            while (length >= 8)
            {
                length -= 8;

                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 3)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 4)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 5)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 6)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 7)))
                {
                    goto Found;
                }

                index += 8;
            }

            if (length >= 4)
            {
                length -= 4;

                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 0)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 1)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 2)) ||
                    value!.Equals(Unsafe.Add(ref searchSpace, index + 3)))
                {
                    goto Found;
                }

                index += 4;
            }

            while (length > 0)
            {
                length--;

                if (value!.Equals(Unsafe.Add(ref searchSpace, index)))
                    goto Found;

                index += 1;
            }
        }
        else
        {
            nint len = length;
            for (index = 0; index < len; index++)
            {
                if ((object?)Unsafe.Add(ref searchSpace, index) is null)
                {
                    goto Found;
                }
            }
        }

        return false;

    Found:
        return true;
    }

    public static unsafe int IndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>?
    {
        Debug.Assert(length >= 0);

        nint index = 0; // Use nint for arithmetic to avoid unnecessary 64->32->64 truncations
        if (default(T) != null || (object?)value != null)
        {
            Debug.Assert(value is not null);

            while (length >= 8)
            {
                length -= 8;

                if (value!.Equals(Unsafe.Add(ref searchSpace, index)))
                    goto Found;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 1)))
                    goto Found1;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 2)))
                    goto Found2;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 3)))
                    goto Found3;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 4)))
                    goto Found4;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 5)))
                    goto Found5;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 6)))
                    goto Found6;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 7)))
                    goto Found7;

                index += 8;
            }

            if (length >= 4)
            {
                length -= 4;

                if (value!.Equals(Unsafe.Add(ref searchSpace, index)))
                    goto Found;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 1)))
                    goto Found1;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 2)))
                    goto Found2;
                if (value!.Equals(Unsafe.Add(ref searchSpace, index + 3)))
                    goto Found3;

                index += 4;
            }

            while (length > 0)
            {
                if (value!.Equals(Unsafe.Add(ref searchSpace, index)))
                    goto Found;

                index += 1;
                length--;
            }
        }
        else
        {
            nint len = (nint)length;
            for (index = 0; index < len; index++)
            {
                if ((object?)Unsafe.Add(ref searchSpace, index) is null)
                {
                    goto Found;
                }
            }
        }
        return -1;

    Found: // Workaround for https://github.com/dotnet/runtime/issues/8795
        return (int)index;
    Found1:
        return (int)(index + 1);
    Found2:
        return (int)(index + 2);
    Found3:
        return (int)(index + 3);
    Found4:
        return (int)(index + 4);
    Found5:
        return (int)(index + 5);
    Found6:
        return (int)(index + 6);
    Found7:
        return (int)(index + 7);
    }

    public static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : IEquatable<T>?
    {
        Debug.Assert(length >= 0);

        if (Unsafe.AreSame(ref first, ref second))
            goto Equal;

        nint index = 0; // Use nint for arithmetic to avoid unnecessary 64->32->64 truncations
        T lookUp0;
        T lookUp1;
        while (length >= 8)
        {
            length -= 8;

            lookUp0 = Unsafe.Add(ref first, index);
            lookUp1 = Unsafe.Add(ref second, index);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 1);
            lookUp1 = Unsafe.Add(ref second, index + 1);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 2);
            lookUp1 = Unsafe.Add(ref second, index + 2);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 3);
            lookUp1 = Unsafe.Add(ref second, index + 3);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 4);
            lookUp1 = Unsafe.Add(ref second, index + 4);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 5);
            lookUp1 = Unsafe.Add(ref second, index + 5);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 6);
            lookUp1 = Unsafe.Add(ref second, index + 6);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 7);
            lookUp1 = Unsafe.Add(ref second, index + 7);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;

            index += 8;
        }

        if (length >= 4)
        {
            length -= 4;

            lookUp0 = Unsafe.Add(ref first, index);
            lookUp1 = Unsafe.Add(ref second, index);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 1);
            lookUp1 = Unsafe.Add(ref second, index + 1);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 2);
            lookUp1 = Unsafe.Add(ref second, index + 2);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            lookUp0 = Unsafe.Add(ref first, index + 3);
            lookUp1 = Unsafe.Add(ref second, index + 3);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;

            index += 4;
        }

        while (length > 0)
        {
            lookUp0 = Unsafe.Add(ref first, index);
            lookUp1 = Unsafe.Add(ref second, index);
            if (!(lookUp0?.Equals(lookUp1) ?? (object?)lookUp1 is null))
                goto NotEqual;
            index += 1;
            length--;
        }

    Equal:
        return true;

    NotEqual: // Workaround for https://github.com/dotnet/runtime/issues/8795
        return false;
    }

    public static int IndexOfAnyExcept<T>(ref T searchSpace, T value0, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = 0; i < length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(Unsafe.Add(ref searchSpace, i), value0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExcept<T>(ref T searchSpace, T value0, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = length - 1; i >= 0; i--)
        {
            if (!EqualityComparer<T>.Default.Equals(Unsafe.Add(ref searchSpace, i), value0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int IndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = 0; i < length; i++)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0) && !EqualityComparer<T>.Default.Equals(current, value1))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = length - 1; i >= 0; i--)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0) && !EqualityComparer<T>.Default.Equals(current, value1))
            {
                return i;
            }
        }

        return -1;
    }

    public static int IndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, T value2, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = 0; i < length; i++)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0)
                && !EqualityComparer<T>.Default.Equals(current, value1)
                && !EqualityComparer<T>.Default.Equals(current, value2))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, T value2, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = length - 1; i >= 0; i--)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0)
                && !EqualityComparer<T>.Default.Equals(current, value1)
                && !EqualityComparer<T>.Default.Equals(current, value2))
            {
                return i;
            }
        }

        return -1;
    }

    public static int IndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, T value2, T value3, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = 0; i < length; i++)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0)
                && !EqualityComparer<T>.Default.Equals(current, value1)
                && !EqualityComparer<T>.Default.Equals(current, value2)
                && !EqualityComparer<T>.Default.Equals(current, value3))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExcept<T>(ref T searchSpace, T value0, T value1, T value2, T value3, int length)
    {
        Debug.Assert(length >= 0, "Expected non-negative length");

        for (int i = length - 1; i >= 0; i--)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if (!EqualityComparer<T>.Default.Equals(current, value0)
                && !EqualityComparer<T>.Default.Equals(current, value1)
                && !EqualityComparer<T>.Default.Equals(current, value2)
                && !EqualityComparer<T>.Default.Equals(current, value3))
            {
                return i;
            }
        }

        return -1;
    }

    public static int IndexOfAnyInRange<T>(ref T searchSpace, T lowInclusive, T highInclusive, int length)
        where T : IComparable<T>
    {
        for (int i = 0; i < length; i++)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if ((lowInclusive.CompareTo(current) <= 0) && (highInclusive.CompareTo(current) >= 0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int IndexOfAnyExceptInRange<T>(ref T searchSpace, T lowInclusive, T highInclusive, int length)
        where T : IComparable<T>
    {
        for (int i = 0; i < length; i++)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if ((lowInclusive.CompareTo(current) > 0) || (highInclusive.CompareTo(current) < 0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyInRange<T>(ref T searchSpace, T lowInclusive, T highInclusive, int length)
        where T : IComparable<T>
    {
        for (int i = length - 1; i >= 0; i--)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if ((lowInclusive.CompareTo(current) <= 0) && (highInclusive.CompareTo(current) >= 0))
            {
                return i;
            }
        }

        return -1;
    }

    public static int LastIndexOfAnyExceptInRange<T>(ref T searchSpace, T lowInclusive, T highInclusive, int length)
        where T : IComparable<T>
    {
        for (int i = length - 1; i >= 0; i--)
        {
            ref T current = ref Unsafe.Add(ref searchSpace, i);
            if ((lowInclusive.CompareTo(current) > 0) || (highInclusive.CompareTo(current) < 0))
            {
                return i;
            }
        }

        return -1;
    }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
