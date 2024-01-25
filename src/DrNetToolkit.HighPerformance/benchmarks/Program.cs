// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using DrNetToolkit.HighPerformance.Benchmarks;

using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Horology;

IConfig config = DefaultConfig.Instance
    .WithOption(ConfigOptions.StopOnFirstError, true)
    .AddJob(
        Job.ShortRun
            .WithIterationTime(TimeInterval.FromMilliseconds(150))
    );

BenchmarkRunner.Run(
    [
        typeof(Box_Boxing_Benchmarks),
        typeof(Box_Unboxing_Benchmarks),
        typeof(Box_SetValue_Benchmarks),
    ],
    config
);
