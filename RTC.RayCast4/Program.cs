using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;
using RTC.Materials;

namespace RTC.RayCast4
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
                    Color = new Color(1, 0.9, 0.9),
                    Specular = 0
                }
            };


            var middle = new Sphere()
            {
                Transform = Matrix4.Translation(-0.5, 1, 0.5),
                Material = new Material
                {
                    Color = new Color(0.1, 1, 0.5),
                    Diffuse = 0.7,
                    Specular = 0.3
                }
            };

            var right = new Sphere()
            {
                Transform = Matrix4.Translation(1.5, 0.5, -0.5) * Matrix4.Scaling(0.5, 0.5, 0.5),
                Material = new Material
                {
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

            var image = Renderer.Render(cam, world, p => System.Console.Write($"\rRendering {100 * p:N2}"));
            PPMWriter.WriteToFile(image, "RayCast4.ppm");
        }
    }
}
