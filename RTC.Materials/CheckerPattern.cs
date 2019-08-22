using RTC.Drawing;
using System;

namespace RTC.Materials
{
    public class CheckerPattern : ColorizerPattern
    {
        public CheckerPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorAt(Geometry.Tuple pos)
        {
            return ColorFromValue(Math.Floor(pos.X) + Math.Floor(pos.Y) + Math.Floor(pos.Z));
        }
    }
}
