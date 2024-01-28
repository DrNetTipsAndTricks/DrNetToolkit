// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Linq;
using Xunit;
using Bogus;
using System.Collections.Generic;
using System.Collections;

namespace DrNetToolkit.HighPerformance.UnitTests;

public class EquatableNullable_Tests
{
    [Fact]
    public void EquatableNullable_Valid()
    {
        Test(true, false);
        Test<byte>(27, 254);
        Test('a', '$');
        Test(4221124, 1241241);
        Test(3.14f, 2342.222f);
        Test(8394324ul, 1343431241ul);
        Test(184013.234324, 14124.23423);
        Test(DateTime.Now, DateTime.FromBinary(278091429014));
        Test(Guid.NewGuid(), Guid.NewGuid());

        //{
        //    TestStruct a = new() { Number = 42, Character = 'a', Text = "Hello" };
        //    TestStruct b = new() { Number = 38293, Character = 'z', Text = "World" };

        //    Test(a, b);
        //}
    }

#if NETSTANDARD2_1_OR_GREATER

    [Fact]
    public void EquatableNullable_AsEquatableAsNullable()
    {
        int len = 50;
        int count = 5;
        int delta = 100;

        var faker = new Faker()
        {
            Random = new Randomizer(334794),
        };

        int?[] nInts = faker.Make(len, () => faker.Random.Number(-delta, delta)).
            Select(n => faker.Random.Double() > 0.1 ? (int?)n : null).ToArray();

        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            int?[] nIntsSlice = nInts[range];

            // Span
            {
                Span<EquatableNullable<int>> eSpan = nInts.AsSpan(range).AsEquatable();

                Assert.Equal(nIntsSlice, eSpan.ToArray().Select(e => e.NullableValue));

                Span<int?> nSpan = eSpan.AsNullable();

                Assert.Equal(nIntsSlice, nSpan.ToArray());

                Assert.Equal(Array.IndexOf(nIntsSlice, null), eSpan.IndexOf((int?)null));
                Assert.Equal(Array.LastIndexOf(nIntsSlice, null), eSpan.LastIndexOf((int?)null));

                if (nSpan.Length > 0)
                {
                    int? v = nSpan[nSpan.Length / 2];
                    Assert.Equal(Array.IndexOf(nIntsSlice, v), eSpan.IndexOf(v));
                    Assert.Equal(Array.LastIndexOf(nIntsSlice, v), eSpan.LastIndexOf(v));
                }

                {
                    int? v = delta + 1;
                    Assert.Equal(-1, eSpan.IndexOf(v));
                    Assert.Equal(-1, eSpan.LastIndexOf(v));
                }

                Array.Sort(nIntsSlice);
                eSpan.Sort();
                Assert.Equal(nIntsSlice, eSpan.ToArray().Select(e => e.NullableValue));
            }

            // ReadOnlySpan
            {
                ReadOnlySpan<EquatableNullable<int>> eSpan = nInts.AsSpan(range).AsReadOnlySpan().AsEquatable();

                Assert.Equal(nIntsSlice, eSpan.ToArray().Select(e => e.NullableValue));

                ReadOnlySpan<int?> nSpan = eSpan.AsNullable();

                Assert.Equal(nIntsSlice, nSpan.ToArray());

                Assert.Equal(Array.IndexOf(nIntsSlice, null), eSpan.IndexOf((int?)null));
                Assert.Equal(Array.LastIndexOf(nIntsSlice, null), eSpan.LastIndexOf((int?)null));

                if (nSpan.Length > 0)
                {
                    int? v = nSpan[nSpan.Length / 2];
                    Assert.Equal(Array.IndexOf(nIntsSlice, v), eSpan.IndexOf(v));
                    Assert.Equal(Array.LastIndexOf(nIntsSlice, v), eSpan.LastIndexOf(v));
                }

                {
                    int? v = delta + 1;
                    Assert.Equal(-1, eSpan.IndexOf(v));
                    Assert.Equal(-1, eSpan.LastIndexOf(v));
                }

                Array.Sort(nIntsSlice);
                eSpan.DangerousAsSpan().Sort();
                Assert.Equal(nIntsSlice, eSpan.ToArray().Select(e => e.NullableValue));
            }
        }
    }

#endif

    /// <summary>
    /// Tests the <see cref="EquatableNullable{T}"/> type for a given pair of values.
    /// </summary>
    /// <typeparam name="T">The type to test.</typeparam>
    /// <param name="value">The initial <typeparamref name="T"/> value.</param>
    /// <param name="test">The new <typeparamref name="T"/> value to assign and test.</param>
    private static void Test<T>(T value, T test)
        where T : struct
    {
        T? nullable;
        T? nullableTest;

        EquatableNullable<T> equatable;

        // Null
        nullable = null;
        nullableTest = test;
        {
            equatable = null;
            Test();

            equatable = new(nullable);
            Test();

            equatable = (EquatableNullable<T>)nullable;
            Test();
        }

        // Not Null
        nullable = value;
        {
            equatable = new(nullable);
            Test();

            equatable = (EquatableNullable<T>)nullable;
            Test();

            equatable = (EquatableNullable<T>)value;
            Test();
        }

        void Test()
        {
            Assert.Equal(nullable, equatable.NullableValue);
            Assert.NotEqual(nullableTest, equatable.NullableValue);

            Assert.Equal(nullable.GetValueOrDefault(), equatable.GetValueOrDefault());
            if (nullableTest.GetValueOrDefault().Equals(nullable.GetValueOrDefault()))
                Assert.Equal(nullableTest.GetValueOrDefault(), equatable.GetValueOrDefault());
            else
                Assert.NotEqual(nullableTest.GetValueOrDefault(), equatable.GetValueOrDefault());

            Assert.Equal(nullable.GetValueOrDefault(test), equatable.GetValueOrDefault(test));
            if (nullableTest.GetValueOrDefault(value).Equals(nullable.GetValueOrDefault(test)))
                Assert.Equal(nullableTest.GetValueOrDefault(value), equatable.GetValueOrDefault(test));
            else
                Assert.NotEqual(nullableTest.GetValueOrDefault(value), equatable.GetValueOrDefault(test));

            if (nullable.HasValue)
                Assert.True(equatable.Equals((object)nullable.Value));
            if (nullableTest.HasValue)
                Assert.False(equatable.Equals((object)nullableTest.Value));

            Assert.True(equatable.Equals((object)nullable!));
            Assert.False(equatable.Equals((object)nullableTest!));

            Assert.True(equatable.Equals((EquatableNullable<T>)nullable));
            Assert.False(equatable.Equals((EquatableNullable<T>)nullableTest));

            Assert.Equal(nullable.GetHashCode(), equatable.GetHashCode());
            if (nullableTest.GetHashCode() == nullable.GetHashCode())
                Assert.Equal(nullableTest.GetHashCode(), equatable.GetHashCode());
            else
                Assert.NotEqual(nullableTest.GetHashCode(), equatable.GetHashCode());

            Assert.Equal(nullable.ToString(), equatable.ToString());
            Assert.NotEqual(nullableTest.ToString(), equatable.ToString());

            Assert.Equal(nullable, (T?)equatable);
            Assert.NotEqual(nullableTest, (T?)equatable);

            if (nullable.HasValue)
            {
                Assert.Equal((T)nullable, (T)equatable);
                Assert.NotEqual((T)nullableTest, (T)equatable);
            }

            // IEquatable
            Assert.True(equatable.Equals((EquatableNullable<T>)nullable));
            Assert.False(equatable.Equals((EquatableNullable<T>)nullableTest));
            Assert.Equal(nullable is null, equatable.Equals((EquatableNullable<T>)null));
            Assert.Equal(nullable is null, ((EquatableNullable<T>)null).Equals(equatable));

            Assert.True(equatable == (EquatableNullable<T>)nullable);
            Assert.False(equatable == (EquatableNullable<T>)nullableTest);
            Assert.Equal(nullable is null, equatable == (EquatableNullable<T>)null);
            Assert.Equal(nullable is null, (EquatableNullable<T>)null == equatable);

            Assert.False(equatable != (EquatableNullable<T>)nullable);
            Assert.True(equatable != (EquatableNullable<T>)nullableTest);
            Assert.Equal(nullable is not null, equatable != (EquatableNullable<T>)null);
            Assert.Equal(nullable is not null, (EquatableNullable<T>)null != equatable);

            // IComparable
            {
                Comparer<T?> comparer = Comparer<T?>.Default;

                Assert.Equal(0, equatable.CompareTo((EquatableNullable<T>)nullable));
                Assert.Equal(Math.Sign(comparer.Compare(nullable, nullableTest)),
                    Math.Sign(equatable.CompareTo((EquatableNullable<T>)nullableTest)));
                Assert.Equal(Math.Sign(comparer.Compare(nullable, null)),
                    Math.Sign(equatable.CompareTo((EquatableNullable<T>)null)));
                Assert.Equal(Math.Sign(comparer.Compare(null, nullable)),
                    Math.Sign(((EquatableNullable<T>)null).CompareTo(equatable)));

                Assert.False(equatable < (EquatableNullable<T>)nullable);
                Assert.Equal(comparer.Compare(nullable, nullableTest) < 0,
                    equatable < (EquatableNullable<T>)nullableTest);
                Assert.Equal(comparer.Compare(nullable, null) < 0, equatable < (EquatableNullable<T>)null);
                Assert.Equal(comparer.Compare(null, nullable) < 0, (EquatableNullable<T>)null < equatable);

                Assert.False(equatable > (EquatableNullable<T>)nullable);
                Assert.Equal(comparer.Compare(nullable, nullableTest) > 0,
                    equatable > (EquatableNullable<T>)nullableTest);
                Assert.Equal(comparer.Compare(nullable, null) > 0, equatable > (EquatableNullable<T>)null);
                Assert.Equal(comparer.Compare(null, nullable) > 0, (EquatableNullable<T>)null > equatable);

                Assert.True(equatable <= (EquatableNullable<T>)nullable);
                Assert.Equal(comparer.Compare(nullable, nullableTest) <= 0,
                    equatable <= (EquatableNullable<T>)nullableTest);
                Assert.Equal(comparer.Compare(nullable, null) <= 0, equatable <= (EquatableNullable<T>)null);
                Assert.Equal(comparer.Compare(null, nullable) <= 0, (EquatableNullable<T>)null <= equatable);

                Assert.True(equatable >= (EquatableNullable<T>)nullable);
                Assert.Equal(comparer.Compare(nullable, nullableTest) >= 0,
                    equatable >= (EquatableNullable<T>)nullableTest);
                Assert.Equal(comparer.Compare(nullable, null) >= 0, equatable >= (EquatableNullable<T>)null);
                Assert.Equal(comparer.Compare(null, nullable) >= 0, (EquatableNullable<T>)null >= equatable);
            }
        }
    }
}
