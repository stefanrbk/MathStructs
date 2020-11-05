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
    public class QuaternionFFTests
    {
        // A test for Dot (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFDotTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            float expected = 70.0f;
            float actual;

            actual = QuaternionF.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Dot did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Length ()
        [Test]
        public void QuaternionFLengthTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);

            float w = 4.0f;

            QuaternionF target = new QuaternionF(v, w);

            float expected = 5.477226f;
            float actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Length did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for LengthSquared ()
        [Test]
        public void QuaternionFLengthSquaredTest()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            float w = 4.0f;

            QuaternionF target = new QuaternionF(v, w);

            float expected = 30.0f;
            float actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.LengthSquared did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionF, QuaternionF, float)
        [Test]
        public void QuaternionFLerpTest()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 0.5f;

            QuaternionF expected = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0f));
            QuaternionF actual;

            actual = QuaternionF.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Lerp did not return the expected value: expected {expected} actual {actual}");

            // Case a and b are same.
            expected = a;
            actual = QuaternionF.Lerp(a, a, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionF, QuaternionF, float)
        // Lerp test when t = 0
        [Test]
        public void QuaternionFLerpTest1()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 0.0f;

            QuaternionF expected = new QuaternionF(a.X, a.Y, a.Z, a.W);
            QuaternionF actual = QuaternionF.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionF, QuaternionF, float)
        // Lerp test when t = 1
        [Test]
        public void QuaternionFLerpTest2()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 1.0f;

            QuaternionF expected = new QuaternionF(b.X, b.Y, b.Z, b.W);
            QuaternionF actual = QuaternionF.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Lerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Lerp (QuaternionF, QuaternionF, float)
        // Lerp test when the two QuaternionFs are more than 90 degree (dot product <0)
        [Test]
        public void QuaternionFLerpTest3()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.Negate(a);

            float t = 1.0f;

            QuaternionF actual = QuaternionF.Lerp(a, b, t);
            // Note that in QuaternionF world, Q == -Q. In the case of QuaternionFs dot product is zero,
            // one of the QuaternionF will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as QuaternionF b but flipped.
            Assert.True(actual == a, $"QuaternionF.Lerp did not return the expected value: expected {a} actual {actual}");
        }

        // A test for Conjugate(QuaternionF)
        [Test]
        public void QuaternionFConjugateTest1()
        {
            QuaternionF a = new QuaternionF(1, 2, 3, 4);

            QuaternionF expected = new QuaternionF(-1, -2, -3, 4);
            QuaternionF actual;

            actual = QuaternionF.Conjugate(a);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Conjugate did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Normalize (QuaternionF)
        [Test]
        public void QuaternionFNormalizeTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionF expected = new QuaternionF(0.182574168f, 0.365148336f, 0.5477225f, 0.7302967f);
            QuaternionF actual;

            actual = QuaternionF.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Normalize did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Normalize (QuaternionF)
        // Normalize zero length QuaternionF
        [Test]
        public void QuaternionFNormalizeTest1()
        {
            QuaternionF a = new QuaternionF(0.0f, 0.0f, -0.0f, 0.0f);

            QuaternionF actual = QuaternionF.Normalize(a);
            Assert.True(float.IsNaN(actual.X) && float.IsNaN(actual.Y) && float.IsNaN(actual.Z) && float.IsNaN(actual.W)
                , $"QuaternionF.Normalize did not return the expected value: expected {new QuaternionF(float.NaN, float.NaN, float.NaN, float.NaN)} actual {actual}");
        }

        // A test for Concatenate(QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFConcatenateTest1()
        {
            QuaternionF b = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF a = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionF actual;

            actual = QuaternionF.Concatenate(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Concatenate did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator - (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFSubtractionTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 6.0f, 7.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 2.0f, 3.0f, 8.0f);

            QuaternionF expected = new QuaternionF(-4.0f, 4.0f, 4.0f, -4.0f);
            QuaternionF actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator - did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator * (QuaternionF, float)
        [Test]
        public void QuaternionFMultiplyTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            float Testor = 0.5f;

            QuaternionF expected = new QuaternionF(0.5f, 1.0f, 1.5f, 2.0f);
            QuaternionF actual;

            actual = a * Testor;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator * did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator * (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFMultiplyTest1()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionF actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator * did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator / (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFDivisionTest1()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);
            QuaternionF actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator / did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator + (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFAdditionTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(6.0f, 8.0f, 10.0f, 12.0f);
            QuaternionF actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator + did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for QuaternionF (float, float, float, float)
        [Test]
        public void QuaternionFConstructorTest()
        {
            float x = 1.0f;
            float y = 2.0f;
            float z = 3.0f;
            float w = 4.0f;

            QuaternionF target = new QuaternionF(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "QuaternionF.constructor (x,y,z,w) did not return the expected value.");
        }

        // A test for QuaternionF (Vector3Ff, float)
        [Test]
        public void QuaternionFConstructorTest1()
        {
            Vector3F v = new Vector3F(1.0f, 2.0f, 3.0f);
            float w = 4.0f;

            QuaternionF target = new QuaternionF(v, w);
            Assert.True(MathHelper.Equal(target.X, v.X) && MathHelper.Equal(target.Y, v.Y) && MathHelper.Equal(target.Z, v.Z) && MathHelper.Equal(target.W, w),
                "QuaternionF.constructor (Vector3Ff,w) did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3Ff, float)
        [Test]
        public void QuaternionFCreateFromAxisAngleTest()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            float angle = MathHelper.ToRadians(30.0f);

            QuaternionF expected = new QuaternionF(0.0691723f, 0.1383446f, 0.207516879f, 0.9659258f);
            QuaternionF actual;

            actual = QuaternionF.CreateFromAxisAngle(axis, angle);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.CreateFromAxisAngle did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for CreateFromAxisAngle (Vector3Ff, float)
        // CreateFromAxisAngle of zero vector
        [Test]
        public void QuaternionFCreateFromAxisAngleTest1()
        {
            Vector3F axis = new Vector3F();
            float angle = MathHelper.ToRadians(-30.0f);

            float cos = (float)System.Math.Cos(angle / 2.0f);
            QuaternionF actual = QuaternionF.CreateFromAxisAngle(axis, angle);

            Assert.True(actual.X == 0.0f && actual.Y == 0.0f && actual.Z == 0.0f && MathHelper.Equal(cos, actual.W)
                , "QuaternionF.CreateFromAxisAngle did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3Ff, float)
        // CreateFromAxisAngle of angle = 30 && 750
        [Test]
        public void QuaternionFCreateFromAxisAngleTest2()
        {
            Vector3F axis = new Vector3F(1, 0, 0);
            float angle1 = MathHelper.ToRadians(30.0f);
            float angle2 = MathHelper.ToRadians(750.0f);

            QuaternionF actual1 = QuaternionF.CreateFromAxisAngle(axis, angle1);
            QuaternionF actual2 = QuaternionF.CreateFromAxisAngle(axis, angle2);
            Assert.True(MathHelper.Equal(actual1, actual2), $"QuaternionF.CreateFromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
        }

        // A test for CreateFromAxisAngle (Vector3Ff, float)
        // CreateFromAxisAngle of angle = 30 && 390
        [Test]
        public void QuaternionFCreateFromAxisAngleTest3()
        {
            Vector3F axis = new Vector3F(1, 0, 0);
            float angle1 = MathHelper.ToRadians(30.0f);
            float angle2 = MathHelper.ToRadians(390.0f);

            QuaternionF actual1 = QuaternionF.CreateFromAxisAngle(axis, angle1);
            QuaternionF actual2 = QuaternionF.CreateFromAxisAngle(axis, angle2);
            actual1.X = -actual1.X;
            actual1.W = -actual1.W;

            Assert.True(MathHelper.Equal(actual1, actual2), $"QuaternionF.CreateFromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
        }

        [Test]
        public void QuaternionFCreateFromYawPitchRollTest1()
        {
            float yawAngle = MathHelper.ToRadians(30.0f);
            float pitchAngle = MathHelper.ToRadians(40.0f);
            float rollAngle = MathHelper.ToRadians(50.0f);

            QuaternionF yaw = QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, yawAngle);
            QuaternionF pitch = QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, pitchAngle);
            QuaternionF roll = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, rollAngle);

            QuaternionF expected = yaw * pitch * roll;
            QuaternionF actual = QuaternionF.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.QuaternionFCreateFromYawPitchRollTest1 did not return the expected value: expected {expected} actual {actual}");
        }

        // Covers more numeric rigions
        [Test]
        public void QuaternionFCreateFromYawPitchRollTest2()
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

                        QuaternionF yaw = QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, yawRad);
                        QuaternionF pitch = QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, pitchRad);
                        QuaternionF roll = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, rollRad);

                        QuaternionF expected = yaw * pitch * roll;
                        QuaternionF actual = QuaternionF.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.QuaternionFCreateFromYawPitchRollTest2 Yaw:{yawAngle} Pitch:{pitchAngle} Roll:{rollAngle} did not return the expected value: expected {expected} actual {actual}");
                    }
                }
            }
        }

        // A test for Slerp (QuaternionF, QuaternionF, float)
        [Test]
        public void QuaternionFSlerpTest()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 0.5f;

            QuaternionF expected = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0f));
            QuaternionF actual;

            actual = QuaternionF.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");

            // Case a and b are same.
            expected = a;
            actual = QuaternionF.Slerp(a, a, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionF, QuaternionF, float)
        // Slerp test where t = 0
        [Test]
        public void QuaternionFSlerpTest1()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 0.0f;

            QuaternionF expected = new QuaternionF(a.X, a.Y, a.Z, a.W);
            QuaternionF actual = QuaternionF.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionF, QuaternionF, float)
        // Slerp test where t = 1
        [Test]
        public void QuaternionFSlerpTest2()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 1.0f;

            QuaternionF expected = new QuaternionF(b.X, b.Y, b.Z, b.W);
            QuaternionF actual = QuaternionF.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionF, QuaternionF, float)
        // Slerp test where dot product is < 0
        [Test]
        public void QuaternionFSlerpTest3()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = -a;

            float t = 1.0f;

            QuaternionF expected = a;
            QuaternionF actual = QuaternionF.Slerp(a, b, t);
            // Note that in QuaternionF world, Q == -Q. In the case of QuaternionFs dot product is zero,
            // one of the QuaternionF will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as QuaternionF b but flipped.
            Assert.True(actual == expected, $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Slerp (QuaternionF, QuaternionF, float)
        // Slerp test where the QuaternionF is flipped
        [Test]
        public void QuaternionFSlerpTest4()
        {
            Vector3F axis = Vector3F.Normalize(new Vector3F(1.0f, 2.0f, 3.0f));
            QuaternionF a = QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0f));
            QuaternionF b = -QuaternionF.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0f));

            float t = 0.0f;

            QuaternionF expected = new QuaternionF(a.X, a.Y, a.Z, a.W);
            QuaternionF actual = QuaternionF.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Slerp did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for operator - (QuaternionF)
        [Test]
        public void QuaternionFUnaryNegationTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionF expected = new QuaternionF(-1.0f, -2.0f, -3.0f, -4.0f);
            QuaternionF actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.operator - did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Inverse (QuaternionF)
        [Test]
        public void QuaternionFInverseTest()
        {
            QuaternionF a = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(-0.0287356321f, -0.03448276f, -0.0402298868f, 0.04597701f);
            QuaternionF actual;

            actual = QuaternionF.Inverse(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Inverse (QuaternionF)
        // Invert zero length QuaternionF
        [Test]
        public void QuaternionFInverseTest1()
        {
            QuaternionF a = new QuaternionF();
            QuaternionF actual = QuaternionF.Inverse(a);

            Assert.True(float.IsNaN(actual.X) && float.IsNaN(actual.Y) && float.IsNaN(actual.Z) && float.IsNaN(actual.W)
                , $"QuaternionF.Inverse - did not return the expected value: expected {new QuaternionF(float.NaN, float.NaN, float.NaN, float.NaN)} actual {actual}");
        }

        // A test for ToString ()
        [Test]
        public void QuaternionFToStringTest()
        {
            QuaternionF target = new QuaternionF(-1.0f, 2.2f, 3.3f, -4.4f);

            string expected = string.Format(CultureInfo.CurrentCulture
                , "{{X:{0} Y:{1} Z:{2} W:{3}}}"
                , -1.0f, 2.2f, 3.3f, -4.4f);

            string actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        // A test for Add (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFAddTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(6.0f, 8.0f, 10.0f, 12.0f);
            QuaternionF actual;

            actual = QuaternionF.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for Divide (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFDivideTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);
            QuaternionF actual;

            actual = QuaternionF.Divide(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Divide did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Equals (object)
        [Test]
        public void QuaternionFEqualsTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for GetHashCode ()
        [Test]
        public void QuaternionFGetHashCodeTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

            int expected = unchecked(a.X.GetHashCode() + a.Y.GetHashCode() + a.Z.GetHashCode() + a.W.GetHashCode());
            int actual = a.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        // A test for Multiply (QuaternionF, float)
        [Test]
        public void QuaternionFMultiplyTest2()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            float Testor = 0.5f;

            QuaternionF expected = new QuaternionF(0.5f, 1.0f, 1.5f, 2.0f);
            QuaternionF actual;

            actual = QuaternionF.Multiply(a, Testor);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Multiply did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Multiply (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFMultiplyTest3()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 6.0f, 7.0f, 8.0f);

            QuaternionF expected = new QuaternionF(24.0f, 48.0f, 48.0f, -6.0f);
            QuaternionF actual;

            actual = QuaternionF.Multiply(a, b);
            Assert.True(MathHelper.Equal(expected, actual), $"QuaternionF.Multiply did not return the expected value: expected {expected} actual {actual}");
        }

        // A test for Negate (QuaternionF)
        [Test]
        public void QuaternionFNegateTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

            QuaternionF expected = new QuaternionF(-1.0f, -2.0f, -3.0f, -4.0f);
            QuaternionF actual;

            actual = QuaternionF.Negate(a);
            Assert.AreEqual(expected, actual);
        }

        // A test for Subtract (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFSubtractTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 6.0f, 7.0f, 4.0f);
            QuaternionF b = new QuaternionF(5.0f, 2.0f, 3.0f, 8.0f);

            QuaternionF expected = new QuaternionF(-4.0f, 4.0f, 4.0f, -4.0f);
            QuaternionF actual;

            actual = QuaternionF.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // A test for operator != (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFInequalityTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for operator == (QuaternionF, QuaternionF)
        [Test]
        public void QuaternionFEqualityTest()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Convert Identity matrix test
        [Test]
        public void QuaternionFFromRotationMatrixTest1()
        {
            Matrix4x4F matrix = Matrix4x4F.Identity;

            QuaternionF expected = new QuaternionF(0.0f, 0.0f, 0.0f, 1.0f);
            QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.Equal(expected, actual),
                $"QuaternionF.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionF.CreateFromQuaternionF did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Convert X axis rotation matrix
        [Test]
        public void QuaternionFFromRotationMatrixTest2()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4F matrix = Matrix4x4F.CreateRotationX(angle);

                QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);
                QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionF.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionF.CreateFromQuaternionF angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Convert Y axis rotation matrix
        [Test]
        public void QuaternionFFromRotationMatrixTest3()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4F matrix = Matrix4x4F.CreateRotationY(angle);

                QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle);
                QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionF.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionF.CreateFromQuaternionF angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Convert Z axis rotation matrix
        [Test]
        public void QuaternionFFromRotationMatrixTest4()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4F matrix = Matrix4x4F.CreateRotationZ(angle);

                QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle);
                QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionF.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionF.CreateFromQuaternionF angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Convert XYZ axis rotation matrix
        [Test]
        public void QuaternionFFromRotationMatrixTest5()
        {
            for (float angle = 0.0f; angle < 720.0f; angle += 10.0f)
            {
                Matrix4x4F matrix = Matrix4x4F.CreateRotationX(angle) * Matrix4x4F.CreateRotationY(angle) * Matrix4x4F.CreateRotationZ(angle);

                QuaternionF expected =
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle) *
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle) *
                    QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);

                QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    $"QuaternionF.CreateFromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    $"QuaternionF.CreateFromQuaternionF angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // X axis is most large axis case
        [Test]
        public void QuaternionFFromRotationMatrixWithScaledMatrixTest1()
        {
            float angle = MathHelper.ToRadians(180.0f);
            Matrix4x4F matrix = Matrix4x4F.CreateRotationY(angle) * Matrix4x4F.CreateRotationZ(angle);

            QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle) * QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle);
            QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionF.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionF.CreateFromQuaternionF did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Y axis is most large axis case
        [Test]
        public void QuaternionFFromRotationMatrixWithScaledMatrixTest2()
        {
            float angle = MathHelper.ToRadians(180.0f);
            Matrix4x4F matrix = Matrix4x4F.CreateRotationX(angle) * Matrix4x4F.CreateRotationZ(angle);

            QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitZ, angle) * QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);
            QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionF.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionF.CreateFromQuaternionF did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4F)
        // Z axis is most large axis case
        [Test]
        public void QuaternionFFromRotationMatrixWithScaledMatrixTest3()
        {
            float angle = MathHelper.ToRadians(180.0f);
            Matrix4x4F matrix = Matrix4x4F.CreateRotationX(angle) * Matrix4x4F.CreateRotationY(angle);

            QuaternionF expected = QuaternionF.CreateFromAxisAngle(Vector3F.UnitY, angle) * QuaternionF.CreateFromAxisAngle(Vector3F.UnitX, angle);
            QuaternionF actual = QuaternionF.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual),
                $"QuaternionF.CreateFromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4F m2 = Matrix4x4F.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2),
                $"QuaternionF.CreateFromQuaternionF did not return the expected value: matrix {matrix} m2 {m2}");
        }

        // A test for Equals (QuaternionF)
        [Test]
        public void QuaternionFEqualsTest1()
        {
            QuaternionF a = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);
            QuaternionF b = new QuaternionF(1.0f, 2.0f, 3.0f, 4.0f);

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

        // A test for Identity
        [Test]
        public void QuaternionFIdentityTest()
        {
            QuaternionF val = new QuaternionF(0, 0, 0, 1);
            Assert.AreEqual(val, QuaternionF.Identity);
        }

        // A test for IsIdentity
        [Test]
        public void QuaternionFIsIdentityTest()
        {
            Assert.True(QuaternionF.Identity.IsIdentity);
            Assert.True(new QuaternionF(0, 0, 0, 1).IsIdentity);
            Assert.False(new QuaternionF(1, 0, 0, 1).IsIdentity);
            Assert.False(new QuaternionF(0, 1, 0, 1).IsIdentity);
            Assert.False(new QuaternionF(0, 0, 1, 1).IsIdentity);
            Assert.False(new QuaternionF(0, 0, 0, 0).IsIdentity);
        }

        // A test for QuaternionF comparison involving NaN values
        [Test]
        public void QuaternionFEqualsNanTest()
        {
            QuaternionF a = new QuaternionF(float.NaN, 0, 0, 0);
            QuaternionF b = new QuaternionF(0, float.NaN, 0, 0);
            QuaternionF c = new QuaternionF(0, 0, float.NaN, 0);
            QuaternionF d = new QuaternionF(0, 0, 0, float.NaN);

            Assert.False(a == new QuaternionF(0, 0, 0, 0));
            Assert.False(b == new QuaternionF(0, 0, 0, 0));
            Assert.False(c == new QuaternionF(0, 0, 0, 0));
            Assert.False(d == new QuaternionF(0, 0, 0, 0));

            Assert.True(a != new QuaternionF(0, 0, 0, 0));
            Assert.True(b != new QuaternionF(0, 0, 0, 0));
            Assert.True(c != new QuaternionF(0, 0, 0, 0));
            Assert.True(d != new QuaternionF(0, 0, 0, 0));

            Assert.False(a.Equals(new QuaternionF(0, 0, 0, 0)));
            Assert.False(b.Equals(new QuaternionF(0, 0, 0, 0)));
            Assert.False(c.Equals(new QuaternionF(0, 0, 0, 0)));
            Assert.False(d.Equals(new QuaternionF(0, 0, 0, 0)));

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

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Test]
        public unsafe void QuaternionFSizeofTest()
        {
            Assert.AreEqual(16, sizeof(QuaternionF));
            Assert.AreEqual(32, sizeof(QuaternionF_2x));
            Assert.AreEqual(20, sizeof(QuaternionFPlusFloat));
            Assert.AreEqual(40, sizeof(QuaternionFPlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QuaternionF_2x
        {
            private QuaternionF _a;
            private QuaternionF _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QuaternionFPlusFloat
        {
            private QuaternionF _v;
            private readonly float _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QuaternionFPlusFloat_2x
        {
            private QuaternionFPlusFloat _a;
            private QuaternionFPlusFloat _b;
        }

        // A test to make sure the fields are laid out how we expect
        [Test]
        public unsafe void QuaternionFFieldOffsetTest()
        {
            QuaternionF quat = new QuaternionF();

            float* basePtr = &quat.X; // Take address of first element
            QuaternionF* quatPtr = &quat; // Take address of whole QuaternionF

            Assert.AreEqual(new IntPtr(basePtr), new IntPtr(quatPtr));

            Assert.AreEqual(new IntPtr(basePtr + 0), new IntPtr(&quat.X));
            Assert.AreEqual(new IntPtr(basePtr + 1), new IntPtr(&quat.Y));
            Assert.AreEqual(new IntPtr(basePtr + 2), new IntPtr(&quat.Z));
            Assert.AreEqual(new IntPtr(basePtr + 3), new IntPtr(&quat.W));
        }
    }
}
