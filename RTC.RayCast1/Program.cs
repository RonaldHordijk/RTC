using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;

namespace RTC.RayCast1
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 100;
            const int height = 100;
            var canvas = new Canvas(width, height);

            var sphere = new Sphere();

            var origin = Tuple.Point(0, 0, -5);
            var red = new Color(255, 0, 0);

            var scale = 0.01;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var ray = new Ray(origin,
                        Tuple.Vector(scale * (x - 0.5 * width),
                                     scale * (y - 0.5 * height), 1));

                    if (sphere.Intersect(ray).Count > 0)
                    {
                        canvas.SetPixel(x, y, red);
                    }
                }
            }

            PPMWriter.WriteToFile(canvas, "RayCast1.ppm");
        }
    }
}
