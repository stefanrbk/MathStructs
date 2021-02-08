using NUnit.Framework;

namespace System.Tests
{
    public partial class Fix16Tests
    {
        #region Math Operators

        [Test, Sequential, Category("op_UnaryNegation")]
        public void NegationTest([RandAdd] double value) =>
            Assert.That(-(Fix16)value, Is.EqualTo((Fix16)Fix(-value)));

        [Test, Sequential, Category("op_UnaryNegation")]
        public void NegationTest2([RandAdd, Values(0, null)] double? value) =>
            Assert.That(-(Fix16?)value, Is.EqualTo((Fix16?)Fix(-value)));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest([RandAdd] double left, [RandAdd] double right) =>
            Assert.That((Fix16)left + (Fix16)right, Is.EqualTo((Fix16)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest2([RandAdd, Values(0, null)] double? left, [RandAdd, Values(null, 0)] double? right) =>
            Assert.That((Fix16?)left + (Fix16?)right, Is.EqualTo((Fix16?)( Fix(left) + Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest([RandSubL] double left, [RandSubR] double right) =>
            Assert.That((Fix16)left - (Fix16)right, Is.EqualTo((Fix16)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest2([RandSubL, Values(0, null)] double? left, [RandSubR, Values(null, 0)] double? right) =>
            Assert.That((Fix16?)left - (Fix16?)right, Is.EqualTo((Fix16?)( Fix(left) - Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest([RandMul] double left, [RandMul] double right) =>
            Assert.That((Fix16)left * (Fix16)right, Is.EqualTo((Fix16)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Multiply")]
        public void MultiplicationTest2([RandMul, Values(0, null)] double? left, [RandMul, Values(null, 0)] double? right) =>
            Assert.That((Fix16?)left * (Fix16?)right, Is.EqualTo((Fix16?)( Fix(left) * Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest([RandDivL] double left, [RandDivR] double right) =>
            Assert.That((Fix16)left / (Fix16)right, Is.EqualTo((Fix16)( Fix(left) / Fix(right) )));

        [Test, Sequential, Category("op_Divide")]
        public void DivisionTest2([RandDivL, Values(0, null, 0, null)] double? left, [RandDivR, Values(null, 0, 0, null)] double? right) =>
            Assert.That(() => (Fix16?)left / (Fix16?)right, ( Fix(right) == 0 ) && ( Fix(left) is not null ) ? Throws.TypeOf<DivideByZeroException>() : Is.EqualTo((Fix16?)( Fix(left) / Fix(right) )));

        #endregion Math Operators

        #region Conditional Operators

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest(double left, double right) =>
            Assert.That((Fix16)left == (Fix16)right, Is.EqualTo(Fix(left) == Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Equality")]
        public void EqualityTest2(double? left, double? right) =>
            Assert.That((Fix16?)left == (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) == FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest(double left, double right) =>
            Assert.That((Fix16)left != (Fix16)right, Is.EqualTo(Fix(left) != Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_Inequality")]
        public void InequalityTest2(double? left, double? right) =>
            Assert.That((Fix16?)left != (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) != FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest(double left, double right) =>
            Assert.That((Fix16)left > (Fix16)right, Is.EqualTo(Fix(left) > Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest2(double? left, double? right) =>
            Assert.That((Fix16?)left > (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) > FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest(double left, double right) =>
            Assert.That((Fix16)left >= (Fix16)right, Is.EqualTo(Fix(left) >= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest2(double? left, double? right) =>
            Assert.That((Fix16?)left >= (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) >= FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest(double left, double right) =>
            Assert.That((Fix16)left < (Fix16)right, Is.EqualTo(Fix(left) < Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThan")]
        public void LessThanTest2(double? left, double? right) =>
            Assert.That((Fix16?)left < (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) < FixNullIsNaN(right)));

        [TestCaseSource("EqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest(double left, double right) =>
            Assert.That((Fix16)left <= (Fix16)right, Is.EqualTo(Fix(left) <= Fix(right)));

        [TestCaseSource("EqualitySource"), TestCaseSource("NullableEqualitySource"), Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest2(double? left, double? right) =>
            Assert.That((Fix16?)left <= (Fix16?)right, Is.EqualTo(FixNullIsNaN(left) <= FixNullIsNaN(right)));

        #endregion Conditional Operators

        #region Cast Operators

        #region -> Implicit From Fix16 Operators

        [TestCaseSource("Fix16Source"), Category("op_Implicit(Fix16):double")]
        public void ImplicitToDoubleTest(double value) =>
            Assert.That(Fix(value) == Fix((Fix16)value), Is.True);

        #endregion -> Implicit From Fix16 Operators

        #region -> Explicit From Fix16 Operators

        [Test, Sequential, Category("op_Explicit(Fix16):byte")]
        public void ExplicitToByteTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToByteSource")] byte expected) =>
            Assert.That((byte)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):sbyte")]
        public void ExplicitToSByteTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToSByteSource")] sbyte expected) =>
            Assert.That((sbyte)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):short")]
        public void ExplicitToShortTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToShortSource")] short expected) =>
            Assert.That((short)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):ushort")]
        public void ExplicitToUShortTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToUShortSource")] ushort expected) =>
            Assert.That((ushort)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):int")]
        public void ExplicitToIntTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToIntSource")] int expected) =>
            Assert.That((int)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):uint")]
        public void ExplicitToUIntTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToUIntSource")] uint expected) =>
            Assert.That((uint)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):long")]
        public void ExplicitToLongTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToLongSource")] long expected) =>
            Assert.That((long)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):ulong")]
        public void ExplicitToULongTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToULongSource")] ulong expected) =>
            Assert.That((ulong)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):Half")]
        public void ExplicitToHalfTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToHalfSource")] Half expected) =>
            Assert.That((Half)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):float")]
        public void ExplicitToFloatTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToFloatSource")] float expected) =>
            Assert.That((float)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):double")]
        public void ExplicitToDoubleTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToDoubleSource")] double expected) =>
            Assert.That((double)(Fix16)actual, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Explicit(Fix16):decimal")]
        public void ExplicitToDecimalTest([ValueSource("Fix16Source")] double actual, [ValueSource("ToDecimalSource")] decimal expected) =>
            Assert.That((decimal)(Fix16)actual, Is.EqualTo(expected));

        #endregion -> Explicit From Fix16 Operators

        #region -> Implicit To Fix16 Operators

        [TestCaseSource("ByteToFix16Source"), Category("op_Implicit(byte):Fix16")]
        public void ImplicitFromByteTest(byte actual, Fix16 expected) =>
            Assert.That(Fix16.Add(actual, Fix16.Zero).Equals(expected), Is.True);

        [TestCaseSource("SByteToFix16Source"), Category("op_Implicit(sbyte):Fix16")]
        public void ImplicitFromSByteTest(sbyte actual, Fix16 expected) =>
            Assert.That(Fix16.Add(actual, Fix16.Zero).Equals(expected), Is.True);

        [TestCaseSource("ShortToFix16Source"), Category("op_Implicit(short):Fix16")]
        public void ImplicitFromShortTest(short actual, Fix16 expected) =>
            Assert.That(Fix16.Add(actual, Fix16.Zero).Equals(expected), Is.True);

        #endregion -> Implicit To Fix16 Operators

        #region -> Explicit To Fix16 Operators

        [TestCaseSource("UShortToFix16Source"), Category("op_Explicit(ushort):Fix16")]
        public void ExplicitFromShortTest(ushort value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("IntToFix16Source"), Category("op_Explicit(int):Fix16")]
        public void ExplicitFromIntTest(int value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("UIntToFix16Source"), Category("op_Explicit(uint):Fix16")]
        public void ExplicitFromUIntTest(uint value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("LongToFix16Source"), Category("op_Explicit(long):Fix16")]
        public void ExplicitFromLongTest(long value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("ULongToFix16Source"), Category("op_Explicit(ulong):Fix16")]
        public void ExplicitFromULongTest(ulong value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("HalfToFix16Source"), Category("op_Explicit(Half):Fix16")]
        public void ExplicitFromHalfTest(Half value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FloatToFix16Source"), Category("op_Explicit(float):Fix16")]
        public void ExplicitFromFloatTest(float value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("DoubleToFix16Source"), Category("op_Explicit(double):Fix16")]
        public void ExplicitFromDoubleTest(double value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("DecimalToFix16Source"), Category("op_Explicit(decimal):Fix16")]
        public void ExplicitFromDecimalTest(decimal value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("UFix16ToFix16Source"), Category("op_Explicit(UFix16):Fix16")]
        public void ExplicitFromFix16Test(UFix16 value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        #endregion -> Explicit To Fix16 Operators

        #endregion Cast Operators
    }
}