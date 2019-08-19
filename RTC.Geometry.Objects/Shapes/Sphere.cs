namespace RTC.Geometry.Objects.Shapes

{
    public class Sphere : Shape
    {
        public override Intersections Intersect(Ray ray)
        {
            var rayTransFormed = ray.Transform(Transform.Inverse());

            var sphereToRay = Tuple.Vector(rayTransFormed.Origin);
            var a = Tuple.Dot(rayTransFormed.Direction, rayTransFormed.Direction);
            var b = 2 * Tuple.Dot(rayTransFormed.Direction, sphereToRay);
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

        public override Tuple Normal(Tuple worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = Tuple.Vector(objectPoint.X, objectPoint.Y, objectPoint.Z);

            var worldNormal = Transform.Inverse().Transpose * objectNormal;
            worldNormal.W = 0;

            return worldNormal.Normalized();
        }
    }
}
