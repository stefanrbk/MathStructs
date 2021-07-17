using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public readonly struct Vector3Fix16 : IEquatable<Vector3Fix16>, IFormattable
    {
        [FieldOffset(0)]
        public readonly Fix16 X;

        [FieldOffset(4)]
        public readonly Fix16 Y;

        [FieldOffset(8)]
        public readonly Fix16 Z;

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        public static Vector3Fix16 One => new Vector3Fix16(Fix16.One);

        public static Vector3Fix16 UnitX => new Vector3Fix16(Fix16.One, Fix16.Zero, Fix16.Zero);

        public static Vector3Fix16 UnitY => new Vector3Fix16(Fix16.Zero, Fix16.One, Fix16.Zero);

        public static Vector3Fix16 UnitZ => new Vector3Fix16(Fix16.Zero, Fix16.Zero, Fix16.One);

        public static Vector3Fix16 Zero => new Vector3Fix16(Fix16.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(Fix16 value)
            : this(value, value, value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(Vector2Fix16 value, Fix16 z)
            : this(value.X, value.Y, z)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16(Fix16 x, Fix16 y, Fix16 z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Abs(Vector3Fix16 value) =>
            new Vector3Fix16(Fix16.Abs(value.X), Fix16.Abs(value.Y), Fix16.Abs(value.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Add(Vector3Fix16 left, Vector3Fix16 right) =>
            left + right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Clamp(Vector3Fix16 value, Vector3Fix16 min, Vector3Fix16 max) =>
            Min(Max(value, min), max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Cross(Vector3Fix16 vector1, Vector3Fix16 vector2) =>
            new Vector3Fix16(( vector1.Y * vector2.Z ) - ( vector1.Z * vector2.Y ),
                             ( vector1.Z * vector2.X ) - ( vector1.X * vector2.Z ),
                             ( vector1.X * vector2.Y ) - ( vector1.Y * vector2.X ));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Distance(Vector3Fix16 vector1, Vector3Fix16 vector2) =>
            ( vector1 - vector2 ).Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 DistanceSquared(Vector3Fix16 vector1, Vector3Fix16 vector2) =>
            ( vector1 - vector2 ).LengthSquared();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Divide(Vector3Fix16 left, Vector3Fix16 right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Divide(Vector3Fix16 left, Fix16 right) =>
            left / right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Fix16 Dot(Vector3Fix16 left, Vector3Fix16 right) =>
            ( left.X * right.X ) + ( left.Y * right.Y ) + ( left.Z * right.Z );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Lerp(Vector3Fix16 bounds1, Vector3Fix16 bounds2, Fix16 amount) =>
            ( bounds1 * ( Fix16.One - amount ) ) + ( bounds2 * amount );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Max(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(( left.X > right.X ) ? left.X : right.X, ( left.Y > right.Y ) ? left.Y : right.Y, ( left.Z > right.Z ) ? left.Z : right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Min(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(( left.X < right.X ) ? left.X : right.X, 
                             ( left.Y < right.Y ) ? left.Y : right.Y,
                             ( left.Z < right.Z ) ? left.Z : right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(Vector3Fix16 left, Vector3Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(Vector3Fix16 left, Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Multiply(Fix16 left, Vector3Fix16 right) =>
            left * right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Negate(Vector3Fix16 value) =>
            -value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Normalize(Vector3Fix16 vector) =>
            vector / vector.Length();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator -(Vector3Fix16 value) =>
            new Vector3Fix16(-value.X, -value.Y, -value.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator -(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3Fix16 left, Vector3Fix16 right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(Vector3Fix16 left, Fix16 right) =>
            new Vector3Fix16(left.X * right, left.Y * right, left.Z * right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator *(Fix16 left, Vector3Fix16 right) =>
            right * left;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator /(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator /(Vector3Fix16 left, Fix16 right) =>
            new Vector3Fix16(left.X / right, left.Y / right, left.Z / right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator +(Vector3Fix16 value) =>
            value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 operator +(Vector3Fix16 left, Vector3Fix16 right) =>
            new Vector3Fix16(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3Fix16 left, Vector3Fix16 right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Reflect(Vector3Fix16 vector, Vector3Fix16 normal) =>
            vector - ( normal * Dot(vector, normal ) * (Fix16)2 );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 SquareRoot(Vector3Fix16 value) =>
            new Vector3Fix16(Fix16.Sqrt(value.X), Fix16.Sqrt(value.Y), Fix16.Sqrt(value.Z));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Subtract(Vector3Fix16 left, Vector3Fix16 right) =>
            left - right;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 Transform(Vector3Fix16 vector, Matrix4x4Fix16 matrix) =>
            new Vector3Fix16(( vector.X * matrix.M11 ) + ( vector.Y * matrix.M21 ) + ( vector.Z * matrix.M31 ) + matrix.M41,
                             ( vector.X * matrix.M12 ) + ( vector.Y * matrix.M22 ) + ( vector.Z * matrix.M32 ) + matrix.M42,
                             ( vector.X * matrix.M13 ) + ( vector.Y * matrix.M23 ) + ( vector.Z * matrix.M33 ) + matrix.M43);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Fix16 TransformNormal(Vector3Fix16 normal, Matrix4x4Fix16 matrix) =>
            new Vector3Fix16(( normal.X * matrix.M11 ) + ( normal.Y * matrix.M21 ) + ( normal.Z * matrix.M31 ),
                             ( normal.X * matrix.M12 ) + ( normal.Y * matrix.M22 ) + ( normal.Z * matrix.M32 ),
                             ( normal.X * matrix.M13 ) + ( normal.Y * matrix.M23 ) + ( normal.Z * matrix.M33 ));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Abs() =>
            Abs(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Clamp(Vector3Fix16 min, Vector3Fix16 max) =>
            Clamp(this, min, max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span) =>
            CopyTo(span, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Span<Fix16> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 3)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index] = X;
            span[index + 1] = Y;
            span[index + 2] = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Cross(Vector3Fix16 vector) =>
            Cross(this, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Vector2Fix16 vector, out Fix16 z)
        {
            vector = new Vector2Fix16(X, Y);
            z = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out Fix16 x, out Fix16 y, out Fix16 z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Distance(Vector3Fix16 value) =>
            Distance(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 DistanceSquared(Vector3Fix16 value) =>
            DistanceSquared(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Dot(Vector3Fix16 vector) =>
            Dot(this, vector);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj) =>
            obj is Vector3Fix16 vec && Equals(vec);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3Fix16 other) =>
            this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3Fix16 other, Fix16 delta)
        {
            if (delta == Fix16.Zero)
                return this == other;
            var vector = Subtract(this, other).Abs();

            return vector.X < delta && vector.Y < delta && vector.Z < delta;
        }

        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 Length() =>
            Fix16.Sqrt(Dot(this, this));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fix16 LengthSquared() =>
            Dot(this, this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Normalize() =>
            Normalize(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Reflect(Vector3Fix16 normal) =>
            Reflect(this, normal);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 SquareRoot() =>
            SquareRoot(this);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}>";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 Transform(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 TransformNormal(Matrix4x4Fix16 matrix) =>
            Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Fix16 TransformV4(Matrix4x4Fix16 matrix) =>
            Vector4Fix16.Transform(this, matrix);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Fix16 With(Fix16? x = null, Fix16? y = null, Fix16? z = null) =>
            new Vector3Fix16(x ?? X, y ?? Y, z ?? Z);
    }
}
