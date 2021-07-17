using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
    /// <summary>
    /// A structure encapsulating two double precision floating point values.
    /// </summary>
    /// <remarks>
    /// Slower than <see cref="Vector2"/> but more precise.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public partial struct Vector2D : IEquatable<Vector2D>, IFormattable
    {
        #region Public Static Properties

        /// <summary>
        /// Returns the vector &lt; 0, 0 &gt;.
        /// </summary>
        public static Vector2D Zero =>
            default;

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

        #endregion Public Static Properties

        #region Public Instance Methods

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode() =>
            HashCode.Combine(X, Y);

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
        /// <remarks>
        ///     More expensive than <see cref="DistanceSquared(Vector2D,Vector2D)"/> if you need the squared distance.
        /// </remarks>
        [MethodImpl(Inline)]
        public static double Distance(Vector2D vector1, Vector2D vector2) =>
            ( vector1 - vector2 ).Length();

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
            ( vector1 - vector2 ).LengthSquared();

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
            bounds1 * ( 1d - amount ) + bounds2 * amount;

        /// <summary>
        ///     Transforms a vector by the given matrix.</summary>
        /// <param name="position">
        ///     The source vector.</param>
        /// <param name="matrix">
        ///     The transformation matrix.</param>
        /// <returns>
        ///     The transformed vector.</returns>
        public static Vector2D Transform(Vector2D position, Matrix3x2D matrix) =>
            new Vector2D(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M31,
                         position.Y * matrix.M12 + position.Y * matrix.M22 + matrix.M32);

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
        /// Transforms a vector normal by the given 3x2 matrix.
        /// </summary>
        /// <param name="normal">
        /// The source vector.
        /// </param>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D TransformNormal(Vector2D normal, Matrix3x2D matrix) =>
            new Vector2D(normal.X * matrix.M11 + normal.Y * matrix.M21,
                         normal.X * matrix.M12 + normal.Y * matrix.M22);

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
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wx2 = rotation.W * z2;
            var x3 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var y3 = rotation.Y * y2;
            var z3 = rotation.Z * z2;

            return new Vector2D(value.X * ( 1 - y3 - z3 ) + value.Y * ( xy2 - wx2 ),
                                value.X * ( xy2 + wx2 ) + value.Y * ( 1 - x3 - z3 ));
        }

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
        [MethodImpl(Inline)]
        public static Vector2D Add(Vector2D left, Vector2D right) =>
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
        [MethodImpl(Inline)]
        public static Vector2D Subtract(Vector2D left, Vector2D right) =>
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
        ///     Negates a given vector.
        /// </summary>
        /// <param name="value">
        ///     The source vector.
        /// </param>
        [MethodImpl(Inline)]
        public static Vector2D Negate(Vector2D value) =>
            -value;

        #endregion Public Operator Methods

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
    }
}