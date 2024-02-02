// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using BenchmarkDotNet.Attributes;
using DrNetToolkit.HighPerformance.Boxing;

namespace DrNetToolkit.HighPerformance.Benchmarks;

public class Box_Unboxing_Benchmarks
{
    [Params(1, 10, 100, 1_000, 10_000)]
    public int Count { get; set; }

#pragma warning disable IDE0052 // Remove unread private members

    private volatile int _value;

    private readonly object _obj = 0;
    private readonly Box<int> _box = 0.ToBox();

    [Benchmark(Baseline = true)]
    public void ObjectToInt()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = (int)_obj;
        }
    }

    [Benchmark]
    public void BoxToInt()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = _box;
        }
    }
}
