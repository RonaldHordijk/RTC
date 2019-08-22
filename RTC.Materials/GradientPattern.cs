using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class GradientPattern : ColorizerPattern
    {
        public GradientPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorFromValue(double value)
        {
            var fraction = value - System.Math.Floor(value);
            return A + fraction * (B - A);
        }

        public override Color ColorAt(Tuple pos)
        {
            return ColorFromValue(pos.X);
        }
    }
}
