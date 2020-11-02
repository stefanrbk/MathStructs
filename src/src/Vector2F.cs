using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    public struct Vector2F : IEquatable<Vector2F>, IFormattable
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public float X;
        public float Y;

        public static Vector2F Zero =>
            new Vector2F(0f);
        public static Vector2F One =>
            new Vector2F(1f);
        public static Vector2F UnitX =>
            new Vector2F(1f, 0f);
        public static Vector2F UnitY =>
            new Vector2F(0f, 1f);
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode());

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector2F vec && Equals(vec);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public float Length() =>
            MathF.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public float LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public static float Distance(Vector2F vector1, Vector2F vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public float Distance(Vector2F value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector2F vector1, Vector2F vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public float DistanceSquared(Vector2F value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public static Vector2F Normalize(Vector2F vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public Vector2F Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public static Vector2F Reflect(Vector2F vector, Vector2F normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        [MethodImpl(Inline)]
        public Vector2F Reflect(Vector2F normal) =>
            Reflect(this, normal);

        [MethodImpl(Inline)]
        public static Vector2F Clamp(Vector2F value, Vector2F min, Vector2F max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public Vector2F Clamp(Vector2F min, Vector2F max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public static Vector2F Lerp(Vector2F bounds1, Vector2F bounds2, float amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector2F Transform(Vector2F vector, Matrix4x4F matrix) =>
            new Vector2F(vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42);

        [MethodImpl(Inline)]
        public Vector2F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector2F Transform(Vector2F value, QuaternionF rotation)
        {
            var n1 = rotation.X + rotation.X;
            var n2 = rotation.Y + rotation.Y;
            var n3 = rotation.Z + rotation.Z;
            var n4 = rotation.W * n3;
            var n5 = rotation.X * n1;
            var n6 = rotation.X * n2;
            var n7 = rotation.Y * n2;
            var n8 = rotation.Z * n3;
            return new Vector2F(value.X * (1 - n7 - n8) + value.Y * (n6 - n4), value.X * (n6 + n4) + value.Y * (1 - n5 - n8));
        }

        [MethodImpl(Inline)]
        public Vector2F Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        [MethodImpl(Inline)]
        public static Vector2F TransformNormal(Vector2F normal, Matrix4x4F matrix) =>
            new Vector2F(normal.X * matrix.M11 + normal.Y * matrix.M21,
                         normal.X * matrix.M12 + normal.Y * matrix.M22);

        [MethodImpl(Inline)]
        public Vector2F TransformNormal(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector2F Add(Vector2F left, Vector2F right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector2F Subtract(Vector2F left, Vector2F right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector2F Multiply(Vector2F left, Vector2F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2F Multiply(Vector2F left, float right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2F Multiply(float left, Vector2F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector2F Divide(Vector2F left, Vector2F right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector2F Divide(Vector2F left, float right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector2F Negate(Vector2F value) =>
            -value;

        public Vector2F(float value)
            : this(value, value) { }

        public Vector2F(float x, float y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y)
        {
            x = X;
            y = Y;
        }

        [MethodImpl(Inline)]
        public Vector2F With(float? x = null, float? y = null) =>
            new Vector2F(x ?? X, y ?? Y);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
        }

        [MethodImpl(Inline)]
        public bool Equals(Vector2F other) =>
            this == other;

        [MethodImpl(Inline)]
        public static float Dot(Vector2F left, Vector2F right) =>
            left.X * right.X +
            left.Y * right.Y;

        [MethodImpl(Inline)]
        public float Dot(Vector2F vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public static Vector2F Min(Vector2F left, Vector2F right) =>
            new Vector2F(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y);

        [MethodImpl(Inline)]
        public static Vector2F Max(Vector2F left, Vector2F right) =>
            new Vector2F(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y);

        [MethodImpl(Inline)]
        public static Vector2F Abs(Vector2F value) =>
            new Vector2F(MathF.Abs(value.X), MathF.Abs(value.Y));

        [MethodImpl(Inline)]
        public Vector2F Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public static Vector2F SquareRoot(Vector2F value) =>
            new Vector2F(MathF.Sqrt(value.X), MathF.Sqrt(value.Y));

        [MethodImpl(Inline)]
        public static Vector2F operator +(Vector2F value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector2F operator -(Vector2F value) =>
            new Vector2F(-value.X, -value.Y);

        [MethodImpl(Inline)]
        public static Vector2F operator +(Vector2F left, Vector2F right) =>
            new Vector2F(left.X + right.X, left.Y + right.Y);

        [MethodImpl(Inline)]
        public static Vector2F operator -(Vector2F left, Vector2F right) =>
            new Vector2F(left.X - right.X, left.Y - right.Y);

        [MethodImpl(Inline)]
        public static Vector2F operator *(Vector2F left, Vector2F right) =>
            new Vector2F(left.X * right.X, left.Y * right.Y);

        [MethodImpl(Inline)]
        public static Vector2F operator *(Vector2F left, float right) =>
            new Vector2F(left.X * right, left.Y * right);

        [MethodImpl(Inline)]
        public static Vector2F operator *(float left, Vector2F right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector2F operator /(Vector2F left, Vector2F right) =>
            new Vector2F(left.X / right.X, left.Y / right.Y);

        [MethodImpl(Inline)]
        public static Vector2F operator /(Vector2F left, float right) =>
            new Vector2F(left.X / right, left.Y / right);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector2F left, Vector2F right) =>
            left.X == right.X && left.Y == right.Y;

        [MethodImpl(Inline)]
        public static bool operator !=(Vector2F left, Vector2F right) =>
            left.X != right.X || left.Y != right.Y;
    }
}
