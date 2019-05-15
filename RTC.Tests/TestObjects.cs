using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;

namespace RTC.Tests
{
    [TestFixture]
    public class TestObjects
    {
        [TestCase(1, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(0.577350269189, 0.577350269189, 0.577350269189)]
        public void TestSphereNormals(double x, double y, double z)
        {
            var sphere = new Sphere();
            var normal = sphere.Normal(Tuple.Point(x, y, z));

            Assert.AreEqual(Tuple.Vector(x, y, z), normal);
        }

        [Test]
        public void TestSphereNormalNomalized()
        {
            var sphere = new Sphere();
            var normal = sphere.Normal(Tuple.Point(0.577350269189, 0.577350269189, 0.577350269189));

            Assert.AreEqual(normal, normal.Normalized());
        }

        [Test]
        public void TestSphereNormalTranslated()
        {
            var sphere = new Sphere();
            sphere.Transform = Matrix4.Translation(0, 1, 0);
            var normal = sphere.Normal(Tuple.Point(0, 1.70711, -0.70711));

            Assert.AreEqual(normal, Tuple.Vector(0, 0.70711, -0.70711));
        }

        [Test]
        public void TestSphereNormalTransformed()
        {
            var sphere = new Sphere();
            sphere.Transform = Matrix4.Scaling(1, 0.5, 1) * Matrix4.RotationZ(System.Math.PI);
            var normal = sphere.Normal(Tuple.Point(0, 0.7071067811, -0.7071067811));

            Assert.AreEqual(normal, Tuple.Vector(0, 0.97014, -0.24254));
        }

        [Test]
        public void TestSphereMaterial()
        {
            var sphere = new Sphere();

            Assert.AreEqual(new Material(), sphere.Material);
        }

        [Test]
        public void TestSphereMaterialSet()
        {
            var sphere = new Sphere();
            sphere.Material.Ambient = 1;

            var mat = new Material
            {
                Ambient = 1
            };

            Assert.AreEqual(mat, sphere.Material);
        }
    }
}
