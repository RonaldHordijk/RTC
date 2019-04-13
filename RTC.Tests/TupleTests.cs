using NUnit.Framework;
using RTC.Geometry;

namespace Tests
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

  }
}