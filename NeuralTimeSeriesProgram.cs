using System;
using IA;
namespace NeuralTimeSeries
{
    class NeuralTimeSeriesProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nIniciando a Demo de Séries Temporais usando RNA");
            Console.WriteLine("O Objetivo é predizer o volume de agendamentos ao longo do tempo ");
            Console.WriteLine("Dados de teste de Janeiro de 2019 a Dezembro de 2020 \n");

            double[][] DadosDeTreino = GetHistoricData();
            DadosDeTreino = Normalizar(DadosDeTreino,100.0); //divide os valores por 100 para melhorar a precisão
            Console.WriteLine("Dados de treinamento normalizados: ");
            ExibeMatriz(DadosDeTreino, 5, 2, true);  // Primeiras 5 linhas, 2 decimais, exibindo índices

            int numInput = 4; // números preditores
            int numHidden = 12;
            int numOutput = 1; // regressão

            Console.WriteLine("Criando uma RNA " + numInput + "-" + numHidden + "-" + numOutput);
            RedeNeural rn = new RedeNeural(numInput, numHidden, numOutput);

            int maxEpocas = 10000;
            double taxaDeAprend = 0.01;
            Console.WriteLine("\nConfigurando maxEpocas = " + maxEpocas);
            Console.WriteLine("Configurando taxaDeAprend = " + taxaDeAprend.ToString("F2"));

            Console.WriteLine("\nIniciando treinamento");
            double[] pesos = rn.Treinamento(DadosDeTreino, maxEpocas, taxaDeAprend);
            Console.WriteLine("Pronto");
            Console.WriteLine("\nPesos e vieses do modelo de rede neural final:\n");
            ExibeVetor(pesos, 2, 10, true);

            double PrecisaoTrein = rn.Precisao(DadosDeTreino, 0.30);  // com 30
            Console.WriteLine("\nPrecisão do modelo (+/- 30%) nos dados de treinamento = " +
              PrecisaoTrein.ToString("F4"));
            
            //os 4 últimos dados históricos para predizer o próximo futuro.
            double[] preditores = new double[] { 5.08, 4.61, 3.90, 4.32 }; 

            double[] forecast = rn.SaidasComputadas(preditores);  // 4.33362252510741
            Console.Write("\nAgendamentos previstos para janeiro 2021 (t=145): ");
            Console.WriteLine((forecast[0] * 100).ToString("F0")); // dígitos inteiros, 0 decimais com sinal negativo opcional.

            #region Parâmetros Comentados - testes e avaliações de estudo
            //double[] preditores = new double[] { 4.61, 3.90, 4.32, 4.33362252510741 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 4.33933519590564
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 3.90, 4.32, 4.33362252510741, 4.33933519590564 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 4.69036205766231
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 4.32, 4.33362252510741, 4.33933519590564, 4.69036205766231 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 4.83360378041341
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 4.33362252510741, 4.33933519590564, 4.69036205766231, 4.83360378041341 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 5.50703476366623
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 4.33933519590564, 4.69036205766231, 4.83360378041341, 5.50703476366623 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 6.39605763609294
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 4.69036205766231, 4.83360378041341, 5.50703476366623, 6.39605763609294 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 6.06664881070054
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 4.83360378041341, 5.50703476366623, 6.39605763609294, 6.06664881070054 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 4.95781531728514
            //Console.WriteLine(forecast[0]);

            //double[] preditores = new double[] { 5.50703476366623, 6.39605763609294, 6.06664881070054, 4.95781531728514 };
            //double[] forecast = nn.ComputeOutputs(preditores);  // 4.45837470369601
            //Console.WriteLine(forecast[0]);
            #endregion

            Console.WriteLine("\nFim da demonstração da série temporal\n");
            Console.ReadLine();
        } // Main

        static double[][] Normalizar(double[][] data,double fator)
        {
            // divide all by 100.0
            int rows = data.Length;
            int cols = data[0].Length;
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];

            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = data[i][j] / fator;
            return result;
        }

        //static double[][] MakeSineData()
        //{
        //  double[][] sineData = new double[17][];
        //  sineData[0] = new double[] { 0.0000, 0.841470985, 0.909297427, 0.141120008 };
        //  sineData[1] = new double[] { 0.841470985, 0.909297427, 0.141120008, -0.756802495 };
        //  sineData[2] = new double[] { 0.909297427, 0.141120008, -0.756802495, -0.958924275 };
        //  sineData[3] = new double[] { 0.141120008, -0.756802495, -0.958924275, -0.279415498 };
        //  sineData[4] = new double[] { -0.756802495, -0.958924275, -0.279415498, 0.656986599 };
        //  sineData[5] = new double[] { -0.958924275, -0.279415498, 0.656986599, 0.989358247 };
        //  sineData[6] = new double[] { -0.279415498, 0.656986599, 0.989358247, 0.412118485 };
        //  sineData[7] = new double[] { 0.656986599, 0.989358247, 0.412118485, -0.544021111 };
        //  sineData[8] = new double[] { 0.989358247, 0.412118485, -0.544021111, -0.999990207 };
        //  sineData[9] = new double[] { 0.412118485, -0.544021111, -0.999990207, -0.536572918 };
        //  sineData[10] = new double[] { -0.544021111, -0.999990207, -0.536572918, 0.420167037 };
        //  sineData[11] = new double[] { -0.999990207, -0.536572918, 0.420167037, 0.990607356 };
        //  sineData[12] = new double[] { -0.536572918, 0.420167037, 0.990607356, 0.65028784 };
        //  sineData[13] = new double[] { 0.420167037, 0.990607356, 0.65028784, -0.287903317 };
        //  sineData[14] = new double[] { 0.990607356, 0.65028784, -0.287903317, -0.961397492 };
        //  sineData[15] = new double[] { 0.65028784, -0.287903317, -0.961397492, -0.750987247 };
        //  sineData[16] = new double[] { -0.287903317, -0.961397492, -0.750987247, 0.14987721 };
        //  return sineData;
        //} // MakeSineData

        static double[][] GetHistoricData()
        {
            double[][] DadosHistoricos = new double[140][];
            DadosHistoricos[0] = new double[] { 112, 118, 132, 129, 121 };
            DadosHistoricos[1] = new double[] { 118, 132, 129, 121, 135 };
            DadosHistoricos[2] = new double[] { 132, 129, 121, 135, 148 };
            DadosHistoricos[3] = new double[] { 129, 121, 135, 148, 148 };
            DadosHistoricos[4] = new double[] { 121, 135, 148, 148, 136 };
            DadosHistoricos[5] = new double[] { 135, 148, 148, 136, 119 };
            DadosHistoricos[6] = new double[] { 148, 148, 136, 119, 104 };
            DadosHistoricos[7] = new double[] { 148, 136, 119, 104, 118 };
            DadosHistoricos[8] = new double[] { 136, 119, 104, 118, 115 };
            DadosHistoricos[9] = new double[] { 119, 104, 118, 115, 126 };
            DadosHistoricos[10] = new double[] { 104, 118, 115, 126, 141 };
            DadosHistoricos[11] = new double[] { 118, 115, 126, 141, 135 };
            DadosHistoricos[12] = new double[] { 115, 126, 141, 135, 125 };
            DadosHistoricos[13] = new double[] { 126, 141, 135, 125, 149 };
            DadosHistoricos[14] = new double[] { 141, 135, 125, 149, 170 };
            DadosHistoricos[15] = new double[] { 135, 125, 149, 170, 170 };
            DadosHistoricos[16] = new double[] { 125, 149, 170, 170, 158 };
            DadosHistoricos[17] = new double[] { 149, 170, 170, 158, 133 };
            DadosHistoricos[18] = new double[] { 170, 170, 158, 133, 114 };
            DadosHistoricos[19] = new double[] { 170, 158, 133, 114, 140 };
            DadosHistoricos[20] = new double[] { 158, 133, 114, 140, 145 };
            DadosHistoricos[21] = new double[] { 133, 114, 140, 145, 150 };
            DadosHistoricos[22] = new double[] { 114, 140, 145, 150, 178 };
            DadosHistoricos[23] = new double[] { 140, 145, 150, 178, 163 };
            DadosHistoricos[24] = new double[] { 145, 150, 178, 163, 172 };
            DadosHistoricos[25] = new double[] { 150, 178, 163, 172, 178 };
            DadosHistoricos[26] = new double[] { 178, 163, 172, 178, 199 };
            DadosHistoricos[27] = new double[] { 163, 172, 178, 199, 199 };
            DadosHistoricos[28] = new double[] { 172, 178, 199, 199, 184 };
            DadosHistoricos[29] = new double[] { 178, 199, 199, 184, 162 };
            DadosHistoricos[30] = new double[] { 199, 199, 184, 162, 146 };
            DadosHistoricos[31] = new double[] { 199, 184, 162, 146, 166 };
            DadosHistoricos[32] = new double[] { 184, 162, 146, 166, 171 };
            DadosHistoricos[33] = new double[] { 162, 146, 166, 171, 180 };
            DadosHistoricos[34] = new double[] { 146, 166, 171, 180, 193 };
            DadosHistoricos[35] = new double[] { 166, 171, 180, 193, 181 };
            DadosHistoricos[36] = new double[] { 171, 180, 193, 181, 183 };
            DadosHistoricos[37] = new double[] { 180, 193, 181, 183, 218 };
            DadosHistoricos[38] = new double[] { 193, 181, 183, 218, 230 };
            DadosHistoricos[39] = new double[] { 181, 183, 218, 230, 242 };
            DadosHistoricos[40] = new double[] { 183, 218, 230, 242, 209 };
            DadosHistoricos[41] = new double[] { 218, 230, 242, 209, 191 };
            DadosHistoricos[42] = new double[] { 230, 242, 209, 191, 172 };
            DadosHistoricos[43] = new double[] { 242, 209, 191, 172, 194 };
            DadosHistoricos[44] = new double[] { 209, 191, 172, 194, 196 };
            DadosHistoricos[45] = new double[] { 191, 172, 194, 196, 196 };
            DadosHistoricos[46] = new double[] { 172, 194, 196, 196, 236 };
            DadosHistoricos[47] = new double[] { 194, 196, 196, 236, 235 };
            DadosHistoricos[48] = new double[] { 196, 196, 236, 235, 229 };
            DadosHistoricos[49] = new double[] { 196, 236, 235, 229, 243 };
            DadosHistoricos[50] = new double[] { 236, 235, 229, 243, 264 };
            DadosHistoricos[51] = new double[] { 235, 229, 243, 264, 272 };
            DadosHistoricos[52] = new double[] { 229, 243, 264, 272, 237 };
            DadosHistoricos[53] = new double[] { 243, 264, 272, 237, 211 };
            DadosHistoricos[54] = new double[] { 264, 272, 237, 211, 180 };
            DadosHistoricos[55] = new double[] { 272, 237, 211, 180, 201 };
            DadosHistoricos[56] = new double[] { 237, 211, 180, 201, 204 };
            DadosHistoricos[57] = new double[] { 211, 180, 201, 204, 188 };
            DadosHistoricos[58] = new double[] { 180, 201, 204, 188, 235 };
            DadosHistoricos[59] = new double[] { 201, 204, 188, 235, 227 };
            DadosHistoricos[60] = new double[] { 204, 188, 235, 227, 234 };
            DadosHistoricos[61] = new double[] { 188, 235, 227, 234, 264 };
            DadosHistoricos[62] = new double[] { 235, 227, 234, 264, 302 };
            DadosHistoricos[63] = new double[] { 227, 234, 264, 302, 293 };
            DadosHistoricos[64] = new double[] { 234, 264, 302, 293, 259 };
            DadosHistoricos[65] = new double[] { 264, 302, 293, 259, 229 };
            DadosHistoricos[66] = new double[] { 302, 293, 259, 229, 203 };
            DadosHistoricos[67] = new double[] { 293, 259, 229, 203, 229 };
            DadosHistoricos[68] = new double[] { 259, 229, 203, 229, 242 };
            DadosHistoricos[69] = new double[] { 229, 203, 229, 242, 233 };
            DadosHistoricos[70] = new double[] { 203, 229, 242, 233, 267 };
            DadosHistoricos[71] = new double[] { 229, 242, 233, 267, 269 };
            DadosHistoricos[72] = new double[] { 242, 233, 267, 269, 270 };
            DadosHistoricos[73] = new double[] { 233, 267, 269, 270, 315 };
            DadosHistoricos[74] = new double[] { 267, 269, 270, 315, 364 };
            DadosHistoricos[75] = new double[] { 269, 270, 315, 364, 347 };
            DadosHistoricos[76] = new double[] { 270, 315, 364, 347, 312 };
            DadosHistoricos[77] = new double[] { 315, 364, 347, 312, 274 };
            DadosHistoricos[78] = new double[] { 364, 347, 312, 274, 237 };
            DadosHistoricos[79] = new double[] { 347, 312, 274, 237, 278 };
            DadosHistoricos[80] = new double[] { 312, 274, 237, 278, 284 };
            DadosHistoricos[81] = new double[] { 274, 237, 278, 284, 277 };
            DadosHistoricos[82] = new double[] { 237, 278, 284, 277, 317 };
            DadosHistoricos[83] = new double[] { 278, 284, 277, 317, 313 };
            DadosHistoricos[84] = new double[] { 284, 277, 317, 313, 318 };
            DadosHistoricos[85] = new double[] { 277, 317, 313, 318, 374 };
            DadosHistoricos[86] = new double[] { 317, 313, 318, 374, 413 };
            DadosHistoricos[87] = new double[] { 313, 318, 374, 413, 405 };
            DadosHistoricos[88] = new double[] { 318, 374, 413, 405, 355 };
            DadosHistoricos[89] = new double[] { 374, 413, 405, 355, 306 };
            DadosHistoricos[90] = new double[] { 413, 405, 355, 306, 271 };
            DadosHistoricos[91] = new double[] { 405, 355, 306, 271, 306 };
            DadosHistoricos[92] = new double[] { 355, 306, 271, 306, 315 };
            DadosHistoricos[93] = new double[] { 306, 271, 306, 315, 301 };
            DadosHistoricos[94] = new double[] { 271, 306, 315, 301, 356 };
            DadosHistoricos[95] = new double[] { 306, 315, 301, 356, 348 };
            DadosHistoricos[96] = new double[] { 315, 301, 356, 348, 355 };
            DadosHistoricos[97] = new double[] { 301, 356, 348, 355, 422 };
            DadosHistoricos[98] = new double[] { 356, 348, 355, 422, 465 };
            DadosHistoricos[99] = new double[] { 348, 355, 422, 465, 467 };
            DadosHistoricos[100] = new double[] { 355, 422, 465, 467, 404 };
            DadosHistoricos[101] = new double[] { 422, 465, 467, 404, 347 };
            DadosHistoricos[102] = new double[] { 465, 467, 404, 347, 305 };
            DadosHistoricos[103] = new double[] { 467, 404, 347, 305, 336 };
            DadosHistoricos[104] = new double[] { 404, 347, 305, 336, 340 };
            DadosHistoricos[105] = new double[] { 347, 305, 336, 340, 318 };
            DadosHistoricos[106] = new double[] { 305, 336, 340, 318, 362 };
            DadosHistoricos[107] = new double[] { 336, 340, 318, 362, 348 };
            DadosHistoricos[108] = new double[] { 340, 318, 362, 348, 363 };
            DadosHistoricos[109] = new double[] { 318, 362, 348, 363, 435 };
            DadosHistoricos[110] = new double[] { 362, 348, 363, 435, 491 };
            DadosHistoricos[111] = new double[] { 348, 363, 435, 491, 505 };
            DadosHistoricos[112] = new double[] { 363, 435, 491, 505, 404 };
            DadosHistoricos[113] = new double[] { 435, 491, 505, 404, 359 };
            DadosHistoricos[114] = new double[] { 491, 505, 404, 359, 310 };
            DadosHistoricos[115] = new double[] { 505, 404, 359, 310, 337 };
            DadosHistoricos[116] = new double[] { 404, 359, 310, 337, 360 };
            DadosHistoricos[117] = new double[] { 359, 310, 337, 360, 342 };
            DadosHistoricos[118] = new double[] { 310, 337, 360, 342, 406 };
            DadosHistoricos[119] = new double[] { 337, 360, 342, 406, 396 };
            DadosHistoricos[120] = new double[] { 360, 342, 406, 396, 420 };
            DadosHistoricos[121] = new double[] { 342, 406, 396, 420, 472 };
            DadosHistoricos[122] = new double[] { 406, 396, 420, 472, 548 };
            DadosHistoricos[123] = new double[] { 396, 420, 472, 548, 559 };
            DadosHistoricos[124] = new double[] { 420, 472, 548, 559, 463 };
            DadosHistoricos[125] = new double[] { 472, 548, 559, 463, 407 };
            DadosHistoricos[126] = new double[] { 548, 559, 463, 407, 362 };
            DadosHistoricos[127] = new double[] { 559, 463, 407, 362, 405 };
            DadosHistoricos[128] = new double[] { 463, 407, 362, 405, 417 };
            DadosHistoricos[129] = new double[] { 407, 362, 405, 417, 391 };
            DadosHistoricos[130] = new double[] { 362, 405, 417, 391, 419 };
            DadosHistoricos[131] = new double[] { 405, 417, 391, 419, 461 };
            DadosHistoricos[132] = new double[] { 417, 391, 419, 461, 472 };
            DadosHistoricos[133] = new double[] { 391, 419, 461, 472, 535 };
            DadosHistoricos[134] = new double[] { 419, 461, 472, 535, 622 };
            DadosHistoricos[135] = new double[] { 461, 472, 535, 622, 606 };
            DadosHistoricos[136] = new double[] { 472, 535, 622, 606, 508 };
            DadosHistoricos[137] = new double[] { 535, 622, 606, 508, 461 };
            DadosHistoricos[138] = new double[] { 622, 606, 508, 461, 390 };
            DadosHistoricos[139] = new double[] { 606, 508, 461, 390, 432 };
            return DadosHistoricos;
        }

        static void ExibeMatriz(double[][] matriz, int numDeLinhas, int decimais, bool indices)
        {
            int len = matriz.Length.ToString().Length;
            for (int i = 0; i < numDeLinhas; ++i)
            {
                if (indices == true)
                    Console.Write("[" + i.ToString().PadLeft(len) + "]  ");
                for (int j = 0; j < matriz[i].Length; ++j)
                {
                    double v = matriz[i][j];
                    if (v >= 0.0)
                        Console.Write(" "); // '+'
                    Console.Write(v.ToString("F" + decimais) + "  ");
                }
                Console.WriteLine("");
            }

            if (numDeLinhas < matriz.Length)
            {
                Console.WriteLine(". . .");
                int lastRow = matriz.Length - 1;
                if (indices == true)
                    Console.Write("[" + lastRow.ToString().PadLeft(len) + "]  ");
                for (int j = 0; j < matriz[lastRow].Length; ++j)
                {
                    double v = matriz[lastRow][j];
                    if (v >= 0.0)
                        Console.Write(" "); // '+'
                    Console.Write(v.ToString("F" + decimais) + "  ");
                }
            }
            Console.WriteLine("\n");
        }

        static void ExibeVetor(double[] vetor, int decimais, int comprimentoLinha, bool novaLinha)
        {
            for (int i = 0; i < vetor.Length; ++i)
            {
                if (i > 0 && i % comprimentoLinha == 0) Console.WriteLine("");
                if (vetor[i] >= 0) Console.Write(" ");
                Console.Write(vetor[i].ToString("F" + decimais) + " ");
            }
            if (novaLinha == true)
                Console.WriteLine("");
        }


    } // Program


} // namespace
