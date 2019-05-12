
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
    }
}
