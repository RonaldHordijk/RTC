using NUnit.Framework;
using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;
using RTC.Materials;
using static RTC.Tests.TestPattern;

namespace RTC.Tests
{
    [TestFixture]
    public class WorldTests
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestEmptyWorld()
        {
            var world = new World();

            Assert.IsNull(world.Light);
            Assert.AreEqual(0, world.Shapes.Count);
        }

        [Test]
        public void TestDefaultWorld()
        {
            var world = WorldBuilder.DefaultWorld();

            Assert.AreEqual(new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)), world.Light);
            Assert.IsTrue(world.Shapes.Contains(new Sphere
            {
                Material = new Material
                {
                    Color = new Color(0.8, 1.0, 0.6),
                    Diffuse = 0.7,
                    Specular = 0.2
                }
            }));
            Assert.IsTrue(world.Shapes.Contains(new Sphere
            {
                Transform = Matrix4.Scaling(0.5, 0.5, 0.5)
            }));
        }

        [Test]
        public void TestDefaultWorldIntersect()
        {
            var world = WorldBuilder.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            var xs = world.Intersect(ray);

            Assert.AreEqual(4, xs.Count);
            Assert.AreEqual(xs[0].Distance, 4.0, Epsilon);
            Assert.AreEqual(xs[1].Distance, 4.5, Epsilon);
            Assert.AreEqual(xs[2].Distance, 5.5, Epsilon);
            Assert.AreEqual(xs[3].Distance, 6.0, Epsilon);
        }

        [Test]
        public void TestPreComputing()
        {
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var i = new Intersection(4, sphere);
            var comp = Computation.Prepare(i, ray);

            Assert.AreEqual(comp.Distance, i.Distance);
            Assert.AreEqual(comp.Shape, i.Shape);
            Assert.AreEqual(comp.Point, Tuple.Point(0, 0, -1));
            Assert.AreEqual(comp.EyeVector, Tuple.Vector(0, 0, -1));
            Assert.AreEqual(comp.NormalVector, Tuple.Vector(0, 0, -1));
            Assert.IsFalse(comp.Inside);
        }

        [Test]
        public void TestPreComputingInside()
        {
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = new Sphere();

            var i = new Intersection(1, sphere);
            var comp = Computation.Prepare(i, ray);

            Assert.AreEqual(comp.Point, Tuple.Point(0, 0, 1));
            Assert.AreEqual(comp.EyeVector, Tuple.Vector(0, 0, -1));
            Assert.AreEqual(comp.NormalVector, Tuple.Vector(0, 0, -1));
            Assert.IsTrue(comp.Inside);
        }

        [Test]
        public void TestShadeIntersection()
        {
            var world = WorldBuilder.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var sphere = world.Shapes[0];
            var i = new Intersection(4, sphere);

            var comp = Computation.Prepare(i, ray);
            var color = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.38066, 0.47583, 0.2855), color);
        }

        [Test]
        public void TestShadeIntersectionInside()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Light = new PointLight(Tuple.Point(0, 0.25, 0), new Color(1, 1, 1));
            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var sphere = world.Shapes[1];
            var i = new Intersection(0.5, sphere);

            var comp = Computation.Prepare(i, ray);
            var color = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.90498, 0.90498, 0.90498), color);
        }

        [Test]
        public void TestIntersectionInShade()
        {
            var world = new World();
            world.Light = new PointLight(Tuple.Point(0, 0, -10), new Color(1, 1, 1));

            world.Shapes.Add(new Sphere());
            world.Shapes.Add(new Sphere
            {
                Transform = Matrix4.Translation(0, 0, 10)
            });

            var s2 = new Sphere
            {
                Transform = Matrix4.Translation(0, 0, 10)
            };
            world.Shapes.Add(s2);

            var ray = new Ray(Tuple.Point(0, 0, 5), Tuple.Vector(0, 0, 1));
            var i = new Intersection(4, s2);

            var comp = Computation.Prepare(i, ray);
            var color = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.1, 0.1, 0.1), color);
        }

        [Test]
        public void IntersectionOn()
        {
            var s = new Sphere
            {
                Transform = Matrix4.Translation(0, 0, 1)
            };

            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var i = new Intersection(5, s);

            var comp = Computation.Prepare(i, ray);

            Assert.That(comp.OverPoint.Z < 0.5 * Epsilon);
            Assert.That(comp.Point.Z > comp.OverPoint.Z);
        }

        [Test]
        public void TestColorAtMiss()
        {
            var world = WorldBuilder.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 1, 0));

            Assert.AreEqual(new Color(0, 0, 0), world.ColorAt(ray));
        }

        [Test]
        public void TestColorAtBehindRay()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Shapes[0].Material.Ambient = 1;
            world.Shapes[1].Material.Ambient = 1;

            var ray = new Ray(Tuple.Point(0, 0, 0.75), Tuple.Vector(0, 0, -1));

            Assert.AreEqual(world.Shapes[1].Material.Color, world.ColorAt(ray));
        }

        [Test]
        public void TestPreComputeReflection()
        {
            var plane = new Plane();
            var ray = new Ray(Tuple.Point(0, 1, -1), Tuple.Vector(0, -0.70710678118, 0.70710678118));
            var i = new Intersection(System.Math.Sqrt(2), plane);

            var comp = Computation.Prepare(i, ray);

            Assert.AreEqual(Tuple.Vector(0, 0.70710678118, 0.70710678118), comp.ReflectVector);
        }

        [Test]
        public void TestReflectOnNonReflectiveShape()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Shapes[1].Material.Ambient = 1.0;

            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 0, 1));
            var i = new Intersection(1, world.Shapes[1]);

            var comp = Computation.Prepare(i, ray);

            var col = world.ReflectedColor(comp);

            Assert.AreEqual(new Color(0, 0, 0), col);
        }

        [Test]
        public void TestReflectOnReflectiveShape()
        {
            var world = WorldBuilder.DefaultWorld();

            var plane = new Plane
            {
                Transform = Matrix4.Translation(0, -1, 0)
            };
            plane.Material.Reflective = 0.5;
            world.Shapes.Add(plane);

            var ray = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2)));
            var i = new Intersection(System.Math.Sqrt(2), plane);

            var comp = Computation.Prepare(i, ray);

            var col = world.ReflectedColor(comp);

            Assert.AreEqual(new Color(0.190332, 0.23791, 0.142749), col);
        }
        public void TestShadeHitOnReflectiveShape()
        {
            var world = WorldBuilder.DefaultWorld();

            var plane = new Plane
            {
                Transform = Matrix4.Translation(0, -1, 0)
            };
            plane.Material.Reflective = 0.5;
            world.Shapes.Add(plane);

            var ray = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2)));
            var i = new Intersection(System.Math.Sqrt(2), plane);

            var comp = Computation.Prepare(i, ray);
            var col = LightUtil.ShadeHit(world, comp);

            Assert.AreEqual(new Color(0.876757, 0.924340, 0.829174), col);
        }

        [Test]
        public void TestInfiniteReflection()
        {
            var world = new World
            {
                Light = new PointLight(Tuple.Point(0, 0, 0), new Color(1, 1, 1)),
                Shapes =
                {
                    new Plane
                    {
                        Material = new Material
                        {
                            Reflective = 1.0
                        },
                        Transform = Matrix4.Translation(0,-1,0)
                    },
                    new Plane
                    {
                        Material = new Material
                        {
                            Reflective = 1.0
                        },
                        Transform = Matrix4.Translation(0,1,0)
                    }
                }
            };


            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 1, 0));
            world.ColorAt(ray);
        }

        [Test]
        public void TestReflectMaxDepth()
        {
            var world = WorldBuilder.DefaultWorld();

            var plane = new Plane
            {
                Transform = Matrix4.Translation(0, -1, 0)
            };
            plane.Material.Reflective = 0.5;
            world.Shapes.Add(plane);

            var ray = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2)));
            var i = new Intersection(System.Math.Sqrt(2), plane);

            var comp = Computation.Prepare(i, ray);

            var col = world.ReflectedColor(comp, 0);

            Assert.AreEqual(new Color(0, 0, 0), col);
        }

        [Test]
        [TestCase(0, 1.0, 1.5)]
        [TestCase(1, 1.5, 2.0)]
        [TestCase(2, 2.0, 2.5)]
        [TestCase(3, 2.5, 2.5)]
        [TestCase(4, 2.5, 1.5)]
        [TestCase(5, 1.5, 1.0)]
        public void TestN1N2AtIntersections(int index, double expectedN1, double expectedN2)
        {
            var sphereA = new Sphere
            {
                Transform = Matrix4.Scaling(2, 2, 2),
                Material = new Material
                {
                    RefractiveIndex = 1.5
                }
            };

            var sphereB = new Sphere
            {
                Transform = Matrix4.Translation(0, 0, -0.25),
                Material = new Material
                {
                    RefractiveIndex = 2.0
                }
            };
            var sphereC = new Sphere
            {
                Transform = Matrix4.Translation(0, 0, 0.25),
                Material = new Material
                {
                    RefractiveIndex = 2.5
                }
            };

            var ray = new Ray(Tuple.Point(0, 0, -4), Tuple.Vector(0, 0, 1));
            var xs = new Intersections
            {
                new Intersection(2, sphereA),
                new Intersection(2.75, sphereB),
                new Intersection(3.25, sphereC),
                new Intersection(4.75, sphereB),
                new Intersection(5.25, sphereC),
                new Intersection(6, sphereA),
            };


            var comp = Computation.Prepare(xs[index], ray, xs);
            Assert.AreEqual(expectedN1, comp.N1, Epsilon);
            Assert.AreEqual(expectedN2, comp.N2, Epsilon);
        }

        [Test]
        public void IntersectionUnder()
        {
            var s = new Sphere
            {
                Transform = Matrix4.Translation(0, 0, 1)
            };

            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));
            var i = new Intersection(5, s);

            var comp = Computation.Prepare(i, ray);

            Assert.That(comp.UnderPoint.Z > 0.5 * Epsilon);
            Assert.That(comp.Point.Z < comp.UnderPoint.Z);
        }

        [Test]
        public void TestRefractColor()
        {
            var world = WorldBuilder.DefaultWorld();
            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            var xs = new Intersections
            {
                new Intersection(4, world.Shapes[0]),
                new Intersection(6, world.Shapes[0])
            };

            var comps = Computation.Prepare(xs[0], ray, xs);
            var col = world.RefractColor(comps, 5);

            Assert.AreEqual(new Color(0, 0, 0), col);
        }

        [Test]
        public void TestRefractMaxDepth()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Shapes[0].Material.Transparency = 1.0;
            world.Shapes[0].Material.RefractiveIndex = 1.5;

            var ray = new Ray(Tuple.Point(0, 0, -5), Tuple.Vector(0, 0, 1));

            var xs = new Intersections
            {
                new Intersection(4, world.Shapes[0]),
                new Intersection(6, world.Shapes[0])
            };

            var comps = Computation.Prepare(xs[0], ray, xs);
            var col = world.RefractColor(comps, 0);

            Assert.AreEqual(new Color(0, 0, 0), col);
        }

        [Test]
        public void TestRefractColorUnderTotalInternalReflection()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Shapes[0].Material.Transparency = 1.0;
            world.Shapes[0].Material.RefractiveIndex = 1.5;

            var ray = new Ray(Tuple.Point(0, 0, 0.5 * System.Math.Sqrt(2)), Tuple.Vector(0, 1, 0));

            var xs = new Intersections
            {
                new Intersection(-0.5 * System.Math.Sqrt(2), world.Shapes[0]),
                new Intersection( 0.5 * System.Math.Sqrt(2), world.Shapes[0])
            };

            var comps = Computation.Prepare(xs[1], ray, xs);
            var col = world.RefractColor(comps, 5);

            Assert.AreEqual(new Color(0, 0, 0), col);
        }

        [Test]
        public void TestRefractRayColor()
        {
            var world = WorldBuilder.DefaultWorld();
            world.Shapes[0].Material.Ambient = 1.0;
            world.Shapes[0].Material.Pattern = new Test_Pattern();
            world.Shapes[1].Material.Transparency = 1.0;
            world.Shapes[1].Material.RefractiveIndex = 1.5;

            var ray = new Ray(Tuple.Point(0, 0, 0.1), Tuple.Vector(0, 1, 0));

            var xs = new Intersections
            {
                new Intersection(-0.9899, world.Shapes[0]),
                new Intersection(-0.4899, world.Shapes[1]),
                new Intersection( 0.4899, world.Shapes[1]),
                new Intersection( 0.9899, world.Shapes[0])
            };

            var comps = Computation.Prepare(xs[2], ray, xs);
            var col = world.RefractColor(comps, 5);

            Assert.AreEqual(new Color(0, 0.998884, 0.047219), col);
        }

        [Test]
        public void TestRefractShadeHit()
        {
            var world = WorldBuilder.DefaultWorld();
            var floor = new Plane
            {
                Transform = Matrix4.Translation(0, -1, 0),
                Material = new Material
                {
                    Reflective = 0.5,
                    Transparency = 0.5,
                    RefractiveIndex = 1.5
                }
            };
            var ball = new Sphere
            {
                Transform = Matrix4.Translation(0, -3.5, -0.5),
                Material = new Material
                {
                    Color = new Color(1, 0, 0),
                    Ambient = 0.5
                }
            };

            world.Shapes.Add(floor);
            world.Shapes.Add(ball);

            var ray = new Ray(Tuple.Point(0, 0, -3), Tuple.Vector(0, -0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2)));

            var xs = new Intersections
            {
                new Intersection(System.Math.Sqrt(2), floor),
            };

            var comps = Computation.Prepare(xs[0], ray, xs);
            var color = LightUtil.ShadeHit(world, comps, 5);

            Assert.AreEqual(new Color(0.93391, 0.69643, 0.69243), color);
            //Assert.AreEqual(new Color(0.93642, 0.68642, 0.68642), color);
            
        }

        [Test]
        public void TestSchlickTotalInternalReflection()
        {
            var sphere = new Sphere
            {
                Material = new Material
                {
                    Transparency = 1.0,
                    RefractiveIndex = 1.5
                }
            };

            var ray = new Ray(Tuple.Point(0, 0, 0.5 * System.Math.Sqrt(2)), Tuple.Vector(0, 1, 0));

            var xs = new Intersections
            {
                new Intersection(-0.5 * System.Math.Sqrt(2), sphere),
                new Intersection( 0.5 * System.Math.Sqrt(2), sphere)
            };

            var comps = Computation.Prepare(xs[1], ray, xs);
            var reflectance = comps.Schlick();

            Assert.AreEqual(1.0, reflectance);
        }

        [Test]
        public void TestSchlickPerpendicular()
        {
            var sphere = new Sphere
            {
                Material = new Material
                {
                    Transparency = 1.0,
                    RefractiveIndex = 1.5
                }
            };

            var ray = new Ray(Tuple.Point(0, 0, 0), Tuple.Vector(0, 1, 0));

            var xs = new Intersections
            {
                new Intersection(-1, sphere),
                new Intersection( 1, sphere)
            };

            var comps = Computation.Prepare(xs[1], ray, xs);
            var reflectance = comps.Schlick();

            Assert.AreEqual(0.04, reflectance, Epsilon);
        }

        [Test]
        public void TestSchlickSmallAngle()
        {
            var sphere = new Sphere
            {
                Material = new Material
                {
                    Transparency = 1.0,
                    RefractiveIndex = 1.5
                }
            };

            var ray = new Ray(Tuple.Point(0, 0.99, -2), Tuple.Vector(0, 0, 1));

            var xs = new Intersections
            {
                new Intersection(1.8589, sphere),
            };

            var comps = Computation.Prepare(xs[0], ray, xs);
            var reflectance = comps.Schlick();

            Assert.AreEqual(0.48873, reflectance, Epsilon);
        }
    }
}
