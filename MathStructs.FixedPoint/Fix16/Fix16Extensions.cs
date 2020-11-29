using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public static class Fix16Extensions
    {
        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3Fix16"/>.
        /// </summary>
        public static Matrix3x3Fix16 ToMatrix3x3Fix16(this ReadOnlySpan<int> span) =>
            new Matrix3x3Fix16(span[0], span[1], span[2],
                                    span[3], span[4], span[5],
                                    span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3Fix16"/>.
        /// </summary>
        public static Matrix3x3Fix16 ToMatrix3x3Fix16(this Span<int> span) =>
            new Matrix3x3Fix16(span[0], span[1], span[2],
                                    span[3], span[4], span[5],
                                    span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3Fix16"/>.
        /// </summary>
        public static Matrix3x3Fix16 ToMatrix3x3Fix16(this int[] array) =>
            new Matrix3x3Fix16(array[0], array[1], array[2],
                                    array[3], array[4], array[5],
                                    array[6], array[7], array[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4Fix16"/>.
        /// </summary>
        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this ReadOnlySpan<int> span) =>
            new Matrix4x4Fix16(span[0], span[1], span[2], span[3],
                                    span[4], span[5], span[6], span[7],
                                    span[8], span[9], span[10], span[11],
                                    span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4Fix16"/>.
        /// </summary>
        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this Span<int> span) =>
            new Matrix4x4Fix16(span[0], span[1], span[2], span[3],
                                    span[4], span[5], span[6], span[7],
                                    span[8], span[9], span[10], span[11],
                                    span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4Fix16"/>.
        /// </summary>
        public static Matrix4x4Fix16 ToMatrix4x4Fix16(this int[] array) =>
            new Matrix4x4Fix16(array[0], array[1], array[2], array[3],
                                    array[4], array[5], array[6], array[7],
                                    array[8], array[9], array[10], array[11],
                                    array[12], array[13], array[14], array[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2Fix16"/>.
        /// </summary>
        public static Vector2Fix16 ToVector2Fix16(this ReadOnlySpan<int> span) =>
            new Vector2Fix16(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2Fix16"/>.
        /// </summary>
        public static Vector2Fix16 ToVector2Fix16(this Span<int> span) =>
            new Vector2Fix16(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2Fix16"/>.
        /// </summary>
        public static Vector2Fix16 ToVector2Fix16(this int[] array) =>
            new Vector2Fix16(array[0], array[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3Fix16"/>.
        /// </summary>
        public static Vector3Fix16 ToVector3Fix16(this ReadOnlySpan<int> span) =>
            new Vector3Fix16(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3Fix16"/>.
        /// </summary>
        public static Vector3Fix16 ToVector3Fix16(this Span<int> span) =>
            new Vector3Fix16(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3Fix16"/>.
        /// </summary>
        public static Vector3Fix16 ToVector3Fix16(this int[] array) =>
            new Vector3Fix16(array[0], array[1], array[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4Fix16"/>.
        /// </summary>
        public static Vector4Fix16 ToVector4Fix16(this ReadOnlySpan<int> span) =>
            new Vector4Fix16(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4Fix16"/>.
        /// </summary>
        public static Vector4Fix16 ToVector4Fix16(this Span<int> span) =>
            new Vector4Fix16(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4Fix16"/>.
        /// </summary>
        public static Vector4Fix16 ToVector4Fix16(this int[] array) =>
            new Vector4Fix16(array[0], array[1], array[2], array[3]);
    }
}
