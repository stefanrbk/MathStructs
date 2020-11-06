using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector4FTests
    {
        [Test]
        public void Vector4MarshalSizeTest()
        {
            Assert.AreEqual(16, Marshal.SizeOf<Vector4F>());
            Assert.AreEqual(16, Marshal.SizeOf<Vector4F>(new Vector4F()));
        }

        [Test]
        public void Vector4CopyToTest()
        {
            Vector4F v1 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);

            float[] a = new float[5];
            float[] b = new float[4];

            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, a.Length - 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.AreEqual(0.0f, a[0]);
            Assert.AreEqual(2.5f, a[1]);
            Assert.AreEqual(2.0f, a[2]);
            Assert.AreEqual(3.0f, a[3]);
            Assert.AreEqual(3.3f, a[4]);
            Assert.AreEqual(2.5f, b[0]);
            Assert.AreEqual(2.0f, b[1]);
            Assert.AreEqual(3.0f, b[2]);
            Assert.AreEqual(3.3f, b[3]);
        }

        [Test]
        public void Vector4GetHashCodeTest()
        {
            Vector4F v1 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v2 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v3 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v5 = new Vector4F(3.3f, 3.0f, 2.0f, 2.5f);
            Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector4F v4 = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);
            Vector4F v6 = new Vector4F(1.0f, 0.0f, 0.0f, 0.0f);
            Vector4F v7 = new Vector4F(0.0f, 1.0f, 0.0f, 0.0f);
            Vector4F v8 = new Vector4F(1.0f, 1.0f, 1.0f, 1.0f);
            Vector4F v9 = new Vector4F(1.0f, 1.0f, 0.0f, 0.0f);
            Assert.AreNotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v7.GetHashCode());
            Assert.AreNotEqual(v9.GetHashCode(), v7.GetHashCode());
        }

        [Test]
        public void Vector4ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector4F v1 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);

            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}{0} {3:G}{0} {4:G}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.AreEqual(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.AreEqual(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2.5, 2, 3, 3.3);
            Assert.AreEqual(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.AreEqual(expectedv3formatted, v3strformatted);
        }

        // A test for DistanceSquared (Vector4f, Vector4f)
        [Test]
        public void Vector4DistanceSquaredTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            float expected = 64.0f;
            float actual;

            actual = Vector4F.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.DistanceSquared did not return the expected value.");
        }

        // A test for Distance (Vector4f, Vector4f)
        [Test]
        public void Vector4DistanceTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            float expected = 8.0f;
            float actual;

            actual = Vector4F.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Distance did not return the expected value.");
        }

        // A test for Distance (Vector4f, Vector4f)
        // Distance from the same point
        [Test]
        public void Vector4DistanceTest1()
        {
            Vector4F a = new Vector4F(new Vector2F(1.051f, 2.05f), 3.478f, 1.0f);
            Vector4F b = new Vector4F(new Vector3F(1.051f, 2.05f, 3.478f), 0.0f)
            {
                W = 1.0f
            };

            float actual = Vector4F.Distance(a, b);
            Assert.AreEqual(0.0f, actual);
        }

        // A test for Dot (Vector4f, Vector4f)
        [Test]
        public void Vector4DotTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            float expected = 70.0f;
            float actual;

            actual = Vector4F.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Dot did not return the expected value.");
        }

        // A test for Dot (Vector4f, Vector4f)
        // Dot test for perpendicular vector
        [Test]
        public void Vector4DotTest1()
        {
            Vector3F a = new Vector3F(1.55f, 1.55f, 1);
            Vector3F b = new Vector3F(2.5f, 3, 1.5f);
            Vector3F c = Vector3F.Cross(a, b);

            Vector4F d = new Vector4F(a, 0);
            Vector4F e = new Vector4F(c, 0);

            float actual = Vector4F.Dot(d, e);
            Assert.True(MathHelper.Equal(0.0f, actual), "Vector4f.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Test]
        public void Vector4LengthTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            float w = 4.0f;

            Vector4F target = new Vector4F(a, w);

            float expected = (float)System.Math.Sqrt(30.0f);
            float actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Test]
        public void Vector4LengthTest1()
        {
            Vector4F target = new Vector4F();

            float expected = 0.0f;
            float actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Test]
        public void Vector4LengthSquaredTest()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            float w = 4.0f;

            Vector4F target = new Vector4F(a, w);

            float expected = 30;
            float actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.LengthSquared did not return the expected value.");
        }

        // A test for Min (Vector4f, Vector4f)
        [Test]
        public void Vector4MinTest()
        {
            Vector4F a = new Vector4F(-1.0f, 4.0f, -3.0f, 1000.0f);
            Vector4F b = new Vector4F(2.0f, 1.0f, -1.0f, 0.0f);

            Vector4F expected = new Vector4F(-1.0f, 1.0f, -3.0f, 0.0f);
            Vector4F actual;
            actual = Vector4F.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Min did not return the expected value.");
        }

        // A test for Max (Vector4f, Vector4f)
        [Test]
        public void Vector4MaxTest()
        {
            Vector4F a = new Vector4F(-1.0f, 4.0f, -3.0f, 1000.0f);
            Vector4F b = new Vector4F(2.0f, 1.0f, -1.0f, 0.0f);

            Vector4F expected = new Vector4F(2.0f, 4.0f, -1.0f, 1000.0f);
            Vector4F actual;
            actual = Vector4F.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Max did not return the expected value.");
        }

        [Test]
        public void Vector4MinMaxCodeCoverageTest()
        {
            Vector4F min = Vector4F.Zero;
            Vector4F max = Vector4F.One;
            Vector4F actual;

            // Min.
            actual = Vector4F.Min(min, max);
            Assert.AreEqual(actual, min);

            actual = Vector4F.Min(max, min);
            Assert.AreEqual(actual, min);

            // Max.
            actual = Vector4F.Max(min, max);
            Assert.AreEqual(actual, max);

            actual = Vector4F.Max(max, min);
            Assert.AreEqual(actual, max);
        }

        // A test for Clamp (Vector4f, Vector4f, Vector4f)
        [Test]
        public void Vector4ClampTest()
        {
            Vector4F a = new Vector4F(0.5f, 0.3f, 0.33f, 0.44f);
            Vector4F min = new Vector4F(0.0f, 0.1f, 0.13f, 0.14f);
            Vector4F max = new Vector4F(1.0f, 1.1f, 1.13f, 1.14f);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector4F expected = new Vector4F(0.5f, 0.3f, 0.33f, 0.44f);
            Vector4F actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector4F(2.0f, 3.0f, 4.0f, 5.0f);
            expected = max;
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector4F(-2.0f, -3.0f, -4.0f, -5.0f);
            expected = min;
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector4F(-2.0f, 0.5f, 4.0f, -5.0f);
            expected = new Vector4F(min.X, a.Y, max.Z, min.W);
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector4F(0.0f, 0.1f, 0.13f, 0.14f);
            min = new Vector4F(1.0f, 1.1f, 1.13f, 1.14f);

            // Case W1: specified value is in the range.
            a = new Vector4F(0.5f, 0.3f, 0.33f, 0.44f);
            expected = max;
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector4F(2.0f, 3.0f, 4.0f, 5.0f);
            expected = max;
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector4F(-2.0f, -3.0f, -4.0f, -5.0f);
            expected = max;
            actual = Vector4F.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        [Test]
        public void Vector4LerpTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            float t = 0.5f;

            Vector4F expected = new Vector4F(3.0f, 4.0f, 5.0f, 6.0f);
            Vector4F actual;

            actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with factor zero
        [Test]
        public void Vector4LerpTest1()
        {
            Vector4F a = new Vector4F(new Vector3F(1.0f, 2.0f, 3.0f), 4.0f);
            Vector4F b = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);

            float t = 0.0f;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with factor one
        [Test]
        public void Vector4LerpTest2()
        {
            Vector4F a = new Vector4F(new Vector3F(1.0f, 2.0f, 3.0f), 4.0f);
            Vector4F b = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);

            float t = 1.0f;
            Vector4F expected = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with factor > 1
        [Test]
        public void Vector4LerpTest3()
        {
            Vector4F a = new Vector4F(new Vector3F(0.0f, 0.0f, 0.0f), 0.0f);
            Vector4F b = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);

            float t = 2.0f;
            Vector4F expected = new Vector4F(8.0f, 10.0f, 12.0f, 14.0f);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with factor < 0
        [Test]
        public void Vector4LerpTest4()
        {
            Vector4F a = new Vector4F(new Vector3F(0.0f, 0.0f, 0.0f), 0.0f);
            Vector4F b = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);

            float t = -2.0f;
            Vector4F expected = -(b * 2);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with special float value
        [Test]
        public void Vector4LerpTest5()
        {
            Vector4F a = new Vector4F(45.67f, 90.0f, 0, 0);
            Vector4F b = new Vector4F(float.PositiveInfinity, float.NegativeInfinity, 0, 0);

            float t = 0.408f;
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(float.IsPositiveInfinity(actual.X), "Vector4f.Lerp did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Y), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test from the same point
        [Test]
        public void Vector4LerpTest6()
        {
            Vector4F a = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);
            Vector4F b = new Vector4F(4.0f, 5.0f, 6.0f, 7.0f);

            float t = 0.85f;
            Vector4F expected = a;
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        [Test]
        public void Vector4LerpTest7()
        {
            Vector4F a = new Vector4F(0.44728136f);
            Vector4F b = new Vector4F(0.46345946f);

            float t = 0.26402435f;

            Vector4F expected = new Vector4F(0.45155275f);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4f, Vector4f, float)
        // Lerp test with values known to be innacurate with the old lerp impl
        // (Old code incorrectly gets 0.33333588)
        [Test]
        public void Vector4LerpTest8()
        {
            Vector4F a = new Vector4F(-100);
            Vector4F b = new Vector4F(0.33333334f);

            float t = 1f;

            Vector4F expected = new Vector4F(0.33333334f);
            Vector4F actual = Vector4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
        }

        // A test for Transform (Vector2f, Matrix4x4F)
        [Test]
        public void Vector4TransformTest1()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4F expected = new Vector4F(10.316987f, 22.183012f, 30.3660259f, 1.0f);
            Vector4F actual;

            actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, Matrix4x4F)
        [Test]
        public void Vector4TransformTest2()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4F expected = new Vector4F(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
            Vector4F actual;

            actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4F.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, Matrix4x4F)
        [Test]
        public void Vector4TransformVector4Test()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4F expected = new Vector4F(2.19198728f, 1.53349376f, 2.61602545f, 0.0f);
            Vector4F actual;

            actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");

            //
            v.W = 1.0f;

            expected = new Vector4F(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
            actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, Matrix4x4F)
        // Transform Vector4F with zero matrix
        [Test]
        public void Vector4TransformVector4Test1()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);
            Matrix4x4F m = new Matrix4x4F();
            Vector4F expected = new Vector4F(0, 0, 0, 0);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, Matrix4x4F)
        // Transform Vector4F with identity matrix
        [Test]
        public void Vector4TransformVector4Test2()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);
            Matrix4x4F m = Matrix4x4F.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, Matrix4x4F)
        // Transform Vector3f test
        [Test]
        public void Vector4TransformVector3Test()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4F expected = Vector4F.Transform(new Vector4F(v, 1.0f), m);
            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, Matrix4x4F)
        // Transform Vector3F with zero matrix
        [Test]
        public void Vector4TransformVector3Test1()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            Matrix4x4F m = new Matrix4x4F();
            Vector4F expected = new Vector4F(0, 0, 0, 0);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, Matrix4x4F)
        // Transform Vector3F with identity matrix
        [Test]
        public void Vector4TransformVector3Test2()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            Matrix4x4F m = Matrix4x4F.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 3.0f, 1.0f);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, Matrix4x4F)
        // Transform Vector2f test
        [Test]
        public void Vector4TransformVector2Test()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4F expected = Vector4F.Transform(new Vector4F(v, 0.0f, 1.0f), m);
            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, Matrix4x4F)
        // Transform Vector2f with zero matrix
        [Test]
        public void Vector4TransformVector2Test1()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            Matrix4x4F m = new Matrix4x4F();
            Vector4F expected = new Vector4F(0, 0, 0, 0);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, Matrix4x4F)
        // Transform Vector2F with identity matrix
        [Test]
        public void Vector4TransformVector2Test2()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            Matrix4x4F m = Matrix4x4F.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 0, 1.0f);

            Vector4F actual = Vector4F.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, QuaternionF)
        [Test]
        public void Vector4TransformVector2QuatanionTest()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));

            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector4F expected = Vector4F.Transform(v, m);
            Vector4F actual;

            actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        [Test]
        public void Vector4TransformVector3Quaternion()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector4F expected = Vector4F.Transform(v, m);
            Vector4F actual;

            actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4F.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, QuaternionF)
        [Test]
        public void Vector4TransformVector4QuaternionTest()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector4F expected = Vector4F.Transform(v, m);
            Vector4F actual;

            actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");

            //
            v.W = 1.0f;
            expected.W = 1.0f;
            actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, QuaternionF)
        // Transform Vector4F with zero QuaternionF
        [Test]
        public void Vector4TransformVector4QuaternionTest1()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);
            QuaternionF q = new QuaternionF();
            Vector4F expected = v;

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4f, QuaternionF)
        // Transform Vector4F with identity matrix
        [Test]
        public void Vector4TransformVector4QuaternionTest2()
        {
            Vector4F v = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);
            QuaternionF q = QuaternionF.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 3.0f, 0.0f);

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        // Transform Vector3f test
        [Test]
        public void Vector4TransformVector3QuaternionTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector4F expected = Vector4F.Transform(v, m);
            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        // Transform Vector3F with zero QuaternionF
        [Test]
        public void Vector4TransformVector3QuaternionTest1()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            QuaternionF q = new QuaternionF();
            Vector4F expected = new Vector4F(v, 1.0f);

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3f, QuaternionF)
        // Transform Vector3F with identity QuaternionF
        [Test]
        public void Vector4TransformVector3QuaternionTest2()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            QuaternionF q = QuaternionF.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 3.0f, 1.0f);

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, QuaternionF)
        // Transform Vector2f by QuaternionF test
        [Test]
        public void Vector4TransformVector2QuaternionTest()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Vector4F expected = Vector4F.Transform(v, m);
            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, QuaternionF)
        // Transform Vector2f with zero QuaternionF
        [Test]
        public void Vector4TransformVector2QuaternionTest1()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            QuaternionF q = new QuaternionF();
            Vector4F expected = new Vector4F(1.0f, 2.0f, 0, 1.0f);

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2f, Matrix4x4F)
        // Transform Vector2F with identity QuaternionF
        [Test]
        public void Vector4TransformVector2QuaternionTest2()
        {
            Vector2F v = new Vector2F(1.0f, 2.0f);
            QuaternionF q = QuaternionF.Identity;
            Vector4F expected = new Vector4F(1.0f, 2.0f, 0, 1.0f);

            Vector4F actual = Vector4F.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector4f)
        [Test]
        public void Vector4NormalizeTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4F expected = new Vector4F(
                0.1825741858350553711523232609336f,
                0.3651483716701107423046465218672f,
                0.5477225575051661134569697828008f,
                0.7302967433402214846092930437344f);
            Vector4F actual;

            actual = Vector4F.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4f)
        // Normalize vector of length one
        [Test]
        public void Vector4NormalizeTest1()
        {
            Vector4F a = new Vector4F(1.0f, 0.0f, 0.0f, 0.0f);

            Vector4F expected = new Vector4F(1.0f, 0.0f, 0.0f, 0.0f);
            Vector4F actual = Vector4F.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4f)
        // Normalize vector of length zero
        [Test]
        public void Vector4NormalizeTest2()
        {
            Vector4F a = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);

            Vector4F actual = Vector4F.Normalize(a);
            Assert.True(float.IsNaN(actual.X) && float.IsNaN(actual.Y) && float.IsNaN(actual.Z) && float.IsNaN(actual.W), "Vector4f.Normalize did not return the expected value.");
        }

        // A test for operator - (Vector4f)
        [Test]
        public void Vector4UnaryNegationTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4F expected = new Vector4F(-1.0f, -2.0f, -3.0f, -4.0f);
            Vector4F actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator - did not return the expected value.");
        }

        // A test for operator - (Vector4f, Vector4f)
        [Test]
        public void Vector4SubtractionTest()
        {
            Vector4F a = new Vector4F(1.0f, 6.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 2.0f, 3.0f, 9.0f);

            Vector4F expected = new Vector4F(-4.0f, 4.0f, 0.0f, -5.0f);
            Vector4F actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator - did not return the expected value.");
        }

        // A test for operator * (Vector4f, float)
        [Test]
        public void Vector4MultiplyOperatorTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            const float factor = 2.0f;

            Vector4F expected = new Vector4F(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4F actual;

            actual = a * factor;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
        }

        // A test for operator * (float, Vector4f)
        [Test]
        public void Vector4MultiplyOperatorTest2()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            const float factor = 2.0f;
            Vector4F expected = new Vector4F(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4F actual;

            actual = factor * a;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
        }

        // A test for operator * (Vector4f, Vector4f)
        [Test]
        public void Vector4MultiplyOperatorTest3()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4F expected = new Vector4F(5.0f, 12.0f, 21.0f, 32.0f);
            Vector4F actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
        }

        // A test for operator / (Vector4f, float)
        [Test]
        public void Vector4DivisionTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            float div = 2.0f;

            Vector4F expected = new Vector4F(0.5f, 1.0f, 1.5f, 2.0f);
            Vector4F actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4f, Vector4f)
        [Test]
        public void Vector4DivisionTest1()
        {
            Vector4F a = new Vector4F(1.0f, 6.0f, 7.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 2.0f, 3.0f, 8.0f);

            Vector4F expected = new Vector4F(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f, 4.0f / 8.0f);
            Vector4F actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4f, Vector4f)
        // Divide by zero
        [Test]
        public void Vector4DivisionTest2()
        {
            Vector4F a = new Vector4F(-2.0f, 3.0f, float.MaxValue, float.NaN);

            float div = 0.0f;

            Vector4F actual = a / div;

            Assert.True(float.IsNegativeInfinity(actual.X), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Y), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(actual.Z), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsNaN(actual.W), "Vector4f.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4f, Vector4f)
        // Divide by zero
        [Test]
        public void Vector4DivisionTest3()
        {
            Vector4F a = new Vector4F(0.047f, -3.0f, float.NegativeInfinity, float.MinValue);
            Vector4F b = new Vector4F();

            Vector4F actual = a / b;

            Assert.True(float.IsPositiveInfinity(actual.X), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Y), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.Z), "Vector4f.operator / did not return the expected value.");
            Assert.True(float.IsNegativeInfinity(actual.W), "Vector4f.operator / did not return the expected value.");
        }

        // A test for operator + (Vector4f, Vector4f)
        [Test]
        public void Vector4AdditionTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4F expected = new Vector4F(6.0f, 8.0f, 10.0f, 12.0f);
            Vector4F actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4f.operator + did not return the expected value.");
        }

        [Test]
        public void OperatorAddTest()
        {
            Vector4F v1 = new Vector4F(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v2 = new Vector4F(5.5f, 4.5f, 6.5f, 7.5f);

            Vector4F v3 = v1 + v2;
            Vector4F v5 = new Vector4F(-1.0f, 0.0f, 0.0f, float.NaN);
            Vector4F v4 = v1 + v5;
            Assert.AreEqual(8.0f, v3.X);
            Assert.AreEqual(6.5f, v3.Y);
            Assert.AreEqual(9.5f, v3.Z);
            Assert.AreEqual(10.8f, v3.W);
            Assert.AreEqual(1.5f, v4.X);
            Assert.AreEqual(2.0f, v4.Y);
            Assert.AreEqual(3.0f, v4.Z);
            Assert.AreEqual(float.NaN, v4.W);
        }

        // A test for Vector4f (float, float, float, float)
        [Test]
        public void Vector4ConstructorTest()
        {
            float x = 1.0f;
            float y = 2.0f;
            float z = 3.0f;
            float w = 4.0f;

            Vector4F target = new Vector4F(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4f constructor(x,y,z,w) did not return the expected value.");
        }

        // A test for Vector4f (Vector2f, float, float)
        [Test]
        public void Vector4ConstructorTest1()
        {
            Vector2F a = new Vector2F(1.0f, 2.0f);
            float z = 3.0f;
            float w = 4.0f;

            Vector4F target = new Vector4F(a, z, w);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4f constructor(Vector2f,z,w) did not return the expected value.");
        }

        // A test for Vector4f (Vector3f, float)
        [Test]
        public void Vector4ConstructorTest2()
        {
            Vector3F a = new Vector3F(1.0f, 2.0f, 3.0f);
            float w = 4.0f;

            Vector4F target = new Vector4F(a, w);

            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, a.Z) && MathHelper.Equal(target.W, w),
                "Vector4f constructor(Vector3f,w) did not return the expected value.");
        }

        // A test for Vector4f ()
        // Constructor with no parameter
        [Test]
        public void Vector4ConstructorTest4()
        {
            Vector4F a = new Vector4F();

            Assert.AreEqual(0.0f, a.X);
            Assert.AreEqual(0.0f, a.Y);
            Assert.AreEqual(0.0f, a.Z);
            Assert.AreEqual(0.0f, a.W);
        }

        // A test for Vector4f ()
        // Constructor with special floating values
        [Test]
        public void Vector4ConstructorTest5()
        {
            Vector4F target = new Vector4F(float.NaN, float.MaxValue, float.PositiveInfinity, float.Epsilon);

            Assert.True(float.IsNaN(target.X), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
            Assert.True(float.Equals(float.MaxValue, target.Y), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
            Assert.True(float.IsPositiveInfinity(target.Z), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
            Assert.True(float.Equals(float.Epsilon, target.W), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
        }

        // A test for Add (Vector4f, Vector4f)
        [Test]
        public void Vector4AddTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4F expected = new Vector4F(6.0f, 8.0f, 10.0f, 12.0f);
            Vector4F actual;

            actual = Vector4F.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector4f, float)
        [Test]
        public void Vector4DivideTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            float div = 2.0f;
            Vector4F expected = new Vector4F(0.5f, 1.0f, 1.5f, 2.0f);
            Vector4F actual;
            actual = Vector4F.Divide(a, div);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector4f, Vector4f)
        [Test]
        public void Vector4DivideTest1()
        {
            Vector4F a = new Vector4F(1.0f, 6.0f, 7.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 2.0f, 3.0f, 8.0f);

            Vector4F expected = new Vector4F(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f, 4.0f / 8.0f);
            Vector4F actual;

            actual = Vector4F.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void Vector4EqualsTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Multiply (float, Vector4f)
        [Test]
        public void Vector4MultiplyTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            const float factor = 2.0f;
            Vector4F expected = new Vector4F(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4F actual = Vector4F.Multiply(factor, a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector4f, float)
        [Test]
        public void Vector4MultiplyTest2()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            const float factor = 2.0f;
            Vector4F expected = new Vector4F(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4F actual = Vector4F.Multiply(a, factor);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector4f, Vector4f)
        [Test]
        public void Vector4MultiplyTest3()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4F expected = new Vector4F(5.0f, 12.0f, 21.0f, 32.0f);
            Vector4F actual;

            actual = Vector4F.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Vector4f)
        [Test]
        public void Vector4NegateTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4F expected = new Vector4F(-1.0f, -2.0f, -3.0f, -4.0f);
            Vector4F actual;

            actual = Vector4F.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator != (Vector4f, Vector4f)
        [Test]
        public void Vector4InequalityTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for operator == (Vector4f, Vector4f)
        [Test]
        public void Vector4EqualityTest()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Subtract (Vector4f, Vector4f)
        [Test]
        public void Vector4SubtractTest()
        {
            Vector4F a = new Vector4F(1.0f, 6.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(5.0f, 2.0f, 3.0f, 9.0f);

            Vector4F expected = new Vector4F(-4.0f, 4.0f, 0.0f, -5.0f);
            Vector4F actual;

            actual = Vector4F.Subtract(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for UnitW
        [Test]
        public void Vector4UnitWTest()
        {
            Vector4F val = new Vector4F(0.0f, 0.0f, 0.0f, 1.0f);
            Assert.AreEqual(val, Vector4F.UnitW);
        }

        // A test for UnitX
        [Test]
        public void Vector4UnitXTest()
        {
            Vector4F val = new Vector4F(1.0f, 0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4F.UnitX);
        }

        // A test for UnitY
        [Test]
        public void Vector4UnitYTest()
        {
            Vector4F val = new Vector4F(0.0f, 1.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4F.UnitY);
        }

        // A test for UnitZ
        [Test]
        public void Vector4UnitZTest()
        {
            Vector4F val = new Vector4F(0.0f, 0.0f, 1.0f, 0.0f);
            Assert.AreEqual(val, Vector4F.UnitZ);
        }

        // A test for One
        [Test]
        public void Vector4OneTest()
        {
            Vector4F val = new Vector4F(1.0f, 1.0f, 1.0f, 1.0f);
            Assert.AreEqual(val, Vector4F.One);
        }

        // A test for Zero
        [Test]
        public void Vector4ZeroTest()
        {
            Vector4F val = new Vector4F(0.0f, 0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4F.Zero);
        }

        // A test for Equals (Vector4f)
        [Test]
        public void Vector4EqualsTest1()
        {
            Vector4F a = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4F b = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            Assert.True(a.Equals(b));

            // case 2: compare between different values
            b.X = 10.0f;
            Assert.False(a.Equals(b));
        }

        // A test for Vector4f (float)
        [Test]
        public void Vector4ConstructorTest6()
        {
            float value = 1.0f;
            Vector4F target = new Vector4F(value);

            Vector4F expected = new Vector4F(value, value, value, value);
            Assert.AreEqual(expected, target);

            value = 2.0f;
            target = new Vector4F(value);
            expected = new Vector4F(value, value, value, value);
            Assert.AreEqual(expected, target);
        }

        // A test for Vector4f comparison involving NaN values
        [Test]
        public void Vector4EqualsNanTest()
        {
            Vector4F a = new Vector4F(float.NaN, 0, 0, 0);
            Vector4F b = new Vector4F(0, float.NaN, 0, 0);
            Vector4F c = new Vector4F(0, 0, float.NaN, 0);
            Vector4F d = new Vector4F(0, 0, 0, float.NaN);

            Assert.False(a == Vector4F.Zero);
            Assert.False(b == Vector4F.Zero);
            Assert.False(c == Vector4F.Zero);
            Assert.False(d == Vector4F.Zero);

            Assert.True(a != Vector4F.Zero);
            Assert.True(b != Vector4F.Zero);
            Assert.True(c != Vector4F.Zero);
            Assert.True(d != Vector4F.Zero);

            Assert.False(a.Equals(Vector4F.Zero));
            Assert.False(b.Equals(Vector4F.Zero));
            Assert.False(c.Equals(Vector4F.Zero));
            Assert.False(d.Equals(Vector4F.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        [Test]
        public void Vector4AbsTest()
        {
            Vector4F v1 = new Vector4F(-2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v3 = Vector4F.Abs(new Vector4F(float.PositiveInfinity, 0.0f, float.NegativeInfinity, float.NaN));
            Vector4F v = Vector4F.Abs(v1);
            Assert.AreEqual(2.5f, v.X);
            Assert.AreEqual(2.0f, v.Y);
            Assert.AreEqual(3.0f, v.Z);
            Assert.AreEqual(3.3f, v.W);
            Assert.AreEqual(float.PositiveInfinity, v3.X);
            Assert.AreEqual(0.0f, v3.Y);
            Assert.AreEqual(float.PositiveInfinity, v3.Z);
            Assert.AreEqual(float.NaN, v3.W);
        }

        [Test]
        public void Vector4SqrtTest()
        {
            Vector4F v1 = new Vector4F(-2.5f, 2.0f, 3.0f, 3.3f);
            Vector4F v2 = new Vector4F(5.5f, 4.5f, 6.5f, 7.5f);
            Assert.AreEqual(2, (int)Vector4F.SquareRoot(v2).X);
            Assert.AreEqual(2, (int)Vector4F.SquareRoot(v2).Y);
            Assert.AreEqual(2, (int)Vector4F.SquareRoot(v2).Z);
            Assert.AreEqual(2, (int)Vector4F.SquareRoot(v2).W);
            Assert.AreEqual(float.NaN, Vector4F.SquareRoot(v1).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Vector4SizeofTest()
        {
            Assert.AreEqual(16, sizeof(Vector4F));
            Assert.AreEqual(32, sizeof(Vector4_2x));
            Assert.AreEqual(20, sizeof(Vector4PlusFloat));
            Assert.AreEqual(40, sizeof(Vector4PlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector4_2x
        {
            private Vector4F _a;
            private Vector4F _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector4PlusFloat
        {
            private Vector4F _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Vector4PlusFloat_2x
        {
            private Vector4PlusFloat _a;
            private Vector4PlusFloat _b;
        }

        [Test]
        public void SetFieldsTest()
        {
            Vector4F v3 = new Vector4F(4f, 5f, 6f, 7f)
            {
                X = 1.0f,
                Y = 2.0f,
                Z = 3.0f,
                W = 4.0f
            };
            Assert.AreEqual(1.0f, v3.X);
            Assert.AreEqual(2.0f, v3.Y);
            Assert.AreEqual(3.0f, v3.Z);
            Assert.AreEqual(4.0f, v3.W);
            Vector4F v4 = v3;
            v4.Y = 0.5f;
            v4.Z = 2.2f;
            v4.W = 3.5f;
            Assert.AreEqual(1.0f, v4.X);
            Assert.AreEqual(0.5f, v4.Y);
            Assert.AreEqual(2.2f, v4.Z);
            Assert.AreEqual(3.5f, v4.W);
            Assert.AreEqual(2.0f, v3.Y);
        }

        [Test]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0f;
            evo.FieldVector.Y = 5.0f;
            evo.FieldVector.Z = 5.0f;
            evo.FieldVector.W = 5.0f;
            Assert.AreEqual(5.0f, evo.FieldVector.X);
            Assert.AreEqual(5.0f, evo.FieldVector.Y);
            Assert.AreEqual(5.0f, evo.FieldVector.Z);
            Assert.AreEqual(5.0f, evo.FieldVector.W);
        }

        [Test]
        public void DeeplyEmbeddedObjectTest()
        {
            DeeplyEmbeddedClass obj = new DeeplyEmbeddedClass();
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector.X = 5f;
            Assert.AreEqual(5f, obj.RootEmbeddedObject.X);
            Assert.AreEqual(5f, obj.RootEmbeddedObject.Y);
            Assert.AreEqual(1f, obj.RootEmbeddedObject.Z);
            Assert.AreEqual(-5f, obj.RootEmbeddedObject.W);
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4F(1, 2, 3, 4);
            Assert.AreEqual(1f, obj.RootEmbeddedObject.X);
            Assert.AreEqual(2f, obj.RootEmbeddedObject.Y);
            Assert.AreEqual(3f, obj.RootEmbeddedObject.Z);
            Assert.AreEqual(4f, obj.RootEmbeddedObject.W);
        }

        [Test]
        public void DeeplyEmbeddedStructTest()
        {
            DeeplyEmbeddedStruct obj = DeeplyEmbeddedStruct.Create();
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector.X = 5f;
            Assert.AreEqual(5f, obj.RootEmbeddedObject.X);
            Assert.AreEqual(5f, obj.RootEmbeddedObject.Y);
            Assert.AreEqual(1f, obj.RootEmbeddedObject.Z);
            Assert.AreEqual(-5f, obj.RootEmbeddedObject.W);
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4F(1, 2, 3, 4);
            Assert.AreEqual(1f, obj.RootEmbeddedObject.X);
            Assert.AreEqual(2f, obj.RootEmbeddedObject.Y);
            Assert.AreEqual(3f, obj.RootEmbeddedObject.Z);
            Assert.AreEqual(4f, obj.RootEmbeddedObject.W);
        }

        private class EmbeddedVectorObject
        {
            public Vector4F FieldVector;
        }

        private class DeeplyEmbeddedClass
        {
            public readonly Level0 L0 = new Level0();
            public Vector4F RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }

            public class Level0
            {
                public readonly Level1 L1 = new Level1();

                public class Level1
                {
                    public readonly Level2 L2 = new Level2();

                    public class Level2
                    {
                        public readonly Level3 L3 = new Level3();

                        public class Level3
                        {
                            public readonly Level4 L4 = new Level4();

                            public class Level4
                            {
                                public readonly Level5 L5 = new Level5();

                                public class Level5
                                {
                                    public readonly Level6 L6 = new Level6();

                                    public class Level6
                                    {
                                        public readonly Level7 L7 = new Level7();

                                        public class Level7
                                        {
                                            public Vector4F EmbeddedVector = new Vector4F(1, 5, 1, -5);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Contrived test for strangely-sized and shaped embedded structures, with unused buffer fields.
#pragma warning disable 0169

        private struct DeeplyEmbeddedStruct
        {
            public static DeeplyEmbeddedStruct Create()
            {
                var obj = new DeeplyEmbeddedStruct
                {
                    L0 = new Level0
                    {
                        L1 = new Level0.Level1
                        {
                            L2 = new Level0.Level1.Level2
                            {
                                L3 = new Level0.Level1.Level2.Level3
                                {
                                    L4 = new Level0.Level1.Level2.Level3.Level4
                                    {
                                        L5 = new Level0.Level1.Level2.Level3.Level4.Level5
                                        {
                                            L6 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6
                                            {
                                                L7 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6.Level7
                                                {
                                                    EmbeddedVector = new Vector4F(1, 5, 1, -5)
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                return obj;
            }

            public Level0 L0;
            public Vector4F RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }

            public struct Level0
            {
#pragma warning disable IDE0051 // Remove unused private members
                private readonly float _buffer0, _buffer1;
                public Level1 L1;
                private readonly float _buffer2;

                public struct Level1
                {
                    private readonly float _buffer0, _buffer1;
                    public Level2 L2;
                    private readonly byte _buffer2;

                    public struct Level2
                    {
                        public Level3 L3;
                        private readonly float _buffer0;
                        private readonly byte _buffer1;

                        public struct Level3
                        {
                            public Level4 L4;

                            public struct Level4
                            {
                                private readonly float _buffer0;
                                public Level5 L5;
                                private readonly long _buffer1;
                                private readonly byte _buffer2;
                                private readonly double _buffer3;

                                public struct Level5
                                {
                                    private readonly byte _buffer0;
                                    public Level6 L6;

                                    public struct Level6
                                    {
                                        private readonly byte _buffer0;
                                        public Level7 L7;
                                        private readonly byte _buffer1, _buffer2;

                                        public struct Level7
                                        {
                                            public Vector4F EmbeddedVector;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

#pragma warning restore IDE0051 // Remove unused private members
            }
        }

#pragma warning restore 0169
    }
}