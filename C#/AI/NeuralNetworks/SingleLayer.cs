namespace AI.NeuralNetworks
{
    using ConsoleTableExt;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    static class SingleLayer
    {
        //Animals
        public enum Environment
        {
            Water = 1,
            Air = 2,
            Ground = 3
        };


        private static readonly double[][] X =
        {
                      //husk //no of legs
            new double[] {1, 0, (double) Environment.Water}, //fish
            new double[] {0, 2, (double) Environment.Air}, //bird
            new double[] {0, 4, (double) Environment.Ground}, //alpaca
            new double[] {0, 2, (double)Environment.Ground }  //human
        };

        private static readonly double[][] Y =  {
            new double[]  {1, 0, 0, 0 },
            new double[]  {0, 1, 0, 0 },
            new double[]  {0, 0, 1, 0 },
            new double[]  {0, 0, 0, 1 }
    };

        //Parameters
        public static double Beta = 1;       //output function factor
        public static double Eta = 0.1;      //learning factor
        public static int N;      //no of Neurons in Input Layer
        public static int M;     //no of Neurons in Output Layer

        public static readonly int Epochs = 100000;  //no of iterations of learing phase


        static double[][] Weights;

        private static readonly List<double> ErrorList = new List<double>();

        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            N = X[0].Length;
            M = Y.Length;


            // INITIALIZATION OF WEIGHT MATRIX
            Weights = Matrix.Create(N, M);

            for (int i = 0; i < Weights.Length; i++) //Macierz wag
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    Weights[i][j] = Random.NextDouble();
                }
            }

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BEFORE TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Train();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AFTER TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ERROR~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //            foreach (var error in ErrorList)
            //            {
            //                Console.Write(error + " ");
            //            }

            Console.ReadKey();
        }

        static void Train()
        {
            //LEARNING
            for (int e = 0; e < Epochs; e++)
            {
                // CHOOSING RANDOM EXAMPLE
                int l = Random.Next(X.Length);

                double[][] input = Matrix.Create(1, N);
                input[0] = X[l];

                double[] output = Y[l];


                //  CALCULATING RESPONSE OF NEURAL NETWORK           
                var networkResponse = CalculateReponse(input);


                // CALCULATING DIFFERENCE
                double[] diff = Vector.Substract(output, networkResponse);


                // BACK PROPAGATION LEARNING
                double[][] diffForWeights = Matrix.Multiple(input.Transpose(), diff, Eta);

                Weights = Matrix.Add(Weights, diffForWeights);


                //STORE ERROR Błąd średnikwadratowy
                double error = 0;
                for (int i = 0; i < diff.Length; i++)
                {
                    error = diff[i] * diff[i];
                }

                error = Math.Sqrt(error);

                ErrorList.Add(error);
            }
        }


        static void Check()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~CHECK~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
            DataTable dt = new DataTable();
            dt.Columns.Add("x1");
            dt.Columns.Add("x2");
            dt.Columns.Add("x3");
            dt.Columns.Add("y1");
            dt.Columns.Add("y2");
            dt.Columns.Add("y3");
            dt.Columns.Add("y4");

            for (int l = 0; l < X.Length; l++)
            {
                double[][] x = Matrix.Create(1, N);

                x[0] = X[l];


                var y = CalculateReponse(x);

                DataRow row = dt.NewRow();
                row[0] = x[0][0];
                row[1] = x[0][1];
                row[2] = x[0][2];

                row[3] = y[0];
                row[4] = y[1];
                row[5] = y[2];
                row[6] = y[3];
                dt.Rows.Add(row);
            }


            ConsoleTableBuilder builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        public static double[] CalculateReponse(double[][] x)
        {
            double[][] u = Matrix.Multiple(x, Weights);

            double[] y = new double[M];
            for (int i = 0; i < u[0].Length; i++)
            {
                y[i] = 1 / (1 + Math.Exp(-Beta * u[0][i]));
            }

            //normalize
            double normalizeSum = y.Sum();

            for (int i = 0; i < y.Length; i++)
            {
                y[i] = y[i] / normalizeSum;
            }

            return y;
        }
    }
}