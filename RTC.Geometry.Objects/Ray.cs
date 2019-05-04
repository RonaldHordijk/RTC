namespace RTC.Geometry.Objects
{
    public class Ray
    {
        public Tuple Origin { get; }
        public Tuple Direction { get; }

        public Ray(Tuple origin, Tuple direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Tuple Position(double distance) => Origin + distance * Direction;

        public Ray Transform(Matrix4 m)
        {
            return new Ray(m * Origin, m * Direction);
        }
    }
}
