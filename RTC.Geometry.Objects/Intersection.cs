using RTC.Geometry.Objects.Shapes;

namespace RTC.Geometry.Objects
{
    public class Intersection
    {
        public double Distance { get; }
        public Shape Shape { get; }

        public Intersection(double distance, Shape s)
        {
            Distance = distance;
            Shape = s;
        }
    }
}
