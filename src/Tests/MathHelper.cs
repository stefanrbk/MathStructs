﻿using MathStructs;

using System;

namespace Tests
{
    static class MathHelper
    {
        public const float Pi = (float)Math.PI;
        public const float PiOver2 = Pi / 2f;
        public const float PiOver4 = Pi / 4f;


        // Angle conversion helper.
        public static float ToRadians(float degrees)
        {
            return degrees * Pi / 180f;
        }


        // Comparison helpers with small tolerance to allow for floating point rounding during computations.
        public static bool Equal(float a, float b)
        {
            return (Math.Abs(a - b) < 1e-5);
        }

        public static bool Equal(Vector2F a, Vector2F b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y);
        }

        public static bool Equal(Vector3F a, Vector3F b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z);
        }

        public static bool Equal(Vector4F a, Vector4F b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);
        }

        public static bool Equal(Matrix4x4F a, Matrix4x4F b)
        {
            return
                Equal(a.M11, b.M11) && Equal(a.M12, b.M12) && Equal(a.M13, b.M13) && Equal(a.M14, b.M14) &&
                Equal(a.M21, b.M21) && Equal(a.M22, b.M22) && Equal(a.M23, b.M23) && Equal(a.M24, b.M24) &&
                Equal(a.M31, b.M31) && Equal(a.M32, b.M32) && Equal(a.M33, b.M33) && Equal(a.M34, b.M34) &&
                Equal(a.M41, b.M41) && Equal(a.M42, b.M42) && Equal(a.M43, b.M43) && Equal(a.M44, b.M44);
        }

        public static bool Equal(PlaneF a, PlaneF b)
        {
            return Equal(a.Normal, b.Normal) && Equal(a.D, b.D);
        }

        public static bool Equal(QuaternionF a, QuaternionF b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);
        }

        public static bool EqualRotation(QuaternionF a, QuaternionF b)
        {
            return Equal(a, b) || Equal(a, -b);
        }
    }
}
