using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace System.Tests
{
    [TestFixture(TestOf = typeof(UFix8))]
    public partial class UFix8Tests
    {
        public const float UFix8Max = 255+(255/256f);

        public const float UFix8Min = 0;

        public static readonly float[][] AddSource = new[]
        {
            new[]{ 0f, 0f },
            new[]{ 200f, 1f },
        };
        public static readonly float[][] SubtractSource = new[]
        {
            new[]{ 0f, 0f },
            new[]{ 200, 10f },
        };
        public static readonly float[][] MultiplySource = new[]
        {
            new[]{ 200f, 0.5f },
            new[]{ 10f, 20f },
            new[]{ 0.125f, 200f },
        };
        public static readonly float[][] DivideSource = new[]
        {
            new[]{ 200f, 2f },
            new[]{ 200f, 200f },
            new[]{ 100f, 0.5f },
        };
        public static readonly float?[][] NullableDivideSource = new[]
        {
            new float?[]{ 1f, 0f, null },
            new float?[]{ 10f, null, null },
            new float?[]{ null, null, null },
            new float?[]{ null, 10f, null },
            new float?[]{ 27.5f, 11f, 2.5f },
            new float?[]{ 2f, 1f, 2f },
        };
        public static readonly float[][] EqualitySource = new[]
        {
            new[]{ 10f, 0.5f },
            new[]{ 10f, 10f },
            new[]{ 10f, 10.1f },
        };
        public static readonly float?[][] NullableEqualitySource = new[]
        {
            new float?[]{ null, null },
            new float?[]{ null, 7f },
            new float?[]{ 7f, null },
        };
        public static readonly float[] UFix8Source = new[]
        {
            0f,
            0.9140625f,
            234f,
            255.99609375f,
            1f,
            2.71875f,
            3.140625f,
            0.00390625f,
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
            255,
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
            255,
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
            255,
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
            255,
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
            255,
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
            255,
            1,
            2,
            3,
            0,
        };
        public static readonly Half[] ToHalfSource = new Half[]
        {
            (Half)0,
            (Half)0.9140625f,
            (Half)234,
            (Half)255.99609375f,
            (Half)1,
            (Half)2.71875f,
            (Half)3.140625f,
            (Half)0.00390625f,
        };
        public static readonly float[] ToFloatSource = new float[]
        {
            0f,
            0.9140625f,
            234f,
            255.99609375f,
            1f,
            2.71875f,
            3.140625f,
            0.00390625f,
        };
        public static readonly double[] ToDoubleSource = new[]
        {
            0d,
            0.9140625d,
            234d,
            255.99609375d,
            1d,
            2.71875d,
            3.140625d,
            0.00390625d,
        };
        public static readonly Fix16[] ToFix16Source = new[]
        {
            (Fix16)0f,
            (Fix16)0.9140625f,
            (Fix16)234f,
            (Fix16)255.99609375f,
            (Fix16)1f,
            (Fix16)2.71875f,
            (Fix16)3.140625f,
            (Fix16)0.00390625f,
        };
        public static readonly UFix16[] ToUFix16Source = new[]
        {
            (UFix16)0f,
            (UFix16)0.9140625f,
            (UFix16)234f,
            (UFix16)255.99609375f,
            (UFix16)1f,
            (UFix16)2.71875f,
            (UFix16)3.140625f,
            (UFix16)0.00390625f,
        };
        public static readonly decimal[] ToDecimalSource = new decimal[]
        {
            0m,
            0.9140625m,
            234m,
            255.9961m,
            1m,
            2.71875m,
            3.140625m,
            0.00390625m,
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
        public static readonly float[] ToTypeValuesSource = new float[]
        {
            0f,
            1f,
            0.1f,
            200f,
            100f,
        };
        public static readonly object[] LerpSource = new object[]
        {
            new object[] { 10f, 11f, (byte)0x80, 10.5f },
            new object[] { 1f, 3f, (byte)0x20, 1.25f },
        };
        public static readonly object[] Lerp2Source = new object[]
        {
            new object[] { 10f, 11f, (ushort)0x8000, 10.5f },
            new object[] { 1f, 3f, (ushort)0x2000, 1.25f },
        };
        public static readonly object[] Lerp3Source = new object[]
        {
            new object[] { 10f, 11f, (uint)0x80000000, 10.5f },
            new object[] { 1f, 3f, (uint)0x20000000, 1.25f },
        };
        public static readonly float[] SqrtSource = new float[]
            {
                25f,
                132.25f,
                0.0625f,
            };
        public static readonly string[] ToStringInvariantCultureExpected = new string[]
        {
            "0",
            "0.9140625",
            "234",
            "255.9961",
            "1",
            "2.71875",
            "3.140625",
            "0.00390625"
        };
        public static IEnumerable<object[]> ToStringTestGenerator()
        {
            for (var i = 0; i < UFix8Source.Length; i++)
            {
                yield return new object[3]
                {
                    (UFix8)UFix8Source[i],
                    ToStringInvariantCultureExpected[i],
                    CultureInfo.InvariantCulture
                };
            }
        }
        public static readonly object[] EqualsSource1 = new object[]
            {
                new object[] { UFix8.One, true },
                new object[] { null!, false },
                new object[] { new object(), false },
            };
        public static readonly object[] EqualsSource2 = new object[]
            {
                new object[] { UFix8.One, UFix8.Zero, true },
                new object[] { UFix8.Zero, (UFix8)3, true },
                new object[] { (UFix8)3, UFix8.One, false },
            };
        public static readonly object[] CompareToSource1 = new object[]
            {
                new object[] { null!, Is.EqualTo(1) },
                new object[] { (UFix8)3, Is.LessThan(0) },
                new object[] { UFix8.Epsilon, Is.GreaterThan(0) },
                new object[] { UFix8.One, Is.Zero },
            };
        public static readonly object[] CompareToSource2 = new object[]
            {
                new object[] { (UFix8)3, Is.LessThan(0) },
                new object[] { UFix8.Epsilon, Is.GreaterThan(0) },
                new object[] { UFix8.One, Is.Zero },
            };
        public static IEnumerable<object[]> ByteToUFix8Source()
        {
            yield return new object[] { (byte)0, UFix8.Zero };
            yield return new object[] { (byte)255, (UFix8)255 };
            yield return new object[] { (byte)3, (UFix8)3 };
        }
        public static IEnumerable<object[]> SByteToUFix8Source()
        {
            yield return new object[] { (sbyte)0, UFix8.Zero };
            yield return new object[] { (sbyte)-22, UFix8.MinValue };
            yield return new object[] { (sbyte)-1, UFix8.MinValue };
            yield return new object[] { (sbyte)7, (UFix8)7 };
        }
        public static IEnumerable<object[]> ShortToUFix8Source()
        {
            yield return new object[] { (short)0, UFix8.Zero };
            yield return new object[] { (short)234, (UFix8)234 };
            yield return new object[] { (short)-1, UFix8.MinValue };
            yield return new object[] { (short)-50, UFix8.MinValue };
        }
        public static IEnumerable<object[]> UShortToUFix8Source()
        {
            yield return new object[] { (ushort)0, UFix8.Zero };
            yield return new object[] { (ushort)234, (UFix8)234 };
            yield return new object[] { (ushort)65535, UFix8.MaxValue };
            yield return new object[] { (ushort)206, (UFix8)206 };
        }
        public static IEnumerable<object[]> IntToUFix8Source()
        {
            yield return new object[] { 0, UFix8.Zero };
            yield return new object[] { 234, (UFix8)234 };
            yield return new object[] { -1, UFix8.MinValue };
            yield return new object[] { -50, UFix8.MinValue };
            yield return new object[] { 338170388, UFix8.MaxValue };
            yield return new object[] { -1589604192, UFix8.MinValue };
        }
        public static IEnumerable<object[]> UIntToUFix8Source()
        {
            yield return new object[] { 0u, UFix8.Zero };
            yield return new object[] { 234u, (UFix8)234 };
            yield return new object[] { UInt32.MaxValue, UFix8.MaxValue };
            yield return new object[] { UInt32.MaxValue-50, UFix8.MaxValue };
            yield return new object[] { 338170388u, UFix8.MaxValue };
            yield return new object[] { 2705363104, UFix8.MaxValue };
        }
        public static IEnumerable<object[]> LongToUFix8Source()
        {
            yield return new object[] { (long)0, UFix8.Zero };
            yield return new object[] { (long)234, (UFix8)234 };
            yield return new object[] { (long)-1, UFix8.MinValue };
            yield return new object[] { (long)-50, UFix8.MinValue };
            yield return new object[] { (long)338170388, UFix8.MaxValue };
            yield return new object[] { (long)-1589604192, UFix8.MinValue };
            yield return new object[] { (long)0x0400002014281214, UFix8.MaxValue };
            yield return new object[] { (long)-0x7FFFFBFD7AFDBD80, UFix8.MinValue };
        }
        public static IEnumerable<object[]> ULongToUFix8Source()
        {
            yield return new object[] { (ulong)0, UFix8.Zero };
            yield return new object[] { (ulong)234, (UFix8)234 };
            yield return new object[] { (ulong)65535, UFix8.MaxValue };
            yield return new object[] { (ulong)206, (UFix8)206 };
            yield return new object[] { (ulong)338170388, UFix8.MaxValue };
            yield return new object[] { (ulong)0xA14090A0, UFix8.MaxValue };
            yield return new object[] { (ulong)0x0400002014281214u, UFix8.MaxValue };
            yield return new object[] { (ulong)0x8000040285024280u, UFix8.MaxValue };
        }
        public static IEnumerable<object[]> HalfToUFix8Source()
        {
            yield return new object[] { (Half)0, UFix8.Zero };
            yield return new object[] { (Half)234, (UFix8)234 };
            yield return new object[] { (Half)( -1 ), UFix8.MinValue };
            yield return new object[] { (Half)( -73 ), UFix8.MinValue };
        }
        public static IEnumerable<object[]> FloatToUFix8Source()
        {
            yield return new object[] { 0f, UFix8.Zero };
            yield return new object[] { 234f, (UFix8)234 };
            yield return new object[] { -1f, UFix8.MinValue };
            yield return new object[] { -73f, UFix8.MinValue };
        }
        public static IEnumerable<object[]> DoubleToUFix8Source()
        {
            yield return new object[] { 0d, UFix8.Zero };
            yield return new object[] { 234d, (UFix8)234 };
            yield return new object[] { -1d, UFix8.MinValue };
            yield return new object[] { -73d, UFix8.MinValue };
        }
        public static IEnumerable<object[]> DecimalToUFix8Source()
        {
            yield return new object[] { 0m, UFix8.Zero };
            yield return new object[] { 234m, (UFix8)234 };
            yield return new object[] { -1m, UFix8.MinValue };
            yield return new object[] { -73m, UFix8.MinValue };
        }
        public static IEnumerable<object[]> Fix16ToUFix8Source()
        {
            yield return new object[] { (Fix16)0, UFix8.Zero };
            yield return new object[] { (Fix16)234, (UFix8)234 };
            yield return new object[] { (Fix16)( -1d ), UFix8.MinValue };
            yield return new object[] { (Fix16)( -73d ), UFix8.MinValue };
        }
        public static IEnumerable<object[]> UFix16ToUFix8Source()
        {
            yield return new object[] { (UFix16)0, UFix8.Zero };
            yield return new object[] { (UFix16)234, (UFix8)234 };
            yield return new object[] { (UFix16)300, UFix8.MaxValue };
            yield return new object[] { UFix16.Epsilon, UFix8.Zero };
        }

        private static float Fix(float value) =>
            (UFix8)value;
        private static float? Fix(float? value) =>
            (UFix8?)value;
        private static float FixNullIsNaN(float? value) =>
            value is null
                ? Single.NaN
                : (UFix8)value;
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
            public RandMathAttribute(float min, float max, int count = 20) : base(min, max, count) { }
        }
        public class RandAddAttribute : RandMathAttribute
        {
            public RandAddAttribute() : base(0, UFix8Max / 2) { }
        }
        public class RandAddOverAttribute : RandMathAttribute
        {
            public RandAddOverAttribute() : base(0, UFix8Max) { }
        }
        public class RandSub1Attribute : RandMathAttribute
        {
            public RandSub1Attribute() : base(UFix8Max / 2, UFix8Max) { }
        }
        public class RandSub2Attribute : RandMathAttribute
        {
            public RandSub2Attribute() : base(UFix8Min, UFix8Max / 2) { }
        }
        public class RandSubOverAttribute : RandMathAttribute
        {
            public RandSubOverAttribute() : base(UFix8Min, UFix8Max) { }
        }
        public class RandMulAttribute : RandMathAttribute
        {
            public RandMulAttribute() : base(UFix8Min, 64 - ( 1 / 256f )) { }
        }
        public class RandMulOverAttribute : RandMathAttribute
        {
            public RandMulOverAttribute() : base(UFix8Min, 128 - ( 1 / 256f )) { }
        }
        public class RandDiv1Attribute : RandMathAttribute
        {
            public RandDiv1Attribute() : base(UFix8Min, UFix8Max) { }
        }
        public class RandDiv2Attribute : RandMathAttribute
        {
            public RandDiv2Attribute() : base(1, UFix8Max/2) { }
        }
        public class RandDivOver1Attribute : RandMathAttribute
        {
            public RandDivOver1Attribute() : base(0, UFix8Max/3) { }
        }
        public class RandDivOver2Attribute : RandMathAttribute
        {
            public RandDivOver2Attribute() : base(1 / 256f, 2) { }
        }
        public class RandExpAttribute : RandMathAttribute
        {
            public RandExpAttribute() : base(0, 2839f / 256f) { }
        }
        public class RandZeroNintyAttribute : RandMathAttribute
        {
            public RandZeroNintyAttribute() : base(0, MathF.PI / 2) { }
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
