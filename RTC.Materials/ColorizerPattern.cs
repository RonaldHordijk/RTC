using RTC.Drawing;
using System;

namespace RTC.Materials
{
    public abstract class ColorizerPattern : AbstractPattern
    {
        public Color A { get; set; }
        public Color B { get; set; }

        public virtual Color ColorFromValue(double value)
        {
            return (long)Math.Floor(value) % 2 == 0 ? A : B;
        }

        public ColorizerPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public override bool Equals(object obj)
        {
            if (obj is ColorizerPattern colorizerPattern)
                return this == colorizerPattern;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ColorizerPattern cp1, ColorizerPattern cp2)
        {
            if (cp1 as AbstractPattern != cp2 as AbstractPattern)
                return false;

            if ((cp1 is null) && (cp2 is null))
                return true;

            return (cp1.A == cp2.A) && (cp1.B == cp2.B);
        }

        public static bool operator !=(ColorizerPattern cp1, ColorizerPattern cp2) => !(cp1 == cp2);
    }
}
