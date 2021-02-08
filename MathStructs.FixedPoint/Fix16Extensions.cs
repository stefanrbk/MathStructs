using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
    public unsafe static class Fix16Extensions
    {
        public static Matrix3x3Fix16 ToMatrix3x3ix16(this ReadOnlySpan<Fix16> span) =>
            new Matrix3x3Fix16(span[0], span[1], span[2],
                               span[3], span[4], span[5],
                               span[6], span[7], span[8]);

        public static Matrix3x3Fix16 ToMatrix3x3ix16(this Span<Fix16> span) =>
            new Matrix3x3Fix16(span[0], span[1], span[2],
                               span[3], span[4], span[5],
                               span[6], span[7], span[8]);

        public static Matrix3x3Fix16 ToMatrix3x3ix16(this Fix16[] array) =>
            new Matrix3x3Fix16(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8]);

        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this ReadOnlySpan<Fix16> span) =>
            new Matrix4x4Fix16(span[0], span[1], span[2], span[3],
                               span[4], span[5], span[6], span[7],
                               span[8], span[9], span[10], span[11],
                               span[12], span[13], span[14], span[15]);

        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this Span<Fix16> span) =>
            new Matrix4x4Fix16(span[0], span[1], span[2], span[3],
                               span[4], span[5], span[6], span[7],
                               span[8], span[9], span[10], span[11],
                               span[12], span[13], span[14], span[15]);

        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this Fix16[] array) =>
            new Matrix4x4Fix16(array[0], array[1], array[2], array[3],
                               array[4], array[5], array[6], array[7],
                               array[8], array[9], array[10], array[11],
                               array[12], array[13], array[14], array[15]);

        public static Vector2Fix16 ToVector2Fix16(this ReadOnlySpan<Fix16> span) =>
            new Vector2Fix16(span[0], span[1]);

        public static Vector2Fix16 ToVector2Fix16(this Span<Fix16> span) =>
            new Vector2Fix16(span[0], span[1]);

        public static Vector2Fix16 ToVector2Fix16(this Fix16[] array) =>
            new Vector2Fix16(array[0], array[1]);

        public static Vector3Fix16 ToVector3Fix16(this ReadOnlySpan<Fix16> span) =>
            new Vector3Fix16(span[0], span[1], span[2]);

        public static Vector3Fix16 ToVector3Fix16(this Span<Fix16> span) =>
            new Vector3Fix16(span[0], span[1], span[2]);

        public static Vector3Fix16 ToVector3Fix16(this Fix16[] array) =>
            new Vector3Fix16(array[0], array[1], array[2]);

        public static Vector4Fix16 ToVector4Fix16(this ReadOnlySpan<Fix16> span) =>
            new Vector4Fix16(span[0], span[1], span[2], span[3]);

        public static Vector4Fix16 ToVector4Fix16(this Span<Fix16> span) =>
            new Vector4Fix16(span[0], span[1], span[2], span[3]);

        public static Vector4Fix16 ToVector4Fix16(this Fix16[] array) =>
            new Vector4Fix16(array[0], array[1], array[2], array[3]);
    }
}
