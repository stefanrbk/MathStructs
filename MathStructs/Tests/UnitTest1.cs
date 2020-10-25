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
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
            var expected = -m;
            Matrix3x3D.AllowSse = true;
            var actual = -m;

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = -m;

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Add()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
            var expected = m + n;
            Matrix3x3D.AllowSse = true;
            var actual = m + n;

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m + n;

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Subtract()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
            var expected = m - n;
            Matrix3x3D.AllowSse = true;
            var actual = m - n;

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m - n;

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Multiply()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
            var expected = m * n;
            Matrix3x3D.AllowSse = true;
            var actual = m * n;

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m * n;

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void MultiplyScalar()
        {
            var rand = new Random();
            var m = new Matrix3x3D(rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next(), rand.Next());
            var n = rand.NextDouble();
            Matrix3x3D.AllowAvx = false;
            Matrix3x3D.AllowSse = false;
            var expected = m * n;
            Matrix3x3D.AllowSse = true;
            var actual = m * n;

            Assert.That(actual, Is.EqualTo(expected));

            Matrix3x3D.AllowAvx = true;
            actual = m * n;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}