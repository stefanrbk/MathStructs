using System;
using System.Runtime.CompilerServices;

namespace MathStructs
{
    public struct PlaneF
    {
        #region Public Fields

        public float D;
        public Vector3F Normal;

        #endregion Public Fields

        #region Private Fields

        private const MethodImplOptions Inline = MethodImplOptions.AggressiveInlining;
        private const MethodImplOptions Optimize = Inline | MethodImplOptions.AggressiveOptimization;

        #endregion Private Fields

        #region Public Constructors

        public PlaneF(float x, float y, float z, float d)
        {
            Normal = new Vector3F(x, y, z);
            D = d;
        }

        public PlaneF(Vector3F normal, float d) =>
            (Normal, D) = (normal, d);

        public PlaneF(Vector4F value) =>
            (Normal, D) = value;

        #endregion Public Constructors

        #region Public Methods

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

        [MethodImpl(Optimize)]
        public static float Dot(PlaneF plane, Vector4F value) =>
            plane.Dot(value);

        [MethodImpl(Optimize)]
        public static float DotCoordinate(PlaneF plane, Vector3F value) =>
            plane.DotCoordinate(value);

        [MethodImpl(Optimize)]
        public static float DotNormal(PlaneF plane, Vector3F value) =>
            plane.DotNormal(value);

        [MethodImpl(Optimize)]
        public static PlaneF Normalize(PlaneF value) =>
            value.Normalize();

        [MethodImpl(Optimize)]
        public static bool operator !=(PlaneF left, PlaneF right) =>
            left.Normal != right.Normal || left.D != right.D;

        [MethodImpl(Optimize)]
        public static bool operator ==(PlaneF left, PlaneF right) =>
            left.Normal == right.Normal && left.D == right.D;

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

        public void Deconstruct(out Vector3F normal, out float d) =>
                                                                                    (normal, d) = (Normal, D);

        public void Deconstruct(out float x, out float y, out float z, out float d) =>
            (x, y, z, d) = (Normal.X, Normal.Y, Normal.Z, D);

        [MethodImpl(Optimize)]
        public float Dot(Vector4F value) =>
            Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D * value.W;

        [MethodImpl(Optimize)]
        public float DotCoordinate(Vector3F value) =>
            DotNormal(value) + D;

        [MethodImpl(Optimize)]
        public float DotNormal(Vector3F value) =>
            Normal.Dot(value);

        [MethodImpl(Optimize)]
        public bool Equals(PlaneF other) =>
            this == other;

        [MethodImpl(Optimize)]
        public override bool Equals(object? obj) =>
            obj is PlaneF p && this == p;

        public override int GetHashCode() =>
            Normal.GetHashCode() + D.GetHashCode();

        [MethodImpl(Optimize)]
        public PlaneF Normalize()
        {
            var n = Normal.LengthSquared();
            if (MathF.Abs(n - 1) < 1.1920929e-7f)
                return this;
            var n2 = MathF.Sqrt(n);
            return new PlaneF(Normal / n2, D / n2);
        }

        public override string ToString() =>
            $"{{Normal:{Normal} D:{D}}}";

        [MethodImpl(Optimize)]
        public PlaneF Transform(Matrix4x4F matrix) =>
            Transform(this, matrix);

        [MethodImpl(Optimize)]
        public PlaneF Transform(QuaternionF rotation) =>
            Transform(this, rotation);

        #endregion Public Methods
    }
}