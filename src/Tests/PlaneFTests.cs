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
    [TestOf(typeof(PlaneF))]
    public class PlaneTests
    {
        // A test for Equals (PlaneF)
        [Test]
        public void PlaneEqualsTest1()
        {
            PlaneF a = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneF b = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3F(10.0f, b.Normal.Y, b.Normal.Z);
            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void PlaneEqualsTest()
        {
            PlaneF a = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneF b = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            object? obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3F(10.0f, b.Normal.Y, b.Normal.Z);

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

        // A test for operator != (PlaneF, PlaneF)
        [Test]
        public void PlaneInequalityTest()
        {
            PlaneF a = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneF b = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3F(10.0f, b.Normal.Y, b.Normal.Z);
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for operator == (PlaneF, PlaneF)
        [Test]
        public void PlaneEqualityTest()
        {
            PlaneF a = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneF b = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.Normal = new Vector3F(10.0f, b.Normal.Y, b.Normal.Z);
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for GetHashCode ()
        [Test]
        public void PlaneGetHashCodeTest()
        {
            PlaneF target = new PlaneF(1.0f, 2.0f, 3.0f, 4.0f);

            int expected = target.Normal.GetHashCode() + target.D.GetHashCode();
            int actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        // A test for PlaneF (float, float, float, float)
        [Test]
        public void PlaneConstructorTest1()
        {
            float a = 1.0f, b = 2.0f, c = 3.0f, d = 4.0f;
            PlaneF target = new PlaneF(a, b, c, d);

            Assert.True(
                target.Normal.X == a && target.Normal.Y == b && target.Normal.Z == c && target.D == d,
                "PlaneF.cstor did not return the expected value.");
        }

        // A test for PlaneF.CreateFromVertices
        [Test]
        public void PlaneCreateFromVerticesTest()
        {
            Vector3F point1 = new Vector3F(0.0f, 1.0f, 1.0f);
            Vector3F point2 = new Vector3F(0.0f, 0.0f, 1.0f);
            Vector3F point3 = new Vector3F(1.0f, 0.0f, 1.0f);

            PlaneF target = PlaneF.CreateFromVertices(point1, point2, point3);
            PlaneF expected = new PlaneF(new Vector3F(0, 0, 1), -1.0f);
            Assert.AreEqual(target, expected);
        }

        // A test for PlaneF.CreateFromVertices
        [Test]
        public void PlaneCreateFromVerticesTest2()
        {
            Vector3F point1 = new Vector3F(0.0f, 0.0f, 1.0f);
            Vector3F point2 = new Vector3F(1.0f, 0.0f, 0.0f);
            Vector3F point3 = new Vector3F(1.0f, 1.0f, 0.0f);

            PlaneF target = PlaneF.CreateFromVertices(point1, point2, point3);
            float invRoot2 = (float)(1 / Math.Sqrt(2));

            PlaneF expected = new PlaneF(new Vector3F(invRoot2, 0, invRoot2), -invRoot2);
            Assert.True(MathHelper.Equal(target, expected), "PlaneF.cstor did not return the expected value.");
        }

        // A test for PlaneF (Vector3f, float)
        [Test]
        public void PlaneConstructorTest3()
        {
            Vector3F normal = new Vector3F(1, 2, 3);
            float d = 4;

            PlaneF target = new PlaneF(normal, d);
            Assert.True(
                target.Normal == normal && target.D == d,
                "PlaneF.cstor did not return the expected value.");
        }

        // A test for PlaneF (Vector4f)
        [Test]
        public void PlaneConstructorTest()
        {
            Vector4F value = new Vector4F(1.0f, 2.0f, 3.0f, 4.0f);
            PlaneF target = new PlaneF(value);

            Assert.True(
                target.Normal.X == value.X && target.Normal.Y == value.Y && target.Normal.Z == value.Z && target.D == value.W,
                "PlaneF.cstor did not return the expected value.");
        }

        [Test]
        public void PlaneDotTest()
        {
            PlaneF target = new PlaneF(2, 3, 4, 5);
            Vector4F value = new Vector4F(5, 4, 3, 2);

            float expected = 10 + 12 + 12 + 10;
            float actual = PlaneF.Dot(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.Dot returns unexpected value.");
        }

        [Test]
        public void PlaneDotCoordinateTest()
        {
            PlaneF target = new PlaneF(2, 3, 4, 5);
            Vector3F value = new Vector3F(5, 4, 3);

            float expected = 10 + 12 + 12 + 5;
            float actual = PlaneF.DotCoordinate(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.DotCoordinate returns unexpected value.");
        }

        [Test]
        public void PlaneDotNormalTest()
        {
            PlaneF target = new PlaneF(2, 3, 4, 5);
            Vector3F value = new Vector3F(5, 4, 3);

            float expected = 10 + 12 + 12;
            float actual = PlaneF.DotNormal(target, value);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.DotCoordinate returns unexpected value.");
        }

        [Test]
        public void PlaneNormalizeTest()
        {
            PlaneF target = new PlaneF(1, 2, 3, 4);

            float f = target.Normal.LengthSquared();
            float invF = 1.0f / (float)Math.Sqrt(f);
            PlaneF expected = new PlaneF(target.Normal * invF, target.D * invF);

            PlaneF actual = PlaneF.Normalize(target);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.Normalize returns unexpected value.");

            // normalize, normalized normal.
            actual = PlaneF.Normalize(actual);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.Normalize returns unexpected value.");
        }

        [Test]
        // Transform by matrix
        public void PlaneTransformTest1()
        {
            PlaneF target = new PlaneF(1, 2, 3, 4);
            target = PlaneF.Normalize(target);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            PlaneF expected = new PlaneF();
            Matrix4x4F inv = m.Invert();
            Matrix4x4F itm = Matrix4x4F.Transpose(inv);
            float x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            expected.Normal = new Vector3F(
                x * itm.M11 + y * itm.M21 + z * itm.M31 + w * itm.M41,
                x * itm.M12 + y * itm.M22 + z * itm.M32 + w * itm.M42,
                x * itm.M13 + y * itm.M23 + z * itm.M33 + w * itm.M43);
            expected.D = x * itm.M14 + y * itm.M24 + z * itm.M34 + w * itm.M44;

            PlaneF actual;
            actual = PlaneF.Transform(target, m);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.Transform did not return the expected value.");
        }

        [Test]
        // Transform by QuaternionF
        public void PlaneTransformTest2()
        {
            PlaneF target = new PlaneF(1, 2, 3, 4);
            target = PlaneF.Normalize(target);

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            PlaneF expected = new PlaneF();
            float x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            expected.Normal = new Vector3F(
                x * m.M11 + y * m.M21 + z * m.M31 + w * m.M41,
                x * m.M12 + y * m.M22 + z * m.M32 + w * m.M42,
                x * m.M13 + y * m.M23 + z * m.M33 + w * m.M43);
            expected.D = x * m.M14 + y * m.M24 + z * m.M34 + w * m.M44;

            PlaneF actual;
            actual = PlaneF.Transform(target, q);
            Assert.True(MathHelper.Equal(expected, actual), "PlaneF.Transform did not return the expected value.");
        }

        // A test for PlaneF comparison involving NaN values
        [Test]
        public void PlaneEqualsNanTest()
        {
            PlaneF a = new PlaneF(float.NaN, 0, 0, 0);
            PlaneF b = new PlaneF(0, float.NaN, 0, 0);
            PlaneF c = new PlaneF(0, 0, float.NaN, 0);
            PlaneF d = new PlaneF(0, 0, 0, float.NaN);

            Assert.False(a == new PlaneF(0, 0, 0, 0));
            Assert.False(b == new PlaneF(0, 0, 0, 0));
            Assert.False(c == new PlaneF(0, 0, 0, 0));
            Assert.False(d == new PlaneF(0, 0, 0, 0));

            Assert.True(a != new PlaneF(0, 0, 0, 0));
            Assert.True(b != new PlaneF(0, 0, 0, 0));
            Assert.True(c != new PlaneF(0, 0, 0, 0));
            Assert.True(d != new PlaneF(0, 0, 0, 0));

            Assert.False(a.Equals(new PlaneF(0, 0, 0, 0)));
            Assert.False(b.Equals(new PlaneF(0, 0, 0, 0)));
            Assert.False(c.Equals(new PlaneF(0, 0, 0, 0)));
            Assert.False(d.Equals(new PlaneF(0, 0, 0, 0)));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        /* Enable when size of Vector3F is correct
        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void PlaneSizeofTest()
        {
            Assert.AreEqual(16, sizeof(PlaneF));
            Assert.AreEqual(32, sizeof(Plane_2x));
            Assert.AreEqual(20, sizeof(PlanePlusFloat));
            Assert.AreEqual(40, sizeof(PlanePlusFloat_2x));
        }
        */

        [Test]
        public void PlaneToStringTest()
        {
            PlaneF target = new PlaneF(1, 2, 3, 4);
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
            private PlaneF _a;
            private PlaneF _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PlanePlusFloat
        {
            private PlaneF _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PlanePlusFloat_2x
        {
            private PlanePlusFloat _a;
            private PlanePlusFloat _b;
        }

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void PlaneFieldOffsetTest()
        {
            PlaneF PlaneF = new PlaneF();

            float* basePtr = &PlaneF.Normal.X; // Take address of first element
            PlaneF* planePtr = &PlaneF; // Take address of whole PlaneF

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(planePtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&PlaneF.Normal));
            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&PlaneF.D));
        }
    }
}
