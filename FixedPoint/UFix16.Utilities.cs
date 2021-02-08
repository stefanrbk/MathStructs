using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    public unsafe partial struct UFix16
    {
        #region ToString

        public override string ToString() =>
            ToString(null, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value / 65536.0 ).ToString(format, formatProvider);

        #endregion ToString

        #region Equals

        public override bool Equals(object? obj) =>
            obj is UFix16 f && f == this;

        public bool Equals([AllowNull] UFix16 other) =>
            this == other;

        public static bool Equals(UFix16 left, UFix16 right, UFix16 delta)
        {
            if (delta == Zero)
                return left == right;
            else
                return ( right > left ? right - left : left - right ) <= delta;
        }

        #endregion Equals

        #region GetHashCode

        public override int GetHashCode() =>
            HashCode.Combine(_value, 3);

        #endregion GetHashCode

        #region CompareTo

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            if (obj is not UFix16)
                throw new ArgumentException("Argument must be a UFix16", nameof(obj));
            return CompareTo((UFix16)obj);
        }

        public int CompareTo([AllowNull] UFix16 other) =>
            ((double)this).CompareTo(other);

        #endregion CompareTo

        #region Raw

        public static UFix16 Raw(uint value) =>
            new() { _value = value };

        #endregion Raw

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double Debug => this;
    }
}