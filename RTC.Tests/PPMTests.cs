using NUnit.Framework;
using RTC.Drawing;
using System.Linq;

namespace RTC.Tests.Drawing
{
    [TestFixture]
    public class PPMTests
    {
        [Test]
        public void TestHeader()
        {
            var canvas = new Canvas(5, 3);

            var ppmLines = PPMWriter.ToText(canvas).ToList();

            Assert.AreEqual("P3", ppmLines[0]);
            Assert.AreEqual("5 3", ppmLines[1]);
            Assert.AreEqual("255", ppmLines[2]);
        }

        [Test]
        public void TestSomeValues()
        {
            var canvas = new Canvas(5, 3);
            var c1 = new Color(1.5, 0, 0);
            var c2 = new Color(0, 0.5, 0);
            var c3 = new Color(-0.5, 0, 1);
            canvas.SetPixel(0, 0, c1);
            canvas.SetPixel(2, 1, c2);
            canvas.SetPixel(4, 2, c3);

            var ppmLines = PPMWriter.ToText(canvas).ToList();

            Assert.AreEqual("255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", ppmLines[3]);
            Assert.AreEqual("0 0 0 0 0 0 0 127 0 0 0 0 0 0 0", ppmLines[4]);
            Assert.AreEqual("0 0 0 0 0 0 0 0 0 0 0 0 0 0 255", ppmLines[5]);
        }

        [Test]
        public void TestSplitLine()
        {
            var canvas = new Canvas(10, 2);
            var c = new Color(1, 0.8, 0.6);

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 10; x++)
                    canvas.SetPixel(x, y, c);
            }
            var ppmLines = PPMWriter.ToText(canvas).ToList();

            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppmLines[3]);
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppmLines[4]);
            Assert.AreEqual("255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204", ppmLines[5]);
            Assert.AreEqual("153 255 204 153 255 204 153 255 204 153 255 204 153", ppmLines[6]);
        }

        [Test]
        public void TestEmptyLineEnd()
        {
            var canvas = new Canvas(5, 3);

            var ppmLines = PPMWriter.ToText(canvas).ToList();

            Assert.AreEqual(7, ppmLines.Count());
            Assert.AreEqual("", ppmLines.Last());
        }
    }
}
