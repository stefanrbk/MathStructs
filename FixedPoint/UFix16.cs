//namespace System
//{
//    public static class UFix16
//    {
//        public const uint Epsilon = 1u;

//        public const uint MaxValue = UInt32.MaxValue;

//        public const uint MinValue = 0u;

//        public const uint Pi = 205887u;

//        public const uint E = 179145u;

//        public const uint One = 65536u;

//        public const uint Zero = 0u;

//        public static string ToString(uint value) =>
//            ToString(value, null, null);

//        public static string ToString(uint value, string? format, IFormatProvider? formatProvider) =>
//            ( value / 65536.0 ).ToString(format, formatProvider);

//        public static bool ToBoolean(uint value) =>
//            value != 0;

//        public static byte ToByte(uint value) =>
//            (byte)( value >> 16 );

//        public static decimal ToDecimal(uint value) =>
//            (decimal)( value / 65536.0 );

//        public static double ToDouble(uint value) =>
//            value / 65536.0;

//        public static Half ToHalf(uint value) =>
//            (Half)( value / 65536.0 );

//        public static short ToInt16(uint value) =>
//            (short)( value >> 16 );

//        public static int ToInt32(uint value) =>
//            (int)( value >> 16 );

//        public static long ToInt64(uint value) =>
//            value >> 16;

//        public static sbyte ToSByte(uint value) =>
//            (sbyte)( value >> 16 );

//        public static float ToSingle(uint value) =>
//            (float)( value / 65536.0 );

//        public static string ToString(uint value, IFormatProvider? provider) =>
//            ToString(value, null, provider);

//        public static object ToType(uint value, Type conversionType, IFormatProvider? provider)
//        {
//            if ((object)conversionType == null!)
//                throw new ArgumentNullException(nameof(conversionType));
//            if (conversionType == typeof(int))
//                return ToInt32(value);
//            if (conversionType == typeof(bool))
//                return ToBoolean(value);
//            if (conversionType == typeof(sbyte))
//                return ToSByte(value);
//            if (conversionType == typeof(byte))
//                return ToByte(value);
//            if (conversionType == typeof(short))
//                return ToInt16(value);
//            if (conversionType == typeof(ushort))
//                return ToUInt16(value);
//            if (conversionType == typeof(int))
//                return ToInt32(value);
//            if (conversionType == typeof(uint))
//                return ToUInt32(value);
//            if (conversionType == typeof(long))
//                return ToInt64(value);
//            if (conversionType == typeof(ulong))
//                return ToUInt64(value);
//            if (conversionType == typeof(Half))
//                return ToHalf(value);
//            if (conversionType == typeof(float))
//                return ToSingle(value);
//            if (conversionType == typeof(double))
//                return ToDouble(value);
//            if (conversionType == typeof(decimal))
//                return ToDecimal(value);
//            if (conversionType == typeof(string))
//                return ToString(value, provider);
//            if (conversionType == typeof(object))
//                return value;
//            throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType.FullName);
//        }

//        public static ushort ToUInt16(uint value) =>
//            (ushort)( value >> 16 );

//        public static uint ToUInt32(uint value) =>
//            value >> 16;

//        public static ulong ToUInt64(uint value) =>
//            value >> 16;

//        public static uint FromByte(byte value) =>
//            (uint)( value << 16 );

//        public static uint FromSByte(sbyte value) =>
//            (uint)( value << 16 );

//        public static uint FromInt16(short value) =>
//            (uint)( value << 16 );

//        public static uint FromUInt16(ushort value) =>
//            (uint)( value << 16 );

//        public static uint FromInt32(int value) =>
//            (uint)( value << 16 );

//        public static uint FromUInt32(uint value) =>
//            value << 16;

//        public static uint FromInt64(long value) =>
//            (uint)( value << 16 );

//        public static uint FromUInt64(ulong value) =>
//            (uint)( value << 16 );

//        public static uint FromHalf(Half value) =>
//            (uint)( (double)value * 65536.0 );

//        public static uint FromSingle(float value) =>
//            (uint)( value * 65536.0 );

//        public static uint FromDouble(double value) =>
//            (uint)( value * 65536.0 );

//        public static uint FromDecimal(decimal value) =>
//            (uint)( (double)value * 65536.0 );

//        public static uint ToUFix16(this byte value) =>
//            FromByte(value);

//        public static uint ToUFix16(this sbyte value) =>
//            FromSByte(value);

//        public static uint ToUFix16(this short value) =>
//            FromInt16(value);

//        public static uint ToUFix16(this ushort value) =>
//            FromUInt16(value);

//        public static uint ToUFix16(this int value) =>
//            FromInt32(value);

//        public static uint ToUFix16(this uint value) =>
//            FromUInt32(value);

//        public static uint ToUFix16(this long value) =>
//            FromInt64(value);

//        public static uint ToUFix16(this ulong value) =>
//            FromUInt64(value);

//        public static uint ToUFix16(this Half value) =>
//            FromHalf(value);

//        public static uint ToUFix16(this float value) =>
//            FromSingle(value);

//        public static uint ToUFix16(this double value) =>
//            FromDouble(value);

//        public static uint ToUFix16(this decimal value) =>
//            FromDecimal(value);
//    }
//}
