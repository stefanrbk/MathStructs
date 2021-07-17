using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace System.Numerics
{
    internal static class InnerExtensions
    {
        #region Public Methods

        public static Vector4D AsVector4D(this Vector256<double> value) =>
            Unsafe.As<Vector256<double>, Vector4D>(ref value);

        public static Vector4 AsVector4(this Vector128<float> value) =>
                    Unsafe.As<Vector128<float>, Vector4>(ref value);

        #endregion Public Methods
    }
}