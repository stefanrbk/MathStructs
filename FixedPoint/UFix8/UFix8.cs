using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System
{
    [StructLayout(LayoutKind.Explicit, Size = 2), DebuggerDisplay("{Debug}")]
    public partial struct UFix8 : IComparable, IComparable<UFix8>, IConvertible, IEquatable<UFix8>, IFormattable
    {
        [FieldOffset(0), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ushort _value;

        internal UFix8(float value) =>
            _value = (ushort)( ( Math.Min(MaxValue, Math.Max(MinValue, value)) * 256.0f ) + ( value < 0 ? -0.5f : 0.5f ) );

    }
}
