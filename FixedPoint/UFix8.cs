namespace System
{
    public static class UFix8
    {
        public const ushort Epsilon = 1;

        public const ushort MaxValue = UInt16.MaxValue;

        public const ushort MinValue = 0;

        public const ushort One = 256;

        public const ushort Zero = 0;

        public static string ToString(ushort value) =>
            ToString(value, null, null);

        public static string ToString(ushort value, string? format, IFormatProvider? formatProvider) =>
            ( value / 256.0 ).ToString(format, formatProvider);

        public static bool ToBoolean(ushort value) =>
            value != 0;

        public static byte ToByte(ushort value) =>
            (byte)( value >> 8 );

        public static decimal ToDecimal(ushort value) =>
            (decimal)( value / 256.0 );

        public static double ToDouble(ushort value) =>
            value / 256.0;

        public static Half ToHalf(ushort value) =>
            (Half)( value / 256.0 );

        public static short ToInt16(ushort value) =>
            (short)( value >> 8 );

        public static int ToInt32(ushort value) =>
            value >> 8;

        public static long ToInt64(ushort value) =>
            value >> 8;

        public static sbyte ToSByte(ushort value) =>
            (sbyte)( value >> 8 );

        public static float ToSingle(ushort value) =>
            (float)( value / 256.0 );

        public static string ToString(ushort value, IFormatProvider? provider) =>
            ToString(value, null, provider);

        public static object ToType(ushort value, Type conversionType, IFormatProvider? provider)
        {
            if ((object)conversionType == null!)
                throw new ArgumentNullException(nameof(conversionType));
            if (conversionType == typeof(int))
                return ToInt32(value);
            if (conversionType == typeof(bool))
                return ToBoolean(value);
            if (conversionType == typeof(sbyte))
                return ToSByte(value);
            if (conversionType == typeof(byte))
                return ToByte(value);
            if (conversionType == typeof(short))
                return ToInt16(value);
            if (conversionType == typeof(ushort))
                return ToUInt16(value);
            if (conversionType == typeof(int))
                return ToInt32(value);
            if (conversionType == typeof(uint))
                return ToUInt32(value);
            if (conversionType == typeof(long))
                return ToInt64(value);
            if (conversionType == typeof(ulong))
                return ToUInt64(value);
            if (conversionType == typeof(Half))
                return ToHalf(value);
            if (conversionType == typeof(float))
                return ToSingle(value);
            if (conversionType == typeof(double))
                return ToDouble(value);
            if (conversionType == typeof(decimal))
                return ToDecimal(value);
            if (conversionType == typeof(string))
                return ToString(value, provider);
            if (conversionType == typeof(object))
                return value;
            throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType.FullName);
        }

        public static ushort ToUInt16(ushort value) =>
            (ushort)( value >> 8 );

        public static uint ToUInt32(ushort value) =>
            (uint)( value >> 8 );

        public static ulong ToUInt64(ushort value) =>
            (uint)( value >> 8 );

        public static ushort FromByte(byte value) =>
            (ushort)( value << 8 );

        public static ushort FromSByte(sbyte value) =>
            (ushort)( value << 8 );

        public static ushort FromInt16(short value) =>
            (ushort)( value << 8 );

        public static ushort FromUInt16(ushort value) =>
            (ushort)( value << 8 );

        public static ushort FromInt32(int value) =>
            (ushort)( value << 8 );

        public static ushort FromUInt32(uint value) =>
            (ushort)( value << 8 );

        public static ushort FromInt64(long value) =>
            (ushort)( value << 8 );

        public static ushort FromUInt64(ulong value) =>
            (ushort)( value << 8 );

        public static ushort FromHalf(Half value) =>
            (ushort)( (double)value * 256.0 );

        public static ushort FromSingle(float value) =>
            (ushort)( value * 256.0 );

        public static ushort FromDouble(double value) =>
            (ushort)( value * 256.0 );

        public static ushort FromDecimal(decimal value) =>
            (ushort)( (double)value * 256.0 );

        public static ushort ToUFix8(this byte value) =>
            FromByte(value);

        public static ushort ToUFix8(this sbyte value) =>
            FromSByte(value);

        public static ushort ToUFix8(this short value) =>
            FromInt16(value);

        public static ushort ToUFix8(this ushort value) =>
            FromUInt16(value);

        public static ushort ToUFix8(this int value) =>
            FromInt32(value);

        public static ushort ToUFix8(this uint value) =>
            FromUInt32(value);

        public static ushort ToUFix8(this long value) =>
            FromInt64(value);

        public static ushort ToUFix8(this ulong value) =>
            FromUInt64(value);

        public static ushort ToUFix8(this Half value) =>
            FromHalf(value);

        public static ushort ToUFix8(this float value) =>
            FromSingle(value);

        public static ushort ToUFix8(this double value) =>
            FromDouble(value);

        public static ushort ToUFix8(this decimal value) =>
            FromDecimal(value);
    }
}
