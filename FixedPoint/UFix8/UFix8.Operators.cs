namespace System
{
    public unsafe partial struct UFix8
    {
        #region Math Operators

        public static UFix8 operator +(UFix8 left, UFix8 right) =>
            Add(left, right);
        public static UFix8? operator +(UFix8? left, UFix8? right) =>
            ( left is null || right is null )
                ? null
                : Add(left.Value, right.Value);
        public static UFix8 operator -(UFix8 left, UFix8 right) =>
            Subtract(left, right);
        public static UFix8? operator -(UFix8? left, UFix8? right) =>
            ( left is null || right is null )
                ? null
                : Subtract(left.Value, right.Value);
        public static UFix8 operator *(UFix8 left, UFix8 right) =>
            Multiply(left, right);
        public static UFix8? operator *(UFix8? left, UFix8? right) =>
            ( left is null || right is null )
                ? null
                : Multiply(left.Value, right.Value);
        public static UFix8 operator /(UFix8 left, UFix8 right) =>
            Divide(left, right);
        public static UFix8? operator /(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue ? left.Value / right.Value : null;

        #endregion Math Operators

        #region Conditional Operators

        public static bool operator ==(UFix8 left, UFix8 right) =>
            *(ushort*)&left == *(ushort*)&right;
        public static bool operator ==(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue && left.Value._value == right.Value._value;
        public static bool operator !=(UFix8 left, UFix8 right) =>
            *(ushort*)&left != *(ushort*)&right;
        public static bool operator !=(UFix8? left, UFix8? right) =>
            !left.HasValue || !right.HasValue || left.Value._value != right.Value._value;
        public static bool operator >=(UFix8 left, UFix8 right) =>
            *(ushort*)&left >= *(ushort*)&right;
        public static bool operator >=(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue && left.Value._value >= right.Value._value;
        public static bool operator <=(UFix8 left, UFix8 right) =>
            *(ushort*)&left <= *(ushort*)&right;
        public static bool operator <=(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue && left.Value._value <= right.Value._value;
        public static bool operator >(UFix8 left, UFix8 right) =>
            *(ushort*)&left > *(ushort*)&right;
        public static bool operator >(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue && left.Value._value > right.Value._value;
        public static bool operator <(UFix8 left, UFix8 right) =>
            *(ushort*)&left < *(ushort*)&right;
        public static bool operator <(UFix8? left, UFix8? right) =>
            left.HasValue && right.HasValue && left.Value._value < right.Value._value;

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From UFix8 Operators

        public static implicit operator float(UFix8 value) =>
            value._value / 256f;
        public static implicit operator UFix16(UFix8 value) =>
            UFix16.Raw((uint)value._value << 8);
        public static implicit operator Fix16(UFix8 value) =>
            Fix16.Raw((int)((uint)value._value << 8));

        #endregion Implicit From UFix8 Operators

        #region Explicit From UFix8 Operators

        public static explicit operator byte(UFix8 value) =>
            (byte)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator sbyte(UFix8 value) =>
            (sbyte)Math.Min(SByte.MaxValue, Math.Max(MinValue, value));
        public static explicit operator short(UFix8 value) =>
            (short)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator ushort(UFix8 value) =>
            (ushort)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator int(UFix8 value) =>
            (int)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator uint(UFix8 value) =>
            (uint)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator long(UFix8 value) =>
            (long)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator ulong(UFix8 value) =>
            (ulong)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator decimal(UFix8 value) =>
            (decimal)(float)value;
        public static explicit operator Half(UFix8 value) =>
            (Half)(float)value;

        #endregion Explicit From UFix8 Operators

        #region Implicit To UFix8 Operators

        public static implicit operator UFix8(byte value) =>
            new(value);

        #endregion Implicit To UFix8 Operators

        #region Explicit To UFix8 Operators

        public static explicit operator UFix8(ushort value) =>
            new(value);
        public static explicit operator UFix8(int value) =>
            new(value);
        public static explicit operator UFix8(uint value) =>
            new(value);
        public static explicit operator UFix8(long value) =>
            new(value);
        public static explicit operator UFix8(ulong value) =>
            new(value);
        public static explicit operator UFix8(Half value) =>
            new((float)value);
        public static explicit operator UFix8(double value) =>
            new((float)value);
        public static explicit operator UFix8(decimal value) =>
            new((float)value);
        public static explicit operator UFix8(UFix16 value) =>
            new((float)value);
        public static explicit operator UFix8(Fix16 value) =>
            new((float)value);

        #endregion Explicit To UFix8 Operators

        #endregion Cast Operators
    }
}