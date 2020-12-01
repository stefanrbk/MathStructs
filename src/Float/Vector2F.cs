using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating two single precision floating point values.
    /// </summary>
    /// <remarks>
    /// Less precise than <see cref="Vector2D"/> but faster.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector2F : IEquatable<Vector2F>, IFormattable
    {
        #region Public Fields

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        [FieldOffset(0)]
        public readonly float X;

        /// <summary>
        ///     The Y component of the vector.
        /// </summary>

        [FieldOffset(4)]
        public readonly float Y;

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
        public Vector2F(float value)
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
        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the vector &lt; 1, 1 &gt;.
        /// </summary>
        public static Vector2F One =>
            new Vector2F(1f);

        /// <summary>
        /// Returns the vector &lt; 1, 0 &gt;.
        /// </summary>
        public static Vector2F UnitX =>
            new Vector2F(1f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 1 &gt;.
        /// </summary>
        public static Vector2F UnitY =>
            new Vector2F(0f, 1f);

        /// <summary>
        /// Returns the vector &lt; 0, 0 &gt;.
        /// </summary>
        public static Vector2F Zero =>
            new Vector2F(0f);

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F Abs(Vector2F value) =>
            new Vector2F(MathF.Abs(value.X), MathF.Abs(value.Y));

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
        public static Vector2F Add(Vector2F left, Vector2F right) =>
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
        public static Vector2F Clamp(Vector2F value, Vector2F min, Vector2F max) =>
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
        ///     More expensive than <see cref="DistanceSquared(Vector2F,Vector2F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float Distance(Vector2F vector1, Vector2F vector2) =>
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
        ///     Less expensive than <see cref="Distance(Vector2F,Vector2F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector2F vector1, Vector2F vector2) =>
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
        public static Vector2F Divide(Vector2F left, Vector2F right) =>
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
        public static Vector2F Divide(Vector2F left, float right) =>
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
        public static float Dot(Vector2F left, Vector2F right) =>
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
        public static Vector2F Lerp(Vector2F bounds1, Vector2F bounds2, float amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

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
        public static Vector2F Max(Vector2F left, Vector2F right) =>
            new Vector2F(left.X > right.X ? left.X : right.X,
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
        public static Vector2F Min(Vector2F left, Vector2F right) =>
            new Vector2F(left.X < right.X ? left.X : right.X,
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
        public static Vector2F Multiply(Vector2F left, Vector2F right) =>
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
        public static Vector2F Multiply(Vector2F left, float right) =>
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
        public static Vector2F Multiply(float left, Vector2F right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F Negate(Vector2F value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F Normalize(Vector2F vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F operator -(Vector2F value) =>
            new Vector2F(-value.X, -value.Y);

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
        public static Vector2F operator -(Vector2F left, Vector2F right) =>
            new Vector2F(left.X - right.X, left.Y - right.Y);

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
        public static bool operator !=(Vector2F left, Vector2F right) =>
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
        public static Vector2F operator *(Vector2F left, Vector2F right) =>
            new Vector2F(left.X * right.X, left.Y * right.Y);

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
        public static Vector2F operator *(Vector2F left, float right) =>
            new Vector2F(left.X * right, left.Y * right);

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
        public static Vector2F operator *(float left, Vector2F right) =>
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
        public static Vector2F operator /(Vector2F left, Vector2F right) =>
            new Vector2F(left.X / right.X, left.Y / right.Y);

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
        public static Vector2F operator /(Vector2F left, float right) =>
            new Vector2F(left.X / right, left.Y / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector2F operator +(Vector2F value) =>
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
        public static Vector2F operator +(Vector2F left, Vector2F right) =>
            new Vector2F(left.X + right.X, left.Y + right.Y);

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
        public static bool operator ==(Vector2F left, Vector2F right) =>
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
        public static Vector2F Reflect(Vector2F vector, Vector2F normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F SquareRoot(Vector2F value) =>
            new Vector2F(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));

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
        public static Vector2F Subtract(Vector2F left, Vector2F right) =>
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
        public static Vector2F Transform(Vector2F vector, Matrix4x4F matrix) =>
            new Vector2F(vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42);

        /// <summary>
        ///     Transforms a vector by the given <see cref="QuaternionF"/> rotation value.
        /// </summary>
        /// <param name="value">
        ///     The source vector to be rotated.
        /// </param>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2F Transform(Vector2F value, QuaternionF rotation)
        {
            var n1 = rotation.X + rotation.X;
            var n2 = rotation.Y + rotation.Y;
            var n3 = rotation.Z + rotation.Z;
            var n4 = rotation.W * n3;
            var n5 = rotation.X * n1;
            var n6 = rotation.X * n2;
            var n7 = rotation.Y * n2;
            var n8 = rotation.Z * n3;
            return new Vector2F(value.X * (1 - n7 - n8) + value.Y * (n6 - n4), value.X * (n6 + n4) + value.Y * (1 - n5 - n8));
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
        public static Vector2F TransformNormal(Vector2F normal, Matrix4x4F matrix) =>
            new Vector2F(normal.X * matrix.M11 + normal.Y * matrix.M21,
                         normal.X * matrix.M12 + normal.Y * matrix.M22);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2F Abs() =>
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
        public Vector2F Clamp(Vector2F min, Vector2F max) =>
            Clamp(this, min, max);

        /// <summary>
        ///     Copies the contents of the vector into the given span.
        /// </summary>
        [MethodImpl(Inline)]
        public void CopyTo(Span<float> span) =>
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
        public void CopyTo(Span<float> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index + 0] = X;
            span[index + 1] = Y;
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="float"/> components.
        /// </summary>
        /// <param name="x">
        ///     The X of this vector.
        /// </param>
        /// <param name="y">
        ///     The Y of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y)
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
        ///     More expensive than <see cref="DistanceSquared(Vector2F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float Distance(Vector2F value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector2F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float DistanceSquared(Vector2F value) =>
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
        public float Dot(Vector2F vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector2F"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector2F"/>;
        ///     False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector2F vec && Equals(vec);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector2F"/> is equal to this <see cref="Vector2F"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector2F"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector2F"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector2F other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector2F"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector2F"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector2F"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector2F other, float delta) =>
            delta == 0.0 ? this == other
                         : MathF.Abs(X - other.X) < delta &&
                           MathF.Abs(Y - other.Y) < delta;

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
        public float Length() =>
            MathF.Sqrt(Dot(this, this));

        /// <summary>
        ///     Returns the length of the vector squared.
        /// </summary>
        /// <remarks>
        ///     This operation is cheaper than <see cref="Length"/> if you need the squared length.
        /// </remarks>
        [MethodImpl(Inline)]
        public float LengthSquared() =>
            Dot(this, this);

        /// <summary>
        ///     Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2F Normalize() =>
            Normalize(this);

        /// <summary>
        /// Returns the reflection of this vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">
        /// The normal of the surface being reflected off.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2F Reflect(Vector2F normal) =>
            Reflect(this, normal);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector2F SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        /// Returns a String representing this <see cref="Vector2F"/> instance.
        /// </summary>
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector2F"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector2F"/> instance, using the specified format to format
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
        public Vector2F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms a copy of this vector by the given <see cref="QuaternionF"/> rotation value.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2F Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        /// <summary>
        /// Transforms this vector normal by the given 4x4 matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2F TransformNormal(Matrix4x4F matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms this vector by the given matrix and returns a <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F TransformV4(Matrix4x4F matrix) =>
            Vector4F.Transform(this, matrix);

        /// <summary>
        ///     Transforms this vector by the given <see cref="QuaternionF"/> rotation value and returns a <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F TransformV4(QuaternionF rotation) =>
            Vector4F.Transform(this, rotation);

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new <see cref="Vector2F"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector2F"/>, otherwise <see cref="Y"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector2F With(float? x = null, float? y = null) =>
            new Vector2F(x ?? X, y ?? Y);

        #endregion Public Methods
    }
}