using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class NoisePattern : AbstractPattern
    {
        private readonly OpenSimplexNoise noise = new OpenSimplexNoise();

        public AbstractPattern Pattern { get; set; }

        public NoisePattern(AbstractPattern pattern)
        {
            Pattern = pattern;
        }

        public override Color ColorAt(Tuple pos)
        {
            var noisePos = Transform.Inverse() * pos;
            var perp = noise.Evaluate(noisePos.X, noisePos.Y, noisePos.Z);
            return Pattern.ColorAt(pos + Tuple.Point(perp, perp, perp));
        }
    }
}
