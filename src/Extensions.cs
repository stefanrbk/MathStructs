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

        #region ReadOnlySpan<double>

        /// <summary>
        /// Converts the top 6 values of <paramref name="span"/> into a <see cref="Matrix3x2D"/>.
        /// </summary>
        public static Matrix3x2D ToMatrix3x2D(this ReadOnlySpan<double> span) =>
            new Matrix3x2D(span[0], span[1],
                           span[2], span[3],
                           span[4], span[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this ReadOnlySpan<double> span) =>
            new Matrix3x3D(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this ReadOnlySpan<double> span) =>
            new Matrix4x4D(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this ReadOnlySpan<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this ReadOnlySpan<double> span) =>
            new Vector3D(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this ReadOnlySpan<double> span) =>
            new Vector4D(span[0], span[1], span[2], span[3]);

        #endregion ReadOnlySpan<double>

        #region ReadOnlySpan<float>

        /// <summary>
        /// Converts the top 6 values of <paramref name="span"/> into a <see cref="Matrix3x2"/>.
        /// </summary>
        public static Matrix3x2 ToMatrix3x2(this ReadOnlySpan<float> span) =>
            new Matrix3x2(span[0], span[1],
                          span[2], span[3],
                          span[4], span[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this ReadOnlySpan<float> span) =>
            new Matrix3x3(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);
        
        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4(this ReadOnlySpan<float> span) =>
            new Matrix4x4(span[0], span[1], span[2], span[3],
                          span[4], span[5], span[6], span[7],
                          span[8], span[9], span[10], span[11],
                          span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2 ToVector2(this ReadOnlySpan<float> span) =>
            new Vector2(span[0], span[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this ReadOnlySpan<float> span) =>
            new Vector3(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this ReadOnlySpan<float> span) =>
            new Vector4(span[0], span[1], span[2], span[3]);

        #endregion ReadOnlySpan<float>

        #region Span<double>

        /// <summary>
        /// Converts the top 6 values of <paramref name="span"/> into a <see cref="Matrix3x2D"/>.
        /// </summary>
        public static Matrix3x2D ToMatrix3x2D(this Span<double> span) =>
            new Matrix3x2D(span[0], span[1],
                           span[2], span[3],
                           span[4], span[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this Span<double> span) =>
            new Matrix3x3D(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this Span<double> span) =>
            new Matrix4x4D(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this Span<double> span) =>
            new Vector2D(span[0], span[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this Span<double> span) =>
            new Vector3D(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this Span<double> span) =>
            new Vector4D(span[0], span[1], span[2], span[3]);

        #endregion Span<double>

        #region Span<float>

        /// <summary>
        /// Converts the top 6 values of <paramref name="span"/> into a <see cref="Matrix3x2"/>.
        /// </summary>
        public static Matrix3x2 ToMatrix3x2(this Span<float> span) =>
            new Matrix3x2(span[0], span[1],
                          span[2], span[3],
                          span[4], span[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this Span<float> span) =>
            new Matrix3x3(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4(this Span<float> span) =>
            new Matrix4x4(span[0], span[1], span[2], span[3],
                          span[4], span[5], span[6], span[7],
                          span[8], span[9], span[10], span[11],
                          span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2"/>.
        /// </summary>
        public static Vector2 ToVector2(this Span<float> span) =>
            new Vector2(span[0], span[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this Span<float> span) =>
            new Vector3(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this Span<float> span) =>
            new Vector4(span[0], span[1], span[2], span[3]);

        #endregion Span<float>

        #region double[]

        /// <summary>
        /// Converts the top 6 values of <paramref name="array"/> into a <see cref="Matrix3x2D"/>.
        /// </summary>
        public static Matrix3x2D ToMatrix3x2D(this double[] array) =>
            new Matrix3x2D(array[0], array[1],
                           array[2], array[3],
                           array[4], array[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3D"/>.
        /// </summary>
        public static Matrix3x3D ToMatrix3x3D(this double[] array) =>
            new Matrix3x3D(array[0], array[1], array[2],
                           array[3], array[4], array[5],
                           array[6], array[7], array[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4D"/>.
        /// </summary>
        public static Matrix4x4D ToMatrix4x4D(this double[] array) =>
            new Matrix4x4D(array[0], array[1], array[2], array[3],
                           array[4], array[5], array[6], array[7],
                           array[8], array[9], array[10], array[11],
                           array[12], array[13], array[14], array[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2D"/>.
        /// </summary>
        public static Vector2D ToVector2D(this double[] array) =>
            new Vector2D(array[0], array[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3D"/>.
        /// </summary>
        public static Vector3D ToVector3D(this double[] array) =>
            new Vector3D(array[0], array[1], array[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4D"/>.
        /// </summary>
        public static Vector4D ToVector4D(this double[] array) =>
            new Vector4D(array[0], array[1], array[2], array[3]);

        #endregion double[]

        #region float[]

        /// <summary>
        /// Converts the top 6 values of <paramref name="array"/> into a <see cref="Matrix3x2"/>.
        /// </summary>
        public static Matrix3x2 ToMatrix3x2(this float[] array) =>
            new Matrix3x2(array[0], array[1],
                          array[2], array[3],
                          array[4], array[5]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3"/>.
        /// </summary>
        public static Matrix3x3 ToMatrix3x3(this float[] array) =>
            new Matrix3x3(array[0], array[1], array[2],
                           array[3], array[4], array[5],
                           array[6], array[7], array[8]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4"/>.
        /// </summary>
        public static Matrix4x4 ToMatrix4x4(this float[] array) =>
            new Matrix4x4(array[0], array[1], array[2], array[3],
                          array[4], array[5], array[6], array[7],
                          array[8], array[9], array[10], array[11],
                          array[12], array[13], array[14], array[15]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2 ToVector2(this float[] array) =>
            new Vector2(array[0], array[1]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3"/>.
        /// </summary>
        public static Vector3 ToVector3(this float[] array) =>
            new Vector3(array[0], array[1], array[2]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4"/>.
        /// </summary>
        public static Vector4 ToVector4(this float[] array) =>
            new Vector4(array[0], array[1], array[2], array[3]);

        #endregion float[]

        #region Plane

        /// <summary>
        /// </summary>
        public static void Deconstruct(this Plane plane, out Vector3 normal, out float d) =>
            (normal, d) = (plane.Normal, plane.D);

        /// <summary>
        /// </summary>
        public static void Deconstruct(this Plane plane, out float x, out float y, out float z, out float d) =>
            ((x, y, z), d) = (plane.Normal, plane.D);

        /// <summary>
        /// </summary>
        public static float Dot(this Plane plane, Vector4 value) =>
            Plane.Dot(plane, value);

        /// <summary>
        /// </summary>
        public static float DotCoordinate(this Plane plane, Vector3 value) =>
            Plane.DotCoordinate(plane, value);

        /// <summary>
        /// </summary>
        public static float DotNormal(this Plane plane, Vector3 value) =>
            Plane.DotNormal(plane, value);

        /// <summary>
        /// </summary>
        public static Plane Normalize(this Plane plane) =>
            Plane.Normalize(plane);

        /// <summary>
        /// </summary>
        public static Plane Transform(this Plane plane, Matrix4x4 matrix) =>
            Plane.Transform(plane, matrix);

        /// <summary>
        /// </summary>
        public static Plane Transform(this Plane plane, Quaternion rotation) =>
            Plane.Transform(plane, rotation);

        #endregion Plane

        #region Quaternion

        /// <summary>
        /// </summary>
        public static Quaternion Concatenate(this Quaternion quaternion, Quaternion value) =>
            Quaternion.Concatenate(quaternion, value);

        /// <summary>
        /// </summary>
        public static Quaternion Conjugate(this Quaternion quaternion) =>
            Quaternion.Conjugate(quaternion);

        /// <summary>
        /// </summary>
        public static void Deconstruct(this Quaternion quaternion, out float x, out float y, out float z, out float w) =>
            (x, y, z, w) = (quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);

        /// <summary>
        /// </summary>
        public static void Deconstruct(this Quaternion quaternion, out Vector3 vector, out float scalar) =>
            (vector, scalar) = (new(quaternion.X, quaternion.Y, quaternion.Z), quaternion.W);

        /// <summary>
        /// </summary>
        public static float Dot(this Quaternion quaternion, Quaternion value) =>
            Quaternion.Dot(quaternion, value);

        ///<summary>
        ///</summary>
        public static bool Equals(this Quaternion quaternion, Quaternion other, float delta) =>
            delta == 0.0 ? quaternion == other
                         : Math.Abs(quaternion.X - other.X) < delta &&
                           Math.Abs(quaternion.Y - other.Y) < delta &&
                           Math.Abs(quaternion.Z - other.Z) < delta &&
                           Math.Abs(quaternion.W - other.W) < delta;

        /// <summary>
        /// </summary>
        public static Quaternion Inverse(this Quaternion quaternion) =>
            Quaternion.Inverse(quaternion);

        /// <summary>
        /// </summary>
        public static Quaternion Normalize(this Quaternion quaternion) =>
            Quaternion.Normalize(quaternion);

        #endregion Quaternion

        #region Matrix4x4

        /// <summary>
        /// </summary>
        public static void CopyTo(this Matrix4x4 m, Span<float> span) =>
            CopyTo(m, span, 0);

        /// <summary>
        /// </summary>
        public static void CopyTo(this Matrix4x4 m, Span<float> span, int index)
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
        /// </summary>
        public static void Deconstruct(this Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            if (!Matrix4x4.Decompose(matrix, out scale, out rotation, out translation))
                (scale, rotation, translation) = (Vector3.Zero, Quaternion.Identity, Vector3.Zero);
        }

        /// <summary>
        /// </summary>
        public static Matrix4x4 Transform(this Matrix4x4 matrix, Quaternion rotation) =>
            Matrix4x4.Transform(matrix, rotation);



        /// <summary>
        /// </summary>
        public static Matrix4x4 Transpose(this Matrix4x4 matrix) =>
            Matrix4x4.Transpose(matrix);

        #endregion Matrix4x4

        #region Vector2

        /// <summary>
        /// Deconstructor for <see cref="Vector2"/> constructor.
        /// </summary>
        public static void Deconstruct(this Vector2 v, out float x, out float y) =>
            (x, y) = (v.X, v.Y);

        #endregion Vector2

        #region Vector3

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

        #endregion Vector3

        #region Vector4

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

        #endregion Vector4
    }
}
