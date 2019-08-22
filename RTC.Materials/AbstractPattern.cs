using RTC.Drawing;
using RTC.Geometry;

namespace RTC.Materials
{
    public abstract class AbstractPattern
    {
        private Matrix4 _transform = Matrix4.Identity;
        private Matrix4 _transformInverse = Matrix4.Identity;


        public Matrix4 Transform
        {
            get { return _transform; }
            set {
                _transform = value;
                CalcInverse();
            }
        }

        private void CalcInverse()
        {
            _transformInverse = Transform.Inverse();
        }

        public abstract Color ColorAt(Tuple pos);

        public Color ColorAtObject(Tuple objectPos)
        {
            var patternPos = _transformInverse * objectPos;

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