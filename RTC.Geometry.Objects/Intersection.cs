namespace RTC.Geometry.Objects
{
    public class Intersection
    {
        public double Distance { get; }
        public object Object { get; }

        public Intersection(double distance, object o)
        {
            Distance = distance;
            Object = o;
        }
    }
}
