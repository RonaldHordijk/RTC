
namespace RTC.Geometry.Objects
{
    public class Computation
    {
        private const double Epsilon = 0.00001;

        public double Distance { get; set; }
        public object Object { get; set; }
        public Tuple Point { get; set; }
        public Tuple OverPoint { get; set; }
        public Tuple EyeVector { get; set; }
        public Tuple NormalVector { get; set; }
        public bool Inside { get; set; }

        public static Computation Prepare(Intersection i, Ray ray)
        {
            var point = ray.Position(i.Distance);
            var eyeVector = -ray.Direction;
            var normalVector = (i.Object as Sphere)?.Normal(point) ?? default;
            bool inside = false;

            if (Tuple.Dot(normalVector, eyeVector) < 0)
            {
                inside = true;
                normalVector = -normalVector;
            }

            return new Computation
            {
                Distance = i.Distance,
                Object = i.Object,
                Point = point,
                OverPoint = point + Epsilon * normalVector,
                EyeVector = eyeVector,
                NormalVector = normalVector,
                Inside = inside
            };
        }

    }
}
