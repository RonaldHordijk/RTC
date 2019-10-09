using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;

namespace RTC.Tests
{
    [TestFixture]
    public class TestCube
    {
        [Test]
        [TestCase(5, 0.5, 0, -1, 0, 0, 4, 6)]
        [TestCase(-5, 0.5, 0, 1, 0, 0, 4, 6)]
        [TestCase(0.5, 5, 0, 0, -1, 0, 4, 6)]
        [TestCase(0.5, -5, 0, 0, 1, 0, 4, 6)]
        [TestCase(0.5, 0, 5, 0, 0, -1, 4, 6)]
        [TestCase(0.5, 0, -5, 0, 0, 1, 4, 6)]
        [TestCase(0, 0.5, 0, 0, 0, 1, -1, 1)]

        public void TestIntersectCube(double o1, double o2, double o3, double d1, double d2, double d3, double r1, double r2)
        {
            var cube = new Cube();
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(d1, d2, d3));
            var xs = cube.Intersect(ray);

            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(r1, xs[0].Distance);
            Assert.AreEqual(r2, xs[1].Distance);
        }

        [Test]
        [TestCase(-2, 0, 0, 0.2673, 0.5345, 0.8018)]
        [TestCase(0, -2, 0, 0.8018, 0.2673, 0.5345)]
        [TestCase(0, 0, -2, 0.5345, 0.8018, 0.2673)]
        [TestCase(2, 0, 2, 0, 0, -1)]
        [TestCase(0, 2, 2, 0, -1, 0)]
        [TestCase(2, 2, 0, -1, 0, 0)]
        public void TestIntersectionmissesCube(double o1, double o2, double o3, double d1, double d2, double d3)
        {
            var cube = new Cube();
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(d1, d2, d3));
            var xs = cube.Intersect(ray);

            Assert.AreEqual(0, xs.Count);
        }

        [Test]
        [TestCase(1, 0.5, -0.8, 1, 0, 0)]
        [TestCase(-1, -0.2, 0.9, -1, 0, 0)]
        [TestCase(-0.4, 1, -0.1, 0, 1, 0)]
        [TestCase(0.3, -1, -0.7, 0, -1, 0)]
        [TestCase(-0.6, 0.3, 1, 0, 0, 1)]
        [TestCase(0.4, 0.4, -1, 0, 0, -1)]
        [TestCase(1, 1, 1, 1, 0, 0)]
        [TestCase(-1, -1, -1, -1, 0, 0)]
        public void TestNormalCube(double px, double py, double pz, double nx, double ny, double nz)
        {
            var cube = new Cube();
            var normal = cube.Normal(Tuple.Point(px, py, pz));

            Assert.AreEqual(normal, Tuple.Vector(nx, ny, nz));
        }
    }
}
