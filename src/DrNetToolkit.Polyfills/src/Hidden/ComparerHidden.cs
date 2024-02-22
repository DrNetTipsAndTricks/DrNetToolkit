// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

using System;
using System.Collections.Generic;

namespace DrNetToolkit.Polyfills.Hidden;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

public sealed class ComparisonComparerHidden<T>(Comparison<T> comparison) : Comparer<T>
{
    public override int Compare(T? x, T? y) => comparison(x!, y!);
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
