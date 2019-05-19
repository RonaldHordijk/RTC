
using RTC.Drawing;

namespace RTC.Geometry.Objects
{
    public class PointLight
    {
        public Tuple Position { get; set; }
        public Color Intensity { get; set; }

        public PointLight(Tuple position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public override bool Equals(object obj)
        {
            if (obj is PointLight pointLight)
                return this == pointLight;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(PointLight pl1, PointLight pl2)
        {
            return pl1.Position == pl2.Position
              && pl1.Intensity == pl2.Intensity;
        }

        public static bool operator !=(PointLight pl1, PointLight pl2) => !(pl1 == pl2);
    }
}
