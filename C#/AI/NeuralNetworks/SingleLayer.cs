namespace AI
{
    using ConsoleTableExt;
    using System;
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
        public static double _beta = 1;       //output function factor
        public static double _eta = 0.1;      //learning factor
        public static int _n;      //no of Neurons in Input Layer
        public static int _m;     //no of Neurons in Output Layer

        public static readonly int epochs = 1000000;  //no of iterations of learing phase


        static double[][] W;

        static Random random = new Random();

        static void Main(string[] args)
        {
            _n = X[0].Length;
            _m = Y.Length;


            // INITIALIZATION OF WEIGHT MATRIX
            W = Matrix.Create(_n, _m);

            for (int i = 0; i < W.Length; i++) //Macierz wag
            {
                for (int j = 0; j < W[i].Length; j++)
                {
                    W[i][j] = random.NextDouble();
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
            for (int e = 0; e < epochs; e++)
            {
                // CHOOSING RANDOM EXAMPLE
                int l = random.Next(X[0].Length);

                double[][] x1 = Matrix.Create(1, _n);
                x1[0] = X.GetColumn(l);


                //  CALCULATING RESPONSE OF NEURAL NETWORK           
                var y1 = CalculateReponse(x1);


                // CALCULATING DIFFERENCE
                double[] d = new double[_m];

                for (int i = 0; i < d.Length; i++)
                {
                    d[i] = Y[i][l] - y1[i];
                }


                // BACK PROPAGATION LEARNING
                double[][] dw = Matrix.Create(_n, _m);

                dw = Matrix.Multiple(x1.Transpose(), d, _eta);


                for (int i = 0; i < W.Length; i++)
                {
                    for (int j = 0; j < W[i].Length; j++)
                    {
                        W[i][j] = W[i][j] + dw[i][j];
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
            dt.Columns.Add("x3");
            dt.Columns.Add("x4");
            dt.Columns.Add("y1");
            dt.Columns.Add("y2");
            dt.Columns.Add("y3");
            dt.Columns.Add("y4");

            for (int l = 0; l < X.Length; l++)
            {
                double[][] x = Matrix.Create(1, _n);

                x[0] = X.GetColumn(l);


                double[][] u = Matrix.Multiple(W.Transpose(), x.Transpose());

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
            double[][] u1 = Matrix.Multiple(W.Transpose(), x1.Transpose());

            double[] y1 = new double[_m];
            for (int i = 0; i < u1.Length; i++)
            {
                y1[i] = 1 / (1 + Math.Exp(-_beta * u1[i][0]));
            }

            return y1;
        }
    }
}