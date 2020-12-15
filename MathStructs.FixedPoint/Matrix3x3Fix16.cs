using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Matrix3x3Fix16 : IEquatable<Matrix3x3Fix16>
    {
        [FieldOffset(0)]
        public readonly Fix16 M11;

        [FieldOffset(4)]
        public readonly Fix16 M12;

        [FieldOffset(8)]
        public readonly Fix16 M13;

        [FieldOffset(12)]
        public readonly Fix16 M21;

        [FieldOffset(16)]
        public readonly Fix16 M22;

        [FieldOffset(20)]
        public readonly Fix16 M23;

        [FieldOffset(24)]
        public readonly Fix16 M31;

        [FieldOffset(28)]
        public readonly Fix16 M32;

        [FieldOffset(32)]
        public readonly Fix16 M33;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        private const MethodImplOptions Optimize = MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization;

        public static Matrix3x3Fix16 Identity => new Matrix3x3Fix16(Fix16.One, Fix16.Zero, Fix16.Zero,
                                                                    Fix16.Zero, Fix16.One, Fix16.Zero,
                                                                    Fix16.Zero, Fix16.Zero, Fix16.One);

        public static Matrix3x3Fix16 Zero => new Matrix3x3Fix16(Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                Fix16.Zero, Fix16.Zero, Fix16.Zero,
                                                                Fix16.Zero, Fix16.Zero, Fix16.Zero);

        public bool IsIdentity => this == Identity;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix3x3Fix16(Fix16 m11, Fix16 m12, Fix16 m13, Fix16 m21, Fix16 m22, Fix16 m23, Fix16 m31, Fix16 m32, Fix16 m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Add(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
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
            span[index + 3] = M21;
            span[index + 4] = M22;
            span[index + 5] = M23;
            span[index + 6] = M31;
            span[index + 7] = M32;
            span[index + 8] = M33;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Invert(Matrix3x3Fix16 matrix)
        {
            var det = matrix.GetDeterminant();
            if (det == Fix16.Zero)
                return Zero;
            var invdet = Fix16.One / det;
            return new Matrix3x3Fix16(( ( matrix.M22 * matrix.M33 ) - ( matrix.M32 * matrix.M23 ) ) * invdet,
                                      ( ( matrix.M13 * matrix.M32 ) - ( matrix.M12 * matrix.M33 ) ) * invdet,
                                      ( ( matrix.M12 * matrix.M23 ) - ( matrix.M13 * matrix.M22 ) ) * invdet,
                                      ( ( matrix.M23 * matrix.M31 ) - ( matrix.M21 * matrix.M33 ) ) * invdet,
                                      ( ( matrix.M11 * matrix.M33 ) - ( matrix.M13 * matrix.M31 ) ) * invdet,
                                      ( ( matrix.M21 * matrix.M13 ) - ( matrix.M11 * matrix.M23 ) ) * invdet,
                                      ( ( matrix.M21 * matrix.M32 ) - ( matrix.M31 * matrix.M22 ) ) * invdet,
                                      ( ( matrix.M31 * matrix.M12 ) - ( matrix.M11 * matrix.M32 ) ) * invdet,
                                      ( ( matrix.M11 * matrix.M22 ) - ( matrix.M21 * matrix.M12 ) ) * invdet);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Lerp(Matrix3x3Fix16 matrix1, Matrix3x3Fix16 matrix2, Fix16 amount)
        {
            var m1 = matrix1;
            var m2 = matrix2;
            //if (Sse.IsSupported)
            //    return Matrix4x4Fix16.Lerp(m1.As4x4(), m2.As4x4(), amount).As3x3();
            return new Matrix3x3Fix16(m1.M11 + ( ( m2.M11 - m1.M11 ) * amount ),
                                      m1.M12 + ( ( m2.M12 - m1.M12 ) * amount ),
                                      m1.M13 + ( ( m2.M13 - m1.M13 ) * amount ),
                                      m1.M21 + ( ( m2.M21 - m1.M21 ) * amount ),
                                      m1.M22 + ( ( m2.M22 - m1.M22 ) * amount ),
                                      m1.M23 + ( ( m2.M23 - m1.M23 ) * amount ),
                                      m1.M31 + ( ( m2.M31 - m1.M31 ) * amount ),
                                      m1.M32 + ( ( m2.M32 - m1.M32 ) * amount ),
                                      m1.M33 + ( ( m2.M33 - m1.M33 ) * amount ));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Multiply(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Multiply(Matrix3x3Fix16 left, Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Negate(Matrix3x3Fix16 value) =>
            -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator -(Matrix3x3Fix16 value) =>
            //if (Sse.IsSupported)
            //    return ( -value.As4x4() ).As3x3();
            new Matrix3x3Fix16(-value.M11, -value.M12, -value.M13,
                               -value.M21, -value.M22, -value.M23,
                               -value.M31, -value.M32, -value.M33);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator -(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            //if (Sse.IsSupported)
            //    return ( left.As4x4() - right.As4x4() ).As3x3();
            new Matrix3x3Fix16(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
                               left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
                               left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator !=(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            //if (Sse.IsSupported)
            //    return left.As4x4() != right.As4x4();
            left.M11 != right.M11 || left.M22 != right.M22 || left.M33 != right.M33 ||
            left.M12 != right.M12 || left.M13 != right.M13 || left.M21 != right.M21 ||
            left.M23 != right.M23 || left.M31 != right.M31 || left.M32 != right.M32;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator *(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            //if (Sse.IsSupported)
            //    return ( left.As4x4() * right.As4x4() ).As3x3();
            new Matrix3x3Fix16(( left.M11 * right.M11 ) + ( left.M12 * right.M21 ) + ( left.M13 * right.M31 ),
                               ( left.M11 * right.M12 ) + ( left.M12 * right.M22 ) + ( left.M13 * right.M32 ),
                               ( left.M11 * right.M13 ) + ( left.M12 * right.M23 ) + ( left.M13 * right.M33 ),
                               ( left.M21 * right.M11 ) + ( left.M22 * right.M21 ) + ( left.M23 * right.M31 ),
                               ( left.M21 * right.M12 ) + ( left.M22 * right.M22 ) + ( left.M23 * right.M32 ),
                               ( left.M21 * right.M13 ) + ( left.M22 * right.M23 ) + ( left.M23 * right.M33 ),
                               ( left.M31 * right.M11 ) + ( left.M32 * right.M21 ) + ( left.M33 * right.M31 ),
                               ( left.M31 * right.M12 ) + ( left.M32 * right.M22 ) + ( left.M33 * right.M32 ),
                               ( left.M31 * right.M13 ) + ( left.M32 * right.M23 ) + ( left.M33 * right.M33 ));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector3Fix16 operator *(Matrix3x3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(( left.M11 * right.X ) + ( left.M12 * right.Y ) + ( left.M13 * right.Z ),
                             ( left.M21 * right.X ) + ( left.M22 * right.Y ) + ( left.M23 * right.Z ),
                             ( left.M31 * right.X ) + ( left.M32 * right.Y ) + ( left.M33 * right.Z ));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, Matrix3x3Fix16 right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector4Fix16 operator *(Matrix3x3Fix16 left, Vector4Fix16 right)
        {
            right.Deconstruct(out var vector, out var _);
            return new Vector4Fix16(( left * vector ), right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, Matrix3x3Fix16 right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator *(Matrix3x3Fix16 left, Fix16 right) =>
            //if (Sse.IsSupported)
            //    return ( left.As4x4() * right ).As3x3();
            new Matrix3x3Fix16(left.M11 * right, left.M12 * right, left.M13 * right,
                               left.M21 * right, left.M22 * right, left.M23 * right,
                               left.M31 * right, left.M32 * right, left.M33 * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator +(Matrix3x3Fix16 value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 operator +(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            //if (Sse.IsSupported)
            //    return ( left.As4x4() + right.As4x4() ).As3x3();
            new Matrix3x3Fix16(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
                               left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23,
                               left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool operator ==(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            //if (Sse.IsSupported)
            //    return left.As4x4() == right.As4x4();
            left.M11 == right.M11 && left.M12 == right.M12 && left.M13 == right.M13 &&
            left.M21 == right.M21 && left.M22 == right.M22 && left.M23 == right.M23 &&
            left.M31 == right.M31 && left.M32 == right.M32 && left.M33 == right.M33;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Subtract(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Matrix3x3Fix16 Transpose(Matrix3x3Fix16 matrix) =>
            //if (Sse.IsSupported)
            //    return Matrix4x4Fix16.Transpose(matrix.As4x4()).As3x3();
            new Matrix3x3Fix16(matrix.M11, matrix.M21, matrix.M31,
                               matrix.M12, matrix.M22, matrix.M32,
                               matrix.M13, matrix.M23, matrix.M33);

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool Equals([AllowNull] Matrix3x3Fix16 other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public override bool Equals(object? obj) =>
            ( obj is Matrix3x3Fix16 matrix ) && this == matrix;

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Fix16 GetDeterminant() =>
            ( M11 * ( ( M22 * M33 ) - ( M32 * M23 ) ) ) - ( M12 * ( ( M21 * M33 ) - ( M23 * M31 ) ) ) + ( M13 * ( ( M21 * M32 ) - ( M22 * M31 ) ) );

        public override int GetHashCode()
        {
            var hash = default(HashCode);
            hash.Add(M11);
            hash.Add(M12);
            hash.Add(M13);
            hash.Add(M21);
            hash.Add(M22);
            hash.Add(M23);
            hash.Add(M31);
            hash.Add(M32);
            hash.Add(M33);
            return hash.ToHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix3x3Fix16 Invert() =>
            Invert(this);

        public override string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13}}} {{M21:{M21} M22:{M22} M23:{M23}}} {{M31:{M31} M32:{M32} M33:{M33}}} }}";

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public Matrix3x3Fix16 With(Fix16? m11 = null, Fix16? m12 = null, Fix16? m13 = null,
                                   Fix16? m21 = null, Fix16? m22 = null, Fix16? m23 = null,
                                   Fix16? m31 = null, Fix16? m32 = null, Fix16? m33 = null) =>
            new Matrix3x3Fix16(m11 ?? M11, m12 ?? M12, m13 ?? M13,
                               m21 ?? M21, m22 ?? M22, m23 ?? M23,
                               m31 ?? M31, m32 ?? M32, m33 ?? M33);

        internal Matrix4x4Fix16 As4x4() =>
            Matrix4x4Fix16.Identity.With(m11: M11, m12: M12, m13: M13,
                                         m21: M21, m22: M22, m23: M23,
                                         m31: M31, m32: M32, m33: M33);
    }
}
