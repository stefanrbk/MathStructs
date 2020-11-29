namespace System
{
    public static class Fix16
    {
        public const int Epsilon = 1;
        public const int MaxValue = Int32.MaxValue;
        public const int MinValue = Int32.MinValue;

        public const int Pi = 205887;
        public const int E = 179145;

        public const int One = 65536;
        public const int Zero = 0;
        public const int NegOne = -65536;

        public static string ToString(int value) =>
            ToString(value, null, null);

        public static string ToString(int value, string? format, IFormatProvider? formatProvider) =>
            ( value / 65536.0 ).ToString(format, formatProvider);

        public static bool ToBoolean(int value) =>
            value != 0;

        public static byte ToByte(int value) =>
            (byte)(value >> 16);

        public static decimal ToDecimal(int value) =>
            (decimal)( value / 65536.0 );

        public static double ToDouble(int value) =>
            (double)( value / 65536.0 );

        public static Half ToHalf(int value) =>
            (Half)( value / 65536.0 );

        public static short ToInt16(int value) =>
            (short)( value >> 16 );

        public static int ToInt32(int value) =>
            (int)( value >> 16 );

        public static long ToInt64(int value) =>
            (long)( value >> 16 );

        public static sbyte ToSByte(int value) =>
            (sbyte)( value >> 16 );

        public static float ToSingle(int value) =>
            (float)( value / 65536.0 );

        public static string ToString(int value, IFormatProvider? provider) =>
            ToString(value, null, provider);

        public static object ToType(int value, Type conversionType, IFormatProvider? provider) 
        {
            if (conversionType is null)
                throw new ArgumentNullException(nameof(conversionType));
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
            throw new InvalidCastException($"Cannot convert from a Q16 fixed-point number to {conversionType.FullName}");
        }

        public static ushort ToUInt16(int value) =>
            (ushort)( value >> 16 );

        public static uint ToUInt32(int value) =>
            (uint)( value >> 16 );

        public static ulong ToUInt64(int value) =>
            (ulong)( value >> 16 );

        public static int FromByte(byte value) =>
            value << 16;

        public static int FromSByte(sbyte value) =>
            value << 16;

        public static int FromInt16(short value) =>
            value << 16;

        public static int FromUInt16(ushort value) =>
            value << 16;

        public static int FromInt32(int value) =>
            value << 16;

        public static int FromUInt32(uint value) =>
            (int)(value << 16);

        public static int FromInt64(long value) =>
            (int)(value << 16);

        public static int FromUInt64(ulong value) =>
            (int)(value << 16);

        public static int FromHalf(Half value) =>
            (int)( (double)value * 65536 );

        public static int FromSingle(float value) =>
            (int)( (double)value * 65536 );

        public static int FromDouble(double value) =>
            (int)( value * 65536 );

        public static int FromDecimal(decimal value) =>
            (int)( (double)value * 65536 );

        public static int ToFix16(this byte value) =>
            FromByte(value);

        public static int ToFix16(this sbyte value) =>
            FromSByte(value);

        public static int ToFix16(this short value) =>
            FromInt16(value);

        public static int ToFix16(this ushort value) =>
            FromUInt16(value);

        public static int ToFix16(this int value) =>
            FromInt32(value);

        public static int ToFix16(this uint value) =>
            FromUInt32(value);

        public static int ToFix16(this long value) =>
            FromInt64(value);

        public static int ToFix16(this ulong value) =>
            FromUInt64(value);

        public static int ToFix16(this Half value) =>
            FromHalf(value);

        public static int ToFix16(this float value) =>
            FromSingle(value);

        public static int ToFix16(this double value) =>
            FromDouble(value);

        public static int ToFix16(this decimal value) =>
            FromDecimal(value);
    }
}
