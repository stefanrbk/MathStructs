using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Matrix4x4Fix16
    {
        //private unsafe delegate Vector128<T> LoadFunc<T>(T* address) where T : unmanaged;

        //private unsafe delegate void StoreFunc<T>(T* address, Vector128<T> vector) where T : unmanaged;

        private struct CanonicalBasis
        {
            public Vector3Fix16 Row0;

            public Vector3Fix16 Row1;

            public Vector3Fix16 Row2;
        }

        private struct VectorBasis
        {
            public unsafe Vector3Fix16* Element0;

            public unsafe Vector3Fix16* Element1;

            public unsafe Vector3Fix16* Element2;
        }

        //private static class VectorMath
        //{
        //    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //    public static bool Equal(Vector256<int> a, Vector256<int> b)
        //    {
        //        if (Avx2.IsSupported)
        //            return Avx.MoveMask(Avx2.Xor(Avx2.CompareEqual(a, b), Vector256.Create(-1)).AsSingle()) == 0;
        //        throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //    public static bool Equal(Vector128<int> a, Vector128<int> b)
        //    {
        //        if (AdvSimd.Arm64.IsSupported)
        //        {
        //            Vector128<uint> vector = AdvSimd.CompareEqual(a, b).AsUInt32();
        //            Vector64<byte> vResult0 = vector.GetLower().AsByte();
        //            Vector64<byte> vResult1 = vector.GetUpper().AsByte();
        //            Vector64<byte> vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            Vector64<byte> vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);
        //            return AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16()).AsUInt32().GetElement(1) == uint.MaxValue;
        //        }
        //        if (Sse2.IsSupported)
        //            return Sse.MoveMask(Sse2.Xor(Sse2.CompareEqual(a, b), Vector128.Create(-1)).AsSingle()) == 0;
        //        throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //    public static Vector128<int> Lerp(Vector128<int> a, Vector128<int> b, Vector128<int> t)
        //    {
        //        if (AdvSimd.IsSupported)
        //            return AdvSimd.Add(a, AdvSimd.Multiply(AdvSimd.Subtract(b, a), t));
        //        if (Sse41.IsSupported)
        //            return Sse2.Add(a, Sse41.MultiplyLow(Sse2.Subtract(b, a), t));
        //        throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //    public static bool NotEqual(Vector128<int> a, Vector128<int> b)
        //    {
        //        if (AdvSimd.Arm64.IsSupported)
        //        {
        //            Vector128<uint> vector = AdvSimd.CompareEqual(a, b).AsUInt32();
        //            Vector64<byte> vResult0 = vector.GetLower().AsByte();
        //            Vector64<byte> vResult1 = vector.GetUpper().AsByte();
        //            Vector64<byte> vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            Vector64<byte> vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);
        //            return AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16()).AsUInt32().GetElement(1) != uint.MaxValue;
        //        }
        //        if (Sse2.IsSupported)
        //            return Sse.MoveMask(Sse2.Xor(Sse2.CompareEqual(a, b), Vector128.Create(-1)).AsSingle()) != 0;
        //        throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //    public static bool NotEqual(Vector256<int> a, Vector256<int> b)
        //    {
        //        if (Avx2.IsSupported)
        //            return Avx.MoveMask(Avx2.Xor(Avx2.CompareEqual(a, b), Vector256.Create(-1)).AsSingle()) != 0;
        //        throw new PlatformNotSupportedException();
        //    }
        //}

        [FieldOffset(0)]
        public readonly int M11;

        [FieldOffset(4)]
        public readonly int M12;

        [FieldOffset(8)]
        public readonly int M13;

        [FieldOffset(12)]
        public readonly int M14;

        [FieldOffset(16)]
        public readonly int M21;

        [FieldOffset(20)]
        public readonly int M22;

        [FieldOffset(24)]
        public readonly int M23;

        [FieldOffset(28)]
        public readonly int M24;

        [FieldOffset(32)]
        public readonly int M31;

        [FieldOffset(36)]
        public readonly int M32;

        [FieldOffset(40)]
        public readonly int M33;

        [FieldOffset(44)]
        public readonly int M34;

        [FieldOffset(48)]
        public readonly int M41;

        [FieldOffset(52)]
        public readonly int M42;

        [FieldOffset(56)]
        public readonly int M43;

        [FieldOffset(60)]
        public readonly int M44;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        private const MethodImplOptions Optimize = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        public static Matrix4x4Fix16 Identity => new Matrix4x4Fix16(1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 1.0);

        public static Matrix4x4Fix16 Zero => new Matrix4x4Fix16(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

        public bool IsIdentity => this == Identity;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16(int m11, int m12, int m13, int m14, int m21, int m22, int m23, int m24, int m31, int m32, int m33, int m34, int m41, int m42, int m43, int m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
            : this((int)m11, (int)m12, (int)m13, (int)m14, (int)m21, (int)m22, (int)m23, (int)m24, (int)m31, (int)m32, (int)m33, (int)m34, (int)m41, (int)m42, (int)m43, (int)m44)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16(Matrix3x3Fix16 value)
        {
            M11 = value.M11;
            M12 = value.M12;
            M13 = value.M13;
            M14 = 0;
            M21 = value.M21;
            M22 = value.M22;
            M23 = value.M23;
            M24 = 0;
            M31 = value.M31;
            M32 = value.M32;
            M33 = value.M33;
            M34 = 0;
            M41 = 0;
            M42 = 0;
            M43 = 0;
            M44 = 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Add(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            return left + right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(Span<int> span)
        {
            CopyTo(span, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(Span<int> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException("index");
            if (span.Length - index < 16)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = M11;
            span[index + 1] = M12;
            span[index + 2] = M13;
            span[index + 3] = M14;
            span[index + 4] = M21;
            span[index + 5] = M22;
            span[index + 6] = M23;
            span[index + 7] = M24;
            span[index + 8] = M31;
            span[index + 9] = M32;
            span[index + 10] = M33;
            span[index + 11] = M34;
            span[index + 12] = M41;
            span[index + 13] = M42;
            span[index + 14] = M43;
            span[index + 15] = M44;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Invert(Matrix4x4Fix16 matrix)
        {
            return SoftwareFallback(matrix);
            static Matrix4x4Fix16 SoftwareFallback(Matrix4x4Fix16 matrix)
            {
                int m2 = matrix.M11;
                int m3 = matrix.M12;
                int m4 = matrix.M13;
                int m5 = matrix.M14;
                int a = m2;
                int b = m3;
                int c = m4;
                int d = m5;
                m4 = matrix.M21;
                m3 = matrix.M22;
                m2 = matrix.M23;
                int m6 = matrix.M24;
                int e = m4;
                int f = m3;
                int g = m2;
                int h = m6;
                m2 = matrix.M31;
                m3 = matrix.M32;
                m4 = matrix.M33;
                int m7 = matrix.M34;
                int i = m2;
                int j = m3;
                int k = m4;
                int l = m7;
                m4 = matrix.M41;
                m3 = matrix.M42;
                m2 = matrix.M43;
                int m8 = matrix.M44;
                int m = m4;
                int n = m3;
                int o = m2;
                int p = m8;
                int kp_lo = k * p - l * o;
                int jp_ln = j * p - l * n;
                int jo_kn = j * o - k * n;
                int ip_lm = i * p - l * m;
                int io_km = i * o - k * m;
                int in_jm = i * n - j * m;
                int a2 = f * kp_lo - g * jp_ln + h * jo_kn;
                int a3 = -(e * kp_lo - g * ip_lm + h * io_km);
                int a4 = e * jp_ln - f * ip_lm + h * in_jm;
                int a5 = -(e * jo_kn - f * io_km + g * in_jm);
                int det = a * a2 + b * a3 + c * a4 + d * a5;
                if (det == 0)
                    return Zero;
                int invDet = 1 / det;
                int gp_ho = g * p - h * o;
                int fp_hn = f * p - h * n;
                int fo_gn = f * o - g * n;
                int ep_hm = e * p - h * m;
                int eo_gm = e * o - g * m;
                int en_fm = e * n - f * m;
                int gl_hk = g * l - h * k;
                int fl_hj = f * l - h * j;
                int fk_gj = f * k - g * j;
                int el_hi = e * l - h * i;
                int ek_gi = e * k - g * i;
                int ej_fi = e * j - f * i;
                int m9 = a2 * invDet;
                m2 = a3 * invDet;
                m3 = a4 * invDet;
                m4 = a5 * invDet;
                int m10 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
                int m11 = (a * kp_lo - c * ip_lm + d * io_km) * invDet;
                int m12 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
                int m13 = (a * jo_kn - b * io_km + c * in_jm) * invDet;
                int m14 = (b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
                int m15 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
                int m16 = (a * fp_hn - b * ep_hm + d * en_fm) * invDet;
                int m17 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;
                return new Matrix4x4Fix16(m9, m10, m14, -( b * gl_hk - c * fl_hj + d * fk_gj ) * invDet, m2, m11, m15, ( a * gl_hk - c * el_hi + d * ek_gi ) * invDet, m3, m12, m16, -( a * fl_hj - b * el_hi + d * ej_fi ) * invDet, m4, m13, m17, ( a * fk_gj - b * ek_gi + c * ej_fi ) * invDet);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Lerp(Matrix4x4Fix16 matrix1, Matrix4x4Fix16 matrix2, int amount)
        {
            Matrix4x4Fix16 m1 = matrix1;
            Matrix4x4Fix16 m2 = matrix2;
            return new Matrix4x4Fix16(m1.M11 + ( m2.M11 - m1.M11 ) * amount, m1.M12 + ( m2.M12 - m1.M12 ) * amount, m1.M13 + ( m2.M13 - m1.M13 ) * amount, m1.M14 + ( m2.M14 - m1.M14 ) * amount, m1.M21 + ( m2.M21 - m1.M21 ) * amount, m1.M22 + ( m2.M22 - m1.M22 ) * amount, m1.M23 + ( m2.M23 - m1.M23 ) * amount, m1.M24 + ( m2.M24 - m1.M24 ) * amount, m1.M31 + ( m2.M31 - m1.M31 ) * amount, m1.M32 + ( m2.M32 - m1.M32 ) * amount, m1.M33 + ( m2.M33 - m1.M33 ) * amount, m1.M34 + ( m2.M34 - m1.M34 ) * amount, m1.M41 + ( m2.M41 - m1.M41 ) * amount, m1.M42 + ( m2.M42 - m1.M42 ) * amount, m1.M43 + ( m2.M43 - m1.M43 ) * amount, m1.M44 + ( m2.M44 - m1.M44 ) * amount);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, int right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Negate(Matrix4x4Fix16 value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator -(Matrix4x4Fix16 value)
        {
            Unsafe.SkipInit<Matrix4x4Fix16>(out var _);
            return new Matrix4x4Fix16(-value.M11, -value.M12, -value.M13, -value.M14, -value.M21, -value.M22, -value.M23, -value.M24, -value.M31, -value.M32, -value.M33, -value.M34, -value.M41, -value.M42, -value.M43, -value.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator -(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            return new Matrix4x4Fix16(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14, left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24, left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34, left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
                return value1.M43 != value2.M43;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            return new Matrix4x4Fix16(value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41, value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42, value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43, value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44, value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41, value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42, value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43, value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44, value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41, value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42, value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43, value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44, value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41, value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42, value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43, value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, int value2)
        {
            return new Matrix4x4Fix16(value1.M11 * value2, value1.M12 * value2, value1.M13 * value2, value1.M14 * value2, value1.M21 * value2, value1.M22 * value2, value1.M23 * value2, value1.M24 * value2, value1.M31 * value2, value1.M32 * value2, value1.M33 * value2, value1.M34 * value2, value1.M41 * value2, value1.M42 * value2, value1.M43 * value2, value1.M44 * value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator +(Matrix4x4Fix16 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator +(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            return new Matrix4x4Fix16(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14, left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24, left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34, left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator ==(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2)
        {
            if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
                return value1.M43 == value2.M43;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Subtract(Matrix4x4Fix16 left, Matrix4x4Fix16 right)
        {
            return left - right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Transpose(Matrix4x4Fix16 matrix)
        {
            return new Matrix4x4Fix16(matrix.M11, matrix.M21, matrix.M31, matrix.M41, matrix.M12, matrix.M22, matrix.M32, matrix.M42, matrix.M13, matrix.M23, matrix.M33, matrix.M43, matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Equals(Matrix4x4Fix16 other)
        {
            return this == other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public override bool Equals(object? obj)
        {
            if (obj is Matrix4x4Fix16)
            {
                Matrix4x4Fix16 value = (Matrix4x4Fix16)obj;
                return this == value;
            }
            return false;
        }

        public int GetDeterminant()
        {
            int m2 = M11;
            int b = M12;
            int c = M13;
            int d = M14;
            int e = M21;
            int f = M22;
            int g = M23;
            int h = M24;
            int m3 = M31;
            int i = M32;
            int j = M33;
            int k = M34;
            int l = M41;
            int m = M42;
            int o = M43;
            int p = M44;
            int kp_lo = j * p - k * o;
            int jp_ln = i * p - k * m;
            int jo_kn = i * o - j * m;
            int ip_lm = m3 * p - k * l;
            int io_km = m3 * o - j * l;
            int in_jm = m3 * m - i * l;
            return m2 * ( f * kp_lo - g * jp_ln + h * jo_kn ) - b * ( e * kp_lo - g * ip_lm + h * io_km ) + c * ( e * jp_ln - f * ip_lm + h * in_jm ) - d * ( e * jo_kn - f * io_km + g * in_jm );
        }

        public override int GetHashCode()
        {
            HashCode hash = default(HashCode);
            hash.Add(M11);
            hash.Add(M12);
            hash.Add(M13);
            hash.Add(M14);
            hash.Add(M21);
            hash.Add(M22);
            hash.Add(M23);
            hash.Add(M24);
            hash.Add(M31);
            hash.Add(M32);
            hash.Add(M33);
            hash.Add(M34);
            hash.Add(M41);
            hash.Add(M42);
            hash.Add(M43);
            hash.Add(M44);
            return hash.ToHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 Invert()
        {
            return Invert(this);
        }

        public override string ToString()
        {
            return $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 Transpose()
        {
            return Transpose(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 With(int? m11 = null, int? m12 = null, int? m13 = null, int? m14 = null, int? m21 = null, int? m22 = null, int? m23 = null, int? m24 = null, int? m31 = null, int? m32 = null, int? m33 = null, int? m34 = null, int? m41 = null, int? m42 = null, int? m43 = null, int? m44 = null)
        {
            return new Matrix4x4Fix16(m11 ?? M11, m12 ?? M12, m13 ?? M13, m14 ?? M14, m21 ?? M21, m22 ?? M22, m23 ?? M23, m24 ?? M24, m31 ?? M31, m32 ?? M32, m33 ?? M33, m34 ?? M34, m41 ?? M41, m42 ?? M42, m43 ?? M43, m44 ?? M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 WithTranslation(Vector3Fix16 value)
        {
            return With(null, null, null, null, null, null, null, null, null, null, null, null, value.X, value.Y, value.Z);
        }

        internal Matrix3x3Fix16 As3x3()
        {
            return new Matrix3x3Fix16(M11, M12, M13, M21, M22, M23, M31, M32, M33);
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //private static Vector128<float> Permute(Vector128<float> value, byte control)
        //{
        //    if (!Avx.IsSupported)
        //        return Sse.Shuffle(value, value, control);
        //    return Avx.Permute(value, control);
        //}
    }
}
