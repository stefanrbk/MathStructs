using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
//using System.Runtime.Intrinsics;
//using System.Runtime.Intrinsics.Arm;
//using System.Runtime.Intrinsics.X86;

namespace System.Numerics
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix of <see cref="Double"/> values.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8)]
    public struct Matrix4x4D
    {
        #region Public Fields

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [FieldOffset(0)]
        public double M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [FieldOffset(8)]
        public double M12;

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [FieldOffset(16)]
        public double M13;

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        [FieldOffset(24)]
        public double M14;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [FieldOffset(32)]
        public double M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [FieldOffset(40)]
        public double M22;

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [FieldOffset(48)]
        public double M23;

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        [FieldOffset(56)]
        public double M24;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [FieldOffset(64)]
        public double M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [FieldOffset(72)]
        public double M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [FieldOffset(80)]
        public double M33;

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        [FieldOffset(88)]
        public double M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        [FieldOffset(96)]
        public double M41;

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        [FieldOffset(104)]
        public double M42;

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        [FieldOffset(112)]
        public double M43;

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        [FieldOffset(120)]
        public double M44;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a <see cref="Matrix4x4D"/> from the given components.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4D(double m11, double m12, double m13, double m14,
                          double m21, double m22, double m23, double m24,
                          double m31, double m32, double m33, double m34,
                          double m41, double m42, double m43, double m44)
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
        /// Constructs a Matrix4x4D from the given <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="value">
        /// The source <see cref="Matrix3x3D"/>.
        /// </param>
        [MethodImpl(Optimize)]
        public Matrix4x4D(Matrix3x3D value)
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
        public static Matrix4x4D Identity => new(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

        /// <summary>
        /// Returns a matrix with all values set to NaN.
        /// </summary>
        public static Matrix4x4D NaN => new(Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN, Double.NaN);

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        public bool IsIdentity =>
            this == Identity;

        /// <summary>
        /// Gets or sets the translation component of this matrix.
        /// </summary>
        public Vector3D Translation
        {
            get => new(M41, M42, M43);
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
        public static Matrix4x4D Add(Matrix4x4D left, Matrix4x4D right) =>
            left + right;

        /// <summary>
        ///     Copies the contents of the matrix into the given span.
        /// </summary>
        [MethodImpl(Optimize)]
        public void CopyTo(Span<double> span) =>
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
        public void CopyTo(Span<double> span, int index)
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
        public static Matrix4x4D CreateBillboard(Vector3D objectPosition, Vector3D cameraPosition, Vector3D cameraUpVector, Vector3D cameraForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = ( !( num < 0.0001f ) ) ? left * ( 1f / Math.Sqrt(num) ) : -cameraForwardVector;
            var v1 = cameraUpVector.Cross(left).Normalize();
            var v2 = left.Cross(v1);
            return Identity.With(m11: v1.X,
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
        public static Matrix4x4D CreateConstrainedBillboard(Vector3D objectPosition, Vector3D cameraPosition, Vector3D rotateAxis, Vector3D cameraForwardVector, Vector3D objectForwardVector)
        {
            var left = objectPosition - cameraPosition;
            var num = left.LengthSquared();
            left = ( !( num < 0.0001f ) ) ? left * ( 1f / Math.Sqrt(num) ) : -cameraForwardVector;
            Vector3D v1 = rotateAxis, v2, v3;
            var x = rotateAxis.Dot(left);
            if (Math.Abs(x) > 0.998254657f)
            {
                v2 = objectForwardVector;
                x = rotateAxis.Dot(v2);
                if (Math.Abs(x) > 0.998254657f)
                    v2 = ( Math.Abs(rotateAxis.Z) > 0.998254657f ) ? Vector3D.UnitX : -Vector3D.UnitZ;
                v3 = rotateAxis.Cross(v2).Normalize();
                v2 = v3.Cross(rotateAxis).Normalize();
            }
            else
            {
                v3 = rotateAxis.Cross(left).Normalize();
                v2 = v3.Cross(v1).Normalize();
            }
            return Identity.With(m11: v3.X,
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
        public static Matrix4x4D CreateFromAxisAngle(Vector3D axis, double angle)
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
            var sa = Math.Sin(angle);
            var ca = Math.Cos(angle);
            (var xx, var yy, var zz) = (x * x, y * y, z * z);
            (var xy, var xz, var yz) = (x * y, x * z, y * z);

            return Identity.With(m11: xx + ( ca * ( 1 - xx ) ),
                                 m22: yy + ( ca * ( 1 - yy ) ),
                                 m33: zz + ( ca * ( 1 - zz ) ),

                                 m12: xy - ( ca * xy ) + ( sa * z ),
                                 m13: xz - ( ca * xz ) - ( sa * y ),

                                 m21: xy - ( ca * xy ) - ( sa * z ),
                                 m23: yz - ( ca * yz ) + ( sa * x ),

                                 m31: xz - ( ca * xz ) + ( sa * y ),
                                 m32: yz - ( ca * yz ) - ( sa * x ));
        }

        /// <summary>
        /// Creates a rotation matrix from the given <see cref="QuaternionD"/> rotation value.
        /// </summary>
        /// <param name="q">
        /// The source <see cref="QuaternionD"/>.
        /// </param>
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

            return Identity.With(m11: 1 - ( 2 * ( n2 + n3 ) ),
                                 m22: 1 - ( 2 * ( n3 + n1 ) ),
                                 m33: 1 - ( 2 * ( n2 + n1 ) ),

                                 m12: 2 * ( n4 + n5 ),
                                 m13: 2 * ( n6 - n7 ),
                                 m21: 2 * ( n4 - n5 ),
                                 m23: 2 * ( n8 + n9 ),
                                 m31: 2 * ( n6 + n7 ),
                                 m32: 2 * ( n8 - n9 ));
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
        public static Matrix4x4D CreateFromYawPitchRoll(double yaw, double pitch, double roll) =>
            CreateFromQuaternion(QuaternionD.CreateFromYawPitchRoll(yaw, pitch, roll));

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <param name="cameraPosition">
        /// The position of the camera.
        /// </param>
        /// <param name="cameraTarget">
        /// The target towards which the camera is pointing.
        /// </param>
        /// <param name="cameraUpVector"></param>
        public static Matrix4x4D CreateLookAt(Vector3D cameraPosition, Vector3D cameraTarget, Vector3D cameraUpVector)
        {
            var v1 = (cameraPosition - cameraTarget).Normalize();
            var v2 = cameraUpVector.Cross(v1).Normalize();
            var v3 = v1.Cross(v2);

            return Identity.With(m11:  v2.X,
                                 m12:  v3.X,
                                 m13:  v1.X,
                                 m21:  v2.Y,
                                 m22:  v3.Y,
                                 m23:  v1.Y,
                                 m31:  v2.Z,
                                 m32:  v3.Z,
                                 m33:  v1.Z,
                                 m41: -v2.Dot(cameraPosition),
                                 m42: -v3.Dot(cameraPosition),
                                 m43: -v1.Dot(cameraPosition));
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
        public static Matrix4x4D CreateOrthographic(double width, double height, double zNearPlane, double zFarPlane) =>
            Identity.With(m11: 2.0        / width,
                          m22: 2.0        / height,
                          m33: 1.0        / ( zNearPlane - zFarPlane ),
                          m43: zNearPlane / ( zNearPlane - zFarPlane ));

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
        public static Matrix4x4D CreateOrthographicOffCenter(double left, double right, double bottom, double top, double zNearPlane, double zFarPlane) =>
            Identity.With(m11: 2.0              / ( right      - left ),
                          m22: 2.0              / ( top        - bottom ),
                          m33: 1.0              / ( zNearPlane - zFarPlane ),
                          m41: ( left + right ) / ( left       - right ),
                          m42: ( top + bottom ) / ( bottom     - top ),
                          m43: zNearPlane       / ( zNearPlane - zFarPlane ));

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
        public static Matrix4x4D CreatePerspective(double width, double height, double nearPlaneDistance, double farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var negFarRange = Double.IsPositiveInfinity(farPlaneDistance) ? -1.0 : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return default(Matrix4x4D).With(m11: 2.0 * nearPlaneDistance / width,
                                            m22: 2.0 * nearPlaneDistance / height,
                                            m33: negFarRange,
                                            m34: -1.0,
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

            var yScale = 1 / Math.Tan(fieldOfView * 0.5);
            var xScale = yScale / aspectRatio;

            var negFarRange = Double.IsPositiveInfinity(farPlaneDistance) ? -1.0 : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return default(Matrix4x4D).With(m11: xScale,
                                            m22: yScale,
                                            m33: negFarRange,
                                            m34: -1,
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
        public static Matrix4x4D CreatePerspectiveOffCenter(double left, double right, double bottom, double top, double nearPlaneDistance, double farPlaneDistance)
        {
            if (nearPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            if (farPlaneDistance is <= 0)
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            if (nearPlaneDistance >= farPlaneDistance)
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));

            var negFarRange = Double.IsPositiveInfinity(farPlaneDistance) ? -1.0 : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance));
            return Identity.With(m11: 2.0 * nearPlaneDistance / ( right - left ),
                                 m22: 2.0 * nearPlaneDistance / ( top - bottom ),
                                 m31: ( left + right ) / ( right - left ),
                                 m32: ( top + bottom ) / ( top - bottom ),
                                 m33: negFarRange,
                                 m34: -1.0,
                                 m43: nearPlaneDistance * negFarRange,
                                 m44: 0);
        }

        /// <summary>
        /// Creates a Matrix that reflects the coordinate system about a specified Plane.
        /// </summary>
        /// <param name="value">
        /// The Plane about which to create a reflection.
        /// </param>
        public static Matrix4x4D CreateReflection(PlaneD value)
        {
            value = value.Normalize();
            var v1 = value.Normal;
            var v2 = -2 * v1;
            return Identity.With(m11: ( v2.X * v1.X ) + 1,
                                 m22: ( v2.Y * v1.Y ) + 1,
                                 m33: ( v2.Z * v1.Z ) + 1,
                                 m12:   v2.Y * v1.X,
                                 m13:   v2.Z * v1.X,
                                 m21:   v2.X * v1.Y,
                                 m23:   v2.Z * v1.Y,
                                 m31:   v2.X * v1.Z,
                                 m32:   v2.Y * v1.Z,
                                 m41:   v2.X * value.D,
                                 m42:   v2.Y * value.D,
                                 m43:   v2.Z * value.D);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis.
        /// </summary>
        /// <param name="radians">
        /// The amount, in radians, by which to rotate around the X-axis.
        /// </param>
        public static Matrix4x4D CreateRotationX(double radians)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);

            /*  [   1   0   0   0   ]
                [   0   c   s   0   ]
                [   0  -s   c   0   ]
                [   0   0   0   1   ] */

            return Identity.With(m22: c, m23: s, m32: -s, m33: c);
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
        public static Matrix4x4D CreateRotationX(double radians, Vector3D centerPoint)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);
            var y = (centerPoint.Y * (1 - c)) + (centerPoint.Z * s);
            var z = (centerPoint.Z * (1 - c)) - (centerPoint.Y * s);

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
        public static Matrix4x4D CreateRotationY(double radians)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);

            /*  [   c   0  -s   0   ]
                [   0   1   0   0   ]
                [   s   0   c   0   ]
                [   0   0   0   1   ] */

            return Identity.With(m11: c, m13: -s, m31: s, m33: c);
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
        public static Matrix4x4D CreateRotationY(double radians, Vector3D centerPoint)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);
            var x = (centerPoint.X * (1 - c)) - (centerPoint.Z * s);
            var z = (centerPoint.Z * (1 - c)) + (centerPoint.X * s);

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
        public static Matrix4x4D CreateRotationZ(double radians)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);

            /*  [   c   s   0   0   ]
                [  -s   c   0   0   ]
                [   0   0   1   0   ]
                [   0   0   0   1   ] */

            return Identity.With(m11: c, m12: s, m21: -s, m22: c);
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
        public static Matrix4x4D CreateRotationZ(double radians, Vector3D centerPoint)
        {
            var c = Math.Cos(radians);
            var s = Math.Sin(radians);
            var x = (centerPoint.X *(1 - c)) + (centerPoint.Y * s);
            var y = (centerPoint.Y *(1 - c)) - (centerPoint.X * s);

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
        public static Matrix4x4D CreateScale(double xScale, double yScale, double zScale) =>
            Identity.With(m11: xScale, m22: yScale, m33: zScale);

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
        public static Matrix4x4D CreateScale(double xScale, double yScale, double zScale, Vector3D centerPoint)
        {
            var m1 = centerPoint.X * (1 - xScale);
            var m2 = centerPoint.Y * (1 - yScale);
            var m3 = centerPoint.Z * (1 - zScale);
            return CreateScale(xScale, yScale, zScale).With(m41: m1, m42: m2, m43: m3);
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scale">
        /// The vector containing the amount to scale by on each axis.
        /// </param>
        public static Matrix4x4D CreateScale(Vector3D scale) =>
            CreateScale(scale.X, scale.Y, scale.Z);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="scale">The vector containing the amount to scale by on each axis.</param>
        /// <param name="centerPoint">
        /// The center point.
        /// </param>
        public static Matrix4x4D CreateScale(Vector3D scale, Vector3D centerPoint) =>
            CreateScale(scale.X, scale.Y, scale.Z, centerPoint);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis.
        /// </summary>
        /// <param name="scale">
        /// The uniform scaling factor.
        /// </param>
        public static Matrix4x4D CreateScale(double scale) =>
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
        public static Matrix4x4D CreateScale(double scale, Vector3D centerPoint) =>
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
        public static Matrix4x4D CreateShadow(Vector3D lightDirection, PlaneD plane)
        {
            plane = plane.Normalize();
            var n = plane.Normal.Dot(lightDirection);
            var v = -new Vector4D(plane.Normal, plane.D);
            return Identity.With(m11: ( v.X * lightDirection.X ) + n,
                                 m12:   v.X * lightDirection.Y,
                                 m13:   v.X * lightDirection.Z,
                                 m21:   v.Y * lightDirection.X,
                                 m22: ( v.Y * lightDirection.Y ) + n,
                                 m23:   v.Y * lightDirection.Z,
                                 m31:   v.Z * lightDirection.X,
                                 m32:   v.Z * lightDirection.Y,
                                 m33: ( v.Z * lightDirection.Z ) + n,
                                 m41:   v.W * lightDirection.X,
                                 m42:   v.W * lightDirection.Y,
                                 m43:   v.W * lightDirection.Z,
                                 m44: n);
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="position">
        /// The amount to translate in each axis.
        /// </param>
        public static Matrix4x4D CreateTranslation(Vector3D position) =>
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
        public static Matrix4x4D CreateTranslation(double xPosition, double yPosition, double zPosition) =>
            Identity.With(m41: xPosition, m42: yPosition, m43: zPosition);

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
        public static bool Decompose(Matrix4x4D matrix, out Vector3D scale, out QuaternionD rotation, out Vector3D translation)
        {
            (scale, rotation, translation) = matrix;
            return scale != Vector3D.Zero || translation != Vector3D.Zero || rotation != QuaternionD.Identity;
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
        public static Matrix4x4D Invert(Matrix4x4D matrix)
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

            var kp_lo = ( k * p ) - ( l * o );
            var jp_ln = ( j * p ) - ( l * n );
            var jo_kn = ( j * o ) - ( k * n );
            var ip_lm = ( i * p ) - ( l * m );
            var io_km = ( i * o ) - ( k * m );
            var in_jm = ( i * n ) - ( j * m );

            var a11 =   (f * kp_lo) - (g * jp_ln) + (h * jo_kn);
            var a12 = -((e * kp_lo) - (g * ip_lm) + (h * io_km));
            var a13 =   (e * jp_ln) - (f * ip_lm) + (h * in_jm);
            var a14 = -((e * jo_kn) - (f * io_km) + (g * in_jm));

            var det = (a * a11) + (b * a12) + (c * a13) + (d * a14);

            if (Math.Abs(det) < Double.Epsilon)
                return NaN;

            var invDet = 1 / det;

            var gp_ho = (g * p) -(h * o);
            var fp_hn = (f * p) -(h * n);
            var fo_gn = (f * o) -(g * n);
            var ep_hm = (e * p) -(h * m);
            var eo_gm = (e * o) -(g * m);
            var en_fm = (e * n) -(f * m);

            var gl_hk = (g * l) -(h * k);
            var fl_hj = (f * l) -(h * j);
            var fk_gj = (f * k) -(g * j);
            var el_hi = (e * l) -(h * i);
            var ek_gi = (e * k) -(g * i);
            var ej_fi = (e * j) -(f * i);
            return new(m11: a11 * invDet,
                       m21: a12 * invDet,
                       m31: a13 * invDet,
                       m41: a14 * invDet,
                       m12: -( ( b * kp_lo ) - ( c * jp_ln ) + ( d * jo_kn ) ) * invDet,
                       m22:  ( ( a * kp_lo ) - ( c * ip_lm ) + ( d * io_km ) ) * invDet,
                       m32: -( ( a * jp_ln ) - ( b * ip_lm ) + ( d * in_jm ) ) * invDet,
                       m42:  ( ( a * jo_kn ) - ( b * io_km ) + ( c * in_jm ) ) * invDet,
                       m13:  ( ( b * gp_ho ) - ( c * fp_hn ) + ( d * fo_gn ) ) * invDet,
                       m23: -( ( a * gp_ho ) - ( c * ep_hm ) + ( d * eo_gm ) ) * invDet,
                       m33:  ( ( a * fp_hn ) - ( b * ep_hm ) + ( d * en_fm ) ) * invDet,
                       m43: -( ( a * fo_gn ) - ( b * eo_gm ) + ( c * en_fm ) ) * invDet,
                       m14: -( ( b * gl_hk ) - ( c * fl_hj ) + ( d * fk_gj ) ) * invDet,
                       m24:  ( ( a * gl_hk ) - ( c * el_hi ) + ( d * ek_gi ) ) * invDet,
                       m34: -( ( a * fl_hj ) - ( b * el_hi ) + ( d * ej_fi ) ) * invDet,
                       m44:  ( ( a * fk_gj ) - ( b * ek_gi ) + ( c * ej_fi ) ) * invDet);
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
        public unsafe static Matrix4x4D Lerp(Matrix4x4D matrix1, Matrix4x4D matrix2, double amount)
        {
            (var m1, var m2) = (matrix1, matrix2);
            //Unsafe.SkipInit(out Matrix4x4D result);
            //if (Avx.IsSupported)
            //{
            //    var amountVec = Vector256.Create(amount);

            //    Avx.Store(&result.M11, VectorMath.Lerp(Avx.LoadVector256(&m1.M11), Avx.LoadVector256(&m2.M11), amountVec));
            //    Avx.Store(&result.M21, VectorMath.Lerp(Avx.LoadVector256(&m1.M21), Avx.LoadVector256(&m2.M21), amountVec));
            //    Avx.Store(&result.M31, VectorMath.Lerp(Avx.LoadVector256(&m1.M31), Avx.LoadVector256(&m2.M31), amountVec));
            //    Avx.Store(&result.M41, VectorMath.Lerp(Avx.LoadVector256(&m1.M41), Avx.LoadVector256(&m2.M41), amountVec));

            //    return result;
            //}
            return new(m1.M11 + ( ( m2.M11 - m1.M11 ) * amount ),
                       m1.M12 + ( ( m2.M12 - m1.M12 ) * amount ),
                       m1.M13 + ( ( m2.M13 - m1.M13 ) * amount ),
                       m1.M14 + ( ( m2.M14 - m1.M14 ) * amount ),
                       m1.M21 + ( ( m2.M21 - m1.M21 ) * amount ),
                       m1.M22 + ( ( m2.M22 - m1.M22 ) * amount ),
                       m1.M23 + ( ( m2.M23 - m1.M23 ) * amount ),
                       m1.M24 + ( ( m2.M24 - m1.M24 ) * amount ),
                       m1.M31 + ( ( m2.M31 - m1.M31 ) * amount ),
                       m1.M32 + ( ( m2.M32 - m1.M32 ) * amount ),
                       m1.M33 + ( ( m2.M33 - m1.M33 ) * amount ),
                       m1.M34 + ( ( m2.M34 - m1.M34 ) * amount ),
                       m1.M41 + ( ( m2.M41 - m1.M41 ) * amount ),
                       m1.M42 + ( ( m2.M42 - m1.M42 ) * amount ),
                       m1.M43 + ( ( m2.M43 - m1.M43 ) * amount ),
                       m1.M44 + ( ( m2.M44 - m1.M44 ) * amount ));
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
        public static Matrix4x4D Multiply(Matrix4x4D left, Matrix4x4D right) =>
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
        public static Matrix4x4D Multiply(Matrix4x4D left, double right) =>
            left * right;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public static Matrix4x4D Negate(Matrix4x4D value) =>
            -value;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D operator -(Matrix4x4D value)
        {
            //Unsafe.SkipInit(out Matrix4x4D result);

            //if (Avx.IsSupported)
            //{
            //    var zero = Vector256<double>.Zero;

            //    Avx.Store(&result.M11, Avx.Subtract(zero, Avx.LoadVector256(&value.M11)));
            //    Avx.Store(&result.M21, Avx.Subtract(zero, Avx.LoadVector256(&value.M21)));
            //    Avx.Store(&result.M31, Avx.Subtract(zero, Avx.LoadVector256(&value.M31)));
            //    Avx.Store(&result.M41, Avx.Subtract(zero, Avx.LoadVector256(&value.M41)));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    var zero = Vector128<double>.Zero;

            //    Sse2.Store(&result.M11, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M11)));
            //    Sse2.Store(&result.M13, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M13)));
            //    Sse2.Store(&result.M21, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M21)));
            //    Sse2.Store(&result.M23, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M23)));
            //    Sse2.Store(&result.M31, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M31)));
            //    Sse2.Store(&result.M33, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M33)));
            //    Sse2.Store(&result.M41, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M41)));
            //    Sse2.Store(&result.M43, Sse2.Subtract(zero, Sse2.LoadVector128(&value.M43)));

            //    return result;
            //}
            //else if (AdvSimd.Arm64.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M11)));
            //    AdvSimd.Store(&result.M13, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M13)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M21)));
            //    AdvSimd.Store(&result.M23, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M23)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M31)));
            //    AdvSimd.Store(&result.M33, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M33)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M41)));
            //    AdvSimd.Store(&result.M43, AdvSimd.Arm64.Negate(AdvSimd.LoadVector128(&value.M43)));

            //    return result;
            //}
            //else
                return new(-value.M11, -value.M12, -value.M13, -value.M14,
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
        public unsafe static Matrix4x4D operator -(Matrix4x4D left, Matrix4x4D right)
        {
            Unsafe.SkipInit(out Matrix4x4D result);

            //if (Avx.IsSupported)
            //{
            //    Avx.Store(&result.M11, Avx.Subtract(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
            //    Avx.Store(&result.M21, Avx.Subtract(Avx.LoadVector256(&left.M21), Avx.LoadVector256(&right.M21)));
            //    Avx.Store(&result.M31, Avx.Subtract(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));
            //    Avx.Store(&result.M41, Avx.Subtract(Avx.LoadVector256(&left.M41), Avx.LoadVector256(&right.M41)));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    Sse2.Store(&result.M11, Sse2.Subtract(Sse2.LoadVector128(&left.M11), Sse2.LoadVector128(&right.M11)));
            //    Sse2.Store(&result.M13, Sse2.Subtract(Sse2.LoadVector128(&left.M13), Sse2.LoadVector128(&right.M13)));
            //    Sse2.Store(&result.M21, Sse2.Subtract(Sse2.LoadVector128(&left.M21), Sse2.LoadVector128(&right.M21)));
            //    Sse2.Store(&result.M23, Sse2.Subtract(Sse2.LoadVector128(&left.M23), Sse2.LoadVector128(&right.M23)));
            //    Sse2.Store(&result.M31, Sse2.Subtract(Sse2.LoadVector128(&left.M31), Sse2.LoadVector128(&right.M31)));
            //    Sse2.Store(&result.M33, Sse2.Subtract(Sse2.LoadVector128(&left.M33), Sse2.LoadVector128(&right.M33)));
            //    Sse2.Store(&result.M41, Sse2.Subtract(Sse2.LoadVector128(&left.M41), Sse2.LoadVector128(&right.M41)));
            //    Sse2.Store(&result.M43, Sse2.Subtract(Sse2.LoadVector128(&left.M43), Sse2.LoadVector128(&right.M43)));

            //    return result;
            //}
            //else if (AdvSimd.Arm64.IsSupported)
            //{
            //    AdvSimd.Store(&result.M11, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M11), AdvSimd.LoadVector128(&right.M11)));
            //    AdvSimd.Store(&result.M13, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M13), AdvSimd.LoadVector128(&right.M13)));
            //    AdvSimd.Store(&result.M21, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M21), AdvSimd.LoadVector128(&right.M21)));
            //    AdvSimd.Store(&result.M23, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M23), AdvSimd.LoadVector128(&right.M23)));
            //    AdvSimd.Store(&result.M31, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M31), AdvSimd.LoadVector128(&right.M31)));
            //    AdvSimd.Store(&result.M33, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M33), AdvSimd.LoadVector128(&right.M33)));
            //    AdvSimd.Store(&result.M41, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M41), AdvSimd.LoadVector128(&right.M41)));
            //    AdvSimd.Store(&result.M43, AdvSimd.Arm64.Subtract(AdvSimd.LoadVector128(&left.M43), AdvSimd.LoadVector128(&right.M43)));

            //    return result;
            //}
            return new(left.M11 - right.M11, left.M12 - right.M12, left.M13 - right.M13, left.M14 - right.M14,
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
        public unsafe static bool operator !=(Matrix4x4D value1, Matrix4x4D value2)
        {
            //if (AdvSimd.Arm64.IsSupported)
            //    return VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M13), AdvSimd.LoadVector128(&value2.M13)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M23), AdvSimd.LoadVector128(&value2.M23)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M33), AdvSimd.LoadVector128(&value2.M33)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41)) ||
            //           VectorMath.NotEqual(AdvSimd.LoadVector128(&value1.M43), AdvSimd.LoadVector128(&value2.M43));

            //else if (Avx.IsSupported)
            //    return VectorMath.NotEqual(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) ||
            //           VectorMath.NotEqual(Avx.LoadVector256(&value1.M21), Avx.LoadVector256(&value2.M21)) ||
            //           VectorMath.NotEqual(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31)) ||
            //           VectorMath.NotEqual(Avx.LoadVector256(&value1.M41), Avx.LoadVector256(&value2.M41));

            //else if (Sse2.IsSupported)
            //    return VectorMath.NotEqual(Sse2.LoadVector128(&value1.M11), Sse2.LoadVector128(&value2.M11)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M13), Sse2.LoadVector128(&value2.M13)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M21), Sse2.LoadVector128(&value2.M21)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M23), Sse2.LoadVector128(&value2.M23)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M31), Sse2.LoadVector128(&value2.M31)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M33), Sse2.LoadVector128(&value2.M33)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M41), Sse2.LoadVector128(&value2.M41)) ||
            //           VectorMath.NotEqual(Sse2.LoadVector128(&value1.M43), Sse2.LoadVector128(&value2.M43));

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
        public unsafe static Matrix4x4D operator *(Matrix4x4D value1, Matrix4x4D value2)
        {
            Unsafe.SkipInit(out Matrix4x4D result);

            //if (AdvSimd.Arm64.IsSupported)
            //{
            //    var M11 = AdvSimd.LoadVector128(&value1.M11);
            //    var M13 = AdvSimd.LoadVector128(&value1.M13);

            //    var vX1 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), M11, 0);
            //    var vX2 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M13), M11, 0);
            //    var vY1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX1, AdvSimd.LoadVector128(&value2.M21), M11, 1);
            //    var vY2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX2, AdvSimd.LoadVector128(&value2.M23), M11, 1);
            //    var vZ1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY1, AdvSimd.LoadVector128(&value2.M31), M13, 0);
            //    var vZ2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY2, AdvSimd.LoadVector128(&value2.M33), M13, 0);
            //    var vW1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ1, AdvSimd.LoadVector128(&value2.M41), M13, 1);
            //    var vW2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ2, AdvSimd.LoadVector128(&value2.M43), M13, 1);

            //    AdvSimd.Store(&result.M11, vW1);
            //    AdvSimd.Store(&result.M13, vW2);

            //    var M21 = AdvSimd.LoadVector128(&value1.M21);
            //    var M23 = AdvSimd.LoadVector128(&value1.M23);

            //    vX1 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), M21, 0);
            //    vX2 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M13), M21, 0);
            //    vY1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX1, AdvSimd.LoadVector128(&value2.M21), M21, 1);
            //    vY2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX2, AdvSimd.LoadVector128(&value2.M23), M21, 1);
            //    vZ1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY1, AdvSimd.LoadVector128(&value2.M31), M23, 0);
            //    vZ2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY2, AdvSimd.LoadVector128(&value2.M33), M23, 0);
            //    vW1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ1, AdvSimd.LoadVector128(&value2.M41), M23, 1);
            //    vW2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ2, AdvSimd.LoadVector128(&value2.M43), M23, 1);

            //    AdvSimd.Store(&result.M11, vW1);
            //    AdvSimd.Store(&result.M13, vW2);

            //    var M31 = AdvSimd.LoadVector128(&value1.M11);
            //    var M33 = AdvSimd.LoadVector128(&value1.M13);

            //    vX1 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), M31, 0);
            //    vX2 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M13), M31, 0);
            //    vY1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX1, AdvSimd.LoadVector128(&value2.M21), M31, 1);
            //    vY2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX2, AdvSimd.LoadVector128(&value2.M23), M31, 1);
            //    vZ1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY1, AdvSimd.LoadVector128(&value2.M31), M33, 0);
            //    vZ2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY2, AdvSimd.LoadVector128(&value2.M33), M33, 0);
            //    vW1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ1, AdvSimd.LoadVector128(&value2.M41), M33, 1);
            //    vW2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ2, AdvSimd.LoadVector128(&value2.M43), M33, 1);

            //    AdvSimd.Store(&result.M11, vW1);
            //    AdvSimd.Store(&result.M13, vW2);

            //    var M41 = AdvSimd.LoadVector128(&value1.M11);
            //    var M43 = AdvSimd.LoadVector128(&value1.M13);

            //    vX1 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M11), M41, 0);
            //    vX2 = AdvSimd.Arm64.MultiplyBySelectedScalar(AdvSimd.LoadVector128(&value2.M13), M41, 0);
            //    vY1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX1, AdvSimd.LoadVector128(&value2.M21), M41, 1);
            //    vY2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vX2, AdvSimd.LoadVector128(&value2.M23), M41, 1);
            //    vZ1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY1, AdvSimd.LoadVector128(&value2.M31), M43, 0);
            //    vZ2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vY2, AdvSimd.LoadVector128(&value2.M33), M43, 0);
            //    vW1 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ1, AdvSimd.LoadVector128(&value2.M41), M43, 1);
            //    vW2 = AdvSimd.Arm64.FusedMultiplyAddBySelectedScalar(vZ2, AdvSimd.LoadVector128(&value2.M43), M43, 1);

            //    AdvSimd.Store(&result.M11, vW1);
            //    AdvSimd.Store(&result.M13, vW2);

            //    return result;
            //}
            //else if (Avx.IsSupported)
            //{
            //    Avx.Store(&result.M11, MultiplyRow(value2, Avx.LoadVector256(&value1.M11)));
            //    Avx.Store(&result.M21, MultiplyRow(value2, Avx.LoadVector256(&value1.M21)));
            //    Avx.Store(&result.M31, MultiplyRow(value2, Avx.LoadVector256(&value1.M31)));
            //    Avx.Store(&result.M41, MultiplyRow(value2, Avx.LoadVector256(&value1.M41)));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    Sse2.Store(&result.M11, MultiplyRowXY(value2, Sse2.LoadVector128(&value1.M11), Sse2.LoadVector128(&value1.M13)));
            //    Sse2.Store(&result.M13, MultiplyRowZW(value2, Sse2.LoadVector128(&value1.M11), Sse2.LoadVector128(&value1.M13)));
            //    Sse2.Store(&result.M21, MultiplyRowXY(value2, Sse2.LoadVector128(&value1.M21), Sse2.LoadVector128(&value1.M23)));
            //    Sse2.Store(&result.M23, MultiplyRowZW(value2, Sse2.LoadVector128(&value1.M21), Sse2.LoadVector128(&value1.M23)));
            //    Sse2.Store(&result.M31, MultiplyRowXY(value2, Sse2.LoadVector128(&value1.M31), Sse2.LoadVector128(&value1.M33)));
            //    Sse2.Store(&result.M33, MultiplyRowZW(value2, Sse2.LoadVector128(&value1.M31), Sse2.LoadVector128(&value1.M33)));
            //    Sse2.Store(&result.M41, MultiplyRowXY(value2, Sse2.LoadVector128(&value1.M41), Sse2.LoadVector128(&value1.M43)));
            //    Sse2.Store(&result.M43, MultiplyRowZW(value2, Sse2.LoadVector128(&value1.M41), Sse2.LoadVector128(&value1.M43)));

            //    return result;
            //}

            result.M11 = ( value1.M11 * value2.M11 ) + ( value1.M12 * value2.M21 ) + ( value1.M13 * value2.M31 ) + ( value1.M14 * value2.M41 );
            result.M12 = ( value1.M11 * value2.M12 ) + ( value1.M12 * value2.M22 ) + ( value1.M13 * value2.M32 ) + ( value1.M14 * value2.M42 );
            result.M13 = ( value1.M11 * value2.M13 ) + ( value1.M12 * value2.M23 ) + ( value1.M13 * value2.M33 ) + ( value1.M14 * value2.M43 );
            result.M14 = ( value1.M11 * value2.M14 ) + ( value1.M12 * value2.M24 ) + ( value1.M13 * value2.M34 ) + ( value1.M14 * value2.M44 );
            result.M21 = ( value1.M21 * value2.M11 ) + ( value1.M22 * value2.M21 ) + ( value1.M23 * value2.M31 ) + ( value1.M24 * value2.M41 );
            result.M22 = ( value1.M21 * value2.M12 ) + ( value1.M22 * value2.M22 ) + ( value1.M23 * value2.M32 ) + ( value1.M24 * value2.M42 );
            result.M23 = ( value1.M21 * value2.M13 ) + ( value1.M22 * value2.M23 ) + ( value1.M23 * value2.M33 ) + ( value1.M24 * value2.M43 );
            result.M24 = ( value1.M21 * value2.M14 ) + ( value1.M22 * value2.M24 ) + ( value1.M23 * value2.M34 ) + ( value1.M24 * value2.M44 );
            result.M31 = ( value1.M31 * value2.M11 ) + ( value1.M32 * value2.M21 ) + ( value1.M33 * value2.M31 ) + ( value1.M34 * value2.M41 );
            result.M32 = ( value1.M31 * value2.M12 ) + ( value1.M32 * value2.M22 ) + ( value1.M33 * value2.M32 ) + ( value1.M34 * value2.M42 );
            result.M33 = ( value1.M31 * value2.M13 ) + ( value1.M32 * value2.M23 ) + ( value1.M33 * value2.M33 ) + ( value1.M34 * value2.M43 );
            result.M34 = ( value1.M31 * value2.M14 ) + ( value1.M32 * value2.M24 ) + ( value1.M33 * value2.M34 ) + ( value1.M34 * value2.M44 );
            result.M41 = ( value1.M41 * value2.M11 ) + ( value1.M42 * value2.M21 ) + ( value1.M43 * value2.M31 ) + ( value1.M44 * value2.M41 );
            result.M42 = ( value1.M41 * value2.M12 ) + ( value1.M42 * value2.M22 ) + ( value1.M43 * value2.M32 ) + ( value1.M44 * value2.M42 );
            result.M43 = ( value1.M41 * value2.M13 ) + ( value1.M42 * value2.M23 ) + ( value1.M43 * value2.M33 ) + ( value1.M44 * value2.M43 );
            result.M44 = ( value1.M41 * value2.M14 ) + ( value1.M42 * value2.M24 ) + ( value1.M43 * value2.M34 ) + ( value1.M44 * value2.M44 );
            return result;
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
        public unsafe static Matrix4x4D operator *(Matrix4x4D value1, double value2)
        {
            Unsafe.SkipInit(out Matrix4x4D result);

            //if (AdvSimd.IsSupported)
            //{
            //    var right = Vector128.Create(value2);

            //    AdvSimd.Store(&result.M11, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M11), right));
            //    AdvSimd.Store(&result.M13, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M13), right));
            //    AdvSimd.Store(&result.M21, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M21), right));
            //    AdvSimd.Store(&result.M23, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M23), right));
            //    AdvSimd.Store(&result.M31, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M31), right));
            //    AdvSimd.Store(&result.M33, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M33), right));
            //    AdvSimd.Store(&result.M41, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M41), right));
            //    AdvSimd.Store(&result.M43, AdvSimd.Arm64.Multiply(AdvSimd.LoadVector128(&value1.M43), right));

            //    return result;
            //}
            //else if (Avx.IsSupported)
            //{
            //    var right = Vector256.Create(value2);

            //    Avx.Store(&result.M11, Avx.Multiply(Avx.LoadVector256(&value1.M11), right));
            //    Avx.Store(&result.M21, Avx.Multiply(Avx.LoadVector256(&value1.M21), right));
            //    Avx.Store(&result.M31, Avx.Multiply(Avx.LoadVector256(&value1.M31), right));
            //    Avx.Store(&result.M41, Avx.Multiply(Avx.LoadVector256(&value1.M41), right));

            //    return result;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    var right = Vector128.Create(value2);

            //    Sse2.Store(&result.M11, Sse2.Multiply(Sse2.LoadVector128(&value1.M11), right));
            //    Sse2.Store(&result.M13, Sse2.Multiply(Sse2.LoadVector128(&value1.M13), right));
            //    Sse2.Store(&result.M21, Sse2.Multiply(Sse2.LoadVector128(&value1.M21), right));
            //    Sse2.Store(&result.M23, Sse2.Multiply(Sse2.LoadVector128(&value1.M23), right));
            //    Sse2.Store(&result.M31, Sse2.Multiply(Sse2.LoadVector128(&value1.M31), right));
            //    Sse2.Store(&result.M33, Sse2.Multiply(Sse2.LoadVector128(&value1.M33), right));
            //    Sse2.Store(&result.M41, Sse2.Multiply(Sse2.LoadVector128(&value1.M41), right));
            //    Sse2.Store(&result.M43, Sse2.Multiply(Sse2.LoadVector128(&value1.M43), right));

            //    return result;
            //}
            //else
            {
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
        }

        /// <summary>
        /// Returns itself.
        /// </summary>
        [MethodImpl(Optimize)]
        public static Matrix4x4D operator +(Matrix4x4D value) =>
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
        public unsafe static Matrix4x4D operator +(Matrix4x4D left, Matrix4x4D right)
        {
            //if (Avx.IsSupported)
            //{
            //    Avx.Store(&left.M11, Avx.Add(Avx.LoadVector256(&left.M11), Avx.LoadVector256(&right.M11)));
            //    Avx.Store(&left.M21, Avx.Add(Avx.LoadVector256(&left.M21), Avx.LoadVector256(&right.M21)));
            //    Avx.Store(&left.M31, Avx.Add(Avx.LoadVector256(&left.M31), Avx.LoadVector256(&right.M31)));
            //    Avx.Store(&left.M41, Avx.Add(Avx.LoadVector256(&left.M41), Avx.LoadVector256(&right.M41)));

            //    return left;
            //}
            //else if (Sse2.IsSupported)
            //{
            //    Sse2.Store(&left.M11, Sse2.Add(Sse2.LoadVector128(&left.M11), Sse2.LoadVector128(&right.M11)));
            //    Sse2.Store(&left.M13, Sse2.Add(Sse2.LoadVector128(&left.M13), Sse2.LoadVector128(&right.M13)));
            //    Sse2.Store(&left.M21, Sse2.Add(Sse2.LoadVector128(&left.M21), Sse2.LoadVector128(&right.M21)));
            //    Sse2.Store(&left.M23, Sse2.Add(Sse2.LoadVector128(&left.M23), Sse2.LoadVector128(&right.M23)));
            //    Sse2.Store(&left.M31, Sse2.Add(Sse2.LoadVector128(&left.M31), Sse2.LoadVector128(&right.M31)));
            //    Sse2.Store(&left.M33, Sse2.Add(Sse2.LoadVector128(&left.M33), Sse2.LoadVector128(&right.M33)));
            //    Sse2.Store(&left.M41, Sse2.Add(Sse2.LoadVector128(&left.M41), Sse2.LoadVector128(&right.M41)));
            //    Sse2.Store(&left.M43, Sse2.Add(Sse2.LoadVector128(&left.M43), Sse2.LoadVector128(&right.M43)));

            //    return left;
            //}
            //else if (AdvSimd.Arm64.IsSupported)
            //{
            //    AdvSimd.Store(&left.M11, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M11), AdvSimd.LoadVector128(&right.M11)));
            //    AdvSimd.Store(&left.M13, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M13), AdvSimd.LoadVector128(&right.M13)));
            //    AdvSimd.Store(&left.M21, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M21), AdvSimd.LoadVector128(&right.M21)));
            //    AdvSimd.Store(&left.M23, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M23), AdvSimd.LoadVector128(&right.M23)));
            //    AdvSimd.Store(&left.M31, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M31), AdvSimd.LoadVector128(&right.M31)));
            //    AdvSimd.Store(&left.M33, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M33), AdvSimd.LoadVector128(&right.M33)));
            //    AdvSimd.Store(&left.M41, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M41), AdvSimd.LoadVector128(&right.M41)));
            //    AdvSimd.Store(&left.M43, AdvSimd.Arm64.Add(AdvSimd.LoadVector128(&left.M43), AdvSimd.LoadVector128(&right.M43)));

            //    return left;
            //}
            return new(left.M11 + right.M11, left.M12 + right.M12, left.M13 + right.M13, left.M14 + right.M14,
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
        public unsafe static bool operator ==(Matrix4x4D value1, Matrix4x4D value2)
        {
            //if (AdvSimd.Arm64.IsSupported)
            //    return VectorMath.Equal(AdvSimd.LoadVector128(&value1.M11), AdvSimd.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M13), AdvSimd.LoadVector128(&value2.M13)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M21), AdvSimd.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M23), AdvSimd.LoadVector128(&value2.M23)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M31), AdvSimd.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M33), AdvSimd.LoadVector128(&value2.M33)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M41), AdvSimd.LoadVector128(&value2.M41)) &&
            //           VectorMath.Equal(AdvSimd.LoadVector128(&value1.M43), AdvSimd.LoadVector128(&value2.M43));

            //else if (Avx.IsSupported)
            //    return VectorMath.Equal(Avx.LoadVector256(&value1.M11), Avx.LoadVector256(&value2.M11)) &&
            //           VectorMath.Equal(Avx.LoadVector256(&value1.M21), Avx.LoadVector256(&value2.M21)) &&
            //           VectorMath.Equal(Avx.LoadVector256(&value1.M31), Avx.LoadVector256(&value2.M31)) &&
            //           VectorMath.Equal(Avx.LoadVector256(&value1.M41), Avx.LoadVector256(&value2.M41));

            //else if (Sse2.IsSupported)
            //    return VectorMath.Equal(Sse2.LoadVector128(&value1.M11), Sse2.LoadVector128(&value2.M11)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M13), Sse2.LoadVector128(&value2.M13)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M21), Sse2.LoadVector128(&value2.M21)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M23), Sse2.LoadVector128(&value2.M23)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M31), Sse2.LoadVector128(&value2.M31)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M33), Sse2.LoadVector128(&value2.M33)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M41), Sse2.LoadVector128(&value2.M41)) &&
            //           VectorMath.Equal(Sse2.LoadVector128(&value1.M43), Sse2.LoadVector128(&value2.M43));

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
        public static Matrix4x4D Subtract(Matrix4x4D left, Matrix4x4D right) =>
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
        public static Matrix4x4D Transform(Matrix4x4D value, QuaternionD rotation) =>
            value.Transform(rotation);

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">
        /// The source matrix.
        /// </param>
        [MethodImpl(Optimize)]
        public unsafe static Matrix4x4D Transpose(Matrix4x4D matrix) =>
            new(matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                matrix.M14, matrix.M24, matrix.M34, matrix.M44);

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
        public unsafe void Deconstruct(out Vector3D scale, out QuaternionD rotation, out Vector3D translation)
        {
            fixed (Vector3D* scaleBase = &scale)
            {
                var pfScales = (double*)scaleBase;

                VectorBasis vectorBasis;
                var pVectorBasis = (Vector3D**)&vectorBasis;

                var matTemp = Identity;
                var canonicalBasis = default(CanonicalBasis);
                var pCanonicalBasis = &canonicalBasis.Row0;

                canonicalBasis.Row0 = Vector3D.UnitX;
                canonicalBasis.Row1 = Vector3D.UnitY;
                canonicalBasis.Row2 = Vector3D.UnitZ;

                translation = new Vector3D(M41, M42, M43);

                pVectorBasis[0] = (Vector3D*)&matTemp.M11;
                pVectorBasis[1] = (Vector3D*)&matTemp.M21;
                pVectorBasis[2] = (Vector3D*)&matTemp.M31;

                *pVectorBasis[0] = new Vector3D(M11, M12, M13);
                *pVectorBasis[1] = new Vector3D(M21, M22, M23);
                *pVectorBasis[2] = new Vector3D(M31, M32, M33);

                scale.X = pVectorBasis[0]->Length();
                scale.Y = pVectorBasis[1]->Length();
                scale.Z = pVectorBasis[2]->Length();

                (var x, var y, var z) = (pfScales[0], pfScales[1], pfScales[2]);
                (var a, var b, var c) =
                    ( x < y ) ?
                        ( y < z ) ?
                            (2u, 1u, 0u) :
                            ( x < z ) ?
                                (1u, 2u, 0u) :
                                (1u, 0u, 2u) :
                        ( x < z ) ?
                            (2u, 0u, 1u) :
                            ( y < z ) ?
                                (0u, 2u, 1u) :
                                (0u, 1u, 2u);

                if (pfScales[a] < 0.0001f)
                    *pVectorBasis[a] = pCanonicalBasis[a];

                *pVectorBasis[a] = pVectorBasis[a]->Normalize();

                if (pfScales[b] < 0.0001f)
                {
                    var fAbsX = Math.Abs(pVectorBasis[a]->X);
                    var fAbsY = Math.Abs(pVectorBasis[a]->Y);
                    var fAbsZ = Math.Abs(pVectorBasis[a]->Z);

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
                    rotation = QuaternionD.Identity;
                    scale = translation = Vector3D.Zero;
                }
                else
                {
                    // generate the quaternion from the matrix
                    rotation = QuaternionD.CreateFromRotationMatrix(matTemp);
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
        public readonly bool Equals(Matrix4x4D other) =>
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
            if (obj is Matrix4x4D value)
            {
                return this == value;
            }
            return false;
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        public double GetDeterminant()
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

            var kp_lo = (k * p) -(l * o);
            var jp_ln = (j * p) -(l * n);
            var jo_kn = (j * o) -(k * n);
            var ip_lm = (i * p) -(l * m);
            var io_km = (i * o) -(k * m);
            var in_jm = (i * n) -(j * m);

            return ( a * ( ( f * kp_lo ) - ( g * jp_ln ) + ( h * jo_kn ) ) ) -
                   ( b * ( ( e * kp_lo ) - ( g * ip_lm ) + ( h * io_km ) ) ) +
                   ( c * ( ( e * jp_ln ) - ( f * ip_lm ) + ( h * in_jm ) ) ) -
                   ( d * ( ( e * jo_kn ) - ( f * io_km ) + ( g * in_jm ) ) );
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
        public Matrix4x4D Invert() =>
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
        public Matrix4x4D Transform(QuaternionD rotation)
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

            return new(( M11 * q11 ) + ( M12 * q21 ) + ( M13 * q31 ),
                       ( M11 * q12 ) + ( M12 * q22 ) + ( M13 * q32 ),
                       ( M11 * q13 ) + ( M12 * q23 ) + ( M13 * q33 ),
                         M14,
                       ( M21 * q11 ) + ( M22 * q21 ) + ( M23 * q31 ),
                       ( M21 * q12 ) + ( M22 * q22 ) + ( M23 * q32 ),
                       ( M21 * q13 ) + ( M22 * q23 ) + ( M23 * q33 ),
                         M24,
                       ( M31 * q11 ) + ( M32 * q21 ) + ( M33 * q31 ),
                       ( M31 * q12 ) + ( M32 * q22 ) + ( M33 * q32 ),
                       ( M31 * q13 ) + ( M32 * q23 ) + ( M33 * q33 ),
                         M34,
                       ( M41 * q11 ) + ( M42 * q21 ) + ( M43 * q31 ),
                       ( M41 * q12 ) + ( M42 * q22 ) + ( M43 * q32 ),
                       ( M41 * q13 ) + ( M42 * q23 ) + ( M43 * q33 ),
                         M44);
        }

        /// <summary>
        /// Transposes the rows and columns of this matrix.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4D Transpose() =>
            Transpose(this);

        /// <summary>
        /// Provides a record-style <see langword="with"/>-like constructor.
        /// </summary>
        [MethodImpl(Optimize)]
        public Matrix4x4D With(double? m11 = null, double? m12 = null, double? m13 = null, double? m14 = null,
                               double? m21 = null, double? m22 = null, double? m23 = null, double? m24 = null,
                               double? m31 = null, double? m32 = null, double? m33 = null, double? m34 = null,
                               double? m41 = null, double? m42 = null, double? m43 = null, double? m44 = null) =>
            new(m11 ?? M11,
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

        internal Matrix3x3D As3x3() =>
            new(M11, M12, M13,
                M21, M22, M23,
                M31, M32, M33);

        #endregion Internal Methods

        #region Private Methods

        //[MethodImpl(Optimize)]
        //private static unsafe Vector128<double> MultiplyRowXY(Matrix4x4D value2, Vector128<double> vector1, Vector128<double> vector2) =>
        //    Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0x00),
        //                                    Sse2.LoadVector128(&value2.M11)),
        //                      Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0x03),
        //                                    Sse2.LoadVector128(&value2.M21))),
        //             Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0x00),
        //                                    Sse2.LoadVector128(&value2.M31)),
        //                      Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0x03),
        //                                    Sse2.LoadVector128(&value2.M41))));

        //[MethodImpl(Optimize)]
        //private static unsafe Vector128<double> MultiplyRowZW(Matrix4x4D value2, Vector128<double> vector1, Vector128<double> vector2) =>
        //    Sse2.Add(Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0x00),
        //                                    Sse2.LoadVector128(&value2.M13)),
        //                      Sse2.Multiply(Sse2.Shuffle(vector1, vector1, 0x03),
        //                                    Sse2.LoadVector128(&value2.M23))),
        //             Sse2.Add(Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0x00),
        //                                    Sse2.LoadVector128(&value2.M33)),
        //                      Sse2.Multiply(Sse2.Shuffle(vector2, vector2, 0x03),
        //                                    Sse2.LoadVector128(&value2.M43))));

        //[MethodImpl(Optimize)]
        //private static unsafe Vector256<double> MultiplyRow(Matrix4x4D value2, Vector256<double> vector)
        //{
        //    if (Avx2.IsSupported)
        //        return Avx.Add(Avx.Add(Avx.Multiply(Avx2.Permute4x64(vector, 0),
        //                                            Avx.LoadVector256(&value2.M11)),
        //                               Avx.Multiply(Avx2.Permute4x64(vector, 85),
        //                                            Avx.LoadVector256(&value2.M21))),
        //                       Avx.Add(Avx.Multiply(Avx2.Permute4x64(vector, 170),
        //                                            Avx.LoadVector256(&value2.M31)),
        //                               Avx.Multiply(Avx2.Permute4x64(vector, Byte.MaxValue),
        //                                            Avx.LoadVector256(&value2.M41))));
        //    else
        //        return Avx.Add(Avx.Add(Avx.Multiply(Select(vector, 0, 0),
        //                                            Avx.LoadVector256(&value2.M11)),
        //                               Avx.Multiply(Select(vector, 3, 0),
        //                                            Avx.LoadVector256(&value2.M21))),
        //                       Avx.Add(Avx.Multiply(Select(vector, 0, 5),
        //                                            Avx.LoadVector256(&value2.M31)),
        //                               Avx.Multiply(Select(vector, 12, 5),
        //                                            Avx.LoadVector256(&value2.M41))));
        //    static Vector256<double> Select(Vector256<double> vector, byte control1, byte control2)
        //    {
        //        var temp = Avx.Permute(vector, control1);
        //        return Avx.Permute2x128(temp, temp, control2);
        //    }
        //}

        #endregion Private Methods

        #region Private Structs

        private struct CanonicalBasis
        {
            #region Public Fields

            public Vector3D Row0;
            public Vector3D Row1;
            public Vector3D Row2;

            #endregion Public Fields
        }

        private struct VectorBasis
        {
            #region Public Fields

            public unsafe Vector3D* Element0;
            public unsafe Vector3D* Element1;
            public unsafe Vector3D* Element2;

            #endregion Public Fields
        }

        #endregion Private Structs

        #region Private Classes

        //private static class VectorMath
        //{
        //    #region Public Methods

        //    [MethodImpl(Optimize)]
        //    public static bool Equal(Vector128<double> a, Vector128<double> b)
        //    {
        //        // This implementation is based on the DirectX Math Library XMVector4Equal method
        //        // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

        //        if (Sse2.IsSupported)
        //            return Sse2.MoveMask(Sse2.CompareNotEqual(a, b)) == 0;
        //        else if (AdvSimd.Arm64.IsSupported)
        //        {
        //            var vResult = AdvSimd.Arm64.CompareEqual(a, b).AsUInt64();

        //            var vResult0 = vResult.GetLower().AsByte();
        //            var vResult1 = vResult.GetUpper().AsByte();

        //            var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

        //            var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
        //            return vTemp21.AsUInt32().GetElement(1) == 0xFFFFFFFF;
        //        }
        //        else
        //            // Redundant test so we won't preJIT remainder of this method on platforms without Sse2.
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool Equal(Vector256<double> a, Vector256<double> b) =>
        //        Avx.MoveMask(Avx.CompareNotEqual(a, b)) == 0;

        //    [MethodImpl(Optimize)]
        //    public static Vector256<double> Lerp(Vector256<double> a, Vector256<double> b, Vector256<double> t)
        //    {
        //        if (Fma.IsSupported)
        //            return Fma.MultiplyAdd(Avx.Subtract(b, a), t, a);

        //        else if (Avx.IsSupported)
        //            return Avx.Add(a, Avx.Multiply(Avx.Subtract(b, a), t));

        //        else
        //            // Redundant test so we won't preJIT remainder of this method on platforms without SIMD.
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool NotEqual(Vector128<double> a, Vector128<double> b)
        //    {
        //        // This implementation is based on the DirectX Math Library XMVector4Equal method
        //        // https://github.com/microsoft/DirectXMath/blob/master/Inc/DirectXMathVector.inl

        //        if (Sse2.IsSupported)
        //            return Sse2.MoveMask(Sse2.CompareNotEqual(a, b)) != 0;
        //        else if (AdvSimd.Arm64.IsSupported)
        //        {
        //            var vResult = AdvSimd.Arm64.CompareEqual(a, b).AsUInt64();

        //            var vResult0 = vResult.GetLower().AsByte();
        //            var vResult1 = vResult.GetUpper().AsByte();

        //            var vTemp10 = AdvSimd.Arm64.ZipLow(vResult0, vResult1);
        //            var vTemp11 = AdvSimd.Arm64.ZipHigh(vResult0, vResult1);

        //            var vTemp21 = AdvSimd.Arm64.ZipHigh(vTemp10.AsUInt16(), vTemp11.AsUInt16());
        //            return vTemp21.AsUInt32().GetElement(1) != 0xFFFFFFFF;
        //        }
        //        else
        //            // Redundant test so we won't preJIT remainder of this method on platforms without Sse2.
        //            throw new PlatformNotSupportedException();
        //    }

        //    [MethodImpl(Optimize)]
        //    public static bool NotEqual(Vector256<double> a, Vector256<double> b) =>
        //            Avx.MoveMask(Avx.CompareNotEqual(a, b)) != 0;

        //    #endregion Public Methods
        //}

        #endregion Private Classes
    }
}