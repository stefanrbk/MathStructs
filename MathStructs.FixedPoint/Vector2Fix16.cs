using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector2Fix16 : IEquatable<Vector2Fix16>, IFormattable
    {
        [FieldOffset(0)]
        public readonly Fix16 X;

        [FieldOffset(4)]
        public readonly Fix16 Y;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector2Fix16 One => new Vector2Fix16(Fix16.One);

        public static Vector2Fix16 UnitX => new Vector2Fix16(Fix16.One, Fix16.Zero);

        public static Vector2Fix16 UnitY => new Vector2Fix16(Fix16.Zero, Fix16.One);

        public static Vector2Fix16 Zero => new Vector2Fix16(Fix16.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16(Fix16 value)
            : this(value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16(Fix16 x, Fix16 y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Abs(Vector2Fix16 value) =>
            new Vector2Fix16(Fix16.Abs(value.X),
                             Fix16.Abs(value.Y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Add(Vector2Fix16 left, Vector2Fix16 right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Clamp(Vector2Fix16 value, Vector2Fix16 min, Vector2Fix16 max) =>
            Min(Max(value, min), max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Distance(Vector2Fix16 vector1, Vector2Fix16 vector2) =>
            ( vector1 - vector2 ).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 DistanceSquared(Vector2Fix16 vector1, Vector2Fix16 vector2) =>
            ( vector1 - vector2 ).LengthSquared();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16? Divide(Vector2Fix16? left, Vector2Fix16? right)
        {
            if (left is null || right is null)
                return null;
            if (right.Value.X == Fix16.Zero || right.Value.Y == Fix16.Zero)
                return null;
            return left.Value / right!.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16? Divide(Vector2Fix16? left, Fix16? right)
        {
            if (left is null || right is null)
                return null;
            if (right.Value == Fix16.Zero)
                return null;
            return left.Value / right!.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Dot(Vector2Fix16 left, Vector2Fix16 right) =>
            (left.X * right.X) + (left.Y * right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Lerp(Vector2Fix16 bounds1, Vector2Fix16 bounds2, Fix16 amount) =>
            (bounds1 * ( Fix16.One - amount )) + (bounds2 * amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Max(Vector2Fix16 left, Vector2Fix16 right) =>
            new Vector2Fix16(( left.X > right.X ) ? left.X : right.X,
                             ( left.Y > right.Y ) ? left.Y : right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Min(Vector2Fix16 left, Vector2Fix16 right) =>
            new Vector2Fix16(( left.X < right.X ) ? left.X : right.X,
                             ( left.Y < right.Y ) ? left.Y : right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(Vector2Fix16 left, Vector2Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(Vector2Fix16 left, Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(Fix16 left, Vector2Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Negate(Vector2Fix16 value) =>
            -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Normalize(Vector2Fix16 vector)
        {
            return vector / vector.Length();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator -(Vector2Fix16 value)
        {
            return new Vector2Fix16(-value.X, -value.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator -(Vector2Fix16 left, Vector2Fix16 right) =>
            new Vector2Fix16(left.X - right.X, left.Y - right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2Fix16 left, Vector2Fix16 right) =>
            left.X != right.X || left.Y != right.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(Vector2Fix16 left, Vector2Fix16 right) =>
            new Vector2Fix16(left.X * right.X, left.Y * right.Y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(Vector2Fix16 left, Fix16 right) =>
            new Vector2Fix16(left.X * right, left.Y * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(Fix16 left, Vector2Fix16 right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator /(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X / right.X, left.Y / right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator /(Vector2Fix16 left, Fix16 right) =>
            new Vector2Fix16(left.X / right, left.Y / right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator +(Vector2Fix16 value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator +(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X + right.X, left.Y + right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2Fix16 left, Vector2Fix16 right) =>
            (left.X == right.X) && left.Y == right.Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Reflect(Vector2Fix16 vector, Vector2Fix16 normal) =>
            vector - (normal * Dot(vector, normal) * (Fix16)2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 SquareRoot(Vector2Fix16 value) =>
            new Vector2Fix16(Fix16.Sqrt(value.X), Fix16.Sqrt(value.Y));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Subtract(Vector2Fix16 left, Vector2Fix16 right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Transform(Vector2Fix16 vector, Matrix4x4Fix16 matrix) =>
            new Vector2Fix16((vector.X * matrix.M11) + (vector.Y * matrix.M21) + matrix.M41,
                             (vector.X * matrix.M12) + (vector.Y * matrix.M22) + matrix.M42);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 TransformNormal(Vector2Fix16 normal, Matrix4x4Fix16 matrix) =>
            new Vector2Fix16((normal.X * matrix.M11) + (normal.Y * matrix.M21),
                             (normal.X * matrix.M12) + (normal.Y * matrix.M22));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Abs() =>
            Abs(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Clamp(Vector2Fix16 min, Vector2Fix16 max) =>
            Clamp(this, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span) =>
            CopyTo(span, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Fix16 x, out Fix16 y)
        {
            x = X;
            y = Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Distance(Vector2Fix16 value) =>
            Distance(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 DistanceSquared(Vector2Fix16 value) =>
            DistanceSquared(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Dot(Vector2Fix16 vector) =>
            Dot(this, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj) =>
            obj is Vector2Fix16 vec && Equals(vec);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2Fix16 other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2Fix16 other, Fix16 delta) =>
            ( delta != Fix16.Zero ) ? ( Fix16.Abs(X - other.X) < delta ) && Fix16.Abs(Y - other.Y) < delta : this == other;

        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Length() =>
            Fix16.Sqrt(Dot(this, this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 LengthSquared() =>
            Dot(this, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Normalize() =>
            Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Reflect(Vector2Fix16 normal) =>
            Reflect(this, normal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 SquareRoot() =>
            SquareRoot(this);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}>";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Transform(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 TransformNormal(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 TransformV4(Matrix4x4Fix16 matrix) =>
            Vector4Fix16.Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 With(Fix16? x = null, Fix16? y = null) =>
            new Vector2Fix16(x ?? X, y ?? Y);
    }
}