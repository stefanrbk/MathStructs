using MathStructs;

using System;

namespace Tests
{
    internal static class MathHelper
    {
        #region Public Fields

        public const float PiF = MathF.PI;
        public const float PiOver2F = PiF / 2f;
        public const float PiOver4F = PiF / 4f;

        public const float Rad90F = PiOver2F;
        public const float Rad180F = PiF;

        public const double PiD = Math.PI;
        public const double PiOver2D = PiD / 2d;
        public const double PiOver4D = PiD / 4d;

        public const double Rad90D = PiOver2D;
        public const double Rad180D = PiD;

        #endregion Public Fields

        #region Public Methods

        // Comparison helpers with small tolerance to allow for floating point rounding during computations.
        public static bool Equal(float a, float b) =>
             Math.Abs(a - b) < 1e-5 ;

        public static bool Equal(Vector2F a, Vector2F b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y);

        public static bool Equal(Vector3F a, Vector3F b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z);

        public static bool Equal(Vector4F a, Vector4F b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);

        public static bool Equal(Matrix4x4F a, Matrix4x4F b) =>
            Equal(a.M11, b.M11) && Equal(a.M12, b.M12) && Equal(a.M13, b.M13) && Equal(a.M14, b.M14) &&
            Equal(a.M21, b.M21) && Equal(a.M22, b.M22) && Equal(a.M23, b.M23) && Equal(a.M24, b.M24) &&
            Equal(a.M31, b.M31) && Equal(a.M32, b.M32) && Equal(a.M33, b.M33) && Equal(a.M34, b.M34) &&
            Equal(a.M41, b.M41) && Equal(a.M42, b.M42) && Equal(a.M43, b.M43) && Equal(a.M44, b.M44);

        public static bool Equal(PlaneF a, PlaneF b) =>
            Equal(a.Normal, b.Normal) && Equal(a.D, b.D);

        public static bool Equal(QuaternionF a, QuaternionF b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);

        // Comparison helpers with small tolerance to allow for doubling point rounding during computations.
        public static bool Equal(double a, double b) =>
            Math.Abs(a - b) < 1e-5 ;

        public static bool Equal(Vector2D a, Vector2D b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y);

        public static bool Equal(Vector3D a, Vector3D b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z);

        public static bool Equal(Vector4D a, Vector4D b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);

        public static bool Equal(Matrix4x4D a, Matrix4x4D b) =>
            Equal(a.M11, b.M11) && Equal(a.M12, b.M12) && Equal(a.M13, b.M13) && Equal(a.M14, b.M14) &&
            Equal(a.M21, b.M21) && Equal(a.M22, b.M22) && Equal(a.M23, b.M23) && Equal(a.M24, b.M24) &&
            Equal(a.M31, b.M31) && Equal(a.M32, b.M32) && Equal(a.M33, b.M33) && Equal(a.M34, b.M34) &&
            Equal(a.M41, b.M41) && Equal(a.M42, b.M42) && Equal(a.M43, b.M43) && Equal(a.M44, b.M44);

        public static bool Equal(PlaneD a, PlaneD b) =>
            Equal(a.Normal, b.Normal) && Equal(a.D, b.D);

        public static bool Equal(QuaternionD a, QuaternionD b) =>
            Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);

        public static bool EqualRotation(QuaternionF a, QuaternionF b) =>
            Equal(a, b) || Equal(a, -b);

        public static bool EqualRotation(QuaternionD a, QuaternionD b) =>
            Equal(a, b) || Equal(a, -b);

        // Angle conversion helper.
        public static float ToRadians(float degrees) =>
            degrees * PiF / 180f;

        public static double ToRadians(double degrees) =>
            degrees * Math.PI / 180;

        #endregion Public Methods
    }
}