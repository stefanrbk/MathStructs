using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector3FTests
    {
        #region Public Methods

        // A test for Multiply (float, Vector3f)
        [Test]
        public static void Vector3MultiplyTest2()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            const float factor = 2.0f;
            Vector3F expected = new Vector3F(2.0f, 4.0f, 6.0f);
            Vector3F actual = Vector3F.Multiply(factor, a);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0f;
            evo.FieldVector.Y = 5.0f;
            evo.FieldVector.Z = 5.0f;
            Assert.AreEqual(5.0f, evo.FieldVector.X);
            Assert.AreEqual(5.0f, evo.FieldVector.Y);
            Assert.AreEqual(5.0f, evo.FieldVector.Z);
        }

        [Test]
        public void SetFieldsTest()
        {
            Vector3F v3 = new Vector3F(4f, 5f, 6f)
            {
                X = 1.0f,
                Y = 2.0f,
                Z = 3.0f
            };
            Assert.AreEqual(1.0f, v3.X);
            Assert.AreEqual(2.0f, v3.Y);
            Assert.AreEqual(3.0f, v3.Z);
            Vector3F v4 = v3;
            v4.Y = 0.5f;
            v4.Z = 2.2f;
            Assert.AreEqual(1.0f, v4.X);
            Assert.AreEqual(0.5f, v4.Y);
            Assert.AreEqual(2.2f, v4.Z);
            Assert.AreEqual(2.0f, v3.Y);

            Vector3F before = new Vector3F(1f, 2f, 3f);
            Vector3F after = before;
            after.X = 500.0f;
            Assert.AreNotEqual(before, after);
        }

        [Test]
        public void Vector3AbsTest()
        {
            Vector3F v1 = new Vector3F(-2.5f, 2.0f, 0.5f);
            Vector3F v3 = Vector3F.Abs(new Vector3F(0.0f, float.NegativeInfinity, float.NaN));
            Vector3F v = Vector3F.Abs(v1);
            Assert.AreEqual(2.5f, v.X);
            Assert.AreEqual(2.0f, v.Y);
            Assert.AreEqual(0.5f, v.Z);
            Assert.AreEqual(0.0f, v3.X);
            Assert.AreEqual(float.PositiveInfinity, v3.Y);
            Assert.AreEqual(float.NaN, v3.Z);
        }

        // A test for operator + (Vector3f, Vector3f)
        [Test]
        public void Vector3AdditionTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            Vector3F expected = new Vector3F(5.0f, 7.0f, 9.0f);
            Vector3F actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator + did not return the expected value.");
        }

        // A test for Add (Vector3f, Vector3f)
        [Test]
        public void Vector3AddTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(5.0f, 6.0f, 7.0f);

            Vector3F expected = new Vector3F(6.0f, 8.0f, 10.0f);
            Vector3F actual;

            actual = Vector3F.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Clamp (Vector3f, Vector3f, Vector3f)
        [Test]
        public void Vector3ClampTest()
        {
            Vector3F a = new Vector3F(0.5f, 0.3f, 0.33f);
            Vector3F min = new Vector3F(0.0f, 0.1f, 0.13f);
            Vector3F max = new Vector3F(1.0f, 1.1f, 1.13f);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector3F expected = new Vector3F(0.5f, 0.3f, 0.33f);
            Vector3F actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector3F(2.0f, 3.0f, 4.0f);
            expected = max;
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector3F(-2.0f, -3.0f, -4.0f);
            expected = min;
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector3F(-2.0f, 0.5f, 4.0f);
            expected = new Vector3F(min.X, a.Y, max.Z);
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector3F(0.0f, 0.1f, 0.13f);
            min = new Vector3F(1.0f, 1.1f, 1.13f);

            // Case W1: specified value is in the range.
            a = new Vector3F(0.5f, 0.3f, 0.33f);
            expected = max;
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector3F(2.0f, 3.0f, 4.0f);
            expected = max;
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector3F(-2.0f, -3.0f, -4.0f);
            expected = max;
            actual = Vector3F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");
        }

        // A test for Vector3f (float, float, float)
        [Test]
        public void Vector3ConstructorTest()
        {
            float x = 1.0f;
            float y = 2.0f;
            float z = 3.0f;

            Vector3F target = new Vector3F(x, y, z);
            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z), "Vector3f.constructor (x,y,z) did not return the expected value.");
        }

        // A test for Vector3f (Vector2f, float)
        [Test]
        public void Vector3ConstructorTest1()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);

            float z = 3.0f;

            Vector3F target = new Vector3F(a, z);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z), "Vector3f.constructor (Vector2f,z) did not return the expected value.");
        }

        // A test for Vector3f ()
        // Constructor with no parameter
        [Test]
        public void Vector3ConstructorTest3()
        {
            Vector3F a = new Vector3F();

            Assert.AreEqual(0.0f, a.X);
            Assert.AreEqual(0.0f, a.Y);
            Assert.AreEqual(0.0f, a.Z);
        }

        // A test for Vector2f (float, float)
        // Constructor with special floating values
        [Test]
        public void Vector3ConstructorTest4()
        {
            Vector3F target = new Vector3F(float.NaN, float.MaxValue, float.PositiveInfinity);

            Assert.True(float.IsNaN(target.X), "Vector3f.constructor (Vector3f) did not return the expected value.");
            Assert.True(float.Equals(float.MaxValue, target.Y), "Vector3f.constructor (Vector3f) did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(target.Z), "Vector3f.constructor (Vector3f) did not return the expected value.");
        }

        // A test for Vector3f (float)
        [Test]
        public void Vector3ConstructorTest5()
        {
            float value = 1.0f;
            Vector3F target = new Vector3F(value);

            Vector3F expected = new Vector3F(value, value, value);
            Assert.AreEqual(expected, target);

            value = 2.0f;
            target = new Vector3F(value);
            expected = new Vector3F(value, value, value);
            Assert.AreEqual(expected, target);
        }

        [Test]
        public void Vector3CopyToTest()
        {
            Vector3F v1 = new Vector3F(2.0f, 3.0f, 3.3f);

            float[] a = new float[4];
            float[] b = new float[3];

            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, a.Length - 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.AreEqual(0.0f, a[0]);
            Assert.AreEqual(2.0f, a[1]);
            Assert.AreEqual(3.0f, a[2]);
            Assert.AreEqual(3.3f, a[3]);
            Assert.AreEqual(2.0f, b[0]);
            Assert.AreEqual(3.0f, b[1]);
            Assert.AreEqual(3.3f, b[2]);
        }

        // A test for Cross (Vector3f, Vector3f)
        [Test]
        public void Vector3CrossTest()
        {
            Vector3F a = new Vector3F(1.0f, 0.0f, 0.0f);
            Vector3F b = new Vector3F(0.0f, 1.0f, 0.0f);

            Vector3F expected = new Vector3F(0.0f, 0.0f, 1.0f);
            Vector3F actual;

            actual = Vector3F.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Cross did not return the expected value.");
        }

        // A test for Cross (Vector3f, Vector3f)
        // Cross test of the same vector
        [Test]
        public void Vector3CrossTest1()
        {
            Vector3F a = new Vector3F(0.0f, 1.0f, 0.0f);
            Vector3F b = new Vector3F(0.0f, 1.0f, 0.0f);

            Vector3F expected = new Vector3F(0.0f, 0.0f, 0.0f);
            Vector3F actual = Vector3F.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Cross did not return the expected value.");
        }

        // A test for DistanceSquared (Vector3f, Vector3f)
        [Test]
        public void Vector3DistanceSquaredTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float expected = 27.0f;
            float actual;

            actual = Vector3F.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.DistanceSquared did not return the expected value.");
        }

        // A test for Distance (Vector3f, Vector3f)
        [Test]
        public void Vector3DistanceTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float expected = (float)System.Math.Sqrt(27);
            float actual;

            actual = Vector3F.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Distance did not return the expected value.");
        }

        // A test for Distance (Vector3f, Vector3f)
        // Distance from the same point
        [Test]
        public void Vector3DistanceTest1()
        {
            Vector3F a = new Vector3F(1.051f, 2.05f, 3.478f);
            Vector3F b = new Vector3F(new Vector2F(1.051f, 0.0f), 1)
            {
                Y = 2.05f,
                Z = 3.478f
            };

            float actual = Vector3F.Distance(a, b);
            Assert.AreEqual(0.0f, actual);
        }

        // A test for Divide (Vector3f, float)
        [Test]
        public void Vector3DivideTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            float div = 2.0f;
            Vector3F expected = new Vector3F(0.5f, 1.0f, 1.5f);
            Vector3F actual;
            actual = Vector3F.Divide(a, div);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector3f, Vector3f)
        [Test]
        public void Vector3DivideTest1()
        {
            Vector3F a = new Vector3F(1.0f, 6.0f, 7.0f);
            Vector3F b = new Vector3F(5.0f, 2.0f, 3.0f);

            Vector3F expected = new Vector3F(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f);
            Vector3F actual;

            actual = Vector3F.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator / (Vector3f, float)
        [Test]
        public void Vector3DivisionTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            float div = 2.0f;

            Vector3F expected = new Vector3F(0.5f, 1.0f, 1.5f);
            Vector3F actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3f, Vector3f)
        [Test]
        public void Vector3DivisionTest1()
        {
            Vector3F a = new Vector3F(4.0f, 2.0f, 3.0f);

            Vector3F b = new Vector3F(1.0f, 5.0f, 6.0f);

            Vector3F expected = new Vector3F(4.0f, 0.4f, 0.5f);
            Vector3F actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3f, Vector3f)
        // Divide by zero
        [Test]
        public void Vector3DivisionTest2()
        {
            Vector3F a = new Vector3F(-2.0f, 3.0f, float.MaxValue);

            float div = 0.0f;

            Vector3F actual = a / div;

            Assert.True(float.IsNegativeInfinity(actual.X), "Vector3f.operator / did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Y), "Vector3f.operator / did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Z), "Vector3f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3f, Vector3f)
        // Divide by zero
        [Test]
        public void Vector3DivisionTest3()
        {
            Vector3F a = new Vector3F(0.047f, -3.0f, float.NegativeInfinity);
            Vector3F b = new Vector3F();

            Vector3F actual = a / b;

            Assert.True(float.IsPositiveInfinity(actual.X), "Vector3f.operator / did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Y), "Vector3f.operator / did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Z), "Vector3f.operator / did not return the expected value.");
        }

        // A test for Dot (Vector3f, Vector3f)
        [Test]
        public void Vector3DotTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float expected = 32.0f;
            float actual;

            actual = Vector3F.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Dot did not return the expected value.");
        }

        // A test for Dot (Vector3f, Vector3f)
        // Dot test for perpendicular vector
        [Test]
        public void Vector3DotTest1()
        {
            Vector3F a = new Vector3F(1.55f, 1.55f, 1);
            Vector3F b = new Vector3F(2.5f, 3, 1.5f);
            Vector3F c = Vector3F.Cross(a, b);

            float expected = 0.0f;
            float actual1 = Vector3F.Dot(a, c);
            float actual2 = Vector3F.Dot(b, c);
            Assert.True(MathHelper.Equal(expected, actual1), "Vector3f.Dot did not return the expected value.");
            Assert.True(MathHelper.Equal(expected, actual2), "Vector3f.Dot did not return the expected value.");
        }

        // A test for operator == (Vector3f, Vector3f)
        [Test]
        public void Vector3EqualityTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(1.0f, 2.0f, 3.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.X = 10.0f;
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for Vector3f comparison involving NaN values
        [Test]
        public void Vector3EqualsNanTest()
        {
            Vector3F a = new Vector3F(float.NaN, 0, 0);
            Vector3F b = new Vector3F(0, float.NaN, 0);
            Vector3F c = new Vector3F(0, 0, float.NaN);

            Assert.False(a == Vector3F.Zero);
            Assert.False(b == Vector3F.Zero);
            Assert.False(c == Vector3F.Zero);

            Assert.True(a != Vector3F.Zero);
            Assert.True(b != Vector3F.Zero);
            Assert.True(c != Vector3F.Zero);

            Assert.False(a.Equals(Vector3F.Zero));
            Assert.False(b.Equals(Vector3F.Zero));
            Assert.False(c.Equals(Vector3F.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
        }

        // A test for Equals (object)
        [Test]
        public void Vector3EqualsTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(1.0f, 2.0f, 3.0f);

            // case 1: compare between same values
            object? obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.X = 10.0f;
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

        // A test for Equals (Vector3f)
        [Test]
        public void Vector3EqualsTest1()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(1.0f, 2.0f, 3.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.X = 10.0f;
            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Vector3GetHashCodeTest()
        {
            Vector3F v1 = new Vector3F(2.0f, 3.0f, 3.3f);
            Vector3F v2 = new Vector3F(2.0f, 3.0f, 3.3f);
            Vector3F v3 = new Vector3F(2.0f, 3.0f, 3.3f);
            Vector3F v5 = new Vector3F(3.0f, 2.0f, 3.3f);
            Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector3F v4 = new Vector3F(0.0f, 0.0f, 0.0f);
            Vector3F v6 = new Vector3F(1.0f, 0.0f, 0.0f);
            Vector3F v7 = new Vector3F(0.0f, 1.0f, 0.0f);
            Vector3F v8 = new Vector3F(1.0f, 1.0f, 1.0f);
            Vector3F v9 = new Vector3F(1.0f, 1.0f, 0.0f);
            Assert.AreNotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v9.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v9.GetHashCode());
        }

        // A test for operator != (Vector3f, Vector3f)
        [Test]
        public void Vector3InequalityTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(1.0f, 2.0f, 3.0f);

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.X = 10.0f;
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for LengthSquared ()
        [Test]
        public void Vector3LengthSquaredTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);

            float z = 3.0f;

            Vector3F target = new Vector3F(a, z);

            float expected = 14.0f;
            float actual;

            actual = target.LengthSquared();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.LengthSquared did not return the expected value.");
        }

        // A test for Length ()
        [Test]
        public void Vector3LengthTest()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);

            float z = 3.0f;

            Vector3F target = new Vector3F(a, z);

            float expected = (float)System.Math.Sqrt(14.0f);
            float actual;

            actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Test]
        public void Vector3LengthTest1()
        {
            Vector3F target = new Vector3F();

            float expected = 0.0f;
            float actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Length did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        [Test]
        public void Vector3LerpTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float t = 0.5f;

            Vector3F expected = new Vector3F(2.5f, 3.5f, 4.5f);
            Vector3F actual;

            actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with factor zero
        [Test]
        public void Vector3LerpTest1()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float t = 0.0f;
            Vector3F expected = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with factor one
        [Test]
        public void Vector3LerpTest2()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float t = 1.0f;
            Vector3F expected = new Vector3F(4.0f, 5.0f, 6.0f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with factor > 1
        [Test]
        public void Vector3LerpTest3()
        {
            Vector3F a = new Vector3F(0.0f, 0.0f, 0.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float t = 2.0f;
            Vector3F expected = new Vector3F(8.0f, 10.0f, 12.0f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with factor < 0
        [Test]
        public void Vector3LerpTest4()
        {
            Vector3F a = new Vector3F(0.0f, 0.0f, 0.0f);
            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            float t = -2.0f;
            Vector3F expected = new Vector3F(-8.0f, -10.0f, -12.0f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with special float value
        [Test]
        public void Vector3LerpTest5()
        {
            Vector3F a = new Vector3F(45.67f, 90.0f, 0f);
            Vector3F b = new Vector3F(float.PositiveInfinity, float.NegativeInfinity, 0);

            float t = 0.408f;
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(float.IsPositiveInfinity(actual.X), "Vector3f.Lerp did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Y), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test from the same point
        [Test]
        public void Vector3LerpTest6()
        {
            Vector3F a = new Vector3F(1.68f, 2.34f, 5.43f);
            Vector3F b = a;

            float t = 0.18f;
            Vector3F expected = new Vector3F(1.68f, 2.34f, 5.43f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        [Test]
        public void Vector3LerpTest7()
        {
            Vector3F a = new Vector3F(0.44728136f);
            Vector3F b = new Vector3F(0.46345946f);

            float t = 0.26402435f;

            Vector3F expected = new Vector3F(0.45155275f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3f, Vector3f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        // (Old code incorrectly gets 0.33333588)
        [Test]
        public void Vector3LerpTest8()
        {
            Vector3F a = new Vector3F(-100);
            Vector3F b = new Vector3F(0.33333334f);

            float t = 1f;

            Vector3F expected = new Vector3F(0.33333334f);
            Vector3F actual = Vector3F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
        }

        [Test]
        public void Vector3MarshalSizeTest()
        {
            Assert.AreEqual(12, Marshal.SizeOf<Vector3F>());
            Assert.AreEqual(12, Marshal.SizeOf<Vector3F>(new Vector3F()));
        }

        // A test for Max (Vector3f, Vector3f)
        [Test]
        public void Vector3MaxTest()
        {
            Vector3F a = new Vector3F(-1.0f, 4.0f, -3.0f);
            Vector3F b = new Vector3F(2.0f, 1.0f, -1.0f);

            Vector3F expected = new Vector3F(2.0f, 4.0f, -1.0f);
            Vector3F actual;
            actual = Vector3F.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3F.Max did not return the expected value.");
        }

        [Test]
        public void Vector3MinMaxCodeCoverageTest()
        {
            Vector3F min = Vector3F.Zero;
            Vector3F max = Vector3F.One;
            Vector3F actual;

            // Min.
            actual = Vector3F.Min(min, max);
            Assert.AreEqual(actual, min);

            actual = Vector3F.Min(max, min);
            Assert.AreEqual(actual, min);

            // Max.
            actual = Vector3F.Max(min, max);
            Assert.AreEqual(actual, max);

            actual = Vector3F.Max(max, min);
            Assert.AreEqual(actual, max);
        }

        // A test for Min (Vector3f, Vector3f)
        [Test]
        public void Vector3MinTest()
        {
            Vector3F a = new Vector3F(-1.0f, 4.0f, -3.0f);
            Vector3F b = new Vector3F(2.0f, 1.0f, -1.0f);

            Vector3F expected = new Vector3F(-1.0f, 1.0f, -3.0f);
            Vector3F actual;
            actual = Vector3F.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Min did not return the expected value.");
        }

        // A test for operator * (Vector3f, float)
        [Test]
        public void Vector3MultiplyOperatorTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            float factor = 2.0f;

            Vector3F expected = new Vector3F(2.0f, 4.0f, 6.0f);
            Vector3F actual;

            actual = a * factor;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
        }

        // A test for operator * (float, Vector3f)
        [Test]
        public void Vector3MultiplyOperatorTest2()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            const float factor = 2.0f;

            Vector3F expected = new Vector3F(2.0f, 4.0f, 6.0f);
            Vector3F actual;

            actual = factor * a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
        }

        // A test for operator * (Vector3f, Vector3f)
        [Test]
        public void Vector3MultiplyOperatorTest3()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            Vector3F b = new Vector3F(4.0f, 5.0f, 6.0f);

            Vector3F expected = new Vector3F(4.0f, 10.0f, 18.0f);
            Vector3F actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
        }

        // A test for Multiply (Vector3f, float)
        [Test]
        public void Vector3MultiplyTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            const float factor = 2.0f;
            Vector3F expected = new Vector3F(2.0f, 4.0f, 6.0f);
            Vector3F actual = Vector3F.Multiply(a, factor);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector3f, Vector3f)
        [Test]
        public void Vector3MultiplyTest3()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            Vector3F b = new Vector3F(5.0f, 6.0f, 7.0f);

            Vector3F expected = new Vector3F(5.0f, 12.0f, 21.0f);
            Vector3F actual;

            actual = Vector3F.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Vector3f)
        [Test]
        public void Vector3NegateTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            Vector3F expected = new Vector3F(-1.0f, -2.0f, -3.0f);
            Vector3F actual;

            actual = Vector3F.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Normalize (Vector3f)
        [Test]
        public void Vector3NormalizeTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            Vector3F expected = new Vector3F(
                0.26726124191242438468455348087975f,
                0.53452248382484876936910696175951f,
                0.80178372573727315405366044263926f);
            Vector3F actual;

            actual = Vector3F.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3f)
        // Normalize vector of length one
        [Test]
        public void Vector3NormalizeTest1()
        {
            Vector3F a = new Vector3F(1.0f, 0.0f, 0.0f);

            Vector3F expected = new Vector3F(1.0f, 0.0f, 0.0f);
            Vector3F actual = Vector3F.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3f)
        // Normalize vector of length zero
        [Test]
        public void Vector3NormalizeTest2()
        {
            Vector3F a = new Vector3F(0.0f, 0.0f, 0.0f);

            Vector3F actual = Vector3F.Normalize(a);
            Assert.True(float.IsNaN(actual.X) && float.IsNaN(actual.Y) && float.IsNaN(actual.Z), "Vector3f.Normalize did not return the expected value.");
        }

        // A test for One
        [Test]
        public void Vector3OneTest()
        {
            Vector3F val = new Vector3F(1.0f, 1.0f, 1.0f);
            Assert.AreEqual(val, Vector3F.One);
        }

        // A test for Reflect (Vector3f, Vector3f)
        [Test]
        public void Vector3ReflectTest()
        {
            Vector3F a = Vector3F.Normalize(new Vector3F(1.0f, 1.0f, 1.0f));

            // Reflect on XZ plane.
            Vector3F n = new Vector3F(0.0f, 1.0f, 0.0f);
            Vector3F expected = new Vector3F(a.X, -a.Y, a.Z);
            Vector3F actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");

            // Reflect on XY plane.
            n = new Vector3F(0.0f, 0.0f, 1.0f);
            expected = new Vector3F(a.X, a.Y, -a.Z);
            actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");

            // Reflect on YZ plane.
            n = new Vector3F(1.0f, 0.0f, 0.0f);
            expected = new Vector3F(-a.X, a.Y, a.Z);
            actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3f, Vector3f)
        // Reflection when normal and source are the same
        [Test]
        public void Vector3ReflectTest1()
        {
            Vector3F n = new Vector3F(0.45f, 1.28f, 0.86f);
            n = Vector3F.Normalize(n);
            Vector3F a = n;

            Vector3F expected = -n;
            Vector3F actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3f, Vector3f)
        // Reflection when normal and source are negation
        [Test]
        public void Vector3ReflectTest2()
        {
            Vector3F n = new Vector3F(0.45f, 1.28f, 0.86f);
            n = Vector3F.Normalize(n);
            Vector3F a = -n;

            Vector3F expected = n;
            Vector3F actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3f, Vector3f)
        // Reflection when normal and source are perpendicular (a dot n = 0)
        [Test]
        public void Vector3ReflectTest3()
        {
            Vector3F n = new Vector3F(0.45f, 1.28f, 0.86f);
            Vector3F temp = new Vector3F(1.28f, 0.45f, 0.01f);
            // find a perpendicular vector of n
            Vector3F a = Vector3F.Cross(temp, n);

            Vector3F expected = a;
            Vector3F actual = Vector3F.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Vector3SizeofTest()
        {
            Assert.AreEqual(12, sizeof(Vector3F));
            Assert.AreEqual(24, sizeof(Vector3_2x));
            Assert.AreEqual(16, sizeof(Vector3PlusFloat));
            Assert.AreEqual(32, sizeof(Vector3PlusFloat_2x));
        }

        [Test]
        public void Vector3SqrtTest()
        {
            Vector3F a = new Vector3F(-2.5f, 2.0f, 0.5f);
            Vector3F b = new Vector3F(5.5f, 4.5f, 16.5f);
            Assert.AreEqual(2, (int)Vector3F.SquareRoot(b).X);
            Assert.AreEqual(2, (int)Vector3F.SquareRoot(b).Y);
            Assert.AreEqual(4, (int)Vector3F.SquareRoot(b).Z);
            Assert.AreEqual(float.NaN, Vector3F.SquareRoot(a).X);
        }

        // A test for operator - (Vector3f, Vector3f)
        [Test]
        public void Vector3SubtractionTest()
        {
            Vector3F a = new Vector3F(4.0f, 2.0f, 3.0f);

            Vector3F b = new Vector3F(1.0f, 5.0f, 7.0f);

            Vector3F expected = new Vector3F(3.0f, -3.0f, -4.0f);
            Vector3F actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator - did not return the expected value.");
        }

        // A test for Subtract (Vector3f, Vector3f)
        [Test]
        public void Vector3SubtractTest()
        {
            Vector3F a = new Vector3F(1.0f, 6.0f, 3.0f);
            Vector3F b = new Vector3F(5.0f, 2.0f, 3.0f);

            Vector3F expected = new Vector3F(-4.0f, 4.0f, 0.0f);
            Vector3F actual;

            actual = Vector3F.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Vector3ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector3F v1 = new Vector3F(2.0f, 3.0f, 3.3f);
            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}{0} {3:G}>"
                , separator, 2, 3, 3.3);
            Assert.AreEqual(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , separator, 2, 3, 3.3);
            Assert.AreEqual(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2, 3, 3.3);
            Assert.AreEqual(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , separator, 2, 3, 3.3);
            Assert.AreEqual(expectedv3formatted, v3strformatted);
        }

        // A test for Transform (Vector3f, QuaternionF)
        [Test]
        public void Vector3TransformByQuaternionTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector3F expected = Vector3F.Transform(v, m);
            Vector3F actual = Vector3F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        // Transform Vector3F with zero QuaternionF
        [Test]
        public void Vector3TransformByQuaternionTest1()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            QuaternionF q = new QuaternionF();
            Vector3F expected = v;

            Vector3F actual = Vector3F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        // Transform Vector3F with identity QuaternionF
        [Test]
        public void Vector3TransformByQuaternionTest2()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            QuaternionF q = QuaternionF.Identity;
            Vector3F expected = v;

            Vector3F actual = Vector3F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
        }

        // A test for TransformNormal (Vector3f, Matrix4x4F)
        [Test]
        public void Vector3TransformNormalTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector3F expected = new Vector3F(2.19198728f, 1.53349364f, 2.61602545f);
            Vector3F actual;

            actual = Vector3F.TransformNormal(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.TransformNormal did not return the expected value.");
        }

        // A test for Transform(Vector3f, Matrix4x4F)
        [Test]
        public void Vector3TransformTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector3F expected = new Vector3F(12.191987f, 21.533493f, 32.616024f);
            Vector3F actual;

            actual = Vector3F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
        }

        // A test for operator - (Vector3f)
        [Test]
        public void Vector3UnaryNegationTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);

            Vector3F expected = new Vector3F(-1.0f, -2.0f, -3.0f);
            Vector3F actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3f.operator - did not return the expected value.");
        }

        [Test]
        public void Vector3UnaryNegationTest1()
        {
            Vector3F a = -new Vector3F(float.NaN, float.PositiveInfinity, float.NegativeInfinity);
            Vector3F b = -new Vector3F(0.0f, 0.0f, 0.0f);
            Assert.AreEqual(float.NaN, a.X);
            Assert.AreEqual(float.NegativeInfinity, a.Y);
            Assert.AreEqual(float.PositiveInfinity, a.Z);
            Assert.AreEqual(0.0f, b.X);
            Assert.AreEqual(0.0f, b.Y);
            Assert.AreEqual(0.0f, b.Z);
        }

        // A test for UnitX
        [Test]
        public void Vector3UnitXTest()
        {
            Vector3F val = new Vector3F(1.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector3F.UnitX);
        }

        // A test for UnitY
        [Test]
        public void Vector3UnitYTest()
        {
            Vector3F val = new Vector3F(0.0f, 1.0f, 0.0f);
            Assert.AreEqual(val, Vector3F.UnitY);
        }

        // A test for UnitZ
        [Test]
        public void Vector3UnitZTest()
        {
            Vector3F val = new Vector3F(0.0f, 0.0f, 1.0f);
            Assert.AreEqual(val, Vector3F.UnitZ);
        }

        // A test for Zero
        [Test]
        public void Vector3ZeroTest()
        {
            Vector3F val = new Vector3F(0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector3F.Zero);
        }

        #endregion Public Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector3_2x
        {
            private Vector3F _a;
            private Vector3F _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector3PlusFloat
        {
            private Vector3F _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector3PlusFloat_2x
        {
            private Vector3PlusFloat _a;
            private Vector3PlusFloat _b;
        }

        #endregion Private Structs

        #region Private Classes

        private class EmbeddedVectorObject
        {
            #region Public Fields

            public Vector3F FieldVector;

            #endregion Public Fields
        }

        #endregion Private Classes
    }
}