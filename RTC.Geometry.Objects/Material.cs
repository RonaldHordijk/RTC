using RTC.Drawing;

namespace RTC.Geometry.Objects
{
    public class Material
    {
        private const double Epsilon = 0.00001;

        public Color Color { get; set; } = new Color(1, 1, 1);
        public double Ambient { get; set; } = 0.1;
        public double Diffuse { get; set; } = 0.9;
        public double Specular { get; set; } = 0.9;
        public double Shininess { get; set; } = 200;

        public override bool Equals(object obj)
        {
            if (obj is Material)
                return this == (Material)obj;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Material m1, Material m2)
        {
            return (m1.Color == m2.Color)
                && System.Math.Abs(m1.Ambient - m2.Ambient) < Epsilon
                && System.Math.Abs(m1.Diffuse - m2.Diffuse) < Epsilon
                && System.Math.Abs(m1.Specular - m2.Specular) < Epsilon
                && System.Math.Abs(m1.Shininess - m2.Shininess) < Epsilon;
        }

        public static bool operator !=(Material m1, Material m2) => !(m1 == m2);
    }
}
