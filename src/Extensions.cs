using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public static class Extensions
    {
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
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3F"/>.
        /// </summary>
        public static Matrix3x3F ToMatrix3x3F(this ReadOnlySpan<float> span) =>
            new Matrix3x3F(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="span"/> into a <see cref="Matrix3x3F"/>.
        /// </summary>
        public static Matrix3x3F ToMatrix3x3F(this Span<float> span) =>
            new Matrix3x3F(span[0], span[1], span[2],
                           span[3], span[4], span[5],
                           span[6], span[7], span[8]);

        /// <summary>
        /// Converts the top 9 values of <paramref name="array"/> into a <see cref="Matrix3x3F"/>.
        /// </summary>
        public static Matrix3x3F ToMatrix3x3F(this float[] array) =>
            new Matrix3x3F(array[0], array[1], array[2],
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
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4F"/>.
        /// </summary>
        public static Matrix4x4F ToMatrix4x4F(this ReadOnlySpan<float> span) =>
            new Matrix4x4F(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="span"/> into a <see cref="Matrix4x4F"/>.
        /// </summary>
        public static Matrix4x4F ToMatrix4x4F(this Span<float> span) =>
            new Matrix4x4F(span[0], span[1], span[2], span[3],
                           span[4], span[5], span[6], span[7],
                           span[8], span[9], span[10], span[11],
                           span[12], span[13], span[14], span[15]);

        /// <summary>
        /// Converts the top 16 values of <paramref name="array"/> into a <see cref="Matrix4x4F"/>.
        /// </summary>
        public static Matrix4x4F ToMatrix4x4F(this float[] array) =>
            new Matrix4x4F(array[0], array[1], array[2], array[3],
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
        public static Vector2F ToVector2F(this ReadOnlySpan<float> span) =>
            new Vector2F(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="span"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2F ToVector2F(this Span<float> span) =>
            new Vector2F(span[0], span[1]);

        /// <summary>
        /// Converts the top 2 values of <paramref name="array"/> into a <see cref="Vector2F"/>.
        /// </summary>
        public static Vector2F ToVector2F(this float[] array) =>
            new Vector2F(array[0], array[1]);

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
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3F"/>.
        /// </summary>
        public static Vector3F ToVector3F(this ReadOnlySpan<float> span) =>
            new Vector3F(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="span"/> into a <see cref="Vector3F"/>.
        /// </summary>
        public static Vector3F ToVector3F(this Span<float> span) =>
            new Vector3F(span[0], span[1], span[2]);

        /// <summary>
        /// Converts the top 3 values of <paramref name="array"/> into a <see cref="Vector3F"/>.
        /// </summary>
        public static Vector3F ToVector3F(this float[] array) =>
            new Vector3F(array[0], array[1], array[2]);

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
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4F"/>.
        /// </summary>
        public static Vector4F ToVector4F(this ReadOnlySpan<float> span) =>
            new Vector4F(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="span"/> into a <see cref="Vector4F"/>.
        /// </summary>
        public static Vector4F ToVector4F(this Span<float> span) =>
            new Vector4F(span[0], span[1], span[2], span[3]);

        /// <summary>
        /// Converts the top 4 values of <paramref name="array"/> into a <see cref="Vector4F"/>.
        /// </summary>
        public static Vector4F ToVector4F(this float[] array) =>
            new Vector4F(array[0], array[1], array[2], array[3]);

        public static void Write(this BeBinaryWriter writer, S15Fixed16 value) =>
            writer.Write(Unsafe.As<S15Fixed16, uint>(ref value));

        public static S15Fixed16 ReadS15Fixed16(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, S15Fixed16>(ref value);
        }

        public static void Write(this BinaryWriter writer, S15Fixed16 value) =>
            writer.Write(Unsafe.As<S15Fixed16, uint>(ref value));

        public static S15Fixed16 ReadS15Fixed16(this BinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, S15Fixed16>(ref value);
        }

        public static void Write(this BeBinaryWriter writer, U16Fixed16 value) =>
            writer.Write(Unsafe.As<U16Fixed16, uint>(ref value));

        public static U16Fixed16 ReadU16Fixed16(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, U16Fixed16>(ref value);
        }

        public static void Write(this BinaryWriter writer, U16Fixed16 value) =>
            writer.Write(Unsafe.As<U16Fixed16, uint>(ref value));

        public static U16Fixed16 ReadU16Fixed16(this BinaryReader reader)
        {
            var value = reader.ReadUInt32();
            return Unsafe.As<uint, U16Fixed16>(ref value);
        }

        public static void Write(this BeBinaryWriter writer, U8Fixed8 value) =>
            writer.Write(Unsafe.As<U8Fixed8, ushort>(ref value));

        public static U8Fixed8 ReadU8Fixed8(this BeBinaryReader reader)
        {
            var value = reader.ReadUInt16();
            return Unsafe.As<ushort, U8Fixed8>(ref value);
        }

        public static void Write(this BinaryWriter writer, U8Fixed8 value) =>
            writer.Write(Unsafe.As<U8Fixed8, ushort>(ref value));

        public static U8Fixed8 ReadU8Fixed8(this BinaryReader reader)
        {
            var value = reader.ReadUInt16();
            return Unsafe.As<ushort, U8Fixed8>(ref value);
        }
    }
}
