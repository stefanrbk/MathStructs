using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace System.Tests
{
    [TestFixture(TestOf = typeof(UFix16))]
    public partial class UFix16Tests
    {
        public const double UFix16Max = 65535+(65535/65536.0);

        public const double UFix16Min = 0;

        public static readonly double[][] AddSource = new[]
        {
            new[]{ 0d, 0 },
            new[]{ 60000d, 1 },
        };
        public static readonly double[][] SubtractSource = new[]
        {
            new[]{ 0d, 0 },
            new[]{ 60000, 10d },
        };
        public static readonly double[][] MultiplySource = new[]
        {
            new[]{ 60000, 0.5 },
            new[]{ 300, 200d },
            new[]{ 0.125, 60000 },
        };
        public static readonly double[][] DivideSource = new[]
        {
            new[]{ 60000, 2d },
            new[]{ 60000, 60000d },
            new[]{ 30000, 0.5 },
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
        public static readonly double[] UFix16Source = new[]
        {
            0,
            0.003570556640625,
            234,
            65535.9999847412109375,
            1,
            2.7335357666015625,
            3.1415863037109375,
            0.0000152587890625,
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
        };
        public static readonly ushort[] ToUShortSource = new ushort[]
        {
            0,
            0,
            234,
            65535,
            1,
            2,
            3,
            0,
        };
        public static readonly int[] ToIntSource = new int[]
        {
            0,
            0,
            234,
            65535,
            1,
            2,
            3,
            0,
        };
        public static readonly uint[] ToUIntSource = new uint[]
        {
            0,
            0,
            234,
            65535,
            1,
            2,
            3,
            0,
        };
        public static readonly long[] ToLongSource = new long[]
        {
            0,
            0,
            234,
            65535,
            1,
            2,
            3,
            0,
        };
        public static readonly ulong[] ToULongSource = new ulong[]
        {
            0,
            0,
            234,
            65535,
            1,
            2,
            3,
            0,
        };
        public static readonly Half[] ToHalfSource = new Half[]
        {
            (Half)0,
            (Half)0.00357,
            (Half)234,
            (Half)65535,
            (Half)1,
            (Half)2.734,
            (Half)3.14,
            (Half)0.00001526,
        };
        public static readonly float[] ToFloatSource = new float[]
        {
            0,
            0.00357055664f,
            234,
            65536,
            1,
            2.73353577f,
            3.1415863f,
            0.0000152587891f,
        };
        public static readonly double[] ToDoubleSource = new[]
        {
            0,
            0.003570556640625,
            234,
            65535.9999847412109375,
            1,
            2.7335357666015625,
            3.1415863037109375,
            0.0000152587890625,
        };
        public static readonly Fix16[] ToFix16Source = new[]
        {
            (Fix16)0,
            (Fix16)0.003570556640625,
            (Fix16)234,
            (Fix16)32767.9999847412109375,
            (Fix16)1,
            (Fix16)2.7335357666015625,
            (Fix16)3.1415863037109375,
            (Fix16)0.0000152587890625,
        };
        public static readonly decimal[] ToDecimalSource = new decimal[]
        {
            0,
            0.003570556640625m,
            234,
            65535.9999847412m,
            1,
            2.73353576660156m,
            3.14158630371094m,
            0.0000152587890625m,
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
        public static readonly double[] SqrtSource = new double[]
            {
                25,
                16385,
                0.0625,
            };
        public static readonly UFix16[] UFix16ConversionSource = new UFix16[]
        {
            UFix16.Raw(0),
            UFix16.Raw(234),
            UFix16.Raw(15335424),
            UFix16.Raw(2147483647),
            UFix16.Raw(65536),
            UFix16.Raw(179145),
            UFix16.Raw(205887),
            UFix16.Raw(1)
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
            "0.0000152587890625"
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
                new object[] { UFix16.One, true },
                new object[] { null!, false },
                new object[] { new object(), false },
            };
        public static readonly object[] EqualsSource2 = new object[]
            {
                new object[] { UFix16.One, UFix16.Zero, true },
                new object[] { UFix16.Zero, UFix16.Pi, true },
                new object[] { UFix16.Pi, UFix16.One, false },
            };
        public static readonly object[] CompareToSource1 = new object[]
            {
                new object[] { null!, Is.EqualTo(1) },
                new object[] { UFix16.Pi, Is.LessThan(0) },
                new object[] { UFix16.Epsilon, Is.GreaterThan(0) },
                new object[] { UFix16.One, Is.Zero },
            };
        public static readonly object[] CompareToSource2 = new object[]
            {
                new object[] { UFix16.Pi, Is.LessThan(0) },
                new object[] { UFix16.Epsilon, Is.GreaterThan(0) },
                new object[] { UFix16.One, Is.Zero },
            };
        public static IEnumerable<object[]> ByteToUFix16Source()
        {
            yield return new object[] { (byte)0, UFix16.Zero };
            yield return new object[] { (byte)255, (UFix16)255 };
            yield return new object[] { (byte)3, (UFix16)3 };
        }
        public static IEnumerable<object[]> SByteToUFix16Source()
        {
            yield return new object[] { (sbyte)0, UFix16.Zero };
            yield return new object[] { (sbyte)-22, UFix16.MinValue };
            yield return new object[] { (sbyte)-1, UFix16.MinValue };
            yield return new object[] { (sbyte)7, (UFix16)7 };
        }
        public static IEnumerable<object[]> ShortToUFix16Source()
        {
            yield return new object[] { (short)0, UFix16.Zero };
            yield return new object[] { (short)234, (UFix16)234 };
            yield return new object[] { (short)-1, UFix16.MinValue };
            yield return new object[] { (short)-50, UFix16.MinValue };
        }
        public static IEnumerable<object[]> UShortToUFix16Source()
        {
            yield return new object[] { (ushort)0, UFix16.Zero };
            yield return new object[] { (ushort)234, (UFix16)234 };
            yield return new object[] { (ushort)65535, (UFix16)65535 };
            yield return new object[] { (ushort)206, (UFix16)206 };
        }
        public static IEnumerable<object[]> IntToUFix16Source()
        {
            yield return new object[] { 0, UFix16.Zero };
            yield return new object[] { 234, (UFix16)234 };
            yield return new object[] { -1, UFix16.MinValue };
            yield return new object[] { -50, UFix16.MinValue };
            yield return new object[] { 338170388, UFix16.MaxValue };
            yield return new object[] { -1589604192, UFix16.MinValue };
        }
        public static IEnumerable<object[]> UIntToUFix16Source()
        {
            yield return new object[] { 0u, UFix16.Zero };
            yield return new object[] { 234u, (UFix16)234 };
            yield return new object[] { unchecked((uint)-1), UFix16.MaxValue };
            yield return new object[] { unchecked((uint)-50), UFix16.MaxValue };
            yield return new object[] { 338170388u, UFix16.MaxValue };
            yield return new object[] { 2705363104, UFix16.MaxValue };
        }
        public static IEnumerable<object[]> LongToUFix16Source()
        {
            yield return new object[] { (long)0, UFix16.Zero };
            yield return new object[] { (long)234, (UFix16)234 };
            yield return new object[] { (long)-1, UFix16.MinValue };
            yield return new object[] { (long)-50, UFix16.MinValue };
            yield return new object[] { (long)338170388, UFix16.MaxValue };
            yield return new object[] { (long)-1589604192, UFix16.MinValue };
            yield return new object[] { (long)0x0400002014281214, UFix16.MaxValue };
            yield return new object[] { (long)-0x7FFFFBFD7AFDBD80, UFix16.MinValue };
        }
        public static IEnumerable<object[]> ULongToUFix16Source()
        {
            yield return new object[] { (ulong)0, UFix16.Zero };
            yield return new object[] { (ulong)234, (UFix16)234 };
            yield return new object[] { (ulong)65535, (UFix16)65535 };
            yield return new object[] { (ulong)206, (UFix16)206 };
            yield return new object[] { (ulong)338170388, UFix16.MaxValue };
            yield return new object[] { (ulong)0xA14090A0, UFix16.MaxValue };
            yield return new object[] { (ulong)0x0400002014281214u, UFix16.MaxValue };
            yield return new object[] { (ulong)0x8000040285024280u, UFix16.MaxValue };
        }
        public static IEnumerable<object[]> HalfToUFix16Source()
        {
            yield return new object[] { (Half)0, UFix16.Zero };
            yield return new object[] { (Half)234, (UFix16)234 };
            yield return new object[] { (Half)( -1 ), UFix16.MinValue };
            yield return new object[] { (Half)( -73 ), UFix16.MinValue };
        }
        public static IEnumerable<object[]> FloatToUFix16Source()
        {
            yield return new object[] { 0f, UFix16.Zero };
            yield return new object[] { 234f, (UFix16)234 };
            yield return new object[] { -1f, UFix16.MinValue };
            yield return new object[] { -73f, UFix16.MinValue };
        }
        public static IEnumerable<object[]> DoubleToUFix16Source()
        {
            yield return new object[] { 0d, UFix16.Zero };
            yield return new object[] { 234d, (UFix16)234 };
            yield return new object[] { -1d, UFix16.MinValue };
            yield return new object[] { -73d, UFix16.MinValue };
        }
        public static IEnumerable<object[]> DecimalToUFix16Source()
        {
            yield return new object[] { 0m, UFix16.Zero };
            yield return new object[] { 234m, (UFix16)234 };
            yield return new object[] { -1m, UFix16.MinValue };
            yield return new object[] { -73m, UFix16.MinValue };
        }
        public static IEnumerable<object[]> Fix16ToUFix16Source()
        {
            yield return new object[] { (Fix16)0, UFix16.Zero };
            yield return new object[] { (Fix16)234, (UFix16)234 };
            yield return new object[] { (Fix16)( -1d ), UFix16.MinValue };
            yield return new object[] { (Fix16)( -73d ), UFix16.MinValue };
        }

        private static double Fix(double value) =>
            (UFix16)value;
        private static double? Fix(double? value) =>
            (UFix16?)value;
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

        #region Attributes

        public abstract class RandMathAttribute : RandomAttribute
        {
            public RandMathAttribute(double min, double max, int count = 20) : base(min, max, count) { }
        }
        public class RandAddAttribute : RandMathAttribute
        {
            public RandAddAttribute() : base(0, UFix16Max / 2) { }
        }
        public class RandAddOverAttribute : RandMathAttribute
        {
            public RandAddOverAttribute() : base(0, UFix16Max) { }
        }
        public class RandSub1Attribute : RandMathAttribute
        {
            public RandSub1Attribute() : base(UFix16Max / 2, UFix16Max) { }
        }
        public class RandSub2Attribute : RandMathAttribute
        {
            public RandSub2Attribute() : base(UFix16Min, UFix16Max / 2) { }
        }
        public class RandSubOverAttribute : RandMathAttribute
        {
            public RandSubOverAttribute() : base(UFix16Min, UFix16Max) { }
        }
        public class RandMulAttribute : RandMathAttribute
        {
            public RandMulAttribute() : base(UFix16Min, 256 - ( 1 / 65536d )) { }
        }
        public class RandMulOverAttribute : RandMathAttribute
        {
            public RandMulOverAttribute() : base(UFix16Min, 512 - ( 1 / 65536d )) { }
        }
        public class RandDiv1Attribute : RandMathAttribute
        {
            public RandDiv1Attribute() : base(UFix16Min, UFix16Max) { }
        }
        public class RandDiv2Attribute : RandMathAttribute
        {
            public RandDiv2Attribute() : base(1, UFix16Max/2) { }
        }
        public class RandDivOver1Attribute : RandMathAttribute
        {
            public RandDivOver1Attribute() : base(0, UFix16Max/3) { }
        }
        public class RandDivOver2Attribute : RandMathAttribute
        {
            public RandDivOver2Attribute() : base(1 / 65536d, 2) { }
        }
        public class RandExpAttribute : RandMathAttribute
        {
            public RandExpAttribute() : base(0, 726817.0 / 65536) { }
        }
        public class RandZeroNintyAttribute : RandMathAttribute
        {
            public RandZeroNintyAttribute() : base(0, Math.PI / 2) { }
        }

        public class RandHalf1Attribute : RandomAttribute
        {
            public RandHalf1Attribute() : base((ushort)0, (ushort)31743, 20) { }
        }
        public class RandHalf2Attribute : RandomAttribute
        {
            public RandHalf2Attribute() : base((ushort)32768, (ushort)64512, 4) { }
        }

        #endregion Attributes
    }
}
