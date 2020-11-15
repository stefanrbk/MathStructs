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
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct QuaternionF : IEquatable<QuaternionF>
    {
        #region Public Fields

        /// <summary>
        /// Specifies the X-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(0)]
        public float X;

        /// <summary>
        /// Specifies the Y-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(4)]
        public float Y;

        /// <summary>
        /// Specifies the Z-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(8)]
        public float Z;

        /// <summary>
        /// Specifies the W-value of the vector component of the Quaternion.
        /// </summary>
        [FieldOffset(12)]
        public float W;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private readonly static QuaternionF _identity = new(0f, 0f, 0f, 1f);

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a Quaternion from the given components.
        /// </summary>
        /// <param name="x">The X component of the Quaternion.</param>
        /// <param name="y">The Y component of the Quaternion.</param>
        /// <param name="z">The Z component of the Quaternion.</param>
        /// <param name="w">The W component of the Quaternion.</param>
        public QuaternionF(float x, float y, float z, float w)
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
        public QuaternionF(Vector3F vectorPart, float scalarPart)
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
        public static QuaternionF Identity => _identity;

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
        public static QuaternionF Add(QuaternionF left, QuaternionF right) =>
            left + right;

        /// <summary>
        /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
        /// </summary>
        /// <param name="value1">The first Quaternion rotation in the series.</param>
        /// <param name="value2">The second Quaternion rotation in the series.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Concatenate(QuaternionF value1, QuaternionF value2)
        {
            float q1x = value2.X;
            float q1y = value2.Y;
            float q1z = value2.Z;
            float q1w = value2.W;

            float q2x = value1.X;
            float q2y = value1.Y;
            float q2z = value1.Z;
            float q2w = value1.W;

            float cx = q1y * q2z - q1z * q2y;
            float cy = q1z * q2x - q1x * q2z;
            float cz = q1x * q2y - q1y * q2x;

            float dot = q1x * q2x + q1y * q2y + q1z * q2z;

            QuaternionF result;
            result.X = q1x * q2w + q2x * q1w + cx;
            result.Y = q1y * q2w + q2y * q1w + cy;
            result.Z = q1z * q2w + q2z * q1w + cz;
            result.W = q1w * q2w - dot;

            return result;
        }

        /// <summary>
        /// Creates the conjugate of a specified Quaternion.
        /// </summary>
        /// <param name="value">The Quaternion of which to return the conjugate.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Conjugate(QuaternionF value) =>
            value.Conjugate();

        /// <summary>
        /// Creates a Quaternion from a normalized vector axis and an angle to rotate about the vector.
        /// </summary>
        /// <param name="axis">The unit vector to rotate around. This vector must be normalized before
        /// calling this function or the resulting Quaternion will be incorrect.</param>
        /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
        [MethodImpl(Inline)]
        public static QuaternionF CreateFromAxisAngle(Vector3F axis, float angle)
        {
            var x = angle * 0.5f;
            return new(axis * MathF.Sin(x), MathF.Cos(x));
        }

        /// <summary>
        /// Creates a Quaternion from the given rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        [MethodImpl(Inline)]
        public static QuaternionF CreateFromRotationMatrix(Matrix4x4F matrix)
        {
            var n = matrix.M11 + matrix.M22 + matrix.M33;
            if (n > 0f)
            {
                var n2 = MathF.Sqrt(n + 1);
                var n3 = 0.5f / n2;
                return new((matrix.M23 - matrix.M32) * n3, (matrix.M31 - matrix.M13) * n3, (matrix.M12 - matrix.M21) * n3, n2 * 0.5f);
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var n2 = MathF.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
                var n3 = 0.5f / n2;
                return new(0.5f * n2, (matrix.M12 + matrix.M21) * n3, (matrix.M13 + matrix.M31) * n3, (matrix.M23 - matrix.M32) * n3);
            }
            else if (matrix.M22 > matrix.M33)
            {
                var n2 = MathF.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
                var n3 = 0.5f / n2;
                return new((matrix.M21 + matrix.M12) * n3, 0.5f * n2, (matrix.M32 + matrix.M23) * n3, (matrix.M31 - matrix.M13) * n3);
            }
            else
            {
                var n2 = MathF.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
                var n3 = 0.5f / n2;
                return new((matrix.M31 + matrix.M13) * n3, (matrix.M32 + matrix.M23) * n3, 0.5f * n2, (matrix.M12 - matrix.M21) * n3);
            }
        }

        /// <summary>
        /// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
        /// <param name="roll">The roll angle, in radians, around the z-axis.</param>
        [MethodImpl(Inline)]
        public static QuaternionF CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            yaw *= 0.5f;
            pitch *= 0.5f;
            roll *= 0.5f;

            var n1 = MathF.Sin(roll);
            var n2 = MathF.Cos(roll);
            var n3 = MathF.Sin(pitch);
            var n4 = MathF.Cos(pitch);
            var n5 = MathF.Sin(yaw);
            var n6 = MathF.Cos(yaw);

            return new(n6 * n3 * n2 + n5 * n4 * n1, n5 * n4 * n2 - n6 * n3 * n1, n6 * n4 * n1 - n5 * n3 * n2, n6 * n4 * n2 + n5 * n3 * n1);
        }

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The divisor.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Divide(QuaternionF left, QuaternionF right) =>
            left / right;

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second source Quaternion.</param>
        [MethodImpl(Inline)]
        public static float Dot(QuaternionF left, QuaternionF right) =>
            left.Dot(right);

        /// <summary>
        /// Returns the inverse of a Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Inverse(QuaternionF value) =>
            value.Inverse();

        /// <summary>
        ///  Linearly interpolates between two quaternions.
        /// </summary>
        /// <param name="q1">The first source Quaternion.</param>
        /// <param name="q2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Lerp(QuaternionF q1, QuaternionF q2, float amount)
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
        public static QuaternionF Multiply(QuaternionF left, QuaternionF right) =>
            left * right;

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The scalar value.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Multiply(QuaternionF left, float right) =>
            left * right;

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Negate(QuaternionF value) =>
            -value;

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Normalize(QuaternionF value) =>
            value.Normalize();

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator -(QuaternionF value) =>
            new(-value.X, -value.Y, -value.Z, -value.W);

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second Quaternion, to be subtracted from the first.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator -(QuaternionF left, QuaternionF right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are not equal.
        /// </summary>
        /// <param name="left">The first Quaternion to compare.</param>
        /// <param name="right">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are not equal; False otherwise.</returns>
        [MethodImpl(Inline)]
        public static bool operator !=(QuaternionF left, QuaternionF right) =>
            !(left == right);

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The scalar value.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator *(QuaternionF left, float right) =>
            new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="left">The Quaternion on the left side of the multiplication.</param>
        /// <param name="right">The Quaternion on the right side of the multiplication.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator *(QuaternionF left, QuaternionF right)
        {
            (var q1x, var q1y, var q1z, var q1w) = left;
            (var q2x, var q2y, var q2z, var q2w) = right;

            var cx = q1y * q2z - q1z * q2y;
            var cy = q1z * q2x - q1x * q2z;
            var cz = q1x * q2y - q1y * q2x;

            var dot = q1x * q2x + q1y * q2y + q1z * q2z;

            return new(q1x * q2w + q2x * q1w + cx, q1y * q2w + q2y * q1w + cy, q1z * q2w + q2z * q1w + cz, q1w * q2w - dot);
        }

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="left">The source Quaternion.</param>
        /// <param name="right">The divisor.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator /(QuaternionF left, QuaternionF right)
        {
            float q1x = left.X;
            float q1y = left.Y;
            float q1z = left.Z;
            float q1w = left.W;

            float ls = right.X * right.X + right.Y * right.Y + right.Z * right.Z + right.W * right.W;
            float invNorm = 1f / ls;

            float q2x = -right.X * invNorm;
            float q2y = -right.Y * invNorm;
            float q2z = -right.Z * invNorm;
            float q2w = right.W * invNorm;

            float cx = q1y * q2z - q1z * q2y;
            float cy = q1z * q2x - q1x * q2z;
            float cz = q1x * q2y - q1y * q2x;

            float dot = q1x * q2x + q1y * q2y + q1z * q2z;

            QuaternionF result;

            result.X = q1x * q2w + q2x * q1w + cx;
            result.Y = q1y * q2w + q2y * q1w + cy;
            result.Z = q1z * q2w + q2z * q1w + cz;
            result.W = q1w * q2w - dot;

            return result;
        }

        /// <summary>
        /// Returns the Quaternion. (nop)
        /// </summary>
        [MethodImpl(Inline)]
        public static QuaternionF operator +(QuaternionF value) =>
            value;

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second source Quaternion.</param>
        [MethodImpl(Inline)]
        public static QuaternionF operator +(QuaternionF left, QuaternionF right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are equal.
        /// </summary>
        /// <param name="left">The first Quaternion to compare.</param>
        /// <param name="right">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are equal; False otherwise.</returns>
        [MethodImpl(Inline)]
        public static bool operator ==(QuaternionF left, QuaternionF right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        /// <summary>
        /// Interpolates between two quaternions, using spherical linear interpolation.
        /// </summary>
        /// <param name="q1">The first source Quaternion.</param>
        /// <param name="q2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Slerp(QuaternionF q1, QuaternionF q2, float amount)
        {
            var n = q1.Dot(q2);
            var f = false;
            if (n < 0)
            {
                f = true;
                n = -n;
            }
            float n2, n3;

            if (n > 0.999999f)
            {
                n2 = 1 - amount;
                n3 = f ? -amount : amount;
            }
            else
            {
                var n4 = MathF.Acos(n);
                var n5 = 1 / MathF.Sin(n4);
                n2 = MathF.Sin((1 - amount) * n4) * n5;
                n3 = f ? ((-MathF.Sin(amount * n4)) * n5) : (MathF.Sin(amount * n4) * n5);
            }
            return q1 * n2 + q2 * n3;
        }

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="left">The first source Quaternion.</param>
        /// <param name="right">The second Quaternion, to be subtracted from the first.</param>
        [MethodImpl(Inline)]
        public static QuaternionF Subtract(QuaternionF left, QuaternionF right) =>
            left - right;

        /// <summary>
        /// Concatenates two Quaternions; the result represents this rotation followed by the value rotation.
        /// </summary>
        /// <param name="value">The other Quaternion rotation.</param>
        [MethodImpl(Inline)]
        public QuaternionF Concatenate(QuaternionF value) =>
            Concatenate(this, value);

        /// <summary>
        /// Creates the conjugate of this Quaternion.
        /// </summary>
        [MethodImpl(Inline)]
        public QuaternionF Conjugate() =>
            (-this).With(w: W);

        /// <summary>
        /// Deconstructs this Quaternion into it's separate components.
        /// </summary>
        /// <param name="x">The X-value of the vector component of this Quaternion.</param>
        /// <param name="y">The Y-value of the vector component of this Quaternion.</param>
        /// <param name="z">The Z-value of the vector component of this Quaternion.</param>
        /// <param name="w">The W-value of the vector component of this Quaternion.</param>
        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z, out float w)
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
        public void Deconstruct(out Vector3F vectorPart, out float scalarPart)
        {
            vectorPart = new Vector3F(X, Y, Z);
            scalarPart = W;
        }

        /// <summary>
        /// Calculates the dot product with another Quaternion.
        /// </summary>
        /// <param name="value">The other source Quaternion.</param>
        [MethodImpl(Inline)]
        public float Dot(QuaternionF value) =>
            X * value.X + Y * value.Y + Z * value.Z + W * value.W;

        /// <summary>
        /// Returns a boolean indicating whether the given Quaternion is equal to this Quaternion instance.
        /// </summary>
        /// <param name="other">The Quaternion to compare this instance to.</param>
        /// <returns>True if the other Quaternion is equal to this instance; False otherwise.</returns>
        [MethodImpl(Inline)]
        public bool Equals(QuaternionF other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Quaternion instance.
        /// </summary>
        /// <param name="obj">The Object to compare this instance to.</param>
        /// <returns>True if the Object is equal to this Quaternion; False otherwise.</returns>
        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is QuaternionF q && this == q;

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
        public QuaternionF Inverse()
        {
            float num = X * X + Y * Y + Z * Z + W * W;
            float num2 = 1f / num;
            QuaternionF result;
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
        public float Length() =>
            MathF.Sqrt(LengthSquared());

        /// <summary>
        /// Calculates the length of the Quaternion.
        /// </summary>
        /// <remarks>Less expensive than <see cref="Length"/> if you need the squared length.</remarks>
        [MethodImpl(Inline)]
        public float LengthSquared() =>
            X * X + Y * Y + Z * Z + W * W;

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        [MethodImpl(Inline)]
        public QuaternionF Normalize() =>
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
        public QuaternionF With(float? x = null, float? y = null, float? z = null, float? w = null) =>
            new(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Public Methods
    }
}