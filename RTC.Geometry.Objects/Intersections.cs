using System.Collections.Generic;
using System.Linq;

namespace RTC.Geometry.Objects
{
    public class Intersections : List<Intersection>
    {
        public Intersection Hit()
        {
            Sort((i1, i2) => (i1.Distance - i2.Distance > 0) ? 1 : -1);

            return this.FirstOrDefault(i => i.Distance > 0);
        }
    }
}
