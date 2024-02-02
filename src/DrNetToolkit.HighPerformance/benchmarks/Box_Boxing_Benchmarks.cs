// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using BenchmarkDotNet.Attributes;
using DrNetToolkit.HighPerformance.Boxing;

namespace DrNetToolkit.HighPerformance.Benchmarks;

public class Box_Boxing_Benchmarks
{
    [Params(1, 10, 100, 1_000, 10_000)]
    public int Count { get; set; }

#pragma warning disable IDE0052 // Remove unread private members

    private volatile object _obj = new();
    private volatile Box<int> _box = 0.ToBox();

#pragma warning restore IDE0052 // Remove unread private members

    [Benchmark(Baseline = true)]
    public void IntToObject()
    {
        for (int i = 0; i < Count; i++)
        {
            _obj = i;
        }
    }

    [Benchmark]
    public void IntToBox()
    {
        for (int i = 0; i < Count; i++)
        {
            _box = i.ToBox();
        }
    }
}
