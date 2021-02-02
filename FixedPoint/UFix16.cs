using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
    internal delegate UFix16 UFix16OverflowFunc(UFix16 left, UFix16 right, UFix16 opResult);

    [StructLayout(LayoutKind.Explicit, Size = 4)]
    [DebuggerDisplay("{Debug}")]
    public unsafe struct UFix16 : IComparable, IComparable<UFix16>, IConvertible, IEquatable<UFix16>, IFormattable
    {
        [FieldOffset(0)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint _value;

        #region Constants
        public TypeCode GetTypeCode() => (TypeCode)101;

        public static UFix16 Epsilon => new() { _value = 1 };

        public static UFix16 MaxValue => new() { _value = UInt32.MaxValue };

        public static UFix16 MinValue => new() { _value = UInt32.MinValue };

        public static UFix16 Pi => new() { _value = 205887 };

        public static UFix16 E => new() { _value = 178145 };

        public static UFix16 One => new() { _value = 65536 };

        public static UFix16 Zero => new() { _value = 0 };

        public static UFix16 FourDivPi => new() { _value = 0x145F3 };

        public static UFix16 FourDivPi2 => new() { _value = 106243 };

        public static UFix16 PiDivFour => new() { _value = 0x0000C910 };

        public static UFix16 ThreePiDivFour => new() { _value = 0x00025B30 };
        #endregion Constants

        #region Constructors

        public UFix16(ushort value) =>
            _value = (uint)value << 16;

        public UFix16(double value) =>
            _value = (uint)( ( value * 65536.0 ) + 0.5 );

        public UFix16(decimal value) =>
            _value = (uint)(int)( ( value * 65536m ) + ( value < 0 ? -0.5m : 0.5m ) );
        #endregion Constructors

        #region Static Math Functions

        #region Overflow Checked Math Functions
        public static UFix16 CheckedAdd(UFix16 left, UFix16 right)
        {
            var value = Add(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedSubtract(UFix16 left, UFix16 right)
        {
            var value = Subtract(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedMultiply(UFix16 left, UFix16 right)
        {
            var value = Multiply(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }

        public static UFix16 CheckedDivide(UFix16 left, UFix16 right)
        {
            var value = Divide(left, right, out var overflow);
            return overflow ? throw new OverflowException() : value;
        }
        #endregion Overflow Checked Math Functions

        #region Min/Max Bounded Math Functions
        public static UFix16 SaturatedAdd(UFix16 left, UFix16 right)
        {
            var value = Add(left, right, out var overflow);

            return overflow
                ? MaxValue
                : value;
        }

        public static UFix16 SaturatedSubtract(UFix16 left, UFix16 right)
        {
            var value = Subtract(left, right, out var overflow);
            return overflow ? MinValue : value;
        }

        public static UFix16 SaturatedMultiply(UFix16 left, UFix16 right)
        {
            var value = Multiply(left, right, out var overflow);
            return overflow ? MaxValue : value;
        }

        public static UFix16 SaturatedDivide(UFix16 left, UFix16 right)
        {
            var value = Divide(left, right, out var overflow);
            return overflow ? MaxValue : value;
        }
        #endregion Min/Max Bounded Math Functions

        #region Overflow Unchecked Math Functions

        public static UFix16 Add(UFix16 left, UFix16 right) =>
            Add(left, right, out _);

        public static UFix16 Subtract(UFix16 left, UFix16 right) =>
            Subtract(left, right, out _);

        public static UFix16 Multiply(UFix16 left, UFix16 right) =>
            Multiply(left, right, out _);

        public static UFix16 Divide(UFix16 left, UFix16 right) =>
            Divide(left, right, out _);

        public static UFix16? NullableDivide(UFix16? left, UFix16? right) =>
            left is UFix16 fLeft
                ? right is UFix16 fix
                    ? fix != Zero
                        ? Divide(fLeft, fix)
                        : null
                    : null
                : null;

        public static UFix16 Lerp(UFix16 left, UFix16 right, byte fraction)
        {
            var tempOut = left._value * ((1 << 8) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 8;
            var result = (uint)tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Lerp(UFix16 left, UFix16 right, ushort fraction)
        {
            var tempOut = left._value * ((1 << 16) - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 16;
            var result = (uint)tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Lerp(UFix16 left, UFix16 right, uint fraction)
        {
            var tempOut = (long)left._value * (0 - fraction);
            tempOut += (long)right._value * fraction;
            tempOut >>= 32;
            var result = tempOut;
            return *(UFix16*)&result;
        }

        public static UFix16 Sqrt(UFix16 value)
        {
            var num = *(uint*)&value;
            var result = 0u;
            var bit = 0u;
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

            return new UFix16() { _value = result };
        }

        #endregion Overflow Unchecked Math Functions

        #region Private Math Functions

        private static UFix16 Add(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(uint*)&left;
            var r = *(uint*)&right;
            var result = l+r;

            if (result < l || result < r)
                overflow = true;

            return Raw(result);
        }

        private static UFix16 Subtract(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var l = *(uint*)&left;
            var r = *(uint*)&right;
            var result = l-r;

            if (r > l)
                overflow = true;

            return Raw(result);
        }

        private static UFix16 Multiply(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;
            var product = (ulong)*(uint*)&left * *(uint*)&right;

            // The upper 16 bits should all be the same (the sign).
            var upper = product >> 48;

            if (upper is not 0)
                overflow = true;

            var result = (int)(product >> 16);
            var temp = *(UFix16*)&result;
            result += (int)( product & 0x8000 ) >> 15;

            return *(UFix16*)&result;
        }

        private static UFix16 Divide(UFix16 left, UFix16 right, out bool overflow)
        {
            overflow = false;

            if (right == Zero)
                throw new DivideByZeroException();

            uint a = *(uint*)&left, b = *(uint*)&right;

            //var result = ((ulong)a <<16) / ((ulong)b<<16);
            //result >>=16;

            ulong remainder = a;
            ulong divider = b;
            ulong quotient = 0u;
            var bitPos = 17;

            while (( divider & 0xF ) is 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder is not 0 && bitPos >= 0)
            {
                // Shift remainder as much as we can without overflowing
                var shift = (int)Clz((uint)remainder);
                if (shift > bitPos) shift = bitPos;
                remainder <<= shift;
                bitPos -= shift;

                var div = remainder / divider;
                remainder %= divider;
                quotient += div << bitPos;

                var mask = 0x1FFFFFFFFu >> bitPos--;
                if (( div & ~mask ) is not 0)
                    overflow = true;

                remainder <<= 1;
            }

            quotient++;

            var result = quotient >> 1;

            return *(UFix16*)&result;
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

        public static UFix16 operator +(UFix16 left, UFix16 right) =>
            Add(left, right);
        public static UFix16? operator +(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Add(left.Value, right.Value);
        public static UFix16 operator -(UFix16 left, UFix16 right) =>
            Subtract(left, right);
        public static UFix16? operator -(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Subtract(left.Value, right.Value);
        public static UFix16 operator *(UFix16 left, UFix16 right) =>
            Multiply(left, right);
        public static UFix16? operator *(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Multiply(left.Value, right.Value);
        public static UFix16 operator /(UFix16 left, UFix16 right) =>
            Divide(left, right);
        public static UFix16? operator /(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Divide(left.Value, right.Value);

        #endregion Math Operators

        #region Conditional Operators

        public static bool operator ==(UFix16 left, UFix16 right) =>
            *(uint*)&left == *(uint*)&right;
        public static bool operator ==(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value == right.Value._value;
        public static bool operator !=(UFix16 left, UFix16 right) =>
            *(uint*)&left != *(uint*)&right;
        public static bool operator !=(UFix16? left, UFix16? right) =>
            !left.HasValue || !right.HasValue || left.Value._value != right.Value._value;
        public static bool operator >=(UFix16 left, UFix16 right) =>
            *(uint*)&left >= *(uint*)&right;
        public static bool operator >=(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value >= right.Value._value;
        public static bool operator <=(UFix16 left, UFix16 right) =>
            *(uint*)&left <= *(uint*)&right;
        public static bool operator <=(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value <= right.Value._value;
        public static bool operator >(UFix16 left, UFix16 right) =>
            *(uint*)&left > *(uint*)&right;
        public static bool operator >(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value > right.Value._value;
        public static bool operator <(UFix16 left, UFix16 right) =>
            *(uint*)&left < *(uint*)&right;
        public static bool operator <(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value < right.Value._value;

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From UFix16 Operators

        public static implicit operator double(UFix16 value) =>
            *(uint*)&value / 65536.0;

        #endregion Implicit From UFix16 Operators

        #region Explicit From UFix16 Operators

        public static explicit operator byte(UFix16 value) =>
            (byte)( *(uint*)&value >> 16 );
        public static explicit operator sbyte(UFix16 value) =>
            (sbyte)( *(uint*)&value >> 16 );
        public static explicit operator short(UFix16 value) =>
            (short)( *(uint*)&value >> 16 );
        public static explicit operator ushort(UFix16 value) =>
            (ushort)( *(uint*)&value >> 16 );
        public static explicit operator int(UFix16 value) =>
            (int)( *(uint*)&value >> 16 );
        public static explicit operator uint(UFix16 value) =>
            (ushort)value;
        public static explicit operator long(UFix16 value) =>
            *(uint*)&value >> 16;
        public static explicit operator ulong(UFix16 value) =>
            (ushort)value;
        public static explicit operator decimal(UFix16 value) =>
            *(uint*)&value / 65536.0m;
        public static explicit operator float(UFix16 value) =>
            (float)( *(uint*)&value / 65536.0 );
        public static explicit operator Half(UFix16 value) =>
            (Half)( *(uint*)&value / 65536.0 );
        public static explicit operator Fix16(UFix16 value) =>
            *(Fix16*)&value;

        #endregion Explicit From UFix16 Operators

        #region Implicit To UFix16 Operators

        public static implicit operator UFix16(ushort value) =>
            new(value);

        #endregion Implicit To UFix16 Operators

        #region Explicit To UFix16 Operators

        public static explicit operator UFix16(sbyte value) =>
            new((double)(byte)value);
        public static explicit operator UFix16(short value) =>
            new((double)(ushort)value);
        public static explicit operator UFix16(int value) =>
            new((double)(uint)value);
        public static explicit operator UFix16(uint value) =>
            new((ushort)value);
        public static explicit operator UFix16(long value) =>
            new((double)(ulong)value);
        public static explicit operator UFix16(ulong value) =>
            new((ushort)value);
        public static explicit operator UFix16(Half value) =>
            new((double)value);
        public static explicit operator UFix16(float value) =>
            new(value);
        public static explicit operator UFix16(decimal value) =>
            new(value);
        public static explicit operator UFix16(Fix16 value) =>
            new(value);
        public static explicit operator UFix16(double value) =>
            new (value);

        #endregion Explicit To UFix16 Operators

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
            obj is UFix16 f && f == this;

        public bool Equals([AllowNull] UFix16 other) =>
            this == other;

        public static bool Equals(UFix16 left, UFix16 right, UFix16 delta)
        {
            if (delta == Zero)
                return left == right;
            else
                return ( right > left ? right - left : left - right ) <= delta;
        }

        #endregion Equals

        #region GetHashCode

        public override int GetHashCode() =>
            HashCode.Combine(_value, 3);

        #endregion GetHashCode

        #region CompareTo

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            if (obj is not UFix16)
                throw new ArgumentException("Argument must be a UFix16", nameof(obj));
            return (int)unchecked(( this - (UFix16)obj )._value);
        }

        public int CompareTo([AllowNull] UFix16 other) =>
            (int)unchecked(( this - other )._value);

        #endregion CompareTo

        #region Raw

        public static UFix16 Raw(uint value) =>
            new() { _value = value };

        #endregion Raw

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double Debug
        {
            get => this;
            set => this = (UFix16)value;
        }

        #endregion Utility Functions

        #region Explicit IConvertible Implementation

        bool IConvertible.ToBoolean(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToBoolean(null);

        byte IConvertible.ToByte(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToByte(null);

        decimal IConvertible.ToDecimal(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToDecimal(null);

        double IConvertible.ToDouble(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToDouble(null);

        short IConvertible.ToInt16(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToInt16(null);

        int IConvertible.ToInt32(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToInt32(null);

        long IConvertible.ToInt64(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToInt64(null);

        sbyte IConvertible.ToSByte(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToSByte(null);

        float IConvertible.ToSingle(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToSingle(null);

        string IConvertible.ToString(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToString(null);

        object IConvertible.ToType(Type conversionType, IFormatProvider? provider) =>
            FixedPointExtensions.toTypeMap.TryGetValue(conversionType?.FullName ?? typeof(void).FullName!, out var func)
                ? func(this, provider)
                : throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType!.FullName);

        ushort IConvertible.ToUInt16(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToUInt16(null);

        uint IConvertible.ToUInt32(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToUInt32(null);

        ulong IConvertible.ToUInt64(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToUInt64(null);
        char IConvertible.ToChar(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToChar(null);
        DateTime IConvertible.ToDateTime(IFormatProvider? provider) =>
            ( (IConvertible)(double)this ).ToDateTime(null);

        #endregion Explicit IConvertible Implementation

    }
}