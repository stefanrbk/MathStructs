using NUnit.Framework;

namespace System.Tests
{
    public partial class Fix16Tests
    {
        #region Overflow Checked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("CheckedAdd")]
        public void CheckedAddTest([RandAddOver] double left, [RandAddOver] double right) =>
            Assert.That(() => Fix16.CheckedAdd((Fix16)left, (Fix16)right),
                ( ( Fix(left) + Fix(right) ) is <= Fix16Max and >= Fix16Min )
                    ? Is.EqualTo((Fix16)( Fix(left) + Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("SubtractSource"), Sequential, Category("CheckedSubtract")]
        public void CheckedSubtractTest([RandSubOverL] double left, [RandSubOverR1, RandSubOverR2] double right) =>
            Assert.That(() => Fix16.CheckedSubtract((Fix16)left, (Fix16)right),
                ( ( Fix(left) - Fix(right) ) is <= Fix16Max and >= Fix16Min )
                    ? Is.EqualTo((Fix16)( Fix(left) - Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("MultiplySource"), Sequential, Category("CheckedMultiply")]
        public void CheckedMultiplyTest([RandMulOver] double left, [RandMulOver] double right) =>
            Assert.That(() => Fix16.CheckedMultiply((Fix16)left, (Fix16)right),
                ( ( Fix(left) * Fix(right) ) is <= Fix16Max and >= Fix16Min )
                    ? Is.EqualTo((Fix16)( Fix(left) * Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("DivideSource"), Sequential, Category("CheckedDivide")]
        public void CheckedDivideTest([RandDivOverL, Values(1)] double left, [RandDivOverR, Values(0)] double right) =>
            Assert.That(() => Fix16.CheckedDivide((Fix16)left, (Fix16)right),
                    ( Fix(right) != 0)
                        ? ( ( Fix(left) / Fix(right) ) is <= Fix16Max and >= Fix16Min )
                            ? Is.EqualTo((Fix16)( Fix(left) / Fix(right) ))
                            : Throws.TypeOf<OverflowException>()
                        : Throws.TypeOf<DivideByZeroException>());

        #endregion Overflow Checked Math Functions

        #region Overflow Unchecked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("Add")]
        public void AddTest([RandAdd, RandAddOver] double left, [RandAdd, RandAddOver] double right) =>
            Assert.That(Fix16.Add((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)( Fix(left) + Fix(right) )));

        [TestCaseSource("SubtractSource"), Sequential, Category("Subtract")]
        public void SubtractTest([RandSubL, RandSubOverL] double left, [RandSubR, RandSubOverR1, RandSubOverR2] double right) =>
            Assert.That(Fix16.Subtract((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)( Fix(left) - Fix(right) )));

        [TestCaseSource("MultiplySource"), Sequential, Category("Multiply")]
        public void MultiplyTest([RandMul, RandMulOver] double left, [RandMul, RandMulOver] double right) =>
            Assert.That(Fix16.Multiply((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)( Fix(left) * Fix(right) )));

        [TestCaseSource("DivideSource"), Sequential, Category("Divide")]
        public void DivideTest([RandDivL, RandDivOverL] double left, [RandDivR, RandDivOverR] double right) =>
            Assert.That(() => Fix16.Divide((Fix16)left, (Fix16)right), Fix(right) != 0 ? Is.EqualTo((Fix16)( Fix(left) / Fix(right) )) : Throws.TypeOf<DivideByZeroException>());

        [TestCaseSource("NullableDivideSource"), Sequential, Category("NullableDivide")]
        public void NullableDivideByTest(double? left, double? right, double? expected) =>
            Assert.That(Fix16.NullableDivide((Fix16?)left, (Fix16?)right), Is.EqualTo((Fix16?)expected));

        [TestCaseSource("LerpSource"), Sequential, Category("Lerp")]
        public void LerpTest(double left, double right, byte fraction, double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [TestCaseSource("Lerp2Source"), Sequential, Category("Lerp")]
        public void LerpTest2(double left, double right, ushort fraction, double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [TestCaseSource("Lerp3Source"), Sequential, Category("Lerp")]
        public void LerpTest3(double left, double right, uint fraction, double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [TestCaseSource("ExpSource"), Category("Exp")]
        public void ExpTest(Fix16 value) =>
            Assert.That(Fix16.Exp(value), Is.EqualTo((Fix16)Math.Min(Fix16Max, Math.Exp(value))));

        [TestCaseSource("SqrtSource"),Category("Sqrt")]
        public void SqrtTest(double value) =>
            Assert.That(Fix16.Sqrt((Fix16)value), Is.EqualTo((value < 0 ? -1 : 1) * (Fix16)Math.Sqrt(Math.Abs(Fix(value)))));

        #endregion Overflow Unchecked Math Functions

        #region Trigonometry Functions

        [TestCaseSource("SinSource")]
        [Category("Sin")]
        public void SinTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Sin((Fix16)value), Is.EqualTo((Fix16)Math.Sin(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.008)));

        [Test]
        [Category("Sin")]
        public void SinTest2() =>
            Assert.That(Fix16.Sin(Fix16.Zero), Is.EqualTo(Fix16.Zero));

        [TestCaseSource("SinSource")]
        [Category("Cos")]
        public void CosTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Cos((Fix16)value), Is.EqualTo((Fix16)Math.Cos(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.008)));

        [Test]
        [Category("Tan")]
        public void TanTest([Random(-1.45, 1.45, 10)] double value) =>
            Assert.That(Fix16.Tan((Fix16)value), Is.EqualTo((Fix16)Math.Tan(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.4)));

        [Test]
        [Category("Tan")]
        public void TanTest2() =>
            Assert.That(Fix16.Tan(Fix16.Pi / (Fix16)2), Is.Null);

        [Test]
        [Category("Asin")]
        public void AsinTest([Values(1.0)][Random(-65535.0/65536, 65535/65536, 10)] double value) =>
            Assert.That(Fix16.Asin((Fix16)value), Is.EqualTo((Fix16)Math.Asin(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.010177)));

        [Test]
        [Category("Asin")]
        public void AsinTest2() =>
            Assert.That(Fix16.Asin((Fix16)1.1), Is.Null);

        [Test]
        [Category("Acos")]
        public void AcosTest([Random(-65535.0/65536, 65535/65536, 10)] double value) =>
            Assert.That(Fix16.Acos((Fix16)value), Is.EqualTo((Fix16)Math.Acos(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.010177)));

        [TestCaseSource("SinSource")]
        [Category("Atan")]
        public void AtanTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Atan((Fix16)value), Is.EqualTo((Fix16)Math.Atan(Fix(value))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.010177)));

        [Test, Category("Atan2")]
        public void Atan2Test([Random(-Math.PI, Math.PI, 10)] double x, [Random(-Math.PI, Math.PI, 10)] double y) =>
            Assert.That(Fix16.Atan2((Fix16)y, (Fix16)x), Is.EqualTo((Fix16)Math.Atan2(Fix(y), Fix(x))).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.010177)));

        [Test]
        [Category("Atan")]
        public void AtanTest2() =>
            Assert.That(Fix16.Atan(Fix16.Zero), Is.EqualTo(Fix16.Atan(Fix16.Zero)));

        #endregion Trigonometry Functions
    }
}