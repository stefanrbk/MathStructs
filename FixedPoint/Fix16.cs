using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    [DebuggerDisplay("{Debug}")]
    public unsafe partial struct Fix16 : IComparable, IComparable<Fix16>, IConvertible, IEquatable<Fix16>, IFormattable
    {
        [FieldOffset(0)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _value;

        private static readonly Fix16[] expCacheIndex = new Fix16[4096];
        private static readonly Fix16[] expCacheValue = new Fix16[4096];
        private static readonly Fix16[] sinCacheIndex = new Fix16[4096];
        private static readonly Fix16[] sinCacheValue = new Fix16[4096];
        private static readonly (Fix16 x, Fix16 y)[] atanCacheIndex = new (Fix16,Fix16)[4096];
        private static readonly Fix16[] atanCacheValue = new Fix16[4096];

        internal Fix16(double value) =>
            _value = (int)( ( Math.Min(MaxValue, Math.Max(MinValue, value)) * 65536.0 ) + ( value < 0 ? -0.5 : 0.5 ) );
    }
}
