using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects;

namespace RTC.Tests.Rays
{
    [TestFixture]
    public class RayTests
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestCreate()
        {
            var ray = new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(4, 5, 6));

            Assert.AreEqual(ray.Origin, Tuple.Point(1, 2, 3));
            Assert.AreEqual(ray.Direction, Tuple.Vector(4, 5, 6));
        }

        [TestCase(0, 2, 3, 4)]
        [TestCase(1, 3, 3, 4)]
        [TestCase(-1, 1, 3, 4)]
        [TestCase(2.5, 4.5, 3, 4)]
        public void TestPosition(double distance, double x, double y, double z)
        {
            var ray = new Ray(Tuple.Point(2, 3, 4), Tuple.Vector(1, 0, 0));

            Assert.AreEqual(ray.Position(distance), Tuple.Point(x, y, z));
        }

        [Test]
        public void TestRaySphere2Points()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Distance, 4.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 6.0, Epsilon);
        }

        [Test]
        public void TestRaySphereTangent()
        {
            var ray = new Ray(Tuple.Point(0, 1, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Distance, 5.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 5.0, Epsilon);
        }

        [Test]
        public void TestRaySphereMiss()
        {
            var ray = new Ray(Tuple.Point(0, 2, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 0);
        }

        [Test]
        public void TestRaySphereInside()
        {
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Distance, -1.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 1.0, Epsilon);
        }

        [Test]
        public void TestRaySphereBehindRay()
        {
            var ray = new Ray(Tuple.Point(0, 0, 5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Distance, -6.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, -4.0, Epsilon);
        }

        [Test]
        public void TestNewIntersection()
        {
            var sphere = new Sphere();
            var intersection = new Intersection(4.5, sphere);

            Assert.AreEqual(intersection.Distance, 4.5, Epsilon);
            Assert.AreEqual(intersection.Object, sphere);
        }

        [Test]
        public void TestIntersectionList()
        {
            var sphere = new Sphere();
            var i1 = new Intersection(1, sphere);
            var i2 = new Intersection(2, sphere);

            var intersections = new Intersections
            {
                i1,
                i2
            };

            Assert.AreEqual(intersections.Count, 2);

            Assert.AreEqual(intersections[0].Distance, 1, Epsilon);
            Assert.AreEqual(intersections[1].Distance, 2, Epsilon);
        }

        [Test]
        public void TestRaySphere2PointsGetObjects()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Object, sphere);
            Assert.AreEqual(xs[1].Object, sphere);
        }

        [Test]
        public void TestHitPositiveDistance()
        {
            var sphere = new Sphere();

            var i1 = new Intersection(1, sphere);
            var i2 = new Intersection(2, sphere);

            var intersections = new Intersections
            {
                i2,
                i1
            };

            Assert.AreEqual(i1, intersections.Hit());
        }

        [Test]
        public void TestHitNegativeDistance()
        {
            var sphere = new Sphere();

            var i1 = new Intersection(-1, sphere);
            var i2 = new Intersection(1, sphere);

            var intersections = new Intersections
            {
                i2,
                i1
            };

            Assert.AreEqual(i2, intersections.Hit());
        }

        [Test]
        public void TestHitAllNegativeDistance()
        {
            var sphere = new Sphere();

            var i1 = new Intersection(-1, sphere);
            var i2 = new Intersection(-2, sphere);

            var intersections = new Intersections
            {
                i2,
                i1
            };

            Assert.IsNull(intersections.Hit()?.Object);
        }

        [Test]
        public void TestHitMultiple()
        {
            var sphere = new Sphere();

            var i1 = new Intersection(5, sphere);
            var i2 = new Intersection(7, sphere);
            var i3 = new Intersection(-2, sphere);
            var i4 = new Intersection(3, sphere);

            var intersections = new Intersections
            {
                i1,
                i2,
                i3,
                i4
            };

            Assert.AreEqual(i4, intersections.Hit());
        }

        [Test]
        public void TestTranslateRay()
        {
            var ray = new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(0, 1, 0));
            var m = Matrix4.Translation(3, 4, 5);
            var rm = ray.Transform(m);

            Assert.AreEqual(rm.Origin, Tuple.Point(4, 6, 8));
            Assert.AreEqual(rm.Direction, Tuple.Vector(0, 1, 0));
        }

        [Test]
        public void TestScaleRay()
        {
            var ray = new Ray(Tuple.Point(1, 2, 3), Tuple.Vector(0, 1, 0));
            var m = Matrix4.Scaling(2, 3, 4);
            var rm = ray.Transform(m);

            Assert.AreEqual(rm.Origin, Tuple.Point(2, 6, 12));
            Assert.AreEqual(rm.Direction, Tuple.Vector(0, 3, 0));
        }

        [Test]
        public void TestDefaultSphereTransformation()
        {
            var sphere = new Sphere();
            Assert.AreEqual(Matrix4.Identity, sphere.Transform);
        }

        [Test]
        public void TestSphereTransformation()
        {
            var sphere = new Sphere();
            var t = Matrix4.Translation(2, 3, 4);
            sphere.Transform = t;

            Assert.AreEqual(t, sphere.Transform);
        }

        [Test]
        public void TestRayScaledSphere()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            sphere.Transform = Matrix4.Scaling(2, 2, 2);

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 2);
            Assert.AreEqual(xs[0].Distance, 3.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 7.0, Epsilon);
        }

        [Test]
        public void TestRayTranslatedSphere()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();
            sphere.Transform = Matrix4.Translation(5, 0, 0);

            var xs = sphere.Intersect(ray);
            Assert.AreEqual(xs.Count, 0);
        }
    }
}
