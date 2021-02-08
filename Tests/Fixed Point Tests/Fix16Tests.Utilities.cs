using System.Globalization;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class Fix16Tests
    {
        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(((Fix16)2.5).ToString(), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator"), Category("ToString")]
        public void ToStringTest1(Fix16 value, string expected, CultureInfo culture) =>
            Assert.That(value.ToString("#,0.################", culture), Is.EqualTo(expected));

        [TestCaseSource("EqualsSource1"), Category("Equals")]
        public void EqualsTest(object value, bool expected) =>
            Assert.That(Fix16.One.Equals(value), Is.EqualTo(expected));

        [TestCaseSource("EqualsSource2")]
        public void EqualsTest2(Fix16 value, Fix16 delta, bool expected) =>
            Assert.That(Fix16.Equals(Fix16.One, value, delta), Is.EqualTo(expected));

        [TestCaseSource("CompareToSource1"), Category("CompareTo")]
        public void CompareToTest(object? value, IResolveConstraint expected) =>
            Assert.That(Fix16.One.CompareTo(value), expected);

        [Test, Category("CompareTo")]
        public void CompareTo_is_not_Fix16_throws_ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(() => Fix16.One.CompareTo(Int32.MinValue));

        [TestCaseSource("CompareToSource2"), Category("CompareTo")]
        public void CompareToTest2(Fix16 value, IResolveConstraint expected) =>
            Assert.That(Fix16.One.CompareTo(value), expected);

        [Test, Category("GetHashCode")]
        public void GetHashCodeTest() =>
            Assert.That(Fix16.Pi.GetHashCode(), Is.EqualTo(HashCode.Combine(205887, 2)));
    }
}