using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;

namespace RTC.Tests
{
    [TestFixture]
    public class TestLights
    {
        [Test]
        public void TestMethod()
        {
            var light = new PointLight(Tuple.Point(0, 0, 0), new Color(1, 1, 1));

            Assert.AreEqual(Tuple.Point(0, 0, 0), light.Position);
            Assert.AreEqual(new Color(1, 1, 1), light.Intensity);
        }
    }
}
