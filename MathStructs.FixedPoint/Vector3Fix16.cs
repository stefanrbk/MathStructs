using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector3Fix16 : IEquatable<Vector3Fix16>, IFormattable
    {
        [FieldOffset(0)]
        public readonly int X;

        [FieldOffset(4)]
        public readonly int Y;

        [FieldOffset(8)]
        public readonly int Z;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector3Fix16 One => new Vector3Fix16(1.0);

        public static Vector3Fix16 UnitX => new Vector3Fix16(1.0, 0.0, 0.0);

        public static Vector3Fix16 UnitY => new Vector3Fix16(0.0, 1.0, 0.0);

        public static Vector3Fix16 UnitZ => new Vector3Fix16(0.0, 0.0, 1.0);

        public static Vector3Fix16 Zero => new Vector3Fix16(0.0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(int value)
            : this(value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(Vector2Fix16 value, int z)
            : this(value.X, value.Y, z)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(double value)
            : this(value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(Vector2Fix16 value, double z)
            : this(value.X, value.Y, (int)z)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(double x, double y, double z)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Abs(Vector3Fix16 value)
        {
            return new Vector3Fix16((int)Math.Abs((double)value.X), (int)Math.Abs((double)value.Y), (int)Math.Abs((double)value.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Add(Vector3Fix16 left, Vector3Fix16 right)
        {
            return left + right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Clamp(Vector3Fix16 value, Vector3Fix16 min, Vector3Fix16 max)
        {
            return Min(Max(value, min), max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Cross(Vector3Fix16 vector1, Vector3Fix16 vector2)
        {
            return new Vector3Fix16(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Distance(Vector3Fix16 vector1, Vector3Fix16 vector2)
        {
            return ( vector1 - vector2 ).Length();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(Vector3Fix16 vector1, Vector3Fix16 vector2)
        {
            return ( vector1 - vector2 ).LengthSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Divide(Vector3Fix16 left, Vector3Fix16 right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Divide(Vector3Fix16 left, int right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Dot(Vector3Fix16 left, Vector3Fix16 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Lerp(Vector3Fix16 bounds1, Vector3Fix16 bounds2, int amount)
        {
            return bounds1 * ( 1 - amount ) + bounds2 * amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Max(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(( left.X > right.X ) ? left.X : right.X, ( left.Y > right.Y ) ? left.Y : right.Y, ( left.Z > right.Z ) ? left.Z : right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Min(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(( left.X < right.X ) ? left.X : right.X, ( left.Y < right.Y ) ? left.Y : right.Y, ( left.Z < right.Z ) ? left.Z : right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(Vector3Fix16 left, Vector3Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(Vector3Fix16 left, int right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(int left, Vector3Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Negate(Vector3Fix16 value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Normalize(Vector3Fix16 vector)
        {
            return vector / vector.Length();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator -(Vector3Fix16 value)
        {
            return new Vector3Fix16(-value.X, -value.Y, -value.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator -(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3Fix16 left, Vector3Fix16 right)
        {
            if (left.X == right.X && left.Y == right.Y)
                return left.Z != right.Z;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, int right)
        {
            return new Vector3Fix16(left.X * right, left.Y * right, left.Z * right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(int left, Vector3Fix16 right)
        {
            return right * left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator /(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator /(Vector3Fix16 left, int right)
        {
            return new Vector3Fix16(left.X / right, left.Y / right, left.Z / right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator +(Vector3Fix16 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator +(Vector3Fix16 left, Vector3Fix16 right)
        {
            return new Vector3Fix16(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3Fix16 left, Vector3Fix16 right)
        {
            if (left.X == right.X && left.Y == right.Y)
                return left.Z == right.Z;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Reflect(Vector3Fix16 vector, Vector3Fix16 normal)
        {
            return vector - normal * Dot(vector, normal) * 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 SquareRoot(Vector3Fix16 value)
        {
            return new Vector3Fix16((int)Math.Sqrt(value.X), (int)Math.Sqrt(value.Y), (int)Math.Sqrt(value.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Subtract(Vector3Fix16 left, Vector3Fix16 right)
        {
            return left - right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Transform(Vector3Fix16 vector, Matrix4x4Fix16 matrix)
        {
            return new Vector3Fix16(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 TransformNormal(Vector3Fix16 normal, Matrix4x4Fix16 matrix)
        {
            return new Vector3Fix16(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Abs()
        {
            return Abs(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Clamp(Vector3Fix16 min, Vector3Fix16 max)
        {
            return Clamp(this, min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<int> span)
        {
            CopyTo(span, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<int> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException("index");
            if (span.Length - index < 3)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Cross(Vector3Fix16 vector)
        {
            return Cross(this, vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector2Fix16 vector, out int z)
        {
            vector = new Vector2Fix16(X, Y);
            z = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Distance(Vector3Fix16 value)
        {
            return Distance(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int DistanceSquared(Vector3Fix16 value)
        {
            return DistanceSquared(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Dot(Vector3Fix16 vector)
        {
            return Dot(this, vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            if (obj is Vector3Fix16)
            {
                Vector3Fix16 vec = (Vector3Fix16)obj;
                return Equals(vec);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3Fix16 other)
        {
            return this == other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3Fix16 other, int delta)
        {
            if (delta == 0)
                return this == other;
            Vector3Fix16 vector = Subtract(this, other).Abs();
            if (vector.X < delta && vector.Y < delta)
                return vector.Z < delta;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Length()
        {
            return (int)Math.Sqrt(Dot(this, this));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LengthSquared()
        {
            return Dot(this, this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Normalize()
        {
            return Normalize(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Reflect(Vector3Fix16 normal)
        {
            return Reflect(this, normal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 SquareRoot()
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
            return "<" + X.ToString(format, formatProvider) + ", " + Y.ToString(format, formatProvider) + ", " + Z.ToString(format, formatProvider) + ">";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Transform(Matrix4x4Fix16 matrix)
        {
            return Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 TransformNormal(Matrix4x4Fix16 matrix)
        {
            return Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 TransformV4(Matrix4x4Fix16 matrix)
        {
            return Vector4Fix16.Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 With(int? x = null, int? y = null, int? z = null)
        {
            return new Vector3Fix16(x ?? X, y ?? Y, z ?? Z);
        }
    }
}
