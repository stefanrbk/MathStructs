using System.Diagnostics.CodeAnalysis;

namespace System
{
    public readonly struct S15Fixed16 : IComparable, IComparable<S15Fixed16>, IConvertible, IEquatable<S15Fixed16>, IFormattable
    {
        public static S15Fixed16 Zero => new S15Fixed16(0);

        private readonly int _value;

        private S15Fixed16(int value) =>
            _value = value;

        public static S15Fixed16 operator -(S15Fixed16 value) =>
            new S15Fixed16(-value._value);
        public static S15Fixed16 operator +(S15Fixed16 left, S15Fixed16 right) =>
            new S15Fixed16(left._value + right._value);
        public static S15Fixed16 operator -(S15Fixed16 left, S15Fixed16 right) =>
            new S15Fixed16(left._value - right._value);
        public static S15Fixed16 operator *(S15Fixed16 left, S15Fixed16 right) =>
            new S15Fixed16(left._value * right._value);
        public static S15Fixed16 operator /(S15Fixed16 left, S15Fixed16 right) =>
            new S15Fixed16(left._value / right._value);
        public static bool operator ==(S15Fixed16 left, S15Fixed16 right) =>
            left._value == right._value;
        public static bool operator !=(S15Fixed16 left, S15Fixed16 right) =>
            left._value != right._value;
        public static bool operator <(S15Fixed16 left, S15Fixed16 right) =>
            left._value < right._value;
        public static bool operator >(S15Fixed16 left, S15Fixed16 right) =>
            left._value > right._value;
        public static bool operator <=(S15Fixed16 left, S15Fixed16 right) =>
            left._value <= right._value;
        public static bool operator >=(S15Fixed16 left, S15Fixed16 right) =>
            left._value >= right._value;

        public static implicit operator S15Fixed16(byte value) =>
            new S15Fixed16(value * 65536);

        public static explicit operator byte(S15Fixed16 value) =>
            (byte)(value._value / 65536);

        public static implicit operator S15Fixed16(sbyte value) =>
            new S15Fixed16(value * 65536);

        public static explicit operator sbyte(S15Fixed16 value) =>
            (sbyte)( value._value / 65536 );

        public static implicit operator S15Fixed16(short value) =>
            new S15Fixed16(value * 65536);

        public static explicit operator short(S15Fixed16 value) =>
            (short)( value._value / 65536 );

        public static explicit operator S15Fixed16(ushort value) =>
            new S15Fixed16(value * 65536);

        public static explicit operator ushort(S15Fixed16 value) =>
            (ushort)( value._value / 65536 );

        public static explicit operator S15Fixed16(int value) =>
            new S15Fixed16(value * 65536);

        public static explicit operator int(S15Fixed16 value) =>
            value._value / 65536;

        public static explicit operator S15Fixed16(uint value) =>
            new S15Fixed16((int)(value * 65536));

        public static explicit operator uint(S15Fixed16 value) =>
            (uint)value._value / 65536;

        public static explicit operator S15Fixed16(long value) =>
            new S15Fixed16((int)(value * 65536));

        public static explicit operator long(S15Fixed16 value) =>
            value._value / 65536;

        public static explicit operator S15Fixed16(ulong value) =>
            new S15Fixed16((int)(value * 65536));

        public static explicit operator ulong(S15Fixed16 value) =>
            (ulong)value._value / 65536;

        public static explicit operator S15Fixed16(float value) =>
            new S15Fixed16((int)(value * 65536));

        public static explicit operator float(S15Fixed16 value) =>
            (float)value._value / 65536;

        public static explicit operator S15Fixed16(Half value) =>
            new S15Fixed16((int)( (float)value * 65536 ));

        public static explicit operator Half(S15Fixed16 value) =>
            (Half)(value._value / 65536);

        public static explicit operator S15Fixed16(double value) =>
            new S15Fixed16((int)(value * 65536));

        public static explicit operator double(S15Fixed16 value) =>
            (double)value._value / 65536;

        public static explicit operator S15Fixed16(decimal value) =>
            new S15Fixed16((int)( value * 65536 ));

        public static explicit operator decimal(S15Fixed16 value) =>
            (decimal)value._value / 65536;

        public override string ToString() =>
            ToString(null, null);

        public override bool Equals(object? obj) =>
            obj is S15Fixed16 f && Equals(f);

        public override int GetHashCode() =>
            HashCode.Combine(_value, -1);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value/65536.0 ).ToString(format, formatProvider);

        public int CompareTo(object? obj) =>
            obj is S15Fixed16 i
                ? CompareTo(i)
                : obj is null 
                    ? 1
                    : throw new ArgumentException("value was not a S15Fixed16", nameof(obj));

        public int CompareTo(S15Fixed16 other) =>
            _value.CompareTo(other._value);

        public TypeCode GetTypeCode() => 
            (TypeCode)100;

        public bool ToBoolean(IFormatProvider? provider) =>
            _value != 0;

        public byte ToByte(IFormatProvider? provider) =>
            (byte)this;

        public char ToChar(IFormatProvider? provider) =>
            throw new InvalidCastException();

        public DateTime ToDateTime(IFormatProvider? provider) =>
            throw new InvalidCastException();

        public decimal ToDecimal(IFormatProvider? provider) =>
            (decimal)this;

        public double ToDouble(IFormatProvider? provider) =>
            (double)this;

        public Half ToHalf(IFormatProvider? provider) =>
            (Half)this;

        public short ToInt16(IFormatProvider? provider) =>
            (short)this;

        public int ToInt32(IFormatProvider? provider) =>
            (int)this;

        public long ToInt64(IFormatProvider? provider) =>
            (long)this;

        public sbyte ToSByte(IFormatProvider? provider) =>
            (sbyte)this;

        public float ToSingle(IFormatProvider? provider) =>
            (float)this;

        public string ToString(IFormatProvider? provider) =>
            ToString(null, provider);

        public object ToType(Type conversionType, IFormatProvider? provider) 
        {
            if (conversionType is null)
                throw new ArgumentNullException(nameof(conversionType));
            if (conversionType == typeof(S15Fixed16))
                return this;
            if (conversionType == typeof(bool))
                return ToBoolean(provider);
            if (conversionType == typeof(char))
                return ToChar(provider);
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
            if (conversionType == typeof(DateTime))
                return ToDateTime(provider);
            if (conversionType == typeof(string))
                return ToString(provider);
            if (conversionType == typeof(object))
                return this;
            throw new InvalidCastException($"Cannot cast from {GetType().FullName} to {conversionType.FullName}");
        }

        public ushort ToUInt16(IFormatProvider? provider) =>
            (ushort)this;

        public uint ToUInt32(IFormatProvider? provider) =>
            (uint)this;

        public ulong ToUInt64(IFormatProvider? provider) =>
            (ulong)this;

        public bool Equals(S15Fixed16 other) =>
            this == other;
    }
}
