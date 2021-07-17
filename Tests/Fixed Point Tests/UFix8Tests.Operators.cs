using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    public partial class UFix8Tests
    {
        #region Math Operators

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest([RandAdd] float left, [RandAdd] float right) =>
            Assert.That((UFix8)left + (UFix8)right, Is.EqualTo((UFix8)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest2([RandAdd, Values(0f, null)] float? left, [RandAdd, Values(null, 0f)] float? right) =>
            Assert.That((UFix8?)left + (UFix8?)right, Is.EqualTo((UFix8?)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest([RandSub1] float left, [RandSub2] float right) =>
            Assert.That((UFix8)left - (UFix8)right, Is.EqualTo((UFix8)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest2([RandSub1, Values(0f, null)] float? left, [RandSub2, Values(null, 0f)] float? right) =>
            Assert.That((UFix8?)left - (UFix8?)right, Is.EqualTo((UFix8?)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest([RandMul] float left, [RandMul] float right) =>
            Assert.That((UFix8)left * (UFix8)right, Is.EqualTo((UFix8)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest2([RandMul, Values(0f, null)] float? left, [RandMul, Values(null, 0f)] float? right) =>
            Assert.That((UFix8?)left * (UFix8?)right, Is.EqualTo((UFix8?)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest([RandDiv1] float left, [RandDiv2] float right) =>
            Assert.That((UFix8)left / (UFix8)right, Is.EqualTo((UFix8)( Fix(left) / Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest2([RandDiv1, Values(0f, null)] float? left, [RandDiv2, Values(null, 0f)] float? right) =>
            Assert.That((UFix8?)left / (UFix8?)right, Is.EqualTo((UFix8?)( Fix(left) / Fix(right) )));

        #endregion Math Operators

        #region Conditional Operators

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest(float left, float right) =>
            Assert.That((UFix8)left == (UFix8)right, Is.EqualTo(Fix(left) == Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest(float? left, float? right) =>
            Assert.That((UFix8?)left == (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) == FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest(float left, float right) =>
            Assert.That((UFix8)left != (UFix8)right, Is.EqualTo(Fix(left) != Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest(float? left, float? right) =>
            Assert.That((UFix8?)left != (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) != FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest(float left, float right) =>
            Assert.That((UFix8)left > (UFix8)right, Is.EqualTo(Fix(left) > Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest(float? left, float? right) =>
            Assert.That((UFix8?)left > (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) > FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest(float left, float right) =>
            Assert.That((UFix8)left >= (UFix8)right, Is.EqualTo(Fix(left) >= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest(float? left, float? right) =>
            Assert.That((UFix8?)left >= (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) >= FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest(float left, float right) =>
            Assert.That((UFix8)left < (UFix8)right, Is.EqualTo(Fix(left) < Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest(float? left, float? right) =>
            Assert.That((UFix8?)left < (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) < FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest(float left, float right) =>
            Assert.That((UFix8)left <= (UFix8)right, Is.EqualTo(Fix(left) <= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest(float? left, float? right) =>
            Assert.That((UFix8?)left <= (UFix8?)right, Is.EqualTo(FixNullIsNaN(left) <= FixNullIsNaN(right)));

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From UFix8 Operators

        [TestCaseSource("UFix8Source"), Category("op_Implicit(UFix8):float")]
        public void ImplicitToFloatTest(float value) =>
            Assert.That(value == (UFix8)value, Is.True);

        [TestCaseSource("UFix8Source"), Category("op_Implicit(UFix8):UFix16")]
        public void ImplicitToUFix16Test(float value) =>
            Assert.That((UFix16)value == (UFix8)value, Is.True);

        #endregion Implicit From UFix8 Operators

        #region Explicit From UFix8 Operators

        [Test, Sequential, Category("op_Explicit(UFix8):byte")]
        public void ExplicitToByteTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToByteSource")] byte expected) =>
            Assert.That((byte)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):sbyte")]
        public void ExplicitToSByteTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToSByteSource")] sbyte expected) =>
            Assert.That((sbyte)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):short")]
        public void ExplicitToShortTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToShortSource")] short expected) =>
            Assert.That((short)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):ushort")]
        public void ExplicitToUShortTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToUShortSource")] ushort expected) =>
            Assert.That((ushort)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):int")]
        public void ExplicitToIntTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToIntSource")] int expected) =>
            Assert.That((int)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):uint")]
        public void ExplicitToUIntTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToUIntSource")] uint expected) =>
            Assert.That((uint)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):long")]
        public void ExplicitToLongTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToLongSource")] long expected) =>
            Assert.That((long)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):ulong")]
        public void ExplicitToULongTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToULongSource")] ulong expected) =>
            Assert.That((ulong)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):Half")]
        public void ExplicitToHalfTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToHalfSource")] Half expected) =>
            Assert.That((Half)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):double")]
        public void ExplicitToDoubleTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToDoubleSource")] double expected) =>
            Assert.That((double)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):decimal")]
        public void ExplicitToDecimalTest([ValueSource("UFix8Source")] float actual, [ValueSource("ToDecimalSource")] decimal expected) =>
            Assert.That((decimal)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):Fix16")]
        public void ExplicitToFix16Test([ValueSource("UFix8Source")] float actual, [ValueSource("ToFix16Source")] Fix16 expected) =>
            Assert.That((Fix16)(UFix8)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(UFix8):UFix16")]
        public void ExplicitToUFix16Test([ValueSource("UFix8Source")] float actual, [ValueSource("ToUFix16Source")] UFix16 expected) =>
            Assert.That((UFix16)(UFix8)actual, Is.EqualTo(expected));

        #endregion Explicit From UFix8 Operators

        #region Implicit To UFix8 Operators

        [TestCaseSource("ByteToUFix8Source"), Category("op_Implicit(byte):UFix8")]
        public void ImplicitFromByteTest(byte actual, UFix8 expected) =>
            Assert.That(actual == expected, Is.True);

        #endregion Implicit To UFix8 Operators

        #region Explicit To UFix8 Operators

        [TestCaseSource("SByteToUFix8Source"), Category("op_Explicit(sbyte):UFix8")]
        public void ExplicitFromSByteTest(sbyte value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("UShortToUFix8Source"), Category("op_Explicit(ushort):UFix8")]
        public void ExplicitFromUShortTest(ushort value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("ShortToUFix8Source"), Category("op_Explicit(short):UFix8")]
        public void ExplicitFromShortTest(short value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("IntToUFix8Source"), Category("op_Explicit(int):UFix8")]
        public void ExplicitFromIntTest(int value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("UIntToUFix8Source"), Category("op_Explicit(uint):UFix8")]
        public void ExplicitFromUIntTest(uint value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("LongToUFix8Source"), Category("op_Explicit(long):UFix8")]
        public void ExplicitFromLongTest(long value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("ULongToUFix8Source"), Category("op_Explicit(ulong):UFix8")]
        public void ExplicitFromULongTest(ulong value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("HalfToUFix8Source"), Category("op_Explicit(Half):UFix8")]
        public void ExplicitFromHalfTest(Half value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("FloatToUFix8Source"), Category("op_Explicit(float):UFix8")]
        public void ExplicitFromFloatTest(float value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("DoubleToUFix8Source"), Category("op_Explicit(double):UFix8")]
        public void ExplicitFromDoubleTest(double value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("DecimalToUFix8Source"), Category("op_Explicit(decimal):UFix8")]
        public void ExplicitFromDecimalTest(decimal value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("Fix16ToUFix8Source"), Category("op_Explicit(Fix16):UFix8")]
        public void ExplicitFromFix16Test(Fix16 value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        [TestCaseSource("UFix16ToUFix8Source"), Category("op_Explicit(UFix16):UFix8")]
        public void ExplicitFromUFix16Test(UFix16 value, UFix8 expected) =>
            Assert.That((UFix8)value, Is.EqualTo(expected));

        #endregion Explicit To UFix8 Operators

        #endregion Cast Operators
    }
}