using System;

namespace RTC.Geometry
{
    public class Matrix2
    {
        private const double Epsilon = 0.00001;

        private readonly double[,] _matrix;

        public Matrix2(double m00, double m01, double m10, double m11)
        {
            _matrix = new double[,] {
                { m00, m01 },
                { m10, m11 } };
        }

        public double ValueAt(int row, int col) => _matrix[row, col];

        public override bool Equals(object obj)
        {
            if (obj is Matrix2)
                return this == (Matrix2)obj;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Matrix2 m1, Matrix2 m2)
        {
            return Math.Abs(m1._matrix[0, 0] - m2._matrix[0, 0]) < Epsilon
                && Math.Abs(m1._matrix[0, 1] - m2._matrix[0, 1]) < Epsilon
                && Math.Abs(m1._matrix[1, 0] - m2._matrix[1, 0]) < Epsilon
                && Math.Abs(m1._matrix[1, 1] - m2._matrix[1, 1]) < Epsilon;
        }

        public static bool operator !=(Matrix2 m1, Matrix2 m2) => !(m1 == m2);

        public double Determinant => (_matrix[0, 0] * _matrix[1, 1]) - (_matrix[1, 0] * _matrix[0, 1]);

        public override string ToString()
        {
            return $"{_matrix[0, 0]} {_matrix[0, 1]}" + Environment.NewLine
                 + $"{_matrix[1, 0]} {_matrix[1, 1]}";
        }
    }
}
