using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public struct QuaternionD : IEquatable<QuaternionD>
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public double X;
        public double Y;
        public double Z;
        public double W;
        private readonly static QuaternionD _identity = new QuaternionD(0, 0, 0, 1);
        public static QuaternionD Identity => _identity;

        public bool IsIdentity =>
            this == _identity;

        public QuaternionD(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public QuaternionD(Vector3D vectorPart, double scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3D vectorPart, out double scalarPart)
        {
            vectorPart = new Vector3D(X, Y, Z);
            scalarPart = W;
        }

        [MethodImpl(Inline)]
        public QuaternionD With(double? x = null, double? y = null, double? z = null, double? w = null) =>
            new QuaternionD(x ?? X, y ?? Y, z ?? Z, w ?? W);

        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(LengthSquared());

        [MethodImpl(Inline)]
        public double LengthSquared() =>
            X * X + Y * Y + Z * Z + W * W;

        [MethodImpl(Inline)]
        public QuaternionD Normalize() =>
            this * (1 / Length());

        [MethodImpl(Inline)]
        public static QuaternionD Normalize(QuaternionD value) =>
            value.Normalize();

        [MethodImpl(Inline)]
        public QuaternionD Conjugate() =>
            (-this).With(w: W);

        [MethodImpl(Inline)]
        public static QuaternionD Conjugate(QuaternionD value) =>
            value.Conjugate();

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

        [MethodImpl(Inline)]
        public static QuaternionD Inverse(QuaternionD value) =>
            value.Inverse();

        [MethodImpl(Inline)]
        public static QuaternionD CreateFromAxisAngle(Vector3D axis, double angle)
        {
            var x = angle * 0.5;
            return new QuaternionD(axis * Math.Sin(x), Math.Cos(x));
        }

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

            return new QuaternionD(n6 * n3 * n2 + n5 * n4 * n1,
                                   n5 * n4 * n2 - n6 * n3 * n1,
                                   n6 * n4 * n1 - n5 * n3 * n2,
                                   n6 * n4 * n2 + n5 * n3 * n1);
        }

        [MethodImpl(Inline)]
        public static QuaternionD CreateFromRotationMatrix(Matrix4x4D matrix)
        {
            var n = matrix.M11 + matrix.M22 + matrix.M33;
            if (n > 0)
            {
                var n2 = Math.Sqrt(n + 1);
                var n3 = 0.5 / n2;
                return new QuaternionD((matrix.M23 - matrix.M32) * n3,
                                       (matrix.M31 - matrix.M13) * n3,
                                       (matrix.M12 - matrix.M21) * n3,
                                       n2 * 0.5);
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var n2 = Math.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
                var n3 = 0.5 / n2;
                return new QuaternionD(0.5 * n2,
                                       (matrix.M12 + matrix.M21) * n3,
                                       (matrix.M13 + matrix.M31) * n3,
                                       (matrix.M23 - matrix.M32) * n3);
            }
            else if (matrix.M22 > matrix.M33)
            {
                var n2 = Math.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
                var n3 = 0.5 / n2;
                return new QuaternionD((matrix.M21 + matrix.M12) * n3,
                                       0.5 * n2,
                                       (matrix.M32 + matrix.M23) * n3,
                                       (matrix.M31 - matrix.M13) * n3);
            }
            else
            {
                var n2 = Math.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
                var n3 = 0.5 / n2;
                return new QuaternionD((matrix.M31 + matrix.M13) * n3,
                                       (matrix.M32 + matrix.M23) * n3,
                                       0.5 * n2,
                                       (matrix.M12 - matrix.M21) * n3);
            }
        }

        [MethodImpl(Inline)]
        public double Dot(QuaternionD value) =>
            X * value.X + Y * value.Y + Z * value.Z + W * value.W;

        [MethodImpl(Inline)]
        public static double Dot(QuaternionD left, QuaternionD right) =>
            left.Dot(right);

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

        [MethodImpl(Inline)]
        public static QuaternionD Lerp(QuaternionD q1, QuaternionD q2, double amount)
        {
            var n = 1 - amount;
            var n2 = q1.Dot(q2);
            var result = (n2 >= 0) ? q1 * n + q2 * amount : q1 * n - q2 * amount;
            return result * (1 / result.Length());
        }

        [MethodImpl(Inline)]
        public QuaternionD Concatenate(QuaternionD value) =>
            Concatenate(this, value);

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

        [MethodImpl(Inline)]
        public static QuaternionD Negate(QuaternionD value) =>
            -value;

        [MethodImpl(Inline)]
        public static QuaternionD Add(QuaternionD left, QuaternionD right) =>
            left + right;

        [MethodImpl(Inline)]
        public static QuaternionD Subtract(QuaternionD left, QuaternionD right) =>
            left - right;

        [MethodImpl(Inline)]
        public static QuaternionD Multiply(QuaternionD left, QuaternionD right) =>
            left * right;

        [MethodImpl(Inline)]
        public static QuaternionD Multiply(QuaternionD left, double right) =>
            left * right;

        [MethodImpl(Inline)]
        public static QuaternionD Divide(QuaternionD left, QuaternionD right) =>
            left / right;

        [MethodImpl(Inline)]
        public static QuaternionD operator +(QuaternionD value) =>
            value;

        [MethodImpl(Inline)]
        public static QuaternionD operator -(QuaternionD value) =>
            new QuaternionD(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(Inline)]
        public static QuaternionD operator +(QuaternionD left, QuaternionD right) =>
            new QuaternionD(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(Inline)]
        public static QuaternionD operator -(QuaternionD left, QuaternionD right) =>
            new QuaternionD(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(Inline)]
        public static QuaternionD operator *(QuaternionD left, double right) =>
            new QuaternionD(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(Inline)]
        public static QuaternionD operator *(QuaternionD left, QuaternionD right)
        {
            (var x, var y, var z, var w) = left;
            (var x2, var y2, var z2, var w2) = right;

            var n1 = y * z2 - z * y2;
            var n2 = z * x2 - x * z2;
            var n3 = x * y2 - y * x2;
            var n4 = x * x2 + y * y2 + z * z2;

            return new QuaternionD(x * w2 + x2 * w + n1, y * w2 + y2 * w + n2, z * w2 + z2 * w + n3, w * w2 - n4);
        }

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

        [MethodImpl(Inline)]
        public static bool operator ==(QuaternionD left, QuaternionD right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(Inline)]
        public static bool operator !=(QuaternionD left, QuaternionD right) =>
            !(left == right);

        [MethodImpl(Inline)]
        public bool Equals(QuaternionD other, double delta) =>
            delta == 0.0 ? this == other
                         : Math.Abs(X - other.X) < delta &&
                           Math.Abs(Y - other.Y) < delta &&
                           Math.Abs(Z - other.Z) < delta &&
                           Math.Abs(W - other.W) < delta;

        [MethodImpl(Inline)]
        public bool Equals(QuaternionD other) =>
            this == other;

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is QuaternionD q && this == q;

        [MethodImpl(Inline)]
        public override string ToString() =>
            $"{{X:{X} Y:{Y} Z:{Z} W:{W}}}";

        [MethodImpl(Inline)]
        public override int GetHashCode() =>
            X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
    }
}
