using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix16Tests
    {
        #region Overflow Checked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("CheckedAdd")]
        public void CheckedAddTest([RandAddOver] double left, [RandAddOver] double right) =>
            Assert.That(() => UFix16.CheckedAdd((UFix16) left, (UFix16) right),
                ((Fix(left) + Fix(right) ) is <= UFix16Max and >= UFix16Min )
                    ? Is.EqualTo((UFix16)(Fix(left) + Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("SubtractSource"), Sequential, Category("CheckedSubtract")]
        public void CheckedSubtractTest([RandSubOver] double left, [RandSubOver] double right) =>
            Assert.That(() => UFix16.CheckedSubtract((UFix16)left, (UFix16)right),
                ( ( Fix(left) - Fix(right) ) is <= UFix16Max and >= UFix16Min )
                    ? Is.EqualTo((UFix16)( Fix(left) - Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("MultiplySource"), Sequential, Category("CheckedMultiply")]
        public void CheckedMultiplyTest([RandMulOver] double left, [RandMulOver] double right) =>
            Assert.That(() => UFix16.CheckedMultiply((UFix16)left, (UFix16)right),
                ( ( Fix(left) * Fix(right) ) is <= UFix16Max and >= UFix16Min )
                    ? Is.EqualTo((UFix16)( Fix(left) * Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("DivideSource"), Sequential, Category("CheckedDivide")]
        public void CheckedDivideTest([RandDivOver1, Values(1)] double left, [RandDivOver2, Values(0)] double right) =>
            Assert.That(() => UFix16.CheckedDivide((UFix16)left, (UFix16)right),
                    ( Fix(right) != 0 )
                        ? ( ( Fix(left) / Fix(right) ) is <= UFix16Max and >= UFix16Min )
                            ? Is.EqualTo((UFix16)( Fix(left) / Fix(right) ))
                            : Throws.TypeOf<OverflowException>()
                        : Throws.TypeOf<DivideByZeroException>());

        #endregion Overflow Checked Math Functions

        #region Overflow Unchecked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("Add")]
        public void AddTest([RandAdd, RandAddOver] double left, [RandAdd, RandAddOver] double right) =>
            Assert.That(UFix16.Add((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)( Fix(left) + Fix(right) )));

        [TestCaseSource("SubtractSource"), Sequential, Category("Subtract")]
        public void SubtractTest([RandSub1, RandSubOver] double left, [RandSub2, RandSubOver] double right) =>
            Assert.That(UFix16.Subtract((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)( Fix(left) - Fix(right) )));

        [TestCaseSource("MultiplySource"), Sequential, Category("Multiply")]
        public void MultiplyTest([RandMul, RandMulOver] double left, [RandMul, RandMulOver] double right) =>
            Assert.That(UFix16.Multiply((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)( Fix(left) * Fix(right) )));

        [TestCaseSource("DivideSource"), Sequential, Category("Divide")]
        public void DivideTest([RandDiv1, RandDivOver1] double left, [RandDiv2, RandDivOver2] double right) =>
            Assert.That(() => UFix16.Divide((UFix16)left, (UFix16)right), Fix(right) != 0 ? Is.EqualTo((UFix16)( Fix(left) / Fix(right) )) : Throws.TypeOf<DivideByZeroException>());

        [TestCaseSource("NullableDivideSource"), Sequential, Category("NullableDivide")]
        public void NullableDivideByTest(double? left, double? right, double? expected) =>
            Assert.That(UFix16.NullableDivide((UFix16?)left, (UFix16?)right), Is.EqualTo((UFix16?)expected));

        [TestCaseSource("LerpSource"), Sequential, Category("Lerp")]
        public void LerpTest(double left, double right, byte fraction, double expected) =>
            Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        [TestCaseSource("Lerp2Source"), Sequential, Category("Lerp")]
        public void LerpTest2(double left, double right, ushort fraction, double expected) =>
            Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        [TestCaseSource("Lerp3Source"), Sequential, Category("Lerp")]
        public void LerpTest3(double left, double right, uint fraction, double expected) =>
            Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        [TestCaseSource("SqrtSource"), Category("Sqrt")]
        public void SqrtTest(double value) =>
            Assert.That(UFix16.Sqrt((UFix16)value), Is.EqualTo((UFix16)Math.Sqrt((UFix16)value)));

        #endregion Overflow Unchecked Math Functions
    }
}