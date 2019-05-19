using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Utils;

namespace RTC.Tests
{
    [TestFixture]
    public class TestLights
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestNewLight()
        {
            var light = new PointLight(Tuple.Point(0, 0, 0), new Color(1, 1, 1));

            Assert.AreEqual(Tuple.Point(0, 0, 0), light.Position);
            Assert.AreEqual(new Color(1, 1, 1), light.Intensity);
        }

        [Test]
        public void TestNewMaterial()
        {
            var material = new Material();

            Assert.AreEqual(new Color(1, 1, 1), material.Color);
            Assert.AreEqual(0.1, material.Ambient, Epsilon);
            Assert.AreEqual(0.9, material.Specular, Epsilon);
            Assert.AreEqual(0.9, material.Diffuse, Epsilon);
            Assert.AreEqual(200, material.Shininess, Epsilon);
        }

        [Test]
        public void TestLightingStraightReflect()
        {
            var material = new Material();
            var position = Tuple.Point(0, 0, 0);

            var eye = Tuple.Vector(0, 0, -1);
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));

            var res = LightUtil.Lighting(material, light, position, eye, normal);

            Assert.AreEqual(new Color(1.9, 1.9, 1.9), res);
        }

        [Test]
        public void TestLightingAngleReflect()
        {
            var material = new Material();
            var position = Tuple.Point(0, 0, 0);

            var eye = Tuple.Vector(0, 0.5 * System.Math.Sqrt(2), -0.5 * System.Math.Sqrt(2));
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));

            var res = LightUtil.Lighting(material, light, position, eye, normal);

            Assert.AreEqual(new Color(1.0, 1.0, 1.0), res);
        }

        [Test]
        public void TestLightingAngleReflect2()
        {
            var material = new Material();
            var position = Tuple.Point(0, 0, 0);

            var eye = Tuple.Vector(0, 0, -1);
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 10, -10), new Color(1, 1, 1));

            var res = LightUtil.Lighting(material, light, position, eye, normal);

            Assert.AreEqual(new Color(0.7364, 0.7364, 0.7364), res);
        }

        [Test]
        public void TestLightingAngleReflectHighlight()
        {
            var material = new Material();
            var position = Tuple.Point(0, 0, 0);

            var eye = Tuple.Vector(0, -0.5 * System.Math.Sqrt(2), -0.5 * System.Math.Sqrt(2));
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 10, -10), new Color(1, 1, 1));

            var res = LightUtil.Lighting(material, light, position, eye, normal);

            Assert.AreEqual(new Color(1.6364, 1.6364, 1.6364), res);
        }

        [Test]
        public void TestLightingAngleBehind()
        {
            var material = new Material();
            var position = Tuple.Point(0, 0, 0);

            var eye = Tuple.Vector(0, 0, -1);
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, 10), new Color(1, 1, 1));

            var res = LightUtil.Lighting(material, light, position, eye, normal);

            Assert.AreEqual(new Color(0.1, 0.1, 0.1), res);
        }
    }
}
