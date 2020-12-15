//using System.Collections;
//using System.Collections.Generic;
//using System.Globalization;

//using NUnit.Framework;

//namespace System.Tests
//{
//    [TestFixture]
//    public class UFix16Tests
//    {
//        public static IEnumerable<object[]> SourceGenerator1(int value1, int value2)
//        {
//            var UFix16ConversionSource = new uint[9]
//            {
//                0u,
//                234u,
//                15335424u,
//                0u,
//                4294967295u,
//                65536u,
//                179145u,
//                205887u,
//                1u
//            };
//            var ToStringInvariantCultureExpected = new string[9]
//            {
//                "0",
//                "0.003570556640625",
//                "234",
//                "0",
//                "65,535.9999847412",
//                "1",
//                "2.73353576660156",
//                "3.14158630371094",
//                "0.0000152587890625"
//            };
//            var ToStringUnitedStatesExpected = new string[9]
//            {
//                "0",
//                "0.003570556640625",
//                "234",
//                "0",
//                "65,535.9999847412",
//                "1",
//                "2.73353576660156",
//                "3.14158630371094",
//                "0.0000152587890625"
//            };
//            var ToStringSouthAfricanExpected = new string[9]
//            {
//                "0",
//                "0,003570556640625",
//                "234",
//                "0",
//                "65\u00a0535,9999847412",
//                "1",
//                "2,73353576660156",
//                "3,14158630371094",
//                "0,0000152587890625"
//            };
//            var ToStringIndianUrduExpected = new string[9]
//            {
//                "0",
//                "0٫003570556640625",
//                "234",
//                "0",
//                "65٬535٫9999847412",
//                "1",
//                "2٫73353576660156",
//                "3٫14158630371094",
//                "0٫0000152587890625"
//            };
//            var ToBooleanExpectedSource = new bool[9]
//            {
//                false,
//                true,
//                true,
//                false,
//                true,
//                true,
//                true,
//                true,
//                true
//            };
//            var ToByteExpectedSource = new byte[9]
//            {
//                0,
//                0,
//                234,
//                0,
//                255,
//                1,
//                2,
//                3,
//                0
//            };
//            var ToDecimalExpectedSource = new decimal[9]
//            {
//                0m,
//                0.003570556640625m,
//                234m,
//                0m,
//                65535.9999847412m,
//                1m,
//                2.73353576660156m,
//                3.14158630371094m,
//                0.0000152587890625m
//            };
//            var ToDoubleExpectedSource = new double[9]
//            {
//                0.0,
//                0.003570556640625,
//                234.0,
//                0.0,
//                65535.999984741211,
//                1.0,
//                2.7335357666015625,
//                3.1415863037109375,
//                1.52587890625E-05
//            };
//            var ToHalfExpectedSource = new Half[9]
//            {
//                (Half)0f,
//                (Half)0.00357,
//                (Half)234f,
//                (Half)0f,
//                Half.PositiveInfinity,
//                (Half)1f,
//                (Half)2.734,
//                (Half)3.14,
//                (Half)1.526E-05
//            };
//            var ToInt16ExpectedSource = new short[9]
//            {
//                0,
//                0,
//                234,
//                0,
//                -1,
//                1,
//                2,
//                3,
//                0
//            };
//            var ToInt32ExpectedSource = new int[9]
//            {
//                0,
//                0,
//                234,
//                0,
//                65535,
//                1,
//                2,
//                3,
//                0
//            };
//            var ToTypeTypeSource = new Type[15]
//            {
//                typeof(bool),
//                typeof(sbyte),
//                typeof(byte),
//                typeof(short),
//                typeof(ushort),
//                typeof(int),
//                typeof(uint),
//                typeof(long),
//                typeof(ulong),
//                typeof(Half),
//                typeof(float),
//                typeof(double),
//                typeof(decimal),
//                typeof(string),
//                typeof(object)
//            };
//            var ToTypeExpectedSource = new object[15]
//            {
//                true,
//                (sbyte)16,
//                (byte)16,
//                (short)16,
//                (ushort)16,
//                16,
//                16u,
//                16L,
//                16uL,
//                (Half)16f,
//                16f,
//                16.0,
//                16m,
//                "16",
//                1048576
//            };
//            var ToInt64ExpectedSource = new long[9]
//            {
//                0L,
//                0L,
//                234L,
//                0L,
//                65535L,
//                1L,
//                2L,
//                3L,
//                0L
//            };
//            var ToSByteExpectedSource = new sbyte[9]
//            {
//                0,
//                0,
//                -22,
//                0,
//                -1,
//                1,
//                2,
//                3,
//                0
//            };
//            var ToSingleExpectedSource = new float[9]
//            {
//                0f,
//                0.00357055664f,
//                234f,
//                0f,
//                65536f,
//                1f,
//                2.73353577f,
//                3.1415863f,
//                1.52587891E-05f
//            };
//            var ToUInt16ExpectedSource = new ushort[9]
//            {
//                0,
//                0,
//                234,
//                0,
//                65535,
//                1,
//                2,
//                3,
//                0
//            };
//            var ToUInt32ExpectedSource = new uint[9]
//            {
//                0u,
//                0u,
//                234u,
//                0u,
//                65535u,
//                1u,
//                2u,
//                3u,
//                0u
//            };
//            var ToUInt64ExpectedSource = new ulong[9]
//            {
//                0uL,
//                0uL,
//                234uL,
//                0uL,
//                65535uL,
//                1uL,
//                2uL,
//                3uL,
//                0uL
//            };
//            var array1 = value1 switch
//            {
//                1 => ToStringInvariantCultureExpected.GetEnumerator(),
//                2 => ToStringUnitedStatesExpected.GetEnumerator(),
//                3 => ToStringSouthAfricanExpected.GetEnumerator(),
//                4 => ToStringIndianUrduExpected.GetEnumerator(),
//                5 => ToBooleanExpectedSource.GetEnumerator(),
//                6 => ToByteExpectedSource.GetEnumerator(),
//                7 => ToSByteExpectedSource.GetEnumerator(),
//                8 => ToInt16ExpectedSource.GetEnumerator(),
//                9 => ToUInt16ExpectedSource.GetEnumerator(),
//                10 => ToInt32ExpectedSource.GetEnumerator(),
//                11 => ToUInt32ExpectedSource.GetEnumerator(),
//                12 => ToInt64ExpectedSource.GetEnumerator(),
//                13 => ToUInt64ExpectedSource.GetEnumerator(),
//                14 => ToHalfExpectedSource.GetEnumerator(),
//                15 => ToSingleExpectedSource.GetEnumerator(),
//                16 => ToDoubleExpectedSource.GetEnumerator(),
//                17 => ToDecimalExpectedSource.GetEnumerator(),
//                18 => ToTypeTypeSource.GetEnumerator(),
//                19 => ToTypeExpectedSource.GetEnumerator(),
//                _ => UFix16ConversionSource.GetEnumerator(),
//            };
//            var array2 = value2 switch
//            {
//                1 => ToStringInvariantCultureExpected.GetEnumerator(),
//                2 => ToStringUnitedStatesExpected.GetEnumerator(),
//                3 => ToStringSouthAfricanExpected.GetEnumerator(),
//                4 => ToStringIndianUrduExpected.GetEnumerator(),
//                5 => ToBooleanExpectedSource.GetEnumerator(),
//                6 => ToByteExpectedSource.GetEnumerator(),
//                7 => ToSByteExpectedSource.GetEnumerator(),
//                8 => ToInt16ExpectedSource.GetEnumerator(),
//                9 => ToUInt16ExpectedSource.GetEnumerator(),
//                10 => ToInt32ExpectedSource.GetEnumerator(),
//                11 => ToUInt32ExpectedSource.GetEnumerator(),
//                12 => ToInt64ExpectedSource.GetEnumerator(),
//                13 => ToUInt64ExpectedSource.GetEnumerator(),
//                14 => ToHalfExpectedSource.GetEnumerator(),
//                15 => ToSingleExpectedSource.GetEnumerator(),
//                16 => ToDoubleExpectedSource.GetEnumerator(),
//                17 => ToDecimalExpectedSource.GetEnumerator(),
//                18 => ToTypeTypeSource.GetEnumerator(),
//                19 => ToTypeExpectedSource.GetEnumerator(),
//                _ => UFix16ConversionSource.GetEnumerator(),
//            };
//            while (array1.MoveNext() && array2.MoveNext())
//            {
//                yield return new object[2]
//                {
//                    array1.Current,
//                    array2.Current
//                };
//            }
//        }

//        public static IEnumerable<object[]> SourceGenerator2(int value)
//        {
//            var FromByteSource = new(byte, uint)[4]
//            {
//                (0, 0u),
//                (1, 65536u),
//                (234, 15335424u),
//                (Byte.MaxValue, 16711680u)
//            };
//            var FromSByteSource = new(sbyte, uint)[4]
//            {
//                (0, 0u),
//                (1, 65536u),
//                (-123, 4286906368u),
//                (SByte.MaxValue, 8323072u)
//            };
//            var FromUInt16Source = new(ushort, uint)[4]
//            {
//                (0, 0u),
//                (1, 65536u),
//                (234, 15335424u),
//                (UInt16.MaxValue, 4294901760u)
//            };
//            var FromInt16Source = new(short, uint)[4]
//            {
//                (0, 0u),
//                (1, 65536u),
//                (-234, 4279631872u),
//                (Int16.MaxValue, 2147418112u)
//            };
//            var FromUInt32Source = new(uint, uint)[4]
//            {
//                (0u, 0u),
//                (1u, 65536u),
//                (234u, 15335424u),
//                (65535u, 4294901760u)
//            };
//            var FromInt32Source = new(int, uint)[4]
//            {
//                (0, 0u),
//                (1, 65536u),
//                (-234, 4279631872u),
//                (32767, 2147418112u)
//            };
//            var FromUInt64Source = new(ulong, uint)[4]
//            {
//                (0uL, 0u),
//                (1uL, 65536u),
//                (234uL, 15335424u),
//                (65535uL, 4294901760u)
//            };
//            var FromInt64Source = new(long, uint)[4]
//            {
//                (0L, 0u),
//                (1L, 65536u),
//                (-234L, 4279631872u),
//                (32767L, 2147418112u)
//            };
//            var FromHalfSource = new(Half, uint)[4]
//            {
//                ((Half)0f, 0u),
//                ((Half)1f, 65536u),
//                ((Half)(-234f), 4279631872u),
//                ((Half)0.5, 32768u)
//            };
//            var FromSingleSource = new(float, uint)[4]
//            {
//                (0f, 0u),
//                (1f, 65536u),
//                (-234f, 4279631872u),
//                (0.5f, 32768u)
//            };
//            var FromDoubleSource = new(double, uint)[4]
//            {
//                (0.0, 0u),
//                (1.0, 65536u),
//                (-234.0, 4279631872u),
//                (0.5, 32768u)
//            };
//            var FromDecimalSource = new(decimal, uint)[4]
//            {
//                (0m, 0u),
//                (1m, 65536u),
//                (-234m, 4279631872u),
//                (0.5m, 32768u)
//            };
//            switch (value)
//            {
//                case 0:
//                    {
//                        var array8 = FromByteSource;
//                        for (var num = 0; num < array8.Length; num++)
//                        {
//                            var j = array8[num];
//                            yield return new object[2]
//                            {
//                                j.Item1,
//                                j.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 1:
//                    {
//                        var array12 = FromSByteSource;
//                        for (var num = 0; num < array12.Length; num++)
//                        {
//                            var k = array12[num];
//                            yield return new object[2]
//                            {
//                                k.Item1,
//                                k.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 2:
//                    {
//                        var array4 = FromInt16Source;
//                        for (var num = 0; num < array4.Length; num++)
//                        {
//                            var l = array4[num];
//                            yield return new object[2]
//                            {
//                                l.Item1,
//                                l.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 3:
//                    {
//                        var array10 = FromUInt16Source;
//                        for (var num = 0; num < array10.Length; num++)
//                        {
//                            var m = array10[num];
//                            yield return new object[2]
//                            {
//                                m.Item1,
//                                m.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 4:
//                    {
//                        var array6 = FromInt32Source;
//                        for (var num = 0; num < array6.Length; num++)
//                        {
//                            var n = array6[num];
//                            yield return new object[2]
//                            {
//                                n.Item1,
//                                n.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 5:
//                    {
//                        var array2 = FromUInt32Source;
//                        for (var num = 0; num < array2.Length; num++)
//                        {
//                            var i2 = array2[num];
//                            yield return new object[2]
//                            {
//                                i2.Item1,
//                                i2.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 6:
//                    {
//                        var array11 = FromInt64Source;
//                        for (var num = 0; num < array11.Length; num++)
//                        {
//                            var i3 = array11[num];
//                            yield return new object[2] { i3.Item1, i3.Item2 };
//                        }
//                        break;
//                    }
//                case 7:
//                    {
//                        var array9 = FromUInt64Source;
//                        for (var num = 0; num < array9.Length; num++)
//                        {
//                            var i4 = array9[num];
//                            yield return new object[2] { i4.Item1, i4.Item2 };
//                        }
//                        break;
//                    }
//                case 8:
//                    {
//                        var array7 = FromHalfSource;
//                        for (var num = 0; num < array7.Length; num++)
//                        {
//                            var i5 = array7[num];
//                            yield return new object[2]
//                            {
//                                i5.Item1,
//                                i5.Item2
//                            };
//                        }
//                        break;
//                    }
//                case 9:
//                    {
//                        var array5 = FromSingleSource;
//                        for (var num = 0; num < array5.Length; num++)
//                        {
//                            var i6 = array5[num];
//                            yield return new object[2] { i6.Item1, i6.Item2 };
//                        }
//                        break;
//                    }
//                case 10:
//                    {
//                        var array3 = FromDoubleSource;
//                        for (var num = 0; num < array3.Length; num++)
//                        {
//                            var i7 = array3[num];
//                            yield return new object[2] { i7.Item1, i7.Item2 };
//                        }
//                        break;
//                    }
//                default:
//                    {
//                        var array = FromDecimalSource;
//                        for (var num = 0; num < array.Length; num++)
//                        {
//                            var i = array[num];
//                            yield return new object[2] { i.Item1, i.Item2 };
//                        }
//                        break;
//                    }
//            }
//        }

//        public static IEnumerable<object[]> ToStringTestGenerator()
//        {
//            var invarGen = SourceGenerator1(0, 1);
//            //SourceGenerator1(0, 2);
//            //SourceGenerator1(0, 3);
//            //SourceGenerator1(0, 4);
//            foreach (var i in invarGen)
//            {
//                yield return new object[3]
//                {
//                    i[0],
//                    i[1],
//                    CultureInfo.InvariantCulture
//                };
//            }
//        }

//        [Test]
//        [Category("Epsilon")]
//        public void Epsilon() =>
//            Assert.That(1u, Is.EqualTo(1));

//        [Test]
//        [Category("ToString")]
//        public void ToStringTest() =>
//            Assert.That(UFix16.ToString(163840u), Is.EqualTo("2.5"));

//        [TestCaseSource("ToStringTestGenerator")]
//        [Category("ToString")]
//        public void ToStringTest1(uint value, string expected, CultureInfo culture) =>
//            Assert.That(UFix16.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 5 })]
//        [Category("ToBoolean")]
//        public void ToBooleanTest(uint value, bool expected) =>
//            Assert.That(UFix16.ToBoolean(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 6 })]
//        [Category("ToByte")]
//        public void ToByteTest(uint value, byte expected) =>
//            Assert.That(UFix16.ToByte(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 17 })]
//        [Category("ToDecimal")]
//        public void ToDecimalTest(uint value, decimal expected) =>
//            Assert.That(UFix16.ToDecimal(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 16 })]
//        [Category("ToDouble")]
//        public void ToDoubleTest(uint value, double expected) =>
//            Assert.That(UFix16.ToDouble(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 14 })]
//        [Category("ToHalf")]
//        public void ToHalfTest(uint value, Half expected) =>
//            Assert.That(UFix16.ToHalf(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 8 })]
//        [Category("ToInt16")]
//        public void ToInt16Test(uint value, short expected) =>
//            Assert.That(UFix16.ToInt16(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 10 })]
//        [Category("ToInt32")]
//        public void ToInt32Test(uint value, int expected) =>
//            Assert.That(UFix16.ToInt32(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 12 })]
//        [Category("ToInt64")]
//        public void ToInt64Test(uint value, long expected) =>
//            Assert.That(UFix16.ToInt64(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 7 })]
//        [Category("ToSByte")]
//        public void ToSByteTest(uint value, sbyte expected) =>
//            Assert.That(UFix16.ToSByte(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 15 })]
//        [Category("ToSingle")]
//        public void ToSingleTest(uint value, float expected) =>
//            Assert.That(UFix16.ToSingle(value), Is.EqualTo(expected));

//        [Test]
//        [Sequential]
//        [Category("ToString")]
//        public void ToStringTest2([Values("0.5", "0,5")] string expected, [Values("en-US", "fr-FR")] string culture) =>
//            Assert.That(UFix16.ToString(32768u, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

//        [Test]
//        [Category("ToType")]
//        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
//            Assert.Throws<ArgumentNullException>(() => UFix16.ToType(1048576u, null!, null));

//        [Test]
//        [Category("ToType")]
//        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
//            Assert.Throws<InvalidCastException>(() => UFix16.ToType(1048576u, typeof(DateTime), null));

//        [TestCaseSource("SourceGenerator1", new object[] { 18, 19 })]
//        [Category("ToType")]
//        public void ToTypeTest_conversionType(Type type, object expected) =>
//            Assert.That(UFix16.ToType(1048576u, type, null), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 9 })]
//        [Category("ToUInt16")]
//        public void ToUInt16Test(uint value, ushort expected) =>
//            Assert.That(UFix16.ToUInt16(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 11 })]
//        [Category("ToUInt32")]
//        public void ToUInt32Test(uint value, uint expected) =>
//            Assert.That(UFix16.ToUInt32(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator1", new object[] { 0, 13 })]
//        [Category("ToUInt64")]
//        public void ToUInt64Test(uint value, ulong expected) =>
//            Assert.That(UFix16.ToUInt64(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 0 })]
//        [Category("FromByte")]
//        public void FromByteTest(byte value, uint expected) =>
//            Assert.That(UFix16.FromByte(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 1 })]
//        [Category("FromSByte")]
//        public void FromSByteTest(sbyte value, uint expected) =>
//            Assert.That(UFix16.FromSByte(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 3 })]
//        [Category("FromUInt16")]
//        public void FromUInt16Test(ushort value, uint expected) =>
//            Assert.That(UFix16.FromUInt16(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 2 })]
//        [Category("FromInt16")]
//        public void FromInt16Test(short value, uint expected) =>
//            Assert.That(UFix16.FromInt16(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 5 })]
//        [Category("FromUInt32")]
//        public void FromUInt32Test(uint value, uint expected) =>
//            Assert.That(UFix16.FromUInt32(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 4 })]
//        [Category("FromInt32")]
//        public void FromInt32Test(int value, uint expected) =>
//            Assert.That(UFix16.FromInt32(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 7 })]
//        [Category("FromUInt64")]
//        public void FromUInt64Test(ulong value, uint expected) =>
//            Assert.That(UFix16.FromUInt64(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 6 })]
//        [Category("FromInt64")]
//        public void FromInt64Test(long value, uint expected) =>
//            Assert.That(UFix16.FromInt64(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 8 })]
//        [Category("FromHalf")]
//        public void FromHalfTest(Half value, uint expected) =>
//            Assert.That(UFix16.FromHalf(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 9 })]
//        [Category("FromSingle")]
//        public void FromSingleTest(float value, uint expected) =>
//            Assert.That(UFix16.FromSingle(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 10 })]
//        [Category("FromDouble")]
//        public void FromDoubleTest(double value, uint expected) =>
//            Assert.That(UFix16.FromDouble(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 11 })]
//        [Category("FromDecimal")]
//        public void FromDecimalTest(decimal value, uint expected) =>
//            Assert.That(UFix16.FromDecimal(value), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 0 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test(byte value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 1 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test1(sbyte value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 3 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test2(ushort value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 2 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test3(short value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 5 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test4(uint value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 4 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test5(int value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 7 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test6(ulong value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 6 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test7(long value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 8 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test8(Half value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 9 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test9(float value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 10 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test10(double value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

//        [TestCaseSource("SourceGenerator2", new object[] { 11 })]
//        [Category("ToUFix16")]
//        public void ToUFix16Test11(decimal value, uint expected) =>
//            Assert.That(value.ToUFix16(), Is.EqualTo(expected));
//    }
//}
