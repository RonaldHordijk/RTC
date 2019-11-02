namespace RTC.Geometry.Objects.Shapes
{
    public class Cylinder : Shape
    {
        private const double Epsilon = 0.00001;

        public double Minimum { get; set; } = double.NegativeInfinity;
        public double Maximum { get; set; } = double.PositiveInfinity;
        public bool Closed { get; set; }

        protected override Intersections LocalIntersection(Ray rayTransformed)
        {
            var dir = rayTransformed.Direction;
            var origin = rayTransformed.Origin;

            var a = dir.X * dir.X + dir.Z * dir.Z;

            if (System.Math.Abs(a) < Epsilon)
            {
                var i = new Intersections();
                CapIntersection(rayTransformed, ref i);
                return i;
            }

            var b = 2 * origin.X * dir.X + 2 * origin.Z * dir.Z;
            var c = origin.X * origin.X + origin.Z * origin.Z - 1.0;

            var disc = b * b - 4 * a * c;

            if (disc < 0)
            {
                var i = new Intersections();
                CapIntersection(rayTransformed, ref i);
                return i;
            }

            var t0 = (-b - System.Math.Sqrt(disc)) / (2 * a);
            var t1 = (-b + System.Math.Sqrt(disc)) / (2 * a);

            if (t0 > t1​)
            {
                var t = t1;
                t1 = t0;
                t0 = t;
            }

            var intersections = new Intersections();

            var y0 = rayTransformed.Origin.Y + t0 * rayTransformed.Direction.Y;
            if ((Minimum < y0) && (Maximum > y0))
            {
                intersections.Add(new Intersection(t0, this));
            }

            var y1 = rayTransformed.Origin.Y + t1 * rayTransformed.Direction.Y;
            if ((Minimum < y1) && (Maximum > y1))
            {
                intersections.Add(new Intersection(t1, this));
            }

            CapIntersection(rayTransformed, ref intersections);

            return intersections;
        }

        private void CapIntersection(Ray ray, ref Intersections intersections)
        {
            if (!Closed)
                return;

            if (System.Math.Abs(ray.Direction.Y) < Epsilon)
                return;

            var t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t))
                intersections.Add(new Intersection(t, this));

            t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t))
                intersections.Add(new Intersection(t, this));
        }

        private bool CheckCap(Ray ray, double t)
        {
            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;

            return (x * x + z * z) <= 1;
        }

        protected override Tuple LocalNormal(Tuple objectPoint)
        {
            var dist = objectPoint.X * objectPoint.X + objectPoint.Z * objectPoint.Z;

            if (dist < 1 && objectPoint.Y >= Maximum - Epsilon)
                return Tuple.Vector(0, 1, 0);

            if (dist < 1 && objectPoint.Y <= Minimum + Epsilon)
                return Tuple.Vector(0, -1, 0);

            return Tuple.Vector(objectPoint.X, 0, objectPoint.Z);
        }
    }
}
