using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Matrix4x4DTests
    {
        static Matrix4x4D GenerateIncrementalMatrixNumber(double value = 0.0f)
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = value + 1.0,
                M12 = value + 2.0,
                M13 = value + 3.0,
                M14 = value + 4.0,
                M21 = value + 5.0,
                M22 = value + 6.0,
                M23 = value + 7.0,
                M24 = value + 8.0,
                M31 = value + 9.0,
                M32 = value + 10.0,
                M33 = value + 11.0,
                M34 = value + 12.0,
                M41 = value + 13.0,
                M42 = value + 14.0,
                M43 = value + 15.0,
                M44 = value + 16.0
            };
            return a;
        }

        static Matrix4x4D GenerateTestMatrix()
        {
            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.Translation = new Vector3D(111.0f, 222.0f, 333.0f);
            return m;
        }

        // A test for Identity
        [Test]
        public void Matrix4x4IdentityTest()
        {
            Matrix4x4D val = new Matrix4x4D();
            val.M11 = val.M22 = val.M33 = val.M44 = 1.0f;

            Assert.True(MathHelper.Equal(val, Matrix4x4D.Identity), "Matrix4x4D.Indentity was not set correctly.");
        }

        // A test for Determinant
        [Test]
        public void Matrix4x4DeterminantTest()
        {
            Matrix4x4D target =
                    Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));

            double val = 1.0f;
            double det = target.GetDeterminant();

            Assert.True(MathHelper.Equal(val, det), "Matrix4x4D.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix4x4DeterminantTest1()
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = 5.0,
                M12 = 2.0,
                M13 = 8.25,
                M14 = 1.0,
                M21 = 12.0,
                M22 = 6.8,
                M23 = 2.14,
                M24 = 9.6,
                M31 = 6.5,
                M32 = 1.0,
                M33 = 3.14,
                M34 = 2.22,
                M41 = 0,
                M42 = 0.86,
                M43 = 4.0,
                M44 = 1.0
            };
            Matrix4x4D i = a.Invert();
            Assert.True(i != Matrix4x4D.NaN);

            double detA = a.GetDeterminant();
            double detI = i.GetDeterminant();
            double t = 1.0f / detI;

            // only accurate to 3 precision
            Assert.True(System.Math.Abs(detA - t) < 1e-3, "Matrix4x4D.Determinant was not set correctly.");
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertTest()
        {
            Matrix4x4D mtx =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(30.0)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(30.0)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(30.0));

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.74999994,
                M12 = -0.216506317,
                M13 = 0.62499994,
                M14 = 0.0,

                M21 = 0.433012635,
                M22 = 0.87499994,
                M23 = -0.216506317,
                M24 = 0.0,

                M31 = -0.49999997,
                M32 = 0.433012635,
                M33 = 0.74999994,
                M34 = 0.0,

                M41 = 0.0,
                M42 = 0.0,
                M43 = 0.0,
                M44 = 0.99999994
            };

            Matrix4x4D actual = mtx.Invert();

            Assert.True(actual != Matrix4x4D.NaN);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.Invert did not return the expected value.");

            // Make sure M*M is identity matrix
            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity), "Matrix4x4D.Invert did not return the expected value.");
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertIdentityTest()
        {
            Matrix4x4D mtx = Matrix4x4D.Identity;

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Assert.True(MathHelper.Equal(actual, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertTranslationTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateTranslation(23, 42, 666);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertRotationTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateFromYawPitchRoll(3, 4, 5);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertScaleTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateScale(23, 42, -666);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertProjectionTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(1, 1.333f, 0.1f, 666);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertAffineTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateFromYawPitchRoll(3, 4, 5) *
                            Matrix4x4D.CreateScale(23, 42, -666) *
                            Matrix4x4D.CreateTranslation(17, 53, 89);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertRank3()
        {
            // A 4x4 Matrix having a rank of 3
            Matrix4x4D mtx = new Matrix4x4D(1.0f, 2.0f, 3.0f, 0.0f,
                                          5.0f, 1.0f, 6.0f, 0.0f,
                                          8.0f, 9.0f, 1.0f, 0.0f,
                                          4.0f, 7.0f, 3.0f, 0.0f);

            Matrix4x4D actual = mtx.Invert();
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.False(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        static void DecomposeTest(double yaw, double pitch, double roll, Vector3D expectedTranslation, Vector3D expectedScales)
        {
            QuaternionD expectedRotation = QuaternionD.CreateFromYawPitchRoll(MathHelper.ToRadiansD(yaw),
                                                                            MathHelper.ToRadiansD(pitch),
                                                                            MathHelper.ToRadiansD(roll));

            Matrix4x4D m = Matrix4x4D.CreateScale(expectedScales) *
                          Matrix4x4D.CreateFromQuaternion(expectedRotation) *
                          Matrix4x4D.CreateTranslation(expectedTranslation);


            bool actualResult = Matrix4x4D.Decompose(m, out Vector3D scales, out QuaternionD rotation, out Vector3D translation);
            Assert.True(actualResult, "Matrix4x4D.Decompose did not return expected value.");

            bool scaleIsZeroOrNegative = expectedScales.X <= 0 ||
                                         expectedScales.Y <= 0 ||
                                         expectedScales.Z <= 0;

            if (scaleIsZeroOrNegative)
            {
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.X), Math.Abs(scales.X)), "Matrix4x4D.Decompose did not return expected value.");
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.Y), Math.Abs(scales.Y)), "Matrix4x4D.Decompose did not return expected value.");
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.Z), Math.Abs(scales.Z)), "Matrix4x4D.Decompose did not return expected value.");
            }
            else
            {
                Assert.True(MathHelper.Equal(expectedScales, scales), string.Format("Matrix4x4D.Decompose did not return expected value Expected:{0} actual:{1}.", expectedScales, scales));
                Assert.True(MathHelper.EqualRotation(expectedRotation, rotation), string.Format("Matrix4x4D.Decompose did not return expected value. Expected:{0} actual:{1}.", expectedRotation, rotation));
            }

            Assert.True(MathHelper.Equal(expectedTranslation, translation), string.Format("Matrix4x4D.Decompose did not return expected value. Expected:{0} actual:{1}.", expectedTranslation, translation));
        }

        // Various rotation decompose test.
        [Test]
        public void Matrix4x4DecomposeTest01()
        {
            DecomposeTest(10.0f, 20.0f, 30.0f, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));

            const double step = 35.0f;

            for (double yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
            {
                for (double pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
                {
                    for (double rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                    {
                        DecomposeTest(yawAngle, pitchAngle, rollAngle, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));
                    }
                }
            }
        }

        // Various scaled matrix decompose test.
        [Test]
        public void Matrix4x4DecomposeTest02()
        {
            DecomposeTest(10.0f, 20.0f, 30.0f, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));

            // Various scales.
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1, 2, 3));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1, 3, 2));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2, 1, 3));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2, 3, 1));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3, 1, 2));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3, 2, 1));

            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(-2, 1, 1));

            // Small scales.
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1e-4f, 2e-4f, 3e-4f));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1e-4f, 3e-4f, 2e-4f));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2e-4f, 1e-4f, 3e-4f));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2e-4f, 3e-4f, 1e-4f));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3e-4f, 1e-4f, 2e-4f));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3e-4f, 2e-4f, 1e-4f));

            // Zero scales.
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(0, 0, 0));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, 0, 0));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(0, 1, 0));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(0, 0, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(0, 1, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, 0, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, 1, 0));

            // Negative scales.
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(-1, -1, -1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, -1, -1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(-1, 1, -1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(-1, -1, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(-1, 1, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, -1, 1));
            DecomposeTest(0, 0, 0, new Vector3D(10, 20, 30), new Vector3D(1, 1, -1));
        }

        static void DecomposeScaleTest(double sx, double sy, double sz)
        {
            Matrix4x4D m = Matrix4x4D.CreateScale(sx, sy, sz);

            Vector3D expectedScales = new Vector3D(sx, sy, sz);

            bool actualResult = Matrix4x4D.Decompose(m, out Vector3D scales, out QuaternionD rotation, out Vector3D translation);
            Assert.True(actualResult, "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(expectedScales, scales), "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.EqualRotation(QuaternionD.Identity, rotation), "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(Vector3D.Zero, translation), "Matrix4x4D.Decompose did not return expected value.");
        }

        // Tiny scale decompose test.
        [Test]
        public void Matrix4x4DecomposeTest03()
        {
            DecomposeScaleTest(1, 2e-4f, 3e-4f);
            DecomposeScaleTest(1, 3e-4f, 2e-4f);
            DecomposeScaleTest(2e-4f, 1, 3e-4f);
            DecomposeScaleTest(2e-4f, 3e-4f, 1);
            DecomposeScaleTest(3e-4f, 1, 2e-4f);
            DecomposeScaleTest(3e-4f, 2e-4f, 1);
        }

        [Test]
        public void Matrix4x4DecomposeTest04() => 
            Assert.False(Matrix4x4D.Decompose(GenerateIncrementalMatrixNumber(), out _, out _, out _), "decompose should have failed.");

        // Transform by QuaternionD test
        [Test]
        public void Matrix4x4TransformTest()
        {
            Matrix4x4D target = GenerateIncrementalMatrixNumber();

            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));

            QuaternionD q = QuaternionD.CreateFromRotationMatrix(m);

            Matrix4x4D expected = target * m;
            Matrix4x4D actual;
            actual = Matrix4x4D.Transform(target, q);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.Transform did not return the expected value.");
        }

        // A test for CreateRotationX (double)
        [Test]
        public void Matrix4x4CreateRotationXTest()
        {
            double radians = MathHelper.ToRadiansD(30.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 1.0,
                M22 = 0.8660254,
                M23 = 0.5,
                M32 = -0.5,
                M33 = 0.8660254,
                M44 = 1.0
            };

            Matrix4x4D actual;

            actual = Matrix4x4D.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationX (double)
        // CreateRotationX of zero degree
        [Test]
        public void Matrix4x4CreateRotationXTest1()
        {
            double radians = 0;

            Matrix4x4D expected = Matrix4x4D.Identity;
            Matrix4x4D actual = Matrix4x4D.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationX (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationXCenterTest()
        {
            double radians = MathHelper.ToRadians(30.0f);
            Vector3D center = new Vector3D(23, 42, 66);

            Matrix4x4D rotateAroundZero = Matrix4x4D.CreateRotationX(radians, Vector3D.Zero);
            Matrix4x4D rotateAroundZeroExpected = Matrix4x4D.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4D rotateAroundCenter = Matrix4x4D.CreateRotationX(radians, center);
            Matrix4x4D rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationX(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CreateRotationY (double)
        [Test]
        public void Matrix4x4CreateRotationYTest()
        {
            double radians = MathHelper.ToRadiansD(60.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.49999997,
                M13 = -0.866025448,
                M22 = 1.0,
                M31 = 0.866025448,
                M33 = 0.49999997,
                M44 = 1.0
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateRotationY did not return the expected value.");
        }

        // A test for RotationY (double)
        // CreateRotationY test for negative angle
        [Test]
        public void Matrix4x4CreateRotationYTest1()
        {
            double radians = MathHelper.ToRadiansD(-300.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.49999997,
                M13 = -0.866025448,
                M22 = 1.0,
                M31 = 0.866025448,
                M33 = 0.49999997,
                M44 = 1.0
            };

            Matrix4x4D actual = Matrix4x4D.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateRotationY did not return the expected value.");
        }

        // A test for CreateRotationY (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationYCenterTest()
        {
            double radians = MathHelper.ToRadians(30.0f);
            Vector3D center = new Vector3D(23, 42, 66);

            Matrix4x4D rotateAroundZero = Matrix4x4D.CreateRotationY(radians, Vector3D.Zero);
            Matrix4x4D rotateAroundZeroExpected = Matrix4x4D.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4D rotateAroundCenter = Matrix4x4D.CreateRotationY(radians, center);
            Matrix4x4D rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationY(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CreateFromAxisAngle(Vector3D,double)
        [Test]
        public void Matrix4x4CreateFromAxisAngleTest()
        {
            double radians = MathHelper.ToRadians(-30.0f);

            Matrix4x4D expected = Matrix4x4D.CreateRotationX(radians);
            Matrix4x4D actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4D.CreateRotationY(radians);
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4D.CreateRotationZ(radians);
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4D.CreateFromQuaternion(QuaternionD.CreateFromAxisAngle(Vector3D.Normalize(Vector3D.One), radians));
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.Normalize(Vector3D.One), radians);
            Assert.True(MathHelper.Equal(expected, actual));

            const int rotCount = 16;
            for (int i = 0; i < rotCount; ++i)
            {
                double latitude = (2.0f * MathHelper.Pi) * ((double)i / (double)rotCount);
                for (int j = 0; j < rotCount; ++j)
                {
                    double longitude = -MathHelper.PiOver2 + MathHelper.Pi * ((double)j / (double)rotCount);

                    Matrix4x4D m = Matrix4x4D.CreateRotationZ(longitude) * Matrix4x4D.CreateRotationY(latitude);
                    Vector3D axis = new Vector3D(m.M11, m.M12, m.M13);
                    for (int k = 0; k < rotCount; ++k)
                    {
                        double rot = (2.0f * MathHelper.Pi) * ((double)k / (double)rotCount);
                        expected = Matrix4x4D.CreateFromQuaternion(QuaternionD.CreateFromAxisAngle(axis, rot));
                        actual = Matrix4x4D.CreateFromAxisAngle(axis, rot);
                        Assert.True(MathHelper.Equal(expected, actual));
                    }
                }
            }
        }

        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest1()
        {
            double yawAngle = MathHelper.ToRadians(30.0f);
            double pitchAngle = MathHelper.ToRadians(40.0f);
            double rollAngle = MathHelper.ToRadians(50.0f);

            Matrix4x4D yaw = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, yawAngle);
            Matrix4x4D pitch = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, pitchAngle);
            Matrix4x4D roll = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, rollAngle);

            Matrix4x4D expected = roll * pitch * yaw;
            Matrix4x4D actual = Matrix4x4D.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.True(MathHelper.Equal(expected, actual));
        }

        // Covers more numeric rigions
        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest2()
        {
            const double step = 35.0f;

            for (double yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
            {
                for (double pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
                {
                    for (double rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                    {
                        double yawRad = MathHelper.ToRadiansD(yawAngle);
                        double pitchRad = MathHelper.ToRadiansD(pitchAngle);
                        double rollRad = MathHelper.ToRadiansD(rollAngle);
                        Matrix4x4D yaw = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, yawRad);
                        Matrix4x4D pitch = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, pitchRad);
                        Matrix4x4D roll = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, rollRad);

                        Matrix4x4D expected = roll * pitch * yaw;
                        Matrix4x4D actual = Matrix4x4D.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), string.Format("Yaw:{0} Pitch:{1} Roll:{2}", yawAngle, pitchAngle, rollAngle));
                    }
                }
            }
        }

        // Simple shadow test.
        [Test]
        public void Matrix4x4CreateShadowTest01()
        {
            Vector3D lightDir = Vector3D.UnitY;
            PlaneD PlaneD = new PlaneD(Vector3D.UnitY, 0);

            Matrix4x4D expected = Matrix4x4D.CreateScale(1, 0, 1);

            Matrix4x4D actual = Matrix4x4D.CreateShadow(lightDir, PlaneD);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateShadow did not returned expected value.");
        }

        // Various PlaneD projections.
        [Test]
        public void Matrix4x4CreateShadowTest02()
        {
            // Complex cases.
            PlaneD[] planes = {
                new PlaneD( 0, 1, 0, 0 ),
                new PlaneD( 1, 2, 3, 4 ),
                new PlaneD( 5, 6, 7, 8 ),
                new PlaneD(-1,-2,-3,-4 ),
                new PlaneD(-5,-6,-7,-8 ),
            };

            Vector3D[] points = {
                new Vector3D( 1, 2, 3),
                new Vector3D( 5, 6, 7),
                new Vector3D( 8, 9, 10),
                new Vector3D(-1,-2,-3),
                new Vector3D(-5,-6,-7),
                new Vector3D(-8,-9,-10),
            };

            foreach (PlaneD p in planes)
            {
                PlaneD PlaneD = PlaneD.Normalize(p);

                // Try various direction of light directions.
                var testDirections = new Vector3D[]
                {
                    new Vector3D( -1.0f, 1.0f, 1.0f ),
                    new Vector3D(  0.0f, 1.0f, 1.0f ),
                    new Vector3D(  1.0f, 1.0f, 1.0f ),
                    new Vector3D( -1.0f, 0.0f, 1.0f ),
                    new Vector3D(  0.0f, 0.0f, 1.0f ),
                    new Vector3D(  1.0f, 0.0f, 1.0f ),
                    new Vector3D( -1.0f,-1.0f, 1.0f ),
                    new Vector3D(  0.0f,-1.0f, 1.0f ),
                    new Vector3D(  1.0f,-1.0f, 1.0f ),

                    new Vector3D( -1.0f, 1.0f, 0.0f ),
                    new Vector3D(  0.0f, 1.0f, 0.0f ),
                    new Vector3D(  1.0f, 1.0f, 0.0f ),
                    new Vector3D( -1.0f, 0.0f, 0.0f ),
                    new Vector3D(  0.0f, 0.0f, 0.0f ),
                    new Vector3D(  1.0f, 0.0f, 0.0f ),
                    new Vector3D( -1.0f,-1.0f, 0.0f ),
                    new Vector3D(  0.0f,-1.0f, 0.0f ),
                    new Vector3D(  1.0f,-1.0f, 0.0f ),

                    new Vector3D( -1.0f, 1.0f,-1.0f ),
                    new Vector3D(  0.0f, 1.0f,-1.0f ),
                    new Vector3D(  1.0f, 1.0f,-1.0f ),
                    new Vector3D( -1.0f, 0.0f,-1.0f ),
                    new Vector3D(  0.0f, 0.0f,-1.0f ),
                    new Vector3D(  1.0f, 0.0f,-1.0f ),
                    new Vector3D( -1.0f,-1.0f,-1.0f ),
                    new Vector3D(  0.0f,-1.0f,-1.0f ),
                    new Vector3D(  1.0f,-1.0f,-1.0f ),
                };

                foreach (Vector3D lightDirInfo in testDirections)
                {
                    if (lightDirInfo.Length() < 0.1f)
                        continue;
                    Vector3D lightDir = Vector3D.Normalize(lightDirInfo);

                    if (PlaneD.DotNormal(PlaneD, lightDir) < 0.1f)
                        continue;

                    Matrix4x4D m = Matrix4x4D.CreateShadow(lightDir, PlaneD);
                    Vector3D pp = -PlaneD.D * PlaneD.Normal; // origin of the PlaneD.

                    //
                    foreach (Vector3D point in points)
                    {
                        Vector4D v4 = Vector4D.Transform(point, m);

                        Vector3D sp = new Vector3D(v4.X, v4.Y, v4.Z) / v4.W;

                        // Make sure transformed position is on the PlaneD.
                        Vector3D v = sp - pp;
                        double d = Vector3D.Dot(v, PlaneD.Normal);
                        Assert.True(MathHelper.Equal(d, 0), "Matrix4x4D.CreateShadow did not provide expected value.");

                        // make sure direction between transformed position and original position are same as light direction.
                        if (Vector3D.Dot(point - pp, PlaneD.Normal) > 0.0001f)
                        {
                            Vector3D dir = Vector3D.Normalize(point - sp);
                            Assert.True(MathHelper.Equal(dir, lightDir), "Matrix4x4D.CreateShadow did not provide expected value.");
                        }
                    }
                }
            }
        }

        static void CreateReflectionTest(PlaneD PlaneD, Matrix4x4D expected)
        {
            Matrix4x4D actual = Matrix4x4D.CreateReflection(PlaneD);
            Assert.True(MathHelper.Equal(actual, expected), "Matrix4x4D.CreateReflection did not return expected value.");
        }

        [Test]
        public void Matrix4x4CreateReflectionTest01()
        {
            // XY PlaneD.
            CreateReflectionTest(new PlaneD(Vector3D.UnitZ, 0), Matrix4x4D.CreateScale(1, 1, -1));
            // XZ PlaneD.
            CreateReflectionTest(new PlaneD(Vector3D.UnitY, 0), Matrix4x4D.CreateScale(1, -1, 1));
            // YZ PlaneD.
            CreateReflectionTest(new PlaneD(Vector3D.UnitX, 0), Matrix4x4D.CreateScale(-1, 1, 1));

            // Complex cases.
            PlaneD[] planes = {
                new PlaneD( 0, 1, 0, 0 ),
                new PlaneD( 1, 2, 3, 4 ),
                new PlaneD( 5, 6, 7, 8 ),
                new PlaneD(-1,-2,-3,-4 ),
                new PlaneD(-5,-6,-7,-8 ),
            };

            Vector3D[] points = {
                new Vector3D( 1, 2, 3),
                new Vector3D( 5, 6, 7),
                new Vector3D(-1,-2,-3),
                new Vector3D(-5,-6,-7),
            };

            foreach (PlaneD p in planes)
            {
                PlaneD PlaneD = PlaneD.Normalize(p);
                Matrix4x4D m = Matrix4x4D.CreateReflection(PlaneD);
                Vector3D pp = -PlaneD.D * PlaneD.Normal; // Position on the PlaneD.

                //
                foreach (Vector3D point in points)
                {
                    Vector3D rp = Vector3D.Transform(point, m);

                    // Manually compute reflection point and compare results.
                    Vector3D v = point - pp;
                    double d = Vector3D.Dot(v, PlaneD.Normal);
                    Vector3D vp = point - 2.0f * d * PlaneD.Normal;
                    Assert.True(MathHelper.Equal(rp, vp), "Matrix4x4D.Reflection did not provide expected value.");
                }
            }
        }

        // A test for CreateRotationZ (double)
        [Test]
        public void Matrix4x4CreateRotationZTest()
        {
            double radians = MathHelper.ToRadiansD(50.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.642787635,
                M12 = 0.766044438,
                M21 = -0.766044438,
                M22 = 0.642787635,
                M33 = 1.0,
                M44 = 1.0
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreateRotationZ(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateRotationZ did not return the expected value.");
        }

        // A test for CreateRotationZ (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationZCenterTest()
        {
            double radians = MathHelper.ToRadiansD(30.0);
            Vector3D center = new Vector3D(23, 42, 66);

            Matrix4x4D rotateAroundZero = Matrix4x4D.CreateRotationZ(radians, Vector3D.Zero);
            Matrix4x4D rotateAroundZeroExpected = Matrix4x4D.CreateRotationZ(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4D rotateAroundCenter = Matrix4x4D.CreateRotationZ(radians, center);
            Matrix4x4D rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationZ(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CrateLookAt (Vector3D, Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateLookAtTest()
        {
            Vector3D cameraPosition = new Vector3D(10.0, 20.0, 30.0);
            Vector3D cameraTarget = new Vector3D(3.0, 2.0, -4.0);
            Vector3D cameraUpVector = new Vector3D(0.0, 1.0, 0.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.979457,
                M12 = -0.0928267762,
                M13 = 0.179017,

                M21 = 0.0,
                M22 = 0.8877481,
                M23 = 0.460329473,

                M31 = -0.201652914,
                M32 = -0.450872928,
                M33 = 0.8695112,

                M41 = -3.74498272,
                M42 = -3.30050683,
                M43 = -37.0820961,
                M44 = 1.0
            };

            Matrix4x4D actual = Matrix4x4D.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateLookAt did not return the expected value.");
        }

        // A test for CreateWorld (Vector3D, Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateWorldTest()
        {
            Vector3D objectPosition = new Vector3D(10.0, 20.0, 30.0);
            Vector3D objectForwardDirection = new Vector3D(3.0, 2.0, -4.0);
            Vector3D objectUpVector = new Vector3D(0.0, 1.0, 0.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.799999952,
                M12 = 0,
                M13 = 0.599999964,
                M14 = 0,

                M21 = -0.2228344,
                M22 = 0.928476632,
                M23 = 0.297112525,
                M24 = 0,

                M31 = -0.557086,
                M32 = -0.371390671,
                M33 = 0.742781341,
                M34 = 0,

                M41 = 10,
                M42 = 20,
                M43 = 30,
                M44 = 1.0
            };

            Matrix4x4D actual = Matrix4x4D.CreateWorld(objectPosition, objectForwardDirection, objectUpVector);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateWorld did not return the expected value.");

            Assert.AreEqual(objectPosition, actual.Translation);
            Assert.True(Vector3D.Dot(Vector3D.Normalize(objectUpVector), new Vector3D(actual.M21, actual.M22, actual.M23)) > 0);
            Assert.True(Vector3D.Dot(Vector3D.Normalize(objectForwardDirection), new Vector3D(-actual.M31, -actual.M32, -actual.M33)) > 0.999f);
        }

        // A test for CreateOrtho (double, double, double, double)
        [Test]
        public void Matrix4x4CreateOrthoTest()
        {
            double width = 100.0;
            double height = 200.0;
            double zNearPlane = 1.5;
            double zFarPlane = 1000.0;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.02,
                M22 = 0.01,
                M33 = -0.00100150227,
                M43 = -0.00150225335,
                M44 = 1.0
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreateOrthographic(width, height, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateOrtho did not return the expected value.");
        }

        // A test for CreateOrthoOffCenter (double, double, double, double, double, double)
        [Test]
        public void Matrix4x4CreateOrthoOffCenterTest()
        {
            double left = 10.0;
            double right = 90.0;
            double bottom = 20.0;
            double top = 180.0;
            double zNearPlane = 1.5;
            double zFarPlane = 1000.0;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.025,
                M22 = 0.0125,
                M33 = -0.00100150227,
                M41 = -1.25,
                M42 = -1.25,
                M43 = -0.00150225335,
                M44 = 1.0
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateOrthoOffCenter did not return the expected value.");
        }

        // A test for CreatePerspective (double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveTest()
        {
            double width = 100.0;
            double height = 200.0;
            double zNearPlane = 1.5;
            double zFarPlane = 1000.0;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.03,
                M22 = 0.015,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreatePerspective(width, height, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreatePerspective did not return the expected value.");
        }

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where znear = zfar
        [Test]
        public void Matrix4x4CreatePerspectiveTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double width = 100.0;
                double height = 200.0;
                double zNearPlane = 0.0;
                double zFarPlane = 0.0;

                Matrix4x4D actual = Matrix4x4D.CreatePerspective(width, height, zNearPlane, zFarPlane);
            });
        }

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where near PlaneD is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D actual = Matrix4x4D.CreatePerspective(10, 10, -10, 10);
            });
        }

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where far PlaneD is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D actual = Matrix4x4D.CreatePerspective(10, 10, 10, -10);
            });
        }

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where near PlaneD is beyond far PlaneD
        [Test]
        public void Matrix4x4CreatePerspectiveTest4()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D actual = Matrix4x4D.CreatePerspective(10, 10, 10, 1);
            });
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest()
        {
            double fieldOfView = MathHelper.ToRadiansD(30.0);
            double aspectRatio = 1280.0 / 720.0;
            double zNearPlane = 1.5;
            double zFarPlane = 1000.0;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 2.09927845,
                M22 = 3.73205066,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };
            Matrix4x4D actual;

            actual = Matrix4x4D.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreatePerspectiveFieldOfView did not return the expected value.");
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where filedOfView is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(-1, 1, 1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where filedOfView is more than pi.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.Pi + 0.01f, 1, 1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, -1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where farPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest4()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 1, -10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest5()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 1, 10, 1);
            });
        }

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest()
        {
            double left = 10.0;
            double right = 90.0;
            double bottom = 20.0;
            double top = 180.0;
            double zNearPlane = 1.5;
            double zFarPlane = 1000.0;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.0375,
                M22 = 0.01875,
                M31 = 1.25,
                M32 = 1.25,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreatePerspectiveOffCenter did not return the expected value.");
        }

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where nearPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4D actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, -1, 10);
            });
        }

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where farPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4D actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, 1, -10);
            });
        }

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4D actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, 10, 1);
            });
        }

        // A test for Invert (Matrix4x4D)
        // Non invertible matrix - determinant is zero - singular matrix
        [Test]
        public void Matrix4x4InvertTest1()
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = 1.0,
                M12 = 2.0,
                M13 = 3.0,
                M14 = 4.0,
                M21 = 5.0,
                M22 = 6.0,
                M23 = 7.0,
                M24 = 8.0,
                M31 = 9.0,
                M32 = 10.0,
                M33 = 11.0,
                M34 = 12.0,
                M41 = 13.0,
                M42 = 14.0,
                M43 = 15.0,
                M44 = 16.0
            };

            double detA = a.GetDeterminant();
            Assert.True(MathHelper.Equal(detA, 0.0f), "Matrix4x4D.Invert did not return the expected value.");

            Matrix4x4D actual = a.Invert();

            // all the elements in Actual is NaN
            Assert.True(
                double.IsNaN(actual.M11) && double.IsNaN(actual.M12) && double.IsNaN(actual.M13) && double.IsNaN(actual.M14) &&
                double.IsNaN(actual.M21) && double.IsNaN(actual.M22) && double.IsNaN(actual.M23) && double.IsNaN(actual.M24) &&
                double.IsNaN(actual.M31) && double.IsNaN(actual.M32) && double.IsNaN(actual.M33) && double.IsNaN(actual.M34) &&
                double.IsNaN(actual.M41) && double.IsNaN(actual.M42) && double.IsNaN(actual.M43) && double.IsNaN(actual.M44)
                , "Matrix4x4D.Invert did not return the expected value.");
        }

        // A test for Lerp (Matrix4x4D, Matrix4x4D, double)
        [Test]
        public void Matrix4x4LerpTest()
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = 11.0,
                M12 = 12.0,
                M13 = 13.0,
                M14 = 14.0,
                M21 = 21.0,
                M22 = 22.0,
                M23 = 23.0,
                M24 = 24.0,
                M31 = 31.0,
                M32 = 32.0,
                M33 = 33.0,
                M34 = 34.0,
                M41 = 41.0,
                M42 = 42.0,
                M43 = 43.0,
                M44 = 44.0
            };

            Matrix4x4D b = GenerateIncrementalMatrixNumber();

            double t = 0.5;

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 + (b.M11 - a.M11) * t,
                M12 = a.M12 + (b.M12 - a.M12) * t,
                M13 = a.M13 + (b.M13 - a.M13) * t,
                M14 = a.M14 + (b.M14 - a.M14) * t,

                M21 = a.M21 + (b.M21 - a.M21) * t,
                M22 = a.M22 + (b.M22 - a.M22) * t,
                M23 = a.M23 + (b.M23 - a.M23) * t,
                M24 = a.M24 + (b.M24 - a.M24) * t,

                M31 = a.M31 + (b.M31 - a.M31) * t,
                M32 = a.M32 + (b.M32 - a.M32) * t,
                M33 = a.M33 + (b.M33 - a.M33) * t,
                M34 = a.M34 + (b.M34 - a.M34) * t,

                M41 = a.M41 + (b.M41 - a.M41) * t,
                M42 = a.M42 + (b.M42 - a.M42) * t,
                M43 = a.M43 + (b.M43 - a.M43) * t,
                M44 = a.M44 + (b.M44 - a.M44) * t
            };

            Matrix4x4D actual;
            actual = Matrix4x4D.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.Lerp did not return the expected value.");
        }

        // A test for operator - (Matrix4x4D)
        [Test]
        public void Matrix4x4UnaryNegationTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = -1.0,
                M12 = -2.0,
                M13 = -3.0,
                M14 = -4.0,
                M21 = -5.0,
                M22 = -6.0,
                M23 = -7.0,
                M24 = -8.0,
                M31 = -9.0,
                M32 = -10.0,
                M33 = -11.0,
                M34 = -12.0,
                M41 = -13.0,
                M42 = -14.0,
                M43 = -15.0,
                M44 = -16.0
            };

            Matrix4x4D actual = -a;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.operator - did not return the expected value.");
        }

        // A test for operator - (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4SubtractionTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 - b.M11,
                M12 = a.M12 - b.M12,
                M13 = a.M13 - b.M13,
                M14 = a.M14 - b.M14,
                M21 = a.M21 - b.M21,
                M22 = a.M22 - b.M22,
                M23 = a.M23 - b.M23,
                M24 = a.M24 - b.M24,
                M31 = a.M31 - b.M31,
                M32 = a.M32 - b.M32,
                M33 = a.M33 - b.M33,
                M34 = a.M34 - b.M34,
                M41 = a.M41 - b.M41,
                M42 = a.M42 - b.M42,
                M43 = a.M43 - b.M43,
                M44 = a.M44 - b.M44
            };

            Matrix4x4D actual = a - b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.operator - did not return the expected value.");
        }

        // A test for operator * (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4MultiplyTest1()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

                M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
                M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

                M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
                M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

                M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
                M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
            };

            Matrix4x4D actual = a * b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.operator * did not return the expected value.");
        }

        // A test for operator * (Matrix4x4D, Matrix4x4D)
        // Multiply with identity matrix
        [Test]
        public void Matrix4x4MultiplyTest4()
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = 1.0,
                M12 = 2.0,
                M13 = 3.0,
                M14 = 4.0,
                M21 = 5.0,
                M22 = -6.0,
                M23 = 7.0,
                M24 = -8.0,
                M31 = 9.0,
                M32 = 10.0,
                M33 = 11.0,
                M34 = 12.0,
                M41 = 13.0,
                M42 = -14.0,
                M43 = 15.0,
                M44 = -16.0
            };

            Matrix4x4D b = Matrix4x4D.Identity;

            Matrix4x4D expected = a;
            Matrix4x4D actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.operator * did not return the expected value.");
        }

        // A test for operator + (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4AdditionTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 + b.M11,
                M12 = a.M12 + b.M12,
                M13 = a.M13 + b.M13,
                M14 = a.M14 + b.M14,
                M21 = a.M21 + b.M21,
                M22 = a.M22 + b.M22,
                M23 = a.M23 + b.M23,
                M24 = a.M24 + b.M24,
                M31 = a.M31 + b.M31,
                M32 = a.M32 + b.M32,
                M33 = a.M33 + b.M33,
                M34 = a.M34 + b.M34,
                M41 = a.M41 + b.M41,
                M42 = a.M42 + b.M42,
                M43 = a.M43 + b.M43,
                M44 = a.M44 + b.M44
            };

            Matrix4x4D actual = a + b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.operator + did not return the expected value.");
        }

        // A test for Transpose (Matrix4x4D)
        [Test]
        public void Matrix4x4TransposeTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11,
                M12 = a.M21,
                M13 = a.M31,
                M14 = a.M41,
                M21 = a.M12,
                M22 = a.M22,
                M23 = a.M32,
                M24 = a.M42,
                M31 = a.M13,
                M32 = a.M23,
                M33 = a.M33,
                M34 = a.M43,
                M41 = a.M14,
                M42 = a.M24,
                M43 = a.M34,
                M44 = a.M44
            };

            Matrix4x4D actual = Matrix4x4D.Transpose(a);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix4x4D)
        // Transpose Identity matrix
        [Test]
        public void Matrix4x4TransposeTest1()
        {
            Matrix4x4D a = Matrix4x4D.Identity;
            Matrix4x4D expected = Matrix4x4D.Identity;

            Matrix4x4D actual = Matrix4x4D.Transpose(a);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.Transpose did not return the expected value.");
        }

        // A test for Matrix4x4D (QuaternionD)
        [Test]
        public void Matrix4x4DFromQuaternionTest1()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD q = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadiansD(30.0));

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = 0.875595033,
                M12 = 0.420031041,
                M13 = -0.2385524,
                M14 = 0.0,

                M21 = -0.38175258,
                M22 = 0.904303849,
                M23 = 0.1910483,
                M24 = 0.0,

                M31 = 0.295970082,
                M32 = -0.07621294,
                M33 = 0.952151954,
                M34 = 0.0,

                M41 = 0.0,
                M42 = 0.0,
                M43 = 0.0,
                M44 = 1.0
            };

            Matrix4x4D target = Matrix4x4D.CreateFromQuaternion(q);
            Assert.True(MathHelper.Equal(expected, target), "Matrix4x4D.Matrix4x4D(QuaternionD) did not return the expected value.");
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert X axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest2()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                QuaternionD quat = QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);

                Matrix4x4D expected = Matrix4x4D.CreateRotationX(angle);
                Matrix4x4D actual = Matrix4x4D.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionD is same as we passed QuaternionD.
                QuaternionD q2 = QuaternionD.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert Y axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest3()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                QuaternionD quat = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle);

                Matrix4x4D expected = Matrix4x4D.CreateRotationY(angle);
                Matrix4x4D actual = Matrix4x4D.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionD is same as we passed QuaternionD.
                QuaternionD q2 = QuaternionD.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert Z axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest4()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                QuaternionD quat = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle);

                Matrix4x4D expected = Matrix4x4D.CreateRotationZ(angle);
                Matrix4x4D actual = Matrix4x4D.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionD is same as we passed QuaternionD.
                QuaternionD q2 = QuaternionD.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert XYZ axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest5()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                QuaternionD quat =
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle) *
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle) *
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);

                Matrix4x4D expected =
                    Matrix4x4D.CreateRotationX(angle) *
                    Matrix4x4D.CreateRotationY(angle) *
                    Matrix4x4D.CreateRotationZ(angle);
                Matrix4x4D actual = Matrix4x4D.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionD is same as we passed QuaternionD.
                QuaternionD q2 = QuaternionD.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for ToString ()
        [Test]
        public void Matrix4x4ToStringTest()
        {
            Matrix4x4D a = new Matrix4x4D
            {
                M11 = 11.0,
                M12 = -12.0,
                M13 = -13.3,
                M14 = 14.4,
                M21 = 21.0,
                M22 = 22.0,
                M23 = 23.0,
                M24 = 24.0,
                M31 = 31.0,
                M32 = 32.0,
                M33 = 33.0,
                M34 = 34.0,
                M41 = 41.0,
                M42 = 42.0,
                M43 = 43.0,
                M44 = 44.0
            };

            string expected = string.Format(CultureInfo.CurrentCulture,
                "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}",
                    11.0, -12.0, -13.3, 14.4,
                    21.0, 22.0, 23.0, 24.0,
                    31.0, 32.0, 33.0, 34.0,
                    41.0, 42.0, 43.0, 44.0);

            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        // A test for Add (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4AddTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 + b.M11,
                M12 = a.M12 + b.M12,
                M13 = a.M13 + b.M13,
                M14 = a.M14 + b.M14,
                M21 = a.M21 + b.M21,
                M22 = a.M22 + b.M22,
                M23 = a.M23 + b.M23,
                M24 = a.M24 + b.M24,
                M31 = a.M31 + b.M31,
                M32 = a.M32 + b.M32,
                M33 = a.M33 + b.M33,
                M34 = a.M34 + b.M34,
                M41 = a.M41 + b.M41,
                M42 = a.M42 + b.M42,
                M43 = a.M43 + b.M43,
                M44 = a.M44 + b.M44
            };

            Matrix4x4D actual = Matrix4x4D.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Equals (object)
        [Test]
        public void Matrix4x4EqualsTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber();

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

        // A test for GetHashCode ()
        [Test]
        public void Matrix4x4GetHashCodeTest()
        {
            Matrix4x4D target = GenerateIncrementalMatrixNumber();

            HashCode hash = default;

            hash.Add(target.M11);
            hash.Add(target.M12);
            hash.Add(target.M13);
            hash.Add(target.M14);

            hash.Add(target.M21);
            hash.Add(target.M22);
            hash.Add(target.M23);
            hash.Add(target.M24);

            hash.Add(target.M31);
            hash.Add(target.M32);
            hash.Add(target.M33);
            hash.Add(target.M34);

            hash.Add(target.M41);
            hash.Add(target.M42);
            hash.Add(target.M43);
            hash.Add(target.M44);

            int expected = hash.ToHashCode();
            int actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4MultiplyTest3()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

                M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
                M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

                M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
                M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

                M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
                M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44
            };
            Matrix4x4D actual;
            actual = Matrix4x4D.Multiply(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix4x4D, double)
        [Test]
        public void Matrix4x4MultiplyTest5()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D expected = new Matrix4x4D(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
            Matrix4x4D actual = Matrix4x4D.Multiply(a, 3);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix4x4D, double)
        [Test]
        public void Matrix4x4MultiplyTest6()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D expected = new Matrix4x4D(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
            Matrix4x4D actual = a * 3;

            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Matrix4x4D)
        [Test]
        public void Matrix4x4NegateTest()
        {
            Matrix4x4D m = GenerateIncrementalMatrixNumber();

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = -1.0,
                M12 = -2.0,
                M13 = -3.0,
                M14 = -4.0,
                M21 = -5.0,
                M22 = -6.0,
                M23 = -7.0,
                M24 = -8.0,
                M31 = -9.0,
                M32 = -10.0,
                M33 = -11.0,
                M34 = -12.0,
                M41 = -13.0,
                M42 = -14.0,
                M43 = -15.0,
                M44 = -16.0
            };
            Matrix4x4D actual;

            actual = Matrix4x4D.Negate(m);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator != (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4InequalityTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0;
            expected = true;
            actual = a != b;
            Assert.AreEqual(expected, actual);
        }

        // A test for operator == (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4EqualityTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber();

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.AreEqual(expected, actual);

            // case 2: compare between different values
            b.M11 = 11.0;
            expected = false;
            actual = a == b;
            Assert.AreEqual(expected, actual);
        }

        // A test for Subtract (Matrix4x4D, Matrix4x4D)
        [Test]
        public void Matrix4x4SubtractTest()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber(-8.0);

            Matrix4x4D expected = new Matrix4x4D
            {
                M11 = a.M11 - b.M11,
                M12 = a.M12 - b.M12,
                M13 = a.M13 - b.M13,
                M14 = a.M14 - b.M14,
                M21 = a.M21 - b.M21,
                M22 = a.M22 - b.M22,
                M23 = a.M23 - b.M23,
                M24 = a.M24 - b.M24,
                M31 = a.M31 - b.M31,
                M32 = a.M32 - b.M32,
                M33 = a.M33 - b.M33,
                M34 = a.M34 - b.M34,
                M41 = a.M41 - b.M41,
                M42 = a.M42 - b.M42,
                M43 = a.M43 - b.M43,
                M44 = a.M44 - b.M44
            };

            Matrix4x4D actual = Matrix4x4D.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        private static void CreateBillboardFact(Vector3D placeDirection, Vector3D cameraUpVector, Matrix4x4D expectedRotation)
        {
            Vector3D cameraPosition = new Vector3D(3.0, 4.0, 5.0);
            Vector3D objectPosition = cameraPosition + placeDirection * 10.0;
            Matrix4x4D expected = expectedRotation * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateBillboard did not return the expected value.");
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Forward side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest01()
        {
            // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(180.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Backward side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest02()
        {
            // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(0.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Right side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest03()
        {
            // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Left side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest04()
        {
            // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3D(-1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(-90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Up side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest05()
        {
            // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3D(0, 1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(180.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Down side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest06()
        {
            // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3D(0, -1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(0.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Right side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest07()
        {
            // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Left side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest08()
        {
            // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(-90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Up side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest09()
        {
            // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(-90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Down side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest10()
        {
            // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3D(0, -1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(90.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Forward side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest11()
        {
            // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(180.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Backward side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateBillboardTest12()
        {
            // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadiansD(90.0)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadiansD(0.0)));
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [Test]
        public void Matrix4x4CreateBillboardTooCloseTest1()
        {
            Vector3D objectPosition = new Vector3D(3.0, 4.0, 5.0);
            Vector3D cameraPosition = objectPosition;
            Vector3D cameraUpVector = new Vector3D(0, 1, 0);

            // Doesn't pass camera face direction. CreateBillboard uses new Vector3D(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(180.0)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(0, 0, 1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateBillboard did not return the expected value.");
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and passed cameraForwardVector.
        [Test]
        public void Matrix4x4CreateBillboardTooCloseTest2()
        {
            Vector3D objectPosition = new Vector3D(3.0, 4.0, 5.0);
            Vector3D cameraPosition = objectPosition;
            Vector3D cameraUpVector = new Vector3D(0, 1, 0);

            // Passes Vector3D.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadiansD(-90.0)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(1, 0, 0));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateBillboard did not return the expected value.");
        }

        private static void CreateConstrainedBillboardFact(Vector3D placeDirection, Vector3D rotateAxis, Matrix4x4D expectedRotation)
        {
            Vector3D cameraPosition = new Vector3D(3.0, 4.0, 5.0);
            Vector3D objectPosition = cameraPosition + placeDirection * 10.0;
            Matrix4x4D expected = expectedRotation * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");

            // When you move camera along rotateAxis, result must be same.
            cameraPosition += rotateAxis * 10.0;
            actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");

            cameraPosition -= rotateAxis * 30.0;
            actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Forward side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest01()
        {
            // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Backward side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest02()
        {
            // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadians(0)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Right side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest03()
        {
            // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadians(90)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Left side of camera on XZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest04()
        {
            // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3D(-1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.ToRadians(-90)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Up side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest05()
        {
            // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(180)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Down side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest06()
        {
            // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, -1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(0)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Right side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest07()
        {
            // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3D(1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Left side of camera on XY-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest08()
        {
            // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Up side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest09()
        {
            // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Down side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest10()
        {
            // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, -1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Forward side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest11()
        {
            // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Place object at Backward side of camera on YZ-PlaneD
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest12()
        {
            // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4D.CreateRotationX(MathHelper.ToRadians(0.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest1()
        {
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D cameraPosition = objectPosition;
            Vector3D cameraUpVector = new Vector3D(0, 1, 0);

            // Doesn't pass camera face direction. CreateConstrainedBillboard uses new Vector3D(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(0, 0, 1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and passed cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest2()
        {
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D cameraPosition = objectPosition;
            Vector3D cameraUpVector = new Vector3D(0, 1, 0);

            // Passes Vector3D.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadians(-90.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(1, 0, 0), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Angle between rotateAxis and camera to object vector is too small. And use doesn't passed objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest1()
        {
            // Place camera at up side of object.
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D rotateAxis = new Vector3D(0, 1, 0);
            Vector3D cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks new Vector3D(0, 0, -1) as object forward vector.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Angle between rotateAxis and camera to object vector is too small. And user doesn't passed objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest2()
        {
            // Place camera at up side of object.
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D rotateAxis = new Vector3D(0, 0, -1);
            Vector3D cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks new Vector3D(1, 0, 0) as object forward vector.
            Matrix4x4D expected = Matrix4x4D.CreateRotationX(MathHelper.ToRadians(-90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(-90.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed correct objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest3()
        {
            // Place camera at up side of object.
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D rotateAxis = new Vector3D(0, 1, 0);
            Vector3D cameraPosition = objectPosition + rotateAxis * 10.0f;

            // User passes correct objectForwardVector.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest4()
        {
            // Place camera at up side of object.
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D rotateAxis = new Vector3D(0, 1, 0);
            Vector3D cameraPosition = objectPosition + rotateAxis * 10.0f;

            // User passes correct objectForwardVector.
            Matrix4x4D expected = Matrix4x4D.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 1, 0));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest5()
        {
            // Place camera at up side of object.
            Vector3D objectPosition = new Vector3D(3.0f, 4.0f, 5.0f);
            Vector3D rotateAxis = new Vector3D(0, 0, -1);
            Vector3D cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks Vector3D.Right as object forward vector.
            Matrix4x4D expected = Matrix4x4D.CreateRotationX(MathHelper.ToRadians(-90.0f)) * Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(-90.0f)) * Matrix4x4D.CreateTranslation(objectPosition);
            Matrix4x4D actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateScale (Vector3D)
        [Test]
        public void Matrix4x4CreateScaleTest1()
        {
            Vector3D scales = new Vector3D(2.0f, 3.0f, 4.0f);
            Matrix4x4D expected = new Matrix4x4D(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 3.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 4.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4D actual = Matrix4x4D.CreateScale(scales);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateScale (Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest1()
        {
            Vector3D scale = new Vector3D(3, 4, 5);
            Vector3D center = new Vector3D(23, 42, 666);

            Matrix4x4D scaleAroundZero = Matrix4x4D.CreateScale(scale, Vector3D.Zero);
            Matrix4x4D scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4D scaleAroundCenter = Matrix4x4D.CreateScale(scale, center);
            Matrix4x4D scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateScale (double)
        [Test]
        public void Matrix4x4CreateScaleTest2()
        {
            double scale = 2.0f;
            Matrix4x4D expected = new Matrix4x4D(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 2.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 2.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4D actual = Matrix4x4D.CreateScale(scale);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateScale (double, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest2()
        {
            double scale = 5;
            Vector3D center = new Vector3D(23, 42, 666);

            Matrix4x4D scaleAroundZero = Matrix4x4D.CreateScale(scale, Vector3D.Zero);
            Matrix4x4D scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4D scaleAroundCenter = Matrix4x4D.CreateScale(scale, center);
            Matrix4x4D scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateScale (double, double, double)
        [Test]
        public void Matrix4x4CreateScaleTest3()
        {
            double xScale = 2.0f;
            double yScale = 3.0f;
            double zScale = 4.0f;
            Matrix4x4D expected = new Matrix4x4D(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 3.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 4.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4D actual = Matrix4x4D.CreateScale(xScale, yScale, zScale);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateScale (double, double, double, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest3()
        {
            Vector3D scale = new Vector3D(3, 4, 5);
            Vector3D center = new Vector3D(23, 42, 666);

            Matrix4x4D scaleAroundZero = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z, Vector3D.Zero);
            Matrix4x4D scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4D scaleAroundCenter = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z, center);
            Matrix4x4D scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z) * Matrix4x4D.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateTranslation (Vector3D)
        [Test]
        public void Matrix4x4CreateTranslationTest1()
        {
            Vector3D position = new Vector3D(2.0f, 3.0f, 4.0f);
            Matrix4x4D expected = new Matrix4x4D(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                2.0f, 3.0f, 4.0f, 1.0f);

            Matrix4x4D actual = Matrix4x4D.CreateTranslation(position);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateTranslation (double, double, double)
        [Test]
        public void Matrix4x4CreateTranslationTest2()
        {
            double xPosition = 2.0f;
            double yPosition = 3.0f;
            double zPosition = 4.0f;

            Matrix4x4D expected = new Matrix4x4D(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                2.0f, 3.0f, 4.0f, 1.0f);

            Matrix4x4D actual = Matrix4x4D.CreateTranslation(xPosition, yPosition, zPosition);
            Assert.AreEqual(expected, actual);
        }

        // A test for Translation
        [Test]
        public void Matrix4x4TranslationTest()
        {
            Matrix4x4D a = GenerateTestMatrix();
            Matrix4x4D b = a;

            // Transformed vector that has same semantics of property must be same.
            Vector3D val = new Vector3D(a.M41, a.M42, a.M43);
            Assert.AreEqual(val, a.Translation);

            // Set value and get value must be same.
            val = new Vector3D(1.0f, 2.0f, 3.0f);
            a.Translation = val;
            Assert.AreEqual(val, a.Translation);

            // Make sure it only modifies expected value of matrix.
            Assert.True(
                a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 && a.M14 == b.M14 &&
                a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 && a.M24 == b.M24 &&
                a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33 && a.M34 == b.M34 &&
                a.M41 != b.M41 && a.M42 != b.M42 && a.M43 != b.M43 && a.M44 == b.M44);
        }

        // A test for Equals (Matrix4x4D)
        [Test]
        public void Matrix4x4EqualsTest1()
        {
            Matrix4x4D a = GenerateIncrementalMatrixNumber();
            Matrix4x4D b = GenerateIncrementalMatrixNumber();

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

        // A test for IsIdentity
        [Test]
        public void Matrix4x4IsIdentityTest()
        {
            Assert.True(Matrix4x4D.Identity.IsIdentity);
            Assert.True(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1).IsIdentity);
            Assert.False(new Matrix4x4D(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0).IsIdentity);
        }

        // A test for Matrix4x4D comparison involving NaN values
        [Test]
        public void Matrix4x4EqualsNanTest()
        {
            Matrix4x4D a = new Matrix4x4D(double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D b = new Matrix4x4D(0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D c = new Matrix4x4D(0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D d = new Matrix4x4D(0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D e = new Matrix4x4D(0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D f = new Matrix4x4D(0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D g = new Matrix4x4D(0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D h = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D i = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4D j = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0, 0);
            Matrix4x4D k = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0, 0);
            Matrix4x4D l = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0, 0);
            Matrix4x4D m = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0, 0);
            Matrix4x4D n = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0, 0);
            Matrix4x4D o = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN, 0);
            Matrix4x4D p = new Matrix4x4D(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, double.NaN);

            Assert.False(a == new Matrix4x4D());
            Assert.False(b == new Matrix4x4D());
            Assert.False(c == new Matrix4x4D());
            Assert.False(d == new Matrix4x4D());
            Assert.False(e == new Matrix4x4D());
            Assert.False(f == new Matrix4x4D());
            Assert.False(g == new Matrix4x4D());
            Assert.False(h == new Matrix4x4D());
            Assert.False(i == new Matrix4x4D());
            Assert.False(j == new Matrix4x4D());
            Assert.False(k == new Matrix4x4D());
            Assert.False(l == new Matrix4x4D());
            Assert.False(m == new Matrix4x4D());
            Assert.False(n == new Matrix4x4D());
            Assert.False(o == new Matrix4x4D());
            Assert.False(p == new Matrix4x4D());

            Assert.True(a != new Matrix4x4D());
            Assert.True(b != new Matrix4x4D());
            Assert.True(c != new Matrix4x4D());
            Assert.True(d != new Matrix4x4D());
            Assert.True(e != new Matrix4x4D());
            Assert.True(f != new Matrix4x4D());
            Assert.True(g != new Matrix4x4D());
            Assert.True(h != new Matrix4x4D());
            Assert.True(i != new Matrix4x4D());
            Assert.True(j != new Matrix4x4D());
            Assert.True(k != new Matrix4x4D());
            Assert.True(l != new Matrix4x4D());
            Assert.True(m != new Matrix4x4D());
            Assert.True(n != new Matrix4x4D());
            Assert.True(o != new Matrix4x4D());
            Assert.True(p != new Matrix4x4D());

            Assert.False(a.Equals(new Matrix4x4D()));
            Assert.False(b.Equals(new Matrix4x4D()));
            Assert.False(c.Equals(new Matrix4x4D()));
            Assert.False(d.Equals(new Matrix4x4D()));
            Assert.False(e.Equals(new Matrix4x4D()));
            Assert.False(f.Equals(new Matrix4x4D()));
            Assert.False(g.Equals(new Matrix4x4D()));
            Assert.False(h.Equals(new Matrix4x4D()));
            Assert.False(i.Equals(new Matrix4x4D()));
            Assert.False(j.Equals(new Matrix4x4D()));
            Assert.False(k.Equals(new Matrix4x4D()));
            Assert.False(l.Equals(new Matrix4x4D()));
            Assert.False(m.Equals(new Matrix4x4D()));
            Assert.False(n.Equals(new Matrix4x4D()));
            Assert.False(o.Equals(new Matrix4x4D()));
            Assert.False(p.Equals(new Matrix4x4D()));

            Assert.False(a.IsIdentity);
            Assert.False(b.IsIdentity);
            Assert.False(c.IsIdentity);
            Assert.False(d.IsIdentity);
            Assert.False(e.IsIdentity);
            Assert.False(f.IsIdentity);
            Assert.False(g.IsIdentity);
            Assert.False(h.IsIdentity);
            Assert.False(i.IsIdentity);
            Assert.False(j.IsIdentity);
            Assert.False(k.IsIdentity);
            Assert.False(l.IsIdentity);
            Assert.False(m.IsIdentity);
            Assert.False(n.IsIdentity);
            Assert.False(o.IsIdentity);
            Assert.False(p.IsIdentity);

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
            Assert.False(j.Equals(j));
            Assert.False(k.Equals(k));
            Assert.False(l.Equals(l));
            Assert.False(m.Equals(m));
            Assert.False(n.Equals(n));
            Assert.False(o.Equals(o));
            Assert.False(p.Equals(p));
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Matrix4x4SizeofTest()
        {
            Assert.AreEqual(128, sizeof(Matrix4x4D));
            Assert.AreEqual(256, sizeof(Matrix4x4_2x));
            Assert.AreEqual(136, sizeof(Matrix4x4Plusdouble));
            Assert.AreEqual(272, sizeof(Matrix4x4Plusdouble_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Matrix4x4_2x
        {
            private Matrix4x4D _a;
            private Matrix4x4D _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Matrix4x4Plusdouble
        {
            private Matrix4x4D _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Matrix4x4Plusdouble_2x
        {
            private Matrix4x4Plusdouble _a;
            private Matrix4x4Plusdouble _b;
        }

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void Matrix4x4DieldOffsetTest()
        {
            Matrix4x4D mat = new Matrix4x4D();

            double* basePtr = &mat.M11; // Take address of first element
            Matrix4x4D* matPtr = &mat; // Take address of whole matrix

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(matPtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&mat.M11));
            Assert.AreEqual(new IntPtr(basePtr + 1), new IntPtr(&mat.M12));
            Assert.AreEqual(new IntPtr(basePtr + 2), new IntPtr(&mat.M13));
            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&mat.M14));

            Assert.AreEqual(new IntPtr(basePtr + 4), new IntPtr(&mat.M21));
            Assert.AreEqual(new IntPtr(basePtr + 5), new IntPtr(&mat.M22));
            Assert.AreEqual(new IntPtr(basePtr + 6), new IntPtr(&mat.M23));
            Assert.AreEqual(new IntPtr(basePtr + 7), new IntPtr(&mat.M24));

            Assert.AreEqual(new IntPtr(basePtr + 8), new IntPtr(&mat.M31));
            Assert.AreEqual(new IntPtr(basePtr + 9), new IntPtr(&mat.M32));
            Assert.AreEqual(new IntPtr(basePtr + 10), new IntPtr(&mat.M33));
            Assert.AreEqual(new IntPtr(basePtr + 11), new IntPtr(&mat.M34));

            Assert.AreEqual(new IntPtr(basePtr + 12), new IntPtr(&mat.M41));
            Assert.AreEqual(new IntPtr(basePtr + 13), new IntPtr(&mat.M42));
            Assert.AreEqual(new IntPtr(basePtr + 14), new IntPtr(&mat.M43));
            Assert.AreEqual(new IntPtr(basePtr + 15), new IntPtr(&mat.M44));
        }

        [Test]
        public void PerspectiveFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4D.CreatePerspective(1.0f, 1.0f, nearPlaneDistance, double.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }

        [Test]
        public void PerspectiveFieldOfViewFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), 1.5f, nearPlaneDistance, double.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }

        [Test]
        public void PerspectiveOffCenterFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4D.CreatePerspectiveOffCenter(0.0f, 0.0f, 1.0f, 1.0f, nearPlaneDistance, double.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }
    }
}
