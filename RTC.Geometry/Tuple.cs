﻿using System;

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
        public static Tuple Vector(Tuple T) => Vector(T.X, T.Y, T.Z);
        public static double Dot(Tuple v1, Tuple v2) => (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z) + (v1.W * v2.W);
        public static Tuple Cross(Tuple v1, Tuple v2) => Vector((v1.Y * v2.Z) - (v1.Z * v2.Y), (v1.Z * v2.X) - (v1.X * v2.Z), (v1.X * v2.Y) - (v1.Y * v2.X));

        public override bool Equals(object obj)
        {
            if (obj is Tuple tuple)
                return this == tuple;

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

        static public Tuple Normalize(Tuple t)
        {
            var Magnitude = t.Magnitude;
            return new Tuple(
                t.X / Magnitude,
                t.Y / Magnitude,
                t.Z / Magnitude,
                t.W / Magnitude);
        }

        public Tuple Normalized()
        {
            var Magnitude = this.Magnitude;
            if (Magnitude < Epsilon)
                return new Tuple(0, 0, 0, 0);

            return new Tuple(
                X / Magnitude,
                Y / Magnitude,
                Z / Magnitude,
                W / Magnitude);
        }

        public static Tuple Reflect(Tuple v, Tuple normal)
        {
            return v - (2 * Dot(v, normal) * normal);
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z} {W}";
        }
    }
}
