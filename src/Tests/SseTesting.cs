using BenchmarkDotNet.Attributes;

using MathStructs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarking
{
    public class SseTesting
    {
        Matrix3x3D[] values = new Matrix3x3D[1024];
        Matrix3x3D[] temp = new Matrix3x3D[1024];
        Matrix3x3F[] valuesF = new Matrix3x3F[1024];
        Matrix3x3F[] tempF = new Matrix3x3F[1024];
        
        public SseTesting()
        {
            var r = new Random();
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = new Matrix3x3D(r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next());
                valuesF[i] = new Matrix3x3F(r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next(), r.Next());
            }
        }

        [Benchmark]
        public unsafe Matrix3x3D[] Add()
        { 
                    for (var i = 0; i < 1024; i += 2)
                        temp[i] = values[i] + values[i + 1];
            return temp;
        }

        [Benchmark]
        public unsafe Matrix3x3D[] Subtract()
        {
                    for (var i = 0; i < 1024; i += 2)
                        temp[i] = values[i] - values[i + 1];
            return temp;
        }

        [Benchmark]
        public unsafe Matrix3x3D[] Multiply()
        {
                    for (var i = 0; i < 1024; i += 2)
                        temp[i] = values[i] * values[i + 1];
            return temp;
        }

        [Benchmark]
        public unsafe Matrix3x3D[] MultiplyScalar()
        {
                    for (var i = 0; i < 1024; i++)
                        temp[i] = values[i] * 3;
            return temp;
        }

        [Benchmark]
        public unsafe Matrix3x3F[] AddF()
        {
                    for (var i = 0; i < 1024; i += 2)
                        tempF[i] = valuesF[i] + valuesF[i + 1];
            return tempF;
        }

        [Benchmark]
        public unsafe Matrix3x3F[] SubtractF()
        {
                    for (var i = 0; i < 1024; i += 2)
                        tempF[i] = valuesF[i] - valuesF[i + 1];
            return tempF;
        }

        [Benchmark]
        public unsafe Matrix3x3F[] MultiplyF()
        {
                    for (var i = 0; i < 1024; i += 2)
                        tempF[i] = valuesF[i] * valuesF[i + 1];
            return tempF;
        }

        [Benchmark]
        public unsafe Matrix3x3F[] MultiplyScalarF()
        {
                    for (var i = 0; i < 1024; i++)
                        tempF[i] = valuesF[i] * 3;
            return tempF;
        }
    }
}
