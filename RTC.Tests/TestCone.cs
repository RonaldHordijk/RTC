using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;

namespace RTC.Tests
{
    [TestFixture]
    public class TestCone
    {
        private const double Epsilon = 0.00001;

        [Test]
        [TestCase(0, 0, -5, 0, 0, 1, 5, 5)]
        [TestCase(0, 0, -5, 1, 1, 1, 8.66025, 8.66025)]
        [TestCase(1, 1, -5, -0.5, -1, 1, 4.55006, 49.44994)]
        public void TestHitCone(double o1, double o2, double o3, double dir1, double dir2, double dir3, double d1, double d2)
        {
            var cylinder = new Cone();
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(dir1, dir2, dir3).Normalized());
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(2, xs.Count);
            Assert.AreEqual(d1, xs[0].Distance, Epsilon);
            Assert.AreEqual(d2, xs[1].Distance, Epsilon);
        }

        [Test]
        public void TestHitConeParallel()
        {
            var cylinder = new Cone();
            var ray = new Ray(Tuple.Point(0, 0, -1), Tuple.Vector(0, 1, 1).Normalized());
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(1, xs.Count);
            Assert.AreEqual(0.35355, xs[0].Distance, Epsilon);
        }

        [Test]
        [TestCase(0, 0, -5, 0, 1, 0, 0)]
        [TestCase(0, 0, -0.25, 0, 1, 1, 2)]
        [TestCase(0, 0, -0.25, 0, 1, 0, 4)]
        public void TestHitConeClosed(double o1, double o2, double o3, double dir1, double dir2, double dir3, int expectedCount)
        {
            var cylinder = new Cone
            {
                Minimum = -0.5,
                Maximum = 0.5,
                Closed = true
            };
            var ray = new Ray(Tuple.Point(o1, o2, o3), Tuple.Vector(dir1, dir2, dir3).Normalized());
            var xs = cylinder.Intersect(ray);

            Assert.AreEqual(expectedCount, xs.Count);
        }

        [Test]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(1, 1, 1, 1, -1.4142135623730950488016887242097, 1)]
        [TestCase(-1, -1, 0,  -1, 1, 0)]
        public void TestHitConeNormal(double p1, double p2, double p3, double n1, double n2, double n3)
        {
            var cylinder = new Cone
            {
                Minimum = -0.5,
                Maximum = 0.5,
                Closed = true
            };

            var n = cylinder.Normal(Tuple.Point(p1, p2, p3));

            Assert.AreEqual(Tuple.Vector(n1, n2, n3).Normalized(), n);
        }
    }
}
