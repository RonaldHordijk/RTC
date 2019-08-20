using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public abstract class AbstractPattern
    {
        public Matrix4 Transform { get; set; } = Matrix4.Identity;

        public abstract Color ColorAt(Tuple pos);

        public Color ColorAtObject(Tuple objectPos)
        {
            var patternPos = Transform.Inverse() * objectPos;

            return ColorAt(patternPos);
        }

        public override bool Equals(object obj)
        {
            if (obj is AbstractPattern pattern)
                return this == pattern;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(AbstractPattern ap1, AbstractPattern ap2)
        {
            if ((ap1 is null) && (ap2 is null))
                return true;

            if ((ap1 is null) != (ap2 is null))
                return false;

            return ap1.Transform == ap2.Transform;
        }

        public static bool operator !=(AbstractPattern ap1, AbstractPattern ap2) => !(ap1 == ap2);
    }
}