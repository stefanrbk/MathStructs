using MathStructs;

using NUnit.Framework;

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Tests
{
    public class Matrix4x4FTests
    {
        #region Public Methods

        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = (new float[] {4.0f, 7.0f, 2.0f, 9.0f,
                                     12.0f, 0.0f, 3.0f, 8.0f,
                                     6.0f, 5.0f, 10.0f, 15.0f,
                                     14.0f, 13.0f, 11.0f, 1.0f}).AsSpan();
            var vec = span.ToMatrix4x4F();

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

        // A test for operator + (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4AdditionTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = a + b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.operator + did not return the expected value.");
        }

        // A test for Add (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4AddTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = Matrix4x4F.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Forward side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest01()
        {
            // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3F(0, 0, -1), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Backward side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest02()
        {
            // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(0)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Right side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest03()
        {
            // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3F(1, 0, 0), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(90)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Left side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest04()
        {
            // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
            CreateBillboardFact(new Vector3F(-1, 0, 0), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(-90)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Up side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest05()
        {
            // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3F(0, 1, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(180)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Down side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest06()
        {
            // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3F(0, -1, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(0)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Right side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest07()
        {
            // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3F(1, 0, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Left side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest08()
        {
            // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateBillboardFact(new Vector3F(-1, 0, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Up side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest09()
        {
            // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3F(0, 1, 0), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Down side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest10()
        {
            // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3F(0, -1, 0), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Forward side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest11()
        {
            // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3F(0, 0, -1), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Backward side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateBillboardTest12()
        {
            // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateBillboardFact(new Vector3F(0, 0, 1), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(0.0f)));
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [Test]
        public void Matrix4x4CreateBillboardTooCloseTest1()
        {
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F cameraPosition = objectPosition;
            Vector3F cameraUpVector = new Vector3F(0, 1, 0);

            // Doesn't pass camera face direction. CreateBillboard uses new Vector3f(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3F(0, 0, 1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateBillboard did not return the expected value.");
        }

        // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Object and camera positions are too close and passed cameraForwardVector.
        [Test]
        public void Matrix4x4CreateBillboardTooCloseTest2()
        {
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F cameraPosition = objectPosition;
            Vector3F cameraUpVector = new Vector3F(0, 1, 0);

            // Passes Vector3f.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3F(1, 0, 0));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Angle between rotateAxis and camera to object vector is too small. And use doesn't passed objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest1()
        {
            // Place camera at up side of object.
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F rotateAxis = new Vector3F(0, 1, 0);
            Vector3F cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks new Vector3f(0, 0, -1) as object forward vector.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Angle between rotateAxis and camera to object vector is too small. And user doesn't passed objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest2()
        {
            // Place camera at up side of object.
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F rotateAxis = new Vector3F(0, 0, -1);
            Vector3F cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks new Vector3f(1, 0, 0) as object forward vector.
            Matrix4x4F expected = Matrix4x4F.CreateRotationX(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed correct objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest3()
        {
            // Place camera at up side of object.
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F rotateAxis = new Vector3F(0, 1, 0);
            Vector3F cameraPosition = objectPosition + rotateAxis * 10.0f;

            // User passes correct objectForwardVector.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest4()
        {
            // Place camera at up side of object.
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F rotateAxis = new Vector3F(0, 1, 0);
            Vector3F cameraPosition = objectPosition + rotateAxis * 10.0f;

            // User passes correct objectForwardVector.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 1, 0));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardAlongAxisTest5()
        {
            // Place camera at up side of object.
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F rotateAxis = new Vector3F(0, 0, -1);
            Vector3F cameraPosition = objectPosition + rotateAxis * 10.0f;

            // In this case, CreateConstrainedBillboard picks Vector3f.Right as object forward vector.
            Matrix4x4F expected = Matrix4x4F.CreateRotationX(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Forward side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest01()
        {
            // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 0, -1), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Backward side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest02()
        {
            // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(0)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Right side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest03()
        {
            // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3F(1, 0, 0), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(90)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Left side of camera on XZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest04()
        {
            // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
            CreateConstrainedBillboardFact(new Vector3F(-1, 0, 0), new Vector3F(0, 1, 0), Matrix4x4F.CreateRotationY(MathHelper.ToRadians(-90)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Up side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest05()
        {
            // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 1, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(180)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Down side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest06()
        {
            // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, -1, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(0)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Right side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest07()
        {
            // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3F(1, 0, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Left side of camera on XY-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest08()
        {
            // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
            CreateConstrainedBillboardFact(new Vector3F(-1, 0, 0), new Vector3F(0, 0, 1),
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Up side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest09()
        {
            // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 1, 0), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(-90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Down side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest10()
        {
            // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, -1, 0), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(90.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Forward side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest11()
        {
            // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 0, -1), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(180.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Place object at Backward side of camera on YZ-PlaneF
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTest12()
        {
            // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
            CreateConstrainedBillboardFact(new Vector3F(0, 0, 1), new Vector3F(-1, 0, 0),
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(90.0f)) * Matrix4x4F.CreateRotationX(MathHelper.ToRadians(0.0f)));
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Object and camera positions are too close and doesn't pass cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest1()
        {
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F cameraPosition = objectPosition;
            Vector3F cameraUpVector = new Vector3F(0, 1, 0);

            // Doesn't pass camera face direction. CreateConstrainedBillboard uses new Vector3f(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(180.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3F(0, 0, 1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
        // Object and camera positions are too close and passed cameraForwardVector.
        [Test]
        public void Matrix4x4CreateConstrainedBillboardTooCloseTest2()
        {
            Vector3F objectPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F cameraPosition = objectPosition;
            Vector3F cameraUpVector = new Vector3F(0, 1, 0);

            // Passes Vector3f.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
            Matrix4x4F expected = Matrix4x4F.CreateRotationY(MathHelper.ToRadians(-90.0f)) * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3F(1, 0, 0), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        // A test for CreateFromAxisAngle(Vector3f,float)
        [Test]
        public void Matrix4x4CreateFromAxisAngleTest()
        {
            float radians = MathHelper.ToRadians(-30.0f);

            Matrix4x4F expected = Matrix4x4F.CreateRotationX(radians);
            Matrix4x4F actual = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitX, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4F.CreateRotationY(radians);
            actual = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitY, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4F.CreateRotationZ(radians);
            actual = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitZ, radians);
            Assert.True(MathHelper.Equal(expected, actual));

            expected = Matrix4x4F.CreateFromQuaternion(QuaternionF.CreateFromAxisAngle(Vector3F.Normalize(Vector3F.One), radians));
            actual = Matrix4x4F.CreateFromAxisAngle(Vector3F.Normalize(Vector3F.One), radians);
            Assert.True(MathHelper.Equal(expected, actual));

            const int rotCount = 16;
            for (int i = 0; i < rotCount; ++i)
            {
                float latitude = (2.0f * MathHelper.PiF) * ((float)i / (float)rotCount);
                for (int j = 0; j < rotCount; ++j)
                {
                    float longitude = -MathHelper.PiOver2F + MathHelper.PiF * ((float)j / (float)rotCount);

                    Matrix4x4F m = Matrix4x4F.CreateRotationZ(longitude) * Matrix4x4F.CreateRotationY(latitude);
                    Vector3F axis = new Vector3F(m.M11, m.M12, m.M13);
                    for (int k = 0; k < rotCount; ++k)
                    {
                        float rot = (2.0f * MathHelper.PiF) * ((float)k / (float)rotCount);
                        expected = Matrix4x4F.CreateFromQuaternion(QuaternionF.CreateFromAxisAngle(axis, rot));
                        actual = Matrix4x4F.CreateFromAxisAngle(axis, rot);
                        Assert.True(MathHelper.Equal(expected, actual));
                    }
                }
            }
        }

        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest1()
        {
            float yawAngle = MathHelper.ToRadians(30.0f);
            float pitchAngle = MathHelper.ToRadians(40.0f);
            float rollAngle = MathHelper.ToRadians(50.0f);

            Matrix4x4F yaw = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitY, yawAngle);
            Matrix4x4F pitch = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitX, pitchAngle);
            Matrix4x4F roll = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitZ, rollAngle);

            Matrix4x4F expected = roll * pitch * yaw;
            Matrix4x4F actual = Matrix4x4F.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.True(MathHelper.Equal(expected, actual));
        }

        // Covers more numeric rigions
        [Test]
        public void Matrix4x4CreateFromYawPitchRollTest2()
        {
            const float step = 35.0f;

            for (float yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
            {
                for (float pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
                {
                    for (float rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                    {
                        float yawRad = MathHelper.ToRadians(yawAngle);
                        float pitchRad = MathHelper.ToRadians(pitchAngle);
                        float rollRad = MathHelper.ToRadians(rollAngle);
                        Matrix4x4F yaw = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitY, yawRad);
                        Matrix4x4F pitch = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitX, pitchRad);
                        Matrix4x4F roll = Matrix4x4F.CreateFromAxisAngle(Vector3F.UnitZ, rollRad);

                        Matrix4x4F expected = roll * pitch * yaw;
                        Matrix4x4F actual = Matrix4x4F.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), string.Format("Yaw:{0} Pitch:{1} Roll:{2}", yawAngle, pitchAngle, rollAngle));
                    }
                }
            }
        }

        // A test for CrateLookAt (Vector3f, Vector3f, Vector3f)
        [Test]
        public void Matrix4x4CreateLookAtTest()
        {
            Vector3F cameraPosition = new Vector3F(10.0f, 20.0f, 30.0f);
            Vector3F cameraTarget = new Vector3F(3.0f, 2.0f, -4.0f);
            Vector3F cameraUpVector = new Vector3F(0.0f, 1.0f, 0.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.979457f,
                M12 = -0.0928267762f,
                M13 = 0.179017f,

                M21 = 0.0f,
                M22 = 0.8877481f,
                M23 = 0.460329473f,

                M31 = -0.201652914f,
                M32 = -0.450872928f,
                M33 = 0.8695112f,

                M41 = -3.74498272f,
                M42 = -3.30050683f,
                M43 = -37.0820961f,
                M44 = 1.0f
            };

            Matrix4x4F actual = Matrix4x4F.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateLookAt did not return the expected value.");
        }

        // A test for CreateOrthoOffCenter (float, float, float, float, float, float)
        [Test]
        public void Matrix4x4CreateOrthoOffCenterTest()
        {
            float left = 10.0f;
            float right = 90.0f;
            float bottom = 20.0f;
            float top = 180.0f;
            float zNearPlane = 1.5f;
            float zFarPlane = 1000.0f;

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.025f,
                M22 = 0.0125f,
                M33 = -0.00100150227f,
                M41 = -1.25f,
                M42 = -1.25f,
                M43 = -0.00150225335f,
                M44 = 1.0f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateOrthoOffCenter did not return the expected value.");
        }

        // A test for CreateOrtho (float, float, float, float)
        [Test]
        public void Matrix4x4CreateOrthoTest()
        {
            float width = 100.0f;
            float height = 200.0f;
            float zNearPlane = 1.5f;
            float zFarPlane = 1000.0f;

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.02f,
                M22 = 0.01f,
                M33 = -0.00100150227f,
                M43 = -0.00150225335f,
                M44 = 1.0f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreateOrthographic(width, height, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateOrtho did not return the expected value.");
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest()
        {
            float fieldOfView = MathHelper.ToRadians(30.0f);
            float aspectRatio = 1280.0f / 720.0f;
            float zNearPlane = 1.5f;
            float zFarPlane = 1000.0f;

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 2.09927845f,
                M22 = 3.73205066f,
                M33 = -1.00150228f,
                M34 = -1.0f,
                M43 = -1.50225341f
            };
            Matrix4x4F actual;

            actual = Matrix4x4F.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreatePerspectiveFieldOfView did not return the expected value.");
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        // CreatePerspectiveFieldOfView test where filedOfView is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(-1, 1, 1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        // CreatePerspectiveFieldOfView test where filedOfView is more than pi.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(MathHelper.PiF + 0.01f, 1, 1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(MathHelper.PiOver4F, 1, -1, 10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        // CreatePerspectiveFieldOfView test where farPlaneDistance is negative value.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest4()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(MathHelper.PiOver4F, 1, 1, -10);
            });
        }

        // A test for CreatePerspectiveFieldOfView (float, float, float, float)
        // CreatePerspectiveFieldOfView test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveFieldOfViewTest5()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(MathHelper.PiOver4F, 1, 10, 1);
            });
        }

        // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest()
        {
            float left = 10.0f;
            float right = 90.0f;
            float bottom = 20.0f;
            float top = 180.0f;
            float zNearPlane = 1.5f;
            float zFarPlane = 1000.0f;

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.0375f,
                M22 = 0.01875f,
                M31 = 1.25f,
                M32 = 1.25f,
                M33 = -1.00150228f,
                M34 = -1.0f,
                M43 = -1.50225341f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreatePerspectiveOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreatePerspectiveOffCenter did not return the expected value.");
        }

        // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
        // CreatePerspectiveOffCenter test where nearPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4F actual = Matrix4x4F.CreatePerspectiveOffCenter(left, right, bottom, top, -1, 10);
            });
        }

        // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
        // CreatePerspectiveOffCenter test where farPlaneDistance is negative.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4F actual = Matrix4x4F.CreatePerspectiveOffCenter(left, right, bottom, top, 1, -10);
            });
        }

        // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
        // CreatePerspectiveOffCenter test where test where nearPlaneDistance is larger than farPlaneDistance.
        [Test]
        public void Matrix4x4CreatePerspectiveOffCenterTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
                Matrix4x4F actual = Matrix4x4F.CreatePerspectiveOffCenter(left, right, bottom, top, 10, 1);
            });
        }

        // A test for CreatePerspective (float, float, float, float)
        [Test]
        public void Matrix4x4CreatePerspectiveTest()
        {
            float width = 100.0f;
            float height = 200.0f;
            float zNearPlane = 1.5f;
            float zFarPlane = 1000.0f;

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.03f,
                M22 = 0.015f,
                M33 = -1.00150228f,
                M34 = -1.0f,
                M43 = -1.50225341f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreatePerspective(width, height, zNearPlane, zFarPlane);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreatePerspective did not return the expected value.");
        }

        // A test for CreatePerspective (float, float, float, float)
        // CreatePerspective test where znear = zfar
        [Test]
        public void Matrix4x4CreatePerspectiveTest1()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float width = 100.0f;
                float height = 200.0f;
                float zNearPlane = 0.0f;
                float zFarPlane = 0.0f;

                Matrix4x4F actual = Matrix4x4F.CreatePerspective(width, height, zNearPlane, zFarPlane);
            });
        }

        // A test for CreatePerspective (float, float, float, float)
        // CreatePerspective test where near PlaneF is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F actual = Matrix4x4F.CreatePerspective(10, 10, -10, 10);
            });
        }

        // A test for CreatePerspective (float, float, float, float)
        // CreatePerspective test where far PlaneF is negative value
        [Test]
        public void Matrix4x4CreatePerspectiveTest3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F actual = Matrix4x4F.CreatePerspective(10, 10, 10, -10);
            });
        }

        // A test for CreatePerspective (float, float, float, float)
        // CreatePerspective test where near PlaneF is beyond far PlaneF
        [Test]
        public void Matrix4x4CreatePerspectiveTest4()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Matrix4x4F actual = Matrix4x4F.CreatePerspective(10, 10, 10, 1);
            });
        }

        [Test]
        public void Matrix4x4CreateReflectionTest01()
        {
            // XY PlaneF.
            CreateReflectionTest(new PlaneF(Vector3F.UnitZ, 0), Matrix4x4F.CreateScale(1, 1, -1));
            // XZ PlaneF.
            CreateReflectionTest(new PlaneF(Vector3F.UnitY, 0), Matrix4x4F.CreateScale(1, -1, 1));
            // YZ PlaneF.
            CreateReflectionTest(new PlaneF(Vector3F.UnitX, 0), Matrix4x4F.CreateScale(-1, 1, 1));

            // Complex cases.
            PlaneF[] planes = {
                new PlaneF( 0, 1, 0, 0 ),
                new PlaneF( 1, 2, 3, 4 ),
                new PlaneF( 5, 6, 7, 8 ),
                new PlaneF(-1,-2,-3,-4 ),
                new PlaneF(-5,-6,-7,-8 ),
            };

            Vector3F[] points = {
                new Vector3F( 1, 2, 3),
                new Vector3F( 5, 6, 7),
                new Vector3F(-1,-2,-3),
                new Vector3F(-5,-6,-7),
            };

            foreach (PlaneF p in planes)
            {
                PlaneF PlaneF = PlaneF.Normalize(p);
                Matrix4x4F m = Matrix4x4F.CreateReflection(PlaneF);
                Vector3F pp = -PlaneF.D * PlaneF.Normal; // Position on the PlaneF.

                //
                foreach (Vector3F point in points)
                {
                    Vector3F rp = Vector3F.Transform(point, m);

                    // Manually compute reflection point and compare results.
                    Vector3F v = point - pp;
                    float d = Vector3F.Dot(v, PlaneF.Normal);
                    Vector3F vp = point - 2.0f * d * PlaneF.Normal;
                    Assert.True(MathHelper.Equal(rp, vp), "Matrix4x4F.Reflection did not provide expected value.");
                }
            }
        }

        // A test for CreateRotationX (float, Vector3f)
        [Test]
        public void Matrix4x4CreateRotationXCenterTest()
        {
            float radians = MathHelper.ToRadians(30.0f);
            Vector3F center = new Vector3F(23, 42, 66);

            Matrix4x4F rotateAroundZero = Matrix4x4F.CreateRotationX(radians, Vector3F.Zero);
            Matrix4x4F rotateAroundZeroExpected = Matrix4x4F.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4F rotateAroundCenter = Matrix4x4F.CreateRotationX(radians, center);
            Matrix4x4F rotateAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateRotationX(radians) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CreateRotationX (float)
        [Test]
        public void Matrix4x4CreateRotationXTest()
        {
            float radians = MathHelper.ToRadians(30.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 1.0f,
                M22 = 0.8660254f,
                M23 = 0.5f,
                M32 = -0.5f,
                M33 = 0.8660254f,
                M44 = 1.0f
            };

            Matrix4x4F actual;

            actual = Matrix4x4F.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationX (float)
        // CreateRotationX of zero degree
        [Test]
        public void Matrix4x4CreateRotationXTest1()
        {
            float radians = 0;

            Matrix4x4F expected = Matrix4x4F.Identity;
            Matrix4x4F actual = Matrix4x4F.CreateRotationX(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateRotationX did not return the expected value.");
        }

        // A test for CreateRotationY (float, Vector3f)
        [Test]
        public void Matrix4x4CreateRotationYCenterTest()
        {
            float radians = MathHelper.ToRadians(30.0f);
            Vector3F center = new Vector3F(23, 42, 66);

            Matrix4x4F rotateAroundZero = Matrix4x4F.CreateRotationY(radians, Vector3F.Zero);
            Matrix4x4F rotateAroundZeroExpected = Matrix4x4F.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4F rotateAroundCenter = Matrix4x4F.CreateRotationY(radians, center);
            Matrix4x4F rotateAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateRotationY(radians) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CreateRotationY (float)
        [Test]
        public void Matrix4x4CreateRotationYTest()
        {
            float radians = MathHelper.ToRadians(60.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.49999997f,
                M13 = -0.866025448f,
                M22 = 1.0f,
                M31 = 0.866025448f,
                M33 = 0.49999997f,
                M44 = 1.0f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateRotationY did not return the expected value.");
        }

        // A test for RotationY (float)
        // CreateRotationY test for negative angle
        [Test]
        public void Matrix4x4CreateRotationYTest1()
        {
            float radians = MathHelper.ToRadians(-300.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.49999997f,
                M13 = -0.866025448f,
                M22 = 1.0f,
                M31 = 0.866025448f,
                M33 = 0.49999997f,
                M44 = 1.0f
            };

            Matrix4x4F actual = Matrix4x4F.CreateRotationY(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateRotationY did not return the expected value.");
        }

        // A test for CreateRotationZ (float, Vector3f)
        [Test]
        public void Matrix4x4CreateRotationZCenterTest()
        {
            float radians = MathHelper.ToRadians(30.0f);
            Vector3F center = new Vector3F(23, 42, 66);

            Matrix4x4F rotateAroundZero = Matrix4x4F.CreateRotationZ(radians, Vector3F.Zero);
            Matrix4x4F rotateAroundZeroExpected = Matrix4x4F.CreateRotationZ(radians);
            Assert.True(MathHelper.Equal(rotateAroundZero, rotateAroundZeroExpected));

            Matrix4x4F rotateAroundCenter = Matrix4x4F.CreateRotationZ(radians, center);
            Matrix4x4F rotateAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateRotationZ(radians) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(rotateAroundCenter, rotateAroundCenterExpected));
        }

        // A test for CreateRotationZ (float)
        [Test]
        public void Matrix4x4CreateRotationZTest()
        {
            float radians = MathHelper.ToRadians(50.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.642787635f,
                M12 = 0.766044438f,
                M21 = -0.766044438f,
                M22 = 0.642787635f,
                M33 = 1.0f,
                M44 = 1.0f
            };

            Matrix4x4F actual;
            actual = Matrix4x4F.CreateRotationZ(radians);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateRotationZ did not return the expected value.");
        }

        // A test for CreateScale (Vector3f, Vector3f)
        [Test]
        public void Matrix4x4CreateScaleCenterTest1()
        {
            Vector3F scale = new Vector3F(3, 4, 5);
            Vector3F center = new Vector3F(23, 42, 666);

            Matrix4x4F scaleAroundZero = Matrix4x4F.CreateScale(scale, Vector3F.Zero);
            Matrix4x4F scaleAroundZeroExpected = Matrix4x4F.CreateScale(scale);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4F scaleAroundCenter = Matrix4x4F.CreateScale(scale, center);
            Matrix4x4F scaleAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateScale(scale) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateScale (float, Vector3f)
        [Test]
        public void Matrix4x4CreateScaleCenterTest2()
        {
            float scale = 5;
            Vector3F center = new Vector3F(23, 42, 666);

            Matrix4x4F scaleAroundZero = Matrix4x4F.CreateScale(scale, Vector3F.Zero);
            Matrix4x4F scaleAroundZeroExpected = Matrix4x4F.CreateScale(scale);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4F scaleAroundCenter = Matrix4x4F.CreateScale(scale, center);
            Matrix4x4F scaleAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateScale(scale) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateScale (float, float, float, Vector3f)
        [Test]
        public void Matrix4x4CreateScaleCenterTest3()
        {
            Vector3F scale = new Vector3F(3, 4, 5);
            Vector3F center = new Vector3F(23, 42, 666);

            Matrix4x4F scaleAroundZero = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z, Vector3F.Zero);
            Matrix4x4F scaleAroundZeroExpected = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z);
            Assert.True(MathHelper.Equal(scaleAroundZero, scaleAroundZeroExpected));

            Matrix4x4F scaleAroundCenter = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z, center);
            Matrix4x4F scaleAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z) * Matrix4x4F.CreateTranslation(center);
            Assert.True(MathHelper.Equal(scaleAroundCenter, scaleAroundCenterExpected));
        }

        // A test for CreateScale (Vector3f)
        [Test]
        public void Matrix4x4CreateScaleTest1()
        {
            Vector3F scales = new Vector3F(2.0f, 3.0f, 4.0f);
            Matrix4x4F expected = new Matrix4x4F(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 3.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 4.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4F actual = Matrix4x4F.CreateScale(scales);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateScale (float)
        [Test]
        public void Matrix4x4CreateScaleTest2()
        {
            float scale = 2.0f;
            Matrix4x4F expected = new Matrix4x4F(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 2.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 2.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4F actual = Matrix4x4F.CreateScale(scale);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateScale (float, float, float)
        [Test]
        public void Matrix4x4CreateScaleTest3()
        {
            float xScale = 2.0f;
            float yScale = 3.0f;
            float zScale = 4.0f;
            Matrix4x4F expected = new Matrix4x4F(
                2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 3.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 4.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
            Matrix4x4F actual = Matrix4x4F.CreateScale(xScale, yScale, zScale);
            Assert.AreEqual(expected, actual);
        }

        // Simple shadow test.
        [Test]
        public void Matrix4x4CreateShadowTest01()
        {
            Vector3F lightDir = Vector3F.UnitY;
            PlaneF PlaneF = new PlaneF(Vector3F.UnitY, 0);

            Matrix4x4F expected = Matrix4x4F.CreateScale(1, 0, 1);

            Matrix4x4F actual = Matrix4x4F.CreateShadow(lightDir, PlaneF);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateShadow did not returned expected value.");
        }

        // Various PlaneF projections.
        [Test]
        public void Matrix4x4CreateShadowTest02()
        {
            // Complex cases.
            PlaneF[] planes = {
                new PlaneF( 0, 1, 0, 0 ),
                new PlaneF( 1, 2, 3, 4 ),
                new PlaneF( 5, 6, 7, 8 ),
                new PlaneF(-1,-2,-3,-4 ),
                new PlaneF(-5,-6,-7,-8 ),
            };

            Vector3F[] points = {
                new Vector3F( 1, 2, 3),
                new Vector3F( 5, 6, 7),
                new Vector3F( 8, 9, 10),
                new Vector3F(-1,-2,-3),
                new Vector3F(-5,-6,-7),
                new Vector3F(-8,-9,-10),
            };

            foreach (PlaneF p in planes)
            {
                PlaneF PlaneF = PlaneF.Normalize(p);

                // Try various direction of light directions.
                var testDirections = new Vector3F[]
                {
                    new Vector3F( -1.0f, 1.0f, 1.0f ),
                    new Vector3F(  0.0f, 1.0f, 1.0f ),
                    new Vector3F(  1.0f, 1.0f, 1.0f ),
                    new Vector3F( -1.0f, 0.0f, 1.0f ),
                    new Vector3F(  0.0f, 0.0f, 1.0f ),
                    new Vector3F(  1.0f, 0.0f, 1.0f ),
                    new Vector3F( -1.0f,-1.0f, 1.0f ),
                    new Vector3F(  0.0f,-1.0f, 1.0f ),
                    new Vector3F(  1.0f,-1.0f, 1.0f ),

                    new Vector3F( -1.0f, 1.0f, 0.0f ),
                    new Vector3F(  0.0f, 1.0f, 0.0f ),
                    new Vector3F(  1.0f, 1.0f, 0.0f ),
                    new Vector3F( -1.0f, 0.0f, 0.0f ),
                    new Vector3F(  0.0f, 0.0f, 0.0f ),
                    new Vector3F(  1.0f, 0.0f, 0.0f ),
                    new Vector3F( -1.0f,-1.0f, 0.0f ),
                    new Vector3F(  0.0f,-1.0f, 0.0f ),
                    new Vector3F(  1.0f,-1.0f, 0.0f ),

                    new Vector3F( -1.0f, 1.0f,-1.0f ),
                    new Vector3F(  0.0f, 1.0f,-1.0f ),
                    new Vector3F(  1.0f, 1.0f,-1.0f ),
                    new Vector3F( -1.0f, 0.0f,-1.0f ),
                    new Vector3F(  0.0f, 0.0f,-1.0f ),
                    new Vector3F(  1.0f, 0.0f,-1.0f ),
                    new Vector3F( -1.0f,-1.0f,-1.0f ),
                    new Vector3F(  0.0f,-1.0f,-1.0f ),
                    new Vector3F(  1.0f,-1.0f,-1.0f ),
                };

                foreach (Vector3F lightDirInfo in testDirections)
                {
                    if (lightDirInfo.Length() < 0.1f)
                        continue;
                    Vector3F lightDir = Vector3F.Normalize(lightDirInfo);

                    if (PlaneF.DotNormal(PlaneF, lightDir) < 0.1f)
                        continue;

                    Matrix4x4F m = Matrix4x4F.CreateShadow(lightDir, PlaneF);
                    Vector3F pp = -PlaneF.D * PlaneF.Normal; // origin of the PlaneF.

                    //
                    foreach (Vector3F point in points)
                    {
                        Vector4F v4 = Vector4F.Transform(point, m);

                        Vector3F sp = new Vector3F(v4.X, v4.Y, v4.Z) / v4.W;

                        // Make sure transformed position is on the PlaneF.
                        Vector3F v = sp - pp;
                        float d = Vector3F.Dot(v, PlaneF.Normal);
                        Assert.True(MathHelper.Equal(d, 0), "Matrix4x4F.CreateShadow did not provide expected value.");

                        // make sure direction between transformed position and original position are same as light direction.
                        if (Vector3F.Dot(point - pp, PlaneF.Normal) > 0.0001f)
                        {
                            Vector3F dir = Vector3F.Normalize(point - sp);
                            Assert.True(MathHelper.Equal(dir, lightDir), "Matrix4x4F.CreateShadow did not provide expected value.");
                        }
                    }
                }
            }
        }

        // A test for CreateTranslation (Vector3f)
        [Test]
        public void Matrix4x4CreateTranslationTest1()
        {
            Vector3F position = new Vector3F(2.0f, 3.0f, 4.0f);
            Matrix4x4F expected = new Matrix4x4F(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                2.0f, 3.0f, 4.0f, 1.0f);

            Matrix4x4F actual = Matrix4x4F.CreateTranslation(position);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateTranslation (float, float, float)
        [Test]
        public void Matrix4x4CreateTranslationTest2()
        {
            float xPosition = 2.0f;
            float yPosition = 3.0f;
            float zPosition = 4.0f;

            Matrix4x4F expected = new Matrix4x4F(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                2.0f, 3.0f, 4.0f, 1.0f);

            Matrix4x4F actual = Matrix4x4F.CreateTranslation(xPosition, yPosition, zPosition);
            Assert.AreEqual(expected, actual);
        }

        // A test for CreateWorld (Vector3f, Vector3f, Vector3f)
        [Test]
        public void Matrix4x4CreateWorldTest()
        {
            Vector3F objectPosition = new Vector3F(10.0f, 20.0f, 30.0f);
            Vector3F objectForwardDirection = new Vector3F(3.0f, 2.0f, -4.0f);
            Vector3F objectUpVector = new Vector3F(0.0f, 1.0f, 0.0f);

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.799999952f,
                M12 = 0,
                M13 = 0.599999964f,
                M14 = 0,

                M21 = -0.2228344f,
                M22 = 0.928476632f,
                M23 = 0.297112525f,
                M24 = 0,

                M31 = -0.557086f,
                M32 = -0.371390671f,
                M33 = 0.742781341f,
                M34 = 0,

                M41 = 10,
                M42 = 20,
                M43 = 30,
                M44 = 1.0f
            };

            Matrix4x4F actual = Matrix4x4F.CreateWorld(objectPosition, objectForwardDirection, objectUpVector);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateWorld did not return the expected value.");

            Assert.AreEqual(objectPosition, actual.Translation);
            Assert.True(Vector3F.Dot(Vector3F.Normalize(objectUpVector), new Vector3F(actual.M21, actual.M22, actual.M23)) > 0);
            Assert.True(Vector3F.Dot(Vector3F.Normalize(objectForwardDirection), new Vector3F(-actual.M31, -actual.M32, -actual.M33)) > 0.999f);
        }

        // Various rotation decompose test.
        [Test]
        public void Matrix4x4DecomposeTest01()
        {
            DecomposeTest(10.0f, 20.0f, 30.0f, new Vector3F(10, 20, 30), new Vector3F(2, 3, 4));

            const float step = 35.0f;

            for (float yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
            {
                for (float pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
                {
                    for (float rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                    {
                        DecomposeTest(yawAngle, pitchAngle, rollAngle, new Vector3F(10, 20, 30), new Vector3F(2, 3, 4));
                    }
                }
            }
        }

        // Various scaled matrix decompose test.
        [Test]
        public void Matrix4x4DecomposeTest02()
        {
            DecomposeTest(10.0f, 20.0f, 30.0f, new Vector3F(10, 20, 30), new Vector3F(2, 3, 4));

            // Various scales.
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(1, 2, 3));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(1, 3, 2));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(2, 1, 3));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(2, 3, 1));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(3, 1, 2));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(3, 2, 1));

            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(-2, 1, 1));

            // Small scales.
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(1e-4f, 2e-4f, 3e-4f));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(1e-4f, 3e-4f, 2e-4f));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(2e-4f, 1e-4f, 3e-4f));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(2e-4f, 3e-4f, 1e-4f));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(3e-4f, 1e-4f, 2e-4f));
            DecomposeTest(0, 0, 0, Vector3F.Zero, new Vector3F(3e-4f, 2e-4f, 1e-4f));

            // Zero scales.
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(0, 0, 0));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, 0, 0));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(0, 1, 0));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(0, 0, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(0, 1, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, 0, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, 1, 0));

            // Negative scales.
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(-1, -1, -1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, -1, -1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(-1, 1, -1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(-1, -1, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(-1, 1, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, -1, 1));
            DecomposeTest(0, 0, 0, new Vector3F(10, 20, 30), new Vector3F(1, 1, -1));
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
        public void Matrix4x4DecomposeTest04()
        {
            Assert.False(Matrix4x4F.Decompose(GenerateIncrementalMatrixNumber(), out _, out _, out _), "decompose should have failed.");
        }

        // A test for Determinant
        [Test]
        public void Matrix4x4DeterminantTest()
        {
            Matrix4x4F target =
                    Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                    Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));

            float val = 1.0f;
            float det = target.GetDeterminant();

            Assert.True(MathHelper.Equal(val, det), "Matrix4x4F.Determinant was not set correctly.");
        }

        // A test for Determinant
        // Determinant test |A| = 1 / |A'|
        [Test]
        public void Matrix4x4DeterminantTest1()
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = 5.0f,
                M12 = 2.0f,
                M13 = 8.25f,
                M14 = 1.0f,
                M21 = 12.0f,
                M22 = 6.8f,
                M23 = 2.14f,
                M24 = 9.6f,
                M31 = 6.5f,
                M32 = 1.0f,
                M33 = 3.14f,
                M34 = 2.22f,
                M41 = 0f,
                M42 = 0.86f,
                M43 = 4.0f,
                M44 = 1.0f
            };
            Matrix4x4F i = a.Invert();
            Assert.True(i != Matrix4x4F.NaN);

            float detA = a.GetDeterminant();
            float detI = i.GetDeterminant();
            float t = 1.0f / detI;

            // only accurate to 3 precision
            Assert.True(System.Math.Abs(detA - t) < 1e-3, "Matrix4x4F.Determinant was not set correctly.");
        }

        // A test for operator == (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4EqualityTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber();

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

        // A test for Matrix4x4F comparison involving NaN values
        [Test]
        public void Matrix4x4EqualsNanTest()
        {
            Matrix4x4F a = new Matrix4x4F(float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F b = new Matrix4x4F(0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F c = new Matrix4x4F(0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F d = new Matrix4x4F(0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F e = new Matrix4x4F(0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F f = new Matrix4x4F(0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F g = new Matrix4x4F(0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F h = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F i = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0, 0);
            Matrix4x4F j = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0, 0);
            Matrix4x4F k = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0, 0);
            Matrix4x4F l = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0, 0);
            Matrix4x4F m = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0, 0);
            Matrix4x4F n = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0, 0);
            Matrix4x4F o = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN, 0);
            Matrix4x4F p = new Matrix4x4F(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, float.NaN);

            Assert.False(a == new Matrix4x4F());
            Assert.False(b == new Matrix4x4F());
            Assert.False(c == new Matrix4x4F());
            Assert.False(d == new Matrix4x4F());
            Assert.False(e == new Matrix4x4F());
            Assert.False(f == new Matrix4x4F());
            Assert.False(g == new Matrix4x4F());
            Assert.False(h == new Matrix4x4F());
            Assert.False(i == new Matrix4x4F());
            Assert.False(j == new Matrix4x4F());
            Assert.False(k == new Matrix4x4F());
            Assert.False(l == new Matrix4x4F());
            Assert.False(m == new Matrix4x4F());
            Assert.False(n == new Matrix4x4F());
            Assert.False(o == new Matrix4x4F());
            Assert.False(p == new Matrix4x4F());

            Assert.True(a != new Matrix4x4F());
            Assert.True(b != new Matrix4x4F());
            Assert.True(c != new Matrix4x4F());
            Assert.True(d != new Matrix4x4F());
            Assert.True(e != new Matrix4x4F());
            Assert.True(f != new Matrix4x4F());
            Assert.True(g != new Matrix4x4F());
            Assert.True(h != new Matrix4x4F());
            Assert.True(i != new Matrix4x4F());
            Assert.True(j != new Matrix4x4F());
            Assert.True(k != new Matrix4x4F());
            Assert.True(l != new Matrix4x4F());
            Assert.True(m != new Matrix4x4F());
            Assert.True(n != new Matrix4x4F());
            Assert.True(o != new Matrix4x4F());
            Assert.True(p != new Matrix4x4F());

            Assert.False(a.Equals(new Matrix4x4F()));
            Assert.False(b.Equals(new Matrix4x4F()));
            Assert.False(c.Equals(new Matrix4x4F()));
            Assert.False(d.Equals(new Matrix4x4F()));
            Assert.False(e.Equals(new Matrix4x4F()));
            Assert.False(f.Equals(new Matrix4x4F()));
            Assert.False(g.Equals(new Matrix4x4F()));
            Assert.False(h.Equals(new Matrix4x4F()));
            Assert.False(i.Equals(new Matrix4x4F()));
            Assert.False(j.Equals(new Matrix4x4F()));
            Assert.False(k.Equals(new Matrix4x4F()));
            Assert.False(l.Equals(new Matrix4x4F()));
            Assert.False(m.Equals(new Matrix4x4F()));
            Assert.False(n.Equals(new Matrix4x4F()));
            Assert.False(o.Equals(new Matrix4x4F()));
            Assert.False(p.Equals(new Matrix4x4F()));

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
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber();

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

        // A test for Equals (Matrix4x4F)
        [Test]
        public void Matrix4x4EqualsTest1()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber();

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
        public unsafe void Matrix4x4FieldOffsetTest()
        {
            Matrix4x4F mat = new Matrix4x4F();

            float* basePtr = &mat.M11; // Take address of first element
            Matrix4x4F* matPtr = &mat; // Take address of whole matrix

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

        // A test for Matrix4x4F (QuaternionF)
        [Test]
        public void Matrix4x4FromQuaternionTest1()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF q = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.875595033f,
                M12 = 0.420031041f,
                M13 = -0.2385524f,
                M14 = 0.0f,

                M21 = -0.38175258f,
                M22 = 0.904303849f,
                M23 = 0.1910483f,
                M24 = 0.0f,

                M31 = 0.295970082f,
                M32 = -0.07621294f,
                M33 = 0.952151954f,
                M34 = 0.0f,

                M41 = 0.0f,
                M42 = 0.0f,
                M43 = 0.0f,
                M44 = 1.0f
            };

            Matrix4x4F target = Matrix4x4F.CreateFromQuaternion(q);
            Assert.True(MathHelper.Equal(expected, target), "Matrix4x4F.Matrix4x4F(QuaternionF) did not return the expected value.");
        }

        // A test for FromQuaternion (Matrix4x4F)
        // Convert X axis rotation matrix
        [Test]
        public void Matrix4x4FromQuaternionTest2()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                QuaternionF quat = QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);

                Matrix4x4F expected = Matrix4x4F.CreateRotationX(angle);
                Matrix4x4F actual = Matrix4x4F.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionF is same as we passed QuaternionF.
                QuaternionF q2 = QuaternionF.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4F)
        // Convert Y axis rotation matrix
        [Test]
        public void Matrix4x4FromQuaternionTest3()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                QuaternionF quat = QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle);

                Matrix4x4F expected = Matrix4x4F.CreateRotationY(angle);
                Matrix4x4F actual = Matrix4x4F.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionF is same as we passed QuaternionF.
                QuaternionF q2 = QuaternionF.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4F)
        // Convert Z axis rotation matrix
        [Test]
        public void Matrix4x4FromQuaternionTest4()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                QuaternionF quat = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle);

                Matrix4x4F expected = Matrix4x4F.CreateRotationZ(angle);
                Matrix4x4F actual = Matrix4x4F.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionF is same as we passed QuaternionF.
                QuaternionF q2 = QuaternionF.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for FromQuaternion (Matrix4x4F)
        // Convert XYZ axis rotation matrix
        [Test]
        public void Matrix4x4FromQuaternionTest5()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                QuaternionF quat =
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle) *
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle) *
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);

                Matrix4x4F expected =
                    Matrix4x4F.CreateRotationX(angle) *
                    Matrix4x4F.CreateRotationY(angle) *
                    Matrix4x4F.CreateRotationZ(angle);
                Matrix4x4F actual = Matrix4x4F.CreateFromQuaternion(quat);
                Assert.True(MathHelper.Equal(expected, actual),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to QuaternionF is same as we passed QuaternionF.
                QuaternionF q2 = QuaternionF.CreateFromRotationMatrix(actual);
                Assert.True(MathHelper.EqualRotation(quat, q2),
                    string.Format("QuaternionF.FromQuaternion did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for GetHashCode ()
        [Test]
        public void Matrix4x4GetHashCodeTest()
        {
            Matrix4x4F target = GenerateIncrementalMatrixNumber();

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
        public void Matrix4x4IdentityTest()
        {
            Matrix4x4F val = new Matrix4x4F();
            val.M11 = val.M22 = val.M33 = val.M44 = 1.0f;

            Assert.True(MathHelper.Equal(val, Matrix4x4F.Identity), "Matrix4x4F.Indentity was not set correctly.");
        }

        // A test for operator != (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4InequalityTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber();

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

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertAffineTest()
        {
            Matrix4x4F mtx = Matrix4x4F.CreateFromYawPitchRoll(3, 4, 5) *
                            Matrix4x4F.CreateScale(23, 42, -666) *
                            Matrix4x4F.CreateTranslation(17, 53, 89);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertIdentityTest()
        {
            Matrix4x4F mtx = Matrix4x4F.Identity;

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Assert.True(MathHelper.Equal(actual, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertProjectionTest()
        {
            Matrix4x4F mtx = Matrix4x4F.CreatePerspectiveFieldOfView(1, 1.333f, 0.1f, 666);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertRank3()
        {
            // A 4x4 Matrix having a rank of 3
            Matrix4x4F mtx = new Matrix4x4F(1.0f, 2.0f, 3.0f, 0.0f,
                                          5.0f, 1.0f, 6.0f, 0.0f,
                                          8.0f, 9.0f, 1.0f, 0.0f,
                                          4.0f, 7.0f, 3.0f, 0.0f);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.False(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertRotationTest()
        {
            Matrix4x4F mtx = Matrix4x4F.CreateFromYawPitchRoll(3, 4, 5);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertScaleTest()
        {
            Matrix4x4F mtx = Matrix4x4F.CreateScale(23, 42, -666);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertTest()
        {
            Matrix4x4F mtx =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = 0.74999994f,
                M12 = -0.216506317f,
                M13 = 0.62499994f,
                M14 = 0.0f,

                M21 = 0.433012635f,
                M22 = 0.87499994f,
                M23 = -0.216506317f,
                M24 = 0.0f,

                M31 = -0.49999997f,
                M32 = 0.433012635f,
                M33 = 0.74999994f,
                M34 = 0.0f,

                M41 = 0.0f,
                M42 = 0.0f,
                M43 = 0.0f,
                M44 = 0.99999994f
            };

            Matrix4x4F actual = mtx.Invert();

            Assert.True(actual != Matrix4x4F.NaN);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.Invert did not return the expected value.");

            // Make sure M*M is identity matrix
            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity), "Matrix4x4F.Invert did not return the expected value.");
        }

        // A test for Invert (Matrix4x4F)
        // Non invertible matrix - determinant is zero - singular matrix
        [Test]
        public void Matrix4x4InvertTest1()
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = 1.0f,
                M12 = 2.0f,
                M13 = 3.0f,
                M14 = 4.0f,
                M21 = 5.0f,
                M22 = 6.0f,
                M23 = 7.0f,
                M24 = 8.0f,
                M31 = 9.0f,
                M32 = 10.0f,
                M33 = 11.0f,
                M34 = 12.0f,
                M41 = 13.0f,
                M42 = 14.0f,
                M43 = 15.0f,
                M44 = 16.0f
            };

            float detA = a.GetDeterminant();
            Assert.True(MathHelper.Equal(detA, 0.0f), "Matrix4x4F.Invert did not return the expected value.");

            Matrix4x4F actual = a.Invert();

            // all the elements in Actual is NaN
            Assert.True(
                float.IsNaN(actual.M11) && float.IsNaN(actual.M12) && float.IsNaN(actual.M13) && float.IsNaN(actual.M14) &&
                float.IsNaN(actual.M21) && float.IsNaN(actual.M22) && float.IsNaN(actual.M23) && float.IsNaN(actual.M24) &&
                float.IsNaN(actual.M31) && float.IsNaN(actual.M32) && float.IsNaN(actual.M33) && float.IsNaN(actual.M34) &&
                float.IsNaN(actual.M41) && float.IsNaN(actual.M42) && float.IsNaN(actual.M43) && float.IsNaN(actual.M44)
                , "Matrix4x4F.Invert did not return the expected value.");
        }

        // A test for Invert (Matrix4x4F)
        [Test]
        public void Matrix4x4InvertTranslationTest()
        {
            Matrix4x4F mtx = Matrix4x4F.CreateTranslation(23, 42, 666);

            Matrix4x4F actual = mtx.Invert();
            Assert.True(actual != Matrix4x4F.NaN);

            Matrix4x4F i = mtx * actual;
            Assert.True(MathHelper.Equal(i, Matrix4x4F.Identity));
        }

        // A test for IsIdentity
        [Test]
        public void Matrix4x4IsIdentityTest()
        {
            Assert.True(Matrix4x4F.Identity.IsIdentity);
            Assert.True(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1).IsIdentity);
            Assert.False(new Matrix4x4F(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0).IsIdentity);
        }

        // A test for Lerp (Matrix4x4F, Matrix4x4F, float)
        [Test]
        public void Matrix4x4LerpTest()
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = 11.0f,
                M12 = 12.0f,
                M13 = 13.0f,
                M14 = 14.0f,
                M21 = 21.0f,
                M22 = 22.0f,
                M23 = 23.0f,
                M24 = 24.0f,
                M31 = 31.0f,
                M32 = 32.0f,
                M33 = 33.0f,
                M34 = 34.0f,
                M41 = 41.0f,
                M42 = 42.0f,
                M43 = 43.0f,
                M44 = 44.0f
            };

            Matrix4x4F b = GenerateIncrementalMatrixNumber();

            float t = 0.5f;

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual;
            actual = Matrix4x4F.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.Lerp did not return the expected value.");
        }

        // A test for operator * (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4MultiplyTest1()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = a * b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4MultiplyTest3()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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
            Matrix4x4F actual;
            actual = Matrix4x4F.Multiply(a, b);

            Assert.AreEqual(expected, actual);
        }

        // A test for operator * (Matrix4x4F, Matrix4x4F)
        // Multiply with identity matrix
        [Test]
        public void Matrix4x4MultiplyTest4()
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = 1.0f,
                M12 = 2.0f,
                M13 = 3.0f,
                M14 = 4.0f,
                M21 = 5.0f,
                M22 = -6.0f,
                M23 = 7.0f,
                M24 = -8.0f,
                M31 = 9.0f,
                M32 = 10.0f,
                M33 = 11.0f,
                M34 = 12.0f,
                M41 = 13.0f,
                M42 = -14.0f,
                M43 = 15.0f,
                M44 = -16.0f
            };

            Matrix4x4F b = Matrix4x4F.Identity;

            Matrix4x4F expected = a;
            Matrix4x4F actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.operator * did not return the expected value.");
        }

        // A test for Multiply (Matrix4x4F, float)
        [Test]
        public void Matrix4x4MultiplyTest5()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F expected = new Matrix4x4F(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
            Matrix4x4F actual = Matrix4x4F.Multiply(a, 3);

            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (Matrix4x4F, float)
        [Test]
        public void Matrix4x4MultiplyTest6()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F expected = new Matrix4x4F(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
            Matrix4x4F actual = a * 3;

            Assert.AreEqual(expected, actual);
        }

        // A test for Negate (Matrix4x4F)
        [Test]
        public void Matrix4x4NegateTest()
        {
            Matrix4x4F m = GenerateIncrementalMatrixNumber();

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = -1.0f,
                M12 = -2.0f,
                M13 = -3.0f,
                M14 = -4.0f,
                M21 = -5.0f,
                M22 = -6.0f,
                M23 = -7.0f,
                M24 = -8.0f,
                M31 = -9.0f,
                M32 = -10.0f,
                M33 = -11.0f,
                M34 = -12.0f,
                M41 = -13.0f,
                M42 = -14.0f,
                M43 = -15.0f,
                M44 = -16.0f
            };
            Matrix4x4F actual;

            actual = Matrix4x4F.Negate(m);
            Assert.AreEqual(expected, actual);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void Matrix4x4SizeofTest()
        {
            Assert.AreEqual(64, sizeof(Matrix4x4F));
            Assert.AreEqual(128, sizeof(Matrix4x4_2x));
            Assert.AreEqual(68, sizeof(Matrix4x4PlusFloat));
            Assert.AreEqual(136, sizeof(Matrix4x4PlusFloat_2x));
        }

        // A test for operator - (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4SubtractionTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = a - b;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.operator - did not return the expected value.");
        }

        // A test for Subtract (Matrix4x4F, Matrix4x4F)
        [Test]
        public void Matrix4x4SubtractTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();
            Matrix4x4F b = GenerateIncrementalMatrixNumber(-8.0f);

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = Matrix4x4F.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for ToString ()
        [Test]
        public void Matrix4x4ToStringTest()
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = 11.0f,
                M12 = -12.0f,
                M13 = -13.3f,
                M14 = 14.4f,
                M21 = 21.0f,
                M22 = 22.0f,
                M23 = 23.0f,
                M24 = 24.0f,
                M31 = 31.0f,
                M32 = 32.0f,
                M33 = 33.0f,
                M34 = 34.0f,
                M41 = 41.0f,
                M42 = 42.0f,
                M43 = 43.0f,
                M44 = 44.0f
            };

            string expected = string.Format(CultureInfo.CurrentCulture,
                "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}",
                    11.0f, -12.0f, -13.3f, 14.4f,
                    21.0f, 22.0f, 23.0f, 24.0f,
                    31.0f, 32.0f, 33.0f, 34.0f,
                    41.0f, 42.0f, 43.0f, 44.0f);

            string actual = a.ToString();
            Assert.AreEqual(expected, actual);
        }

        // Transform by QuaternionF test
        [Test]
        public void Matrix4x4TransformTest()
        {
            Matrix4x4F target = GenerateIncrementalMatrixNumber();

            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));

            QuaternionF q = QuaternionF.CreateFromRotationMatrix(m);

            Matrix4x4F expected = target * m;
            Matrix4x4F actual;
            actual = Matrix4x4F.Transform(target, q);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.Transform did not return the expected value.");
        }

        // A test for Translation
        [Test]
        public void Matrix4x4TranslationTest()
        {
            Matrix4x4F a = GenerateTestMatrix();
            Matrix4x4F b = a;

            // Transformed vector that has same semantics of property must be same.
            Vector3F val = new Vector3F(a.M41, a.M42, a.M43);
            Assert.AreEqual(val, a.Translation);

            // Set value and get value must be same.
            val = new Vector3F(1.0f, 2.0f, 3.0f);
            a.Translation = val;
            Assert.AreEqual(val, a.Translation);

            // Make sure it only modifies expected value of matrix.
            Assert.True(
                a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 && a.M14 == b.M14 &&
                a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 && a.M24 == b.M24 &&
                a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33 && a.M34 == b.M34 &&
                a.M41 != b.M41 && a.M42 != b.M42 && a.M43 != b.M43 && a.M44 == b.M44);
        }

        // A test for Transpose (Matrix4x4F)
        [Test]
        public void Matrix4x4TransposeTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();

            Matrix4x4F expected = new Matrix4x4F
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

            Matrix4x4F actual = Matrix4x4F.Transpose(a);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.Transpose did not return the expected value.");
        }

        // A test for Transpose (Matrix4x4F)
        // Transpose Identity matrix
        [Test]
        public void Matrix4x4TransposeTest1()
        {
            Matrix4x4F a = Matrix4x4F.Identity;
            Matrix4x4F expected = Matrix4x4F.Identity;

            Matrix4x4F actual = Matrix4x4F.Transpose(a);
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.Transpose did not return the expected value.");
        }

        // A test for operator - (Matrix4x4F)
        [Test]
        public void Matrix4x4UnaryNegationTest()
        {
            Matrix4x4F a = GenerateIncrementalMatrixNumber();

            Matrix4x4F expected = new Matrix4x4F
            {
                M11 = -1.0f,
                M12 = -2.0f,
                M13 = -3.0f,
                M14 = -4.0f,
                M21 = -5.0f,
                M22 = -6.0f,
                M23 = -7.0f,
                M24 = -8.0f,
                M31 = -9.0f,
                M32 = -10.0f,
                M33 = -11.0f,
                M34 = -12.0f,
                M41 = -13.0f,
                M42 = -14.0f,
                M43 = -15.0f,
                M44 = -16.0f
            };

            Matrix4x4F actual = -a;
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.operator - did not return the expected value.");
        }

        [Test]
        public void PerspectiveFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4F.CreatePerspective(1.0f, 1.0f, nearPlaneDistance, float.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }

        [Test]
        public void PerspectiveFieldOfViewFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4F.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), 1.5f, nearPlaneDistance, float.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }

        [Test]
        public void PerspectiveOffCenterFarPlaneAtInfinityTest()
        {
            var nearPlaneDistance = 0.125f;
            var m = Matrix4x4F.CreatePerspectiveOffCenter(0.0f, 0.0f, 1.0f, 1.0f, nearPlaneDistance, float.PositiveInfinity);
            Assert.AreEqual(-1.0f, m.M33);
            Assert.AreEqual(-nearPlaneDistance, m.M43);
        }

        #endregion Public Methods

        #region Private Methods

        private static void CreateBillboardFact(Vector3F placeDirection, Vector3F cameraUpVector, Matrix4x4F expectedRotation)
        {
            Vector3F cameraPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F objectPosition = cameraPosition + placeDirection * 10.0f;
            Matrix4x4F expected = expectedRotation * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateBillboard(objectPosition, cameraPosition, cameraUpVector, new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateBillboard did not return the expected value.");
        }

        private static void CreateConstrainedBillboardFact(Vector3F placeDirection, Vector3F rotateAxis, Matrix4x4F expectedRotation)
        {
            Vector3F cameraPosition = new Vector3F(3.0f, 4.0f, 5.0f);
            Vector3F objectPosition = cameraPosition + placeDirection * 10.0f;
            Matrix4x4F expected = expectedRotation * Matrix4x4F.CreateTranslation(objectPosition);
            Matrix4x4F actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");

            // When you move camera along rotateAxis, result must be same.
            cameraPosition += rotateAxis * 10.0f;
            actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");

            cameraPosition -= rotateAxis * 30.0f;
            actual = Matrix4x4F.CreateConstrainedBillboard(objectPosition, cameraPosition, rotateAxis, new Vector3F(0, 0, -1), new Vector3F(0, 0, -1));
            Assert.True(MathHelper.Equal(expected, actual), "Matrix4x4F.CreateConstrainedBillboard did not return the expected value.");
        }

        private static void CreateReflectionTest(PlaneF PlaneF, Matrix4x4F expected)
        {
            Matrix4x4F actual = Matrix4x4F.CreateReflection(PlaneF);
            Assert.True(MathHelper.Equal(actual, expected), "Matrix4x4F.CreateReflection did not return expected value.");
        }

        private static void DecomposeScaleTest(float sx, float sy, float sz)
        {
            Matrix4x4F m = Matrix4x4F.CreateScale(sx, sy, sz);

            Vector3F expectedScales = new Vector3F(sx, sy, sz);

            bool actualResult = Matrix4x4F.Decompose(m, out Vector3F scales, out QuaternionF rotation, out Vector3F translation);
            Assert.True(actualResult, "Matrix4x4F.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(expectedScales, scales), "Matrix4x4F.Decompose did not return expected value.");
            Assert.True(MathHelper.EqualRotation(QuaternionF.Identity, rotation), "Matrix4x4F.Decompose did not return expected value.");
            Assert.True(MathHelper.Equal(Vector3F.Zero, translation), "Matrix4x4F.Decompose did not return expected value.");
        }

        private static void DecomposeTest(float yaw, float pitch, float roll, Vector3F expectedTranslation, Vector3F expectedScales)
        {
            QuaternionF expectedRotation = QuaternionF.CreateFromYawPitchRoll(MathHelper.ToRadians(yaw),
                                                                            MathHelper.ToRadians(pitch),
                                                                            MathHelper.ToRadians(roll));

            Matrix4x4F m = Matrix4x4F.CreateScale(expectedScales) *
                          Matrix4x4F.CreateFromQuaternion(expectedRotation) *
                          Matrix4x4F.CreateTranslation(expectedTranslation);

            bool actualResult = Matrix4x4F.Decompose(m, out Vector3F scales, out QuaternionF rotation, out Vector3F translation);
            Assert.True(actualResult, "Matrix4x4F.Decompose did not return expected value.");

            bool scaleIsZeroOrNegative = expectedScales.X <= 0 ||
                                         expectedScales.Y <= 0 ||
                                         expectedScales.Z <= 0;

            if (scaleIsZeroOrNegative)
            {
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.X), Math.Abs(scales.X)), "Matrix4x4F.Decompose did not return expected value.");
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.Y), Math.Abs(scales.Y)), "Matrix4x4F.Decompose did not return expected value.");
                Assert.True(MathHelper.Equal(Math.Abs(expectedScales.Z), Math.Abs(scales.Z)), "Matrix4x4F.Decompose did not return expected value.");
            }
            else
            {
                Assert.True(MathHelper.Equal(expectedScales, scales), string.Format("Matrix4x4F.Decompose did not return expected value Expected:{0} actual:{1}.", expectedScales, scales));
                Assert.True(MathHelper.EqualRotation(expectedRotation, rotation), string.Format("Matrix4x4F.Decompose did not return expected value. Expected:{0} actual:{1}.", expectedRotation, rotation));
            }

            Assert.True(MathHelper.Equal(expectedTranslation, translation), string.Format("Matrix4x4F.Decompose did not return expected value. Expected:{0} actual:{1}.", expectedTranslation, translation));
        }

        private static Matrix4x4F GenerateIncrementalMatrixNumber(float value = 0.0f)
        {
            Matrix4x4F a = new Matrix4x4F
            {
                M11 = value + 1.0f,
                M12 = value + 2.0f,
                M13 = value + 3.0f,
                M14 = value + 4.0f,
                M21 = value + 5.0f,
                M22 = value + 6.0f,
                M23 = value + 7.0f,
                M24 = value + 8.0f,
                M31 = value + 9.0f,
                M32 = value + 10.0f,
                M33 = value + 11.0f,
                M34 = value + 12.0f,
                M41 = value + 13.0f,
                M42 = value + 14.0f,
                M43 = value + 15.0f,
                M44 = value + 16.0f
            };
            return a;
        }

        private static Matrix4x4F GenerateTestMatrix()
        {
            Matrix4x4F m =
                Matrix4x4F.CreateRotationX(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationY(MathHelper.ToRadians(30.0f)) *
                Matrix4x4F.CreateRotationZ(MathHelper.ToRadians(30.0f));
            m.Translation = new Vector3F(111.0f, 222.0f, 333.0f);
            return m;
        }

        #endregion Private Methods

        #region Private Structs

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4_2x
        {
            private Matrix4x4F _a;
            private Matrix4x4F _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4PlusFloat
        {
            private Matrix4x4F _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Matrix4x4PlusFloat_2x
        {
            private Matrix4x4PlusFloat _a;
            private Matrix4x4PlusFloat _b;
        }

        #endregion Private Structs
    }
}