using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix16Tests
    {
        #region Math Operators

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest([RandAdd] double left, [RandAdd] double right) =>
            Assert.That((UFix16)left + (UFix16)right, Is.EqualTo((UFix16)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest2([RandAdd, Values(0, null)] double? left, [RandAdd, Values(null, 0)] double? right) =>
            Assert.That((UFix16?)left + (UFix16?)right, Is.EqualTo((UFix16?)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest([RandSub1] double left, [RandSub2] double right) =>
            Assert.That((UFix16)left - (UFix16)right, Is.EqualTo((UFix16)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest2([RandSub1, Values(0, null)] double? left, [RandSub2, Values(null, 0)] double? right) =>
            Assert.That((UFix16?)left - (UFix16?)right, Is.EqualTo((UFix16?)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest([RandMul] double left, [RandMul] double right) =>
            Assert.That((UFix16)left * (UFix16)right, Is.EqualTo((UFix16)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest2([RandMul, Values(0, null)] double? left, [RandMul, Values(null, 0)] double? right) =>
            Assert.That((UFix16?)left * (UFix16?)right, Is.EqualTo((UFix16?)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest([RandDiv1] double left, [RandDiv2] double right) =>
            Assert.That((UFix16)left / (UFix16)right, Is.EqualTo((UFix16)( Fix(left) / Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest2([RandDiv1, Values(0, null)] double? left, [RandDiv2, Values(null, 0)] double? right) =>
            Assert.That((UFix16?)left / (UFix16?)right, Is.EqualTo((UFix16?)( Fix(left) / Fix(right) )));

        #endregion Math Operators

        #region Conditional Operators

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest(double left, double right) =>
            Assert.That((UFix16)left == (UFix16)right, Is.EqualTo(Fix(left) == Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest(double? left, double? right) =>
            Assert.That((UFix16?)left == (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) == FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest(double left, double right) =>
            Assert.That((UFix16)left != (UFix16)right, Is.EqualTo(Fix(left) != Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest(double? left, double? right) =>
            Assert.That((UFix16?)left != (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) != FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest(double left, double right) =>
            Assert.That((UFix16)left > (UFix16)right, Is.EqualTo(Fix(left) > Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest(double? left, double? right) =>
            Assert.That((UFix16?)left > (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) > FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest(double left, double right) =>
            Assert.That((UFix16)left >= (UFix16)right, Is.EqualTo(Fix(left) >= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest(double? left, double? right) =>
            Assert.That((UFix16?)left >= (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) >= FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest(double left, double right) =>
            Assert.That((UFix16)left < (UFix16)right, Is.EqualTo(Fix(left) < Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest(double? left, double? right) =>
            Assert.That((UFix16?)left < (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) < FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest(double left, double right) =>
            Assert.That((UFix16)left <= (UFix16)right, Is.EqualTo(Fix(left) <= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest(double? left, double? right) =>
            Assert.That((UFix16?)left <= (UFix16?)right, Is.EqualTo(FixNullIsNaN(left) <= FixNullIsNaN(right)));

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From UFix16 Operators

        [TestCaseSource("UFix16Source"), Category("op_Implicit(UFix16):double")]
        public void ImplicitToDoubleTest(double value) =>
            Assert.That(value == (UFix16)value, Is.True);

        #endregion Implicit From UFix16 Operators

        #region Explicit From UFix16 Operators

        [Test, Sequential, Category("op_Explicit(UFix16):byte")]
        public void ExplicitToByteTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToByteSource")] byte expected) =>
            Assert.That((byte)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):sbyte")]
        public void ExplicitToSByteTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToSByteSource")] sbyte expected) =>
            Assert.That((sbyte)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):short")]
        public void ExplicitToShortTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToShortSource")] short expected) =>
            Assert.That((short)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):ushort")]
        public void ExplicitToUShortTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToUShortSource")] ushort expected) =>
            Assert.That((ushort)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):int")]
        public void ExplicitToIntTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToIntSource")] int expected) =>
            Assert.That((int)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):uint")]
        public void ExplicitToUIntTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToUIntSource")] uint expected) =>
            Assert.That((uint)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):long")]
        public void ExplicitToLongTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToLongSource")] long expected) =>
            Assert.That((long)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):ulong")]
        public void ExplicitToULongTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToULongSource")] ulong expected) =>
            Assert.That((ulong)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):Half")]
        public void ExplicitToHalfTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToHalfSource")] Half expected) =>
            Assert.That((Half)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):float")]
        public void ExplicitToFloatTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToFloatSource")] float expected) =>
            Assert.That((float)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):double")]
        public void ExplicitToDoubleTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToDoubleSource")] double expected) =>
            Assert.That((double)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):decimal")]
        public void ExplicitToDecimalTest([ValueSource("UFix16Source")] double actual, [ValueSource("ToDecimalSource")] decimal expected) =>
            Assert.That((decimal)(UFix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix16):Fix16")]
        public void ExplicitToFix16Test([ValueSource("UFix16Source")] double actual, [ValueSource("ToFix16Source")] Fix16 expected) =>
            Assert.That((Fix16)(UFix16)actual, Is.EqualTo(expected));

        #endregion Explicit From UFix16 Operators

        #region Implicit To UFix16 Operators

        [TestCaseSource("ByteToUFix16Source"), Category("op_Implicit(byte):UFix16")]
        public void ImplicitFromByteTest(byte actual, UFix16 expected) =>
            Assert.That(actual == expected, Is.True);

        [TestCaseSource("UShortToUFix16Source"), Category("op_Implicit(ushort):UFix16")]
        public void ImplicitFromUShortTest(ushort value, UFix16 expected) =>
            Assert.That(value == expected, Is.True);

        #endregion Implicit To UFix16 Operators

        #region Explicit To UFix16 Operators

        [TestCaseSource("SByteToUFix16Source"), Category("op_Explicit(sbyte):UFix16")]
        public void ExplicitFromSByteTest(sbyte value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("ShortToUFix16Source"), Category("op_Explicit(short):UFix16")]
        public void ExplicitFromShortTest(short value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("IntToUFix16Source"), Category("op_Explicit(int):UFix16")]
        public void ExplicitFromIntTest(int value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("UIntToUFix16Source"), Category("op_Explicit(uint):UFix16")]
        public void ExplicitFromUIntTest(uint value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("LongToUFix16Source"), Category("op_Explicit(long):UFix16")]
        public void ExplicitFromLongTest(long value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("ULongToUFix16Source"), Category("op_Explicit(ulong):UFix16")]
        public void ExplicitFromULongTest(ulong value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("HalfToUFix16Source"), Category("op_Explicit(Half):UFix16")]
        public void ExplicitFromHalfTest(Half value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("FloatToUFix16Source"), Category("op_Explicit(float):UFix16")]
        public void ExplicitFromFloatTest(float value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("DoubleToUFix16Source"), Category("op_Explicit(double):UFix16")]
        public void ExplicitFromDoubleTest(double value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("DecimalToUFix16Source"), Category("op_Explicit(decimal):UFix16")]
        public void ExplicitFromDecimalTest(decimal value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        [TestCaseSource("Fix16ToUFix16Source"), Category("op_Explicit(Fix16):UFix16")]
        public void ExplicitFromFix16Test(Fix16 value, UFix16 expected) =>
            Assert.That((UFix16)value, Is.EqualTo(expected));

        #endregion Explicit To UFix16 Operators

        #endregion Cast Operators
    }
}