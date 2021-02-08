using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System
{
    public partial struct UFix8
    {
        #region ToString

        public override string ToString() =>
            ToString(null, null);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            ( _value / 256f ).ToString(format, formatProvider);

        #endregion ToString

        #region Equals

        public override bool Equals(object? obj) =>
            obj is UFix8 f && f == this;

        public bool Equals([AllowNull] UFix8 other) =>
            this == other;

        public static bool Equals(UFix8 left, UFix8 right, UFix8 delta) =>
            ( delta == Zero )
                ? left == right
                : ( left > right )
                    ? ( left - right ) <= delta
                    : ( right - left ) <= delta;

        #endregion Equals

        #region GetHashCode

        public override int GetHashCode() =>
            HashCode.Combine(_value, 4);

        #endregion GetHashCode

        #region CompareTo

        public int CompareTo(object? obj) =>
            ( obj is null )
                ? 1
                : ( obj is UFix8 f )
                    ? CompareTo(f)
                    : throw new ArgumentException("Argument must be a UFix8", nameof(obj));

        public int CompareTo([AllowNull] UFix8 other) =>
            ( (float)this ).CompareTo(other);

        #endregion CompareTo

        #region Raw
        public static UFix8 Raw(ushort value) =>
            new() { _value = value };

        #endregion Raw

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private double Debug => this;
    }
}