using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
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
        public readonly Fix16 M11;

        [FieldOffset(4)]
        public readonly Fix16 M12;

        [FieldOffset(8)]
        public readonly Fix16 M13;

        [FieldOffset(12)]
        public readonly Fix16 M14;

        [FieldOffset(16)]
        public readonly Fix16 M21;

        [FieldOffset(20)]
        public readonly Fix16 M22;

        [FieldOffset(24)]
        public readonly Fix16 M23;

        [FieldOffset(28)]
        public readonly Fix16 M24;

        [FieldOffset(32)]
        public readonly Fix16 M31;

        [FieldOffset(36)]
        public readonly Fix16 M32;

        [FieldOffset(40)]
        public readonly Fix16 M33;

        [FieldOffset(44)]
        public readonly Fix16 M34;

        [FieldOffset(48)]
        public readonly Fix16 M41;

        [FieldOffset(52)]
        public readonly Fix16 M42;

        [FieldOffset(56)]
        public readonly Fix16 M43;

        [FieldOffset(60)]
        public readonly Fix16 M44;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        private const MethodImplOptions Optimize = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        public static Matrix4x4Fix16 Identity => new Matrix4x4Fix16(Fix16.One, Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                    Fix16.Zero, Fix16.One, Fix16.Zero, Fix16.Zero,
                                                                    Fix16.Zero, Fix16.Zero, Fix16.One, Fix16.Zero,
                                                                    Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.One);

        public static Matrix4x4Fix16 Zero => new Matrix4x4Fix16(Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.Zero);

        public bool IsIdentity => this == Identity;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16(Fix16 m11, Fix16 m12, Fix16 m13, Fix16 m14,
                              Fix16 m21, Fix16 m22, Fix16 m23, Fix16 m24,
                              Fix16 m31, Fix16 m32, Fix16 m33, Fix16 m34,
                              Fix16 m41, Fix16 m42, Fix16 m43, Fix16 m44)
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
        public Matrix4x4Fix16(Matrix3x3Fix16 value)
        {
            M11 = value.M11;
            M12 = value.M12;
            M13 = value.M13;
            M14 = Fix16.Zero;
            M21 = value.M21;
            M22 = value.M22;
            M23 = value.M23;
            M24 = Fix16.Zero;
            M31 = value.M31;
            M32 = value.M32;
            M33 = value.M33;
            M34 = Fix16.Zero;
            M41 = Fix16.Zero;
            M42 = Fix16.Zero;
            M43 = Fix16.Zero;
            M44 = Fix16.One;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Add(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(Span<Fix16> span) =>
            CopyTo(span, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(Span<Fix16> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
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
                var m2 = matrix.M11;
                var m3 = matrix.M12;
                var m4 = matrix.M13;
                var m5 = matrix.M14;
                var a = m2;
                var b = m3;
                var c = m4;
                var d = m5;
                m4 = matrix.M21;
                m3 = matrix.M22;
                m2 = matrix.M23;
                var m6 = matrix.M24;
                var e = m4;
                var f = m3;
                var g = m2;
                var h = m6;
                m2 = matrix.M31;
                m3 = matrix.M32;
                m4 = matrix.M33;
                var m7 = matrix.M34;
                var i = m2;
                var j = m3;
                var k = m4;
                var l = m7;
                m4 = matrix.M41;
                m3 = matrix.M42;
                m2 = matrix.M43;
                var m8 = matrix.M44;
                var m = m4;
                var n = m3;
                var o = m2;
                var p = m8;
                var kp_lo = ( k * p ) - ( l * o );
                var jp_ln = ( j * p ) - ( l * n );
                var jo_kn = ( j * o ) - ( k * n );
                var ip_lm = ( i * p ) - ( l * m );
                var io_km = ( i * o ) - ( k * m );
                var in_jm = ( i * n ) - ( j * m );
                var a2 =    ( f * kp_lo ) - ( g * jp_ln ) + ( h * jo_kn );
                var a3 = -( ( e * kp_lo ) - ( g * ip_lm ) + ( h * io_km ));
                var a4 =    ( e * jp_ln ) - ( f * ip_lm ) + ( h * in_jm );
                var a5 = -( ( e * jo_kn ) - ( f * io_km ) + ( g * in_jm ));
                var det = ( a * a2 ) + ( b * a3 ) + ( c * a4 ) + ( d * a5 );
                if (det == Fix16.Zero)
                    return Zero;
                var invDet = Fix16.One / det;
                var gp_ho = ( g * p ) - ( h * o );
                var fp_hn = ( f * p ) - ( h * n );
                var fo_gn = ( f * o ) - ( g * n );
                var ep_hm = ( e * p ) - ( h * m );
                var eo_gm = ( e * o ) - ( g * m );
                var en_fm = ( e * n ) - ( f * m );
                var gl_hk = ( g * l ) - ( h * k );
                var fl_hj = ( f * l ) - ( h * j );
                var fk_gj = ( f * k ) - ( g * j );
                var el_hi = ( e * l ) - ( h * i );
                var ek_gi = ( e * k ) - ( g * i );
                var ej_fi = ( e * j ) - ( f * i );
                var m9 = a2 * invDet;
                m2 = a3 * invDet;
                m3 = a4 * invDet;
                m4 = a5 * invDet;
                var m10 = -( ( b * kp_lo ) - ( c * jp_ln ) + ( d * jo_kn ) ) * invDet;
                var m11 =  ( ( a * kp_lo ) - ( c * ip_lm ) + ( d * io_km ) ) * invDet;
                var m12 = -( ( a * jp_ln ) - ( b * ip_lm ) + ( d * in_jm ) ) * invDet;
                var m13 =  ( ( a * jo_kn ) - ( b * io_km ) + ( c * in_jm ) ) * invDet;
                var m14 =  ( ( b * gp_ho ) - ( c * fp_hn ) + ( d * fo_gn ) ) * invDet;
                var m15 = -( ( a * gp_ho ) - ( c * ep_hm ) + ( d * eo_gm ) ) * invDet;
                var m16 =  ( ( a * fp_hn ) - ( b * ep_hm ) + ( d * en_fm ) ) * invDet;
                var m17 = -( ( a * fo_gn ) - ( b * eo_gm ) + ( c * en_fm ) ) * invDet;
                return new Matrix4x4Fix16(m9, m10, m14, -( ( b * gl_hk ) - ( c * fl_hj ) + ( d * fk_gj ) ) * invDet,
                                          m2, m11, m15,  ( ( a * gl_hk ) - ( c * el_hi ) + ( d * ek_gi ) ) * invDet,
                                          m3, m12, m16, -( ( a * fl_hj ) - ( b * el_hi ) + ( d * ej_fi ) ) * invDet,
                                          m4, m13, m17,  ( ( a * fk_gj ) - ( b * ek_gi ) + ( c * ej_fi ) ) * invDet);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Lerp(Matrix4x4Fix16 matrix1, Matrix4x4Fix16 matrix2, Fix16 amount)
        {
            var m1 = matrix1;
            var m2 = matrix2;
            return new Matrix4x4Fix16(m1.M11 + ( ( m2.M11 - m1.M11 ) * amount ), m1.M12 + ( ( m2.M12 - m1.M12 ) * amount ),
                                      m1.M13 + ( ( m2.M13 - m1.M13 ) * amount ), m1.M14 + ( ( m2.M14 - m1.M14 ) * amount ),
                                      m1.M21 + ( ( m2.M21 - m1.M21 ) * amount ), m1.M22 + ( ( m2.M22 - m1.M22 ) * amount ),
                                      m1.M23 + ( ( m2.M23 - m1.M23 ) * amount ), m1.M24 + ( ( m2.M24 - m1.M24 ) * amount ),
                                      m1.M31 + ( ( m2.M31 - m1.M31 ) * amount ), m1.M32 + ( ( m2.M32 - m1.M32 ) * amount ),
                                      m1.M33 + ( ( m2.M33 - m1.M33 ) * amount ), m1.M34 + ( ( m2.M34 - m1.M34 ) * amount ),
                                      m1.M41 + ( ( m2.M41 - m1.M41 ) * amount ), m1.M42 + ( ( m2.M42 - m1.M42 ) * amount ),
                                      m1.M43 + ( ( m2.M43 - m1.M43 ) * amount ), m1.M44 + ( ( m2.M44 - m1.M44 ) * amount ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Multiply(Matrix4x4Fix16 left, Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Negate(Matrix4x4Fix16 value) =>
            -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator -(Matrix4x4Fix16 value) =>
            new Matrix4x4Fix16(-value.M11, -value.M12, -value.M13, -value.M14,
                               -value.M21, -value.M22, -value.M23, -value.M24,
                               -value.M31, -value.M32, -value.M33, -value.M34,
                               -value.M41, -value.M42, -value.M43, -value.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator -(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            new Matrix4x4Fix16(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
                               left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24,
                               left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34,
                               left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2) =>
            value1.M11 != value2.M11 || value1.M22 != value2.M22 || value1.M33 != value2.M33 || value1.M44 != value2.M44 ||
            value1.M12 != value2.M12 || value1.M13 != value2.M13 || value1.M14 != value2.M14 || value1.M21 != value2.M21 ||
            value1.M23 != value2.M23 || value1.M24 != value2.M24 || value1.M31 != value2.M31 || value1.M32 != value2.M32 ||
            value1.M34 != value2.M34 || value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2) =>
            new Matrix4x4Fix16(( value1.M11 * value2.M11 ) + ( value1.M12 * value2.M21 ) + ( value1.M13 * value2.M31 ) + ( value1.M14 * value2.M41 ),
                               ( value1.M11 * value2.M12 ) + ( value1.M12 * value2.M22 ) + ( value1.M13 * value2.M32 ) + ( value1.M14 * value2.M42 ),
                               ( value1.M11 * value2.M13 ) + ( value1.M12 * value2.M23 ) + ( value1.M13 * value2.M33 ) + ( value1.M14 * value2.M43 ),
                               ( value1.M11 * value2.M14 ) + ( value1.M12 * value2.M24 ) + ( value1.M13 * value2.M34 ) + ( value1.M14 * value2.M44 ),
                               ( value1.M21 * value2.M11 ) + ( value1.M22 * value2.M21 ) + ( value1.M23 * value2.M31 ) + ( value1.M24 * value2.M41 ),
                               ( value1.M21 * value2.M12 ) + ( value1.M22 * value2.M22 ) + ( value1.M23 * value2.M32 ) + ( value1.M24 * value2.M42 ),
                               ( value1.M21 * value2.M13 ) + ( value1.M22 * value2.M23 ) + ( value1.M23 * value2.M33 ) + ( value1.M24 * value2.M43 ),
                               ( value1.M21 * value2.M14 ) + ( value1.M22 * value2.M24 ) + ( value1.M23 * value2.M34 ) + ( value1.M24 * value2.M44 ),
                               ( value1.M31 * value2.M11 ) + ( value1.M32 * value2.M21 ) + ( value1.M33 * value2.M31 ) + ( value1.M34 * value2.M41 ),
                               ( value1.M31 * value2.M12 ) + ( value1.M32 * value2.M22 ) + ( value1.M33 * value2.M32 ) + ( value1.M34 * value2.M42 ),
                               ( value1.M31 * value2.M13 ) + ( value1.M32 * value2.M23 ) + ( value1.M33 * value2.M33 ) + ( value1.M34 * value2.M43 ),
                               ( value1.M31 * value2.M14 ) + ( value1.M32 * value2.M24 ) + ( value1.M33 * value2.M34 ) + ( value1.M34 * value2.M44 ),
                               ( value1.M41 * value2.M11 ) + ( value1.M42 * value2.M21 ) + ( value1.M43 * value2.M31 ) + ( value1.M44 * value2.M41 ),
                               ( value1.M41 * value2.M12 ) + ( value1.M42 * value2.M22 ) + ( value1.M43 * value2.M32 ) + ( value1.M44 * value2.M42 ),
                               ( value1.M41 * value2.M13 ) + ( value1.M42 * value2.M23 ) + ( value1.M43 * value2.M33 ) + ( value1.M44 * value2.M43 ),
                               ( value1.M41 * value2.M14 ) + ( value1.M42 * value2.M24 ) + ( value1.M43 * value2.M34 ) + ( value1.M44 * value2.M44 ));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator *(Matrix4x4Fix16 value1, Fix16 value2) =>
            new Matrix4x4Fix16(value1.M11 * value2, value1.M12 * value2, value1.M13 * value2, value1.M14 * value2,
                               value1.M21 * value2, value1.M22 * value2, value1.M23 * value2, value1.M24 * value2,
                               value1.M31 * value2, value1.M32 * value2, value1.M33 * value2, value1.M34 * value2,
                               value1.M41 * value2, value1.M42 * value2, value1.M43 * value2, value1.M44 * value2);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator +(Matrix4x4Fix16 value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 operator +(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            new Matrix4x4Fix16(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
                               left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24,
                               left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34,
                               left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator ==(Matrix4x4Fix16 value1, Matrix4x4Fix16 value2) =>
            value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 &&
            value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 &&
            value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 &&
            value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Subtract(Matrix4x4Fix16 left, Matrix4x4Fix16 right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix4x4Fix16 Transpose(Matrix4x4Fix16 matrix) =>
            new Matrix4x4Fix16(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                               matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                               matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                               matrix.M14, matrix.M24, matrix.M34, matrix.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Equals(Matrix4x4Fix16 other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public override bool Equals(object? obj) =>
            ( obj is Matrix4x4Fix16 value ) && this == value;

        public Fix16 GetDeterminant()
        {
            var a = M11;
            var b = M12;
            var c = M13;
            var d = M14;
            var e = M21;
            var f = M22;
            var g = M23;
            var h = M24;
            var i = M31;
            var j = M32;
            var k = M33;
            var l = M34;
            var m = M41;
            var n = M42;
            var o = M43;
            var p = M44;
            var kp_lo = ( k * p ) - ( l * o );
            var jp_ln = ( j * p ) - ( l * n );
            var jo_kn = ( j * o ) - ( k * n );
            var ip_lm = ( i * p ) - ( l * m );
            var io_km = ( i * o ) - ( k * m );
            var in_jm = ( i * n ) - ( j * m );
            return ( a * ( ( f * kp_lo ) - ( g * jp_ln ) + ( h * jo_kn ) ) ) - ( b * ( ( e * kp_lo ) - ( g * ip_lm ) + ( h * io_km ) ) ) +
                   ( c * ( ( e * jp_ln ) - ( f * ip_lm ) + ( h * in_jm ) ) ) - ( d * ( ( e * jo_kn ) - ( f * io_km ) + ( g * in_jm ) ) );
        }

        public override int GetHashCode()
        {
            var hash = default(HashCode);
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
        public Matrix4x4Fix16 Invert() =>
            Invert(this);

        public override string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} " +
            $"{{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 Transpose() =>
            Transpose(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 With(Fix16? m11 = null, Fix16? m12 = null, Fix16? m13 = null, Fix16? m14 = null,
                                   Fix16? m21 = null, Fix16? m22 = null, Fix16? m23 = null, Fix16? m24 = null,
                                   Fix16? m31 = null, Fix16? m32 = null, Fix16? m33 = null, Fix16? m34 = null,
                                   Fix16? m41 = null, Fix16? m42 = null, Fix16? m43 = null, Fix16? m44 = null) =>
            new Matrix4x4Fix16(m11 ?? M11, m12 ?? M12, m13 ?? M13, m14 ?? M14,
                               m21 ?? M21, m22 ?? M22, m23 ?? M23, m24 ?? M24,
                               m31 ?? M31, m32 ?? M32, m33 ?? M33, m34 ?? M34,
                               m41 ?? M41, m42 ?? M42, m43 ?? M43, m44 ?? M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix4x4Fix16 WithTranslation(Vector3Fix16 value) =>
            With(m41: value.X, m42: value.Y, m43: value.Z);

        internal Matrix3x3Fix16 As3x3() =>
            new Matrix3x3Fix16(M11, M12, M13, M21, M22, M23, M31, M32, M33);

        //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        //private static Vector128<float> Permute(Vector128<float> value, byte control)
        //{
        //    if (!Avx.IsSupported)
        //        return Sse.Shuffle(value, value, control);
        //    return Avx.Permute(value, control);
        //}
    }
}
