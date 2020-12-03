using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector4Fix16 : IEquatable<Vector4Fix16>, IFormattable
    {
        [FieldOffset(0)]
        public readonly int X;

        [FieldOffset(4)]
        public readonly int Y;

        [FieldOffset(8)]
        public readonly int Z;

        [FieldOffset(12)]
        public readonly int W;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector4Fix16 One => new Vector4Fix16(1.0);

        public static Vector4Fix16 UnitW => new Vector4Fix16(0.0, 0.0, 0.0, 1.0);

        public static Vector4Fix16 UnitX => new Vector4Fix16(1.0, 0.0, 0.0, 0.0);

        public static Vector4Fix16 UnitY => new Vector4Fix16(0.0, 1.0, 0.0, 0.0);

        public static Vector4Fix16 UnitZ => new Vector4Fix16(0.0, 0.0, 1.0, 0.0);

        public static Vector4Fix16 Zero => new Vector4Fix16(0.0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(int value)
            : this(value, value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(double value)
            : this(value, value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(double x, double y, double z, double w)
            : this((int)x, (int)y, (int)z, (int)w)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector3Fix16 vector, int w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector3Fix16 vector, double w)
            : this(vector, (int)w)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector2Fix16 vector, int z, int w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector2Fix16 vector, double z, double w)
            : this(vector, (int)z, (int)w)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Abs(Vector4Fix16 value)
        {
            return new Vector4Fix16(Math.Abs((double)value.X), Math.Abs((double)value.Y), Math.Abs((double)value.Z), Math.Abs((double)value.W));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Add(Vector4Fix16 left, Vector4Fix16 right)
        {
            return left + right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Clamp(Vector4Fix16 value, Vector4Fix16 min, Vector4Fix16 max)
        {
            return Min(Max(value, min), max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Distance(Vector4Fix16 vector1, Vector4Fix16 vector2)
        {
            return ( vector1 - vector2 ).Length();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int DistanceSquared(Vector4Fix16 vector1, Vector4Fix16 vector2)
        {
            return ( vector1 - vector2 ).LengthSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Divide(Vector4Fix16 left, Vector4Fix16 right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Divide(Vector4Fix16 left, int right)
        {
            return left / right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Dot(Vector4Fix16 left, Vector4Fix16 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Lerp(Vector4Fix16 bounds1, Vector4Fix16 bounds2, int amount)
        {
            return bounds1 * ( 1 - amount ) + bounds2 * amount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Max(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(( left.X > right.X ) ? left.X : right.X, ( left.Y > right.Y ) ? left.Y : right.Y, ( left.Z > right.Z ) ? left.Z : right.Z, ( left.W > right.W ) ? left.W : right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Min(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(( left.X < right.X ) ? left.X : right.X, ( left.Y < right.Y ) ? left.Y : right.Y, ( left.Z < right.Z ) ? left.Z : right.Z, ( left.W < right.W ) ? left.W : right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(Vector4Fix16 left, Vector4Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(Vector4Fix16 left, int right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(int left, Vector4Fix16 right)
        {
            return left * right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Negate(Vector4Fix16 value)
        {
            return -value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Normalize(Vector4Fix16 vector)
        {
            return vector / vector.Length();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator -(Vector4Fix16 value)
        {
            return new Vector4Fix16(-value.X, -value.Y, -value.Z, -value.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator -(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4Fix16 left, Vector4Fix16 right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Z == right.Z)
                return left.W != right.W;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, int right)
        {
            return new Vector4Fix16(left.X * right, left.Y * right, left.Z * right, left.W * right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(int left, Vector4Fix16 right)
        {
            return right * left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator /(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator /(Vector4Fix16 left, int right)
        {
            return new Vector4Fix16(left.X / right, left.Y / right, left.Z / right, left.W / right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator +(Vector4Fix16 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator +(Vector4Fix16 left, Vector4Fix16 right)
        {
            return new Vector4Fix16(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4Fix16 left, Vector4Fix16 right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Z == right.Z)
                return left.W == right.W;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 SquareRoot(Vector4Fix16 value)
        {
            return new Vector4Fix16(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z), Math.Sqrt(value.W));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Subtract(Vector4Fix16 left, Vector4Fix16 right)
        {
            return left - right;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector2Fix16 position, Matrix4x4Fix16 matrix)
        {
            return new Vector4Fix16(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector3Fix16 vector, Matrix4x4Fix16 matrix)
        {
            return new Vector4Fix16(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + matrix.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector4Fix16 vector, Matrix4x4Fix16 matrix)
        {
            return new Vector4Fix16(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Abs()
        {
            return Abs(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Clamp(Vector4Fix16 min, Vector4Fix16 max)
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
            if (span.Length - index < 4)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
            span[index + 3] = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector3Fix16 vector, out int w)
        {
            w = W;
            vector = new Vector3Fix16(X, Y, Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector2Fix16 vector, out int z, out int w)
        {
            vector = new Vector2Fix16(X, Y);
            z = Z;
            w = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Distance(Vector4Fix16 value)
        {
            return Distance(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int DistanceSquared(Vector4Fix16 value)
        {
            return DistanceSquared(this, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Dot(Vector4Fix16 vector)
        {
            return Dot(this, vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            if (obj is Vector4Fix16)
            {
                Vector4Fix16 vec = (Vector4Fix16)obj;
                return Equals(vec);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4Fix16 other)
        {
            return this == other;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4Fix16 other, int delta)
        {
            if (delta == 0)
                return this == other;
            Vector4Fix16 vector = Subtract(this, other).Abs();
            if (vector.X < delta && vector.Y < delta && vector.Z < delta)
                return vector.W < delta;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());
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
        public Vector4Fix16 Normalize()
        {
            return Normalize(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 SquareRoot()
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
            return "<" + X.ToString(format, formatProvider) + ", " + Y.ToString(format, formatProvider) + ", " + Z.ToString(format, formatProvider) + ", " + W.ToString(format, formatProvider) + ">";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Transform(Matrix4x4Fix16 matrix)
        {
            return Transform(this, matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 With(int? x = null, int? y = null, int? z = null, int? w = null)
        {
            return new Vector4Fix16(x ?? X, y ?? Y, z ?? Z, w ?? W);
        }
    }
}
