using RTC.Drawing;

namespace RTC.Geometry.Objects.Utils
{
    public static class LightUtil
    {
        public static Color Lighting(Material material, PointLight light, Tuple position, Tuple eye, Tuple normal)
        {
            var effectiveColor = material.Color * light.Intensity;
            var lightVector = Tuple.Normalize(light.Position - position);

            var ambient = material.Ambient * effectiveColor;
            var lightNormalDot = Tuple.Dot(lightVector, normal);

            if (lightNormalDot <= 0)
                return ambient;

            var specular = new Color(0, 0, 0);
            var diffuse = lightNormalDot * material.Diffuse * effectiveColor;

            var reflectVector = Tuple.Reflect(-lightVector, normal);
            var reflectEyeDot = Tuple.Dot(reflectVector, eye);

            if (reflectEyeDot > 0)
            {
                var factor = System.Math.Pow(reflectEyeDot, material.Shininess);
                specular = factor * material.Specular * light.Intensity;
            }

            return ambient + diffuse + specular;
        }
    }
}
