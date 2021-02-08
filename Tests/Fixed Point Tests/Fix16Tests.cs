using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;

namespace System.Tests
{
    [TestFixture(TestOf = typeof(Fix16))]
    public partial class Fix16Tests
    {
        public const double Fix16Max = 32767+(65535/65536.0);
        public const double Fix16Min = -32768;

        public static readonly double[][] AddSource = new[]
        {
            new[]{ 0d, 0 },
            new[]{ 30000d, 1 },
            new[]{ 30000d, 30000d },
            new[]{ -30000d, -30000d },
        };
        public static readonly double[][] SubtractSource = new[]
        {
            new[]{ 0d, 0 },
            new[]{ 30000, 10d },
            new[]{ 30000d, -30000d },
            new[]{ -30000d, 30000d },
        };
        public static readonly double[][] MultiplySource = new[]
        {
            new[]{ 30000, 0.5 },
            new[]{ -30000, 0.5 },
            new[]{ 300, 200d },
            new[]{ 0.125, 30000 },
        };
        public static readonly double[][] DivideSource = new[]
        {
            new[]{ 30000, 2d },
            new[]{ 30000, 30000d },
            new[]{ 30000, 0.5 },
            new[]{ -30000, 0.5 },
            new[]{ 30000, 0d },
        };
        public static readonly double?[][] NullableDivideSource = new[]
        {
            new double?[]{ 1, 0, null },
            new double?[]{ 10, null, null },
            new double?[]{ null, null, null },
            new double?[]{ null, 10, null },
            new double?[]{ 27.5, 11, 2.5 },
            new double?[]{ 2, 1, 2 },
        };
        public static readonly object[] LerpSource = new object[]
        {
            new object[] { 10.0, 11.0, (byte)0x80, 10.5 },
            new object[] { 1.0, 3.0, (byte)0x20, 1.25 },
        };
        public static readonly object[] Lerp2Source = new object[]
        {
            new object[] { 10.0, 11.0, (ushort)0x8000, 10.5 },
            new object[] { 1.0, 3.0, (ushort)0x2000, 1.25 },
        };
        public static readonly object[] Lerp3Source = new object[]
        {
            new object[] { 10.0, 11.0, (uint)0x80000000, 10.5 },
            new object[] { 1.0, 3.0, (uint)0x20000000, 1.25 },
        };
        public static readonly Fix16[] ExpSource = new Fix16[]
        {
            0,
            1,
            Fix16.Raw(681392),
            Fix16.Raw(-772244),
            2,
            2,
        };
        public static readonly double[] SqrtSource = new double[]
        {
            25,
            16385,
            -0.0625,
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
        public static readonly double[][] EqualitySource = new[]
        {
            new[]{ 10, 0.5 },
            new[]{ -10, -10.0 },
            new[]{ 10, 10.1 },
        };
        public static readonly double?[][] NullableEqualitySource = new[]
        {
            new double?[]{ null, null },
            new double?[]{ null, 7 },
            new double?[]{ 7, null },
        };
        public static readonly double[] Fix16Source = new[]
        {
            0,
            0.003570556640625,
            234,
            32767.9999847412109375,
            1,
            2.7335357666015625,
            3.1415863037109375,
            0.0000152587890625,
            -1,
            -2.7335357666015625,
            -3.1415863037109375,
        };
        public static readonly byte[] ToByteSource = new byte[]
            {
                0,
                0,
                234,
                255,
                1,
                2,
                3,
                0,
                0,
                0,
                0,
            };
        public static readonly sbyte[] ToSByteSource = new sbyte[]
            {
                0,
                0,
                127,
                127,
                1,
                2,
                3,
                0,
                -1,
                -2,
                -3,
            };
        public static readonly short[] ToShortSource = new short[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                -1,
                -2,
                -3,
            };
        public static readonly ushort[] ToUShortSource = new ushort[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                0,
                0,
                0,
            };
        public static readonly int[] ToIntSource = new int[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                -1,
                -2,
                -3,
            };
        public static readonly uint[] ToUIntSource = new uint[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                0,
                0,
                0,
            };
        public static readonly long[] ToLongSource = new long[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                -1,
                -2,
                -3,
            };
        public static readonly ulong[] ToULongSource = new ulong[]
            {
                0,
                0,
                234,
                32767,
                1,
                2,
                3,
                0,
                0,
                0,
                0,
            };
        public static readonly Half[] ToHalfSource = new Half[]
            {
                (Half)0,
                (Half)0.00357,
                (Half)234,
                (Half)32770,
                (Half)1,
                (Half)2.734,
                (Half)3.14,
                (Half)0.00001526,
                (Half)(-1),
                (Half)(-2.734),
                (Half)(-3.14),
            };
        public static readonly float[] ToFloatSource = new float[]
            {
                0,
                0.00357055664f,
                234,
                32768,
                1,
                2.73353577f,
                3.1415863f,
                0.0000152587891f,
                -1,
                -2.73353577f,
                -3.1415863f,
            };
        public static readonly double[] ToDoubleSource = new[]
            {
                0,
                0.003570556640625,
                234,
                32767.9999847412109375,
                1,
                2.7335357666015625,
                3.1415863037109375,
                0.0000152587890625,
                -1,
                -2.7335357666015625,
                -3.1415863037109375,
            };
        public static readonly UFix16[] ToUFix16Source = new[]
            {
                (UFix16)0,
                (UFix16)0.003570556640625,
                (UFix16)234,
                (UFix16)32767.9999847412109375,
                (UFix16)1,
                (UFix16)2.7335357666015625,
                (UFix16)3.1415863037109375,
                (UFix16)0.0000152587890625,
                (UFix16)0,
                (UFix16)0,
                (UFix16)0,
            };
        public static readonly decimal[] ToDecimalSource = new decimal[]
            {
                0,
                0.003570556640625m,
                234,
                32767.9999847412m,
                1,
                2.73353576660156m,
                3.14158630371094m,
                0.0000152587890625m,
                -1m,
                -2.73353576660156m,
                -3.14158630371094m,
            };
        public static readonly Type[] ToTypeTypesSource = new Type[]
        {
            null!,
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(char),
            typeof(DateTime),
        };
        public static readonly double[] ToTypeValuesSource = new double[]
        {
            0,
            1,
            0.1,
            60000,
            120000,
        };
        public static IEnumerable<object[]> ByteToFix16Source()
        {
            yield return new object[] { (byte)0, Fix16.Zero };
            yield return new object[] { (byte)255, (Fix16)255 };
            yield return new object[] { (byte)3, (Fix16)3 };
        }
        public static IEnumerable<object[]> SByteToFix16Source()
        {
            yield return new object[] { (sbyte)0, Fix16.Zero };
            yield return new object[] { (sbyte)-22, (Fix16)(-22) };
            yield return new object[] { (sbyte)-1, (Fix16)(-1) };
            yield return new object[] { (sbyte)7, (Fix16)7 };
        }
        public static IEnumerable<object[]> ShortToFix16Source()
        {
            yield return new object[] { (short)0, Fix16.Zero };
            yield return new object[] { (short)234, (Fix16)234 };
            yield return new object[] { (short)-1, (Fix16)(-1) };
            yield return new object[] { (short)-50, (Fix16)(-50) };
        }
        public static IEnumerable<object[]> UShortToFix16Source()
        {
            yield return new object[] { (ushort)0, Fix16.Zero };
            yield return new object[] { (ushort)234, (Fix16)234 };
            yield return new object[] { (ushort)65535, (Fix16)65535 };
            yield return new object[] { (ushort)206, (Fix16)206 };
        }
        public static IEnumerable<object[]> IntToFix16Source()
        {
            yield return new object[] { 0, Fix16.Zero };
            yield return new object[] { 234, (Fix16)234 };
            yield return new object[] { -1, (Fix16)(-1) };
            yield return new object[] { -50, (Fix16)(-50) };
            yield return new object[] { 4628, (Fix16)4628 };
            yield return new object[] { -37024, (Fix16)(-37024) };
        }
        public static IEnumerable<object[]> UIntToFix16Source()
        {
            yield return new object[] { 0u, Fix16.Zero };
            yield return new object[] { 234u, (Fix16)234 };
            yield return new object[] { unchecked((uint)-1), Fix16.MaxValue };
            yield return new object[] { unchecked((uint)-50), Fix16.MaxValue };
            yield return new object[] { 338170388u, Fix16.MaxValue };
            yield return new object[] { 2705363104, Fix16.MaxValue };
        }
        public static IEnumerable<object[]> LongToFix16Source()
        {
            yield return new object[] { (long)0, Fix16.Zero };
            yield return new object[] { (long)234, (Fix16)234 };
            yield return new object[] { (long)-1, (Fix16)(-1) };
            yield return new object[] { (long)-50, (Fix16)(-50) };
            yield return new object[] { (long)338170388, Fix16.MaxValue };
            yield return new object[] { (long)-1589604192, Fix16.MinValue };
            yield return new object[] { (long)0x0400002014281214, Fix16.MaxValue };
            yield return new object[] { (long)-0x7FFFFBFD7AFDBD80, Fix16.MinValue };
        }
        public static IEnumerable<object[]> ULongToFix16Source()
        {
            yield return new object[] { (ulong)0, Fix16.Zero };
            yield return new object[] { (ulong)234, (Fix16)234 };
            yield return new object[] { (ulong)65535, (Fix16)65535 };
            yield return new object[] { (ulong)206, (Fix16)206 };
            yield return new object[] { (ulong)338170388, Fix16.MaxValue };
            yield return new object[] { (ulong)0xA14090A0, Fix16.MaxValue };
            yield return new object[] { (ulong)0x0400002014281214u, Fix16.MaxValue };
            yield return new object[] { (ulong)0x8000040285024280u, Fix16.MaxValue };
        }
        public static IEnumerable<object[]> HalfToFix16Source()
        {
            yield return new object[] { (Half)0, Fix16.Zero };
            yield return new object[] { (Half)234, (Fix16)234 };
            yield return new object[] { (Half)( -1 ), (Fix16)( -1 ) };
            yield return new object[] { (Half)( -73 ), (Fix16)( -73 ) };
        }
        public static IEnumerable<object[]> FloatToFix16Source()
        {
            yield return new object[] { 0f, Fix16.Zero };
            yield return new object[] { 234f, (Fix16)234 };
            yield return new object[] { -1f, (Fix16)( -1 ) };
            yield return new object[] { -73f, (Fix16)( -73 ) };
        }
        public static IEnumerable<object[]> DoubleToFix16Source()
        {
            yield return new object[] { 0d, Fix16.Zero };
            yield return new object[] { 234d, (Fix16)234 };
            yield return new object[] { -1d, (Fix16)( -1 ) };
            yield return new object[] { -73d, (Fix16)( -73 ) };
        }
        public static IEnumerable<object[]> DecimalToFix16Source()
        {
            yield return new object[] { 0m, Fix16.Zero };
            yield return new object[] { 234m, (Fix16)234 };
            yield return new object[] { -1m, (Fix16)( -1 ) };
            yield return new object[] { -73m, (Fix16)( -73 ) };
        }
        public static IEnumerable<object[]> UFix16ToFix16Source()
        {
            yield return new object[] { (UFix16)0, Fix16.Zero };
            yield return new object[] { (UFix16)234, (Fix16)234 };
            yield return new object[] { (UFix16)65535, Fix16.MaxValue };
            yield return new object[] { (UFix16)65463, Fix16.MaxValue };
        }
        public static readonly Fix16[] UFix16ConversionSource = new Fix16[]
        {
            Fix16.Raw(0),
            Fix16.Raw(234),
            Fix16.Raw(15335424),
            Fix16.Raw(2147483647),
            Fix16.Raw(65536),
            Fix16.Raw(179145),
            Fix16.Raw(205887),
            Fix16.Raw(1),
            Fix16.Raw(-65536),
            Fix16.Raw(-179145),
            Fix16.Raw(-205887),
        };
        public static readonly string[] ToStringInvariantCultureExpected = new string[]
        {
            "0",
            "0.003570556640625",
            "234",
            "32,767.9999847412",
            "1",
            "2.73353576660156",
            "3.14158630371094",
            "0.0000152587890625",
            "-1",
            "-2.73353576660156",
            "-3.14158630371094",
        };
        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            for (var i = 0; i < UFix16ConversionSource.Length; i++)
            {
                yield return new object[3]
                {
                    UFix16ConversionSource[i],
                    ToStringInvariantCultureExpected[i],
                    CultureInfo.InvariantCulture
                };
            }
        }
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
                new object[] { Fix16.Epsilon, Is.GreaterThan(0) },
                new object[] { Fix16.One, Is.Zero },
            };
        public static readonly object[] CompareToSource2 = new object[]
            {
                new object[] { Fix16.Pi, Is.LessThan(0) },
                new object[] { Fix16.Epsilon, Is.GreaterThan(0) },
                new object[] { Fix16.One, Is.Zero },
            };

        private static double Fix(double value) =>
                (Fix16)value;
        private static double? Fix(double? value) =>
            (Fix16?)value;
        private static double FixNullIsNaN(double? value) =>
            value is null
                ? Double.NaN
                : (UFix16)value;
        private static void ToTypeTest<T>(Func<IFormatProvider?, T> actualFunc, Func<IFormatProvider?, T> expectedFunc) where T : IConvertible
        {
            IConvertible actual, expected;
            try { actual = actualFunc(null); }
            catch (Exception e) { Assert.That(() => expectedFunc(null), Throws.TypeOf(e.GetType())); return; } 
            
            try { expected = expectedFunc(null); } catch (Exception e) { Assert.Fail($"Expected: {e.GetType().FullName} thrown  But was: {actual}"); return; }

            Assert.That(actual, Is.EqualTo(expected));
        }
        private static void ToTypeTest(Func<Half> actualFunc, Func<Half> expectedFunc)
        {
            Half actual, expected;
            try { actual = actualFunc(); }
            catch (Exception e) { Assert.That(expectedFunc, Throws.TypeOf(e.GetType())); return; } 
            
            try { expected = expectedFunc(); } catch (Exception e) { Assert.Fail($"Expected: {e.GetType().FullName} thrown  But was: {actual}"); return; }

            Assert.That(actual, Is.EqualTo(expected));
        }

        public abstract class RandMathAttribute : RandomAttribute
        {
            public RandMathAttribute(double min, double max, int count = 20) : base(min, max, count) { }
        }

        public class RandAddAttribute : RandMathAttribute
        {
            public RandAddAttribute() : base(Fix16Min / 2 + 1, Fix16Max / 2 - 1) { }
        }
        public class RandSubLAttribute : RandMathAttribute
        {
            public RandSubLAttribute() : base(Fix16Max / 2, Fix16Max) { }
        }
        public class RandSubRAttribute : RandMathAttribute
        {
            public RandSubRAttribute() : base(Fix16Min, Fix16Max / 2) { }
        }
        public class RandMulAttribute : RandMathAttribute
        {
            public RandMulAttribute() : base(-( 256 - ( 1 / 65536d ) ), 256 - ( 1 / 65536d )) { }
        }
        public class RandDivLAttribute : RandMathAttribute
        {
            public RandDivLAttribute() : base(Fix16Min, Fix16Max) { }
        }
        public class RandDivRAttribute : RandMathAttribute
        {
            public RandDivRAttribute() : base(1, Fix16Max/2) { }
        }

        public class RandAddOverAttribute : RandMathAttribute
        {
            public RandAddOverAttribute() : base(Fix16Min / 4, Fix16Max) { }
        }
        public class RandSubOverLAttribute : RandMathAttribute
        {
            public RandSubOverLAttribute() : base(Fix16Min, Fix16Max) { }
        }
        public class RandSubOverR1Attribute : RandMathAttribute
        {
            public RandSubOverR1Attribute() : base(Fix16Max / 2, Fix16Max) { }
        }
        public class RandSubOverR2Attribute : RandMathAttribute
        {
            public RandSubOverR2Attribute() : base(Fix16Min, Fix16Min / 2) { }
        }
        public class RandMulOverAttribute : RandMathAttribute
        {
            public RandMulOverAttribute() : base(-( 512 - ( 1 / 65536d ) ), 512 - ( 1 / 65536d )) { }
        }
        public class RandDivOverLAttribute : RandMathAttribute
        {
            public RandDivOverLAttribute() : base(0, Fix16Max/3) { }
        }
        public class RandDivOverRAttribute : RandMathAttribute
        {
            public RandDivOverRAttribute() : base(-1.5, 1.5) { }
        }

        public class RandLerpAttribute : RandMathAttribute
        {
            public RandLerpAttribute() : base(Fix16Min, Fix16Max) { }
        }

        public class RandHalfLAttribute : RandomAttribute
        {
            public RandHalfLAttribute() : base((ushort)0, (ushort)31743, 20) { }
        }
        public class RandHalfRAttribute : RandomAttribute
        {
            public RandHalfRAttribute() : base((ushort)32768, (ushort)64512, 4) { }
        }
    }
}