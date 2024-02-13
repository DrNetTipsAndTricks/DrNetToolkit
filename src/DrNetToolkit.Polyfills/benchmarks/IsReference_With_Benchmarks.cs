// Licensed to the "DrNet Tips&Tricks" under one or more agreements.
// The "DrNet Tips&Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.Runtime.Benchmarks;

public class IsReference_With_Benchmarks: IsReference_Benchmarks<StructWithReference>
{ }

public struct StructWithReference
{
    public (int i, (int i, (int i, (int i, (int i, object obj))))) value;
}

