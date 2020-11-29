using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector2Fix16Tests
    {
        #region Public Methods

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new int[] {(int)4.0, (int)7.0}).AsSpan();
            var vec = span.ToVector2Fix16();

            Assert.That(vec.X, Is.EqualTo(span[0]));
            Assert.That(vec.Y, Is.EqualTo(span[1]));
        }

        //[Test]
        //public void Vector2AbsTest()
        //{
        //    Vector2Fix16 v1 = new Vector2Fix16(-2.5f, 2.0f);
        //    Vector2Fix16 v3 = Vector2Fix16.Abs(new Vector2Fix16(0.0f, double.NegativeInfinity));
        //    Vector2Fix16 v = Vector2Fix16.Abs(v1);
        //    Assert.AreEqual(2.5f, v.X);
        //    Assert.AreEqual(2.0f, v.Y);
        //    Assert.AreEqual(0.0f, v3.X);
        //    Assert.AreEqual(double.PositiveInfinity, v3.Y);
        //}

        //// A test for operator + (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2AdditionTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.0f, 4.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(4.0f, 6.0f);
        //    Vector2Fix16 actual;

        //    actual = a + b;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator + did not return the expected value.");
        //}

        //// A test for Add (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2AddTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(5.0f, 6.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(6.0f, 8.0f);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Add(a, b);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Clamp (Vector2Fix16, Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2ClampTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.5f, 0.3f);
        //    Vector2Fix16 min = new Vector2Fix16(0.0f, 0.1f);
        //    Vector2Fix16 max = new Vector2Fix16(1.0f, 1.1f);

        //    // Normal case.
        //    // Case N1: specified value is in the range.
        //    Vector2Fix16 expected = new Vector2Fix16(0.5f, 0.3f);
        //    Vector2Fix16 actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");
        //    // Normal case.
        //    // Case N2: specified value is bigger than max value.
        //    a = new Vector2Fix16(2.0f, 3.0f);
        //    expected = max;
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");
        //    // Case N3: specified value is smaller than max value.
        //    a = new Vector2Fix16(-1.0f, -2.0f);
        //    expected = min;
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");
        //    // Case N4: combination case.
        //    a = new Vector2Fix16(-2.0f, 4.0f);
        //    expected = new Vector2Fix16(min.X, max.Y);
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");
        //    // User specified min value is bigger than max value.
        //    max = new Vector2Fix16(0.0f, 0.1f);
        //    min = new Vector2Fix16(1.0f, 1.1f);

        //    // Case W1: specified value is in the range.
        //    a = new Vector2Fix16(0.5f, 0.3f);
        //    expected = max;
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");

        //    // Normal case.
        //    // Case W2: specified value is bigger than max and min value.
        //    a = new Vector2Fix16(2.0f, 3.0f);
        //    expected = max;
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");

        //    // Case W3: specified value is smaller than min and max value.
        //    a = new Vector2Fix16(-1.0f, -2.0f);
        //    expected = max;
        //    actual = Vector2Fix16.Clamp(a, min, max);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Clamp did not return the expected value.");
        //}

        //// A test for Vector2Fix16 (double, double)
        //[Test]
        //public void Vector2ConstructorTest()
        //{
        //    double x = 1.0f;
        //    double y = 2.0f;

        //    Vector2Fix16 target = new Vector2Fix16(x, y);
        //    Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y), "Vector2Fix16(x,y) constructor did not return the expected value.");
        //}

        //// A test for Vector2Fix16 ()
        //// Constructor with no parameter
        //[Test]
        //public void Vector2ConstructorTest2()
        //{
        //    Vector2Fix16 target = new Vector2Fix16();
        //    Assert.AreEqual(0.0f, target.X);
        //    Assert.AreEqual(0.0f, target.Y);
        //}

        //// A test for Vector2Fix16 (double, double)
        //// Constructor with special doubleing values
        //[Test]
        //public void Vector2ConstructorTest3()
        //{
        //    Vector2Fix16 target = new Vector2Fix16(double.NaN, double.MaxValue);
        //    Assert.AreEqual(target.X, double.NaN);
        //    Assert.AreEqual(target.Y, double.MaxValue);
        //}

        //// A test for Vector2Fix16 (double)
        //[Test]
        //public void Vector2ConstructorTest4()
        //{
        //    double value = 1.0f;
        //    Vector2Fix16 target = new Vector2Fix16(value);

        //    Vector2Fix16 expected = new Vector2Fix16(value, value);
        //    Assert.AreEqual(expected, target);

        //    value = 2.0f;
        //    target = new Vector2Fix16(value);
        //    expected = new Vector2Fix16(value, value);
        //    Assert.AreEqual(expected, target);
        //}

        //[Test]
        //public void Vector2CopyToTest()
        //{
        //    Vector2Fix16 v1 = new Vector2Fix16(2.0f, 3.0f);

        //    double[] a = new double[3];
        //    double[] b = new double[2];

        //    Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
        //    Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
        //    Assert.Throws<ArgumentException>(() => v1.CopyTo(a, 2));

        //    v1.CopyTo(a, 1);
        //    v1.CopyTo(b);
        //    Assert.AreEqual(0.0, a[0]);
        //    Assert.AreEqual(2.0, a[1]);
        //    Assert.AreEqual(3.0, a[2]);
        //    Assert.AreEqual(2.0, b[0]);
        //    Assert.AreEqual(3.0, b[1]);
        //}

        //// A test for DistanceSquared (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2Fix16istanceSquaredTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.0f, 4.0f);

        //    double expected = 8.0f;
        //    double actual;

        //    actual = Vector2Fix16.DistanceSquared(a, b);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.DistanceSquared did not return the expected value.");
        //}

        //// A test for Distance (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2Fix16istanceTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.0f, 4.0f);

        //    double expected = (double)System.Math.Sqrt(8);
        //    double actual;

        //    actual = Vector2Fix16.Distance(a, b);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Distance did not return the expected value.");
        //}

        //// A test for Distance (Vector2Fix16, Vector2Fix16)
        //// Distance from the same point
        //[Test]
        //public void Vector2Fix16istanceTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.051f, 2.05f);
        //    Vector2Fix16 b = new Vector2Fix16(1.051f, 2.05f);

        //    double actual = Vector2Fix16.Distance(a, b);
        //    Assert.AreEqual(0.0f, actual);
        //}

        //// A test for Divide (Vector2Fix16, double)
        //[Test]
        //public void Vector2Fix16ivideTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    double div = 2.0f;
        //    Vector2Fix16 expected = new Vector2Fix16(0.5f, 1.0f);
        //    Vector2Fix16 actual;
        //    actual = Vector2Fix16.Divide(a, div);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Divide (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2Fix16ivideTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0, 6.0);
        //    Vector2Fix16 b = new Vector2Fix16(5.0, 2.0);

        //    Vector2Fix16 expected = new Vector2Fix16(1.0 / 5.0, 6.0 / 2.0);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Divide(a, b);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for operator / (Vector2Fix16, double)
        //[Test]
        //public void Vector2Fix16ivisionTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);

        //    double div = 2.0f;

        //    Vector2Fix16 expected = new Vector2Fix16(1.0f, 1.5f);
        //    Vector2Fix16 actual;

        //    actual = a / div;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator / did not return the expected value.");
        //}

        //// A test for operator / (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2Fix16ivisionTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 b = new Vector2Fix16(4.0f, 5.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(2.0f / 4.0f, 3.0f / 5.0f);
        //    Vector2Fix16 actual;

        //    actual = a / b;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator / did not return the expected value.");
        //}

        //// A test for operator / (Vector2Fix16, double)
        //// Divide by zero
        //[Test]
        //public void Vector2Fix16ivisionTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(-2.0f, 3.0f);

        //    double div = 0.0f;

        //    Vector2Fix16 actual = a / div;

        //    Assert.True(double.IsNegativeInfinity(actual.X), "Vector2Fix16.operator / did not return the expected value.");
        //    Assert.True(double.IsPositiveInfinity(actual.Y), "Vector2Fix16.operator / did not return the expected value.");
        //}

        //// A test for operator / (Vector2Fix16, Vector2Fix16)
        //// Divide by zero
        //[Test]
        //public void Vector2Fix16ivisionTest3()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.047f, -3.0f);
        //    Vector2Fix16 b = new Vector2Fix16();

        //    Vector2Fix16 actual = a / b;

        //    Assert.True(double.IsInfinity(actual.X), "Vector2Fix16.operator / did not return the expected value.");
        //    Assert.True(double.IsInfinity(actual.Y), "Vector2Fix16.operator / did not return the expected value.");
        //}

        //// A test for Dot (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2Fix16otTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.0f, 4.0f);

        //    double expected = 11.0f;
        //    double actual;

        //    actual = Vector2Fix16.Dot(a, b);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Dot did not return the expected value.");
        //}

        //// A test for Dot (Vector2Fix16, Vector2Fix16)
        //// Dot test for perpendicular vector
        //[Test]
        //public void Vector2Fix16otTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.55f, 1.55f);
        //    Vector2Fix16 b = new Vector2Fix16(-1.55f, 1.55f);

        //    double expected = 0.0f;
        //    double actual = Vector2Fix16.Dot(a, b);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Dot (Vector2Fix16, Vector2Fix16)
        //// Dot test with specail double values
        //[Test]
        //public void Vector2Fix16otTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(double.MinValue, double.MinValue);
        //    Vector2Fix16 b = new Vector2Fix16(double.MaxValue, double.MaxValue);

        //    double actual = Vector2Fix16.Dot(a, b);
        //    Assert.True(double.IsNegativeInfinity(actual), "Vector2Fix16.Dot did not return the expected value.");
        //}

        //// A test for operator == (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2EqualityTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(1.0f, 2.0f);

        //    // case 1: compare between same values
        //    bool expected = true;
        //    bool actual = a == b;
        //    Assert.AreEqual(expected, actual);

        //    // case 2: compare between different values
        //    b.X = 10.0f;
        //    expected = false;
        //    actual = a == b;
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Vector2Fix16 comparison involving NaN values
        //[Test]
        //public void Vector2EqualsNanTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(double.NaN, 0);
        //    Vector2Fix16 b = new Vector2Fix16(0, double.NaN);

        //    Assert.False(a == Vector2Fix16.Zero);
        //    Assert.False(b == Vector2Fix16.Zero);

        //    Assert.True(a != Vector2Fix16.Zero);
        //    Assert.True(b != Vector2Fix16.Zero);

        //    Assert.False(a.Equals(Vector2Fix16.Zero));
        //    Assert.False(b.Equals(Vector2Fix16.Zero));

        //    // Counterintuitive result - IEEE rules for NaN comparison are weird!
        //    Assert.False(a.Equals(a));
        //    Assert.False(b.Equals(b));
        //}

        //// A test for Equals (object)
        //[Test]
        //public void Vector2EqualsTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0, 2.0);
        //    Vector2Fix16 b = new Vector2Fix16(1.0, 2.0);

        //    // case 1: compare between same values
        //    object? obj = b;

        //    bool expected = true;
        //    bool actual = a.Equals(obj);
        //    Assert.AreEqual(expected, actual);

        //    // case 2: compare between different values
        //    b.X = 10.0;
        //    obj = b;
        //    expected = false;
        //    actual = a.Equals(obj);
        //    Assert.AreEqual(expected, actual);

        //    // case 3: compare between different types.
        //    obj = new QuaternionD();
        //    expected = false;
        //    actual = a.Equals(obj);
        //    Assert.AreEqual(expected, actual);

        //    // case 3: compare against null.
        //    obj = null;
        //    expected = false;
        //    actual = a.Equals(obj);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Equals (Vector2Fix16)
        //[Test]
        //public void Vector2EqualsTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(1.0f, 2.0f);

        //    // case 1: compare between same values
        //    bool expected = true;
        //    bool actual = a.Equals(b);
        //    Assert.AreEqual(expected, actual);

        //    // case 2: compare between different values
        //    b.X = 10.0f;
        //    expected = false;
        //    actual = a.Equals(b);
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Vector2GetHashCodeTest()
        //{
        //    Vector2Fix16 v1 = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 v2 = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 v3 = new Vector2Fix16(3.0f, 2.0f);
        //    Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
        //    Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
        //    Assert.AreNotEqual(v1.GetHashCode(), v3.GetHashCode());
        //    Vector2Fix16 v4 = new Vector2Fix16(0.0f, 0.0f);
        //    Vector2Fix16 v6 = new Vector2Fix16(1.0f, 0.0f);
        //    Vector2Fix16 v7 = new Vector2Fix16(0.0f, 1.0f);
        //    Vector2Fix16 v8 = new Vector2Fix16(1.0f, 1.0f);
        //    Assert.AreNotEqual(v4.GetHashCode(), v6.GetHashCode());
        //    Assert.AreNotEqual(v4.GetHashCode(), v7.GetHashCode());
        //    Assert.AreNotEqual(v4.GetHashCode(), v8.GetHashCode());
        //    Assert.AreNotEqual(v7.GetHashCode(), v6.GetHashCode());
        //    Assert.AreNotEqual(v8.GetHashCode(), v6.GetHashCode());
        //    Assert.AreNotEqual(v8.GetHashCode(), v7.GetHashCode());
        //}

        //// A test for operator != (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2InequalityTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(1.0f, 2.0f);

        //    // case 1: compare between same values
        //    bool expected = false;
        //    bool actual = a != b;
        //    Assert.AreEqual(expected, actual);

        //    // case 2: compare between different values
        //    b.X = 10.0f;
        //    expected = true;
        //    actual = a != b;
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for LengthSquared ()
        //[Test]
        //public void Vector2LengthSquaredTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 4.0f);

        //    Vector2Fix16 target = a;

        //    double expected = 20.0f;
        //    double actual;

        //    actual = target.LengthSquared();

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.LengthSquared did not return the expected value.");
        //}

        //// A test for LengthSquared ()
        //// LengthSquared test where the result is zero
        //[Test]
        //public void Vector2LengthSquaredTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.0f, 0.0f);

        //    double expected = 0.0f;
        //    double actual = a.LengthSquared();

        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Length ()
        //[Test]
        //public void Vector2LengthTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 4.0f);

        //    Vector2Fix16 target = a;

        //    double expected = (double)System.Math.Sqrt(20);
        //    double actual;

        //    actual = target.Length();

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Length did not return the expected value.");
        //}

        //// A test for Length ()
        //// Length test where length is zero
        //[Test]
        //public void Vector2LengthTest1()
        //{
        //    Vector2Fix16 target = new Vector2Fix16(0.0);

        //    double expected = 0.0;
        //    double actual = target.Length();

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Length did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //[Test]
        //public void Vector2LerpTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.0f, 4.0f);

        //    double t = 0.5f;

        //    Vector2Fix16 expected = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 actual;
        //    actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with factor zero
        //[Test]
        //public void Vector2LerpTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.0f, 0.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.18f, 4.25f);

        //    double t = 0.0f;
        //    Vector2Fix16 expected = Vector2Fix16.Zero;
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with factor one
        //[Test]
        //public void Vector2LerpTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.0f, 0.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.18f, 4.25f);

        //    double t = 1.0f;
        //    Vector2Fix16 expected = new Vector2Fix16(3.18f, 4.25f);
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with factor > 1
        //[Test]
        //public void Vector2LerpTest3()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.0f, 0.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.18f, 4.25f);

        //    double t = 2.0f;
        //    Vector2Fix16 expected = b * 2.0f;
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with factor < 0
        //[Test]
        //public void Vector2LerpTest4()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.0f, 0.0f);
        //    Vector2Fix16 b = new Vector2Fix16(3.18f, 4.25f);

        //    double t = -2.0f;
        //    Vector2Fix16 expected = -(b * 2.0f);
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with special double value
        //[Test]
        //public void Vector2LerpTest5()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(45.67f, 90.0f);
        //    Vector2Fix16 b = new Vector2Fix16(double.PositiveInfinity, double.NegativeInfinity);

        //    double t = 0.408f;
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(double.IsPositiveInfinity(actual.X), "Vector2Fix16.Lerp did not return the expected value.");
        //    Assert.True(double.IsNegativeInfinity(actual.Y), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test from the same point
        //[Test]
        //public void Vector2LerpTest6()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 b = new Vector2Fix16(1.0f, 2.0f);

        //    double t = 0.5f;

        //    Vector2Fix16 expected = new Vector2Fix16(1.0f, 2.0f);
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with values known to be innacurate with the old lerp impl
        //[Test]
        //public void Vector2LerpTest7()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(0.44728136f);
        //    Vector2Fix16 b = new Vector2Fix16(0.46345946f);

        //    double t = 0.26402435f;

        //    Vector2Fix16 expected = new Vector2Fix16(0.45155275f);
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //// A test for Lerp (Vector2Fix16, Vector2Fix16, double)
        //// Lerp test with values known to be innacurate with the old lerp impl
        //// (Old code incorrectly gets 0.33333588)
        //[Test]
        //public void Vector2LerpTest8()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(-100);
        //    Vector2Fix16 b = new Vector2Fix16(0.33333334f);

        //    double t = 1f;

        //    Vector2Fix16 expected = new Vector2Fix16(0.33333334f);
        //    Vector2Fix16 actual = Vector2Fix16.Lerp(a, b, t);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Lerp did not return the expected value.");
        //}

        //[Test]
        //public void Vector2MarshalSizeTest()
        //{
        //    Assert.AreEqual(16, Marshal.SizeOf<Vector2Fix16>());
        //    Assert.AreEqual(16, Marshal.SizeOf<Vector2Fix16>(new Vector2Fix16()));
        //}

        //// A test for Max (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2MaxTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(-1.0f, 4.0f);
        //    Vector2Fix16 b = new Vector2Fix16(2.0f, 1.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(2.0f, 4.0f);
        //    Vector2Fix16 actual;
        //    actual = Vector2Fix16.Max(a, b);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Max did not return the expected value.");
        //}

        //[Test]
        //public void Vector2MinMaxCodeCoverageTest()
        //{
        //    Vector2Fix16 min = new Vector2Fix16(0, 0);
        //    Vector2Fix16 max = new Vector2Fix16(1, 1);
        //    Vector2Fix16 actual;

        //    // Min.
        //    actual = Vector2Fix16.Min(min, max);
        //    Assert.AreEqual(actual, min);

        //    actual = Vector2Fix16.Min(max, min);
        //    Assert.AreEqual(actual, min);

        //    // Max.
        //    actual = Vector2Fix16.Max(min, max);
        //    Assert.AreEqual(actual, max);

        //    actual = Vector2Fix16.Max(max, min);
        //    Assert.AreEqual(actual, max);
        //}

        //// A test for Min (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2MinTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(-1.0f, 4.0f);
        //    Vector2Fix16 b = new Vector2Fix16(2.0f, 1.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(-1.0f, 1.0f);
        //    Vector2Fix16 actual;
        //    actual = Vector2Fix16.Min(a, b);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Min did not return the expected value.");
        //}

        //// A test for operator * (Vector2Fix16, double)
        //[Test]
        //public void Vector2MultiplyOperatorTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);
        //    const double factor = 2.0f;

        //    Vector2Fix16 expected = new Vector2Fix16(4.0f, 6.0f);
        //    Vector2Fix16 actual;

        //    actual = a * factor;
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator * did not return the expected value.");
        //}

        //// A test for operator * (double, Vector2Fix16)
        //[Test]
        //public void Vector2MultiplyOperatorTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);
        //    const double factor = 2.0f;

        //    Vector2Fix16 expected = new Vector2Fix16(4.0f, 6.0f);
        //    Vector2Fix16 actual;

        //    actual = factor * a;
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator * did not return the expected value.");
        //}

        //// A test for operator * (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2MultiplyOperatorTest3()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 b = new Vector2Fix16(4.0f, 5.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(8.0f, 15.0f);
        //    Vector2Fix16 actual;

        //    actual = a * b;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator * did not return the expected value.");
        //}

        //// A test for Multiply (Vector2Fix16, double)
        //[Test]
        //public void Vector2MultiplyTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0, 2.0);
        //    const double factor = 2.0;
        //    Vector2Fix16 expected = new Vector2Fix16(2.0, 4.0);
        //    Vector2Fix16 actual = Vector2Fix16.Multiply(a, factor);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Multiply (double, Vector2Fix16)
        //[Test]
        //public void Vector2MultiplyTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0, 2.0);
        //    const double factor = 2.0;
        //    Vector2Fix16 expected = new Vector2Fix16(2.0, 4.0);
        //    Vector2Fix16 actual = Vector2Fix16.Multiply(factor, a);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Multiply (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2MultiplyTest3()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0, 2.0);
        //    Vector2Fix16 b = new Vector2Fix16(5.0, 6.0);

        //    Vector2Fix16 expected = new Vector2Fix16(5.0, 12.0);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Multiply(a, b);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Negate (Vector2Fix16)
        //[Test]
        //public void Vector2NegateTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(-1.0f, -2.0f);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Negate(a);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for Normalize (Vector2Fix16)
        //[Test]
        //public void Vector2NormalizeTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(2.0f, 3.0f);
        //    Vector2Fix16 expected = new Vector2Fix16(0.554700196225229122018341733457f, 0.8320502943378436830275126001855f);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Normalize(a);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Normalize did not return the expected value.");
        //}

        //// A test for Normalize (Vector2Fix16)
        //// Normalize zero length vector
        //[Test]
        //public void Vector2NormalizeTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(); // no parameter, default to 0.0f
        //    Vector2Fix16 actual = Vector2Fix16.Normalize(a);
        //    Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y), "Vector2Fix16.Normalize did not return the expected value.");
        //}

        //// A test for Normalize (Vector2Fix16)
        //// Normalize infinite length vector
        //[Test]
        //public void Vector2NormalizeTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(double.MaxValue, double.MaxValue);
        //    Vector2Fix16 actual = Vector2Fix16.Normalize(a);
        //    Vector2Fix16 expected = new Vector2Fix16(0, 0);
        //    Assert.AreEqual(expected, actual);
        //}

        //// A test for One
        //[Test]
        //public void Vector2OneTest()
        //{
        //    Vector2Fix16 val = new Vector2Fix16(1.0f, 1.0f);
        //    Assert.AreEqual(val, Vector2Fix16.One);
        //}

        //// A test for Reflect (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2ReflectTest()
        //{
        //    Vector2Fix16 a = Vector2Fix16.Normalize(new Vector2Fix16(1.0f, 1.0f));

        //    // Reflect on XZ PlaneD.
        //    Vector2Fix16 n = new Vector2Fix16(0.0f, 1.0f);
        //    Vector2Fix16 expected = new Vector2Fix16(a.X, -a.Y);
        //    Vector2Fix16 actual = Vector2Fix16.Reflect(a, n);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Reflect did not return the expected value.");

        //    // Reflect on XY PlaneD.
        //    n = new Vector2Fix16(0.0f, 0.0f);
        //    expected = new Vector2Fix16(a.X, a.Y);
        //    actual = Vector2Fix16.Reflect(a, n);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Reflect did not return the expected value.");

        //    // Reflect on YZ PlaneD.
        //    n = new Vector2Fix16(1.0f, 0.0f);
        //    expected = new Vector2Fix16(-a.X, a.Y);
        //    actual = Vector2Fix16.Reflect(a, n);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Reflect did not return the expected value.");
        //}

        //// A test for Reflect (Vector2Fix16, Vector2Fix16)
        //// Reflection when normal and source are the same
        //[Test]
        //public void Vector2ReflectTest1()
        //{
        //    Vector2Fix16 n = new Vector2Fix16(0.45f, 1.28f);
        //    n = Vector2Fix16.Normalize(n);
        //    Vector2Fix16 a = n;

        //    Vector2Fix16 expected = -n;
        //    Vector2Fix16 actual = Vector2Fix16.Reflect(a, n);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Reflect did not return the expected value.");
        //}

        //// A test for Reflect (Vector2Fix16, Vector2Fix16)
        //// Reflection when normal and source are negation
        //[Test]
        //public void Vector2ReflectTest2()
        //{
        //    Vector2Fix16 n = new Vector2Fix16(0.45f, 1.28f);
        //    n = Vector2Fix16.Normalize(n);
        //    Vector2Fix16 a = -n;

        //    Vector2Fix16 expected = n;
        //    Vector2Fix16 actual = Vector2Fix16.Reflect(a, n);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Reflect did not return the expected value.");
        //}

        //// A test to make sure these types are blittable directly into GPU buffer memory layouts
        //[Test]
        //public unsafe void Vector2SizeofTest()
        //{
        //    Assert.AreEqual(16, sizeof(Vector2Fix16));
        //    Assert.AreEqual(32, sizeof(Vector2_2x));
        //    Assert.AreEqual(24, sizeof(Vector2Plusdouble));
        //    Assert.AreEqual(48, sizeof(Vector2Plusdouble_2x));
        //}

        //[Test]
        //public void Vector2SqrtTest()
        //{
        //    Vector2Fix16 v1 = new Vector2Fix16(-2.5f, 2.0f);
        //    Vector2Fix16 v2 = new Vector2Fix16(5.5f, 4.5f);
        //    Assert.AreEqual(2, (int)Vector2Fix16.SquareRoot(v2).X);
        //    Assert.AreEqual(2, (int)Vector2Fix16.SquareRoot(v2).Y);
        //    Assert.AreEqual(double.NaN, Vector2Fix16.SquareRoot(v1).X);
        //}

        //// A test for operator - (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2SubtractionTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 3.0f);
        //    Vector2Fix16 b = new Vector2Fix16(2.0f, 1.5f);

        //    Vector2Fix16 expected = new Vector2Fix16(-1.0f, 1.5f);
        //    Vector2Fix16 actual;

        //    actual = a - b;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator - did not return the expected value.");
        //}

        //// A test for Subtract (Vector2Fix16, Vector2Fix16)
        //[Test]
        //public void Vector2SubtractTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 6.0f);
        //    Vector2Fix16 b = new Vector2Fix16(5.0f, 2.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(-4.0f, 4.0f);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Subtract(a, b);
        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Vector2ToStringTest()
        //{
        //    string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
        //    CultureInfo enUsCultureInfo = new CultureInfo("en-US");

        //    Vector2Fix16 v1 = new Vector2Fix16(2.0f, 3.0f);

        //    string v1str = v1.ToString();
        //    string expectedv1 = string.Format(CultureInfo.CurrentCulture
        //        , "<{1:G}{0} {2:G}>"
        //        , new object[] { separator, 2, 3 });
        //    Assert.AreEqual(expectedv1, v1str);

        //    string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
        //    string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
        //        , "<{1:c}{0} {2:c}>"
        //        , new object[] { separator, 2, 3 });
        //    Assert.AreEqual(expectedv1formatted, v1strformatted);

        //    string v2strformatted = v1.ToString("c", enUsCultureInfo);
        //    string expectedv2formatted = string.Format(enUsCultureInfo
        //        , "<{1:c}{0} {2:c}>"
        //        , new object[] { enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2, 3 });
        //    Assert.AreEqual(expectedv2formatted, v2strformatted);

        //    string v3strformatted = v1.ToString("c");
        //    string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
        //        , "<{1:c}{0} {2:c}>"
        //        , new object[] { separator, 2, 3 });
        //    Assert.AreEqual(expectedv3formatted, v3strformatted);
        //}

        //// A test for Transform (Vector2Fix16, QuaternionD)
        //[Test]
        //public void Vector2TransformByQuaternionTest()
        //{
        //    Vector2Fix16 v = new Vector2Fix16(1.0, 2.0);

        //    Matrix4x4D m =
        //        Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0));
        //    QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

        //    Vector2Fix16 expected = Vector2Fix16.Transform(v, m);
        //    Vector2Fix16 actual = Vector2Fix16.Transform(v, q);
        //    Assert.That(actual, Is.EqualTo(expected).Using<Vector2Fix16>((a, b) => a.Equals(b, 1e-15)), "Vector2Fix16.Transform did not return the expected value.");
        //}

        ////    actual = Vector2Fix16.TransformNormal(v, m);
        ////    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Transform did not return the expected value.");
        ////}
        //// A test for Transform (Vector2Fix16, QuaternionD)
        //// Transform Vector2Fix16 with zero QuaternionD
        //[Test]
        //public void Vector2TransformByQuaternionTest1()
        //{
        //    Vector2Fix16 v = new Vector2Fix16(1.0f, 2.0f);
        //    QuaternionD q = new QuaternionD();
        //    Vector2Fix16 expected = v;

        //    Vector2Fix16 actual = Vector2Fix16.Transform(v, q);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Transform did not return the expected value.");
        //}

        ////    Vector2Fix16 expected = new Vector2Fix16(-0.133974612f, 2.232051f);
        ////    Vector2Fix16 actual;
        //// A test for Transform (Vector2Fix16, QuaternionD)
        //// Transform Vector2Fix16 with identity QuaternionD
        //[Test]
        //public void Vector2TransformByQuaternionTest2()
        //{
        //    Vector2Fix16 v = new Vector2Fix16(1.0f, 2.0f);
        //    QuaternionD q = QuaternionD.Identity;
        //    Vector2Fix16 expected = v;

        //    Vector2Fix16 actual = Vector2Fix16.Transform(v, q);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Transform did not return the expected value.");
        //}

        //// A test for TransformNormal (Vector2Fix16, Matrix4x4D)
        //[Test]
        //public void Vector2TransformNormalTest()
        //{
        //    Vector2Fix16 v = new Vector2Fix16(1.0, 2.0);
        //    Matrix4x4D m =
        //        Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0));
        //    m.M41 = 10.0;
        //    m.M42 = 20.0;
        //    m.M43 = 30.0;

        //    Vector2Fix16 expected = new Vector2Fix16(0.31698729810778076, 2.1830127018922196);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.TransformNormal(v, m);
        //    Assert.AreEqual(expected, actual, "Vector2Fix16.Tranform did not return the expected value.");
        //}

        //// A test for Transform(Vector2Fix16, Matrix4x4D)
        //[Test]
        //public void Vector2TransformTest()
        //{
        //    Vector2Fix16 v = new Vector2Fix16(1.0, 2.0);
        //    Matrix4x4D m =
        //        Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
        //        Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0));
        //    m.M41 = 10.0;
        //    m.M42 = 20.0;
        //    m.M43 = 30.0;

        //    Vector2Fix16 expected = new Vector2Fix16(10.316987298107781, 22.18301270189222);
        //    Vector2Fix16 actual;

        //    actual = Vector2Fix16.Transform(v, m);
        //    Assert.AreEqual(expected, actual, "Vector2Fix16.Transform did not return the expected value.");
        //}

        ////// A test for Transform(Vector2Fix16, Matrix3x2)
        ////[Test]
        ////public void Vector2Transform3x2Test()
        ////{
        ////    Vector2Fix16 v = new Vector2Fix16(1.0f, 2.0f);
        ////    Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0f));
        ////    m.M31 = 10.0f;
        ////    m.M32 = 20.0f;

        ////    Vector2Fix16 expected = new Vector2Fix16(9.866025f, 22.23205f);
        ////    Vector2Fix16 actual;

        ////    actual = Vector2Fix16.Transform(v, m);
        ////    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.Transform did not return the expected value.");
        ////}
        ////// A test for TransformNormal (Vector2Fix16, Matrix3x2)
        ////[Test]
        ////public void Vector2TransformNormal3x2Test()
        ////{
        ////    Vector2Fix16 v = new Vector2Fix16(1.0f, 2.0f);
        ////    Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0f));
        ////    m.M31 = 10.0f;
        ////    m.M32 = 20.0f;
        //// A test for operator - (Vector2Fix16)
        //[Test]
        //public void Vector2UnaryNegationTest()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(1.0f, 2.0f);

        //    Vector2Fix16 expected = new Vector2Fix16(-1.0f, -2.0f);
        //    Vector2Fix16 actual;

        //    actual = -a;

        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2Fix16.operator - did not return the expected value.");
        //}

        //// A test for operator - (Vector2Fix16)
        //// Negate test with special double value
        //[Test]
        //public void Vector2UnaryNegationTest1()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(double.PositiveInfinity, double.NegativeInfinity);

        //    Vector2Fix16 actual = -a;

        //    Assert.True(double.IsNegativeInfinity(actual.X), "Vector2Fix16.operator - did not return the expected value.");
        //    Assert.True(double.IsPositiveInfinity(actual.Y), "Vector2Fix16.operator - did not return the expected value.");
        //}

        //// A test for operator - (Vector2Fix16)
        //// Negate test with special double value
        //[Test]
        //public void Vector2UnaryNegationTest2()
        //{
        //    Vector2Fix16 a = new Vector2Fix16(double.NaN, 0.0f);
        //    Vector2Fix16 actual = -a;

        //    Assert.That(actual.X, Is.NaN, "Vector2Fix16.operator - did not return the expected value.");
        //    Assert.AreEqual(0.0f, actual.Y, "Vector2Fix16.operator - did not return the expected value.");
        //}

        //// A test for UnitX
        //[Test]
        //public void Vector2UnitXTest()
        //{
        //    Vector2Fix16 val = new Vector2Fix16(1.0f, 0.0f);
        //    Assert.AreEqual(val, Vector2Fix16.UnitX);
        //}

        //// A test for UnitY
        //[Test]
        //public void Vector2UnitYTest()
        //{
        //    Vector2Fix16 val = new Vector2Fix16(0.0f, 1.0f);
        //    Assert.AreEqual(val, Vector2Fix16.UnitY);
        //}

        //// A test for Zero
        //[Test]
        //public void Vector2ZeroTest()
        //{
        //    Vector2Fix16 val = new Vector2Fix16(0.0f, 0.0f);
        //    Assert.AreEqual(val, Vector2Fix16.Zero);
        //}

        #endregion Public Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2_2x
        {
            private Vector2Fix16 _a;
            private Vector2Fix16 _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2Plusdouble
        {
            private Vector2Fix16 _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2Plusdouble_2x
        {
            private Vector2Plusdouble _a;
            private Vector2Plusdouble _b;
        }

        #endregion Private Structs

        #region Private Classes

        private class EmbeddedVectorObject
        {
            #region Public Fields

            public Vector2Fix16 FieldVector;

            #endregion Public Fields
        }

        #endregion Private Classes
    }
}