using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Numerics
{
    /// <summary>
    /// </summary>
    public struct Matrix3x2D : IEquatable<Matrix3x2D>
    {
        private const double RotationEpsilon = 0x001 * Math.PI / 180.0;

        /// <summary>
        /// </summary>
        public double M11;
        /// <summary>
        /// </summary>
        public double M12;
        /// <summary>
        /// </summary>
        public double M21;
        /// <summary>
        /// </summary>
        public double M22;
        /// <summary>
        /// </summary>
        public double M31;
        /// <summary>
        /// </summary>
        public double M32;

        /// <summary>
        /// </summary>
        public Matrix3x2D(double m11, double m12, double m21, double m22, double m31, double m32) =>
            (M11, M12, M21, M22, M31, M32) = (m11, m12, m21, m22, m31, m32);

        private static readonly Matrix3x2D _identity = new Matrix3x2D(1, 0, 0, 1, 0, 0);
        private static readonly Matrix3x2D _nan= new Matrix3x2D(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN);

        /// <summary>
        /// </summary>
        public static Matrix3x2D Identity => _identity;

        /// <summary>
        /// </summary>
        public static Matrix3x2D NaN => _nan;

        /// <summary>
        /// </summary>
        public bool IsIdentity =>
            M11 == 1.0 && M22 == 1.0 &&
            M12 == 0.0 && M21 == 0.0 &&
            M31 == 0.0 && M32 == 0.0;

        /// <summary>
        /// </summary>
        public Vector2D Translation
        {
            readonly get => new Vector2D(M31, M32);
            set => (M31, M32) = value;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateTranslation(Vector2D position) =>
            CreateTranslation(position.X, position.Y);

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateTranslation(double xPosition, double yPosition)
        {
            var result = _identity;

            result.M31 = xPosition;
            result.M32 = yPosition;

            return result;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(double scale) =>
            CreateScale(scale, scale);

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(Vector2D scales) =>
            CreateScale(scales.X, scales.Y);

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(double xScale, double yScale)
        {
            var result = _identity;

            result.M11 = xScale;
            result.M22 = yScale;

            return result;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(double scale, Vector2D centerPoint) =>
            CreateScale(scale, scale, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(Vector2D scales, Vector2D centerPoint) =>
            CreateScale(scales.X, scales.Y, centerPoint);

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateScale(double xScale, double yScale, Vector2D centerPoint) =>
            CreateScale(xScale, yScale).SetTranslation(centerPoint.X * ( 1-xScale ), centerPoint.Y * ( 1-yScale ));

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateSkew(double radiansX, double radiansY)
        {
            var result = _identity;

            result.M12 = Math.Tan(radiansX);
            result.M21 = Math.Tan(radiansY);

            return result;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateSkew(double radiansX, double radiansY, Vector2D centerPoint)
        {
            var result = _identity;

            var xTan = Math.Tan(radiansX);
            var yTan = Math.Tan(radiansY);

            result.M12 = xTan;
            result.M21 = yTan;

            return result.SetTranslation(-centerPoint.Y * xTan, -centerPoint.X * yTan);
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateRotation(double radians)
        {
            radians = Math.IEEERemainder(radians, Math.PI * 2);

            double c, s;

            if (radians is > -RotationEpsilon and < RotationEpsilon)
                (c, s) = (1, 0);
            else if (radians is > Math.PI / 2 - RotationEpsilon and < Math.PI / 2 + RotationEpsilon)
                (c, s) = (0, 1);
            else if (radians is < -Math.PI + RotationEpsilon or > Math.PI - RotationEpsilon)
                (c, s) = (-1, 0);
            else if (radians is > -Math.PI / 2 - RotationEpsilon and < -Math.PI / 2 + RotationEpsilon)
                (c, s) = (0, -1);
            else
                (c, s) = (Math.Cos(radians), Math.Sin(radians));

            return new Matrix3x2D(c, s, -s, c, 0, 0);
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D CreateRotation(double radians, Vector2D centerPoint)
        {
            radians = Math.IEEERemainder(radians, Math.PI * 2);

            double c, s;

            if (radians is > -RotationEpsilon and < RotationEpsilon)
                (c, s) = (1, 0);
            else if (radians is > Math.PI / 2 - RotationEpsilon and < Math.PI / 2 + RotationEpsilon)
                (c, s) = (0, 1);
            else if (radians is < -Math.PI + RotationEpsilon or > Math.PI - RotationEpsilon)
                (c, s) = (-1, 0);
            else if (radians is > -Math.PI / 2 - RotationEpsilon and < -Math.PI / 2 + RotationEpsilon)
                (c, s) = (0, -1);
            else
                (c, s) = (Math.Cos(radians), Math.Sin(radians));

            return new Matrix3x2D(c, s, -s, c, centerPoint.X * ( 1-c ) + centerPoint.Y * s, centerPoint.Y * ( 1-c ) - centerPoint.X * s);
        }

        /// <summary>
        /// </summary>
        public double GetDeterminant() =>
            ( M11 * M22 ) - ( M21 * M12 );

        /// <summary>
        /// </summary>
        public static bool Invert(Matrix3x2D matrix, out Matrix3x2D result)
        {
            result = NaN;

            var det = matrix.GetDeterminant();

            if (Math.Abs(det) < Double.Epsilon)
                return false;

            var invDet = 1 / det;

            result = new Matrix3x2D(matrix.M22 * invDet, -matrix.M12 * invDet,
                                  -matrix.M21 * invDet, matrix.M11 * invDet,
                                  ( matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22 ) * invDet,
                                  ( matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32 ) * invDet);
            return false;
        }

        /// <summary>
        /// </summary>
        public static Matrix3x2D Lerp(Matrix3x2D matrix1, Matrix3x2D matrix2, double amount) =>
            matrix1 + ( matrix2 - matrix1 ) * amount;

        /// <summary>
        /// </summary>
        public static Matrix3x2D Negate(Matrix3x2D value) =>
            new Matrix3x2D(-value.M11, -value.M12, -value.M21, -value.M22, -value.M31, -value.M32);

        /// <summary>
        /// </summary>
        public static Matrix3x2D Add(Matrix3x2D left, Matrix3x2D right) =>
            new Matrix3x2D(left.M11 + right.M11,
                           left.M12 + right.M12,
                           left.M21 + right.M21,
                           left.M22 + right.M22,
                           left.M31 + right.M31,
                           left.M32 + right.M32);

        /// <summary>
        /// </summary>
        public static Matrix3x2D Subtract(Matrix3x2D left, Matrix3x2D right) =>
            new Matrix3x2D(left.M11 - right.M11,
                           left.M12 - right.M12,
                           left.M21 - right.M21,
                           left.M22 - right.M22,
                           left.M31 - right.M31,
                           left.M32 - right.M32);

        /// <summary>
        /// </summary>
        public static Matrix3x2D Multiply(Matrix3x2D left, Matrix3x2D right) =>
            new Matrix3x2D(left.M11 * right.M11 + left.M12 * right.M21,
                           left.M11 * right.M12 + left.M12 * right.M22,
                           left.M21 * right.M11 + left.M22 * right.M21,
                           left.M21 * right.M12 + left.M22 * right.M22,
                           left.M31 * right.M11 + left.M32 * right.M21 + right.M31,
                           left.M31 * right.M12 + left.M32 * right.M22 + right.M32);

        /// <summary>
        /// </summary>
        public static Matrix3x2D Multiply(Matrix3x2D matrix, double scalar) =>
            new Matrix3x2D(matrix.M11 * scalar,
                           matrix.M12 * scalar,
                           matrix.M21 * scalar,
                           matrix.M22 * scalar,
                           matrix.M31 * scalar,
                           matrix.M32 * scalar);

        /// <summary>
        /// </summary>
        public static Matrix3x2D operator -(Matrix3x2D value) =>
            Negate(value);

        /// <summary>
        /// </summary>
        public static Matrix3x2D operator +(Matrix3x2D left, Matrix3x2D right) =>
            Add(left, right);

        /// <summary>
        /// </summary>
        public static Matrix3x2D operator -(Matrix3x2D left, Matrix3x2D right) =>
            Subtract(left, right);

        /// <summary>
        /// </summary>
        public static Matrix3x2D operator *(Matrix3x2D left, Matrix3x2D right) =>
            Multiply(left, right);

        /// <summary>
        /// </summary>
        public static Matrix3x2D operator *(Matrix3x2D left, double right) =>
            Multiply(left, right);

        /// <summary>
        /// </summary>
        public static bool operator ==(Matrix3x2D left, Matrix3x2D right) =>
            left.Equals(right);

        /// <summary>
        /// </summary>
        public static bool operator !=(Matrix3x2D left, Matrix3x2D right) =>
            !left.Equals(right);

        /// <summary>
        /// </summary>
        public readonly bool Equals(Matrix3x2D other) =>
            M11 == other.M11 && M22 == other.M22 &&
            M12 == other.M12 && M21 == other.M21 &&
            M31 == other.M31 && M32 == other.M32;

        /// <summary>
        /// </summary>
        public override readonly bool Equals(object? obj) =>
            obj is Matrix3x2D m && Equals(m);

        /// <summary>
        /// </summary>
        public override readonly string ToString() =>
            $"{{ {{M11:{M11} M12:{M12}}} {{M21:{M21} M22:{M22}}} {{M31:{M31} M32:{312}}} }}";

        /// <summary>
        /// </summary>
        public override readonly int GetHashCode() =>
            HashCode.Combine(M11, M12, M21, M22, M31, M32);

        private Matrix3x2D SetTranslation(double x, double y)
        {
            M31 = x;
            M32 = y;

            return this;
        }
    }
}
