using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;
using RTC.Materials;

namespace RTC.Tests
{
    [TestFixture]
    public class TestPattern
    {
        private readonly Color _white = new Color(1, 1, 1);
        private readonly Color _black = new Color(0, 0, 0);


        public class Test_Pattern : AbstractPattern
        {
            public override Color ColorAt(Tuple pos)
            {
                return new Color(pos.X, pos.Y, pos.Z);
            }
        }

        [Test]
        public void TestCreate()
        {
            var pattern = new StripedPattern(_white, _black);
            Assert.AreEqual(_white, pattern.A);
            Assert.AreEqual(_black, pattern.B);
        }

        [Test]
        public void TestConstantY()
        {
            var pattern = new StripedPattern(_white, _black);
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 0, 0)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 1, 0)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 2, 0)));
        }

        [Test]
        public void TestConstantZ()
        {
            var pattern = new StripedPattern(_white, _black);
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 0, 0)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 0, 1)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 0, 2)));
        }

        [Test]
        public void TestVariableX()
        {
            var pattern = new StripedPattern(_white, _black);
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0, 0, 0)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(0.9, 0, 0)));
            Assert.AreEqual(_black, pattern.ColorAt(Tuple.Point(1, 0, 0)));

            Assert.AreEqual(_black, pattern.ColorAt(Tuple.Point(-0.1, 0, 0)));
            Assert.AreEqual(_black, pattern.ColorAt(Tuple.Point(-1, 0, 0)));
            Assert.AreEqual(_white, pattern.ColorAt(Tuple.Point(-1.1, 0, 0)));
        }

        [Test]
        public void TestLighting()
        {
            var m = new Material
            {
                Ambient = 1,
                Diffuse = 0,
                Specular = 0,
                Pattern = new StripedPattern(_white, _black)
            };
            var s = new Sphere
            {
                Material = m
            };

            var eye = Tuple.Vector(0, 0, -1);
            var normal = Tuple.Vector(0, 0, -1);
            var light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));

            Assert.AreEqual(_white, LightUtil.Lighting(m, s, light, Tuple.Point(0.9, 0, 0), eye, normal, false));
            Assert.AreEqual(_black, LightUtil.Lighting(m, s, light, Tuple.Point(1.1, 0, 0), eye, normal, false));
        }

        [Test]
        public void TestObjectTransform()
        {
            var o = new Sphere
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };
            var pattern = new StripedPattern(_white, _black);

            Assert.AreEqual(_white, pattern.ColorAtObject(o.WorldToObject(Tuple.Point(1.5, 0, 0))));
        }

        [Test]
        public void TestPatternTransform()
        {
            var o = new Sphere();
            var pattern = new StripedPattern(_white, _black)
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };

            Assert.AreEqual(_white, pattern.ColorAtObject(o.WorldToObject(Tuple.Point(1.5, 0, 0))));
        }

        [Test]
        public void TestObjectAndPatternTransform()
        {
            var o = new Sphere()
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };
            var pattern = new StripedPattern(_white, _black)
            {
                Transform = Matrix4.Translation(0.5, 0, 0)
            };

            Assert.AreEqual(_white, pattern.ColorAtObject(o.WorldToObject(Tuple.Point(2.5, 0, 0))));
        }

        [Test]
        public void TestDefaultTransformation()
        {
            var p = new Test_Pattern();

            Assert.AreEqual(Matrix4.Identity, p.Transform);
        }

        [Test]
        public void TestDefaultAssignTransformation()
        {
            var p = new Test_Pattern();
            p.Transform = Matrix4.Translation(1, 2, 3);

            Assert.AreEqual(Matrix4.Translation(1, 2, 3), p.Transform);
        }

        [Test]
        public void TestDefaultObjectTransform()
        {
            var o = new Sphere
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };
            var pattern = new Test_Pattern();

            var c = pattern.ColorAtObject(o.WorldToObject(Tuple.Point(2, 3, 4)));
            Assert.AreEqual(new Color(1,1.5,2), c);
        }

        [Test]
        public void TestDefaultPatternTransform()
        {
            var o = new Sphere();
            var pattern = new Test_Pattern
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };

            var c = pattern.ColorAtObject(o.WorldToObject(Tuple.Point(2, 3, 4)));
            Assert.AreEqual(new Color(1, 1.5, 2), c);
        }

        [Test]
        public void TestDefaultObjectAndPatternTransform()
        {
            var o = new Sphere()
            {
                Transform = Matrix4.Scaling(2, 2, 2)
            };
            var pattern = new Test_Pattern
            {
                Transform = Matrix4.Translation(0.5, 1, 1.5)
            };

            var c = pattern.ColorAtObject(o.WorldToObject(Tuple.Point(2.5, 3, 3.5)));
            Assert.AreEqual(new Color(0.75, 0.5, 0.25), c);
        }
    }
}
