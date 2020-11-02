using MathStructs;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Matrix4x4FTests
    {
        [Test]
        public void CreateScaleCenterTest()
        {
            var scale = new Vector3F(3, 4, 5);
            var center = new Vector3F(23, 42, 666);

            var scaleAroundZero = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z, Vector3F.Zero);
            var scaleAroundZeroExpected = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z);

            Assert.That(scaleAroundZero, Is.EqualTo(scaleAroundZeroExpected), "scale");

            var scaleAroundCenter = Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z, center);
            var scaleAroundCenterExpected = Matrix4x4F.CreateTranslation(-center) * Matrix4x4F.CreateScale(scale.X, scale.Y, scale.Z) * Matrix4x4F.CreateTranslation(center);

            Assert.That(scaleAroundCenter, Is.EqualTo(scaleAroundCenterExpected), "center");
        }
    }
}
