using System;
using System.Collections.Generic;
using System.Text;

namespace AI.NeuralNetworks
{
    class MultiLayer
    {

        //        int n = 2;
        //        int k1 = 2;
        //        //int k2 = Y.Length;
        //        int k2 = 1;
        //
        //
        //        double beta = 1;
        //        double eta = 0.1;
        //        double a = -0.1;
        //        double b = 0.1;
        //
        //        Random random = new Random();
        //
        //        // WSTEPNA INICJACJA MACIERZY WAG
        //        double[][] W1 = Create(n, k1);
        //
        //        double[][] W2 = Create(k1, k2);
        //
        //
        //            for (int i = 0; i<W1.Length; i++) // Warstwa ukryta(N + 1 x K1) 
        //            {
        //                for (int j = 0; j<W1[i].Length; j++)
        //                {
        //                    W1[i][j] = (b - a) * random.NextDouble() + a;
        //                }
        //}
        //
        //            for (int i = 0; i<W2.Length; i++) // Warstwa ukryta(N + 1 x K1) 
        //            {
        //                for (int j = 0; j<W2[i].Length; j++)
        //                {
        //                    W2[i][j] = (b - a) * random.NextDouble() + a;
        //                }
        //            }
        //
        //            //UCZENIE
        //            int Epoki = 100000;
        //
        //            for (int e = 0; e<Epoki; e++)
        //            {
        //                // LOSOWANIE PRZYKŁADU Z BAZY UCZĄCEJ
        //                int l = random.Next(n - 1);
        //
        ////    % DODANIE WEJSCIA BIASU
        //double[][] x1 = Create(n, 1);
        //
        //x1[0] = new double[] { -1, -1 };
        //                x1[1] = X.GetColumn(l);
        //
        //                //  % OBLICZENIE POBUDZENIA 1.WARSTWY            
        //                double[][] u1 = Multiple(W1.Transpose(), x1);
        //
        //double[][] y1 = Create(u1.Length, u1[0].Length);
        //                for (int i = 0; i<y1.Length; i++)
        //                {
        //                    for (int j = 0; j<y1[i].Length; j++)
        //                    {
        //                        y1[i][j] = 1 / (1 + Math.Exp(-beta* u1[i][j]));
        //                    }
        //                }
        //
        //                // % PRZEKAZANIE ODP. 1.WARSTWY NA WEJSCIE 2.WARSTWY
        //                //double[][] x2 = Create(y1.Length + 1, y1[0].Length);
        //                //x2[0] = new double[] { -1, -1, -1, -1 };
        //
        //                // % OBLICZENIE POBUDZENIE 2.WARSTWY
        //                double[][] u2 = Multiple(W2.Transpose(), y1);
        //
        //double y2 = 0;
        ////                for (int i = 0; i < u2.Length; i++)
        ////                {
        ////                    for (int j = 0; j < u2[i].Length; j++)
        ////                    {
        ////                        y2 += 1 / (1 + Math.Exp(-beta * u2[i][j]));
        ////                    }
        ////                }
        //
        //y2 += 1 / (1 + Math.Exp(-beta* u2[0][0]));
        //
        //                //   % WSTECZNA PROPAGACJA BLEDU
        //                //  % OBLICZENIE POPRAWEK DLA 2.WARSTWY
        //                double d2 = Y[l] - y2;
        //double e2 = beta * y2 * (1 - y2) * d2; // f'(u2)*d2
        //double[][] dW2 = Create(k1, k2);
        //
        //                for (int i = 0; i<dW2.Length; i++) // Warstwa ukryta(N + 1 x K1) 
        //                {
        //                    for (int j = 0; j<dW2[i].Length; j++)
        //                    {
        //                        dW2[i][j] = eta* u2[i][j] * e2;
        //                    }
        //                }
        //
        //
        //                //   % OBLICZENIE POPRAWEK DLA 1.WARSTWY
        //                double[][] d1 = Create(n, k1);
        //// d1 = W2(2:end, :) * e2; // d_j = w_1,j* e_1 +w_2,j* e2 +...
        ////              e1 = beta * y1.* (1 - y1).* d1; % f'(u1)*d1
        ////                dW1 = eta * X1 * e1';
        ////                // % modyfikacja macierzy wag
        ////                W1 = W1 + dW1;
        ////                W2 = W2 + dW2;
    }
}
