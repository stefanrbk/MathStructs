namespace System
{
    public unsafe partial struct UFix8
    {
        TypeCode IConvertible.GetTypeCode() => (TypeCode)102;

        bool IConvertible.ToBoolean(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToBoolean(null);

        byte IConvertible.ToByte(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToByte(null);

        decimal IConvertible.ToDecimal(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToDecimal(null);

        double IConvertible.ToDouble(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToDouble(null);

        short IConvertible.ToInt16(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToInt16(null);

        int IConvertible.ToInt32(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToInt32(null);

        long IConvertible.ToInt64(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToInt64(null);

        sbyte IConvertible.ToSByte(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToSByte(null);

        float IConvertible.ToSingle(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToSingle(null);

        string IConvertible.ToString(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToString(null);

        object IConvertible.ToType(Type conversionType, IFormatProvider? provider) =>
            FixedPointExtensions.toTypeMap.TryGetValue(conversionType?.FullName ?? typeof(void).FullName!, out var func)
                ? func(this, provider)
                : throw new InvalidCastException("Cannot convert from a Q16 fixed-point number to " + conversionType!.FullName);

        ushort IConvertible.ToUInt16(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToUInt16(null);

        uint IConvertible.ToUInt32(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToUInt32(null);

        ulong IConvertible.ToUInt64(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToUInt64(null);
        char IConvertible.ToChar(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToChar(null);
        DateTime IConvertible.ToDateTime(IFormatProvider? provider) =>
            ( (IConvertible)(float)this ).ToDateTime(null);
    }
}