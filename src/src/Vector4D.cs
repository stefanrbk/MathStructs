using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    public struct Vector4D : IEquatable<Vector4D>, IFormattable
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public double X;
        public double Y;
        public double Z;
        public double W;

        public static Vector4D Zero =>
            new Vector4D(0f);
        public static Vector4D One =>
            new Vector4D(1f);
        public static Vector4D UnitX =>
            new Vector4D(1f, 0f, 0f, 0f);
        public static Vector4D UnitY =>
            new Vector4D(0f, 1f, 0f, 0f);
        public static Vector4D UnitZ =>
            new Vector4D(0f, 0f, 1f, 0f);
        public static Vector4D UnitW =>
            new Vector4D(0f, 0f, 0f, 1f);
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector4D vec && Equals(vec);
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}, {W.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public double LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public static double Distance(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public double Distance(Vector4D value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public double DistanceSquared(Vector4D value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public static Vector4D Normalize(Vector4D vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public Vector4D Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public static Vector4D Clamp(Vector4D value, Vector4D min, Vector4D max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public Vector4D Clamp(Vector4D min, Vector4D max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public static Vector4D Lerp(Vector4D bounds1, Vector4D bounds2, double amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector2D position, Matrix4x4F matrix)
        {
            return new Vector4D(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
        }

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector3D vector, Matrix4x4F matrix) =>
            new Vector4D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + matrix.M44);

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector4D vector, Matrix4x4F matrix) =>
            new Vector4D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector2D value, QuaternionF rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
        }

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector3D value, QuaternionF rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
        }

        [MethodImpl(Inline)]
        public static Vector4D Transform(Vector4D value, QuaternionF rotation)
        {
            double num = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num3 = rotation.Z + rotation.Z;
            double num4 = rotation.W * num;
            double num5 = rotation.W * num2;
            double num6 = rotation.W * num3;
            double num7 = rotation.X * num;
            double num8 = rotation.X * num2;
            double num9 = rotation.X * num3;
            double num10 = rotation.Y * num2;
            double num11 = rotation.Y * num3;
            double num12 = rotation.Z * num3;
            return new Vector4D(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
        }

        [MethodImpl(Inline)]
        public Vector4D Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector4D Add(Vector4D left, Vector4D right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector4D Subtract(Vector4D left, Vector4D right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector4D Multiply(Vector4D left, Vector4D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4D Multiply(Vector4D left, double right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4D Multiply(double left, Vector4D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4D Divide(Vector4D left, Vector4D right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector4D Divide(Vector4D left, double right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector4D Negate(Vector4D value) =>
            -value;

        public Vector4D(double value)
            : this(value, value, value, value) { }

        public Vector4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4D(Vector3D vector, double w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        public Vector4D(Vector2D vector, double z, double w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
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
        public void Deconstruct(out Vector3D vector, out double w)
        {
            w = W;
            vector = new Vector3D(X, Y, Z);
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2D vector, out double z, out double w)
        {
            vector = new Vector2D(X, Y);
            z = Z;
            w = W;
        }

        [MethodImpl(Inline)]
        public Vector4D With(double? x = null, double? y = null, double? z = null, double? w = null) =>
            new Vector4D(x ?? X, y ?? Y, z ?? Z, w ?? W);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 4)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
            array[index + 3] = W;
        }

        [MethodImpl(Inline)]
        public bool Equals(Vector4D other) =>
            this == other;

        [MethodImpl(Inline)]
        public static double Dot(Vector4D left, Vector4D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z +
            left.W * right.W;

        [MethodImpl(Inline)]
        public double Dot(Vector4D vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public static Vector4D Min(Vector4D left, Vector4D right) =>
            new Vector4D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z,
                         left.W < right.W ? left.W : right.W);

        [MethodImpl(Inline)]
        public static Vector4D Max(Vector4D left, Vector4D right) =>
            new Vector4D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z,
                         left.W > right.W ? left.W : right.W);

        [MethodImpl(Inline)]
        public static Vector4D Abs(Vector4D value) =>
            new Vector4D(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z), Math.Abs(value.W));

        [MethodImpl(Inline)]
        public Vector4D Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public static Vector4D SquareRoot(Vector4D value) =>
            new Vector4D(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z), Math.Sqrt(value.W));

        [MethodImpl(Inline)]
        public static Vector4D operator +(Vector4D value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector4D operator -(Vector4D value) =>
            new Vector4D(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(Inline)]
        public static Vector4D operator +(Vector4D left, Vector4D right) =>
            new Vector4D(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(Inline)]
        public static Vector4D operator -(Vector4D left, Vector4D right) =>
            new Vector4D(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(Inline)]
        public static Vector4D operator *(Vector4D left, Vector4D right) =>
            new Vector4D(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        [MethodImpl(Inline)]
        public static Vector4D operator *(Vector4D left, double right) =>
            new Vector4D(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(Inline)]
        public static Vector4D operator *(double left, Vector4D right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector4D operator /(Vector4D left, Vector4D right) =>
            new Vector4D(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        [MethodImpl(Inline)]
        public static Vector4D operator /(Vector4D left, double right) =>
            new Vector4D(left.X / right, left.Y / right, left.Z / right, left.W / right);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector4D left, Vector4D right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(Inline)]
        public static bool operator !=(Vector4D left, Vector4D right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;
    }
}
