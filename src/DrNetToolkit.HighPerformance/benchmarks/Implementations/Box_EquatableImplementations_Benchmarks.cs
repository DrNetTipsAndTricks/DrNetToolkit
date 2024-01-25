// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.HighPerformance.Benchmarks.Implementations;

using DrNetToolkit.HighPerformance.Boxing;

using BenchmarkDotNet.Attributes;

public class Box_EquatableImplementations_Benchmarks
{
    [Params(1, 10, 100, 1_000, 10_000)]
    public int Count { get; set; }

#pragma warning disable IDE0052 // Remove unread private members
#pragma warning disable IDE0044 // Add readonly modifier

    private volatile Box<int> box0 = 0.ToBox();
    private volatile Box<int> box1 = 1.ToBox();
    private volatile Box<int> box0_2 = 0.ToBox();

    private volatile bool result;

#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0052 // Remove unread private members

    [Benchmark(Baseline = true)]
    public void Equals()
    {
        for (int i = 0; i < this.Count; i++)
        {
            this.result = this.box0.Equals(this.box1);
            this.result = this.box0.Equals(this.box0_2);
        }
    }

    //[Benchmark]
    //public void Equals2()
    //{
    //    for (int i = 0; i < this.Count; i++)
    //    {
    //        this.result = this.box0.Equals2(this.box1);
    //        this.result = this.box0.Equals2(this.box0_2);
    //    }
    //}

    //public bool Equals2(Box<T>? other)
    //{
    //    if (other is null || this is null)
    //    {
    //        return ReferenceEquals(this, other);
    //    }

    //    if (this.Value is IEquatable<T> equatable)
    //    {
    //        return equatable.Equals(other);
    //    }

    //    return EqualityComparer<T>.Default.Equals(this, other);
    //}
}
