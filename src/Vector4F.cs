using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating four single precision floating point values.
    /// </summary>
    /// <remarks>
    /// Less precise than <see cref="Vector4D"/> but faster.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct Vector4F : IEquatable<Vector4F>, IFormattable
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

        /// <summary>
        ///     The W component of the vector.
        /// </summary>

        [FieldOffset(12)]
        public float W;

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
        public Vector4F(float value)
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
        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        ///     Constructs a <see cref="Vector4F"/> from the given <see cref="Vector3F"/> and a W component.
        /// </summary>
        /// <param name="vector">
        ///     The vector to use as the X, Y, and Z components.
        /// </param>
        /// <param name="w">
        ///     The W component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F(Vector3F vector, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        /// <summary>
        /// Constructs a <see cref="Vector4F"/> from the given <see cref="Vector2F"/> and a Z and W component.
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
        public Vector4F(Vector2F vector, float z, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the vector &lt; 1, 1, 1, 1 &gt;.
        /// </summary>
        public static Vector4F One =>
            new(1f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 1 &gt;.
        /// </summary>
        public static Vector4F UnitW =>
            new(0f, 0f, 0f, 1f);

        /// <summary>
        /// Returns the vector &lt; 1, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4F UnitX =>
            new(1f, 0f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 1, 0, 0 &gt;.
        /// </summary>
        public static Vector4F UnitY =>
            new(0f, 1f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 1, 0 &gt;.
        /// </summary>
        public static Vector4F UnitZ =>
            new(0f, 0f, 1f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4F Zero =>
            new(0f);

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F Abs(Vector4F value) =>
            new(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));

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
        public static Vector4F Add(Vector4F left, Vector4F right) =>
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
        public static Vector4F Clamp(Vector4F value, Vector4F min, Vector4F max) =>
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
        ///     More expensive than <see cref="DistanceSquared(Vector4F,Vector4F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float Distance(Vector4F vector1, Vector4F vector2) =>
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
        ///     Less expensive than <see cref="Distance(Vector4F,Vector4F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector4F vector1, Vector4F vector2) =>
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
        public static Vector4F Divide(Vector4F left, Vector4F right) =>
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
        public static Vector4F Divide(Vector4F left, float right) =>
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
        public static float Dot(Vector4F left, Vector4F right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z +
            left.W * right.W;

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
        public static Vector4F Lerp(Vector4F bounds1, Vector4F bounds2, float amount) =>
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
        public static Vector4F Max(Vector4F left, Vector4F right) =>
            new(left.X > right.X ? left.X : right.X, left.Y > right.Y ? left.Y : right.Y, left.Z > right.Z ? left.Z : right.Z, left.W > right.W ? left.W : right.W);

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
        public static Vector4F Min(Vector4F left, Vector4F right) =>
            new(left.X < right.X ? left.X : right.X, left.Y < right.Y ? left.Y : right.Y, left.Z < right.Z ? left.Z : right.Z, left.W < right.W ? left.W : right.W);

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
        public static Vector4F Multiply(Vector4F left, Vector4F right) =>
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
        public static Vector4F Multiply(Vector4F left, float right) =>
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
        public static Vector4F Multiply(float left, Vector4F right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F Negate(Vector4F value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F Normalize(Vector4F vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F operator -(Vector4F value) =>
            new(-value.X, -value.Y, -value.Z, -value.W);

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
        public static Vector4F operator -(Vector4F left, Vector4F right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

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
        public static bool operator !=(Vector4F left, Vector4F right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;

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
        public static Vector4F operator *(Vector4F left, Vector4F right) =>
            new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

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
        public static Vector4F operator *(Vector4F left, float right) =>
            new(left.X * right, left.Y * right, left.Z * right, left.W * right);

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
        public static Vector4F operator *(float left, Vector4F right) =>
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
        public static Vector4F operator /(Vector4F left, Vector4F right) =>
            new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

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
        public static Vector4F operator /(Vector4F left, float right) =>
            new(left.X / right, left.Y / right, left.Z / right, left.W / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector4F operator +(Vector4F value) =>
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
        public static Vector4F operator +(Vector4F left, Vector4F right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

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
        public static bool operator ==(Vector4F left, Vector4F right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F SquareRoot(Vector4F value) =>
            new(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));

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
        public static Vector4F Subtract(Vector4F left, Vector4F right) =>
            left - right;

        /// <summary>
        ///     Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">
        ///     The source vector.
        /// </param>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector2F position, Matrix4x4F matrix)
        {
            return new Vector4F(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
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
        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector3F vector, Matrix4x4F matrix) =>
            new(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + matrix.M44);

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
        public static Vector4F Transform(Vector4F vector, Matrix4x4F matrix) =>
            new(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);

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
        public static Vector4F Transform(Vector2F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
        }

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
        public static Vector4F Transform(Vector3F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
        }

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
        public static Vector4F Transform(Vector4F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
        }

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4F Abs() =>
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
        public Vector4F Clamp(Vector4F min, Vector4F max) =>
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
            if (span.Length - index < 4)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index + 0] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
            span[index + 3] = W;
        }

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4F"/>.
        /// </summary>
        public static explicit operator Vector4F(ReadOnlySpan<float> span) =>
            new Vector4F(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4F"/>.
        /// </summary>
        public static explicit operator Vector4F(Span<float> span) =>
            new Vector4F(span[0], span[1], span[2], span[3]);

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
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="float"/> components and builds the
        ///     X, Y, and Z into a new <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="vector">
        ///     The X, Y, and Z of this vector as a <see cref="Vector3F"/>.
        /// </param>
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3F vector, out float w)
        {
            w = W;
            vector = new Vector3F(X, Y, Z);
        }

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
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2F vector, out float z, out float w)
        {
            vector = new Vector2F(X, Y);
            z = Z;
            w = W;
        }

        /// <summary>
        ///     Returns the Euclidean distance between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     More expensive than <see cref="DistanceSquared(Vector4F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float Distance(Vector4F value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector4F)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public float DistanceSquared(Vector4F value) =>
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
        public float Dot(Vector4F vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector4F"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector4F"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector4F vec && Equals(vec);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector4F"/> is equal to this <see cref="Vector4F"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4F"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4F"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4F other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector4F"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4F"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4F"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4F other, float delta)
        {
            if (delta is 0.0f)
                return this == other;

            var vector = Subtract(this, other).Abs();
            return vector.X < delta &&
                   vector.Y < delta &&
                   vector.Z < delta &&
                   vector.W < delta;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        /// <summary>
        ///     Returns the length of the vector.
        /// </summary>
        /// <remarks>
        ///     <see cref="LengthSquared"/> is cheaper to use if you need to square this result.
        /// </remarks>
        [MethodImpl(Inline)]
        public float Length() =>
            MathF.Sqrt(Dot(this, this));

        /// <summary>
        ///     Returns the length of the vector squared.
        /// </summary>
        /// <remarks>
        ///     This operation is cheaper than <see cref="Length"/>.
        /// </remarks>
        [MethodImpl(Inline)]
        public float LengthSquared() =>
            Dot(this, this);

        /// <summary>
        ///     Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4F Normalize() =>
            Normalize(this);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4F SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        /// Returns a String representing this <see cref="Vector4F"/> instance.
        /// </summary>
        public override string ToString() =>
                                    ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector4F"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector4F"/> instance, using the specified format to format
        ///     individual elements and the given <see cref="IFormatProvider"/>.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        /// <param name="formatProvider">
        ///     The format provider to use when formatting elements.
        /// </param>
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}, {W.ToString(format, formatProvider)}>";

        /// <summary>
        ///     Transforms a copy of this vector by the given matrix.
        /// </summary>
        /// <param name="matrix">
        ///     The transformation matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Transforms a copy of this vector by the given <see cref="QuaternionF"/> rotation value.
        /// </summary>
        /// <param name="rotation">
        ///     The rotation to apply.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F Transfrom(QuaternionF rotation) =>
            Transform(this, rotation);

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new <see cref="Vector4F"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector4F"/>, otherwise <see cref="Y"/>.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new <see cref="Vector4F"/>, otherwise <see cref="Z"/>.
        /// </param>
        /// <param name="w">
        ///     If provided, the W value for the new <see cref="Vector4F"/>, otherwise <see cref="W"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4F With(float? x = null, float? y = null, float? z = null, float? w = null) =>
            new(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Public Methods
    }
}