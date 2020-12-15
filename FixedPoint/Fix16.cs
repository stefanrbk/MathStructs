using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace System
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    public unsafe struct Fix16 : IComparable, IComparable<Fix16>, IConvertible, IEquatable<Fix16>, IFormattable
    {
        [FieldOffset(0)]
        private int _value;

        private static readonly Fix16[] expCacheIndex = new Fix16[4096];
        private static readonly Fix16[] expCacheValue = new Fix16[4096];
        private static readonly Fix16[] sinCacheIndex = new Fix16[4096];
        private static readonly Fix16[] sinCacheValue = new Fix16[4096];
        private static readonly (Fix16 x, Fix16 y)[] atanCacheIndex = new (Fix16,Fix16)[4096];
        private static readonly Fix16[] atanCacheValue = new Fix16[4096];

        public static Fix16 Epsilon => new() { _value = 1 };

        public static Fix16 MaxValue => new() { _value = Int32.MaxValue };

        public static Fix16 MinValue => new() { _value = Int32.MinValue };

        public static Fix16 Pi => new() { _value = 205887 };

        public static Fix16 E => new() { _value = 178145 };

        public static Fix16 One => new() { _value = 65536 };

        public static Fix16 Zero => new() { _value = 0 };

        public static Fix16 NegOne => new() { _value = -65536 };

        public static Fix16 FourDivPi => new() { _value = 0x145F3 };

        public static Fix16 FourDivPi2 => new() { _value = 106243 };

        public static Fix16 PiDivFour => new() { _value = 0x0000C910 };

        public static Fix16 ThreePiDivFour => new() { _value = 0x00025B30 };

        public Fix16(int value) =>
            _value = value << 16;

        public Fix16(double value) =>
            _value = (int)( value * 65536.0 + ( value < 0 ? -0.5 : 0.5 ) );

        public Fix16(decimal value) =>
            _value = (int)( value * 65536m + ( value < 0 ? -0.5m : 0.5m ) );

        public static explicit operator Fix16(ushort value) =>
            new(value);
        public static explicit operator Fix16(int value) =>
            new(value);
        public static explicit operator Fix16(uint value) =>
            new((int)value);
        public static explicit operator Fix16(long value) =>
            new((int)value);
        public static explicit operator Fix16(ulong value) =>
            new((int)value);
        public static explicit operator Fix16(Half value) =>
            new((double)value);
        public static explicit operator Fix16(double value) =>
            new(value);
        public static explicit operator Fix16(decimal value) =>
            new(value);

        public unsafe static explicit operator byte(Fix16 value) =>
            (byte)( *(int*)&value >> 16 );
        public unsafe static explicit operator sbyte(Fix16 value) =>
            (sbyte)( *(int*)&value >> 16 );
        public unsafe static explicit operator short(Fix16 value) =>
            (short)( *(int*)&value >> 16 );
        public unsafe static explicit operator ushort(Fix16 value) =>
            (ushort)( *(int*)&value >> 16 );
        public unsafe static explicit operator int(Fix16 value) =>
            *(int*)&value >> 16;
        public unsafe static explicit operator uint(Fix16 value) =>
            (ushort)value;
        public unsafe static explicit operator long(Fix16 value) =>
            *(int*)&value >> 16;
        public unsafe static explicit operator ulong(Fix16 value) =>
            (ushort)value;
        public unsafe static explicit operator decimal(Fix16 value) =>
            *(int*)&value / 65536.0m;
        public unsafe static explicit operator double(Fix16 value) =>
            *(int*)&value / 65536.0;
        public unsafe static explicit operator float(Fix16 value) =>
            (float)( *(int*)&value / 65536.0 );
        public unsafe static explicit operator Half(Fix16 value) =>
            (Half)( *(int*)&value / 65536.0 );

        public unsafe static Fix16 operator -(Fix16 value) =>
            Raw(-*(int*)&value);
        public static Fix16? operator -(Fix16? value) =>
            Raw(-value?._value);
        public unsafe static Fix16 operator +(Fix16 left, Fix16 right) =>
            Raw(*(int*)&left + *(int*)&right);
        public static Fix16? operator +(Fix16? left, Fix16 right) =>
            Raw(left?._value + right._value);
        public static Fix16? operator +(Fix16 left, Fix16? right) =>
            Raw(left._value + right?._value);
        public static Fix16? operator +(Fix16? left, Fix16? right) =>
            Raw(left?._value + right?._value);
        public unsafe static Fix16 operator -(Fix16 left, Fix16 right) =>
            Raw(*(int*)&left - *(int*)&right);
        public static Fix16? operator -(Fix16? left, Fix16? right) =>
            Raw(left?._value - right?._value);
        public static Fix16? operator -(Fix16? left, Fix16 right) =>
            Raw(left?._value - right._value);
        public static Fix16? operator -(Fix16 left, Fix16? right) =>
            Raw(left._value - right?._value);
        public static Fix16 operator *(Fix16 left, Fix16 right)
        {
            var product = (long)*(int*)&left * *(int*)&right;

            // The upper 17 bits should all be the same (the sign).
            var upper = product >> 47;

            if (product < 0)
                product--;

            var result = (int)(product >> 16);
            result += (int)( product & 0x8000 ) >> 15;

            return new Fix16() { _value = result };
        }
        public static Fix16? operator *(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue ? left.Value * right.Value : null;
        public static Fix16? operator *(Fix16? left, Fix16 right) =>
            left.HasValue ? left.Value * right : null;
        public static Fix16? operator *(Fix16 left, Fix16? right) =>
            right.HasValue ? left * right.Value : null;
        public static Fix16 operator /(Fix16 left, Fix16 right)
        {
            // This uses the basic binary restoring division algorithm.
            // It appears to be faster to do the whole division manually than
            // trying to compose a 64-bit divide out of 32-bit divisions on
            // platforms without hardware divide.

            if (right == Zero)
                throw new DivideByZeroException();

            int a = *(int*)&left, b = *(int*)&right;

            var remainder = a >= 0 ? a : -a;
            var divider = b >= 0 ? b : -b;
            var quotient = 0;
            var bit = 0x10000;

            // The algorithm requires D >= R
            while (divider < remainder)
            {
                divider <<= 1;
                bit <<= 1;
            }

            if (bit == 0)
                return Zero;

            if ((divider & 0x80000000) != 0)
            {
                // Perform one step manually to avoid overflows later.
                // We know that divider's bottom bit is 0 here.
                if (remainder >= divider)
                {
                    quotient |= bit;
                    remainder -= divider;
                }
                divider >>= 1;
                bit >>= 1;
            }

            // Main division loop
            while (bit != 0 && remainder != 0)
            {
                if (remainder >= divider)
                {
                    quotient |= bit;
                    remainder -= divider;
                }

                remainder <<= 1;
                bit >>= 1;
            }

            if (remainder >= divider)
                quotient++;

            var result = Fix16.Raw(quotient);

            // Figure out the sign of result
            if (((a ^ b) & 0x80000000) != 0)
            {
                if (quotient == Int32.MinValue)
                    return Zero;

                result = -result;
            }

            return result;
        }
        public static Fix16? operator /(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue ? left.Value / right.Value : null;
        public static Fix16? operator /(Fix16? left, Fix16 right) =>
            left.HasValue ? left.Value / right : null;
        public static Fix16? operator /(Fix16 left, Fix16? right) =>
            right.HasValue ? left / right.Value : null;
        public static bool operator ==(Fix16 left, Fix16 right) =>
            *(int*)&left == *(int*)&right;
        public static bool operator ==(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value == right.Value._value;
        public static bool operator ==(Fix16? left, Fix16 right) =>
            left.HasValue && left.Value._value == right._value;
        public static bool operator ==(Fix16 left, Fix16? right) =>
            right.HasValue && left._value == right.Value._value;
        public static bool operator !=(Fix16 left, Fix16 right) =>
            *(int*)&left != *(int*)&right;
        public static bool operator !=(Fix16? left, Fix16 right) =>
            !left.HasValue || left.Value._value != right._value;
        public static bool operator !=(Fix16 left, Fix16? right) =>
            !right.HasValue || left._value != right.Value._value;
        public static bool operator !=(Fix16? left, Fix16? right) =>
            !left.HasValue || !right.HasValue || left.Value._value != right.Value._value;
        public static bool operator >=(Fix16 left, Fix16 right) =>
            *(int*)&left >= *(int*)&right;
        public static bool operator >=(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value >= right.Value._value;
        public static bool operator >=(Fix16? left, Fix16 right) =>
            left.HasValue && left.Value._value >= right._value;
        public static bool operator >=(Fix16 left, Fix16? right) =>
            right.HasValue && left._value >= right.Value._value;
        public static bool operator <=(Fix16 left, Fix16 right) =>
            *(int*)&left <= *(int*)&right;
        public static bool operator <=(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value <= right.Value._value;
        public static bool operator <=(Fix16? left, Fix16 right) =>
            left.HasValue && left.Value._value <= right._value;
        public static bool operator <=(Fix16 left, Fix16? right) =>
            right.HasValue && left._value <= right.Value._value;
        public static bool operator >(Fix16 left, Fix16 right) =>
            *(int*)&left > *(int*)&right;
        public static bool operator >(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value > right.Value._value;
        public static bool operator >(Fix16? left, Fix16 right) =>
            left.HasValue && left.Value._value > right._value;
        public static bool operator >(Fix16 left, Fix16? right) =>
            right.HasValue && left._value > right.Value._value;
        public static bool operator <(Fix16 left, Fix16 right) =>
            *(int*)&left < *(int*)&right;
        public static bool operator <(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value < right.Value._value;
        public static bool operator <(Fix16? left, Fix16 right) =>
            left.HasValue && left.Value._value < right._value;
        public static bool operator <(Fix16 left, Fix16? right) =>
            right.HasValue && left._value < right.Value._value;

        public static Fix16 Lerp(Fix16 left, Fix16 right, byte fraction)
        {
            var tempOut = (long)left._value * ((1 << 8) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 8;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Lerp(Fix16 left, Fix16 right, ushort fraction)
        {
            var tempOut = (long)left._value * ((1 << 16) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 16;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Lerp(Fix16 left, Fix16 right, uint fraction)
        {
            var tempOut = left._value * (0 - fraction);
            tempOut += right._value * fraction;
            tempOut >>= 32;
            var result = (int)tempOut;
            return *(Fix16*)&result;
        }

        public static Fix16 Exp(Fix16 value)
        {
            if (value == Zero)
                return One;
            if (value == One)
                return E;
            if (value > new Fix16() { _value = 681391 })
                return MaxValue;
            if (value < new Fix16() { _value = -772243 })
                return Zero;

            var tempIndex = (*(int*)&value ^ (*(int*)&value >> 4)) & 0x0FFF;
            if (expCacheIndex[tempIndex] == value)
                return expCacheValue[tempIndex];
            // Too inaccurate
            /*
            var one = One;

            var tempOut = (long)*(int*)&one + *(int*)&value;
            long tempValue = *(int*)&value;

            for (int i = 3, n = 2; i < 17; n *= i, i++)
            {
                tempValue *= *(int*)&value;
                tempValue += 32768;
                tempValue >>= 16;
                tempOut += tempValue / n;
            }

            var outValue = new Fix16() { _value = (int)tempOut };
            */

            var outValue = (Fix16)Math.Exp((double)value);

            expCacheIndex[tempIndex] = value;
            expCacheValue[tempIndex] = outValue;

            return outValue;
        }

        public unsafe static Fix16 Sqrt(Fix16 value)
        {
            var neg = *(int*)&value < 0;
            var num = neg ? -*(int*)&value : *(int*)&value;
            var result = 0;
            var bit = 0;
            byte n;

            // Many numbers will be less than 15, so
            // this gives a good balance between time spent
            // in if vs. time spent in the while loop
            // when searching for the starting value.
            bit = ( num & 0xFFF00000 ) != 0 ?
                      1 << 30 :
                      1 << 18;

            while (bit > num) bit >>= 2;

            // The main part is executed twice, in order to avoid
            // using 64 bit values in computations.
            for (n = 0; n < 2; n++)
            {
                // First we get the top 24 bits of the answer.
                while (bit != 0)
                {
                    if (num >= result + bit)
                    {
                        num -= result + bit;
                        result = ( result >> 1 ) + bit;
                    }
                    else
                        result >>= 1;
                    bit >>=2;
                }

                if (n == 0)
                {
                    // Then process it again to get the lowest 8 bits.
                    if (num > 65535)
                    {
                        Console.WriteLine(value.ToString());
                        // The remainder 'num' is too large to be shifted left
                        // by 16, so we have to add 1 to result manually and
                        // adjust 'num' accordingly.
                        // num = a - (result + 0.5)^2
                        //   = num + result^2 - (result + 0.5)^2
                        //   = num - result - 0.5
                        num -= result;
                        num = ( num << 16 ) - 0x8000;
                        result = ( result << 16 ) + 0x8000;
                    }
                    else
                    {
                        num <<= 16;
                        result <<= 16;
                    }

                    bit = 1 << 14;
                }
            }

            // Finally, if next bit would have been 1, round the result upwards.
            if (num > result)
                result++;

            return new Fix16() { _value = neg ? -result : result };
        }

        public static Fix16 Abs(Fix16 value)
        {
            var mask = *(int*)&value >> 31;
            return new Fix16() { _value = ( *(int*)&value + mask ) ^ mask };
        }

        public static Fix16 Raw(int value) =>
            new() { _value = value };
        public static Fix16? Raw(int? value) =>
            value is int v ? Raw(v) : null;

        public static Fix16 Sin(Fix16 angle)
        {
            var tempAngle = Raw(angle._value % ( Pi._value << 1 ));

            if (tempAngle > Pi)
                tempAngle -= Raw(Pi._value << 1);
            else if (tempAngle < -Pi)
                tempAngle += Raw(Pi._value << 1);

            var tempIndex = ( angle._value >> 5 ) & 0xFFF;
            if (sinCacheIndex[tempIndex] == angle)
                return sinCacheValue[tempIndex];

            var tempAngleSq = tempAngle * tempAngle;
            var tempOut = tempAngle._value;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 6;
            tempAngle *= tempAngleSq;
            tempOut += tempAngle._value / 120;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 5040;
            tempAngle *= tempAngleSq;
            tempOut += tempAngle._value / 362880;
            tempAngle *= tempAngleSq;
            tempOut -= tempAngle._value / 39916800;

            sinCacheIndex[tempIndex] = angle;
            sinCacheValue[tempIndex] = Raw(tempOut);

            return Raw(tempOut);
        }

        public static Fix16 Cos(Fix16 angle) =>
            Sin(angle + Raw(Pi._value >> 1));

        public static Fix16? Tan(Fix16 angle)
        {
            var cos = Cos(angle);
            var halfPi = Pi / (Fix16)2;
            return cos == Zero || angle == halfPi ? null : Sin(angle) / cos;
        }

        public static Fix16? Asin(Fix16 value)
        {
            if (( value > One ) || ( value < NegOne ))
                return null;
            if (value == One)
                return Pi / (Fix16)2;

            var tempOut = One - (value * value);
            tempOut = value / Sqrt(tempOut);
            tempOut = Atan(tempOut);
            return tempOut;
        }

        public static Fix16? Acos(Fix16 value) =>
            Raw(Pi._value >> 1) - Asin(value);

        public static Fix16 Atan2(Fix16 y, Fix16 x)
        {
            Fix16 r, r3, angle;

            var hash = x._value ^ y._value;
            hash ^= hash >> 20;
            hash &= 0xFFF;
            if (atanCacheIndex[hash] == (x, y))
                return atanCacheValue[hash];

            var absY = Abs(y);

            if (x >= Zero)
            {
                r = ( x - absY ) / ( x + absY );
                r3 = r * r * r;
                angle = ( Raw(0x3240) * r3 ) - ( Raw(0xFB50) * r ) + PiDivFour;
            }
            else
            {
                r = ( x + absY ) / ( absY - x );
                r3 = r * r * r;
                angle = ( Raw(0x3240) * r3 ) - ( Raw(0xFB50) * r ) + ThreePiDivFour;
            }
            if (y < Zero)
                angle = -angle;

            atanCacheIndex[hash] = (x, y);
            atanCacheValue[hash] = angle;

            return angle;
        }

        public static Fix16 Atan(Fix16 value) =>
            Atan2(value, One);

        public override string ToString() =>
            ToString(null, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value / 65536.0 ).ToString(format, formatProvider);

        public bool ToBoolean(IFormatProvider? provider) =>
            _value != 0;

        public byte ToByte(IFormatProvider? provider) =>
            (byte)( _value >> 16 );

        public decimal ToDecimal(IFormatProvider? provider) =>
            (decimal)( _value / 65536.0 );

        public double ToDouble(IFormatProvider? provider) =>
            _value / 65536.0;

        public Half ToHalf(IFormatProvider? _) =>
            (Half)( _value / 65536.0 );

        public short ToInt16(IFormatProvider? provider) =>
            (short)( _value >> 16 );

        public int ToInt32(IFormatProvider? provider) =>
            _value >> 16;

        public long ToInt64(IFormatProvider? provider) =>
            _value >> 16;

        public sbyte ToSByte(IFormatProvider? provider) =>
            (sbyte)( _value >> 16 );

        public float ToSingle(IFormatProvider? provider) =>
            (float)( _value / 65536.0 );

        public string ToString(IFormatProvider? provider) =>
            ToString(null, provider);

        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            if ((object)conversionType == null!)
                throw new ArgumentNullException(nameof(conversionType));
            if (conversionType == typeof(bool))
                return ToBoolean(provider);
            if (conversionType == typeof(sbyte))
                return ToSByte(provider);
            if (conversionType == typeof(byte))
                return ToByte(provider);
            if (conversionType == typeof(short))
                return ToInt16(provider);
            if (conversionType == typeof(ushort))
                return ToUInt16(provider);
            if (conversionType == typeof(int))
                return ToInt32(provider);
            if (conversionType == typeof(uint))
                return ToUInt32(provider);
            if (conversionType == typeof(long))
                return ToInt64(provider);
            if (conversionType == typeof(ulong))
                return ToUInt64(provider);
            if (conversionType == typeof(Half))
                return ToHalf(provider);
            if (conversionType == typeof(float))
                return ToSingle(provider);
            if (conversionType == typeof(double))
                return ToDouble(provider);
            if (conversionType == typeof(decimal))
                return ToDecimal(provider);
            if (conversionType == typeof(string))
                return ToString(provider);
            if (conversionType == typeof(object))
                return this;
            throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType.FullName);
        }

        public ushort ToUInt16(IFormatProvider? provider) =>
            (ushort)( _value >> 16 );

        public uint ToUInt32(IFormatProvider? provider) =>
            (uint)( _value >> 16 );

        public ulong ToUInt64(IFormatProvider? provider) =>
            (ulong)( _value >> 16 );
        public TypeCode GetTypeCode() => (TypeCode)100;
        public char ToChar(IFormatProvider? provider) => throw new InvalidCastException();
        public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException();

        public override bool Equals(object? obj) =>
            obj is Fix16 f && f == this;

        public override int GetHashCode() =>
            HashCode.Combine(_value, 2);
        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            if (obj is not Fix16)
                throw new ArgumentException("Argument must be a Fix16", nameof(obj));
            return (this - (Fix16)obj)._value;
        }

        public int CompareTo([AllowNull] Fix16 other) =>
            ( this - other )._value;
        public bool Equals([AllowNull] Fix16 other) =>
            this == other;

        public static bool Equals(Fix16 left, Fix16 right, Fix16 delta)
        {
            if (delta == Zero)
                return left == right;
            else
                return Abs(left - right) <= delta;
        }

        public static Fix16? NullableDivideBy(Fix16? left, Fix16? right) =>
            left is Fix16 fLeft
                ? right is Fix16 fix 
                    ? fix != Zero 
                        ? fLeft / fix
                        : null
                    : null
                : null;
    }
}
