using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MathStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 8, Size = 32)]
    public struct PlaneD
    {
        #region Public Fields
        [FieldOffset(24)]
        public double D;
        [FieldOffset(0)]
        public Vector3D Normal;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        public PlaneD(double x, double y, double z, double d)
        {
            Normal = new Vector3D(x, y, z);
            D = d;
        }

        public PlaneD(Vector3D normal, double d) =>
            (Normal, D) = (normal, d);

        public PlaneD(Vector4D value) =>
            (Normal, D) = value;

        #endregion Public Constructors

        #region Public Methods

        [MethodImpl(Optimize)]
        public static PlaneD CreateFromVertices(Vector3D point1, Vector3D point2, Vector3D point3)
        {
            var vector = point2 - point1;
            var vector2 = point3 - point1;
            var value = Vector3D.Cross(vector, vector2);
            var vector3 = Vector3D.Normalize(value);
            var d = 0f - Vector3D.Dot(vector3, point1);
            return new PlaneD(vector3, d);
        }

        [MethodImpl(Optimize)]
        public static double Dot(PlaneD plane, Vector4D value) =>
            plane.Dot(value);

        [MethodImpl(Optimize)]
        public static double DotCoordinate(PlaneD plane, Vector3D value) =>
            plane.DotCoordinate(value);

        [MethodImpl(Optimize)]
        public static double DotNormal(PlaneD plane, Vector3D value) =>
            plane.DotNormal(value);

        [MethodImpl(Optimize)]
        public static PlaneD Normalize(PlaneD value) =>
            value.Normalize();

        [MethodImpl(Optimize)]
        public static bool operator !=(PlaneD left, PlaneD right) =>
            left.Normal != right.Normal || left.D != right.D;

        [MethodImpl(Optimize)]
        public static bool operator ==(PlaneD left, PlaneD right) =>
            left.Normal == right.Normal && left.D == right.D;

        [MethodImpl(Optimize)]
        public static PlaneD Transform(PlaneD plane, Matrix4x4D matrix)
        {
            var result = matrix.Invert();
            (var x, var y, var z, var d) = plane;
            return new PlaneD(x * result.M11 + y * result.M12 + z * result.M13 + d * result.M14,
                              x * result.M21 + y * result.M22 + z * result.M23 + d * result.M24,
                              x * result.M31 + y * result.M32 + z * result.M33 + d * result.M34,
                              x * result.M41 + y * result.M42 + z * result.M43 + d * result.M44);
        }

        [MethodImpl(Optimize)]
        public static PlaneD Transform(PlaneD plane, QuaternionD rotation)
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
            return new PlaneD(x * n13 + y * n14 + z * n15, x * n16 + y * n17 + z * n18, x * n19 + y * n20 + z * n21, plane.D);
        }

        public void Deconstruct(out Vector3D normal, out double d) =>
                                                                                    (normal, d) = (Normal, D);

        public void Deconstruct(out double x, out double y, out double z, out double d) =>
            (x, y, z, d) = (Normal.X, Normal.Y, Normal.Z, D);

        [MethodImpl(Optimize)]
        public double Dot(Vector4D value) =>
            Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D * value.W;

        [MethodImpl(Optimize)]
        public double DotCoordinate(Vector3D value) =>
            DotNormal(value) + D;

        [MethodImpl(Optimize)]
        public double DotNormal(Vector3D value) =>
            Normal.Dot(value);

        [MethodImpl(Optimize)]
        public bool Equals(PlaneD other) =>
            this == other;

        [MethodImpl(Optimize)]
        public override bool Equals(object? obj) =>
            obj is PlaneD p && this == p;

        public override int GetHashCode() =>
            Normal.GetHashCode() + D.GetHashCode();

        [MethodImpl(Optimize)]
        public PlaneD Normalize()
        {
            var n = Normal.LengthSquared();
            if (Math.Abs(n - 1) < 1.1920929e-7f)
                return this;
            var n2 = Math.Sqrt(n);
            return new PlaneD(Normal / n2, D / n2);
        }

        public override string ToString() =>
            $"{{Normal:{Normal} D:{D}}}";

        [MethodImpl(Optimize)]
        public PlaneD Transform(Matrix4x4D matrix) =>
            Transform(this, matrix);

        [MethodImpl(Optimize)]
        public PlaneD Transform(QuaternionD rotation) =>
            Transform(this, rotation);

        #endregion Public Methods
    }
}