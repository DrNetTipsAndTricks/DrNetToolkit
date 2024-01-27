// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using Xunit;
using Bogus;
using System.Linq;

namespace DrNetToolkit.HighPerformance.UnitTests;

public class MemoryMarshaling_Tests
{
#if NETSTANDARD2_1_OR_GREATER

    [Fact]
    public unsafe void MemoryMarshaling_Cast_Span()
    {
        int len = 100;
        int count = 10;

        var faker = new Faker()
        {
            Random = new Randomizer(248218),
        };

        byte[] bytes = [.. faker.Make(len, () => (byte)faker.Random.Number(255))];
        byte?[] nBytes = bytes.Select(n => faker.Random.Double() > 0.1 ? (byte?)n : null).ToArray();

        // Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Span<byte?> nBytesSpan = nBytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            Span<long?> nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long?), nLongsSpan.Length);

            Span<byte?> span = MemoryMarshaling.CastToNullable<long, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte?), span.Length);

            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            nBytesSpan = nBytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long?) / sizeof(byte?) - 1);
            nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(nBytesSpan);
            Assert.Equal(0, nLongsSpan.Length);
        }

        // Nullable to Non Nullable
        for (int i = 0; i < count; i++)
        {
            Span<byte?> nBytesSpan = nBytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            Span<long> longsSpan = MemoryMarshaling.Cast<byte, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long), longsSpan.Length);

            Span<byte?> span = MemoryMarshaling.CastToNullable<long, byte>(longsSpan);
            Assert.Equal(longsSpan.Length * sizeof(long) / sizeof(byte?), span.Length);

            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            nBytesSpan = nBytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long) / sizeof(byte?) - 1);
            longsSpan = MemoryMarshaling.Cast<byte, long>(nBytesSpan);
            Assert.Equal(0, longsSpan.Length);
        }

        // Non Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            Span<byte> bytesSpan = bytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            Span<long?> nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(bytesSpan);
            Assert.Equal(bytesSpan.Length * sizeof(byte) / sizeof(long?), nLongsSpan.Length);

            Span<byte> span = MemoryMarshaling.Cast<long, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte), span.Length);

            Assert.Equal(bytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            bytesSpan = bytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long?) / sizeof(byte) - 1);
            nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(bytesSpan);
            Assert.Equal(0, nLongsSpan.Length);
        }
    }

    [Fact]
    public unsafe void MemoryMarshaling_Cast_ReadOnlySpan()
    {
        int len = 100;
        int count = 10;

        var faker = new Faker()
        {
            Random = new Randomizer(792361),
        };

        byte[] bytes = [.. faker.Make(len, () => (byte)faker.Random.Number(255))];
        byte?[] nBytes = bytes.Select(n => faker.Random.Double() > 0.1 ? (byte?)n : null).ToArray();

        // Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            ReadOnlySpan<byte?> nBytesSpan = nBytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            ReadOnlySpan<long?> nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long?), nLongsSpan.Length);

            ReadOnlySpan<byte?> span = MemoryMarshaling.CastToNullable<long, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte?), span.Length);

            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            nBytesSpan = nBytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long?) / sizeof(byte?) - 1);
            nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(nBytesSpan);
            Assert.Equal(0, nLongsSpan.Length);
        }

        // Nullable to Non Nullable
        for (int i = 0; i < count; i++)
        {
            ReadOnlySpan<byte?> nBytesSpan = nBytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            ReadOnlySpan<long> longsSpan = MemoryMarshaling.Cast<byte, long>(nBytesSpan);
            Assert.Equal(nBytesSpan.Length * sizeof(byte?) / sizeof(long), longsSpan.Length);

            ReadOnlySpan<byte?> span = MemoryMarshaling.CastToNullable<long, byte>(longsSpan);
            Assert.Equal(longsSpan.Length * sizeof(long) / sizeof(byte?), span.Length);

            Assert.Equal(nBytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            nBytesSpan = nBytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long) / sizeof(byte?) - 1);
            longsSpan = MemoryMarshaling.Cast<byte, long>(nBytesSpan);
            Assert.Equal(0, longsSpan.Length);
        }

        // Non Nullable to Nullable
        for (int i = 0; i < count; i++)
        {
            ReadOnlySpan<byte> bytesSpan = bytes.AsSpan().
                Slice(faker.Random.Number(len / 10), faker.Random.Odd(len / 2, len - len / 10));

            ReadOnlySpan<long?> nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(bytesSpan);
            Assert.Equal(bytesSpan.Length * sizeof(byte) / sizeof(long?), nLongsSpan.Length);

            ReadOnlySpan<byte> span = MemoryMarshaling.Cast<long, byte>(nLongsSpan);
            Assert.Equal(nLongsSpan.Length * sizeof(long?) / sizeof(byte), span.Length);

            Assert.Equal(bytesSpan.Slice(0, span.Length).ToArray(), span.ToArray());

            bytesSpan = bytes.AsSpan().Slice(faker.Random.Number(len / 10), sizeof(long?) / sizeof(byte) - 1);
            nLongsSpan = MemoryMarshaling.CastToNullable<byte, long>(bytesSpan);
            Assert.Equal(0, nLongsSpan.Length);
        }
    }

#endif
}
