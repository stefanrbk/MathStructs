namespace System
{
    public readonly struct U8Fixed8 : IComparable, IComparable<U8Fixed8>, IConvertible, IEquatable<U8Fixed8>, IFormattable
    {
        public static U8Fixed8 Zero => new U8Fixed8(0);

        private readonly ushort _value;

        private U8Fixed8(ushort value) =>
            _value = value;

        public static U8Fixed8 operator +(U8Fixed8 left, U8Fixed8 right) =>
            new U8Fixed8((ushort)(left._value + right._value));
        public static U8Fixed8 operator -(U8Fixed8 left, U8Fixed8 right) =>
            new U8Fixed8((ushort)(left._value - right._value));
        public static U8Fixed8 operator *(U8Fixed8 left, U8Fixed8 right) =>
            new U8Fixed8((ushort)(left._value * right._value));
        public static U8Fixed8 operator /(U8Fixed8 left, U8Fixed8 right) =>
            new U8Fixed8((ushort)(left._value / right._value));
        public static bool operator ==(U8Fixed8 left, U8Fixed8 right) =>
            left._value == right._value;
        public static bool operator !=(U8Fixed8 left, U8Fixed8 right) =>
            left._value != right._value;
        public static bool operator <(U8Fixed8 left, U8Fixed8 right) =>
            left._value < right._value;
        public static bool operator >(U8Fixed8 left, U8Fixed8 right) =>
            left._value > right._value;
        public static bool operator <=(U8Fixed8 left, U8Fixed8 right) =>
            left._value <= right._value;
        public static bool operator >=(U8Fixed8 left, U8Fixed8 right) =>
            left._value >= right._value;

        public static implicit operator U8Fixed8(byte value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator byte(U8Fixed8 value) =>
            (byte)( value._value / 256u );

        public static implicit operator U8Fixed8(sbyte value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator sbyte(U8Fixed8 value) =>
            (sbyte)( value._value / 256u );

        public static implicit operator U8Fixed8(short value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator short(U8Fixed8 value) =>
            (short)( value._value / 256u );

        public static explicit operator U8Fixed8(ushort value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator ushort(U8Fixed8 value) =>
            (ushort)( value._value / 256u );

        public static explicit operator U8Fixed8(int value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator int(U8Fixed8 value) =>
            (int)value._value / 256;

        public static explicit operator U8Fixed8(uint value) =>
            new U8Fixed8((ushort)(value * 256u));

        public static explicit operator uint(U8Fixed8 value) =>
            value._value / 256u;

        public static explicit operator U8Fixed8(long value) =>
            new U8Fixed8((ushort)( value * 256u ));

        public static explicit operator long(U8Fixed8 value) =>
            value._value / 256;

        public static explicit operator U8Fixed8(ulong value) =>
            new U8Fixed8((ushort)( value * 256u ));

        public static explicit operator ulong(U8Fixed8 value) =>
            (ulong)value._value / 256;

        public static explicit operator U8Fixed8(Half value) =>
            new U8Fixed8((ushort)( (float)value * 65536 ));

        public static explicit operator Half(U8Fixed8 value) =>
            (Half)( value._value / 65536 );

        public static explicit operator U8Fixed8(float value) =>
            new U8Fixed8((ushort)( value * 256u ));

        public static explicit operator float(U8Fixed8 value) =>
            (float)value._value / 256;

        public static explicit operator U8Fixed8(double value) =>
            new U8Fixed8((ushort)( value * 256u ));

        public static explicit operator double(U8Fixed8 value) =>
            (double)value._value / 256;

        public static explicit operator U8Fixed8(decimal value) =>
            new U8Fixed8((ushort)( value * 256u ));

        public static explicit operator decimal(U8Fixed8 value) =>
            (decimal)value._value / 256;

        public override string ToString() =>
            ( _value/256.0 ).ToString();

        public override bool Equals(object? obj) =>
            obj is U8Fixed8 f && this == f;

        public override int GetHashCode() =>
            HashCode.Combine(_value, -1);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value/65536.0 ).ToString(format, formatProvider);

        public int CompareTo(object? obj) =>
            obj is U8Fixed8 i
                ? CompareTo(i)
                : obj is null
                    ? 1
                    : throw new ArgumentException("value was not a U8Fixed8", nameof(obj));

        public int CompareTo(U8Fixed8 other) =>
            _value.CompareTo(other._value);

        public TypeCode GetTypeCode() =>
            (TypeCode)102;

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
            if (conversionType == typeof(U8Fixed8))
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

        public bool Equals(U8Fixed8 other) =>
            this == other;
    }
}
