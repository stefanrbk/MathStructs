using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public static class Extensions
    {
        public static Vector4F AsVector4F(this Vector128<float> value) =>
            Unsafe.As<Vector128<float>, Vector4F>(ref value);
    }
}
