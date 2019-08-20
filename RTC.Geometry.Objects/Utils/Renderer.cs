using RTC.Drawing;

namespace RTC.Geometry.Objects.Utils
{
    public static class Renderer
    {
        public static Canvas Render(Camera cam, World world, System.Action<double> progress = null)
        {
            var canvas = new Canvas(cam.HSize, cam.VSize);

            for (int x = 0; x < cam.HSize; x++)
            {
                progress?.Invoke((double)x / cam.HSize);

                for (int y = 0; y < cam.VSize; y++)
                {
                    var ray = cam.RayAt(x, y);
                    canvas.SetPixel(x, y, world.ColorAt(ray));
                }
            }

            return canvas;
        }
    }
}
