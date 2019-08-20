using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;

namespace RTC.RayCast2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 100;
            const int height = 100;
            var canvas = new Canvas(width, height);

            var sphere = new Sphere();
            sphere.Material.Color = new Color(1, 0.2, 1);

            var light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1));

            var origin = Tuple.Point(0, 0, -5);
            var red = new Color(255, 0, 0);

            var scale = 0.01;

            for (int x = 0; x < width; x++)
            {
                System.Console.Write(".");
                for (int y = 0; y < height; y++)
                {
                    var ray = new Ray(origin,
                        Tuple.Vector(scale * (x - 0.5 * width),
                                     scale * (y - 0.5 * height), 1));
                    ray.Direction.Normalize();

                    var intersections = sphere.Intersect(ray);

                    if (intersections.Count > 0)
                    {
                        var hit = intersections.Hit();
                        var hitObject = hit.Shape;
                        var point = ray.Position(hit.Distance);
                        var normal = hitObject.Normal(point);
                        var eye = -ray.Direction;

                        var color = LightUtil.Lighting(hitObject.Material, hitObject, light, point, eye, normal, false);

                        canvas.SetPixel(x, y, color);
                    }
                }
            }

            PPMWriter.WriteToFile(canvas, "RayCast2.ppm");
        }
    }
}
