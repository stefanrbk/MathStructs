using NUnit.Framework;

namespace System.Tests
{
    public partial class Fix16Tests
    {
        [Test, Category("GetTypeCode")]
        public void GetTypeCodeTest() =>
              Assert.That(( (IConvertible)Fix16.Zero ).GetTypeCode(), Is.EqualTo((TypeCode)100));

        [TestCaseSource("Fix16Source"), Category("ToBoolean")]
        public void ToBooleanTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToBoolean, value.ToBoolean);

        [TestCaseSource("Fix16Source"), Category("ToByte")]
        public void ToByteTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToByte, value.ToByte);

        [TestCaseSource("Fix16Source"), Category("ToSByte")]
        public void ToSByteTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToSByte, value.ToSByte);

        [TestCaseSource("Fix16Source"), Category("ToInt16")]
        public void ToInt16Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToInt16, value.ToInt16);

        [TestCaseSource("Fix16Source"), Category("ToUInt16")]
        public void ToUInt16Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToUInt16, value.ToUInt16);

        [TestCaseSource("Fix16Source"), Category("ToInt32")]
        public void ToInt32Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToInt32, value.ToInt32);

        [TestCaseSource("Fix16Source"), Category("ToUInt32")]
        public void ToUInt32Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToUInt32, value.ToUInt32);

        [TestCaseSource("Fix16Source"), Category("ToInt64")]
        public void ToInt64Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToInt64, value.ToInt64);

        [TestCaseSource("Fix16Source"), Category("ToUInt64")]
        public void ToUInt64Test(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToUInt64, value.ToUInt64);

        [TestCaseSource("Fix16Source"), Category("ToSingle")]
        public void ToSingleTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToSingle, value.ToSingle);

        [TestCaseSource("Fix16Source"), Category("ToDouble")]
        public void ToDoubleTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToDouble, value.ToDouble);

        [TestCaseSource("Fix16Source"), Category("ToDecimal")]
        public void ToDecimalTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToDecimal, value.ToDecimal);

        [TestCaseSource("Fix16Source"), Category("ToString")]
        public void ToStringTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToString, value.ToString);

        [Test, Category("ToType")]
        public void ToTypeTest([ValueSource("ToTypeTypesSource")] Type type, [ValueSource("ToTypeValuesSource")] double value) =>
            ToTypeTest((p) => (IConvertible)( (IConvertible)(Fix16)value ).ToType(type, p), (p) => (IConvertible)( (IConvertible)Fix(value) ).ToType(type, p));

        [Test, Category("ToHalf")]
        public unsafe void ToHalfTest([RandHalfL, RandHalfR] ushort value)
        {
            var v = *(Half*)&value;
            ToTypeTest(() => v.ToFix16(null).ToHalf(null), () => (Half)v.ToFix16(null));
        }

        [TestCaseSource("Fix16Source"), Category("ToChar")]
        public void ToCharTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToChar, value.ToChar);

        [TestCaseSource("Fix16Source"), Category("ToDateTime")]
        public void ToDateTimeTest(IConvertible value) =>
            ToTypeTest(( (IConvertible)value.ToFix16(null) ).ToDateTime, value.ToDateTime);
    }
}