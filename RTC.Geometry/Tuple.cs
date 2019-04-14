using System;

namespace RTC.Geometry
{
  public struct Tuple
  {
    private const double Epsilon = 0.00001;

    public Tuple(double x, double y, double z, double w)
    {
      X = x;
      Y = y;
      Z = z;
      W = w;
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }

    public bool IsPoint => !IsVector;
    public bool IsVector => Math.Abs(W) < Epsilon;

    public double Magnitude => Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));

    public static Tuple Point(double x, double y, double z) => new Tuple(x, y, z, 1.0);
    public static Tuple Vector(double x, double y, double z) => new Tuple(x, y, z, 0.0);

    public override bool Equals(object obj)
    {
      if (obj is Tuple)
        return this == (Tuple)obj;

      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static bool operator ==(Tuple t1, Tuple t2)
    {
      return Math.Abs(t1.X - t2.X) < Epsilon
        && Math.Abs(t1.Y - t2.Y) < Epsilon
        && Math.Abs(t1.Z - t2.Z) < Epsilon
        && Math.Abs(t1.W - t2.W) < Epsilon;
    }

    public static bool operator !=(Tuple t1, Tuple t2) => !(t1 == t2);

    public static Tuple operator +(Tuple t1, Tuple t2)
    {
      return new Tuple(
        t1.X + t2.X,
        t1.Y + t2.Y,
        t1.Z + t2.Z,
        t1.W + t2.W);
    }

    public static Tuple operator -(Tuple t1, Tuple t2)
    {
      return new Tuple(
        t1.X - t2.X,
        t1.Y - t2.Y,
        t1.Z - t2.Z,
        t1.W - t2.W);
    }

    public static Tuple operator -(Tuple tuple)
    {
      return new Tuple(
        -tuple.X,
        -tuple.Y,
        -tuple.Z,
        -tuple.W);
    }

    public static Tuple operator *(double factor, Tuple tuple)
    {
      return new Tuple(
        factor * tuple.X,
        factor * tuple.Y,
        factor * tuple.Z,
        factor * tuple.W);
    }

    public static Tuple operator /(Tuple tuple, double divisor)
    {
      return new Tuple(
        tuple.X / divisor,
        tuple.Y / divisor,
        tuple.Z / divisor,
        tuple.W / divisor);
    }

    public void Normalize()
    {
      var Magnitude = this.Magnitude;
      X /= Magnitude;
      Y /= Magnitude;
      Z /= Magnitude;
      W /= Magnitude;
    }
  }
}
