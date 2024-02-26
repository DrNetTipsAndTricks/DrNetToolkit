// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using DrNetToolkit.Polyfills;
using DrNetToolkit.Polyfills.Hidden;



#if NETSTANDARD2_1_OR_GREATER
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpers;
#else
using RTHelpers = System.Runtime.CompilerServices.RuntimeHelpersPolyfills;
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
    public void RuntimeHelpers_T()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = RTHelpers.IsReferenceOrContainsReferences<T>();
        }
    }

    [Benchmark]
    public void RuntimeHelpersPolyfills_T()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = RuntimeHelpersPolyfills.IsReferenceOrContainsReferences<T>();
        }
    }

    [Benchmark]
    [RequiresUnreferencedCode("This functionality is not compatible with trimming.")]
    public void RuntimeHelpersHidden_TypeOf()
    {
        for (int i = 0; i < Count; i++)
        {
            _value = RuntimeHelpersHidden.IsReferenceOrContainsReferences(s_type);
        }
    }
}

