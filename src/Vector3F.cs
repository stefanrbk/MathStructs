using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating three single precision floating point values.
    /// </summary>
    /// <remarks>
    /// Less precise than <see cref="Vector3D"/> but faster.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct Vector3F : IEquatable<Vector3F>, IFormattable
    {
        #region Public Fields

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        [FieldOffset(0)]
        public float X;

        /// <summary>
        ///     The Y component of the vector.
        /// </summary>
        [FieldOffset(4)]
        public float Y;

        /// <summary>
        ///     The Z component of the vector.
        /// </summary>
        [FieldOffset(8)]
        public float Z;

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
        public Vector3F(float value)
            : this(value, value, value) { }

        /// <summary>
        /// Constructs a <see cref="Vector3F"/> from the given <see cref="Vector2F"/> and a Z component.
        /// </summary>
        /// <param name="value">
        ///     The vector to use as the X and Y component.
        /// </param>
        /// <param name="z">
        ///     The Z component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F(Vector2F value, float z)
            : this(value.X, value.Y, z) { }

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
        [MethodImpl(Inline)]
        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the vector &lt; 1, 1, 1 &gt;.
        /// </summary>
        public static Vector3F One =>
            new Vector3F(1f);

        /// <summary>
        /// Returns the vector &lt; 1, 0, 0 &gt;.
        /// </summary>
        public static Vector3F UnitX =>
            new Vector3F(1f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 1, 0 &gt;.
        /// </summary>
        public static Vector3F UnitY =>
            new Vector3F(0f, 1f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 1 &gt;.
        /// </summary>
        public static Vector3F UnitZ =>
            new Vector3F(0f, 0f, 1f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0 &gt;.
        /// </summary>
        public static Vector3F Zero =>
            new Vector3F(0f);

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F Abs(Vector3F value) =>
            new Vector3F(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));

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
        public static Vector3F Add(Vector3F left, Vector3F right) =>
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
        public static Vector3F Clamp(Vector3F value, Vector3F min, Vector3F max) =>
            Min(Max(value, min), max);

        /// <summary>
        /// Computes the cross product of two vectors which is a vector perpendicular to the two vectors.
        /// </summary>
        /// <remarks>
        /// <para>The cross product performs the following:</para>
        /// &lt; v₁.Y * v₂.Z - v₁.Z * v₂.Y , v₁.Z * v₂.X - v₁.X  *v₂.Z , v₁.X * v₂.Y - v₁.Y * v₂.X &gt;
        /// </remarks>
        /// <param name="vector1">
        /// The first vector.
        /// </param>
        /// <param name="vector2">
        /// The second vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F Cross(Vector3F vector1, Vector3F vector2) =>
            new Vector3F(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

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
        ///     More expensive than <see cref="DistanceSquared(Vector3F,Vector3F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float Distance(Vector3F vector1, Vector3F vector2) =>
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
        ///     Less expensive than <see cref="Distance(Vector3F,Vector3F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector3F vector1, Vector3F vector2) =>
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
        public static Vector3F Divide(Vector3F left, Vector3F right) =>
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
        public static Vector3F Divide(Vector3F left, float right) =>
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
        public static float Dot(Vector3F left, Vector3F right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z;

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
        public static Vector3F Lerp(Vector3F bounds1, Vector3F bounds2, float amount) =>
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
        public static Vector3F Max(Vector3F left, Vector3F right) =>
            new Vector3F(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z);

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
        public static Vector3F Min(Vector3F left, Vector3F right) =>
            new Vector3F(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z);

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
        public static Vector3F Multiply(Vector3F left, Vector3F right) =>
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
        public static Vector3F Multiply(Vector3F left, float right) =>
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
        public static Vector3F Multiply(float left, Vector3F right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F Negate(Vector3F value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F Normalize(Vector3F vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F operator -(Vector3F value) =>
            new Vector3F(-value.X, -value.Y, -value.Z);

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
        public static Vector3F operator -(Vector3F left, Vector3F right) =>
            new Vector3F(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

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
        public static bool operator !=(Vector3F left, Vector3F right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z;

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
        public static Vector3F operator *(Vector3F left, Vector3F right) =>
            new Vector3F(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

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
        public static Vector3F operator *(Vector3F left, float right) =>
            new Vector3F(left.X * right, left.Y * right, left.Z * right);

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
        public static Vector3F operator *(float left, Vector3F right) =>
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
        public static Vector3F operator /(Vector3F left, Vector3F right) =>
            new Vector3F(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

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
        public static Vector3F operator /(Vector3F left, float right) =>
            new Vector3F(left.X / right, left.Y / right, left.Z / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector3F operator +(Vector3F value) =>
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
        public static Vector3F operator +(Vector3F left, Vector3F right) =>
            new Vector3F(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

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
        public static bool operator ==(Vector3F left, Vector3F right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z;

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
        public static Vector3F Reflect(Vector3F vector, Vector3F normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3F SquareRoot(Vector3F value) =>
            new Vector3F(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));

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
        public static Vector3F Subtract(Vector3F left, Vector3F right) =>
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
        public static Vector3F Transform(Vector3F vector, Matrix4x4F matrix) =>
            new Vector3F(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);

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
        public static Vector3F Transform(Vector3F value, QuaternionF rotation)
        {
            var num = rotation.X + rotation.X;
            var num2 = rotation.Y + rotation.Y;
            var num3 = rotation.Z + rotation.Z;
            var num4 = rotation.W * num;
            var num5 = rotation.W * num2;
            var num6 = rotation.W * num3;
            var num7 = rotation.X * num;
            var num8 = rotation.X * num2;
            var num9 = rotation.X * num3;
            var num10 = rotation.Y * num2;
            var num11 = rotation.Y * num3;
            var num12 = rotation.Z * num3;
            return new Vector3F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
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
        public static Vector3F TransformNormal(Vector3F normal, Matrix4x4F matrix) =>
            new Vector3F(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                         normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                         normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector3F Abs() =>
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
        public Vector3F Clamp(Vector3F min, Vector3F max) =>
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
            if (span.Length - index < 3)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index + 0] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
        }

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3F"/>.
        /// </summary>
        public static explicit operator Vector3F(ReadOnlySpan<float> span) =>
            new Vector3F(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3F"/>.
        /// </summary>
        public static explicit operator Vector3F(Span<float> span) =>
            new Vector3F(span[0], span[1], span[2]);

        /// <summary>
        /// Computes the cross product of two vectors which is a vector perpendicular to the two vectors.
        /// </summary>
        /// <remarks>
        /// <para>The cross product performs the following:</para>
        /// &lt; v₁.Y * v₂.Z - v₁.Z * v₂.Y , v₁.Z * v₂.X - v₁.X  *v₂.Z , v₁.X * v₂.Y - v₁.Y * v₂.X &gt;
        /// </remarks>
        /// <param name="vector">
        /// The other vector.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F Cross(Vector3F vector) =>
            Cross(this, vector);

        /// <summary>
        ///     Deconstructs this vector into its <see cref="float"/> components and builds the
        ///     X and Y into a new <see cref="Vector2F"/>.
        /// </summary>
        /// <param name="vector">
        ///     The X and Y of this vector as a <see cref="Vector2F"/>.
        /// </param>
        /// <param name="z">
        ///     The Z of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2F vector, out float z)
        {
            vector = new Vector2F(X, Y);
            z = Z;
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
        /// <param name="z">
        ///     The Z of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <summary>
        ///     Returns the Euclidean distance between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     More expensive than <see cref="DistanceSquared(Vector3F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float Distance(Vector3F value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector3F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float DistanceSquared(Vector3F value) =>
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
        public float Dot(Vector3F vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector3F"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector3F"/>;
        ///     False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector3F vec && Equals(vec);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector3F"/> is equal to this <see cref="Vector3F"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector3F"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector3F"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector3F other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector3F"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector3F"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector3F"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector3F other, double delta)
        {
            if (delta is 0.0f)
                return this == other;

            var vector = Subtract(this, other).Abs();
            return vector.X < delta &&
                   vector.Y < delta &&
                   vector.Z < delta;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());

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
        public Vector3F Normalize() =>
            Normalize(this);

        /// <summary>
        /// Returns the reflection of this vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">
        /// The normal of the surface being reflected off.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F Reflect(Vector3F normal) =>
            Reflect(this, normal);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector3F SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        /// Returns a String representing this <see cref="Vector3F"/> instance.
        /// </summary>
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector3F"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector3F"/> instance, using the specified format to format
        ///     individual elements and the given <see cref="IFormatProvider"/>.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        /// <param name="formatProvider">
        ///     The format provider to use when formatting elements.
        /// </param>
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}>";

        /// <summary>
        ///     Transforms a copy of this vector by the given matrix.
        /// </summary>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        /// <summary>
        /// Transforms this vector normal by the given 4x4 matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F TransformNormal(Matrix4x4F matrix) =>
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
        ///     If provided, the X value for the new <see cref="Vector3F"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector3F"/>, otherwise <see cref="Y"/>.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new <see cref="Vector3F"/>, otherwise <see cref="Z"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3F With(float? x = null, float? y = null, float? z = null) =>
            new Vector3F(x ?? X, y ?? Y, z ?? Z);

        #endregion Public Methods
    }
}