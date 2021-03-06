﻿using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class StripedPattern : ColorizerPattern
    {
        public StripedPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorAt(Tuple pos)
        {
            return ColorFromValue(pos.X);
        }
    }
}
