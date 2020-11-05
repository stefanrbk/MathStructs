using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating four double precision floating point values.
    /// </summary>
    public struct Vector4D : IEquatable<Vector4D>, IFormattable
    {
        #region Constants

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        #endregion

        #region Public Fields

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        public double X;
        /// <summary>
        ///     The Y component of the vector.
        /// </summary>
        public double Y;
        /// <summary>
        ///     The Z component of the vector.
        /// </summary>
        public double Z;
        /// <summary>
        ///     The W component of the vector.
        /// </summary>
        public double W;

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4D Zero =>
            new Vector4D(0f);

        /// <summary>
        /// Returns the vector &lt; 1, 1, 1, 1 &gt;.
        /// </summary>
        public static Vector4D One =>
            new Vector4D(1f);

        /// <summary>
        /// Returns the vector &lt; 1, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4D UnitX =>
            new Vector4D(1f, 0f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 1, 0, 0 &gt;.
        /// </summary>
        public static Vector4D UnitY =>
            new Vector4D(0f, 1f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 1, 0 &gt;.
        /// </summary>
        public static Vector4D UnitZ =>
            new Vector4D(0f, 0f, 1f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 1 &gt;.
        /// </summary>
        public static Vector4D UnitW =>
            new Vector4D(0f, 0f, 0f, 1f);

        #endregion Public Static Properties

        #region Public Instance Methods

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        ///     The hash code.
        /// </returns>
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector4D"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the Object is equal to this <see cref="Vector4D"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector4D vec && Equals(vec);

        /// <summary>
        /// Returns a String representing this Vector4D instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this Vector4D instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        /// <returns>
        ///     The string representation.
        /// </returns>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector4D"/> instance, using the specified format to format
        ///     individual elements and the given <see cref="IFormatProvider"/>.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        /// <param name="formatProvider">
        ///     The format provider to use when formatting elements.
        /// </param>
        /// <returns>
        ///     The string representation.
        /// </returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var sep = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            return $"<{X.ToString(format, formatProvider)}{sep} {Y.ToString(format, formatProvider)}{sep} {Z.ToString(format, formatProvider)}{sep} {W.ToString(format, formatProvider)}>";
        }

        /// <summary>
        ///     Returns the length of the vector.
        /// </summary>
        /// <remarks>
        ///     <see cref="LengthSquared"/> is cheaper to use if you need to square this result.
        /// </remarks>
        /// <returns>
        ///     The vector's length.
        /// </returns>
        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        /// <summary>
        ///     Returns the length of the vector squared.
        /// </summary>
        /// <remarks>
        ///     This operation is cheaper than <see cref="Length"/>.
        /// </remarks>
        /// <returns>
        ///     The vector's length squared.
        /// </returns>
        [MethodImpl(Inline)]
        public double LengthSquared() =>
            Dot(this, this);

        /// <summary>
        ///     Returns the Euclidean distance between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <returns>
        ///     The distance.
        /// </returns>
        [MethodImpl(Inline)]
        public double Distance(Vector4D value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance"/>.
        /// </remarks>
        /// <returns>
        ///     The distance squared.
        /// </returns>
        [MethodImpl(Inline)]
        public double DistanceSquared(Vector4D value) =>
            DistanceSquared(this, value);

        /// <summary>
        ///     Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        /// <returns>
        ///     The normalized vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D Normalize() =>
            Normalize(this);

        /// <summary>
        ///     Restricts a copy of this vector between a min and max value.
        /// </summary>
        /// <param name="min">
        ///     The minimum value.
        /// </param>
        /// <param name="max">
        ///     The maximum value.
        /// </param>
        /// <returns>
        ///     The restricted vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D Clamp(Vector4D min, Vector4D max) =>
            Clamp(this, min, max);

        /// <summary>
        ///     Transforms a copy of this vector by the given matrix.
        /// </summary>
        /// <param name="vector">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D Transform(Matrix4x4D matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms a copy of this vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D Transfrom(QuaternionD rotation) =>
            Transform(this, rotation);

        /// <summary>
        ///     Copies the contents of the vector into the given array.
        /// </summary>
        [MethodImpl(Inline)]
        public void CopyTo(double[] array) =>
            CopyTo(array, 0);

        /// <summary>
        ///     Copies the contents of the vector into the given array, starting from index.
        /// </summary>
        /// <exception cref="RankException">
        ///     If array is multidimensional.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If index is greater than end of the array or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If number of elements in source vector is greater than those available in destination array.
        /// </exception>
        [MethodImpl(Inline)]
        public void CopyTo(double[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 4)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
            array[index + 3] = W;
        }

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector4D"/> is equal to this <see cref="Vector4D"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4D"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4D"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4D other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector4D"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4D"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4D"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4D other, double delta)
        {
            if (delta is 0.0)
                return this == other;

            var vector = Subtract(this, other).Abs();
            return vector.X < delta &&
                   vector.Y < delta &&
                   vector.Z < delta &&
                   vector.W < delta;
        }

        /// <summary>
        ///     Returns the dot product of this instance and the given vector.
        /// </summary>
        /// <remarks>
        ///     The dot product of two vectors is the sum of the products of each of the
        ///     pairs of elements from two vectors
        /// </remarks>
        /// <param name="vector">
        ///     The other vector.
        /// </param>
        /// <returns>
        ///     The dot product.
        /// </returns>
        [MethodImpl(Inline)]
        public double Dot(Vector4D vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        /// <returns>
        ///     The absolute value vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D Abs() =>
            Abs(this);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        /// <returns>
        ///     The square root vector.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D SquareRoot() =>
            SquareRoot(this);

        #endregion Public Instance Methods

        #region Public Static Methods

        /// <summary>
        ///     Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="vector1">
        ///     The first point.
        /// </param>
        /// <param name="vector2">
        ///     The second point.
        /// </param>
        /// <returns>
        ///     The distance.
        /// </returns>
        [MethodImpl(Inline)]
        public static double Distance(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).Length();

        /// <summary>
        ///     Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="vector1">
        ///     The first point.
        /// </param>
        /// <param name="vector2">
        ///     The second point.
        /// </param>
        /// <returns>
        ///     The distance squared.
        /// </returns>
        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).LengthSquared();

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        /// <returns>
        ///     The normalized vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Normalize(Vector4D vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        /// <param name="min">
        ///     The minimum value.
        /// </param>
        /// <param name="max">
        ///     The maximum value.
        /// </param>
        /// <returns>
        ///     The restricted vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Clamp(Vector4D value, Vector4D min, Vector4D max) =>
            Min(Max(value, min), max);

        /// <summary>
        ///     Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="bounds1">
        ///     The first source vector.
        /// </param>
        /// <param name="bounds2">
        ///     The second source vector.
        /// </param>
        /// <param name="amount">
        ///     Value between 0 and 1 indicating the weight of the second source vector.
        /// </param>
        /// <returns>
        ///     The interpolated vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Lerp(Vector4D bounds1, Vector4D bounds2, double amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

        /// <summary>
        ///     Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector2D position, Matrix4x4D matrix)
        {
            return new Vector4D(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
        }

        /// <summary>
        ///     Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="vector">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector3D vector, Matrix4x4D matrix) =>
            new Vector4D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + matrix.M44);

        /// <summary>
        ///     Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="vector">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector4D vector, Matrix4x4D matrix) =>
            new Vector4D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);

        /// <summary>
        ///     Transforms a vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="value">
        ///     The source vector to be rotated.
        /// </param>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector2D value, QuaternionD rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
        }

        /// <summary>
        ///     Transforms a vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="value">
        ///     The source vector to be rotated.
        /// </param>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector3D value, QuaternionD rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
        }

        /// <summary>
        ///     Transforms a vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="value">
        ///     The source vector to be rotated.
        /// </param>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        /// <returns>
        ///     The transformed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector4D value, QuaternionD rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
        }

        /// <summary>
        ///     Returns the dot product of two vectors.
        /// </summary>
        /// <remarks>
        ///     The dot product of two vectors is the sum of the products of each of the
        ///     pairs of elements from two vectors
        /// </remarks>
        /// <param name="left">
        ///     The first vector.
        /// </param>
        /// <param name="right">
        ///     The second vector.
        /// </param>
        /// <returns>
        ///     The dot product.
        /// </returns>
        [MethodImpl(Inline)]
        public static double Dot(Vector4D left, Vector4D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z +
            left.W * right.W;

        /// <summary>
        ///     Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The minimized vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Min(Vector4D left, Vector4D right) =>
            new Vector4D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z,
                         left.W < right.W ? left.W : right.W);

        /// <summary>
        ///     Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The maximized vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Max(Vector4D left, Vector4D right) =>
            new Vector4D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z,
                         left.W > right.W ? left.W : right.W);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The absolute value vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Abs(Vector4D value) =>
            new Vector4D(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z), Math.Abs(value.W));

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The square root vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D SquareRoot(Vector4D value) =>
            new Vector4D(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z), Math.Sqrt(value.W));

        #endregion Public Static Methods

        #region Public Operator Methods

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The summed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Add(Vector4D left, Vector4D right) =>
            left + right;

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The difference vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Subtract(Vector4D left, Vector4D right) =>
            left - right;

        /// <summary>
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns></returns>
        [MethodImpl(Inline)]
        public static Vector4D Multiply(Vector4D left, Vector4D right) =>
            left * right;

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        /// <returns>
        ///     The scaled vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Multiply(Vector4D left, double right) =>
            left * right;

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The scalar value.
        /// </param>
        /// <param name="right">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The scaled vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Multiply(double left, Vector4D right) =>
            left * right;

        /// <summary>
        ///     Divides the first vector by the second.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The vector resulting from the division.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Divide(Vector4D left, Vector4D right) =>
            left / right;

        /// <summary>
        ///     Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        /// <returns>
        ///     The vector resulting from the division.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Divide(Vector4D left, double right) =>
            left / right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The negated vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D Negate(Vector4D value) =>
            -value;

        #endregion Public Operator Methods

        #region Constructors

        /// <summary>
        ///     Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">
        ///     The element to fill the vector with.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D(double value)
            : this(value, value, value, value) { }

        /// <summary>
        ///     Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">
        ///     X component.
        /// </param>
        /// <param name="y">
        ///     Y component.
        /// </param>
        /// <param name="z">
        ///     Z component.
        /// </param>
        /// <param name="w">
        ///     W component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Constructs a <see cref="Vector4D"/> from the given <see cref="Vector3D"/> and a W component.
        /// </summary>
        /// <param name="vector">
        ///     The vector to use as the X, Y, and Z components.
        /// </param>
        /// <param name="w">
        ///     The W component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D(Vector3D vector, double w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        /// <summary>
        /// Constructs a <see cref="Vector4D"/> from the given <see cref="Vector2D"/> and a Z and W component.
        /// </summary>
        /// <param name="vector">
        ///     The vector to use as the X and Y component.
        /// </param>
        /// <param name="z">
        ///     The Z component.
        /// </param>
        /// <param name="w">
        ///     The W component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D(Vector2D vector, double z, double w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new <see cref="Vector4D"/>, otherwise <see cref="this.X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector4D"/>, otherwise <see cref="this.Y"/>.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new <see cref="Vector4D"/>, otherwise <see cref="this.Z"/>.
        /// </param>
        /// <param name="w">
        ///     If provided, the W value for the new <see cref="Vector4D"/>, otherwise <see cref="this.W"/>.
        /// </param>
        /// <returns>
        ///     The constructed <see cref="Vector4D"/>.
        /// </returns>
        [MethodImpl(Inline)]
        public Vector4D With(double? x = null, double? y = null, double? z = null, double? w = null) =>
            new Vector4D(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Constructors

        #region Deconstructors (into Tuples)

        /// <summary>
        ///     Deconstructs this vector into its <see cref="double"/> components.
        /// </summary>
        /// <param name="x">
        ///     The X of this vector.
        /// </param>
        /// <param name="y">
        ///     The Y of this vector.
        /// </param>
        /// <param name="z">
        ///     The Z of this vector.
        /// </param>
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="double"/> components and builds the
        ///     X, Y, and Z into a new <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="vector">
        ///     The X, Y, and Z of this vector as a <see cref="Vector3D"/>.
        /// </param>
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3D vector, out double w)
        {
            w = W;
            vector = new Vector3D(X, Y, Z);
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="double"/> components and builds the
        ///     X and Y into a new <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="vector">
        ///     The X and Y of this vector as a <see cref="Vector2D"/>.
        /// </param>
        /// <param name="z">
        ///     The Z of this vector.
        /// </param>
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2D vector, out double z, out double w)
        {
            vector = new Vector2D(X, Y);
            z = Z;
            w = W;
        }

        #endregion Deconstructors (into Tuples)

        #region Public Static Operators

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector4D operator +(Vector4D value) =>
            value;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The negated vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator -(Vector4D value) =>
            new Vector4D(-value.X, -value.Y, -value.Z, -value.W);

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The summed vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator +(Vector4D left, Vector4D right) =>
            new Vector4D(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The difference vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator -(Vector4D left, Vector4D right) =>
            new Vector4D(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The product vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator *(Vector4D left, Vector4D right) =>
            new Vector4D(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        /// <returns>
        ///     The scaled vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator *(Vector4D left, double right) =>
            new Vector4D(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The scalar value.
        /// </param>
        /// <param name="right">
        ///     The source vector.
        /// </param>
        /// <returns>
        ///     The scaled vector.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator *(double left, Vector4D right) =>
            right * left;

        /// <summary>
        ///     Divides the first vector by the second.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        /// <returns>
        ///     The vector resulting from the division.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator /(Vector4D left, Vector4D right) =>
            new Vector4D(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        /// <summary>
        ///     Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        /// <returns>
        ///     The result of the division.
        /// </returns>
        [MethodImpl(Inline)]
        public static Vector4D operator /(Vector4D left, double right) =>
            new Vector4D(left.X / right, left.Y / right, left.Z / right, left.W / right);

        /// <summary>
        ///     Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">
        ///     The first vector to compare.
        /// </param>
        /// <param name="right">
        ///     The second vector to compare.
        /// </param>
        /// <returns>
        ///     True if the vectors are equal; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public static bool operator ==(Vector4D left, Vector4D right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        /// <summary>
        ///     Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">
        ///     The first vector to compare.
        /// </param>
        /// <param name="right">
        ///     The second vector to compare.
        /// </param>
        /// <returns>
        ///     True if the vectors are not equal; False if they are equal.
        /// </returns>
        [MethodImpl(Inline)]
        public static bool operator !=(Vector4D left, Vector4D right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;

        #endregion Public Static Operators
    }
}
