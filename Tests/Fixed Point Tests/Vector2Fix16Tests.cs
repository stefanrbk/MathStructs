using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace MathStructs.Tests
{
    [TestFixture]
    public class Vector2Fix16Tests
    {
        [Test, Category("ToVector2Fix16")]
        public void SpanToVector2Fix16Test()
        {
            var span = new Fix16[] { (Fix16)4, (Fix16)7 }.AsSpan();
            var vec = span.ToVector2Fix16();

            Assert.That(vec.X, Is.EqualTo(span[0]));
            Assert.That(vec.Y, Is.EqualTo(span[1]));
        }

        [Test, Category("ToVector2Fix16")]
        public void ReadOnlySpanToVector2Fix16Test()
        {
            var span = new Fix16[] { (Fix16)4, (Fix16)7 }.ToImmutableArray().AsSpan();
            var vec = span.ToVector2Fix16();

            Assert.That(vec.X, Is.EqualTo(span[0]));
            Assert.That(vec.Y, Is.EqualTo(span[1]));
        }

        [Test, Category("ToVector2Fix16")]
        public void ArrayToVector2Fix16Test()
        {
            var array = new Fix16[] { (Fix16)4, (Fix16)7 };
            var vec = array.ToVector2Fix16();

            Assert.That(vec.X, Is.EqualTo(array[0]));
            Assert.That(vec.Y, Is.EqualTo(array[1]));
        }

        [Test, Category("One")]
        public void Vector2OneTest() =>
            Assert.That(Vector2Fix16.One, Is.EqualTo(new Fix16[] { Fix16.One, Fix16.One }.ToVector2Fix16()));

        [Test, Category("Zero")]
        public void Vector2ZeroTest() =>
            Assert.That(Vector2Fix16.Zero, Is.EqualTo(new Fix16[] { Fix16.Zero, Fix16.Zero }.ToVector2Fix16()));

        [Test, Category("UnitX")]
        public void Vector2UnitXTest() =>
            Assert.That(Vector2Fix16.UnitX, Is.EqualTo(new Fix16[] { Fix16.One, Fix16.Zero }.ToVector2Fix16()));

        [Test, Category("UnitY")]
        public void Vector2UnitYTest() =>
            Assert.That(Vector2Fix16.UnitY, Is.EqualTo(new Fix16[] { Fix16.Zero, Fix16.One }.ToVector2Fix16()));

        [Test, Category("With")]
        public void Vector2WithTest()
        {
            var v3 = new Vector2Fix16((Fix16)4, (Fix16)5);
            Assert.That(v3.X, Is.EqualTo((Fix16)4));
            Assert.That(v3.Y, Is.EqualTo((Fix16)5));
            v3 = v3.With((Fix16)1, (Fix16)2);
            var v4 = v3.With(y: (Fix16)0.5);
            Assert.That(v4.X, Is.EqualTo((Fix16)1));
            Assert.That(v4.Y, Is.EqualTo((Fix16)0.5));
            Assert.That(v3.Y, Is.EqualTo((Fix16)2));
        }

        [Test, Category("op_Addition")]
        public void Vector2AdditionTest()
        {
            var a = new Vector2Fix16((Fix16)1, (Fix16)2);
            var b = new Vector2Fix16((Fix16)3, (Fix16)4);

            var expected = new Vector2Fix16((Fix16)4, (Fix16)6);
            var actual = a + b;

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.operator + did not return the expected value.");
        }

        [Test, Category("Abs")]
        public void Vector2AbsTest()
        {
            var v1 = new Vector2Fix16((Fix16)(-2.5), (Fix16)2);
            var v3 = Vector2Fix16.Abs(new Vector2Fix16(Fix16.Zero, (Fix16)(-234)));
            var v = v1.Abs();
            Assert.That(v.X, Is.EqualTo((Fix16)2.5));
            Assert.That(v.Y, Is.EqualTo((Fix16)2));
            Assert.That(v3.X, Is.EqualTo(Fix16.Zero));
            Assert.That(v3.Y, Is.EqualTo((Fix16)234));
        }

        [Test, Category("Add")]
        public void Vector2AddTest()
        {
            var a = new Vector2Fix16((Fix16)1, (Fix16)2);
            var b = new Vector2Fix16((Fix16)3, (Fix16)4);

            var expected = new Vector2Fix16((Fix16)4, (Fix16)6);
            var actual = Vector2Fix16.Add(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Add did not return the expected value.");
        }

        [Test, Category("Clamp")]
        public void Vector2ClampTest()
        {
            var a = new Vector2Fix16((Fix16)0.5, (Fix16)0.3);
            var min = new Vector2Fix16(Fix16.Zero, (Fix16)0.1);
            var max = new Vector2Fix16(Fix16.One, (Fix16)1.1);

            // Normal case.
            // Case N1: specified value is in the range.
            var expected = new Vector2Fix16((Fix16)0.5, (Fix16)0.3);
            var actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N1 did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector2Fix16((Fix16)2, (Fix16)3);
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N2 did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector2Fix16((Fix16)(-1), (Fix16)(-2));
            expected = min;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N3 did not return the expected value.");

            // Case N4: combination case.
            a = new Vector2Fix16((Fix16)(-2), (Fix16)4);
            expected = new Vector2Fix16(min.X, max.Y);
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case N4 did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector2Fix16(Fix16.Zero, (Fix16)0.1);
            min = new Vector2Fix16(Fix16.One, (Fix16)1.1);

            // Case W1: specified value is in the range
            a = new Vector2Fix16((Fix16)0.5, (Fix16)0.3);
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W1 did not return the expected value.");

            // Case W2: specified value is bigger than max and min value.
            a = new Vector2Fix16((Fix16)2, (Fix16)3);
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W2 did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector2Fix16((Fix16)(-1), (Fix16)(-2));
            expected = max;
            actual = a.Clamp(min, max);
            Assert.That(actual, Is.EqualTo(expected), "Case W3 did not return the expected value.");
        }

        [Test, Category(".ctor")]
        public void Vector2ConstructorTest()
        {
            var x = Fix16.One;
            var y = (Fix16)2;

            var target = new Vector2Fix16(x, y);
            Assert.That(target.X, Is.EqualTo(Fix16.One), "target.X was not set correctly.");
            Assert.That(target.Y, Is.EqualTo((Fix16)2), "target.Y was not set correctly.");
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

            value = (Fix16)2;
            target = new Vector2Fix16(value);
            expected = new Vector2Fix16(value, value);
            Assert.That(target, Is.EqualTo(expected));
        }

        [Test, Category("CopyTo")]
        public void Vector2CopyToTest()
        {
            var v1 = new Vector2Fix16((Fix16)2, (Fix16)3);

            var a = new Fix16[3];
            var b = new Fix16[2];

            TestContext.Write(Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1)));
            TestContext.Write(Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length)));
            TestContext.Write(Assert.Throws<ArgumentException>(() => v1.CopyTo(a, 2)));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.That(a[0], Is.EqualTo(Fix16.Zero));
            Assert.That(a[1], Is.EqualTo((Fix16)2));
            Assert.That(a[2], Is.EqualTo((Fix16)3));
            Assert.That(b[0], Is.EqualTo((Fix16)2));
            Assert.That(b[1], Is.EqualTo((Fix16)3));
        }

        [Test, Category("DistanceSquared")]
        public void Vector2DistanceSquaredTest()
        {
            var a = new Vector2Fix16(Fix16.One, (Fix16)2);
            var b = new Vector2Fix16((Fix16)3, (Fix16)4);

            var expected = (Fix16)8;
            var actual = Vector2Fix16.DistanceSquared(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.DistanceSquared did not return the expected value.");
        }

        [Test, Category("Distance")]
        public void Vector2DistanceTest()
        {
            var a = new Vector2Fix16(Fix16.One, (Fix16)2);
            var b = new Vector2Fix16((Fix16)3, (Fix16)4);

            var expected = Fix16.Sqrt((Fix16)8);
            var actual = Vector2Fix16.Distance(a, b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Distance did not return the expected value.");
        }
        
        // Distance from the same point
        [Test, Category("Distance")]
        public void Vector2DistanceTest2()
        {
            var a = new Vector2Fix16((Fix16)1.05, (Fix16)2.05);
            var b = new Vector2Fix16((Fix16)1.05, (Fix16)2.05);

            var actual = Vector2Fix16.Distance(a, b);

            Assert.That(actual, Is.EqualTo(Fix16.Zero));
        }

        [Test, Category("Divide")]
        public void Vector2DivideTest()
        {
            var a = new Vector2Fix16((Fix16)1, (Fix16)2);
            var div = (Fix16)2;
            var expected = new Vector2Fix16((Fix16)0.5, (Fix16)1);
            var actual = Vector2Fix16.Divide(a, div);

            Assert.That(actual, Is.EqualTo(expected));

            actual = Vector2Fix16.Divide(a, (Fix16?)null);

            Assert.That(actual, Is.Null);

            actual = Vector2Fix16.Divide(null, div);

            Assert.That(actual, Is.Null);

            actual = Vector2Fix16.Divide(a, Fix16.Zero);

            Assert.That(actual, Is.Null);
        }

        [Test, Category("Divide")]
        public void Vector2DivideTest2()
        {
            var a = new Vector2Fix16((Fix16)1, (Fix16)6);
            var b = new Vector2Fix16((Fix16)5, (Fix16)2);

            var expected = new Vector2Fix16((Fix16)0.2, (Fix16)3);
            var actual = Vector2Fix16.Divide(a, b);

            Assert.That(actual, Is.EqualTo(expected));

            actual = Vector2Fix16.Divide(a, (Vector2Fix16?)null);

            Assert.That(actual, Is.Null);

            actual = Vector2Fix16.Divide(null, b);

            Assert.That(actual, Is.Null);

            var c = b.With(y: Fix16.Zero);
            actual = Vector2Fix16.Divide(a, c);

            Assert.That(actual, Is.Null);

            c = b.With(x: Fix16.Zero);
            actual = Vector2Fix16.Divide(a, c);

            Assert.That(actual, Is.Null);
        }

        [Test, Category("Division")]
        public void Vector2DivisionTest()
        {
            var a = new Vector2Fix16((Fix16)2, (Fix16)3);
            var div = (Fix16)2;

            var expected = new Vector2Fix16((Fix16)1, (Fix16)1.5);
            var actual = a / div;

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.operator / did not return the expected value.");
        }

        [Test, Category("Division")]
        public void Vector2DivisionTest2()
        {
            var a = new Vector2Fix16((Fix16)2, (Fix16)3);
            var b = new Vector2Fix16((Fix16)4, (Fix16)5);

            var expected = new Vector2Fix16((Fix16)0.5, (Fix16)0.6);
            var actual = a / b;

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.operator / did not return the expected value.");
        }

        [Test, Category("Dot")]
        public void Vector2DotTest()
        {
            var a = new Vector2Fix16(Fix16.One, (Fix16)2);
            var b = new Vector2Fix16((Fix16)3, (Fix16)4);

            var expected = (Fix16)11;
            var actual = a.Dot(b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Dot did not return the expected value.");
        }

        [Test, Category("Dot")]
        public void Vector2DotTest2()
        {
            var a = new Vector2Fix16((Fix16)1.55, (Fix16)1.55);
            var b = new Vector2Fix16((Fix16)(-1.55), (Fix16)1.55);

            var expected = (Fix16)0;
            var actual = a.Dot(b);

            Assert.That(actual, Is.EqualTo(expected), "Vector2Fix16.Dot did not return the expected value.");
        }
    }
}
