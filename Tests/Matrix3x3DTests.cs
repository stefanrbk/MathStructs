using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;

namespace Tests
{
    public class Matrix3x3DTests
    {
        #region Public Methods

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new double[] {4.0, 7.0, 2.0, 9.0, 12.0, 0.0, 3.0, 8.0, 6.0}).AsSpan();
            var vec = (Matrix3x3D)span;

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

        // A test for operator + (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DAdditionTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = a + b;
            Assert.AreEqual(expected, actual, "Matrix3x3D.operator + did not return the expected value.");
        }

        // A test for Add (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DAddTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = Matrix3x3D.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Determinant
        [Test]
        public void Matrix3x3DDeterminantTest()
        {
            Matrix3x3D target = As3x3(
                    Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
                    Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
                    Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0)));

            double val = 1.0;
            double det = target.GetDeterminant();

            Assert.AreEqual(val, det, 1e-15, "Matrix3x3D.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix3x3DDeterminantTest1()
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = 5.0,
                M12 = 2.0,
                M13 = 8.25,
                M21 = 12.0,
                M22 = 6.8,
                M23 = 2.14,
                M31 = 6.5,
                M32 = 1.0,
                M33 = 3.14
            };
            Matrix3x3D i = a.Invert();
            Assert.AreNotEqual(i, Matrix3x3D.NaN);

            double detA = a.GetDeterminant();
            double detI = i.GetDeterminant();
            double t = 1.0 / detI;

            // only accurate to 3 precision
            Assert.Less(Math.Abs(detA - t), 1e-3, "Matrix3x3D.Determinant was not set correctly.");
        }

        // A test for operator == (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DEqualityTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber();

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

        // A test for Matrix3x3D comparison involving NaN values
        [Test]
        public void Matrix3x3DEqualsNanTest()
        {
            Matrix3x3D a = new Matrix3x3D(double.NaN, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3D b = new Matrix3x3D(0, double.NaN, 0, 0, 0, 0, 0, 0, 0);
            Matrix3x3D c = new Matrix3x3D(0, 0, double.NaN, 0, 0, 0, 0, 0, 0);
            Matrix3x3D d = new Matrix3x3D(0, 0, 0, double.NaN, 0, 0, 0, 0, 0);
            Matrix3x3D e = new Matrix3x3D(0, 0, 0, 0, double.NaN, 0, 0, 0, 0);
            Matrix3x3D f = new Matrix3x3D(0, 0, 0, 0, 0, double.NaN, 0, 0, 0);
            Matrix3x3D g = new Matrix3x3D(0, 0, 0, 0, 0, 0, double.NaN, 0, 0);
            Matrix3x3D h = new Matrix3x3D(0, 0, 0, 0, 0, 0, 0, double.NaN, 0);
            Matrix3x3D i = new Matrix3x3D(0, 0, 0, 0, 0, 0, 0, 0, double.NaN);

            Assert.False(a == new Matrix3x3D());
            Assert.False(b == new Matrix3x3D());
            Assert.False(c == new Matrix3x3D());
            Assert.False(d == new Matrix3x3D());
            Assert.False(e == new Matrix3x3D());
            Assert.False(f == new Matrix3x3D());
            Assert.False(g == new Matrix3x3D());
            Assert.False(h == new Matrix3x3D());
            Assert.False(i == new Matrix3x3D());

            Assert.True(a != new Matrix3x3D());
            Assert.True(b != new Matrix3x3D());
            Assert.True(c != new Matrix3x3D());
            Assert.True(d != new Matrix3x3D());
            Assert.True(e != new Matrix3x3D());
            Assert.True(f != new Matrix3x3D());
            Assert.True(g != new Matrix3x3D());
            Assert.True(h != new Matrix3x3D());
            Assert.True(i != new Matrix3x3D());

            Assert.False(a.Equals(new Matrix3x3D()));
            Assert.False(b.Equals(new Matrix3x3D()));
            Assert.False(c.Equals(new Matrix3x3D()));
            Assert.False(d.Equals(new Matrix3x3D()));
            Assert.False(e.Equals(new Matrix3x3D()));
            Assert.False(f.Equals(new Matrix3x3D()));
            Assert.False(g.Equals(new Matrix3x3D()));
            Assert.False(h.Equals(new Matrix3x3D()));
            Assert.False(i.Equals(new Matrix3x3D()));

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
        public void Matrix3x3DEqualsTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber();

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
            obj = new Vector4D();
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (Matrix3x3D)
        [Test]
        public void Matrix3x3DEqualsTest1()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber();

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
        public unsafe void Matrix3x3DFieldOffsetTest()
        {
            Matrix3x3D mat = new Matrix3x3D();

            double* basePtr = &mat.M11; // Take address of first element
            Matrix3x3D* matPtr = &mat; // Take address of whole matrix

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
        public void Matrix3x3DGetHashCodeTest()
        {
            Matrix3x3D target = GenerateIncrementalMatrixNumber();

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
        public void Matrix3x3DIdentityTest()
        {
            Matrix3x3D val = new Matrix3x3D();
            val.M11 = val.M22 = val.M33 = 1.0f;

            Assert.AreEqual(val, Matrix3x3D.Identity, "Matrix3x3D.Indentity was not set correctly.");
        }

        // A test for operator != (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DInequalityTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber();

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

        // A test for Invert (Matrix3x3D)
        [Test]
        public void Matrix3x3DInvertIdentityTest()
        {
            Matrix3x3D mtx = Matrix3x3D.Identity;

            Matrix3x3D actual = mtx.Invert();
            Assert.AreNotEqual(actual, Matrix3x3D.NaN);

            Assert.AreEqual(actual, Matrix3x3D.Identity);
        }

        // A test for Invert (Matrix3x3D)
        // Non invertible matrix - determinant is zero - singular matrix
        [Test]
        public void Matrix3x3DInvertTest1()
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = 1.0,
                M12 = 2.0,
                M13 = 3.0,
                M21 = 4.0,
                M22 = 5.0,
                M23 = 6.0,
                M31 = 7.0,
                M32 = 8.0,
                M33 = 9.0
            };

            double detA = a.GetDeterminant();
            Assert.AreEqual(detA, 0.0, "Matrix3x3D.Invert did not return the expected value.");

            Matrix3x3D actual = a.Invert();

            // all the elements in Actual is NaN
            Assert.True(
                double.IsNaN(actual.M11) && double.IsNaN(actual.M12) && double.IsNaN(actual.M13) &&
                double.IsNaN(actual.M21) && double.IsNaN(actual.M22) && double.IsNaN(actual.M23) &&
                double.IsNaN(actual.M31) && double.IsNaN(actual.M32) && double.IsNaN(actual.M33)
                , "Matrix3x3D.Invert did not return the expected value.");
        }

        // A test for IsIdentity
        [Test]
        public void Matrix3x3DIsIdentityTest()
        {
            Assert.True(Matrix3x3D.Identity.IsIdentity);
            Assert.True(new Matrix3x3D(1, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(0, 0, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 1, 0, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 1, 0, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 1, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 0, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 0, 1, 1, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 0, 1, 0, 1, 0, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 0, 1, 0, 0, 1, 1).IsIdentity);
            Assert.False(new Matrix3x3D(1, 0, 0, 0, 1, 0, 0, 0, 0).IsIdentity);
        }

        // A test for Lerp (Matrix3x3D, Matrix3x3D, double)
        [Test]
        public void Matrix3x3DLerpTest()
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = 11.0,
                M12 = 12.0,
                M13 = 13.0,
                M21 = 14.0,
                M22 = 21.0,
                M23 = 22.0,
                M31 = 23.0,
                M32 = 24.0,
                M33 = 31.0
            };

            Matrix3x3D b = GenerateIncrementalMatrixNumber();

            double t = 0.5;

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual;
            actual = Matrix3x3D.Lerp(a, b, t);
            Assert.AreEqual(expected, actual, "Matrix3x3D.Lerp did not return the expected value.");
        }

        // A test for operator * (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DMultiplyTest1()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = a * b;
            Assert.AreEqual(expected, actual, "Matrix3x3D.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DMultiplyTest3()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix3x3D expected = new Matrix3x3D
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
            Matrix3x3D actual;
            actual = Matrix3x3D.Multiply(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for operator * (Matrix3x3D, Matrix3x3D)
        // Multiply with identity matrix
        [Test]
        public void Matrix3x3DMultiplyTest4()
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = 1.0,
                M12 = 2.0,
                M13 = 3.0,
                M21 = 4.0,
                M22 = 5.0,
                M23 = -6.0,
                M31 = 7.0,
                M32 = -8.0,
                M33 = 9.0
            };

            Matrix3x3D b = Matrix3x3D.Identity;

            Matrix3x3D expected = a;
            Matrix3x3D actual = a * b;

            Assert.AreEqual(expected, actual, "Matrix3x3D.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix3x3D, double)
        [Test]
        public void Matrix3x3DMultiplyTest5()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D expected = new Matrix3x3D(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3D actual = Matrix3x3D.Multiply(a, 3);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix3x3D, double)
        [Test]
        public void Matrix3x3DMultiplyTest6()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D expected = new Matrix3x3D(3, 6, 9, 12, 15, 18, 21, 24, 27);
            Matrix3x3D actual = a * 3;

            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Matrix3x3D)
        [Test]
        public void Matrix3x3DNegateTest()
        {
            Matrix3x3D m = GenerateIncrementalMatrixNumber();

            Matrix3x3D expected = new Matrix3x3D
            {
                M11 = -1.0,
                M12 = -2.0,
                M13 = -3.0,
                M21 = -4.0,
                M22 = -5.0,
                M23 = -6.0,
                M31 = -7.0,
                M32 = -8.0,
                M33 = -9.0
            };
            Matrix3x3D actual;

            actual = Matrix3x3D.Negate(m);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator - (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DSubtractionTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = a - b;
            Assert.AreEqual(expected, actual, "Matrix3x3D.operator - did not return the expected value.");
        }

        // A test for Subtract (Matrix3x3D, Matrix3x3D)
        [Test]
        public void Matrix3x3DSubtractTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();
            Matrix3x3D b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = Matrix3x3D.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for ToString ()
        [Test]
        public void Matrix3x3DToStringTest()
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = 11.0,
                M12 = -12.0,
                M13 = -13.3,
                M21 = 14.4,
                M22 = 21.0,
                M23 = 22.0,
                M31 = 23.0,
                M32 = 24.0,
                M33 = 31.0
            };

            string expected = string.Format(CultureInfo.CurrentCulture,
                "{{ {{M11:{0} M12:{1} M13:{2}}} {{M21:{3} M22:{4} M23:{5}}} {{M31:{6} M32:{7} M33:{8}}} }}",
                    11.0, -12.0, -13.3,
                    14.4, 21.0, 22.0,
                    23.0, 24.0, 31.0);

            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        // A test for Transpose (Matrix3x3D)
        [Test]
        public void Matrix3x3DTransposeTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();

            Matrix3x3D expected = new Matrix3x3D
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

            Matrix3x3D actual = Matrix3x3D.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3D.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix3x3D)
        // Transpose Identity matrix
        [Test]
        public void Matrix3x3DTransposeTest1()
        {
            Matrix3x3D a = Matrix3x3D.Identity;
            Matrix3x3D expected = Matrix3x3D.Identity;

            Matrix3x3D actual = Matrix3x3D.Transpose(a);
            Assert.AreEqual(expected, actual, "Matrix3x3D.Transpose did not return the expected value.");
        }

        // A test for operator - (Matrix3x3D)
        [Test]
        public void Matrix3x3DUnaryNegationTest()
        {
            Matrix3x3D a = GenerateIncrementalMatrixNumber();

            Matrix3x3D expected = new Matrix3x3D
            {
                M11 = -1.0,
                M12 = -2.0,
                M13 = -3.0,
                M21 = -4.0,
                M22 = -5.0,
                M23 = -6.0,
                M31 = -7.0,
                M32 = -8.0,
                M33 = -9.0
            };

            Matrix3x3D actual = -a;
            Assert.AreEqual(expected, actual, "Matrix3x3D.operator - did not return the expected value.");
        }

        #endregion Public Methods

        #region Private Methods

        //static Matrix4x4D As4x4(Matrix3x3D m) =>
        //    Matrix4x4D.Identity.With(m11: m.M11, m12: m.M12, m13: m.M13, m21: m.M21, m22: m.M22, m23: m.M23, m31: m.M31, m32: m.M32, m33: m.M33);
        private static Matrix3x3D As3x3(Matrix4x4D m) =>
            new Matrix3x3D(m.M11, m.M12, m.M13, m.M21, m.M22, m.M23, m.M31, m.M32, m.M33);

        private static Matrix3x3D GenerateIncrementalMatrixNumber(double value = 0.0f)
        {
            Matrix3x3D a = new Matrix3x3D
            {
                M11 = value + 1.0,
                M12 = value + 2.0,
                M13 = value + 3.0,
                M21 = value + 4.0,
                M22 = value + 5.0,
                M23 = value + 6.0,
                M31 = value + 7.0,
                M32 = value + 8.0,
                M33 = value + 9.0
            };
            return a;
        }

        #endregion Private Methods
    }
}