namespace System
{
    public unsafe partial struct UFix16
    {
        #region Math Operators

        public static UFix16 operator +(UFix16 left, UFix16 right) =>
            Add(left, right);
        public static UFix16? operator +(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Add(left.Value, right.Value);
        public static UFix16 operator -(UFix16 left, UFix16 right) =>
            Subtract(left, right);
        public static UFix16? operator -(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Subtract(left.Value, right.Value);
        public static UFix16 operator *(UFix16 left, UFix16 right) =>
            Multiply(left, right);
        public static UFix16? operator *(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Multiply(left.Value, right.Value);
        public static UFix16 operator /(UFix16 left, UFix16 right) =>
            Divide(left, right);
        public static UFix16? operator /(UFix16? left, UFix16? right) =>
            ( left is null || right is null )
                ? null
                : Divide(left.Value, right.Value);

        #endregion Math Operators

        #region Conditional Operators

        public static bool operator ==(UFix16 left, UFix16 right) =>
            *(uint*)&left == *(uint*)&right;
        public static bool operator ==(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value == right.Value._value;
        public static bool operator !=(UFix16 left, UFix16 right) =>
            *(uint*)&left != *(uint*)&right;
        public static bool operator !=(UFix16? left, UFix16? right) =>
            !left.HasValue || !right.HasValue || left.Value._value != right.Value._value;
        public static bool operator >=(UFix16 left, UFix16 right) =>
            *(uint*)&left >= *(uint*)&right;
        public static bool operator >=(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value >= right.Value._value;
        public static bool operator <=(UFix16 left, UFix16 right) =>
            *(uint*)&left <= *(uint*)&right;
        public static bool operator <=(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value <= right.Value._value;
        public static bool operator >(UFix16 left, UFix16 right) =>
            *(uint*)&left > *(uint*)&right;
        public static bool operator >(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value > right.Value._value;
        public static bool operator <(UFix16 left, UFix16 right) =>
            *(uint*)&left < *(uint*)&right;
        public static bool operator <(UFix16? left, UFix16? right) =>
            left.HasValue && right.HasValue && left.Value._value < right.Value._value;

        #endregion Conditional Operators

        #region Cast Operators

        #region Implicit From UFix16 Operators

        public static implicit operator double(UFix16 value) =>
            *(uint*)&value / 65536.0;

        #endregion Implicit From UFix16 Operators

        #region Explicit From UFix16 Operators

        public static explicit operator byte(UFix16 value) =>
            (byte)Math.Min(Byte.MaxValue, Math.Max(MinValue, value));
        public static explicit operator sbyte(UFix16 value) =>
            (sbyte)Math.Min(SByte.MaxValue, Math.Max(MinValue, value));
        public static explicit operator short(UFix16 value) =>
            (short)Math.Min(Int16.MaxValue, Math.Max(MinValue, value));
        public static explicit operator ushort(UFix16 value) =>
            (ushort)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator int(UFix16 value) =>
            (int)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator uint(UFix16 value) =>
            (uint)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator long(UFix16 value) =>
            (long)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator ulong(UFix16 value) =>
            (ulong)Math.Min(MaxValue, Math.Max(MinValue, value));
        public static explicit operator decimal(UFix16 value) =>
            (decimal)(double)value;
        public static explicit operator float(UFix16 value) =>
            (float)(double)value;
        public static explicit operator Half(UFix16 value) =>
            (Half)(double)value;
        public static explicit operator Fix16(UFix16 value) =>
            (Fix16)(double)value;

        #endregion Explicit From UFix16 Operators

        #region Implicit To UFix16 Operators

        public static implicit operator UFix16(ushort value) =>
            new(value);

        #endregion Implicit To UFix16 Operators

        #region Explicit To UFix16 Operators

        public static explicit operator UFix16(sbyte value) =>
            new(value);
        public static explicit operator UFix16(short value) =>
            new(value);
        public static explicit operator UFix16(int value) =>
            new(value);
        public static explicit operator UFix16(uint value) =>
            new(value);
        public static explicit operator UFix16(long value) =>
            new(value);
        public static explicit operator UFix16(ulong value) =>
            new(value);
        public static explicit operator UFix16(Half value) =>
            new((double)value);
        public static explicit operator UFix16(float value) =>
            new(value);
        public static explicit operator UFix16(decimal value) =>
            new((double)value);
        public static explicit operator UFix16(Fix16 value) =>
            new(value);
        public static explicit operator UFix16(double value) =>
            new(value);

        #endregion Explicit To UFix16 Operators

        #endregion Cast Operators
    }
}