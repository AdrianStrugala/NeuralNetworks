namespace AI.NeuralNetworks
{
    using ConsoleTableExt;
    using System;
    using System.Data;

    class MultiLayer
    {
        private static readonly double[][] X =
        {
            new double[] {0, 1, 0, 1},
            new double[] {1, 0, 0, 1}
        };

        private static readonly double[][] Y =  {
            new double[]  {1, 1, 0, 0 },
            new double[]  {0, 0, 1, 1 }
    };

        //Parameters
        public static double Beta = 1;       //output function factor
        public static double Eta = 0.1;      //learning factor
        public static int N;      //no of Neurons in Input Layer
        public static int K;      //no of Neurons in Hidden Layer
        public static int M;     //no of Neurons in Output Layer

        public static readonly int Epochs = 1000000;  //no of iterations of learing phase


        static double[][] W;
        static double[][] W2;

        static Random random = new Random();

        static void Main2(string[] args)
        {
            N = X.Length;
            M = Y.Length;

            K = (M + N) / 2;


            // INITIALIZATION OF WEIGHT MATRIX
            W = Matrix.Create(N, K);

            for (int i = 0; i < W.Length; i++) //Macierz wag
            {
                for (int j = 0; j < W[i].Length; j++)
                {
                    W[i][j] = random.NextDouble();
                }
            }

            W2 = Matrix.Create(K, M);

            for (int i = 0; i < W2.Length; i++) //Macierz wag
            {
                for (int j = 0; j < W2[i].Length; j++)
                {
                    W2[i][j] = random.NextDouble();
                }
            }

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BEFORE TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Train();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AFTER TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();



            Console.ReadKey();
        }

        static void Train()
        {
            //LEARNING
            for (int e = 0; e < Epochs; e++)
            {
                // CHOOSING RANDOM EXAMPLE
                int l = random.Next(X[0].Length);

                double[][] x1 = Matrix.Create(1, N);
                x1[0] = X.GetColumn(l);


                //  CALCULATING RESPONSE OF NEURAL NETWORK           
                double[][] u1 = Matrix.Multiple(W.Transpose(), x1.Transpose());

                double[][] y1 = Matrix.Create(K, 1);
                for (int i = 0; i < u1.Length; i++)
                {
                    y1[i][0] = 1 / (1 + Math.Exp(-Beta * u1[i][0]));
                }

                //2nd layer
                double[][] u2 = Matrix.Multiple(W2.Transpose(), y1);

                double[] y2 = new double[M];
                for (int i = 0; i < u1.Length; i++)
                {
                    y2[i] = 1 / (1 + Math.Exp(-Beta * u2[i][0]));
                }

                // CALCULATING DIFFERENCE
                double[] d2 = new double[M];

                for (int i = 0; i < d2.Length; i++)
                {
                    d2[i] = Y[i][l] - y2[i];
                }


                // BACK PROPAGATION LEARNING

                double[][] e2 = Matrix.Create(1, M);

                for (int i = 0; i < e2[0].Length; i++)
                {
                    e2[0][i] = Beta * y2[i] * (1 - y2[i]) * d2[i];
                }

                double[][] dW2 = Matrix.Create(K, M);

                dW2 = Matrix.Multiple(y1, e2, Eta);

                //To 1st layer
                double[][] d1 = Matrix.Multiple(W2, e2.Transpose());

                double[][] e1 = Matrix.Create(1, K);

                for (int i = 0; i < e1[0].Length; i++)
                {
                    e1[0][i] = Beta * y1[i][0] * (1 - y1[i][0]) * d1[i][0];
                }

                double[][] dW1 = Matrix.Create(N, K);

                dW1 = Matrix.Multiple(x1.Transpose(), e1, Eta);


                //APPLY DIFERENCES
                for (int i = 0; i < W2.Length; i++)
                {
                    for (int j = 0; j < W2[i].Length; j++)
                    {
                        W2[i][j] = W2[i][j] + dW2[i][j];
                    }
                }

                for (int i = 0; i < W.Length; i++)
                {
                    for (int j = 0; j < W[i].Length; j++)
                    {
                        W[i][j] = W[i][j] + dW1[i][j];
                    }
                }

            }
        }


        static void Check()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~CHECK~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
            DataTable dt = new DataTable();
            dt.Columns.Add("x1");
            dt.Columns.Add("x2");
            dt.Columns.Add("true");
            dt.Columns.Add("false");

            for (int l = 0; l < X[0].Length; l++)
            {
                double[][] x = Matrix.Create(1, N);

                x[0] = X.GetColumn(l);

                double[][] u1 = Matrix.Multiple(W.Transpose(), x.Transpose());

                double[][] y1 = Matrix.Create(K, 1);
                for (int i = 0; i < u1.Length; i++)
                {
                    y1[i][0] = 1 / (1 + Math.Exp(-Beta * u1[i][0]));
                }

                //2nd layer
                double[][] u2 = Matrix.Multiple(W2.Transpose(), y1);

                double[] y2 = new double[M];
                for (int i = 0; i < u1.Length; i++)
                {
                    y2[i] = 1 / (1 + Math.Exp(-Beta * u2[i][0]));
                }

                DataRow row = dt.NewRow();
                row[0] = x[0][0];
                row[1] = x[0][1];
                row[2] = y2[0];
                row[3] = y2[1];
                dt.Rows.Add(row);
            }


            ConsoleTableBuilder builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

    }
}
