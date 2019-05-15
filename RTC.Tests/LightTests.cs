using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;

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
    }
}
