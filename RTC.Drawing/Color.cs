using System;

namespace RTC.Drawing
{
  public struct Color
  {
    private const double Epsilon = 0.00001;

    private readonly double[] _color;

    public Color(double r, double g, double b)
    {
      _color = new double[3];

      _color[0] = r;
      _color[1] = g;
      _color[2] = b;
    }

    public double Red
    {
      get => _color[0];
      set => _color[0] = value;
    }

    public double Green
    {
      get => _color[1];
      set => _color[1] = value;
    }

    public double Blue
    {
      get => _color[2];
      set => _color[2] = value;
    }

    public override bool Equals(object obj)
    {
      if (obj is Color)
        return this == (Color)obj;

      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static bool operator ==(Color c1, Color c2)
    {
      return Math.Abs(c1._color[0] - c2._color[0]) < Epsilon
        && Math.Abs(c1._color[1] - c2._color[1]) < Epsilon
        && Math.Abs(c1._color[2] - c2._color[2]) < Epsilon;
    }

    public static bool operator !=(Color c1, Color c2) => !(c1 == c2);

    public static Color operator +(Color c1, Color c2)
    {
      return new Color(
        c1._color[0] + c2._color[0],
        c1._color[1] + c2._color[1],
        c1._color[2] + c2._color[2]);
    }

    public static Color operator -(Color c1, Color c2)
    {
      return new Color(
        c1._color[0] - c2._color[0],
        c1._color[1] - c2._color[1],
        c1._color[2] - c2._color[2]);
    }

    public static Color operator *(double factor, Color c)
    {
      return new Color(
        factor * c._color[0],
        factor * c._color[1],
        factor * c._color[2]);
    }

    public static Color operator *(Color c1, Color c2)
    {
      // Hadamard product (or Schur product)
      return new Color(
        c1._color[0] * c2._color[0],
        c1._color[1] * c2._color[1],
        c1._color[2] * c2._color[2]);
    }
  }
}
