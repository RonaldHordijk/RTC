using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;

namespace RTC.Tests
{
    [TestFixture]
    public class TestCylinder
    {
        private const double Epsilon = 0.00001;

        [Test]
        [TestCase(1, 0, 0, 0, 1, 0)]
        [TestCase(0, 0, 0, 0, 1, 0)]
        [TestCase(0, 0, -5, 1, 1, 1)]
        public void TestMissCylinder(double o1, double o2, double o3, double d1, double d2, double d3)
        {
            var cylinder = new Cylinder();
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(d1, d2, d3));
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(0, xs.Count);
        }

        [Test]
        [TestCase(1, 0, -5, 0, 0, 1, 5, 5)]
        [TestCase(0, 0, -5, 0, 0, 1, 4, 6)]
        [TestCase(0.5, 0, -5, 0.1, 1, 1, 6.80798, 7.08872)]
        public void TestHitCylinder(double o1, double o2, double o3, double dir1, double dir2, double dir3, double d1, double d2)
        {
            var cylinder = new Cylinder();
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(dir1, dir2, dir3).Normalized());
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(d1, xs[0].Distance, Epsilon);
            Assert.AreEqual(d2, xs[1].Distance, Epsilon);
        }

        [Test]
        [TestCase(1, 0, 0, 1, 0, 0)]
        [TestCase(0, -5, -1, 0, 0, -1)]
        [TestCase(0, -2, 1, 0, 0, 1)]
        [TestCase(-1, 1, 0, -1, 0, 0)]
        public void TestNormal(double px, double py, double pz, double nx, double ny, double nz)
        {
            var cylinder = new Cylinder();
            var normal = cylinder.Normal(Tuple.Point(px, py, pz));

            Assert.AreEqual(normal, Tuple.Vector(nx, ny, nz));
        }

        [Test]
        public void TestDefaultBounds()
        {
            var cylinder = new Cylinder();

            Assert.IsTrue(double.IsNegativeInfinity(cylinder.Minimum));
            Assert.IsTrue(double.IsPositiveInfinity(cylinder.Maximum));
        }

        [Test]
        [TestCase(0, 1.5, 0, 0.1, 1, 0, 0)]
        [TestCase(0, 3, -5, 0, 0, 1, 0)]
        [TestCase(0, 0, -5, 0, 0, 1, 0)]
        [TestCase(0, 2, -5, 0, 0, 1, 0)]
        [TestCase(0, 1, -5, 0, 0, 1, 0)]
        [TestCase(0, 1.5, -2, 0, 0, 1, 2)]
        public void TestHitConstraintCylinder(double o1, double o2, double o3, double dir1, double dir2, double dir3, int expectedCount)
        {
            var cylinder = new Cylinder
            {
                Minimum = 1.0,
                Maximum = 2.0
            };

            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(dir1, dir2, dir3));
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(expectedCount, xs.Count);
        }

        [Test]
        public void TestDefaultClosed()
        {
            var cylinder = new Cylinder();

            Assert.IsFalse(cylinder.Closed);
        }

        [Test]
        [TestCase(0, 3, 0, 0, -1, 0, 2)]
        [TestCase(0, 3, -2, 0, -1, 2, 2)]
        [TestCase(0, 4, -2, 0, -1, 1, 2)]
        [TestCase(0, 0, -2, 0, 1, 2, 2)]
        [TestCase(0, -1, -2, 0, 1, 1, 2)]
        public void TestHitClosedCylinder(double o1, double o2, double o3, double dir1, double dir2, double dir3, int expectedCount)
        {
            var cylinder = new Cylinder
            {
                Minimum = 1.0,
                Maximum = 2.0,
                Closed = true

            };

            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(dir1, dir2, dir3).Normalized());
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(expectedCount, xs.Count);
        }

        [Test]
        [TestCase(0, 1, 0, 0, -1, 0)]
        [TestCase(0.5, 1, 0, 0, -1, 0)]
        [TestCase(0, 1, 0.5, 0, -1, 0)]
        [TestCase(0, 2, 0, 0, 1, 0)]
        [TestCase(0.5, 2, 0, 0, 1, 0)]
        [TestCase(0, 2, 0.5, 0, 1, 0)]
        public void TestNormalCaped(double px, double py, double pz, double nx, double ny, double nz)
        {
            var cylinder = new Cylinder
            {
                Minimum = 1,
                Maximum = 2,
                Closed = true
            };
            var normal = cylinder.Normal(Tuple.Point(px, py, pz));

            Assert.AreEqual(normal, Tuple.Vector(nx, ny, nz));
        }

    }
}
