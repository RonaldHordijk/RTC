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

        public Color ColorAt(Ray ray)
        {
            var intersections = Intersect(ray);
            var hit = intersections.Hit();
            if (hit is null)
                return new Color(0, 0, 0);

            var comps = Computation.Prepare(hit, ray);

            return LightUtil.ShadeHit(this, comps);
        }
    }
}
