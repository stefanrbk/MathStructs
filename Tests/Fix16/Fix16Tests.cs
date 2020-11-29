using NUnit.Framework;

using System.Collections.Generic;
using System.Globalization;

namespace System.Tests
{
    [TestFixture]
    public class Fix16Tests
    {
        public static readonly int[] Fix16ConversionSource = new int[]
        {
            0,
            234,
            234 * 65536,
            Fix16.MinValue,
            Fix16.MaxValue,
            Fix16.One,
            Fix16.NegOne,
            Fix16.E,
            Fix16.Pi,
            Fix16.Epsilon,
        };

        public static readonly string[] ToStringInvariantCultureExpected = new string[]
        {
            "0",
            "0.003570556640625",
            "234",
            "-32,768",
            "32,767.9999847412",
            "1",
            "-1",
            "2.73353576660156",
            "3.14158630371094",
            "0.0000152587890625",
        };

        public static readonly string[] ToStringUnitedStatesExpected = new string[]
        {
            "0",
            "0.003570556640625",
            "234",
            "-32,768",
            "32,767.9999847412",
            "1",
            "-1",
            "2.73353576660156",
            "3.14158630371094",
            "0.0000152587890625",
        };

        public static readonly string[] ToStringSouthAfricanExpected = new string[]
        {
            "0",
            "0,003570556640625",
            "234",
            "-32\u00A0768",
            "32\u00A0767,9999847412",
            "1",
            "-1",
            "2,73353576660156",
            "3,14158630371094",
            "0,0000152587890625",
        };

        public static readonly string[] ToStringIndianUrduExpected = new string[]
        {
            "0",
            "0\u066B003570556640625",
            "234",
            "\u200E-\u200E32\u066C768",
            "32\u066C767\u066B9999847412",
            "1",
            "\u200E-\u200E1",
            "2\u066B73353576660156",
            "3\u066B14158630371094",
            "0\u066B0000152587890625",
        };

        public static readonly bool[] ToBooleanExpectedSource = new bool[]
        {
            false,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
        };

        public static readonly byte[] ToByteExpectedSource = new byte[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            255,
            2,
            3,
            0,
        };

        public static readonly decimal[] ToDecimalExpectedSource = new decimal[]
        {
            0,
            0.003570556640625m,
            234,
            -32768,
            32767.9999847412m,
            1,
            -1,
            2.73353576660156m,
            3.14158630371094m,
            1.52587890625e-5m,
        };

        public static readonly double[] ToDoubleExpectedSource = new double[]
        {
            0,
            0.003570556640625,
            234,
            -32768,
            32767.999984741211,
            1,
            -1,
            2.7335357666015625,
            3.1415863037109375,
            1.52587890625e-5,
        };

        public static readonly Half[] ToHalfExpectedSource = new Half[]
        {
            (Half)0,
            (Half)0.00357,
            (Half)234,
            (Half)(-32770),
            (Half)32770,
            (Half)1,
            (Half)(-1),
            (Half)2.734,
            (Half)3.14,
            (Half)1.526e-5,
        };

        public static readonly short[] ToInt16ExpectedSource = new short[]
        {
            0,
            0,
            234,
            -32768,
            32767,
            1,
            -1,
            2,
            3,
            0,
        };

        public static readonly int[] ToInt32ExpectedSource = new int[]
        {
            0,
            0,
            234,
            -32768,
            32767,
            1,
            -1,
            2,
            3,
            0,
        };

        public static readonly Type[] ToTypeTypeSource = new Type[]
        {
            typeof(bool),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(Half),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(object)
        };

        public static readonly object[] ToTypeExpectedSource = new object[]
        {   true,
            (sbyte)16,
            (byte)16,
            (short)16,
            (ushort)16,
            16,
            (uint)16,
            (long)16,
            (ulong)16,
            (Half)16,
            (float)16,
            (double)16,
            (decimal)16,
            "16",
            0x00100000
        };

        public static readonly long[] ToInt64ExpectedSource = new long[]
        {
            0,
            0,
            234,
            -32768,
            32767,
            1,
            -1,
            2,
            3,
            0,
        };

        public static readonly sbyte[] ToSByteExpectedSource = new sbyte[]
        {
            0,
            0,
            -22,
            0,
            -1,
            1,
            -1,
            2,
            3,
            0,
        };

        public static readonly float[] ToSingleExpectedSource = new float[]
        {
            0,
            0.00357055664f,
            234,
            -32768,
            32768,
            1,
            -1,
            2.73353577f,
            3.1415863f,
            1.52587891e-5f,
        };

        public static readonly ushort[] ToUInt16ExpectedSource = new ushort[]
        {
            0,
            0,
            234,
            32768,
            32767,
            1,
            65535,
            2,
            3,
            0,
        };

        public static readonly uint[] ToUInt32ExpectedSource = new uint[]
        {
            0,
            0,
            234,
            4294934528,
            32767,
            1,
            4294967295,
            2,
            3,
            0,
        };

        public static readonly ulong[] ToUInt64ExpectedSource = new ulong[]
        {
            0,
            0,
            234,
            18446744073709518848,
            32767,
            1,
            18446744073709551615,
            2,
            3,
            0,
        };

        public static readonly object[] FromByteSource = new object[]
        {
            new object[] { (byte)0, 0x00000000 },
            new object[] { (byte)1, 0x00010000 },
            new object[] { (byte)234, 0x00EA0000 },
            new object[] { (byte)255, 0x00FF0000 },
        };

        public static readonly object[] FromSByteSource = new object[]
        {
            new object[] { (sbyte)0, 0x00000000 },
            new object[] { (sbyte)1, 0x00010000 },
            new object[] { (sbyte)-123, -0x007B0000 },
            new object[] { (sbyte)127, 0x007F0000 },
        };

        public static readonly object[] FromUInt16Source = new object[]
        {
            new object[] { (ushort)0, 0x00000000 },
            new object[] { (ushort)1, 0x00010000 },
            new object[] { (ushort)234, 0x00EA0000 },
            new object[] { (ushort)65535, -0x00010000 },
        };

        public static readonly object[] FromInt16Source = new object[]
        {
            new object[] { (short)0, 0x00000000 },
            new object[] { (short)1, 0x00010000 },
            new object[] { (short)-234, -0x00EA0000 },
            new object[] { (short)32767, 0x7FFF0000 },
        };

        public static readonly object[] FromUInt32Source = new object[]
        {
            new object[] { (uint)0, 0x00000000 },
            new object[] { (uint)1, 0x00010000 },
            new object[] { (uint)234, 0x00EA0000 },
            new object[] { (uint)65535, -0x00010000 },
        };

        public static readonly object[] FromInt32Source = new object[]
        {
            new object[] { 0, 0x00000000 },
            new object[] { 1, 0x00010000 },
            new object[] { -234, -0x00EA0000 },
            new object[] { 32767, 0x7FFF0000 },
        };

        public static readonly object[] FromUInt64Source = new object[]
        {
            new object[] { (ulong)0, 0x00000000 },
            new object[] { (ulong)1, 0x00010000 },
            new object[] { (ulong)234, 0x00EA0000 },
            new object[] { (ulong)65535, -0x00010000 },
        };

        public static readonly object[] FromInt64Source = new object[]
        {
            new object[] { (long)0, 0x00000000 },
            new object[] { (long)1, 0x00010000 },
            new object[] { (long)-234, -0x00EA0000 },
            new object[] { (long)32767, 0x7FFF0000 },
        };

        public static readonly object[] FromHalfSource = new object[]
        {
            new object[] { (Half)0, 0x00000000 },
            new object[] { (Half)1, 0x00010000 },
            new object[] { (Half)(-234), -0x00EA0000 },
            new object[] { (Half)0.5, 0x00008000 },
        };

        public static readonly object[] FromSingleSource = new object[]
        {
            new object[] { 0f, 0x00000000 },
            new object[] { 1f, 0x00010000 },
            new object[] { -234f, -0x00EA0000 },
            new object[] { 0.5f, 0x00008000 },
        };

        public static readonly object[] FromDoubleSource = new object[]
        {
            new object[] { 0d, 0x00000000 },
            new object[] { 1d, 0x00010000 },
            new object[] { -234d, -0x00EA0000 },
            new object[] { 0.5d, 0x00008000 },
        };

        public static readonly object[] FromDecimalSource = new object[]
        {
            new object[] { 0m, 0x00000000 },
            new object[] { 1m, 0x00010000 },
            new object[] { -234m, -0x00EA0000 },
            new object[] { 0.5m, 0x00008000 },
        };

        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            for (var i = 0; i < Fix16ConversionSource.Length; i++)
            {
                yield return new object[] { Fix16ConversionSource[i], ToStringInvariantCultureExpected[i], CultureInfo.InvariantCulture };
                yield return new object[] { Fix16ConversionSource[i], ToStringUnitedStatesExpected[i], CultureInfo.CreateSpecificCulture("en-US") };
                yield return new object[] { Fix16ConversionSource[i], ToStringSouthAfricanExpected[i], CultureInfo.CreateSpecificCulture("en-ZA") };
                yield return new object[] { Fix16ConversionSource[i], ToStringIndianUrduExpected[i], CultureInfo.CreateSpecificCulture("ur-IN") };

            }
        }

        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(Fix16.Epsilon, Is.EqualTo(1));

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(Fix16.ToString(163840), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator"), Category("ToString")]
        public void ToStringTest1(int value,
                                  string expected,
                                  CultureInfo culture) =>
            Assert.That(Fix16.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

        [Test, Sequential, Category("ToBoolean")]
        public void ToBooleanTest([ValueSource("Fix16ConversionSource")] int value,
                                  [ValueSource("ToBooleanExpectedSource")] bool expected) =>
            Assert.That(Fix16.ToBoolean(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToByte")]
        public void ToByteTest([ValueSource("Fix16ConversionSource")] int value,
                               [ValueSource("ToByteExpectedSource")]  byte expected) =>
            Assert.That(Fix16.ToByte(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToDecimal")]
        public void ToDecimalTest([ValueSource("Fix16ConversionSource")]   int value,
                                  [ValueSource("ToDecimalExpectedSource")] decimal expected) =>
            Assert.That(Fix16.ToDecimal(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToDouble")]
        public void ToDoubleTest([ValueSource("Fix16ConversionSource")]  int value,
                                 [ValueSource("ToDoubleExpectedSource")] double expected) =>
            Assert.That(Fix16.ToDouble(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToHalf")]
        public void ToHalfTest([ValueSource("Fix16ConversionSource")] int value,
                               [ValueSource("ToHalfExpectedSource")]  Half expected) =>
            Assert.That(Fix16.ToHalf(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToShort")]
        public void ToInt16Test([ValueSource("Fix16ConversionSource")] int value,
                                [ValueSource("ToInt16ExpectedSource")] short expected) =>
            Assert.That(Fix16.ToInt16(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToInt32")]
        public void ToInt32Test([ValueSource("Fix16ConversionSource")] int value,
                                [ValueSource("ToInt32ExpectedSource")] int expected) =>
            Assert.That(Fix16.ToInt32(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToInt64")]
        public void ToInt64Test([ValueSource("Fix16ConversionSource")] int value,
                                [ValueSource("ToInt64ExpectedSource")] long expected) =>
            Assert.That(Fix16.ToInt64(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToSByte")]
        public void ToSByteTest([ValueSource("Fix16ConversionSource")] int value,
                                [ValueSource("ToSByteExpectedSource")] sbyte expected) =>
            Assert.That(Fix16.ToSByte(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToSingle")]
        public void ToSingleTest([ValueSource("Fix16ConversionSource")]  int value,
                                 [ValueSource("ToSingleExpectedSource")] float expected) =>
            Assert.That(Fix16.ToSingle(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected,
                                  [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(Fix16.ToString(0x00008000, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test, Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Fix16.ToType(0x00100000, null!, null));

        [Test, Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => Fix16.ToType(0x00100000, typeof(DateTime), null));

        [Test, Sequential, Category("ToType")]
        public void ToTypeTest_conversionType([ValueSource("ToTypeTypeSource")] Type type,
                                              [ValueSource("ToTypeExpectedSource")] object expected) =>
            Assert.That(Fix16.ToType(0x00100000, type, null), Is.EqualTo(expected));

        [Test, Sequential, Category("ToUInt16")]
        public void ToUInt16Test([ValueSource("Fix16ConversionSource")]  int value,
                                 [ValueSource("ToUInt16ExpectedSource")] ushort expected) =>
            Assert.That(Fix16.ToUInt16(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToUInt32")]
        public void ToUInt32Test([ValueSource("Fix16ConversionSource")] int value,
                                 [ValueSource("ToUInt32ExpectedSource")] uint expected) =>
            Assert.That(Fix16.ToUInt32(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToUInt64")]
        public void ToUInt64Test([ValueSource("Fix16ConversionSource")] int value,
                                 [ValueSource("ToUInt64ExpectedSource")] ulong expected) =>
            Assert.That(Fix16.ToUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromByteSource"), Category("FromByte")]
        public void FromByteTest(byte value, int expected) =>
            Assert.That(Fix16.FromByte(value), Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource"), Category("FromSByte")]
        public void FromSByteTest(sbyte value, int expected) =>
            Assert.That(Fix16.FromSByte(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source"), Category("FromUInt16")]
        public void FromUInt16Test(ushort value, int expected) =>
            Assert.That(Fix16.FromUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source"), Category("FromInt16")]
        public void FromInt16Test(short value, int expected) =>
            Assert.That(Fix16.FromInt16(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source"), Category("FromUInt32")]
        public void FromUInt32Test(uint value, int expected) =>
            Assert.That(Fix16.FromUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source"), Category("FromInt32")]
        public void FromInt32Test(int value, int expected) =>
            Assert.That(Fix16.FromInt32(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source"), Category("FromUInt64")]
        public void FromUInt64Test(ulong value, int expected) =>
            Assert.That(Fix16.FromUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source"), Category("FromInt64")]
        public void FromInt64Test(long value, int expected) =>
            Assert.That(Fix16.FromInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource"), Category("FromHalf")]
        public void FromHalfTest(Half value, int expected) =>
            Assert.That(Fix16.FromHalf(value), Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource"), Category("FromSingle")]
        public void FromSingleTest(float value, int expected) =>
            Assert.That(Fix16.FromSingle(value), Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource"), Category("FromDouble")]
        public void FromDoubleTest(double value, int expected) =>
            Assert.That(Fix16.FromDouble(value), Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource"), Category("FromDecimal")]
        public void FromDecimalTest(decimal value, int expected) =>
            Assert.That(Fix16.FromDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("FromByteSource"), Category("ToFix16")]
        public void ToFix16Test(byte value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource"), Category("ToFix16")]
        public void ToFix16Test1(sbyte value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source"), Category("ToFix16")]
        public void ToFix16Test2(ushort value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source"), Category("ToFix16")]
        public void ToFix16Test3(short value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source"), Category("ToFix16")]
        public void ToFix16Test4(uint value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source"), Category("ToFix16")]
        public void ToFix16Test5(int value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source"), Category("ToFix16")]
        public void ToFix16Test6(ulong value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source"), Category("ToFix16")]
        public void ToFix16Test7(long value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource"), Category("ToFix16")]
        public void ToFix16Test8(Half value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource"), Category("ToFix16")]
        public void ToFix16Test9(float value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource"), Category("ToFix16")]
        public void ToFix16Test10(double value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource"), Category("ToFix16")]
        public void ToFix16Test11(decimal value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));
    }
}