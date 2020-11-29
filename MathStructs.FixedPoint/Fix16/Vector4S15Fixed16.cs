using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating four fixed point values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector4Fix16 : IEquatable<Vector4Fix16>, IFormattable
    {
        #region Public Fields

        /// <summary>
        ///     The X component of the vector.
        /// </summary>
        [FieldOffset(0)]
        public readonly int X;

        /// <summary>
        ///     The Y component of the vector.
        /// </summary>

        [FieldOffset(4)]
        public readonly int Y;

        /// <summary>
        ///     The Z component of the vector.
        /// </summary>

        [FieldOffset(8)]
        public readonly int Z;

        /// <summary>
        ///     The W component of the vector.
        /// </summary>

        [FieldOffset(12)]
        public readonly int W;

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
        public Vector4Fix16(int value)
            : this(value, value, value, value) { }

        [MethodImpl(Inline)]
        public Vector4Fix16(double value)
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
        public Vector4Fix16(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(Inline)]
        public Vector4Fix16(double x, double y, double z, double w)
            : this((int)x,
                   (int)y,
                   (int)z,
                   (int)w) { }

        /// <summary>
        ///     Constructs a <see cref="Vector4Fix16"/> from the given <see cref="Vector3Fix16"/> and a W component.
        /// </summary>
        /// <param name="vector">
        ///     The vector to use as the X, Y, and Z components.
        /// </param>
        /// <param name="w">
        ///     The W component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4Fix16(Vector3Fix16 vector, int w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        [MethodImpl(Inline)]
        public Vector4Fix16(Vector3Fix16 vector, double w)
            : this(vector, (int)w) { }

        /// <summary>
        /// Constructs a <see cref="Vector4Fix16"/> from the given <see cref="Vector2Fix16"/> and a Z and W component.
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
        public Vector4Fix16(Vector2Fix16 vector, int z, int w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        [MethodImpl(Inline)]
        public Vector4Fix16(Vector2Fix16 vector, double z, double w)
            : this(vector, (int)z, (int)w) { }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the vector &lt; 1, 1, 1, 1 &gt;.
        /// </summary>
        public static Vector4Fix16 One =>
            new(1f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 1 &gt;.
        /// </summary>
        public static Vector4Fix16 UnitW =>
            new(0f, 0f, 0f, 1f);

        /// <summary>
        /// Returns the vector &lt; 1, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4Fix16 UnitX =>
            new(1f, 0f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 1, 0, 0 &gt;.
        /// </summary>
        public static Vector4Fix16 UnitY =>
            new(0f, 1f, 0f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 1, 0 &gt;.
        /// </summary>
        public static Vector4Fix16 UnitZ =>
            new(0f, 0f, 1f, 0f);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0, 0 &gt;.
        /// </summary>
        public static Vector4Fix16 Zero =>
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
        public static Vector4Fix16 Abs(Vector4Fix16 value) =>
            new(Math.Abs((double)value.X), Math.Abs((double)value.Y), Math.Abs((double)value.Z), Math.Abs((double)value.W));

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
        public static Vector4Fix16 Add(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Clamp(Vector4Fix16 value, Vector4Fix16 min, Vector4Fix16 max) =>
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
        ///     More expensive than <see cref="DistanceSquared(Vector4Fix16,Vector4Fix16)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static int Distance(Vector4Fix16 vector1, Vector4Fix16 vector2) =>
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
        ///     Less expensive than <see cref="Distance(Vector4Fix16,Vector4Fix16)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static int DistanceSquared(Vector4Fix16 vector1, Vector4Fix16 vector2) =>
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
        public static Vector4Fix16 Divide(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Divide(Vector4Fix16 left, int right) =>
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
        public static int Dot(Vector4Fix16 left, Vector4Fix16 right) =>
            (left.X * right.X) +
            (left.Y * right.Y) +
            ( left.Z * right.Z ) +
            ( left.W * right.W );

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
        public static Vector4Fix16 Lerp(Vector4Fix16 bounds1, Vector4Fix16 bounds2, int amount) =>
            (bounds1 * ((int)1f - amount)) + (bounds2 * amount);

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
        public static Vector4Fix16 Max(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Min(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Multiply(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Multiply(Vector4Fix16 left, int right) =>
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
        public static Vector4Fix16 Multiply(int left, Vector4Fix16 right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4Fix16 Negate(Vector4Fix16 value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4Fix16 Normalize(Vector4Fix16 vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4Fix16 operator -(Vector4Fix16 value) =>
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
        public static Vector4Fix16 operator -(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static bool operator !=(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 operator *(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 operator *(Vector4Fix16 left, int right) =>
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
        public static Vector4Fix16 operator *(int left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 operator /(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 operator /(Vector4Fix16 left, int right) =>
            new(left.X / right, left.Y / right, left.Z / right, left.W / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector4Fix16 operator +(Vector4Fix16 value) =>
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
        public static Vector4Fix16 operator +(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static bool operator ==(Vector4Fix16 left, Vector4Fix16 right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector4Fix16 SquareRoot(Vector4Fix16 value) =>
            new(Math.Sqrt((double)value.X), Math.Sqrt((double)value.Y), Math.Sqrt((double)value.Z), Math.Sqrt((double)value.W));

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
        public static Vector4Fix16 Subtract(Vector4Fix16 left, Vector4Fix16 right) =>
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
        public static Vector4Fix16 Transform(Vector2Fix16 position, Matrix4x4Fix16 matrix) =>
            new Vector4Fix16(( position.X * matrix.M11 ) + ( position.Y * matrix.M21 ) + matrix.M41,
                             ( position.X * matrix.M12 ) + ( position.Y * matrix.M22 ) + matrix.M42,
                             ( position.X * matrix.M13 ) + ( position.Y * matrix.M23 ) + matrix.M43,
                             ( position.X * matrix.M14 ) + ( position.Y * matrix.M24 ) + matrix.M44);

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
        public static Vector4Fix16 Transform(Vector3Fix16 vector, Matrix4x4Fix16 matrix) =>
            new(( vector.X * matrix.M11 ) + ( vector.Y * matrix.M21 ) + ( vector.Z * matrix.M31 ) + matrix.M41, 
                ( vector.X * matrix.M12 ) + ( vector.Y * matrix.M22 ) + ( vector.Z * matrix.M32 ) + matrix.M42, 
                ( vector.X * matrix.M13 ) + ( vector.Y * matrix.M23 ) + ( vector.Z * matrix.M33 ) + matrix.M43, 
                ( vector.X * matrix.M14 ) + ( vector.Y * matrix.M24 ) + ( vector.Z * matrix.M34 ) + matrix.M44);

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
        public static Vector4Fix16 Transform(Vector4Fix16 vector, Matrix4x4Fix16 matrix) =>
            new(( vector.X * matrix.M11 ) + ( vector.Y * matrix.M21 ) + ( vector.Z * matrix.M31 ) + ( vector.W * matrix.M41 ),
                ( vector.X * matrix.M12 ) + ( vector.Y * matrix.M22 ) + ( vector.Z * matrix.M32 ) + ( vector.W * matrix.M42 ),
                ( vector.X * matrix.M13 ) + ( vector.Y * matrix.M23 ) + ( vector.Z * matrix.M33 ) + ( vector.W * matrix.M43 ),
                ( vector.X * matrix.M14 ) + ( vector.Y * matrix.M24 ) + ( vector.Z * matrix.M34 ) + ( vector.W * matrix.M44 ));

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4Fix16 Abs() =>
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
        public Vector4Fix16 Clamp(Vector4Fix16 min, Vector4Fix16 max) =>
            Clamp(this, min, max);

        /// <summary>
        ///     Copies the contents of the vector into the given span.
        /// </summary>
        [MethodImpl(Inline)]
        public void CopyTo(Span<int> span) =>
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
        public void CopyTo(Span<int> span, int index)
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
        ///     Deconstructs this vector into its <see cref="int"/> components.
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
        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="int"/> components and builds the
        ///     X, Y, and Z into a new <see cref="Vector3Fix16"/>.
        /// </summary>
        /// <param name="vector">
        ///     The X, Y, and Z of this vector as a <see cref="Vector3Fix16"/>.
        /// </param>
        /// <param name="w">
        ///     The W of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3Fix16 vector, out int w)
        {
            w = W;
            vector = new Vector3Fix16(X, Y, Z);
        }

        /// <summary>
        ///     Deconstructs this vector into its <see cref="int"/> components and builds the
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
        public void Deconstruct(out Vector2Fix16 vector, out int z, out int w)
        {
            vector = new Vector2Fix16(X, Y);
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
        ///     More expensive than <see cref="DistanceSquared(Vector4Fix16)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public int Distance(Vector4Fix16 value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector4Fix16)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public int DistanceSquared(Vector4Fix16 value) =>
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
        public int Dot(Vector4Fix16 vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector4Fix16"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector4Fix16"/>;
        ///     <see langword="false"/> otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector4Fix16 vec && Equals(vec);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector4Fix16"/> is equal to this <see cref="Vector4Fix16"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4Fix16"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4Fix16"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4Fix16 other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector4Fix16"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector4Fix16"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector4Fix16"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector4Fix16 other, int delta)
        {
            if (delta is 0)
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
        public int Length() =>
            (int)Math.Sqrt((double)Dot(this, this));

        /// <summary>
        ///     Returns the length of the vector squared.
        /// </summary>
        /// <remarks>
        ///     This operation is cheaper than <see cref="Length"/>.
        /// </remarks>
        [MethodImpl(Inline)]
        public int LengthSquared() =>
            Dot(this, this);

        /// <summary>
        ///     Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4Fix16 Normalize() =>
            Normalize(this);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector4Fix16 SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        /// Returns a String representing this <see cref="Vector4Fix16"/> instance.
        /// </summary>
        public override string ToString() =>
                                    ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector4Fix16"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector4Fix16"/> instance, using the specified format to format
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
        public Vector4Fix16 Transform(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new <see cref="Vector4Fix16"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector4Fix16"/>, otherwise <see cref="Y"/>.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new <see cref="Vector4Fix16"/>, otherwise <see cref="Z"/>.
        /// </param>
        /// <param name="w">
        ///     If provided, the W value for the new <see cref="Vector4Fix16"/>, otherwise <see cref="W"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector4Fix16 With(int? x = null, int? y = null, int? z = null, int? w = null) =>
            new(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Public Methods
    }
}