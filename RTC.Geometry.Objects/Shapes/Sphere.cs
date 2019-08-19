namespace RTC.Geometry.Objects.Shapes

{
    public class Sphere : Shape
    {
        protected override Intersections LocalIntersection(Ray rayTransformed)
        {
            var sphereToRay = Tuple.Vector(rayTransformed.Origin);
            var a = Tuple.Dot(rayTransformed.Direction, rayTransformed.Direction);
            var b = 2 * Tuple.Dot(rayTransformed.Direction, sphereToRay);
            var c = Tuple.Dot(sphereToRay, sphereToRay) - 1.0;

            var discriminant = (b * b) - (4 * a * c);
            if (discriminant < 0)
                return new Intersections();

            var t1 = (-b - System.Math.Sqrt(discriminant)) / (2 * a);
            var t2 = (-b + System.Math.Sqrt(discriminant)) / (2 * a);
            if (t1 < t2)
            {
                return new Intersections {
                    new Intersection(t1, this),
                    new Intersection(t2, this) };
            }

            return new Intersections {
                    new Intersection(t2, this),
                    new Intersection(t1, this) };
        }

        protected override Tuple LocalNormal(Tuple objectPoint)
        {
            return Tuple.Vector(objectPoint.X, objectPoint.Y, objectPoint.Z);
        }
    }
}
