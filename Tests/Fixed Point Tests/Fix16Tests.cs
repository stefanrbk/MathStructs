using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;

namespace System.Tests
{
    [TestFixture]
    public class Fix16Tests
    {
        public static readonly int[] Fix16ConversionSource = new int[10]
    {
        0,
        234,
        15335424,
        -2147483648,
        2147483647,
        65536,
        -65536,
        179145,
        205887,
        1
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
        1048576
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
            0
        },
        new object[2]
        {
            (byte)1,
            65536
        },
        new object[2]
        {
            (byte)234,
            15335424
        },
        new object[2]
        {
            Byte.MaxValue,
            16711680
        }
    };

        public static readonly object[] FromSByteSource = new object[4]
    {
        new object[2]
        {
            (sbyte)0,
            0
        },
        new object[2]
        {
            (sbyte)1,
            65536
        },
        new object[2]
        {
            (sbyte)-123,
            -8060928
        },
        new object[2]
        {
            SByte.MaxValue,
            8323072
        }
    };

        public static readonly object[] FromUInt16Source = new object[4]
    {
        new object[2]
        {
            (ushort)0,
            0
        },
        new object[2]
        {
            (ushort)1,
            65536
        },
        new object[2]
        {
            (ushort)234,
            15335424
        },
        new object[2]
        {
            UInt16.MaxValue,
            -65536
        }
    };

        public static readonly object[] FromInt16Source = new object[4]
    {
        new object[2]
        {
            (short)0,
            0
        },
        new object[2]
        {
            (short)1,
            65536
        },
        new object[2]
        {
            (short)-234,
            -15335424
        },
        new object[2]
        {
            Int16.MaxValue,
            2147418112
        }
    };

        public static readonly object[] FromUInt32Source = new object[4]
    {
        new object[2]
        {
            0u,
            0
        },
        new object[2]
        {
            1u,
            65536
        },
        new object[2]
        {
            234u,
            15335424
        },
        new object[2]
        {
            65535u,
            -65536
        }
    };

        public static readonly object[] FromInt32Source = new object[4]
    {
        new object[2]
        {
            0,
            0
        },
        new object[2]
        {
            1,
            65536
        },
        new object[2]
        {
            -234,
            -15335424
        },
        new object[2]
        {
            32767,
            2147418112
        }
    };

        public static readonly object[] FromUInt64Source = new object[4]
    {
        new object[2]
        {
            0uL,
            0
        },
        new object[2]
        {
            1uL,
            65536
        },
        new object[2]
        {
            234uL,
            15335424
        },
        new object[2]
        {
            65535uL,
            -65536
        }
    };

        public static readonly object[] FromInt64Source = new object[4]
    {
        new object[2]
        {
            0L,
            0
        },
        new object[2]
        {
            1L,
            65536
        },
        new object[2]
        {
            -234L,
            -15335424
        },
        new object[2]
        {
            32767L,
            2147418112
        }
    };

        public static readonly object[] FromHalfSource = new object[4]
    {
        new object[2]
        {
            (Half)0f,
            0
        },
        new object[2]
        {
            (Half)1f,
            65536
        },
        new object[2]
        {
            (Half)(-234f),
            -15335424
        },
        new object[2]
        {
            (Half)0.5,
            32768
        }
    };

        public static readonly object[] FromSingleSource = new object[4]
    {
        new object[2]
        {
            0f,
            0
        },
        new object[2]
        {
            1f,
            65536
        },
        new object[2]
        {
            -234f,
            -15335424
        },
        new object[2]
        {
            0.5f,
            32768
        }
    };

        public static readonly object[] FromDoubleSource = new object[4]
    {
        new object[2]
        {
            0.0,
            0
        },
        new object[2]
        {
            1.0,
            65536
        },
        new object[2]
        {
            -234.0,
            -15335424
        },
        new object[2]
        {
            0.5,
            32768
        }
    };

        public static readonly object[] FromDecimalSource = new object[4]
    {
        new object[2]
        {
            0m,
            0
        },
        new object[2]
        {
            1m,
            65536
        },
        new object[2]
        {
            -234m,
            -15335424
        },
        new object[2]
        {
            0.5m,
            32768
        }
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

        [Test]
        [Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(1, Is.EqualTo(1));

        [Test]
        [Category("ToString")]
        public void ToStringTest() =>
            Assert.That(Fix16.ToString(163840), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator")]
        [Category("ToString")]
        public void ToStringTest1(int value, string expected, CultureInfo culture) =>
            Assert.That(Fix16.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToBoolean")]
        public void ToBooleanTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToBooleanExpectedSource")] bool expected) =>
            Assert.That(Fix16.ToBoolean(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToByte")]
        public void ToByteTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToByteExpectedSource")] byte expected) =>
            Assert.That(Fix16.ToByte(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToDecimal")]
        public void ToDecimalTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToDecimalExpectedSource")] decimal expected) =>
            Assert.That(Fix16.ToDecimal(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToDouble")]
        public void ToDoubleTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToDoubleExpectedSource")] double expected) =>
            Assert.That(Fix16.ToDouble(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToHalf")]
        public void ToHalfTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToHalfExpectedSource")] Half expected) =>
            Assert.That(Fix16.ToHalf(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToShort")]
        public void ToInt16Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToInt16ExpectedSource")] short expected) =>
            Assert.That(Fix16.ToInt16(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToInt32")]
        public void ToInt32Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToInt32ExpectedSource")] int expected) =>
            Assert.That(Fix16.ToInt32(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToInt64")]
        public void ToInt64Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToInt64ExpectedSource")] long expected) =>
            Assert.That(Fix16.ToInt64(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToSByte")]
        public void ToSByteTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToSByteExpectedSource")] sbyte expected) =>
            Assert.That(Fix16.ToSByte(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToSingle")]
        public void ToSingleTest([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToSingleExpectedSource")] float expected) =>
            Assert.That(Fix16.ToSingle(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected, [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(Fix16.ToString(32768, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => Fix16.ToType(1048576, null!, null));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => Fix16.ToType(1048576, typeof(DateTime), null));

        [Test]
        [Sequential]
        [Category("ToType")]
        public void ToTypeTest_conversionType([ValueSource("ToTypeTypeSource")] Type type, [ValueSource("ToTypeExpectedSource")] object expected) =>
            Assert.That(Fix16.ToType(1048576, type, null), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt16")]
        public void ToUInt16Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToUInt16ExpectedSource")] ushort expected) =>
            Assert.That(Fix16.ToUInt16(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt32")]
        public void ToUInt32Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToUInt32ExpectedSource")] uint expected) =>
            Assert.That(Fix16.ToUInt32(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToUInt64")]
        public void ToUInt64Test([ValueSource("Fix16ConversionSource")] int value, [ValueSource("ToUInt64ExpectedSource")] ulong expected) =>
            Assert.That(Fix16.ToUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromByteSource")]
        [Category("FromByte")]
        public void FromByteTest(byte value, int expected) =>
            Assert.That(Fix16.FromByte(value), Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource")]
        [Category("FromSByte")]
        public void FromSByteTest(sbyte value, int expected) =>
            Assert.That(Fix16.FromSByte(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source")]
        [Category("FromUInt16")]
        public void FromUInt16Test(ushort value, int expected) =>
            Assert.That(Fix16.FromUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source")]
        [Category("FromInt16")]
        public void FromInt16Test(short value, int expected) =>
            Assert.That(Fix16.FromInt16(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source")]
        [Category("FromUInt32")]
        public void FromUInt32Test(uint value, int expected) =>
            Assert.That(Fix16.FromUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source")]
        [Category("FromInt32")]
        public void FromInt32Test(int value, int expected) =>
            Assert.That(Fix16.FromInt32(value), Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source")]
        [Category("FromUInt64")]
        public void FromUInt64Test(ulong value, int expected) =>
            Assert.That(Fix16.FromUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source")]
        [Category("FromInt64")]
        public void FromInt64Test(long value, int expected) =>
            Assert.That(Fix16.FromInt64(value), Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource")]
        [Category("FromHalf")]
        public void FromHalfTest(Half value, int expected) =>
            Assert.That(Fix16.FromHalf(value), Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource")]
        [Category("FromSingle")]
        public void FromSingleTest(float value, int expected) =>
            Assert.That(Fix16.FromSingle(value), Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource")]
        [Category("FromDouble")]
        public void FromDoubleTest(double value, int expected) =>
            Assert.That(Fix16.FromDouble(value), Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource")]
        [Category("FromDecimal")]
        public void FromDecimalTest(decimal value, int expected) =>
            Assert.That(Fix16.FromDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("FromByteSource")]
        [Category("ToFix16")]
        public void ToFix16Test(byte value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromSByteSource")]
        [Category("ToFix16")]
        public void ToFix16Test1(sbyte value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt16Source")]
        [Category("ToFix16")]
        public void ToFix16Test2(ushort value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt16Source")]
        [Category("ToFix16")]
        public void ToFix16Test3(short value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt32Source")]
        [Category("ToFix16")]
        public void ToFix16Test4(uint value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt32Source")]
        [Category("ToFix16")]
        public void ToFix16Test5(int value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromUInt64Source")]
        [Category("ToFix16")]
        public void ToFix16Test6(ulong value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromInt64Source")]
        [Category("ToFix16")]
        public void ToFix16Test7(long value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromHalfSource")]
        [Category("ToFix16")]
        public void ToFix16Test8(Half value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromSingleSource")]
        [Category("ToFix16")]
        public void ToFix16Test9(float value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromDoubleSource")]
        [Category("ToFix16")]
        public void ToFix16Test10(double value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));

        [TestCaseSource("FromDecimalSource")]
        [Category("ToFix16")]
        public void ToFix16Test11(decimal value, int expected) =>
            Assert.That(value.ToFix16(), Is.EqualTo(expected));
    }
}
