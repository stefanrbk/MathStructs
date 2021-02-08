using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class FixedPointExtensions
    {
        internal static readonly ImmutableDictionary<string, Func<IConvertible, IFormatProvider?, object>> toTypeMap;

        static FixedPointExtensions()
        {
            Dictionary<string, Func<IConvertible,IFormatProvider?, object>> map = new();
            map.Add(typeof(void).FullName!, (v, p) => throw new ArgumentNullException());
            map.Add(typeof(bool).FullName!, (v, p) => v.ToBoolean(p));
            map.Add(typeof(sbyte).FullName!, (v, p) => v.ToSByte(p));
            map.Add(typeof(byte).FullName!, (v, p) => v.ToByte(p));
            map.Add(typeof(short).FullName!, (v, p) => v.ToInt16(p));
            map.Add(typeof(ushort).FullName!, (v, p) => v.ToUInt16(p));
            map.Add(typeof(int).FullName!, (v, p) => v.ToInt32(p));
            map.Add(typeof(uint).FullName!, (v, p) => v.ToUInt32(p));
            map.Add(typeof(long).FullName!, (v, p) => v.ToInt64(p));
            map.Add(typeof(ulong).FullName!, (v, p) => v.ToUInt64(p));
            map.Add(typeof(float).FullName!, (v, p) => v.ToSingle(p));
            map.Add(typeof(double).FullName!, (v, p) => v.ToDouble(p));
            map.Add(typeof(decimal).FullName!, (v, p) => v.ToDecimal(p));
            map.Add(typeof(string).FullName!, (v, p) => v.ToString(p));
            map.Add(typeof(object).FullName!, (v, p) => v);
            map.Add(typeof(Half).FullName!, (v, p) => v.ToHalf(p));
            map.Add(typeof(Fix16).FullName!, (v, p) => v.ToFix16(p));
            map.Add(typeof(UFix16).FullName!, (v, p) => v.ToUFix16(p));

            toTypeMap = map.ToImmutableDictionary();
        }

        public static Half ToHalf(this IConvertible value, IFormatProvider? provider) =>
            (Half)value.ToDouble(provider);
        public static Fix16 ToFix16(this IConvertible value, IFormatProvider? provider)
        {
            var d = (Fix16)value.ToDouble(provider);
            return d > Fix16.MaxValue ||
                   d < Fix16.MinValue
                       ? throw new OverflowException()
                       : d;
        }
        public static UFix16 ToUFix16(this IConvertible value, IFormatProvider? provider)
        {
            var d = (UFix16)value.ToDouble(provider);
            return d > UFix16.MaxValue ||
                   d < UFix16.MinValue
                       ? throw new OverflowException()
                       : d;
        }
        public static UFix16 ToUFix16(this Half value, IFormatProvider? _) =>
            (double)value > UFix16.MaxValue ||
            (double)value < UFix16.MinValue
                ? throw new OverflowException()
                : (UFix16)value;
        public static Fix16 ToFix16(this Half value, IFormatProvider? _) =>
            (double)value > Fix16.MaxValue ||
            (double)value < Fix16.MinValue
                ? throw new OverflowException()
                : (Fix16)value;
        public static UFix8 ToUFix8(this IConvertible value, IFormatProvider? provider)
        {
            var d = (UFix8)value.ToSingle(provider);
            return d > UFix8.MaxValue ||
                   d < UFix8.MinValue
                       ? throw new OverflowException()
                       : d;
        }
        public static UFix8 ToUFix8(this Half value, IFormatProvider? _) =>
            (float)value > UFix8.MaxValue ||
            (float)value < UFix8.MinValue
                ? throw new OverflowException()
                : (UFix8)value;
    }
}
