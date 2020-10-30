using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.Intrinsics.X86.Sse2;
using static System.Runtime.Intrinsics.X86.Sse;
namespace MathStructs
{
    public struct Vector4F
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public override bool Equals(object? obj) =>
            obj is Vector4F vec && Equals(vec);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4F other)
        {
            return other.X == X && other.Y == Y && other.Z == Z && other.W == W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !TEST
        private
#else
        public
#endif
            static unsafe Vector4F SseAdd(Vector4F left, Vector4F right)
        {
            Unsafe.SkipInit<Vector4F>(out var result);

            Store(&result.X, Add(*(Vector128<float>*)&left, *(Vector128<float>*)&right));

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if !TEST
        private
#else
        public
#endif
            static Vector4F SisdAdd(Vector4F left, Vector4F right)
            => new Vector4F(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
    }
}
