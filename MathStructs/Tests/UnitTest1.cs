using MathStructs;

using NUnit.Framework;

using System;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Negate()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
#endif
            var expected = -m;
#if DEBUG
            Matrix3x3D.AllowSse = true;
#endif
            var actual = -m;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = -m;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Add()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
#endif
            var expected = m + n;
#if DEBUG
            Matrix3x3D.AllowSse = true;
#endif
            var actual = m + n;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m + n;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Subtract()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
#endif
            var expected = m - n;
#if DEBUG
            Matrix3x3D.AllowSse = true;
#endif
            var actual = m - n;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m - n;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Multiply()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
#endif
            var expected = m * n;
#if DEBUG
            Matrix3x3D.AllowSse = true;
#endif
            var actual = m * n;

#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m * n;
#endif

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void MultiplyScalar()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = rand.NextDouble();
#if DEBUG
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
#endif
            var expected = m * n;
#if DEBUG
            Matrix3x3D.AllowSse = true;
#endif
            var actual = m * n;
#if DEBUG

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m * n;
#endif

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void NegateF()
        {
            var rand = new Random();
            var m = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3F.AllowAvx = false;
            Matrix3x3F.AllowSse = false;
#endif
            var expected = -m;
#if DEBUG
            Matrix3x3F.AllowSse = true;
#endif
            var actual = -m;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3F.AllowAvx = true;
            actual = -m;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void AddF()
        {
            var rand = new Random();
            var m = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3F.AllowAvx = false;
            Matrix3x3F.AllowSse = false;
#endif
            var expected = m + n;
#if DEBUG
            Matrix3x3F.AllowSse = true;
#endif
            var actual = m + n;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3F.AllowAvx = true;
            actual = m + n;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void SubtractF()
        {
            var rand = new Random();
            var m = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3F.AllowAvx = false;
            Matrix3x3F.AllowSse = false;
#endif
            var expected = m - n;
#if DEBUG
            Matrix3x3F.AllowSse = true;
#endif
            var actual = m - n;
#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3F.AllowAvx = true;
            actual = m - n;
#endif
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void MultiplyF()
        {
            var rand = new Random();
            var m = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
#if DEBUG
            Matrix3x3F.AllowAvx = false;
            Matrix3x3F.AllowSse = false;
#endif
            var expected = m * n;
#if DEBUG
            Matrix3x3F.AllowSse = true;
#endif
            var actual = m * n;

#if DEBUG
            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3F.AllowAvx = true;
            actual = m * n;
#endif

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void MultiplyScalarF()
        {
            var rand = new Random();
            var m = new Matrix3x3F(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = (float)rand.NextDouble();
#if DEBUG
            Matrix3x3F.AllowAvx = false;
            Matrix3x3F.AllowSse = false;
#endif
            var expected = m * n;
#if DEBUG
            Matrix3x3F.AllowSse = true;
#endif
            var actual = m * n;
#if DEBUG

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3F.AllowAvx = true;
            actual = m * n;
#endif

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}