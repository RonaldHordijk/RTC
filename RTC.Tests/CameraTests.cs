using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Utils;
using static System.Math;

namespace RTC.Tests
{
    [TestFixture]
    public class TestCamera
    {
        [Test]
        public void TestCreateCamera()
        {
            var cam = new Camera(160, 120, PI / 2);
            Assert.AreEqual(cam.HSize, 160);
            Assert.AreEqual(cam.VSize, 120);
            Assert.AreEqual(cam.FieldOfView, PI / 2, 1E-5);
            Assert.AreEqual(cam.Transform, Matrix4.Identity);
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
        public void TestPixelSize()
        {
            var cam = new Camera(200, 125, PI / 2);
            Assert.AreEqual(cam.PixelSize, 0.01, 1E-5);

            var cam2 = new Camera(125, 200, PI / 2);
            Assert.AreEqual(cam2.PixelSize, 0.01, 1E-5);
        }

        [Test]
        public void TestRayCenter()
        {
            var cam = new Camera(201, 101, PI / 2);
            var ray = cam.RayAt(100, 50);
            Assert.AreEqual(Tuple.Point(0, 0, 0), ray.Origin);
            Assert.AreEqual(Tuple.Vector(0, 0, -1), ray.Direction);
        }

        [Test]
        public void TestRayCorner()
        {
            var cam = new Camera(201, 101, PI / 2);
            var ray = cam.RayAt(0, 0);
            Assert.AreEqual(Tuple.Point(0, 0, 0), ray.Origin);
            Assert.AreEqual(Tuple.Vector(0.66519, 0.33259, -0.66851), ray.Direction);
        }

        [Test]
        public void TestRayTransformed()
        {
            var cam = new Camera(201, 101, PI / 2)
            {
                Transform = Matrix4.RotationY(PI / 4) * Matrix4.Translation(0, -2, 5)
            };
            var ray = cam.RayAt(100, 50);
            Assert.AreEqual(Tuple.Point(0, 2, -5), ray.Origin);
            Assert.AreEqual(Tuple.Vector(0.5 * Sqrt(2), 0, -0.5 * Sqrt(2)), ray.Direction);
        }

        [Test]
        public void TestRenderWorldWithCamera()
        {
            var w = DefaultWorld();
            var cam = new Camera(11, 11, PI / 2)
            {
                Transform = Matrix4.ViewTransform(
                    Tuple.Point(0, 0, -5),
                    Tuple.Point(0, 0, 0),
                    Tuple.Vector(0, 1, 0))
            };

            Canvas image = Renderer.Render(cam, w);
            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), image.PixelAt(5, 5));
        }
    }
}
