namespace AI
{
    using System;
    using System.Linq;

    public static class Matrix
    {
        private static readonly Random Random = new Random();

        public static double[] GetColumn(this double[][] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.Length)
                .Select(x => matrix[x][columnNumber])
                .ToArray();
        }

        public static double[][] Transpose(this double[][] matrix)
        {
            int w = matrix.Length;
            int h = matrix[0].Length;

            double[][] result = Create(h, w);

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j][i] = matrix[i][j];
                }
            }

            return result;
        }

        public static double[][] Create(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        public static double[][] CreateRandom(int rows, int cols)
        {
            var result = Create(rows, cols);

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = Random.NextDouble();
                }
            }

            return result;
        }

        public static double[][] Multiple(
            double[][] matrixA,
            double[] vectorB,
            double factor = 1)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bCols = vectorB.Length;
            if (aCols != 1)
                throw new Exception("Dimensions are not corresponding");
            double[][] result = Matrix.Create(aRows, bCols);
            for (int i = 0; i < aRows; ++i) // each row of A
                for (int j = 0; j < bCols; ++j) // each col of B
                    result[i][j] += matrixA[i][0] * vectorB[j] * factor;

            return result;
        }

        public static double[][] Multiple(
            double[][] matrixA,
            double[][] matrixB,
            double factor = 1)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bRows)
                throw new Exception("Dimensions are not corresponding");
            double[][] result = Matrix.Create(aRows, bCols);
            for (int i = 0; i < aRows; ++i) // each row of A
                for (int j = 0; j < bCols; ++j) // each col of B
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matrixA[i][k] * matrixB[k][j] * factor;
            return result;
        }

        public static double[][] Add(
            double[][] matrixA,
            double[][] matrixB)
        {
            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bCols || aRows != bRows)
                throw new Exception("Dimensions are not corresponding");
            double[][] result = Matrix.Create(aRows, aCols);

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }

            return result;
        }
    }
}
