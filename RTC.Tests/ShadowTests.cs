using NUnit.Framework;
using RTC.Geometry;
using RTC.Geometry.Objects.Utils;

namespace RTC.Tests
{
    [TestFixture]
    public class ShadowTests
    {
        [Test]
        public void NoShadow()
        {
            var world = WorldBuilder.DefaultWorld();
            var point = Tuple.Point(0, 10, 0);
            Assert.IsFalse(ShadowUtil.IsShadowed(world, point));
        }

        [Test]
        public void InShadow()
        {
            var world = WorldBuilder.DefaultWorld();    
            var point = Tuple.Point(10, -10, 10);
            Assert.IsTrue(ShadowUtil.IsShadowed(world, point));
        }

        [Test]
        public void BehindLight()
        {
            var world = WorldBuilder.DefaultWorld();
            var point = Tuple.Point(-20, 20, -20);
            Assert.IsFalse(ShadowUtil.IsShadowed(world, point));
        }

        [Test]
        public void ObjectBehindPoint()
        {
            var world = WorldBuilder.DefaultWorld();
            var point = Tuple.Point(-2, 2, -2);
            Assert.IsFalse(ShadowUtil.IsShadowed(world, point));
        }
    }
}
