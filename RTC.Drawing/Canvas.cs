using System;
using System.Collections.Generic;
using System.Text;

namespace RTC.Drawing
{
    public class Canvas
    {
        private readonly Color[,] _pixels;
        public int Width { get; }
        public int Height { get;}

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;

            _pixels = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _pixels[x, y] = new Color(0, 0, 0);
                }
            }
        }

        public Color PixelAt(int x, int y) => _pixels[x, y];
        public void SetPixel(int x, int y, Color c) => _pixels[x, y] = c;
    }
}
