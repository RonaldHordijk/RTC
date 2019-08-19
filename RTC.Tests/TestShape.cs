using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTC.Tests
{
    [TestFixture]
    public class TestShape
    {
        class TestAShape : Shape
        {
            public override Intersections Intersect(Ray ray)
            {
                throw new NotImplementedException();
            }

            public override RTC.Geometry.Tuple Normal(RTC.Geometry.Tuple worldPoint)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestShapeMaterial()
        {
            var shape = new TestAShape();

            Assert.AreEqual(new Material(), shape.Material);
        }

        [Test]
        public void TestShapeMaterialSet()
        {
            var shape = new TestAShape();
            shape.Material.Ambient = 1;

            var mat = new Material
            {
                Ambient = 1
            };

            Assert.AreEqual(mat, shape.Material);
        }

        [Test]
        public void TestTransform()
        {
            var shape = new TestAShape();

            Assert.AreEqual(Matrix4.Identity, shape.Transform);
        }

        [Test]
        public void TestAssignTransform()
        {
            var shape = new TestAShape();
            shape.Transform = Matrix4.Translation(2, 3, 4);

            Assert.AreEqual(Matrix4.Translation(2, 3, 4), shape.Transform);
        }
    }
}
