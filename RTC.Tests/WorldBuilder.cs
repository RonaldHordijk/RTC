using RTC.Drawing;
using RTC.Geometry;
using RTC.Geometry.Objects;

namespace RTC.Tests
{
    static internal class WorldBuilder
    {
        static internal World DefaultWorld()
        {
            return new World
            {
                Light = new PointLight(Tuple.Point(-10, 10, -10), new Color(1, 1, 1)),
                Objects =
                {
                    new Sphere
                        {
                            Material = new Material
                            {
                                Color = new Color(0.8, 1.0, 0.6),
                                Diffuse = 0.7,
                                Specular = 0.2
                            }
                        },
                    new Sphere
                        {
                            Transform = Matrix4.Scaling(0.5, 0.5, 0.5)
                        }
                }
            };
        }
    }
}
