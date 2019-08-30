using NUnit.Framework;
using RTC.Materials;

namespace RTC.Tests
{
    [TestFixture]
    public class TestMaterial
    {
        [Test]
        public void TestMaterialReflective()
        {
            var material = new Material();
            Assert.AreEqual(0.0, material.Reflective);

            material.Reflective = 1.0;
            Assert.AreEqual(1.0, material.Reflective);
        }

        [Test]
        public void TestMaterialTransparencyAndRefractiveIndex()
        {
            var material = new Material();
            Assert.AreEqual(0.0, material.Transparency);
            Assert.AreEqual(1.0, material.RefractiveIndex);

            material.Transparency = 1.0;
            material.RefractiveIndex = 0.0;
            Assert.AreEqual(1.0, material.Transparency);
            Assert.AreEqual(0.0, material.RefractiveIndex);
        }
    }
}
