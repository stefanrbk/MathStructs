using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
    [StructLayout(LayoutKind.Explicit, Size = 4)]
    [DebuggerDisplay("{Debug}")]
    public unsafe partial struct UFix16 : IComparable, IComparable<UFix16>, IConvertible, IEquatable<UFix16>, IFormattable
    {
        [FieldOffset(0)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private uint _value;

        internal UFix16(double value) =>
            _value = (uint)( ( Math.Min(MaxValue, Math.Max(MinValue, value)) * 65536.0 ) + ( value < 0 ? -0.5 : 0.5 ) );

    }
}