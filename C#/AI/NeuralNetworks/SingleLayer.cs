namespace AI
{
    using ConsoleTableExt;
    using System;
    using System.Collections.Generic;
    using System.Data;

    static class SingleLayer
    {
        private static readonly double[][] X =
        {
            new double[] {4, 2, 2, 4},
            new double[] {1, 1, 0, 0},
            new double[] {0, 0, 1, 0},
            new double[] {5, 2, 1, 10}
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

        public static readonly int Epochs = 1000;  //no of iterations of learing phase


        static double[][] Weights;

        private static readonly List<double> ErrorList = new List<double>();

        static Random random = new Random();

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
                    Weights[i][j] = random.NextDouble();
                }
            }

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BEFORE TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Train();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AFTER TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ERROR~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            foreach (var error in ErrorList)
            {
                Console.Write(error + " ");
            }

            Console.ReadKey();
        }

        static void Train()
        {
            //LEARNING
            for (int e = 0; e < Epochs; e++)
            {
                // CHOOSING RANDOM EXAMPLE
                int l = random.Next(X[0].Length);

                double[][] input = Matrix.Create(1, N);
                input[0] = X.GetColumn(l);


                //  CALCULATING RESPONSE OF NEURAL NETWORK           
                var response = CalculateReponse(input);


                // CALCULATING DIFFERENCE
                double[] diff = new double[M];

                for (int i = 0; i < diff.Length; i++)
                {
                    diff[i] = Y[i][l] - response[i];
                }


                // BACK PROPAGATION LEARNING
                double[][] diffForWeights = Matrix.Create(N, M);

                diffForWeights = Matrix.Multiple(input.Transpose(), diff, Eta);


                for (int i = 0; i < Weights.Length; i++)
                {
                    for (int j = 0; j < Weights[i].Length; j++)
                    {
                        Weights[i][j] = Weights[i][j] + diffForWeights[i][j];
                    }
                }



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
            dt.Columns.Add("x4");
            dt.Columns.Add("y1");
            dt.Columns.Add("y2");
            dt.Columns.Add("y3");
            dt.Columns.Add("y4");

            for (int l = 0; l < X.Length; l++)
            {
                double[][] x = Matrix.Create(1, N);

                x[0] = X.GetColumn(l);


                var y = CalculateReponse(x);

                DataRow row = dt.NewRow();
                row[0] = x[0][0];
                row[1] = x[0][1];
                row[2] = x[0][2];
                row[3] = x[0][3];
                row[4] = y[0];
                row[5] = y[1];
                row[6] = y[2];
                row[7] = y[3];
                dt.Rows.Add(row);
            }


            ConsoleTableBuilder builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        public static double[] CalculateReponse(double[][] x1)
        {
            double[][] u1 = Matrix.Multiple(Weights.Transpose(), x1.Transpose());

            double[] y1 = new double[M];
            for (int i = 0; i < u1.Length; i++)
            {
                y1[i] = 1 / (1 + Math.Exp(-Beta * u1[i][0]));
            }

            return y1;
        }
    }
}