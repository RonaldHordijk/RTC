using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;
using RTC.Materials;
using System.Diagnostics;

namespace RTC.RayCast7
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 1000;
            const int height = 500;

            var floor = new Plane()
            {
                Material = new Material()
                {
                    Pattern = new NoisePattern(
                        new SpiralPattern(new Color(1, 0.9, 0.9), new Color(0.5, 0.3, 0.3))
                        {
                            Transform = Matrix4.Translation(0, 0, 2)
                        }
                    ),
                    Color = new Color(1, 0.9, 0.9),
                    Specular = 0,
                    Reflective = 0.31
                }
            };

            var middle = new Sphere()
            {
                Transform = Matrix4.Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Pattern = new CheckerPattern(new Color(0.1, 1, 0.5), new Color(0.0, 0.5, 0.3))
                    {
                        Transform = Matrix4.Scaling(0.2, 0.2, 0.2)
                    },
                    Color = new Color(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.3,
                    Reflective = 0.9
                }
            };

            var right = new Sphere()
            {
                Transform = Matrix4.Translation(1.5, 0.5, -0.5) * Matrix4.Scaling(0.5, 0.5, 0.5),
                Material = new Material
                {
                    Pattern = new StripedPattern(new Color(0.5, 1, 0.1), new Color(0.25, 0.5, 0.03))
                    {
                        Transform = Matrix4.Scaling(0.1, 0.1, 0.1).RotateY(0.6)
                    },

                    Color = new Color(0.5, 1, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            };

            var left = new Sphere()
            {
                Transform = Matrix4.Translation(-1.5, 0.33, -0.75) * Matrix4.Scaling(0.33, 0.33, 0.33),
                Material = new Material
                {
                    Pattern = new GradientPattern(new Color(0, 0, 1), new Color(1, 0, 0))
                    {
                        Transform = Matrix4.Scaling(0.2, 0.2, 0.2).RotateY(-0.6)
                    },
                    Color = new Color(1, 0.8, 0.1),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            };

            var world = new World
            {
                Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)),
                Shapes = { floor, middle, left, right }
            };

            var cam = new Camera(width, height, System.Math.PI / 3)
            {
                Transform = Matrix4.ViewTransform(
                    Tuple.Point(0, 1.5, -5),
                    Tuple.Point(0, 1, 0),
                    Tuple.Vector(0, 1, 0))
            };

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var image = Renderer.Render(cam, world, p => System.Console.Write($"\rRendering {100 * p:N2}"));
            PPMWriter.WriteToFile(image, "RayCast7.ppm");

            stopwatch.Stop();
            System.Console.WriteLine($"Elapsed {stopwatch.Elapsed}");
        }
    }
}
