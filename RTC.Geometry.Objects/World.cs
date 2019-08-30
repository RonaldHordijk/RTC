using RTC.Drawing;
using RTC.Geometry.Objects.Shapes;
using RTC.Geometry.Objects.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RTC.Geometry.Objects
{
    public class World
    {
        public PointLight Light { get; set; }
        public List<Shape> Shapes { get; set; } = new List<Shape>();

        public Intersections Intersect(Ray ray)
        {
            var result = new Intersections();

            result.AddRange(Shapes
                .Select(o => o.Intersect(ray))
                .SelectMany(i => i));

            result.Sort((l, r) => (l.Distance - r.Distance) > 0 ? 1 : -1);
            return result;
        }

        public Color ColorAt(Ray ray, int remaining = 5)
        {
            var intersections = Intersect(ray);
            var hit = intersections.Hit();
            if (hit is null)
                return new Color(0, 0, 0);

            var comps = Computation.Prepare(hit, ray, intersections);

            return LightUtil.ShadeHit(this, comps, remaining);
        }

        public Color ReflectedColor(Computation comp, int remaining = 5)
        {
            const double Epsilon = 0.00001;

            if (remaining <= 0)
                return new Color(0, 0, 0);

            if (comp.Shape.Material.Reflective < Epsilon)
                return new Color(0, 0, 0);

            var ray = new Ray(comp.OverPoint, comp.ReflectVector);
            var color = ColorAt(ray, remaining - 1);
            return comp.Shape.Material.Reflective * color;
        }

        public Color RefractColor(Computation comps, int remaining = 5)
        {
            const double Epsilon = 0.00001;

            if (remaining <= 0)
                return new Color(0, 0, 0);

            if (comps.Shape.Material.Transparency < Epsilon)
                return new Color(0, 0, 0);

            var nRatio = comps.N1 / comps.N2;
            var cosI = Tuple.Dot(comps.EyeVector, comps.NormalVector);
            var sin2T = nRatio * nRatio * (1 - cosI * cosI);

            // Total internal reflection
            if (sin2T > 1)
                return new Color(0, 0, 0);

            var cosT = System.Math.Sqrt(1.0 - sin2T);

            var dir = (nRatio * cosI - cosT) * comps.NormalVector - nRatio * comps.EyeVector;
            var refractRay = new Ray(comps.UnderPoint, dir);

            return comps.Shape.Material.Transparency * ColorAt(refractRay, remaining - 1);
        }
    }
}
