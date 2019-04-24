using System;

namespace RTC.Geometry
{
    public class Matrix3
    {
        private readonly double[,] _matrix;

        public Matrix3(double m00, double m01, double m02,
            double m10, double m11, double m12,
            double m20, double m21, double m22)
        {
            _matrix = new double[,] {
                { m00, m01, m02 },
                { m10, m11, m12 },
                { m20, m21, m22 } };
        }

        public double ValueAt(int row, int col) => _matrix[row, col];

        public override bool Equals(object obj)
        {
            if (obj is Matrix3)
                return this == (Matrix3)obj;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Matrix3 m1, Matrix3 m2)
        {
            return m1._matrix[0, 0] == m2._matrix[0, 0]
                && m1._matrix[0, 1] == m2._matrix[0, 1]
                && m1._matrix[0, 2] == m2._matrix[0, 2]
                && m1._matrix[1, 0] == m2._matrix[1, 0]
                && m1._matrix[1, 1] == m2._matrix[1, 1]
                && m1._matrix[1, 2] == m2._matrix[1, 2]
                && m1._matrix[2, 0] == m2._matrix[2, 0]
                && m1._matrix[2, 1] == m2._matrix[2, 1]
                && m1._matrix[2, 2] == m2._matrix[2, 2];
        }

        public static bool operator !=(Matrix3 m1, Matrix3 m2) => !(m1 == m2);

        public Matrix2 SubMatrix(int row, int col)
        {
            return new Matrix2(
                _matrix[row != 0 ? 0 : 1, col != 0 ? 0 : 1], _matrix[row != 0 ? 0 : 1, col != 2 ? 2 : 1],
                _matrix[row != 2 ? 2 : 1, col != 0 ? 0 : 1], _matrix[row != 2 ? 2 : 1, col != 2 ? 2 : 1]);
        }

        public double Minor(int row, int col) => SubMatrix(row, col).Determinant;

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
                 + _matrix[0, 2] * CoFactor(0, 2);
        }

        public override string ToString()
        {
            return $"{_matrix[0, 0]} {_matrix[0, 1]} {_matrix[0, 2]}" + Environment.NewLine
                 + $"{_matrix[1, 0]} {_matrix[1, 1]} {_matrix[1, 2]}" + Environment.NewLine
                 + $"{_matrix[2, 0]} {_matrix[2, 1]} {_matrix[2, 2]}";
        }


    }
}
