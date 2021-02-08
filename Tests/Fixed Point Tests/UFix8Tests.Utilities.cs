using System.Globalization;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix8Tests
    {
        #region ToString

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(( (UFix16)2.5 ).ToString(), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator")]
        [Category("ToString")]
        public void ToStringTest1(UFix8 value, string expected, CultureInfo culture) =>
            Assert.That(value.ToString("#,0.################", culture), Is.EqualTo(expected));

        #endregion ToString

        #region Equals

        [TestCaseSource("EqualsSource1")]
        [Category("Equals")]
        public void EqualsTest(object value, bool expected) =>
            Assert.That(UFix8.One.Equals(value), Is.EqualTo(expected));

        [TestCaseSource("EqualsSource2")]
        public void EqualsTest2(UFix8 value, UFix8 delta, bool expected) =>
            Assert.That(UFix8.Equals(UFix8.One, value, delta), Is.EqualTo(expected));

        #endregion Equals

        #region CompareTo

        [TestCaseSource("CompareToSource1")]
        [Category("CompareTo")]
        public void CompareToTest(object? value, IResolveConstraint expected) =>
            Assert.That(UFix8.One.CompareTo(value), expected);

        [Test, Category("CompareTo")]
        public void CompareTo_is_not_UFix8_throws_ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(() => UFix8.One.CompareTo(UInt16.MinValue));

        [TestCaseSource("CompareToSource2")]
        [Category("CompareTo")]
        public void CompareToTest2(UFix8 value, IResolveConstraint expected) =>
            Assert.That(UFix8.One.CompareTo(value), expected);

        #endregion CompareTo

        #region GetHashCode

        [Test]
        [Category("GetHashCode")]
        public void GetHashCodeTest() =>
            Assert.That(UFix8.One.GetHashCode(), Is.EqualTo(HashCode.Combine(256, 4)));

        #endregion GetHashCode
    }
}