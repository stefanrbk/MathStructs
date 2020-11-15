using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating a four-dimensional vector (x,y,z,w),
    /// which is used to efficiently rotate an object about the (x,y,z)
    /// vector by the angle theta, where w = cos(theta/2).
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct QuaternionD : IEquatable<QuaternionD>
    {
        #region Public Fields

        /// <summary>
        /// Specifies the W-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(24)]
        public double W;

        /// <summary>
        /// Specifies the X-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(0)]
        public double X;

        /// <summary>
        /// Specifies the Y-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(8)]
        public double Y;

        /// <summary>
        /// Specifies the Z-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(16)]
        public double Z;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private readonly static QuaternionD _identity = new(0, 0, 0, 1);

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a Quaternion from the given components.
        /// </summary>
        /// <param name="x">The X component of the Quaternion.</param>
        /// <param name="y">The Y component of the Quaternion.</param>
        /// <param name="z">The Z component of the Quaternion.</param>
        /// <param name="w">The W component of the Quaternion.</param>
        public QuaternionD(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a Quaternion from the given vector and rotation parts.
        /// </summary>
        /// <param name="vectorPart">The vector part of the Quaternion.</param>
        /// <param name="scalarPart">The rotation part of the Quaternion.</param>
        public QuaternionD(Vector3D vectorPart, double scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns a Quaternion representing no rotation.
        /// </summary>
        public static QuaternionD Identity => _identity;

        /// <summary>
        /// Returns whether the Quaternion is the identity Quaternion.
        /// </summary>
        public bool IsIdentity =>
            this == _identity;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Add(QuaternionD left, QuaternionD right) =>
            left + right;

        /// <summary>
        /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
        /// </summary>
        /// <param name="value1">The first Quaternion rotation in the series.</param>
        /// <param name="value2">The second Quaternion rotation in the series.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Concatenate(QuaternionD value1, QuaternionD value2)
        {
            double x = value2.X;
            double y = value2.Y;
            double z = value2.Z;
            double w = value2.W;
            double x2 = value1.X;
            double y2 = value1.Y;
            double z2 = value1.Z;
            double w2 = value1.W;
            double num = y * z2 - z * y2;
            double num2 = z * x2 - x * z2;
            double num3 = x * y2 - y * x2;
            double num4 = x * x2 + y * y2 + z * z2;
            QuaternionD result;
            result.X = x * w2 + x2 * w + num;
            result.Y = y * w2 + y2 * w + num2;
            result.Z = z * w2 + z2 * w + num3;
            result.W = w * w2 - num4;
            return result;
        }

        /// <summary>
        /// Creates the conjugate of a specified Quaternion.
        /// </summary>
        /// <param name="value">The Quaternion of which to return the conjugate.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Conjugate(QuaternionD value) =>
            value.Conjugate();

        /// <summary>
        /// Creates a Quaternion from a normalized vector axis and an angle to rotate about the vector.
        /// </summary>
        /// <param name="axis">The unit vector to rotate around. This vector must be normalized before
        /// calling this function or the resulting Quaternion will be incorrect.</param>
        /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
        [MethodImpl(Inline)]
        public static QuaternionD CreateFromAxisAngle(Vector3D axis, double angle)
        {
            var x = angle * 0.5;
            return new(axis * Math.Sin(x), Math.Cos(x));
        }

        /// <summary>
        /// Creates a Quaternion from the given rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        [MethodImpl(Inline)]
        public static QuaternionD CreateFromRotationMatrix(Matrix4x4D matrix)
        {
            var n = matrix.M11 + matrix.M22 + matrix.M33;
            if (n > 0)
            {
                var n2 = Math.Sqrt(n + 1);
                var n3 = 0.5 / n2;
                return new((matrix.M23 - matrix.M32) * n3, (matrix.M31 - matrix.M13) * n3, (matrix.M12 - matrix.M21) * n3, n2 * 0.5);
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var n2 = Math.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
                var n3 = 0.5 / n2;
                return new(0.5 * n2, (matrix.M12 + matrix.M21) * n3, (matrix.M13 + matrix.M31) * n3, (matrix.M23 - matrix.M32) * n3);
            }
            else if (matrix.M22 > matrix.M33)
            {
                var n2 = Math.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
                var n3 = 0.5 / n2;
                return new((matrix.M21 + matrix.M12) * n3, 0.5 * n2, (matrix.M32 + matrix.M23) * n3, (matrix.M31 - matrix.M13) * n3);
            }
            else
            {
                var n2 = Math.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
                var n3 = 0.5 / n2;
                return new((matrix.M31 + matrix.M13) * n3, (matrix.M32 + matrix.M23) * n3, 0.5 * n2, (matrix.M12 - matrix.M21) * n3);
            }
        }

        /// <summary>
        /// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
        /// <param name="roll">The roll angle, in radians, around the z-axis.</param>
        [MethodImpl(Inline)]
        public static QuaternionD CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            yaw *= 0.5;
            pitch *= 0.5;
            roll *= 0.5;

            var n1 = Math.Sin(roll);
            var n2 = Math.Cos(roll);
            var n3 = Math.Sin(pitch);
            var n4 = Math.Cos(pitch);
            var n5 = Math.Sin(yaw);
            var n6 = Math.Cos(yaw);

            return new(n6 * n3 * n2 + n5 * n4 * n1, n5 * n4 * n2 - n6 * n3 * n1, n6 * n4 * n1 - n5 * n3 * n2, n6 * n4 * n2 + n5 * n3 * n1);
        }

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The divisor.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Divide(QuaternionD left, QuaternionD right) =>
            left / right;

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second source Quaternion.</param>
        [MethodImpl(Inline)]
        public static double Dot(QuaternionD left, QuaternionD right) =>
            left.Dot(right);

        /// <summary>
        /// Returns the inverse of a Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Inverse(QuaternionD value) =>
            value.Inverse();

        /// <summary>
        ///  Linearly interpolates between two quaternions.
        /// </summary>
        /// <param name="q1">The first source Quaternion.</param>
        /// <param name="q2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Lerp(QuaternionD q1, QuaternionD q2, double amount)
        {
            var n = 1 - amount;
            var n2 = q1.Dot(q2);
            var result = (n2 >= 0) ? q1 * n + q2 * amount : q1 * n - q2 * amount;
            return result * (1 / result.Length());
        }

        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="left">The Quaternion on the left side of the multiplication.</param>
        /// <param name="right">The Quaternion on the right side of the multiplication.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Multiply(QuaternionD left, QuaternionD right) =>
            left * right;

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The scalar value.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Multiply(QuaternionD left, double right) =>
            left * right;

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Negate(QuaternionD value) =>
            -value;

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Normalize(QuaternionD value) =>
            value.Normalize();

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator -(QuaternionD value) =>
            new(-value.X, -value.Y, -value.Z, -value.W);

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second Quaternion, to be subtracted from the first.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator -(QuaternionD left, QuaternionD right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are not equal.
        /// </summary>
        /// <param name="left">The first Quaternion to compare.</param>
        /// <param name="right">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are not equal; False otherwise.</returns>
        [MethodImpl(Inline)]
        public static bool operator !=(QuaternionD left, QuaternionD right) =>
            !(left == right);

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The scalar value.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator *(QuaternionD left, double right) =>
            new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="left">The Quaternion on the left side of the multiplication.</param>
        /// <param name="right">The Quaternion on the right side of the multiplication.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator *(QuaternionD left, QuaternionD right)
        {
            (var x, var y, var z, var w) = left;
            (var x2, var y2, var z2, var w2) = right;

            var n1 = y * z2 - z * y2;
            var n2 = z * x2 - x * z2;
            var n3 = x * y2 - y * x2;
            var n4 = x * x2 + y * y2 + z * z2;

            return new(x * w2 + x2 * w + n1, y * w2 + y2 * w + n2, z * w2 + z2 * w + n3, w * w2 - n4);
        }

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The divisor.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator /(QuaternionD left, QuaternionD right)
        {
            double x = left.X;
            double y = left.Y;
            double z = left.Z;
            double w = left.W;
            double num = right.X * right.X + right.Y * right.Y + right.Z * right.Z + right.W * right.W;
            double num2 = 1 / num;
            double num3 = (0 - right.X) * num2;
            double num4 = (0 - right.Y) * num2;
            double num5 = (0 - right.Z) * num2;
            double num6 = right.W * num2;
            double num7 = y * num5 - z * num4;
            double num8 = z * num3 - x * num5;
            double num9 = x * num4 - y * num3;
            double num10 = x * num3 + y * num4 + z * num5;
            QuaternionD result;
            result.X = x * num6 + num3 * w + num7;
            result.Y = y * num6 + num4 * w + num8;
            result.Z = z * num6 + num5 * w + num9;
            result.W = w * num6 - num10;
            return result;
        }

        /// <summary>
        /// Returns the Quaternion. (nop)
        /// </summary>
        [MethodImpl(Inline)]
        public static QuaternionD operator +(QuaternionD value) =>
            value;

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionD operator +(QuaternionD left, QuaternionD right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are equal.
        /// </summary>
        /// <param name="left">The first Quaternion to compare.</param>
        /// <param name="right">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are equal; False otherwise.</returns>
        [MethodImpl(Inline)]
        public static bool operator ==(QuaternionD left, QuaternionD right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        /// <summary>
        /// Interpolates between two quaternions, using spherical linear interpolation.
        /// </summary>
        /// <param name="q1">The first source Quaternion.</param>
        /// <param name="q2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Slerp(QuaternionD q1, QuaternionD q2, double amount)
        {
            var n = q1.Dot(q2);
            var f = false;
            if (n < 0)
            {
                f = true;
                n = -n;
            }
            double n2, n3;

            if (n > 0.999999)
            {
                n2 = 1 - amount;
                n3 = f ? -amount : amount;
            }
            else
            {
                var n4 = Math.Acos(n);
                var n5 = 1 / Math.Sin(n4);
                n2 = Math.Sin((1 - amount) * n4) * n5;
                n3 = f ? ((-Math.Sin(amount * n4)) * n5) : (Math.Sin(amount * n4) * n5);
            }
            return q1 * n2 + q2 * n3;
        }

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second Quaternion, to be subtracted from the first.</param>
        [MethodImpl(Inline)]
        public static QuaternionD Subtract(QuaternionD left, QuaternionD right) =>
            left - right;

        /// <summary>
        /// Concatenates two Quaternions; the result represents this rotation followed by the value rotation.
        /// </summary>
        /// <param name="value">The other Quaternion rotation.</param>
        [MethodImpl(Inline)]
        public QuaternionD Concatenate(QuaternionD value) =>
            Concatenate(this, value);

        /// <summary>
        /// Creates the conjugate of this Quaternion.
        /// </summary>
        [MethodImpl(Inline)]
        public QuaternionD Conjugate() =>
            (-this).With(w: W);

        /// <summary>
        /// Deconstructs this Quaternion into it's separate components.
        /// </summary>
        /// <param name="x">The X-value of the vector component of this Quaternion.</param>
        /// <param name="y">The Y-value of the vector component of this Quaternion.</param>
        /// <param name="z">The Z-value of the vector component of this Quaternion.</param>
        /// <param name="w">The W-value of the vector component of this Quaternion.</param>
        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Deconstructs this Quaternion into it's separate components with the X, Y, and Z
        /// components packed into a vector.
        /// </summary>
        /// <param name="vectorPart">The vector component of this Quaternion.</param>
        /// <param name="scalarPart">The scalar component of this Quaternion.</param>
        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3D vectorPart, out double scalarPart)
        {
            vectorPart = new Vector3D(X, Y, Z);
            scalarPart = W;
        }

        /// <summary>
        /// Calculates the dot product with another Quaternion.
        /// </summary>
        /// <param name="value">The other source Quaternion.</param>
        [MethodImpl(Inline)]
        public double Dot(QuaternionD value) =>
            X * value.X + Y * value.Y + Z * value.Z + W * value.W;

        /// <summary>
        /// Returns a boolean indicating whether the given Quaternion is equal to this Quaternion instance
        /// within a given delta margin of error.
        /// </summary>
        /// <param name="other">The Quaternion to compare this instance to.</param>
        /// <param name="delta">The margin of error for the equality.</param>
        /// <returns>True if the other Quaternion is within delta of this instance; False otherwise.</returns>
        [MethodImpl(Inline)]
        public bool Equals(QuaternionD other, double delta) =>
            delta == 0.0 ? this == other
                         : Math.Abs(X - other.X) < delta &&
                           Math.Abs(Y - other.Y) < delta &&
                           Math.Abs(Z - other.Z) < delta &&
                           Math.Abs(W - other.W) < delta;

        /// <summary>
        /// Returns a boolean indicating whether the given Quaternion is equal to this Quaternion instance.
        /// </summary>
        /// <param name="other">The Quaternion to compare this instance to.</param>
        /// <returns>True if the other Quaternion is equal to this instance; False otherwise.</returns>
        [MethodImpl(Inline)]
        public bool Equals(QuaternionD other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Quaternion instance.
        /// </summary>
        /// <param name="obj">The Object to compare this instance to.</param>
        /// <returns>True if the Object is equal to this Quaternion; False otherwise.</returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is QuaternionD q && this == q;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        [MethodImpl(Inline)]
        public override int GetHashCode() =>
            X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();

        /// <summary>
        /// Returns the inverse of this Quaternion.
        /// </summary>
        [MethodImpl(Inline)]
        public QuaternionD Inverse()
        {
            double num = X * X + Y * Y + Z * Z + W * W;
            double num2 = 1f / num;
            QuaternionD result;
            result.X = (0f - X) * num2;
            result.Y = (0f - Y) * num2;
            result.Z = (0f - Z) * num2;
            result.W = W * num2;
            return result;
        }

        /// <summary>
        /// Calculates the length of the Quaternion.
        /// </summary>
        /// <remarks>More expensive than <see cref="LengthSquared"/> if you need the squared length.</remarks>
        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(LengthSquared());

        /// <summary>
        /// Calculates the length of the Quaternion.
        /// </summary>
        /// <remarks>Less expensive than <see cref="Length"/> if you need the squared length.</remarks>
        [MethodImpl(Inline)]
        public double LengthSquared() =>
            X * X + Y * Y + Z * Z + W * W;

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        [MethodImpl(Inline)]
        public QuaternionD Normalize() =>
            this * (1 / Length());

        /// <summary>
        /// Returns a String representing this Quaternion instance.
        /// </summary>
        [MethodImpl(Inline)]
        public override string ToString() =>
            $"{{X:{X} Y:{Y} Z:{Z} W:{W}}}";

        /// <summary>
        ///     Record-like <see langword="with"/>-style constructor
        /// </summary>
        /// <param name="x">
        ///     If provided, the X value for the new Quaternion, otherwise the <see cref="X"/> of this Quaternion.
        /// </param>
        /// <param name="y">
        ///     If provided, the Y value for the new Quaternion, otherwise the <see cref="Y"/> of this Quaternion.
        /// </param>
        /// <param name="z">
        ///     If provided, the Z value for the new Quaternion, otherwise the <see cref="Z"/> of this Quaternion.
        /// </param>
        /// <param name="w">
        ///     If provided, the W value for the new Quaternion, otherwise the <see cref="W"/> of this Quaternion.
        /// </param>
        [MethodImpl(Inline)]
        public QuaternionD With(double? x = null, double? y = null, double? z = null, double? w = null) =>
            new(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Public Methods
    }
}