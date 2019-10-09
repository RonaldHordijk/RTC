using System;

namespace RTC.Geometry.Objects.Shapes
{
    public class Cube : Shape
    {
        private const double Epsilon = 0.00001;

        private (double, double) CheckAxis(double origin, double dir)
        {
            var tmin = -1 - origin;
            var tmax = 1 - origin;

            if (Math.Abs(dir) < Epsilon)
            {
                tmin *= 1E9;
                tmax *= 1E9;
            }

            tmin /= dir;
            tmax /= dir;

            if (tmin < tmax)
                return (tmin, tmax);

            return (tmax, tmin);
        }


        protected override Intersections LocalIntersection(Ray rayTransformed)
        {
            (double xtmin, double xtmax) = CheckAxis(rayTransformed.Origin.X, rayTransformed.Direction.X);
            (double ytmin, double ytmax) = CheckAxis(rayTransformed.Origin.Y, rayTransformed.Direction.Y);
            (double ztmin, double ztmax) = CheckAxis(rayTransformed.Origin.Z, rayTransformed.Direction.Z);

            double tmin = Math.Max(xtmin, Math.Max(ytmin, ztmin));
            double tmax = Math.Min(xtmax, Math.Min(ytmax, ztmax));

            if (tmin > tmax)
                return new Intersections();

            return new Intersections
            {
                new Intersection(tmin, this),
                new Intersection(tmax, this)
            };
        }

        protected override Tuple LocalNormal(Tuple objectPoint)
        {
            var maxc = Math.Max(Math.Abs(objectPoint.X),
                                Math.Max(Math.Abs(objectPoint.Y), Math.Abs(objectPoint.Z)));

            if (maxc == Math.Abs(objectPoint.X))
                return Tuple.Vector(objectPoint.X, 0, 0);

            if (maxc == Math.Abs(objectPoint.Y))
                return Tuple.Vector(0, objectPoint.Y, 0);

            return Tuple.Vector(0, 0, objectPoint.Z);
        }
    }
}
