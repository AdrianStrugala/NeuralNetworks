namespace AI.NeuralNetworks
{
    using ConsoleTableExt;
    using System;
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
                      //husk //no of legs                   //hoof
            new double[] {1, 0, (double) Environment.Water, 0}, //fish
            new double[] {0, 2, (double) Environment.Air, 0}, //bird
            new double[] {0, 4, (double) Environment.Ground, 1}, //alpaca
            new double[] {0, 2, (double)Environment.Ground, 0 }  //human
        };

        private static readonly double[][] Y =  {
            new double[]  {1, 0, 0, 0 },
            new double[]  {0, 1, 0, 0 },
            new double[]  {0, 0, 1, 0 },
            new double[]  {0, 0, 0, 1 }
    };

        //Parameters
        public static double Beta = 1;       //output function factor
        public static double Eta = 1;      //learning factor
        public static int N;      //no of Neurons in Input Layer
        public static int M;     //no of Neurons in Output Layer

        public const double AcceptedError = 0.01;

        public static readonly int Epochs = 100000;  //no of iterations of learing phase


        static double[][] Weights;

        private static double[] ErrorList;

        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            N = X[0].Length;
            M = Y.Length;

            ErrorList = new double[M];
            for (int i = 0; i < ErrorList.Length; i++)
            {
                ErrorList[i] = Double.MaxValue;
            }

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
            var noOfEpochsToLearn = Train();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AFTER TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Check();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~EPOCHS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(noOfEpochsToLearn);


            Console.ReadKey();
        }

        static int Train()
        {
            bool finishLearning = false;

            int e = 0;
            //LEARNING
            for (; e < Epochs && !finishLearning; e++)
            {
                // CHOOSING RANDOM EXAMPLE
                int l = e % X.Length;

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


                //STORE Medium Square Error
                double error = MediumSquareError(diff);
                ErrorList[l] = error;

                foreach (var err in ErrorList)
                {
                    if (err > AcceptedError)
                    {
                        break;
                    }
                    finishLearning = true;
                }             
            }
            return e;
        }


        static void Check()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~CHECK~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 
            DataTable dt = new DataTable();
            dt.Columns.Add("husk");
            dt.Columns.Add("no of legs");
            dt.Columns.Add("enviroment");
            dt.Columns.Add("hoof");

            dt.Columns.Add("fish");
            dt.Columns.Add("bird");
            dt.Columns.Add("alpaca");
            dt.Columns.Add("human");

            for (int l = 0; l < X.Length; l++)
            {
                double[][] x = Matrix.Create(1, N);

                x[0] = X[l];


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

        public static double MediumSquareError(double[] vector)
        {
            double error = 0;
            foreach (var t in vector)
            {
                error += t * t;
            }
            error = Math.Sqrt(error);
            return error;
        }
    }
}