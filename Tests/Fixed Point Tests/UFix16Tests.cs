using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace System.Tests
{
    [TestFixture]
    public class UFix16Tests
    {
        #region Test Setup
        public const double UFix16Max = 65535+(65535/65536.0);

        public const double UFix16Min = 0;

        //    public static readonly UFix16[] Fix16ConversionSource = new UFix16[10]
        //{
        //    UFix16.Raw(0),
        //    UFix16.Raw(234),
        //    UFix16.Raw(15335424),
        //    UFix16.Raw(-2147483648),
        //    UFix16.Raw(2147483647),
        //    UFix16.Raw(65536),
        //    UFix16.Raw(-65536),
        //    UFix16.Raw(179145),
        //    UFix16.Raw(205887),
        //    UFix16.Raw(1)
        //};

        //    public static readonly string[] ToStringInvariantCultureExpected = new string[10]
        //{
        //    "0",
        //    "0.003570556640625",
        //    "234",
        //    "-32,768",
        //    "32,767.9999847412",
        //    "1",
        //    "-1",
        //    "2.73353576660156",
        //    "3.14158630371094",
        //    "0.0000152587890625"
        //};

        //    public static readonly string[] ToStringUnitedStatesExpected = new string[10]
        //{
        //    "0",
        //    "0.003570556640625",
        //    "234",
        //    "-32,768",
        //    "32,767.9999847412",
        //    "1",
        //    "-1",
        //    "2.73353576660156",
        //    "3.14158630371094",
        //    "0.0000152587890625"
        //};

        //    public static readonly string[] ToStringSouthAfricanExpected = new string[10]
        //{
        //    "0",
        //    "0,003570556640625",
        //    "234",
        //    "-32\u00a0768",
        //    "32\u00a0767,9999847412",
        //    "1",
        //    "-1",
        //    "2,73353576660156",
        //    "3,14158630371094",
        //    "0,0000152587890625"
        //};

        //    public static readonly string[] ToStringIndianUrduExpected = new string[10]
        //{
        //    "0",
        //    "0٫003570556640625",
        //    "234",
        //    "\u200e-\u200e32٬768",
        //    "32٬767٫9999847412",
        //    "1",
        //    "\u200e-\u200e1",
        //    "2٫73353576660156",
        //    "3٫14158630371094",
        //    "0٫0000152587890625"
        //};

        //    public static readonly bool[] ToBooleanExpectedSource = new bool[10]
        //{
        //    false,
        //    true,
        //    true,
        //    true,
        //    true,
        //    true,
        //    true,
        //    true,
        //    true,
        //    true
        //};

        //    public static readonly byte[] ToByteExpectedSource = new byte[10]
        //{
        //    0,
        //    0,
        //    234,
        //    0,
        //    255,
        //    1,
        //    255,
        //    2,
        //    3,
        //    0
        //};

        //    public static readonly decimal[] ToDecimalExpectedSource = new decimal[10]
        //{
        //    0m,
        //    0.003570556640625m,
        //    234m,
        //    -32768m,
        //    32767.9999847412m,
        //    1m,
        //    -1m,
        //    2.73353576660156m,
        //    3.14158630371094m,
        //    0.0000152587890625m
        //};

        //    public static readonly double[] ToDoubleExpectedSource = new double[10]
        //{
        //    0.0,
        //    0.003570556640625,
        //    234.0,
        //    -32768.0,
        //    32767.999984741211,
        //    1.0,
        //    -1.0,
        //    2.7335357666015625,
        //    3.1415863037109375,
        //    1.52587890625E-05
        //};

        //    public static readonly Half[] ToHalfExpectedSource = new Half[10]
        //{
        //    (Half)0f,
        //    (Half)0.00357,
        //    (Half)234f,
        //    (Half)(-32770f),
        //    (Half)32770f,
        //    (Half)1f,
        //    (Half)(-1f),
        //    (Half)2.734,
        //    (Half)3.14,
        //    (Half)1.526E-05
        //};

        //    public static readonly short[] ToInt16ExpectedSource = new short[10]
        //{
        //    0,
        //    0,
        //    234,
        //    -32768,
        //    32767,
        //    1,
        //    -1,
        //    2,
        //    3,
        //    0
        //};

        //    public static readonly int[] ToInt32ExpectedSource = new int[10]
        //{
        //    0,
        //    0,
        //    234,
        //    -32768,
        //    32767,
        //    1,
        //    -1,
        //    2,
        //    3,
        //    0
        //};

        //    public static readonly Type[] ToTypeTypeSource = new Type[15]
        //{
        //    typeof(bool),
        //    typeof(sbyte),
        //    typeof(byte),
        //    typeof(short),
        //    typeof(ushort),
        //    typeof(int),
        //    typeof(uint),
        //    typeof(long),
        //    typeof(ulong),
        //    typeof(Half),
        //    typeof(float),
        //    typeof(double),
        //    typeof(decimal),
        //    typeof(string),
        //    typeof(object)
        //};

        //    public static readonly object[] ToTypeExpectedSource = new object[15]
        //{
        //    true,
        //    (sbyte)16,
        //    (byte)16,
        //    (short)16,
        //    (ushort)16,
        //    16,
        //    16u,
        //    16L,
        //    16uL,
        //    (Half)16f,
        //    16f,
        //    16.0,
        //    16m,
        //    "16",
        //    UFix16.Raw(1048576)
        //};

        //    public static readonly long[] ToInt64ExpectedSource = new long[10]
        //{
        //    0L,
        //    0L,
        //    234L,
        //    -32768L,
        //    32767L,
        //    1L,
        //    -1L,
        //    2L,
        //    3L,
        //    0L
        //};

        //    public static readonly sbyte[] ToSByteExpectedSource = new sbyte[10]
        //{
        //    0,
        //    0,
        //    -22,
        //    0,
        //    -1,
        //    1,
        //    -1,
        //    2,
        //    3,
        //    0
        //};

        //    public static readonly float[] ToSingleExpectedSource = new float[10]
        //{
        //    0f,
        //    0.00357055664f,
        //    234f,
        //    -32768f,
        //    32768f,
        //    1f,
        //    -1f,
        //    2.73353577f,
        //    3.1415863f,
        //    1.52587891E-05f
        //};

        //    public static readonly ushort[] ToUInt16ExpectedSource = new ushort[10]
        //{
        //    0,
        //    0,
        //    234,
        //    32768,
        //    32767,
        //    1,
        //    65535,
        //    2,
        //    3,
        //    0
        //};

        //    public static readonly uint[] ToUInt32ExpectedSource = new uint[10]
        //{
        //    0u,
        //    0u,
        //    234u,
        //    4294934528u,
        //    32767u,
        //    1u,
        //    4294967295u,
        //    2u,
        //    3u,
        //    0u
        //};

        //    public static readonly ulong[] ToUInt64ExpectedSource = new ulong[10]
        //{
        //    0uL,
        //    0uL,
        //    234uL,
        //    18446744073709518848uL,
        //    32767uL,
        //    1uL,
        //    18446744073709551615uL,
        //    2uL,
        //    3uL,
        //    0uL
        //};

        //    public static readonly object[] FromByteSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        (byte)0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        (byte)1,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        (byte)234,
        //        UFix16.Raw(15335424)
        //    },
        //    new object[2]
        //    {
        //        Byte.MaxValue,
        //        UFix16.Raw(16711680)
        //    }
        //};

        //    public static readonly object[] FromSByteSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        (sbyte)0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        (sbyte)1,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        (sbyte)-123,
        //        UFix16.Raw(-8060928)
        //    },
        //    new object[2]
        //    {
        //        SByte.MaxValue,
        //        UFix16.Raw(8323072)
        //    }
        //};

        //    public static readonly object[] FromUInt16Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        (ushort)0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        (ushort)1,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        (ushort)234,
        //        UFix16.Raw(15335424)
        //    },
        //    new object[2]
        //    {
        //        UInt16.MaxValue,
        //        UFix16.Raw(-65536)
        //    }
        //};

        //    public static readonly object[] FromInt16Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        (short)0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        (short)1,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        (short)-234,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        Int16.MaxValue,
        //        UFix16.Raw(2147418112)
        //    }
        //};

        //    public static readonly object[] FromUInt32Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        0u,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1u,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        234u,
        //        UFix16.Raw(15335424)
        //    },
        //    new object[2]
        //    {
        //        65535u,
        //        UFix16.Raw(-65536)
        //    }
        //};

        //    public static readonly object[] FromInt32Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        -234,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        32767,
        //        UFix16.Raw(2147418112)
        //    }
        //};

        //    public static readonly object[] FromUInt64Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        0uL,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1uL,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        234uL,
        //        UFix16.Raw(15335424)
        //    },
        //    new object[2]
        //    {
        //        65535uL,
        //        UFix16.Raw(-65536)
        //    }
        //};

        //    public static readonly object[] FromInt64Source = new object[4]
        //{
        //    new object[2]
        //    {
        //        0L,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1L,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        -234L,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        32767L,
        //        UFix16.Raw(2147418112)
        //    }
        //};

        //    public static readonly object[] FromHalfSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        (Half)0f,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        (Half)1f,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        (Half)(-234f),
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        (Half)0.5,
        //        UFix16.Raw(32768)
        //    }
        //};

        //    public static readonly object[] FromSingleSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        0f,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1f,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        -234f,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        0.5f,
        //        UFix16.Raw(32768)
        //    }
        //};

        //    public static readonly object[] FromDoubleSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        0.0,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1.0,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        -234.0,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        0.5,
        //        UFix16.Raw(32768)
        //    }
        //};

        //    public static readonly object[] FromDecimalSource = new object[4]
        //{
        //    new object[2]
        //    {
        //        0m,
        //        UFix16.Raw(0)
        //    },
        //    new object[2]
        //    {
        //        1m,
        //        UFix16.Raw(65536)
        //    },
        //    new object[2]
        //    {
        //        -234m,
        //        UFix16.Raw(-15335424)
        //    },
        //    new object[2]
        //    {
        //        0.5m,
        //        UFix16.Raw(32768)
        //    }
        //};

        //    public static readonly UFix16[] ExpSource = new UFix16[]
        //    {
        //        UFix16.Zero,
        //        UFix16.One,
        //        UFix16.Raw(681392),
        //        UFix16.Raw(-772244),
        //        (UFix16)2,
        //        (UFix16)2,
        //    };

        //    public static readonly UFix16[][] SqrtSource = new UFix16[][]
        //    {
        //        new UFix16[] { (UFix16)25, (UFix16)5 },
        //        new UFix16[] { (UFix16)16385, (UFix16)(-127.994140625) },
        //        new UFix16[] { (UFix16)(-0.0625), (UFix16)(-0.25) },
        //    };

        //    public static readonly double[] SinSource = new double[]
        //    {
        //        (double)UFix16.Zero,
        //        (double)UFix16.Pi,
        //        (double)(UFix16.Pi + (UFix16)0.2),
        //        (double)-UFix16.Pi,
        //        (double)(-UFix16.Pi -(UFix16)0.2),
        //        1.57080078125,
        //        -1.57080078125,
        //    };

        //    public static readonly object[] EqualsSource1 = new object[]
        //    {
        //        new object[] { UFix16.One, true },
        //        new object[] { null!, false },
        //        new object[] { new object(), false },
        //    };

        //    public static readonly object[] EqualsSource2 = new object[]
        //    {
        //        new object[] { UFix16.One, UFix16.Zero, true },
        //        new object[] { UFix16.Zero, UFix16.Pi, true },
        //        new object[] { UFix16.Pi, UFix16.One, false },
        //    };

        //    public static readonly object[] CompareToSource1 = new object[]
        //    {
        //        new object[] { null!, Is.EqualTo(1) },
        //        new object[] { UFix16.Pi, Is.LessThan(0) },
        //        new object[] { UFix16.NegOne, Is.GreaterThan(0) },
        //        new object[] { UFix16.One, Is.Zero },
        //    };

        //    public static readonly object[] CompareToSource2 = new object[]
        //    {
        //        new object[] { UFix16.Pi, Is.LessThan(0) },
        //        new object[] { UFix16.NegOne, Is.GreaterThan(0) },
        //        new object[] { UFix16.One, Is.Zero },
        //    };

        //    public static IEnumerable<object[]> ToStringTestGenerator()
        //    {
        //        for (var i = 0; i < Fix16ConversionSource.Length; i++)
        //        {
        //            yield return new object[3]
        //            {
        //            Fix16ConversionSource[i],
        //            ToStringInvariantCultureExpected[i],
        //            CultureInfo.InvariantCulture
        //            };
        //        }
        //    }
        #endregion Test Setup

        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(UFix16.Epsilon, Is.EqualTo(UFix16.Raw(1)));

        [Test, Category("MaxValue")]
        public void MaxValue() =>
            Assert.That(UFix16.MaxValue, Is.EqualTo(UFix16.Raw(UInt32.MaxValue)));

        [Test, Category("MinValue")]
        public void MinValue() =>
            Assert.That(UFix16.MinValue, Is.EqualTo(UFix16.Raw(UInt32.MinValue)));

        [Test, Category("Pi")]
        public void Pi() =>
            Assert.That(UFix16.Pi, Is.EqualTo((UFix16)Math.PI));

        [Test, Category("E")]
        public void E() =>
            Assert.That(UFix16.E, Is.EqualTo((UFix16)Math.E));

        [Test, Category("One")]
        public void One() =>
            Assert.That(UFix16.One, Is.EqualTo((UFix16)1));

        [Test, Category("Zero")]
        public void Zero() =>
            Assert.That(UFix16.Zero, Is.EqualTo((UFix16)0));

        [Test, Category("FourDivPi")]
        public void FourDivPi() =>
            Assert.That(UFix16.FourDivPi, Is.EqualTo((UFix16)( 4 / Math.PI )));

        [Test, Category("FourDivPi2")]
        public void FourDivPi2() =>
            Assert.That(UFix16.FourDivPi2, Is.EqualTo((UFix16)Math.Pow(4 / Math.PI, 2)));

        [Test, Category("PiDivFour")]
        public void PiDivFour() =>
            Assert.That(UFix16.PiDivFour, Is.EqualTo((UFix16)( Math.PI / 4 )));

        [Test, Category("ThreePiDivFour")]
        public void ThreePiDivFour() =>
            Assert.That(UFix16.ThreePiDivFour, Is.EqualTo((UFix16)( 3 * Math.PI / 4 )));

        //    [Test, Category("ctor")]
        //    public void CtorTest() =>
        //        Assert.That(new UFix16(20), Is.EqualTo(UFix16.Raw(20<<16)));

        //    [Test, Category("ctor")]
        //    public void CtorTest2() =>
        //        Assert.That(new UFix16(2.5), Is.EqualTo(UFix16.Raw(5<<15)));

        //    [Test, Category("ctor")]
        //    public void CtorTest3() =>
        //        Assert.That(new UFix16(0.25m), Is.EqualTo(UFix16.Raw(1<<14)));

        //    [Test, Sequential, Category("SaturatedAdd")]
        //    public void SaturatedAddTest([Values(30000, -30000, -30000)] double left,
        //                                 [Values(10000, -10000, 10000)] double right,
        //                                 [Values(Fix16Max, Fix16Min, -20000)] double expected) =>
        //        Assert.That(UFix16.SaturatedAdd((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("SaturatedSubtract")]
        //    public void SaturatedSubtractTest([Values(30000, -30000, -10000)] double left,
        //                                      [Values(-10000, 10000, 10000)] double right,
        //                                      [Values(Fix16Max, Fix16Min, -20000)] double expected) =>
        //        Assert.That(UFix16.SaturatedSubtract((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("SaturatedMultiply")]
        //    public void SaturatedMultiplyTest([Values(30, -30, 30000)] double left,
        //                                      [Values(10, 30000, 2)] double right,
        //                                      [Values(300, Fix16Min, Fix16Max)] double expected) =>
        //        Assert.That(UFix16.SaturatedMultiply((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("SaturatedDivide")]
        //    public void SaturatedDivideTest([Values(30, -10000, 30000)] double left,
        //                                    [Values(10, 0.125, 0.5)] double right,
        //                                    [Values(3, Fix16Min, Fix16Max)] double expected) =>
        //        Assert.That(UFix16.SaturatedDivide((UFix16)left, (UFix16)right), Is.EqualTo((UFix16)expected));

        //    [Test, Category("ToString")]
        //    public void ToStringTest() =>
        //        Assert.That(new UFix16(2.5).ToString(), Is.EqualTo("2.5"));

        //    [TestCaseSource("ToStringTestGenerator")]
        //    [Category("ToString")]
        //    public void ToStringTest1(UFix16 value, string expected, CultureInfo culture) =>
        //        Assert.That(value.ToString("#,0.################", culture), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToBoolean")]
        //    public void ToBooleanTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToBooleanExpectedSource")] bool expected) =>
        //        Assert.That(value.ToBoolean(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToByte")]
        //    public void ToByteTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToByteExpectedSource")] byte expected) =>
        //        Assert.That(value.ToByte(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToDecimal")]
        //    public void ToDecimalTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToDecimalExpectedSource")] decimal expected) =>
        //        Assert.That(value.ToDecimal(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToDouble")]
        //    public void ToDoubleTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToDoubleExpectedSource")] double expected) =>
        //        Assert.That(value.ToDouble(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToHalf")]
        //    public void ToHalfTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToHalfExpectedSource")] Half expected) =>
        //        Assert.That(value.ToHalf(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToShort")]
        //    public void ToInt16Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToInt16ExpectedSource")] short expected) =>
        //        Assert.That(value.ToInt16(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToInt32")]
        //    public void ToInt32Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToInt32ExpectedSource")] int expected) =>
        //        Assert.That(value.ToInt32(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToInt64")]
        //    public void ToInt64Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToInt64ExpectedSource")] long expected) =>
        //        Assert.That(value.ToInt64(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToSByte")]
        //    public void ToSByteTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToSByteExpectedSource")] sbyte expected) =>
        //        Assert.That(value.ToSByte(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToSingle")]
        //    public void ToSingleTest([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToSingleExpectedSource")] float expected) =>
        //        Assert.That(value.ToSingle(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToString")]
        //    public void ToStringTest2([Values("0.5", "0,5")] string expected, [Values("en-US", "fr-FR")] string culture) =>
        //        Assert.That(new UFix16(0.5).ToString(CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        //    [Test]
        //    [Category("ToType")]
        //    public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
        //        Assert.Throws<ArgumentNullException>(() => UFix16.Raw(1048576).ToType(null!, null));

        //    [Test]
        //    [Category("ToType")]
        //    public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
        //        Assert.Throws<InvalidCastException>(() => UFix16.Raw(1048576).ToType(typeof(DateTime), null));

        //    [Test]
        //    [Sequential]
        //    [Category("ToType")]
        //    public void ToTypeTest_conversionType([ValueSource("ToTypeTypeSource")] Type type, [ValueSource("ToTypeExpectedSource")] object expected) =>
        //        Assert.That(UFix16.Raw(1048576).ToType(type, null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToUInt16")]
        //    public void ToUInt16Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToUInt16ExpectedSource")] ushort expected) =>
        //        Assert.That(value.ToUInt16(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToUInt32")]
        //    public void ToUInt32Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToUInt32ExpectedSource")] uint expected) =>
        //        Assert.That(value.ToUInt32(null), Is.EqualTo(expected));

        //    [Test]
        //    [Sequential]
        //    [Category("ToUInt64")]
        //    public void ToUInt64Test([ValueSource("Fix16ConversionSource")] UFix16 value, [ValueSource("ToUInt64ExpectedSource")] ulong expected) =>
        //        Assert.That(value.ToUInt64(null), Is.EqualTo(expected));

        //    [TestCaseSource("FromByteSource")]
        //    [Category("FromByte")]
        //    public void FromByteTest(byte value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromSByteSource")]
        //    [Category("FromSByte")]
        //    public void FromSByteTest(sbyte value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt16Source")]
        //    [Category("FromUInt16")]
        //    public void FromUInt16Test(ushort value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt16Source")]
        //    [Category("FromInt16")]
        //    public void FromInt16Test(short value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt32Source")]
        //    [Category("FromUInt32")]
        //    public void FromUInt32Test(uint value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt32Source")]
        //    [Category("FromInt32")]
        //    public void FromInt32Test(int value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt64Source")]
        //    [Category("FromUInt64")]
        //    public void FromUInt64Test(ulong value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt64Source")]
        //    [Category("FromInt64")]
        //    public void FromInt64Test(long value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromHalfSource")]
        //    [Category("FromHalf")]
        //    public void FromHalfTest(Half value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromSingleSource")]
        //    [Category("FromSingle")]
        //    public void FromSingleTest(float value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromDoubleSource")]
        //    [Category("FromDouble")]
        //    public void FromDoubleTest(double value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromDecimalSource")]
        //    [Category("FromDecimal")]
        //    public void FromDecimalTest(decimal value, UFix16 expected) =>
        //        Assert.That((UFix16)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromByteSource")]
        //    [Category("ToByte")]
        //    public void ToByte(byte expected, UFix16 value) =>
        //        Assert.That((byte)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromSByteSource")]
        //    [Category("ToSByte")]
        //    public void ToSByte(sbyte expected, UFix16 value) =>
        //        Assert.That((sbyte)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt16Source")]
        //    [Category("ToUInt16")]
        //    public void ToUInt16(ushort expected, UFix16 value) =>
        //        Assert.That((ushort)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt16Source")]
        //    [Category("ToInt16")]
        //    public void ToInt16(short expected, UFix16 value) =>
        //        Assert.That((short)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt32Source")]
        //    [Category("ToUInt32")]
        //    public void ToUInt32(uint expected, UFix16 value) =>
        //        Assert.That((uint)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt32Source")]
        //    [Category("ToInt32")]
        //    public void ToInt32(int expected, UFix16 value) =>
        //        Assert.That((int)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromUInt64Source")]
        //    [Category("ToUInt64")]
        //    public void ToUInt64(ulong expected, UFix16 value) =>
        //        Assert.That((ulong)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromInt64Source")]
        //    [Category("ToInt64")]
        //    public void ToInt64(long expected, UFix16 value) =>
        //        Assert.That((long)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromHalfSource")]
        //    [Category("ToHalf")]
        //    public void ToHalf(Half expected, UFix16 value) =>
        //        Assert.That((Half)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromSingleSource")]
        //    [Category("ToSingle")]
        //    public void ToSingle(float expected, UFix16 value) =>
        //        Assert.That((float)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromDoubleSource")]
        //    [Category("ToDouble")]
        //    public void ToDouble(double expected, UFix16 value) =>
        //        Assert.That((double)value, Is.EqualTo(expected));

        //    [TestCaseSource("FromDecimalSource")]
        //    [Category("ToDecimal")]
        //    public void ToDecimal(decimal expected, UFix16 value) =>
        //        Assert.That((decimal)value, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_UnaryNegation")]
        //    public void NegationTest([Values(-1, 234)] int actual,
        //                             [Values(1, -234)] int expected) =>
        //        Assert.That(-(UFix16)actual, Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("op_UnaryNegation")]
        //    public void NegationTest2([Values(-1, 234, null)] int? actual,
        //                              [Values(1, -234, null)] int? expected) =>
        //        Assert.That(-(UFix16?)actual, Is.EqualTo((UFix16?)expected));

        //    [Test, Sequential, Category("op_Multiplication")]
        //    public void MultiplicationTest([Values(2.5, Int32.MaxValue)] double left,
        //                                   [Values(11, 1.0 / 65535)] double right,
        //                                   [Values(27.5, -0.5)] double expected) =>
        //        Assert.That((UFix16)left * (UFix16)right, Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("op_Multiplication")]
        //    public void MultiplicationTest4([Values(null, null, 10, 2.5, Int32.MaxValue)] double? left,
        //                                    [Values(10, null, null, 11, 1.0 / 65535)] double? right,
        //                                    [Values(null, null, null, 27.5, -0.5)] double? expected) =>
        //        Assert.That((UFix16?)left * (UFix16?)right, Is.EqualTo((UFix16?)expected));

        //    [Test, Sequential, Category("op_Division")]
        //    public void DivisionTest([Values(27.5, -10, 2000, 16384)] double left,
        //                             [Values(11, 2, -1000, 0.25)] double right,
        //                             [Values(2.5, -5, -2, 0)] double expected) =>
        //        Assert.That((UFix16)left / (UFix16)right, Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("op_Division")]
        //    public void DivisionTest2([Values(10, null, null, 27.5, -10, 2)] double? left,
        //                              [Values(null, null, 10, 11, 2, -1)] double? right,
        //                              [Values(null, null, null, 2.5, -5, -2)] double? expected) =>
        //        Assert.That((UFix16?)left / (UFix16?)right, Is.EqualTo((UFix16?)expected));

        //    [Test, Sequential, Category("NullableDivideBy")]
        //    public void NullableDivideByTest([Values(1, 10, null, null, 27.5, -10, 2)] double? left,
        //                                     [Values(0, null, null, 10, 11, 2, -1)] double? right,
        //                                     [Values(null, null, null, null, 2.5, -5, -2)] double? expected) =>
        //        Assert.That(UFix16.NullableDivideBy((UFix16?)left, (UFix16?)right), Is.EqualTo((UFix16?)expected));

        //    [Test]
        //    [Category("op_Division")]
        //    public void DivisionTest_Divide_by_zero_throws_DivideByZeroException() =>
        //        Assert.Throws<DivideByZeroException>(() => _ = UFix16.One / UFix16.Zero);

        //    [Test, Sequential, Category("op_Equality")]
        //    public void EqualityTest([Values(10, -10, 10)] double left,
        //                             [Values(0.5, -10, 10.1)] double right,
        //                             [Values(false, true, false)] bool expected) =>
        //        Assert.That((UFix16)left == (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Equality")]
        //    public void EqualityTest2([Values(null, 10, -10, 10)] double? left,
        //                              [Values(7, 0.5, -10, 10.1)] double right,
        //                              [Values(false, false, true, false)] bool expected) =>
        //        Assert.That((UFix16?)left == (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Equality")]
        //    public void EqualityTest3([Values(7, 10, -10, 10)] double left,
        //                              [Values(null, 0.5, -10, 10.1)] double? right,
        //                              [Values(false, false, true, false)] bool expected) =>
        //        Assert.That((UFix16)left == (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Equality")]
        //    public void EqualityTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                              [Values(null, 7, null, 0.5, -10, 10.1)] double? right,
        //                              [Values(false, false, false, false, true, false)] bool expected) =>
        //        Assert.That((UFix16?)left == (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Inequality")]
        //    public void InequalityTest([Values(10, -10, 10)] double left,
        //                               [Values(0.5, -10, 10.1)] double right,
        //                               [Values(true, false, true)] bool expected) =>
        //        Assert.That((UFix16)left != (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Inequality")]
        //    public void InequalityTest2([Values(null, 10, -10, 10)] double? left,
        //                                [Values(7, 0.5, -10, 10.1)] double right,
        //                                [Values(true, true, false, true)] bool expected) =>
        //        Assert.That((UFix16?)left != (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Inequality")]
        //    public void InequalityTest3([Values(7, 10, -10, 10)] double left,
        //                                [Values(null, 0.5, -10, 10.1)] double? right,
        //                                [Values(true, true, false, true)] bool expected) =>
        //        Assert.That((UFix16)left != (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Inequality")]
        //    public void InequalityTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                                [Values(7, null, null, 0.5, -10, 10.1)] double? right,
        //                                [Values(true, true, true, true, false, true)] bool expected) =>
        //        Assert.That((UFix16?)left != (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThanOrEqual")]
        //    public void GreaterThanOrEqualTest([Values(10, -10, 10)] double left,
        //                                       [Values(0.5, -10, 10.1)] double right,
        //                                       [Values(true, true, false)] bool expected) =>
        //        Assert.That((UFix16)left >= (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThanOrEqual")]
        //    public void GreaterThanOrEqualTest2([Values(null, 10, -10, 10)] double? left,
        //                                        [Values(7, 0.5, -10, 10.1)] double right,
        //                                        [Values(false, true, true, false)] bool expected) =>
        //        Assert.That((UFix16?)left >= (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThanOrEqual")]
        //    public void GreaterThanOrEqualTest3([Values(7, 10, -10, 10)] double left,
        //                                        [Values(null, 0.5, -10, 10.1)] double? right,
        //                                        [Values(false, true, true, false)] bool expected) =>
        //        Assert.That((UFix16)left >= (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThanOrEqual")]
        //    public void GreaterThanOrEqualTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                                        [Values(7, null, null, 0.5, -10, 10.1)] double? right,
        //                                        [Values(false, false, false, true, true, false)] bool expected) =>
        //        Assert.That((UFix16?)left >= (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThan")]
        //    public void GreaterThanTest([Values(10, -10, 10)] double left,
        //                                [Values(0.5, -10, 10.1)] double right,
        //                                [Values(true, false, false)] bool expected) =>
        //        Assert.That((UFix16)left > (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThan")]
        //    public void GreaterThanTest2([Values(null, 10, -10, 10)] double? left,
        //                                 [Values(7, 0.5, -10, 10.1)] double right,
        //                                 [Values(false, true, false, false)] bool expected) =>
        //        Assert.That((UFix16?)left > (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThan")]
        //    public void GreaterThanTest3([Values(7, 10, -10, 10)] double left,
        //                                 [Values(null, 0.5, -10, 10.1)] double? right,
        //                                 [Values(false, true, false, false)] bool expected) =>
        //        Assert.That((UFix16)left > (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_GreaterThan")]
        //    public void GreaterThanTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                                 [Values(7, null, null, 0.5, -10, 10.1)] double? right,
        //                                 [Values(false, false, false, true, false, false)] bool expected) =>
        //        Assert.That((UFix16?)left > (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThanOrEqual")]
        //    public void LessThanOrEqualTest([Values(10, -10, 10)] double left,
        //                                    [Values(0.5, -10, 10.1)] double right,
        //                                    [Values(false, true, true)] bool expected) =>
        //        Assert.That((UFix16)left <= (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThanOrEqual")]
        //    public void LessThanOrEqualTest2([Values(null, 10, -10, 10)] double? left,
        //                                     [Values(7, 0.5, -10, 10.1)] double right,
        //                                     [Values(false, false, true, true)] bool expected) =>
        //        Assert.That((UFix16?)left <= (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThanOrEqual")]
        //    public void LessThanOrEqualTest3([Values(7, 10, -10, 10)] double left,
        //                                     [Values(null, 0.5, -10, 10.1)] double? right,
        //                                     [Values(false, false, true, true)] bool expected) =>
        //        Assert.That((UFix16)left <= (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThanOrEqual")]
        //    public void LessThanOrEqualTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                                     [Values(7, null, null, 0.5, -10, 10.1)] double? right,
        //                                     [Values(false, false, false, false, true, true)] bool expected) =>
        //        Assert.That((UFix16?)left <= (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThan")]
        //    public void LessThanTest([Values(10, -10, 10)] double left,
        //                             [Values(0.5, -10, 10.1)] double right,
        //                             [Values(false, false, true)] bool expected) =>
        //        Assert.That((UFix16)left < (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThan")]
        //    public void LessThanTest2([Values(null, 10, -10, 10)] double? left,
        //                              [Values(7, 0.5, -10, 10.1)] double right,
        //                              [Values(false, false, false, true)] bool expected) =>
        //        Assert.That((UFix16?)left < (UFix16)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThan")]
        //    public void LessThanTest3([Values(7, 10, -10, 10)] double left,
        //                              [Values(null, 0.5, -10, 10.1)] double? right,
        //                              [Values(false, false, false, true)] bool expected) =>
        //        Assert.That((UFix16)left < (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_LessThan")]
        //    public void LessThanTest4([Values(null, null, 7, 10, -10, 10)] double? left,
        //                              [Values(7, null, null, 0.5, -10, 10.1)] double? right,
        //                              [Values(false, false, false, false, false, true)] bool expected) =>
        //        Assert.That((UFix16?)left < (UFix16?)right, Is.EqualTo(expected));

        //    [Test, Sequential, Category("op_Addition")]
        //    public void AdditionTest([Values(0.5, 1, -10, 7, -3)] double left,
        //                             [Values(0.25, 2, 3, -1, -5)] double right) =>
        //        Assert.That((UFix16)left + (UFix16)right, Is.EqualTo((UFix16)( left + right )));

        //    [Test, Sequential, Category("op_Addition")]
        //    public void AdditionTest2([Values(0.5, 1, -10, 7, -3, null)] double? left,
        //                              [Values(0.25, 2, 3, -1, -5, 0)] double right) =>
        //        Assert.That((UFix16?)left + (UFix16)right, Is.EqualTo((UFix16?)( left + right )));

        //    [Test, Sequential, Category("op_Addition")]
        //    public void AdditionTest3([Values(0.5, 1, -10, 7, -3, 0)] double left,
        //                              [Values(0.25, 2, 3, -1, -5, null)] double? right) =>
        //        Assert.That((UFix16)left + (UFix16?)right, Is.EqualTo((UFix16?)( left + right )));

        //    [Test, Sequential, Category("op_Addition")]
        //    public void AdditionTest4([Values(0.5, 1, -10, 7, -3, 0, null, null)] double? left,
        //                              [Values(0.25, 2, 3, -1, -5, null, null, 0)] double? right) =>
        //        Assert.That((UFix16?)left + (UFix16?)right, Is.EqualTo((UFix16?)( left + right )));

        //    [Test, Sequential, Category("op_Subtraction")]
        //    public void SubtractionTest([Values(0.5, 1, -10, 7, -3)] double left,
        //                             [Values(0.25, 2, 3, -1, -5)] double right) =>
        //        Assert.That((UFix16)left - (UFix16)right, Is.EqualTo((UFix16)( left - right )));

        //    [Test, Sequential, Category("op_Subtraction")]
        //    public void SubtractionTest2([Values(0.5, 1, -10, 7, -3, null)] double? left,
        //                              [Values(0.25, 2, 3, -1, -5, 0)] double right) =>
        //        Assert.That((UFix16?)left - (UFix16)right, Is.EqualTo((UFix16?)( left - right )));

        //    [Test, Sequential, Category("op_Subtraction")]
        //    public void SubtractionTest3([Values(0.5, 1, -10, 7, -3, 0)] double left,
        //                              [Values(0.25, 2, 3, -1, -5, null)] double? right) =>
        //        Assert.That((UFix16)left - (UFix16?)right, Is.EqualTo((UFix16?)( left - right )));

        //    [Test, Sequential, Category("op_Subtraction")]
        //    public void SubtractionTest4([Values(0.5, 1, -10, 7, -3, 0, null, null)] double? left,
        //                              [Values(0.25, 2, 3, -1, -5, null, null, 0)] double? right) =>
        //        Assert.That((UFix16?)left - (UFix16?)right, Is.EqualTo((UFix16?)( left - right )));

        //    [Test, Sequential, Category("Lerp")]
        //    public void LerpTest([Values(10, 1)] double left,
        //                         [Values(11, 3)] double right,
        //                         [Values(0x80, 0x20)] byte fraction,
        //                         [Values(10.5, 1.25)] double expected) =>
        //        Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("Lerp")]
        //    public void LerpTest2([Values(10, 1)] double left,
        //                          [Values(11, 3)] double right,
        //                          [Values((ushort)0x8000u, (ushort)0x2000)] ushort fraction,
        //                          [Values(10.5, 1.25)] double expected) =>
        //        Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        //    [Test, Sequential, Category("Lerp")]
        //    public void LerpTest3([Values(10, 1)] double left,
        //                          [Values(11, 3)] double right,
        //                          [Values(0x80000000u, 0x20000000u)] uint fraction,
        //                          [Values(10.5, 1.25)] double expected) =>
        //        Assert.That(UFix16.Lerp((UFix16)left, (UFix16)right, fraction), Is.EqualTo((UFix16)expected));

        //    [TestCaseSource("ExpSource")]
        //    [Category("Exp")]
        //    public void ExpTest(UFix16 value) =>
        //        Assert.That(UFix16.Exp(value), Is.EqualTo(Math.Exp((double)value) >= (double)UFix16.MaxValue ? UFix16.MaxValue : (UFix16)Math.Exp((double)value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, UFix16.Epsilon * new UFix16(5))));

        //    [TestCaseSource("SqrtSource")]
        //    [Category("Sqrt")]
        //    public void SqrtTest(UFix16 value, UFix16 expected) =>
        //        Assert.That(UFix16.Sqrt(value), Is.EqualTo(expected));

        //    [TestCaseSource("SinSource")]
        //    [Category("Sin")]
        //    public void SinTest([Random(-Math.PI, Math.PI, 10)] double value) =>
        //        Assert.That(UFix16.Sin((UFix16)value), Is.EqualTo((UFix16)Math.Sin(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.007)));

        //    [Test]
        //    [Category("Sin")]
        //    public void SinTest2() =>
        //        Assert.That(UFix16.Sin(UFix16.Zero), Is.EqualTo(UFix16.Sin(UFix16.Zero)));

        //    [TestCaseSource("SinSource")]
        //    [Category("Cos")]
        //    public void CosTest([Random(-Math.PI, Math.PI, 10)] double value) =>
        //        Assert.That(UFix16.Cos((UFix16)value), Is.EqualTo((UFix16)Math.Cos(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.007)));

        //    [Test]
        //    [Category("Tan")]
        //    public void TanTest([Random(-1.45, 1.45, 10)] double value) =>
        //        Assert.That(UFix16.Tan((UFix16)value), Is.EqualTo((UFix16)Math.Tan(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.6)));

        //    [Test]
        //    [Category("Tan")]
        //    public void TanTest2() =>
        //        Assert.That(UFix16.Tan(UFix16.Pi / (UFix16)2), Is.Null);

        //    [Test]
        //    [Category("Asin")]
        //    public void AsinTest([Values(1.0)][Random(-65535.0/65536, 65535/65536, 10)] double value) =>
        //        Assert.That(UFix16.Asin((UFix16)value), Is.EqualTo((UFix16)Math.Asin(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.011)));

        //    [Test]
        //    [Category("Asin")]
        //    public void AsinTest2() =>
        //        Assert.That(UFix16.Asin((UFix16)1.1), Is.Null);

        //    [Test]
        //    [Category("Acos")]
        //    public void AcosTest([Random(-65535.0/65536, 65535/65536, 10)] double value) =>
        //        Assert.That(UFix16.Acos((UFix16)value), Is.EqualTo((UFix16)Math.Acos(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.011)));

        //    [TestCaseSource("SinSource")]
        //    [Category("Atan")]
        //    public void AtanTest([Random(-Math.PI, Math.PI, 10)] double value) =>
        //        Assert.That(UFix16.Atan((UFix16)value), Is.EqualTo((UFix16)Math.Atan(value)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.011)));

        //    [Test, Category("Atan2")]
        //    public void Atan2Test([Random(-Math.PI, Math.PI, 10)] double x, [Random(-Math.PI, Math.PI, 10)] double y) =>
        //        Assert.That(UFix16.Atan2((UFix16)y, (UFix16)x), Is.EqualTo((UFix16)Math.Atan2(y, x)).Using<UFix16>((a, b) => UFix16.Equals(a, b, (UFix16)0.011)));

        //    [Test]
        //    [Category("Atan")]
        //    public void AtanTest2() =>
        //        Assert.That(UFix16.Atan(UFix16.Zero), Is.EqualTo(UFix16.Atan(UFix16.Zero)));

        //    [Test]
        //    [Category("GetTypeCode")]
        //    public void GetTypeCodeTest() =>
        //        Assert.That(UFix16.Zero.GetTypeCode(), Is.EqualTo((TypeCode)100));

        //    [Test]
        //    [Category("ToChar")]
        //    public void ToChar_throws_InvalidCastExceptionTest() =>
        //        Assert.Throws<InvalidCastException>(() => UFix16.Zero.ToChar(null));

        //    [Test]
        //    [Category("ToDateTime")]
        //    public void ToDateTime_throws_InvalidCastExceptionTest() =>
        //        Assert.Throws<InvalidCastException>(() => UFix16.Zero.ToDateTime(null));

        //    [TestCaseSource("EqualsSource1")]
        //    [Category("Equals")]
        //    public void EqualsTest(object value, bool expected) =>
        //        Assert.That(UFix16.One.Equals(value), Is.EqualTo(expected));

        //    [Test]
        //    [Category("GetHashCode")]
        //    public void GetHashCodeTest() =>
        //        Assert.That(UFix16.Pi.GetHashCode(), Is.EqualTo(HashCode.Combine(205887, 2)));

        //    [TestCaseSource("CompareToSource1")]
        //    [Category("CompareTo")]
        //    public void CompareToTest(object? value, IResolveConstraint expected) =>
        //        Assert.That(UFix16.One.CompareTo(value), expected);

        //    [Test, Category("CompareTo")]
        //    public void CompareTo_is_not_Fix16_throws_ArgumentExceptionTest() =>
        //        Assert.Throws<ArgumentException>(() => UFix16.One.CompareTo(1));

        //    [TestCaseSource("CompareToSource2")]
        //    [Category("CompareTo")]
        //    public void CompareToTest2(UFix16 value, IResolveConstraint expected) =>
        //        Assert.That(UFix16.One.CompareTo(value), expected);

        //    [TestCaseSource("EqualsSource2")]
        //    public void EqualsTest2(UFix16 value, UFix16 delta, bool expected) =>
        //        Assert.That(UFix16.Equals(UFix16.One, value, delta), Is.EqualTo(expected));
    }
}
