using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    /// <summary>
    /// A structure encapsulating a 3D Plane
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 4)]
    public struct PlaneF
    {
        #region Public Fields

        /// <summary>
        /// The normal vector of the Plane.
        /// </summary>
        [FieldOffset(0)]
        public Vector3F Normal;

        /// <summary>
        /// The distance of the Plane along its normal from the origin.
        /// </summary>
        [FieldOffset(12)]
        public float D;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal
        /// </summary>
        /// <param name="x">The X-component of the normal.</param>
        /// <param name="y">The Y-component of the normal.</param>
        /// <param name="z">The Z-component of the normal.</param>
        /// <param name="d">The distance of the Plane along its normal from the origin.</param>
        public PlaneF(float x, float y, float z, float d)
        {
            Normal = new Vector3F(x, y, z);
            D = d;
        }

        /// <summary>
        /// Constructs a Plane from the given normal and distance along the normal from the origin.
        /// </summary>
        /// <param name="normal">The Plane's normal vector.</param>
        /// <param name="d">The Plane's distance from the origin along its normal vector.</param>
        public PlaneF(Vector3F normal, float d) =>
            (Normal, D) = (normal, d);

        /// <summary>
        /// Constructs a Plane from the given <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="value">A vector whose first 3 elements describe the normal vector,
        /// and whose W component defines the distance along that normal from the origin.</param>
        public PlaneF(Vector4F value) =>
            (Normal, D) = value;

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Creates a Plane that contains the three given points.
        /// </summary>
        /// <param name="point1">The first point defining the Plane.</param>
        /// <param name="point2">The second point defining the Plane.</param>
        /// <param name="point3">The third point defining the Plane.</param>
        [MethodImpl(Optimize)]
        public static PlaneF CreateFromVertices(Vector3F point1, Vector3F point2, Vector3F point3)
        {
            var vector = point2 - point1;
            var vector2 = point3 - point1;
            var value = Vector3F.Cross(vector, vector2);
            var vector3 = Vector3F.Normalize(value);
            var d = 0f - Vector3F.Dot(vector3, point1);
            return new PlaneF(vector3, d);
        }

        /// <summary>
        /// Calculates the dot product of a Plane and <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="plane">The Plane.</param>
        /// <param name="value">The <see cref="Vector4F"/>.</param>
        [MethodImpl(Optimize)]
        public static float Dot(PlaneF plane, Vector4F value) =>
            plane.Dot(value);

        /// <summary>
        /// Returns the dot product of a specified <see cref="Vector3F"/> and the normal vector of a <see cref="PlaneF"/>
        /// plus the distance value of that <see cref="PlaneF"/> (<see cref="D"/>).
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="value">The <see cref="Vector3F"/>.</param>
        [MethodImpl(Optimize)]
        public static float DotCoordinate(PlaneF plane, Vector3F value) =>
            plane.DotCoordinate(value);

        /// <summary>
        /// Returns the dot product of a specified <see cref="Vector3F"/> and the <see cref="Normal"/>
        /// vector of the specified <see cref="PlaneF"/>.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="value">The <see cref="Vector3F"/>.</param>
        [MethodImpl(Optimize)]
        public static float DotNormal(PlaneF plane, Vector3F value) =>
            plane.DotNormal(value);

        /// <summary>
        /// Create a new Plane whose normal vector is the source Plane's normal vector normalized.
        /// </summary>
        /// <param name="value">The source Plane.</param>
        [MethodImpl(Optimize)]
        public static PlaneF Normalize(PlaneF value) =>
            value.Normalize();

        /// <summary>
        /// Returns a boolean indicating whether the two given Planes are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="PlaneF"/> to compare.</param>
        /// <param name="right">The second <see cref="PlaneF"/> to compare.</param>
        /// <returns>True if the Planes are not equal; False otherwise.</returns>
        [MethodImpl(Optimize)]
        public static bool operator !=(PlaneF left, PlaneF right) =>
            left.Normal != right.Normal || left.D != right.D;

        /// <summary>
        /// Returns a boolean indicating whether the two given Planes are equal.
        /// </summary>
        /// <param name="left">The first <see cref="PlaneF"/> to compare.</param>
        /// <param name="right">The second <see cref="PlaneF"/> to compare.</param>
        /// <returns>True if the Planes are equal; False otherwise.</returns>
        [MethodImpl(Optimize)]
        public static bool operator ==(PlaneF left, PlaneF right) =>
            left.Normal == right.Normal && left.D == right.D;

        /// <summary>
        /// Transforms a normalized Plane by a Matrix.
        /// </summary>
        /// <param name="plane">The normalized Plane to transform.
        /// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
        /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
        [MethodImpl(Optimize)]
        public static PlaneF Transform(PlaneF plane, Matrix4x4F matrix)
        {
            var result = matrix.Invert();
            (var x, var y, var z, var d) = plane;
            return new PlaneF(x * result.M11 + y * result.M12 + z * result.M13 + d * result.M14,
                              x * result.M21 + y * result.M22 + z * result.M23 + d * result.M24,
                              x * result.M31 + y * result.M32 + z * result.M33 + d * result.M34,
                              x * result.M41 + y * result.M42 + z * result.M43 + d * result.M44);
        }

        /// <summary>
        /// Transforms a normalized Plane by a Quaternion rotation.
        /// </summary>
        /// <param name="plane">The normalized Plane to transform.
        /// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
        /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
        [MethodImpl(Optimize)]
        public static PlaneF Transform(PlaneF plane, QuaternionF rotation)
        {
            var n1 = rotation.X + rotation.X;
            var n2 = rotation.Y + rotation.Y;
            var n3 = rotation.Z + rotation.Z;
            var n4 = rotation.W * n1;
            var n5 = rotation.W * n2;
            var n6 = rotation.W * n3;
            var n7 = rotation.X * n1;
            var n8 = rotation.X * n2;
            var n9 = rotation.X * n3;
            var n10 = rotation.Y * n2;
            var n11 = rotation.Y * n3;
            var n12 = rotation.Z * n3;
            var n13 = 1f - n10 - n12;
            var n14 = n8 - n6;
            var n15 = n9 + n5;
            var n16 = n8 + n6;
            var n17 = 1f - n7 - n12;
            var n18 = n11 - n4;
            var n19 = n9 - n5;
            var n20 = n11 + n4;
            var n21 = 1f - n7 - n10;
            var x = plane.Normal.X;
            var y = plane.Normal.Y;
            var z = plane.Normal.Z;
            return new PlaneF(x * n13 + y * n14 + z * n15, x * n16 + y * n17 + z * n18, x * n19 + y * n20 + z * n21, plane.D);
        }

        /// <summary>
        /// Extracts the <see cref="Normal"/> vector and distance (<see cref="D"/>) components from this <see cref="PlaneF"/>.
        /// </summary>
        /// <param name="normal">The <see cref="Normal"/> component.</param>
        /// <param name="d">The distance (<see cref="D"/>) component.</param>
        [MethodImpl(Optimize)]
        public void Deconstruct(out Vector3F normal, out float d) =>
            (normal, d) = (Normal, D);

        /// <summary>
        /// Extracts the <see cref="Vector3F.X"/>, <see cref="Vector3F.Y"/>, and <see cref="Vector3F.Z"/> components from the
        /// <see cref="Normal"/> vector and distance (<see cref="D"/>) components from this <see cref="PlaneF"/>.
        /// </summary>
        /// <param name="x">The <see cref="Vector3F.X"/> of the <see cref="Normal"/> component.</param>
        /// <param name="y">The <see cref="Vector3F.Y"/> of the <see cref="Normal"/> component.</param>
        /// <param name="z">The <see cref="Vector3F.Z"/> of the <see cref="Normal"/> component.</param>
        /// <param name="d">The distance (<see cref="D"/>) component.</param>
        [MethodImpl(Optimize)]
        public void Deconstruct(out float x, out float y, out float z, out float d) =>
            (x, y, z, d) = (Normal.X, Normal.Y, Normal.Z, D);

        /// <summary>
        /// Calculates the dot product of this Plane and <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector4F"/>.</param>
        [MethodImpl(Optimize)]
        public float Dot(Vector4F value) =>
            Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D * value.W;

        /// <summary>
        /// Returns the dot product of a specified <see cref="Vector3F"/> and the <see cref="Normal"/>
        /// vector of this <see cref="PlaneF"/> plus the distance of this <see cref="PlaneF"/> (<see cref="D"/>).
        /// </summary>
        /// <param name="value">The <see cref="Vector3F"/>.</param>
        [MethodImpl(Optimize)]
        public float DotCoordinate(Vector3F value) =>
            DotNormal(value) + D;

        /// <summary>
        /// Returns the dot product of a specified <see cref="Vector3F"/> and the <see cref="Normal"/>
        /// vector of this <see cref="PlaneF"/>.
        /// </summary>
        /// <param name="value">The <see cref="Vector3F"/>.</param>
        [MethodImpl(Optimize)]
        public float DotNormal(Vector3F value) =>
            Normal.Dot(value);

        /// <summary>
        /// Returns a boolean indicating whether the given <see cref="PlaneF"/> is equal to this
        /// <see cref="PlaneF"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="PlaneF"/> to compare this instance to.</param>
        /// <returns>True if the other <see cref="PlaneF"/> is equal to this instance; False otherwise.</returns>
        [MethodImpl(Optimize)]
        public bool Equals(PlaneF other) =>
            this == other;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this
        /// <see cref="PlaneF"/> instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this <see cref="PlaneF"/>; False otherwise.</returns>
        [MethodImpl(Optimize)]
        public override bool Equals(object? obj) =>
            obj is PlaneF p && this == p;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode() =>
            Normal.GetHashCode() + D.GetHashCode();

        /// <summary>
        /// Create a new Plane whose normal vector is this Plane's normal vector normalized.
        /// </summary>
        [MethodImpl(Optimize)]
        public PlaneF Normalize()
        {
            var n = Normal.LengthSquared();
            if (MathF.Abs(n - 1) < 1.1920929e-7f)
                return this;
            var n2 = MathF.Sqrt(n);
            return new PlaneF(Normal / n2, D / n2);
        }

        /// <summary>
        /// Returns a String representing this <see cref="PlaneF"/> instance.
        /// </summary>
        public override string ToString() =>
            $"{{Normal:{Normal} D:{D}}}";

        /// <summary>
        /// Transforms this normalized Plane by a Matrix.
        /// </summary>
        /// <remarks>This <see cref="PlaneF"/> must already be normalized, so that its <see cref="Normal"/>
        /// vector is of unit length, before this method is called.</remarks>
        /// <param name="matrix">The transformation matrix to apply to this Plane.</param>
        [MethodImpl(Optimize)]
        public PlaneF Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        /// <summary>
        /// Transforms this normalized Plane by a Quaternion rotation.
        /// </summary>
        /// <remarks>This <see cref="PlaneF"/> must already be normalized, so that its <see cref="Normal"/>
        /// vector is of unit length, before this method is called.</remarks>
        /// <param name="rotation">The Quaternion rotation to apply to this Plane.</param>
        [MethodImpl(Optimize)]
        public PlaneF Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        #endregion Public Methods
    }
}