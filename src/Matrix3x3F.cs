using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    public struct Matrix3x3F : IEquatable<Matrix3x3F>
    {
        #region Public Fields

        public float M11;
        public float M12;
        public float M13;
        public float M21;
        public float M22;
        public float M23;
        public float M31;
        public float M32;
        public float M33;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;
        private static readonly Matrix3x3F _identity = new Matrix3x3F(1, 0, 0, 0, 1, 0, 0, 0, 1);
        private static readonly Matrix3x3F _nan = new Matrix3x3F(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);

        #endregion Private Fields

        #region Public Constructors

        [MethodImpl(Optimize)]
        public Matrix3x3F(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
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

        #endregion Public Constructors

        #region Public Properties

        public static Matrix3x3F Identity => _identity;
        public static Matrix3x3F NaN => _nan;

        public readonly bool IsIdentity =>
            this == _identity;

        #endregion Public Properties

        #region Public Methods

        [MethodImpl(Optimize)]
        public static Matrix3x3F Add(Matrix3x3F left, Matrix3x3F right) =>
            left + right;

        [MethodImpl(Optimize)]
        public static Matrix3x3F Invert(Matrix3x3F matrix)
        {
            var det = matrix.GetDeterminant();
            if (MathF.Abs(det) < float.Epsilon)
                return _nan;

            var invdet = 1 / det;

            return new Matrix3x3F((matrix.M22 * matrix.M33 - matrix.M32 * matrix.M23) * invdet,
                                  (matrix.M13 * matrix.M32 - matrix.M12 * matrix.M33) * invdet,
                                  (matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22) * invdet,
                                  (matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33) * invdet,
                                  (matrix.M11 * matrix.M33 - matrix.M13 * matrix.M31) * invdet,
                                  (matrix.M21 * matrix.M13 - matrix.M11 * matrix.M23) * invdet,
                                  (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * invdet,
                                  (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * invdet,
                                  (matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12) * invdet);
        }

        [MethodImpl(Optimize)]
        public unsafe static Matrix3x3F Lerp(Matrix3x3F matrix1, Matrix3x3F matrix2, float amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            if (Sse.IsSupported)
                return Matrix4x4F.Lerp(m1.As4x4(), m2.As4x4(), amount).As3x3();
            return new Matrix3x3F(m1.M11 + (m2.M11 - m1.M11) * amount,
                                  m1.M12 + (m2.M12 - m1.M12) * amount,
                                  m1.M13 + (m2.M13 - m1.M13) * amount,
                                  m1.M21 + (m2.M21 - m1.M21) * amount,
                                  m1.M22 + (m2.M22 - m1.M22) * amount,
                                  m1.M23 + (m2.M23 - m1.M23) * amount,
                                  m1.M31 + (m2.M31 - m1.M31) * amount,
                                  m1.M32 + (m2.M32 - m1.M32) * amount,
                                  m1.M33 + (m2.M33 - m1.M33) * amount);
        }

        [MethodImpl(Optimize)]
        public static Matrix3x3F Multiply(Matrix3x3F left, Matrix3x3F right) =>
            left * right;

        [MethodImpl(Optimize)]
        public static Matrix3x3F Multiply(Matrix3x3F left, float right) =>
            left * right;

        [MethodImpl(Optimize)]
        public static Matrix3x3F Negate(Matrix3x3F value) =>
            -value;

        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3F operator -(Matrix3x3F value)
        {
            if (Sse.IsSupported)
                return (-value.As4x4()).As3x3();
            else
                return new Matrix3x3F(-value.M11, -value.M12, -value.M13,
                                      -value.M21, -value.M22, -value.M23,
                                      -value.M31, -value.M32, -value.M33);
        }

        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3F operator -(Matrix3x3F left, Matrix3x3F right)
        {
            if (Sse.IsSupported)
                return (left.As4x4() - right.As4x4()).As3x3();
            else
                return new Matrix3x3F(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
                                      left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
                                      left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33);
        }

        [MethodImpl(Optimize)]
        public static bool operator !=(Matrix3x3F left, Matrix3x3F right)
        {
            if (Sse.IsSupported)
                return left.As4x4() != right.As4x4();
            else
                return left.M11 != right.M11 || left.M12 != right.M12 || left.M13 != right.M13 ||
                       left.M21 != right.M21 || left.M22 != right.M22 || left.M23 != right.M23 ||
                       left.M31 != right.M31 || left.M32 != right.M32 || left.M33 != right.M33;
        }

        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3F operator *(Matrix3x3F left, Matrix3x3F right)
        {
            var result = new Matrix3x3F();

            if (Sse.IsSupported)
                result = (left.As4x4() * right.As4x4()).As3x3();
            else
            {
                result.M11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31;
                result.M12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32;
                result.M13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33;
                result.M21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31;
                result.M22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32;
                result.M23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33;
                result.M31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31;
                result.M32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32;
                result.M33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33;
            }

            return result;
        }

        [MethodImpl(Optimize)]
        public static unsafe Vector3F operator *(Matrix3x3F left, Vector3F right)
        {
            return new Vector3F(left.M11 * right.X + left.M12 * right.Y + left.M13 * right.Z,
                                left.M21 * right.X + left.M22 * right.Y + left.M23 * right.Z,
                                left.M31 * right.X + left.M32 * right.Y + left.M33 * right.Z);
        }

        [MethodImpl(Optimize)]
        public static Vector3F operator *(Vector3F left, Matrix3x3F right) =>
            right * left;

        [MethodImpl(Optimize)]
        public static unsafe Vector4F operator *(Matrix3x3F left, Vector4F right)
        {
            (var v, _) = right;
            return new Vector4F(left * v, right.W);
        }

        [MethodImpl(Optimize)]
        public static Vector4F operator *(Vector4F left, Matrix3x3F right) =>
            right * left;

        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3F operator *(Matrix3x3F left, float right)
        {
            if (Sse.IsSupported)
                return (left.As4x4() * right).As3x3();
            else
                return new Matrix3x3F(left.M11 * right, left.M12 * right, left.M13 * right,
                                      left.M21 * right, left.M22 * right, left.M23 * right,
                                      left.M31 * right, left.M32 * right, left.M33 * right);
        }

        [MethodImpl(Optimize)]
        public static Matrix3x3F operator +(Matrix3x3F value) =>
            value;

        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3F operator +(Matrix3x3F left, Matrix3x3F right)
        {
            if (Sse.IsSupported)
                return (left.As4x4() + right.As4x4()).As3x3();
            else
                return new Matrix3x3F(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
                                      left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23,
                                      left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33);
        }

        [MethodImpl(Optimize)]
        public static unsafe bool operator ==(Matrix3x3F left, Matrix3x3F right)
        {
            if (Sse.IsSupported)
                return left.As4x4() == right.As4x4();
            else
                return left.M11 == right.M11 && left.M12 == right.M12 && left.M13 == right.M13 &&
                       left.M21 == right.M21 && left.M22 == right.M22 && left.M23 == right.M23 &&
                       left.M31 == right.M31 && left.M32 == right.M32 && left.M33 == right.M33;
        }

        [MethodImpl(Optimize)]
        public static Matrix3x3F Subtract(Matrix3x3F left, Matrix3x3F right) =>
            left - right;

        [MethodImpl(Optimize)]
        public static Matrix3x3F Transpose(Matrix3x3F matrix)
        {
            if (Sse.IsSupported)
                return Matrix4x4F.Transpose(matrix.As4x4()).As3x3();
            return new Matrix3x3F(matrix.M11, matrix.M21, matrix.M31,
                                  matrix.M12, matrix.M22, matrix.M32,
                                  matrix.M13, matrix.M23, matrix.M33);
        }

        [MethodImpl(Optimize)]
        public bool Equals([AllowNull] Matrix3x3F other) =>
            this == other;

        [MethodImpl(Optimize)]
        public override bool Equals(object? obj) =>
            obj is Matrix3x3F matrix && this == matrix;

        [MethodImpl(Optimize)]
        public float GetDeterminant() =>
            M11 * (M22 * M33 - M32 * M23) -
            M12 * (M21 * M33 - M23 * M31) +
            M13 * (M21 * M32 - M22 * M31);

        public override int GetHashCode()
        {
            var hash = new HashCode();
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

        [MethodImpl(Optimize)]
        public Matrix3x3F Invert() =>
            Invert(this);

        public override string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13}}} {{M21:{M21} M22:{M22} M23:{M23}}} {{M31:{M31} M32:{M32} M33:{M33}}} }}";

        [MethodImpl(Optimize)]
        public Matrix3x3F With(float? m11 = null, float? m12 = null, float? m13 = null, float? m21 = null, float? m22 = null, float? m23 = null, float? m31 = null, float? m32 = null, float? m33 = null) =>
            new Matrix3x3F(m11 ?? M11, m12 ?? M12, m13 ?? M13, m21 ?? M21, m22 ?? M22, m23 ?? M23, m31 ?? M31, m32 ?? M32, m33 ?? M33);

        #endregion Public Methods

        #region Internal Methods

        internal Matrix4x4F As4x4() =>
            Matrix4x4F.Identity.With(m11: M11, m12: M12, m13: M13, m21: M21, m22: M22, m23: M23, m31: M31, m32: M32, m33: M33);

        #endregion Internal Methods
    }
}