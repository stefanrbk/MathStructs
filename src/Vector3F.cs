using System;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace MathStructs
{
    public struct Vector3F : IEquatable<Vector3F>, IFormattable
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public float X;
        public float Y;
        public float Z;

        public static Vector3F Zero =>
            new Vector3F(0f);
        public static Vector3F One =>
            new Vector3F(1f);
        public static Vector3F UnitX =>
            new Vector3F(1f, 0f, 0f);
        public static Vector3F UnitY =>
            new Vector3F(0f, 1f, 0f);
        public static Vector3F UnitZ =>
            new Vector3F(0f, 0f, 1f);
        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector3F vec && Equals(vec);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public float Length() =>
            MathF.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public float LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public static float Distance(Vector3F vector1, Vector3F vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public float Distance(Vector3F value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector3F vector1, Vector3F vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public float DistanceSquared(Vector3F value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public static Vector3F Normalize(Vector3F vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public Vector3F Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public static Vector3F Cross(Vector3F vector1, Vector3F vector2) =>
            new Vector3F(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

        [MethodImpl(Inline)]
        public Vector3F Cross(Vector3F vector) =>
            Cross(this, vector);

        [MethodImpl(Inline)]
        public static Vector3F Reflect(Vector3F vector, Vector3F normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        [MethodImpl(Inline)]
        public Vector3F Reflect(Vector3F normal) =>
            Reflect(this, normal);

        [MethodImpl(Inline)]
        public static Vector3F Clamp(Vector3F value, Vector3F min, Vector3F max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public Vector3F Clamp(Vector3F min, Vector3F max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public static Vector3F Lerp(Vector3F bounds1, Vector3F bounds2, float amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector3F Transform(Vector3F vector, Matrix4x4F matrix) =>
            new Vector3F(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);

        [MethodImpl(Inline)]
        public Vector3F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector3F Transform(Vector3F value, QuaternionF rotation)
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
            return new Vector3F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
        }

        [MethodImpl(Inline)]
        public static Vector3F TransformNormal(Vector3F normal, Matrix4x4F matrix) =>
            new Vector3F(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                         normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                         normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);

        [MethodImpl(Inline)]
        public Vector3F TransformNormal(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public static Vector3F Add(Vector3F left, Vector3F right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector3F Subtract(Vector3F left, Vector3F right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector3F Multiply(Vector3F left, Vector3F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3F Multiply(Vector3F left, float right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3F Multiply(float left, Vector3F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3F Divide(Vector3F left, Vector3F right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector3F Divide(Vector3F left, float right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector3F Negate(Vector3F value) =>
            -value;

        public Vector3F(float value)
            : this(value, value, value) { }

        public Vector3F(Vector2F value, float z)
            : this(value.X, value.Y, z) { }

        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2F vector, out float z)
        {
            vector = new Vector2F(X, Y);
            z = Z;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        [MethodImpl(Inline)]
        public Vector3F With(float? x = null, float? y = null, float? z = null) =>
            new Vector3F(x ?? X, y ?? Y, z ?? Z);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array, int index)
        {
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < 3)
                throw new ArgumentException("Elements in source is greater than destination");
            array[index + 0] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
        }

        [MethodImpl(Inline)]
        public bool Equals(Vector3F other) =>
            this == other;

        [MethodImpl(Inline)]
        public static float Dot(Vector3F left, Vector3F right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z;

        [MethodImpl(Inline)]
        public float Dot(Vector3F vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public static Vector3F Min(Vector3F left, Vector3F right) =>
            new Vector3F(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z);

        [MethodImpl(Inline)]
        public static Vector3F Max(Vector3F left, Vector3F right) =>
            new Vector3F(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z);

        [MethodImpl(Inline)]
        public static Vector3F Abs(Vector3F value) =>
            new Vector3F(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));

        [MethodImpl(Inline)]
        public Vector3F Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public static Vector3F SquareRoot(Vector3F value) =>
            new Vector3F(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));

        [MethodImpl(Inline)]
        public static Vector3F operator +(Vector3F value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector3F operator -(Vector3F value) =>
            new Vector3F(-value.X, -value.Y, -value.Z);

        [MethodImpl(Inline)]
        public static Vector3F operator +(Vector3F left, Vector3F right) =>
            new Vector3F(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        [MethodImpl(Inline)]
        public static Vector3F operator -(Vector3F left, Vector3F right) =>
            new Vector3F(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        [MethodImpl(Inline)]
        public static Vector3F operator *(Vector3F left, Vector3F right) =>
            new Vector3F(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        [MethodImpl(Inline)]
        public static Vector3F operator *(Vector3F left, float right) =>
            new Vector3F(left.X * right, left.Y * right, left.Z * right);

        [MethodImpl(Inline)]
        public static Vector3F operator *(float left, Vector3F right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector3F operator /(Vector3F left, Vector3F right) =>
            new Vector3F(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        [MethodImpl(Inline)]
        public static Vector3F operator /(Vector3F left, float right) =>
            new Vector3F(left.X / right, left.Y / right, left.Z / right);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector3F left, Vector3F right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z;

        [MethodImpl(Inline)]
        public static bool operator !=(Vector3F left, Vector3F right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z;
    }
}
