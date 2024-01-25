// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.HighPerformance.Boxing;

using DrNetToolkit.HighPerformance.Internal.Boxing;

using System.Runtime.CompilerServices;

/// <inheritdoc/>
public sealed class Box<T> : BoxBase<T> where T : struct
{
    /// <inheritdoc/>
    private Box() : base() { }

    /// <summary>
    /// Implicitly creates a new boxed <paramref name="value"/> instance and cast it to the box.
    /// </summary>
    /// <param name="value">The value to box into the box.</param>
    /// <returns>The created boxed value, casted to the box.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Box<T>(T value)
        => Box.DangerousAsBox<T>(value);
}
