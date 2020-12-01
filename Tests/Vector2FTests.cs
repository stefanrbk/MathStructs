using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector2FTests
    {
        #region Public Methods

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new float[] {4.0f, 7.0f}).AsSpan();
            var vec = span.ToVector2F();

            Assert.That(vec.X, Is.EqualTo(span[0]));
            Assert.That(vec.Y, Is.EqualTo(span[1]));
        }

        [Test]
        public void WithTest()
        {
            Vector2F v3 = new Vector2F(
                1.0f,
                2.0f);
            Assert.AreEqual(1.0f, v3.X);
            Assert.AreEqual(2.0f, v3.Y);
            Vector2F v4 = v3.With(y: 0.5f);
            Assert.AreEqual(1.0f, v4.X);
            Assert.AreEqual(0.5f, v4.Y);
            Assert.AreEqual(2.0f, v3.Y);
        }

        [Test]
        public void Vector2AbsTest()
        {
            Vector2F v1 = new Vector2F(-2.5f, 2.0f);
            Vector2F v3 = Vector2F.Abs(new Vector2F(0.0f, float.NegativeInfinity));
            Vector2F v = Vector2F.Abs(v1);
            Assert.AreEqual(2.5f, v.X);
            Assert.AreEqual(2.0f, v.Y);
            Assert.AreEqual(0.0f, v3.X);
            Assert.AreEqual(float.PositiveInfinity, v3.Y);
        }

        // A test for operator + (Vector2f, Vector2f)
        [Test]
        public void Vector2AdditionTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(3.0f, 4.0f);

            Vector2F expected = new Vector2F(4.0f, 6.0f);
            Vector2F actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator + did not return the expected value.");
        }

        // A test for Add (Vector2f, Vector2f)
        [Test]
        public void Vector2AddTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(5.0f, 6.0f);

            Vector2F expected = new Vector2F(6.0f, 8.0f);
            Vector2F actual;

            actual = Vector2F.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Clamp (Vector2f, Vector2f, Vector2f)
        [Test]
        public void Vector2ClampTest()
        {
            Vector2F a = new Vector2F(0.5f, 0.3f);
            Vector2F min = new Vector2F(0.0f, 0.1f);
            Vector2F max = new Vector2F(1.0f, 1.1f);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector2F expected = new Vector2F(0.5f, 0.3f);
            Vector2F actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector2F(2.0f, 3.0f);
            expected = max;
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
            // Case N3: specified value is smaller than max value.
            a = new Vector2F(-1.0f, -2.0f);
            expected = min;
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
            // Case N4: combination case.
            a = new Vector2F(-2.0f, 4.0f);
            expected = new Vector2F(min.X, max.Y);
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
            // User specified min value is bigger than max value.
            max = new Vector2F(0.0f, 0.1f);
            min = new Vector2F(1.0f, 1.1f);

            // Case W1: specified value is in the range.
            a = new Vector2F(0.5f, 0.3f);
            expected = max;
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector2F(2.0f, 3.0f);
            expected = max;
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector2F(-1.0f, -2.0f);
            expected = max;
            actual = Vector2F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
        }

        // A test for Vector2f (float, float)
        [Test]
        public void Vector2ConstructorTest()
        {
            float x = 1.0f;
            float y = 2.0f;

            Vector2F target = new Vector2F(x, y);
            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y), "Vector2f(x,y) constructor did not return the expected value.");
        }

        // A test for Vector2f ()
        // Constructor with no parameter
        [Test]
        public void Vector2ConstructorTest2()
        {
            Vector2F target = new Vector2F();
            Assert.AreEqual(0.0f, target.X);
            Assert.AreEqual(0.0f, target.Y);
        }

        // A test for Vector2f (float, float)
        // Constructor with special floating values
        [Test]
        public void Vector2ConstructorTest3()
        {
            Vector2F target = new Vector2F(float.NaN, float.MaxValue);
            Assert.AreEqual(target.X, float.NaN);
            Assert.AreEqual(target.Y, float.MaxValue);
        }

        // A test for Vector2f (float)
        [Test]
        public void Vector2ConstructorTest4()
        {
            float value = 1.0f;
            Vector2F target = new Vector2F(value);

            Vector2F expected = new Vector2F(value, value);
            Assert.AreEqual(expected, target);

            value = 2.0f;
            target = new Vector2F(value);
            expected = new Vector2F(value, value);
            Assert.AreEqual(expected, target);
        }

        [Test]
        public void Vector2CopyToTest()
        {
            Vector2F v1 = new Vector2F(2.0f, 3.0f);

            float[] a = new float[3];
            float[] b = new float[2];

            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.AreEqual(0.0, a[0]);
            Assert.AreEqual(2.0, a[1]);
            Assert.AreEqual(3.0, a[2]);
            Assert.AreEqual(2.0, b[0]);
            Assert.AreEqual(3.0, b[1]);
        }

        // A test for DistanceSquared (Vector2f, Vector2f)
        [Test]
        public void Vector2DistanceSquaredTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(3.0f, 4.0f);

            float expected = 8.0f;
            float actual;

            actual = Vector2F.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.DistanceSquared did not return the expected value.");
        }

        // A test for Distance (Vector2f, Vector2f)
        [Test]
        public void Vector2DistanceTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(3.0f, 4.0f);

            float expected = (float)System.Math.Sqrt(8);
            float actual;

            actual = Vector2F.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Distance did not return the expected value.");
        }

        // A test for Distance (Vector2f, Vector2f)
        // Distance from the same point
        [Test]
        public void Vector2DistanceTest2()
        {
            Vector2F a = new Vector2F(1.051f, 2.05f);
            Vector2F b = new Vector2F(1.051f, 2.05f);

            float actual = Vector2F.Distance(a, b);
            Assert.AreEqual(0.0f, actual);
        }

        // A test for Divide (Vector2f, float)
        [Test]
        public void Vector2DivideTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            float div = 2.0f;
            Vector2F expected = new Vector2F(0.5f, 1.0f);
            Vector2F actual;
            actual = Vector2F.Divide(a, div);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector2f, Vector2f)
        [Test]
        public void Vector2DivideTest1()
        {
            Vector2F a = new Vector2F(1.0f, 6.0f);
            Vector2F b = new Vector2F(5.0f, 2.0f);

            Vector2F expected = new Vector2F(1.0f / 5.0f, 6.0f / 2.0f);
            Vector2F actual;

            actual = Vector2F.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator / (Vector2f, float)
        [Test]
        public void Vector2DivisionTest()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);

            float div = 2.0f;

            Vector2F expected = new Vector2F(1.0f, 1.5f);
            Vector2F actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2f, Vector2f)
        [Test]
        public void Vector2DivisionTest1()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);
            Vector2F b = new Vector2F(4.0f, 5.0f);

            Vector2F expected = new Vector2F(2.0f / 4.0f, 3.0f / 5.0f);
            Vector2F actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2f, float)
        // Divide by zero
        [Test]
        public void Vector2DivisionTest2()
        {
            Vector2F a = new Vector2F(-2.0f, 3.0f);

            float div = 0.0f;

            Vector2F actual = a / div;

            Assert.True(float.IsNegativeInfinity(actual.X), "Vector2f.operator / did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Y), "Vector2f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2f, Vector2f)
        // Divide by zero
        [Test]
        public void Vector2DivisionTest3()
        {
            Vector2F a = new Vector2F(0.047f, -3.0f);
            Vector2F b = new Vector2F();

            Vector2F actual = a / b;

            Assert.True(float.IsInfinity(actual.X), "Vector2f.operator / did not return the expected value.");
            Assert.True(float.IsInfinity(actual.Y), "Vector2f.operator / did not return the expected value.");
        }

        // A test for Dot (Vector2f, Vector2f)
        [Test]
        public void Vector2DotTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(3.0f, 4.0f);

            float expected = 11.0f;
            float actual;

            actual = Vector2F.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Dot did not return the expected value.");
        }

        // A test for Dot (Vector2f, Vector2f)
        // Dot test for perpendicular vector
        [Test]
        public void Vector2DotTest1()
        {
            Vector2F a = new Vector2F(1.55f, 1.55f);
            Vector2F b = new Vector2F(-1.55f, 1.55f);

            float expected = 0.0f;
            float actual = Vector2F.Dot(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Dot (Vector2f, Vector2f)
        // Dot test with specail float values
        [Test]
        public void Vector2DotTest2()
        {
            Vector2F a = new Vector2F(float.MinValue, float.MinValue);
            Vector2F b = new Vector2F(float.MaxValue, float.MaxValue);

            float actual = Vector2F.Dot(a, b);
            Assert.True(float.IsNegativeInfinity(actual), "Vector2f.Dot did not return the expected value.");
        }

        // A test for operator == (Vector2f, Vector2f)
        [Test]
        public void Vector2EqualityTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(1.0f, 2.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b = b.With(x: 10.0f);
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for Vector2f comparison involving NaN values
        [Test]
        public void Vector2EqualsNanTest()
        {
            Vector2F a = new Vector2F(float.NaN, 0);
            Vector2F b = new Vector2F(0, float.NaN);

            Assert.False(a == Vector2F.Zero);
            Assert.False(b == Vector2F.Zero);

            Assert.True(a != Vector2F.Zero);
            Assert.True(b != Vector2F.Zero);

            Assert.False(a.Equals(Vector2F.Zero));
            Assert.False(b.Equals(Vector2F.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
        }

        // A test for Equals (object)
        [Test]
        public void Vector2EqualsTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(1.0f, 2.0f);

            // case 1: compare between same values
            object? obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b = b.With(x: 10.0f);
            obj = b;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare between different types.
            obj = new QuaternionF();
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (Vector2f)
        [Test]
        public void Vector2EqualsTest1()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(1.0f, 2.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b = b.With(x: 10.0f);
            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Vector2GetHashCodeTest()
        {
            Vector2F v1 = new Vector2F(2.0f, 3.0f);
            Vector2F v2 = new Vector2F(2.0f, 3.0f);
            Vector2F v3 = new Vector2F(3.0f, 2.0f);
            Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector2F v4 = new Vector2F(0.0f, 0.0f);
            Vector2F v6 = new Vector2F(1.0f, 0.0f);
            Vector2F v7 = new Vector2F(0.0f, 1.0f);
            Vector2F v8 = new Vector2F(1.0f, 1.0f);
            Assert.AreNotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v7.GetHashCode());
        }

        // A test for operator != (Vector2f, Vector2f)
        [Test]
        public void Vector2InequalityTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(1.0f, 2.0f);

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b = b.With(x: 10.0f);
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for LengthSquared ()
        [Test]
        public void Vector2LengthSquaredTest()
        {
            Vector2F a = new Vector2F(2.0f, 4.0f);

            Vector2F target = a;

            float expected = 20.0f;
            float actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.LengthSquared did not return the expected value.");
        }

        // A test for LengthSquared ()
        // LengthSquared test where the result is zero
        [Test]
        public void Vector2LengthSquaredTest1()
        {
            Vector2F a = new Vector2F(0.0f, 0.0f);

            float expected = 0.0f;
            float actual = a.LengthSquared();

            Assert.AreEqual(expected, actual);
        }

        // A test for Length ()
        [Test]
        public void Vector2LengthTest()
        {
            Vector2F a = new Vector2F(2.0f, 4.0f);

            Vector2F target = a;

            float expected = (float)System.Math.Sqrt(20);
            float actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Test]
        public void Vector2LengthTest1()
        {
            Vector2F target = new Vector2F(
                0.0f,
                0.0f);

            float expected = 0.0f;
            float actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Length did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        [Test]
        public void Vector2LerpTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(3.0f, 4.0f);

            float t = 0.5f;

            Vector2F expected = new Vector2F(2.0f, 3.0f);
            Vector2F actual;
            actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with factor zero
        [Test]
        public void Vector2LerpTest1()
        {
            Vector2F a = new Vector2F(0.0f, 0.0f);
            Vector2F b = new Vector2F(3.18f, 4.25f);

            float t = 0.0f;
            Vector2F expected = Vector2F.Zero;
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with factor one
        [Test]
        public void Vector2LerpTest2()
        {
            Vector2F a = new Vector2F(0.0f, 0.0f);
            Vector2F b = new Vector2F(3.18f, 4.25f);

            float t = 1.0f;
            Vector2F expected = new Vector2F(3.18f, 4.25f);
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with factor > 1
        [Test]
        public void Vector2LerpTest3()
        {
            Vector2F a = new Vector2F(0.0f, 0.0f);
            Vector2F b = new Vector2F(3.18f, 4.25f);

            float t = 2.0f;
            Vector2F expected = b * 2.0f;
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with factor < 0
        [Test]
        public void Vector2LerpTest4()
        {
            Vector2F a = new Vector2F(0.0f, 0.0f);
            Vector2F b = new Vector2F(3.18f, 4.25f);

            float t = -2.0f;
            Vector2F expected = -(b * 2.0f);
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with special float value
        [Test]
        public void Vector2LerpTest5()
        {
            Vector2F a = new Vector2F(45.67f, 90.0f);
            Vector2F b = new Vector2F(float.PositiveInfinity, float.NegativeInfinity);

            float t = 0.408f;
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(float.IsPositiveInfinity(actual.X), "Vector2f.Lerp did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Y), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test from the same point
        [Test]
        public void Vector2LerpTest6()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(1.0f, 2.0f);

            float t = 0.5f;

            Vector2F expected = new Vector2F(1.0f, 2.0f);
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        [Test]
        public void Vector2LerpTest7()
        {
            Vector2F a = new Vector2F(0.44728136f);
            Vector2F b = new Vector2F(0.46345946f);

            float t = 0.26402435f;

            Vector2F expected = new Vector2F(0.45155275f);
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2f, Vector2f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        // (Old code incorrectly gets 0.33333588)
        [Test]
        public void Vector2LerpTest8()
        {
            Vector2F a = new Vector2F(-100);
            Vector2F b = new Vector2F(0.33333334f);

            float t = 1f;

            Vector2F expected = new Vector2F(0.33333334f);
            Vector2F actual = Vector2F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
        }

        [Test]
        public void Vector2MarshalSizeTest()
        {
            Assert.AreEqual(8, Marshal.SizeOf<Vector2F>());
            Assert.AreEqual(8, Marshal.SizeOf<Vector2F>(new Vector2F()));
        }

        // A test for Max (Vector2f, Vector2f)
        [Test]
        public void Vector2MaxTest()
        {
            Vector2F a = new Vector2F(-1.0f, 4.0f);
            Vector2F b = new Vector2F(2.0f, 1.0f);

            Vector2F expected = new Vector2F(2.0f, 4.0f);
            Vector2F actual;
            actual = Vector2F.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Max did not return the expected value.");
        }

        [Test]
        public void Vector2MinMaxCodeCoverageTest()
        {
            Vector2F min = new Vector2F(0, 0);
            Vector2F max = new Vector2F(1, 1);
            Vector2F actual;

            // Min.
            actual = Vector2F.Min(min, max);
            Assert.AreEqual(actual, min);

            actual = Vector2F.Min(max, min);
            Assert.AreEqual(actual, min);

            // Max.
            actual = Vector2F.Max(min, max);
            Assert.AreEqual(actual, max);

            actual = Vector2F.Max(max, min);
            Assert.AreEqual(actual, max);
        }

        // A test for Min (Vector2f, Vector2f)
        [Test]
        public void Vector2MinTest()
        {
            Vector2F a = new Vector2F(-1.0f, 4.0f);
            Vector2F b = new Vector2F(2.0f, 1.0f);

            Vector2F expected = new Vector2F(-1.0f, 1.0f);
            Vector2F actual;
            actual = Vector2F.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Min did not return the expected value.");
        }

        // A test for operator * (Vector2f, float)
        [Test]
        public void Vector2MultiplyOperatorTest()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);
            const float factor = 2.0f;

            Vector2F expected = new Vector2F(4.0f, 6.0f);
            Vector2F actual;

            actual = a * factor;
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
        }

        // A test for operator * (float, Vector2f)
        [Test]
        public void Vector2MultiplyOperatorTest2()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);
            const float factor = 2.0f;

            Vector2F expected = new Vector2F(4.0f, 6.0f);
            Vector2F actual;

            actual = factor * a;
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
        }

        // A test for operator * (Vector2f, Vector2f)
        [Test]
        public void Vector2MultiplyOperatorTest3()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);
            Vector2F b = new Vector2F(4.0f, 5.0f);

            Vector2F expected = new Vector2F(8.0f, 15.0f);
            Vector2F actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
        }

        // A test for Multiply (Vector2f, float)
        [Test]
        public void Vector2MultiplyTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            const float factor = 2.0f;
            Vector2F expected = new Vector2F(2.0f, 4.0f);
            Vector2F actual = Vector2F.Multiply(a, factor);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (float, Vector2f)
        [Test]
        public void Vector2MultiplyTest2()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            const float factor = 2.0f;
            Vector2F expected = new Vector2F(2.0f, 4.0f);
            Vector2F actual = Vector2F.Multiply(factor, a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector2f, Vector2f)
        [Test]
        public void Vector2MultiplyTest3()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            Vector2F b = new Vector2F(5.0f, 6.0f);

            Vector2F expected = new Vector2F(5.0f, 12.0f);
            Vector2F actual;

            actual = Vector2F.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Vector2f)
        [Test]
        public void Vector2NegateTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);

            Vector2F expected = new Vector2F(-1.0f, -2.0f);
            Vector2F actual;

            actual = Vector2F.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Normalize (Vector2f)
        [Test]
        public void Vector2NormalizeTest()
        {
            Vector2F a = new Vector2F(2.0f, 3.0f);
            Vector2F expected = new Vector2F(0.554700196225229122018341733457f, 0.8320502943378436830275126001855f);
            Vector2F actual;

            actual = Vector2F.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector2f)
        // Normalize zero length vector
        [Test]
        public void Vector2NormalizeTest1()
        {
            Vector2F a = new Vector2F(); // no parameter, default to 0.0f
            Vector2F actual = Vector2F.Normalize(a);
            Assert.True(float.IsNaN(actual.X) && float.IsNaN(actual.Y), "Vector2f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector2f)
        // Normalize infinite length vector
        [Test]
        public void Vector2NormalizeTest2()
        {
            Vector2F a = new Vector2F(float.MaxValue, float.MaxValue);
            Vector2F actual = Vector2F.Normalize(a);
            Vector2F expected = new Vector2F(0, 0);
            Assert.AreEqual(expected, actual);
        }

        // A test for One
        [Test]
        public void Vector2OneTest()
        {
            Vector2F val = new Vector2F(1.0f, 1.0f);
            Assert.AreEqual(val, Vector2F.One);
        }

        // A test for Reflect (Vector2f, Vector2f)
        [Test]
        public void Vector2ReflectTest()
        {
            Vector2F a = Vector2F.Normalize(new Vector2F(1.0f, 1.0f));

            // Reflect on XZ PlaneF.
            Vector2F n = new Vector2F(0.0f, 1.0f);
            Vector2F expected = new Vector2F(a.X, -a.Y);
            Vector2F actual = Vector2F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");

            // Reflect on XY PlaneF.
            n = new Vector2F(0.0f, 0.0f);
            expected = new Vector2F(a.X, a.Y);
            actual = Vector2F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");

            // Reflect on YZ PlaneF.
            n = new Vector2F(1.0f, 0.0f);
            expected = new Vector2F(-a.X, a.Y);
            actual = Vector2F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector2f, Vector2f)
        // Reflection when normal and source are the same
        [Test]
        public void Vector2ReflectTest1()
        {
            Vector2F n = new Vector2F(0.45f, 1.28f);
            n = Vector2F.Normalize(n);
            Vector2F a = n;

            Vector2F expected = -n;
            Vector2F actual = Vector2F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector2f, Vector2f)
        // Reflection when normal and source are negation
        [Test]
        public void Vector2ReflectTest2()
        {
            Vector2F n = new Vector2F(0.45f, 1.28f);
            n = Vector2F.Normalize(n);
            Vector2F a = -n;

            Vector2F expected = n;
            Vector2F actual = Vector2F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Vector2SizeofTest()
        {
            Assert.AreEqual(8, sizeof(Vector2F));
            Assert.AreEqual(16, sizeof(Vector2_2x));
            Assert.AreEqual(12, sizeof(Vector2PlusFloat));
            Assert.AreEqual(24, sizeof(Vector2PlusFloat_2x));
        }

        [Test]
        public void Vector2SqrtTest()
        {
            Vector2F v1 = new Vector2F(-2.5f, 2.0f);
            Vector2F v2 = new Vector2F(5.5f, 4.5f);
            Assert.AreEqual(2, (int)Vector2F.SquareRoot(v2).X);
            Assert.AreEqual(2, (int)Vector2F.SquareRoot(v2).Y);
            Assert.AreEqual(float.NaN, Vector2F.SquareRoot(v1).X);
        }

        // A test for operator - (Vector2f, Vector2f)
        [Test]
        public void Vector2SubtractionTest()
        {
            Vector2F a = new Vector2F(1.0f, 3.0f);
            Vector2F b = new Vector2F(2.0f, 1.5f);

            Vector2F expected = new Vector2F(-1.0f, 1.5f);
            Vector2F actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator - did not return the expected value.");
        }

        // A test for Subtract (Vector2f, Vector2f)
        [Test]
        public void Vector2SubtractTest()
        {
            Vector2F a = new Vector2F(1.0f, 6.0f);
            Vector2F b = new Vector2F(5.0f, 2.0f);

            Vector2F expected = new Vector2F(-4.0f, 4.0f);
            Vector2F actual;

            actual = Vector2F.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Vector2ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector2F v1 = new Vector2F(2.0f, 3.0f);

            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}>"
                , new object[] { separator, 2, 3 });
            Assert.AreEqual(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}>"
                , new object[] { separator, 2, 3 });
            Assert.AreEqual(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}>"
                , new object[] { enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2, 3 });
            Assert.AreEqual(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}>"
                , new object[] { separator, 2, 3 });
            Assert.AreEqual(expectedv3formatted, v3strformatted);
        }

        // A test for Transform (Vector2f, QuaternionF)
        [Test]
        public void Vector2TransformByQuaternionTest()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector2F expected = Vector2F.Transform(v, m);
            Vector2F actual = Vector2F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        }

        //    actual = Vector2F.TransformNormal(v, m);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        //}
        // A test for Transform (Vector2f, QuaternionF)
        // Transform Vector2f with zero QuaternionF
        [Test]
        public void Vector2TransformByQuaternionTest1()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            QuaternionF q = new QuaternionF();
            Vector2F expected = v;

            Vector2F actual = Vector2F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        }

        //    Vector2F expected = new Vector2F(-0.133974612f, 2.232051f);
        //    Vector2F actual;
        // A test for Transform (Vector2f, QuaternionF)
        // Transform Vector2f with identity QuaternionF
        [Test]
        public void Vector2TransformByQuaternionTest2()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            QuaternionF q = QuaternionF.Identity;
            Vector2F expected = v;

            Vector2F actual = Vector2F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        }

        // A test for TransformNormal (Vector2f, Matrix4x4F)
        [Test]
        public void Vector2TransformNormalTest()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector2F expected = new Vector2F(0.3169873f, 2.18301272f);
            Vector2F actual;

            actual = Vector2F.TransformNormal(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Tranform did not return the expected value.");
        }

        // A test for Transform(Vector2f, Matrix4x4F)
        [Test]
        public void Vector2TransformTest()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector2F expected = new Vector2F(10.316987f, 22.183012f);
            Vector2F actual;

            actual = Vector2F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        }

        //// A test for Transform(Vector2f, Matrix3x2)
        //[Test]
        //public void Vector2Transform3x2Test()
        //{
        //    Vector2F v = new Vector2F(1.0f, 2.0f);
        //    Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0f));
        //    m.M31 = 10.0f;
        //    m.M32 = 20.0f;

        //    Vector2F expected = new Vector2F(9.866025f, 22.23205f);
        //    Vector2F actual;

        //    actual = Vector2F.Transform(v, m);
        //    Assert.True(MathHelper.Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
        //}
        //// A test for TransformNormal (Vector2f, Matrix3x2)
        //[Test]
        //public void Vector2TransformNormal3x2Test()
        //{
        //    Vector2F v = new Vector2F(1.0f, 2.0f);
        //    Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0f));
        //    m.M31 = 10.0f;
        //    m.M32 = 20.0f;
        // A test for operator - (Vector2f)
        [Test]
        public void Vector2UnaryNegationTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);

            Vector2F expected = new Vector2F(-1.0f, -2.0f);
            Vector2F actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2f.operator - did not return the expected value.");
        }

        // A test for operator - (Vector2f)
        // Negate test with special float value
        [Test]
        public void Vector2UnaryNegationTest1()
        {
            Vector2F a = new Vector2F(float.PositiveInfinity, float.NegativeInfinity);

            Vector2F actual = -a;

            Assert.True(float.IsNegativeInfinity(actual.X), "Vector2f.operator - did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Y), "Vector2f.operator - did not return the expected value.");
        }

        // A test for operator - (Vector2f)
        // Negate test with special float value
        [Test]
        public void Vector2UnaryNegationTest2()
        {
            Vector2F a = new Vector2F(float.NaN, 0.0f);
            Vector2F actual = -a;

            Assert.True(float.IsNaN(actual.X), "Vector2f.operator - did not return the expected value.");
            Assert.True(float.Equals(0.0f, actual.Y), "Vector2f.operator - did not return the expected value.");
        }

        // A test for UnitX
        [Test]
        public void Vector2UnitXTest()
        {
            Vector2F val = new Vector2F(1.0f, 0.0f);
            Assert.AreEqual(val, Vector2F.UnitX);
        }

        // A test for UnitY
        [Test]
        public void Vector2UnitYTest()
        {
            Vector2F val = new Vector2F(0.0f, 1.0f);
            Assert.AreEqual(val, Vector2F.UnitY);
        }

        // A test for Zero
        [Test]
        public void Vector2ZeroTest()
        {
            Vector2F val = new Vector2F(0.0f, 0.0f);
            Assert.AreEqual(val, Vector2F.Zero);
        }

        #endregion Public Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2_2x
        {
            private Vector2F _a;
            private Vector2F _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2PlusFloat
        {
            private Vector2F _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector2PlusFloat_2x
        {
            private Vector2PlusFloat _a;
            private Vector2PlusFloat _b;
        }

        #endregion Private Structs

        #region Private Classes

        private class EmbeddedVectorObject
        {
            #region Public Fields

            public Vector2F FieldVector;

            #endregion Public Fields
        }

        #endregion Private Classes
    }
}