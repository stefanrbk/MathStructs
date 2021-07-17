namespace System
{
    public unsafe partial struct Fix16
    {
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
            (byte)Math.Min(Byte.MaxValue, Math.Max(Byte.MinValue, value));
        public static explicit operator sbyte(Fix16 value) =>
            (sbyte)Math.Min(SByte.MaxValue, Math.Max(SByte.MinValue, value));
        public static explicit operator short(Fix16 value) =>
            (short)Math.Min(Int16.MaxValue, Math.Max(Int16.MinValue, value));
        public static explicit operator ushort(Fix16 value) =>
            (ushort)Math.Min(MaxValue, Math.Max(UInt16.MinValue, value));
        public static explicit operator int(Fix16 value) =>
            (int)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator uint(Fix16 value) =>
            (uint)Math.Min(MaxValue, Math.Max(UInt32.MinValue, value));
        public static explicit operator long(Fix16 value) =>
            (long)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator ulong(Fix16 value) =>
            (ulong)Math.Min(MaxValue, Math.Max(UInt64.MinValue, value));
        public static explicit operator decimal(Fix16 value) =>
            (decimal)(double)value;
        public static explicit operator float(Fix16 value) =>
            (float)(double)value;
        public static explicit operator Half(Fix16 value) =>
            (Half)(double)value;

        #endregion Explicit From Fix16 Operators

        #region Implicit To Fix16 Operators

        public static implicit operator Fix16(short value) =>
            new(value);

        #endregion Implicit To Fix16 Operators

        #region Explicit To Fix16 Operators

        public static explicit operator Fix16(ushort value) =>
            new(value);
        public static explicit operator Fix16(int value) =>
            new(value);
        public static explicit operator Fix16(uint value) =>
            new(value);
        public static explicit operator Fix16(long value) =>
            new(value);
        public static explicit operator Fix16(ulong value) =>
            new(value);
        public static explicit operator Fix16(Half value) =>
            new((double)value);
        public static explicit operator Fix16(double value) =>
            new(value);
        public static explicit operator Fix16(decimal value) =>
            new((double)value);

        #endregion Explicit To Fix16 Operators

        #endregion Cast Operators
    }
}