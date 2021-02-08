using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix8Tests
    {
        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(UFix8.Epsilon, Is.EqualTo(UFix8.Raw(1)));

        [Test, Category("MaxValue")]
        public void MaxValue() =>
            Assert.That(UFix8.MaxValue, Is.EqualTo(UFix8.Raw(UInt16.MaxValue)));

        [Test, Category("MinValue")]
        public void MinValue() =>
            Assert.That(UFix8.MinValue, Is.EqualTo(UFix8.Raw(UInt16.MinValue)));

        [Test, Category("One")]
        public void One() =>
            Assert.That(UFix8.One, Is.EqualTo((UFix8)1));

        [Test, Category("Zero")]
        public void Zero() =>
            Assert.That(UFix8.Zero, Is.EqualTo((UFix8)0));
    }
}