using RTC.Materials;

namespace RTC.Geometry.Objects.Shapes
{
    public abstract class Shape
    {
        private Matrix4 _transform = Matrix4.Identity;

        public Matrix4 Transform
        {
            get { return _transform; }
            set
            {
                _transform = value;
                CalcInverse();
            }
        }

        public Matrix4 TransformInverse { get; private set; } = Matrix4.Identity;

        private void CalcInverse()
        {
            TransformInverse = _transform.Inverse();
        }

        public Material Material { get; set; } = new Material();

        public override bool Equals(object obj)
        {
            if (obj is Shape shape)
                return this == shape;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Shape s1, Shape s2)
        {
            if ((s1 is null) || (s2 is null))
                return false;

            return s1.GetType() == s2.GetType()
              && s1.Transform == s2.Transform
              && s1.Material == s2.Material;
        }

        public static bool operator !=(Shape s1, Shape s2) => !(s1 == s2);

        public Intersections Intersect(Ray ray)
        {
            var rayTransformed = ray.Transform(TransformInverse);
            return LocalIntersection(rayTransformed);
        }

        protected abstract Intersections LocalIntersection(Ray rayTransformed);

        public Tuple Normal(Tuple worldPoint)
        {
            var objectPoint = TransformInverse * worldPoint;
            var objectNormal = LocalNormal(objectPoint);

            var worldNormal = TransformInverse.Transpose * objectNormal;
            worldNormal.W = 0;

            return worldNormal.Normalized();
        }

        public Tuple WorldToObject(Tuple worldPos) => TransformInverse * worldPos;

        protected abstract Tuple LocalNormal(Tuple objectPoint);
    }
}
