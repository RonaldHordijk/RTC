using NUnit.Framework;
using RTC.Geometry;

namespace RTC.Tests.Geometry
{
    public class TupleTests
    {
        private const double Epsilon = 0.00001;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestTuplePoint()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 1.0);

            Assert.AreEqual(4.3, a.X, Epsilon);
            Assert.AreEqual(-4.2, a.Y, Epsilon);
            Assert.AreEqual(3.1, a.Z, Epsilon);
            Assert.AreEqual(1.0, a.W, Epsilon);

            Assert.True(a.IsPoint);
            Assert.False(a.IsVector);
        }

        [Test]
        public void TestTupleVector()
        {
            var a = new Tuple(4.3, -4.2, 3.1, 0.0);

            Assert.AreEqual(4.3, a.X, Epsilon);
            Assert.AreEqual(-4.2, a.Y, Epsilon);
            Assert.AreEqual(3.1, a.Z, Epsilon);
            Assert.AreEqual(0.0, a.W, Epsilon);

            Assert.False(a.IsPoint);
            Assert.True(a.IsVector);
        }

        [Test]
        public void TestTupleNewPoint()
        {
            var a = Tuple.Point(4, -4, 3);
            var t = new Tuple(4, -4, 3, 1);

            Assert.AreEqual(a, t);
        }

        [Test]
        public void TestTupleNewVector()
        {
            var v = Tuple.Vector(4, -4, 3);
            var t = new Tuple(4, -4, 3, 0);

            Assert.AreEqual(v, t);
        }

        [Test]
        public void TestAddTwoTuples()
        {
            var a1 = new Tuple(3, -2, 5, 1);
            var a2 = new Tuple(-2, 3, 1, 0);

            var sum = a1 + a2;
            var expectedResult = new Tuple(1, 1, 6, 1);
            Assert.AreEqual(expectedResult, sum);
        }

        [Test]
        public void TestSubtractingTwoPoints()
        {
            var p1 = Tuple.Point(3, 2, 1);
            var p2 = Tuple.Point(5, 6, 7);

            var sub = p1 - p2;
            var expectedResult = Tuple.Vector(-2, -4, -6);
            Assert.AreEqual(expectedResult, sub);
        }

        [Test]
        public void TestSubtractingVectorFromPoint()
        {
            var p1 = Tuple.Point(3, 2, 1);
            var v2 = Tuple.Vector(5, 6, 7);

            var sub = p1 - v2;
            var expectedResult = Tuple.Point(-2, -4, -6);
            Assert.AreEqual(expectedResult, sub);
        }

        [Test]
        public void TestSubtractingTwoVectors()
        {
            var v1 = Tuple.Vector(3, 2, 1);
            var v2 = Tuple.Vector(5, 6, 7);

            var sub = v1 - v2;
            var expectedResult = Tuple.Vector(-2, -4, -6);
            Assert.AreEqual(expectedResult, sub);
        }

        [Test]
        public void TestSubtractingVectorFromZeros()
        {
            var v1 = Tuple.Vector(1, -2, 3);
            var zero = Tuple.Vector(0, 0, 0);

            var sub = zero - v1;
            var expectedResult = Tuple.Vector(-1, 2, -3);
            Assert.AreEqual(expectedResult, sub);
        }

        [Test]
        public void TestNegateTuple()
        {
            var v1 = Tuple.Vector(1, -2, 3);
            var expectedResult = Tuple.Vector(-1, 2, -3);
            Assert.AreEqual(expectedResult, -v1);
        }

        [Test]
        public void TestMultiplyTupleByScalar()
        {
            var t = new Tuple(1, -2, 3, -4);
            var expectedResult = new Tuple(3.5, -7, 10.5, -14);
            Assert.AreEqual(expectedResult, 3.5 * t);
        }

        [Test]
        public void TestMultiplyTupleByFraction()
        {
            var t = new Tuple(1, -2, 3, -4);
            var expectedResult = new Tuple(0.5, -1, 1.5, -2);
            Assert.AreEqual(expectedResult, 0.5 * t);
        }

        [Test]
        public void TestDividetuple()
        {
            var t = new Tuple(1, -2, 3, -4);
            var expectedResult = new Tuple(0.5, -1, 1.5, -2);
            Assert.AreEqual(expectedResult, t / 2.0);
        }

        [TestCase(1.0, 0.0, 0.0, 1.0)]
        [TestCase(0.0, 1.0, 0.0, 1.0)]
        [TestCase(0.0, 0.0, 1.0, 1.0)]
        [TestCase(1.0, 2.0, 3.0, 3.741657386773941)]
        [TestCase(-1.0, -2.0, -3.0, 3.741657386773941)]
        public void TestMagnitude(double x, double y, double z, double expectedResult)
        {
            var v = Tuple.Vector(x, y, z);
            Assert.AreEqual(expectedResult, v.Magnitude, Epsilon);
        }

        [Test]
        public void TestNormalization()
        {
            var v = Tuple.Vector(0, 0, 4);
            v.Normalize();

            Assert.AreEqual(Tuple.Vector(0, 0, 1), v);

            var v2 = Tuple.Vector(1, 2, 3);
            v2.Normalize();
            Assert.AreEqual(Tuple.Vector(0.26726, 0.53452, 0.80178), v2);
        }

        [Test]
        public void TestNormalizationMagnitude()
        {
            var v = Tuple.Vector(1, 2, 3);
            v.Normalize();

            Assert.AreEqual(1.0, v.Magnitude, Epsilon);
        }

        [Test]
        public void TestDotProduct()
        {
            var v1 = Tuple.Vector(1, 2, 3);
            var v2 = Tuple.Vector(2, 3, 4);

            Assert.AreEqual(20.0, Tuple.Dot(v1, v2));
        }

        [Test]
        public void TestCrossProduct()
        {
            var v1 = Tuple.Vector(1, 2, 3);
            var v2 = Tuple.Vector(2, 3, 4);

            Assert.AreEqual(Tuple.Vector(-1, 2, -1), Tuple.Cross(v1, v2));
            Assert.AreEqual(Tuple.Vector(1, -2, 1), Tuple.Cross(v2, v1));
        }

        [Test]
        public void TestReflect()
        {
            var v = Tuple.Vector(1, -1, 0);
            var n = Tuple.Vector(0, 1, 0);

            Assert.AreEqual(Tuple.Vector(1, 1, 0), Tuple.Reflect(v, n));
        }

        [Test]
        public void TestReflect2()
        {
            var v = Tuple.Vector(0, -1, 0);
            var n = Tuple.Vector(0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2), 0);

            Assert.AreEqual(Tuple.Vector(1, 0, 0), Tuple.Reflect(v, n));
        }
    }
}