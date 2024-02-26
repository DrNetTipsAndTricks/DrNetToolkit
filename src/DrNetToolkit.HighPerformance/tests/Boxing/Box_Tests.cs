// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Xunit;

using DrNetToolkit.HighPerformance.Boxing;
using DrNetToolkit.HighPerformance.Boxing.Base;

namespace DrNetToolkit.HighPerformance.UnitTests;

public class Box_Tests
{
    [Fact]
    public void Box_Valid()
    {
        Test<bool>(true, false);
        Test<bool>(true, null);
        Test<bool>(null, false);

        Test<byte>(27, 254);
        Test<byte>(27, null);
        Test<byte>(null, 254);

        Test<char>('a', '$');
        Test<char>('a', null);
        Test<char>(null, '$');

        Test<int>(4221124, 1241241);
        Test<int>(4221124, null);
        Test<int>(null, 1241241);

        Test<float>(3.14f, 2342.222f);
        Test<float>(3.14f, null);
        Test<float>(null, 2342.222f);

        Test<ulong>(8394324ul, 1343431241ul);
        Test<ulong>(8394324ul, null);
        Test<ulong>(null, 1343431241ul);

        Test<double>(184013.234324, 14124.23423);
        Test<double>(184013.234324, null);
        Test<double>(null, 14124.23423);

        Test<DateTime>(DateTime.Now, DateTime.FromBinary(278091429014));
        Test<DateTime>(DateTime.Now, null);
        Test<DateTime>(null, DateTime.FromBinary(278091429014));

        Test<Guid>(Guid.NewGuid(), Guid.NewGuid());
        Test<Guid>(Guid.NewGuid(), null);
        Test<Guid>(null, Guid.NewGuid());

        {
            TestStruct a = new() { Number = 42, Character = 'a', Text = "Hello" };
            TestStruct b = new() { Number = 38293, Character = 'z', Text = "World" };

            Test<TestStruct>(a, b);
            Test<TestStruct>(a, null);
            Test<TestStruct>(null, b);
        }
    }

    [Fact]
    public void Box_Invalid()
    {
        long lValue = 0x0123_4567_89AB_CDEF;
        int iValue = Unsafe.As<long, int>(ref lValue);
        object? lObj = lValue;
        object? iObj = iValue;

        _ = Assert.Throws<InvalidCastException>(() => BoxBase.AsBox<int, Box<int>>(lObj));
        _ = Assert.Throws<InvalidCastException>(() => BoxBase.AsBox<long, Box<long>>(iObj));
        Assert.Null(BoxBase.TryAsBox<int, Box<int>>(lObj));
        Assert.Null(BoxBase.TryAsBox<long, Box<long>>(iObj));
        Assert.Same(lObj, BoxBase.DangerousAsBox<int, Box<int>>(lObj));
        Assert.Same(iObj, BoxBase.DangerousAsBox<long, Box<long>>(iObj));

        _ = Assert.Throws<InvalidCastException>(() => BoxBase.AsBox<int, Box<int>>(lValue));
        _ = Assert.Throws<InvalidCastException>(() => BoxBase.AsBox<long, Box<long>>(iValue));
        Assert.Null(BoxBase.TryAsBox<int, Box<int>>(lValue));
        Assert.Null(BoxBase.TryAsBox<long, Box<long>>(iValue));
        Assert.NotNull(BoxBase.DangerousAsBox<int, Box<int>>(lValue));
        Assert.NotNull(BoxBase.DangerousAsBox<long, Box<long>>(iValue));

        _ = Assert.Throws<InvalidCastException>(() => Box.AsBox<int>(lObj));
        _ = Assert.Throws<InvalidCastException>(() => Box.AsBox<long>(iObj));
        Assert.Null(Box.TryAsBox<int>(lObj));
        Assert.Null(Box.TryAsBox<long>(iObj));
        Assert.Same(lObj, Box.DangerousAsBox<int>(lObj));
        Assert.Same(iObj, Box.DangerousAsBox<long>(iObj));

        _ = Assert.Throws<InvalidCastException>(() => Box.AsBox<int>(lValue));
        _ = Assert.Throws<InvalidCastException>(() => Box.AsBox<long>(iValue));
        Assert.Null(Box.TryAsBox<int>(lValue));
        Assert.Null(Box.TryAsBox<long>(iValue));
        Assert.NotNull(Box.DangerousAsBox<int>(lValue));
        Assert.NotNull(Box.DangerousAsBox<long>(iValue));

        // DangerousAsBox
        Box<int> box = Box.DangerousAsBox<int>(lObj);
        object boxObj = box;
        Assert.Same(lObj, box);
        Assert.Same(lObj, boxObj);

        Assert.Equal(iValue, box.GetReference());
        Assert.Equal(iValue, box.DangerousGetReference());
        Assert.Equal(iValue, box.Value);
        Assert.Equal(iValue, (int)box);
        Assert.Equal(iValue, (long)box);
        _ = Assert.Throws<InvalidCastException>(() => (int)boxObj);
        Assert.NotEqual(iValue, (long)boxObj);

        Assert.NotEqual(lValue, box.GetReference());
        Assert.NotEqual(lValue, box.DangerousGetReference());
        Assert.NotEqual(lValue, box.Value);
        Assert.NotEqual(lValue, (int)box);
        Assert.NotEqual(lValue, (long)box);
        Assert.Equal(lValue, (long)boxObj);

        Assert.Equal(iObj, box.GetReference());
        Assert.Equal(iObj, box.DangerousGetReference());
        Assert.Equal(iObj, box.Value);
        Assert.Equal(iObj, (int)box);
        Assert.NotEqual(iObj, (long)box);
        Assert.NotEqual(iObj, (long)boxObj);

        Assert.NotEqual(lObj, box.GetReference());
        Assert.NotEqual(lObj, box.DangerousGetReference());
        Assert.NotEqual(lObj, box.Value);
        Assert.NotEqual(lObj, (int)box);
        Assert.NotEqual(lObj, (long)box);
        Assert.Equal(lObj, (long)boxObj);

        Assert.NotEqual(boxObj, box.GetReference());
        Assert.NotEqual(boxObj, box.DangerousGetReference());
        Assert.NotEqual(boxObj, box.Value);
        Assert.NotEqual(boxObj, (int)box);
        Assert.NotEqual(boxObj, (long)box);
        Assert.Equal(boxObj, (long)boxObj);

        Assert.NotEqual(iValue.ToString(CultureInfo.InvariantCulture), box.ToString());
        Assert.Equal(lValue.ToString(CultureInfo.InvariantCulture), box.ToString());
        Assert.NotEqual(iValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
        Assert.Equal(lValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
        Assert.NotEqual(iObj.ToString(), box.ToString());
        Assert.Equal(lObj.ToString(), box.ToString());
        Assert.NotEqual(iObj.ToString(), boxObj.ToString());
        Assert.Equal(lObj.ToString(), boxObj.ToString());

        Assert.NotEqual(iValue.GetHashCode(), box.GetHashCode());
        Assert.Equal(lValue.GetHashCode(), box.GetHashCode());
        Assert.NotEqual(iValue.GetHashCode(), boxObj.GetHashCode());
        Assert.Equal(lValue.GetHashCode(), boxObj.GetHashCode());
        Assert.NotEqual(iObj.GetHashCode(), box.GetHashCode());
        Assert.Equal(lObj.GetHashCode(), box.GetHashCode());
        Assert.NotEqual(iObj.GetHashCode(), boxObj.GetHashCode());
        Assert.Equal(lObj.GetHashCode(), boxObj.GetHashCode());

        Assert.True(iValue.Equals(box));
        Assert.False(lValue.Equals(box));
        Assert.False(iValue.Equals(boxObj));
        Assert.True(lValue.Equals(boxObj));
        Assert.False(box.Equals((object)iValue));
        Assert.True(box.Equals(lValue));
        Assert.False(boxObj.Equals(iValue));
        Assert.True(boxObj.Equals(lValue));

        Assert.False(iObj.Equals(box));
        Assert.True(lObj.Equals(box));
        Assert.False(iObj.Equals(boxObj));
        Assert.True(lObj.Equals(boxObj));
        Assert.False(box.Equals(iObj));
        Assert.True(box.Equals(lObj));
        Assert.False(boxObj.Equals(iObj));
        Assert.True(boxObj.Equals(lObj));

        // Testing that unboxing uses a fast process without unnecessary type-checking
        _ = (ValueTuple)Unsafe.As<Box<int>, Box<ValueTuple>>(ref box);
        _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).GetReference();
        _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).DangerousGetReference();
    }

    /// <summary>
    /// Tests the <see cref="Box{T}"/> type for a given pair of values.
    /// </summary>
    /// <typeparam name="T">The type to test.</typeparam>
    /// <param name="value">The initial <typeparamref name="T"/> value.</param>
    /// <param name="test">The new <typeparamref name="T"/> value to assign and test.</param>
    private static void Test<T>(T? value, T? test)
        where T : struct
    {
        object? obj = value;
        Box<T>? box;
        {
            box = BoxBase.AsBox<T, Box<T>>(obj);
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = BoxBase.TryAsBox<T, Box<T>>(obj);
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = BoxBase.DangerousAsBox<T, Box<T>>(obj);
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = BoxBase.AsBox<T, Box<T>>(value);
            Test();

            box = BoxBase.TryAsBox<T, Box<T>>(value);
            Test();

            box = BoxBase.DangerousAsBox<T, Box<T>>(value);
            Test();
        }

        {
            box = obj.AsBox<T>();
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = obj.TryAsBox<T>();
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = Box.DangerousAsBox<T>(obj);
            Assert.Same(obj, box);
            Test();
            if (box is not null && test is not null)
            {
                box.DangerousGetReference() = test.Value;
                Assert.Same(obj, box);
                (value, test) = (test, value);
                Test();
                (value, test) = (test, value);
                box.DangerousGetReference() = value.Value;
            }

            box = value;
            Test();

            box = value?.ToBox();
            Test();

            box = value.AsBox<T>();
            Test();

            box = value.TryAsBox<T>();
            Test();

            box = Box.DangerousAsBox<T>(value);
            Test();
        }

        // Testing that unboxing uses a fast process without unnecessary type-checking
        if (box is not null)
        {
            _ = (ValueTuple)Unsafe.As<Box<T>, Box<ValueTuple>>(ref box);
            _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).GetReference();
            _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).DangerousGetReference();
        }

        void Test()
        {
            object? boxObj = box;
            Assert.Same(box, boxObj);

            Assert.Equal(value, box?.GetReference());
            Assert.Equal(value, box?.DangerousGetReference());
            Assert.Equal(value, box?.Value);
            Assert.Equal(value, (T?)box);
            Assert.Equal(value, (T?)boxObj);
            Assert.Equal(value, boxObj);

            Assert.Equal(obj, box?.GetReference());
            Assert.Equal(obj, box?.DangerousGetReference());
            Assert.Equal(obj, box?.Value);
            Assert.Equal(obj, (T?)box);
            Assert.Equal(obj, (T?)boxObj);
            Assert.Equal(obj, boxObj);

            Assert.Equal(boxObj, box?.GetReference());
            Assert.Equal(boxObj, box?.DangerousGetReference());
            Assert.Equal(boxObj, box?.Value);
            Assert.Equal(boxObj, (T?)box);
            Assert.Equal(boxObj, (T?)boxObj);

            Assert.Equal(value?.ToString(), box?.ToString());
            Assert.Equal(value?.ToString(), boxObj?.ToString());
            Assert.Equal(obj?.ToString(), box?.ToString());
            Assert.Equal(obj?.ToString(), boxObj?.ToString());

            Assert.Equal(value?.GetHashCode(), box?.GetHashCode());
            Assert.Equal(value?.GetHashCode(), boxObj?.GetHashCode());
            Assert.Equal(obj?.GetHashCode(), box?.GetHashCode());
            Assert.Equal(obj?.GetHashCode(), boxObj?.GetHashCode());

            if (box is not null)
            {
                Assert.True(box.Equals((object?)value));
                Assert.True(boxObj?.Equals(value));
                Assert.True(box.Equals(obj));
                Assert.True(boxObj?.Equals(obj));

            }

            // test
            Assert.NotEqual(test, box?.GetReference());
            Assert.NotEqual(test, box?.DangerousGetReference());
            Assert.NotEqual(test, box?.Value);
            Assert.NotEqual(test, (T?)box);
            Assert.NotEqual(test, (T?)boxObj);
            Assert.NotEqual(test, boxObj);

            if (value.ToString() != test.ToString())
            {
                Assert.NotEqual(test.ToString(), box?.ToString());
                Assert.NotEqual(test.ToString(), boxObj?.ToString());
            }

            Assert.NotEqual(test.GetHashCode(), box?.GetHashCode());
            Assert.NotEqual(test.GetHashCode(), boxObj?.GetHashCode());

            if (box is not null)
            {
                Assert.False(box.Equals((object?)test));
                Assert.False(boxObj?.Equals(test));
            }

            // IEquatable
            if (box is not null)
                Assert.True(box?.Equals(value?.ToBox()));
            Assert.True(box == value?.ToBox());
            Assert.False(box != value?.ToBox());

            if (box is not null)
                Assert.False(box?.Equals(test?.ToBox()));
            Assert.False(box == test?.ToBox());
            Assert.True(box != test?.ToBox());

            // IComparable
            if (value is IComparable<T>)
            {
                int compare = Math.Sign(Comparer<T?>.Default.Compare(value, test));

                if (box is not null)
                {
                    Assert.Equal(0, box.CompareTo(value?.ToBox()));
                    Assert.Equal(compare, Math.Sign(box.CompareTo(test?.ToBox())));
                    Assert.Equal(1, Math.Sign(box.CompareTo(null)));
                }

                Assert.False(box < value?.ToBox());
                Assert.False(box > value?.ToBox());
                Assert.Equal(compare < 0, box < test?.ToBox());
                Assert.Equal(compare > 0, box > test?.ToBox());

                Assert.True(box <= value?.ToBox());
                Assert.True(box >= value?.ToBox());
                Assert.Equal(compare <= 0, box <= test?.ToBox());
                Assert.Equal(compare >= 0, box >= test?.ToBox());
            }
        }
    }

#pragma warning disable CA1067 // Override Object.Equals(object) when implementing IEquatable<T>

    private struct TestStruct : IEquatable<TestStruct>, IComparable<TestStruct>
    {
        public int Number;
        public char Character;
        public string Text;

        public readonly bool Equals(TestStruct other) =>
            Number == other.Number &&
            Character == other.Character &&
            Text == other.Text;

        public readonly int CompareTo(TestStruct other)
            => Number.CompareTo(other.Number) switch
            {
                0 => Character.CompareTo(other.Character) switch
                {
                    0 => Text.CompareTo(other.Text),
                    var r => r
                },
                var r => r
            };
    }

#pragma warning restore CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
}
