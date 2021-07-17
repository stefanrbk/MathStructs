using NUnit.Framework;

using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Tests
{
    public class QuaternionDFTests
    {
        #region Public Methods

        // A test for operator + (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDAdditionTest()
        {
            QuaternionD a = new QuaternionD(1.0, 2.0, 3.0, 4.0);
            QuaternionD b = new QuaternionD(5.0, 6.0, 7.0, 8.0);

            QuaternionD expected = new QuaternionD(6.0, 8.0, 10.0, 12.0);
            QuaternionD actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator + did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Add (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDAddTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(6.0f, 8.0f, 10.0f, 12.0f);
            QuaternionD actual;

            actual = QuaternionD.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Concatenate(QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDConcatenateTest1()
        {
            QuaternionD b = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD a = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionD actual;

            actual = QuaternionD.Concatenate(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Concatenate did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Conjugate(QuaternionD)
        [Test]
        public void QuaternionDConjugateTest1()
        {
            QuaternionD a = new QuaternionD(1, 2, 3, 4);

            QuaternionD expected = new QuaternionD(-1, -2, -3, 4);
            QuaternionD actual;

            actual = QuaternionD.Conjugate(a);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Conjugate did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for QuaternionD (double, double, double, double)
        [Test]
        public void QuaternionDConstructorTest()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;
            double w = 4.0;

            QuaternionD target = new QuaternionD(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "QuaternionD.constructor (x,y,z,w) did not return the expected value.");
        }

        // A test for QuaternionD (Vector3Df, double)
        [Test]
        public void QuaternionDConstructorTest1()
        {
            Vector3D v = new Vector3D(1.0, 2.0, 3.0);
            double w = 4.0;

            QuaternionD target = new QuaternionD(v, w);
            Assert.True(MathHelper.Equal(target.X, v.X) && MathHelper.Equal(target.Y, v.Y) && MathHelper.Equal(target.Z, v.Z) && MathHelper.Equal(target.W, w),
                "QuaternionD.constructor (Vector3Df,w) did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3Df, double)
        [Test]
        public void QuaternionDCreateFromAxisAngleTest()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            double angle = MathHelper.ToRadians(30.0);

            QuaternionD expected = new QuaternionD(0.0691723, 0.1383446, 0.207516879, 0.9659258);
            QuaternionD actual;

            actual = QuaternionD.CreateFromAxisAngle(axis, angle);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.CreateFromAxisAngle did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for CreateFromAxisAngle (Vector3Df, double)
        // CreateFromAxisAngle of zero vector
        [Test]
        public void QuaternionDCreateFromAxisAngleTest1()
        {
            Vector3D axis = new Vector3D();
            double angle = MathHelper.ToRadians(-30.0f);

            double cos = (double)System.Math.Cos(angle / 2.0f);
            QuaternionD actual = QuaternionD.CreateFromAxisAngle(axis, angle);

            Assert.True(actual.X == 0.0f && actual.Y == 0.0f && actual.Z == 0.0f && MathHelper.Equal(cos, actual.W)
                , "QuaternionD.CreateFromAxisAngle did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3Df, double)
        // CreateFromAxisAngle of angle = 30 && 750
        [Test]
        public void QuaternionDCreateFromAxisAngleTest2()
        {
            Vector3D axis = new Vector3D(1, 0, 0);
            double angle1 = MathHelper.ToRadians(30.0f);
            double angle2 = MathHelper.ToRadians(750.0f);

            QuaternionD actual1 = QuaternionD.CreateFromAxisAngle(axis, angle1);
            QuaternionD actual2 = QuaternionD.CreateFromAxisAngle(axis, angle2);
            Assert.True(MathHelper.Equal(actual1, actual2), $"QuaternionD.CreateFromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
        }

        // A test for CreateFromAxisAngle (Vector3Df, double)
        // CreateFromAxisAngle of angle = 30 && 390
        [Test]
        public void QuaternionDCreateFromAxisAngleTest3()
        {
            Vector3D axis = new Vector3D(1, 0, 0);
            double angle1 = MathHelper.ToRadians(30.0f);
            double angle2 = MathHelper.ToRadians(390.0f);

            QuaternionD actual1 = QuaternionD.CreateFromAxisAngle(axis, angle1);
            QuaternionD actual2 = QuaternionD.CreateFromAxisAngle(axis, angle2);
            actual1.X = -actual1.X;
            actual1.W = -actual1.W;

            Assert.True(MathHelper.Equal(actual1, actual2), $"QuaternionD.CreateFromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
        }

        [Test]
        public void QuaternionDCreateFromYawPitchRollTest1()
        {
            double yawAngle = MathHelper.ToRadians(30.0f);
            double pitchAngle = MathHelper.ToRadians(40.0f);
            double rollAngle = MathHelper.ToRadians(50.0f);

            QuaternionD yaw = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, yawAngle);
            QuaternionD pitch = QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, pitchAngle);
            QuaternionD roll = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, rollAngle);

            QuaternionD expected = yaw * pitch * roll;
            QuaternionD actual = QuaternionD.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.QuaternionDCreateFromYawPitchRollTest1 did not return the expected value: expected {expected} actual {actual}");
        }

        // Covers more numeric rigions
        [Test]
        public void QuaternionDCreateFromYawPitchRollTest2()
        {
            const double step = 35.0f;

            for (double yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
            {
                for (double pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
                {
                    for (double rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                    {
                        double yawRad = MathHelper.ToRadians(yawAngle);
                        double pitchRad = MathHelper.ToRadians(pitchAngle);
                        double rollRad = MathHelper.ToRadians(rollAngle);

                        QuaternionD yaw = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, yawRad);
                        QuaternionD pitch = QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, pitchRad);
                        QuaternionD roll = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, rollRad);

                        QuaternionD expected = yaw * pitch * roll;
                        QuaternionD actual = QuaternionD.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.QuaternionDCreateFromYawPitchRollTest2 Yaw:{yawAngle} Pitch:{pitchAngle} Roll:{rollAngle} did not return the expected value: expected {expected} actual {actual}");
                    }
                }
            }
        }

        // A test for Divide (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDDivideTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);
            QuaternionD actual;

            actual = QuaternionD.Divide(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Divide did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator / (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDDivisionTest1()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);
            QuaternionD actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator / did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Dot (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDDotTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            double expected = 70.0f;
            double actual;

            actual = QuaternionD.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Dot did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator == (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDEqualityTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for QuaternionD comparison involving NaN values
        [Test]
        public void QuaternionDEqualsNanTest()
        {
            QuaternionD a = new QuaternionD(double.NaN, 0, 0, 0);
            QuaternionD b = new QuaternionD(0, double.NaN, 0, 0);
            QuaternionD c = new QuaternionD(0, 0, double.NaN, 0);
            QuaternionD d = new QuaternionD(0, 0, 0, double.NaN);

            Assert.False(a == new QuaternionD(0, 0, 0, 0));
            Assert.False(b == new QuaternionD(0, 0, 0, 0));
            Assert.False(c == new QuaternionD(0, 0, 0, 0));
            Assert.False(d == new QuaternionD(0, 0, 0, 0));

            Assert.True(a != new QuaternionD(0, 0, 0, 0));
            Assert.True(b != new QuaternionD(0, 0, 0, 0));
            Assert.True(c != new QuaternionD(0, 0, 0, 0));
            Assert.True(d != new QuaternionD(0, 0, 0, 0));

            Assert.False(a.Equals(new QuaternionD(0, 0, 0, 0)));
            Assert.False(b.Equals(new QuaternionD(0, 0, 0, 0)));
            Assert.False(c.Equals(new QuaternionD(0, 0, 0, 0)));
            Assert.False(d.Equals(new QuaternionD(0, 0, 0, 0)));

            Assert.False(a.IsIdentity);
            Assert.False(b.IsIdentity);
            Assert.False(c.IsIdentity);
            Assert.False(d.IsIdentity);

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        // A test for Equals (object)
        [Test]
        public void QuaternionDEqualsTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Equals (QuaternionD)
        [Test]
        public void QuaternionDEqualsTest1()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void QuaternionDFieldOffsetTest()
        {
            QuaternionD quat = new QuaternionD();

            double* basePtr = &quat.X; // Take address of first element
            QuaternionD* quatPtr = &quat; // Take address of whole QuaternionD

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(quatPtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&quat.X));
            Assert.AreEqual(new IntPtr(basePtr + 1), new IntPtr(&quat.Y));
            Assert.AreEqual(new IntPtr(basePtr + 2), new IntPtr(&quat.Z));
            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&quat.W));
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Convert Identity matrix test
        [Test]
        public void QuaternionDFromRotationMatrixTest1()
        {
            Matrix4x4D matrix = Matrix4x4D.Identity;

            QuaternionD expected = new QuaternionD(0.0f, 0.0f, 0.0f, 1.0f);
            QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.Equal(expected, actual),
                $"QuaternionD.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionD.CreateFromQuaternionD did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Convert X axis rotation matrix
        [Test]
        public void QuaternionDFromRotationMatrixTest2()
        {
            for (double angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4D matrix = Matrix4x4D.CreateRotationX(angle);

                QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);
                QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionD.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionD.CreateFromQuaternionD angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Convert Y axis rotation matrix
        [Test]
        public void QuaternionDFromRotationMatrixTest3()
        {
            for (double angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4D matrix = Matrix4x4D.CreateRotationY(angle);

                QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle);
                QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionD.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionD.CreateFromQuaternionD angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Convert Z axis rotation matrix
        [Test]
        public void QuaternionDFromRotationMatrixTest4()
        {
            for (double angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4D matrix = Matrix4x4D.CreateRotationZ(angle);

                QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle);
                QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionD.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionD.CreateFromQuaternionD angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Convert XYZ axis rotation matrix
        [Test]
        public void QuaternionDFromRotationMatrixTest5()
        {
            for (double angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4D matrix = Matrix4x4D.CreateRotationX(angle) * Matrix4x4D.CreateRotationY(angle) * Matrix4x4D.CreateRotationZ(angle);

                QuaternionD expected =
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle) *
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle) *
                    QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);

                QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionD.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionD.CreateFromQuaternionD angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // X axis is most large axis case
        [Test]
        public void QuaternionDFromRotationMatrixWithScaledMatrixTest1()
        {
            double angle = MathHelper.ToRadians(180.0f);
            Matrix4x4D matrix = Matrix4x4D.CreateRotationY(angle) * Matrix4x4D.CreateRotationZ(angle);

            QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle) * QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle);
            QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionD.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionD.CreateFromQuaternionD did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Y axis is most large axis case
        [Test]
        public void QuaternionDFromRotationMatrixWithScaledMatrixTest2()
        {
            double angle = MathHelper.ToRadians(180.0f);
            Matrix4x4D matrix = Matrix4x4D.CreateRotationX(angle) * Matrix4x4D.CreateRotationZ(angle);

            QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitZ, angle) * QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);
            QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionD.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionD.CreateFromQuaternionD did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4D)
        // Z axis is most large axis case
        [Test]
        public void QuaternionDFromRotationMatrixWithScaledMatrixTest3()
        {
            double angle = MathHelper.ToRadians(180.0f);
            Matrix4x4D matrix = Matrix4x4D.CreateRotationX(angle) * Matrix4x4D.CreateRotationY(angle);

            QuaternionD expected = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle) * QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);
            QuaternionD actual = QuaternionD.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionD.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4D m2 = Matrix4x4D.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionD.CreateFromQuaternionD did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for GetHashCode ()
        [Test]
        public void QuaternionDGetHashCodeTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

            int expected = unchecked(a.X.GetHashCode() + a.Y.GetHashCode() + a.Z.GetHashCode() + a.W.GetHashCode());
            int actual = a.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        // A test for Identity
        [Test]
        public void QuaternionDIdentityTest()
        {
            QuaternionD val = new QuaternionD(0, 0, 0, 1);
            Assert.AreEqual(val, QuaternionD.Identity);
        }

        // A test for operator != (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDInequalityTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Inverse (QuaternionD)
        [Test]
        public void QuaternionDInverseTest()
        {
            QuaternionD a = new QuaternionD(5.0, 6.0, 7.0, 8.0);

            QuaternionD expected = new QuaternionD(-0.028735632183908046, -0.034482758620689655, -0.040229885057471264, 0.04597701149425287);
            QuaternionD actual;

            actual = QuaternionD.Inverse(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Inverse (QuaternionD)
        // Invert zero length QuaternionD
        [Test]
        public void QuaternionDInverseTest1()
        {
            QuaternionD a = new QuaternionD();
            QuaternionD actual = QuaternionD.Inverse(a);

            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W)
                , $"QuaternionD.Inverse - did not return the expected value: expected {new QuaternionD(double.NaN, double.NaN, double.NaN, double.NaN)} actual {actual}");
        }

        // A test for IsIdentity
        [Test]
        public void QuaternionDIsIdentityTest()
        {
            Assert.True(QuaternionD.Identity.IsIdentity);
            Assert.True(new QuaternionD(0, 0, 0, 1).IsIdentity);
            Assert.False(new QuaternionD(1, 0, 0, 1).IsIdentity);
            Assert.False(new QuaternionD(0, 1, 0, 1).IsIdentity);
            Assert.False(new QuaternionD(0, 0, 1, 1).IsIdentity);
            Assert.False(new QuaternionD(0, 0, 0, 0).IsIdentity);
        }

        // A test for LengthSquared ()
        [Test]
        public void QuaternionDLengthSquaredTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);
            double w = 4.0f;

            QuaternionD target = new QuaternionD(v, w);

            double expected = 30.0f;
            double actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.LengthSquared did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Length ()
        [Test]
        public void QuaternionDLengthTest()
        {
            Vector3D v = new Vector3D(1.0f, 2.0f, 3.0f);

            double w = 4.0f;

            QuaternionD target = new QuaternionD(v, w);

            double expected = 5.477226f;
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Length did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionD, QuaternionD, double)
        [Test]
        public void QuaternionDLerpTest()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0f, 2.0f, 3.0f));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            double t = 0.5f;

            QuaternionD expected = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0f));
            QuaternionD actual;

            actual = QuaternionD.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Lerp did not return the expected value: expected {expected} actual {actual}");

            // Case a and b are same.
            expected = a;
            actual = QuaternionD.Lerp(a, a, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionD, QuaternionD, double)
        // Lerp test when t = 0
        [Test]
        public void QuaternionDLerpTest1()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0f, 2.0f, 3.0f));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            double t = 0.0f;

            QuaternionD expected = new QuaternionD(a.X, a.Y, a.Z, a.W);
            QuaternionD actual = QuaternionD.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionD, QuaternionD, double)
        // Lerp test when t = 1
        [Test]
        public void QuaternionDLerpTest2()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0f, 2.0f, 3.0f));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            double t = 1.0f;

            QuaternionD expected = new QuaternionD(b.X, b.Y, b.Z, b.W);
            QuaternionD actual = QuaternionD.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionD, QuaternionD, double)
        // Lerp test when the two QuaternionDs are more than 90 degree (dot product <0)
        [Test]
        public void QuaternionDLerpTest3()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0f, 2.0f, 3.0f));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionD b = QuaternionD.Negate(a);

            double t = 1.0f;

            QuaternionD actual = QuaternionD.Lerp(a, b, t);
            // Note that in QuaternionD world, Q == -Q. In the case of QuaternionDs dot product is zero,
            // one of the QuaternionD will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as QuaternionD b but flipped.
            Assert.True(actual == a, $"QuaternionD.Lerp did not return the expected value: expected {a} actual {actual}");
        }

        // A test for operator * (QuaternionD, double)
        [Test]
        public void QuaternionDMultiplyTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            double Testor = 0.5f;

            QuaternionD expected = new QuaternionD(0.5f, 1.0f, 1.5f, 2.0f);
            QuaternionD actual;

            actual = a * Testor;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator * did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator * (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDMultiplyTest1()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionD actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator * did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Multiply (QuaternionD, double)
        [Test]
        public void QuaternionDMultiplyTest2()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            double Testor = 0.5f;

            QuaternionD expected = new QuaternionD(0.5f, 1.0f, 1.5f, 2.0f);
            QuaternionD actual;

            actual = QuaternionD.Multiply(a, Testor);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Multiply did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Multiply (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDMultiplyTest3()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionD expected = new QuaternionD(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionD actual;

            actual = QuaternionD.Multiply(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Multiply did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Negate (QuaternionD)
        [Test]
        public void QuaternionDNegateTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionD expected = new QuaternionD(-1.0f, -2.0f, -3.0f, -4.0f);
            QuaternionD actual;

            actual = QuaternionD.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Normalize (QuaternionD)
        [Test]
        public void QuaternionDNormalizeTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionD expected = new QuaternionD(0.182574168f, 0.365148336f, 0.5477225f, 0.7302967f);
            QuaternionD actual;

            actual = QuaternionD.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Normalize did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Normalize (QuaternionD)
        // Normalize zero length QuaternionD
        [Test]
        public void QuaternionDNormalizeTest1()
        {
            QuaternionD a = new QuaternionD(0.0f, 0.0f, -0.0f, 0.0f);

            QuaternionD actual = QuaternionD.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W)
                , $"QuaternionD.Normalize did not return the expected value: expected {new QuaternionD(double.NaN, double.NaN, double.NaN, double.NaN)} actual {actual}");
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void QuaternionDSizeofTest()
        {
            Assert.AreEqual(32, sizeof(QuaternionD));
            Assert.AreEqual(64, sizeof(QuaternionD_2x));
            Assert.AreEqual(40, sizeof(QuaternionDPlusdouble));
            Assert.AreEqual(80, sizeof(QuaternionDPlusdouble_2x));
        }

        // A test for Slerp (QuaternionD, QuaternionD, double)
        [Test]
        public void QuaternionDSlerpTest()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.5;

            QuaternionD expected = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0));
            QuaternionD actual;

            actual = QuaternionD.Slerp(a, b, t);
            Assert.That(actual, Is.EqualTo(expected).Using<QuaternionD>((a, b) => a.Equals(b, 1e-15)), $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");

            // Case a and b are same.
            expected = a;
            actual = QuaternionD.Slerp(a, a, t);
            Assert.AreEqual(expected, actual, $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionD, QuaternionD, double)
        // Slerp test where t = 0
        [Test]
        public void QuaternionDSlerpTest1()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.0f;

            QuaternionD expected = new QuaternionD(a.X, a.Y, a.Z, a.W);
            QuaternionD actual = QuaternionD.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionD, QuaternionD, double)
        // Slerp test where t = 1
        [Test]
        public void QuaternionDSlerpTest2()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            QuaternionD b = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 1.0f;

            QuaternionD expected = new QuaternionD(b.X, b.Y, b.Z, b.W);
            QuaternionD actual = QuaternionD.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionD, QuaternionD, double)
        // Slerp test where dot product is < 0
        [Test]
        public void QuaternionDSlerpTest3()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            QuaternionD b = -a;

            double t = 1.0;

            QuaternionD expected = a;
            QuaternionD actual = QuaternionD.Slerp(a, b, t);
            // Note that in QuaternionD world, Q == -Q. In the case of QuaternionDs dot product is zero,
            // one of the QuaternionD will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as QuaternionD b but flipped.
            Assert.That(actual, Is.EqualTo(expected).Using<QuaternionD>((a, b) => a.Equals(b, 1e-15)), $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionD, QuaternionD, double)
        // Slerp test where the QuaternionD is flipped
        [Test]
        public void QuaternionDSlerpTest4()
        {
            Vector3D axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            QuaternionD a = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            QuaternionD b = -QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.0;

            QuaternionD expected = new QuaternionD(a.X, a.Y, a.Z, a.W);
            QuaternionD actual = QuaternionD.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator - (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDSubtractionTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 6.0f, 7.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 2.0f, 3.0f, 8.0f);

            QuaternionD expected = new QuaternionD(-4.0f, 4.0f, 4.0f, -4.0f);
            QuaternionD actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator - did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Subtract (QuaternionD, QuaternionD)
        [Test]
        public void QuaternionDSubtractTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 6.0f, 7.0f, 4.0f);
            QuaternionD b = new QuaternionD(5.0f, 2.0f, 3.0f, 8.0f);

            QuaternionD expected = new QuaternionD(-4.0f, 4.0f, 4.0f, -4.0f);
            QuaternionD actual;

            actual = QuaternionD.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for ToString ()
        [Test]
        public void QuaternionDToStringTest()
        {
            QuaternionD target = new QuaternionD(-1.0, 2.2, 3.3, -4.4);

            string expected = string.Format(CultureInfo.CurrentCulture
                , "{{X:{0} Y:{1} Z:{2} W:{3}}}"
                , -1.0, 2.2, 3.3, -4.4);

            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        // A test for operator - (QuaternionD)
        [Test]
        public void QuaternionDUnaryNegationTest()
        {
            QuaternionD a = new QuaternionD(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionD expected = new QuaternionD(-1.0f, -2.0f, -3.0f, -4.0f);
            QuaternionD actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionD.operator - did not return the expected value: expected {expected} actual {actual}");
        }

        #endregion Public Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct QuaternionD_2x
        {
            private QuaternionD _a;
            private QuaternionD _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct QuaternionDPlusdouble
        {
            private QuaternionD _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct QuaternionDPlusdouble_2x
        {
            private QuaternionDPlusdouble _a;
            private QuaternionDPlusdouble _b;
        }

        #endregion Private Structs
    }
}