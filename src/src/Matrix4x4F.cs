using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathStructs
{
    public struct Matrix4x4F
    {
        //private struct CanonicalBasis
        //{
        //    public Vector3F Row0;
        //    public Vector3F Row1;
        //    public Vector3F Row2;
        //}
        //private struct VectorBasis
        //{
        //    public unsafe Vector3F* Element0;
        //    public unsafe Vector3F* Element1;
        //    public unsafe Vector3F* Element2;
        //}
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
    }
}
