using NUnit.Framework;

namespace System.Tests
{
    public partial class Fix16Tests
    {
        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(Fix16.Epsilon, Is.EqualTo(Fix16.Raw(1)));

        [Test, Category("MaxValue")]
        public void MaxValue() =>
            Assert.That(Fix16.MaxValue, Is.EqualTo(Fix16.Raw(Int32.MaxValue)));

        [Test, Category("MinValue")]
        public void MinValue() =>
            Assert.That(Fix16.MinValue, Is.EqualTo(Fix16.Raw(Int32.MinValue)));

        [Test, Category("Pi")]
        public void Pi() =>
            Assert.That(Fix16.Pi, Is.EqualTo((Fix16)Math.PI));

        [Test, Category("E")]
        public void E() =>
            Assert.That(Fix16.E, Is.EqualTo((Fix16)Math.E));

        [Test, Category("One")]
        public void One() =>
            Assert.That(Fix16.One, Is.EqualTo((Fix16)1));

        [Test, Category("Zero")]
        public void Zero() =>
            Assert.That(Fix16.Zero, Is.EqualTo((Fix16)0));

        [Test, Category("NegOne")]
        public void NegOne() =>
            Assert.That(Fix16.NegOne, Is.EqualTo((Fix16)( -1 )));

        [Test, Category("FourDivPi")]
        public void FourDivPi() =>
            Assert.That(Fix16.FourDivPi, Is.EqualTo((Fix16)( 4 / Math.PI )));

        [Test, Category("FourDivPi2")]
        public void FourDivPi2() =>
            Assert.That(Fix16.FourDivPi2, Is.EqualTo((Fix16)Math.Pow(4 / Math.PI, 2)));

        [Test, Category("PiDivFour")]
        public void PiDivFour() =>
            Assert.That(Fix16.PiDivFour, Is.EqualTo((Fix16)( Math.PI / 4 )));

        [Test, Category("ThreePiDivFour")]
        public void ThreePiDivFour() =>
            Assert.That(Fix16.ThreePiDivFour, Is.EqualTo((Fix16)( 3 * Math.PI / 4 )));
    }
}