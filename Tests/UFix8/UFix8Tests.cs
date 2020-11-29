using NUnit.Framework;

using System.Collections.Generic;
using System.Globalization;

namespace System.Tests
{
    [TestFixture]
    public class UFix8Tests
    {
        public static IEnumerable<object[]> SourceGenerator1(int value1, int value2)
        {
            #region Source Arrays
            var UFix8ConversionSource = new ushort[]
            {
                0,
                234,
                234 * 256,
                UFix8.MinValue,
                UFix8.MaxValue,
                UFix8.One,
                UFix8.Epsilon,
            };

            var ToStringInvariantCultureExpected = new string[]
            {
                "0",
                "0.9140625",
                "234",
                "0",
                "255.99609375",
                "1",
                "0.00390625",
            };

            var ToStringUnitedStatesExpected = new string[]
        {
            "0",
            "0.9140625",
            "234",
            "0",
            "255.99609375",
            "1",
            "0.00390625",
        };

            var ToStringSouthAfricanExpected = new string[]
        {
            "0",
            "0,9140625",
            "234",
            "0",
            "255,99609375",
            "1",
            "0,00390625",
        };

            var ToStringIndianUrduExpected = new string[]
            {
                "0",
                "0\u066B9140625",
                "234",
                "0",
                "255\u066B99609375",
                "1",
                "0\u066B00390625",
            };

            var ToBooleanExpectedSource = new bool[]
        {
            false,
            true,
            true,
            false,
            true,
            true,
            true,
        };

            var ToByteExpectedSource = new byte[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };

            var ToDecimalExpectedSource = new decimal[]
            {
                0,
                0.9140625m,
                234,
                0,
                255.99609375m,
                1,
                0.00390625m,
            };

            var ToDoubleExpectedSource = new double[]
            {
                0,
                0.9140625,
                234,
                0,
                255.99609375,
                1,
                0.00390625,
            };

            var ToHalfExpectedSource = new Half[]
            {
                (Half)0,
                (Half)0.914,
                (Half)234,
                (Half)0,
                (Half)256,
                (Half)1,
                (Half)0.003906,
            };

            var ToInt16ExpectedSource = new short[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };

            var ToInt32ExpectedSource = new int[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };

            var ToTypeTypeSource = new Type[]
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

            var ToTypeExpectedSource = new object[]
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
                0x1000
            };

            var ToInt64ExpectedSource = new long[]
            {
                0,
                0,
                234,
                0,
                255,
                1,
                0,
            };

            var ToSByteExpectedSource = new sbyte[]
        {
            0,
            0,
            -22,
            0,
            -1,
            1,
            0,
        };

            var ToSingleExpectedSource = new float[]
            {
                0,
                0.9140625f,
                234,
                0,
                255.996094f,
                1,
                0.00390625f,
            };

            var ToUInt16ExpectedSource = new ushort[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };

            var ToUInt32ExpectedSource = new uint[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };

            var ToUInt64ExpectedSource = new ulong[]
        {
            0,
            0,
            234,
            0,
            255,
            1,
            0,
        };
            #endregion Source Arrays

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
                _ => UFix8ConversionSource.GetEnumerator()
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
                _ => UFix8ConversionSource.GetEnumerator()
            };

            while (array1.MoveNext() && array2.MoveNext())
                yield return new object[] { array1.Current, array2.Current };
        }

        public static IEnumerable<object[]> SourceGenerator2(int value)
        {
            #region Source Arrays

            var FromByteSource = new (byte, ushort)[]
            {
                ( 0,   (ushort)0x0000u ),
                ( 1,   (ushort)0x0100u ),
                ( 234, (ushort)0xEA00u ),
                ( 255, (ushort)0xFF00u ),
            };

            var FromSByteSource = new (sbyte, ushort)[]
        {
                ( 0,    (ushort)0x0000u ),
                ( 1,    (ushort)0x0100u ),
                ( -123, (ushort)0x8500u ),
                ( 127,  (ushort)0x7F00u ),
        };

            var FromUInt16Source = new (ushort, ushort)[]
        {
                ( 0,     (ushort)0x0000u ),
                ( 1,     (ushort)0x0100u ),
                ( 234,   (ushort)0xEA00u ),
                ( 65535, (ushort)0xFF00u ),
        };

            var FromInt16Source = new (short, ushort)[]
            {
                (  0,     (ushort)0x0000u ),
                (  1,     (ushort)0x0100u ),
                ( -123,   (ushort)0x8500u ),
                (  32767, (ushort)0xFF00u ),
            };

            var FromUInt32Source = new (uint, ushort)[]
            {
                ( 0,     (ushort)0x0000u ),
                ( 1,     (ushort)0x0100u ),
                ( 234,   (ushort)0xEA00u ),
                ( 65535, (ushort)0xFF00u ),
            };

            var FromInt32Source = new (int, ushort)[]
            {
                (  0,     (ushort)0x0000u ),
                (  1,     (ushort)0x0100u ),
                ( -123,   (ushort)0x8500u ),
                (  32767, (ushort)0xFF00u ),
            };

            var FromUInt64Source = new (ulong, ushort)[]
            {
                ( 0,     (ushort)0x0000u ),
                ( 1,     (ushort)0x0100u ),
                ( 234,   (ushort)0xEA00u ),
                ( 65535, (ushort)0xFF00u ),
            };

            var FromInt64Source = new (long, ushort)[]
            {
                (  0,     (ushort)0x0000u ),
                (  1,     (ushort)0x0100u ),
                ( -123,   (ushort)0x8500u ),
                (  32767, (ushort)0xFF00u ),
            };

            var FromHalfSource = new (Half, ushort)[]
            {
                ( (Half)0,      (ushort)0x0000u ),
                ( (Half)1,      (ushort)0x0100u ),
                ( (Half)(-123), (ushort)0x8500u ),
                ( (Half)0.5,    (ushort)0x0080u ),
            };

            var FromSingleSource = new (float, ushort)[]
            {
                (  0f,   (ushort)0x0000u ),
                (  1f,   (ushort)0x0100u ),
                ( -123f, (ushort)0x8500u ),
                (  0.5f, (ushort)0x0080u ),
            };

            var FromDoubleSource = new (double, ushort)[]
            {
                (  0,   (ushort)0x0000u ),
                (  1,   (ushort)0x0100u ),
                ( -123, (ushort)0x8500u ),
                (  0.5, (ushort)0x0080u ),
            };

            var FromDecimalSource = new (decimal, ushort)[]
            {
                (  0m,   (ushort)0x0000u ),
                (  1m,   (ushort)0x0100u ),
                ( -123m, (ushort)0x8500u ),
                (  0.5m, (ushort)0x0080u ),
            };
            #endregion Source Arrays

            switch (value)
            {
                case 0:
                    foreach (var i in FromByteSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 1:
                    foreach (var i in FromSByteSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 2:
                    foreach (var i in FromInt16Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 3:
                    foreach (var i in FromUInt16Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 4:
                    foreach (var i in FromInt32Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 5:
                    foreach (var i in FromUInt32Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 6:
                    foreach (var i in FromInt64Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 7:
                    foreach (var i in FromUInt64Source)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 8:
                    foreach (var i in FromHalfSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 9:
                    foreach (var i in FromSingleSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                case 10:
                    foreach (var i in FromDoubleSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
                default:
                    foreach (var i in FromDecimalSource)
                        yield return new object[] { i.Item1, i.Item2 };
                    break;
            }
        }

        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            var invarGen = SourceGenerator1(0, 1);
            var usGen = SourceGenerator1(0, 2);
            var saGen = SourceGenerator1(0, 3);
            var urGen = SourceGenerator1(0, 4);
            foreach (var i in invarGen)
                yield return new object[] { i[0], i[1], CultureInfo.InvariantCulture };
            //foreach (var i in usGen)
            //    yield return new object[] { i[0], i[1], CultureInfo.CreateSpecificCulture("en-US") };
            //foreach (var i in saGen)
            //    yield return new object[] { i[0], i[1], CultureInfo.CreateSpecificCulture("en-ZA") };
            //foreach (var i in urGen)
            //    yield return new object[] { i[0], i[1], CultureInfo.CreateSpecificCulture("ur-IN") };
        }

        [Test, Category("Epsilon")]
        public void Epsilon() =>
            Assert.That(UFix8.Epsilon, Is.EqualTo(1));

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(UFix8.ToString(640), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator"), Category("ToString")]
        public void ToStringTest1(ushort value, string expected, CultureInfo culture) =>
            Assert.That(UFix8.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 5 }), Category("ToBoolean")]
        public void ToBooleanTest(ushort value, bool expected) =>
            Assert.That(UFix8.ToBoolean(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 6 }), Category("ToByte")]
        public void ToByteTest(ushort value, byte expected) =>
            Assert.That(UFix8.ToByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 17 }), Category("ToDecimal")]
        public void ToDecimalTest(ushort value, decimal expected) =>
            Assert.That(UFix8.ToDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 16 }), Category("ToDouble")]
        public void ToDoubleTest(ushort value, double expected) =>
            Assert.That(UFix8.ToDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 14 }), Category("ToHalf")]
        public void ToHalfTest(ushort value, Half expected) =>
            Assert.That(UFix8.ToHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 8 }), Category("ToInt16")]
        public void ToInt16Test(ushort value, short expected) =>
            Assert.That(UFix8.ToInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 10 }), Category("ToInt32")]
        public void ToInt32Test(ushort value, int expected) =>
            Assert.That(UFix8.ToInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 12 }), Category("ToInt64")]
        public void ToInt64Test(ushort value, long expected) =>
            Assert.That(UFix8.ToInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 7 }), Category("ToSByte")]
        public void ToSByteTest(ushort value, sbyte expected) =>
            Assert.That(UFix8.ToSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 15 }), Category("ToSingle")]
        public void ToSingleTest(ushort value, float expected) =>
            Assert.That(UFix8.ToSingle(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected,
                                  [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(UFix8.ToString(0x0080, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test, Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => UFix8.ToType(0x1000, null!, null));

        [Test, Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => UFix8.ToType(0x1000, typeof(DateTime), null));

        [TestCaseSource("SourceGenerator1", new object[] { 18, 19 }), Category("ToType")]
        public void ToTypeTest_conversionType(Type type, object expected) =>
            Assert.That(UFix8.ToType(0x1000, type, null), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 9 }), Category("ToUInt16")]
        public void ToUInt16Test(ushort value, ushort expected) =>
            Assert.That(UFix8.ToUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 11 }), Category("ToUInt32")]
        public void ToUInt32Test(ushort value, uint expected) =>
            Assert.That(UFix8.ToUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 13 }), Category("ToUInt64")]
        public void ToUInt64Test(ushort value, ulong expected) =>
            Assert.That(UFix8.ToUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 }), Category("FromByte")]
        public void FromByteTest(byte value, ushort expected) =>
            Assert.That(UFix8.FromByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 }), Category("FromSByte")]
        public void FromSByteTest(sbyte value, ushort expected) =>
            Assert.That(UFix8.FromSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 }), Category("FromUInt16")]
        public void FromUInt16Test(ushort value, ushort expected) =>
            Assert.That(UFix8.FromUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 }), Category("FromInt16")]
        public void FromInt16Test(short value, ushort expected) =>
            Assert.That(UFix8.FromInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 }), Category("FromUInt32")]
        public void FromUInt32Test(uint value, ushort expected) =>
            Assert.That(UFix8.FromUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 }), Category("FromInt32")]
        public void FromInt32Test(int value, ushort expected) =>
            Assert.That(UFix8.FromInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 }), Category("FromUInt64")]
        public void FromUInt64Test(ulong value, ushort expected) =>
            Assert.That(UFix8.FromUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 }), Category("FromInt64")]
        public void FromInt64Test(long value, ushort expected) =>
            Assert.That(UFix8.FromInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 }), Category("FromHalf")]
        public void FromHalfTest(Half value, ushort expected) =>
            Assert.That(UFix8.FromHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 }), Category("FromSingle")]
        public void FromSingleTest(float value, ushort expected) =>
            Assert.That(UFix8.FromSingle(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 }), Category("FromDouble")]
        public void FromDoubleTest(double value, ushort expected) =>
            Assert.That(UFix8.FromDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 }), Category("FromDecimal")]
        public void FromDecimalTest(decimal value, ushort expected) =>
            Assert.That(UFix8.FromDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 }), Category("ToUFix8")]
        public void ToUFix8Test(byte value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 }), Category("ToUFix8")]
        public void ToUFix8Test1(sbyte value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 }), Category("ToUFix8")]
        public void ToUFix8Test2(ushort value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 }), Category("ToUFix8")]
        public void ToUFix8Test3(short value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 }), Category("ToUFix8")]
        public void ToUFix8Test4(uint value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 }), Category("ToUFix8")]
        public void ToUFix8Test5(int value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 }), Category("ToUFix8")]
        public void ToUFix8Test6(ulong value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 }), Category("ToUFix8")]
        public void ToUFix8Test7(long value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 }), Category("ToUFix8")]
        public void ToUFix8Test8(Half value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 }), Category("ToUFix8")]
        public void ToUFix8Test9(float value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 }), Category("ToUFix8")]
        public void ToUFix8Test10(double value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 }), Category("ToUFix8")]
        public void ToUFix8Test11(decimal value, ushort expected) =>
            Assert.That(value.ToUFix8(), Is.EqualTo(expected));
    }
}