using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MathStructs
{
    internal static class Fix16InnerExtensions
    {
        public static Vector4Fix16 AsVector4Fix16(this Vector128<int> value) =>
            Unsafe.As<Vector128<int>, Vector4Fix16>(ref value);

        public static Vector4Fix16 AsVector4Fix16(this Vector128<float> value) =>
            Unsafe.As<Vector128<float>, Vector4Fix16>(ref value);
    }
}
