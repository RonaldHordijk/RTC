

using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class RingPattern : ColorizerPattern
    {
        public RingPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorAt(Tuple pos)
        {
            return ColorFromValue(System.Math.Sqrt(pos.X * pos.X + pos.Z * pos.Z));
        }
    }
}
