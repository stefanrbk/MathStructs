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
        public readonly Fix16 X;

        [FieldOffset(4)]
        public readonly Fix16 Y;

        [FieldOffset(8)]
        public readonly Fix16 Z;

        [FieldOffset(12)]
        public readonly Fix16 W;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector4Fix16 One => new(Fix16.One);

        public static Vector4Fix16 UnitW => new(Fix16.Zero, Fix16.Zero, Fix16.Zero, Fix16.One);

        public static Vector4Fix16 UnitX => new(Fix16.One, Fix16.Zero, Fix16.Zero, Fix16.Zero);

        public static Vector4Fix16 UnitY => new(Fix16.Zero, Fix16.One, Fix16.Zero, Fix16.Zero);

        public static Vector4Fix16 UnitZ => new(Fix16.Zero, Fix16.Zero, Fix16.One, Fix16.Zero);

        public static Vector4Fix16 Zero => new(Fix16.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Fix16 value)
            : this(value, value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Fix16 x, Fix16 y, Fix16 z, Fix16 w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector3Fix16 vector, Fix16 w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16(Vector2Fix16 vector, Fix16 z, Fix16 w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Abs(Vector4Fix16 value) =>
            new(Fix16.Abs(value.X), Fix16.Abs(value.Y), Fix16.Abs(value.Z), Fix16.Abs(value.W));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Add(Vector4Fix16 left, Vector4Fix16 right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Clamp(Vector4Fix16 value, Vector4Fix16 min, Vector4Fix16 max) =>
            Min(Max(value, min), max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Distance(Vector4Fix16 vector1, Vector4Fix16 vector2) =>
            ( vector1 - vector2 ).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 DistanceSquared(Vector4Fix16 vector1, Vector4Fix16 vector2) =>
            ( vector1 - vector2 ).LengthSquared();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Divide(Vector4Fix16 left, Vector4Fix16 right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Divide(Vector4Fix16 left, Fix16 right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Dot(Vector4Fix16 left, Vector4Fix16 right) =>
            ( left.X * right.X ) + ( left.Y * right.Y ) + ( left.Z * right.Z ) + ( left.W * right.W );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Lerp(Vector4Fix16 bounds1, Vector4Fix16 bounds2, Fix16 amount) =>
            ( bounds1 * ( Fix16.One - amount ) ) + ( bounds2 * amount );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Max(Vector4Fix16 left, Vector4Fix16 right) =>
            new(( left.X > right.X ) ? left.X : right.X, ( left.Y > right.Y ) ? left.Y : right.Y,
                ( left.Z > right.Z ) ? left.Z : right.Z, ( left.W > right.W ) ? left.W : right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Min(Vector4Fix16 left, Vector4Fix16 right) =>
            new(( left.X < right.X ) ? left.X : right.X, ( left.Y < right.Y ) ? left.Y : right.Y,
                ( left.Z < right.Z ) ? left.Z : right.Z, ( left.W < right.W ) ? left.W : right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(Vector4Fix16 left, Vector4Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(Vector4Fix16 left, Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Multiply(Fix16 left, Vector4Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Negate(Vector4Fix16 value) =>
            -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Normalize(Vector4Fix16 vector) =>
            vector / vector.Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator -(Vector4Fix16 value) =>
            new(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator -(Vector4Fix16 left, Vector4Fix16 right) =>
            new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4Fix16 left, Vector4Fix16 right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, Vector4Fix16 right) =>
            new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(Vector4Fix16 left, Fix16 right) =>
            new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator *(Fix16 left, Vector4Fix16 right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator /(Vector4Fix16 left, Vector4Fix16 right) =>
            new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator /(Vector4Fix16 left, Fix16 right) =>
            new(left.X / right, left.Y / right, left.Z / right, left.W / right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator +(Vector4Fix16 value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 operator +(Vector4Fix16 left, Vector4Fix16 right) =>
            new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4Fix16 left, Vector4Fix16 right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 SquareRoot(Vector4Fix16 value) =>
            new(Fix16.Sqrt(value.X), Fix16.Sqrt(value.Y), Fix16.Sqrt(value.Z), Fix16.Sqrt(value.W));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Subtract(Vector4Fix16 left, Vector4Fix16 right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector2Fix16 position, Matrix4x4Fix16 matrix) =>
            new(( position.X * matrix.M11 ) + ( position.Y * matrix.M21 ) + matrix.M41,
                ( position.X * matrix.M12 ) + ( position.Y * matrix.M22 ) + matrix.M42,
                ( position.X * matrix.M13 ) + ( position.Y * matrix.M23 ) + matrix.M43,
                ( position.X * matrix.M14 ) + ( position.Y * matrix.M24 ) + matrix.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector3Fix16 vector, Matrix4x4Fix16 matrix) =>
            new(( vector.X * matrix.M11 ) + ( vector.Y * matrix.M21 ) + ( vector.Z * matrix.M31 ) + matrix.M41,
                ( vector.X * matrix.M12 ) + ( vector.Y * matrix.M22 ) + ( vector.Z * matrix.M32 ) + matrix.M42,
                ( vector.X * matrix.M13 ) + ( vector.Y * matrix.M23 ) + ( vector.Z * matrix.M33 ) + matrix.M43,
                ( vector.X * matrix.M14 ) + ( vector.Y * matrix.M24 ) + ( vector.Z * matrix.M34 ) + matrix.M44);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4Fix16 Transform(Vector4Fix16 vector, Matrix4x4Fix16 matrix) =>
            new(( vector.X * matrix.M11 ) + ( vector.Y * matrix.M21 ) + ( vector.Z * matrix.M31 ) + ( vector.W * matrix.M41 ),
                ( vector.X * matrix.M12 ) + ( vector.Y * matrix.M22 ) + ( vector.Z * matrix.M32 ) + ( vector.W * matrix.M42 ),
                ( vector.X * matrix.M13 ) + ( vector.Y * matrix.M23 ) + ( vector.Z * matrix.M33 ) + ( vector.W * matrix.M43 ),
                ( vector.X * matrix.M14 ) + ( vector.Y * matrix.M24 ) + ( vector.Z * matrix.M34 ) + ( vector.W * matrix.M44 ));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Abs() =>
            Abs(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Clamp(Vector4Fix16 min, Vector4Fix16 max) =>
            Clamp(this, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span) =>
            CopyTo(span, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 4)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
            span[index + 3] = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Fix16 x, out Fix16 y, out Fix16 z, out Fix16 w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector3Fix16 vector, out Fix16 w)
        {
            w = W;
            vector = new Vector3Fix16(X, Y, Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector2Fix16 vector, out Fix16 z, out Fix16 w)
        {
            vector = new Vector2Fix16(X, Y);
            z = Z;
            w = W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Distance(Vector4Fix16 value) =>
            Distance(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 DistanceSquared(Vector4Fix16 value) =>
            DistanceSquared(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Dot(Vector4Fix16 vector) =>
            Dot(this, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj) =>
            obj is Vector4Fix16 vec && Equals(vec);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4Fix16 other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector4Fix16 other, Fix16 delta)
        {
            if (delta == Fix16.Zero)
                return this == other;
            var vector = Subtract(this, other).Abs();
            if (vector.X < delta && vector.Y < delta && vector.Z < delta)
                return vector.W < delta;
            return false;
        }

        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Length() =>
            Fix16.Sqrt(Dot(this, this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 LengthSquared() =>
            Dot(this, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Normalize() =>
            Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 SquareRoot() =>
            SquareRoot(this);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}, {W.ToString(format, formatProvider)}>";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 Transform(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 With(Fix16? x = null, Fix16? y = null, Fix16? z = null, Fix16? w = null) =>
            new(x ?? X, y ?? Y, z ?? Z, w ?? W);
    }
}
