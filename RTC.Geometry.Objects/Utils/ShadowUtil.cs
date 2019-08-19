namespace RTC.Geometry.Objects.Utils
{
    static public class ShadowUtil
    {
        public static bool IsShadowed(World world, Tuple p)
        {
            var v = world.Light.Position - p;
            var distance = v.Magnitude;
            var dir = v.Normalized();

            var ray = new Ray(p, dir);
            var intersections = world.Intersect(ray);

            var hit = intersections.Hit();
            return  (hit != null) && (hit.Distance < distance);
        }
    }
}
