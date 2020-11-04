using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Vector3DTests
    {
        [Test]
        public void Vector3MarshalSizeTest()
        {
            Assert.AreEqual(24, Marshal.SizeOf<Vector3D>());
            Assert.AreEqual(24, Marshal.SizeOf<Vector3D>(new Vector3D()));
        }

        [Test]
        public void Vector3CopyToTest()
        {
            Vector3D v1 = new Vector3D(2.0f, 3.0f, 3.3f);

            double[] a = new double[4];
            double[] b = new double[3];

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

        [Test]
        public void Vector3GetHashCodeTest()
        {
            Vector3D v1 = new Vector3D(2.0f, 3.0f, 3.3f);
            Vector3D v2 = new Vector3D(2.0f, 3.0f, 3.3f);
            Vector3D v3 = new Vector3D(2.0f, 3.0f, 3.3f);
            Vector3D v5 = new Vector3D(3.0f, 2.0f, 3.3f);
            Assert.AreEqual(v1.GetHashCode(), v1.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v2.GetHashCode());
            Assert.AreNotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.AreEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector3D v4 = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D v6 = new Vector3D(1.0f, 0.0f, 0.0f);
            Vector3D v7 = new Vector3D(0.0f, 1.0f, 0.0f);
            Vector3D v8 = new Vector3D(1.0f, 1.0f, 1.0f);
            Vector3D v9 = new Vector3D(1.0f, 1.0f, 0.0f);
            Assert.AreNotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.AreNotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.AreNotEqual(v8.GetHashCode(), v9.GetHashCode());
            Assert.AreNotEqual(v7.GetHashCode(), v9.GetHashCode());
        }

        [Test]
        public void Vector3ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector3D v1 = new Vector3D(2.0, 3.0, 3.3);
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

        // A test for Cross (Vector3D, Vector3D)
        [Test]
        public void Vector3CrossTest()
        {
            Vector3D a = new Vector3D(1.0f, 0.0f, 0.0f);
            Vector3D b = new Vector3D(0.0f, 1.0f, 0.0f);

            Vector3D expected = new Vector3D(0.0f, 0.0f, 1.0f);
            Vector3D actual;

            actual = Vector3D.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Cross did not return the expected value.");
        }

        // A test for Cross (Vector3D, Vector3D)
        // Cross test of the same vector
        [Test]
        public void Vector3CrossTest1()
        {
            Vector3D a = new Vector3D(0.0f, 1.0f, 0.0f);
            Vector3D b = new Vector3D(0.0f, 1.0f, 0.0f);

            Vector3D expected = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D actual = Vector3D.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Cross did not return the expected value.");
        }

        // A test for Distance (Vector3D, Vector3D)
        [Test]
        public void Vector3DistanceTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double expected = (double)System.Math.Sqrt(27);
            double actual;

            actual = Vector3D.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Distance did not return the expected value.");
        }

        // A test for Distance (Vector3D, Vector3D)
        // Distance from the same point
        [Test]
        public void Vector3DistanceTest1()
        {
            Vector3D a = new Vector3D(1.051f, 2.05f, 3.478f);
            Vector3D b = new Vector3D(new Vector2D(1.051f, 0.0f), 1);
            b.Y = 2.05f;
            b.Z = 3.478f;

            double actual = Vector3D.Distance(a, b);
            Assert.AreEqual(0.0f, actual);
        }

        // A test for DistanceSquared (Vector3D, Vector3D)
        [Test]
        public void Vector3DistanceSquaredTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double expected = 27.0f;
            double actual;

            actual = Vector3D.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.DistanceSquared did not return the expected value.");
        }

        // A test for Dot (Vector3D, Vector3D)
        [Test]
        public void Vector3DotTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double expected = 32.0f;
            double actual;

            actual = Vector3D.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Dot did not return the expected value.");
        }

        // A test for Dot (Vector3D, Vector3D)
        // Dot test for perpendicular vector
        [Test]
        public void Vector3DotTest1()
        {
            Vector3D a = new Vector3D(1.55f, 1.55f, 1);
            Vector3D b = new Vector3D(2.5f, 3, 1.5f);
            Vector3D c = Vector3D.Cross(a, b);

            double expected = 0.0f;
            double actual1 = Vector3D.Dot(a, c);
            double actual2 = Vector3D.Dot(b, c);
            Assert.True(MathHelper.Equal(expected, actual1), "Vector3D.Dot did not return the expected value.");
            Assert.True(MathHelper.Equal(expected, actual2), "Vector3D.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Test]
        public void Vector3LengthTest()
        {
            Vector2D a = new Vector2D(1.0f, 2.0f);

            double z = 3.0f;

            Vector3D target = new Vector3D(a, z);

            double expected = (double)System.Math.Sqrt(14.0f);
            double actual;

            actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Test]
        public void Vector3LengthTest1()
        {
            Vector3D target = new Vector3D();

            double expected = 0.0f;
            double actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Test]
        public void Vector3LengthSquaredTest()
        {
            Vector2D a = new Vector2D(1.0f, 2.0f);

            double z = 3.0f;

            Vector3D target = new Vector3D(a, z);

            double expected = 14.0f;
            double actual;

            actual = target.LengthSquared();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.LengthSquared did not return the expected value.");
        }

        // A test for Min (Vector3D, Vector3D)
        [Test]
        public void Vector3MinTest()
        {
            Vector3D a = new Vector3D(-1.0f, 4.0f, -3.0f);
            Vector3D b = new Vector3D(2.0f, 1.0f, -1.0f);

            Vector3D expected = new Vector3D(-1.0f, 1.0f, -3.0f);
            Vector3D actual;
            actual = Vector3D.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Min did not return the expected value.");
        }

        // A test for Max (Vector3D, Vector3D)
        [Test]
        public void Vector3MaxTest()
        {
            Vector3D a = new Vector3D(-1.0f, 4.0f, -3.0f);
            Vector3D b = new Vector3D(2.0f, 1.0f, -1.0f);

            Vector3D expected = new Vector3D(2.0f, 4.0f, -1.0f);
            Vector3D actual;
            actual = Vector3D.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Max did not return the expected value.");
        }

        [Test]
        public void Vector3MinMaxCodeCoverageTest()
        {
            Vector3D min = Vector3D.Zero;
            Vector3D max = Vector3D.One;
            Vector3D actual;

            // Min.
            actual = Vector3D.Min(min, max);
            Assert.AreEqual(actual, min);

            actual = Vector3D.Min(max, min);
            Assert.AreEqual(actual, min);

            // Max.
            actual = Vector3D.Max(min, max);
            Assert.AreEqual(actual, max);

            actual = Vector3D.Max(max, min);
            Assert.AreEqual(actual, max);
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        [Test]
        public void Vector3LerpTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double t = 0.5f;

            Vector3D expected = new Vector3D(2.5f, 3.5f, 4.5f);
            Vector3D actual;

            actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with factor zero
        [Test]
        public void Vector3LerpTest1()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double t = 0.0f;
            Vector3D expected = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with factor one
        [Test]
        public void Vector3LerpTest2()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double t = 1.0f;
            Vector3D expected = new Vector3D(4.0f, 5.0f, 6.0f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with factor > 1
        [Test]
        public void Vector3LerpTest3()
        {
            Vector3D a = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double t = 2.0f;
            Vector3D expected = new Vector3D(8.0f, 10.0f, 12.0f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with factor < 0
        [Test]
        public void Vector3LerpTest4()
        {
            Vector3D a = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            double t = -2.0f;
            Vector3D expected = new Vector3D(-8.0f, -10.0f, -12.0f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with special double value
        [Test]
        public void Vector3LerpTest5()
        {
            Vector3D a = new Vector3D(45.67f, 90.0f, 0f);
            Vector3D b = new Vector3D(double.PositiveInfinity, double.NegativeInfinity, 0);

            double t = 0.408f;
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(double.IsPositiveInfinity(actual.X), "Vector3D.Lerp did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test from the same point
        [Test]
        public void Vector3LerpTest6()
        {
            Vector3D a = new Vector3D(1.68f, 2.34f, 5.43f);
            Vector3D b = a;

            double t = 0.18f;
            Vector3D expected = new Vector3D(1.68f, 2.34f, 5.43f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with values known to be innacurate with the old lerp impl
        [Test]
        public void Vector3LerpTest7()
        {
            Vector3D a = new Vector3D(0.44728136f);
            Vector3D b = new Vector3D(0.46345946f);

            double t = 0.26402435f;

            Vector3D expected = new Vector3D(0.45155275f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3D, Vector3D, double)
        // Lerp test with values known to be innacurate with the old lerp impl
        // (Old code incorrectly gets 0.33333588)
        [Test]
        public void Vector3LerpTest8()
        {
            Vector3D a = new Vector3D(-100);
            Vector3D b = new Vector3D(0.33333334f);

            double t = 1f;

            Vector3D expected = new Vector3D(0.33333334f);
            Vector3D actual = Vector3D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Lerp did not return the expected value.");
        }

        // A test for Reflect (Vector3D, Vector3D)
        [Test]
        public void Vector3ReflectTest()
        {
            Vector3D a = Vector3D.Normalize(new Vector3D(1.0f, 1.0f, 1.0f));

            // Reflect on XZ plane.
            Vector3D n = new Vector3D(0.0f, 1.0f, 0.0f);
            Vector3D expected = new Vector3D(a.X, -a.Y, a.Z);
            Vector3D actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");

            // Reflect on XY plane.
            n = new Vector3D(0.0f, 0.0f, 1.0f);
            expected = new Vector3D(a.X, a.Y, -a.Z);
            actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");

            // Reflect on YZ plane.
            n = new Vector3D(1.0f, 0.0f, 0.0f);
            expected = new Vector3D(-a.X, a.Y, a.Z);
            actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3D, Vector3D)
        // Reflection when normal and source are the same
        [Test]
        public void Vector3ReflectTest1()
        {
            Vector3D n = new Vector3D(0.45f, 1.28f, 0.86f);
            n = Vector3D.Normalize(n);
            Vector3D a = n;

            Vector3D expected = -n;
            Vector3D actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3D, Vector3D)
        // Reflection when normal and source are negation
        [Test]
        public void Vector3ReflectTest2()
        {
            Vector3D n = new Vector3D(0.45f, 1.28f, 0.86f);
            n = Vector3D.Normalize(n);
            Vector3D a = -n;

            Vector3D expected = n;
            Vector3D actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3D, Vector3D)
        // Reflection when normal and source are perpendicular (a dot n = 0)
        [Test]
        public void Vector3ReflectTest3()
        {
            Vector3D n = new Vector3D(0.45f, 1.28f, 0.86f);
            Vector3D temp = new Vector3D(1.28f, 0.45f, 0.01f);
            // find a perpendicular vector of n
            Vector3D a = Vector3D.Cross(temp, n);

            Vector3D expected = a;
            Vector3D actual = Vector3D.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Reflect did not return the expected value.");
        }

        // A test for Transform(Vector3D, Matrix4x4D)
        [Test]
        public void Vector3TransformTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector3D expected = new Vector3D(12.191987f, 21.533493f, 32.616024f);
            Vector3D actual;

            actual = Vector3D.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Transform did not return the expected value.");
        }

        // A test for Clamp (Vector3D, Vector3D, Vector3D)
        [Test]
        public void Vector3ClampTest()
        {
            Vector3D a = new Vector3D(0.5f, 0.3f, 0.33f);
            Vector3D min = new Vector3D(0.0f, 0.1f, 0.13f);
            Vector3D max = new Vector3D(1.0f, 1.1f, 1.13f);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector3D expected = new Vector3D(0.5f, 0.3f, 0.33f);
            Vector3D actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector3D(2.0f, 3.0f, 4.0f);
            expected = max;
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector3D(-2.0f, -3.0f, -4.0f);
            expected = min;
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector3D(-2.0f, 0.5f, 4.0f);
            expected = new Vector3D(min.X, a.Y, max.Z);
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector3D(0.0f, 0.1f, 0.13f);
            min = new Vector3D(1.0f, 1.1f, 1.13f);

            // Case W1: specified value is in the range.
            a = new Vector3D(0.5f, 0.3f, 0.33f);
            expected = max;
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector3D(2.0f, 3.0f, 4.0f);
            expected = max;
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector3D(-2.0f, -3.0f, -4.0f);
            expected = max;
            actual = Vector3D.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Clamp did not return the expected value.");
        }

        // A test for TransformNormal (Vector3D, Matrix4x4D)
        [Test]
        public void Vector3TransformNormalTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            Vector3D expected = new Vector3D(2.19198728f, 1.53349364f, 2.61602545f);
            Vector3D actual;

            actual = Vector3D.TransformNormal(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.TransformNormal did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        [Test]
        public void Vector3TransformByQuaternionTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Vector3D expected = Vector3D.Transform(v, m);
            Vector3D actual = Vector3D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        // Transform Vector3D with zero QuaternionD
        [Test]
        public void Vector3TransformByQuaternionTest1()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            QuaternionD q = new QuaternionD();
            Vector3D expected = v;

            Vector3D actual = Vector3D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3D, QuaternionD)
        // Transform Vector3D with identity QuaternionD
        [Test]
        public void Vector3TransformByQuaternionTest2()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            QuaternionD q = QuaternionD.Identity;
            Vector3D expected = v;

            Vector3D actual = Vector3D.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector3D)
        [Test]
        public void Vector3NormalizeTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            Vector3D expected = new Vector3D(
                0.26726124191242438468455348087975f,
                0.53452248382484876936910696175951f,
                0.80178372573727315405366044263926f);
            Vector3D actual;

            actual = Vector3D.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3D)
        // Normalize vector of length one
        [Test]
        public void Vector3NormalizeTest1()
        {
            Vector3D a = new Vector3D(1.0f, 0.0f, 0.0f);

            Vector3D expected = new Vector3D(1.0f, 0.0f, 0.0f);
            Vector3D actual = Vector3D.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3D)
        // Normalize vector of length zero
        [Test]
        public void Vector3NormalizeTest2()
        {
            Vector3D a = new Vector3D(0.0f, 0.0f, 0.0f);

            Vector3D expected = new Vector3D(0.0f, 0.0f, 0.0f);
            Vector3D actual = Vector3D.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z), "Vector3D.Normalize did not return the expected value.");
        }

        // A test for operator - (Vector3D)
        [Test]
        public void Vector3UnaryNegationTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            Vector3D expected = new Vector3D(-1.0f, -2.0f, -3.0f);
            Vector3D actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator - did not return the expected value.");
        }

        [Test]
        public void Vector3UnaryNegationTest1()
        {
            Vector3D a = -new Vector3D(double.NaN, double.PositiveInfinity, double.NegativeInfinity);
            Vector3D b = -new Vector3D(0.0f, 0.0f, 0.0f);
            Assert.AreEqual(double.NaN, a.X);
            Assert.AreEqual(double.NegativeInfinity, a.Y);
            Assert.AreEqual(double.PositiveInfinity, a.Z);
            Assert.AreEqual(0.0f, b.X);
            Assert.AreEqual(0.0f, b.Y);
            Assert.AreEqual(0.0f, b.Z);
        }

        // A test for operator - (Vector3D, Vector3D)
        [Test]
        public void Vector3SubtractionTest()
        {
            Vector3D a = new Vector3D(4.0f, 2.0f, 3.0f);

            Vector3D b = new Vector3D(1.0f, 5.0f, 7.0f);

            Vector3D expected = new Vector3D(3.0f, -3.0f, -4.0f);
            Vector3D actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator - did not return the expected value.");
        }

        // A test for operator * (Vector3D, double)
        [Test]
        public void Vector3MultiplyOperatorTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            double factor = 2.0f;

            Vector3D expected = new Vector3D(2.0f, 4.0f, 6.0f);
            Vector3D actual;

            actual = a * factor;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator * did not return the expected value.");
        }

        // A test for operator * (double, Vector3D)
        [Test]
        public void Vector3MultiplyOperatorTest2()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            const double factor = 2.0f;

            Vector3D expected = new Vector3D(2.0f, 4.0f, 6.0f);
            Vector3D actual;

            actual = factor * a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator * did not return the expected value.");
        }

        // A test for operator * (Vector3D, Vector3D)
        [Test]
        public void Vector3MultiplyOperatorTest3()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            Vector3D expected = new Vector3D(4.0f, 10.0f, 18.0f);
            Vector3D actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator * did not return the expected value.");
        }

        // A test for operator / (Vector3D, double)
        [Test]
        public void Vector3DivisionTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            double div = 2.0f;

            Vector3D expected = new Vector3D(0.5f, 1.0f, 1.5f);
            Vector3D actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3D, Vector3D)
        [Test]
        public void Vector3DivisionTest1()
        {
            Vector3D a = new Vector3D(4.0f, 2.0f, 3.0f);

            Vector3D b = new Vector3D(1.0f, 5.0f, 6.0f);

            Vector3D expected = new Vector3D(4.0f, 0.4f, 0.5f);
            Vector3D actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3D, Vector3D)
        // Divide by zero
        [Test]
        public void Vector3DivisionTest2()
        {
            Vector3D a = new Vector3D(-2.0f, 3.0f, double.MaxValue);

            double div = 0.0f;

            Vector3D actual = a / div;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector3D.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector3D.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Z), "Vector3D.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3D, Vector3D)
        // Divide by zero
        [Test]
        public void Vector3DivisionTest3()
        {
            Vector3D a = new Vector3D(0.047f, -3.0f, double.NegativeInfinity);
            Vector3D b = new Vector3D();

            Vector3D actual = a / b;

            Assert.True(double.IsPositiveInfinity(actual.X), "Vector3D.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector3D.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Z), "Vector3D.operator / did not return the expected value.");
        }

        // A test for operator + (Vector3D, Vector3D)
        [Test]
        public void Vector3AdditionTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(4.0f, 5.0f, 6.0f);

            Vector3D expected = new Vector3D(5.0f, 7.0f, 9.0f);
            Vector3D actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3D.operator + did not return the expected value.");
        }

        // A test for Vector3D (double, double, double)
        [Test]
        public void Vector3ConstructorTest()
        {
            double x = 1.0f;
            double y = 2.0f;
            double z = 3.0f;

            Vector3D target = new Vector3D(x, y, z);
            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z), "Vector3D.constructor (x,y,z) did not return the expected value.");
        }

        // A test for Vector3D (Vector2D, double)
        [Test]
        public void Vector3ConstructorTest1()
        {
            Vector2D a = new Vector2D(1.0f, 2.0f);

            double z = 3.0f;

            Vector3D target = new Vector3D(a, z);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z), "Vector3D.constructor (Vector2D,z) did not return the expected value.");
        }

        // A test for Vector3D ()
        // Constructor with no parameter
        [Test]
        public void Vector3ConstructorTest3()
        {
            Vector3D a = new Vector3D();

            Assert.AreEqual(0.0f, a.X);
            Assert.AreEqual(0.0f, a.Y);
            Assert.AreEqual(0.0f, a.Z);
        }

        // A test for Vector2D (double, double)
        // Constructor with special doubleing values
        [Test]
        public void Vector3ConstructorTest4()
        {
            Vector3D target = new Vector3D(double.NaN, double.MaxValue, double.PositiveInfinity);

            Assert.True(double.IsNaN(target.X), "Vector3D.constructor (Vector3D) did not return the expected value.");
            Assert.True(double.Equals(double.MaxValue, target.Y), "Vector3D.constructor (Vector3D) did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(target.Z), "Vector3D.constructor (Vector3D) did not return the expected value.");
        }

        // A test for Add (Vector3D, Vector3D)
        [Test]
        public void Vector3AddTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(5.0f, 6.0f, 7.0f);

            Vector3D expected = new Vector3D(6.0f, 8.0f, 10.0f);
            Vector3D actual;

            actual = Vector3D.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector3D, double)
        [Test]
        public void Vector3DivideTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            double div = 2.0f;
            Vector3D expected = new Vector3D(0.5f, 1.0f, 1.5f);
            Vector3D actual;
            actual = Vector3D.Divide(a, div);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (Vector3D, Vector3D)
        [Test]
        public void Vector3DivideTest1()
        {
            Vector3D a = new Vector3D(1.0, 6.0, 7.0);
            Vector3D b = new Vector3D(5.0, 2.0, 3.0);

            Vector3D expected = new Vector3D(1.0 / 5.0, 6.0 / 2.0, 7.0 / 3.0);
            Vector3D actual;

            actual = Vector3D.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void Vector3EqualsTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(1.0f, 2.0f, 3.0f);

            // case 1: compare between same values
            object obj = b;

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

        // A test for Multiply (Vector3D, double)
        [Test]
        public void Vector3MultiplyTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            const double factor = 2.0f;
            Vector3D expected = new Vector3D(2.0f, 4.0f, 6.0f);
            Vector3D actual = Vector3D.Multiply(a, factor);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (double, Vector3D)
        [Test]
        public static void Vector3MultiplyTest2()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            const double factor = 2.0f;
            Vector3D expected = new Vector3D(2.0f, 4.0f, 6.0f);
            Vector3D actual = Vector3D.Multiply(factor, a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Vector3D, Vector3D)
        [Test]
        public void Vector3MultiplyTest3()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(5.0f, 6.0f, 7.0f);

            Vector3D expected = new Vector3D(5.0f, 12.0f, 21.0f);
            Vector3D actual;

            actual = Vector3D.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Vector3D)
        [Test]
        public void Vector3NegateTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);

            Vector3D expected = new Vector3D(-1.0f, -2.0f, -3.0f);
            Vector3D actual;

            actual = Vector3D.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator != (Vector3D, Vector3D)
        [Test]
        public void Vector3InequalityTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(1.0f, 2.0f, 3.0f);

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

        // A test for operator == (Vector3D, Vector3D)
        [Test]
        public void Vector3EqualityTest()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(1.0f, 2.0f, 3.0f);

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

        // A test for Subtract (Vector3D, Vector3D)
        [Test]
        public void Vector3SubtractTest()
        {
            Vector3D a = new Vector3D(1.0f, 6.0f, 3.0f);
            Vector3D b = new Vector3D(5.0f, 2.0f, 3.0f);

            Vector3D expected = new Vector3D(-4.0f, 4.0f, 0.0f);
            Vector3D actual;

            actual = Vector3D.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for One
        [Test]
        public void Vector3OneTest()
        {
            Vector3D val = new Vector3D(1.0f, 1.0f, 1.0f);
            Assert.AreEqual(val, Vector3D.One);
        }

        // A test for UnitX
        [Test]
        public void Vector3UnitXTest()
        {
            Vector3D val = new Vector3D(1.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector3D.UnitX);
        }

        // A test for UnitY
        [Test]
        public void Vector3UnitYTest()
        {
            Vector3D val = new Vector3D(0.0f, 1.0f, 0.0f);
            Assert.AreEqual(val, Vector3D.UnitY);
        }

        // A test for UnitZ
        [Test]
        public void Vector3UnitZTest()
        {
            Vector3D val = new Vector3D(0.0f, 0.0f, 1.0f);
            Assert.AreEqual(val, Vector3D.UnitZ);
        }

        // A test for Zero
        [Test]
        public void Vector3ZeroTest()
        {
            Vector3D val = new Vector3D(0.0f, 0.0f, 0.0f);
            Assert.AreEqual(val, Vector3D.Zero);
        }

        // A test for Equals (Vector3D)
        [Test]
        public void Vector3EqualsTest1()
        {
            Vector3D a = new Vector3D(1.0f, 2.0f, 3.0f);
            Vector3D b = new Vector3D(1.0f, 2.0f, 3.0f);

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

        // A test for Vector3D (double)
        [Test]
        public void Vector3ConstructorTest5()
        {
            double value = 1.0f;
            Vector3D target = new Vector3D(value);

            Vector3D expected = new Vector3D(value, value, value);
            Assert.AreEqual(expected, target);

            value = 2.0f;
            target = new Vector3D(value);
            expected = new Vector3D(value, value, value);
            Assert.AreEqual(expected, target);
        }

        // A test for Vector3D comparison involving NaN values
        [Test]
        public void Vector3EqualsNanTest()
        {
            Vector3D a = new Vector3D(double.NaN, 0, 0);
            Vector3D b = new Vector3D(0, double.NaN, 0);
            Vector3D c = new Vector3D(0, 0, double.NaN);

            Assert.False(a == Vector3D.Zero);
            Assert.False(b == Vector3D.Zero);
            Assert.False(c == Vector3D.Zero);

            Assert.True(a != Vector3D.Zero);
            Assert.True(b != Vector3D.Zero);
            Assert.True(c != Vector3D.Zero);

            Assert.False(a.Equals(Vector3D.Zero));
            Assert.False(b.Equals(Vector3D.Zero));
            Assert.False(c.Equals(Vector3D.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
        }

        [Test]
        public void Vector3AbsTest()
        {
            Vector3D v1 = new Vector3D(-2.5f, 2.0f, 0.5f);
            Vector3D v3 = Vector3D.Abs(new Vector3D(0.0f, double.NegativeInfinity, double.NaN));
            Vector3D v = Vector3D.Abs(v1);
            Assert.AreEqual(2.5f, v.X);
            Assert.AreEqual(2.0f, v.Y);
            Assert.AreEqual(0.5f, v.Z);
            Assert.AreEqual(0.0f, v3.X);
            Assert.AreEqual(double.PositiveInfinity, v3.Y);
            Assert.AreEqual(double.NaN, v3.Z);
        }

        [Test]
        public void Vector3SqrtTest()
        {
            Vector3D a = new Vector3D(-2.5f, 2.0f, 0.5f);
            Vector3D b = new Vector3D(5.5f, 4.5f, 16.5f);
            Assert.AreEqual(2, (int)Vector3D.SquareRoot(b).X);
            Assert.AreEqual(2, (int)Vector3D.SquareRoot(b).Y);
            Assert.AreEqual(4, (int)Vector3D.SquareRoot(b).Z);
            Assert.AreEqual(double.NaN, Vector3D.SquareRoot(a).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Vector3SizeofTest()
        {
            Assert.AreEqual(24, sizeof(Vector3D));
            Assert.AreEqual(48, sizeof(Vector3_2x));
            Assert.AreEqual(32, sizeof(Vector3Plusdouble));
            Assert.AreEqual(64, sizeof(Vector3Plusdouble_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3_2x
        {
            private Vector3D _a;
            private Vector3D _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3Plusdouble
        {
            private Vector3D _v;
            private double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3Plusdouble_2x
        {
            private Vector3Plusdouble _a;
            private Vector3Plusdouble _b;
        }

        [Test]
        public void SetFieldsTest()
        {
            Vector3D v3 = new Vector3D(4f, 5f, 6f);
            v3.X = 1.0f;
            v3.Y = 2.0f;
            v3.Z = 3.0f;
            Assert.AreEqual(1.0f, v3.X);
            Assert.AreEqual(2.0f, v3.Y);
            Assert.AreEqual(3.0f, v3.Z);
            Vector3D v4 = v3;
            v4.Y = 0.5f;
            v4.Z = 2.2f;
            Assert.AreEqual(1.0f, v4.X);
            Assert.AreEqual(0.5f, v4.Y);
            Assert.AreEqual(2.2f, v4.Z);
            Assert.AreEqual(2.0f, v3.Y);

            Vector3D before = new Vector3D(1f, 2f, 3f);
            Vector3D after = before;
            after.X = 500.0f;
            Assert.AreNotEqual(before, after);
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

        private class EmbeddedVectorObject
        {
            public Vector3D FieldVector;
        }
    }
}
