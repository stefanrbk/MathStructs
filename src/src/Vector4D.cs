using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    public struct Vector4D : IEquatable<Vector4D>, IFormattable
    {
        public double X;
        public double Y;
        public double Z;
        public double W;

        public static Vector4D Zero =>
            new Vector4D(0d);
        public static Vector4D One =>
            new Vector4D(1d);
        public static Vector4D UnitX =>
            new Vector4D(1d, 0d, 0d, 0d);
        public static Vector4D UnitY =>
            new Vector4D(0d, 1d, 0d, 0d);
        public static Vector4D UnitZ =>
            new Vector4D(0d, 0d, 1d, 0d);
        public static Vector4D UnitW =>
            new Vector4D(0d, 0d, 0d, 1d);
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj) =>
            obj is Vector4D vec && Equals(vec);
        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);
        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);
        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}, {W.ToString(format, formatProvider)}>";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double LengthSquared() =>
            Dot(this, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Distance(Vector4D value) =>
            Distance(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DistanceSquared(Vector4D vector1, Vector4D vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double DistanceSquared(Vector4D value) =>
            DistanceSquared(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Normalize(Vector4D vector) =>
            vector / vector.Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Normalize() =>
            Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Clamp(Vector4D value, Vector4D min, Vector4D max) =>
            Min(Max(value, min), max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Clamp(Vector4D min, Vector4D max) =>
            Clamp(this, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Lerp(Vector4D bounds1, Vector4D bounds2, double amount) =>
            bounds1 * (1d - amount) + bounds2 * amount;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Transform(Vector4D vector, Matrix4x4F matrix) =>
            new Vector4D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4D Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Add(Vector4D left, Vector4D right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Subtract(Vector4D left, Vector4D right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(Vector4D left, Vector4D right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(Vector4D left, double right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Multiply(double left, Vector4D right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(Vector4D left, Vector4D right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Divide(Vector4D left, double right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(double[] array) =>
            CopyTo(array, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4D other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(Vector4D left, Vector4D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z +
            left.W * right.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Min(Vector4D left, Vector4D right) =>
            new Vector4D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z,
                         left.W < right.W ? left.W : right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Max(Vector4D left, Vector4D right) =>
            new Vector4D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z,
                         left.W > right.W ? left.W : right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D Abs(Vector4D value) =>
            new Vector4D(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z), Math.Abs(value.W));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D SquareRoot(Vector4D value) =>
            new Vector4D(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z), Math.Sqrt(value.W));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D value) =>
            new Vector4D(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Vector4D left, Vector4D right) =>
            new Vector4D(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Vector4D left, Vector4D right) =>
            new Vector4D(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Vector4D left, Vector4D right) =>
            new Vector4D(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(Vector4D left, double right) =>
            new Vector4D(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator *(double left, Vector4D right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Vector4D left, Vector4D right) =>
            new Vector4D(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator /(Vector4D left, double right) =>
            new Vector4D(left.X / right, left.Y / right, left.Z / right, left.W / right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4D left, Vector4D right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4D left, Vector4D right) =>
            left.X != right.X && left.Y != right.Y && left.Z != right.Z && left.W != right.W;
    }
}
