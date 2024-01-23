// Licensed to the "DrNet Tips & Tricks" under one or more agreements.
// The "DrNet Tips & Tricks" licenses this file to you under the MIT license.
// See the License.md file in the project root for more information.

namespace CommunityToolkit.HighPerformance.UnitTests;

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BoxOfT_Tests
{
    [TestMethod]
    public void BoxOfT_Types()
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

    [TestMethod]
    public void BoxOfT_Invalid()
    {
        long lValue = 0x0123_4567_89AB_CDEF;
        int iValue = Unsafe.As<long, int>(ref lValue);
        object lObj = lValue;
        object iObj = iValue;

        {
            bool success = Box<int>.TryGetFrom(lValue, out Box<int>? aBox);
            Assert.IsFalse(success);
            Assert.IsNull(aBox);
        }

        {
            bool success = Box<int>.TryGetFrom(lValue, out Box<int>? aBox);
            Assert.IsFalse(success);
            Assert.IsNull(aBox);
        }

        _ = Assert.ThrowsException<InvalidCastException>(() => Box<int>.GetFrom(lObj));

        Box<int> box = Box<int>.DangerousGetFrom(lObj);
        object boxObj = box;
        Assert.AreSame(lObj, box);
        Assert.AreSame(lObj, boxObj);

        Assert.IsNotNull(box);
        Assert.AreSame(box, boxObj);

        Assert.AreEqual(iValue, box.ValueReference());
        Assert.AreEqual(iValue, box.DangerousValueReference());
        Assert.AreNotEqual(lValue, box.ValueReference());
        Assert.AreNotEqual(lValue, box.DangerousValueReference());
        Assert.AreEqual(iObj, box.ValueReference());
        Assert.AreEqual(iObj, box.DangerousValueReference());
        Assert.AreNotEqual(lObj, box.ValueReference());
        Assert.AreNotEqual(lObj, box.DangerousValueReference());
        Assert.AreNotEqual(boxObj, box.ValueReference());
        Assert.AreNotEqual(boxObj, box.DangerousValueReference());

        Assert.AreNotEqual(iValue.ToString(CultureInfo.InvariantCulture), box.ToString());
        Assert.AreEqual(lValue.ToString(CultureInfo.InvariantCulture), box.ToString());
        Assert.AreNotEqual(iValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
        Assert.AreEqual(lValue.ToString(CultureInfo.InvariantCulture), boxObj.ToString());
        Assert.AreNotEqual(iObj.ToString(), box.ToString());
        Assert.AreEqual(lObj.ToString(), box.ToString());
        Assert.AreNotEqual(iObj.ToString(), boxObj.ToString());
        Assert.AreEqual(lObj.ToString(), boxObj.ToString());

        Assert.AreNotEqual(iValue.GetHashCode(), box.GetHashCode());
        Assert.AreEqual(lValue.GetHashCode(), box.GetHashCode());
        Assert.AreNotEqual(iValue.GetHashCode(), boxObj.GetHashCode());
        Assert.AreEqual(lValue.GetHashCode(), boxObj.GetHashCode());
        Assert.AreNotEqual(iObj.GetHashCode(), box.GetHashCode());
        Assert.AreEqual(lObj.GetHashCode(), box.GetHashCode());
        Assert.AreNotEqual(iObj.GetHashCode(), boxObj.GetHashCode());
        Assert.AreEqual(lObj.GetHashCode(), boxObj.GetHashCode());

        Assert.AreEqual(iValue, (int)box);
        Assert.AreNotEqual(lValue, (int)box);
        Assert.ThrowsException<InvalidCastException>(() => (int)boxObj);
        Assert.AreEqual(iObj, (int)box);
        Assert.AreNotEqual(lObj, (int)box);

        Assert.AreEqual(iValue, (long)box);
        Assert.AreNotEqual(lValue, (long)box);
        Assert.AreNotEqual(iValue, (long)boxObj);
        Assert.AreEqual(lValue, (long)boxObj);
        Assert.AreNotEqual(iObj, (long)box);
        Assert.AreNotEqual(lObj, (long)box);
        Assert.AreNotEqual(iObj, (long)boxObj);
        Assert.AreEqual(lObj, (long)boxObj);

        Assert.AreNotEqual(iValue, (object)box);
        Assert.AreEqual(lValue, (object)box);
        Assert.AreNotEqual(iValue, boxObj);
        Assert.AreEqual(lValue, boxObj);
        Assert.AreNotEqual(iObj, box);
        Assert.AreEqual(lObj, box);
        Assert.AreNotEqual(iObj, boxObj);
        Assert.AreEqual(lObj, boxObj);

        Assert.IsTrue(iValue.Equals(box));
        Assert.IsFalse(lValue.Equals(box));
        Assert.IsFalse(iValue.Equals(boxObj));
        Assert.IsTrue(lValue.Equals(boxObj));
        Assert.IsFalse(box.Equals(iValue));
        Assert.IsTrue(box.Equals(lValue));
        Assert.IsFalse(boxObj.Equals(iValue));
        Assert.IsTrue(boxObj.Equals(lValue));

        Assert.IsFalse(iObj.Equals(box));
        Assert.IsTrue(lObj.Equals(box));
        Assert.IsFalse(iObj.Equals(boxObj));
        Assert.IsTrue(lObj.Equals(boxObj));
        Assert.IsFalse(box.Equals(iObj));
        Assert.IsTrue(box.Equals(lObj));
        Assert.IsFalse(boxObj.Equals(iObj));
        Assert.IsTrue(boxObj.Equals(lObj));

        // Testing that unboxing uses a fast process without unnecessary type-checking
        _ = (ValueTuple)Unsafe.As<Box<int>, Box<ValueTuple>>(ref box);
        _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).ValueReference();
        _ = Unsafe.As<Box<int>, Box<ValueTuple>>(ref box).DangerousValueReference();
    }

    /// <summary>
    /// Tests the <see cref="Box{T}"/> type for a given pair of values.
    /// </summary>
    /// <typeparam name="T">The type to test.</typeparam>
    /// <param name="value">The initial <typeparamref name="T"/> value.</param>
    /// <param name="test">The new <typeparamref name="T"/> value to assign and test.</param>
    private static void Test<T>(T value, T test)
        where T : struct
    {
        Box<T> box;
        object boxObj;
        object obj = value;

        {
            box = value;
            boxObj = box;
            Test();
        }

        {
            Assert.IsTrue(Box<T>.TryGetFrom(obj, out Box<T>? aBox));
            box = aBox;
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();
        }


        {
            box = Box<T>.GetFrom(obj);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();
        }

        {
            box = Box<T>.DangerousGetFrom(obj);
            boxObj = box;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();
        }

        {
            //var valueT = value;
            box.DangerousValueReference() = test;
            value = test;
            Assert.AreSame(obj, box);
            Assert.AreSame(obj, boxObj);
            Test();
            //value = valueT;
        }

        // Testing that unboxing uses a fast process without unnecessary type-checking
        _ = (ValueTuple)Unsafe.As<Box<T>, Box<ValueTuple>>(ref box);
        _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).ValueReference();
        _ = Unsafe.As<Box<T>, Box<ValueTuple>>(ref box).DangerousValueReference();

        void Test()
        {
            Assert.IsNotNull(box);
            Assert.AreSame(box, boxObj);

            Assert.AreEqual(value, box.ValueReference());
            Assert.AreEqual(obj, box.ValueReference());
            Assert.AreEqual(boxObj, box.ValueReference());

            Assert.AreEqual(value.ToString(), box.ToString());
            Assert.AreEqual(value.ToString(), boxObj.ToString());
            Assert.AreEqual(obj.ToString(), box.ToString());
            Assert.AreEqual(obj.ToString(), boxObj.ToString());

            Assert.AreEqual(value.GetHashCode(), box.GetHashCode());
            Assert.AreEqual(value.GetHashCode(), boxObj.GetHashCode());
            Assert.AreEqual(obj.GetHashCode(), box.GetHashCode());
            Assert.AreEqual(obj.GetHashCode(), boxObj.GetHashCode());

            Assert.AreEqual(value, (T)box);
            Assert.AreEqual(value, boxObj);
            Assert.AreEqual(obj, (T)box);
            Assert.AreEqual(obj, boxObj);

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

#pragma warning disable CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
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
#pragma warning restore CA1067 // Override Object.Equals(object) when implementing IEquatable<T>
}
