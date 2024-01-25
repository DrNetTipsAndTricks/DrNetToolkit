// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace DrNetToolkit.HighPerformance.UnitTests;

using DrNetToolkit.HighPerformance.Internal.Boxing;
using DrNetToolkit.HighPerformance.Boxing;

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BoxOfT_Tests
{
    [TestMethod]
    public void BoxOfT_Valid()
    {
        Test(true, false);
        Test<byte>(27, 254);
        Test('a', '$');
        Test(4221124, 1241241);
        Test(3.14f, 2342.222f);
        Test(8394324ul, 1343431241ul);
        Test(184013.234324, 14124.23423);
        Test(DateTime.Now, DateTime.FromBinary(278091429014));
        Test(Guid.NewGuid(), Guid.NewGuid());

        {
            TestStruct a = new() { Number = 42, Character = 'a', Text = "Hello" };
            TestStruct b = new() { Number = 38293, Character = 'z', Text = "World" };

            Test(a, b);
        }
    }

    //[TestMethod]
    //public void BoxOfT_Invalid()
    //{
    //    long lValue = 0x0123_4567_89AB_CDEF;
    //    int iValue = Unsafe.As<long, int>(ref lValue);
    //    object? lObj = lValue;
    //    object? iObj = iValue;

    //    _ = Assert.ThrowsException<InvalidCastException>(() => Box<int>.CastFrom(lObj));
    //    Assert.IsNull(Box<int>.TryCastFrom(lValue));
    //    Box<int> box = Box<int>.DangerousCastFrom(lObj);

    //    object boxObj = box;
    //    Assert.AreSame(lObj, box);
    //    Assert.AreSame(lObj, boxObj);

    //    Assert.AreEqual(iValue, box.GetReference());
    //    Assert.AreEqual(iValue, box.GetDangerousReference());
    //    Assert.AreEqual(iValue, box.Value);
    //    Assert.AreEqual(iValue, (int)box);
    //    Assert.AreEqual(iValue, (long)box);
    //    _ = Assert.ThrowsException<InvalidCastException>(() => (int)boxObj);
    //    Assert.AreNotEqual(iValue, (long)boxObj);

    //    Assert.AreNotEqual(lValue, box.GetReference());
    //    Assert.AreNotEqual(lValue, box.GetDangerousReference());
    //    Assert.AreNotEqual(lValue, box.Value);
    //    Assert.AreNotEqual(lValue, (int)box);
    //    Assert.AreNotEqual(lValue, (long)box);
    //    Assert.AreEqual(lValue, (long)boxObj);

    //    Assert.AreEqual(iObj, box.GetReference());
    //    Assert.AreEqual(iObj, box.GetDangerousReference());
    //    Assert.AreEqual(iObj, box.Value);
    //    Assert.AreEqual(iObj, (int)box);
    //    Assert.AreNotEqual(iObj, (long)box);
    //    Assert.AreNotEqual(iObj, (long)boxObj);

    //    Assert.AreNotEqual(lObj, box.GetReference());
    //    Assert.AreNotEqual(lObj, box.GetDangerousReference());
    //    Assert.AreNotEqual(lObj, box.Value);
    //    Assert.AreNotEqual(lObj, (int)box);
    //    Assert.AreNotEqual(lObj, (long)box);
    //    Assert.AreEqual(lObj, (long)boxObj);

    //    Assert.AreNotEqual(boxObj, box.GetReference());
    //    Assert.AreNotEqual(boxObj, box.GetDangerousReference());
    //    Assert.AreNotEqual(boxObj, box.Value);
    //    Assert.AreNotEqual(boxObj, (int)box);
    //    Assert.AreNotEqual(boxObj, (long)box);
    //    Assert.AreEqual(boxObj, (long)boxObj);

    //    Assert.AreNotEqual(iValue.ToString(CultureInfo.InvariantCulture), box.ToString());
    //    Assert.AreEqual(lValue.ToString(CultureInfo.InvariantCulture), box.ToString());
    //    Assert.AreNotEqual(iValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
    //    Assert.AreEqual(lValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
    //    Assert.AreNotEqual(iObj.ToString(), box.ToString());
    //    Assert.AreEqual(lObj.ToString(), box.ToString());
    //    Assert.AreNotEqual(iObj.ToString(), boxObj.ToString());
    //    Assert.AreEqual(lObj.ToString(), boxObj.ToString());

    //    Assert.AreNotEqual(iValue.GetHashCode(), box.GetHashCode());
    //    Assert.AreEqual(lValue.GetHashCode(), box.GetHashCode());
    //    Assert.AreNotEqual(iValue.GetHashCode(), boxObj.GetHashCode());
    //    Assert.AreEqual(lValue.GetHashCode(), boxObj.GetHashCode());
    //    Assert.AreNotEqual(iObj.GetHashCode(), box.GetHashCode());
    //    Assert.AreEqual(lObj.GetHashCode(), box.GetHashCode());
    //    Assert.AreNotEqual(iObj.GetHashCode(), boxObj.GetHashCode());
    //    Assert.AreEqual(lObj.GetHashCode(), boxObj.GetHashCode());

    //    Assert.IsTrue(iValue.Equals(box));
    //    Assert.IsFalse(lValue.Equals(box));
    //    Assert.IsFalse(iValue.Equals(boxObj));
    //    Assert.IsTrue(lValue.Equals(boxObj));
    //    Assert.IsFalse(box.Equals(iValue));
    //    Assert.IsTrue(box.Equals(lValue));
    //    Assert.IsFalse(boxObj.Equals(iValue));
    //    Assert.IsTrue(boxObj.Equals(lValue));

    //    Assert.IsFalse(iObj.Equals(box));
    //    Assert.IsTrue(lObj.Equals(box));
    //    Assert.IsFalse(iObj.Equals(boxObj));
    //    Assert.IsTrue(lObj.Equals(boxObj));
    //    Assert.IsFalse(box.Equals(iObj));
    //    Assert.IsTrue(box.Equals(lObj));
    //    Assert.IsFalse(boxObj.Equals(iObj));
    //    Assert.IsTrue(boxObj.Equals(lObj));

    //    // Testing that unboxing uses a fast process without unnecessary type-checking
    //    _ = (ValueTuple)Unsafe.As<Box<int>, Box<ValueTuple>>(ref box);
    //    _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).GetReference();
    //    _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).GetDangerousReference();
    //}

    /// <summary>
    /// Tests the <see cref="Box{T}"/> type for a given pair of values.
    /// </summary>
    /// <typeparam name="T">The type to test.</typeparam>
    /// <param name="value">The initial <typeparamref name="T"/> value.</param>
    /// <param name="test">The new <typeparamref name="T"/> value to assign and test.</param>
    private static void Test<T>(T value, T test)
        where T : struct
    {
        // Null
        object? obj = null;
        {
            Assert.IsNull(BoxBase.AsBox<T, Box<T>>(obj));
            Assert.IsNull(BoxBase.TryAsBox<T, Box<T>>(obj));
            Assert.IsNull(BoxBase.DangerousAsBox<T, Box<T>>(obj));

            Assert.IsNull(obj.AsBox<T>());
            Assert.IsNull(obj.TryAsBox<T>());
            Assert.IsNull(obj.DangerousAsBox<T>());
        }

        // Not null
        obj = value;

        Box<T>? box;
        object boxObj;
        {
            box = BoxBase.AsBox<T, Box<T>>(obj);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = BoxBase.TryAsBox<T, Box<T>>(obj);
            Assert.IsNotNull(box);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = BoxBase.DangerousAsBox<T, Box<T>>(obj);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = BoxBase.AsBox<T, Box<T>>(value);
            boxObj = box;
            Test();

            box = BoxBase.TryAsBox<T, Box<T>>(value);
            Assert.IsNotNull(box);
            boxObj = box;
            Test();

            box = BoxBase.DangerousAsBox<T, Box<T>>(value);
            boxObj = box;
            Test();
        }

        {
            box = obj.AsBox<T>();
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = obj.TryAsBox<T>();
            Assert.IsNotNull(box);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = obj.DangerousAsBox<T>();
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();

            box = value;
            boxObj = box;
            Test();

            box = value.ToBox();
            boxObj = box;
            Test();

            box = value.AsBox<T>();
            boxObj = box;
            Test();

            box = value.TryAsBox<T>();
            Assert.IsNotNull(box);
            boxObj = box;
            Test();

            box = value.DangerousAsBox<T>();
            boxObj = box;
            Test();
        }

        {
            //var valueT = value;
            box.GetDangerousReference() = test;
            value = test;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();
            //value = valueT;
        }

        // Testing that unboxing uses a fast process without unnecessary type-checking
        _ = (ValueTuple)Unsafe.As<Box<T>, Box<ValueTuple>>(ref box);
        _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).GetReference();
        _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).GetDangerousReference();

        void Test()
        {
            Assert.IsNotNull(box);
            Assert.AreSame(box, boxObj);

            Assert.AreEqual(value, box.GetReference());
            Assert.AreEqual(value, box.GetDangerousReference());
            Assert.AreEqual(value, box.Value);
            Assert.AreEqual(value, (T)box);
            Assert.AreEqual(value, (T)boxObj);

            Assert.AreEqual(obj, box.GetReference());
            Assert.AreEqual(obj, box.GetDangerousReference());
            Assert.AreEqual(obj, box.Value);
            Assert.AreEqual(obj, (T)box);
            Assert.AreEqual(obj, (T)boxObj);

            Assert.AreEqual(boxObj, box.GetReference());
            Assert.AreEqual(boxObj, box.GetDangerousReference());
            Assert.AreEqual(boxObj, box.Value);
            Assert.AreEqual(boxObj, (T)box);
            Assert.AreEqual(boxObj, (T)boxObj);

            Assert.AreEqual(value.ToString(), box.ToString());
            Assert.AreEqual(value.ToString(), boxObj.ToString());
            Assert.AreEqual(obj.ToString(), box.ToString());
            Assert.AreEqual(obj.ToString(), boxObj.ToString());

            Assert.AreEqual(value.GetHashCode(), box.GetHashCode());
            Assert.AreEqual(value.GetHashCode(), boxObj.GetHashCode());
            Assert.AreEqual(obj.GetHashCode(), box.GetHashCode());
            Assert.AreEqual(obj.GetHashCode(), boxObj.GetHashCode());

            Assert.IsTrue(value.Equals(box));
            Assert.IsTrue(value.Equals(boxObj));
            Assert.IsTrue(box.Equals(value));
            Assert.IsTrue(boxObj.Equals(value));

            Assert.IsTrue(obj.Equals(box));
            Assert.IsTrue(obj.Equals(boxObj));
            Assert.IsTrue(box.Equals(obj));
            Assert.IsTrue(boxObj.Equals(obj));
        }
    }

#if NETSTANDARD2_1_OR_GREATER
#pragma warning disable CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
#endif

    internal struct TestStruct : IEquatable<TestStruct>
    {
        public int Number;
        public char Character;
        public string Text;

        /// <inheritdoc/>
        public readonly bool Equals(TestStruct other) =>
            this.Number == other.Number &&
                this.Character == other.Character &&
                this.Text == other.Text;
    }

#if NETSTANDARD2_1_OR_GREATER
#pragma warning restore CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
#endif
}
