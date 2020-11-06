using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MathStructs
{
    public static class Extensions
    {
        #region Public Methods

        public static Vector4D AsVector4D(this Vector256<double> value) =>
            Unsafe.As<Vector256<double>, Vector4D>(ref value);

        public static Vector4F AsVector4F(this Vector128<float> value) =>
                    Unsafe.As<Vector128<float>, Vector4F>(ref value);

        #endregion Public Methods
    }
}