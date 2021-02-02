using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
    internal delegate Fix16 OverflowFunc(Fix16 left, Fix16 right, Fix16 opResult);

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    [DebuggerDisplay("{Debug}")]
    public unsafe struct Fix16 : IComparable, IComparable<Fix16>, IConvertible, IEquatable<Fix16>, IFormattable
    {
        [FieldOffset(0)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _value;

        #region Trig Cache

        private static readonly Fix16[] expCacheIndex = new Fix16[4096];
        private static readonly Fix16[] expCacheValue = new Fix16[4096];
        private static readonly Fix16[] sinCacheIndex = new Fix16[4096];
        private static readonly Fix16[] sinCacheValue = new Fix16[4096];
        private static readonly (Fix16 x, Fix16 y)[] atanCacheIndex = new (Fix16,Fix16)[4096];
        private static readonly Fix16[] atanCacheValue = new Fix16[4096];

        #endregion Trig Cache

        #region Constants

        public TypeCode GetTypeCode() => (TypeCode)100;

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

        #endregion Constants

        #region Constructors

        public Fix16(short value) =>
            _value = value << 16;

        public Fix16(double value) =>
            _value = (int)( ( value * 65536.0 ) + ( value < 0 ? -0.5 : 0.5 ) );

        public Fix16(decimal value) =>
            _value = (int)( ( value * 65536m ) + ( value < 0 ? -0.5m : 0.5m ) );

        #endregion Constructors

        #region Static Math Functions

        #region Overflow Checked Math Functions

        public static Fix16 CheckedAdd(Fix16 left, Fix16 right)
        {
            var value = Add(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedSubtract(Fix16 left, Fix16 right)
        {
            var value = Subtract(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedMultiply(Fix16 left, Fix16 right)
        {
            var value = Multiply(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static Fix16 CheckedDivide(Fix16 left, Fix16 right)
        {
            var value = Divide(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        #endregion

        #region Min/Max Bounded Math Functions

        public static Fix16 SaturatedAdd(Fix16 left, Fix16 right)
        {
            var value = Add(left, right, out var overflow);

            return overflow
                ? left >= 0
                    ? MaxValue
                    : MinValue
                : value;
        }

        public static Fix16 SaturatedSubtract(Fix16 left, Fix16 right)
        {
            var value = Subtract(left, right, out var overflow);

            return overflow
                ? left >= 0
                    ? MaxValue
                    : MinValue
                : value;
        }

        public static Fix16 SaturatedMultiply(Fix16 left, Fix16 right)
        {
            var value = Multiply(left, right, out var overflow);

            return overflow
                ? ( left >= 0 ) == ( right >= 0 )
                    ? MaxValue
                    : MinValue
                : value;
        }

        public static Fix16 SaturatedDivide(Fix16 left, Fix16 right)
        {
            var value = Divide(left, right, out var overflow);

            return overflow
                ? ( left >= 0 ) == ( right >= 0 )
                    ? MaxValue
                    : MinValue
                : value;
        }

        #endregion Min/Max Bounded Math Functions

        #region Overflow Unchecked Math Functions

        public static Fix16 Add(Fix16 left, Fix16 right) =>
            Add(left, right, out var _);

        public static Fix16 Subtract(Fix16 left, Fix16 right) =>
            Subtract(left, right, out var _);

        public static Fix16 Multiply(Fix16 left, Fix16 right) =>
            Multiply(left, right, out var _);

        public static Fix16 Divide(Fix16 left, Fix16 right) =>
            Divide(left, right, out var _);

        public static Fix16? NullableDivideBy(Fix16? left, Fix16? right) =>
            left is Fix16 fLeft
                ? right is Fix16 fix
                    ? fix != Zero
                        ? Divide(fLeft, fix)
                        : null
                    : null
                : null;

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

            var outValue = (Fix16)Math.Exp((double)value);

            expCacheIndex[tempIndex] = value;
            expCacheValue[tempIndex] = outValue;

            return outValue;
        }

        public static Fix16 Sqrt(Fix16 value)
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

        #endregion Overflow Unchecked Math Functions

        #region Trig Functions

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

        #endregion Trig Functions

        #region Private Math Functions

        private static Fix16 Add(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(int*)&left;
            var r = *(int*)&right;
            var result = l+r;

            if (( ( ~( l^r ) )&0x80000000 & ( l^result )&0x80000000 ) is not 0)
                overflow = true;

            return Raw(result);
        }

        private static Fix16 Subtract(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(int*)&left;
            var r = *(int*)&right;
            var result = l-r;

            if (( ( l^r )&0x80000000 & ( l^result )&0x80000000 ) is not 0)
                overflow = true;

            return Raw(result);
        }

        private static Fix16 Multiply(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;
            var product = (long)*(int*)&left * *(int*)&right;

            // The upper 17 bits should all be the same (the sign).
            var upper = product >> 47;

            if (product < 0)
            {
                if (( ~upper ) is not 0)
                    overflow = true;

                product--;
            }
            else if (upper is not 0)
                overflow = true;

            var result = (int)(product >> 16);
            result += (int)( product & 0x8000 ) >> 15;

            return *(Fix16*)&result;
        }

        private static Fix16 Divide(Fix16 left, Fix16 right, out bool overflow)
        {
            overflow = false;

            if (right == Zero)
                throw new DivideByZeroException();

            int a = *(int*)&left, b = *(int*)&right;

            var remainder = (uint)((a >= 0) ? a : -a);
            var divider = (uint)((b >= 0) ? b : -b);
            var quotient = 0u;
            var bitPos = 17;

            while (( divider & 0xF ) is 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder is not 0 && bitPos >= 0)
            {
                // Shift remainder as much as we can without overflowing
                var shift = (int)Clz(remainder);
                if (shift > bitPos) shift = bitPos;
                remainder <<= shift;
                bitPos -= shift;

                var div = remainder / divider;
                remainder %= divider;
                quotient += div << bitPos;

                if (( div & ~( 0xFFFFFFFF >> bitPos ) ) is not 0)
                    overflow = true;

                remainder <<= 1;
                bitPos--;
            }

            quotient++;

            var result = quotient >> 1;

            if (( ( a^b )&0x80000000 ) is not 0)
                result = (uint)-(int)result;

            return *(Fix16*)&result;
        }

        private static byte Clz(uint x)
        {
            byte result = 255;
            var arch = RuntimeInformation.ProcessArchitecture;
            if (arch is Architecture.X86 or Architecture.X64 && Lzcnt.IsSupported)
                result = (byte)Lzcnt.LeadingZeroCount(x);
            else
                if (arch is Architecture.Arm or Architecture.Arm64 && ArmBase.IsSupported)
                result = (byte)ArmBase.LeadingZeroCount(x);
#if DEBUG
            var simd = result;
#else
            else
#endif
            {
                result = 0;
                if (x == 0) return 32;
                while (( x & 0xF0000000 ) is 0) { result += 4; x <<= 4; }
                while (( x & 0x80000000 ) is 0) { result += 1; x <<=1; }
            }
#if DEBUG
            if (simd is not 255 && simd != result)
                throw new Exception($"SIMD result: \"{simd}\" does not match non-SIMD result: \"{result}\"");
#endif
            return result;
        }

        #endregion Private Math Functions

        #endregion Static Math Functions

        #region Operators

        #region Math Operators

        public static Fix16 operator -(Fix16 value) =>
            Raw(-*(int*)&value);
        public static Fix16? operator -(Fix16? value) =>
            value is null
                ? null
                : Raw(-value.Value._value);
        public static Fix16 operator +(Fix16 left, Fix16 right) =>
            Add(left, right);
        public static Fix16? operator +(Fix16? left, Fix16? right) =>
            ( left is null || right is null )
                ? null
                : Add(left.Value, right.Value);
        public static Fix16 operator -(Fix16 left, Fix16 right) =>
            Subtract(left, right);
        public static Fix16? operator -(Fix16? left, Fix16? right) =>
            ( left is null || right is null )
                ? null
                : Subtract(left.Value, right.Value);
        public static Fix16 operator *(Fix16 left, Fix16 right) =>
            Multiply(left, right);
        public static Fix16? operator *(Fix16? left, Fix16? right) =>
            ( left is null || right is null )
                ? null
                : Multiply(left.Value, right.Value);
        public static Fix16 operator /(Fix16 left, Fix16 right) =>
            Divide(left, right);
        public static Fix16? operator /(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue ? left.Value / right.Value : null;

        #endregion Math Operators

        #region Conditional Operators

        public static bool operator ==(Fix16 left, Fix16 right) =>
            *(int*)&left == *(int*)&right;
        public static bool operator ==(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value == right.Value._value;
        public static bool operator !=(Fix16 left, Fix16 right) =>
            *(int*)&left != *(int*)&right;
        public static bool operator !=(Fix16? left, Fix16? right) =>
            !left.HasValue || !right.HasValue || left.Value._value != right.Value._value;
        public static bool operator >=(Fix16 left, Fix16 right) =>
            *(int*)&left >= *(int*)&right;
        public static bool operator >=(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value >= right.Value._value;
        public static bool operator <=(Fix16 left, Fix16 right) =>
            *(int*)&left <= *(int*)&right;
        public static bool operator <=(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value <= right.Value._value;
        public static bool operator >(Fix16 left, Fix16 right) =>
            *(int*)&left > *(int*)&right;
        public static bool operator >(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value > right.Value._value;
        public static bool operator <(Fix16 left, Fix16 right) =>
            *(int*)&left < *(int*)&right;
        public static bool operator <(Fix16? left, Fix16? right) =>
            left.HasValue && right.HasValue && left.Value._value < right.Value._value;

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From Fix16 Operators

        public static implicit operator double(Fix16 value) =>
            *(int*)&value / 65536.0;

        #endregion Implicit From Fix16 Operators

        #region Explicit From Fix16 Operators

        public static explicit operator byte(Fix16 value) =>
            (byte)( *(int*)&value >> 16 );
        public static explicit operator sbyte(Fix16 value) =>
            (sbyte)( *(int*)&value >> 16 );
        public static explicit operator short(Fix16 value) =>
            (short)( *(int*)&value >> 16 );
        public static explicit operator ushort(Fix16 value) =>
            (ushort)( *(int*)&value >> 16 );
        public static explicit operator int(Fix16 value) =>
            *(int*)&value >> 16;
        public static explicit operator uint(Fix16 value) =>
            (ushort)value;
        public static explicit operator long(Fix16 value) =>
            *(int*)&value >> 16;
        public static explicit operator ulong(Fix16 value) =>
            (ushort)value;
        public static explicit operator decimal(Fix16 value) =>
            *(int*)&value / 65536.0m;
        public static explicit operator float(Fix16 value) =>
            (float)( *(int*)&value / 65536.0 );
        public static explicit operator Half(Fix16 value) =>
            (Half)( *(int*)&value / 65536.0 );

        #endregion Explicit From Fix16 Operators

        #region Implicit To Fix16 Operators

        public static implicit operator Fix16(byte value) =>
            new(value);
        public static implicit operator Fix16(short value) =>
            new(value);

        #endregion Implicit To Fix16 Operators

        #region Explicit To Fix16 Operators

        public static explicit operator Fix16(ushort value) =>
            new((short)value);
        public static explicit operator Fix16(int value) =>
            new((short)value);
        public static explicit operator Fix16(uint value) =>
            new((short)value);
        public static explicit operator Fix16(long value) =>
            new((short)value);
        public static explicit operator Fix16(ulong value) =>
            new((short)value);
        public static explicit operator Fix16(Half value) =>
            new((double)value);
        public static explicit operator Fix16(double value) =>
            new(value);
        public static explicit operator Fix16(decimal value) =>
            new(value);

        #endregion Explicit To Fix16 Operators

        #endregion Cast Operators

        #endregion Operators

        #region Utility Functions

        #region ToString

        public override string ToString() =>
            ToString(null, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value / 65536.0 ).ToString(format, formatProvider);

        #endregion ToString

        #region Equals

        public override bool Equals(object? obj) =>
            obj is Fix16 f && f == this;

        public bool Equals([AllowNull] Fix16 other) =>
            this == other;

        public static bool Equals(Fix16 left, Fix16 right, Fix16 delta)
        {
            if (delta == Zero)
                return left == right;
            else
                return Abs(left - right) <= delta;
        }

        #endregion Equals

        #region GetHashCode

        public override int GetHashCode() =>
            HashCode.Combine(_value, 2);

        #endregion GetHashCode

        #region CompareTo

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            if (obj is not Fix16)
                throw new ArgumentException("Argument must be a Fix16", nameof(obj));
            return ( this - (Fix16)obj )._value;
        }

        public int CompareTo([AllowNull] Fix16 other) =>
            ( this - other )._value;

        #endregion CompareTo

        #region Raw

        public static Fix16 Raw(int value) =>
            new() { _value = value };

        #endregion Raw

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double Debug
        {
            get => this;
            set => this = (Fix16)value;
        }

        #endregion Utility Functions

        #region Explicit IConvertible Implementation

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

        public object ToType(Type conversionType, IFormatProvider? provider) =>
            FixedPointExtensions.toTypeMap.TryGetValue(conversionType?.FullName ?? typeof(void).FullName!, out var func)
                ? func(this, provider)
                : throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType!.FullName);

        public ushort ToUInt16(IFormatProvider? provider) =>
            (ushort)( _value >> 16 );

        public uint ToUInt32(IFormatProvider? provider) =>
            (uint)( _value >> 16 );

        public ulong ToUInt64(IFormatProvider? provider) =>
            (ulong)( _value >> 16 );
        public char ToChar(IFormatProvider? provider) =>
            throw new InvalidCastException();
        public DateTime ToDateTime(IFormatProvider? provider) =>
            throw new InvalidCastException();

        #endregion Explicit IConvertible Implementation

    }
}
