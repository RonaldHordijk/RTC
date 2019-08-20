

namespace RTC.Geometry.Objects.Shapes
{
    public class Plane : Shape
    {
        private const double Epsilon = 0.00001;

        protected override Intersections LocalIntersection(Ray rayTransformed)
        {
            if (System.Math.Abs(rayTransformed.Direction.Y) < Epsilon)
                return new Intersections();

            var distance = -rayTransformed.Origin.Y / rayTransformed.Direction.Y;

            return new Intersections
            {
                new Intersection (distance, this)
            };
        }

        protected override Tuple LocalNormal(Tuple objectPoint)
        {
            return Tuple.Vector(0, 1, 0);
        }
    }
}
