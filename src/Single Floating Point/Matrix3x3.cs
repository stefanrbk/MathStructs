using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//using System.Runtime.Intrinsics.X86;

namespace System.Numerics
{
    /// <summary>
    /// A structure encapsulating a 3x3 matrix of <see cref="Single"/> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct Matrix3x3 : IEquatable<Matrix3x3>
    {
        #region Public Fields

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [FieldOffset(0)]
        public float M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [FieldOffset(4)]
        public float M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [FieldOffset(8)]
        public float M13;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [FieldOffset(12)]
        public float M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [FieldOffset(16)]
        public float M22;

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [FieldOffset(20)]
        public float M23;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [FieldOffset(24)]
        public float M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [FieldOffset(28)]
        public float M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [FieldOffset(32)]
        public float M33;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;
        private static readonly Matrix3x3 _identity = new Matrix3x3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        private static readonly Matrix3x3 _nan = new Matrix3x3(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a <see cref="Matrix3x3"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix3x3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
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

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix3x3 Identity => _identity;

        /// <summary>
        /// Returns a matrix with all values set to NaN.
        /// </summary>
        public static Matrix3x3 NaN => _nan;

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public readonly bool IsIdentity =>
            this == _identity;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Add(Matrix3x3 left, Matrix3x3 right) =>
            left + right;

        /// <summary>
        ///     Copies the contents of the matrix into the given span.
        /// </summary>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<float> span) =>
            CopyTo(span, 0);

        /// <summary>
        ///     Copies the contents of the matrix into the given span, starting from index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If index is greater than end of the span or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If number of elements in source matrix is greater than those available in destination span.
        /// </exception>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<float> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 16)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index +  0] = M11;
            span[index +  1] = M12;
            span[index +  2] = M13;
            span[index +  3] = M21;
            span[index +  4] = M22;
            span[index +  5] = M23;
            span[index +  6] = M31;
            span[index +  7] = M32;
            span[index +  8] = M33;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateTranslation(float xPosition, float yPosition) =>
            (Matrix3x3)Matrix3x2.CreateTranslation(xPosition, yPosition);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateTranslation(Vector2 position) =>
            CreateTranslation(position.X, position.Y);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(float xScale, float yScale) =>
            (Matrix3x3)Matrix3x2.CreateScale(xScale, yScale);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(float scale) =>
            CreateScale(scale, scale);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(Vector2 scales) =>
            CreateScale(scales.X, scales.Y);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(float xScale, float yScale, Vector2 centerPoint) =>
            (Matrix3x3)Matrix3x2.CreateScale(xScale, yScale, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(float scale, Vector2 centerPoint) =>
            CreateScale(scale, scale, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateScale(Vector2 scales, Vector2 centerPoint) =>
            CreateScale(scales.X, scales.Y, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateSkew(float radiansX, float radiansY) =>
            (Matrix3x3)Matrix3x2.CreateSkew(radiansX, radiansY);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateSkew(float radiansX, float radiansY, Vector2 centerPoint) =>
            (Matrix3x3)Matrix3x2.CreateSkew(radiansX, radiansY, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateRotation(float radians) =>
            (Matrix3x3)Matrix3x2.CreateRotation(radians);

        /// <summary>
        /// </summary>
        public static Matrix3x3 CreateRotation(float radians, Vector2 centerPoint) =>
            (Matrix3x3)Matrix3x2.CreateRotation(radians, centerPoint);

        /// <summary>
        /// </summary>
        public static explicit operator Matrix3x3(Matrix3x2 matrix) =>
            new Matrix3x3(matrix.M11, matrix.M12, 0,
                           matrix.M21, matrix.M22, 0,
                           matrix.M31, matrix.M32, 1);

        /// <summary>
        /// </summary>
        public static explicit operator Matrix3x2(Matrix3x3 matrix) =>
            new Matrix3x2(matrix.M11, matrix.M12,
                           matrix.M21, matrix.M22,
                           matrix.M31, matrix.M32);

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, the result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix to invert.
        /// </param>
        /// <returns>
        /// If successful, the inverted matrix; NaN matrix otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static bool Invert(Matrix3x3 matrix, out Matrix3x3 result)
        {
            result = _nan;
            var det = matrix.GetDeterminant();
            if (MathF.Abs(det) < float.Epsilon)
                return false;

            var invdet = 1 / det;

            result = new Matrix3x3((matrix.M22 * matrix.M33 - matrix.M32 * matrix.M23) * invdet,
                                  (matrix.M13 * matrix.M32 - matrix.M12 * matrix.M33) * invdet,
                                  (matrix.M12 * matrix.M23 - matrix.M13 * matrix.M22) * invdet,
                                  (matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33) * invdet,
                                  (matrix.M11 * matrix.M33 - matrix.M13 * matrix.M31) * invdet,
                                  (matrix.M21 * matrix.M13 - matrix.M11 * matrix.M23) * invdet,
                                  (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * invdet,
                                  (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * invdet,
                                  (matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12) * invdet);
            return true;
        }

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">
        /// The first source matrix.
        /// </param>
        /// <param name="matrix2">
        /// The second source matrix.
        /// </param>
        /// <param name="amount">
        /// The relative weight of the second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix3x3 Lerp(Matrix3x3 matrix1, Matrix3x3 matrix2, float amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            //if (Sse.IsSupported)
            //    return Matrix4x4F.Lerp(m1.As4x4(), m2.As4x4(), amount).As3x3();
            return new Matrix3x3(m1.M11 + (m2.M11 - m1.M11) * amount,
                                  m1.M12 + (m2.M12 - m1.M12) * amount,
                                  m1.M13 + (m2.M13 - m1.M13) * amount,
                                  m1.M21 + (m2.M21 - m1.M21) * amount,
                                  m1.M22 + (m2.M22 - m1.M22) * amount,
                                  m1.M23 + (m2.M23 - m1.M23) * amount,
                                  m1.M31 + (m2.M31 - m1.M31) * amount,
                                  m1.M32 + (m2.M32 - m1.M32) * amount,
                                  m1.M33 + (m2.M33 - m1.M33) * amount);
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Multiply(Matrix3x3 left, Matrix3x3 right) =>
            left * right;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Multiply(Matrix3x3 left, float right) =>
            left * right;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Negate(Matrix3x3 value) =>
            -value;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3 operator -(Matrix3x3 value)
        {
            //if (Sse.IsSupported)
            //    return (-value.As4x4()).As3x3();
            //else
                return new Matrix3x3(-value.M11, -value.M12, -value.M13,
                                      -value.M21, -value.M22, -value.M23,
                                      -value.M31, -value.M32, -value.M33);
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3 operator -(Matrix3x3 left, Matrix3x3 right)
        {
            //if (Sse.IsSupported)
            //    return (left.As4x4() - right.As4x4()).As3x3();
            //else
                return new Matrix3x3(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
                                      left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23,
                                      left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are not equal.
        /// </summary>
        /// <param name="left">
        /// The first matrix to compare.
        /// </param>
        /// <param name="right">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are not equal; False if they are equal.
        /// </returns>
        [MethodImpl(Optimize)]
        public static bool operator !=(Matrix3x3 left, Matrix3x3 right)
        {
            //if (Sse.IsSupported)
            //    return left.As4x4() != right.As4x4();
            //else
                return left.M11 != right.M11 || left.M12 != right.M12 || left.M13 != right.M13 ||
                       left.M21 != right.M21 || left.M22 != right.M22 || left.M23 != right.M23 ||
                       left.M31 != right.M31 || left.M32 != right.M32 || left.M33 != right.M33;
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3 operator *(Matrix3x3 left, Matrix3x3 right)
        {
            var result = new Matrix3x3();

            //if (Sse.IsSupported)
            //    result = (left.As4x4() * right.As4x4()).As3x3();
            //else
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

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The source vector.
        /// </param>
        /// <remarks>
        /// Multiplying a 3x3 matrix by a 3x1 vector is supposed to result in a 1x3 vector. <see cref="Vector3"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 3x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static unsafe Vector3 operator *(Matrix3x3 left, Vector3 right)
        {
            return new Vector3(left.M11 * right.X + left.M12 * right.Y + left.M13 * right.Z,
                               left.M21 * right.X + left.M22 * right.Y + left.M23 * right.Z,
                               left.M31 * right.X + left.M32 * right.Y + left.M33 * right.Z);
        }

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="left">
        /// The source vector.
        /// </param>
        /// <param name="right">
        /// The source matrix.
        /// </param>
        /// <remarks>
        /// Multiplying a 3x3 matrix by a 3x1 vector is supposed to result in a 1x3 vector. <see cref="Vector3"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 3x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static Vector3 operator *(Vector3 left, Matrix3x3 right) =>
            right * left;

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The source vector.
        /// </param>
        /// <remarks>
        /// In a strictly mathematical sense, this method pretends the Matrix3x3D is actually a Matrix4x4D with the extra
        /// cells populated like the identity matrix. Effectively, this multiplies the 3x3 matrix with the XYZ of the 4x1
        /// vector and pulls the W through as the new W.
        /// Multiplying a 4x4 matrix by a 4x1 vector is supposed to result in a 1x4 vector. <see cref="Vector4"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 4x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static unsafe Vector4 operator *(Matrix3x3 left, Vector4 right)
        {
            (var v, _) = right;
            return new Vector4(left * v, right.W);
        }

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The source vector.
        /// </param>
        /// <remarks>
        /// In a strictly mathematical sense, this method pretends the Matrix3x3 is actually a Matrix4x4 with the extra
        /// cells populated like the identity matrix. Effectively, this multiplies the 3x3 matrix with the XYZ of the 4x1
        /// vector and pulls the W through as the new W.
        /// Multiplying a 4x4 matrix by a 4x1 vector is supposed to result in a 1x4 vector. <see cref="Vector4"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 4x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static Vector4 operator *(Vector4 left, Matrix3x3 right) =>
            right * left;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3 operator *(Matrix3x3 left, float right)
        {
            //if (Sse.IsSupported)
            //    return (left.As4x4() * right).As3x3();
            //else
                return new Matrix3x3(left.M11 * right, left.M12 * right, left.M13 * right,
                                      left.M21 * right, left.M22 * right, left.M23 * right,
                                      left.M31 * right, left.M32 * right, left.M33 * right);
        }

        /// <summary>
        /// Returns itself. (nop)
        /// </summary>
        [MethodImpl(Optimize)]
        public static Matrix3x3 operator +(Matrix3x3 value) =>
            value;

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3 operator +(Matrix3x3 left, Matrix3x3 right)
        {
            //if (Sse.IsSupported)
            //    return (left.As4x4() + right.As4x4()).As3x3();
            //else
                return new Matrix3x3(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
                                      left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23,
                                      left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are equal.
        /// </summary>
        /// <param name="left">
        /// The first matrix to compare.
        /// </param>
        /// <param name="right">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static unsafe bool operator ==(Matrix3x3 left, Matrix3x3 right)
        {
            //if (Sse.IsSupported)
            //    return left.As4x4() == right.As4x4();
            //else
                return left.M11 == right.M11 && left.M12 == right.M12 && left.M13 == right.M13 &&
                       left.M21 == right.M21 && left.M22 == right.M22 && left.M23 == right.M23 &&
                       left.M31 == right.M31 && left.M32 == right.M32 && left.M33 == right.M33;
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Subtract(Matrix3x3 left, Matrix3x3 right) =>
            left - right;

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3 Transpose(Matrix3x3 matrix)
        {
            //if (Sse.IsSupported)
            //    return Matrix4x4F.Transpose(matrix.As4x4()).As3x3();
            return new Matrix3x3(matrix.M11, matrix.M21, matrix.M31,
                                  matrix.M12, matrix.M22, matrix.M32,
                                  matrix.M13, matrix.M23, matrix.M33);
        }

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to compare this instance to.
        /// </param>
        /// <returns>
        /// True if the matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public bool Equals([AllowNull] Matrix3x3 other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">
        /// The Object to compare against.
        /// </param>
        /// <returns>
        /// True if the Object is equal to this matrix; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public override bool Equals(object? obj) =>
            obj is Matrix3x3 matrix && this == matrix;

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        [MethodImpl(Optimize)]
        public float GetDeterminant() =>
            M11 * (M22 * M33 - M32 * M23) -
            M12 * (M21 * M33 - M23 * M31) +
            M13 * (M21 * M32 - M22 * M31);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
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

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        public override string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13}}} {{M21:{M21} M22:{M22} M23:{M23}}} {{M31:{M31} M32:{M32} M33:{M33}}} }}";
        #endregion Public Methods

        #region Internal Methods

        internal Matrix4x4 As4x4()
        {
            var result = Matrix4x4.Identity;

            result.M11 = M11;
            result.M12 = M12;
            result.M13 = M13;
            result.M21 = M21;
            result.M22 = M22;
            result.M23 = M23;
            result.M31 = M31;
            result.M32 = M32;
            result.M33 = M33;

            return result;
        }

        #endregion Internal Methods
    }
}