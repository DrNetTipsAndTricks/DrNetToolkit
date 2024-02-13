// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Linq;
using Bogus;
using Xunit;

namespace DrNetToolkit.HighPerformance.UnitTests;

public class MemoryMarshaling_Tests
{
    [Fact]
    public unsafe void MemoryMarshaling_CastFromToNullable_Span()
    {
        int len = 50;
        int count = 5;

        var faker = new Faker()
        {
            Random = new Randomizer(248218),
        };

        byte[] bytes = [.. faker.Make(len, () => (byte)faker.Random.Number(255))];
        byte?[] nBytes = bytes.Select(n => faker.Random.Double() > 0.1 ? (byte?)n : null).ToArray();

        // Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            Span<byte?> nBytesSpan = nBytes.AsSpan(range).ToArray();

            Span<long?> nLongsSpan = MemoryHelpers.Cast<byte?, long?>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long?), nLongsSpan.Length);

            Span<byte?> span = MemoryHelpers.Cast<long?, byte?>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte?), span.Length);
            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());
        }

        // Nullable to Non Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            Span<byte?> nBytesSpan = nBytes.AsSpan(range).ToArray();

            Span<long> longsSpan = MemoryHelpers.Cast<byte?, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long), longsSpan.Length);

            Span<byte?> span = MemoryHelpers.Cast<long, byte?>(longsSpan);
            Assert.Equal(longsSpan.Length * sizeof(long) / sizeof(byte?), span.Length);
            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());
        }

        // Non Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            Span<byte> bytesSpan = bytes.AsSpan(range).ToArray();

            Span<long?> nLongsSpan = MemoryHelpers.Cast<byte, long?>(bytesSpan);
            Assert.Equal(bytesSpan.Length * sizeof(byte) / sizeof(long?), nLongsSpan.Length);

            Span<byte> span = MemoryHelpers.Cast<long?, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte), span.Length);
            Assert.Equal(bytesSpan.Slice(0, span.Length), span);
        }
    }

    [Fact]
    public unsafe void MemoryMarshaling_CastFromToNullable_ReadOnlySpan()
    {
        int len = 50;
        int count = 5;

        var faker = new Faker()
        {
            Random = new Randomizer(248218),
        };

        byte[] bytes = [.. faker.Make(len, () => (byte)faker.Random.Number(255))];
        byte?[] nBytes = bytes.Select(n => faker.Random.Double() > 0.1 ? (byte?)n : null).ToArray();

        // Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            ReadOnlySpan<byte?> nBytesSpan = nBytes.AsSpan(range).ToArray();

            ReadOnlySpan<long?> nLongsSpan = MemoryHelpers.Cast<byte?, long?>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long?), nLongsSpan.Length);

            ReadOnlySpan<byte?> span = MemoryHelpers.Cast<long?, byte?>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte?), span.Length);
            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());
        }

        // Nullable to Non Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            ReadOnlySpan<byte?> nBytesSpan = nBytes.AsSpan(range).ToArray();

            ReadOnlySpan<long> longsSpan = MemoryHelpers.Cast<byte?, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long), longsSpan.Length);

            ReadOnlySpan<byte?> span = MemoryHelpers.Cast<long, byte?>(longsSpan);
            Assert.Equal(longsSpan.Length * sizeof(long) / sizeof(byte?), span.Length);
            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());
        }

        // Non Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Range range = faker.Random.Number(len / 10)..^faker.Random.Odd(len / 2, len - len / 10);
            if (i == 0)
                range = (len / 2)..(len / 2);

            ReadOnlySpan<byte> bytesSpan = bytes.AsSpan(range).ToArray();

            ReadOnlySpan<long?> nLongsSpan = MemoryHelpers.Cast<byte, long?>(bytesSpan);
            Assert.Equal(bytesSpan.Length * sizeof(byte) / sizeof(long?), nLongsSpan.Length);

            ReadOnlySpan<byte> span = MemoryHelpers.Cast<long?, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte), span.Length);
            Assert.Equal(bytesSpan.Slice(0, span.Length), span);
        }
    }
}
