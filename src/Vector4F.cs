using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct Vector4F : IEquatable<Vector4F>, IFormattable
    {
        #region Public Fields

        [FieldOffset(0)]
        public float X;

        [FieldOffset(4)]
        public float Y;

        [FieldOffset(8)]
        public float Z;

        [FieldOffset(12)]
        public float W;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        #endregion Private Fields

        #region Public Constructors

        [MethodImpl(Inline)]
        public Vector4F(float value)
            : this(value, value, value, value) { }

        [MethodImpl(Inline)]
        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(Inline)]
        public Vector4F(Vector3F vector, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
            W = w;
        }

        [MethodImpl(Inline)]
        public Vector4F(Vector2F vector, float z, float w)
        {
            X = vector.X;
            Y = vector.Y;
            Z = z;
            W = w;
        }

        #endregion Public Constructors

        #region Public Properties

        public static Vector4F One =>
            new Vector4F(1f);

        public static Vector4F UnitW =>
            new Vector4F(0f, 0f, 0f, 1f);

        public static Vector4F UnitX =>
            new Vector4F(1f, 0f, 0f, 0f);

        public static Vector4F UnitY =>
            new Vector4F(0f, 1f, 0f, 0f);

        public static Vector4F UnitZ =>
            new Vector4F(0f, 0f, 1f, 0f);

        public static Vector4F Zero =>
                                                                                    new Vector4F(0f);

        #endregion Public Properties

        #region Public Methods

        [MethodImpl(Inline)]
        public static Vector4F Abs(Vector4F value) =>
            new Vector4F(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));

        [MethodImpl(Inline)]
        public static Vector4F Add(Vector4F left, Vector4F right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector4F Clamp(Vector4F value, Vector4F min, Vector4F max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public static float Distance(Vector4F vector1, Vector4F vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public static float DistanceSquared(Vector4F vector1, Vector4F vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public static Vector4F Divide(Vector4F left, Vector4F right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector4F Divide(Vector4F left, float right) =>
            left / right;

        [MethodImpl(Inline)]
        public static float Dot(Vector4F left, Vector4F right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z +
            left.W * right.W;

        [MethodImpl(Inline)]
        public static Vector4F Lerp(Vector4F bounds1, Vector4F bounds2, float amount) =>
            bounds1 * (1f - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector4F Max(Vector4F left, Vector4F right) =>
            new Vector4F(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z,
                         left.W > right.W ? left.W : right.W);

        [MethodImpl(Inline)]
        public static Vector4F Min(Vector4F left, Vector4F right) =>
            new Vector4F(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z,
                         left.W < right.W ? left.W : right.W);

        [MethodImpl(Inline)]
        public static Vector4F Multiply(Vector4F left, Vector4F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4F Multiply(Vector4F left, float right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4F Multiply(float left, Vector4F right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector4F Negate(Vector4F value) =>
            -value;

        [MethodImpl(Inline)]
        public static Vector4F Normalize(Vector4F vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public static Vector4F operator -(Vector4F value) =>
            new Vector4F(-value.X, -value.Y, -value.Z, -value.W);

        [MethodImpl(Inline)]
        public static Vector4F operator -(Vector4F left, Vector4F right) =>
            new Vector4F(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        [MethodImpl(Inline)]
        public static bool operator !=(Vector4F left, Vector4F right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z || left.W != right.W;

        [MethodImpl(Inline)]
        public static Vector4F operator *(Vector4F left, Vector4F right) =>
            new Vector4F(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        [MethodImpl(Inline)]
        public static Vector4F operator *(Vector4F left, float right) =>
            new Vector4F(left.X * right, left.Y * right, left.Z * right, left.W * right);

        [MethodImpl(Inline)]
        public static Vector4F operator *(float left, Vector4F right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector4F operator /(Vector4F left, Vector4F right) =>
            new Vector4F(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        [MethodImpl(Inline)]
        public static Vector4F operator /(Vector4F left, float right) =>
            new Vector4F(left.X / right, left.Y / right, left.Z / right, left.W / right);

        [MethodImpl(Inline)]
        public static Vector4F operator +(Vector4F value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector4F operator +(Vector4F left, Vector4F right) =>
            new Vector4F(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector4F left, Vector4F right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;

        [MethodImpl(Inline)]
        public static Vector4F SquareRoot(Vector4F value) =>
            new Vector4F(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));

        [MethodImpl(Inline)]
        public static Vector4F Subtract(Vector4F left, Vector4F right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector2F position, Matrix4x4F matrix)
        {
            return new Vector4F(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
        }

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector3F vector, Matrix4x4F matrix) =>
            new Vector4F(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + matrix.M44);

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector4F vector, Matrix4x4F matrix) =>
            new Vector4F(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43,
                         vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector2F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
        }

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector3F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
        }

        [MethodImpl(Inline)]
        public static Vector4F Transform(Vector4F value, QuaternionF rotation)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            return new Vector4F(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
        }

        [MethodImpl(Inline)]
        public Vector4F Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public Vector4F Clamp(Vector4F min, Vector4F max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(float[] array, int index)
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

        [MethodImpl(Inline)]
        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector3F vector, out float w)
        {
            w = W;
            vector = new Vector3F(X, Y, Z);
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2F vector, out float z, out float w)
        {
            vector = new Vector2F(X, Y);
            z = Z;
            w = W;
        }

        [MethodImpl(Inline)]
        public float Distance(Vector4F value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public float DistanceSquared(Vector4F value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public float Dot(Vector4F vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector4F vec && Equals(vec);

        [MethodImpl(Inline)]
        public bool Equals(Vector4F other) =>
            this == other;

        [MethodImpl(Inline)]
        public bool Equals(Vector4F other, float delta)
        {
            if (delta is 0.0f)
                return this == other;

            var vector = Subtract(this, other).Abs();
            return vector.X < delta &&
                   vector.Y < delta &&
                   vector.Z < delta &&
                   vector.W < delta;
        }

        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode(), W.GetHashCode());

        [MethodImpl(Inline)]
        public float Length() =>
            MathF.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public float LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public Vector4F Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public Vector4F SquareRoot() =>
            SquareRoot(this);

        public override string ToString() =>
                                    ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}, {W.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public Vector4F Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public Vector4F Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        [MethodImpl(Inline)]
        public Vector4F With(float? x = null, float? y = null, float? z = null, float? w = null) =>
            new Vector4F(x ?? X, y ?? Y, z ?? Z, w ?? W);

        #endregion Public Methods
    }
}