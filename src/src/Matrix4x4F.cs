using System;
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
    public struct Matrix4x4F
    {
        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        private struct CanonicalBasis
        {
            public Vector3F Row0;
            public Vector3F Row1;
            public Vector3F Row2;
        }
        private struct VectorBasis
        {
            public unsafe Vector3F* Element0;
            public unsafe Vector3F* Element1;
            public unsafe Vector3F* Element2;
        }
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;

        private static readonly Matrix4x4F _identity = new Matrix4x4F(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
        public static Matrix4x4F Identity => _identity;

        public bool IsIdentity =>
            this == Identity;

        public Vector3F Translation
        {
            get => new Vector3F(M41, M42, M43);
            set => (M41, M42, M43) = (value.X, value.Y, value.Z);
        }

        public Matrix4x4F(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
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

        public unsafe void Deconstruct(out Vector3F scale, out QuaternionF rotation, out Vector3F translation)
        {
            fixed (Vector3F* ptr = &scale)
            {
                var ptr2 = (float*)ptr;
                VectorBasis vectorBasis;
                var ptr3 = (Vector3F**)&vectorBasis;
                var identity = _identity;
                var canonicalBasis = default(CanonicalBasis);
                var ptr4 = &canonicalBasis.Row0;
                canonicalBasis.Row0 = Vector3F.UnitX;
                canonicalBasis.Row1 = Vector3F.UnitY;
                canonicalBasis.Row2 = Vector3F.UnitZ;
                translation = new Vector3F(M41, M42, M43);
                *ptr3 = (Vector3F*)&identity.M11;
                ptr3[1] = (Vector3F*)&identity.M21;
                ptr3[2] = (Vector3F*)&identity.M31;
                *(*ptr3) = new Vector3F(M11, M12, M13);
                *ptr3[1] = new Vector3F(M21, M22, M23);
                *ptr3[2] = new Vector3F(M31, M32, M33);
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
                    var n7 = MathF.Abs(ptr3[n4]->X);
                    var n8 = MathF.Abs(ptr3[n4]->Y);
                    var n9 = MathF.Abs(ptr3[n4]->Z);
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
                    rotation = QuaternionF.Identity;
                    scale = translation = Vector3F.Zero;
                }
                else
                {
                    rotation = QuaternionF.CreateFromRotationMatrix(identity);
                }
            }
        }

        public Matrix4x4F With(float? m11 = null, float? m12 = null, float? m13 = null, float? m14 = null, float? m21 = null, float? m22 = null, float? m23 = null, float? m24 = null, float? m31 = null, float? m32 = null, float? m33 = null, float? m34 = null, float? m41 = null, float? m42 = null, float? m43 = null, float? m44 = null) =>
            new Matrix4x4F(
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

        public static Matrix4x4F CreateBillboard(Vector3F objectPosition, Vector3F cameraPosition, Vector3F cameraUpVector, Vector3F cameraForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = (!(num < 0.0001f)) ? left * (1f / MathF.Sqrt(num)) : -cameraForwardVector;
            var v1 = cameraUpVector.Cross(left).Normalize();
            var v2 = left.Cross(v1);
            return new Matrix4x4F(v1.X, v1.Y, v1.Z, 0, v2.X, v2.Y, v2.Z, 0, left.X, left.Y, left.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1);
        }

        public static Matrix4x4F CreateConstrainedBillboard(Vector3F objectPosition, Vector3F cameraPosition, Vector3F rotateAxis, Vector3F cameraForwardVector, Vector3F objectForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = (!(num < 0.0001f)) ? left * (1f / MathF.Sqrt(num)) : -cameraForwardVector;
            Vector3F v1 = rotateAxis, v2, v3;
            var x = rotateAxis.Dot(left);
            if (MathF.Abs(x) > 0.998254657f)
            {
                v2 = objectForwardVector;
                x = rotateAxis.Dot(v2);
                if (MathF.Abs(x) > 0.998254657f)
                    v2 = (MathF.Abs(rotateAxis.Z) > 0.998254657f) ? Vector3F.UnitX : -Vector3F.UnitZ;
                v3 = rotateAxis.Cross(v2).Normalize();
                v2 = v3.Cross(rotateAxis).Normalize();
            }
            else
            {
                v3 = rotateAxis.Cross(left).Normalize();
                v2 = v3.Cross(v1).Normalize();
            }
            return new Matrix4x4F(v3.X, v3.Y, v3.Z, 0, v1.X, v1.Y, v1.Z, 0, v2.X, v2.Y, v2.Z, 0, objectPosition.X, objectPosition.Y, objectPosition.Z, 1f);
        }

        public static Matrix4x4F CreateTranslation(Vector3F position) =>
            CreateTranslation(position.X, position.Y, position.Z);

        public static Matrix4x4F CreateTranslation(float xPosition, float yPosition, float zPosition) =>
            _identity.With(m41: xPosition, m42: yPosition, m43: zPosition);

        public static Matrix4x4F CreateScale(float xScale, float yScale, float zScale) =>
            _identity.With(m11: xScale, m22: yScale, m33: zScale);

        public static Matrix4x4F CreateScale(float xScale, float yScale, float zScale, Vector3F centerPoint)
        {
            var m1 = centerPoint.X * (1f - xScale);
            var m2 = centerPoint.Y * (1f - yScale);
            var m3 = centerPoint.Z * (1f - zScale);
            return CreateScale(xScale, yScale, zScale).With(m41: m1, m42: m2, m43: m3);
        }

        public static Matrix4x4F CreateScale(Vector3F scale) =>
            CreateScale(scale.X, scale.Y, scale.Z);

        public static Matrix4x4F CreateScale(Vector3F scale, Vector3F centerPoint) =>
            CreateScale(scale.X, scale.Y, scale.Z, centerPoint);

        public static Matrix4x4F CreateScale(float scale) =>
            CreateScale(scale, scale, scale);

        public static Matrix4x4F CreateScale(float scale, Vector3F centerPoint) =>
            CreateScale(scale, scale, scale, centerPoint);

        public static Matrix4x4F CreateRotationX(float radians)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            return _identity.With(m22:n1, m23: n2, m32: -n2, m33: n1);
        }

        public static Matrix4x4F CreateRotationX(float radians, Vector3F centerPoint)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            var m1 = centerPoint.Y * (1 - n1) + centerPoint.Z * n2;
            var m2 = centerPoint.Z * (1 - n1) + centerPoint.Y * n2;
            return CreateRotationX(radians).With(m42: m1, m43: m2);
        }

        public static Matrix4x4F CreateRotationY(float radians)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            return _identity.With(m11: n1, m13: -n2, m31: n2, m33: n1);
        }

        public static Matrix4x4F CreateRotationY(float radians, Vector3F centerPoint)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            var m1 = centerPoint.X * (1 - n1) + centerPoint.Z * n2;
            var m2 = centerPoint.Z * (1 - n1) + centerPoint.X * n2;
            return CreateRotationY(radians).With(m41: m1, m43: m2);
        }

        public static Matrix4x4F CreateRotationZ(float radians)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            return _identity.With(m11: n1, m12: n2, m21: -n2, m22: n1);
        }

        public static Matrix4x4F CreateRotationZ(float radians, Vector3F centerPoint)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);
            var m1 = centerPoint.X * (1 - n1) + centerPoint.Y * n2;
            var m2 = centerPoint.Y * (1 - n1) + centerPoint.X * n2;
            return CreateRotationX(radians).With(m41: m1, m42: m2);
        }

        public static Matrix4x4F CreateFromAxisAngle(Vector3F axis, float angle)
        {
            (var x, var y, var z) = axis;
            var n1 = MathF.Sin(angle);
            var n2 = MathF.Cos(angle);
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
                                  m32: n8 - n2 * n8 - n1 * y,
                                  m33: n5 + n2 * (1 - n5));
        }

        public static Matrix4x4F CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
        {
            if (fieldOfView is <= 0 or >= MathF.PI)
                throw new ArgumentOutOfRangeException(nameof(fieldOfView));
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = 1 / MathF.Tan(fieldOfView * 0.5f);
            var n2 = n1 / aspectRatio;
            var n3 = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: n2,
                                  m22: n1,
                                  m33: n3,
                                  m34: -1,
                                  m43: nearPlaneDistance * n3,
                                  m44: 0);
        }

        public static Matrix4x4F CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: 2 * nearPlaneDistance / width,
                                  m22: 2 * nearPlaneDistance / height,
                                  m33: n1,
                                  m34: -1f,
                                  m43: nearPlaneDistance * n1,
                                  m44: 0);
        }

        public static Matrix4x4F CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var n1 = float.IsPositiveInfinity(farPlaneDistance) ? -1f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: 2 * nearPlaneDistance / (right - left),
                                  m22: 2 * nearPlaneDistance / (top - bottom),
                                  m31: (left + right) / (right - left),
                                  m32: (top + bottom) / (top - bottom),
                                  m33: n1,
                                  m34: -1f,
                                  m43: nearPlaneDistance * n1,
                                  m44: 0);
        }

        public static Matrix4x4F CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane) =>
            _identity.With(m11: 2f / width,
                           m22: 2f / height,
                           m33: 1 / (zNearPlane - zFarPlane),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        public static Matrix4x4F CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane) =>
            _identity.With(m11: 2f / (right - left),
                           m22: 2f / (top - bottom),
                           m33: 1 / (zNearPlane - zFarPlane),
                           m41: (left + right) / (left - right),
                           m42: (top + bottom) / (bottom - top),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        public static Matrix4x4F CreateLookAt(Vector3F cameraPosition, Vector3F cameraTarget, Vector3F cameraUpVector)
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

        public static Matrix4x4F CreateWorld(Vector3F position, Vector3F forward, Vector3F up)
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

        public static Matrix4x4F CreateFromQuaternion(QuaternionF q)
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

        public static Matrix4x4F CreateFromYawPitchRoll(float yaw, float pitch, float roll) =>
            CreateFromQuaternion(QuaternionF.CreateFromYawPitchRoll(yaw, pitch, roll));

        public static Matrix4x4F CreateShadow(Vector3F lightDirection, PlaneF plane)
        {
            plane = plane.Normalize();
            var n = plane.Normal.Dot(lightDirection);
            var v = -new Vector4F(plane.Normal, plane.D);
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

        public static Matrix4x4F CreateReflection(PlaneF value)
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

        public static void Decompose(Matrix4x4F matrix, out Vector3F scale, out QuaternionF rotation, out Vector3F translation) =>
            (scale, rotation, translation) = matrix;

        public float GetDeterminant()
        {
            float m1 = M11;
            float m2 = M12;
            float m3 = M13;
            float m4 = M14;
            float m5 = M21;
            float m6 = M22;
            float m7 = M23;
            float m8 = M24;
            float m9 = M31;
            float m10 = M32;
            float m11 = M33;
            float m12 = M34;
            float m13 = M41;
            float m14 = M42;
            float m15 = M43;
            float m16 = M44;
            float n1 = m11 * m16 - m12 * m15;
            float n2 = m10 * m16 - m12 * m14;
            float n3 = m10 * m15 - m11 * m14;
            float n4 = m9 * m16 - m12 * m13;
            float n5 = m9 * m15 - m11 * m13;
            float n6 = m9 * m14 - m10 * m13;
            return m1 * (m6 * n1 - m7 * n2 + m8 * n3) - m2 * (m5 * n1 - m7 * n4 + m8 * n5) + m3 * (m5 * n2 - m6 * n4 + m8 * n6) - m4 * (m5 * n3 - m6 * n5 + m7 * n6);
        }

        [MethodImpl(Optimize)]
        private static Vector128<float> Permute(Vector128<float> value, byte control) =>
            Avx.IsSupported ? Avx.Permute(value, control) : Sse.Shuffle(value, value, control);

        [MethodImpl(Optimize)]
        public Matrix4x4F Invert() =>
            Invert(this);

        [MethodImpl(Optimize)]
        public static Matrix4x4F Invert(Matrix4x4F matrix)
        {
            if (Sse.IsSupported)
            {
                unsafe
                {
                    var l1 = Sse.LoadVector128(&matrix.M11);
                    var r1 = Sse.LoadVector128(&matrix.M21);
                    var l2 = Sse.LoadVector128(&matrix.M31);
                    var r2 = Sse.LoadVector128(&matrix.M41);
                    var l3 = Sse.Shuffle(l1, r1, 68);
                    var l4 = Sse.Shuffle(l1, r1, 238);
                    var r3 = Sse.Shuffle(l2, r2, 68);
                    var r4 = Sse.Shuffle(l2, r2, 238);
                    l1 = Sse.Shuffle(l3, r3, 136);
                    r1 = Sse.Shuffle(l3, r3, 221);
                    l2 = Sse.Shuffle(l4, r4, 136);
                    r2 = Sse.Shuffle(l4, r4, 221);
                    var l5 = Permute(l2, 80);
                    var r5 = Permute(r2, 238);
                    var l6 = Permute(l1, 80);
                    var r6 = Permute(r1, 238);
                    var l7 = Sse.Shuffle(l2, l1, 136);
                    var r7 = Sse.Shuffle(r2, r1, 221);
                    var l8 = Sse.Multiply(l5, r5);
                    var l9 = Sse.Multiply(l6, r6);
                    var l10 = Sse.Multiply(l7, r7);
                    l5 = Permute(l2, 238);
                    r5 = Permute(r2, 80);
                    l6 = Permute(l1, 238);
                    r6 = Permute(r1, 80);
                    l7 = Sse.Shuffle(l2, l1, 221);
                    r7 = Sse.Shuffle(r2, r1, 136);
                    l8 = Sse.Subtract(l8, Sse.Multiply(l5, r5));
                    l9 = Sse.Subtract(l9, Sse.Multiply(l6, r6));
                    l10 = Sse.Subtract(l10, Sse.Multiply(l7, r7));
                    r6 = Sse.Shuffle(l8, l10, 93);
                    l5 = Permute(r1, 73);
                    r5 = Sse.Shuffle(r6, l8, 50);
                    l6 = Permute(l1, 18);
                    r6 = Sse.Shuffle(r6, l8, 153);
                    var l11 = Sse.Shuffle(l9, l10, 253);
                    l7 = Permute(r2, 73);
                    r7 = Sse.Shuffle(l11, l9, 50);
                    var l12 = Permute(l2, 18);
                    l11 = Sse.Shuffle(l11, l9, 153);
                    var l13 = Sse.Multiply(l5, r5);
                    var l14 = Sse.Multiply(l6, r6);
                    var l15 = Sse.Multiply(l7, r7);
                    var l16 = Sse.Multiply(l12, l11);
                    r6 = Sse.Shuffle(l8, l10, 4);
                    l5 = Permute(r1, 158);
                    r5 = Sse.Shuffle(l8, r6, 147);
                    l6 = Permute(l1, 123);
                    r6 = Sse.Shuffle(l8, r6, 38);
                    l11 = Sse.Shuffle(l9, l10, 164);
                    l7 = Permute(r2, 158);
                    r7 = Sse.Shuffle(l9, l11, 147);
                    l12 = Permute(l2, 123);
                    l11 = Sse.Shuffle(l9, l11, 38);
                    l13 = Sse.Subtract(l13, Sse.Multiply(l5, r5));
                    l14 = Sse.Subtract(l14, Sse.Multiply(l6, r6));
                    l15 = Sse.Subtract(l15, Sse.Multiply(l7, r7));
                    l16 = Sse.Subtract(l16, Sse.Multiply(l12, l11));
                    l5 = Permute(r1, 51);
                    r5 = Sse.Shuffle(l8, l10, 74);
                    r5 = Permute(r5, 44);
                    l6 = Permute(l1, 141);
                    r6 = Sse.Shuffle(l8, l10, 76);
                    r6 = Permute(r6, 147);
                    l7 = Permute(r2, 51);
                    r7 = Sse.Shuffle(l9, l10, 234);
                    r7 = Permute(r7, 44);
                    l12 = Permute(l2, 141);
                    l11 = Sse.Shuffle(l9, l10, 236);
                    l11 = Permute(l11, 147);
                    l5 = Sse.Multiply(l5, r5);
                    l6 = Sse.Multiply(l6, r6);
                    l7 = Sse.Multiply(l7, r7);
                    l12 = Sse.Multiply(l12, l11);
                    var r8 = Sse.Subtract(l13, l5);
                    l13 = Sse.Add(l13, l5);
                    var r9 = Sse.Add(l14, l6);
                    l14 = Sse.Subtract(l14, l6);
                    var r10 = Sse.Subtract(l15, l7);
                    l15 = Sse.Add(l15, l7);
                    var r11 = Sse.Add(l16, l12);
                    l16 = Sse.Subtract(l16, l12);
                    l13 = Sse.Shuffle(l13, r8, 216);
                    l14 = Sse.Shuffle(l14, r9, 216);
                    l15 = Sse.Shuffle(l15, r10, 216);
                    l16 = Sse.Shuffle(l16, r11, 216);
                    l13 = Permute(l13, 216);
                    l14 = Permute(l14, 216);
                    l15 = Permute(l15, 216);
                    l16 = Permute(l16, 216);
                    r3 = l1;
                    var num = l13.AsVector4F().Dot(r3.AsVector4F());
                    if (MathF.Abs(num) < float.Epsilon)
                        return new Matrix4x4F(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
                    var l17 = Vector128.Create(1f);
                    var r12 = Vector128.Create(num);
                    r12 = Sse.Divide(l17, r12);
                    l1 = Sse.Multiply(l12, r12);
                    r1 = Sse.Multiply(l14, r12);
                    l2 = Sse.Multiply(l15, r12);
                    r2 = Sse.Multiply(l16, r12);
                    Unsafe.SkipInit<Matrix4x4F>(out var result);
                    ref var reference = ref Unsafe.As<Matrix4x4F, Vector128<float>>(ref result);
                    reference = l1;
                    Unsafe.Add(ref reference, 1) = r1;
                    Unsafe.Add(ref reference, 2) = l2;
                    Unsafe.Add(ref reference, 3) = r2;
                    return result;
                }
            }
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
            float n1 = m11 * m16 - m12 * m15;
            float n2 = m10 * m16 - m12 * m14;
            float n3 = m10 * m15 - m11 * m14;
            float n4 = m9 * m16 - m12 * m13;
            float n5 = m9 * m15 - m11 * m13;
            float n6 = m9 * m14 - m10 * m13;
            float n7 = m6 * n1 - m7 * n2 + m8 * n3;
            float n8 = -(m5 * n1 - m7 * n4 + m8 * n5);
            float n9 = m5 * n2 - m6 * n4 + m8 * n6;
            float n10 = -(m5 * n3 - m6 * n5 + m7 * n6);
            float n11 = m1 * n7 + m2 * n8 + m3 * n9 + m4 * n10;
            if (MathF.Abs(n11) < float.Epsilon)
                return new Matrix4x4F(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
            float n12 = 1f / n11;
            float n13 = m7 * m16 - m8 * m15;
            float n14 = m6 * m16 - m8 * m14;
            float n15 = m6 * m15 - m7 * m14;
            float n16 = m5 * m16 - m8 * m13;
            float n17 = m5 * m15 - m7 * m13;
            float n18 = m5 * m14 - m6 * m13;
            float n19 = m7 * m12 - m8 * m11;
            float n20 = m6 * m12 - m8 * m10;
            float n21 = m6 * m11 - m7 * m10;
            float n22 = m5 * m12 - m8 * m9;
            float n23 = m5 * m11 - m7 * m9;
            float n24 = m5 * m10 - m6 * m9;
            return new Matrix4x4F(n7 * n12,
                                  n8 * n12,
                                  n9 * n12,
                                  n10 * n12,
                                  (-(m2 * n1 - m3 * n2 + m4 * n3)) * n12,
                                  (m1 * n1 - m3 * n4 + m4 * n5) * n12,
                                  (-(m1 * n2 - m2 * n4 + m4 * n6)) * n12,
                                  (m1 * n3 - m2 * n5 + m3 * n6) * n12,
                                  (m2 * n13 - m3 * n14 + m4 * n15) * n12,
                                  (-(m1 * n13 - m3 * n16 + m4 * n17)) * n12,
                                  (m1 * n14 - m2 * n16 + m4 * n18) * n12,
                                  (-(m1 * n15 - m2 * n17 + m3 * n18)) * n12,
                                  (-(m2 * n19 - m3 * n20 + m4 * n21)) * n12,
                                  (m1 * n19 - m3 * n22 + m4 * n23) * n12,
                                  (-(m1 * n20 - m2 * n22 + m4 * n24)) * n12,
                                  (m1 * n21 - m2 * n23 + m3 * n24) * n12);


        }

        public Matrix4x4F Transform(QuaternionF rotation)
        {
            float n1 = rotation.X + rotation.X;
            float n2 = rotation.Y + rotation.Y;
            float n3 = rotation.Z + rotation.Z;
            float n4 = rotation.W * n1;
            float n5 = rotation.W * n2;
            float n6 = rotation.W * n3;
            float n7 = rotation.X * n1;
            float n8 = rotation.X * n2;
            float n9 = rotation.X * n3;
            float n10 = rotation.Y * n2;
            float n11 = rotation.Y * n3;
            float n12 = rotation.Z * n3;
            float n13 = 1f - n10 - n12;
            float n14 = n8 - n6;
            float n15 = n9 + n5;
            float n16 = n8 + n6;
            float n17 = 1f - n7 - n12;
            float n18 = n11 - n4;
            float n19 = n9 - n5;
            float n20 = n11 + n4;
            float n21 = 1f - n7 - n10;
            return new Matrix4x4F(M11 * n13 + M12 * n14 + M13 * n15,
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

        public static Matrix4x4F Transform(Matrix4x4F value, QuaternionF rotation) =>
            value.Transform(rotation);

        public unsafe static Matrix4x4F Transpose(Matrix4x4F matrix)
        {
            if (Sse.IsSupported)
            {
                var l1 = Sse.LoadVector128(&matrix.M11);
                var r1 = Sse.LoadVector128(&matrix.M21);
                var l2 = Sse.LoadVector128(&matrix.M31);
                var r2 = Sse.LoadVector128(&matrix.M41);
                var v1 = Sse.UnpackLow(l1, r1);
                var v2 = Sse.UnpackLow(l2, r2);
                var v3 = Sse.UnpackHigh(l1, r1);
                var v4 = Sse.UnpackHigh(l2, r2);
                Sse.Store(&matrix.M11, Sse.MoveLowToHigh(v1, v2));
                Sse.Store(&matrix.M21, Sse.MoveHighToLow(v2, v1));
                Sse.Store(&matrix.M31, Sse.MoveLowToHigh(v3, v4));
                Sse.Store(&matrix.M41, Sse.MoveHighToLow(v4, v3));
                return matrix;
            }
            return new Matrix4x4F(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                                  matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                                  matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                                  matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        public Matrix4x4F Transpose() =>
            Transpose(this);

        public unsafe static Matrix4x4F Lerp(Matrix4x4F matrix1, Matrix4x4F matrix2, float amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            if (Sse.IsSupported)
            {
                var t = Vector128.Create(amount);
                Sse.Store(&m1.M11, VectorMath.Lerp(Sse.LoadVector128(&m1.M11), Sse.LoadVector128(&m2.M11), t));
                Sse.Store(&m1.M21, VectorMath.Lerp(Sse.LoadVector128(&m1.M21), Sse.LoadVector128(&m2.M21), t));
                Sse.Store(&m1.M31, VectorMath.Lerp(Sse.LoadVector128(&m1.M31), Sse.LoadVector128(&m2.M31), t));
                Sse.Store(&m1.M41, VectorMath.Lerp(Sse.LoadVector128(&m1.M41), Sse.LoadVector128(&m2.M41), t));
                return m1;
            }
            return new Matrix4x4F(m1.M11 + (m2.M11 - m1.M11) * amount,
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

        public static Matrix4x4F Negate(Matrix4x4F value) =>
            -value;

        public unsafe static Matrix4x4F operator -(Matrix4x4F value)
        {
            if (Sse.IsSupported)
            {
                var zero = Vector128<float>.Zero;
                Sse.Store(&value.M11, Sse.Subtract(zero, Sse.LoadVector128(&value.M11)));
                Sse.Store(&value.M21, Sse.Subtract(zero, Sse.LoadVector128(&value.M21)));
                Sse.Store(&value.M31, Sse.Subtract(zero, Sse.LoadVector128(&value.M31)));
                Sse.Store(&value.M41, Sse.Subtract(zero, Sse.LoadVector128(&value.M41)));
                return value;
            }
            return new Matrix4x4F(-value.M11, -value.M12, -value.M13, -value.M14,
                                  -value.M21, -value.M22, -value.M23, -value.M24,
                                  -value.M31, -value.M32, -value.M33, -value.M34,
                                  -value.M41, -value.M42, -value.M43, -value.M44);
        }

        public static Matrix4x4F operator +(Matrix4x4F value) =>
            value;

        public unsafe static Matrix4x4F operator +(Matrix4x4F left, Matrix4x4F right)
        {
            if (Sse.IsSupported)
            {
                Sse.Store(&left.M11, Sse.Add(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
                Sse.Store(&left.M21, Sse.Add(Sse.LoadVector128(&left.M21), Sse.LoadVector128(&right.M21)));
                Sse.Store(&left.M31, Sse.Add(Sse.LoadVector128(&left.M31), Sse.LoadVector128(&right.M31)));
                Sse.Store(&left.M41, Sse.Add(Sse.LoadVector128(&left.M41), Sse.LoadVector128(&right.M41)));

                return left;
            }
            return new Matrix4x4F(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
                                  left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24,
                                  left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34,
                                  left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);
        }

        public unsafe static Matrix4x4F operator -(Matrix4x4F left, Matrix4x4F right)
        {
            if (Sse.IsSupported)
            {
                Sse.Store(&left.M11, Sse.Subtract(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
                Sse.Store(&left.M21, Sse.Subtract(Sse.LoadVector128(&left.M21), Sse.LoadVector128(&right.M21)));
                Sse.Store(&left.M31, Sse.Subtract(Sse.LoadVector128(&left.M31), Sse.LoadVector128(&right.M31)));
                Sse.Store(&left.M41, Sse.Subtract(Sse.LoadVector128(&left.M41), Sse.LoadVector128(&right.M41)));

                return left;
            }
            return new Matrix4x4F(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
                                  left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24,
                                  left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34,
                                  left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);
        }

        public unsafe static Matrix4x4F operator *(Matrix4x4F value1, Matrix4x4F value2)
        {
            if (Sse.IsSupported)
            {
                Sse.Store(&value1.M11, MultiplyRow(value2, Sse.LoadVector128(&value1.M11)));
                Sse.Store(&value1.M21, MultiplyRow(value2, Sse.LoadVector128(&value1.M21)));
                Sse.Store(&value1.M31, MultiplyRow(value2, Sse.LoadVector128(&value1.M31)));
                Sse.Store(&value1.M41, MultiplyRow(value2, Sse.LoadVector128(&value1.M41)));
                return value1;
            }
            Matrix4x4F result;
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

        private static unsafe Vector128<float> MultiplyRow(Matrix4x4F value2, Vector128<float> vector)
        {
            return Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0),
                                                Sse.LoadVector128(&value2.M11)),
                                   Sse.Multiply(Sse.Shuffle(vector, vector, 85),
                                                Sse.LoadVector128(&value2.M21))),
                           Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 170),
                                                Sse.LoadVector128(&value2.M31)),
                                   Sse.Multiply(Sse.Shuffle(vector, vector, byte.MaxValue),
                                                Sse.LoadVector128(&value2.M41))));
        }

        public unsafe static Matrix4x4F operator *(Matrix4x4F value1, float value2)
        {
            if (Sse.IsSupported)
            {
                Vector128<float> right = Vector128.Create(value2);
                Sse.Store(&value1.M11, Sse.Multiply(Sse.LoadVector128(&value1.M11), right));
                Sse.Store(&value1.M21, Sse.Multiply(Sse.LoadVector128(&value1.M21), right));
                Sse.Store(&value1.M31, Sse.Multiply(Sse.LoadVector128(&value1.M31), right));
                Sse.Store(&value1.M41, Sse.Multiply(Sse.LoadVector128(&value1.M41), right));
                return value1;
            }
            Matrix4x4F result;
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

        public unsafe static bool operator ==(Matrix4x4F value1, Matrix4x4F value2)
        {
            if (Sse.IsSupported)
            {
                if (VectorMath.Equal(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) && VectorMath.Equal(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) && VectorMath.Equal(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)))
                    return VectorMath.Equal(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));
                return false;
            }
            if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
                return value1.M43 == value2.M43;
            return false;
        }

        public unsafe static bool operator !=(Matrix4x4F value1, Matrix4x4F value2)
        {
            if (Sse.IsSupported)
            {
                if (!VectorMath.NotEqual(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) && !VectorMath.NotEqual(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) && !VectorMath.NotEqual(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)))
                    return VectorMath.NotEqual(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));
                return true;
            }
            if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43)
                return value1.M44 != value2.M44;
            return true;
        }

        public readonly bool Equals(Matrix4x4F other)
        {
            return this == other;
        }

        public override readonly bool Equals(object? obj)
        {
            if (obj is Matrix4x4F)
            {
                Matrix4x4F value = (Matrix4x4F)obj;
                return this == value;
            }
            return false;
        }

        public override readonly string ToString() =>
            $"{{{{ {{{{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}}}} {{{{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}}}} {{{{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}}}} }}}}";

        public override readonly int GetHashCode()
        {
            return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() + M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() + M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
        }

        private static class VectorMath
        {
            public static Vector128<float> Lerp(Vector128<float> a, Vector128<float> b, Vector128<float> t) =>
                Sse.Add(a, Sse.Multiply(Sse.Subtract(b, a), t));

            public static bool Equal(Vector128<float> a, Vector128<float> b) =>
                Sse.MoveMask(Sse.CompareNotEqual(a, b)) == 0;

            public static bool NotEqual(Vector128<float> a, Vector128<float> b) =>
                Sse.MoveMask(Sse.CompareNotEqual(a, b)) != 0;
        }
    }
}
