// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.HighPerformance.Benchmarks;

using DrNetToolkit.HighPerformance.Boxing;

using BenchmarkDotNet.Attributes;

public class Box_Unboxing_Benchmarks
{
    [Params(1, 10, 100, 1_000, 10_000)]
    public int Count { get; set; }

#pragma warning disable IDE0052 // Remove unread private members

    private volatile int value;

#pragma warning restore IDE0052 // Remove unread private members

    private readonly object obj = 0;
    private readonly Box<int> box = 0.ToBox();

    [Benchmark(Baseline = true)]
    public void ObjectToInt()
    {
        for (int i = 0; i < this.Count; i++)
        {
            this.value = (int)this.obj;
        }
    }

    [Benchmark]
    public void BoxToInt()
    {
        for (int i = 0; i < this.Count; i++)
        {
            this.value = this.box;
        }
    }
}
