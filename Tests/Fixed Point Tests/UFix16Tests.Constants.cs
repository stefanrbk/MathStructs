using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix16Tests
    {
        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(UFix16.Epsilon, Is.EqualTo(UFix16.Raw(1)));

        [Test, Category("MaxValue")]
        public void MaxValue() =>
            Assert.That(UFix16.MaxValue, Is.EqualTo(UFix16.Raw(UInt32.MaxValue)));

        [Test, Category("MinValue")]
        public void MinValue() =>
            Assert.That(UFix16.MinValue, Is.EqualTo(UFix16.Raw(UInt32.MinValue)));

        [Test, Category("Pi")]
        public void Pi() =>
            Assert.That(UFix16.Pi, Is.EqualTo((UFix16)Math.PI));

        [Test, Category("E")]
        public void E() =>
            Assert.That(UFix16.E, Is.EqualTo((UFix16)Math.E));

        [Test, Category("One")]
        public void One() =>
            Assert.That(UFix16.One, Is.EqualTo((UFix16)1));

        [Test, Category("Zero")]
        public void Zero() =>
            Assert.That(UFix16.Zero, Is.EqualTo((UFix16)0));

        [Test, Category("FourDivPi")]
        public void FourDivPi() =>
            Assert.That(UFix16.FourDivPi, Is.EqualTo((UFix16)( 4 / Math.PI )));

        [Test, Category("FourDivPi2")]
        public void FourDivPi2() =>
            Assert.That(UFix16.FourDivPi2, Is.EqualTo((UFix16)Math.Pow(4 / Math.PI, 2)));

        [Test, Category("PiDivFour")]
        public void PiDivFour() =>
            Assert.That(UFix16.PiDivFour, Is.EqualTo((UFix16)( Math.PI / 4 )));

        [Test, Category("ThreePiDivFour")]
        public void ThreePiDivFour() =>
            Assert.That(UFix16.ThreePiDivFour, Is.EqualTo((UFix16)( 3 * Math.PI / 4 )));
    }
}