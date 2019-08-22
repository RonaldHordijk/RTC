using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class SolidPattern : AbstractPattern
    {
        public Color Color { get; }

        public SolidPattern(Color c)
        {
            Color = c;
        }

        public override Color ColorAt(Tuple pos)
        {
            return Color;
        }
    }
}
