using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace MathStructs.Tests
{
    [TestFixture]
    public class Vector2Fix16Tests
    {
        [Test, Category("op_Explicit")]
        public void CastTest()
        {
            var span = new int[] { 4, 7 }.AsSpan();
            var vec = span.ToVector2Fix16();

            Assert.That(vec.X, Is.EqualTo(span[0]));
            Assert.That(vec.Y, Is.EqualTo(span[1]));
        }

        [Test, Category("With")]
        public void WithTest()
        {
            var v3 = new Vector2Fix16(4.0.ToFix16(), 5.0.ToFix16());
            Assert.That(v3.X, Is.EqualTo(4.0.ToFix16()));
            Assert.That(v3.Y, Is.EqualTo(5.0.ToFix16()));
            v3 = v3.With(1.0.ToFix16(), 2.0.ToFix16());
            var v4 = v3.With(y: 0.5.ToFix16());
            Assert.That(v4.X, Is.EqualTo(Fix16.FromDouble(1.0)));
            Assert.That(v4.Y, Is.EqualTo(Fix16.FromDouble(0.5)));
            Assert.That(v3.Y, Is.EqualTo(Fix16.FromDouble(2.0)));
        }

        [Test, Category("op_Addition")]
        public void Vector2AdditionTest()
        {
            var a = new Vector2Fix16(1.0.ToFix16(), 2.0.ToFix16());
            var b = new Vector2Fix16(3.0.ToFix16(), 4.0.ToFix16());

            var expected = new Vector2Fix16(4.0.ToFix16(), 6.0.ToFix16());
            var actual = a + b;

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.operator + did not return the expected value.");
        }

        [Test, Category("Abs")]
        public void Vector2AbsTest()
        {
            var v1 = new Vector2Fix16(-2.5.ToFix16(), 2.0.ToFix16());
            var v3 = Vector2Fix16.Abs(new Vector2Fix16(Fix16.Zero, -234.0.ToFix16()));
            var v = v1.Abs();
            Assert.That(v.X, Is.EqualTo(2.5.ToFix16()));
            Assert.That(v.Y, Is.EqualTo(2.0.ToFix16()));
            Assert.That(v3.X, Is.EqualTo(Fix16.Zero));
            Assert.That(v3.Y, Is.EqualTo(234.0.ToFix16()));
        }

        [Test, Category("Add")]
        public void Vector2AddTest()
        {
            var a = new Vector2Fix16(1.0.ToFix16(), 2.0.ToFix16());
            var b = new Vector2Fix16(3.0.ToFix16(), 4.0.ToFix16());

            var expected = new Vector2Fix16(4.0.ToFix16(), 6.0.ToFix16());
            var actual = Vector2Fix16.Add(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Add did not return the expected value.");
        }

        [Test, Category("Clamp")]
        public void Vector2ClampTest()
        {
            var a = new Vector2Fix16(0.5.ToFix16(), 0.3.ToFix16());
            var min = new Vector2Fix16(Fix16.Zero, 0.1.ToFix16());
            var max = new Vector2Fix16(Fix16.One, 1.1.ToFix16());

            // Normal case.
            // Case N1: specified value is in the range.
            var expected = new Vector2Fix16(0.5.ToFix16(), 0.3.ToFix16());
            var actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N1 did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector2Fix16(2.ToFix16(), 3.ToFix16());
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N2 did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector2Fix16(-1.ToFix16(), -2.ToFix16());
            expected = min;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N3 did not return the expected value.");

            // Case N4: combination case.
            a = new Vector2Fix16(-2.ToFix16(), 4.ToFix16());
            expected = new Vector2Fix16(min.X, max.Y);
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N4 did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector2Fix16(Fix16.Zero, 0.1.ToFix16());
            min = new Vector2Fix16(Fix16.One, 1.1.ToFix16());

            // Case W1: specified value is in the range
            a = new Vector2Fix16(0.5.ToFix16(), 0.3.ToFix16());
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W1 did not return the expected value.");

            // Case W2: specified value is bigger than max and min value.
            a = new Vector2Fix16(2.ToFix16(), 3.ToFix16());
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W2 did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector2Fix16(-1.ToFix16(), -2.ToFix16());
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W3 did not return the expected value.");
        }

        [Test, Category(".ctor")]
        public void Vector2ConstructorTest()
        {
            var x = Fix16.One;
            var y = 2.ToFix16();

            var target = new Vector2Fix16(x, y);
            Assert.That(target.X, Is.EqualTo(Fix16.One), "target.X was not set correctly.");
            Assert.That(target.Y, Is.EqualTo(2.ToFix16()), "target.Y was not set correctly.");
        }

        [Test, Category(".ctor")]
        public void Vector2ConstructorTest2()
        {
            var target = new Vector2Fix16();
            Assert.That(target.X, Is.EqualTo(Fix16.Zero), "target.X was not set correctly.");
            Assert.That(target.Y, Is.EqualTo(Fix16.Zero), "target.Y was not set correctly.");
        }

        [Test, Category(".ctor")]
        public void Vector2ConstructorTest3()
        {
            var value = Fix16.One;
            var target = new Vector2Fix16(value);

            var expected = new Vector2Fix16(value, value);
            Assert.That(target, Is.EqualTo(expected));

            value = 2.ToFix16();
            target = new Vector2Fix16(value);
            expected = new Vector2Fix16(value, value);
            Assert.That(target, Is.EqualTo(expected));
        }

        [Test, Category("CopyTo")]
        public void Vector2CopyToTest()
        {
            var v1 = new Vector2Fix16(2.ToFix16(), 3.ToFix16());

            var a = new int[3];
            var b = new int[2];

            TestContext.Write(Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1)));
            TestContext.Write(Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length)));
            TestContext.Write(Assert.Throws<ArgumentException>(() => v1.CopyTo(a, 2)));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.That(a[0], Is.EqualTo(Fix16.Zero));
            Assert.That(a[1], Is.EqualTo(2.ToFix16()));
            Assert.That(a[2], Is.EqualTo(3.ToFix16()));
            Assert.That(b[0], Is.EqualTo(2.ToFix16()));
            Assert.That(b[1], Is.EqualTo(3.ToFix16()));
        }

        [Test, Category("DistanceSquared")]
        public void Vector2DistanceSquaredTest()
        {
            var a = new Vector2Fix16(Fix16.One, 2.ToFix16());
            var b = new Vector2Fix16(3.ToFix16(), 4.ToFix16());

            var expected = 8.ToFix16();
            var actual = Vector2Fix16.DistanceSquared(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.DistanceSquared did not return the expected value.");
        }

        [Test, Category("Distance")]
        public void Vector2DistanceTest()
        {
            var a = new Vector2Fix16(Fix16.One, 2.ToFix16());
            var b = new Vector2Fix16(3.ToFix16(), 4.ToFix16());

            var expected = Math.Sqrt(8).ToFix16();
            var actual = Vector2Fix16.Distance(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Distance did not return the expected value.");
        }

        [Test, Category("Dot")]
        public void Vector2DotTest()
        {
            var a = new Vector2Fix16(Fix16.One, 2.ToFix16());
            var b = new Vector2Fix16(3.ToFix16(), 4.ToFix16());

            var expected = 11.ToFix16();
            var actual = a.Dot(b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Dot did not return the expected value.");
        }
    }
}
