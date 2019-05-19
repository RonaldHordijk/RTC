using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;

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
    }
}
