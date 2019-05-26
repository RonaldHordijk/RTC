using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Utils;

namespace RTC.Tests
{
    [TestFixture]
    public class WorldTests
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestEmptyWorld()
        {
            var world = new World();

            Assert.IsNull(world.Light);
            Assert.AreEqual(0, world.Objects.Count);
        }

        private World DefaultWorld()
        {
            return new World
            {
                Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)),
                Objects =
                {
                    new Sphere
                        {
                            Material = new Material
                            {
                                Color = new Color(0.8, 1.0, 0.6),
                                Diffuse = 0.7,
                                Specular = 0.2
                            }
                        },
                    new Sphere
                        {
                            Transform = Matrix4.Scaling(0.5, 0.5, 0.5)
                        }
                }
            };
        }

        [Test]
        public void TestDefaultWorld()
        {
            var world = DefaultWorld();

            Assert.AreEqual(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)), world.Light);
            Assert.IsTrue(world.Objects.Contains(new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            }));
            Assert.IsTrue(world.Objects.Contains(new Sphere
            {
                Transform = Matrix4.Scaling(0.5, 0.5, 0.5)
            }));
        }

        [Test]
        public void TestDefaultWorldIntersect()
        {
            var world = DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            var xs = world.Intersect(ray);

            Assert.AreEqual(4, xs.Count);
            Assert.AreEqual(xs[0].Distance, 4.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 4.5, Epsilon);
            Assert.AreEqual(xs[2].Distance, 5.5, Epsilon);
            Assert.AreEqual(xs[3].Distance, 6.0, Epsilon);
        }

        [Test]
        public void TestPreComputing()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var i = new Intersection(4, sphere);
            var comp = Computation.Prepare(i, ray);

            Assert.AreEqual(comp.Distance, i.Distance);
            Assert.AreEqual(comp.Object, i.Object);
            Assert.AreEqual(comp.Point, Tuple.Point(0, 0, -1));
            Assert.AreEqual(comp.EyeVector, Tuple.Vector(0, 0, -1));
            Assert.AreEqual(comp.NormalVector, Tuple.Vector(0, 0, -1));
            Assert.IsFalse(comp.Inside);
        }

        [Test]
        public void TestPreComputingInside()
        {
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var i = new Intersection(1, sphere);
            var comp = Computation.Prepare(i, ray);

            Assert.AreEqual(comp.Point, Tuple.Point(0, 0, 1));
            Assert.AreEqual(comp.EyeVector, Tuple.Vector(0, 0, -1));
            Assert.AreEqual(comp.NormalVector, Tuple.Vector(0, 0, -1));
            Assert.IsTrue(comp.Inside);
        }

        [Test]
        public void TestShadeIntersection()
        {
            var world = DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = world.Objects[0];
            var i = new Intersection(4, sphere);

            var comp = Computation.Prepare(i, ray);
            var color = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), color);
        }

        [Test]
        public void TestShadeIntersectionInside()
        {
            var world = DefaultWorld();
            world.Light = new PointLight(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1));
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = world.Objects[1];
            var i = new Intersection(0.5, sphere);

            var comp = Computation.Prepare(i, ray);
            var color = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.90498, 0.90498, 0.90498), color);
        }

        [Test]
        public void TestColorAtMiss()
        {
            var world = DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 1, 0));

            Assert.AreEqual(new Color(0, 0, 0), world.ColorAt(ray));
        }

        [Test]
        public void TestColorAtBehindRay()
        {
            var world = DefaultWorld();
            world.Objects[0].Material.Ambient = 1;
            world.Objects[1].Material.Ambient = 1;

            var ray = new Ray(Tuple.Point(0, 0, 0.75), Tuple.Vector(0, 0, -1));

            Assert.AreEqual(world.Objects[1].Material.Color, world.ColorAt(ray));
        }
    }
}
