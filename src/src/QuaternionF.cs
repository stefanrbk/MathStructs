using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public struct QuaternionF : IEquatable<QuaternionF>
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public float X;
        public float Y;
        public float Z;
        public float W;
        private readonly static QuaternionF _identity = new QuaternionF(0f, 0f, 0f, 1f);
        public static QuaternionF Identity => _identity;

        public bool IsIdentity =>
            this == _identity;

        public QuaternionF(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public QuaternionF(Vector3F vectorPart, float scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3F vectorPart, out float scalarPart)
        {
            vectorPart = new Vector3F(X, Y, Z);
            scalarPart = W;
        }

        [MethodImpl(Inline)]
        public QuaternionF With(float? x = null, float? y = null, float? z = null, float? w = null) =>
            new QuaternionF(x ?? X, y ?? Y, z ?? Z, w ?? W);

        [MethodImpl(Inline)]
        public float Length() =>
            MathF.Sqrt(LengthSquared());

        [MethodImpl(Inline)]
        public float LengthSquared() =>
            X * X + Y * Y + Z * Z + W * W;

        [MethodImpl(Inline)]
        public QuaternionF Normalize() =>
            this * (1 / Length());

        [MethodImpl(Inline)]
        public static QuaternionF Normalize(QuaternionF value) =>
            value.Normalize();

        [MethodImpl(Inline)]
        public QuaternionF Conjugate() =>
            -With(w: W);

        [MethodImpl(Inline)]
        public static QuaternionF Conjugate(QuaternionF value) =>
            value.Conjugate();

        [MethodImpl(Inline)]
        public QuaternionF Inverse() =>
            Conjugate() * (1 / Length());

        [MethodImpl(Inline)]
        public static QuaternionF Inverse(QuaternionF value) =>
            value.Inverse();

        [MethodImpl(Inline)]
        public static QuaternionF CreateFromAxisAngle(Vector3F axis, float angle)
        {
            var x = angle * 0.5f;
            return new QuaternionF(axis * MathF.Sin(x), MathF.Cos(x));
        }

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

            return new QuaternionF(n6 * n3 * n2 + n5 * n4 * n1,
                                   n5 * n4 * n2 - n6 * n3 * n1,
                                   n6 * n4 * n1 - n5 * n3 * n2,
                                   n6 * n4 * n2 + n5 * n3 * n1);
        }

        [MethodImpl(Inline)]
        public static QuaternionF CreateFromRotationMatrix(Matrix4x4F matrix)
        {
            var n = matrix.M11 + matrix.M12 + matrix.M13;
            if (n > 0f)
            {
                var n2 = MathF.Sqrt(n + 1);
                var n3 = 0.5f / n2;
                return new QuaternionF((matrix.M23 - matrix.M32) * n3,
                                       (matrix.M31 - matrix.M13) * n3,
                                       (matrix.M12 - matrix.M21) * n3,
                                       n2 * 0.5f);
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var n2 = MathF.Sqrt(1 + matrix.M11 - matrix.M22 - matrix.M33);
                var n3 = 0.5f / n2;
                return new QuaternionF(0.5f * n2,
                                       (matrix.M12 + matrix.M21) * n3,
                                       (matrix.M13 + matrix.M31) * n3,
                                       (matrix.M23 - matrix.M32) * n3);
            }
            else if (matrix.M22 > matrix.M33)
            {
                var n2 = MathF.Sqrt(1 + matrix.M22 - matrix.M11 - matrix.M33);
                var n3 = 0.5f / n2;
                return new QuaternionF((matrix.M21 + matrix.M12) * n3,
                                       0.5f * n2,
                                       (matrix.M32 + matrix.M23) * n3,
                                       (matrix.M31 - matrix.M13) * n3);
            }
            else
            {
                var n2 = MathF.Sqrt(1 + matrix.M33 - matrix.M11 - matrix.M22);
                var n3 = 0.5f / n2;
                return new QuaternionF((matrix.M31 + matrix.M13) * n3,
                                       (matrix.M32 + matrix.M23) * n3,
                                       0.5f * n2,
                                       (matrix.M12 + matrix.M21) * n3);
            }
        }

        [MethodImpl(Inline)]
        public float Dot(QuaternionF value) =>
            X * value.X + Y * value.Y + Z * value.Z + W * value.W;

        [MethodImpl(Inline)]
        public static float Dot(QuaternionF left, QuaternionF right) =>
            left.Dot(right);

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

        [MethodImpl(Inline)]
        public static QuaternionF Lerp(QuaternionF q1, QuaternionF q2, float amount)
        {
            var n = 1 - amount;
            var n2 = q1.Dot(q2);
            var result = (n2 >= 0) ? q1 * n + q2 * amount : q1 * n - q2 * amount;
            return result * (1 / result.Length());
        }

        [MethodImpl(Inline)]
        public QuaternionF Concatenate(QuaternionF value) =>
            Concatenate(this, value);

        [MethodImpl(Inline)]
        public static QuaternionF Concatenate(QuaternionF q1, QuaternionF q2)
        {
            (var v1, _) = q1;
            (var v2, _) = q2;
            var q3 = new QuaternionF(v1.Cross(v2), v1.Dot(v2));

            return q1 * q2.W + q2 * q1.W + q3;
        }

        [MethodImpl(Inline)]
        public static QuaternionF Negate(QuaternionF value) =>
            -value;

        [MethodImpl(Inline)]
        public static QuaternionF Add(QuaternionF left, QuaternionF right) =>
            left + right;

        [MethodImpl(Inline)]
        public static QuaternionF Subtract(QuaternionF left, QuaternionF right) =>
            left - right;

        [MethodImpl(Inline)]
        public static QuaternionF Multiply(QuaternionF left, QuaternionF right) =>
            left * right;

        [MethodImpl(Inline)]
        public static QuaternionF Multiply(QuaternionF left, float right) =>
            left * right;

        [MethodImpl(Inline)]
        public static QuaternionF Divide(QuaternionF left, QuaternionF right) =>
            left / right;

        [MethodImpl(Inline)]
        public static QuaternionF operator +(QuaternionF value) =>
            value;

        [MethodImpl(Inline)]
        public static QuaternionF operator -(QuaternionF value) =>
            new QuaternionF(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(Inline)]
        public static QuaternionF operator +(QuaternionF left, QuaternionF right) =>
            new QuaternionF(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(Inline)]
        public static QuaternionF operator -(QuaternionF left, QuaternionF right) =>
            new QuaternionF(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(Inline)]
        public static QuaternionF operator *(QuaternionF left, float right) =>
            new QuaternionF(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(Inline)]
        public static QuaternionF operator *(QuaternionF left, QuaternionF right)
        {
            (var x, var y, var z, var w) = left;
            (var x2, var y2, var z2, var w2) = right;

            var n1 = y * z2 - z * y2;
            var n2 = z * x2 - x * z2;
            var n3 = x * y2 - y * x2;
            var n4 = x * x2 + y * y2 + z * z2;

            return new QuaternionF(x * w2 + x2 * w + n1, y * w2 + y2 * w + n2, z * w2 + z2 * w + n3, w * w2 - n4);
        }

        [MethodImpl(Inline)]
        public static QuaternionF operator /(QuaternionF left, QuaternionF right) =>
            left * right.Inverse();

        [MethodImpl(Inline)]
        public static bool operator ==(QuaternionF left, QuaternionF right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(Inline)]
        public static bool operator !=(QuaternionF left, QuaternionF right) =>
            !(left == right);

        [MethodImpl(Inline)]
        public bool Equals(QuaternionF other) =>
            this == other;

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is QuaternionF q && this == q;

        [MethodImpl(Inline)]
        public override string ToString() =>
            $"{{{{X:{X} Y:{Y} Z:{Z} W:{W}}}}}";

        [MethodImpl(Inline)]
        public override int GetHashCode() =>
            X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
    }
}
