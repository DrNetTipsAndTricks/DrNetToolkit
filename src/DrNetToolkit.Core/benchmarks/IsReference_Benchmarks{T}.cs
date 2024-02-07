// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using BenchmarkDotNet.Attributes;

#if NETSTANDARD2_1_OR_GREATER
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#endif

namespace DrNetToolkit.Runtime.Benchmarks;

public class IsReference_Benchmarks<T>
{
    [Params(1, 10, 100, 1_000, 10_000)]
    public int Count { get; set; }

#pragma warning disable IDE0052 // Remove unread private members

    private volatile bool _value;

#pragma warning restore IDE0052 // Remove unread private members

    private static readonly Type s_type = typeof(T);

    [Benchmark(Baseline = true)]
    public void RuntimeLib()
    {
        for (int i = 0; i < Count; i++)
        {
#if NETSTANDARD2_1_OR_GREATER
            _value = RTHelpers.IsReferenceOrContainsReferences<T>();
#else
            _value = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
#endif
        }
    }

    [Benchmark]
    public void OurTypeInfo()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = TypeInfo<T>.IsReferenceOrContainsReferences;
        }
    }

    [Benchmark]
    public void OurTypeOf()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = RuntimeHelpers.IsReferenceOrContainsReferences(s_type);
        }
    }

    private struct StructWithReference
    {
        public (int i, (int i, (int i, (int i, (int i, object obj))))) value;
    }

    private struct StructWithoutReference
    {
        public (int i, (int i, (int i, (int i, (int i, int obj))))) value;
    }
}

