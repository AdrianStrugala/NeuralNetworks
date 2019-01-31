namespace AI.NeuralNetworks
{
    using ConsoleTableExt;
    using System;
    using System.Data;
    using System.Linq;

    static class PIDSingleLayer
    {
        public enum PID
        {
            P = 0,
            I = 1,
            D = 2
        };

        private static readonly double[][] X_train =
        {
new double[] {2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000, 2.0000 },
new double[] {1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000, 1.0000 },
new double[] {0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000, 0.5000 },
new double[] {0.0000, 0.0100, 0.0200, 0.0300, 0.0400, 0.0500, 0.0600, 0.0700, 0.0800, 0.0900, 0.1000, 0.1100, 0.1200, 0.1300, 0.1400, 0.1500, 0.1600, 0.1700, 0.1800, 0.1900, 0.2000, 0.2100, 0.2200, 0.2300, 0.2400, 0.2500, 0.2600, 0.2700, 0.2800, 0.2900, 0.3000, 0.3100, 0.3200, 0.3300, 0.3400, 0.3500, 0.3600, 0.3700, 0.3800, 0.3900, 0.4000, 0.4100, 0.4200, 0.4300, 0.4400, 0.4500, 0.4600, 0.4700, 0.4800, 0.4900, 0.5000 },
new double[] {0.0000, 0.0250, 0.0500, 0.0750, 0.1000, 0.1250, 0.1500, 0.1750, 0.2000, 0.2250, 0.2500, 0.2750, 0.3000, 0.3250, 0.3500, 0.3750, 0.4000, 0.4250, 0.4500, 0.4750, 0.5000, 0.5250, 0.5500, 0.5750, 0.6000, 0.6250, 0.6500, 0.6750, 0.7000, 0.7250, 0.7500, 0.7750, 0.8000, 0.8250, 0.8500, 0.8750, 0.9000, 0.9250, 0.9500, 0.9750, 1.0000, 1.0250, 1.0500, 1.0750, 1.1000, 1.1250, 1.1500, 1.1750, 1.2000, 1.2250, 1.2500 },
new double[] {0.0000, 0.0500, 0.1000, 0.1500, 0.2000, 0.2500, 0.3000, 0.3500, 0.4000, 0.4500, 0.5000, 0.5500, 0.6000, 0.6500, 0.7000, 0.7500, 0.8000, 0.8500, 0.9000, 0.9500, 1.0000, 1.0500, 1.1000, 1.1500, 1.2000, 1.2500, 1.3000, 1.3500, 1.4000, 1.4500, 1.5000, 1.5500, 1.6000, 1.6500, 1.7000, 1.7500, 1.8000, 1.8500, 1.9000, 1.9500, 2.0000, 2.0500, 2.1000, 2.1500, 2.2000, 2.2500, 2.3000, 2.3500, 2.4000, 2.4500, 2.5000 },
new double[] {2.0000, 1.6375, 1.3406, 1.0976, 0.8987, 0.7358, 0.6024, 0.4932, 0.4038, 0.3306, 0.2707, 0.2216, 0.1814, 0.1485, 0.1216, 0.0996, 0.0815, 0.0667, 0.0546, 0.0447, 0.0366, 0.0300, 0.0246, 0.0201, 0.0165, 0.0135, 0.0110, 0.0090, 0.0074, 0.0061, 0.0050, 0.0041, 0.0033, 0.0027, 0.0022, 0.0018, 0.0015, 0.0012, 0.0010, 0.0008, 0.0007, 0.0005, 0.0004, 0.0004, 0.0003, 0.0002, 0.0002, 0.0002, 0.0001, 0.0001, 0.0001 },
new double[] {2.0000, 1.8097, 1.6375, 1.4816, 1.3406, 1.2131, 1.0976, 0.9932, 0.8987, 0.8131, 0.7358, 0.6657, 0.6024, 0.5451, 0.4932, 0.4463, 0.4038, 0.3654, 0.3306, 0.2991, 0.2707, 0.2449, 0.2216, 0.2005, 0.1814, 0.1642, 0.1485, 0.1344, 0.1216, 0.1100, 0.0996, 0.0901, 0.0815, 0.0738, 0.0667, 0.0604, 0.0546, 0.0494, 0.0447, 0.0405, 0.0366, 0.0331, 0.0300, 0.0271, 0.0246, 0.0222, 0.0201, 0.0182, 0.0165, 0.0149, 0.0135 },
new double[] {1.0000, 0.9512, 0.9048, 0.8607, 0.8187, 0.7788, 0.7408, 0.7047, 0.6703, 0.6376, 0.6065, 0.5769, 0.5488, 0.5220, 0.4966, 0.4724, 0.4493, 0.4274, 0.4066, 0.3867, 0.3679, 0.3499, 0.3329, 0.3166, 0.3012, 0.2865, 0.2725, 0.2592, 0.2466, 0.2346, 0.2231, 0.2122, 0.2019, 0.1920, 0.1827, 0.1738, 0.1653, 0.1572, 0.1496, 0.1423, 0.1353, 0.1287, 0.1225, 0.1165, 0.1108, 0.1054, 0.1003, 0.0954, 0.0907, 0.0863, 0.0821 }
        };

        private static readonly double[][] Y_train =  {
            new double[]  {1, 0, 0},
            new double[]  {1, 0, 0},
            new double[]  {1, 0, 0},
            new double[]  {0, 1, 0},
            new double[]  {0, 1, 0},
            new double[]  {0, 1, 0},
            new double[]  {0, 0, 1},
            new double[]  {0, 0, 1},
            new double[]  {0, 0, 1}
    };

        // P, I, D, PI, PD, PID
        private static readonly double[][] X_validation =
        {
            new double[] {0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500, 0.7500},
            new double[] {0.0000, 0.0250, 0.0500, 0.0750, 0.1000, 0.1250, 0.1500, 0.1750, 0.2000, 0.2250, 0.2500, 0.2750, 0.3000, 0.3250, 0.3500, 0.3750, 0.4000, 0.4250, 0.4500, 0.4750, 0.5000, 0.5250, 0.5500, 0.5750, 0.6000, 0.6250, 0.6500, 0.6750, 0.7000, 0.7250, 0.7500, 0.7750, 0.8000, 0.8250, 0.8500, 0.8750, 0.9000, 0.9250, 0.9500, 0.9750, 1.0000, 1.0250, 1.0500, 1.0750, 1.1000, 1.1250, 1.1500, 1.1750, 1.2000, 1.2250, 1.2500},
            new double[] {2.0000, 1.9025, 1.8097, 1.7214, 1.6375, 1.5576, 1.4816, 1.4094, 1.3406, 1.2753, 1.2131, 1.1539, 1.0976, 1.0441, 0.9932, 0.9447, 0.8987, 0.8548, 0.8131, 0.7735, 0.7358, 0.6999, 0.6657, 0.6333, 0.6024, 0.5730, 0.5451, 0.5185, 0.4932, 0.4691, 0.4463, 0.4245, 0.4038, 0.3841, 0.3654, 0.3475, 0.3306, 0.3145, 0.2991, 0.2845, 0.2707, 0.2575, 0.2449, 0.2330, 0.2216, 0.2108, 0.2005, 0.1907, 0.1814, 0.1726, 0.1642},
            new double[] {0.5000, 0.5250, 0.5500, 0.5750, 0.6000, 0.6250, 0.6500, 0.6750, 0.7000, 0.7250, 0.7500, 0.7750, 0.8000, 0.8250, 0.8500, 0.8750, 0.9000, 0.9250, 0.9500, 0.9750, 1.0000, 1.0250, 1.0500, 1.0750, 1.1000, 1.1250, 1.1500, 1.1750, 1.2000, 1.2250, 1.2500, 1.2750, 1.3000, 1.3250, 1.3500, 1.3750, 1.4000, 1.4250, 1.4500, 1.4750, 1.5000, 1.5250, 1.5500, 1.5750, 1.6000, 1.6250, 1.6500, 1.6750, 1.7000, 1.7250, 1.7500},
            new double[] {1.5000, 1.3187, 1.1703, 1.0488, 0.9493, 0.8679, 0.8012, 0.7466, 0.7019, 0.6653, 0.6353, 0.6108, 0.5907, 0.5743, 0.5608, 0.5498, 0.5408, 0.5334, 0.5273, 0.5224, 0.5183, 0.5150, 0.5123, 0.5101, 0.5082, 0.5067, 0.5055, 0.5045, 0.5037, 0.5030, 0.5025, 0.5020, 0.5017, 0.5014, 0.5011, 0.5009, 0.5007, 0.5006, 0.5005, 0.5004, 0.5003, 0.5003, 0.5002, 0.5002, 0.5002, 0.5001, 0.5001, 0.5001, 0.5001, 0.5001, 0.5000},
            new double[] {1.0000, 0.9344, 0.8852, 0.8494, 0.8247, 0.8089, 0.8006, 0.7983, 0.8009, 0.8076, 0.8177, 0.8304, 0.8454, 0.8621, 0.8804, 0.8999, 0.9204, 0.9417, 0.9637, 0.9862, 1.0092, 1.0325, 1.0561, 1.0800, 1.1041, 1.1284, 1.1528, 1.1773, 1.2018, 1.2265, 1.2512, 1.2760, 1.3008, 1.3257, 1.3506, 1.3755, 1.4004, 1.4253, 1.4503, 1.4752, 1.5002, 1.5251, 1.5501, 1.5751, 1.6001, 1.6251, 1.6501, 1.6750, 1.7000, 1.7250, 1.7500}
        };


        private static readonly double[][] Y_validation =  {
            new double[]  {1, 0, 0},
            new double[]  {0, 1, 0},
            new double[]  {0, 0, 1}
        };

        //Parameters
        public static double Beta = 1;       //output function factor
        public static double Eta = 1;      //learning factor
        public static int N;      //no of Neurons in Input Layer
        public static int M;     //no of Neurons in Output Layer

        public static double MinError;

        public static readonly int Epochs = 100000;  //max no of iterations of learing phase


        static double[][] Weights;

        private static double[] ErrorList;

        private static readonly Random Random = new Random();

        static void Main(string[] args)
        {
            N = X_train[0].Length;
            M = Y_train[0].Length;

            ErrorList = new double[Y_validation.Length];
            for (int i = 0; i < ErrorList.Length; i++)
            {
                ErrorList[i] = double.MaxValue;
            }

            MinError = double.MaxValue;

            // INITIALIZATION OF WEIGHT MATRIX
            Weights = Matrix.CreateRandom(N, M);


            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BEFORE TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            DisplayQuickNetworkResponse();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var noOfEpochsToLearn = Train();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AFTER TRAINING~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            DisplayQuickNetworkResponse();

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~EPOCHS~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine(noOfEpochsToLearn);
            //    DisplayFullNetworkResponse();

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
                int l = e % X_train.Length;

                double[][] input = Matrix.Create(1, N);
                input[0] = X_train[l];

                double[] output = Y_train[l];


                //  CALCULATING RESPONSE OF NEURAL NETWORK           
                var networkResponse = CalculateReponse(input);


                // CALCULATING DIFFERENCE
                double[] diff = Vector.Subtract(output, networkResponse);


                // BACK PROPAGATION LEARNING
                double[][] diffForWeights = Matrix.Multiple(input.Transpose(), diff, Eta);
                Weights = Matrix.Add(Weights, diffForWeights);

                //VALIDATION
                for (int k = 0; k < Y_validation.Length; k++)
                {
                    double error = MediumSquareError(Validate(k));
                    ErrorList[k] = error;
                }

                //CONDITION OF STOPPING LEARNING

                double errorOfThisLearningEpoch = MediumSquareError(ErrorList);

                if (errorOfThisLearningEpoch < MinError)
                {
                    MinError = errorOfThisLearningEpoch;
                }
                else if (errorOfThisLearningEpoch > MinError * 1.2)
                {
                    finishLearning = true;
                }
            }
            return e;
        }


        static void DisplayQuickNetworkResponse()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EXPECTED");
            dt.Columns.Add("ACTUAL");
            dt.Columns.Add("PROBABILITY [%]");


            for (int l = 0; l < Y_validation.Length; l++)
            {

                int expectedResponse = Y_validation[l].ToList().IndexOf(Y_validation[l].Max());

                var y = Validate(l);
                double actualMaxValue = y.Max();
                int actualResponse = y.ToList().IndexOf(actualMaxValue);

                DataRow row = dt.NewRow();
                row[0] = (PID)expectedResponse;
                row[1] = (PID)actualResponse;
                row[2] = actualMaxValue;

                dt.Rows.Add(row);
            }

            ConsoleTableBuilder builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        static double[] Validate(int index)
        {
            double[][] x = Matrix.Create(1, N);
            x[0] = X_validation[index];

            double[] y = CalculateReponse(x);
            return y;
        }

        static void DisplayFullNetworkResponse()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("y1");
            dt.Columns.Add("y2");
            dt.Columns.Add("y3");

            for (int l = 0; l < X_train.Length; l++)
            {
                double[][] x = Matrix.Create(1, N);
                x[0] = X_train[l];

                var y = CalculateReponse(x);

                DataRow row = dt.NewRow();
                row[0] = "PD";
                row[1] = "PI";
                row[2] = "PID";
                row[3] = y[0];
                row[4] = y[1];
                row[5] = y[2];

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