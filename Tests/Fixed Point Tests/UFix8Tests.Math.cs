using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix8Tests
    {
        #region Overflow Checked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("CheckedAdd")]
        public void CheckedAddTest([RandAddOver] float left, [RandAddOver] float right) =>
            Assert.That(() => UFix8.CheckedAdd((UFix8) left, (UFix8) right),
                ((Fix(left) + Fix(right) ) is <= UFix8Max and >= UFix8Min )
                    ? Is.EqualTo((UFix8)(Fix(left) + Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("SubtractSource"), Sequential, Category("CheckedSubtract")]
        public void CheckedSubtractTest([RandSubOver] float left, [RandSubOver] float right) =>
            Assert.That(() => UFix8.CheckedSubtract((UFix8)left, (UFix8)right),
                ( ( Fix(left) - Fix(right) ) is <= UFix8Max and >= UFix8Min )
                    ? Is.EqualTo((UFix8)( Fix(left) - Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("MultiplySource"), Sequential, Category("CheckedMultiply")]
        public void CheckedMultiplyTest([RandMulOver] float left, [RandMulOver] float right) =>
            Assert.That(() => UFix8.CheckedMultiply((UFix8)left, (UFix8)right),
                ( ( Fix(left) * Fix(right) ) is <= UFix8Max and >= UFix8Min )
                    ? Is.EqualTo((UFix8)( Fix(left) * Fix(right) ))
                    : Throws.TypeOf<OverflowException>());

        [TestCaseSource("DivideSource"), Sequential, Category("CheckedDivide")]
        public void CheckedDivideTest([RandDivOver1, Values(1)] float left, [RandDivOver2, Values(0)] float right) =>
            Assert.That(() => UFix8.CheckedDivide((UFix8)left, (UFix8)right),
                    ( Fix(right) != 0 )
                        ? ( ( Fix(left) / Fix(right) ) is <= UFix8Max and >= UFix8Min )
                            ? Is.EqualTo((UFix8)( Fix(left) / Fix(right) ))
                            : Throws.TypeOf<OverflowException>()
                        : Throws.TypeOf<DivideByZeroException>());

        #endregion Overflow Checked Math Functions

        #region Overflow Unchecked Math Functions

        [TestCaseSource("AddSource"), Sequential, Category("Add")]
        public void AddTest([RandAdd, RandAddOver] float left, [RandAdd, RandAddOver] float right) =>
            Assert.That(UFix8.Add((UFix8)left, (UFix8)right), Is.EqualTo((UFix8)( Fix(left) + Fix(right) )));

        [TestCaseSource("SubtractSource"), Sequential, Category("Subtract")]
        public void SubtractTest([RandSub1, RandSubOver] float left, [RandSub2, RandSubOver] float right) =>
            Assert.That(UFix8.Subtract((UFix8)left, (UFix8)right), Is.EqualTo((UFix8)( Fix(left) - Fix(right) )));

        [TestCaseSource("MultiplySource"), Sequential, Category("Multiply")]
        public void MultiplyTest([RandMul, RandMulOver] float left, [RandMul, RandMulOver] float right) =>
            Assert.That(UFix8.Multiply((UFix8)left, (UFix8)right), Is.EqualTo((UFix8)( Fix(left) * Fix(right) )));

        [TestCaseSource("DivideSource"), Sequential, Category("Divide")]
        public void DivideTest([RandDiv1, RandDivOver1] float left, [RandDiv2, RandDivOver2] float right) =>
            Assert.That(() => UFix8.Divide((UFix8)left, (UFix8)right), Fix(right) != 0 ? Is.EqualTo((UFix8)( Fix(left) / Fix(right) )) : Throws.TypeOf<DivideByZeroException>());

        [TestCaseSource("NullableDivideSource"), Sequential, Category("NullableDivide")]
        public void NullableDivideByTest(float? left, float? right, float? expected) =>
            Assert.That(UFix8.NullableDivide((UFix8?)left, (UFix8?)right), Is.EqualTo((UFix8?)expected));

        [TestCaseSource("LerpSource"), Sequential, Category("Lerp")]
        public void LerpTest(float left, float right, byte fraction, float expected) =>
            Assert.That(UFix8.Lerp((UFix8)left, (UFix8)right, fraction), Is.EqualTo((UFix8)expected));

        [TestCaseSource("Lerp2Source"), Sequential, Category("Lerp")]
        public void LerpTest2(float left, float right, ushort fraction, float expected) =>
            Assert.That(UFix8.Lerp((UFix8)left, (UFix8)right, fraction), Is.EqualTo((UFix8)expected));

        [TestCaseSource("Lerp3Source"), Sequential, Category("Lerp")]
        public void LerpTest3(float left, float right, uint fraction, float expected) =>
            Assert.That(UFix8.Lerp((UFix8)left, (UFix8)right, fraction), Is.EqualTo((UFix8)expected));

        [TestCaseSource("SqrtSource"), Category("Sqrt")]
        public void SqrtTest(float value) =>
            Assert.That(UFix8.Sqrt((UFix8)value), Is.EqualTo((UFix8)Math.Sqrt((UFix8)value)));

        #endregion Overflow Unchecked Math Functions
    }
}