using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating a 3x3 matrix of <see cref="int"/> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Matrix3x3Fix16 : IEquatable<Matrix3x3Fix16>
    {
#pragma warning disable IDE0024 // Use expression body for operators
#pragma warning disable IDE0045 // Convert to conditional expression
        #region Public Fields

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [FieldOffset(0)]
        public readonly int M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [FieldOffset(4)]
        public readonly int M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [FieldOffset(8)]
        public readonly int M13;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [FieldOffset(12)]
        public readonly int M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [FieldOffset(16)]
        public readonly int M22;

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [FieldOffset(20)]
        public readonly int M23;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [FieldOffset(24)]
        public readonly int M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [FieldOffset(28)]
        public readonly int M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [FieldOffset(32)]
        public readonly int M33;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a <see cref="Matrix3x3Fix16"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix3x3Fix16(int m11, int m12, int m13, int m21, int m22, int m23, int m31, int m32, int m33)
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

        /// <summary>
        /// Constructs a <see cref="Matrix3x3Fix16"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix3x3Fix16(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
            : this((int)m11, (int)m12, (int)m13, (int)m21, (int)m22, (int)m23, (int)m31, (int)m32, (int)m33) { }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix3x3Fix16 Identity => new Matrix3x3Fix16(1.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 1.0);

        /// <summary>
        /// Returns a matrix with all values set to zero.
        /// </summary>
        public static Matrix3x3Fix16 Zero => new Matrix3x3Fix16(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public readonly bool IsIdentity =>
            this == Identity;

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
        public static Matrix3x3Fix16 Add(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            left + right;

        /// <summary>
        ///     Copies the contents of the matrix into the given span.
        /// </summary>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<int> span) =>
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
        public void CopyTo(Span<int> span, int index)
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
        /// Attempts to calculate the inverse of the given matrix. If successful, the result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix to invert.
        /// </param>
        /// <returns>
        /// If successful, the inverted matrix; NaN matrix otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static Matrix3x3Fix16 Invert(Matrix3x3Fix16 matrix)
        {
            var det = matrix.GetDeterminant();
            if (det == 0)
                return Zero;

            var invdet = 1 / det;

            return new Matrix3x3Fix16((( matrix.M22 * matrix.M33 ) - ( matrix.M32 * matrix.M23 )) * invdet,
                                           (( matrix.M13 * matrix.M32 ) - ( matrix.M12 * matrix.M33 )) * invdet,
                                           (( matrix.M12 * matrix.M23 ) - ( matrix.M13 * matrix.M22 )) * invdet,
                                           (( matrix.M23 * matrix.M31 ) - ( matrix.M21 * matrix.M33 )) * invdet,
                                           (( matrix.M11 * matrix.M33 ) - ( matrix.M13 * matrix.M31 )) * invdet,
                                           (( matrix.M21 * matrix.M13 ) - ( matrix.M11 * matrix.M23 )) * invdet,
                                           (( matrix.M21 * matrix.M32 ) - ( matrix.M31 * matrix.M22 )) * invdet,
                                           (( matrix.M31 * matrix.M12 ) - ( matrix.M11 * matrix.M32 )) * invdet,
                                           (( matrix.M11 * matrix.M22 ) - ( matrix.M21 * matrix.M12 )) * invdet);
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
        public unsafe static Matrix3x3Fix16 Lerp(Matrix3x3Fix16 matrix1, Matrix3x3Fix16 matrix2, int amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
#if !NOSIMD
            if (Sse.IsSupported)
                return Matrix4x4Fix16.Lerp(m1.As4x4(), m2.As4x4(), amount).As3x3();
#endif
            return new Matrix3x3Fix16(m1.M11 + (( m2.M11 - m1.M11) * amount ),
                                           m1.M12 + (( m2.M12 - m1.M12) * amount ),
                                           m1.M13 + (( m2.M13 - m1.M13) * amount ),
                                           m1.M21 + (( m2.M21 - m1.M21) * amount ),
                                           m1.M22 + (( m2.M22 - m1.M22) * amount ),
                                           m1.M23 + (( m2.M23 - m1.M23) * amount ),
                                           m1.M31 + (( m2.M31 - m1.M31) * amount ),
                                           m1.M32 + (( m2.M32 - m1.M32) * amount ),
                                           m1.M33 + (( m2.M33 - m1.M33) * amount ));
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
        public static Matrix3x3Fix16 Multiply(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
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
        public static Matrix3x3Fix16 Multiply(Matrix3x3Fix16 left, int right) =>
            left * right;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3Fix16 Negate(Matrix3x3Fix16 value) =>
            -value;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static unsafe Matrix3x3Fix16 operator -(Matrix3x3Fix16 value)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return (-value.As4x4()).As3x3();
            else
#endif
                return new Matrix3x3Fix16(-value.M11, -value.M12, -value.M13,
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
        public static unsafe Matrix3x3Fix16 operator -(Matrix3x3Fix16 left, Matrix3x3Fix16 right)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return (left.As4x4() - right.As4x4()).As3x3();
            else
#endif
                return new Matrix3x3Fix16(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13,
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
        public static bool operator !=(Matrix3x3Fix16 left, Matrix3x3Fix16 right)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return left.As4x4() != right.As4x4();
            else
#endif
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
        public static unsafe Matrix3x3Fix16 operator *(Matrix3x3Fix16 left, Matrix3x3Fix16 right)
        {
            Matrix3x3Fix16 result;
#if !NOSIMD
            if (Sse.IsSupported)
                result = (left.As4x4() * right.As4x4()).As3x3();
            else
#endif
            {
                result = new Matrix3x3Fix16(( left.M11 * right.M11 ) + ( left.M12 * right.M21 ) + ( left.M13 * right.M31 ),
                                            ( left.M11 * right.M12 ) + ( left.M12 * right.M22 ) + ( left.M13 * right.M32 ),
                                            ( left.M11 * right.M13 ) + ( left.M12 * right.M23 ) + ( left.M13 * right.M33 ),
                                            ( left.M21 * right.M11 ) + ( left.M22 * right.M21 ) + ( left.M23 * right.M31 ),
                                            ( left.M21 * right.M12 ) + ( left.M22 * right.M22 ) + ( left.M23 * right.M32 ),
                                            ( left.M21 * right.M13 ) + ( left.M22 * right.M23 ) + ( left.M23 * right.M33 ),
                                            ( left.M31 * right.M11 ) + ( left.M32 * right.M21 ) + ( left.M33 * right.M31 ),
                                            ( left.M31 * right.M12 ) + ( left.M32 * right.M22 ) + ( left.M33 * right.M32 ),
                                            ( left.M31 * right.M13 ) + ( left.M32 * right.M23 ) + ( left.M33 * right.M33 ));
            }

            return result;
        }

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The source vector.
        /// </param>
        /// <remarks>
        /// Multiplying a 3x3 matrix by a 3x1 vector is supposed to result in a 1x3 vector. <see cref="Vector3D"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 3x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static unsafe Vector3Fix16 operator *(Matrix3x3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(( left.M11 * right.X ) + ( left.M12 * right.Y ) + ( left.M13 * right.Z ),
                                    ( left.M21 * right.X ) + ( left.M22 * right.Y ) + ( left.M23 * right.Z ),
                                    ( left.M31 * right.X ) + ( left.M32 * right.Y ) + ( left.M33 * right.Z ));
        }

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="left">
        /// The source vector.
        /// </param>
        /// <param name="right">
        /// The source matrix.
        /// </param>
        /// <remarks>
        /// Multiplying a 3x3 matrix by a 3x1 vector is supposed to result in a 1x3 vector. <see cref="Vector3D"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 3x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, Matrix3x3Fix16 right) =>
            right * left;

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector4D"/>.
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
        /// Multiplying a 4x4 matrix by a 4x1 vector is supposed to result in a 1x4 vector. <see cref="Vector4D"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 4x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static Vector4Fix16 operator *(Matrix3x3Fix16 left, Vector4Fix16 right)
        {
            (var v, _) = right;
            return new Vector4Fix16(left * v, right.W);
        }

        /// <summary>
        /// Multiplies a matrix by a <see cref="Vector4D"/>.
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
        /// Multiplying a 4x4 matrix by a 4x1 vector is supposed to result in a 1x4 vector. <see cref="Vector4D"/> has no
        /// concept of being vertical or horizontal, so the result is "translated" as a 4x1 vector.
        /// </remarks>
        [MethodImpl(Optimize)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, Matrix3x3Fix16 right) =>
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
        public static unsafe Matrix3x3Fix16 operator *(Matrix3x3Fix16 left, int right)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return (left.As4x4() * right).As3x3();
            else
#endif
                return new Matrix3x3Fix16(left.M11 * right, left.M12 * right, left.M13 * right,
                                          left.M21 * right, left.M22 * right, left.M23 * right,
                                          left.M31 * right, left.M32 * right, left.M33 * right);
        }

        /// <summary>
        /// Returns itself. (nop)
        /// </summary>
        [MethodImpl(Optimize)]
        public static Matrix3x3Fix16 operator +(Matrix3x3Fix16 value) =>
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
        public static unsafe Matrix3x3Fix16 operator +(Matrix3x3Fix16 left, Matrix3x3Fix16 right)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return (left.As4x4() + right.As4x4()).As3x3();
            else
#endif
                return new Matrix3x3Fix16(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13,
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
        public static unsafe bool operator ==(Matrix3x3Fix16 left, Matrix3x3Fix16 right)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return left.As4x4() == right.As4x4();
            else
#endif
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
        public static Matrix3x3Fix16 Subtract(Matrix3x3Fix16 left, Matrix3x3Fix16 right) =>
            left - right;

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix3x3Fix16 Transpose(Matrix3x3Fix16 matrix)
        {
#if !NOSIMD
            if (Sse.IsSupported)
                return Matrix4x4Fix16.Transpose(matrix.As4x4()).As3x3();
#endif
            return new Matrix3x3Fix16(matrix.M11, matrix.M21, matrix.M31,
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
        public bool Equals([AllowNull] Matrix3x3Fix16 other) =>
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
            obj is Matrix3x3Fix16 matrix && this == matrix;

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        [MethodImpl(Optimize)]
        public int GetDeterminant() =>
            ( M11 * (( M22 * M33 ) - ( M32 * M23 )) ) -
            ( M12 * (( M21 * M33 ) - ( M23 * M31 )) ) +
            ( M13 * (( M21 * M32 ) - ( M22 * M31 )) );

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
        /// Attempts to calculate the inverse of this matrix. If successful, the inverted matrix will be returned.
        /// </summary>
        /// <returns>
        /// The inverted matrix or a NaN matrix if the inverse could not be calculated.
        /// </returns>
        [MethodImpl(Optimize)]
        public Matrix3x3Fix16 Invert() =>
            Invert(this);

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        public override string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13}}} {{M21:{M21} M22:{M22} M23:{M23}}} {{M31:{M31} M32:{M32} M33:{M33}}} }}";

        /// <summary>
        /// Provides a record-style <see langword="with"/>-like constructor.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix3x3Fix16 With(int? m11 = null, int? m12 = null, int? m13 = null, int? m21 = null, int? m22 = null, int? m23 = null, int? m31 = null, int? m32 = null, int? m33 = null) =>
            new Matrix3x3Fix16(m11 ?? M11, m12 ?? M12, m13 ?? M13, m21 ?? M21, m22 ?? M22, m23 ?? M23, m31 ?? M31, m32 ?? M32, m33 ?? M33);

#endregion Public Methods

#region Internal Methods

        internal Matrix4x4Fix16 As4x4() =>
            Matrix4x4Fix16.Identity.With(m11: M11, m12: M12, m13: M13, m21: M21, m22: M22, m23: M23, m31: M31, m32: M32, m33: M33);

#endregion Internal Methods
#pragma warning restore IDE0045 // Convert to conditional expression
#pragma warning restore IDE0024 // Use expression body for operators
    }
}