using System;

namespace RTC.Geometry
{
    public struct Matrix4
    {
        private const double Epsilon = 0.00001;

        private readonly double[,] _matrix;

        public Matrix4(double m00, double m01, double m02, double m03,
            double m10, double m11, double m12, double m13,
            double m20, double m21, double m22, double m23,
            double m30, double m31, double m32, double m33)
        {
            _matrix = new double[,] {
                { m00, m01, m02, m03 },
                { m10, m11, m12, m13 },
                { m20, m21, m22, m23 },
                { m30, m31, m32, m33 } };
        }

        public static Matrix4 Identity => new Matrix4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public Matrix4 Transpose => new Matrix4(
            _matrix[0, 0], _matrix[1, 0], _matrix[2, 0], _matrix[3, 0],
            _matrix[0, 1], _matrix[1, 1], _matrix[2, 1], _matrix[3, 1],
            _matrix[0, 2], _matrix[1, 2], _matrix[2, 2], _matrix[3, 2],
            _matrix[0, 3], _matrix[1, 3], _matrix[2, 3], _matrix[3, 3]);

        public bool IsInvertable => Math.Abs(Determinant()) > Epsilon;

        public double ValueAt(int row, int col) => _matrix[row, col];

        public override bool Equals(object obj)
        {
            if (obj is Matrix4)
                return this == (Matrix4)obj;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Matrix4 m1, Matrix4 m2)
        {
            return m1._matrix[0, 0] == m2._matrix[0, 0]
                && m1._matrix[0, 1] == m2._matrix[0, 1]
                && m1._matrix[0, 2] == m2._matrix[0, 2]
                && m1._matrix[0, 3] == m2._matrix[0, 3]
                && m1._matrix[1, 0] == m2._matrix[1, 0]
                && m1._matrix[1, 1] == m2._matrix[1, 1]
                && m1._matrix[1, 2] == m2._matrix[1, 2]
                && m1._matrix[1, 3] == m2._matrix[1, 3]
                && m1._matrix[2, 0] == m2._matrix[2, 0]
                && m1._matrix[2, 1] == m2._matrix[2, 1]
                && m1._matrix[2, 2] == m2._matrix[2, 2]
                && m1._matrix[2, 3] == m2._matrix[2, 3]
                && m1._matrix[3, 0] == m2._matrix[3, 0]
                && m1._matrix[3, 1] == m2._matrix[3, 1]
                && m1._matrix[3, 2] == m2._matrix[3, 2]
                && m1._matrix[3, 3] == m2._matrix[3, 3];
        }

        public static bool operator !=(Matrix4 m1, Matrix4 m2) => !(m1 == m2);

        public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
        {
            var result = Identity;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    result._matrix[row, col] = m1._matrix[row, 0] * m2._matrix[0, col]
                        + m1._matrix[row, 1] * m2._matrix[1, col]
                        + m1._matrix[row, 2] * m2._matrix[2, col]
                        + m1._matrix[row, 3] * m2._matrix[3, col];
                }
            }

            return result;
        }

        public static Tuple operator *(Matrix4 m, Tuple t)
        {
            return new Tuple(
                m._matrix[0, 0] * t.X + m._matrix[0, 1] * t.Y + m._matrix[0, 2] * t.Z + m._matrix[0, 3] * t.W,
                m._matrix[1, 0] * t.X + m._matrix[1, 1] * t.Y + m._matrix[1, 2] * t.Z + m._matrix[1, 3] * t.W,
                m._matrix[2, 0] * t.X + m._matrix[2, 1] * t.Y + m._matrix[2, 2] * t.Z + m._matrix[2, 3] * t.W,
                m._matrix[3, 0] * t.X + m._matrix[3, 1] * t.Y + m._matrix[3, 2] * t.Z + m._matrix[3, 3] * t.W);
        }

        public Matrix3 SubMatrix(int row, int col)
        {
            return new Matrix3(
                _matrix[row != 0 ? 0 : 1, col != 0 ? 0 : 1], _matrix[row != 0 ? 0 : 1, col < 2 ? 2 : 1], _matrix[row != 0 ? 0 : 1, col != 3 ? 3 : 2],
                _matrix[row < 2 ? 2 : 1, col != 0 ? 0 : 1], _matrix[row < 2 ? 2 : 1, col < 2 ? 2 : 1], _matrix[row < 2 ? 2 : 1, col != 3 ? 3 : 2],
                _matrix[row != 3 ? 3 : 2, col != 0 ? 0 : 1], _matrix[row != 3 ? 3 : 2, col < 2 ? 2 : 1], _matrix[row != 3 ? 3 : 2, col != 3 ? 3 : 2]);
        }

        public double Minor(int row, int col) => SubMatrix(row, col).Determinant();

        public double CoFactor(int row, int col)
        {
            var minor = Minor(row, col);

            if ((row + col) % 2 == 1)
                minor = -minor;

            return minor;
        }

        public double Determinant()
        {
            return _matrix[0, 0] * CoFactor(0, 0)
                 + _matrix[0, 1] * CoFactor(0, 1)
                 + _matrix[0, 2] * CoFactor(0, 2)
                 + _matrix[0, 3] * CoFactor(0, 3);
        }

        public override string ToString()
        {
            return $"{_matrix[0, 0]} {_matrix[0, 1]} {_matrix[0, 2]} {_matrix[0, 3]}" + Environment.NewLine
                 + $"{_matrix[1, 0]} {_matrix[1, 1]} {_matrix[1, 2]} {_matrix[1, 3]}" + Environment.NewLine
                 + $"{_matrix[2, 0]} {_matrix[2, 1]} {_matrix[2, 2]} {_matrix[2, 3]}" + Environment.NewLine
                 + $"{_matrix[3, 0]} {_matrix[3, 1]} {_matrix[3, 2]} {_matrix[3, 3]}";
        }
    }
}
