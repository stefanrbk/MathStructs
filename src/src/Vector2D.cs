using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    public struct Vector2D : IEquatable<Vector2D>, IFormattable
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public double X;
        public double Y;

        public static Vector2D Zero =>
            new Vector2D(0d);
        public static Vector2D One =>
            new Vector2D(1d);
        public static Vector2D UnitX =>
            new Vector2D(1d, 0d);
        public static Vector2D UnitY =>
            new Vector2D(0d, 1d);
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode());

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector2D vec && Equals(vec);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public double LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public static double Distance(Vector2D vector1, Vector2D vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public double Distance(Vector2D value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector2D vector1, Vector2D vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public double DistanceSquared(Vector2D value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public static Vector2D Normalize(Vector2D vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public Vector2D Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public static Vector2D Reflect(Vector2D vector, Vector2D normal) =>
            vector - normal * Dot(vector, normal) * 2d;

        [MethodImpl(Inline)]
        public Vector2D Reflect(Vector2D normal) =>
            Reflect(this, normal);

        [MethodImpl(Inline)]
        public static Vector2D Clamp(Vector2D value, Vector2D min, Vector2D max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public Vector2D Clamp(Vector2D min, Vector2D max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public static Vector2D Lerp(Vector2D bounds1, Vector2D bounds2, double amount) =>
            bounds1 * (1d - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector2D Transform(Vector2D vector, Matrix4x4F matrix) =>
            new Vector2D(vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42);

        [MethodImpl(Inline)]
        public Vector2D Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector2D Transform(Vector2D value, QuaternionF rotation)
        {
            var n1 = rotation.X + rotation.X;
            var n2 = rotation.Y + rotation.Y;
            var n3 = rotation.Z + rotation.Z;
            var n4 = rotation.W * n3;
            var n5 = rotation.X * n1;
            var n6 = rotation.X * n2;
            var n7 = rotation.Y * n2;
            var n8 = rotation.Z * n3;
            return new Vector2D(value.X * (1 - n7 - n8) + value.Y * (n6 - n4), value.X * (n6 + n4) + value.Y * (1 - n5 - n8));
        }

        [MethodImpl(Inline)]
        public Vector2D Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        [MethodImpl(Inline)]
        public static Vector2D TransformNormal(Vector2D normal, Matrix4x4F matrix) =>
            new Vector2D(normal.X * matrix.M11 + normal.Y * matrix.M21,
                         normal.X * matrix.M12 + normal.Y * matrix.M22);

        [MethodImpl(Inline)]
        public Vector2D TransformNormal(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector2D Add(Vector2D left, Vector2D right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector2D Subtract(Vector2D left, Vector2D right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector2D Multiply(Vector2D left, Vector2D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2D Multiply(Vector2D left, double right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2D Multiply(double left, Vector2D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2D Divide(Vector2D left, Vector2D right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector2D Divide(Vector2D left, double right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector2D Negate(Vector2D value) =>
            -value;

        public Vector2D(double value)
            : this(value, value) { }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }

        [MethodImpl(Inline)]
        public Vector2D With(double? x = null, double? y = null) =>
            new Vector2D(x ?? X, y ?? Y);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
        }

        [MethodImpl(Inline)]
        public bool Equals(Vector2D other) =>
            this == other;

        [MethodImpl(Inline)]
        public static double Dot(Vector2D left, Vector2D right) =>
            left.X * right.X +
            left.Y * right.Y;

        [MethodImpl(Inline)]
        public double Dot(Vector2D vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public static Vector2D Min(Vector2D left, Vector2D right) =>
            new Vector2D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y);

        [MethodImpl(Inline)]
        public static Vector2D Max(Vector2D left, Vector2D right) =>
            new Vector2D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y);

        [MethodImpl(Inline)]
        public static Vector2D Abs(Vector2D value) =>
            new Vector2D(Math.Abs(value.X), Math.Abs(value.Y));

        [MethodImpl(Inline)]
        public Vector2D Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public static Vector2D SquareRoot(Vector2D value) =>
            new Vector2D(Math.Sqrt(value.X), Math.Sqrt(value.Y));

        [MethodImpl(Inline)]
        public static Vector2D operator +(Vector2D value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector2D operator -(Vector2D value) =>
            new Vector2D(-value.X, -value.Y);

        [MethodImpl(Inline)]
        public static Vector2D operator +(Vector2D left, Vector2D right) =>
            new Vector2D(left.X + right.X, left.Y + right.Y);

        [MethodImpl(Inline)]
        public static Vector2D operator -(Vector2D left, Vector2D right) =>
            new Vector2D(left.X - right.X, left.Y - right.Y);

        [MethodImpl(Inline)]
        public static Vector2D operator *(Vector2D left, Vector2D right) =>
            new Vector2D(left.X * right.X, left.Y * right.Y);

        [MethodImpl(Inline)]
        public static Vector2D operator *(Vector2D left, double right) =>
            new Vector2D(left.X * right, left.Y * right);

        [MethodImpl(Inline)]
        public static Vector2D operator *(double left, Vector2D right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector2D operator /(Vector2D left, Vector2D right) =>
            new Vector2D(left.X / right.X, left.Y / right.Y);

        [MethodImpl(Inline)]
        public static Vector2D operator /(Vector2D left, double right) =>
            new Vector2D(left.X / right, left.Y / right);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector2D left, Vector2D right) =>
            left.X == right.X && left.Y == right.Y;

        [MethodImpl(Inline)]
        public static bool operator !=(Vector2D left, Vector2D right) =>
            left.X != right.X || left.Y != right.Y;
    }
}
