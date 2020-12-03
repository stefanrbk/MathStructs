using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//using System.Runtime.Intrinsics;
//using System.Runtime.Intrinsics.Arm;
//using System.Runtime.Intrinsics.X86;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix of <see cref="float"/> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct Matrix4x4F
    {
        #region Public Fields

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [FieldOffset(0)]
        public float M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [FieldOffset(4)]
        public float M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [FieldOffset(8)]
        public float M13;

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        [FieldOffset(12)]
        public float M14;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [FieldOffset(16)]
        public float M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [FieldOffset(20)]
        public float M22;
        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [FieldOffset(24)]
        public float M23;

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        [FieldOffset(28)]
        public float M24;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [FieldOffset(32)]
        public float M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [FieldOffset(36)]
        public float M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [FieldOffset(40)]
        public float M33;

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        [FieldOffset(44)]
        public float M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        [FieldOffset(48)]
        public float M41;

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        [FieldOffset(52)]
        public float M42;

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        [FieldOffset(56)]
        public float M43;

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        [FieldOffset(60)]
        public float M44;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        private static readonly Matrix4x4F _identity = new Matrix4x4F(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

        private static readonly Matrix4x4F _nan = new Matrix4x4F(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a <see cref="Matrix4x4F"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4F(float m11, float m12, float m13, float m14,
                          float m21, float m22, float m23, float m24,
                          float m31, float m32, float m33, float m34,
                          float m41, float m42, float m43, float m44)
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

        /// <summary>
        /// Constructs a Matrix4x4D from the given <see cref="Matrix3x3F"/>.
        /// </summary>
        /// <param name="value">
        /// The source <see cref="Matrix3x3F"/>.
        /// </param>
        [MethodImpl(Optimize)]
        public Matrix4x4F(Matrix3x3F value)
        {
            M11 = value.M11;
            M12 = value.M12;
            M13 = value.M13;
            M14 = 0;
            M21 = value.M21;
            M22 = value.M22;
            M23 = value.M23;
            M24 = 0;
            M31 = value.M31;
            M32 = value.M32;
            M33 = value.M33;
            M34 = 0;
            M41 = 0;
            M42 = 0;
            M43 = 0;
            M44 = 1;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix4x4F Identity => _identity;

        /// <summary>
        /// Returns a matrix with all values set to NaN.
        /// </summary>
        public static Matrix4x4F NaN => _nan;

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity =>
            this == Identity;

        /// <summary>
        /// Gets or sets the translation component of this matrix.
        /// </summary>
        public Vector3F Translation
        {
            get => new Vector3F(M41, M42, M43);
            set => (M41, M42, M43) = value;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Add(Matrix4x4F left, Matrix4x4F right) =>
            left + right;

        /// <summary>
        ///     Copies the contents of the matrix into the given span.
        /// </summary>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<float> span) =>
            CopyTo(span, 0);

        /// <summary>
        ///     Copies the contents of the matrix into the given span, starting from index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     If index is greater than end of the span or index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     If number of elements in source matrix is greater than those available in destination span.
        /// </exception>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<float> span, int index)
        {
            if (index < 0 || index >= span.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (span.Length - index < 16)
                throw new ArgumentException("Elements in source is greater than destination");
            span[index +  0] = M11;
            span[index +  1] = M12;
            span[index +  2] = M13;
            span[index +  3] = M14;
            span[index +  4] = M21;
            span[index +  5] = M22;
            span[index +  6] = M23;
            span[index +  7] = M24;
            span[index +  8] = M31;
            span[index +  9] = M32;
            span[index + 10] = M33;
            span[index + 11] = M34;
            span[index + 12] = M41;
            span[index + 13] = M42;
            span[index + 14] = M43;
            span[index + 15] = M44;
        }

        /// <summary>
        /// Creates a spherical billboard that rotates around a specified object position.
        /// </summary>
        /// <param name="objectPosition">
        /// Position of the object the billboard will rotate around.
        /// </param>
        /// <param name="cameraPosition">
        /// Position of the camera.
        /// </param>
        /// <param name="cameraUpVector">
        /// The up vector of the camera.
        /// </param>
        /// <param name="cameraForwardVector">
        /// The forward vector of the camera.
        /// </param>
        public static Matrix4x4F CreateBillboard(Vector3F objectPosition, Vector3F cameraPosition, Vector3F cameraUpVector, Vector3F cameraForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = (!(num < 0.0001f)) ? left * (1f / MathF.Sqrt(num)) : -cameraForwardVector;
            var v1 = cameraUpVector.Cross(left).Normalize();
            var v2 = left.Cross(v1);
            return _identity.With(m11: v1.X,
                                  m12: v1.Y,
                                  m13: v1.Z,
                                  m21: v2.X,
                                  m22: v2.Y,
                                  m23: v2.Z,
                                  m31: left.X,
                                  m32: left.Y,
                                  m33: left.Z,
                                  m41: objectPosition.X,
                                  m42: objectPosition.Y,
                                  m43: objectPosition.Z);
        }

        /// <summary>
        /// Creates a cylindrical billboard that rotates around a specified axis.
        /// </summary>
        /// <param name="objectPosition">
        /// Position of the object the billboard will rotate around.
        /// </param>
        /// <param name="cameraPosition">
        /// Position of the camera.
        /// </param>
        /// <param name="rotateAxis">
        /// Axis to rotate the billboard around.
        /// </param>
        /// <param name="cameraForwardVector">
        /// Forward vector of the camera.
        /// </param>
        /// <param name="objectForwardVector">
        /// Forward vector of the object.
        /// </param>
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
            return _identity.With(m11: v3.X,
                                  m12: v3.Y,
                                  m13: v3.Z,
                                  m21: v1.X,
                                  m22: v1.Y,
                                  m23: v1.Z,
                                  m31: v2.X,
                                  m32: v2.Y,
                                  m33: v2.Z,
                                  m41: objectPosition.X,
                                  m42: objectPosition.Y,
                                  m43: objectPosition.Z);
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary vector.
        /// </summary>
        /// <param name="axis">
        /// The axis to rotate around.
        /// </param>
        /// <param name="angle">
        /// The angle to rotate around the given axis, in radians.
        /// </param>
        public static Matrix4x4F CreateFromAxisAngle(Vector3F axis, float angle)
        {
            /*
             * a: angle
             * x, y, z: unit vector for axis.
             *
             * Rotation matrix M can computed by using below equation.
             *
             *
             *  M = UUᵀ + ( cos a )( I - UUᵀ ) + ( sin a )S
             *
             * Where:
             *
             *  U = ( x , y , z )
             *
             *      ⎡  0 -z  y  ⎤
             *  S = ⎢  z  0 -x  ⎥
             *      ⎣ -y  x  0  ⎦
             *
             *      ⎡  1  0  0  ⎤
             *  I = ⎢  0  1  0  ⎥
             *      ⎣  0  0  1  ⎦
             *
             *      ⎡    xx + cos a ∙ ( 1 - xx )       yx - cos a ∙ yx - sin a ∙ z     zx - cos a ∙ xz + sin a ∙ y  ⎤
             *  M = ⎢  xy - cos a ∙ yx + sin a ∙ z       yy + cos a ∙ ( 1 - yy )       yz - cos a ∙ yz - sin a ∙ x  ⎥
             *      ⎣  zx - cos a ∙ zx - sin a ∙ y     zy - cos a ∙ zy + sin a ∙ x       zz + cos a ∙ ( 1 - zz )    ⎦
             */
            (var x, var y, var z) = axis;
            var sa = MathF.Sin(angle);
            var ca = MathF.Cos(angle);
            (var xx, var yy, var zz) = (x * x, y * y, z * z);
            (var xy, var xz, var yz) = (x * y, x * z, y * z);

            return _identity.With(m11: xx + ca * (1 - xx),
                                  m12: xy - ca * xy + sa * z,
                                  m13: xz - ca * xz - sa * y,

                                  m21: xy - ca * xy - sa * z,
                                  m22: yy + ca * (1 - yy),
                                  m23: yz - ca * yz + sa * x,

                                  m31: xz - ca * xz + sa * y,
                                  m32: yz - ca * yz - sa * x,
                                  m33: zz + ca * (1 - zz));
        }

        /// <summary>
        /// Creates a rotation matrix from the given <see cref="QuaternionF"/> rotation value.
        /// </summary>
        /// <param name="q">
        /// The source <see cref="QuaternionF"/>.
        /// </param>
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

        /// <summary>
        /// Creates a rotation matrix from the specified yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">
        /// Angle of rotation, in radians, around the Y-axis.
        /// </param>
        /// <param name="pitch">
        /// Angle of rotation, in radians, around the X-axis.
        /// </param>
        /// <param name="roll">
        /// Angle of rotation, in radians, around the Z-axis.
        /// </param>
        public static Matrix4x4F CreateFromYawPitchRoll(float yaw, float pitch, float roll) =>
            CreateFromQuaternion(QuaternionF.CreateFromYawPitchRoll(yaw, pitch, roll));

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <param name="cameraPosition">
        /// The position of the camera.
        /// </param>
        /// <param name="cameraTarget">
        /// The target towards which the camera is pointing.
        /// </param>
        /// <param name="cameraUpVector">
        /// The direction that is "up" from the camera's point of view.
        /// </param>
        public static Matrix4x4F CreateLookAt(Vector3F cameraPosition, Vector3F cameraTarget, Vector3F cameraUpVector)
        {
            var zAxis = (cameraPosition - cameraTarget).Normalize();
            var xAxis = cameraUpVector.Cross(zAxis).Normalize();
            var yAxis = zAxis.Cross(xAxis);

            return _identity.With(m11: xAxis.X,
                                  m12: yAxis.X,
                                  m13: zAxis.X,
                                  m21: xAxis.Y,
                                  m22: yAxis.Y,
                                  m23: zAxis.Y,
                                  m31: xAxis.Z,
                                  m32: yAxis.Z,
                                  m33: zAxis.Z,
                                  m41: -xAxis.Dot(cameraPosition),
                                  m42: -yAxis.Dot(cameraPosition),
                                  m43: -zAxis.Dot(cameraPosition));
        }

        /// <summary>
        /// Creates an orthographic perspective matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">
        /// Width of the view volume.
        /// </param>
        /// <param name="height">
        /// Height of the view volume.
        /// </param>
        /// <param name="zNearPlane">
        /// Minimum Z-value of the view volume.
        /// </param>
        /// <param name="zFarPlane">
        /// Maximum Z-value of the view volume.
        /// </param>
        public static Matrix4x4F CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane) =>
            _identity.With(m11: 2.0f / width,
                           m22: 2.0f / height,
                           m33: 1.0f / (zNearPlane - zFarPlane),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        /// <summary>
        /// Builds a customized, orthographic projection matrix.
        /// </summary>
        /// <param name="left">
        /// Minimum X-value of the view volume.
        /// </param>
        /// <param name="right">
        /// Maximum X-value of the view volume.
        /// </param>
        /// <param name="bottom">
        /// Minimum Y-value of the view volume.
        /// </param>
        /// <param name="top">
        /// Maximum Y-value of the view volume.
        /// </param>
        /// <param name="zNearPlane">
        /// Minimum Z-value of the view volume.
        /// </param>
        /// <param name="zFarPlane">
        /// Maximum Z-value of the view volume.
        /// </param>
        public static Matrix4x4F CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane) =>
            _identity.With(m11: 2.0f / (right - left),
                           m22: 2.0f / (top - bottom),
                           m33: 1.0f / (zNearPlane - zFarPlane),
                           m41: (left + right) / (left - right),
                           m42: (top + bottom) / (bottom - top),
                           m43: zNearPlane / (zNearPlane - zFarPlane));

        /// <summary>
        /// Create a perspective projection matrix from the given view volume dimension.
        /// </summary>
        /// <param name="width">
        /// Width of the view volume at the near view plane.
        /// </param>
        /// <param name="height">
        /// Height of the view volume at the near view plane.
        /// </param>
        /// <param name="nearPlaneDistance">
        /// Distance to the near view plane.
        /// </param>
        /// <param name="farPlaneDistance">
        /// Distance to the far view plane.
        /// </param>
        public static Matrix4x4F CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var negFarRange = float.IsPositiveInfinity(farPlaneDistance) ? -1.0f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return default(Matrix4x4F).With(m11: 2.0f * nearPlaneDistance / width,
                                            m22: 2.0f * nearPlaneDistance / height,
                                            m33: negFarRange,
                                            m34: -1.0f,
                                            m43: nearPlaneDistance * negFarRange);
        }

        /// <summary>
        /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.
        /// </summary>
        /// <param name="fieldOfView">
        /// Field of view in the y direction, in radians.
        /// </param>
        /// <param name="aspectRatio">
        /// Aspect ratio, defined as view space width divided by height.
        /// </param>
        /// <param name="nearPlaneDistance">
        /// Distance to the near view plane.
        /// </param>
        /// <param name="farPlaneDistance">
        /// Distance to the far view plane.
        /// </param>
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

            var yScale = 1 / MathF.Tan(fieldOfView * 0.5f);
            var xScale = yScale / aspectRatio;

            var negFarRange = float.IsPositiveInfinity(farPlaneDistance) ? -1.0f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return default(Matrix4x4F).With(m11: xScale,
                                            m22: yScale,
                                            m33: negFarRange,
                                            m34: -1.0f,
                                            m43: nearPlaneDistance * negFarRange);
        }

        /// <summary>
        /// Creates a customized, perspective projection matrix.
        /// </summary>
        /// <param name="left">
        /// Minimum x-value of the view volume at the near view plane.
        /// </param>
        /// <param name="right">
        /// Maximum x-value of the view volume at the near view plane.
        /// </param>
        /// <param name="bottom">
        /// Minimum y-value of the view volume at the near view plane.
        /// </param>
        /// <param name="top">
        /// Maximum y-value of the view volume at the near view plane.
        /// </param>
        /// <param name="nearPlaneDistance">
        /// Distance to the near view plane.
        /// </param>
        /// <param name="farPlaneDistance">
        /// Distance to the far view plane.
        /// </param>
        public static Matrix4x4F CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var negFarRange = float.IsPositiveInfinity(farPlaneDistance) ? -1.0f : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return _identity.With(m11: 2.0f * nearPlaneDistance / (right - left),
                                  m22: 2.0f * nearPlaneDistance / (top - bottom),
                                  m31: (left + right) / (right - left),
                                  m32: (top + bottom) / (top - bottom),
                                  m33: negFarRange,
                                  m34: -1.0f,
                                  m43: nearPlaneDistance * negFarRange,
                                  m44: 0);
        }

        /// <summary>
        /// Creates a Matrix that reflects the coordinate system about a specified Plane.
        /// </summary>
        /// <param name="value">
        /// The Plane about which to create a reflection.
        /// </param>
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

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the X-axis.
        /// </param>
        public static Matrix4x4F CreateRotationX(float radians)
        {
            var c = MathF.Cos(radians);
            var s = MathF.Sin(radians);

            /*  [   1   0   0   0   ]
                [   0   c   s   0   ]
                [   0  -s   c   0   ]
                [   0   0   0   1   ] */

            return _identity.With(m22: c, m23: s, m32: -s, m33: c);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis, from a center point.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the X-axis.
        /// </param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateRotationX(float radians, Vector3F centerPoint)
        {
            var c = MathF.Cos(radians);
            var s = MathF.Sin(radians);
            var y = centerPoint.Y * (1 - c) + centerPoint.Z * s;
            var z = centerPoint.Z * (1 - c) - centerPoint.Y * s;

            /*  [   1   0   0   0   ]
                [   0   c   s   0   ]
                [   0  -s   c   0   ]
                [   0   y   z   1   ] */

            return CreateRotationX(radians).With(m42: y, m43: z);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Y-axis.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the Y-axis.
        /// </param>
        public static Matrix4x4F CreateRotationY(float radians)
        {
            var n1 = MathF.Cos(radians);
            var n2 = MathF.Sin(radians);

            /*  [   c   0  -s   0   ]
                [   0   1   0   0   ]
                [   s   0   c   0   ]
                [   0   0   0   1   ] */

            return _identity.With(m11: n1, m13: -n2, m31: n2, m33: n1);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Y-axis, from a center point.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the Y-axis.
        /// </param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateRotationY(float radians, Vector3F centerPoint)
        {
            var c = MathF.Cos(radians);
            var s = MathF.Sin(radians);
            var x = centerPoint.X * (1 - c) - centerPoint.Z * s;
            var z = centerPoint.Z * (1 - c) + centerPoint.X * s;

            /*  [   c   0  -s   0   ]
                [   0   1   0   0   ]
                [   s   0   c   0   ]
                [   x   0   z   1   ] */

            return CreateRotationY(radians).With(m41: x, m43: z);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Z-axis.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the Z-axis.
        /// </param>
        public static Matrix4x4F CreateRotationZ(float radians)
        {
            var c = MathF.Cos(radians);
            var s = MathF.Sin(radians);

            /*  [   c   s   0   0   ]
                [  -s   c   0   0   ]
                [   0   0   1   0   ]
                [   0   0   0   1   ] */

            return _identity.With(m11: c, m12: s, m21: -s, m22: c);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Z-axis, from a center point.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the Z-axis.
        /// </param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateRotationZ(float radians, Vector3F centerPoint)
        {
            var c = MathF.Cos(radians);
            var s = MathF.Sin(radians);
            var x = centerPoint.X * (1 - c) + centerPoint.Y * s;
            var y = centerPoint.Y * (1 - c) - centerPoint.X * s;

            /*  [   c   s   0   0   ]
                [  -s   c   0   0   ]
                [   0   0   1   0   ]
                [   x   y   0   1   ] */

            return CreateRotationZ(radians).With(m41: x, m42: y);
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="xScale">
        /// Value to scale by on the X-axis.
        /// </param>
        /// <param name="yScale">
        /// Value to scale by on the Y-axis.
        /// </param>
        /// <param name="zScale">
        /// Value to scale by on the Z-axis.
        /// </param>
        public static Matrix4x4F CreateScale(float xScale, float yScale, float zScale) =>
            _identity.With(m11: xScale, m22: yScale, m33: zScale);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="xScale">
        /// Value to scale by on the X-axis.
        /// </param>
        /// <param name="yScale">
        /// Value to scale by on the Y-axis.
        /// </param>
        /// <param name="zScale">
        /// Value to scale by on the Z-axis.
        /// </param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateScale(float xScale, float yScale, float zScale, Vector3F centerPoint)
        {
            var m1 = centerPoint.X * (1f - xScale);
            var m2 = centerPoint.Y * (1f - yScale);
            var m3 = centerPoint.Z * (1f - zScale);
            return CreateScale(xScale, yScale, zScale).With(m41: m1, m42: m2, m43: m3);
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scale">
        /// The vector containing the amount to scale by on each axis.
        /// </param>
        public static Matrix4x4F CreateScale(Vector3F scale) =>
            CreateScale(scale.X, scale.Y, scale.Z);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="scale">The vector containing the amount to scale by on each axis.</param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateScale(Vector3F scale, Vector3F centerPoint) =>
            CreateScale(scale.X, scale.Y, scale.Z, centerPoint);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis.
        /// </summary>
        /// <param name="scale">
        /// The uniform scaling factor.
        /// </param>
        public static Matrix4x4F CreateScale(float scale) =>
            CreateScale(scale, scale, scale);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
        /// </summary>
        /// <param name="scale">
        /// The uniform scaling factor.
        /// </param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4F CreateScale(float scale, Vector3F centerPoint) =>
            CreateScale(scale, scale, scale, centerPoint);

        /// <summary>
        /// Creates a Matrix that flattens geometry into a specified Plan as if casting a shadow from a specified light source.
        /// </summary>
        /// <param name="lightDirection">
        /// The direction from which the light that will cast the shadow is coming.
        /// </param>
        /// <param name="plane">
        /// The Plane onto which the new matrix should flatten geometry so as to cast a shadow.
        /// </param>
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

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="position">
        /// The amount to translate in each axis.
        /// </param>
        public static Matrix4x4F CreateTranslation(Vector3F position) =>
            CreateTranslation(position.X, position.Y, position.Z);

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="xPosition">
        /// The amount to translate on the X-axis.
        /// </param>
        /// <param name="yPosition">
        /// The amount to translate on the Y-axis.
        /// </param>
        /// <param name="zPosition">
        /// The amount to translate on the Z-axis.
        /// </param>
        public static Matrix4x4F CreateTranslation(float xPosition, float yPosition, float zPosition) =>
            _identity.With(m41: xPosition, m42: yPosition, m43: zPosition);

        /// <summary>
        /// Creates a world matrix with the specified parameters.
        /// </summary>
        /// <param name="position">
        /// The position of the object; used in translation operations.
        /// </param>
        /// <param name="forward">
        /// Forward direction of the object.
        /// </param>
        /// <param name="up">
        /// Upward direction of the object; usually &lt; 0 1 0 &gt;
        /// </param>
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

        /// <summary>
        /// Attempts to extract the scale, translation, and rotation components from the given scale/rotation/translation matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        /// <param name="scale">
        /// The scaling component of the transformation matrix.
        /// </param>
        /// <param name="rotation">
        /// The rotation component of the transformation matrix.
        /// </param>
        /// <param name="translation">
        /// The translation component of the transformation matrix.
        /// </param>
        /// <returns>
        /// True if the source matrix was successfully decomposed; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static bool Decompose(Matrix4x4F matrix, out Vector3F scale, out QuaternionF rotation, out Vector3F translation)
        {
            (scale, rotation, translation) = matrix;
            return scale != Vector3F.Zero || translation != Vector3F.Zero || rotation != QuaternionF.Identity;
        }

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, the result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix to invert.
        /// </param>
        /// <returns>
        /// If successful, the inverted matrix; NaN matrix otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Invert(Matrix4x4F matrix)
        {
            // This implementation is based on the DirectX Math Library XMMatrixInverse method
            // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathMatrix.inl

            //if (Sse.IsSupported)
            //    return SseImpl(matrix);

            //return SoftwareFallback(matrix);

            //static unsafe Matrix4x4F SseImpl(Matrix4x4F matrix)
            //{
            //    if (!Sse.IsSupported)
            //        // Redundant test so we won't preJIT remainder of this method on platforms without SSE.
            //        throw new PlatformNotSupportedException();

            //    // Load the matrix values into rows
            //    var row1 = Sse.LoadVector128(&matrix.M11);
            //    var row2 = Sse.LoadVector128(&matrix.M21);
            //    var row3 = Sse.LoadVector128(&matrix.M31);
            //    var row4 = Sse.LoadVector128(&matrix.M41);

            //    // Transpose the matrix
            //    var vTemp1 = Sse.Shuffle(row1, row2, 0x44); //_MM_SHUFFLE(1, 0, 1, 0)
            //    var vTemp3 = Sse.Shuffle(row1, row2, 0xEE); //_MM_SHUFFLE(3, 2, 3, 2)
            //    var vTemp2 = Sse.Shuffle(row3, row4, 0x44); //_MM_SHUFFLE(1, 0, 1, 0)
            //    var vTemp4 = Sse.Shuffle(row3, row4, 0xEE); //_MM_SHUFFLE(3, 2, 3, 2)

            //    row1 = Sse.Shuffle(vTemp1, vTemp2, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    row2 = Sse.Shuffle(vTemp1, vTemp2, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)
            //    row3 = Sse.Shuffle(vTemp3, vTemp4, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    row4 = Sse.Shuffle(vTemp3, vTemp4, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)

            //    var V00 = Permute(row3, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    var V10 = Permute(row4, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    var V01 = Permute(row1, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    var V11 = Permute(row2, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    var V02 = Sse.Shuffle(row3, row1, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)
            //    var V12 = Sse.Shuffle(row4, row2, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)

            //    var D0 = Sse.Multiply(V00, V10);
            //    var D1 = Sse.Multiply(V01, V11);
            //    var D2 = Sse.Multiply(V02, V12);

            //    V00 = Permute(row3, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    V10 = Permute(row4, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    V01 = Permute(row1, 0xEE);           //_MM_SHUFFLE(3, 2, 3, 2)
            //    V11 = Permute(row2, 0x50);           //_MM_SHUFFLE(1, 1, 0, 0)
            //    V02 = Sse.Shuffle(row3, row1, 0xDD); //_MM_SHUFFLE(3, 1, 3, 1)
            //    V12 = Sse.Shuffle(row4, row2, 0x88); //_MM_SHUFFLE(2, 0, 2, 0)

            //    // Note: We use this expansion pattern instead of Fused Multiply Add
            //    // in order to support older hardware
            //    D0 = Sse.Subtract(D0, Sse.Multiply(V00, V10));
            //    D1 = Sse.Subtract(D1, Sse.Multiply(V01, V11));
            //    D2 = Sse.Subtract(D2, Sse.Multiply(V02, V12));

            //    // V11 = D0Y,D0W,D2Y,D2Y
            //    V11 = Sse.Shuffle(D0, D2, 0x5D);  //_MM_SHUFFLE(1, 1, 3, 1)
            //    V00 = Permute(row2, 0x49);        //_MM_SHUFFLE(1, 0, 2, 1)
            //    V10 = Sse.Shuffle(V11, D0, 0x32); //_MM_SHUFFLE(0, 3, 0, 2)
            //    V01 = Permute(row1, 0x12);        //_MM_SHUFFLE(0, 1, 0, 2)
            //    V11 = Sse.Shuffle(V11, D0, 0x99); //_MM_SHUFFLE(2, 1, 2, 1)

            //    // V13 = D1Y,D1W,D2W,D2W
            //    var V13 = Sse.Shuffle(D1, D2, 0xFD); //_MM_SHUFFLE(3, 3, 3, 1)
            //    V02 = Permute(row4, 0x49);           //_MM_SHUFFLE(1, 0, 2, 1)
            //    V12 = Sse.Shuffle(V13, D1, 0x32);    //_MM_SHUFFLE(0, 3, 0, 2)
            //    var V03 = Permute(row3, 0x12);       //_MM_SHUFFLE(0, 1, 0, 2)
            //    V13 = Sse.Shuffle(V13, D1, 153);     //_MM_SHUFFLE(2, 1, 2, 1)

            //    var C0 = Sse.Multiply(V00, V10);
            //    var C2 = Sse.Multiply(V01, V11);
            //    var C4 = Sse.Multiply(V02, V12);
            //    var C6 = Sse.Multiply(V03, V13);

            //    // V11 = D0X,D0Y,D2X,D2X
            //    V11 = Sse.Shuffle(D0, D2, 0x04);  //_MM_SHUFFLE(0, 0, 1, 0)
            //    V00 = Permute(row2, 0x9E);        //_MM_SHUFFLE(2, 1, 3, 2)
            //    V10 = Sse.Shuffle(D0, V11, 0x93); //_MM_SHUFFLE(2, 1, 0, 3)
            //    V01 = Permute(row1, 0x7B);        //_MM_SHUFFLE(1, 3, 2, 3)
            //    V11 = Sse.Shuffle(D0, V11, 0x26); //_MM_SHUFFLE(0, 2, 1, 2)

            //    // V13 = D1X,D1Y,D2Z,D2Z
            //    V13 = Sse.Shuffle(D1, D2, 0xA4);  //_MM_SHUFFLE(2, 2, 1, 0)
            //    V02 = Permute(row4, 0x9E);        //_MM_SHUFFLE(2, 1, 3, 2)
            //    V12 = Sse.Shuffle(D1, V13, 0x93); //_MM_SHUFFLE(2, 1, 0, 3)
            //    V03 = Permute(row3, 0x7B);        //_MM_SHUFFLE(1, 3, 2, 3)
            //    V13 = Sse.Shuffle(D1, V13, 0x26); //_MM_SHUFFLE(0, 2, 1, 2)

            //    C0 = Sse.Subtract(C0, Sse.Multiply(V00, V10));
            //    C2 = Sse.Subtract(C2, Sse.Multiply(V01, V11));
            //    C4 = Sse.Subtract(C4, Sse.Multiply(V02, V12));
            //    C6 = Sse.Subtract(C6, Sse.Multiply(V03, V13));

            //    V00 = Permute(row2, 0x33); //_MM_SHUFFLE(0, 3, 0, 3)

            //    // V10 = D0Z,D0Z,D2X,D2Y
            //    V10 = Sse.Shuffle(D0, D2, 0x4A); //_MM_SHUFFLE(1, 0, 2, 2)
            //    V10 = Permute(V10, 0x2C);        //_MM_SHUFFLE(0, 2, 3, 0)
            //    V01 = Permute(row1, 0x8D);       //_MM_SHUFFLE(2, 0, 3, 1)

            //    // V11 = D0X,D0W,D2X,D2Y
            //    V11 = Sse.Shuffle(D0, D2, 0x4C); //_MM_SHUFFLE(1, 0, 3, 0)
            //    V11 = Permute(V11, 0x93);        //_MM_SHUFFLE(2, 1, 0, 3)
            //    V02 = Permute(row4, 0x33);       //_MM_SHUFFLE(0, 3, 0, 3)

            //    // V12 = D1Z,D1Z,D2Z,D2W
            //    V12 = Sse.Shuffle(D1, D2, 0xEA); //_MM_SHUFFLE(3, 2, 2, 2)
            //    V12 = Permute(V12, 0x2C);        //_MM_SHUFFLE(0, 2, 3, 0)
            //    V03 = Permute(row3, 0x8D);       //_MM_SHUFFLE(2, 0, 3, 1)

            //    // V13 = D1X,D1W,D2Z,D2W
            //    V13 = Sse.Shuffle(D1, D2, 0xEC); //_MM_SHUFFLE(3, 2, 3, 0)
            //    V13 = Permute(V13, 0x93);        //_MM_SHUFFLE(2, 1, 0, 3)

            //    V00 = Sse.Multiply(V00, V10);
            //    V01 = Sse.Multiply(V01, V11);
            //    V02 = Sse.Multiply(V02, V12);
            //    V03 = Sse.Multiply(V03, V13);

            //    var C1 = Sse.Subtract(C0, V00);
            //    C0 = Sse.Add(C0, V00);
            //    var C3 = Sse.Add(C2, V01);
            //    C2 = Sse.Subtract(C2, V01);
            //    var C5 = Sse.Subtract(C4, V02);
            //    C4 = Sse.Add(C4, V02);
            //    var C7 = Sse.Add(C6, V03);
            //    C6 = Sse.Subtract(C6, V03);

            //    C0 = Sse.Shuffle(C0, C1, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C2 = Sse.Shuffle(C2, C3, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C4 = Sse.Shuffle(C4, C5, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C6 = Sse.Shuffle(C6, C7, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)

            //    C0 = Permute(C0, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C2 = Permute(C2, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C4 = Permute(C4, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)
            //    C6 = Permute(C6, 0xD8); //_MM_SHUFFLE(3, 1, 2, 0)

            //    // Get the determinant
            //    vTemp2 = row1;
            //    float det = Vector4F.Dot(C0.AsVector4F(), vTemp2.AsVector4F());

            //    // Check determinate is not zero
            //    if (MathF.Abs(det) < float.Epsilon)
            //        return _nan;

            //    // Create Vector128<float> copy of the determinant and invert them.
            //    var ones = Vector128.Create(1f);
            //    var vTemp = Vector128.Create(det);
            //    vTemp = Sse.Divide(ones, vTemp);

            //    row1 = Sse.Multiply(C0, vTemp);
            //    row2 = Sse.Multiply(C2, vTemp);
            //    row3 = Sse.Multiply(C4, vTemp);
            //    row4 = Sse.Multiply(C6, vTemp);

            //    Unsafe.SkipInit<Matrix4x4F>(out var result);
            //    ref var vResult = ref Unsafe.As<Matrix4x4F, Vector128<float>>(ref result);

            //    vResult = row1;
            //    Unsafe.Add(ref vResult, 1) = row2;
            //    Unsafe.Add(ref vResult, 2) = row3;
            //    Unsafe.Add(ref vResult, 3) = row4;

            //    return result;
            //}

            //static Matrix4x4F SoftwareFallback(Matrix4x4F matrix)
            {
                /*
                 * If you have matrix M, inverse Matrix M⁻¹ can compute
                 *
                 *              1
                 *    M⁻¹ = ⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯ A
                 *            det(M)
                 *
                 * A is adjugate (adjoint) of M, where,
                 *
                 *
                 * A = Cᵀ
                 *
                 * C is Cofactor matrix of M, where,
                 *
                 * Cᵢⱼ = ( -1 ) ^ ( i + j ) ∙ det(Mᵢⱼ)
                 *
                 *
                 *     ⎡ a b c d ⎤
                 * M = ⎢ e f g h ⎥
                 *     ⎢ i j k l ⎥
                 *     ⎣ m n o p ⎦
                 *
                 * First Row
                 *             ⎡ f g h ⎤
                 * C₁₁ = (-1)² ⎢ j k l ⎥ = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ e g h ⎤
                 * C₁₂ = (-1)³ ⎢ i k l ⎥ = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ e f h ⎤
                 * C₁₃ = (-1)⁴ ⎢ i j l ⎥ = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ e f g ⎤
                 * C₁₄ = (-1)⁵ ⎢ i j k ⎥ = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Second Row
                 *             ⎡ b c d ⎤
                 * C₂₁ = (-1)³ ⎢ j k l ⎥ = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₂₂ = (-1)⁴ ⎢ i k l ⎥ = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₂₃ = (-1)⁵ ⎢ i j l ⎥ = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₂₄ = (-1)⁶ ⎢ i j k ⎥ = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Third Row
                 *             ⎡ b c d ⎤
                 * C₃₁ = (-1)⁴ ⎢ f g h ⎥ = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
                 *             ⎣ n o p ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₃₂ = (-1)⁵ ⎢ e g h ⎥ = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
                 *             ⎣ m o p ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₃₃ = (-1)⁶ ⎢ e f h ⎥ = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
                 *             ⎣ m n p ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₃₄ = (-1)⁷ ⎢ e f g ⎥ = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
                 *             ⎣ m n o ⎦
                 *
                 * Fourth Row
                 *             ⎡ b c d ⎤
                 * C₄₁ = (-1)⁵ ⎢ f g h ⎥ = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
                 *             ⎣ j k l ⎦
                 *
                 *             ⎡ a c d ⎤
                 * C₄₂ = (-1)⁶ ⎢ e g h ⎥ = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
                 *             ⎣ i k l ⎦
                 *
                 *             ⎡ a b d ⎤
                 * C₄₃ = (-1)⁷ ⎢ e f h ⎥ = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
                 *             ⎣ i j l ⎦
                 *
                 *             ⎡ a b c ⎤
                 * C₄₄ = (-1)⁸ ⎢ e f g ⎥ = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
                 *             ⎣ i j k ⎦
                 *
                 * Cost of operation
                 * 53 adds, 104 muls, and 1 div.
                 */
                (var a, var b, var c, var d) = (matrix.M11, matrix.M12, matrix.M13, matrix.M14);
                (var e, var f, var g, var h) = (matrix.M21, matrix.M22, matrix.M23, matrix.M24);
                (var i, var j, var k, var l) = (matrix.M31, matrix.M32, matrix.M33, matrix.M34);
                (var m, var n, var o, var p) = (matrix.M41, matrix.M42, matrix.M43, matrix.M44);

                var kp_lo = k * p - l * o;
                var jp_ln = j * p - l * n;
                var jo_kn = j * o - k * n;
                var ip_lm = i * p - l * m;
                var io_km = i * o - k * m;
                var in_jm = i * n - j * m;

                var a11 = f * kp_lo - g * jp_ln + h * jo_kn;
                var a12 = -(e * kp_lo - g * ip_lm + h * io_km);
                var a13 = e * jp_ln - f * ip_lm + h * in_jm;
                var a14 = -(e * jo_kn - f * io_km + g * in_jm);

                var det = a * a11 + b * a12 + c * a13 + d * a14;

                if (MathF.Abs(det) < float.Epsilon)
                    return _nan;

                var invDet = 1 / det;

                var gp_ho = g * p - h * o;
                var fp_hn = f * p - h * n;
                var fo_gn = f * o - g * n;
                var ep_hm = e * p - h * m;
                var eo_gm = e * o - g * m;
                var en_fm = e * n - f * m;

                var gl_hk = g * l - h * k;
                var fl_hj = f * l - h * j;
                var fk_gj = f * k - g * j;
                var el_hi = e * l - h * i;
                var ek_gi = e * k - g * i;
                var ej_fi = e * j - f * i;
                return new Matrix4x4F(m11: a11 * invDet,
                                      m21: a12 * invDet,
                                      m31: a13 * invDet,
                                      m41: a14 * invDet,
                                      m12: -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet,
                                      m22: (a * kp_lo - c * ip_lm + d * io_km) * invDet,
                                      m32: -(a * jp_ln - b * ip_lm + d * in_jm) * invDet,
                                      m42: (a * jo_kn - b * io_km + c * in_jm) * invDet,
                                      m13: (b * gp_ho - c * fp_hn + d * fo_gn) * invDet,
                                      m23: -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet,
                                      m33: (a * fp_hn - b * ep_hm + d * en_fm) * invDet,
                                      m43: -(a * fo_gn - b * eo_gm + c * en_fm) * invDet,
                                      m14: -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet,
                                      m24: (a * gl_hk - c * el_hi + d * ek_gi) * invDet,
                                      m34: -(a * fl_hj - b * el_hi + d * ej_fi) * invDet,
                                      m44: (a * fk_gj - b * ek_gi + c * ej_fi) * invDet);
            }
        }

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">
        /// The first source matrix.
        /// </param>
        /// <param name="matrix2">
        /// The second source matrix.
        /// </param>
        /// <param name="amount">
        /// The relative weight of the second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F Lerp(Matrix4x4F matrix1, Matrix4x4F matrix2, float amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (Sse.IsSupported)
            //{
            //    var amountVec = Vector128.Create(amount);

            //    Sse.Store(&result.M11, VectorMath.Lerp(Sse.LoadVector128(&m1.M11), Sse.LoadVector128(&m2.M11), amountVec));
            //    Sse.Store(&result.M21, VectorMath.Lerp(Sse.LoadVector128(&m1.M21), Sse.LoadVector128(&m2.M21), amountVec));
            //    Sse.Store(&result.M31, VectorMath.Lerp(Sse.LoadVector128(&m1.M31), Sse.LoadVector128(&m2.M31), amountVec));
            //    Sse.Store(&result.M41, VectorMath.Lerp(Sse.LoadVector128(&m1.M41), Sse.LoadVector128(&m2.M41), amountVec));

            //    return result;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    var amountVec = Vector128.Create(amount);

            //    AdvSimd.Store(&result.M11, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M11), AdvSimd.LoadVector128(&matrix2.M11), amountVec));
            //    AdvSimd.Store(&result.M21, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M21), AdvSimd.LoadVector128(&matrix2.M21), amountVec));
            //    AdvSimd.Store(&result.M31, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M31), AdvSimd.LoadVector128(&matrix2.M31), amountVec));
            //    AdvSimd.Store(&result.M41, VectorMath.Lerp(AdvSimd.LoadVector128(&matrix1.M41), AdvSimd.LoadVector128(&matrix2.M41), amountVec));

            //    return result;
            //}
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

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Multiply(Matrix4x4F left, Matrix4x4F right) =>
            left * right;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="left">
        /// The source matrix.
        /// </param>
        /// <param name="right">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Multiply(Matrix4x4F left, float right) =>
            left * right;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Negate(Matrix4x4F value) =>
            -value;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F operator -(Matrix4x4F value)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (Avx.IsSupported)
            //{
            //    var zero = Vector256<float>.Zero;

            //    Avx.Store(&result.M11, Avx.Subtract(zero, Avx.LoadVector256(&value.M11)));
            //    Avx.Store(&result.M31, Avx.Subtract(zero, Avx.LoadVector256(&value.M31)));

            //    return result;
            //}
            //else if (Sse.IsSupported)
            //{
            //    var zero = Vector128<float>.Zero;

            //    Sse.Store(&value.M11, Sse.Subtract(zero, Sse.LoadVector128(&value.M11)));
            //    Sse.Store(&value.M21, Sse.Subtract(zero, Sse.LoadVector128(&value.M21)));
            //    Sse.Store(&value.M31, Sse.Subtract(zero, Sse.LoadVector128(&value.M31)));
            //    Sse.Store(&value.M41, Sse.Subtract(zero, Sse.LoadVector128(&value.M41)));

            //    return value;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M11)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M21)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M31)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Negate(AdvSimd.LoadVector128(&value.M41)));

            //    return result;
            //}
            return new Matrix4x4F(-value.M11, -value.M12, -value.M13, -value.M14,
                                  -value.M21, -value.M22, -value.M23, -value.M24,
                                  -value.M31, -value.M32, -value.M33, -value.M34,
                                  -value.M41, -value.M42, -value.M43, -value.M44);
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F operator -(Matrix4x4F left, Matrix4x4F right)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (Avx.IsSupported)
            //{
            //    Avx.Store(&result.M11, Avx.Subtract(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
            //    Avx.Store(&result.M31, Avx.Subtract(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));

            //    return result;
            //}
            //else if (Sse.IsSupported)
            //{
            //    Sse.Store(&result.M11, Sse.Subtract(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
            //    Sse.Store(&result.M21, Sse.Subtract(Sse.LoadVector128(&left.M21), Sse.LoadVector128(&right.M21)));
            //    Sse.Store(&result.M31, Sse.Subtract(Sse.LoadVector128(&left.M31), Sse.LoadVector128(&right.M31)));
            //    Sse.Store(&result.M41, Sse.Subtract(Sse.LoadVector128(&left.M41), Sse.LoadVector128(&right.M41)));

            //    return result;
            //}
            //else if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Subtract(AdvSimd.LoadVector128(&left.M11), AdvSimd.LoadVector128(&right.M11)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Subtract(AdvSimd.LoadVector128(&left.M21), AdvSimd.LoadVector128(&right.M21)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Subtract(AdvSimd.LoadVector128(&left.M31), AdvSimd.LoadVector128(&right.M31)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Subtract(AdvSimd.LoadVector128(&left.M41), AdvSimd.LoadVector128(&right.M41)));

            //    return result;
            //}
            return new Matrix4x4F(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
                                  left.M21 - right.M21, left.M22 - right.M22, left.M23 - right.M23, left.M24 - right.M24,
                                  left.M31 - right.M31, left.M32 - right.M32, left.M33 - right.M33, left.M34 - right.M34,
                                  left.M41 - right.M41, left.M42 - right.M42, left.M43 - right.M43, left.M44 - right.M44);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are not equal.
        /// </summary>
        /// <param name="value1">
        /// The first matrix to compare.
        /// </param>
        /// <param name="value2">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are not equal; False if they are equal.
        /// </returns>
        [MethodImpl(Optimize)]
        public unsafe static bool operator !=(Matrix4x4F value1, Matrix4x4F value2)
        {
            //if (AdvSimd.IsSupported)
            //    return VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41));

            //else if (Avx.IsSupported)
            //    return VectorMath.NotEqual(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) ||
            //           VectorMath.NotEqual(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31));

            //else if (Sse.IsSupported)
            //    return VectorMath.NotEqual(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) ||
            //           VectorMath.NotEqual(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) ||
            //           VectorMath.NotEqual(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)) ||
            //           VectorMath.NotEqual(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));

            //else
                return value1.M11 != value2.M11 || value1.M22 != value2.M22 ||
                       value1.M33 != value2.M33 || value1.M44 != value2.M44 ||
                       value1.M12 != value2.M12 || value1.M13 != value2.M13 ||
                       value1.M14 != value2.M14 || value1.M21 != value2.M21 ||
                       value1.M23 != value2.M23 || value1.M24 != value2.M24 ||
                       value1.M31 != value2.M31 || value1.M32 != value2.M32 ||
                       value1.M34 != value2.M34 || value1.M41 != value2.M41 ||
                       value1.M42 != value2.M42 || value1.M43 != value2.M43;
        }

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">
        /// The first source matrix.
        /// </param>
        /// <param name="value2">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F operator *(Matrix4x4F value1, Matrix4x4F value2)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (Sse.IsSupported)
            //{
            //    Sse.Store(&result.M11, MultiplyRow(value2, Sse.LoadVector128(&value1.M11)));
            //    Sse.Store(&result.M21, MultiplyRow(value2, Sse.LoadVector128(&value1.M21)));
            //    Sse.Store(&result.M31, MultiplyRow(value2, Sse.LoadVector128(&value1.M31)));
            //    Sse.Store(&result.M41, MultiplyRow(value2, Sse.LoadVector128(&value1.M41)));
            //    return result;
            //}
            //else if (AdvSimd.Arm64.IsSupported)
            //{
            //    var M11 = AdvSimd.LoadVector128(&value1.M11);

            //    var vX = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), M11, 0);
            //    var vY = AdvSimd.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M21), M11, 1);
            //    var vZ = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX, AdvSimd.LoadVector128(&value2.M31), M11, 2);
            //    var vW = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY, AdvSimd.LoadVector128(&value2.M41), M11, 3);

            //    AdvSimd.Store(&result.M11, AdvSimd.Add(vZ, vW));
            //}
            return new Matrix4x4F(value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41,
                                  value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42,
                                  value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43,
                                  value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44,
                                  value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41,
                                  value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42,
                                  value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43,
                                  value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44,
                                  value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41,
                                  value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42,
                                  value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43,
                                  value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44,
                                  value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41,
                                  value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42,
                                  value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43,
                                  value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44);
        }

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="value1">
        /// The source matrix.
        /// </param>
        /// <param name="value2">
        /// The scaling factor.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F operator *(Matrix4x4F value1, float value2)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (AdvSimd.IsSupported)
            //{
            //    var right = Vector128.Create(value2);
            //    AdvSimd.Store(&result.M11, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M11), right));
            //    AdvSimd.Store(&result.M21, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M21), right));
            //    AdvSimd.Store(&result.M31, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M31), right));
            //    AdvSimd.Store(&result.M41, AdvSimd.Multiply(AdvSimd.LoadVector128(&value1.M41), right));

            //    return result;
            //}
            //else if (Avx.IsSupported)
            //{
            //    var right = Vector256.Create(value2);

            //    Avx.Store(&result.M11, Avx.Multiply(Avx.LoadVector256(&value1.M11), right));
            //    Avx.Store(&result.M31, Avx.Multiply(Avx.LoadVector256(&value1.M31), right));

            //    return result;
            //}
            //else if (Sse.IsSupported)
            //{
            //    Vector128<float> right = Vector128.Create(value2);
            //    Sse.Store(&result.M11, Sse.Multiply(Sse.LoadVector128(&value1.M11), right));
            //    Sse.Store(&result.M21, Sse.Multiply(Sse.LoadVector128(&value1.M21), right));
            //    Sse.Store(&result.M31, Sse.Multiply(Sse.LoadVector128(&value1.M31), right));
            //    Sse.Store(&result.M41, Sse.Multiply(Sse.LoadVector128(&value1.M41), right));

            //    return result;
            //}
            //else
            {
                return new Matrix4x4F(value1.M11 * value2,
                                      value1.M12 * value2,
                                      value1.M13 * value2,
                                      value1.M14 * value2,
                                      value1.M21 * value2,
                                      value1.M22 * value2,
                                      value1.M23 * value2,
                                      value1.M24 * value2,
                                      value1.M31 * value2,
                                      value1.M32 * value2,
                                      value1.M33 * value2,
                                      value1.M34 * value2,
                                      value1.M41 * value2,
                                      value1.M42 * value2,
                                      value1.M43 * value2,
                                      value1.M44 * value2);
            }
        }

        /// <summary>
        /// Returns itself.
        /// </summary>
        [MethodImpl(Optimize)]
        public static Matrix4x4F operator +(Matrix4x4F value) =>
            value;

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F operator +(Matrix4x4F left, Matrix4x4F right)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (AdvSimd.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Add(AdvSimd.LoadVector128(&left.M11), AdvSimd.LoadVector128(&right.M11)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Add(AdvSimd.LoadVector128(&left.M21), AdvSimd.LoadVector128(&right.M21)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Add(AdvSimd.LoadVector128(&left.M31), AdvSimd.LoadVector128(&right.M31)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Add(AdvSimd.LoadVector128(&left.M41), AdvSimd.LoadVector128(&right.M41)));

            //    return result;
            //}
            //else if (Avx.IsSupported)
            //{
            //    Avx.Store(&result.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
            //    Avx.Store(&result.M31, Avx.Add(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));

            //    return result;
            //}
            //else if(Sse.IsSupported)
            //{
            //    Sse.Store(&result.M11, Sse.Add(Sse.LoadVector128(&left.M11), Sse.LoadVector128(&right.M11)));
            //    Sse.Store(&result.M21, Sse.Add(Sse.LoadVector128(&left.M21), Sse.LoadVector128(&right.M21)));
            //    Sse.Store(&result.M31, Sse.Add(Sse.LoadVector128(&left.M31), Sse.LoadVector128(&right.M31)));
            //    Sse.Store(&result.M41, Sse.Add(Sse.LoadVector128(&left.M41), Sse.LoadVector128(&right.M41)));

            //    return result;
            //}
            //else
                return new Matrix4x4F(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
                                      left.M21 + right.M21, left.M22 + right.M22, left.M23 + right.M23, left.M24 + right.M24,
                                      left.M31 + right.M31, left.M32 + right.M32, left.M33 + right.M33, left.M34 + right.M34,
                                      left.M41 + right.M41, left.M42 + right.M42, left.M43 + right.M43, left.M44 + right.M44);
        }

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are equal.
        /// </summary>
        /// <param name="value1">
        /// The first matrix to compare.
        /// </param>
        /// <param name="value2">
        /// The second matrix to compare.
        /// </param>
        /// <returns>
        /// True if the given matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public unsafe static bool operator ==(Matrix4x4F value1, Matrix4x4F value2)
        {
            //if (AdvSimd.IsSupported)
            //    return VectorMath.Equal(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41));

            //else if (Avx.IsSupported)
            //    return VectorMath.Equal(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) &&
            //           VectorMath.Equal(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31));

            //else if (Sse.IsSupported)
            //    return VectorMath.Equal(Sse.LoadVector128(&value1.M11), Sse.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M21), Sse.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M31), Sse.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(Sse.LoadVector128(&value1.M41), Sse.LoadVector128(&value2.M41));

            //else
                return value1.M11 == value2.M11 && value1.M22 == value2.M22 &&
                       value1.M33 == value2.M33 && value1.M44 == value2.M44 &&
                       value1.M12 == value2.M12 && value1.M13 == value2.M13 &&
                       value1.M14 == value2.M14 && value1.M21 == value2.M21 &&
                       value1.M23 == value2.M23 && value1.M24 == value2.M24 &&
                       value1.M31 == value2.M31 && value1.M32 == value2.M32 &&
                       value1.M34 == value2.M34 && value1.M41 == value2.M41 &&
                       value1.M42 == value2.M42 && value1.M43 == value2.M43;
        }

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="left">
        /// The first source matrix.
        /// </param>
        /// <param name="right">
        /// The second source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Subtract(Matrix4x4F left, Matrix4x4F right) =>
            left - right;

        /// <summary>
        /// Transforms the given matrix by applying the given Quaternion rotation
        /// </summary>
        /// <param name="value">
        /// The source matrix to transform.
        /// </param>
        /// <param name="rotation">
        /// The rotation to apply.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4F Transform(Matrix4x4F value, QuaternionF rotation) =>
            value.Transform(rotation);

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4F Transpose(Matrix4x4F matrix)
        {
            //Unsafe.SkipInit<Matrix4x4F>(out var result);

            //if (AdvSimd.Arm64.IsSupported)
            //{
            //    // This implementation is based on the DirectX Math Library XMMatrixTranspose method
            //    // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathMatrix.inl

            //    var M11 = AdvSimd.LoadVector128(&matrix.M11);
            //    var M31 = AdvSimd.LoadVector128(&matrix.M31);

            //    var P00 = AdvSimd.Arm64.ZipLow(M11, M31);
            //    var P01 = AdvSimd.Arm64.ZipHigh(M11, M31);

            //    var M21 = AdvSimd.LoadVector128(&matrix.M21);
            //    var M41 = AdvSimd.LoadVector128(&matrix.M41);

            //    var P10 = AdvSimd.Arm64.ZipLow(M21, M41);
            //    var P11 = AdvSimd.Arm64.ZipHigh(M21, M41);

            //    AdvSimd.Store(&result.M11, AdvSimd.Arm64.ZipLow(P00, P10));
            //    AdvSimd.Store(&result.M21, AdvSimd.Arm64.ZipHigh(P00, P10));
            //    AdvSimd.Store(&result.M31, AdvSimd.Arm64.ZipLow(P01, P11));
            //    AdvSimd.Store(&result.M41, AdvSimd.Arm64.ZipHigh(P01, P11));

            //    return result;
            //}
            //else if (Sse.IsSupported)
            //{
            //    var row1 = Sse.LoadVector128(&matrix.M11);
            //    var row2 = Sse.LoadVector128(&matrix.M21);
            //    var row3 = Sse.LoadVector128(&matrix.M31);
            //    var row4 = Sse.LoadVector128(&matrix.M41);

            //    var l12 = Sse.UnpackLow(row1, row2);
            //    var l34 = Sse.UnpackLow(row3, row4);
            //    var h12 = Sse.UnpackHigh(row1, row2);
            //    var h34 = Sse.UnpackHigh(row3, row4);

            //    Sse.Store(&result.M11, Sse.MoveLowToHigh(l12, l34));
            //    Sse.Store(&result.M21, Sse.MoveHighToLow(l34, l12));
            //    Sse.Store(&result.M31, Sse.MoveLowToHigh(h12, h34));
            //    Sse.Store(&result.M41, Sse.MoveHighToLow(h34, h12));

            //    return result;
            //}
            //else
                return new Matrix4x4F(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                                      matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                                      matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                                      matrix.M14, matrix.M24, matrix.M34, matrix.M44);
        }

        /// <summary>
        /// Extracts the scale, translation, and rotation components from this scale/rotation/translation matrix.
        /// </summary>
        /// <param name="scale">
        /// The scaling component of this transformation matrix.
        /// </param>
        /// <param name="rotation">
        /// The rotation component of this transformation matrix.
        /// </param>
        /// <param name="translation">
        /// The translation component of the transformation matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe void Deconstruct(out Vector3F scale, out QuaternionF rotation, out Vector3F translation)
        {
            fixed (Vector3F* scaleBase = &scale)
            {
                var pfScales = (float*)scaleBase;

                VectorBasis vectorBasis;
                var pVectorBasis = (Vector3F**)&vectorBasis;

                var matTemp = _identity;
                var canonicalBasis = default(CanonicalBasis);
                var pCanonicalBasis = &canonicalBasis.Row0;

                canonicalBasis.Row0 = Vector3F.UnitX;
                canonicalBasis.Row1 = Vector3F.UnitY;
                canonicalBasis.Row2 = Vector3F.UnitZ;

                translation = new Vector3F(M41, M42, M43);

                pVectorBasis[0] = (Vector3F*)&matTemp.M11;
                pVectorBasis[1] = (Vector3F*)&matTemp.M21;
                pVectorBasis[2] = (Vector3F*)&matTemp.M31;

                *pVectorBasis[0] = new Vector3F(M11, M12, M13);
                *pVectorBasis[1] = new Vector3F(M21, M22, M23);
                *pVectorBasis[2] = new Vector3F(M31, M32, M33);

                scale.X = pVectorBasis[0]->Length();
                scale.Y = pVectorBasis[1]->Length();
                scale.Z = pVectorBasis[2]->Length();

                (var x, var y, var z) = (pfScales[0], pfScales[1], pfScales[2]);
                (uint a, uint b, uint c) =
                    (x < y) ?
                        (y < z) ?
                            (2u, 1u, 0u) :
                            (x < z) ?
                                (1u, 2u, 0u) :
                                (1u, 0u, 2u) :
                        (x < z) ?
                            (2u, 0u, 1u) :
                            (y < z) ?
                                (0u, 2u, 1u) :
                                (0u, 1u, 2u);

                if (pfScales[a] < 0.0001f)
                    *pVectorBasis[a] = pCanonicalBasis[a];

                *pVectorBasis[a] = pVectorBasis[a]->Normalize();

                if (pfScales[b] < 0.0001f)
                {
                    var fAbsX = MathF.Abs(pVectorBasis[a]->X);
                    var fAbsY = MathF.Abs(pVectorBasis[a]->Y);
                    var fAbsZ = MathF.Abs(pVectorBasis[a]->Z);

                    var cc =
                        (fAbsX < fAbsY) ?
                            (fAbsY < fAbsZ) ?
                                0u :
                                (fAbsX < fAbsZ) ?
                                    0u :
                                    2u :
                            (fAbsX < fAbsZ) ?
                                1u :
                                (fAbsY < fAbsZ) ?
                                    1u :
                                    2u;

                    *pVectorBasis[b] = pVectorBasis[a]->Cross(pCanonicalBasis[cc]);
                }

                *pVectorBasis[b] = pVectorBasis[b]->Normalize();

                if (pfScales[c] < 0.0001f)
                    *pVectorBasis[c] = pVectorBasis[a]->Cross(*pVectorBasis[b]);

                *pVectorBasis[c] = pVectorBasis[c]->Normalize();

                var det = matTemp.GetDeterminant();

                // use Kramer's rule to check for handedness of coordinate system
                if (det < 0)
                {
                    // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
                    pfScales[a] = -pfScales[a];
                    *pVectorBasis[a] = -*pVectorBasis[a];

                    det = -det;
                }

                det -= 1;
                det *= det;

                if (0.0001f < det)
                {
                    // Non-SRT matrix encountered
                    rotation = QuaternionF.Identity;
                    scale = translation = Vector3F.Zero;
                }
                else
                {
                    // generate the quaternion from the matrix
                    rotation = QuaternionF.CreateFromRotationMatrix(matTemp);
                }
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
        /// </summary>
        /// <param name="other">
        /// The matrix to compare this instance to.
        /// </param>
        /// <returns>
        /// True if the matrices are equal; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public readonly bool Equals(Matrix4x4F other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">
        /// The Object to compare against.
        /// </param>
        /// <returns>
        /// True if the Object is equal to this matrix; False otherwise.
        /// </returns>
        [MethodImpl(Optimize)]
        public override readonly bool Equals(object? obj)
        {
            if (obj is Matrix4x4F value)
            {
                return this == value;
            }
            return false;
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        public float GetDeterminant()
        {
            /*
             *  ⎡  a  b  c  d  ⎤       ⎡  f  g  h  ⎤       ⎡  e  g  h  ⎤       ⎡  e  f  h  ⎤       ⎡  e  f  g  ⎤
             *  ⎢  e  f  g  h  ⎥ = a ∙ ⎢  j  k  l  ⎥ - b ∙ ⎢  i  k  l  ⎥ + c ∙ ⎢  i  j  l  ⎥ - d ∙ ⎢  i  j  k  ⎥
             *  ⎢  i  j  k  l  ⎥       ⎣  n  o  p  ⎦       ⎣  m  o  p  ⎦       ⎣  m  n  p  ⎦       ⎣  m  n  o  ⎦
             *  ⎣  m  n  o  p  ⎦
             *
             *     ⎡  f  g  h  ⎤       ⎛                                                     ⎞
             * a ∙ ⎢  j  k  l  ⎥ = a ∙ ⎜ f ∙ ( kp - lo ) - g ∙ ( jp - ln ) + h ∙ ( jo - kn ) ⎟
             *     ⎣  n  o  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  g  h  ⎤       ⎛                                                     ⎞
             * b ∙ ⎢  i  k  l  ⎥ = b ∙ ⎜ e ∙ ( kp - lo ) - g ∙ ( ip - lm ) + h ∙ ( io - km ) ⎟
             *     ⎣  m  o  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  f  h  ⎤       ⎛                                                     ⎞
             * c ∙ ⎢  i  j  l  ⎥ = c ∙ ⎜ e ∙ ( jp - ln ) - f ∙ ( ip - lm ) + h ∙ ( in - jm ) ⎟
             *     ⎣  m  n  p  ⎦       ⎝                                                     ⎠
             *
             *     ⎡  e  f  g  ⎤       ⎛                                                     ⎞
             * d ∙ ⎢  i  j  k  ⎥ = d ∙ ⎜ e ∙ ( jo - kn ) - f ∙ ( io - km ) + g ∙ ( in - jm ) ⎟
             *     ⎣  m  n  o  ⎦       ⎝                                                     ⎠
             *
             * Cost of operation
             * 17 adds and 28 muls.
             *
             * add: 6 + 8 + 3 = 17
             * mul: 12 + 16 = 28
             */
            var a = M11;
            var b = M12;
            var c = M13;
            var d = M14;
            var e = M21;
            var f = M22;
            var g = M23;
            var h = M24;
            var i = M31;
            var j = M32;
            var k = M33;
            var l = M34;
            var m = M41;
            var n = M42;
            var o = M43;
            var p = M44;

            var kp_lo = k * p - l * o;
            var jp_ln = j * p - l * n;
            var jo_kn = j * o - k * n;
            var ip_lm = i * p - l * m;
            var io_km = i * o - k * m;
            var in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                   b * (e * kp_lo - g * ip_lm + h * io_km) +
                   c * (e * jp_ln - f * ip_lm + h * in_jm) -
                   d * (e * jo_kn - f * io_km + g * in_jm);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
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

        /// <summary>
        /// Attempts to calculate the inverse of this matrix. If successful, the inverted matrix will be returned.
        /// </summary>
        /// <returns>
        /// The inverted matrix or a NaN matrix if the inverse could not be calculated.
        /// </returns>
        [MethodImpl(Optimize)]
        public Matrix4x4F Invert() =>
            Invert(this);

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        public override readonly string ToString() =>
            $"{{ {{M11:{M11} M12:{M12} M13:{M13} M14:{M14}}} {{M21:{M21} M22:{M22} M23:{M23} M24:{M24}}} {{M31:{M31} M32:{M32} M33:{M33} M34:{M34}}} {{M41:{M41} M42:{M42} M43:{M43} M44:{M44}}} }}";

        /// <summary>
        /// Transforms this matrix by applying the given Quaternion rotation
        /// </summary>
        /// <param name="rotation">
        /// The rotation to apply.
        /// </param>
        [MethodImpl(Optimize)]
        public Matrix4x4F Transform(QuaternionF rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;

            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;

            var q11 = 1 - yy2 - zz2;
            var q21 = xy2 - wz2;
            var q31 = xz2 + wy2;

            var q12 = xy2 + wz2;
            var q22 = 1 - xx2 - zz2;
            var q32 = yz2 - wx2;

            var q13 = xz2 - wy2;
            var q23 = yz2 + wx2;
            var q33 = 1 - xx2 - yy2;

            return new Matrix4x4F(M11 * q11 + M12 * q21 + M13 * q31,
                                  M11 * q12 + M12 * q22 + M13 * q32,
                                  M11 * q13 + M12 * q23 + M13 * q33,
                                  M14,
                                  M21 * q11 + M22 * q21 + M23 * q31,
                                  M21 * q12 + M22 * q22 + M23 * q32,
                                  M21 * q13 + M22 * q23 + M23 * q33,
                                  M24,
                                  M31 * q11 + M32 * q21 + M33 * q31,
                                  M31 * q12 + M32 * q22 + M33 * q32,
                                  M31 * q13 + M32 * q23 + M33 * q33,
                                  M34,
                                  M41 * q11 + M42 * q21 + M43 * q31,
                                  M41 * q12 + M42 * q22 + M43 * q32,
                                  M41 * q13 + M42 * q23 + M43 * q33,
                                  M44);
        }

        /// <summary>
        /// Transposes the rows and columns of this matrix.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4F Transpose() =>
            Transpose(this);

        /// <summary>
        /// Provides a record-style <see langword="with"/>-like constructor.
        /// </summary>
        [MethodImpl(Optimize)]
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

        #endregion Public Methods

        #region Internal Methods

        internal Matrix3x3F As3x3() =>
            new Matrix3x3F(M11, M12, M13,
                           M21, M22, M23,
                           M31, M32, M33);

        #endregion Internal Methods

        #region Private Methods

        //[MethodImpl(Optimize)]
        //private static unsafe Vector128<float> MultiplyRow(Matrix4x4F value2, Vector128<float> vector)
        //{
        //    return Sse.Add(Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0x00),
        //                                        Sse.LoadVector128(&value2.M11)),
        //                           Sse.Multiply(Sse.Shuffle(vector, vector, 0x55),
        //                                        Sse.LoadVector128(&value2.M21))),
        //                   Sse.Add(Sse.Multiply(Sse.Shuffle(vector, vector, 0xAA),
        //                                        Sse.LoadVector128(&value2.M31)),
        //                           Sse.Multiply(Sse.Shuffle(vector, vector, 0xFF),
        //                                        Sse.LoadVector128(&value2.M41))));
        //}

        //[MethodImpl(Optimize)]
        //private static Vector128<float> Permute(Vector128<float> value, byte control) =>
        //    Avx.IsSupported ? Avx.Permute(value, control) : Sse.Shuffle(value, value, control);

        #endregion Private Methods

        #region Private Structs

        private struct CanonicalBasis
        {
            #region Public Fields

            public Vector3F Row0;
            public Vector3F Row1;
            public Vector3F Row2;

            #endregion Public Fields
        }

        private struct VectorBasis
        {
            #region Public Fields

            public unsafe Vector3F* Element0;
            public unsafe Vector3F* Element1;
            public unsafe Vector3F* Element2;

            #endregion Public Fields
        }

        #endregion Private Structs

        #region Private Classes

        //private static class VectorMath
        //{
        //    #region Public Methods

        //    [MethodImpl(Optimize)]
        //    public static bool Equal(Vector256<float> a, Vector256<float> b)
        //    {
        //        if (Avx.IsSupported)
        //            return Avx.MoveMask(Avx.CompareNotEqual(a, b)) == 0;
        //        else
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool Equal(Vector128<float> a, Vector128<float> b)
        //    {
        //        // This implementation is based on the DirectX Math Library XMVector4Equal method
        //        // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

        //        if (AdvSimd.Arm64.IsSupported)
        //        {
        //            var vResult = AdvSimd.CompareEqual(a, b).AsUInt32();

        //            var vResult0 = vResult.GetLower().AsByte();
        //            var vResult1 = vResult.GetUpper().AsByte();

        //            var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

        //            var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
        //            return vTemp21.AsUInt32().GetElement(1) == 0xFFFFFFFF;
        //        }

        //        else if (Sse.IsSupported)
        //            return Sse.MoveMask(Sse.CompareNotEqual(a, b)) == 0;

        //        else
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static Vector128<float> Lerp(Vector128<float> a, Vector128<float> b, Vector128<float> t)
        //    {
        //        // This implementation is based on the DirectX Math Library XMVectorLerp method
        //        // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

        //        if (AdvSimd.IsSupported)
        //            return AdvSimd.FusedMultiplyAdd(a, AdvSimd.Subtract(b, a), t);

        //        else if (Fma.IsSupported)
        //            return Fma.MultiplyAdd(Sse.Subtract(b, a), t, a);

        //        else if (Sse.IsSupported)
        //            return Sse.Add(a, Sse.Multiply(Sse.Subtract(b, a), t));

        //        else
        //            // Redundant test so we won't preJIT remainder of this method on platforms without SIMD.
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool NotEqual(Vector128<float> a, Vector128<float> b)
        //    {
        //        // This implementation is based on the DirectX Math Library XMVector4Equal method
        //        // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

        //        if (AdvSimd.Arm64.IsSupported)
        //        {
        //            var vResult = AdvSimd.CompareEqual(a, b).AsUInt32();

        //            var vResult0 = vResult.GetLower().AsByte();
        //            var vResult1 = vResult.GetUpper().AsByte();

        //            var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

        //            var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
        //            return vTemp21.AsUInt32().GetElement(1) != 0xFFFFFFFF;
        //        }

        //        else if (Sse.IsSupported)
        //            return Sse.MoveMask(Sse.CompareNotEqual(a, b)) != 0;

        //        else
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool NotEqual(Vector256<float> a, Vector256<float> b)
        //    {
        //        if (Avx.IsSupported)
        //            return Avx.MoveMask(Avx.CompareNotEqual(a, b)) != 0;
        //        else
        //            throw new PlatformNotSupportedException();
        //    }

        //    #endregion Public Methods
        //}

        #endregion Private Classes
    }
}