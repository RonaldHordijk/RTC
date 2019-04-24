using NUnit.Framework;
using RTC.Geometry;

namespace RTC.Tests.Geometry
{
    [TestFixture]
    public class MatrixTests
    {
        private const double Epsilon = 0.00001;

        [Test]
        public void TestNewMatrix4()
        {
            var m = new Matrix4(1, 2, 3, 4,
                                5.5, 6.5, 7.5, 8.5,
                                9, 10, 11, 12,
                                13.5, 14.5, 15.5, 16.5);


            Assert.AreEqual(1, m.ValueAt(0, 0), Epsilon);
            Assert.AreEqual(4, m.ValueAt(0, 3), Epsilon);
            Assert.AreEqual(5.5, m.ValueAt(1, 0), Epsilon);
            Assert.AreEqual(7.5, m.ValueAt(1, 2), Epsilon);
            Assert.AreEqual(11, m.ValueAt(2, 2), Epsilon);
            Assert.AreEqual(13.5, m.ValueAt(3, 0), Epsilon);
            Assert.AreEqual(15.5, m.ValueAt(3, 2), Epsilon);
        }

        [Test]
        public void TestNewMatrix2()
        {
            var m = new Matrix2(-3, 5, 1, -2);


            Assert.AreEqual(-3, m.ValueAt(0, 0), Epsilon);
            Assert.AreEqual(5, m.ValueAt(0, 1), Epsilon);
            Assert.AreEqual(1, m.ValueAt(1, 0), Epsilon);
            Assert.AreEqual(-2, m.ValueAt(1, 1), Epsilon);
        }

        [Test]
        public void TestNewMatrix3()
        {
            var m = new Matrix3(-3, 5, 0, 1, -2, -7, 0, 1, 1);


            Assert.AreEqual(-3, m.ValueAt(0, 0), Epsilon);
            Assert.AreEqual(-2, m.ValueAt(1, 1), Epsilon);
            Assert.AreEqual(1, m.ValueAt(2, 2), Epsilon);
        }

        [Test]
        public void TestEqual()
        {
            var m1 = new Matrix4(1, 2, 3, 4,
                                5, 6, 7, 8,
                                9, 10, 11, 12,
                                13, 14, 15, 16);

            var m2 = new Matrix4(1, 2, 3, 4,
                                5, 6, 7, 8,
                                9, 10, 11, 12,
                                13, 14, 15, 16);


            Assert.AreEqual(m1, m2);
        }

        [Test]
        public void TestNotEqual()
        {
            var m1 = new Matrix4(1, 2, 3, 4,
                                5, 6, 7, 8,
                                9, 10, 11, 12,
                                13, 14, 15, 16);

            var m2 = new Matrix4(2, 3, 4, 5,
                                6, 7, 8, 9,
                                10, 11, 12, 13,
                                14, 15, 16, 1);


            Assert.AreNotEqual(m1, m2);
        }


        [Test]
        public void TestMultiplicationMatrices()
        {

            var m1 = new Matrix4(1, 2, 3, 4,
                                5, 6, 7, 8,
                                9, 8, 7, 6,
                                5, 4, 3, 2);

            var m2 = new Matrix4(-2, 1, 2, 3,
                                  3, 2, 1, -1,
                                  4, 3, 6, 5,
                                  1, 2, 7, 8);

            var expected = new Matrix4(20, 22, 50, 48,
                                        44, 54, 114, 108,
                                        40, 58, 110, 102,
                                        16, 26, 46, 42);

            Assert.AreEqual(expected, m1 * m2);
        }

        [Test]
        public void TestMultiplicationMatriceTuple()
        {

            var m = new Matrix4(1, 2, 3, 4,
                                2, 4, 4, 2,
                                8, 6, 4, 1,
                                0, 0, 0, 1);
            var t = new Tuple(1, 2, 3, 1);


            Assert.AreEqual(new Tuple(18, 24, 33, 1), m * t);
        }

        [Test]
        public void TestMultiplicationIndenity()
        {

            var m = new Matrix4(1, 2, 3, 4,
                                2, 4, 4, 2,
                                8, 6, 4, 1,
                                0, 0, 0, 1);
            var t = new Tuple(1, 2, 3, 1);


            Assert.AreEqual(m, Matrix4.Identity * m);
            Assert.AreEqual(t, Matrix4.Identity * t);
        }


        [Test]
        public void TestTranspose()
        {

            var m = new Matrix4(0, 9, 3, 0,
                                9, 8, 0, 8,
                                1, 8, 5, 3,
                                0, 0, 5, 8);
            var t = new Matrix4(0, 9, 1, 0,
                                9, 8, 8, 0,
                                3, 0, 5, 5,
                                0, 8, 3, 8);


            Assert.AreEqual(t, m.Transpose);
        }

        [Test]
        public void TestTransposeIdent()
        {
            Assert.AreEqual(Matrix4.Identity, Matrix4.Identity.Transpose);
        }

        [Test]
        public void TestDeterminant2()
        {
            var m = new Matrix2(1, 5, -3, 2);
            Assert.AreEqual(17, m.Determinant, Epsilon);
        }

        [Test]
        public void TestSubMatrix3()
        {
            var m3 = new Matrix3(
                1, 5, 0,
                -3, 2, 7,
                0, 6, -3);

            var m2 = new Matrix2(-3, 2, 0, 6);

            Assert.AreEqual(m2, m3.SubMatrix(0, 2));
        }

        [Test]
        public void TestSubMatrix4()
        {
            var m4 = new Matrix4(
                -6, 1, 1, -6,
                -8, 5, 8, 6,
                -1, 0, 8, 2,
                -7, 1, -1, 1);


            var m3 = new Matrix3(
                -6, 1, -6,
                -8, 8, 6,
                -7, -1, 1);

            Assert.AreEqual(m3, m4.SubMatrix(2, 1));
        }

        [Test]
        public void TestMinor3()
        {
            var m = new Matrix3(
                3, 5, 0,
                2, -1, -7,
                6, -1, 5);

            var b = m.SubMatrix(1, 0);

            Assert.AreEqual(25, b.Determinant, Epsilon);
            Assert.AreEqual(25, m.Minor(1, 0), Epsilon);
        }

        [Test]
        public void TestCoFactor3()
        {
            var m = new Matrix3(
                3, 5, 0,
                2, -1, -7,
                6, -1, 5);

            Assert.AreEqual(-12, m.Minor(0, 0), Epsilon);
            Assert.AreEqual(-12, m.CoFactor(0, 0), Epsilon);
            Assert.AreEqual(25, m.Minor(1, 0), Epsilon);
            Assert.AreEqual(-25, m.CoFactor(1, 0), Epsilon);
        }

        [Test]
        public void TestDeterminant3()
        {
            var m = new Matrix3(
                1, 2, 6,
                -5, 8, -4,
                2, 6, 4);

            Assert.AreEqual(56, m.CoFactor(0, 0), Epsilon);
            Assert.AreEqual(12, m.CoFactor(0, 1), Epsilon);
            Assert.AreEqual(-46, m.CoFactor(0, 2), Epsilon);
            Assert.AreEqual(-196, m.Determinant(), Epsilon);
        }

        [Test]
        public void TestDeterminant4()
        {
            var m = new Matrix4(
                -2, -8, 3, 5,
                -3, 1, 7, 3,
                1, 2, -9, 6,
                -6, 7, 7, -9);

            Assert.AreEqual(690, m.CoFactor(0, 0), Epsilon);
            Assert.AreEqual(447, m.CoFactor(0, 1), Epsilon);
            Assert.AreEqual(210, m.CoFactor(0, 2), Epsilon);
            Assert.AreEqual(51, m.CoFactor(0, 3), Epsilon);
            Assert.AreEqual(-4071, m.Determinant(), Epsilon);
        }

        [Test]
        public void TestInvertable()
        {
            var m1 = new Matrix4(
                         6, 4, 4, 4,
                         5, 5, 7, 6,
                         4, -9, 3, -7,
                         9, 1, 7, -6);

            var m2 = new Matrix4(
                         -4, 2, -2, -3,
                         9, 6, 2, 6,
                         0, -5, 1, -5,
                         0, 0, 0, 0);

            Assert.AreEqual(-2120, m1.Determinant(), Epsilon);
            Assert.IsTrue(m1.IsInvertable);
            Assert.AreEqual(0, m2.Determinant(), Epsilon);
            Assert.IsFalse(m2.IsInvertable);
        }
    }
}
