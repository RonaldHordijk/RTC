using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public class StripedPattern : AbstractPattern
    {
        public Color A { get; set; }
        public Color B { get; set; }

        public StripedPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override Color ColorAt(Tuple pos)
        {
            return ((long)System.Math.Floor(pos.X)) % 2 == 0 ? A : B;
        }

        public override bool Equals(object obj)
        {
            if (obj is StripedPattern stripedPattern)
                return this == stripedPattern;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(StripedPattern sp1, StripedPattern sp2)
        {
            if ((sp1 is null) && (sp2 is null))
                return true;

            if ((sp1 is null) != (sp2 is null))
                return false;

            return (sp1.A == sp2.A) && (sp1.B == sp2.B);
        }

        public static bool operator !=(StripedPattern sp1, StripedPattern sp2) => !(sp1 == sp2);
    }
}
