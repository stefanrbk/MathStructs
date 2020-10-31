using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Tests
{
    [TestFixture(TestOf = typeof(Vector2F))]
    public class Vector2FTests
    {
        const float EPS = float.Epsilon * 5;
        private IEqualityComparer<Vector2F> v2fComparer = new Vector2FEqualityComparer();
        private class Vector2FEqualityComparer : IEqualityComparer<Vector2F>
        {
            public bool Equals([AllowNull]Vector2F x, [AllowNull]Vector2F y) =>
                x is Vector2F a && y is Vector2F b && MathF.Abs(a.X - b.X) < EPS && MathF.Abs(a.Y - b.Y) < EPS;

            public int GetHashCode([DisallowNull] Vector2F obj)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void AbsSqrt()
        {
            var a = new Vector2F(11, 8);
            var b = new Vector2F(11, 2);
            var d = a * b;
            var s = Vector2F.SquareRoot(d);
            s *= -1;
            s = s.Abs();
            Assert.That(s, Is.EqualTo(new Vector2F(11, 4)).Using(v2fComparer));
        }

        [Test]
        public void BoxUnbox()
        {
            var a = Vector2F.One;
            object b = a;
            Assert.That(a, Is.EqualTo(b).Using(v2fComparer));
        }

        [Test]
        public void Ctors()
        {
            var a = new Vector2F(2, 2);
            var b = new Vector2F(2);
            Assert.That(a, Is.EqualTo(b).Using(v2fComparer));
        }

        [Test]
        public void MinMax()
        {
            var a = new Vector2F(10, -100);
            var b = new Vector2F(5);
            var c = Vector2F.Max(a, b);
            Assert.That(c.Y, Is.EqualTo(5f).Within(EPS));
            Assert.That(c.X, Is.EqualTo(10f).Within(EPS));
            var d = Vector2F.Min(a, b);
            var q = Vector2F.Min(d, d);
            Assert.That(q, Is.EqualTo(d).Using(v2fComparer));
            Assert.That(d.Y, Is.EqualTo(-100f).Within(EPS));
        }

        [Test]
        public void Add()
        {
            var a = new Vector2F(2);
            var b = new Vector2F(1);
            var c = a + b;
            Assert.That(c, Is.EqualTo(new Vector2F(3)).Using(v2fComparer));
        }

        [Test]
        public void Sub()
        {
            var a = new Vector2F(3);
            var b = new Vector2F(2);
            var c = a - b;
            Assert.That(c, Is.EqualTo(Vector2F.One).Using(v2fComparer));
        }

        [Test]
        public void Mul()
        {
            var a = new Vector2F(2);
            var b = new Vector2F(3);
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector2F(6)).Using(v2fComparer));
        }

        [Test]
        public void MulScalar()
        {
            var a = new Vector2F(2);
            var b = 2;
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector2F(4)).Using(v2fComparer));
        }

        [Test]
        public void ScalarMul()
        {
            var a = 5;
            var b = new Vector2F(2);
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector2F(10)).Using(v2fComparer));
        }

        [Test]
        public void Div()
        {
            var a = new Vector2F(2);
            var b = new Vector2F(3);
            var c = a / b;
            Assert.That(c, Is.EqualTo(new Vector2F(2f/3)).Using(v2fComparer));
        }

        [Test]
        public void DivScalar()
        {
            var a = new Vector2F(2);
            var b = 5;
            var c = a / b;
            Assert.That(c, Is.EqualTo(new Vector2F(2f/5)).Using(v2fComparer));
        }

        [Test]
        public void Sum()
        {
            var a = new Vector2F(1, 2);
            var b = new Vector2F(2, 2);
            var c = 33f;
            var d = (b + a) * c;
            var q = d + a;
            Assert.That(q.X, Is.EqualTo(100f).Within(EPS));
        }

        [Test]
        public void Abs()
        {
            var a = new Vector2F(-1);
            var b = a.Abs();
            Assert.That(b, Is.EqualTo(Vector2F.One).Using(v2fComparer));
        }

        [Test]
        public void Dot()
        {
            var a = new Vector2F(3);
            var b = new Vector2F(2);
            var c = Vector2F.Dot(a, b);

            Assert.That(c, Is.EqualTo(12f).Within(EPS));
        }

        [Test]
        public void Init()
        {
            var v4 = new Vector2F(2);
            var result = Vector2F.Dot(v4, v4);

            Assert.That(result, Is.EqualTo(2f * 2f * 2f).Within(EPS));
        }

        [Test]
        public void InitN()
        {
            var v4 = new Vector2F(0.5f, -0.5f);

            Assert.That(v4.X, Is.EqualTo(0.5f).Within(EPS), "Vector2F.X failed");
            Assert.That(v4.Y, Is.EqualTo(-0.5f).Within(EPS), "Vector2F.Y failed");

            var message = "Vector2F evaluation order failed.";
            Assert.That(v4.X, Is.GreaterThan(v4.Y).Within(EPS), message);
        }

        [Test]
        public void Max()
        {
            var a = new Vector2F(2);
            var b = new Vector2F(3);
            var c = Vector2F.Max(a, b);

            Assert.That(c, Is.EqualTo(new Vector2F(3)).Using(v2fComparer));
        }

        [Test]
        public void Min()
        {
            var a = new Vector2F(2);
            var b = new Vector2F(3);
            var c = Vector2F.Min(a, b);

            Assert.That(c, Is.EqualTo(new Vector2F(2)).Using(v2fComparer));
        }

        [Test]
        public void Return()
        {
            var r = new Random();
            var array = new Vector2F[10];
            for (var i = 0; i < 10; i++)
                array[i] = new Vector2F(r.Next(100));
            var v0 = new Vector2F(r.Next(100));
            var v1 = new Vector2F(r.Next(100));
            var v2 = new Vector2F(r.Next(100));
            var v3 = new Vector2F(r.Next(100));

            var result = f2(0.7f);
            var expected = f1(0.7f);

            Assert.That(result, Is.EqualTo(expected).Using(v2fComparer));

            Vector2F f1(float t)
            {
                var ti = 1 - t;
                var t0 = ti * ti * ti;
                var t1 = 3 * ti * ti * t;
                var t2 = 3 * ti * t * t;
                var t3 = 3 * t * t * t;
                return (t0 * v0) + (t1 * v1) + (t2 * v2) + (t3 * v3);
            }

            Vector2F f2(float u) =>
                u < 0 ? array![0] :
                u >= 1 ? array![1] :
                u < 0.1 ? array![2] :
                u > 0.9 ? array![3] : f1(u);
        }
    }
}
