using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating two double precision floating point values.
    /// </summary>
    /// <remarks>
    /// Slower than <see cref="Vector2F"/> but more precise.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct Vector2D : IEquatable<Vector2D>, IFormattable
    {
        #region Public Fields

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        [FieldOffset(0)]
        public double X;

        /// <summary>
        ///     The Y component of the vector.
        /// </summary>
        [FieldOffset(8)]
        public double Y;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        #endregion Private Fields

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

        #region Public Properties

        /// <summary>
        /// Returns the vector &lt; 1, 1 &gt;.
        /// </summary>
        public static Vector2D One =>
            new Vector2D(1d);

        /// <summary>
        /// Returns the vector &lt; 1, 0 &gt;.
        /// </summary>
        public static Vector2D UnitX =>
            new Vector2D(1d, 0d);

        /// <summary>
        /// Returns the vector &lt; 0, 1 &gt;.
        /// </summary>
        public static Vector2D UnitY =>
            new Vector2D(0d, 1d);

        /// <summary>
        /// Returns the vector &lt; 0, 0 &gt;.
        /// </summary>
        public static Vector2D Zero =>
            new Vector2D(0d);

        #endregion Public Properties

        #region Public Methods

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
        ///     Adds two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Add(Vector2D left, Vector2D right) =>
            left + right;

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
        [MethodImpl(Inline)]
        public static Vector2D Clamp(Vector2D value, Vector2D min, Vector2D max) =>
            Min(Max(value, min), max);

        /// <summary>
        ///     Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="vector1">
        ///     The first point.
        /// </param>
        /// <param name="vector2">
        ///     The second point.
        /// </param>
        /// <remarks>
        ///     More expensive than <see cref="DistanceSquared(Vector2D,Vector2D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static double Distance(Vector2D vector1, Vector2D vector2) =>
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
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector2D,Vector2D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector2D vector1, Vector2D vector2) =>
            (vector1 - vector2).LengthSquared();

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
        public static Vector2D Divide(Vector2D left, Vector2D right) =>
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
        [MethodImpl(Inline)]
        public static Vector2D Divide(Vector2D left, double right) =>
            left / right;

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
        [MethodImpl(Inline)]
        public static Vector2D Lerp(Vector2D bounds1, Vector2D bounds2, double amount) =>
            bounds1 * (1d - amount) + bounds2 * amount;

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
        ///     Multiplies two vectors together.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Multiply(Vector2D left, Vector2D right) =>
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
        [MethodImpl(Inline)]
        public static Vector2D Multiply(Vector2D left, double right) =>
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
        [MethodImpl(Inline)]
        public static Vector2D Multiply(double left, Vector2D right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Negate(Vector2D value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Normalize(Vector2D vector) =>
            vector / vector.Length();

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
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">
        /// The source vector.
        /// </param>
        /// <param name="normal">
        /// The normal of the surface being reflected off.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Reflect(Vector2D vector, Vector2D normal) =>
            vector - normal * Dot(vector, normal) * 2d;

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
        ///     Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">
        ///     The first source vector.
        /// </param>
        /// <param name="right">
        ///     The second source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Subtract(Vector2D left, Vector2D right) =>
            left - right;

        /// <summary>
        ///     Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="vector">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Transform(Vector2D vector, Matrix4x4D matrix) =>
            new Vector2D(vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42);

        /// <summary>
        ///     Transforms a vector by the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="value">
        ///     The source vector to be rotated.
        /// </param>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Transform(Vector2D value, QuaternionD rotation)
        {
            var n1 = rotation.X + rotation.X;
            var n2 = rotation.Y + rotation.Y;
            var n3 = rotation.Z + rotation.Z;
            var n4 = rotation.W * n3;
            var n5 = rotation.X * n1;
            var n6 = rotation.X * n2;
            var n7 = rotation.Y * n2;
            var n8 = rotation.Z * n3;
            return new Vector2D(value.X * (1 - n7 - n8) + value.Y * (n6 - n4), value.X * (n6 + n4) + value.Y * (1 - n5 - n8));
        }

        /// <summary>
        /// Transforms a vector normal by the given 4x4 matrix.
        /// </summary>
        /// <param name="normal">
        /// The source vector.
        /// </param>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D TransformNormal(Vector2D normal, Matrix4x4D matrix) =>
            new Vector2D(normal.X * matrix.M11 + normal.Y * matrix.M21,
                         normal.X * matrix.M12 + normal.Y * matrix.M22);

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
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector2D"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector2D"/>;
        ///     False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector2D vec && Equals(vec);

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
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static explicit operator Vector2D(ReadOnlySpan<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static explicit operator Vector2D(Span<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode());

        /// <summary>
        ///     Returns the length of the vector.
        /// </summary>
        /// <remarks>
        ///     <see cref="LengthSquared"/> is cheaper to use if you need the squared length.
        /// </remarks>
        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        /// <summary>
        ///     Returns the length of the vector squared.
        /// </summary>
        /// <remarks>
        ///     This operation is cheaper than <see cref="Length"/> if you need the squared length.
        /// </remarks>
        [MethodImpl(Inline)]
        public double LengthSquared() =>
            Dot(this, this);

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
        /// Returns a String representing this <see cref="Vector2D"/> instance.
        /// </summary>
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector2D"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector2D"/> instance, using the specified format to format
        ///     individual elements and the given <see cref="IFormatProvider"/>.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        /// <param name="formatProvider">
        ///     The format provider to use when formatting elements.
        /// </param>
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}>";

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

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new <see cref="Vector2D"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector2D"/>, otherwise <see cref="Y"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2D With(double? x = null, double? y = null) =>
            new Vector2D(x ?? X, y ?? Y);

        #endregion Public Methods
    }
}