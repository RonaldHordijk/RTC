using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Math;

namespace RTC.Drawing
{
    public class PPMWriter
    {
        public static void WriteToFile(Canvas canvas, string filename)
        {
            var text = ToText(canvas);
            File.WriteAllLines(filename, text);
        }

        public static IEnumerable<string> ToText(Canvas canvas)
        {
            if (canvas is null)
                yield break;

            yield return "P3";
            yield return $"{canvas.Width} {canvas.Height}";
            yield return "255";

            for (int y = 0; y < canvas.Height; y++)
            {
                var sb = new StringBuilder();
                for (int x = 0; x < canvas.Width; x++)
                {
                    var c = canvas.PixelAt(x, y);
                    sb.Append((int)(Clamp(c.Red, 0, 1) * 255))
                      .Append(" ")
                      .Append((int)(Clamp(c.Green, 0, 1) * 255))
                      .Append(" ")
                      .Append((int)(Clamp(c.Blue, 0, 1) * 255))
                      .Append(" ");
                }

                var lines = Split(sb.ToString().TrimEnd(), 70);
                foreach (var line in lines)
                    yield return line;
            }
            yield return "";
        }

        private static IEnumerable<string> Split(string s, int maxLength)
        {
            while (true)
            {
                if (s.Length <= maxLength)
                {
                    yield return s;
                    yield break;
                }

                int lastindex = 0;
                while (true)
                {
                    var index = s.IndexOf(" ", lastindex + 1);
                    if (index < maxLength && index > 0)
                    {
                        lastindex = index;
                    }
                    else
                    {
                        var sp = s.Substring(0, lastindex);
                        yield return sp.TrimEnd();
                        s = s.Substring(lastindex + 1);
                        break;
                    }
                }
            }
        }
    }
}
