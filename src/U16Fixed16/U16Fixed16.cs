namespace System
{
    public readonly struct U16Fixed16 : IComparable, IComparable<U16Fixed16>, IConvertible, IEquatable<U16Fixed16>, IFormattable
    {
        public static U16Fixed16 Zero => new U16Fixed16(0);

        private readonly uint _value;

        private U16Fixed16(uint value) =>
            _value = value;

        public static U16Fixed16 operator +(U16Fixed16 left, U16Fixed16 right) =>
            new U16Fixed16(left._value + right._value);
        public static U16Fixed16 operator -(U16Fixed16 left, U16Fixed16 right) =>
            new U16Fixed16(left._value - right._value);
        public static U16Fixed16 operator *(U16Fixed16 left, U16Fixed16 right) =>
            new U16Fixed16(left._value * right._value);
        public static U16Fixed16 operator /(U16Fixed16 left, U16Fixed16 right) =>
            new U16Fixed16(left._value / right._value);
        public static bool operator ==(U16Fixed16 left, U16Fixed16 right) =>
            left._value == right._value;
        public static bool operator !=(U16Fixed16 left, U16Fixed16 right) =>
            left._value != right._value;
        public static bool operator <(U16Fixed16 left, U16Fixed16 right) =>
            left._value < right._value;
        public static bool operator >(U16Fixed16 left, U16Fixed16 right) =>
            left._value > right._value;
        public static bool operator <=(U16Fixed16 left, U16Fixed16 right) =>
            left._value <= right._value;
        public static bool operator >=(U16Fixed16 left, U16Fixed16 right) =>
            left._value >= right._value;

        public static implicit operator U16Fixed16(byte value) =>
            new U16Fixed16(value * 65536u);

        public static explicit operator byte(U16Fixed16 value) =>
            (byte)( value._value / 65536u );

        public static implicit operator U16Fixed16(sbyte value) =>
            new U16Fixed16((uint)value * 65536u);

        public static explicit operator sbyte(U16Fixed16 value) =>
            (sbyte)( value._value / 65536u );

        public static implicit operator U16Fixed16(short value) =>
            new U16Fixed16((uint)value * 65536u);

        public static explicit operator short(U16Fixed16 value) =>
            (short)( value._value / 65536u );

        public static explicit operator U16Fixed16(ushort value) =>
            new U16Fixed16(value * 65536u);

        public static explicit operator ushort(U16Fixed16 value) =>
            (ushort)( value._value / 65536u );

        public static explicit operator U16Fixed16(int value) =>
            new U16Fixed16((uint)value * 65536u);

        public static explicit operator int(U16Fixed16 value) =>
            (int)value._value / 65536;

        public static explicit operator U16Fixed16(uint value) =>
            new U16Fixed16(value * 65536u);

        public static explicit operator uint(U16Fixed16 value) =>
            value._value / 65536;

        public static explicit operator U16Fixed16(long value) =>
            new U16Fixed16((uint)( value * 65536u ));

        public static explicit operator long(U16Fixed16 value) =>
            value._value / 65536;

        public static explicit operator U16Fixed16(ulong value) =>
            new U16Fixed16((uint)( value * 65536u ));

        public static explicit operator ulong(U16Fixed16 value) =>
            (ulong)value._value / 65536;

        public static explicit operator U16Fixed16(Half value) =>
            new U16Fixed16((uint)( (float)value * 65536 ));

        public static explicit operator Half(U16Fixed16 value) =>
            (Half)( value._value / 65536 );

        public static explicit operator U16Fixed16(float value) =>
            new U16Fixed16((uint)( value * 65536u ));

        public static explicit operator float(U16Fixed16 value) =>
            (float)value._value / 65536;

        public static explicit operator U16Fixed16(double value) =>
            new U16Fixed16((uint)( value * 65536u ));

        public static explicit operator double(U16Fixed16 value) =>
            (double)value._value / 65536;

        public static explicit operator U16Fixed16(decimal value) =>
            new U16Fixed16((uint)( value * 65536u ));

        public static explicit operator decimal(U16Fixed16 value) =>
            (decimal)value._value / 65536;

        public override string ToString() =>
            ( _value/65536.0 ).ToString();

        public override bool Equals(object? obj) =>
            obj is U16Fixed16 f && this == f;

        public override int GetHashCode() =>
            HashCode.Combine(_value, -1);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value/65536.0 ).ToString(format, formatProvider);

        public int CompareTo(object? obj) =>
            obj is U16Fixed16 i
                ? CompareTo(i)
                : obj is null
                    ? 1
                    : throw new ArgumentException("value was not a U16Fixed16", nameof(obj));

        public int CompareTo(U16Fixed16 other) =>
            _value.CompareTo(other._value);

        public TypeCode GetTypeCode() =>
            (TypeCode)101;

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
            if (conversionType == typeof(U16Fixed16))
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

        public bool Equals(U16Fixed16 other) =>
            this == other;
    }
}
