using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class MathTesting
    {
        private readonly Matrix4x4 AlteredIdentity = new Matrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);
        private readonly Matrix4x4 Pow24x4 = new Matrix4x4(1, 2, 4, 0, 8, 16, 32, 0, 64, 128, 256, 0, 0, 0, 0, 0);
        private readonly Matrix3x3D Pow23x3D = new Matrix3x3D(1, 2, 4, 8, 16, 32, 64, 128, 256);

        private bool Equal(Matrix3x3D left, Matrix4x4 right) =>
            (float)left.M11 == right.M11 &&
            (float)left.M12 == right.M12 &&
            (float)left.M13 == right.M13 &&
            (float)left.M21 == right.M21 &&
            (float)left.M22 == right.M22 &&
            (float)left.M23 == right.M23 &&
            (float)left.M31 == right.M31 &&
            (float)left.M32 == right.M32 &&
            (float)left.M33 == right.M33;
        [Test]
        public void IdentityAddTest()
        {
            var actual = Matrix3x3D.Identity + Matrix3x3D.Identity;
            var expected = AlteredIdentity + AlteredIdentity;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void IdentitySubtractTest()
        {
            var actual = Matrix3x3D.Identity - Matrix3x3D.Identity;
            var expected = AlteredIdentity - AlteredIdentity;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void IdentityNegateTest()
        {
            var actual = -Matrix3x3D.Identity;
            var expected = -AlteredIdentity;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void IdentityMultiplyTest()
        {
            var actual = Matrix3x3D.Identity * Matrix3x3D.Identity;
            var expected = AlteredIdentity * AlteredIdentity;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void IdentityMultiplyScalarTest()
        {
            var actual = Matrix3x3D.Identity *4;
            var expected = AlteredIdentity *4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void Pow2AddTest()
        {
            var actual = Pow23x3D + Pow23x3D;
            var expected = Pow24x4 + Pow24x4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void Pow2SubtractTest()
        {
            var actual = Pow23x3D - Pow23x3D;
            var expected = Pow24x4 - Pow24x4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void Pow2NegateTest()
        {
            var actual = -Pow23x3D;
            var expected = -Pow24x4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void Pow2MultiplyTest()
        {
            var actual = Pow23x3D * Pow23x3D;
            var expected = Pow24x4 * Pow24x4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
        [Test]
        public void Pow2MultiplyScalarTest()
        {
            var actual = Pow23x3D *4;
            var expected = Pow24x4 *4;

            Console.WriteLine(actual.ToString());
            Console.WriteLine(expected.ToString());

            Assert.That(Equal(actual, expected));
        }
    }
}
