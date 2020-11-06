using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;

namespace Tests
{
    public class Matrix3x3FTests
    {
        #region Public Methods

        // A test for operator + (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FAdditionTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 + b.M11,
                M12 = a.M12 + b.M12,
                M13 = a.M13 + b.M13,
                M21 = a.M21 + b.M21,
                M22 = a.M22 + b.M22,
                M23 = a.M23 + b.M23,
                M31 = a.M31 + b.M31,
                M32 = a.M32 + b.M32,
                M33 = a.M33 + b.M33
            };

            Matrix3x3F actual = a + b;
            Assert.AreEqual(expected, actual, "Matrix3x3F.operator + did not return the expected value.");
        }

        // A test for Add (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FAddTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 + b.M11,
                M12 = a.M12 + b.M12,
                M13 = a.M13 + b.M13,
                M21 = a.M21 + b.M21,
                M22 = a.M22 + b.M22,
                M23 = a.M23 + b.M23,
                M31 = a.M31 + b.M31,
                M32 = a.M32 + b.M32,
                M33 = a.M33 + b.M33
            };

            Matrix3x3F actual = Matrix3x3F.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Determinant
        [Test]
        public void Matrix3x3FDeterminantTest()
        {
            Matrix3x3F target = As3x3(
                    Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f)));

            float val = 1.0f;
            float det = target.GetDeterminant();

            Assert.AreEqual(val, det, "Matrix3x3F.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix3x3FDeterminantTest1()
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = 5.0f,
                M12 = 2.0f,
                M13 = 8.25f,
                M21 = 12.0f,
                M22 = 6.8f,
                M23 = 2.14f,
                M31 = 6.5f,
                M32 = 1.0f,
                M33 = 3.14f
            };
            Matrix3x3F i = a.Invert();
            Assert.AreNotEqual(i, Matrix3x3F.NaN);

            float detA = a.GetDeterminant();
            float detI = i.GetDeterminant();
            float t = 1.0f / detI;

            // only accurate to 3 precision
            Assert.Less(Math.Abs(detA - t), 1e-3, "Matrix3x3F.Determinant was not set correctly.");
        }

        // A test for operator == (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FEqualityTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0f;
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for Matrix3x3F comparison involving NaN values
        [Test]
        public void Matrix3x3FEqualsNanTest()
        {
            Matrix3x3F a = new Matrix3x3F(float.NaN, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3F b = new Matrix3x3F(0, float.NaN, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3F c = new Matrix3x3F(0, 0, float.NaN, 0, 0, 0, 0, 0, 0);
            Matrix3x3F d = new Matrix3x3F(0, 0, 0, float.NaN, 0, 0, 0, 0, 0);
            Matrix3x3F e = new Matrix3x3F(0, 0, 0, 0, float.NaN, 0, 0, 0, 0);
            Matrix3x3F f = new Matrix3x3F(0, 0, 0, 0, 0, float.NaN, 0, 0, 0);
            Matrix3x3F g = new Matrix3x3F(0, 0, 0, 0, 0, 0, float.NaN, 0, 0);
            Matrix3x3F h = new Matrix3x3F(0, 0, 0, 0, 0, 0, 0, float.NaN, 0);
            Matrix3x3F i = new Matrix3x3F(0, 0, 0, 0, 0, 0, 0, 0, float.NaN);

            Assert.False(a == new Matrix3x3F());
            Assert.False(b == new Matrix3x3F());
            Assert.False(c == new Matrix3x3F());
            Assert.False(d == new Matrix3x3F());
            Assert.False(e == new Matrix3x3F());
            Assert.False(f == new Matrix3x3F());
            Assert.False(g == new Matrix3x3F());
            Assert.False(h == new Matrix3x3F());
            Assert.False(i == new Matrix3x3F());

            Assert.True(a != new Matrix3x3F());
            Assert.True(b != new Matrix3x3F());
            Assert.True(c != new Matrix3x3F());
            Assert.True(d != new Matrix3x3F());
            Assert.True(e != new Matrix3x3F());
            Assert.True(f != new Matrix3x3F());
            Assert.True(g != new Matrix3x3F());
            Assert.True(h != new Matrix3x3F());
            Assert.True(i != new Matrix3x3F());

            Assert.False(a.Equals(new Matrix3x3F()));
            Assert.False(b.Equals(new Matrix3x3F()));
            Assert.False(c.Equals(new Matrix3x3F()));
            Assert.False(d.Equals(new Matrix3x3F()));
            Assert.False(e.Equals(new Matrix3x3F()));
            Assert.False(f.Equals(new Matrix3x3F()));
            Assert.False(g.Equals(new Matrix3x3F()));
            Assert.False(h.Equals(new Matrix3x3F()));
            Assert.False(i.Equals(new Matrix3x3F()));

            Assert.False(a.IsIdentity);
            Assert.False(b.IsIdentity);
            Assert.False(c.IsIdentity);
            Assert.False(d.IsIdentity);
            Assert.False(e.IsIdentity);
            Assert.False(f.IsIdentity);
            Assert.False(g.IsIdentity);
            Assert.False(h.IsIdentity);
            Assert.False(i.IsIdentity);

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
            Assert.False(e.Equals(e));
            Assert.False(f.Equals(f));
            Assert.False(g.Equals(g));
            Assert.False(h.Equals(h));
            Assert.False(i.Equals(i));
        }

        // A test for Equals (object)
        [Test]
        public void Matrix3x3FEqualsTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            object? obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0f;
            obj = b;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare between different types.
            obj = new Vector4F();
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (Matrix3x3F)
        [Test]
        public void Matrix3x3FEqualsTest1()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0f;
            expected = false;
            actual = a.Equals(b);
            Assert.AreEqual(expected, actual);
        }

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void Matrix3x3FFieldOffsetTest()
        {
            Matrix3x3F mat = new Matrix3x3F();

            float* basePtr = &mat.M11; // Take address of first element
            Matrix3x3F* matPtr = &mat; // Take address of whole matrix

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(matPtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&mat.M11));
            Assert.AreEqual(new IntPtr(basePtr + 1), new IntPtr(&mat.M12));
            Assert.AreEqual(new IntPtr(basePtr + 2), new IntPtr(&mat.M13));

            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&mat.M21));
            Assert.AreEqual(new IntPtr(basePtr + 4), new IntPtr(&mat.M22));
            Assert.AreEqual(new IntPtr(basePtr + 5), new IntPtr(&mat.M23));

            Assert.AreEqual(new IntPtr(basePtr + 6), new IntPtr(&mat.M31));
            Assert.AreEqual(new IntPtr(basePtr + 7), new IntPtr(&mat.M32));
            Assert.AreEqual(new IntPtr(basePtr + 8), new IntPtr(&mat.M33));
        }

        // A test for GetHashCode ()
        [Test]
        public void Matrix3x3FGetHashCodeTest()
        {
            Matrix3x3F target = GenerateIncrementalMatrixNumber();

            HashCode hash = default;

            hash.Add(target.M11);
            hash.Add(target.M12);
            hash.Add(target.M13);

            hash.Add(target.M21);
            hash.Add(target.M22);
            hash.Add(target.M23);

            hash.Add(target.M31);
            hash.Add(target.M32);
            hash.Add(target.M33);

            int expected = hash.ToHashCode();
            int actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        // A test for Identity
        [Test]
        public void Matrix3x3FIdentityTest()
        {
            Matrix3x3F val = new Matrix3x3F();
            val.M11 = val.M22 = val.M33 = 1.0f;

            Assert.AreEqual(val, Matrix3x3F.Identity, "Matrix3x3F.Indentity was not set correctly.");
        }

        // A test for operator != (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FInequalityTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0f;
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for Invert (Matrix3x3F)
        [Test]
        public void Matrix3x3FInvertIdentityTest()
        {
            Matrix3x3F mtx = Matrix3x3F.Identity;

            Matrix3x3F actual = mtx.Invert();
            Assert.AreNotEqual(actual, Matrix3x3F.NaN);

            Assert.AreEqual(actual, Matrix3x3F.Identity);
        }

        // A test for Invert (Matrix3x3F)
        // Non invertible matrix - determinant is zero - singular matrix
        [Test]
        public void Matrix3x3FInvertTest1()
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = 1.0f,
                M12 = 2.0f,
                M13 = 3.0f,
                M21 = 4.0f,
                M22 = 5.0f,
                M23 = 6.0f,
                M31 = 7.0f,
                M32 = 8.0f,
                M33 = 9.0f
            };

            float detA = a.GetDeterminant();
            Assert.AreEqual(detA, 0.0f, "Matrix3x3F.Invert did not return the expected value.");

            Matrix3x3F actual = a.Invert();

            // all the elements in Actual is NaN
            Assert.True(
                float.IsNaN(actual.M11) && float.IsNaN(actual.M12) && float.IsNaN(actual.M13) &&
                float.IsNaN(actual.M21) && float.IsNaN(actual.M22) && float.IsNaN(actual.M23) &&
                float.IsNaN(actual.M31) && float.IsNaN(actual.M32) && float.IsNaN(actual.M33)
                , "Matrix3x3F.Invert did not return the expected value.");
        }

        // A test for IsIdentity
        [Test]
        public void Matrix3x3FIsIdentityTest()
        {
            Assert.True(Matrix3x3F.Identity.IsIdentity);
            Assert.True(new Matrix3x3F(1, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(0, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 1, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 1, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 1, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 0, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 0, 1, 1, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 0, 1, 0, 1, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 0, 1, 0, 0, 1, 1).IsIdentity);
            Assert.False(new Matrix3x3F(1, 0, 0, 0, 1, 0, 0, 0, 0).IsIdentity);
        }

        // A test for Lerp (Matrix3x3F, Matrix3x3F, float)
        [Test]
        public void Matrix3x3FLerpTest()
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = 11.0f,
                M12 = 12.0f,
                M13 = 13.0f,
                M21 = 14.0f,
                M22 = 21.0f,
                M23 = 22.0f,
                M31 = 23.0f,
                M32 = 24.0f,
                M33 = 31.0f
            };

            Matrix3x3F b = GenerateIncrementalMatrixNumber();

            float t = 0.5f;

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 + (b.M11 - a.M11) * t,
                M12 = a.M12 + (b.M12 - a.M12) * t,
                M13 = a.M13 + (b.M13 - a.M13) * t,

                M21 = a.M21 + (b.M21 - a.M21) * t,
                M22 = a.M22 + (b.M22 - a.M22) * t,
                M23 = a.M23 + (b.M23 - a.M23) * t,

                M31 = a.M31 + (b.M31 - a.M31) * t,
                M32 = a.M32 + (b.M32 - a.M32) * t,
                M33 = a.M33 + (b.M33 - a.M33) * t
            };

            Matrix3x3F actual;
            actual = Matrix3x3F.Lerp(a, b, t);
            Assert.AreEqual(expected, actual, "Matrix3x3F.Lerp did not return the expected value.");
        }

        // A test for operator * (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FMultiplyTest1()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,

                M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,

                M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33
            };

            Matrix3x3F actual = a * b;
            Assert.AreEqual(expected, actual, "Matrix3x3F.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FMultiplyTest3()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,

                M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,

                M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33
            };
            Matrix3x3F actual;
            actual = Matrix3x3F.Multiply(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for operator * (Matrix3x3F, Matrix3x3F)
        // Multiply with identity matrix
        [Test]
        public void Matrix3x3FMultiplyTest4()
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = 1.0f,
                M12 = 2.0f,
                M13 = 3.0f,
                M21 = 4.0f,
                M22 = 5.0f,
                M23 = -6.0f,
                M31 = 7.0f,
                M32 = -8.0f,
                M33 = 9.0f
            };

            Matrix3x3F b = Matrix3x3F.Identity;

            Matrix3x3F expected = a;
            Matrix3x3F actual = a * b;

            Assert.AreEqual(expected, actual, "Matrix3x3F.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3F, float)
        [Test]
        public void Matrix3x3FMultiplyTest5()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F expected = new Matrix3x3F(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3F actual = Matrix3x3F.Multiply(a, 3);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix3x3F, float)
        [Test]
        public void Matrix3x3FMultiplyTest6()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F expected = new Matrix3x3F(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3F actual = a * 3;

            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Matrix3x3F)
        [Test]
        public void Matrix3x3FNegateTest()
        {
            Matrix3x3F m = GenerateIncrementalMatrixNumber();

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = -1.0f,
                M12 = -2.0f,
                M13 = -3.0f,
                M21 = -4.0f,
                M22 = -5.0f,
                M23 = -6.0f,
                M31 = -7.0f,
                M32 = -8.0f,
                M33 = -9.0f
            };
            Matrix3x3F actual;

            actual = Matrix3x3F.Negate(m);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator - (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FSubtractionTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 - b.M11,
                M12 = a.M12 - b.M12,
                M13 = a.M13 - b.M13,
                M21 = a.M21 - b.M21,
                M22 = a.M22 - b.M22,
                M23 = a.M23 - b.M23,
                M31 = a.M31 - b.M31,
                M32 = a.M32 - b.M32,
                M33 = a.M33 - b.M33
            };

            Matrix3x3F actual = a - b;
            Assert.AreEqual(expected, actual, "Matrix3x3F.operator - did not return the expected value.");
        }

        // A test for Subtract (Matrix3x3F, Matrix3x3F)
        [Test]
        public void Matrix3x3FSubtractTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();
            Matrix3x3F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11 - b.M11,
                M12 = a.M12 - b.M12,
                M13 = a.M13 - b.M13,
                M21 = a.M21 - b.M21,
                M22 = a.M22 - b.M22,
                M23 = a.M23 - b.M23,
                M31 = a.M31 - b.M31,
                M32 = a.M32 - b.M32,
                M33 = a.M33 - b.M33
            };

            Matrix3x3F actual = Matrix3x3F.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for ToString ()
        [Test]
        public void Matrix3x3FToStringTest()
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = 11.0f,
                M12 = -12.0f,
                M13 = -13.3f,
                M21 = 14.4f,
                M22 = 21.0f,
                M23 = 22.0f,
                M31 = 23.0f,
                M32 = 24.0f,
                M33 = 31.0f
            };

            string expected = string.Format(CultureInfo.CurrentCulture,
                "{{ {{M11:{0} M12:{1} M13:{2}}} {{M21:{3} M22:{4} M23:{5}}} {{M31:{6} M32:{7} M33:{8}}} }}",
                    11.0f, -12.0f, -13.3f,
                    14.4f, 21.0f, 22.0f,
                    23.0f, 24.0f, 31.0f);

            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        // A test for Transpose (Matrix3x3F)
        [Test]
        public void Matrix3x3FTransposeTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = a.M11,
                M12 = a.M21,
                M13 = a.M31,
                M21 = a.M12,
                M22 = a.M22,
                M23 = a.M32,
                M31 = a.M13,
                M32 = a.M23,
                M33 = a.M33
            };

            Matrix3x3F actual = Matrix3x3F.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3F.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix3x3F)
        // Transpose Identity matrix
        [Test]
        public void Matrix3x3FTransposeTest1()
        {
            Matrix3x3F a = Matrix3x3F.Identity;
            Matrix3x3F expected = Matrix3x3F.Identity;

            Matrix3x3F actual = Matrix3x3F.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3F.Transpose did not return the expected value.");
        }

        // A test for operator - (Matrix3x3F)
        [Test]
        public void Matrix3x3FUnaryNegationTest()
        {
            Matrix3x3F a = GenerateIncrementalMatrixNumber();

            Matrix3x3F expected = new Matrix3x3F
            {
                M11 = -1.0f,
                M12 = -2.0f,
                M13 = -3.0f,
                M21 = -4.0f,
                M22 = -5.0f,
                M23 = -6.0f,
                M31 = -7.0f,
                M32 = -8.0f,
                M33 = -9.0f
            };

            Matrix3x3F actual = -a;
            Assert.AreEqual(expected, actual, "Matrix3x3F.operator - did not return the expected value.");
        }

        #endregion Public Methods

        #region Private Methods

        //static Matrix4x4F As4x4(Matrix3x3F m) =>
        //    Matrix4x4F.Identity.With(m11: m.M11, m12: m.M12, m13: m.M13, m21: m.M21, m22: m.M22, m23: m.M23, m31: m.M31, m32: m.M32, m33: m.M33);
        private static Matrix3x3F As3x3(Matrix4x4F m) =>
            new Matrix3x3F(m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33);

        private static Matrix3x3F GenerateIncrementalMatrixNumber(float value = 0.0f)
        {
            Matrix3x3F a = new Matrix3x3F
            {
                M11 = value + 1.0f,
                M12 = value + 2.0f,
                M13 = value + 3.0f,
                M21 = value + 4.0f,
                M22 = value + 5.0f,
                M23 = value + 6.0f,
                M31 = value + 7.0f,
                M32 = value + 8.0f,
                M33 = value + 9.0f
            };
            return a;
        }

        #endregion Private Methods
    }
}