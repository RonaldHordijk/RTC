using System;
using RTC.Drawing;
using RTC.Geometry.Objects.Shapes;

namespace RTC.Geometry.Objects
{
    public class StripedPattern
    {
        public Color A { get; set; }
        public Color B { get; set; }
        public Matrix4 Transform { get; set; } = Matrix4.Identity;

        public StripedPattern(Color a, Color b)
        {
            A = a;
            B = b;
        }

        public Color ColorAt(Tuple pos)
        {
            return ((long)System.Math.Floor(pos.X)) % 2 == 0 ? A : B;
        }

        public Color ColorAtObject(Shape shape, Tuple worldPos)
        {
            var objectPos = shape.Transform.Inverse() * worldPos;
            var patternPos= Transform.Inverse() * objectPos;

            return ColorAt(patternPos);
        }

        public override bool Equals(object obj)
        {
            if (obj is StripedPattern)
                return this == (StripedPattern)obj;

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
