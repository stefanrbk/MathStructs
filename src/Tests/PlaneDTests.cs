using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestOf(typeof(PlaneD))]
    public class PlaneDTests
    {
        // A test for Equals (PlaneD)
        [Test]
        public void PlaneEqualsTest1()
        {
            PlaneD a = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneD b = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3D(10.0f, b.Normal.Y, b.Normal.Z);
            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void PlaneEqualsTest()
        {
            PlaneD a = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneD b = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            object? obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3D(10.0f, b.Normal.Y, b.Normal.Z);

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

        // A test for operator != (PlaneD, PlaneD)
        [Test]
        public void PlaneInequalityTest()
        {
            PlaneD a = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneD b = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3D(10.0f, b.Normal.Y, b.Normal.Z);
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for operator == (PlaneD, PlaneD)
        [Test]
        public void PlaneEqualityTest()
        {
            PlaneD a = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneD b = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3D(10.0f, b.Normal.Y, b.Normal.Z);
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for GetHashCode ()
        [Test]
        public void PlaneGetHashCodeTest()
        {
            PlaneD target = new PlaneD(1.0f, 2.0f, 3.0f, 4.0f);

            int expected = target.Normal.GetHashCode() + target.D.GetHashCode();
            int actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        // A test for PlaneD (double, double, double, double)
        [Test]
        public void PlaneConstructorTest1()
        {
            double a = 1.0f, b = 2.0f, c = 3.0f, d = 4.0f;
            PlaneD target = new PlaneD(a, b, c, d);

            Assert.True(
                target.Normal.X == a && target.Normal.Y == b && target.Normal.Z == c && target.D == d,
                "PlaneD.cstor did not return the expected value.");
        }

        // A test for PlaneD.CreateFromVertices
        [Test]
        public void PlaneCreateFromVerticesTest()
        {
            Vector3D point1 = new Vector3D(0.0f, 1.0f, 1.0f);
            Vector3D point2 = new Vector3D(0.0f, 0.0f, 1.0f);
            Vector3D point3 = new Vector3D(1.0f, 0.0f, 1.0f);

            PlaneD target = PlaneD.CreateFromVertices(point1, point2, point3);
            PlaneD expected = new PlaneD(new Vector3D(0, 0, 1), -1.0f);
            Assert.AreEqual(target, expected);
        }

        // A test for PlaneD.CreateFromVertices
        [Test]
        public void PlaneCreateFromVerticesTest2()
        {
            Vector3D point1 = new Vector3D(0.0f, 0.0f, 1.0f);
            Vector3D point2 = new Vector3D(1.0f, 0.0f, 0.0f);
            Vector3D point3 = new Vector3D(1.0f, 1.0f, 0.0f);

            PlaneD target = PlaneD.CreateFromVertices(point1, point2, point3);
            double invRoot2 = (double)(1 / Math.Sqrt(2));

            PlaneD expected = new PlaneD(new Vector3D(invRoot2, 0, invRoot2), -invRoot2);
            Assert.True(MathHelper.Equal(target, expected), "PlaneD.cstor did not return the expected value.");
        }

        // A test for PlaneD (Vector3D, double)
        [Test]
        public void PlaneConstructorTest3()
        {
            Vector3D normal = new Vector3D(1, 2, 3);
            double d = 4;

            PlaneD target = new PlaneD(normal, d);
            Assert.True(
                target.Normal == normal && target.D == d,
                "PlaneD.cstor did not return the expected value.");
        }

        // A test for PlaneD (Vector4D)
        [Test]
        public void PlaneConstructorTest()
        {
            Vector4D value = new Vector4D(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneD target = new PlaneD(value);

            Assert.True(
                target.Normal.X == value.X && target.Normal.Y == value.Y && target.Normal.Z == value.Z && target.D == value.W,
                "PlaneD.cstor did not return the expected value.");
        }

        [Test]
        public void PlaneDotTest()
        {
            PlaneD target = new PlaneD(2, 3, 4, 5);
            Vector4D value = new Vector4D(5, 4, 3, 2);

            double expected = 10 + 12 + 12 + 10;
            double actual = PlaneD.Dot(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.Dot returns unexpected value.");
        }

        [Test]
        public void PlaneDotCoordinateTest()
        {
            PlaneD target = new PlaneD(2, 3, 4, 5);
            Vector3D value = new Vector3D(5, 4, 3);

            double expected = 10 + 12 + 12 + 5;
            double actual = PlaneD.DotCoordinate(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.DotCoordinate returns unexpected value.");
        }

        [Test]
        public void PlaneDotNormalTest()
        {
            PlaneD target = new PlaneD(2, 3, 4, 5);
            Vector3D value = new Vector3D(5, 4, 3);

            double expected = 10 + 12 + 12;
            double actual = PlaneD.DotNormal(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.DotCoordinate returns unexpected value.");
        }

        [Test]
        public void PlaneNormalizeTest()
        {
            PlaneD target = new PlaneD(1, 2, 3, 4);

            double f = target.Normal.LengthSquared();
            double invF = 1.0f / (double)Math.Sqrt(f);
            PlaneD expected = new PlaneD(target.Normal * invF, target.D * invF);

            PlaneD actual = PlaneD.Normalize(target);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.Normalize returns unexpected value.");

            // normalize, normalized normal.
            actual = PlaneD.Normalize(actual);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.Normalize returns unexpected value.");
        }

        [Test]
        // Transform by matrix
        public void PlaneTransformTest1()
        {
            PlaneD target = new PlaneD(1, 2, 3, 4);
            target = PlaneD.Normalize(target);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            PlaneD expected = new PlaneD();
            Matrix4x4D inv = m.Invert();
            Matrix4x4D itm = Matrix4x4D.Transpose(inv);
            double x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            expected.Normal = new Vector3D(
                x * itm.M11 + y * itm.M21 + z * itm.M31 + w * itm.M41,
                x * itm.M12 + y * itm.M22 + z * itm.M32 + w * itm.M42,
                x * itm.M13 + y * itm.M23 + z * itm.M33 + w * itm.M43);
            expected.D = x * itm.M14 + y * itm.M24 + z * itm.M34 + w * itm.M44;

            PlaneD actual;
            actual = PlaneD.Transform(target, m);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.Transform did not return the expected value.");
        }

        [Test]
        // Transform by QuaternionD
        public void PlaneTransformTest2()
        {
            PlaneD target = new PlaneD(1, 2, 3, 4);
            target = PlaneD.Normalize(target);

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            PlaneD expected = new PlaneD();
            double x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            expected.Normal = new Vector3D(
                x * m.M11 + y * m.M21 + z * m.M31 + w * m.M41,
                x * m.M12 + y * m.M22 + z * m.M32 + w * m.M42,
                x * m.M13 + y * m.M23 + z * m.M33 + w * m.M43);
            expected.D = x * m.M14 + y * m.M24 + z * m.M34 + w * m.M44;

            PlaneD actual;
            actual = PlaneD.Transform(target, q);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneD.Transform did not return the expected value.");
        }

        // A test for PlaneD comparison involving NaN values
        [Test]
        public void PlaneEqualsNanTest()
        {
            PlaneD a = new PlaneD(double.NaN, 0, 0, 0);
            PlaneD b = new PlaneD(0, double.NaN, 0, 0);
            PlaneD c = new PlaneD(0, 0, double.NaN, 0);
            PlaneD d = new PlaneD(0, 0, 0, double.NaN);

            Assert.False(a == new PlaneD(0, 0, 0, 0));
            Assert.False(b == new PlaneD(0, 0, 0, 0));
            Assert.False(c == new PlaneD(0, 0, 0, 0));
            Assert.False(d == new PlaneD(0, 0, 0, 0));

            Assert.True(a != new PlaneD(0, 0, 0, 0));
            Assert.True(b != new PlaneD(0, 0, 0, 0));
            Assert.True(c != new PlaneD(0, 0, 0, 0));
            Assert.True(d != new PlaneD(0, 0, 0, 0));

            Assert.False(a.Equals(new PlaneD(0, 0, 0, 0)));
            Assert.False(b.Equals(new PlaneD(0, 0, 0, 0)));
            Assert.False(c.Equals(new PlaneD(0, 0, 0, 0)));
            Assert.False(d.Equals(new PlaneD(0, 0, 0, 0)));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        /* Enable when size of Vector3D is correct
        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void PlaneSizeofTest()
        {
            Assert.AreEqual(16, sizeof(PlaneD));
            Assert.AreEqual(32, sizeof(Plane_2x));
            Assert.AreEqual(20, sizeof(PlanePlusdouble));
            Assert.AreEqual(40, sizeof(PlanePlusdouble_2x));
        }
        */

        [Test]
        public void PlaneToStringTest()
        {
            PlaneD target = new PlaneD(1, 2, 3, 4);
            string expected = string.Format(
                CultureInfo.CurrentCulture,
                "{{Normal:{0:G} D:{1}}}",
                target.Normal,
                target.D);

            Assert.AreEqual(expected, target.ToString());
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Plane_2x
        {
            private PlaneD _a;
            private PlaneD _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PlanePlusdouble
        {
            private PlaneD _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PlanePlusdouble_2x
        {
            private PlanePlusdouble _a;
            private PlanePlusdouble _b;
        }

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void PlaneDieldOffsetTest()
        {
            PlaneD PlaneD = new PlaneD();

            double* basePtr = &PlaneD.Normal.X; // Take address of first element
            PlaneD* planePtr = &PlaneD; // Take address of whole PlaneD

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(planePtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&PlaneD.Normal));
            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&PlaneD.D));
        }
    }
}
