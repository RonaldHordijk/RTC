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

        [Test]
        public void TestInverse()
        {
            var m = new Matrix4(
                         -5, 2, 6, -8,
                         1, -5, 1, 8,
                         7, 7, -6, -7,
                         1, -3, 7, 4);

            var mInverse = m.Inverse();

            var expectedInverse = new Matrix4(
                    0.21805, 0.45113, 0.24060, -0.04511,
                    -0.80827, -1.45677, -0.44361, 0.52068,
                    -0.07895, -0.22368, -0.05263, 0.19737,
                    -0.52256, -0.81391, -0.30075, 0.30639);

            Assert.AreEqual(532, m.Determinant(), Epsilon);
            Assert.AreEqual(-160, m.CoFactor(2, 3));
            Assert.AreEqual(-160.0 / 532, mInverse.ValueAt(3, 2));
            Assert.AreEqual(105, m.CoFactor(3, 2));
            Assert.AreEqual(105.0 / 532, mInverse.ValueAt(2, 3));
            Assert.AreEqual(expectedInverse, mInverse);
        }

        [Test]
        public void TestInverse2()
        {
            var m = new Matrix4(
                            8, -5, 9, 2,
                            7, 5, 6, 1,
                            -6, 0, 9, 6,
                            -3, 0, -9, -4);

            var mInverse = m.Inverse();

            var expectedInverse = new Matrix4(
                            -0.15385, -0.15385, -0.28205, -0.53846,
                            -0.07692, 0.12308, 0.02564, 0.03077,
                            0.35897, 0.35897, 0.43590, 0.92308,
                            -0.69231, -0.69231, -0.76923, -1.92308);

            Assert.AreEqual(expectedInverse, mInverse);
        }

        [Test]
        public void TestInverse3()
        {
            var m = new Matrix4(
                            9, 3, 0, 9,
                            -5, -2, -6, -3,
                            -4, 9, 6, 4,
                            -7, 6, 6, 2);

            var mInverse = m.Inverse();

            var expectedInverse = new Matrix4(
                            -0.04074, -0.07778, 0.14444, -0.22222,
                            -0.07778, 0.03333, 0.36667, -0.33333,
                            -0.02901, -0.14630, -0.10926, 0.12963,
                            0.17778, 0.06667, -0.26667, 0.33333);

            Assert.AreEqual(expectedInverse, mInverse);
        }

        [Test]
        public void TestMultInverse()
        {
            var m1 = new Matrix4(
                            3, -9, 7, 3,
                            3, -8, 2, -9,
                            -4, 4, 4, 1,
                            -6, 5, -1, 1);

            var m2 = new Matrix4(
                            8, 2, 2, 2,
                            3, -1, 7, 0,
                            7, 0, 5, 4,
                            6, -2, 0, 5);

            var mm = m1 * m2;

            Assert.AreEqual(m1, mm * m2.Inverse());
            Assert.AreEqual(m2, m1.Inverse() * mm);
        }

        [Test]
        public void TestInverseIdent()
        {
            Assert.AreEqual(Matrix4.Identity, Matrix4.Identity.Inverse());
        }

        [Test]
        public void TestMultSelfInverse()
        {
            var m = new Matrix4(
                            3, -9, 7, 3,
                            3, -8, 2, -9,
                            -4, 4, 4, 1,
                            -6, 5, -1, 1);

            Assert.AreEqual(Matrix4.Identity, m * m.Inverse());
        }

        [Test]
        public void TestInverseTransPoseOrder()
        {
            var m = new Matrix4(
                            3, -9, 7, 3,
                            3, -8, 2, -9,
                            -4, 4, 4, 1,
                            -6, 5, -1, 1);

            Assert.AreEqual(m.Transpose.Inverse(), m.Inverse().Transpose);
        }

        [Test]
        public void TestTranslate()
        {
            var m = Matrix4.Translation(5, -3, 2);
            var p = Tuple.Point(-3, 4, 5);

            Assert.AreEqual(m * p, Tuple.Point(2, 1, 7));
        }

        [Test]
        public void TestInverseTranslate()
        {
            var m = Matrix4.Translation(5, -3, 2);
            var p = Tuple.Point(-3, 4, 5);

            Assert.AreEqual(m.Inverse() * p, Tuple.Point(-8, 7, 3));
        }

        [Test]
        public void TestTranslateVector()
        {
            var m = Matrix4.Translation(5, -3, 2);
            var v = Tuple.Vector(-3, 4, 5);

            Assert.AreEqual(m * v, v);
        }

        [Test]
        public void TestScalePoint()
        {
            var m = Matrix4.Scaling(2, 3, 4);
            var p = Tuple.Point(-4, 6, 8);

            Assert.AreEqual(m * p, Tuple.Point(-8, 18, 32));
        }

        [Test]
        public void TestScaleVector()
        {
            var m = Matrix4.Scaling(2, 3, 4);
            var v = Tuple.Vector(-4, 6, 8);

            Assert.AreEqual(m * v, Tuple.Vector(-8, 18, 32));
        }

        [Test]
        public void TestScaleInverseVector()
        {
            var m = Matrix4.Scaling(2, 3, 4);
            var v = Tuple.Vector(-4, 6, 8);

            Assert.AreEqual(m.Inverse() * v, Tuple.Vector(-2, 2, 2));
        }

        [Test]
        public void TestReflectionPoint()
        {
            var m = Matrix4.Scaling(-1, 1, 1);
            var p = Tuple.Point(2, 3, 4);

            Assert.AreEqual(m * p, Tuple.Point(-2, 3, 4));
        }

        [Test]
        public void TestRotateX()
        {
            var p = Tuple.Point(0, 1, 0);
            var m1 = Matrix4.RotationX(System.Math.PI / 4);
            var m2 = Matrix4.RotationX(System.Math.PI / 2);

            Assert.AreEqual(m1 * p, Tuple.Point(0, 0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2)));
            Assert.AreEqual(m2 * p, Tuple.Point(0, 0, 1));
        }

        [Test]
        public void TestInvertRotateX()
        {
            var p = Tuple.Point(0, 1, 0);
            var m = Matrix4.RotationX(System.Math.PI / 4);

            Assert.AreEqual(m.Inverse() * p, Tuple.Point(0, 0.5 * System.Math.Sqrt(2), -0.5 * System.Math.Sqrt(2)));
        }

        [Test]
        public void TestRotateY()
        {
            var p = Tuple.Point(0, 0, 1);
            var m1 = Matrix4.RotationY(System.Math.PI / 4);
            var m2 = Matrix4.RotationY(System.Math.PI / 2);

            Assert.AreEqual(m1 * p, Tuple.Point(0.5 * System.Math.Sqrt(2), 0, 0.5 * System.Math.Sqrt(2)));
            Assert.AreEqual(m2 * p, Tuple.Point(1, 0, 0));
        }

        [Test]
        public void TestRotateZ()
        {
            var p = Tuple.Point(0, 1, 0);
            var m1 = Matrix4.RotationZ(System.Math.PI / 4);
            var m2 = Matrix4.RotationZ(System.Math.PI / 2);

            Assert.AreEqual(m1 * p, Tuple.Point(-0.5 * System.Math.Sqrt(2), 0.5 * System.Math.Sqrt(2), 0));
            Assert.AreEqual(m2 * p, Tuple.Point(-1, 0, 0));
        }

        [Test]
        public void TestShearing()
        {
            var p = Tuple.Point(2, 3, 4);

            Assert.AreEqual(Matrix4.Shearing(1, 0, 0, 0, 0, 0) * p, Tuple.Point(5, 3, 4));
            Assert.AreEqual(Matrix4.Shearing(0, 1, 0, 0, 0, 0) * p, Tuple.Point(6, 3, 4));
            Assert.AreEqual(Matrix4.Shearing(0, 0, 1, 0, 0, 0) * p, Tuple.Point(2, 5, 4));
            Assert.AreEqual(Matrix4.Shearing(0, 0, 0, 1, 0, 0) * p, Tuple.Point(2, 7, 4));
            Assert.AreEqual(Matrix4.Shearing(0, 0, 0, 0, 1, 0) * p, Tuple.Point(2, 3, 6));
            Assert.AreEqual(Matrix4.Shearing(0, 0, 0, 0, 0, 1) * p, Tuple.Point(2, 3, 7));
        }

        [Test]
        public void TestChaining()
        {
            var p = Tuple.Point(1, 0, 1);
            var a = Matrix4.RotationX(System.Math.PI / 2);
            var b = Matrix4.Scaling(5, 5, 5);
            var c = Matrix4.Translation(10, 5, 7);

            var p2 = a * p;
            Assert.AreEqual(Tuple.Point(1, -1, 0), p2);

            var p3 = b * p2;
            Assert.AreEqual(Tuple.Point(5, -5, 0), p3);

            var p4 = c * p3;
            Assert.AreEqual(Tuple.Point(15, 0, 7), p4);

            var t = c * b * a;
            Assert.AreEqual(Tuple.Point(15, 0, 7), t * p);

            var t2 = Matrix4.Identity
                .RotateX(System.Math.PI / 2)
                .Scale(5, 5, 5)
                .Translate(10, 5, 7);
            Assert.AreEqual(Tuple.Point(15, 0, 7), t2 * p);
        }
    }
}
