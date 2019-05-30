using System;

namespace RTC.Geometry.Objects
{
    public class Camera
    {
        public int HSize { get; set; }
        public int VSize { get; set; }
        public double FieldOfView { get; set; }
        public Matrix4 Transform { get; set; } = Matrix4.Identity;
        public double PixelSize { get; private set; }

        private double HalfWidth;
        private double HalfHeight;

        public Camera(int hSize, int vSize, double fieldOfView)
        {
            HSize = hSize;
            VSize = vSize;
            FieldOfView = fieldOfView;

            CalculatePixelSize();
        }

        private void CalculatePixelSize()
        {
            var halfView = Math.Tan(0.5 * FieldOfView);
            var aspect = 1.0 * HSize / VSize;

            if (aspect >= 1)
            {
                HalfWidth = halfView;
                HalfHeight = halfView / aspect;
            }
            else
            {
                HalfWidth = halfView * aspect;
                HalfHeight = halfView;
            }

            PixelSize = 2 * HalfWidth / HSize;
        }

        public Ray RayAt(int px, int py)
        {
            var xOffset = (px + 0.5) * PixelSize;
            var yOffset = (py + 0.5) * PixelSize;

            var worldX = HalfWidth - xOffset;
            var worldY = HalfHeight - yOffset;

            var inverse = Transform.Inverse();

            var pixel = inverse * Tuple.Point(worldX, worldY, -1);
            var origin = inverse * Tuple.Point(0, 0, 0);
            var direction = Tuple.Normalize(pixel - origin);

            return new Ray(origin, direction);
        }
    }
}