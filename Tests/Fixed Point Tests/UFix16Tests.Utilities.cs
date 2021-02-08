using System.Globalization;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix16Tests
    {
        #region ToString

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(( (UFix16)2.5 ).ToString(), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator")]
        [Category("ToString")]
        public void ToStringTest1(UFix16 value, string expected, CultureInfo culture) =>
            Assert.That(value.ToString("#,0.################", culture), Is.EqualTo(expected));

        #endregion ToString

        #region Equals

        [TestCaseSource("EqualsSource1")]
        [Category("Equals")]
        public void EqualsTest(object value, bool expected) =>
            Assert.That(UFix16.One.Equals(value), Is.EqualTo(expected));

        [TestCaseSource("EqualsSource2")]
        public void EqualsTest2(UFix16 value, UFix16 delta, bool expected) =>
            Assert.That(UFix16.Equals(UFix16.One, value, delta), Is.EqualTo(expected));

        #endregion Equals

        #region CompareTo

        [TestCaseSource("CompareToSource1")]
        [Category("CompareTo")]
        public void CompareToTest(object? value, IResolveConstraint expected) =>
            Assert.That(UFix16.One.CompareTo(value), expected);

        [Test, Category("CompareTo")]
        public void CompareTo_is_not_Fix16_throws_ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(() => UFix16.One.CompareTo(UInt32.MinValue));

        [TestCaseSource("CompareToSource2")]
        [Category("CompareTo")]
        public void CompareToTest2(UFix16 value, IResolveConstraint expected) =>
            Assert.That(UFix16.One.CompareTo(value), expected);

        #endregion CompareTo

        #region GetHashCode

        [Test]
        [Category("GetHashCode")]
        public void GetHashCodeTest() =>
            Assert.That(UFix16.Pi.GetHashCode(), Is.EqualTo(HashCode.Combine(205887, 3)));

        #endregion GetHashCode
    }
}