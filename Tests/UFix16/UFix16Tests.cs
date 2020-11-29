using NUnit.Framework;

using System.Collections.Generic;
using System.Globalization;

namespace System.Tests
{
    [TestFixture]
    public class UFix16Tests
    {
        public static IEnumerable<object[]> SourceGenerator1(int value1, int value2)
        {
            #region Source Arrays
            var UFix16ConversionSource = new uint[]
            {
                0,
                234,
                234 * 65536,
                UFix16.MinValue,
                UFix16.MaxValue,
                UFix16.One,
                UFix16.E,
                UFix16.Pi,
                UFix16.Epsilon,
            };

            var ToStringInvariantCultureExpected = new string[]
        {
            "0",
            "0.003570556640625",
            "234",
            "0",
            "65,535.9999847412",
            "1",
            "2.73353576660156",
            "3.14158630371094",
            "0.0000152587890625",
        };

            var ToStringUnitedStatesExpected = new string[]
        {
            "0",
            "0.003570556640625",
            "234",
            "0",
            "65,535.9999847412",
            "1",
            "2.73353576660156",
            "3.14158630371094",
            "0.0000152587890625",
        };

            var ToStringSouthAfricanExpected = new string[]
        {
            "0",
            "0,003570556640625",
            "234",
            "0",
            "65\u00A0535,9999847412",
            "1",
            "2,73353576660156",
            "3,14158630371094",
            "0,0000152587890625",
        };

            var ToStringIndianUrduExpected = new string[]
        {
            "0",
            "0\u066B003570556640625",
            "234",
            "0",
            "65\u066C535\u066B9999847412",
            "1",
            "2\u066B73353576660156",
            "3\u066B14158630371094",
            "0\u066B0000152587890625",
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
            2,
            3,
            0,
        };

            var ToDecimalExpectedSource = new decimal[]
        {
            0,
            0.003570556640625m,
            234,
            0,
            65535.9999847412m,
            1,
            2.73353576660156m,
            3.14158630371094m,
            1.52587890625e-5m,
        };

            var ToDoubleExpectedSource = new double[]
        {
            0,
            0.003570556640625,
            234,
            0,
            65535.999984741211,
            1,
            2.7335357666015625,
            3.1415863037109375,
            1.52587890625e-5,
        };

            var ToHalfExpectedSource = new Half[]
        {
            (Half)0,
            (Half)0.00357,
            (Half)234,
            (Half)0,
            Half.PositiveInfinity,
            (Half)1,
            (Half)2.734,
            (Half)3.14,
            (Half)1.526e-5,
        };

            var ToInt16ExpectedSource = new short[]
        {
            0,
            0,
            234,
            0,
            -1,
            1,
            2,
            3,
            0,
        };

            var ToInt32ExpectedSource = new int[]
        {
            0,
            0,
            234,
            0,
            65535,
            1,
            2,
            3,
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
                0x00100000
            };

            var ToInt64ExpectedSource = new long[]
        {
            0,
            0,
            234,
            0,
            65535,
            1,
            2,
            3,
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
            2,
            3,
            0,
        };

            var ToSingleExpectedSource = new float[]
        {
            0,
            0.00357055664f,
            234,
            0,
            65536f,
            1,
            2.73353577f,
            3.1415863f,
            1.52587891e-5f,
        };

            var ToUInt16ExpectedSource = new ushort[]
        {
            0,
            0,
            234,
            0,
            65535,
            1,
            2,
            3,
            0,
        };

            var ToUInt32ExpectedSource = new uint[]
        {
            0,
            0,
            234,
            0,
            65535,
            1,
            2,
            3,
            0,
        };

            var ToUInt64ExpectedSource = new ulong[]
        {
            0,
            0,
            234,
            0,
            65535,
            1,
            2,
            3,
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
                _ => UFix16ConversionSource.GetEnumerator()
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
                _ => UFix16ConversionSource.GetEnumerator()
            };

            while (array1.MoveNext() && array2.MoveNext())
                yield return new object[] { array1.Current, array2.Current };
        }

        public static IEnumerable<object[]> SourceGenerator2(int value)
        {
            #region Source Arrays

            var FromByteSource = new (byte, uint)[]
            {
                ( 0, 0x00000000u ),
                ( 1, 0x00010000u ),
                ( 234, 0x00EA0000u ),
                ( 255, 0x00FF0000u ),
            };

            var FromSByteSource = new (sbyte, uint)[]
        {
            (0, 0x00000000u ),
            (1, 0x00010000u ),
            (-123, 0xFF850000u ),
            (127, 0x007F0000u ),
        };

            var FromUInt16Source = new (ushort, uint)[]
        {
            (0, 0x00000000u ),
            (1, 0x00010000u ),
            (234, 0x00EA0000u ),
            (65535, 0xFFFF0000u ),
        };

            var FromInt16Source = new (short, uint)[]
            {
                (0, 0x00000000u ),
                (1, 0x00010000u ),
                (-234, 0xFF160000u ),
                (32767, 0x7FFF0000u ),
            };

            var FromUInt32Source = new (uint, uint)[]
            {
                (0, 0x00000000u ),
                (1, 0x00010000u ),
                (234, 0x00EA0000u ),
                (65535, 0xFFFF0000u ),
            };

            var FromInt32Source = new (int, uint)[]
            {
                    (0, 0x00000000u ),
                    (1, 0x00010000u ),
                    (-234, 0xFF160000u ),
                    (32767, 0x7FFF0000u ),
            };

            var FromUInt64Source = new (ulong, uint)[]
            {
                    (0, 0x00000000u ),
                    (1, 0x00010000u ),
                    (234, 0x00EA0000u ),
                    (65535, 0xFFFF0000u ),
            };

            var FromInt64Source = new (long, uint)[]
            {
                (0, 0x00000000u ),
                (1, 0x00010000u ),
                (-234, 0xFF160000u ),
                (32767, 0x7FFF0000u ),
            };

            var FromHalfSource = new (Half, uint)[]
            {
                ((Half)0, 0x00000000u ),
                ((Half)1, 0x00010000u ),
                ((Half)(-234), 0xFF160000u ),
                ((Half)0.5, 0x00008000u ),
            };

            var FromSingleSource = new (float, uint)[]
            {
                ( 0f, 0x00000000u ),
                ( 1f, 0x00010000u ),
                ( -234f, 0xFF160000u ),
                ( 0.5f, 0x00008000u ),
            };

            var FromDoubleSource = new (double, uint)[]
            {
                ( 0d, 0x00000000u ),
                ( 1d, 0x00010000u ),
                ( -234d, 0xFF160000u ),
                ( 0.5d, 0x00008000u ),
            };

            var FromDecimalSource = new (decimal, uint)[]
            {
                ( 0m, 0x00000000u ),
                ( 1m, 0x00010000u ),
                ( -234m, 0xFF160000u ),
                ( 0.5m, 0x00008000u ),
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
            Assert.That(UFix16.Epsilon, Is.EqualTo(1));

        [Test, Category("ToString")]
        public void ToStringTest() =>
            Assert.That(UFix16.ToString(163840), Is.EqualTo("2.5"));

        [TestCaseSource("ToStringTestGenerator"), Category("ToString")]
        public void ToStringTest1(uint value, string expected, CultureInfo culture) =>
            Assert.That(UFix16.ToString(value, "#,0.################", culture), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 5 }), Category("ToBoolean")]
        public void ToBooleanTest(uint value, bool expected) =>
            Assert.That(UFix16.ToBoolean(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 6 }), Category("ToByte")]
        public void ToByteTest(uint value, byte expected) =>
            Assert.That(UFix16.ToByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 17 }), Category("ToDecimal")]
        public void ToDecimalTest(uint value, decimal expected) =>
            Assert.That(UFix16.ToDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 16 }), Category("ToDouble")]
        public void ToDoubleTest(uint value, double expected) =>
            Assert.That(UFix16.ToDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 14 }), Category("ToHalf")]
        public void ToHalfTest(uint value, Half expected) =>
            Assert.That(UFix16.ToHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 8 }), Category("ToInt16")]
        public void ToInt16Test(uint value, short expected) =>
            Assert.That(UFix16.ToInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 10 }), Category("ToInt32")]
        public void ToInt32Test(uint value, int expected) =>
            Assert.That(UFix16.ToInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 12 }), Category("ToInt64")]
        public void ToInt64Test(uint value, long expected) =>
            Assert.That(UFix16.ToInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 7 }), Category("ToSByte")]
        public void ToSByteTest(uint value, sbyte expected) =>
            Assert.That(UFix16.ToSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 15 }), Category("ToSingle")]
        public void ToSingleTest(uint value, float expected) =>
            Assert.That(UFix16.ToSingle(value), Is.EqualTo(expected));

        [Test, Sequential, Category("ToString")]
        public void ToStringTest2([Values("0.5", "0,5")] string expected,
                                  [Values("en-US", "fr-FR")] string culture) =>
            Assert.That(UFix16.ToString(0x00008000, CultureInfo.CreateSpecificCulture(culture)), Is.EqualTo(expected));

        [Test, Category("ToType")]
        public void ToTypeTest_null_conversionType_throws_ArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => UFix16.ToType(0x00100000, null!, null));

        [Test, Category("ToType")]
        public void ToTypeTest_DateTime_conversionType_throws_InvalidCastException() =>
            Assert.Throws<InvalidCastException>(() => UFix16.ToType(0x00100000, typeof(DateTime), null));

        [TestCaseSource("SourceGenerator1", new object[] { 18, 19 }), Category("ToType")]
        public void ToTypeTest_conversionType(Type type, object expected) =>
            Assert.That(UFix16.ToType(0x00100000, type, null), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 9 }), Category("ToUInt16")]
        public void ToUInt16Test(uint value, ushort expected) =>
            Assert.That(UFix16.ToUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 11 }), Category("ToUInt32")]
        public void ToUInt32Test(uint value, uint expected) =>
            Assert.That(UFix16.ToUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator1", new object[] { 0, 13 }), Category("ToUInt64")]
        public void ToUInt64Test(uint value, ulong expected) =>
            Assert.That(UFix16.ToUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 }), Category("FromByte")]
        public void FromByteTest(byte value, uint expected) =>
            Assert.That(UFix16.FromByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 }), Category("FromSByte")]
        public void FromSByteTest(sbyte value, uint expected) =>
            Assert.That(UFix16.FromSByte(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 }), Category("FromUInt16")]
        public void FromUInt16Test(ushort value, uint expected) =>
            Assert.That(UFix16.FromUInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 }), Category("FromInt16")]
        public void FromInt16Test(short value, uint expected) =>
            Assert.That(UFix16.FromInt16(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 }), Category("FromUInt32")]
        public void FromUInt32Test(uint value, uint expected) =>
            Assert.That(UFix16.FromUInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 }), Category("FromInt32")]
        public void FromInt32Test(int value, uint expected) =>
            Assert.That(UFix16.FromInt32(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 }), Category("FromUInt64")]
        public void FromUInt64Test(ulong value, uint expected) =>
            Assert.That(UFix16.FromUInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 }), Category("FromInt64")]
        public void FromInt64Test(long value, uint expected) =>
            Assert.That(UFix16.FromInt64(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 }), Category("FromHalf")]
        public void FromHalfTest(Half value, uint expected) =>
            Assert.That(UFix16.FromHalf(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 }), Category("FromSingle")]
        public void FromSingleTest(float value, uint expected) =>
            Assert.That(UFix16.FromSingle(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 }), Category("FromDouble")]
        public void FromDoubleTest(double value, uint expected) =>
            Assert.That(UFix16.FromDouble(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 }), Category("FromDecimal")]
        public void FromDecimalTest(decimal value, uint expected) =>
            Assert.That(UFix16.FromDecimal(value), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 0 }), Category("ToUFix16")]
        public void ToUFix16Test(byte value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 1 }), Category("ToUFix16")]
        public void ToUFix16Test1(sbyte value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 3 }), Category("ToUFix16")]
        public void ToUFix16Test2(ushort value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 2 }), Category("ToUFix16")]
        public void ToUFix16Test3(short value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 5 }), Category("ToUFix16")]
        public void ToUFix16Test4(uint value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 4 }), Category("ToUFix16")]
        public void ToUFix16Test5(int value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 7 }), Category("ToUFix16")]
        public void ToUFix16Test6(ulong value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 6 }), Category("ToUFix16")]
        public void ToUFix16Test7(long value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 8 }), Category("ToUFix16")]
        public void ToUFix16Test8(Half value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 9 }), Category("ToUFix16")]
        public void ToUFix16Test9(float value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 10 }), Category("ToUFix16")]
        public void ToUFix16Test10(double value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));

        [TestCaseSource("SourceGenerator2", new object[] { 11 }), Category("ToUFix16")]
        public void ToUFix16Test11(decimal value, uint expected) =>
            Assert.That(value.ToUFix16(), Is.EqualTo(expected));
    }
}