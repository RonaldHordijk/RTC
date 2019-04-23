namespace RTC.Geometry
{
    public struct Matrix4
    {
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

        public double ValueAt(int row, int col) => _matrix[row, col];
    }
}
