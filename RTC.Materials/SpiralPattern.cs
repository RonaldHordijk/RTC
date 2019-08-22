
using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class SpiralPattern : ColorizerPattern
    {
        private const double Epsilon = 0.00001;

        public SpiralPattern(Color a, Color b) : base(a, b)
        {
        }

        public override Color ColorAt(Tuple pos)
        {
            var radius = System.Math.Sqrt(pos.X * pos.X + pos.Z * pos.Z);
            var angle = 0.0;

            if (System.Math.Abs(pos.X) > Epsilon || System.Math.Abs(pos.Z) > Epsilon)
            {
                angle = System.Math.Atan2(pos.X, pos.Z);
                if (angle < 0)
                    angle += 2.0 * System.Math.PI;
            }

            return ColorFromValue(radius + 2 * angle * 0.15915494309189);
        }
    }
}
