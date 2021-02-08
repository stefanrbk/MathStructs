using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Numerics
{
    /// <summary>
    /// Contains extensions to span and array types to help with conversions to matrix and vector types.
    /// </summary>
    public static class Extensions
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this ReadOnlySpan<double> span) =>
            new Matrix3x3D(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this Span<double> span) =>
            new Matrix3x3D(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this double[] array) =>
            new Matrix3x3D(array[0], array[1], array[2],
                           array[3], array[4], array[5],
                           array[6], array[7], array[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this ReadOnlySpan<float> span) =>
            new Matrix3x3(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this Span<float> span) =>
            new Matrix3x3(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this float[] array) =>
            new Matrix3x3(array[0], array[1], array[2],
                           array[3], array[4], array[5],
                           array[6], array[7], array[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this ReadOnlySpan<double> span) =>
            new Matrix4x4D(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this Span<double> span) =>
            new Matrix4x4D(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this double[] array) =>
            new Matrix4x4D(array[0], array[1], array[2], array[3],
                           array[4], array[5], array[6], array[7],
                           array[8], array[9], array[10], array[11],
                           array[12], array[13], array[14], array[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4F(this ReadOnlySpan<float> span) =>
            new Matrix4x4(span[0], span[1], span[2], span[3],
                          span[4], span[5], span[6], span[7],
                          span[8], span[9], span[10], span[11],
                          span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4F(this Span<float> span) =>
            new Matrix4x4(span[0], span[1], span[2], span[3],
                          span[4], span[5], span[6], span[7],
                          span[8], span[9], span[10], span[11],
                          span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4(this float[] array) =>
            new Matrix4x4(array[0], array[1], array[2], array[3],
                          array[4], array[5], array[6], array[7],
                          array[8], array[9], array[10], array[11],
                          array[12], array[13], array[14], array[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this ReadOnlySpan<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this Span<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this double[] array) =>
            new Vector2D(array[0], array[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2 ToVector2(this ReadOnlySpan<float> span) =>
            new Vector2(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2 ToVector2(this Span<float> span) =>
            new Vector2(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2 ToVector2F(this float[] array) =>
            new Vector2(array[0], array[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this ReadOnlySpan<double> span) =>
            new Vector3D(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this Span<double> span) =>
            new Vector3D(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this double[] array) =>
            new Vector3D(array[0], array[1], array[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this ReadOnlySpan<float> span) =>
            new Vector3(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this Span<float> span) =>
            new Vector3(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this float[] array) =>
            new Vector3(array[0], array[1], array[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this ReadOnlySpan<double> span) =>
            new Vector4D(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this Span<double> span) =>
            new Vector4D(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this double[] array) =>
            new Vector4D(array[0], array[1], array[2], array[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this ReadOnlySpan<float> span) =>
            new Vector4(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this Span<float> span) =>
            new Vector4(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this float[] array) =>
            new Vector4(array[0], array[1], array[2], array[3]); 
        public static void CopyTo(this Matrix4x4 m, Span<float> span, int index = 0)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 16)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index +  0] = m.M11;
            span[index +  1] = m.M12;
            span[index +  2] = m.M13;
            span[index +  3] = m.M14;
            span[index +  4] = m.M21;
            span[index +  5] = m.M22;
            span[index +  6] = m.M23;
            span[index +  7] = m.M24;
            span[index +  8] = m.M31;
            span[index +  9] = m.M32;
            span[index + 10] = m.M33;
            span[index + 11] = m.M34;
            span[index + 12] = m.M41;
            span[index + 13] = m.M42;
            span[index + 14] = m.M43;
            span[index + 15] = m.M44;
        }

        /// <summary>
        /// Deconstructor for <see cref="Vector2"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector2 v, out float x, out float y) =>
            (x, y) = (v.X, v.Y);

        /// <summary>
        /// Deconstructor for 3 parameter <see cref="Vector3"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector3 v, out float x, out float y, out float z) =>
            (x, y, z) = (v.X, v.Y, v.Z);

        /// <summary>
        /// Deconstructor for 2 parameter <see cref="Vector3"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector3 v, out Vector2 xy, out float z) =>
            (xy, z) = (new Vector2(v.X, v.Y), v.Z);

        /// <summary>
        /// Deconstructor for 4 parameter <see cref="Vector4"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector4 v, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (v.X, v.Y, v.Z, v.W);

        /// <summary>
        /// Deconstructor for 3 parameter <see cref="Vector4"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector4 v, out Vector2 xy, out float z, out float w) =>
            (xy, z, w) = (new Vector2(v.X, v.Y), v.Z, v.W);

        /// <summary>
        /// Deconstructor for 2 parameter <see cref="Vector4"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector4 v, out Vector3 xyz, out float w) =>
            (xyz, w) = (new Vector3(v.X, v.Y, v.Z), v.W);

        public static IEnumerable<T> With<T>(this IEnumerable<T> source, IEnumerable<T?> values) where T : class
        {
            var enumerator = values.GetEnumerator();

            foreach (var s in source)
            {
                _=enumerator.MoveNext();
                yield return enumerator.Current ?? s;
            }
        }

        public static IEnumerable<T> With<T>(this IEnumerable<T> source, IEnumerable<Nullable<T>> values) where T : struct
        {
            var enumerator = values.GetEnumerator();

            foreach (var s in source)
            {
                _=enumerator.MoveNext();
                yield return enumerator.Current ?? s;
            }
        }
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4 With(this Matrix4x4 m, float? m11 = null, float? m12 = null, float? m13 = null, float? m14 = null, float? m21 = null, float? m22 = null, float? m23 = null, float? m24 = null, float? m31 = null, float? m32 = null, float? m33 = null, float? m34 = null, float? m41 = null, float? m42 = null, float? m43 = null, float? m44 = null)
        {
            var array = new float[9];
            void* ptr = &m;
            Marshal.Copy((IntPtr)ptr, array, 0, 9);

            return array.With(new[] { m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44 }).ToArray().ToMatrix4x4();
        }
    }
}
