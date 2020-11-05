using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector4DTests
    {
        [Test]
        public void Vector4MarshalSizeTest()
        {
            Assert.AreEqual(32, Marshal.SizeOf<Vector4D>());
            Assert.AreEqual(32, Marshal.SizeOf<Vector4D>(new Vector4D()));
        }

        [Test]
        public void Vector4CopyToTest()
        {
            Vector4D v1 = new Vector4D(2.5f, 2.0f, 3.0f, 3.3f);

            double[] a = new double[5];
            double[] b = new double[4];

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
            Vector4D v1 = new Vector4D(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4D v2 = new Vector4D(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4D v3 = new Vector4D(2.5f, 2.0f, 3.0f, 3.3f);
            Vector4D v5 = new Vector4D(3.3f, 3.0f, 2.0f, 2.5f);
            Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector4D v4 = new Vector4D(0.0f, 0.0f, 0.0f, 0.0f);
            Vector4D v6 = new Vector4D(1.0f, 0.0f, 0.0f, 0.0f);
            Vector4D v7 = new Vector4D(0.0f, 1.0f, 0.0f, 0.0f);
            Vector4D v8 = new Vector4D(1.0f, 1.0f, 1.0f, 1.0f);
            Vector4D v9 = new Vector4D(1.0f, 1.0f, 0.0f, 0.0f);
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

            Vector4D v1 = new Vector4D(2.5, 2.0, 3.0, 3.3);

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

        // A test for DistanceSquared (Vector4D, Vector4D)
        [Test]
        public void Vector4DistanceSquaredTest()
        {
            Vector4D a = new Vector4D(1.0, 2.0, 3.0, 4.0);
            Vector4D b = new Vector4D(5.0, 6.0, 7.0, 8.0);

            double expected = 64.0;
            double actual;

            actual = Vector4D.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.DistanceSquared did not return the expected value.");
        }

        // A test for Distance (Vector4D, Vector4D)
        [Test]
        public void Vector4DistanceTest()
        {
            Vector4D a = new Vector4D(1.0, 2.0, 3.0, 4.0);
            Vector4D b = new Vector4D(5.0, 6.0, 7.0, 8.0);

            double expected = 8.0f;
            double actual;

            actual = Vector4D.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Distance did not return the expected value.");
        }

        // A test for Distance (Vector4D, Vector4D)
        // Distance from the same point
        [Test]
        public void Vector4DistanceTest1()
        {
            Vector4D a = new Vector4D(new Vector2D(1.051, 2.05), 3.478, 1.0);
            Vector4D b = new Vector4D(new Vector3D(1.051, 2.05, 3.478), 0.0)
            {
                W = 1.0
            };

            double actual = Vector4D.Distance(a, b);
            Assert.AreEqual(0.0, actual);
        }

        // A test for Dot (Vector4D, Vector4D)
        [Test]
        public void Vector4DotTest()
        {
            Vector4D a = new Vector4D(1.0, 2.0, 3.0, 4.0);
            Vector4D b = new Vector4D(5.0, 6.0, 7.0, 8.0);

            double expected = 70.0;
            double actual;

            actual = Vector4D.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Dot did not return the expected value.");
        }

        // A test for Dot (Vector4D, Vector4D)
        // Dot test for perpendicular vector
        [Test]
        public void Vector4DotTest1()
        {
            Vector3D a = new Vector3D(1.55, 1.55, 1);
            Vector3D b = new Vector3D(2.5, 3, 1.5);
            Vector3D c = Vector3D.Cross(a, b);

            Vector4D d = new Vector4D(a, 0);
            Vector4D e = new Vector4D(c, 0);

            double actual = Vector4D.Dot(d, e);
            Assert.True(MathHelper.Equal(0.0, actual), "Vector4D.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Test]
        public void Vector4LengthTest()
        {
            Vector3D a = new Vector3D(1.0, 2.0, 3.0);
            double w = 4.0;

            Vector4D target = new Vector4D(a, w);

            double expected = (double)System.Math.Sqrt(30.0);
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Test]
        public void Vector4LengthTest1()
        {
            Vector4D target = new Vector4D();

            double expected = 0.0;
            double actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Test]
        public void Vector4LengthSquaredTest()
        {
            Vector3D a = new Vector3D(1.0, 2.0, 3.0);
            double w = 4.0;

            Vector4D target = new Vector4D(a, w);

            double expected = 30;
            double actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.LengthSquared did not return the expected value.");
        }

        // A test for Min (Vector4D, Vector4D)
        [Test]
        public void Vector4MinTest()
        {
            Vector4D a = new Vector4D(-1.0f, 4.0f, -3.0f, 1000.0f);
            Vector4D b = new Vector4D(2.0f, 1.0f, -1.0f, 0.0f);

            Vector4D expected = new Vector4D(-1.0f, 1.0f, -3.0f, 0.0f);
            Vector4D actual;
            actual = Vector4D.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Min did not return the expected value.");
        }

        // A test for Max (Vector4D, Vector4D)
        [Test]
        public void Vector4MaxTest()
        {
            Vector4D a = new Vector4D(-1.0f, 4.0f, -3.0f, 1000.0f);
            Vector4D b = new Vector4D(2.0f, 1.0f, -1.0f, 0.0f);

            Vector4D expected = new Vector4D(2.0f, 4.0f, -1.0f, 1000.0f);
            Vector4D actual;
            actual = Vector4D.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Max did not return the expected value.");
        }

        [Test]
        public void Vector4MinMaxCodeCoverageTest()
        {
            Vector4D min = Vector4D.Zero;
            Vector4D max = Vector4D.One;
            Vector4D actual;

            // Min.
            actual = Vector4D.Min(min, max);
            Assert.AreEqual(actual, min);

            actual = Vector4D.Min(max, min);
            Assert.AreEqual(actual, min);

            // Max.
            actual = Vector4D.Max(min, max);
            Assert.AreEqual(actual, max);

            actual = Vector4D.Max(max, min);
            Assert.AreEqual(actual, max);
        }

        // A test for Clamp (Vector4D, Vector4D, Vector4D)
        [Test]
        public void Vector4ClampTest()
        {
            Vector4D a = new Vector4D(0.5f, 0.3f, 0.33f, 0.44f);
            Vector4D min = new Vector4D(0.0f, 0.1f, 0.13f, 0.14f);
            Vector4D max = new Vector4D(1.0f, 1.1f, 1.13f, 1.14f);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector4D expected = new Vector4D(0.5f, 0.3f, 0.33f, 0.44f);
            Vector4D actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector4D(2.0f, 3.0f, 4.0f, 5.0f);
            expected = max;
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector4D(-2.0f, -3.0f, -4.0f, -5.0f);
            expected = min;
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector4D(-2.0f, 0.5f, 4.0f, -5.0f);
            expected = new Vector4D(min.X, a.Y, max.Z, min.W);
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector4D(0.0f, 0.1f, 0.13f, 0.14f);
            min = new Vector4D(1.0f, 1.1f, 1.13f, 1.14f);

            // Case W1: specified value is in the range.
            a = new Vector4D(0.5f, 0.3f, 0.33f, 0.44f);
            expected = max;
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector4D(2.0f, 3.0f, 4.0f, 5.0f);
            expected = max;
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector4D(-2.0f, -3.0f, -4.0f, -5.0f);
            expected = max;
            actual = Vector4D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Clamp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        [Test]
        public void Vector4LerpTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 6.0f, 7.0f, 8.0f);

            double t = 0.5f;

            Vector4D expected = new Vector4D(3.0f, 4.0f, 5.0f, 6.0f);
            Vector4D actual;

            actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with factor zero
        [Test]
        public void Vector4LerpTest1()
        {
            Vector4D a = new Vector4D(new Vector3D(1.0f, 2.0f, 3.0f), 4.0f);
            Vector4D b = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);

            double t = 0.0f;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with factor one
        [Test]
        public void Vector4LerpTest2()
        {
            Vector4D a = new Vector4D(new Vector3D(1.0f, 2.0f, 3.0f), 4.0f);
            Vector4D b = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);

            double t = 1.0f;
            Vector4D expected = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with factor > 1
        [Test]
        public void Vector4LerpTest3()
        {
            Vector4D a = new Vector4D(new Vector3D(0.0f, 0.0f, 0.0f), 0.0f);
            Vector4D b = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);

            double t = 2.0f;
            Vector4D expected = new Vector4D(8.0f, 10.0f, 12.0f, 14.0f);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with factor < 0
        [Test]
        public void Vector4LerpTest4()
        {
            Vector4D a = new Vector4D(new Vector3D(0.0f, 0.0f, 0.0f), 0.0f);
            Vector4D b = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);

            double t = -2.0f;
            Vector4D expected = -(b * 2);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with special double value
        [Test]
        public void Vector4LerpTest5()
        {
            Vector4D a = new Vector4D(45.67f, 90.0f, 0, 0);
            Vector4D b = new Vector4D(double.PositiveInfinity, double.NegativeInfinity, 0, 0);

            double t = 0.408f;
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(double.IsPositiveInfinity(actual.X), "Vector4D.Lerp did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test from the same point
        [Test]
        public void Vector4LerpTest6()
        {
            Vector4D a = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);
            Vector4D b = new Vector4D(4.0f, 5.0f, 6.0f, 7.0f);

            double t = 0.85f;
            Vector4D expected = a;
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with values known to be innacurate with the old lerp impl
        [Test]
        public void Vector4LerpTest7()
        {
            Vector4D a = new Vector4D(0.44728136f);
            Vector4D b = new Vector4D(0.46345946f);

            double t = 0.26402435f;

            Vector4D expected = new Vector4D(0.45155275f);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4D, Vector4D, double)
        // Lerp test with values known to be innacurate with the old lerp impl
        // (Old code incorrectly gets 0.33333588)
        [Test]
        public void Vector4LerpTest8()
        {
            Vector4D a = new Vector4D(-100);
            Vector4D b = new Vector4D(0.33333334f);

            double t = 1f;

            Vector4D expected = new Vector4D(0.33333334f);
            Vector4D actual = Vector4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Lerp did not return the expected value.");
        }

        // A test for Transform (Vector2D, Matrix4x4D)
        [Test]
        public void Vector4TransformTest1()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4D expected = new Vector4D(10.316987f, 22.183012f, 30.3660259f, 1.0f);
            Vector4D actual;

            actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, Matrix4x4D)
        [Test]
        public void Vector4TransformTest2()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4D expected = new Vector4D(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
            Vector4D actual;

            actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, Matrix4x4D)
        [Test]
        public void Vector4TransformVector4Test()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4D expected = new Vector4D(2.19198728f, 1.53349376f, 2.61602545f, 0.0f);
            Vector4D actual;

            actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");

            //
            v.W = 1.0f;

            expected = new Vector4D(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
            actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, Matrix4x4D)
        // Transform Vector4D with zero matrix
        [Test]
        public void Vector4TransformVector4Test1()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);
            Matrix4x4D m = new Matrix4x4D();
            Vector4D expected = new Vector4D(0, 0, 0, 0);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, Matrix4x4D)
        // Transform Vector4D with identity matrix
        [Test]
        public void Vector4TransformVector4Test2()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);
            Matrix4x4D m = Matrix4x4D.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, Matrix4x4D)
        // Transform Vector3D test
        [Test]
        public void Vector4TransformVector3Test()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4D expected = Vector4D.Transform(new Vector4D(v, 1.0f), m);
            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, Matrix4x4D)
        // Transform Vector3D with zero matrix
        [Test]
        public void Vector4TransformVector3Test1()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            Matrix4x4D m = new Matrix4x4D();
            Vector4D expected = new Vector4D(0, 0, 0, 0);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, Matrix4x4D)
        // Transform Vector3D with identity matrix
        [Test]
        public void Vector4TransformVector3Test2()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            Matrix4x4D m = Matrix4x4D.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 3.0f, 1.0f);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, Matrix4x4D)
        // Transform Vector2D test
        [Test]
        public void Vector4TransformVector2Test()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector4D expected = Vector4D.Transform(new Vector4D(v, 0.0f, 1.0f), m);
            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, Matrix4x4D)
        // Transform Vector2D with zero matrix
        [Test]
        public void Vector4TransformVector2Test1()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);
            Matrix4x4D m = new Matrix4x4D();
            Vector4D expected = new Vector4D(0, 0, 0, 0);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, Matrix4x4D)
        // Transform Vector2D with identity matrix
        [Test]
        public void Vector4TransformVector2Test2()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);
            Matrix4x4D m = Matrix4x4D.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 0, 1.0f);

            Vector4D actual = Vector4D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, QuaternionD)
        [Test]
        public void Vector4TransformVector2QuatanionTest()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));

            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector4D expected = Vector4D.Transform(v, m);
            Vector4D actual;

            actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        [Test]
        public void Vector4TransformVector3Quaternion()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector4D expected = Vector4D.Transform(v, m);
            Vector4D actual;

            actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, QuaternionD)
        [Test]
        public void Vector4TransformVector4QuaternionTest()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector4D expected = Vector4D.Transform(v, m);
            Vector4D actual;

            actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");

            //
            v.W = 1.0f;
            expected.W = 1.0f;
            actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, QuaternionD)
        // Transform Vector4D with zero QuaternionD
        [Test]
        public void Vector4TransformVector4QuaternionTest1()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);
            QuaternionD q = new QuaternionD();
            Vector4D expected = v;

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4D, QuaternionD)
        // Transform Vector4D with identity matrix
        [Test]
        public void Vector4TransformVector4QuaternionTest2()
        {
            Vector4D v = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);
            QuaternionD q = QuaternionD.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 3.0f, 0.0f);

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        // Transform Vector3D test
        [Test]
        public void Vector4TransformVector3QuaternionTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector4D expected = Vector4D.Transform(v, m);
            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        // Transform Vector3D with zero QuaternionD
        [Test]
        public void Vector4TransformVector3QuaternionTest1()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            QuaternionD q = new QuaternionD();
            Vector4D expected = new Vector4D(v, 1.0f);

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        // Transform Vector3D with identity QuaternionD
        [Test]
        public void Vector4TransformVector3QuaternionTest2()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            QuaternionD q = QuaternionD.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 3.0f, 1.0f);

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, QuaternionD)
        // Transform Vector2D by QuaternionD test
        [Test]
        public void Vector4TransformVector2QuaternionTest()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector4D expected = Vector4D.Transform(v, m);
            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, QuaternionD)
        // Transform Vector2D with zero QuaternionD
        [Test]
        public void Vector4TransformVector2QuaternionTest1()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);
            QuaternionD q = new QuaternionD();
            Vector4D expected = new Vector4D(1.0f, 2.0f, 0, 1.0f);

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2D, Matrix4x4D)
        // Transform Vector2D with identity QuaternionD
        [Test]
        public void Vector4TransformVector2QuaternionTest2()
        {
            Vector2D v = new Vector2D(1.0f, 2.0f);
            QuaternionD q = QuaternionD.Identity;
            Vector4D expected = new Vector4D(1.0f, 2.0f, 0, 1.0f);

            Vector4D actual = Vector4D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector4D)
        [Test]
        public void Vector4NormalizeTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4D expected = new Vector4D(
                0.1825741858350553711523232609336f,
                0.3651483716701107423046465218672f,
                0.5477225575051661134569697828008f,
                0.7302967433402214846092930437344f);
            Vector4D actual;

            actual = Vector4D.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4D)
        // Normalize vector of length one
        [Test]
        public void Vector4NormalizeTest1()
        {
            Vector4D a = new Vector4D(1.0f, 0.0f, 0.0f, 0.0f);

            Vector4D expected = new Vector4D(1.0f, 0.0f, 0.0f, 0.0f);
            Vector4D actual = Vector4D.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4D)
        // Normalize vector of length zero
        [Test]
        public void Vector4NormalizeTest2()
        {
            Vector4D a = new Vector4D(0.0f, 0.0f, 0.0f, 0.0f);

            Vector4D actual = Vector4D.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W), "Vector4D.Normalize did not return the expected value.");
        }

        // A test for operator - (Vector4D)
        [Test]
        public void Vector4UnaryNegationTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4D expected = new Vector4D(-1.0f, -2.0f, -3.0f, -4.0f);
            Vector4D actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator - did not return the expected value.");
        }

        // A test for operator - (Vector4D, Vector4D)
        [Test]
        public void Vector4SubtractionTest()
        {
            Vector4D a = new Vector4D(1.0f, 6.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 2.0f, 3.0f, 9.0f);

            Vector4D expected = new Vector4D(-4.0f, 4.0f, 0.0f, -5.0f);
            Vector4D actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator - did not return the expected value.");
        }

        // A test for operator * (Vector4D, double)
        [Test]
        public void Vector4MultiplyOperatorTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            const double factor = 2.0f;

            Vector4D expected = new Vector4D(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4D actual;

            actual = a * factor;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator * did not return the expected value.");
        }

        // A test for operator * (double, Vector4D)
        [Test]
        public void Vector4MultiplyOperatorTest2()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            const double factor = 2.0f;
            Vector4D expected = new Vector4D(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4D actual;

            actual = factor * a;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator * did not return the expected value.");
        }

        // A test for operator * (Vector4D, Vector4D)
        [Test]
        public void Vector4MultiplyOperatorTest3()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4D expected = new Vector4D(5.0f, 12.0f, 21.0f, 32.0f);
            Vector4D actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator * did not return the expected value.");
        }

        // A test for operator / (Vector4D, double)
        [Test]
        public void Vector4DivisionTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            double div = 2.0f;

            Vector4D expected = new Vector4D(0.5f, 1.0f, 1.5f, 2.0f);
            Vector4D actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4D, Vector4D)
        [Test]
        public void Vector4DivisionTest1()
        {
            Vector4D a = new Vector4D(1.0f, 6.0f, 7.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 2.0f, 3.0f, 8.0f);

            Vector4D expected = new Vector4D(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f, 4.0f / 8.0f);
            Vector4D actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4D, Vector4D)
        // Divide by zero
        [Test]
        public void Vector4DivisionTest2()
        {
            Vector4D a = new Vector4D(-2.0f, 3.0f, double.MaxValue, double.NaN);

            double div = 0.0f;

            Vector4D actual = a / div;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Z), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsNaN(actual.W), "Vector4D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4D, Vector4D)
        // Divide by zero
        [Test]
        public void Vector4DivisionTest3()
        {
            Vector4D a = new Vector4D(0.047f, -3.0f, double.NegativeInfinity, double.MinValue);
            Vector4D b = new Vector4D();

            Vector4D actual = a / b;

            Assert.True(double.IsPositiveInfinity(actual.X), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Z), "Vector4D.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.W), "Vector4D.operator / did not return the expected value.");
        }

        // A test for operator + (Vector4D, Vector4D)
        [Test]
        public void Vector4AdditionTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4D expected = new Vector4D(6.0f, 8.0f, 10.0f, 12.0f);
            Vector4D actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4D.operator + did not return the expected value.");
        }

        [Test]
        public void OperatorAddTest()
        {
            Vector4D v1 = new Vector4D(2.5, 2.0, 3.0, 3.3);
            Vector4D v2 = new Vector4D(5.5, 4.5, 6.5, 7.5);

            Vector4D v3 = v1 + v2;
            Vector4D v5 = new Vector4D(-1.0, 0.0, 0.0, double.NaN);
            Vector4D v4 = v1 + v5;
            Assert.AreEqual(8.0, v3.X);
            Assert.AreEqual(6.5, v3.Y);
            Assert.AreEqual(9.5, v3.Z);
            Assert.AreEqual(10.8, v3.W);
            Assert.AreEqual(1.5, v4.X);
            Assert.AreEqual(2.0, v4.Y);
            Assert.AreEqual(3.0, v4.Z);
            Assert.AreEqual(double.NaN, v4.W);
        }

        // A test for Vector4D (double, double, double, double)
        [Test]
        public void Vector4ConstructorTest()
        {
            double x = 1.0f;
            double y = 2.0f;
            double z = 3.0f;
            double w = 4.0f;

            Vector4D target = new Vector4D(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4D constructor(x,y,z,w) did not return the expected value.");
        }

        // A test for Vector4D (Vector2D, double, double)
        [Test]
        public void Vector4ConstructorTest1()
        {
            Vector2D a = new Vector2D(1.0f, 2.0f);
            double z = 3.0f;
            double w = 4.0f;

            Vector4D target = new Vector4D(a, z, w);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4D constructor(Vector2D,z,w) did not return the expected value.");
        }

        // A test for Vector4D (Vector3D, double)
        [Test]
        public void Vector4ConstructorTest2()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            double w = 4.0f;

            Vector4D target = new Vector4D(a, w);

            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, a.Z) && MathHelper.Equal(target.W, w),
                "Vector4D constructor(Vector3D,w) did not return the expected value.");
        }

        // A test for Vector4D ()
        // Constructor with no parameter
        [Test]
        public void Vector4ConstructorTest4()
        {
            Vector4D a = new Vector4D();

            Assert.AreEqual(0.0f, a.X);
            Assert.AreEqual(0.0f, a.Y);
            Assert.AreEqual(0.0f, a.Z);
            Assert.AreEqual(0.0f, a.W);
        }

        // A test for Vector4D ()
        // Constructor with special doubleing values
        [Test]
        public void Vector4ConstructorTest5()
        {
            Vector4D target = new Vector4D(double.NaN, double.MaxValue, double.PositiveInfinity, double.Epsilon);

            Assert.True(double.IsNaN(target.X), "Vector4D.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.Equals(double.MaxValue, target.Y), "Vector4D.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(target.Z), "Vector4D.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.Equals(double.Epsilon, target.W), "Vector4D.constructor (double, double, double, double) did not return the expected value.");
        }

        // A test for Add (Vector4D, Vector4D)
        [Test]
        public void Vector4AddTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4D expected = new Vector4D(6.0f, 8.0f, 10.0f, 12.0f);
            Vector4D actual;

            actual = Vector4D.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector4D, double)
        [Test]
        public void Vector4DivideTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            double div = 2.0f;
            Vector4D expected = new Vector4D(0.5f, 1.0f, 1.5f, 2.0f);
            Vector4D actual;
            actual = Vector4D.Divide(a, div);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector4D, Vector4D)
        [Test]
        public void Vector4DivideTest1()
        {
            Vector4D a = new Vector4D(1.0, 6.0, 7.0, 4.0);
            Vector4D b = new Vector4D(5.0, 2.0, 3.0, 8.0);

            Vector4D expected = new Vector4D(1.0 / 5.0, 6.0 / 2.0, 7.0 / 3.0, 4.0 / 8.0);
            Vector4D actual;

            actual = Vector4D.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void Vector4EqualsTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

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
            obj = new QuaternionD();
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (double, Vector4D)
        [Test]
        public void Vector4MultiplyTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            const double factor = 2.0f;
            Vector4D expected = new Vector4D(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4D actual = Vector4D.Multiply(factor, a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector4D, double)
        [Test]
        public void Vector4MultiplyTest2()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            const double factor = 2.0f;
            Vector4D expected = new Vector4D(2.0f, 4.0f, 6.0f, 8.0f);
            Vector4D actual = Vector4D.Multiply(a, factor);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector4D, Vector4D)
        [Test]
        public void Vector4MultiplyTest3()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 6.0f, 7.0f, 8.0f);

            Vector4D expected = new Vector4D(5.0f, 12.0f, 21.0f, 32.0f);
            Vector4D actual;

            actual = Vector4D.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Vector4D)
        [Test]
        public void Vector4NegateTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            Vector4D expected = new Vector4D(-1.0f, -2.0f, -3.0f, -4.0f);
            Vector4D actual;

            actual = Vector4D.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator != (Vector4D, Vector4D)
        [Test]
        public void Vector4InequalityTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for operator == (Vector4D, Vector4D)
        [Test]
        public void Vector4EqualityTest()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Subtract (Vector4D, Vector4D)
        [Test]
        public void Vector4SubtractTest()
        {
            Vector4D a = new Vector4D(1.0f, 6.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(5.0f, 2.0f, 3.0f, 9.0f);

            Vector4D expected = new Vector4D(-4.0f, 4.0f, 0.0f, -5.0f);
            Vector4D actual;

            actual = Vector4D.Subtract(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for UnitW
        [Test]
        public void Vector4UnitWTest()
        {
            Vector4D val = new Vector4D(0.0f, 0.0f, 0.0f, 1.0f);
            Assert.AreEqual(val, Vector4D.UnitW);
        }

        // A test for UnitX
        [Test]
        public void Vector4UnitXTest()
        {
            Vector4D val = new Vector4D(1.0f, 0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4D.UnitX);
        }

        // A test for UnitY
        [Test]
        public void Vector4UnitYTest()
        {
            Vector4D val = new Vector4D(0.0f, 1.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4D.UnitY);
        }

        // A test for UnitZ
        [Test]
        public void Vector4UnitZTest()
        {
            Vector4D val = new Vector4D(0.0f, 0.0f, 1.0f, 0.0f);
            Assert.AreEqual(val, Vector4D.UnitZ);
        }

        // A test for One
        [Test]
        public void Vector4OneTest()
        {
            Vector4D val = new Vector4D(1.0f, 1.0f, 1.0f, 1.0f);
            Assert.AreEqual(val, Vector4D.One);
        }

        // A test for Zero
        [Test]
        public void Vector4ZeroTest()
        {
            Vector4D val = new Vector4D(0.0f, 0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector4D.Zero);
        }

        // A test for Equals (Vector4D)
        [Test]
        public void Vector4EqualsTest1()
        {
            Vector4D a = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            Vector4D b = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            Assert.True(a.Equals(b));

            // case 2: compare between different values
            b.X = 10.0f;
            Assert.False(a.Equals(b));
        }

        // A test for Vector4D (double)
        [Test]
        public void Vector4ConstructorTest6()
        {
            double value = 1.0f;
            Vector4D target = new Vector4D(value);

            Vector4D expected = new Vector4D(value, value, value, value);
            Assert.AreEqual(expected, target);

            value = 2.0f;
            target = new Vector4D(value);
            expected = new Vector4D(value, value, value, value);
            Assert.AreEqual(expected, target);
        }

        // A test for Vector4D comparison involving NaN values
        [Test]
        public void Vector4EqualsNanTest()
        {
            Vector4D a = new Vector4D(double.NaN, 0, 0, 0);
            Vector4D b = new Vector4D(0, double.NaN, 0, 0);
            Vector4D c = new Vector4D(0, 0, double.NaN, 0);
            Vector4D d = new Vector4D(0, 0, 0, double.NaN);

            Assert.False(a == Vector4D.Zero);
            Assert.False(b == Vector4D.Zero);
            Assert.False(c == Vector4D.Zero);
            Assert.False(d == Vector4D.Zero);

            Assert.True(a != Vector4D.Zero);
            Assert.True(b != Vector4D.Zero);
            Assert.True(c != Vector4D.Zero);
            Assert.True(d != Vector4D.Zero);

            Assert.False(a.Equals(Vector4D.Zero));
            Assert.False(b.Equals(Vector4D.Zero));
            Assert.False(c.Equals(Vector4D.Zero));
            Assert.False(d.Equals(Vector4D.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        [Test]
        public void Vector4AbsTest()
        {
            Vector4D v1 = new Vector4D(-2.5f, 2.0f, 3.0f, 3.3f);
            Vector4D v3 = Vector4D.Abs(new Vector4D(double.PositiveInfinity, 0.0f, double.NegativeInfinity, double.NaN));
            Vector4D v = Vector4D.Abs(v1);
            Assert.AreEqual(2.5f, v.X);
            Assert.AreEqual(2.0f, v.Y);
            Assert.AreEqual(3.0f, v.Z);
            Assert.AreEqual(3.3f, v.W);
            Assert.AreEqual(double.PositiveInfinity, v3.X);
            Assert.AreEqual(0.0f, v3.Y);
            Assert.AreEqual(double.PositiveInfinity, v3.Z);
            Assert.AreEqual(double.NaN, v3.W);
        }

        [Test]
        public void Vector4SqrtTest()
        {
            Vector4D v1 = new Vector4D(-2.5f, 2.0f, 3.0f, 3.3f);
            Vector4D v2 = new Vector4D(5.5f, 4.5f, 6.5f, 7.5f);
            Assert.AreEqual(2, (int)Vector4D.SquareRoot(v2).X);
            Assert.AreEqual(2, (int)Vector4D.SquareRoot(v2).Y);
            Assert.AreEqual(2, (int)Vector4D.SquareRoot(v2).Z);
            Assert.AreEqual(2, (int)Vector4D.SquareRoot(v2).W);
            Assert.AreEqual(double.NaN, Vector4D.SquareRoot(v1).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Vector4SizeofTest()
        {
            Assert.AreEqual(32, sizeof(Vector4D));
            Assert.AreEqual(64, sizeof(Vector4_2x));
            Assert.AreEqual(40, sizeof(Vector4Plusdouble));
            Assert.AreEqual(80, sizeof(Vector4Plusdouble_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4_2x
        {
            private Vector4D _a;
            private Vector4D _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4Plusdouble
        {
            private Vector4D _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4Plusdouble_2x
        {
            private Vector4Plusdouble _a;
            private Vector4Plusdouble _b;
        }

        [Test]
        public void SetFieldsTest()
        {
            Vector4D v3 = new Vector4D(4, 5, 6, 7)
            {
                X = 1.0,
                Y = 2.0,
                Z = 3.0,
                W = 4.0
            };
            Assert.AreEqual(1.0, v3.X);
            Assert.AreEqual(2.0, v3.Y);
            Assert.AreEqual(3.0, v3.Z);
            Assert.AreEqual(4.0, v3.W);
            Vector4D v4 = v3;
            v4.Y = 0.5;
            v4.Z = 2.2;
            v4.W = 3.5;
            Assert.AreEqual(1.0, v4.X);
            Assert.AreEqual(0.5, v4.Y);
            Assert.AreEqual(2.2, v4.Z);
            Assert.AreEqual(3.5, v4.W);
            Assert.AreEqual(2.0, v3.Y);
        }

        [Test]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0;
            evo.FieldVector.Y = 5.0;
            evo.FieldVector.Z = 5.0;
            evo.FieldVector.W = 5.0;
            Assert.AreEqual(5.0, evo.FieldVector.X);
            Assert.AreEqual(5.0, evo.FieldVector.Y);
            Assert.AreEqual(5.0, evo.FieldVector.Z);
            Assert.AreEqual(5.0, evo.FieldVector.W);
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
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4D(1, 2, 3, 4);
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
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4D(1, 2, 3, 4);
            Assert.AreEqual(1f, obj.RootEmbeddedObject.X);
            Assert.AreEqual(2f, obj.RootEmbeddedObject.Y);
            Assert.AreEqual(3f, obj.RootEmbeddedObject.Z);
            Assert.AreEqual(4f, obj.RootEmbeddedObject.W);
        }

        private class EmbeddedVectorObject
        {
            public Vector4D FieldVector;
        }

        private class DeeplyEmbeddedClass
        {
            public readonly Level0 L0 = new Level0();
            public Vector4D RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }
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
                                            public Vector4D EmbeddedVector = new Vector4D(1, 5, 1, -5);
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
                    L0 = new Level0()
                };
                obj.L0.L1 = new Level0.Level1
                {
                    L2 = new Level0.Level1.Level2()
                };
                obj.L0.L1.L2.L3 = new Level0.Level1.Level2.Level3
                {
                    L4 = new Level0.Level1.Level2.Level3.Level4()
                };
                obj.L0.L1.L2.L3.L4.L5 = new Level0.Level1.Level2.Level3.Level4.Level5
                {
                    L6 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6()
                };
                obj.L0.L1.L2.L3.L4.L5.L6.L7 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6.Level7
                {
                    EmbeddedVector = new Vector4D(1, 5, 1, -5)
                };

                return obj;
            }

            public Level0 L0;
            public Vector4D RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }
            public struct Level0
            {
#pragma warning disable IDE0051 // Remove unused private members
                private readonly double _buffer0;
                private readonly double _buffer1;
                public Level1 L1;
                private readonly double _buffer2;
                public struct Level1
                {
                    private readonly double _buffer0;
                    private readonly double _buffer1;
                    public Level2 L2;
                    private readonly byte _buffer2;
                    public struct Level2
                    {
                        public Level3 L3;
                        private readonly double _buffer0;
                        private readonly byte _buffer1;
                        public struct Level3
                        {
                            public Level4 L4;
                            public struct Level4
                            {
                                private readonly double _buffer0;
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
                                        private readonly byte _buffer1;
                                        private readonly byte _buffer2;

                                        public struct Level7
                                        {
                                            public Vector4D EmbeddedVector;
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
