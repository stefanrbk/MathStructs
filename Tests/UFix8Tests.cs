using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;

namespace System.Tests
{
    [TestFixture]
    public class UFix8Tests
    {
        public static IEnumerable<object[]> SourceGenerator1(int value1, int value2)
        {
            var UFix8ConversionSource = new ushort[7]
            {
                0,
                234,
                59904,
                0,
                65535,
                256,
                1
            };
            var ToStringInvariantCultureExpected = new string[7]
            {
                "0",
                "0.9140625",
                "234",
                "0",
                "255.99609375",
                "1",
                "0.00390625"
            };
            var ToStringUnitedStatesExpected = new string[7]
            {
                "0",
                "0.9140625",
                "234",
                "0",
                "255.99609375",
                "1",
                "0.00390625"
            };
            var ToStringSouthAfricanExpected = new string[7]
            {
                "0",
                "0,9140625",
                "234",
                "0",
                "255,99609375",
                "1",
                "0,00390625"
            };
            var ToStringIndianUrduExpected = new string[7]
            {
                "0",
                "0٫9140625",
                "234",
                "0",
                "255٫99609375",
                "1",
                "0٫00390625"
            };
            var ToBooleanExpectedSource = new bool[7]
            {
                false,
                true,
                true,
                false,
                true,
                true,
                true
            };
            var ToByteExpectedSource = new byte[7]
            {
                0,
                0,
                234,
                0,
                255,
                1,
                0
            };
            var ToDecimalExpectedSource = new decimal[7]
            {
                0m,
                0.9140625m,
                234m,
                0m,
                255.99609375m,
                1m,
                0.00390625m
            };
            var ToDoubleExpectedSource = new double[7]
            {
                0.0,
                117.0 / 128.0,
                234.0,
                0.0,
                255.99609375,
                1.0,
                0.00390625
            };
            var ToHalfExpectedSource = new Half[7]
            {
                (Half)0f,
                (Half)0.914,
                (Half)234f,
                (Half)0f,
                (Half)256f,
                (Half)1f,
                (Half)0.003906
            };
            var ToInt16ExpectedSource = new short[7]
            {
                0,
                0,
                234,
                0,
                255,
                1,
                0
            };
            var ToInt32ExpectedSource = new int[7]
            {
                0,
                0,
                234,
                0,
                255,
                1,
                0
            };
            var ToTypeTypeSource = new Type[15]
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
            var ToTypeExpectedSource = new object[15]
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
                4096
            };
            var ToInt64ExpectedSource = new long[7]
            {
                0L,
                0L,
                234L,
                0L,
                255L,
                1L,
                0L
            };
            var ToSByteExpectedSource = new sbyte[7]
            {
                0,
                0,
                -22,
                0,
                -1,
                1,
                0
            };
            var ToSingleExpectedSource = new float[7]
            {
                0f,
                117f / 128f,
                234f,
                0f,
                255.9961f,
                1f,
                0.00390625f
            };
            var ToUInt16ExpectedSource = new ushort[7]
            {
                0,
                0,
                234,
                0,
                255,
                1,
                0
            };
            var ToUInt32ExpectedSource = new uint[7]
            {
                0u,
                0u,
                234u,
                0u,
                255u,
                1u,
                0u
            };
            var ToUInt64ExpectedSource = new ulong[7]
            {
                0uL,
                0uL,
                234uL,
                0uL,
                255uL,
                1uL,
                0uL
            };
            var array1 = value1 switch
            {
                1 => ToStringInvariantCultureExpected.GetEnumerator(),
                2 => ToStringUnitedStatesExpected.GetEnumerator(),
                3 => ToStringSouthAfricanExpected.GetEnumerator(),
                4 => ToStringIndianUrduExpected.GetEnumerator(),
                5 => ToBooleanExpectedSource.GetEnumerator(),
                6 => ToByteExpectedSource.GetEnumerator(),
                7 => ToSByteExpectedSource.GetEnumerator(),
                8 => ToInt16ExpectedSource.GetEnumerator(),
                9 => ToUInt16ExpectedSource.GetEnumerator(),
                10 => ToInt32ExpectedSource.GetEnumerator(),
                11 => ToUInt32ExpectedSource.GetEnumerator(),
                12 => ToInt64ExpectedSource.GetEnumerator(),
                13 => ToUInt64ExpectedSource.GetEnumerator(),
                14 => ToHalfExpectedSource.GetEnumerator(),
                15 => ToSingleExpectedSource.GetEnumerator(),
                16 => ToDoubleExpectedSource.GetEnumerator(),
                17 => ToDecimalExpectedSource.GetEnumerator(),
                18 => ToTypeTypeSource.GetEnumerator(),
                19 => ToTypeExpectedSource.GetEnumerator(),
                _ => UFix8ConversionSource.GetEnumerator(),
            };
            var array2 = value2 switch
            {
                1 => ToStringInvariantCultureExpected.GetEnumerator(),
                2 => ToStringUnitedStatesExpected.GetEnumerator(),
                3 => ToStringSouthAfricanExpected.GetEnumerator(),
                4 => ToStringIndianUrduExpected.GetEnumerator(),
                5 => ToBooleanExpectedSource.GetEnumerator(),
                6 => ToByteExpectedSource.GetEnumerator(),
                7 => ToSByteExpectedSource.GetEnumerator(),
                8 => ToInt16ExpectedSource.GetEnumerator(),
                9 => ToUInt16ExpectedSource.GetEnumerator(),
                10 => ToInt32ExpectedSource.GetEnumerator(),
                11 => ToUInt32ExpectedSource.GetEnumerator(),
                12 => ToInt64ExpectedSource.GetEnumerator(),
                13 => ToUInt64ExpectedSource.GetEnumerator(),
                14 => ToHalfExpectedSource.GetEnumerator(),
                15 => ToSingleExpectedSource.GetEnumerator(),
                16 => ToDoubleExpectedSource.GetEnumerator(),
                17 => ToDecimalExpectedSource.GetEnumerator(),
                18 => ToTypeTypeSource.GetEnumerator(),
                19 => ToTypeExpectedSource.GetEnumerator(),
                _ => UFix8ConversionSource.GetEnumerator(),
            };
            while (array1.MoveNext() && array2.MoveNext())
            {
                yield return new object[2] { array1.Current, array2.Current };
            }
        }

        public static IEnumerable<object[]> SourceGenerator2(int value)
        {
            var FromByteSource = new(byte, ushort)[4]
            {
                (0, 0),
                (1, 256),
                (234, 59904),
                (Byte.MaxValue, 65280)
            };
            var FromSByteSource = new(sbyte, ushort)[4]
            {
                (0, 0),
                (1, 256),
                (-123, 34048),
                (SByte.MaxValue, 32512)
            };
            var FromUInt16Source = new(ushort, ushort)[4]
            {
                (0, 0),
                (1, 256),
                (234, 59904),
                (UInt16.MaxValue, 65280)
            };
            var FromInt16Source = new(short, ushort)[4]
            {
                (0, 0),
                (1, 256),
                (-123, 34048),
                (Int16.MaxValue, 65280)
            };
            var FromUInt32Source = new(uint, ushort)[4]
            {
                (0u, 0),
                (1u, 256),
                (234u, 59904),
                (65535u, 65280)
            };
            var FromInt32Source = new(int, ushort)[4]
            {
                (0, 0),
                (1, 256),
                (-123, 34048),
                (32767, 65280)
            };
            var FromUInt64Source = new(ulong, ushort)[4]
            {
                (0uL, 0),
                (1uL, 256),
                (234uL, 59904),
                (65535uL, 65280)
            };
            var FromInt64Source = new(long, ushort)[4]
            {
                (0L, 0),
                (1L, 256),
                (-123L, 34048),
                (32767L, 65280)
            };
            var FromHalfSource = new(Half, ushort)[4]
            {
                ((Half)0f, 0),
                ((Half)1f, 256),
                ((Half)(-123f), 34048),
                ((Half)0.5, 128)
            };
            var FromSingleSource = new(float, ushort)[4]
            {
                (0f, 0),
                (1f, 256),
                (-123f, 34048),
                (0.5f, 128)
            };
            var FromDoubleSource = new(double, ushort)[4]
            {
                (0.0, 0),
                (1.0, 256),
                (-123.0, 34048),
                (0.5, 128)
            };
            var FromDecimalSource = new(decimal, ushort)[4]
            {
                (0m, 0),
                (1m, 256),
                (-123m, 34048),
                (0.5m, 128)
            };
            switch (value)
            {
                case 0:
                    {
                        var array8 = FromByteSource;
                        for (var num = 0; num < array8.Length; num++)
                        {
                            var j = array8[num];
                            yield return new object[2] { j.Item1, j.Item2 };
                        }
                        break;
                    }
                case 1:
                    {
                        var array12 = FromSByteSource;
                        for (var num = 0; num < array12.Length; num++)
                        {
                            var k = array12[num];
                            yield return new object[2] { k.Item1, k.Item2 };
                        }
                        break;
                    }
                case 2:
                    {
                        var array4 = FromInt16Source;
                        for (var num = 0; num < array4.Length; num++)
                        {
                            var l = array4[num];
                            yield return new object[2] { l.Item1, l.Item2 };
                        }
                        break;
                    }
                case 3:
                    {
                        var array10 = FromUInt16Source;
                        for (var num = 0; num < array10.Length; num++)
                        {
                            var m = array10[num];
                            yield return new object[2] { m.Item1, m.Item2 };
                        }
                        break;
                    }
                case 4:
                    {
                        var array6 = FromInt32Source;
                        for (var num = 0; num < array6.Length; num++)
                        {
                            var n = array6[num];
                            yield return new object[2] { n.Item1, n.Item2 };
                        }
                        break;
                    }
                case 5:
                    {
                        var array2 = FromUInt32Source;
                        for (var num = 0; num < array2.Length; num++)
                        {
                            var i2 = array2[num];
                            yield return new object[2] { i2.Item1, i2.Item2 };
                        }
                        break;
                    }
                case 6:
                    {
                        var array11 = FromInt64Source;
                        for (var num = 0; num < array11.Length; num++)
                        {
                            var i3 = array11[num];
                            yield return new object[2] { i3.Item1, i3.Item2 };
                        }
                        break;
                    }
                case 7:
                    {
                        var array9 = FromUInt64Source;
                        for (var num = 0; num < array9.Length; num++)
                        {
                            var i4 = array9[num];
                            yield return new object[2] { i4.Item1, i4.Item2 };
                        }
                        break;
                    }
                case 8:
                    {
                        var array7 = FromHalfSource;
                        for (var num = 0; num < array7.Length; num++)
                        {
                            var i5 = array7[num];
                            yield return new object[2] { i5.Item1, i5.Item2 };
                        }
                        break;
                    }
                case 9:
                    {
                        var array5 = FromSingleSource;
                        for (var num = 0; num < array5.Length; num++)
                        {
                            var i6 = array5[num];
                            yield return new object[2] { i6.Item1, i6.Item2 };
                        }
                        break;
                    }
                case 10:
                    {
                        var array3 = FromDoubleSource;
                        for (var num = 0; num < array3.Length; num++)
                        {
                            var i7 = array3[num];
                            yield return new object[2] { i7.Item1, i7.Item2 };
                        }
                        break;
                    }
                default:
                    {
                        var array = FromDecimalSource;
                        for (var num = 0; num < array.Length; num++)
                        {
                            var i = array[num];
                            yield return new object[2] { i.Item1, i.Item2 };
                        }
                        break;
                    }
            }
        }

        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            var invarGen = SourceGenerator1(0, 1);
            //SourceGenerator1(0, 2);
            //SourceGenerator1(0, 3);
            //SourceGenerator1(0, 4);
            foreach (var i in invarGen)
            {
                yield return new object[3] { i[0], i[1], CultureInfo.InvariantCulture };
            }
        }

        [Test]
        [Category("Epsilon")]
        public void Epsilon() =>
            Assert.That((ushort)1, Is.EqualTo(1));

        [Test]
        [Category("ToString")]
        public void ToStringTest() =>
            Assert.That(UFix8.ToString(640), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator")]
        [Category("ToString")]
        public void ToStringTest1(ushort value, string expected, CultureInfo culture) =>
            Assert.That(UFix8.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 5 })]
        [Category("ToBoolean")]
        public void ToBooleanTest(ushort value, bool expected) =>
            Assert.That(UFix8.ToBoolean(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 6 })]
        [Category("ToByte")]
        public void ToByteTest(ushort value, byte expected) =>
            Assert.That(UFix8.ToByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 17 })]
        [Category("ToDecimal")]
        public void ToDecimalTest(ushort value, decimal expected) =>
            Assert.That(UFix8.ToDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 16 })]
        [Category("ToDouble")]
        public void ToDoubleTest(ushort value, double expected) =>
            Assert.That(UFix8.ToDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 14 })]
        [Category("ToHalf")]
        public void ToHalfTest(ushort value, Half expected) =>
            Assert.That(UFix8.ToHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 8 })]
        [Category("ToInt16")]
        public void ToInt16Test(ushort value, short expected) =>
            Assert.That(UFix8.ToInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 10 })]
        [Category("ToInt32")]
        public void ToInt32Test(ushort value, int expected) =>
            Assert.That(UFix8.ToInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 12 })]
        [Category("ToInt64")]
        public void ToInt64Test(ushort value, long expected) =>
            Assert.That(UFix8.ToInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 7 })]
        [Category("ToSByte")]
        public void ToSByteTest(ushort value, sbyte expected) =>
            Assert.That(UFix8.ToSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 15 })]
        [Category("ToSingle")]
        public void ToSingleTest(ushort value, float expected) =>
            Assert.That(UFix8.ToSingle(value), Is.EqualTo(expected));

        [Test]
        [Sequential]
        [Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected, [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(UFix8.ToString(128, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => UFix8.ToType(4096, null!, null));

        [Test]
        [Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => UFix8.ToType(4096, typeof(DateTime), null));

        [TestCaseSource("SourceGenerator1", new object[] { 18, 19 })]
        [Category("ToType")]
        public void ToTypeTest_conversionType(Type type, object expected) =>
            Assert.That(UFix8.ToType(4096, type, null), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 9 })]
        [Category("ToUInt16")]
        public void ToUInt16Test(ushort value, ushort expected) =>
            Assert.That(UFix8.ToUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 11 })]
        [Category("ToUInt32")]
        public void ToUInt32Test(ushort value, uint expected) =>
            Assert.That(UFix8.ToUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 13 })]
        [Category("ToUInt64")]
        public void ToUInt64Test(ushort value, ulong expected) =>
            Assert.That(UFix8.ToUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 })]
        [Category("FromByte")]
        public void FromByteTest(byte value, ushort expected) =>
            Assert.That(UFix8.FromByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 })]
        [Category("FromSByte")]
        public void FromSByteTest(sbyte value, ushort expected) =>
            Assert.That(UFix8.FromSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 })]
        [Category("FromUInt16")]
        public void FromUInt16Test(ushort value, ushort expected) =>
            Assert.That(UFix8.FromUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 })]
        [Category("FromInt16")]
        public void FromInt16Test(short value, ushort expected) =>
            Assert.That(UFix8.FromInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 })]
        [Category("FromUInt32")]
        public void FromUInt32Test(uint value, ushort expected) =>
            Assert.That(UFix8.FromUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 })]
        [Category("FromInt32")]
        public void FromInt32Test(int value, ushort expected) =>
            Assert.That(UFix8.FromInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 })]
        [Category("FromUInt64")]
        public void FromUInt64Test(ulong value, ushort expected) =>
            Assert.That(UFix8.FromUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 })]
        [Category("FromInt64")]
        public void FromInt64Test(long value, ushort expected) =>
            Assert.That(UFix8.FromInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 })]
        [Category("FromHalf")]
        public void FromHalfTest(Half value, ushort expected) =>
            Assert.That(UFix8.FromHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 })]
        [Category("FromSingle")]
        public void FromSingleTest(float value, ushort expected) =>
            Assert.That(UFix8.FromSingle(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 })]
        [Category("FromDouble")]
        public void FromDoubleTest(double value, ushort expected) =>
            Assert.That(UFix8.FromDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 })]
        [Category("FromDecimal")]
        public void FromDecimalTest(decimal value, ushort expected) =>
            Assert.That(UFix8.FromDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 })]
        [Category("ToUFix8")]
        public void ToUFix8Test(byte value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 })]
        [Category("ToUFix8")]
        public void ToUFix8Test1(sbyte value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 })]
        [Category("ToUFix8")]
        public void ToUFix8Test2(ushort value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 })]
        [Category("ToUFix8")]
        public void ToUFix8Test3(short value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 })]
        [Category("ToUFix8")]
        public void ToUFix8Test4(uint value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 })]
        [Category("ToUFix8")]
        public void ToUFix8Test5(int value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 })]
        [Category("ToUFix8")]
        public void ToUFix8Test6(ulong value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 })]
        [Category("ToUFix8")]
        public void ToUFix8Test7(long value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 })]
        [Category("ToUFix8")]
        public void ToUFix8Test8(Half value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 })]
        [Category("ToUFix8")]
        public void ToUFix8Test9(float value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 })]
        [Category("ToUFix8")]
        public void ToUFix8Test10(double value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 })]
        [Category("ToUFix8")]
        public void ToUFix8Test11(decimal value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));
    }
}
