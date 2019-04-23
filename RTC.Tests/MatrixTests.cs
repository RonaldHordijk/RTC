using NUnit.Framework;
using RTC.Geometry;

namespace RTC.Tests.Geometry
{
    [TestFixture]
    public class MatrixTests
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestNewMatrix4()
        {
            var m = new Matrix4(1, 2, 3, 4,
                                5.5, 6.5, 7.5, 8.5,
                                9, 10, 11, 12,
                                13.5, 14.5, 15.5, 16.5);


            Assert.AreEqual(1, m.ValueAt(0, 0), Epsilon);
            Assert.AreEqual(4, m.ValueAt(0, 3), Epsilon);
            Assert.AreEqual(5.5, m.ValueAt(1, 0), Epsilon);
            Assert.AreEqual(7.5, m.ValueAt(1, 2), Epsilon);
            Assert.AreEqual(11, m.ValueAt(2, 2), Epsilon);
            Assert.AreEqual(13.5, m.ValueAt(3, 0), Epsilon);
            Assert.AreEqual(15.5, m.ValueAt(3, 2), Epsilon);
        }
    }
}
