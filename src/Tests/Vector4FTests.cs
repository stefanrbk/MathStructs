using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Tests
{
    [TestFixture(TestOf = typeof(Vector4F))]
    public class Vector4FTests
    {
        const float EPS = float.Epsilon * 5;
        private IEqualityComparer<Vector4F> v4fComparer = new Vector4FEqualityComparer();
        private class Vector4FEqualityComparer : IEqualityComparer<Vector4F>
        {
            public bool Equals([AllowNull]Vector4F x, [AllowNull]Vector4F y) =>
                x is Vector4F a && y is Vector4F b && MathF.Abs(a.X - b.X) < EPS && MathF.Abs(a.Y - b.Y) < EPS && Math.Abs(a.Z - b.Z) < EPS && Math.Abs(a.W - b.W) < EPS;

            public int GetHashCode([DisallowNull] Vector4F obj)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void AbsSqrt()
        {
            var a = new Vector4F(11, 13, 8, 4);
            var b = new Vector4F(11, 13, 2, 1);
            var d = a * b;
            var s = Vector4F.SquareRoot(d);
            s *= -1;
            s = s.Abs();
            Assert.That(s, Is.EqualTo(new Vector4F(11, 13, 4, 2)).Using(v4fComparer));
        }

        [Test]
        public void BoxUnbox()
        {
            var a = Vector4F.One;
            object b = a;
            Assert.That(a, Is.EqualTo(b).Using(v4fComparer));
        }

        [Test]
        public void Ctors()
        {
            var a = new Vector4F(2, 2, 2, 2);
            var b = new Vector4F(2);
            Assert.That(a, Is.EqualTo(b).Using(v4fComparer));

            var w = new Vector3F(5);
            var d = new Vector4F(w, 2);
            var q = new Vector4F(5, 5, 5, 2);
            Assert.That(d, Is.EqualTo(q).Using(v4fComparer));
        }

        [Test]
        public void MinMax()
        {
            var a = new Vector4F(10, 50, 0, -100);
            var b = new Vector4F(10);
            var c = Vector4F.Max(a, b);
            Assert.That(c.Y, Is.EqualTo(50f).Within(EPS));
            Assert.That(c.W, Is.EqualTo(10f).Within(EPS));
            var d = Vector4F.Min(a, b);
            var q = Vector4F.Min(d, d);
            Assert.That(q, Is.EqualTo(d).Using(v4fComparer));
            Assert.That(d.W, Is.EqualTo(-100f).Within(EPS));
        }

        [Test]
        public void Add()
        {
            var a = new Vector4F(2);
            var b = new Vector4F(1);
            var c = a + b;
            Assert.That(c, Is.EqualTo(new Vector4F(3)).Using(v4fComparer));
        }

        [Test]
        public void Sub()
        {
            var a = new Vector4F(3);
            var b = new Vector4F(2);
            var c = a - b;
            Assert.That(c, Is.EqualTo(Vector4F.One).Using(v4fComparer));
        }

        [Test]
        public void Mul()
        {
            var a = new Vector4F(2);
            var b = new Vector4F(3);
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector4F(6)).Using(v4fComparer));
        }

        [Test]
        public void MulScalar()
        {
            var a = new Vector4F(2);
            var b = 2;
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector4F(4)).Using(v4fComparer));
        }

        [Test]
        public void ScalarMul()
        {
            var a = 5;
            var b = new Vector4F(2);
            var c = a * b;
            Assert.That(c, Is.EqualTo(new Vector4F(10)).Using(v4fComparer));
        }

        [Test]
        public void Div()
        {
            var a = new Vector4F(2);
            var b = new Vector4F(3);
            var c = a / b;
            Assert.That(c, Is.EqualTo(new Vector4F(2f/3)).Using(v4fComparer));
        }

        [Test]
        public void DivScalar()
        {
            var a = new Vector4F(2);
            var b = 5;
            var c = a / b;
            Assert.That(c, Is.EqualTo(new Vector4F(2f/5)).Using(v4fComparer));
        }

        [Test]
        public void Sum()
        {
            var a = new Vector4F(1, 2, 3, 4);
            var b = new Vector4F(2, 2, 1, 1);
            var c = 33f;
            var d = (b + a) * c;
            var q = d + a;
            Assert.That(q.X, Is.EqualTo(100f).Within(EPS));
        }

        [Test]
        public void Abs()
        {
            var a = new Vector4F(-1);
            var b = a.Abs();
            Assert.That(b, Is.EqualTo(Vector4F.One).Using(v4fComparer));
        }

        [Test]
        public void Dot()
        {
            var a = new Vector4F(3);
            var b = new Vector4F(2);
            var c = Vector4F.Dot(a, b);

            Assert.That(c, Is.EqualTo(24f).Within(EPS));
        }

        [Test]
        public void Init()
        {
            var v4 = new Vector4F(2);
            var result = Vector4F.Dot(v4, v4);

            Assert.That(result, Is.EqualTo(4f * 2f * 2f).Within(EPS));
        }

        [Test]
        public void InitN()
        {
            var v4 = new Vector4F(0.5f, -0.5f, 0f, 1.0f);

            Assert.That(v4.X, Is.EqualTo(0.5f).Within(EPS), "Vector4F.X failed");
            Assert.That(v4.Y, Is.EqualTo(-0.5f).Within(EPS), "Vector4F.Y failed");
            Assert.That(v4.Z, Is.EqualTo(0f).Within(EPS), "Vector4F.Z failed");
            Assert.That(v4.W, Is.EqualTo(1f).Within(EPS), "Vector4F.W failed");

            var message = "Vector4F evaluation order failed.";
            Assert.That(v4.X, Is.GreaterThan(v4.Y).Within(EPS), message);
            Assert.That(v4.Y, Is.LessThan(v4.Z).Within(EPS), message);
            Assert.That(v4.Z, Is.LessThan(v4.W).Within(EPS), message);
        }

        [Test]
        public void Max()
        {
            var a = new Vector4F(2);
            var b = new Vector4F(3);
            var c = Vector4F.Max(a, b);

            Assert.That(c, Is.EqualTo(new Vector4F(3)).Using(v4fComparer));
        }

        [Test]
        public void Min()
        {
            var a = new Vector4F(2);
            var b = new Vector4F(3);
            var c = Vector4F.Min(a, b);

            Assert.That(c, Is.EqualTo(new Vector4F(2)).Using(v4fComparer));
        }

        [Test]
        public void Return()
        {
            var r = new Random();
            var array = new Vector4F[10];
            for (var i = 0; i < 10; i++)
                array[i] = new Vector4F(r.Next(100));
            var v0 = new Vector4F(r.Next(100));
            var v1 = new Vector4F(r.Next(100));
            var v2 = new Vector4F(r.Next(100));
            var v3 = new Vector4F(r.Next(100));

            var result = f2(0.7f);
            var expected = f1(0.7f);

            Assert.That(result, Is.EqualTo(expected).Using(v4fComparer));

            Vector4F f1(float t)
            {
                var ti = 1 - t;
                var t0 = ti * ti * ti;
                var t1 = 3 * ti * ti * t;
                var t2 = 3 * ti * t * t;
                var t3 = 3 * t * t * t;
                return (t0 * v0) + (t1 * v1) + (t2 * v2) + (t3 * v3);
            }

            Vector4F f2(float u) =>
                u < 0 ? array![0] :
                u >= 1 ? array![1] :
                u < 0.1 ? array![2] :
                u > 0.9 ? array![3] : f1(u);
        }
    }
}
