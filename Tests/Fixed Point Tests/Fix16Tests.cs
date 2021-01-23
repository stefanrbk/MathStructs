using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    [TestFixture]
    public class Fix16Tests
    {
        #region Test Setup
        public const double Fix16Max = 32767+(65535/65536.0);

        public const double Fix16Min = -32768;

        public static readonly Fix16[] Fix16ConversionSource = new Fix16[10]
    {
        Fix16.Raw(0),
        Fix16.Raw(234),
        Fix16.Raw(15335424),
        Fix16.Raw(-2147483648),
        Fix16.Raw(2147483647),
        Fix16.Raw(65536),
        Fix16.Raw(-65536),
        Fix16.Raw(179145),
        Fix16.Raw(205887),
        Fix16.Raw(1)
    };

        public static readonly string[] ToStringInvariantCultureExpected = new string[10]
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
        "0.0000152587890625"
    };

        public static readonly string[] ToStringUnitedStatesExpected = new string[10]
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
        "0.0000152587890625"
    };

        public static readonly string[] ToStringSouthAfricanExpected = new string[10]
    {
        "0",
        "0,003570556640625",
        "234",
        "-32\u00a0768",
        "32\u00a0767,9999847412",
        "1",
        "-1",
        "2,73353576660156",
        "3,14158630371094",
        "0,0000152587890625"
    };

        public static readonly string[] ToStringIndianUrduExpected = new string[10]
    {
        "0",
        "0٫003570556640625",
        "234",
        "\u200e-\u200e32٬768",
        "32٬767٫9999847412",
        "1",
        "\u200e-\u200e1",
        "2٫73353576660156",
        "3٫14158630371094",
        "0٫0000152587890625"
    };

        public static readonly bool[] ToBooleanExpectedSource = new bool[10]
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
        true
    };

        public static readonly byte[] ToByteExpectedSource = new byte[10]
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
        0
    };

        public static readonly decimal[] ToDecimalExpectedSource = new decimal[10]
    {
        0m,
        0.003570556640625m,
        234m,
        -32768m,
        32767.9999847412m,
        1m,
        -1m,
        2.73353576660156m,
        3.14158630371094m,
        0.0000152587890625m
    };

        public static readonly double[] ToDoubleExpectedSource = new double[10]
    {
        0.0,
        0.003570556640625,
        234.0,
        -32768.0,
        32767.999984741211,
        1.0,
        -1.0,
        2.7335357666015625,
        3.1415863037109375,
        1.52587890625E-05
    };

        public static readonly Half[] ToHalfExpectedSource = new Half[10]
    {
        (Half)0f,
        (Half)0.00357,
        (Half)234f,
        (Half)(-32770f),
        (Half)32770f,
        (Half)1f,
        (Half)(-1f),
        (Half)2.734,
        (Half)3.14,
        (Half)1.526E-05
    };

        public static readonly short[] ToInt16ExpectedSource = new short[10]
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
        0
    };

        public static readonly int[] ToInt32ExpectedSource = new int[10]
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
        0
    };

        public static readonly Type[] ToTypeTypeSource = new Type[15]
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

        public static readonly object[] ToTypeExpectedSource = new object[15]
    {
        true,
        (sbyte)16,
        (byte)16,
        (short)16,
        (ushort)16,
        16,
        16u,
        16L,
        16uL,
        (Half)16f,
        16f,
        16.0,
        16m,
        "16",
        Fix16.Raw(1048576)
    };

        public static readonly long[] ToInt64ExpectedSource = new long[10]
    {
        0L,
        0L,
        234L,
        -32768L,
        32767L,
        1L,
        -1L,
        2L,
        3L,
        0L
    };

        public static readonly sbyte[] ToSByteExpectedSource = new sbyte[10]
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
        0
    };

        public static readonly float[] ToSingleExpectedSource = new float[10]
    {
        0f,
        0.00357055664f,
        234f,
        -32768f,
        32768f,
        1f,
        -1f,
        2.73353577f,
        3.1415863f,
        1.52587891E-05f
    };

        public static readonly ushort[] ToUInt16ExpectedSource = new ushort[10]
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
        0
    };

        public static readonly uint[] ToUInt32ExpectedSource = new uint[10]
    {
        0u,
        0u,
        234u,
        4294934528u,
        32767u,
        1u,
        4294967295u,
        2u,
        3u,
        0u
    };

        public static readonly ulong[] ToUInt64ExpectedSource = new ulong[10]
    {
        0uL,
        0uL,
        234uL,
        18446744073709518848uL,
        32767uL,
        1uL,
        18446744073709551615uL,
        2uL,
        3uL,
        0uL
    };

        public static readonly object[] FromByteSource = new object[4]
    {
        new object[2]
        {
            (byte)0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            (byte)1,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            (byte)234,
            Fix16.Raw(15335424)
        },
        new object[2]
        {
            Byte.MaxValue,
            Fix16.Raw(16711680)
        }
    };

        public static readonly object[] FromSByteSource = new object[4]
    {
        new object[2]
        {
            (sbyte)0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            (sbyte)1,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            (sbyte)-123,
            Fix16.Raw(-8060928)
        },
        new object[2]
        {
            SByte.MaxValue,
            Fix16.Raw(8323072)
        }
    };

        public static readonly object[] FromUInt16Source = new object[4]
    {
        new object[2]
        {
            (ushort)0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            (ushort)1,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            (ushort)234,
            Fix16.Raw(15335424)
        },
        new object[2]
        {
            UInt16.MaxValue,
            Fix16.Raw(-65536)
        }
    };

        public static readonly object[] FromInt16Source = new object[4]
    {
        new object[2]
        {
            (short)0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            (short)1,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            (short)-234,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            Int16.MaxValue,
            Fix16.Raw(2147418112)
        }
    };

        public static readonly object[] FromUInt32Source = new object[4]
    {
        new object[2]
        {
            0u,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1u,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            234u,
            Fix16.Raw(15335424)
        },
        new object[2]
        {
            65535u,
            Fix16.Raw(-65536)
        }
    };

        public static readonly object[] FromInt32Source = new object[4]
    {
        new object[2]
        {
            0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            -234,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            32767,
            Fix16.Raw(2147418112)
        }
    };

        public static readonly object[] FromUInt64Source = new object[4]
    {
        new object[2]
        {
            0uL,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1uL,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            234uL,
            Fix16.Raw(15335424)
        },
        new object[2]
        {
            65535uL,
            Fix16.Raw(-65536)
        }
    };

        public static readonly object[] FromInt64Source = new object[4]
    {
        new object[2]
        {
            0L,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1L,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            -234L,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            32767L,
            Fix16.Raw(2147418112)
        }
    };

        public static readonly object[] FromHalfSource = new object[4]
    {
        new object[2]
        {
            (Half)0f,
            Fix16.Raw(0)
        },
        new object[2]
        {
            (Half)1f,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            (Half)(-234f),
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            (Half)0.5,
            Fix16.Raw(32768)
        }
    };

        public static readonly object[] FromSingleSource = new object[4]
    {
        new object[2]
        {
            0f,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1f,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            -234f,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            0.5f,
            Fix16.Raw(32768)
        }
    };

        public static readonly object[] FromDoubleSource = new object[4]
    {
        new object[2]
        {
            0.0,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1.0,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            -234.0,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            0.5,
            Fix16.Raw(32768)
        }
    };

        public static readonly object[] FromDecimalSource = new object[4]
    {
        new object[2]
        {
            0m,
            Fix16.Raw(0)
        },
        new object[2]
        {
            1m,
            Fix16.Raw(65536)
        },
        new object[2]
        {
            -234m,
            Fix16.Raw(-15335424)
        },
        new object[2]
        {
            0.5m,
            Fix16.Raw(32768)
        }
    };

        public static readonly Fix16[] ExpSource = new Fix16[]
        {
            Fix16.Zero,
            Fix16.One,
            Fix16.Raw(681392),
            Fix16.Raw(-772244),
            (Fix16)2,
            (Fix16)2,
        };

        public static readonly Fix16[][] SqrtSource = new Fix16[][]
        {
            new Fix16[] { (Fix16)25, (Fix16)5 },
            new Fix16[] { (Fix16)16385, (Fix16)(-127.994140625) },
            new Fix16[] { (Fix16)(-0.0625), (Fix16)(-0.25) },
        };

        public static readonly double[] SinSource = new double[]
        {
            (double)Fix16.Zero,
            (double)Fix16.Pi,
            (double)(Fix16.Pi + (Fix16)0.2),
            (double)-Fix16.Pi,
            (double)(-Fix16.Pi -(Fix16)0.2),
            1.57080078125,
            -1.57080078125,
        };

        public static readonly object[] EqualsSource1 = new object[]
        {
            new object[] { Fix16.One, true },
            new object[] { null!, false },
            new object[] { new object(), false },
        };

        public static readonly object[] EqualsSource2 = new object[]
        {
            new object[] { Fix16.One, Fix16.Zero, true },
            new object[] { Fix16.Zero, Fix16.Pi, true },
            new object[] { Fix16.Pi, Fix16.One, false },
        };

        public static readonly object[] CompareToSource1 = new object[]
        {
            new object[] { null!, Is.EqualTo(1) },
            new object[] { Fix16.Pi, Is.LessThan(0) },
            new object[] { Fix16.NegOne, Is.GreaterThan(0) },
            new object[] { Fix16.One, Is.Zero },
        };

        public static readonly object[] CompareToSource2 = new object[]
        {
            new object[] { Fix16.Pi, Is.LessThan(0) },
            new object[] { Fix16.NegOne, Is.GreaterThan(0) },
            new object[] { Fix16.One, Is.Zero },
        };

        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            for (var i = 0; i < Fix16ConversionSource.Length; i++)
            {
                yield return new object[3]
                {
                Fix16ConversionSource[i],
                ToStringInvariantCultureExpected[i],
                CultureInfo.InvariantCulture
                };
            }
        }
        #endregion Test Setup

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

        [Test, Category("ctor")]
        public void CtorTest() =>
            Assert.That(new Fix16(20), Is.EqualTo(Fix16.Raw(20<<16)));

        [Test, Category("ctor")]
        public void CtorTest2() =>
            Assert.That(new Fix16(2.5), Is.EqualTo(Fix16.Raw(5<<15)));

        [Test, Category("ctor")]
        public void CtorTest3() =>
            Assert.That(new Fix16(0.25m), Is.EqualTo(Fix16.Raw(1<<14)));

        [Test, Sequential, Category("SaturatedAdd")]
        public void SaturatedAddTest([Values(30000, -30000, -30000)] double left,
                                     [Values(10000, -10000,  10000)] double right,
                                     [Values(Fix16Max, Fix16Min, -20000)] double expected) =>
            Assert.That(Fix16.SaturatedAdd((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("SaturatedSubtract")]
        public void SaturatedSubtractTest([Values(30000, -30000, -10000)] double left,
                                          [Values(-10000, 10000, 10000)] double right,
                                          [Values(Fix16Max, Fix16Min, -20000)] double expected) =>
            Assert.That(Fix16.SaturatedSubtract((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("SaturatedMultiply")]
        public void SaturatedMultiplyTest([Values(30, -30, 30000)] double left,
                                          [Values(10, 30000, 2)] double right,
                                          [Values(300, Fix16Min, Fix16Max)] double expected) =>
            Assert.That(Fix16.SaturatedMultiply((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("SaturatedDivide")]
        public void SaturatedDivideTest([Values(30, -10000, 30000)] double left,
                                        [Values(10, 0.125, 0.5)] double right,
                                        [Values(3, Fix16Min, Fix16Max)] double expected) =>
            Assert.That(Fix16.SaturatedDivide((Fix16)left, (Fix16)right), Is.EqualTo((Fix16)expected));

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(new Fix16(2.5).ToString(), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator")]
        [Category("ToString")]
        public void ToStringTest1(Fix16 value, string expected, CultureInfo culture) =>
            Assert.That(value.ToString("#,0.################", culture), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToBoolean")]
        public void ToBooleanTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToBooleanExpectedSource")] bool expected) =>
            Assert.That(value.ToBoolean(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToByte")]
        public void ToByteTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToByteExpectedSource")] byte expected) =>
            Assert.That(value.ToByte(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToDecimal")]
        public void ToDecimalTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToDecimalExpectedSource")] decimal expected) =>
            Assert.That(value.ToDecimal(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToDouble")]
        public void ToDoubleTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToDoubleExpectedSource")] double expected) =>
            Assert.That(value.ToDouble(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToHalf")]
        public void ToHalfTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToHalfExpectedSource")] Half expected) =>
            Assert.That(value.ToHalf(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToShort")]
        public void ToInt16Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToInt16ExpectedSource")] short expected) =>
            Assert.That(value.ToInt16(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToInt32")]
        public void ToInt32Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToInt32ExpectedSource")] int expected) =>
            Assert.That(value.ToInt32(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToInt64")]
        public void ToInt64Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToInt64ExpectedSource")] long expected) =>
            Assert.That(value.ToInt64(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToSByte")]
        public void ToSByteTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToSByteExpectedSource")] sbyte expected) =>
            Assert.That(value.ToSByte(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToSingle")]
        public void ToSingleTest([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToSingleExpectedSource")] float expected) =>
            Assert.That(value.ToSingle(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected, [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(new Fix16(0.5).ToString(CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Fix16.Raw(1048576).ToType(null!, null));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => Fix16.Raw(1048576).ToType(typeof(DateTime), null));

        [Test]
        [Sequential]
        [Category("ToType")]
        public void ToTypeTest_conversionType([ValueSource("ToTypeTypeSource")] Type type, [ValueSource("ToTypeExpectedSource")] object expected) =>
            Assert.That(Fix16.Raw(1048576).ToType(type, null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt16")]
        public void ToUInt16Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToUInt16ExpectedSource")] ushort expected) =>
            Assert.That(value.ToUInt16(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt32")]
        public void ToUInt32Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToUInt32ExpectedSource")] uint expected) =>
            Assert.That(value.ToUInt32(null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt64")]
        public void ToUInt64Test([ValueSource("Fix16ConversionSource")] Fix16 value, [ValueSource("ToUInt64ExpectedSource")] ulong expected) =>
            Assert.That(value.ToUInt64(null), Is.EqualTo(expected));

        [TestCaseSource("FromByteSource")]
        [Category("FromByte")]
        public void FromByteTest(byte value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource")]
        [Category("FromSByte")]
        public void FromSByteTest(sbyte value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source")]
        [Category("FromUInt16")]
        public void FromUInt16Test(ushort value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source")]
        [Category("FromInt16")]
        public void FromInt16Test(short value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source")]
        [Category("FromUInt32")]
        public void FromUInt32Test(uint value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source")]
        [Category("FromInt32")]
        public void FromInt32Test(int value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source")]
        [Category("FromUInt64")]
        public void FromUInt64Test(ulong value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source")]
        [Category("FromInt64")]
        public void FromInt64Test(long value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource")]
        [Category("FromHalf")]
        public void FromHalfTest(Half value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource")]
        [Category("FromSingle")]
        public void FromSingleTest(float value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource")]
        [Category("FromDouble")]
        public void FromDoubleTest(double value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource")]
        [Category("FromDecimal")]
        public void FromDecimalTest(decimal value, Fix16 expected) =>
            Assert.That((Fix16)value, Is.EqualTo(expected));

        [TestCaseSource("FromByteSource")]
        [Category("ToByte")]
        public void ToByte(byte expected, Fix16 value) =>
            Assert.That((byte)value, Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource")]
        [Category("ToSByte")]
        public void ToSByte(sbyte expected, Fix16 value) =>
            Assert.That((sbyte)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source")]
        [Category("ToUInt16")]
        public void ToUInt16(ushort expected, Fix16 value) =>
            Assert.That((ushort)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source")]
        [Category("ToInt16")]
        public void ToInt16(short expected, Fix16 value) =>
            Assert.That((short)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source")]
        [Category("ToUInt32")]
        public void ToUInt32(uint expected, Fix16 value) =>
            Assert.That((uint)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source")]
        [Category("ToInt32")]
        public void ToInt32(int expected, Fix16 value) =>
            Assert.That((int)value, Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source")]
        [Category("ToUInt64")]
        public void ToUInt64(ulong expected, Fix16 value) =>
            Assert.That((ulong)value, Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source")]
        [Category("ToInt64")]
        public void ToInt64(long expected, Fix16 value) =>
            Assert.That((long)value, Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource")]
        [Category("ToHalf")]
        public void ToHalf(Half expected, Fix16 value) =>
            Assert.That((Half)value, Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource")]
        [Category("ToSingle")]
        public void ToSingle(float expected, Fix16 value) =>
            Assert.That((float)value, Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource")]
        [Category("ToDouble")]
        public void ToDouble(double expected, Fix16 value) =>
            Assert.That((double)value, Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource")]
        [Category("ToDecimal")]
        public void ToDecimal(decimal expected, Fix16 value) =>
            Assert.That((decimal)value, Is.EqualTo(expected));

        [Test, Sequential, Category("op_UnaryNegation")]
        public void NegationTest([Values(-1, 234)] int actual,
                                 [Values(1, -234)] int expected) =>
            Assert.That(-(Fix16)actual, Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("op_UnaryNegation")]
        public void NegationTest2([Values(-1, 234, null)] int? actual,
                                  [Values(1, -234, null)] int? expected) =>
            Assert.That(-(Fix16?)actual, Is.EqualTo((Fix16?)expected));

        [Test, Sequential, Category("op_Multiplication")]
        public void MultiplicationTest([Values(2.5, Int32.MaxValue)] double left,
                                       [Values(11, 1.0 / 65535)] double right,
                                       [Values(27.5, -0.5)] double expected) =>
            Assert.That((Fix16)left * (Fix16)right, Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("op_Multiplication")]
        public void MultiplicationTest4([Values(null, null, 10, 2.5, Int32.MaxValue)] double? left,
                                        [Values(10, null, null, 11, 1.0 / 65535)] double? right,
                                        [Values(null, null, null, 27.5, -0.5)] double? expected) =>
            Assert.That((Fix16?)left * (Fix16?)right, Is.EqualTo((Fix16?)expected));

        [Test, Sequential, Category("op_Division")]
        public void DivisionTest([Values(27.5, -10, 2000, 16384)] double left,
                                 [Values(11, 2, -1000, 0.25)] double right,
                                 [Values(2.5, -5, -2, 0)] double expected) =>
            Assert.That((Fix16)left / (Fix16)right, Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("op_Division")]
        public void DivisionTest2([Values(10, null, null, 27.5, -10, 2)] double? left,
                                  [Values(null, null, 10, 11, 2, -1)] double? right,
                                  [Values(null, null, null, 2.5, -5, -2)] double? expected) =>
            Assert.That((Fix16?)left / (Fix16?)right, Is.EqualTo((Fix16?)expected));

        [Test, Sequential, Category("NullableDivideBy")]
        public void NullableDivideByTest([Values(1, 10, null, null, 27.5, -10, 2)] double? left,
                                         [Values(0, null, null, 10, 11, 2, -1)] double? right,
                                         [Values(null, null, null, null, 2.5, -5, -2)] double? expected) =>
            Assert.That(Fix16.NullableDivideBy((Fix16?)left, (Fix16?)right), Is.EqualTo((Fix16?)expected));

        [Test]
        [Category("op_Division")]
        public void DivisionTest_Divide_by_zero_throws_DivideByZeroException() =>
            Assert.Throws<DivideByZeroException>(() => _ = Fix16.One / Fix16.Zero);

        [Test, Sequential, Category("op_Equality")]
        public void EqualityTest([Values(10, -10, 10)] double left,
                                 [Values(0.5, -10, 10.1)] double right,
                                 [Values(false, true, false)] bool expected) =>
            Assert.That((Fix16)left == (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Equality")]
        public void EqualityTest2([Values(null, 10, -10, 10)] double? left,
                                  [Values(7, 0.5, -10, 10.1)] double right,
                                  [Values(false, false, true, false)] bool expected) =>
            Assert.That((Fix16?)left == (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Equality")]
        public void EqualityTest3([Values(7, 10, -10, 10)] double left,
                                  [Values(null, 0.5, -10, 10.1)] double? right,
                                  [Values(false, false, true, false)] bool expected) =>
            Assert.That((Fix16)left == (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Equality")]
        public void EqualityTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                  [Values(null, 7, null, 0.5, -10, 10.1)] double? right,
                                  [Values(false, false, false, false, true, false)] bool expected) =>
            Assert.That((Fix16?)left == (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Inequality")]
        public void InequalityTest([Values(10, -10, 10)] double left,
                                   [Values(0.5, -10, 10.1)] double right,
                                   [Values(true, false, true)] bool expected) =>
            Assert.That((Fix16)left != (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Inequality")]
        public void InequalityTest2([Values(null, 10, -10, 10)] double? left,
                                    [Values(7, 0.5, -10, 10.1)] double right,
                                    [Values(true, true, false, true)] bool expected) =>
            Assert.That((Fix16?)left != (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Inequality")]
        public void InequalityTest3([Values(7, 10, -10, 10)] double left,
                                    [Values(null, 0.5, -10, 10.1)] double? right,
                                    [Values(true, true, false, true)] bool expected) =>
            Assert.That((Fix16)left != (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Inequality")]
        public void InequalityTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                    [Values(7, null, null, 0.5, -10, 10.1)] double? right,
                                    [Values(true, true, true, true, false, true)] bool expected) =>
            Assert.That((Fix16?)left != (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest([Values(10, -10, 10)] double left,
                                           [Values(0.5, -10, 10.1)] double right,
                                           [Values(true, true, false)] bool expected) =>
            Assert.That((Fix16)left >= (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest2([Values(null, 10, -10, 10)] double? left,
                                            [Values(7, 0.5, -10, 10.1)] double right,
                                            [Values(false, true, true, false)] bool expected) =>
            Assert.That((Fix16?)left >= (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest3([Values(7, 10, -10, 10)] double left,
                                            [Values(null, 0.5, -10, 10.1)] double? right,
                                            [Values(false, true, true, false)] bool expected) =>
            Assert.That((Fix16)left >= (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThanOrEqual")]
        public void GreaterThanOrEqualTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                            [Values(7, null, null, 0.5, -10, 10.1)] double? right,
                                            [Values(false, false, false, true, true, false)] bool expected) =>
            Assert.That((Fix16?)left >= (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest([Values(10, -10, 10)] double left,
                                    [Values(0.5, -10, 10.1)] double right,
                                    [Values(true, false, false)] bool expected) =>
            Assert.That((Fix16)left > (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest2([Values(null, 10, -10, 10)] double? left,
                                     [Values(7, 0.5, -10, 10.1)] double right,
                                     [Values(false, true, false, false)] bool expected) =>
            Assert.That((Fix16?)left > (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest3([Values(7, 10, -10, 10)] double left,
                                     [Values(null, 0.5, -10, 10.1)] double? right,
                                     [Values(false, true, false, false)] bool expected) =>
            Assert.That((Fix16)left > (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_GreaterThan")]
        public void GreaterThanTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                     [Values(7, null, null, 0.5, -10, 10.1)] double? right,
                                     [Values(false, false, false, true, false, false)] bool expected) =>
            Assert.That((Fix16?)left > (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest([Values(10, -10, 10)] double left,
                                        [Values(0.5, -10, 10.1)] double right,
                                        [Values(false, true, true)] bool expected) =>
            Assert.That((Fix16)left <= (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest2([Values(null, 10, -10, 10)] double? left,
                                         [Values(7, 0.5, -10, 10.1)] double right,
                                         [Values(false, false, true, true)] bool expected) =>
            Assert.That((Fix16?)left <= (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest3([Values(7, 10, -10, 10)] double left,
                                         [Values(null, 0.5, -10, 10.1)] double? right,
                                         [Values(false, false, true, true)] bool expected) =>
            Assert.That((Fix16)left <= (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThanOrEqual")]
        public void LessThanOrEqualTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                         [Values(7, null, null, 0.5, -10, 10.1)] double? right,
                                         [Values(false, false, false, false, true, true)] bool expected) =>
            Assert.That((Fix16?)left <= (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThan")]
        public void LessThanTest([Values(10, -10, 10)] double left,
                                 [Values(0.5, -10, 10.1)] double right,
                                 [Values(false, false, true)] bool expected) =>
            Assert.That((Fix16)left < (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThan")]
        public void LessThanTest2([Values(null, 10, -10, 10)] double? left,
                                  [Values(7, 0.5, -10, 10.1)] double right,
                                  [Values(false, false, false, true)] bool expected) =>
            Assert.That((Fix16?)left < (Fix16)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThan")]
        public void LessThanTest3([Values(7, 10, -10, 10)] double left,
                                  [Values(null, 0.5, -10, 10.1)] double? right,
                                  [Values(false, false, false, true)] bool expected) =>
            Assert.That((Fix16)left < (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_LessThan")]
        public void LessThanTest4([Values(null, null, 7, 10, -10, 10)] double? left,
                                  [Values(7, null, null, 0.5, -10, 10.1)] double? right,
                                  [Values(false, false, false, false, false, true)] bool expected) =>
            Assert.That((Fix16?)left < (Fix16?)right, Is.EqualTo(expected));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest([Values(0.5, 1, -10, 7, -3)] double left,
                                 [Values(0.25, 2, 3, -1, -5)] double right) =>
            Assert.That((Fix16)left + (Fix16)right, Is.EqualTo((Fix16)( left + right )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest2([Values(0.5, 1, -10, 7, -3, null)] double? left,
                                  [Values(0.25, 2, 3, -1, -5, 0)] double right) =>
            Assert.That((Fix16?)left + (Fix16)right, Is.EqualTo((Fix16?)( left + right )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest3([Values(0.5, 1, -10, 7, -3, 0)] double left,
                                  [Values(0.25, 2, 3, -1, -5, null)] double? right) =>
            Assert.That((Fix16)left + (Fix16?)right, Is.EqualTo((Fix16?)( left + right )));

        [Test, Sequential, Category("op_Addition")]
        public void AdditionTest4([Values(0.5, 1, -10, 7, -3, 0, null, null)] double? left,
                                  [Values(0.25, 2, 3, -1, -5, null, null, 0)] double? right) =>
            Assert.That((Fix16?)left + (Fix16?)right, Is.EqualTo((Fix16?)( left + right )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest([Values(0.5, 1, -10, 7, -3)] double left,
                                 [Values(0.25, 2, 3, -1, -5)] double right) =>
            Assert.That((Fix16)left - (Fix16)right, Is.EqualTo((Fix16)( left - right )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest2([Values(0.5, 1, -10, 7, -3, null)] double? left,
                                  [Values(0.25, 2, 3, -1, -5, 0)] double right) =>
            Assert.That((Fix16?)left - (Fix16)right, Is.EqualTo((Fix16?)( left - right )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest3([Values(0.5, 1, -10, 7, -3, 0)] double left,
                                  [Values(0.25, 2, 3, -1, -5, null)] double? right) =>
            Assert.That((Fix16)left - (Fix16?)right, Is.EqualTo((Fix16?)( left - right )));

        [Test, Sequential, Category("op_Subtraction")]
        public void SubtractionTest4([Values(0.5, 1, -10, 7, -3, 0, null, null)] double? left,
                                  [Values(0.25, 2, 3, -1, -5, null, null, 0)] double? right) =>
            Assert.That((Fix16?)left - (Fix16?)right, Is.EqualTo((Fix16?)( left - right )));

        [Test, Sequential, Category("Lerp")]
        public void LerpTest([Values(10, 1)] double left,
                             [Values(11, 3)] double right,
                             [Values(0x80, 0x20)] byte fraction,
                             [Values(10.5, 1.25)] double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("Lerp")]
        public void LerpTest2([Values(10, 1)] double left,
                              [Values(11, 3)] double right,
                              [Values((ushort)0x8000u, (ushort)0x2000)] ushort fraction,
                              [Values(10.5, 1.25)] double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [Test, Sequential, Category("Lerp")]
        public void LerpTest3([Values(10, 1)] double left,
                              [Values(11, 3)] double right,
                              [Values(0x80000000u, 0x20000000u)] uint fraction,
                              [Values(10.5, 1.25)] double expected) =>
            Assert.That(Fix16.Lerp((Fix16)left, (Fix16)right, fraction), Is.EqualTo((Fix16)expected));

        [TestCaseSource("ExpSource")]
        [Category("Exp")]
        public void ExpTest(Fix16 value) =>
            Assert.That(Fix16.Exp(value), Is.EqualTo(Math.Exp((double)value) >= (double)Fix16.MaxValue ? Fix16.MaxValue : (Fix16)Math.Exp((double)value)).Using<Fix16>((a,b) => Fix16.Equals(a, b, Fix16.Epsilon * new Fix16(5))));

        [TestCaseSource("SqrtSource")]
        [Category("Sqrt")]
        public void SqrtTest(Fix16 value, Fix16 expected) =>
            Assert.That(Fix16.Sqrt(value), Is.EqualTo(expected));

        [TestCaseSource("SinSource")]
        [Category("Sin")]
        public void SinTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Sin((Fix16)value), Is.EqualTo((Fix16)Math.Sin(value)).Using<Fix16>((a,b) => Fix16.Equals(a, b, (Fix16)0.007)));

        [Test]
        [Category("Sin")]
        public void SinTest2() =>
            Assert.That(Fix16.Sin(Fix16.Zero), Is.EqualTo(Fix16.Sin(Fix16.Zero)));

        [TestCaseSource("SinSource")]
        [Category("Cos")]
        public void CosTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Cos((Fix16)value), Is.EqualTo((Fix16)Math.Cos(value)).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.007)));

        [Test]
        [Category("Tan")]
        public void TanTest([Random(-1.45, 1.45, 10)] double value) =>
            Assert.That(Fix16.Tan((Fix16)value), Is.EqualTo((Fix16)Math.Tan(value)).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.6)));

        [Test]
        [Category("Tan")]
        public void TanTest2() =>
            Assert.That(Fix16.Tan(Fix16.Pi / (Fix16)2), Is.Null);

        [Test]
        [Category("Asin")]
        public void AsinTest([Values(1.0)][Random(-65535.0/65536, 65535/65536, 10)] double value) =>
            Assert.That(Fix16.Asin((Fix16)value), Is.EqualTo((Fix16)Math.Asin(value)).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.011)));

        [Test]
        [Category("Asin")]
        public void AsinTest2() =>
            Assert.That(Fix16.Asin((Fix16)1.1), Is.Null);

        [Test]
        [Category("Acos")]
        public void AcosTest([Random(-65535.0/65536, 65535/65536, 10)] double value) =>
            Assert.That(Fix16.Acos((Fix16)value), Is.EqualTo((Fix16)Math.Acos(value)).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.011)));

        [TestCaseSource("SinSource")]
        [Category("Atan")]
        public void AtanTest([Random(-Math.PI, Math.PI, 10)] double value) =>
            Assert.That(Fix16.Atan((Fix16)value), Is.EqualTo((Fix16)Math.Atan(value)).Using<Fix16>((a, b) => Fix16.Equals(a, b, (Fix16)0.011)));

        [Test, Category("Atan2")]
        public void Atan2Test([Random(-Math.PI, Math.PI, 10)]double x, [Random(-Math.PI, Math.PI, 10)] double y) =>
            Assert.That(Fix16.Atan2((Fix16)y, (Fix16)x), Is.EqualTo((Fix16)Math.Atan2(y, x)).Using<Fix16>((a,b) => Fix16.Equals(a, b, (Fix16)0.011)));

        [Test]
        [Category("Atan")]
        public void AtanTest2() =>
            Assert.That(Fix16.Atan(Fix16.Zero), Is.EqualTo(Fix16.Atan(Fix16.Zero)));

        [Test]
        [Category("GetTypeCode")]
        public void GetTypeCodeTest() =>
            Assert.That(Fix16.Zero.GetTypeCode(), Is.EqualTo((TypeCode)100));

        [Test]
        [Category("ToChar")]
        public void ToChar_throws_InvalidCastExceptionTest() =>
            Assert.Throws<InvalidCastException>(() => Fix16.Zero.ToChar(null));

        [Test]
        [Category("ToDateTime")]
        public void ToDateTime_throws_InvalidCastExceptionTest() =>
            Assert.Throws<InvalidCastException>(() => Fix16.Zero.ToDateTime(null));

        [TestCaseSource("EqualsSource1")]
        [Category("Equals")]
        public void EqualsTest(object value, bool expected) =>
            Assert.That(Fix16.One.Equals(value), Is.EqualTo(expected));

        [Test]
        [Category("GetHashCode")]
        public void GetHashCodeTest() =>
            Assert.That(Fix16.Pi.GetHashCode(), Is.EqualTo(HashCode.Combine(205887, 2)));

        [TestCaseSource("CompareToSource1")]
        [Category("CompareTo")]
        public void CompareToTest(object? value, IResolveConstraint expected) =>
            Assert.That(Fix16.One.CompareTo(value), expected);

        [Test, Category("CompareTo")]
        public void CompareTo_is_not_Fix16_throws_ArgumentExceptionTest() =>
            Assert.Throws<ArgumentException>(() => Fix16.One.CompareTo(1));

        [TestCaseSource("CompareToSource2")]
        [Category("CompareTo")]
        public void CompareToTest2(Fix16 value, IResolveConstraint expected) =>
            Assert.That(Fix16.One.CompareTo(value), expected);

        [TestCaseSource("EqualsSource2")]
        public void EqualsTest2(Fix16 value, Fix16 delta, bool expected) =>
            Assert.That(Fix16.Equals(Fix16.One, value, delta), Is.EqualTo(expected));
    }
}
