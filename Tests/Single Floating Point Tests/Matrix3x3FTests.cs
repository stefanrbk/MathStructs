using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Numerics;

namespace Tests
{
    public class Matrix3x3Tests
    {
        #region Public Methods

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new float[] {4.0f, 7.0f, 2.0f, 9.0f, 12.0f, 0.0f, 3.0f, 8.0f, 6.0f}).AsSpan();
            var vec = span.ToMatrix3x3();

            Assert.That(vec.M11, Is.EqualTo(span[0]));
            Assert.That(vec.M12, Is.EqualTo(span[1]));
            Assert.That(vec.M13, Is.EqualTo(span[2]));
            Assert.That(vec.M21, Is.EqualTo(span[3]));
            Assert.That(vec.M22, Is.EqualTo(span[4]));
            Assert.That(vec.M23, Is.EqualTo(span[5]));
            Assert.That(vec.M31, Is.EqualTo(span[6]));
            Assert.That(vec.M32, Is.EqualTo(span[7]));
            Assert.That(vec.M33, Is.EqualTo(span[8]));
        }

        // A test for operator + (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3AdditionTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = a + b;
            Assert.AreEqual(expected, actual, "Matrix3x3.operator + did not return the expected value.");
        }

        // A test for Add (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3AddTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = Matrix3x3.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Determinant
        [Test]
        public void Matrix3x3DeterminantTest()
        {
            Matrix3x3 target = As3x3(
                    Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0f)));

            float val = 1.0f;
            float det = target.GetDeterminant();

            Assert.AreEqual(val, det, "Matrix3x3.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix3x3DeterminantTest1()
        {
            Matrix3x3 a = new Matrix3x3
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
            Matrix3x3 i = a.Invert();
            Assert.AreNotEqual(i, Matrix3x3.NaN);

            float detA = a.GetDeterminant();
            float detI = i.GetDeterminant();
            float t = 1.0f / detI;

            // only accurate to 3 precision
            Assert.Less(Math.Abs(detA - t), 1e-3, "Matrix3x3.Determinant was not set correctly.");
        }

        // A test for operator == (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3EqualityTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber();

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

        // A test for Matrix3x3 comparison involving NaN values
        [Test]
        public void Matrix3x3EqualsNanTest()
        {
            Matrix3x3 a = new Matrix3x3(float.NaN, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3 b = new Matrix3x3(0, float.NaN, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3 c = new Matrix3x3(0, 0, float.NaN, 0, 0, 0, 0, 0, 0);
            Matrix3x3 d = new Matrix3x3(0, 0, 0, float.NaN, 0, 0, 0, 0, 0);
            Matrix3x3 e = new Matrix3x3(0, 0, 0, 0, float.NaN, 0, 0, 0, 0);
            Matrix3x3 f = new Matrix3x3(0, 0, 0, 0, 0, float.NaN, 0, 0, 0);
            Matrix3x3 g = new Matrix3x3(0, 0, 0, 0, 0, 0, float.NaN, 0, 0);
            Matrix3x3 h = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, float.NaN, 0);
            Matrix3x3 i = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, float.NaN);

            Assert.False(a == new Matrix3x3());
            Assert.False(b == new Matrix3x3());
            Assert.False(c == new Matrix3x3());
            Assert.False(d == new Matrix3x3());
            Assert.False(e == new Matrix3x3());
            Assert.False(f == new Matrix3x3());
            Assert.False(g == new Matrix3x3());
            Assert.False(h == new Matrix3x3());
            Assert.False(i == new Matrix3x3());

            Assert.True(a != new Matrix3x3());
            Assert.True(b != new Matrix3x3());
            Assert.True(c != new Matrix3x3());
            Assert.True(d != new Matrix3x3());
            Assert.True(e != new Matrix3x3());
            Assert.True(f != new Matrix3x3());
            Assert.True(g != new Matrix3x3());
            Assert.True(h != new Matrix3x3());
            Assert.True(i != new Matrix3x3());

            Assert.False(a.Equals(new Matrix3x3()));
            Assert.False(b.Equals(new Matrix3x3()));
            Assert.False(c.Equals(new Matrix3x3()));
            Assert.False(d.Equals(new Matrix3x3()));
            Assert.False(e.Equals(new Matrix3x3()));
            Assert.False(f.Equals(new Matrix3x3()));
            Assert.False(g.Equals(new Matrix3x3()));
            Assert.False(h.Equals(new Matrix3x3()));
            Assert.False(i.Equals(new Matrix3x3()));

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
        public void Matrix3x3EqualsTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber();

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
            obj = new Vector4();
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (Matrix3x3)
        [Test]
        public void Matrix3x3EqualsTest1()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber();

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
        public unsafe void Matrix3x3FieldOffsetTest()
        {
            Matrix3x3 mat = new Matrix3x3();

            float* basePtr = &mat.M11; // Take address of first element
            Matrix3x3* matPtr = &mat; // Take address of whole matrix

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
        public void Matrix3x3GetHashCodeTest()
        {
            Matrix3x3 target = GenerateIncrementalMatrixNumber();

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
        public void Matrix3x3IdentityTest()
        {
            Matrix3x3 val = new Matrix3x3();
            val.M11 = val.M22 = val.M33 = 1.0f;

            Assert.AreEqual(val, Matrix3x3.Identity, "Matrix3x3.Indentity was not set correctly.");
        }

        // A test for operator != (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3InequalityTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber();

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

        // A test for Invert (Matrix3x3)
        [Test]
        public void Matrix3x3InvertIdentityTest()
        {
            Matrix3x3 mtx = Matrix3x3.Identity;

            Matrix3x3 actual = mtx.Invert();
            Assert.AreNotEqual(actual, Matrix3x3.NaN);

            Assert.AreEqual(actual, Matrix3x3.Identity);
        }

        // A test for Invert (Matrix3x3)
        // Non invertible matrix - determinant is zero - singular matrix
        [Test]
        public void Matrix3x3InvertTest1()
        {
            Matrix3x3 a = new Matrix3x3
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
            Assert.AreEqual(detA, 0.0f, "Matrix3x3.Invert did not return the expected value.");

            Matrix3x3 actual = a.Invert();

            // all the elements in Actual is NaN
            Assert.True(
                float.IsNaN(actual.M11) && float.IsNaN(actual.M12) && float.IsNaN(actual.M13) &&
                float.IsNaN(actual.M21) && float.IsNaN(actual.M22) && float.IsNaN(actual.M23) &&
                float.IsNaN(actual.M31) && float.IsNaN(actual.M32) && float.IsNaN(actual.M33)
                , "Matrix3x3.Invert did not return the expected value.");
        }

        // A test for IsIdentity
        [Test]
        public void Matrix3x3IsIdentityTest()
        {
            Assert.True(Matrix3x3.Identity.IsIdentity);
            Assert.True(new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(0, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 1, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 1, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 1, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 0, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 0, 1, 1, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 0, 1, 0, 1, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 1, 1).IsIdentity);
            Assert.False(new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 0).IsIdentity);
        }

        // A test for Lerp (Matrix3x3, Matrix3x3, float)
        [Test]
        public void Matrix3x3LerpTest()
        {
            Matrix3x3 a = new Matrix3x3
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

            Matrix3x3 b = GenerateIncrementalMatrixNumber();

            float t = 0.5f;

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual;
            actual = Matrix3x3.Lerp(a, b, t);
            Assert.AreEqual(expected, actual, "Matrix3x3.Lerp did not return the expected value.");
        }

        // A test for operator * (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3MultiplyTest1()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = a * b;
            Assert.AreEqual(expected, actual, "Matrix3x3.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3MultiplyTest3()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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
            Matrix3x3 actual;
            actual = Matrix3x3.Multiply(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for operator * (Matrix3x3, Matrix3x3)
        // Multiply with identity matrix
        [Test]
        public void Matrix3x3MultiplyTest4()
        {
            Matrix3x3 a = new Matrix3x3
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

            Matrix3x3 b = Matrix3x3.Identity;

            Matrix3x3 expected = a;
            Matrix3x3 actual = a * b;

            Assert.AreEqual(expected, actual, "Matrix3x3.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3, float)
        [Test]
        public void Matrix3x3MultiplyTest5()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 expected = new Matrix3x3(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3 actual = Matrix3x3.Multiply(a, 3);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix3x3, float)
        [Test]
        public void Matrix3x3MultiplyTest6()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 expected = new Matrix3x3(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3 actual = a * 3;

            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Matrix3x3)
        [Test]
        public void Matrix3x3NegateTest()
        {
            Matrix3x3 m = GenerateIncrementalMatrixNumber();

            Matrix3x3 expected = new Matrix3x3
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
            Matrix3x3 actual;

            actual = Matrix3x3.Negate(m);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator - (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3SubtractionTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = a - b;
            Assert.AreEqual(expected, actual, "Matrix3x3.operator - did not return the expected value.");
        }

        // A test for Subtract (Matrix3x3, Matrix3x3)
        [Test]
        public void Matrix3x3SubtractTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();
            Matrix3x3 b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = Matrix3x3.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for ToString ()
        [Test]
        public void Matrix3x3ToStringTest()
        {
            Matrix3x3 a = new Matrix3x3
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

        // A test for Transpose (Matrix3x3)
        [Test]
        public void Matrix3x3TransposeTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = Matrix3x3.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix3x3)
        // Transpose Identity matrix
        [Test]
        public void Matrix3x3TransposeTest1()
        {
            Matrix3x3 a = Matrix3x3.Identity;
            Matrix3x3 expected = Matrix3x3.Identity;

            Matrix3x3 actual = Matrix3x3.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3.Transpose did not return the expected value.");
        }

        // A test for operator - (Matrix3x3)
        [Test]
        public void Matrix3x3UnaryNegationTest()
        {
            Matrix3x3 a = GenerateIncrementalMatrixNumber();

            Matrix3x3 expected = new Matrix3x3
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

            Matrix3x3 actual = -a;
            Assert.AreEqual(expected, actual, "Matrix3x3.operator - did not return the expected value.");
        }

        #endregion Public Methods

        #region Private Methods

        //static Matrix4x4F As4x4(Matrix3x3 m) =>
        //    Matrix4x4F.Identity.With(m11: m.M11, m12: m.M12, m13: m.M13, m21: m.M21, m22: m.M22, m23: m.M23, m31: m.M31, m32: m.M32, m33: m.M33);
        private static Matrix3x3 As3x3(Matrix4x4 m) =>
            new Matrix3x3(m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33);

        private static Matrix3x3 GenerateIncrementalMatrixNumber(float value = 0.0f)
        {
            Matrix3x3 a = new Matrix3x3
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