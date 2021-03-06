﻿using RTC.Geometry;
using RTC.Geometry.Objects;
using RTC.Materials;

namespace RTC.Abstract
{
    public abstract class Shape
    {
        public Matrix4 Transform { get; set; } = Matrix4.Identity;
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
            var rayTransformed = ray.Transform(Transform.Inverse());
            return LocalIntersection(rayTransformed);
        }

        protected abstract Intersections LocalIntersection(Ray rayTransformed);

        public Tuple Normal(Tuple worldPoint)
        {
            var objectPoint = Transform.Inverse() * worldPoint;
            var objectNormal = LocalNormal(objectPoint);

            var worldNormal = Transform.Inverse().Transpose * objectNormal;
            worldNormal.W = 0;

            return worldNormal.Normalized();
        }

        protected abstract Tuple LocalNormal(Tuple objectPoint);
    }
}
