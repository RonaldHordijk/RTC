namespace RTC.Geometry.Objects
{
    public class Sphere
    {
        public double Radius { get; set; } = 1.0;
        public Tuple Center { get; set; } = Tuple.Point(0, 0, 0);
        public Matrix4 Transform { get; set; } = Matrix4.Identity;
        public Material Material { get; set; } = new Material();

        public Intersections Intersect(Ray ray)
        {
            var rayTransFormed = ray.Transform(Transform.Inverse());

            var sphereToRay = rayTransFormed.Origin - Center;
            var a = Tuple.Dot(rayTransFormed.Direction, rayTransFormed.Direction);
            var b = 2 * Tuple.Dot(rayTransFormed.Direction, sphereToRay);
            var c = Tuple.Dot(sphereToRay, sphereToRay) - Radius * Radius;

            var discriminant = b * b - 4 * a * c;
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

        public Tuple Normal(Tuple worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = Tuple.Vector(objectPoint.X, objectPoint.Y, objectPoint.Z);

            var worldNormal = Transform.Inverse().Transpose * objectNormal;
            worldNormal.W = 0;

            return worldNormal.Normalized();
        }
    }
}
