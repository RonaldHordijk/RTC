using NUnit.Framework;
using RTC.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTC.Tests.Drawing
{
  [TestFixture]
  public class TestColor
  {
    private const double Epsilon = 0.00001;

    [Test]
    public void TestNewColor()
    {
      var c = new Color(-0.5, 0.4, 1.7);

      Assert.AreEqual(-0.5, c.Red, Epsilon);
      Assert.AreEqual(0.4, c.Green, Epsilon);
      Assert.AreEqual(1.7, c.Blue, Epsilon);
    }

    [Test]
    public void TestAddColor()
    {
      var c1 = new Color(0.9, 0.6, 0.75);
      var c2 = new Color(0.7, 0.1, 0.25);

      Assert.AreEqual(c1 + c2, new Color(1.6, 0.7, 1.0));
    }

    [Test]
    public void TestSubtractColor()
    {
      var c1 = new Color(0.9, 0.6, 0.75);
      var c2 = new Color(0.7, 0.1, 0.25);

      Assert.AreEqual(c1 - c2, new Color(0.2, 0.5, 0.5));
    }

    [Test]
    public void TestMultiplyColor()
    {
      var c = new Color(0.2, 0.3, 0.4);

      Assert.AreEqual(2 * c, new Color(0.4, 0.6, 0.8));
    }

    [Test]
    public void TestMultiplyColors()
    {
      var c1 = new Color(1, 0.2, 0.4);
      var c2 = new Color(0.9, 1, 0.1);

      Assert.AreEqual(c1 * c2, new Color(0.9, 0.2, 0.04));
    }
  }
}
