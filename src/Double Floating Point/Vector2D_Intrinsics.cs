using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Numerics
{
    public partial struct Vector2D
    {

        #region Public Constructors

        /// <summary>
        ///     Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">
        ///     The element to fill the vector with.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D(double value)
            : this(value, value) { }

        /// <summary>
        ///     Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">
        ///     X component.
        /// </param>
        /// <param name="y">
        ///     Y component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Abs(Vector2D value) =>
            new Vector2D(Math.Abs(value.X), Math.Abs(value.Y));

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
        [MethodImpl(Inline)]
        public static double Dot(Vector2D left, Vector2D right) =>
            left.X * right.X +
            left.Y * right.Y;

        /// <summary>
        ///     Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Max(Vector2D left, Vector2D right) =>
            new Vector2D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y);

        /// <summary>
        ///     Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Min(Vector2D left, Vector2D right) =>
            new Vector2D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y);

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator -(Vector2D value) =>
            new Vector2D(-value.X, -value.Y);

        /// <summary>
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator -(Vector2D left, Vector2D right) =>
            new Vector2D(left.X - right.X, left.Y - right.Y);

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
        public static bool operator !=(Vector2D left, Vector2D right) =>
            left.X != right.X || left.Y != right.Y;

        /// <summary>
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator *(Vector2D left, Vector2D right) =>
            new Vector2D(left.X * right.X, left.Y * right.Y);

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator *(Vector2D left, double right) =>
            new Vector2D(left.X * right, left.Y * right);

        /// <summary>
        ///     Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The scalar value.
        /// </param>
        /// <param name="right">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator *(double left, Vector2D right) =>
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
        [MethodImpl(Inline)]
        public static Vector2D operator /(Vector2D left, Vector2D right) =>
            new Vector2D(left.X / right.X, left.Y / right.Y);

        /// <summary>
        ///     Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">
        ///     The source vector.
        /// </param>
        /// <param name="right">
        ///     The scalar value.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator /(Vector2D left, double right) =>
            new Vector2D(left.X / right, left.Y / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector2D operator +(Vector2D value) =>
            value;

        /// <summary>
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D operator +(Vector2D left, Vector2D right) =>
            new Vector2D(left.X + right.X, left.Y + right.Y);

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
        public static bool operator ==(Vector2D left, Vector2D right) =>
            left.X == right.X && left.Y == right.Y;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D SquareRoot(Vector2D value) =>
            new Vector2D(Math.Sqrt(value.X), Math.Sqrt(value.Y));

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2D Abs() =>
            Abs(this);

        /// <summary>
        ///     Restricts a copy of this vector between a min and max value.
        /// </summary>
        /// <param name="min">
        ///     The minimum value.
        /// </param>
        /// <param name="max">
        ///     The maximum value.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D Clamp(Vector2D min, Vector2D max) =>
            Clamp(this, min, max);

        /// <summary>
        ///     Copies the contents of the vector into the given span.
        /// </summary>
        [MethodImpl(Inline)]
        public void CopyTo(Span<double> span) =>
            CopyTo(span, 0);

        /// <summary>
        ///     Copies the contents of the vector into the given span, starting from index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If index is greater than end of the span or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If number of elements in source vector is greater than those available in destination span.
        /// </exception>
        [MethodImpl(Inline)]
        public void CopyTo(Span<double> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index + 0] = X;
            span[index + 1] = Y;
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="double"/> components.
        /// </summary>
        /// <param name="x">
        ///     The X of this vector.
        /// </param>
        /// <param name="y">
        ///     The Y of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        ///     Returns the Euclidean distance between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     More expensive than <see cref="DistanceSquared(Vector2D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public double Distance(Vector2D value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector2D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public double DistanceSquared(Vector2D value) =>
            DistanceSquared(this, value);

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
        [MethodImpl(Inline)]
        public double Dot(Vector2D vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector2D"/> is equal to this <see cref="Vector2D"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector2D"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector2D"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector2D other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector2D"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector2D"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector2D"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector2D other, double delta) =>
            delta == 0.0 ? this == other
                         : Math.Abs(X - other.X) < delta &&
                           Math.Abs(Y - other.Y) < delta;

        /// <summary>
        ///     Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2D Normalize() =>
            Normalize(this);

        /// <summary>
        /// Returns the reflection of this vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">
        /// The normal of the surface being reflected off.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D Reflect(Vector2D normal) =>
            Reflect(this, normal);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2D SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        ///     Transforms a copy of this vector by the given matrix.
        /// </summary>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D Transform(Matrix4x4D matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms a copy of this vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D Transform(QuaternionD rotation) =>
            Transform(this, rotation);

        /// <summary>
        /// Transforms this vector normal by the given 4x4 matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D TransformNormal(Matrix4x4D matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms this vector by the given matrix and returns a <see cref="Vector4D"/>.
        /// </summary>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D TransformV4(Matrix4x4D matrix) =>
            Vector4D.Transform(this, matrix);

        /// <summary>
        ///     Transforms this vector by the given <see cref="QuaternionD"/> rotation value and returns a <see cref="Vector4D"/>.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4D TransformV4(QuaternionD rotation) =>
            Vector4D.Transform(this, rotation);
    }
}
