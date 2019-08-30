using RTC.Drawing;

namespace RTC.Materials
{
    public class Material
    {
        private const double Epsilon = 0.00001;

        public Color Color { get; set; } = new Color(1, 1, 1);
        public double Ambient { get; set; } = 0.1;
        public double Diffuse { get; set; } = 0.9;
        public double Specular { get; set; } = 0.9;
        public double Shininess { get; set; } = 200;
        public AbstractPattern Pattern { get; set; }
        public double Reflective { get; set; }
        public double Transparency { get; set; }
        public double RefractiveIndex { get; set; } = 1.0;

        public override bool Equals(object obj)
        {
            if (obj is Material material)
                return this == material;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Material m1, Material m2)
        {
            return (m1.Color == m2.Color)
                && (m1.Pattern == m2.Pattern)
                && System.Math.Abs(m1.Ambient - m2.Ambient) < Epsilon
                && System.Math.Abs(m1.Diffuse - m2.Diffuse) < Epsilon
                && System.Math.Abs(m1.Specular - m2.Specular) < Epsilon
                && System.Math.Abs(m1.Shininess - m2.Shininess) < Epsilon;
        }

        public static bool operator !=(Material m1, Material m2) => !(m1 == m2);
    }
}
