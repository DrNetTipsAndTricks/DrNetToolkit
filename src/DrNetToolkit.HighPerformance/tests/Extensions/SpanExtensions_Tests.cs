// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using Bogus;
using Xunit;

namespace DrNetToolkit.HighPerformance.UnitTests;

#if NETSTANDARD2_1_OR_GREATER

public class SpanExtensions_Tests
{
    [Fact]
    public void SpanExtensions_DangerousAsSpan_Test()
    {
        int len = 50;
        int count = 5;
        int delta = 100;

        var faker = new Faker()
        {
            Random = new Randomizer(146321),
        };

        int[] ints = [.. faker.Make(len, () => faker.Random.Number(-delta, delta))];

        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            ReadOnlySpan<int> roInts = ints.AsSpan(range).ToArray();

            Span<int> sInts = roInts.DangerousAsSpan();
            Assert.Equal(roInts, sInts);

            sInts.Sort();
            Assert.Equal(sInts, roInts);
        }
    }

    [Fact]
    public void SpanExtensions_AsReadOnlySpan_Test()
    {
        int len = 50;
        int count = 5;
        int delta = 100;

        var faker = new Faker()
        {
            Random = new Randomizer(686762),
        };

        int[] ints = [.. faker.Make(len, () => faker.Random.Number(-delta, delta))];

        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            Span<int> sInts = ints.AsSpan(range).ToArray();

            ReadOnlySpan<int> roInts = sInts.AsReadOnlySpan();
            Assert.Equal(sInts, roInts);

            sInts.Sort();
            Assert.Equal(sInts, roInts);
        }
    }
}

#endif
