
using RTC.Geometry.Objects.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace RTC.Geometry.Objects
{
    public class Computation
    {
        private const double Epsilon = 0.00001;

        public double Distance { get; set; }
        public Shape Shape { get; set; }
        public Tuple Point { get; set; }
        public Tuple OverPoint { get; set; }
        public Tuple UnderPoint { get; set; }
        public Tuple EyeVector { get; set; }
        public Tuple NormalVector { get; set; }
        public Tuple ReflectVector { get; set; }
        public bool Inside { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }

        public static Computation Prepare(Intersection hit, Ray ray, Intersections intersections = null)
        {
            (double n1, double n2) = GetRefractiveValues(hit, intersections);

            var point = ray.Position(hit.Distance);
            var eyeVector = -ray.Direction;
            var normalVector = hit.Shape?.Normal(point) ?? default;
            bool inside = false;

            if (Tuple.Dot(normalVector, eyeVector) < 0)
            {
                inside = true;
                normalVector = -normalVector;
            }

            return new Computation
            {
                Distance = hit.Distance,
                Shape = hit.Shape,
                Point = point,
                OverPoint = point + Epsilon * normalVector,
                UnderPoint = point - Epsilon * normalVector,
                EyeVector = eyeVector,
                NormalVector = normalVector,
                ReflectVector = Tuple.Reflect(ray.Direction, normalVector),
                Inside = inside,
                N1 = n1,
                N2 = n2
            };
        }

        private static (double, double) GetRefractiveValues(Intersection hit, Intersections intersections)
        {
            if (intersections is null)
                return (1.0, 1.0);

            var container = new List<Shape>();
            foreach (var x in intersections)
            {
                if (x == hit)
                {
                    double n1 = 1.0;
                    double n2 = 1.0;

                    if (container.Count != 0)
                    {
                        n1 = container.Last().Material.RefractiveIndex;
                    }

                    if (!container.Contains(x.Shape))
                    {
                        n2 = x.Shape.Material.RefractiveIndex;
                    }
                    else if (container.Count >= 2)
                    {
                        container.Remove(x.Shape);
                        n2 = container.Last().Material.RefractiveIndex;
                    }

                    return (n1, n2);
                }

                if (container.Contains(x.Shape))
                {
                    container.Remove(x.Shape);
                }
                else
                {
                    container.Add(x.Shape);
                }
            }

            return (1.0, 1.0);
        }
    }
}
