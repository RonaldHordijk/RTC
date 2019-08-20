using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Materials;

namespace RTC.Tests
{
    [TestFixture]
    public class TestShape
    {
        public class TestAShape : Shape
        {
            public Ray SavedRay { get; set; }

            protected override Intersections LocalIntersection(Ray rayTransformed)
            {
                SavedRay = rayTransformed;
                return null;
            }

            protected override Tuple LocalNormal(Tuple objectPoint)
            {
                return Tuple.Vector(objectPoint.X, objectPoint.Y, objectPoint.Z);
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

        [Test]
        public void TestIntersectScaledRay()
        {
            var shape = new TestAShape
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            shape.Intersect(ray);

            Assert.AreEqual(Tuple.Point(0, 0, -2.5), shape.SavedRay.Origin);
            Assert.AreEqual(Tuple.Vector(0, 0, 0.5), shape.SavedRay.Direction);
        }

        [Test]
        public void TestIntersectTranslatedShapeRay()
        {
            var shape = new TestAShape
            {
                Transform = Matrix4.Translation(5, 0, 0)
            };
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            shape.Intersect(ray);

            Assert.AreEqual(Tuple.Point(-5, 0, -5), shape.SavedRay.Origin);
            Assert.AreEqual(Tuple.Vector(0, 0, 1), shape.SavedRay.Direction);
        }

        [Test]
        public void TestNormalTranslatedShape()
        {
            var shape = new TestAShape
            {
                Transform = Matrix4.Translation(0, 1, 0)
            };

            var n = shape.Normal(Tuple.Point(0, 1.70711, -0.70711));

            Assert.AreEqual(Tuple.Vector(0, 0.70711, -0.70711), n);
        }

        [Test]
        public void TestNormalTransFormedShape()
        {
            var shape = new TestAShape
            {
                Transform = Matrix4.Scaling(1, 0.5, 1) * Matrix4.RotationZ(System.Math.PI / 5)
            };

            var n = shape.Normal(Tuple.Point(0, 0.70711, -0.70711));

            Assert.AreEqual(Tuple.Vector(0, 0.97014, -0.24254), n);
        }
    }
}
