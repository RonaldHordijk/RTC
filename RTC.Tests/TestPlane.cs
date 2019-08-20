using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;

namespace RTC.Tests
{
    [TestFixture]
    public class TestPlane
    {
        [Test]
        public void TestPlaneNormal()
        {
            var plane = new Plane();

            Assert.AreEqual(Tuple.Vector(0, 1, 0), plane.Normal(Tuple.Point(0, 0, 0)));
            Assert.AreEqual(Tuple.Vector(0, 1, 0), plane.Normal(Tuple.Point(10, 0, -10)));
            Assert.AreEqual(Tuple.Vector(0, 1, 0), plane.Normal(Tuple.Point(-5, 0, 150)));
        }

        [Test]
        public void TestIntersectParallel()
        {
            var plane = new Plane();
            var ray = new Ray(Tuple.Point(0, 10, 0), Tuple.Vector(0, 0, 1));

            var xs = plane.Intersect(ray);

            Assert.AreEqual(0, xs.Count);
        }

        [Test]
        public void TestIntersectCoPlanar()
        {
            var plane = new Plane();
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));

            var xs = plane.Intersect(ray);

            Assert.AreEqual(0, xs.Count);
        }

        [Test]
        public void TestIntersectAbove()
        {
            var plane = new Plane();
            var ray = new Ray(Tuple.Point(0, 1, 0), Tuple.Vector(0, -1, 0));

            var xs = plane.Intersect(ray);

            Assert.AreEqual(1, xs.Count);
            Assert.AreEqual(1.0, xs[0].Distance);
            Assert.AreEqual(plane, xs[0].Shape);
        }
        [Test]
        public void TestIntersectBelow()
        {
            var plane = new Plane();
            var ray = new Ray(Tuple.Point(0, -1, 0), Tuple.Vector(0, 1, 0));

            var xs = plane.Intersect(ray);

            Assert.AreEqual(1, xs.Count);
            Assert.AreEqual(1.0, xs[0].Distance);
            Assert.AreEqual(plane, xs[0].Shape);
        }
    }
}
