using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
    /// <summary>
    /// A structure encapsulating three double precision floating point values.
    /// </summary>
    /// <remarks>
    /// Slower than <see cref="Vector3"/> but more precise.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct Vector3D : IEquatable<Vector3D>, IFormattable
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

        /// <summary>
        ///     The Z component of the vector.
        /// </summary>
        [FieldOffset(16)]
        public double Z;

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
        public Vector3D(double value)
            : this(value, value, value) { }

        /// <summary>
        /// Constructs a <see cref="Vector3D"/> from the given <see cref="Vector2D"/> and a Z component.
        /// </summary>
        /// <param name="value">
        ///     The vector to use as the X and Y component.
        /// </param>
        /// <param name="z">
        ///     The Z component.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3D(Vector2D value, double z)
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
        public Vector3D(double x, double y, double z)
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
        public static Vector3D One =>
            new Vector3D(1d);

        /// <summary>
        /// Returns the vector &lt; 1, 0, 0 &gt;.
        /// </summary>
        public static Vector3D UnitX =>
            new Vector3D(1d, 0d, 0d);

        /// <summary>
        /// Returns the vector &lt; 0, 1, 0 &gt;.
        /// </summary>
        public static Vector3D UnitY =>
            new Vector3D(0d, 1d, 0d);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 1 &gt;.
        /// </summary>
        public static Vector3D UnitZ =>
            new Vector3D(0d, 0d, 1d);

        /// <summary>
        /// Returns the vector &lt; 0, 0, 0 &gt;.
        /// </summary>
        public static Vector3D Zero =>
            new Vector3D(0d);

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3D Abs(Vector3D value) =>
            new Vector3D(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));

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
        public static Vector3D Add(Vector3D left, Vector3D right) =>
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
        public static Vector3D Clamp(Vector3D value, Vector3D min, Vector3D max) =>
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
        public static Vector3D Cross(Vector3D vector1, Vector3D vector2) =>
            new Vector3D(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

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
        ///     More expensive than <see cref="DistanceSquared(Vector3D,Vector3D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static double Distance(Vector3D vector1, Vector3D vector2) =>
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
        ///     Less expensive than <see cref="Distance(Vector3D,Vector3D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector3D vector1, Vector3D vector2) =>
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
        public static Vector3D Divide(Vector3D left, Vector3D right) =>
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
        public static Vector3D Divide(Vector3D left, double right) =>
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
        public static double Dot(Vector3D left, Vector3D right) =>
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
        public static Vector3D Lerp(Vector3D bounds1, Vector3D bounds2, double amount) =>
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
        public static Vector3D Max(Vector3D left, Vector3D right) =>
            new Vector3D(left.X > right.X ? left.X : right.X,
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
        public static Vector3D Min(Vector3D left, Vector3D right) =>
            new Vector3D(left.X < right.X ? left.X : right.X,
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
        public static Vector3D Multiply(Vector3D left, Vector3D right) =>
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
        public static Vector3D Multiply(Vector3D left, double right) =>
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
        public static Vector3D Multiply(double left, Vector3D right) =>
            left * right;

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3D Negate(Vector3D value) =>
            -value;

        /// <summary>
        ///     Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">
        ///     The vector to normalize.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3D Normalize(Vector3D vector) =>
            vector / vector.Length();

        /// <summary>
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3D operator -(Vector3D value) =>
            new Vector3D(-value.X, -value.Y, -value.Z);

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
        public static Vector3D operator -(Vector3D left, Vector3D right) =>
            new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

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
        public static bool operator !=(Vector3D left, Vector3D right) =>
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
        public static Vector3D operator *(Vector3D left, Vector3D right) =>
            new Vector3D(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

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
        public static Vector3D operator *(Vector3D left, double right) =>
            new Vector3D(left.X * right, left.Y * right, left.Z * right);

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
        public static Vector3D operator *(double left, Vector3D right) =>
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
        public static Vector3D operator /(Vector3D left, Vector3D right) =>
            new Vector3D(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

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
        public static Vector3D operator /(Vector3D left, double right) =>
            new Vector3D(left.X / right, left.Y / right, left.Z / right);

        /// <summary>
        ///     Returns the unary plus of the provided vector (nop).
        /// </summary>
        [MethodImpl(Inline)]
        public static Vector3D operator +(Vector3D value) =>
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
        public static Vector3D operator +(Vector3D left, Vector3D right) =>
            new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

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
        public static bool operator ==(Vector3D left, Vector3D right) =>
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
        public static Vector3D Reflect(Vector3D vector, Vector3D normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector3D SquareRoot(Vector3D value) =>
            new Vector3D(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z));

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
        public static Vector3D Subtract(Vector3D left, Vector3D right) =>
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
        public static Vector3D Transform(Vector3D vector, Matrix4x4D matrix) =>
            new Vector3D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);

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
        public static Vector3D Transform(Vector3D value, QuaternionD rotation)
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
            return new Vector3D(value.X * (1d - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1d - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1d - num7 - num10));
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
        public static Vector3D TransformNormal(Vector3D normal, Matrix4x4D matrix) =>
            new Vector3D(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                         normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                         normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);

        /// <summary>
        ///     Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector3D Abs() =>
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
        public Vector3D Clamp(Vector3D min, Vector3D max) =>
            Clamp(this, min, max);

        /// <summary>
        ///     Copies the contents of the vector into the given span.
        /// </summary>
        [MethodImpl(Inline)]
        public void CopyTo(Span<double> array) =>
            CopyTo(array, 0);

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
        public void CopyTo(Span<double> array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 3)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
        }

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
        public Vector3D Cross(Vector3D vector) =>
            Cross(this, vector);

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
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2D vector, out double z)
        {
            vector = new Vector2D(X, Y);
            z = Z;
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
        /// <param name="z">
        ///     The Z of this vector.
        /// </param>
        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y, out double z)
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
        ///     More expensive than <see cref="DistanceSquared(Vector3D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public double Distance(Vector3D value) =>
            Distance(this, value);

        /// <summary>
        ///     Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="value">
        ///     The other point.
        /// </param>
        /// <remarks>
        ///     Less expensive than <see cref="Distance(Vector3D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public double DistanceSquared(Vector3D value) =>
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
        public double Dot(Vector3D vector) =>
            Dot(this, vector);

        /// <summary>
        ///     Returns a boolean indicating whether the given Object is equal to this <see cref="Vector3D"/> instance.
        /// </summary>
        /// <param name="obj">
        ///     The Object to compare against.
        /// </param>
        /// <returns>
        ///     True if the Object is equal to this <see cref="Vector3D"/>;
        ///     False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector3D vec && Equals(vec);

        /// <summary>
        ///     Returns a boolean indicating whether the given <see cref="Vector3D"/> is equal to this <see cref="Vector3D"/> instance.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector3D"/> to compare this instance to.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector3D"/> is equal to this instance; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector3D other) =>
            this == other;

        /// <summary>
        ///     Returns a boolean indicating whether the given Vector4D is equal to this <see cref="Vector3D"/> instance ± delta.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="Vector3D"/> to compare this instance to.
        /// </param>
        /// <param name="delta">
        ///     The allowable margin of error to determine equality for each element pair between the vectors.
        /// </param>
        /// <returns>
        ///     True if the other <see cref="Vector3D"/> is equal to this instance ± delta; False otherwise.
        /// </returns>
        [MethodImpl(Inline)]
        public bool Equals(Vector3D other, double delta)
        {
            if (delta is 0.0)
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
        public Vector3D Normalize() =>
            Normalize(this);

        /// <summary>
        /// Returns the reflection of this vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">
        /// The normal of the surface being reflected off.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3D Reflect(Vector3D normal) =>
            Reflect(this, normal);

        /// <summary>
        ///     Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        [MethodImpl(Inline)]
        public Vector3D SquareRoot() =>
            SquareRoot(this);

        /// <summary>
        /// Returns a String representing this <see cref="Vector3D"/> instance.
        /// </summary>
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector3D"/> instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">
        ///     The format of individual elements.
        /// </param>
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        ///     Returns a String representing this <see cref="Vector3D"/> instance, using the specified format to format
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
        public Vector3D Transform(Matrix4x4D matrix) =>
            Transform(this, matrix);

        /// <summary>
        /// Transforms this vector normal by the given 4x4 matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3D TransformNormal(Matrix4x4D matrix) =>
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
        ///     If provided, the X value for the new <see cref="Vector3D"/>, otherwise <see cref="X"/>.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new <see cref="Vector3D"/>, otherwise <see cref="Y"/>.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new <see cref="Vector3D"/>, otherwise <see cref="Z"/>.
        /// </param>
        [MethodImpl(Inline)]
        public Vector3D With(double? x = null, double? y = null, double? z = null) =>
            new Vector3D(x ?? X, y ?? Y, z ?? Z);

        #endregion Public Methods
    }
}