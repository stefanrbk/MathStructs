﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public struct Matrix4x4D
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        private struct CanonicalBasis
        {
            public Vector3D Row0;
            public Vector3D Row1;
            public Vector3D Row2;
        }
        private struct VectorBasis
        {
            public unsafe Vector3D* Element0;
            public unsafe Vector3D* Element1;
            public unsafe Vector3D* Element2;
        }

        public double M11;
        public double M12;
        public double M13;
        public double M14;
        public double M21;
        public double M22;
        public double M23;
        public double M24;
        public double M31;
        public double M32;
        public double M33;
        public double M34;
        public double M41;
        public double M42;
        public double M43;
        public double M44;

        private static readonly Matrix4x4D _identity = new Matrix4x4D(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
        private static readonly Matrix4x4D _nan = new Matrix4x4D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

        public static Matrix4x4D Identity => _identity;

        public static Matrix4x4D NaN => _nan;

        public bool IsIdentity =>
            this == Identity;

        public Vector3D Translation
        {
            get => new Vector3D(M41, M42, M43);
            set => (M41, M42, M43) = (value.X, value.Y, value.Z);
        }

        [MethodImpl(Optimize)]
        public Matrix4x4D(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        [MethodImpl(Optimize)]
        public unsafe void Deconstruct(out Vector3D scale, out QuaternionD rotation, out Vector3D translation)
        {
            fixed (Vector3D* ptr = &scale)
            {
                var ptr2 = (double*)ptr;
                VectorBasis vectorBasis;
                var ptr3 = (Vector3D**)&vectorBasis;
                var identity = _identity;
                var canonicalBasis = default(CanonicalBasis);
                var ptr4 = &canonicalBasis.Row0;
                canonicalBasis.Row0 = Vector3D.UnitX;
                canonicalBasis.Row1 = Vector3D.UnitY;
                canonicalBasis.Row2 = Vector3D.UnitZ;
                translation = new Vector3D(M41, M42, M43);
                *ptr3 = (Vector3D*)&identity.M11;
                ptr3[1] = (Vector3D*)&identity.M21;
                ptr3[2] = (Vector3D*)&identity.M31;
                *(*ptr3) = new Vector3D(M11, M12, M13);
                *ptr3[1] = new Vector3D(M21, M22, M23);
                *ptr3[2] = new Vector3D(M31, M32, M33);
                scale.X = (*ptr3)->Length();
                scale.Y = ptr3[1]->Length();
                scale.Z = ptr3[2]->Length();
                var n1 = *ptr2;
                var n2 = ptr2[1];
                var n3 = ptr2[2];
                uint n4, n5, n6;
                if (n1 < n2)
                {
                    if (n2 < n3)
                    {
                        n4 = 2;
                        n5 = 1;
                        n6 = 0;
                    }
                    else
                    {
                        n4 = 1;
                        if (n1 < n3)
                        {
                            n5 = 2;
                            n6 = 0;
                        }
                        else
                        {
                            n5 = 0;
                            n6 = 2;
                        }
                    }
                }
                else if (n1 < n3)
                {
                    n4 = 2;
                    n5 = 0;
                    n6 = 1;
                }
                else
                {
                    n4 = 0;
                    if (n2 < n3)
                    {
                        n5 = 2;
                        n6 = 1;
                    }
                    else
                    {
                        n5 = 1;
                        n6 = 2;
                    }
                }
                if (ptr2[n4] < 0.0001f)
                    *ptr3[n4] = ptr4[n4];
                *ptr3[n4] = ptr3[n4]->Normalize();
                if (ptr2[n5] < 0.0001f)
                {
                    var n7 = Math.Abs(ptr3[n4]->X);
                    var n8 = Math.Abs(ptr3[n4]->Y);
                    var n9 = Math.Abs(ptr3[n4]->Z);
                    var n10 = (n7 < n8) ? ((!(n8 < n9)) ? ((!(n7 < n9)) ? 2u : 0u) : 0u) : ((n7 < n9) ? 1u : ((n8 < n9) ? 1u : 2u));
                    *ptr3[n5] = ptr3[n4]->Cross(ptr4[n10]);
                }
                *ptr3[n5] = ptr3[n5]->Normalize();
                if (ptr2[n6] < 0.0001f)
                    *ptr3[n6] = ptr3[n4]->Cross(*ptr3[n5]);
                *ptr3[n6] = ptr3[n6]->Normalize();
                var n11 = identity.GetDeterminant();
                if (n11 < 0f)
                {
                    ptr2[n4] = -ptr2[n4];
                    *ptr3[n4] = -*ptr3[n4];
                    n11 = -n11;
                }
                n11 -= 1f;
                n11 *= n11;
                if (0.0001f < n11)
                {
                    rotation = QuaternionD.Identity;
                    scale = translation = Vector3D.Zero;
                }
                else
                {
                    rotation = QuaternionD.CreateFromRotationMatrix(identity);
                }
            }
        }

        [MethodImpl(Optimize)]
        public Matrix4x4D With(double? m11 = null, double? m12 = null, double? m13 = null, double? m14 = null, double? m21 = null, double? m22 = null, double? m23 = null, double? m24 = null, double? m31 = null, double? m32 = null, double? m33 = null, double? m34 = null, double? m41 = null, double? m42 = null, double? m43 = null, double? m44 = null) =>
            new Matrix4x4D(
            m11 ?? M11,
            m12 ?? M12,
            m13 ?? M13,
            m14 ?? M14,
            m21 ?? M21,
            m22 ?? M22,
            m23 ?? M23,
            m24 ?? M24,
            m31 ?? M31,
            m32 ?? M32,
            m33 ?? M33,
            m34 ?? M34,
            m41 ?? M41,
            m42 ?? M42,
            m43 ?? M43,
            m44 ?? M44);

        public static Matrix4x4D CreateBillboard(Vector3D objectPosition, Vector3D cameraPosition, Vector3D cameraUpVector, Vector3D cameraForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = (!(num < 0.0001f)) ? left * (1f / Math.Sqrt(num)) : -cameraForwardVector;
            var v1 = cameraUpVector.Cross(left).Normalize();
            var v2 = left.Cross(v1);
            return new Matrix4x4D(v1.X, v1.Y, v1.Z, 0, v2.X, v2.Y, v2.Z, 0, left.X, left.Y, left.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1);
        }

        public static Matrix4x4D CreateConstrainedBillboard(Vector3D objectPosition, Vector3D cameraPosition, Vector3D rotateAxis, Vector3D cameraForwardVector, Vector3D objectForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = (!(num < 0.0001f)) ? left * (1f / Math.Sqrt(num)) : -cameraForwardVector;
            Vector3D v1 = rotateAxis, v2, v3;
            var x = rotateAxis.Dot(left);
            if (Math.Abs(x) > 0.998254657f)
            {
                v2 = objectForwardVector;
                x = rotateAxis.Dot(v2);
                if (Math.Abs(x) > 0.998254657f)
                    v2 = (Math.Abs(rotateAxis.Z) > 0.998254657f) ? Vector3D.UnitX : -Vector3D.UnitZ;
                v3 = rotateAxis.Cross(v2).Normalize();
                v2 = v3.Cross(rotateAxis).Normalize();
            }
            else
            {
                v3 = rotateAxis.Cross(left).Normalize();
                v2 = v3.Cross(v1).Normalize();
            }
            return new Matrix4x4D(v3.X, v3.Y, v3.Z, 0, v1.X, v1.Y, v1.Z, 0, v2.X, v2.Y, v2.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1f);
        }

        public static Matrix4x4D CreateTranslation(Vector3D position) =>
            CreateTranslation(position.X, position.Y, position.Z);

        public static Matrix4x4D CreateTranslation(double xPosition, double yPosition, double zPosition) =>
            _identity.With(m41: xPosition, m42: yPosition, m43: zPosition);

        public static Matrix4x4D CreateScale(double xScale, double yScale, double zScale) =>
            _identity.With(m11: xScale, m22: yScale, m33: zScale);

        public static Matrix4x4D CreateScale(double xScale, double yScale, double zScale, Vector3D centerPoint)
        {
            var m1 = centerPoint.X * (1 - xScale);
            var m2 = centerPoint.Y * (1 - yScale);
            var m3 = centerPoint.Z * (1 - zScale);
            return CreateScale(xScale, yScale, zScale).With(m41: m1, m42: m2, m43: m3);
        }

        public static Matrix4x4D CreateScale(Vector3D scale) =>
            CreateScale(scale.X, scale.Y, scale.Z);

        public static Matrix4x4D CreateScale(Vector3D scale, Vector3D centerPoint) =>
            CreateScale(scale.X, scale.Y, scale.Z, centerPoint);

        public static Matrix4x4D CreateScale(double scale) =>
            CreateScale(scale, scale, scale);

        public static Matrix4x4D CreateScale(double scale, Vector3D centerPoint) =>
            CreateScale(scale, scale, scale, centerPoint);

        public static Matrix4x4D CreateRotationX(double radians)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            return _identity.With(m22: n1, m23: n2, m32: -n2, m33: n1);
        }

        public static Matrix4x4D CreateRotationX(double radians, Vector3D centerPoint)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            var m1 = centerPoint.Y * (1 - n1) + centerPoint.Z * n2;
            var m2 = centerPoint.Z * (1 - n1) - centerPoint.Y * n2;
            return CreateRotationX(radians).With(m42: m1, m43: m2);
        }

        public static Matrix4x4D CreateRotationY(double radians)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            return _identity.With(m11: n1, m13: -n2, m31: n2, m33: n1);
        }

        public static Matrix4x4D CreateRotationY(double radians, Vector3D centerPoint)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            var m1 = centerPoint.X * (1 - n1) - centerPoint.Z * n2;
            var m2 = centerPoint.Z * (1 - n1) + centerPoint.X * n2;
            return CreateRotationY(radians).With(m41: m1, m43: m2);
        }

        public static Matrix4x4D CreateRotationZ(double radians)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            return _identity.With(m11: n1, m12: n2, m21: -n2, m22: n1);
        }

        public static Matrix4x4D CreateRotationZ(double radians, Vector3D centerPoint)
        {
            var n1 = Math.Cos(radians);
            var n2 = Math.Sin(radians);
            var m1 = centerPoint.X * (1 - n1) + centerPoint.Y * n2;
            var m2 = centerPoint.Y * (1 - n1) - centerPoint.X * n2;
            return CreateRotationZ(radians).With(m41: m1, m42: m2);
        }

        public static Matrix4x4D CreateFromAxisAngle(Vector3D axis, double angle)
        {
            (var x, var y, var z) = axis;
            var n1 = Math.Sin(angle);
            var n2 = Math.Cos(angle);
            var n3 = x * x;
            var n4 = y * y;
            var n5 = z * z;
            var n6 = x * y;
            var n7 = x * z;
            var n8 = y * z;
            return _identity.With(m11: n3 + n2 * (1 - n3),
                                  m12: n6 - n2 * n6 + n1 * z,
                                  m13: n7 - n2 * n7 - n1 * y,
                                  m21: n6 - n2 * n6 - n1 * z,
                                  m22: n4 + n2 * (1 - n4),
                                  m23: n8 - n2 * n8 + n1 * x,
                                  m31: n7 - n2 * n7 + n1 * y,
                                  m32: n8 - n2 * n8 - n1 * x,
                                  m33: n5 + n2 * (1 - n5));
        }

        public static Matrix4x4D CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio, double nearPlaneDistance, double farPlaneDistance)
        {
            if (fieldOfView is <= 0 or >= Math.PI)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView));
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = 1 / Math.Tan(fieldOfView * 0.5f);
            var n2 = n1 / aspectRatio;
            var n3 = double.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: n2,
                                  m22: n1,
                                  m33: n3,
                                  m34: -1,
                                  m43: nearPlaneDistance * n3,
                                  m44: 0);
        }

        public static Matrix4x4D CreatePerspective(double width, double height, double nearPlaneDistance, double farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = double.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: 2 * nearPlaneDistance / width,
                                  m22: 2 * nearPlaneDistance / height,
                                  m33: n1,
                                  m34: -1f,
                                  m43: nearPlaneDistance * n1,
                                  m44: 0);
        }

        public static Matrix4x4D CreatePerspectiveOffCenter(double left, double right, double bottom, double top, double nearPlaneDistance, double farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = double.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: 2 * nearPlaneDistance / (right - left),
                                  m22: 2 * nearPlaneDistance / (top - bottom),
                                  m31: (left + right) / (right - left),
                                  m32: (top + bottom) / (top - bottom),
                                  m33: n1,
                                  m34: -1f,
                                  m43: nearPlaneDistance * n1,
                                  m44: 0);
        }

        public static Matrix4x4D CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane) =>
            _identity.With(m11: 2f / width,
                           m22: 2f / height,
                           m33: 1 / (zNearPlane - zFarPlane),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        public static Matrix4x4D CreateOrthographicOffCenter(double left, double right, double bottom, double top, double zNearPlane, double zFarPlane) =>
            _identity.With(m11: 2f / (right - left),
                           m22: 2f / (top - bottom),
                           m33: 1 / (zNearPlane - zFarPlane),
                           m41: (left + right) / (left - right),
                           m42: (top + bottom) / (bottom - top),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        public static Matrix4x4D CreateLookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3D cameraUpVector)
        {
            var v1 = (cameraPosition - cameraTarget).Normalize();
            var v2 = cameraUpVector.Cross(v1).Normalize();
            var v3 = v1.Cross(v2);

            return _identity.With(m11: v2.X,
                                  m12: v3.X,
                                  m13: v1.X,
                                  m21: v2.Y,
                                  m22: v3.Y,
                                  m23: v1.Y,
                                  m31: v2.Z,
                                  m32: v3.Z,
                                  m33: v1.Z,
                                  m41: -v2.Dot(cameraPosition),
                                  m42: -v3.Dot(cameraPosition),
                                  m43: -v1.Dot(cameraPosition));
        }

        public static Matrix4x4D CreateWorld(Vector3D position, Vector3D forward, Vector3D up)
        {
            var v1 = (-forward).Normalize();
            var v2 = up.Cross(v1).Normalize();
            var v3 = v1.Cross(v2);

            return CreateTranslation(position).With(m11: v2.X,
                                                    m12: v2.Y,
                                                    m13: v2.Z,
                                                    m21: v3.X,
                                                    m22: v3.Y,
                                                    m23: v3.Z,
                                                    m31: v1.X,
                                                    m32: v1.Y,
                                                    m33: v1.Z);
        }

        public static Matrix4x4D CreateFromQuaternion(QuaternionD q)
        {
            var n1 = q.X * q.X;
            var n2 = q.Y * q.Y;
            var n3 = q.Z * q.Z;
            var n4 = q.X * q.Y;
            var n5 = q.Z * q.W;
            var n6 = q.Z * q.X;
            var n7 = q.Y * q.W;
            var n8 = q.Y * q.Z;
            var n9 = q.X * q.W;

            return _identity.With(m11: 1 - 2 * (n2 + n3),
                                  m12: 2 * (n4 + n5),
                                  m13: 2 * (n6 - n7),
                                  m21: 2 * (n4 - n5),
                                  m22: 1 - 2 * (n3 + n1),
                                  m23: 2 * (n8 + n9),
                                  m31: 2 * (n6 + n7),
                                  m32: 2 * (n8 - n9),
                                  m33: 1 - 2 * (n2 + n1));
        }

        public static Matrix4x4D CreateFromYawPitchRoll(double yaw, double pitch, double roll) =>
            CreateFromQuaternion(QuaternionD.CreateFromYawPitchRoll(yaw, pitch, roll));

        public static Matrix4x4D CreateShadow(Vector3D lightDirection, PlaneD plane)
        {
            plane = plane.Normalize();
            var n = plane.Normal.Dot(lightDirection);
            var v = -new Vector4D(plane.Normal, plane.D);
            return _identity.With(m11: v.X * lightDirection.X + n,
                                  m12: v.X * lightDirection.Y,
                                  m13: v.X * lightDirection.Z,
                                  m21: v.Y * lightDirection.X,
                                  m22: v.Y * lightDirection.Y + n,
                                  m23: v.Y * lightDirection.Z,
                                  m31: v.Z * lightDirection.X,
                                  m32: v.Z * lightDirection.Y,
                                  m33: v.Z * lightDirection.Z + n,
                                  m41: v.W * lightDirection.X,
                                  m42: v.W * lightDirection.Y,
                                  m43: v.W * lightDirection.Z,
                                  m44: n);
        }

        public static Matrix4x4D CreateReflection(PlaneD value)
        {
            value = value.Normalize();
            var v1 = value.Normal;
            var v2 = -2 * v1;
            return _identity.With(m11: v2.X * v1.X + 1,
                                  m12: v2.Y * v1.X,
                                  m13: v2.Z * v1.X,
                                  m21: v2.X * v1.Y,
                                  m22: v2.Y * v1.Y + 1,
                                  m23: v2.Z * v1.Y,
                                  m31: v2.X * v1.Z,
                                  m32: v2.Y * v1.Z,
                                  m33: v2.Z * v1.Z + 1,
                                  m41: v2.X * value.D,
                                  m42: v2.Y * value.D,
                                  m43: v2.Z * value.D);
        }

        [MethodImpl(Optimize)]
        public static bool Decompose(Matrix4x4D matrix, out Vector3D scales, out QuaternionD rotation, out Vector3D translation) 
        { 
            (scales, rotation, translation) = matrix;
            return scales != Vector3D.Zero || translation != Vector3D.Zero || rotation != QuaternionD.Identity;
        }

        public double GetDeterminant()
        {
            double m1 = M11;
            double m2 = M12;
            double m3 = M13;
            double m4 = M14;
            double m5 = M21;
            double m6 = M22;
            double m7 = M23;
            double m8 = M24;
            double m9 = M31;
            double m10 = M32;
            double m11 = M33;
            double m12 = M34;
            double m13 = M41;
            double m14 = M42;
            double m15 = M43;
            double m16 = M44;
            double n1 = m11 * m16 - m12 * m15;
            double n2 = m10 * m16 - m12 * m14;
            double n3 = m10 * m15 - m11 * m14;
            double n4 = m9 * m16 - m12 * m13;
            double n5 = m9 * m15 - m11 * m13;
            double n6 = m9 * m14 - m10 * m13;
            return m1 * (m6 * n1 - m7 * n2 + m8 * n3) - m2 * (m5 * n1 - m7 * n4 + m8 * n5) + m3 * (m5 * n2 - m6 * n4 + m8 * n6) - m4 * (m5 * n3 - m6 * n5 + m7 * n6);
        }

        [MethodImpl(Optimize)]
        public Matrix4x4D Invert() =>
            Invert(this);

        [MethodImpl(Optimize)]
        public static Matrix4x4D Invert(Matrix4x4D matrix)
        {
            //if (Avx.IsSupported)
            //{
            //    unsafe
            //    {
            //        var left = Avx.LoadVector256(&matrix.M11);                                      //                          < M11 M12 M13 M14 >
            //        var right = Avx.LoadVector256(&matrix.M21);                                     //                          < M21 M22 M23 M24 >
            //        var left2 = Avx.LoadVector256(&matrix.M31);                                     //                          < M31 M32 M33 M34 >
            //        var right2 = Avx.LoadVector256(&matrix.M41);                                    //                          < M41 M42 M43 M44 >
            //        var left3 = Avx.Shuffle(left, right, 68);                                       //  68 = < A1 A2 B1 B2 >    < M11 M12 M21 M22 >
            //        var left4 = Avx.Shuffle(left, right, 238);                                      // 238 = < A3 A4 B3 B4 >    < M13 M14 M23 M24 >
            //        var right3 = Avx.Shuffle(left2, right2, 68);                                    //  68 = < A1 A2 B1 B2 >    < M31 M32 M41 M42 >
            //        var right4 = Avx.Shuffle(left2, right2, 238);                                   // 238 = < A3 A4 B3 B4 >    < M33 M34 M43 M44 >
            //        left = Avx.Shuffle(left3, right3, 136);                                         // 136 = < A1 A3 B1 B3 >    < M11 M21 M31 M41 >
            //        right = Avx.Shuffle(left3, right3, 221);                                        // 221 = < A2 A4 B2 B4 >    < M12 M22 M32 M42 >
            //        left2 = Avx.Shuffle(left4, right4, 136);                                        // 136 = < A1 A3 B1 B3 >    < M13 M23 M33 M43 >
            //        right2 = Avx.Shuffle(left4, right4, 221);                                       // 221 = < A2 A4 B2 B4 >    < M14 M24 M34 M44 >
            //        var left5 = Avx.Permute(left2, 80);                                             //  80 = < A1 A1 A2 A2 >    < M13 M13 M23 M23 >
            //        var right5 = Avx.Permute(right2, 238);                                          // 238 = < A3 A4 A3 A4 >    < M34 M44 M34 M44 >
            //        var left6 = Avx.Permute(left, 80);                                              //  80 = < A1 A1 A2 A2 >    < M11 M11 M21 M21 >
            //        var right6 = Avx.Permute(right, 238);                                           // 238 = < A3 A4 A3 A4 >    < M32 M42 M34 M42 >
            //        var left7 = Avx.Shuffle(left2, left, 136);                                      // 136 = < A1 A3 B1 B3 >    < M13 M33 M11 M31 >
            //        var right7 = Avx.Shuffle(right2, right, 221);                                   // 221 = < A2 A4 B2 B4 >    < M24 M44 M22 M42 >
            //        var left8 = Avx.Multiply(left5, right5);                                        //                          < M13*M34 M13*M44 M23*M34 M23*M44 >
            //        var left9 = Avx.Multiply(left6, right6);                                        //                          < M11*M32 M11*M42 M21*M34 M21*M42 >
            //        var left10 = Avx.Multiply(left7, right7);                                       //                          < M13*M24 M33*M44 M11*M22 M31*M42 >
            //        left5 = Avx.Permute(left2, 238);                                                // 238 = < A3 A4 A3 A4 >    < M33 M43 M33 M43 >
            //        right5 = Avx.Permute(right2, 80);                                               //  80 = < A1 A1 A2 A2 >    < M14 M14 M24 M24 >
            //        left6 = Avx.Permute(left, 238);                                                 // 238 = < A3 A4 A3 A4 >    < M31 M41 M34 M41 >
            //        right6 = Avx.Permute(right, 80);                                                //  80 = < A1 A1 A2 A2 >    < M12 M12 M22 M22 >
            //        left7 = Avx.Shuffle(left2, left, 221);                                          // 221 = < A2 A4 B2 B4 >    < M23 M43 M21 M41 >
            //        right7 = Avx.Shuffle(right2, right, 136);                                       // 136 = < A1 A3 B1 B3 >    < M14 M34 M12 M34 >
            //        left8 = Avx.Subtract(left8, Avx.Multiply(left5, right5));                       //                          < M13*M34-M33*M14 M13*M44-M43*M14 M23*M34-M33*M24 M23*M44-M43*M24 >
            //        left9 = Avx.Subtract(left9, Avx.Multiply(left6, right6));                       //                          < M11*M32-M31*M12 M11*M42-M41*M12 M21*M34-M34*M22 M21*M42-M41*M22 >
            //        left10 = Avx.Subtract(left10, Avx.Multiply(left7, right7));                     //                          < M13*M24-M23*M14 M33*M44-M43*M34 M11*M22-M21*M12 M31*M42-M41*M34 >
            //        right6 = Avx.Shuffle(left8, left10, 93);                                        //  93 = < A2 A4 B2 B2 >    < M13*M44-M43*M14 M23*M44-M43*M24 M33*M44-M43*M34 M33*M44-M43*M34 >
            //        left5 = Avx.Permute(right, 73);                                                 //  73 = < A2 A3 A1 A2 >    < M22 M32 M12 M22 >
            //        right5 = Avx.Shuffle(right6, left8, 50);                                        //  50 = < A3 A1 B4 B1 >    < M33*M44-M43*M34 M13*M44-M43*M14 M23*M44-M43*M24 M13*M34-M33*M14 >
            //        left6 = Avx.Permute(left, 18);                                                  //  18 = < A3 A1 A2 A1 >    < M31 M11 M21 M11 >
            //        right6 = Avx.Shuffle(right6, left8, 153);                                       // 153 = < A2 A3 B2 B3 >    < M23*M44-M43*M24 M33*M44-M43*M34 M13*M44-M43*M14 M23*M34-M33*M24 >
            //        var left11 = Avx.Shuffle(left9, left10, 253);                                   // 253 = < A2 A4 B4 B4 >    < M11*M42-M41*M12 M21*M42-M41*M22 M31*M42-M41*M34 M31*M42-M41*M34 >
            //        left7 = Avx.Permute(right2, 73);                                                //  73 = < A2 A3 A1 A2 >    < M24 M34 M14 M24 >
            //        right7 = Avx.Shuffle(left11, left9, 50);                                        //  50 = < A3 A1 B4 B1 >    < M31*M42-M41*M34 M11*M42-M41*M12 M21*M42-M41*M22 M21*M42-M41*M22 >
            //        var left12 = Avx.Permute(left2, 18);                                            //  18 = < A3 A1 A2 A1 >    < M33 M13 M23 M13 >
            //        left11 = Avx.Shuffle(left11, left9, 153);                                       // 153 = < A2 A3 B2 B3 >    < M21*M42-M41*M22 M31*M42-M41*M34 M11*M42-M41*M12 M21*M34-M34*M22 >
            //        var left13 = Avx.Multiply(left5, right5);                                       //                          < M22*(M33*M44-M43*M34) M32*(M13*M44-M43*M14) M12*(M23*M44-M43*M24) M22*(M13*M34-M33*M14) >
            //        var left14 = Avx.Multiply(left6, right6);                                       //                          < M31*(M23*M44-M43*M24) M11*(M33*M44-M43*M34) M21*(M13*M44-M43*M14) M11*(M23*M34-M33*M24) >
            //        var left15 = Avx.Multiply(left7, right7);                                       //                          < M24*(M31*M42-M41*M34) M34*(M11*M42-M41*M12) M14*(M21*M42-M41*M22) M24*(M21*M42-M41*M22) >
            //        var left16 = Avx.Multiply(left12, left11);                                      //                          < M33*(M21*M42-M41*M22) M13*(M31*M42-M41*M34) M23*(M11*M42-M41*M12) M13*(M21*M34-M34*M22) >
            //        right6 = Avx.Shuffle(left8, left10, 4);                                         //   4 = < A1 A2 B1 B1 >    < M13*M34-M33*M14 M13*M44-M43*M14 M13*M24-M23*M14 M13*M24-M23*M14 >
            //        left5 = Avx.Permute(right, 158);                                                // 158 = < A3 A4 A2 A3 >    < M32 M42 M22 M32 >
            //        right5 = Avx.Shuffle(left8, right6, 147);                                       // 147 = < A4 A1 B2 B3 >    < M23*M44-M43*M24 M13*M34-M33*M14 M13*M44-M43*M14 M13*M24-M23*M14 >
            //        left6 = Avx.Permute(left, 123);                                                 // 123 = < A4 A3 A4 A2 >    < M41 M31 M41 M21 >
            //        right6 = Avx.Shuffle(left8, right6, 38);                                        //  38 = < A3 A2 B3 B1 >    < M23*M34-M33*M24 M13*M44-M43*M14 M13*M24-M23*M14 M13*M34-M33*M14 >
            //        left11 = Avx.Shuffle(left9, left10, 164);                                       // 164 = < A1 A2 B3 B3 >    < M11*M32-M31*M12 M11*M42-M41*M12 M11*M22-M21*M12 M11*M22-M21*M12 >
            //        left7 = Avx.Permute(right2, 158);                                               // 158 = < A3 A4 A2 A3 >    < M34 M44 M24 M34 >
            //        right7 = Avx.Shuffle(left9, left11, 147);                                       // 147 = < A4 A1 B2 B3 >    < M21*M42-M41*M22 M11*M32-M31*M12 M11*M42-M41*M12 M11*M22-M21*M12 >
            //        left12 = Avx.Permute(left2, 123);                                               // 123 = < A4 A3 A4 A2 >    < M43 M33 M43 M23 >
            //        left11 = Avx.Shuffle(left9, left11, 38);                                        //  38 = < A3 A2 B3 B1 >    < M21*M34-M34*M22 M11*M42-M41*M12 M11*M22-M21*M12 M11*M32-M31*M12 >
            //        left13 = Avx.Subtract(left13, Avx.Multiply(left5, right5));                     //                          < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14) >
            //        left14 = Avx.Subtract(left14, Avx.Multiply(left6, right6));                     //                          < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14) >
            //        left15 = Avx.Subtract(left15, Avx.Multiply(left7, right7));                     //                          < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12) >
            //        left16 = Avx.Subtract(left16, Avx.Multiply(left12, left11));                    //                          < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12) >
            //        left5 = Avx.Permute(right, 51);                                                 //  51 = < A4 A1 A4 A1 >    < M42 M12 M42 M12 >
            //        right5 = Avx.Shuffle(left8, left10, 74);                                        //  74 = < A3 A3 B1 B2 >    < M23*M34-M33*M24 M23*M34-M33*M24 M13*M24-M23*M14 M33*M44-M43*M34 >
            //        right5 = Avx.Permute(right5, 44);                                               //  44 = < A1 A4 A3 A1 >    < M23*M34-M33*M24 M33*M44-M43*M34 M13*M24-M23*M14 M23*M34-M33*M24 >
            //        left6 = Avx.Permute(left, 141);                                                 // 141 = < A2 A4 A1 A3 >    < M21 M41 M11 M31 >
            //        right6 = Avx.Shuffle(left8, left10, 76);                                        //  76 = < A1 A4 B1 B2 >    < M13*M34-M33*M14 M23*M44-M43*M24 M13*M24-M23*M14 M33*M44-M43*M34 >
            //        right6 = Avx.Permute(right6, 147);                                              // 147 = < A4 A1 A2 A3 >    < M33*M44-M43*M34 M13*M34-M33*M14 M23*M44-M43*M24 M13*M24-M23*M14 >
            //        left7 = Avx.Permute(right2, 51);                                                //  51 = < A4 A1 A4 A1 >    < M44 M14 M44 M14 >
            //        right7 = Avx.Shuffle(left9, left10, 234);                                       // 234 = < A3 A3 B3 B4 >    < M21*M34-M34*M22 M21*M34-M34*M22 M11*M22-M21*M12 M31*M42-M41*M34 >
            //        right7 = Avx.Permute(right7, 44);                                               //  44 = < A1 A4 A3 A1 >    < M21*M34-M34*M22 M31*M42-M41*M34 M11*M22-M21*M12 M21*M34-M34*M22 >
            //        left12 = Avx.Permute(left2, 141);                                               // 141 = < A2 A4 A1 A3 >    < M23 M43 M13 M33 >
            //        left11 = Avx.Shuffle(left9, left10, 236);                                       // 236 = < A1 A4 B3 B4 >    < M11*M32-M31*M12 M21*M42-M41*M22 M11*M22-M21*M12 M31*M42-M41*M34 >
            //        left11 = Avx.Permute(left11, 147);                                              // 147 = < A4 A1 A2 A3 >    < M31*M42-M41*M34 M11*M32-M31*M12 M21*M42-M41*M22 M11*M22-M21*M12 >
            //        left5 = Avx.Multiply(left5, right5);                                            //                          < M42*(M23*M34-M33*M24) M12*(M33*M44-M43*M34) M42*(M13*M24-M23*M14) M12*(M23*M34-M33*M24) >
            //        left6 = Avx.Multiply(left6, right6);                                            //                          < M21*(M33*M44-M43*M34) M41*(M13*M34-M33*M14) M11*(M23*M44-M43*M24) M31*(M13*M24-M23*M14) >
            //        left7 = Avx.Multiply(left7, right7);                                            //                          < M44*(M21*M34-M34*M22) M14*(M31*M42-M41*M34) M44*(M11*M22-M21*M12) M14*(M21*M34-M34*M22) >
            //        left12 = Avx.Multiply(left12, left11);                                          //                          < M23*(M31*M42-M41*M34) M43*(M11*M32-M31*M12) M13*(M21*M42-M41*M22) M33*(M11*M22-M21*M12) >
            //        var right8 = Avx.Subtract(left13, left5);                                       //                          < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)-M42*(M23*M34-M33*M24) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)-M12*(M33*M44-M43*M34) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)-M42*(M13*M24-M23*M14) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)-M12*(M23*M34-M33*M24) >
            //        left13 = Avx.Add(left13, left5);                                                //                          < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)+M42*(M23*M34-M33*M24) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)+M12*(M33*M44-M43*M34) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)+M42*(M13*M24-M23*M14) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)+M12*(M23*M34-M33*M24) >
            //        var right9 = Avx.Add(left14, left6);                                            //                          < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24)-M21*(M33*M44-M43*M34) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14)-M41*(M13*M34-M33*M14) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14)-M11*(M23*M44-M43*M24) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14)-M31*(M13*M24-M23*M14) >
            //        left14 = Avx.Subtract(left14, left6);                                           //                          < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24)+M21*(M33*M44-M43*M34) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14)+M41*(M13*M34-M33*M14) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14)+M11*(M23*M44-M43*M24) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14)+M31*(M13*M24-M23*M14) >
            //        var right10 = Avx.Subtract(left15, left7);                                      //                          < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22)-M44*(M21*M34-M34*M22) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12)-M14*(M31*M42-M41*M34) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12)-M44*(M11*M22-M21*M12) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12)-M14*(M21*M34-M34*M22) >
            //        left15 = Avx.Add(left15, left7);                                                //                          < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22)+M44*(M21*M34-M34*M22) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12)+M14*(M31*M42-M41*M34) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12)+M44*(M11*M22-M21*M12) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12)+M14*(M21*M34-M34*M22) >
            //        var right11 = Avx.Add(left16, left12);                                          //                          < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22)+M23*(M31*M42-M41*M34) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12)+M43*(M11*M32-M31*M12) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12)+M13*(M21*M42-M41*M22) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12)+M33*(M11*M22-M21*M12) >
            //        left16 = Avx.Subtract(left16, left12);                                          //                          < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22)-M23*(M31*M42-M41*M34) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12)-M43*(M11*M32-M31*M12) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12)-M13*(M21*M42-M41*M22) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12)-M33*(M11*M22-M21*M12) >
            //        left13 = Avx.Shuffle(left13, right8, 216);                                      // 216 = < A1 A3 B2 B4 >    < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)+M42*(M23*M34-M33*M24) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)+M42*(M13*M24-M23*M14) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)-M12*(M33*M44-M43*M34) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)-M12*(M23*M34-M33*M24) >
            //        left14 = Avx.Shuffle(left14, right9, 216);                                      // 216 = < A1 A3 B2 B4 >    < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24)+M21*(M33*M44-M43*M34) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14)+M11*(M23*M44-M43*M24) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14)-M41*(M13*M34-M33*M14) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14)-M31*(M13*M24-M23*M14) >
            //        left15 = Avx.Shuffle(left15, right10, 216);                                     // 216 = < A1 A3 B2 B4 >    < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22)+M44*(M21*M34-M34*M22) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12)+M44*(M11*M22-M21*M12) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12)-M14*(M31*M42-M41*M34) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12)-M14*(M21*M34-M34*M22) >
            //        left16 = Avx.Shuffle(left16, right11, 216);                                     // 216 = < A1 A3 B2 B4 >    < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22)-M23*(M31*M42-M41*M34) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12)-M13*(M21*M42-M41*M22) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12)+M43*(M11*M32-M31*M12) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12)+M33*(M11*M22-M21*M12) >
            //        left13 = Avx.Permute(left13, 216);                                              // 216 = < A1 A3 A2 A4 >    < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)+M42*(M23*M34-M33*M24) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)-M12*(M33*M44-M43*M34) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)+M42*(M13*M24-M23*M14) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)-M12*(M23*M34-M33*M24) >
            //        left14 = Avx.Permute(left14, 216);                                              // 216 = < A1 A3 A2 A4 >    < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24)+M21*(M33*M44-M43*M34) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14)-M41*(M13*M34-M33*M14) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14)+M11*(M23*M44-M43*M24) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14)-M31*(M13*M24-M23*M14) >
            //        left15 = Avx.Permute(left15, 216);                                              // 216 = < A1 A3 A2 A4 >    < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22)+M44*(M21*M34-M34*M22) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12)-M14*(M31*M42-M41*M34) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12)+M44*(M11*M22-M21*M12) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12)-M14*(M21*M34-M34*M22) >
            //        left16 = Avx.Permute(left16, 216);                                              // 216 = < A1 A3 A2 A4 >    < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22)-M23*(M31*M42-M41*M34) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12)+M43*(M11*M32-M31*M12) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12)-M13*(M21*M42-M41*M22) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12)+M33*(M11*M22-M21*M12) >
            //        right3 = left;                                                                  //                          < M11 M21 M31 M41 >
            //        double num25 = Vector4D.Dot(left13.AsVector4D(), right3.AsVector4D());          //                          M11*(M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)+M42*(M23*M34-M33*M24))+M21*(M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)-M12*(M33*M44-M43*M34))+M31*(M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)+M42*(M13*M24-M23*M14))+M41*(M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)-M12*(M23*M34-M33*M24))
            //        if (Math.Abs(num25) < double.Epsilon)                                           // 
            //            return _nan;                                                                // 
            //        var left17 = Vector256.Create(1d);                                              //                          <   1   1   1   1 >
            //        var right12 = Vector256.Create(num25);                                          //                          < N25 N25 N25 N25 >
            //        right12 = Avx.Divide(left17, right12);                                          //                          < 1/N25 1/N25 1/N25 1/N25 >
            //        left = Avx.Multiply(left13, right12);                                           //                          < M22*(M33*M44-M43*M34)-M32*(M23*M44-M43*M24)+M42*(M23*M34-M33*M24)*(1/N25) M32*(M13*M44-M43*M14)-M42*(M13*M34-M33*M14)-M12*(M33*M44-M43*M34)*(1/N25) M12*(M23*M44-M43*M24)-M22*(M13*M44-M43*M14)+M42*(M13*M24-M23*M14)*(1/N25) M22*(M13*M34-M33*M14)-M32*(M13*M24-M23*M14)-M12*(M23*M34-M33*M24)*(1/N25) >
            //        right = Avx.Multiply(left14, right12);                                          //                          < M31*(M23*M44-M43*M24)-M41*(M23*M34-M33*M24)+M21*(M33*M44-M43*M34)*(1/N25) M11*(M33*M44-M43*M34)-M31*(M13*M44-M43*M14)-M41*(M13*M34-M33*M14)*(1/N25) M21*(M13*M44-M43*M14)-M41*(M13*M24-M23*M14)+M11*(M23*M44-M43*M24)*(1/N25) M11*(M23*M34-M33*M24)-M21*(M13*M34-M33*M14)-M31*(M13*M24-M23*M14)*(1/N25) >
            //        left2 = Avx.Multiply(left15, right12);                                          //                          < M24*(M31*M42-M41*M34)-M34*(M21*M42-M41*M22)+M44*(M21*M34-M34*M22)*(1/N25) M34*(M11*M42-M41*M12)-M44*(M11*M32-M31*M12)-M14*(M31*M42-M41*M34)*(1/N25) M14*(M21*M42-M41*M22)-M24*(M11*M42-M41*M12)+M44*(M11*M22-M21*M12)*(1/N25) M24*(M21*M42-M41*M22)-M34*(M11*M22-M21*M12)-M14*(M21*M34-M34*M22)*(1/N25) >
            //        right2 = Avx.Multiply(left16, right12);                                         //                          < M33*(M21*M42-M41*M22)-M43*(M21*M34-M34*M22)-M23*(M31*M42-M41*M34)*(1/N25) M13*(M31*M42-M41*M34)-M33*(M11*M42-M41*M12)+M43*(M11*M32-M31*M12)*(1/N25) M23*(M11*M42-M41*M12)-M43*(M11*M22-M21*M12)-M13*(M21*M42-M41*M22)*(1/N25) M13*(M21*M34-M34*M22)-M23*(M11*M32-M31*M12)+M33*(M11*M22-M21*M12)*(1/N25) >
            //        Unsafe.SkipInit<Matrix4x4D>(out var result);
            //        ref var reference = ref Unsafe.As<Matrix4x4D, Vector256<double>>(ref result);
            //        reference = left;
            //        Unsafe.Add(ref reference, 1) = right;
            //        Unsafe.Add(ref reference, 2) = left2;
            //        Unsafe.Add(ref reference, 3) = right2;
            //        return result;
            //    }
            //}
            var m1 = matrix.M11;
            var m2 = matrix.M12;
            var m3 = matrix.M13;
            var m4 = matrix.M14;
            var m5 = matrix.M21;
            var m6 = matrix.M22;
            var m7 = matrix.M23;
            var m8 = matrix.M24;
            var m9 = matrix.M31;
            var m10 = matrix.M32;
            var m11 = matrix.M33;
            var m12 = matrix.M34;
            var m13 = matrix.M41;
            var m14 = matrix.M42;
            var m15 = matrix.M43;
            var m16 = matrix.M44;
            double n1 = m11 * m16 - m12 * m15;
            double n2 = m10 * m16 - m12 * m14;
            double n3 = m10 * m15 - m11 * m14;
            double n4 = m9 * m16 - m12 * m13;
            double n5 = m9 * m15 - m11 * m13;
            double n6 = m9 * m14 - m10 * m13;
            double n7 = m6 * n1 - m7 * n2 + m8 * n3;
            double n8 = -(m5 * n1 - m7 * n4 + m8 * n5);
            double n9 = m5 * n2 - m6 * n4 + m8 * n6;
            double n10 = -(m5 * n3 - m6 * n5 + m7 * n6);
            double n11 = m1 * n7 + m2 * n8 + m3 * n9 + m4 * n10;
            if (Math.Abs(n11) < double.Epsilon)
                return _nan;
            double n12 = 1f / n11;
            double n13 = m7 * m16 - m8 * m15;
            double n14 = m6 * m16 - m8 * m14;
            double n15 = m6 * m15 - m7 * m14;
            double n16 = m5 * m16 - m8 * m13;
            double n17 = m5 * m15 - m7 * m13;
            double n18 = m5 * m14 - m6 * m13;
            double n19 = m7 * m12 - m8 * m11;
            double n20 = m6 * m12 - m8 * m10;
            double n21 = m6 * m11 - m7 * m10;
            double n22 = m5 * m12 - m8 * m9;
            double n23 = m5 * m11 - m7 * m9;
            double n24 = m5 * m10 - m6 * m9;
            return new Matrix4x4D(m11: n7 * n12,
                                  m21: n8 * n12,
                                  m31: n9 * n12,
                                  m41: n10 * n12,
                                  m12: (-(m2 * n1 - m3 * n2 + m4 * n3)) * n12,
                                  m22: (m1 * n1 - m3 * n4 + m4 * n5) * n12,
                                  m32: (-(m1 * n2 - m2 * n4 + m4 * n6)) * n12,
                                  m42: (m1 * n3 - m2 * n5 + m3 * n6) * n12,
                                  m13: (m2 * n13 - m3 * n14 + m4 * n15) * n12,
                                  m23: (-(m1 * n13 - m3 * n16 + m4 * n17)) * n12,
                                  m33: (m1 * n14 - m2 * n16 + m4 * n18) * n12,
                                  m43: (-(m1 * n15 - m2 * n17 + m3 * n18)) * n12,
                                  m14: (-(m2 * n19 - m3 * n20 + m4 * n21)) * n12,
                                  m24: (m1 * n19 - m3 * n22 + m4 * n23) * n12,
                                  m34: (-(m1 * n20 - m2 * n22 + m4 * n24)) * n12,
                                  m44: (m1 * n21 - m2 * n23 + m3 * n24) * n12);


        }

        [MethodImpl(Optimize)]
        public Matrix4x4D Transform(QuaternionD rotation)
        {
            double n1 = rotation.X + rotation.X;
            double n2 = rotation.Y + rotation.Y;
            double n3 = rotation.Z + rotation.Z;
            double n4 = rotation.W * n1;
            double n5 = rotation.W * n2;
            double n6 = rotation.W * n3;
            double n7 = rotation.X * n1;
            double n8 = rotation.X * n2;
            double n9 = rotation.X * n3;
            double n10 = rotation.Y * n2;
            double n11 = rotation.Y * n3;
            double n12 = rotation.Z * n3;
            double n13 = 1f - n10 - n12;
            double n14 = n8 - n6;
            double n15 = n9 + n5;
            double n16 = n8 + n6;
            double n17 = 1f - n7 - n12;
            double n18 = n11 - n4;
            double n19 = n9 - n5;
            double n20 = n11 + n4;
            double n21 = 1f - n7 - n10;
            return new Matrix4x4D(M11 * n13 + M12 * n14 + M13 * n15,
                                  M11 * n16 + M12 * n17 + M13 * n18,
                                  M11 * n19 + M12 * n20 + M13 * n21,
                                  M14,
                                  M21 * n13 + M22 * n14 + M23 * n15,
                                  M21 * n16 + M22 * n17 + M23 * n18,
                                  M21 * n19 + M22 * n20 + M23 * n21,
                                  M24,
                                  M31 * n13 + M32 * n14 + M33 * n15,
                                  M31 * n16 + M32 * n17 + M33 * n18,
                                  M31 * n19 + M32 * n20 + M33 * n21,
                                  M34,
                                  M41 * n13 + M42 * n14 + M43 * n15,
                                  M41 * n16 + M42 * n17 + M43 * n18,
                                  M41 * n19 + M42 * n20 + M43 * n21,
                                  M44);
        }

        [MethodImpl(Optimize)]
        public static Matrix4x4D Transform(Matrix4x4D value, QuaternionD rotation) =>
            value.Transform(rotation);

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D Transpose(Matrix4x4D matrix)
        {
            //if (Avx.IsSupported)
            //{
            //    var left = Avx.LoadVector256(&matrix.M11);
            //    var right = Avx.LoadVector256(&matrix.M21);
            //    var left2 = Avx.LoadVector256(&matrix.M31);
            //    var right2 = Avx.LoadVector256(&matrix.M41);
            //    var left3 = Avx.Shuffle(left, right, 68);
            //    var left4 = Avx.Shuffle(left, right, 238);
            //    var right3 = Avx.Shuffle(left2, right2, 68);
            //    var right4 = Avx.Shuffle(left2, right2, 238);
            //    Avx.Store(&matrix.M11, Avx.Shuffle(left3, right3, 136));
            //    Avx.Store(&matrix.M21, Avx.Shuffle(left3, right3, 221));
            //    Avx.Store(&matrix.M31, Avx.Shuffle(left4, right4, 136));
            //    Avx.Store(&matrix.M41, Avx.Shuffle(left4, right4, 221));
            //    return matrix;
            //}
            return new Matrix4x4D(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                                  matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                                  matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                                  matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        [MethodImpl(Optimize)]
        public Matrix4x4D Transpose() =>
            Transpose(this);

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D Lerp(Matrix4x4D matrix1, Matrix4x4D matrix2, double amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            if (Avx.IsSupported)
            {
                var t = Vector256.Create(amount);
                Avx.Store(&m1.M11, VectorMath.Lerp(Avx.LoadVector256(&m1.M11), Avx.LoadVector256(&m2.M11), t));
                Avx.Store(&m1.M21, VectorMath.Lerp(Avx.LoadVector256(&m1.M21), Avx.LoadVector256(&m2.M21), t));
                Avx.Store(&m1.M31, VectorMath.Lerp(Avx.LoadVector256(&m1.M31), Avx.LoadVector256(&m2.M31), t));
                Avx.Store(&m1.M41, VectorMath.Lerp(Avx.LoadVector256(&m1.M41), Avx.LoadVector256(&m2.M41), t));
                return m1;
            }
            return new Matrix4x4D(m1.M11 + (m2.M11 - m1.M11) * amount,
                                  m1.M12 + (m2.M12 - m1.M12) * amount,
                                  m1.M13 + (m2.M13 - m1.M13) * amount,
                                  m1.M14 + (m2.M14 - m1.M14) * amount,
                                  m1.M21 + (m2.M21 - m1.M21) * amount,
                                  m1.M22 + (m2.M22 - m1.M22) * amount,
                                  m1.M23 + (m2.M23 - m1.M23) * amount,
                                  m1.M24 + (m2.M24 - m1.M24) * amount,
                                  m1.M31 + (m2.M31 - m1.M31) * amount,
                                  m1.M32 + (m2.M32 - m1.M32) * amount,
                                  m1.M33 + (m2.M33 - m1.M33) * amount,
                                  m1.M34 + (m2.M34 - m1.M34) * amount,
                                  m1.M41 + (m2.M41 - m1.M41) * amount,
                                  m1.M42 + (m2.M42 - m1.M42) * amount,
                                  m1.M43 + (m2.M43 - m1.M43) * amount,
                                  m1.M44 + (m2.M44 - m1.M44) * amount);
        }

        [MethodImpl(Optimize)]
        public static Matrix4x4D Negate(Matrix4x4D value) =>
            -value;

        [MethodImpl(Optimize)]
        public static Matrix4x4D Add(Matrix4x4D left, Matrix4x4D right) =>
            left + right;

        [MethodImpl(Optimize)]
        public static Matrix4x4D Subtract(Matrix4x4D left, Matrix4x4D right) =>
            left - right;

        [MethodImpl(Optimize)]
        public static Matrix4x4D Multiply(Matrix4x4D left, Matrix4x4D right) =>
            left * right;

        [MethodImpl(Optimize)]
        public static Matrix4x4D Multiply(Matrix4x4D left, double right) =>
            left * right;

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator -(Matrix4x4D value)
        {
            if (Avx.IsSupported)
            {
                var zero = Vector256<double>.Zero;
                Avx.Store(&value.M11, Avx.Subtract(zero, Avx.LoadVector256(&value.M11)));
                Avx.Store(&value.M21, Avx.Subtract(zero, Avx.LoadVector256(&value.M21)));
                Avx.Store(&value.M31, Avx.Subtract(zero, Avx.LoadVector256(&value.M31)));
                Avx.Store(&value.M41, Avx.Subtract(zero, Avx.LoadVector256(&value.M41)));
                return value;
            }
            return new Matrix4x4D(-value.M11, -value.M12, -value.M13, -value.M14,
                                  -value.M21, -value.M22, -value.M23, -value.M24,
                                  -value.M31, -value.M32, -value.M33, -value.M34,
                                  -value.M41, -value.M42, -value.M43, -value.M44);
        }

        [MethodImpl(Optimize)]
        public static Matrix4x4D operator +(Matrix4x4D value) =>
            value;

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator +(Matrix4x4D left, Matrix4x4D right)
        {
            if (Avx.IsSupported)
            {
                Avx.Store(&left.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
                Avx.Store(&left.M21, Avx.Add(Avx.LoadVector256(&left.M21), Avx.LoadVector256(&right.M21)));
                Avx.Store(&left.M31, Avx.Add(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));
                Avx.Store(&left.M41, Avx.Add(Avx.LoadVector256(&left.M41), Avx.LoadVector256(&right.M41)));

                return left;
            }
            return new Matrix4x4D(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
                                  left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24,
                                  left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34,
                                  left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);
        }

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator -(Matrix4x4D left, Matrix4x4D right)
        {
            if (Avx.IsSupported)
            {
                Avx.Store(&left.M11, Avx.Subtract(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
                Avx.Store(&left.M21, Avx.Subtract(Avx.LoadVector256(&left.M21), Avx.LoadVector256(&right.M21)));
                Avx.Store(&left.M31, Avx.Subtract(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));
                Avx.Store(&left.M41, Avx.Subtract(Avx.LoadVector256(&left.M41), Avx.LoadVector256(&right.M41)));

                return left;
            }
            return new Matrix4x4D(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
                                  left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24,
                                  left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34,
                                  left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);
        }

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator *(Matrix4x4D value1, Matrix4x4D value2)
        {
            //if (Avx.IsSupported)
            //{
            //    Avx.Store(&value1.M11, MultiplyRow(value2, Avx.LoadVector256(&value1.M11)));
            //    Avx.Store(&value1.M21, MultiplyRow(value2, Avx.LoadVector256(&value1.M21)));
            //    Avx.Store(&value1.M31, MultiplyRow(value2, Avx.LoadVector256(&value1.M31)));
            //    Avx.Store(&value1.M41, MultiplyRow(value2, Avx.LoadVector256(&value1.M41)));
            //    return value1;
            //}
            Matrix4x4D result;
            result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
            result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
            result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
            result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
            result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
            result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
            result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
            result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
            result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
            result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
            result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
            result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
            result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
            result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
            result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
            result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
            return result;
        }
        [MethodImpl(Optimize)]
        private static unsafe Vector256<double> MultiplyRow(Matrix4x4D value2, Vector256<double> vector)
        {
            if (Avx2.IsSupported)
                return Avx.Add(Avx.Add(Avx.Multiply(Avx2.Permute4x64(vector, 0),
                                                    Avx.LoadVector256(&value2.M11)),
                                       Avx.Multiply(Avx2.Permute4x64(vector, 85),
                                                    Avx.LoadVector256(&value2.M21))),
                               Avx.Add(Avx.Multiply(Avx2.Permute4x64(vector, 170),
                                                    Avx.LoadVector256(&value2.M31)),
                                       Avx.Multiply(Avx2.Permute4x64(vector, byte.MaxValue),
                                                    Avx.LoadVector256(&value2.M41))));
            else
                return Avx.Add(Avx.Add(Avx.Multiply(Select(vector, 0, 0),
                                                    Avx.LoadVector256(&value2.M11)),
                                       Avx.Multiply(Select(vector, 3, 0),
                                                    Avx.LoadVector256(&value2.M21))),
                               Avx.Add(Avx.Multiply(Select(vector, 0, 5),
                                                    Avx.LoadVector256(&value2.M31)),
                                       Avx.Multiply(Select(vector, 12, 5),
                                                    Avx.LoadVector256(&value2.M41))));
            static Vector256<double> Select(Vector256<double> vector, byte control1, byte control2)
            {
                var temp = Avx.Permute(vector, control1);
                return Avx.Permute2x128(temp, temp, control2);
            }
        }

        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator *(Matrix4x4D value1, double value2)
        {
            //if (Avx.IsSupported)
            //{
            //    Vector256<double> right = Vector256.Create(value2);
            //    Avx.Store(&value1.M11, Avx.Multiply(Avx.LoadVector256(&value1.M11), right));
            //    Avx.Store(&value1.M21, Avx.Multiply(Avx.LoadVector256(&value1.M21), right));
            //    Avx.Store(&value1.M31, Avx.Multiply(Avx.LoadVector256(&value1.M31), right));
            //    Avx.Store(&value1.M41, Avx.Multiply(Avx.LoadVector256(&value1.M41), right));
            //    return value1;
            //}
            Matrix4x4D result;
            result.M11 = value1.M11 * value2;
            result.M12 = value1.M12 * value2;
            result.M13 = value1.M13 * value2;
            result.M14 = value1.M14 * value2;
            result.M21 = value1.M21 * value2;
            result.M22 = value1.M22 * value2;
            result.M23 = value1.M23 * value2;
            result.M24 = value1.M24 * value2;
            result.M31 = value1.M31 * value2;
            result.M32 = value1.M32 * value2;
            result.M33 = value1.M33 * value2;
            result.M34 = value1.M34 * value2;
            result.M41 = value1.M41 * value2;
            result.M42 = value1.M42 * value2;
            result.M43 = value1.M43 * value2;
            result.M44 = value1.M44 * value2;
            return result;
        }

        [MethodImpl(Optimize)]
        public unsafe static bool operator ==(Matrix4x4D value1, Matrix4x4D value2)
        {
            if (Avx.IsSupported)
            {
                if (VectorMath.Equal(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) && VectorMath.Equal(Avx.LoadVector256(&value1.M21), Avx.LoadVector256(&value2.M21)) && VectorMath.Equal(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31)))
                    return VectorMath.Equal(Avx.LoadVector256(&value1.M41), Avx.LoadVector256(&value2.M41));
                return false;
            }
            if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
                return value1.M43 == value2.M43;
            return false;
        }

        [MethodImpl(Optimize)]
        public unsafe static bool operator !=(Matrix4x4D value1, Matrix4x4D value2)
        {
            if (Avx.IsSupported)
            {
                if (!VectorMath.NotEqual(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) && !VectorMath.NotEqual(Avx.LoadVector256(&value1.M21), Avx.LoadVector256(&value2.M21)) && !VectorMath.NotEqual(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31)))
                    return VectorMath.NotEqual(Avx.LoadVector256(&value1.M41), Avx.LoadVector256(&value2.M41));
                return true;
            }
            if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43)
                return value1.M44 != value2.M44;
            return true;
        }

        [MethodImpl(Optimize)]
        public readonly bool Equals(Matrix4x4D other)
        {
            return this == other;
        }

        [MethodImpl(Optimize)]
        public override readonly bool Equals(object? obj)
        {
            if (obj is Matrix4x4D value)
            {
                return this == value;
            }
            return false;
        }

        public override readonly string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(M11);
            hash.Add(M12);
            hash.Add(M13);
            hash.Add(M14);
            hash.Add(M21);
            hash.Add(M22);
            hash.Add(M23);
            hash.Add(M24);
            hash.Add(M31);
            hash.Add(M32);
            hash.Add(M33);
            hash.Add(M34);
            hash.Add(M41);
            hash.Add(M42);
            hash.Add(M43);
            hash.Add(M44);
            return hash.ToHashCode();
        }

        internal Matrix3x3D As3x3() =>
            new Matrix3x3D(M11, M12, M13,
                           M21, M22, M23,
                           M31, M32, M33);

        private static class VectorMath
        {
        [MethodImpl(Optimize)]
            public static Vector256<double> Lerp(Vector256<double> a, Vector256<double> b, Vector256<double> t) =>
                Avx.Add(a, Avx.Multiply(Avx.Subtract(b, a), t));

        [MethodImpl(Optimize)]
            public static bool Equal(Vector256<double> a, Vector256<double> b) =>
                Avx.MoveMask(Avx.CompareNotEqual(a, b)) == 0;

        [MethodImpl(Optimize)]
            public static bool NotEqual(Vector256<double> a, Vector256<double> b) =>
                Avx.MoveMask(Avx.CompareNotEqual(a, b)) != 0;
        }
    }
}