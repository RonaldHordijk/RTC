using NUnit.Framework;
using RTC.Drawing;

namespace Drawing
{
    [TestFixture]
    public class CanvasTests
    {
        [Test]
        public void TestNewCanvas()
        {
            var canvas = new Canvas(10, 20);

            Assert.AreEqual(10, canvas.Width);
            Assert.AreEqual(20, canvas.Height);

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Assert.AreEqual(canvas.PixelAt(x, y), new Color(0, 0, 0));
                }
            }
        }

        [Test]
        public void TestSetPixel()
        {
            var canvas = new Canvas(10, 20);
            var red = new Color(1, 0, 0);

            canvas.SetPixel(2, 3, red);

            Assert.AreEqual(canvas.PixelAt(2, 3), new Color(1, 0, 0));
        }
    }
}
