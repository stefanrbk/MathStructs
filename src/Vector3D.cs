using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace MathStructs
{
    public struct Vector3D : IEquatable<Vector3D>, IFormattable
    {
        #region Public Fields

        public double X;
        public double Y;
        public double Z;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;

        #endregion Private Fields

        #region Public Constructors

        [MethodImpl(Inline)]
        public Vector3D(double value)
            : this(value, value, value) { }

        [MethodImpl(Inline)]
        public Vector3D(Vector2D value, double z)
            : this(value.X, value.Y, z) { }

        [MethodImpl(Inline)]
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion Public Constructors

        #region Public Properties

        public static Vector3D One =>
            new Vector3D(1d);

        public static Vector3D UnitX =>
            new Vector3D(1d, 0d, 0d);

        public static Vector3D UnitY =>
            new Vector3D(0d, 1d, 0d);

        public static Vector3D UnitZ =>
            new Vector3D(0d, 0d, 1d);

        public static Vector3D Zero =>
                                                                    new Vector3D(0d);

        #endregion Public Properties

        #region Public Methods

        [MethodImpl(Inline)]
        public static Vector3D Abs(Vector3D value) =>
            new Vector3D(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));

        [MethodImpl(Inline)]
        public static Vector3D Add(Vector3D left, Vector3D right) =>
            left + right;

        [MethodImpl(Inline)]
        public static Vector3D Clamp(Vector3D value, Vector3D min, Vector3D max) =>
            Min(Max(value, min), max);

        [MethodImpl(Inline)]
        public static Vector3D Cross(Vector3D vector1, Vector3D vector2) =>
            new Vector3D(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);

        [MethodImpl(Inline)]
        public static double Distance(Vector3D vector1, Vector3D vector2) =>
            (vector1 - vector2).Length();

        [MethodImpl(Inline)]
        public static double DistanceSquared(Vector3D vector1, Vector3D vector2) =>
            (vector1 - vector2).LengthSquared();

        [MethodImpl(Inline)]
        public static Vector3D Divide(Vector3D left, Vector3D right) =>
            left / right;

        [MethodImpl(Inline)]
        public static Vector3D Divide(Vector3D left, double right) =>
            left / right;

        [MethodImpl(Inline)]
        public static double Dot(Vector3D left, Vector3D right) =>
            left.X * right.X +
            left.Y * right.Y +
            left.Z * right.Z;

        [MethodImpl(Inline)]
        public static Vector3D Lerp(Vector3D bounds1, Vector3D bounds2, double amount) =>
            bounds1 * (1d - amount) + bounds2 * amount;

        [MethodImpl(Inline)]
        public static Vector3D Max(Vector3D left, Vector3D right) =>
            new Vector3D(left.X > right.X ? left.X : right.X,
                         left.Y > right.Y ? left.Y : right.Y,
                         left.Z > right.Z ? left.Z : right.Z);

        [MethodImpl(Inline)]
        public static Vector3D Min(Vector3D left, Vector3D right) =>
            new Vector3D(left.X < right.X ? left.X : right.X,
                         left.Y < right.Y ? left.Y : right.Y,
                         left.Z < right.Z ? left.Z : right.Z);

        [MethodImpl(Inline)]
        public static Vector3D Multiply(Vector3D left, Vector3D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3D Multiply(Vector3D left, double right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3D Multiply(double left, Vector3D right) =>
            left * right;

        [MethodImpl(Inline)]
        public static Vector3D Negate(Vector3D value) =>
            -value;

        [MethodImpl(Inline)]
        public static Vector3D Normalize(Vector3D vector) =>
            vector / vector.Length();

        [MethodImpl(Inline)]
        public static Vector3D operator -(Vector3D value) =>
            new Vector3D(-value.X, -value.Y, -value.Z);

        [MethodImpl(Inline)]
        public static Vector3D operator -(Vector3D left, Vector3D right) =>
            new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        [MethodImpl(Inline)]
        public static bool operator !=(Vector3D left, Vector3D right) =>
            left.X != right.X || left.Y != right.Y || left.Z != right.Z;

        [MethodImpl(Inline)]
        public static Vector3D operator *(Vector3D left, Vector3D right) =>
            new Vector3D(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        [MethodImpl(Inline)]
        public static Vector3D operator *(Vector3D left, double right) =>
            new Vector3D(left.X * right, left.Y * right, left.Z * right);

        [MethodImpl(Inline)]
        public static Vector3D operator *(double left, Vector3D right) =>
            right * left;

        [MethodImpl(Inline)]
        public static Vector3D operator /(Vector3D left, Vector3D right) =>
            new Vector3D(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        [MethodImpl(Inline)]
        public static Vector3D operator /(Vector3D left, double right) =>
            new Vector3D(left.X / right, left.Y / right, left.Z / right);

        [MethodImpl(Inline)]
        public static Vector3D operator +(Vector3D value) =>
            value;

        [MethodImpl(Inline)]
        public static Vector3D operator +(Vector3D left, Vector3D right) =>
            new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        [MethodImpl(Inline)]
        public static bool operator ==(Vector3D left, Vector3D right) =>
            left.X == right.X && left.Y == right.Y && left.Z == right.Z;

        [MethodImpl(Inline)]
        public static Vector3D Reflect(Vector3D vector, Vector3D normal) =>
            vector - normal * Dot(vector, normal) * 2f;

        [MethodImpl(Inline)]
        public static Vector3D SquareRoot(Vector3D value) =>
            new Vector3D(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z));

        [MethodImpl(Inline)]
        public static Vector3D Subtract(Vector3D left, Vector3D right) =>
            left - right;

        [MethodImpl(Inline)]
        public static Vector3D Transform(Vector3D vector, Matrix4x4D matrix) =>
            new Vector3D(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41,
                         vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42,
                         vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);

        [MethodImpl(Inline)]
        public static Vector3D Transform(Vector3D value, QuaternionD rotation)
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
            return new Vector3D(value.X * (1d - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1d - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1d - num7 - num10));
        }

        [MethodImpl(Inline)]
        public static Vector3D TransformNormal(Vector3D normal, Matrix4x4D matrix) =>
            new Vector3D(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                         normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                         normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);

        [MethodImpl(Inline)]
        public Vector3D Abs() =>
            Abs(this);

        [MethodImpl(Inline)]
        public Vector3D Clamp(Vector3D min, Vector3D max) =>
            Clamp(this, min, max);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array) =>
            CopyTo(array, 0);

        [MethodImpl(Inline)]
        public void CopyTo(double[] array, int index)
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
        public Vector3D Cross(Vector3D vector) =>
            Cross(this, vector);

        [MethodImpl(Inline)]
        public void Deconstruct(out Vector2D vector, out double z)
        {
            vector = new Vector2D(X, Y);
            z = Z;
        }

        [MethodImpl(Inline)]
        public void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        [MethodImpl(Inline)]
        public double Distance(Vector3D value) =>
            Distance(this, value);

        [MethodImpl(Inline)]
        public double DistanceSquared(Vector3D value) =>
            DistanceSquared(this, value);

        [MethodImpl(Inline)]
        public double Dot(Vector3D vector) =>
            Dot(this, vector);

        [MethodImpl(Inline)]
        public override bool Equals(object? obj) =>
            obj is Vector3D vec && Equals(vec);

        [MethodImpl(Inline)]
        public bool Equals(Vector3D other) =>
            this == other;

        [MethodImpl(Inline)]
        public bool Equals(Vector3D other, double delta)
        {
            if (delta is 0.0)
                return this == other;

            var vector = Subtract(this, other).Abs();
            return vector.X < delta &&
                   vector.Y < delta &&
                   vector.Z < delta;
        }

        public override int GetHashCode() =>
            HashCode.Combine(X.GetHashCode(), Y.GetHashCode(), Z.GetHashCode());

        [MethodImpl(Inline)]
        public double Length() =>
            Math.Sqrt(Dot(this, this));

        [MethodImpl(Inline)]
        public double LengthSquared() =>
            Dot(this, this);

        [MethodImpl(Inline)]
        public Vector3D Normalize() =>
            Normalize(this);

        [MethodImpl(Inline)]
        public Vector3D Reflect(Vector3D normal) =>
            Reflect(this, normal);

        [MethodImpl(Inline)]
        public Vector3D SquareRoot() =>
            SquareRoot(this);

        public override string ToString() =>
            ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string? format) =>
            ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string? format, IFormatProvider? formatProvider) =>
            $"<{X.ToString(format, formatProvider)}, {Y.ToString(format, formatProvider)}, {Z.ToString(format, formatProvider)}>";

        [MethodImpl(Inline)]
        public Vector3D Transform(Matrix4x4D matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public Vector3D TransformNormal(Matrix4x4D matrix) =>
            Transform(this, matrix);

        [MethodImpl(Inline)]
        public Vector4D TransformV4(Matrix4x4D matrix) =>
            Vector4D.Transform(this, matrix);

        [MethodImpl(Inline)]
        public Vector4D TransformV4(QuaternionD rotation) =>
            Vector4D.Transform(this, rotation);

        [MethodImpl(Inline)]
        public Vector3D With(double? x = null, double? y = null, double? z = null) =>
            new Vector3D(x ?? X, y ?? Y, z ?? Z);

        #endregion Public Methods
    }
}