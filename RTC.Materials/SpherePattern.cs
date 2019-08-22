
using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class SpherePattern : ColorizerPattern
    {
        public SpherePattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorAt(Tuple pos)
        {
            return ColorFromValue(System.Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y + pos.Z * pos.Z));
        }
    }
}
