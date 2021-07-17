using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    public unsafe partial struct Fix16
    {
        #region ToString

        public override string ToString() =>
            ToString(null, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value / 65536.0 ).ToString(format, formatProvider);

        #endregion ToString

        #region Equals

        public override bool Equals(object? obj) =>
            obj is Fix16 f && f == this;

        public bool Equals([AllowNull] Fix16 other) =>
            this == other;

        public static bool Equals(Fix16 left, Fix16 right, Fix16 delta)
        {
            if (delta == Zero)
                return left == right;
            else
                return Abs(left - right) <= delta;
        }

        #endregion Equals

        #region GetHashCode

        public override int GetHashCode() =>
            HashCode.Combine(_value, 2);

        #endregion GetHashCode

        #region CompareTo

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;
            if (obj is not Fix16)
                throw new ArgumentException("Argument must be a Fix16", nameof(obj));
            return CompareTo((Fix16)obj);
        }

        public int CompareTo([AllowNull] Fix16 other) =>
            ((double)this).CompareTo(other);

        #endregion CompareTo

        #region Raw

        public static Fix16 Raw(int value) =>
            new() { _value = value };

        #endregion Raw

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double Debug => this;
    }
}