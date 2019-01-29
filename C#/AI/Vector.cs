using System;

namespace AI
{
    public static class Vector
    {
        public static double[] Substract(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
                throw new Exception("Dimensions are not corresponding");

            double[] result = new double[vector1.Length];

            for (int i = 0; i < vector1.Length; i++)
            {
                result[i] = vector1[i] - vector2[i];
            }


            return result;
        }
    }
}
