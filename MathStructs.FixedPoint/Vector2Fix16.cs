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
        public readonly int X;

        [FieldOffset(4)]
        public readonly int Y;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector2Fix16 One => new Vector2Fix16(Fix16.One);

        public static Vector2Fix16 UnitX => new Vector2Fix16(Fix16.One, Fix16.Zero);

        public static Vector2Fix16 UnitY => new Vector2Fix16(Fix16.Zero, Fix16.One);

        public static Vector2Fix16 Zero => new Vector2Fix16(Fix16.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16(int value)
            : this(value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16(int x, int y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Abs(Vector2Fix16 value) =>
            new Vector2Fix16(Math.Abs(Fix16.ToDouble(value.X)).ToFix16(),
                             Math.Abs(Fix16.ToDouble(value.Y)).ToFix16());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Add(Vector2Fix16 left, Vector2Fix16 right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Clamp(Vector2Fix16 value, Vector2Fix16 min, Vector2Fix16 max) =>
            Min(Max(value, min), max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Distance(Vector2Fix16 vector1, Vector2Fix16 vector2) =>
            ( vector1 - vector2 ).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(Vector2Fix16 vector1, Vector2Fix16 vector2) =>
            ( vector1 - vector2 ).LengthSquared();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Divide(Vector2Fix16 left, Vector2Fix16 right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Divide(Vector2Fix16 left, int right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Dot(Vector2Fix16 left, Vector2Fix16 right) =>
            (int)( ( (long)left.X * right.X + (long)left.Y * right.Y ) >> 16 );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Lerp(Vector2Fix16 bounds1, Vector2Fix16 bounds2, int amount)
        {
            return bounds1 * ( 1 - amount ) + bounds2 * amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Max(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(( left.X > right.X ) ? left.X : right.X, ( left.Y > right.Y ) ? left.Y : right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Min(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(( left.X < right.X ) ? left.X : right.X, ( left.Y < right.Y ) ? left.Y : right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(Vector2Fix16 left, Vector2Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(Vector2Fix16 left, int right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Multiply(int left, Vector2Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Negate(Vector2Fix16 value)
        {
            return -value;
        }

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
        public static Vector2Fix16 operator -(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X - right.X, left.Y - right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2Fix16 left, Vector2Fix16 right)
        {
            if (left.X == right.X)
                return left.Y != right.Y;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X * right.X, left.Y * right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(Vector2Fix16 left, int right)
        {
            return new Vector2Fix16(left.X * right, left.Y * right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator *(int left, Vector2Fix16 right)
        {
            return right * left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator /(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X / right.X, left.Y / right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator /(Vector2Fix16 left, int right)
        {
            return new Vector2Fix16(left.X / right, left.Y / right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator +(Vector2Fix16 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 operator +(Vector2Fix16 left, Vector2Fix16 right)
        {
            return new Vector2Fix16(left.X + right.X, left.Y + right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2Fix16 left, Vector2Fix16 right)
        {
            if (left.X == right.X)
                return left.Y == right.Y;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Reflect(Vector2Fix16 vector, Vector2Fix16 normal)
        {
            return vector - normal * Dot(vector, normal) * 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 SquareRoot(Vector2Fix16 value)
        {
            return new Vector2Fix16(Math.Sqrt(value.X).ToFix16(), Math.Sqrt(value.Y).ToFix16());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Subtract(Vector2Fix16 left, Vector2Fix16 right)
        {
            return left - right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 Transform(Vector2Fix16 vector, Matrix4x4Fix16 matrix)
        {
            return new Vector2Fix16(vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Fix16 TransformNormal(Vector2Fix16 normal, Matrix4x4Fix16 matrix)
        {
            return new Vector2Fix16(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Abs()
        {
            return Abs(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Clamp(Vector2Fix16 min, Vector2Fix16 max)
        {
            return Clamp(this, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<int> span) =>
            CopyTo(span, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<int> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 2)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Distance(Vector2Fix16 value) =>
            Distance(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int DistanceSquared(Vector2Fix16 value) =>
            DistanceSquared(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Dot(Vector2Fix16 vector) =>
            Dot(this, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            if (obj is Vector2Fix16)
            {
                Vector2Fix16 vec = (Vector2Fix16)obj;
                return Equals(vec);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2Fix16 other)
        {
            return this == other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2Fix16 other, int delta)
        {
            if (delta != 0)
            {
                if ((int)Math.Abs((double)( X - other.X )) < delta)
                    return (int)Math.Abs((double)( Y - other.Y )) < delta;
                return false;
            }
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Length() =>
            (int)Math.Sqrt(Dot(this, this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LengthSquared() =>
            Dot(this, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Normalize()
        {
            return Normalize(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Reflect(Vector2Fix16 normal)
        {
            return Reflect(this, normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 SquareRoot()
        {
            return SquareRoot(this);
        }

        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        public string ToString(string? format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return "<" + X.ToString(format, formatProvider) + ", " + Y.ToString(format, formatProvider) + ">";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 Transform(Matrix4x4Fix16 matrix)
        {
            return Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 TransformNormal(Matrix4x4Fix16 matrix)
        {
            return Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 TransformV4(Matrix4x4Fix16 matrix)
        {
            return Vector4Fix16.Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 With(int? x = null, int? y = null)
        {
            return new Vector2Fix16(x ?? X, y ?? Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Fix16 With(double? x = null, double? y = null) =>
            new Vector2Fix16(x?.ToFix16() ?? X, y?.ToFix16() ?? Y);
    }
}
