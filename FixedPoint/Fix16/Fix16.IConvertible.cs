namespace System
{
    public unsafe partial struct Fix16
    {
        TypeCode IConvertible.GetTypeCode() => (TypeCode)100;

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
    }
}