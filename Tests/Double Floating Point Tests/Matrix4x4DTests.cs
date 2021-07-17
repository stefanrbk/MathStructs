using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Matrix4x4DTests
    {
        [Test]
        public void ConstructorTest()
        {
            var actual = new Matrix4x4D( 1,  2,  3,  4,
                                         5,  6,  7,  8,
                                         9, 10, 11, 12,
                                        13, 14, 15, 16);
            var expected = GenerateIncrementalMatrixNumber();

            Assert.That(actual.M11, Is.EqualTo(expected.M11));
            Assert.That(actual.M12, Is.EqualTo(expected.M12));
            Assert.That(actual.M13, Is.EqualTo(expected.M13));
            Assert.That(actual.M14, Is.EqualTo(expected.M14));
            Assert.That(actual.M21, Is.EqualTo(expected.M21));
            Assert.That(actual.M22, Is.EqualTo(expected.M22));
            Assert.That(actual.M23, Is.EqualTo(expected.M23));
            Assert.That(actual.M24, Is.EqualTo(expected.M24));
            Assert.That(actual.M31, Is.EqualTo(expected.M31));
            Assert.That(actual.M32, Is.EqualTo(expected.M32));
            Assert.That(actual.M33, Is.EqualTo(expected.M33));
            Assert.That(actual.M34, Is.EqualTo(expected.M34));
            Assert.That(actual.M41, Is.EqualTo(expected.M41));
            Assert.That(actual.M42, Is.EqualTo(expected.M42));
            Assert.That(actual.M43, Is.EqualTo(expected.M43));
            Assert.That(actual.M44, Is.EqualTo(expected.M44));
        }

        [Test]
        public void ConstructorTest1()
        {
            var expected = new Matrix3x2D(1, 2, 3, 4, 5, 6);
            var actual = new Matrix4x4D(expected);

            Assert.That(actual.M11, Is.EqualTo(expected.M11));
            Assert.That(actual.M12, Is.EqualTo(expected.M12));
            Assert.That(actual.M13, Is.EqualTo(0.0));
            Assert.That(actual.M14, Is.EqualTo(0.0));
            Assert.That(actual.M21, Is.EqualTo(expected.M21));
            Assert.That(actual.M22, Is.EqualTo(expected.M22));
            Assert.That(actual.M23, Is.EqualTo(0.0));
            Assert.That(actual.M24, Is.EqualTo(0.0));
            Assert.That(actual.M31, Is.EqualTo(0.0));
            Assert.That(actual.M32, Is.EqualTo(0.0));
            Assert.That(actual.M33, Is.EqualTo(1.0));
            Assert.That(actual.M34, Is.EqualTo(0.0));
            Assert.That(actual.M41, Is.EqualTo(expected.M31));
            Assert.That(actual.M42, Is.EqualTo(expected.M32));
            Assert.That(actual.M43, Is.EqualTo(0.0));
            Assert.That(actual.M44, Is.EqualTo(1.0));
        }

        [Test]
        public void ConstructorTest2()
        {
            var expected = new Matrix3x3D(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var actual = new Matrix4x4D(expected);

            Assert.That(actual.M11, Is.EqualTo(expected.M11));
            Assert.That(actual.M12, Is.EqualTo(expected.M12));
            Assert.That(actual.M13, Is.EqualTo(expected.M13));
            Assert.That(actual.M14, Is.EqualTo(0.0));
            Assert.That(actual.M21, Is.EqualTo(expected.M21));
            Assert.That(actual.M22, Is.EqualTo(expected.M22));
            Assert.That(actual.M23, Is.EqualTo(expected.M23));
            Assert.That(actual.M24, Is.EqualTo(0.0));
            Assert.That(actual.M31, Is.EqualTo(expected.M31));
            Assert.That(actual.M32, Is.EqualTo(expected.M32));
            Assert.That(actual.M33, Is.EqualTo(expected.M33));
            Assert.That(actual.M34, Is.EqualTo(0.0));
            Assert.That(actual.M41, Is.EqualTo(0.0));
            Assert.That(actual.M42, Is.EqualTo(0.0));
            Assert.That(actual.M43, Is.EqualTo(0.0));
            Assert.That(actual.M44, Is.EqualTo(1.0));
        }

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new double[] {4.0, 7.0, 2.0, 9.0,
                                      12.0, 0.0, 3.0, 8.0,
                                      6.0, 5.0, 10.0, 15.0,
                                      14.0, 13.0, 11.0, 1.0}).AsSpan();
            var vec = span.ToMatrix4x4D();

            Assert.That(vec.M11, Is.EqualTo(span[0]));
            Assert.That(vec.M12, Is.EqualTo(span[1]));
            Assert.That(vec.M13, Is.EqualTo(span[2]));
            Assert.That(vec.M14, Is.EqualTo(span[3]));
            Assert.That(vec.M21, Is.EqualTo(span[4]));
            Assert.That(vec.M22, Is.EqualTo(span[5]));
            Assert.That(vec.M23, Is.EqualTo(span[6]));
            Assert.That(vec.M24, Is.EqualTo(span[7]));
            Assert.That(vec.M31, Is.EqualTo(span[8]));
            Assert.That(vec.M32, Is.EqualTo(span[9]));
            Assert.That(vec.M33, Is.EqualTo(span[10]));
            Assert.That(vec.M34, Is.EqualTo(span[11]));
            Assert.That(vec.M41, Is.EqualTo(span[12]));
            Assert.That(vec.M42, Is.EqualTo(span[13]));
            Assert.That(vec.M43, Is.EqualTo(span[14]));
            Assert.That(vec.M44, Is.EqualTo(span[15]));
        }

        public static IEnumerable<(Vector3D, Vector3D, Matrix4x4D)> CreateBillboardTest()
        {            
            // Object placed at Forward side of camera on XZ-Plane.
            // Result must be same as 180 degrees rotate along Y-axis.
            yield return (new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.Rad180D));

            // Object placed at Backward side of camera on XZ-Plane.
            // This result must be same as 0 degrees rotate along Y-axis.
            yield return (new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(0.0));

            // Place object at Right side of camera on XZ-Plane.
            // This result must be same as 90 degrees rotate along Y-axis.
            yield return (new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.Rad90D));

            // Place object at Left side of camera on XZ-Plane.
            // This result must be same as -90 degrees rotate along Y-axis.
            yield return (new Vector3D(-1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(-MathHelper.Rad90D));

            // Place object at Up side of camera on XY-Plane.
            // Result must be same as 180 degrees rotate along Z-axis after 90 degrees rotate along X-axis.
            yield return (new Vector3D(0, 1, 0), new Vector3D(0, 0, 1), Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(MathHelper.Rad180D));

            // Place object at Down side of camera on XY-Plane.
            // Result must be same as 0 degrees rotate along Z-axis after 90 degrees rotate along X-axis.
            yield return (new Vector3D(0, -1, 0), new Vector3D(0, 0, 1), Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(0.0));

            // Place object at Right side of camera on XY-Plane.
            // Result must be same as 90 degrees rotate along Z-axis after 90 degrees rotate along X-axis.
            yield return (new Vector3D(1, 0, 0), new Vector3D(0, 0, 1), Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(MathHelper.Rad90D));

            // Place object at Left side of camera on XY-Plane.
            // Result must be same as -90 degrees rotate along Z-axis after 90 degrees rotate along X-axis.
            yield return (new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1), Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(-MathHelper.Rad90D));

            // Place object at Up side of camera on YZ-Plane.
            // Result must be same as -90 degrees rotate along X-axis after 90 degrees rotate along Z-axis.
            yield return (new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0), Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(-MathHelper.Rad90D));

            // Place object at Down side of camera on YZ-Plane.
            // Result must be same as 90 degrees rotate along X-axis after 90 degrees rotate along Z-axis.
            yield return (new Vector3D(0, -1, 0), new Vector3D(-1, 0, 0), Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(MathHelper.Rad90D));

            // Place object at Forward side of camera on YZ-Plane.
            // Result must be same as 180 degrees rotate along X-axis after 90 degrees rotate along Z-axis.
            yield return (new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0), Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(MathHelper.Rad180D));

            // Place object at Backward side of camera on YZ-Plane.
            // Result must be same as 0 degrees rotate along X-axis after 90 degrees rotate along Z-axis.
            yield return (new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0), Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(0.0));
        }

        public static IEnumerable<(double, Vector3D)> CreateBillboardTooCloseTest()
        {
            yield return (MathHelper.Rad180D, new Vector3D(0, 0, 1));
            yield return (-MathHelper.Rad90D, new Vector3D(1, 0, 0));
        }

        public static IEnumerable<(Vector3D, double, double, double, Vector3D)> CreateConstrainedBillboardAlongAxisTest()
        {
            // Angle between rotateAxis and camera to object vector is too small. And user passed correct objectForwardVector parameter.
            yield return(new Vector3D(0, 1, 0), 0.0, MathHelper.Rad180D, 0.0, new Vector3D(0, 0, -1));

            // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
            yield return(new Vector3D(0, 1, 0), 0.0, MathHelper.Rad180D, 0.0, new Vector3D(0, 1, 0));
            
            // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
            yield return(new Vector3D(0, 0, -1), -MathHelper.Rad90D, 0.0, -MathHelper.Rad90D, new Vector3D(0, 0, -1));
        }

        public static IEnumerable<(Vector3D, Vector3D, Matrix4x4D)> CreateConstrainedBillboardTest()
        {
            // Object placed at Forward side of camera on XZ-Plane. result must be same as 180 degrees rotate along y-axis.
            yield return (new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.Rad180D));

            // Object placed at Backward side of camera on XZ-Plane. This result must be same as 0 degrees rotate along y-axis.
            yield return (new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(0.0));

            // Place object at Right side of camera on XZ-Plane. This result must be same as 90 degrees rotate along y-axis.
            yield return (new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(MathHelper.Rad90D));
            
            // Place object at Left side of camera on XZ-Plane. This result must be same as -90 degrees rotate along y-axis.
            yield return (new Vector3D(-1, 0, 0), new Vector3D(0, 1, 0), Matrix4x4D.CreateRotationY(-MathHelper.Rad90D));

            // Place object at Up side of camera on XY-Plane. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            yield return (new Vector3D(0, 1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(MathHelper.Rad180D));

            // Place object at Down side of camera on XY-Plane. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            yield return (new Vector3D(0, -1, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(0.0));

            // Place object at Right side of camera on XY-Plane. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            yield return (new Vector3D(1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(MathHelper.Rad90D));

            // Place object at Left side of camera on XY-Plane. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            yield return (new Vector3D(-1, 0, 0), new Vector3D(0, 0, 1),
                Matrix4x4D.CreateRotationX(MathHelper.Rad90D) * Matrix4x4D.CreateRotationZ(-MathHelper.Rad90D));

            // Place object at Up side of camera on YZ-Plane. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            yield return (new Vector3D(0, 1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(-MathHelper.Rad90D));

            // Place object at Down side of camera on YZ-Plane. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            yield return (new Vector3D(0, -1, 0), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(MathHelper.Rad90D));

            // Place object at Forward side of camera on YZ-Plane. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            yield return (new Vector3D(0, 0, -1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(MathHelper.Rad180D));

            // Place object at Backward side of camera on YZ-Plane. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            yield return (new Vector3D(0, 0, 1), new Vector3D(-1, 0, 0),
                Matrix4x4D.CreateRotationZ(MathHelper.Rad90D) * Matrix4x4D.CreateRotationX(0.0));
        }

        #region Public Methods

        // A test for operator + (Matrix4x4D, Matrix4x4D)
        [Test, Category("Matrix4x4D"), Category("op_Add")]
        public void Matrix4x4DAdditionTest()
        {
            var a = GenerateIncrementalMatrixNumber();
            var b = GenerateIncrementalMatrixNumber(-8.0);

            var expected = new Matrix4x4D
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

            var actual = a + b;
            Assert.AreEqual(actual, expected, "Matrix4x4D.operator + did not return the expected value.");
        }

        // A test for Add (Matrix4x4D, Matrix4x4D)
        [Test, Category("Matrix4x4D"), Category("Add")]
        public void Matrix4x4DAddTest()
        {
            var a = GenerateIncrementalMatrixNumber();
            var b = GenerateIncrementalMatrixNumber(-8.0);

            var expected = new Matrix4x4D
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

            var actual = Matrix4x4D.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // Tests for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D)
        [TestCaseSource("CreateBillboardTest", Category = "Matrix4x4D,CreateBillboard")]
        public void Matrix4x4DCreateBillboardTest((Vector3D, Vector3D, Matrix4x4D) input)
        {
            (var placeDirection, var cameraUpVector, var expectedRotation) = input;
            var cameraPosition = new Vector3D(3.0, 4.0, 5.0);
            var objectPosition = cameraPosition + (placeDirection * 10.0);
            var expected = expectedRotation * Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateBillboard did not return the expected value.");
        }

        // A test for CreateBillboard (Vector3D, Vector3D, Vector3D, Vector3D)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [TestCaseSource("CreateBillboardTooCloseTest", Category = "Matrix4x4D,CreateBillboard")]
        public void Matrix4x4DCreateBillboardTooCloseTest((double, Vector3D) input)
        {
            (var degrees, var vector) = input;
            var objectPosition = new Vector3D(3.0, 4.0, 5.0);
            var cameraUpVector = new Vector3D(0, 1, 0);

            // Doesn't pass camera face direction. CreateBillboard uses new Vector3D(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            var expected = Matrix4x4D.CreateRotationY(degrees) * Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateBillboard(objectPosition, objectPosition, cameraUpVector, vector);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D)
        [TestCaseSource("CreateConstrainedBillboardAlongAxisTest", Category = "Matrix4x4D,CreateConstrainedBillboard")]
        public void Matrix4x4DCreateConstrainedBillboardAlongAxisTest((Vector3D, double, double, double, Vector3D) input)
        {
            (var rotateAxis, var x, var y, var z, var objectForwardVector) = input;
            // Place camera at up side of object.
            var objectPosition = new Vector3D(3.0, 4.0, 5.0);
            var cameraPosition = objectPosition + (rotateAxis * 10.0);

            var expected = Matrix4x4D.CreateRotationX(x) *
                           Matrix4x4D.CreateRotationY(y) *
                           Matrix4x4D.CreateRotationZ(z) *
                           Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), objectForwardVector);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D)
        [TestCaseSource("CreateConstrainedBillboardTest", Category = "Matrix4x4D,CreateConstrainedBillboard")]
        public static void Matrix4x4DCreateConstrainedBillboardTest((Vector3D, Vector3D, Matrix4x4D) input)
        {
            (var placeDirection, var rotateAxis, var expectedRotation) = input;
            var cameraPosition = new Vector3D(3.0, 4.0, 5.0);
            var objectPosition = cameraPosition + (placeDirection * 10.0);
            var expected = expectedRotation * Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");

            // When you move camera along rotateAxis, result must be same.
            cameraPosition += rotateAxis * 10.0;
            actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");

            cameraPosition -= rotateAxis * 30.0;
            actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3D(0, 0, -1), new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest1()
        {
            var objectPosition = new Vector3D(3.0, 4.0, 5.0);
            var cameraPosition = objectPosition;
            var cameraUpVector = new Vector3D(0, 1, 0);

            // Doesn't pass camera face direction. CreateConstrainedBillboard uses new Vector3D(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            var expected = Matrix4x4D.CreateRotationY(MathHelper.Rad180D) * Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(0, 0, 1), new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3D, Vector3D, Vector3D, Vector3D?)
        // Object and camera positions are too close and passed cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest2()
        {
            var objectPosition = new Vector3D(3.0, 4.0, 5.0);
            var cameraPosition = objectPosition;
            var cameraUpVector = new Vector3D(0, 1, 0);

            // Passes Vector3D.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
            var expected = Matrix4x4D.CreateRotationY(-MathHelper.Rad90D) * Matrix4x4D.CreateTranslation(objectPosition);
            var actual = Matrix4x4D.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3D(1, 0, 0), new Vector3D(0, 0, -1));
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateFromAxisAngle(Vector3D,double)
        [Test]
        public void Matrix4x4CreateFromAxisAngleTest()
        {
            var radians = MathHelper.ToRadians(-30.0);

            var expected = Matrix4x4D.CreateRotationX(radians);
            var actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));

            expected = Matrix4x4D.CreateRotationY(radians);
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));

            expected = Matrix4x4D.CreateRotationZ(radians);
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));

            expected = Matrix4x4D.CreateFromQuaternion(QuaternionD.CreateFromAxisAngle(Vector3D.Normalize(Vector3D.One), radians));
            actual = Matrix4x4D.CreateFromAxisAngle(Vector3D.Normalize(Vector3D.One), radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));

            const int rotCount = 16;
            for (var i = 0; i < rotCount; ++i)
            {
                var latitude = 2.0 * MathHelper.PiF * (i / (double)rotCount);
                for (var j = 0; j < rotCount; ++j)
                {
                    var longitude = -MathHelper.PiOver2F + (MathHelper.PiF * (j / (double)rotCount));

                    var m = Matrix4x4D.CreateRotationZ(longitude) * Matrix4x4D.CreateRotationY(latitude);
                    var axis = new Vector3D(m.M11, m.M12, m.M13);
                    for (var k = 0; k < rotCount; ++k)
                    {
                        var rot = 2.0 * MathHelper.PiF * (k / (double)rotCount);
                        expected = Matrix4x4D.CreateFromQuaternion(QuaternionD.CreateFromAxisAngle(axis, rot));
                        actual = Matrix4x4D.CreateFromAxisAngle(axis, rot);
                        Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));
                    }
                }
            }
        }

        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest1()
        {
            var yawAngle   = MathHelper.ToRadians(30.0);
            var pitchAngle = MathHelper.ToRadians(40.0);
            var rollAngle  = MathHelper.ToRadians(50.0);

            var yaw   = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, yawAngle);
            var pitch = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, pitchAngle);
            var roll  = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, rollAngle);

            var expected = roll * pitch * yaw;
            var actual = Matrix4x4D.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // Covers more numeric regions
        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest2()
        {
            const double step = 35.0;

            for (var yawAngle = -720.0; yawAngle <= 720.0; yawAngle += step)
            {
                for (var pitchAngle = -720.0; pitchAngle <= 720.0; pitchAngle += step)
                {
                    for (var rollAngle = -720.0; rollAngle <= 720.0; rollAngle += step)
                    {
                        var yawRad   = MathHelper.ToRadians(yawAngle);
                        var pitchRad = MathHelper.ToRadians(pitchAngle);
                        var rollRad  = MathHelper.ToRadians(rollAngle);
                        var yaw   = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitY, yawRad);
                        var pitch = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitX, pitchRad);
                        var roll  = Matrix4x4D.CreateFromAxisAngle(Vector3D.UnitZ, rollRad);

                        var expected = roll * pitch * yaw;
                        var actual = Matrix4x4D.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), String.Format("Yaw:{0} Pitch:{1} Roll:{2}", yawAngle, pitchAngle, rollAngle));
                    }
                }
            }
        }

        // A test for CrateLookAt (Vector3D, Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateLookAtTest()
        {
            var cameraPosition = new Vector3D(10.0, 20.0, 30.0);
            var cameraTarget = new Vector3D(3.0, 2.0, -4.0);
            var cameraUpVector = new Vector3D(0.0, 1.0, 0.0);

            var expected = new Matrix4x4D
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

            var actual = Matrix4x4D.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateLookAt did not return the expected value.");
        }

        // A test for CreateOrthoOffCenter (double, double, double, double, double, double)
        [Test]
        public void Matrix4x4CreateOrthoOffCenterTest()
        {
            var left = 10.0;
            var right = 90.0;
            var bottom = 20.0;
            var top = 180.0;
            var zNearPlane = 1.5;
            var zFarPlane = 1000.0;

            var expected = new Matrix4x4D
            {
                M11 = 0.025,
                M22 = 0.0125,
                M33 = -0.00100150227,
                M41 = -1.25,
                M42 = -1.25,
                M43 = -0.00150225335,
                M44 = 1.0
            };

            var actual= Matrix4x4D.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateOrthoOffCenter did not return the expected value.");
        }

        // A test for CreateOrtho (double, double, double, double)
        [Test]
        public void Matrix4x4CreateOrthoTest()
        {
            var width = 100.0;
            var height = 200.0;
            var zNearPlane = 1.5;
            var zFarPlane = 1000.0;

            var expected = new Matrix4x4D
            {
                M11 = 0.02,
                M22 = 0.01,
                M33 = -0.00100150227,
                M43 = -0.00150225335,
                M44 = 1.0
            };

            var actual = Matrix4x4D.CreateOrthographic(width, height, zNearPlane, zFarPlane);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateOrtho did not return the expected value.");
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest()
        {
            var fieldOfView = MathHelper.ToRadians(30.0);
            var aspectRatio = 1280.0 / 720.0;
            var zNearPlane = 1.5;
            var zFarPlane = 1000.0;

            var expected = new Matrix4x4D
            {
                M11 = 2.09927845,
                M22 = 3.73205066,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };

            var actual = Matrix4x4D.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, zNearPlane, zFarPlane);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreatePerspectiveFieldOfView did not return the expected value.");
        }

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where filedOfView is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest1() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspectiveFieldOfView(-1, 1, 1, 10));

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where filedOfView is more than pi.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest2() =>
            _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
                    Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiD + 0.01, 1, 1, 10));

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest3() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4D, 1, -1, 10));

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where farPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest4() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4D, 1, 1, -10));

        // A test for CreatePerspectiveFieldOfView (double, double, double, double)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest5() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspectiveFieldOfView(MathHelper.PiOver4D, 1, 10, 1));

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest()
        {
            var left = 10.0;
            var right = 90.0;
            var bottom = 20.0;
            var top = 180.0;
            var zNearPlane = 1.5;
            var zFarPlane = 1000.0;

            var expected = new Matrix4x4D
            {
                M11 = 0.0375,
                M22 = 0.01875,
                M31 = 1.25,
                M32 = 1.25,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };

            var actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreatePerspectiveOffCenter did not return the expected value.");
        }

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where nearPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest1() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0, right = 90.0, bottom = 20.0, top = 180.0;
                var actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, -1, 10);
            });

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where farPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest2() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0, right = 90.0, bottom = 20.0, top = 180.0;
                var actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, 1, -10);
            });

        // A test for CreatePerspectiveOffCenter (double, double, double, double, double, double)
        // CreatePerspectiveOffCenter test where test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest3() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                double left = 10.0, right = 90.0, bottom = 20.0, top = 180.0;
                var actual = Matrix4x4D.CreatePerspectiveOffCenter(left, right, bottom, top, 10, 1);
            });

        // A test for CreatePerspective (double, double, double, double)
        [Test]
        public void Matrix4x4CreatePerspectiveTest()
        {
            var width = 100.0;
            var height = 200.0;
            var zNearPlane = 1.5;
            var zFarPlane = 1000.0;

            var expected = new Matrix4x4D
            {
                M11 = 0.03,
                M22 = 0.015,
                M33 = -1.00150228,
                M34 = -1.0,
                M43 = -1.50225341
            };

            var actual = Matrix4x4D.CreatePerspective(width, height, zNearPlane, zFarPlane);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreatePerspective did not return the expected value.");
        }

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where znear = zfar
        [Test]
        public void Matrix4x4CreatePerspectiveTest1() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var width = 100.0;
                var height = 200.0;
                var zNearPlane = 0.0;
                var zFarPlane = 0.0;

                var actual = Matrix4x4D.CreatePerspective(width, height, zNearPlane, zFarPlane);
            });

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where near PlaneD is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest2() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspective(10, 10, -10, 10));

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where far PlaneD is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest3() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspective(10, 10, 10, -10));

        // A test for CreatePerspective (double, double, double, double)
        // CreatePerspective test where near PlaneD is beyond far PlaneD
        [Test]
        public void Matrix4x4CreatePerspectiveTest4() =>
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Matrix4x4D.CreatePerspective(10, 10, 10, 1));

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

            foreach (var p in planes)
            {
                var planeD = PlaneD.Normalize(p);
                var m = Matrix4x4D.CreateReflection(planeD);
                var pp = -planeD.D * planeD.Normal; // Position on the PlaneD.

                //
                foreach (var point in points)
                {
                    var rp = Vector3D.Transform(point, m);

                    // Manually compute reflection point and compare results.
                    var v = point - pp;
                    var d = Vector3D.Dot(v, planeD.Normal);
                    var vp = point - (2.0 * d * planeD.Normal);
                    Assert.That(vp, Is.EqualTo(rp).Using<Vector3D>(MathHelper.Equal), "Matrix4x4D.Reflection did not provide expected value.");
                }
            }
        }

        // A test for CreateRotationX (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationXCenterTest()
        {
            var radians = MathHelper.ToRadians(30.0);
            var center = new Vector3D(23, 42, 66);

            var rotateAroundZero = Matrix4x4D.CreateRotationX(radians, Vector3D.Zero);
            var rotateAroundZeroExpected = Matrix4x4D.CreateRotationX(radians);
            Assert.That(rotateAroundZero, Is.EqualTo(rotateAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var rotateAroundCenter = Matrix4x4D.CreateRotationX(radians, center);
            var rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationX(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.That(rotateAroundCenter, Is.EqualTo(rotateAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateRotationX (double)
        [Test]
        public void Matrix4x4CreateRotationXTest()
        {
            var radians = MathHelper.ToRadians(30.0);

            var expected = new Matrix4x4D
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationX (double)
        // CreateRotationX of zero degree
        [Test]
        public void Matrix4x4CreateRotationXTest1()
        {
            double radians = 0;

            var expected = Matrix4x4D.Identity;
            var actual = Matrix4x4D.CreateRotationX(radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationY (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationYCenterTest()
        {
            double radians = MathHelper.ToRadians(30.0f);
            var center = new Vector3D(23, 42, 66);

            var rotateAroundZero = Matrix4x4D.CreateRotationY(radians, Vector3D.Zero);
            var rotateAroundZeroExpected = Matrix4x4D.CreateRotationY(radians);
            Assert.That(rotateAroundZero, Is.EqualTo(rotateAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var rotateAroundCenter = Matrix4x4D.CreateRotationY(radians, center);
            var rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationY(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.That(rotateAroundCenter, Is.EqualTo(rotateAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateRotationY (double)
        [Test]
        public void Matrix4x4CreateRotationYTest()
        {
            var radians = MathHelper.ToRadians(60.0);

            var expected = new Matrix4x4D
            {
                M11 = 0.49999997,
                M13 = -0.866025448,
                M22 = 1.0,
                M31 = 0.866025448,
                M33 = 0.49999997,
                M44 = 1.0
            };

            var actual = Matrix4x4D.CreateRotationY(radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateRotationY did not return the expected value.");
        }

        // A test for RotationY (double)
        // CreateRotationY test for negative angle
        [Test]
        public void Matrix4x4CreateRotationYTest1()
        {
            var radians = MathHelper.ToRadians(-300.0);

            var expected = new Matrix4x4D
            {
                M11 = 0.49999997,
                M13 = -0.866025448,
                M22 = 1.0,
                M31 = 0.866025448,
                M33 = 0.49999997,
                M44 = 1.0
            };

            var actual = Matrix4x4D.CreateRotationY(radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateRotationY did not return the expected value.");
        }

        // A test for CreateRotationZ (double, Vector3D)
        [Test]
        public void Matrix4x4CreateRotationZCenterTest()
        {
            var radians = MathHelper.ToRadians(30.0);
            var center = new Vector3D(23, 42, 66);

            var rotateAroundZero = Matrix4x4D.CreateRotationZ(radians, Vector3D.Zero);
            var rotateAroundZeroExpected = Matrix4x4D.CreateRotationZ(radians);
            Assert.That(rotateAroundZero, Is.EqualTo(rotateAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var rotateAroundCenter = Matrix4x4D.CreateRotationZ(radians, center);
            var rotateAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateRotationZ(radians) * Matrix4x4D.CreateTranslation(center);
            Assert.That(rotateAroundCenter, Is.EqualTo(rotateAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateRotationZ (double)
        [Test]
        public void Matrix4x4CreateRotationZTest()
        {
            var radians = MathHelper.ToRadians(50.0);

            var expected = new Matrix4x4D
            {
                M11 = 0.642787635,
                M12 = 0.766044438,
                M21 = -0.766044438,
                M22 = 0.642787635,
                M33 = 1.0,
                M44 = 1.0
            };

            var actual = Matrix4x4D.CreateRotationZ(radians);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateRotationZ did not return the expected value.");
        }

        // A test for CreateScale (Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest1()
        {
            var scale = new Vector3D(3, 4, 5);
            var center = new Vector3D(23, 42, 666);

            var scaleAroundZero = Matrix4x4D.CreateScale(scale, Vector3D.Zero);
            var scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale);
            Assert.That(scaleAroundZero, Is.EqualTo(scaleAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var scaleAroundCenter = Matrix4x4D.CreateScale(scale, center);
            var scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale) * Matrix4x4D.CreateTranslation(center);
            Assert.That(scaleAroundCenter, Is.EqualTo(scaleAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateScale (double, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest2()
        {
            var scale = 5.0;
            var center = new Vector3D(23, 42, 666);

            var scaleAroundZero = Matrix4x4D.CreateScale(scale, Vector3D.Zero);
            var scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale);
            Assert.That(scaleAroundZero, Is.EqualTo(scaleAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var scaleAroundCenter = Matrix4x4D.CreateScale(scale, center);
            var scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale) * Matrix4x4D.CreateTranslation(center);
            Assert.That(scaleAroundCenter, Is.EqualTo(scaleAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateScale (double, double, double, Vector3D)
        [Test]
        public void Matrix4x4CreateScaleCenterTest3()
        {
            var scale = new Vector3D(3, 4, 5);
            var center = new Vector3D(23, 42, 666);

            var scaleAroundZero = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z, Vector3D.Zero);
            var scaleAroundZeroExpected = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z);
            Assert.That(scaleAroundZero, Is.EqualTo(scaleAroundZeroExpected).Using<Matrix4x4D>(MathHelper.Equal));

            var scaleAroundCenter = Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z, center);
            var scaleAroundCenterExpected = Matrix4x4D.CreateTranslation(-center) * Matrix4x4D.CreateScale(scale.X, scale.Y, scale.Z) * Matrix4x4D.CreateTranslation(center);
            Assert.That(scaleAroundCenter, Is.EqualTo(scaleAroundCenterExpected).Using<Matrix4x4D>(MathHelper.Equal));
        }

        // A test for CreateScale (Vector3D)
        [Test]
        public void Matrix4x4CreateScaleTest1()
        {
            var scales = new Vector3D(2.0, 3.0, 4.0);
            var expected = new Matrix4x4D(
                2.0, 0.0, 0.0, 0.0,
                0.0, 3.0, 0.0, 0.0,
                0.0, 0.0, 4.0, 0.0,
                0.0, 0.0, 0.0, 1.0);
            var actual = Matrix4x4D.CreateScale(scales);
            Assert.That(actual, Is.EqualTo(expected));
        }

        // A test for CreateScale (double)
        [Test]
        public void Matrix4x4CreateScaleTest2()
        {
            var scale = 2.0;
            var expected = new Matrix4x4D(
                2.0, 0.0, 0.0, 0.0,
                0.0, 2.0, 0.0, 0.0,
                0.0, 0.0, 2.0, 0.0,
                0.0, 0.0, 0.0, 1.0);
            var actual = Matrix4x4D.CreateScale(scale);
            Assert.That(actual, Is.EqualTo(expected));
        }

        // A test for CreateScale (double, double, double)
        [Test]
        public void Matrix4x4CreateScaleTest3()
        {
            var xScale = 2.0;
            var yScale = 3.0;
            var zScale = 4.0;
            var expected = new Matrix4x4D(
                2.0, 0.0, 0.0, 0.0,
                0.0, 3.0, 0.0, 0.0,
                0.0, 0.0, 4.0, 0.0,
                0.0, 0.0, 0.0, 1.0);
            var actual = Matrix4x4D.CreateScale(xScale, yScale, zScale);
            Assert.That(actual, Is.EqualTo(expected));
        }

        // Simple shadow test.
        [Test]
        public void Matrix4x4CreateShadowTest01()
        {
            var lightDir = Vector3D.UnitY;
            var PlaneD = new PlaneD(Vector3D.UnitY, 0);

            var expected = Matrix4x4D.CreateScale(1, 0, 1);

            var actual = Matrix4x4D.CreateShadow(lightDir, PlaneD);
            Assert.That(actual, Is.EqualTo(expected), "Matrix4x4D.CreateShadow did not returned expected value.");
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

            foreach (var p in planes)
            {
                var planeD = PlaneD.Normalize(p);

                // Try various direction of light directions.
                var testDirections = new Vector3D[]
                {
                    new Vector3D( -1.0, 1.0, 1.0 ),
                    new Vector3D(  0.0, 1.0, 1.0 ),
                    new Vector3D(  1.0, 1.0, 1.0 ),
                    new Vector3D( -1.0, 0.0, 1.0 ),
                    new Vector3D(  0.0, 0.0, 1.0 ),
                    new Vector3D(  1.0, 0.0, 1.0 ),
                    new Vector3D( -1.0,-1.0, 1.0 ),
                    new Vector3D(  0.0,-1.0, 1.0 ),
                    new Vector3D(  1.0,-1.0, 1.0 ),

                    new Vector3D( -1.0, 1.0, 0.0 ),
                    new Vector3D(  0.0, 1.0, 0.0 ),
                    new Vector3D(  1.0, 1.0, 0.0 ),
                    new Vector3D( -1.0, 0.0, 0.0 ),
                    new Vector3D(  0.0, 0.0, 0.0 ),
                    new Vector3D(  1.0, 0.0, 0.0 ),
                    new Vector3D( -1.0,-1.0, 0.0 ),
                    new Vector3D(  0.0,-1.0, 0.0 ),
                    new Vector3D(  1.0,-1.0, 0.0 ),

                    new Vector3D( -1.0, 1.0,-1.0 ),
                    new Vector3D(  0.0, 1.0,-1.0 ),
                    new Vector3D(  1.0, 1.0,-1.0 ),
                    new Vector3D( -1.0, 0.0,-1.0 ),
                    new Vector3D(  0.0, 0.0,-1.0 ),
                    new Vector3D(  1.0, 0.0,-1.0 ),
                    new Vector3D( -1.0,-1.0,-1.0 ),
                    new Vector3D(  0.0,-1.0,-1.0 ),
                    new Vector3D(  1.0,-1.0,-1.0 ),
                };

                foreach (var lightDirInfo in testDirections)
                {
                    if (lightDirInfo.Length() < 0.1)
                        continue;
                    var lightDir = Vector3D.Normalize(lightDirInfo);

                    if (PlaneD.DotNormal(planeD, lightDir) < 0.1)
                        continue;

                    var m = Matrix4x4D.CreateShadow(lightDir, planeD);
                    var pp = -planeD.D * planeD.Normal; // origin of the PlaneD.

                    //
                    foreach (var point in points)
                    {
                        var v4 = Vector4D.Transform(point, m);

                        var sp = new Vector3D(v4.X, v4.Y, v4.Z) / v4.W;

                        // Make sure transformed position is on the PlaneD.
                        var v = sp - pp;
                        var d = Vector3D.Dot(v, planeD.Normal);
                        Assert.That(d, Is.EqualTo(0.0).Using<double>(MathHelper.Equal), "Matrix4x4D.CreateShadow did not provide expected value.");

                        // make sure direction between transformed position and original position are same as light direction.
                        if (Vector3D.Dot(point - pp, planeD.Normal) > 0.0001)
                        {
                            var dir = Vector3D.Normalize(point - sp);
                            Assert.That(dir, Is.EqualTo(lightDir).Using<Vector3D>(MathHelper.Equal), "Matrix4x4D.CreateShadow did not provide expected value.");
                        }
                    }
                }
            }
        }

        // A test for CreateTranslation (Vector3D)
        [Test]
        public void Matrix4x4CreateTranslationTest1()
        {
            var position = new Vector3D(2.0, 3.0, 4.0);
            var expected = new Matrix4x4D(
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                2.0, 3.0, 4.0, 1.0);

            var actual = Matrix4x4D.CreateTranslation(position);
            Assert.That(actual, Is.EqualTo(expected));
        }

        // A test for CreateTranslation (double, double, double)
        [Test]
        public void Matrix4x4CreateTranslationTest2()
        {
            var xPosition = 2.0;
            var yPosition = 3.0;
            var zPosition = 4.0;

            var expected = new Matrix4x4D(
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                2.0, 3.0, 4.0, 1.0);

            var actual = Matrix4x4D.CreateTranslation(xPosition, yPosition, zPosition);
            Assert.That(actual, Is.EqualTo(expected));
        }

        // A test for CreateWorld (Vector3D, Vector3D, Vector3D)
        [Test]
        public void Matrix4x4CreateWorldTest()
        {
            var objectPosition = new Vector3D(10.0, 20.0, 30.0);
            var objectForwardDirection = new Vector3D(3.0, 2.0, -4.0);
            var objectUpVector = new Vector3D(0.0, 1.0, 0.0);

            var expected = new Matrix4x4D
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

            var actual = Matrix4x4D.CreateWorld(objectPosition, objectForwardDirection, objectUpVector);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.CreateWorld did not return the expected value.");

            Assert.That(actual.Translation, Is.EqualTo(objectPosition).Using<Vector3D>(MathHelper.Equal));
            Assert.That(objectUpVector.Normalize().Dot(new Vector3D(actual.M21, actual.M22, actual.M23)), Is.GreaterThan(0.0));
            Assert.That(objectForwardDirection.Normalize().Dot(new Vector3D(-actual.M31, -actual.M32, -actual.M33)), Is.GreaterThan(0.999));
        }

        // Various rotation decompose test.
        [Test]
        public void Matrix4x4DecomposeTest01()
        {
            DecomposeTest(10.0, 20.0, 30.0, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));

            const double step = 35.0;

            for (var yawAngle = -720.0; yawAngle <= 720.0; yawAngle += step)
                for (var pitchAngle = -720.0; pitchAngle <= 720.0; pitchAngle += step)
                    for (var rollAngle = -720.0; rollAngle <= 720.0; rollAngle += step)
                        DecomposeTest(yawAngle, pitchAngle, rollAngle, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));
        }

        // Various scaled matrix decompose test.
        [Test]
        public void Matrix4x4DecomposeTest02()
        {
            DecomposeTest(10.0, 20.0, 30.0, new Vector3D(10, 20, 30), new Vector3D(2, 3, 4));

            // Various scales.
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1, 2, 3));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1, 3, 2));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2, 1, 3));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2, 3, 1));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3, 1, 2));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3, 2, 1));

            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(-2, 1, 1));

            // Small scales.
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1e-4, 2e-4, 3e-4));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(1e-4, 3e-4, 2e-4));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2e-4, 1e-4, 3e-4));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(2e-4, 3e-4, 1e-4));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3e-4, 1e-4, 2e-4));
            DecomposeTest(0, 0, 0, Vector3D.Zero, new Vector3D(3e-4, 2e-4, 1e-4));

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

        // Tiny scale decompose test.
        [Test]
        public void Matrix4x4DecomposeTest03()
        {
            DecomposeScaleTest(1, 2e-4, 3e-4);
            DecomposeScaleTest(1, 3e-4, 2e-4);
            DecomposeScaleTest(2e-4, 1, 3e-4);
            DecomposeScaleTest(2e-4, 3e-4, 1);
            DecomposeScaleTest(3e-4, 1, 2e-4);
            DecomposeScaleTest(3e-4, 2e-4, 1);
        }

        [Test]
        public void Matrix4x4DecomposeTest04() =>
            Assert.False(Matrix4x4D.Decompose(GenerateIncrementalMatrixNumber(), out var scale, out var rotation, out var translation), $"decompose should have failed, but returned scale:{scale}, rotation:{rotation}, translation:{translation}.");

        // A test for Determinant
        [Test]
        public void Matrix4x4DDeterminantTest()
        {
            var target =
                    Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
                    Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
                    Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0));

            var val = 1.0;
            var det = target.GetDeterminant();

            Assert.That(det, Is.EqualTo(val).Using<double>(MathHelper.Equal), "Matrix4x4D.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix4x4DeterminantTest1()
        {
            var a = new Matrix4x4D
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
            Matrix4x4D.Invert(a, out var i);
            Assert.AreNotEqual(Matrix4x4D.NaN, i);

            var detA = a.GetDeterminant();
            var detI = i.GetDeterminant();
            var t = 1.0 / detI;

            // only accurate to 3 precision
            Assert.AreEqual(t, detA, 1e-3, "Matrix4x4D.Determinant was not set correctly.");
        }

        // A test for Matrix4x4D (QuaternionD)
        [Test]
        public void Matrix4x4DFromQuaternionTest1()
        {
            var axis = Vector3D.Normalize(new Vector3D(1.0, 2.0, 3.0));
            var q = QuaternionD.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            var expected = new Matrix4x4D
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

            var target = Matrix4x4D.CreateFromQuaternion(q);
            Assert.That(target, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Matrix4x4D(QuaternionD) did not return the expected value.");
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert X axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest2()
        {
            for (var angle = 0.0; angle < 720.0; angle += 10.0)
            {
                var quat = QuaternionD.CreateFromAxisAngle(Vector3D.UnitX, angle);

                var expected = Matrix4x4D.CreateRotationX(angle);
                var actual = Matrix4x4D.CreateFromQuaternion(quat);
                Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal),
                    "QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle);

                // make sure convert back to QuaternionD is same as we passed QuaternionD.
                var q2 = QuaternionD.CreateFromRotationMatrix(actual);
                Assert.That(q2, Is.EqualTo(quat).Using<QuaternionD>(MathHelper.EqualRotation),
                    "QuaternionD.FromQuaternion did not return the expected value. angle:{0}",
                    angle);
            }
        }

        // A test for FromQuaternion (Matrix4x4D)
        // Convert Y axis rotation matrix
        [Test]
        public void Matrix4x4DFromQuaternionTest3()
        {
            for (var angle = 0.0; angle < 720.0; angle += 10.0)
            {
                var quat = QuaternionD.CreateFromAxisAngle(Vector3D.UnitY, angle);

                var expected = Matrix4x4D.CreateRotationY(angle);
                var actual = Matrix4x4D.CreateFromQuaternion(quat);
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

        // A test for Identity
        [Test]
        public void Matrix4x4DIdentityTest()
        {
            var val = new Matrix4x4D();
            val.M11 = val.M22 = val.M33 = val.M44 = 1.0;

            Assert.That(Matrix4x4D.Identity, Is.EqualTo(val).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Indentity was not set correctly.");
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

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertAffineTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateFromYawPitchRoll(3, 4, 5) *
                            Matrix4x4D.CreateScale(23, 42, -666) *
                            Matrix4x4D.CreateTranslation(17, 53, 89);

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertIdentityTest()
        {
            Matrix4x4D mtx = Matrix4x4D.Identity;

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Assert.True(MathHelper.Equal(actual, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertProjectionTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreatePerspectiveFieldOfView(1, 1.333f, 0.1f, 666);

            Matrix4x4D.Invert(mtx, out var actual);
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

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.False(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertRotationTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateFromYawPitchRoll(3, 4, 5);

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertScaleTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateScale(23, 42, -666);

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertTest()
        {
            Matrix4x4D mtx =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0));

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

            Matrix4x4D.Invert(mtx, out var actual);

            Assert.True(actual != Matrix4x4D.NaN);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Invert did not return the expected value.");

            // Make sure M*M is identity matrix
            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity), "Matrix4x4D.Invert did not return the expected value.");
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

            Matrix4x4D.Invert(a, out var actual);

            // all the elements in Actual is NaN
            Assert.True(
                double.IsNaN(actual.M11) && double.IsNaN(actual.M12) && double.IsNaN(actual.M13) && double.IsNaN(actual.M14) &&
                double.IsNaN(actual.M21) && double.IsNaN(actual.M22) && double.IsNaN(actual.M23) && double.IsNaN(actual.M24) &&
                double.IsNaN(actual.M31) && double.IsNaN(actual.M32) && double.IsNaN(actual.M33) && double.IsNaN(actual.M34) &&
                double.IsNaN(actual.M41) && double.IsNaN(actual.M42) && double.IsNaN(actual.M43) && double.IsNaN(actual.M44)
                , "Matrix4x4D.Invert did not return the expected value.");
        }

        // A test for Invert (Matrix4x4D)
        [Test]
        public void Matrix4x4InvertTranslationTest()
        {
            Matrix4x4D mtx = Matrix4x4D.CreateTranslation(23, 42, 666);

            Matrix4x4D.Invert(mtx, out var actual);
            Assert.True(actual != Matrix4x4D.NaN);

            Matrix4x4D i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4D.Identity));
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Lerp did not return the expected value.");
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.operator * did not return the expected value.");
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

            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.operator * did not return the expected value.");
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

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Matrix4x4SizeofTest()
        {
            Assert.AreEqual(128, sizeof(Matrix4x4D));
            Assert.AreEqual(256, sizeof(Matrix4x4_2x));
            Assert.AreEqual(136, sizeof(Matrix4x4Plusdouble));
            Assert.AreEqual(272, sizeof(Matrix4x4Plusdouble_2x));
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.operator - did not return the expected value.");
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Transform did not return the expected value.");
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix4x4D)
        // Transpose Identity matrix
        [Test]
        public void Matrix4x4TransposeTest1()
        {
            Matrix4x4D a = Matrix4x4D.Identity;
            Matrix4x4D expected = Matrix4x4D.Identity;

            Matrix4x4D actual = Matrix4x4D.Transpose(a);
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.Transpose did not return the expected value.");
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
            Assert.That(actual, Is.EqualTo(expected).Using<Matrix4x4D>(MathHelper.Equal), "Matrix4x4D.operator - did not return the expected value.");
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

        #endregion Public Methods

        #region Private Methods

        private static void CreateReflectionTest(PlaneD PlaneD, Matrix4x4D expected)
        {
            Matrix4x4D actual = Matrix4x4D.CreateReflection(PlaneD);
            Assert.That(actual, Is.EqualTo(expected), "Matrix4x4D.CreateReflection did not return expected value.");
        }

        private static void DecomposeScaleTest(double sx, double sy, double sz)
        {
            Matrix4x4D m = Matrix4x4D.CreateScale(sx, sy, sz);

            Vector3D expectedScales = new Vector3D(sx, sy, sz);

            bool actualResult = Matrix4x4D.Decompose(m, out Vector3D scales, out QuaternionD rotation, out Vector3D translation);
            Assert.True(actualResult, "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(expectedScales, scales), "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.EqualRotation(QuaternionD.Identity, rotation), "Matrix4x4D.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(Vector3D.Zero, translation), "Matrix4x4D.Decompose did not return expected value.");
        }

        private static void DecomposeTest(double yaw, double pitch, double roll, Vector3D expectedTranslation, Vector3D expectedScales)
        {
            QuaternionD expectedRotation = QuaternionD.CreateFromYawPitchRoll(MathHelper.ToRadians(yaw),
                                                                            MathHelper.ToRadians(pitch),
                                                                            MathHelper.ToRadians(roll));

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

        private static Matrix4x4D GenerateIncrementalMatrixNumber(double value = 0.0f)
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

        private static Matrix4x4D GenerateTestMatrix()
        {
            Matrix4x4D m =
                Matrix4x4D.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4D.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.Translation = new Vector3D(111.0f, 222.0f, 333.0f);
            return m;
        }

        #endregion Private Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4_2x
        {
            private Matrix4x4D _a;
            private Matrix4x4D _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4Plusdouble
        {
            private Matrix4x4D _v;
            private readonly double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4Plusdouble_2x
        {
            private Matrix4x4Plusdouble _a;
            private Matrix4x4Plusdouble _b;
        }

        #endregion Private Structs
    }
}