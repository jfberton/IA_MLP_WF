namespace IA_MLP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }


        #region funciones varias

        public static List<string> ShowMatrix(double[][] matrix, int numRows, int decimals, bool indices)
        {
            List<string> result = new List<string>();
            
            for (int i = 0; i < numRows; ++i)
            {
                string line = "";
                if (indices == true)
                    line += i.ToString().PadLeft(3) + ": ";
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    double v = matrix[i][j];
                    if (v >= 0.0)
                        line += " ";
                    line += v.ToString("F" + decimals) + " ";
                }
                result.Add(line);
            }

            if (numRows < matrix.Length)
            {
                result.Add(".  .  .  ");

                int lastRow = matrix.Length - 1;

                string line = "";
                
                if (indices == true)
                    line += lastRow.ToString().PadLeft(3) + ": ";
                for (int j = 0; j < matrix[lastRow].Length; ++j)
                {
                    double v = matrix[lastRow][j];
                    if (v >= 0.0)
                        line += " ";
                    line += v.ToString("F" + decimals) + " ";
                }
                result.Add(line);
            }

            return result;
        }

        public static string ShowVector(double[] vector, int decimals, int lineLen, bool newLine)
        {
            string result = string.Empty;
            string line = "";
            for (int i = 0; i < vector.Length; ++i)
            {
                double v = vector[i];
                if (v >= 0.0)
                    line += " ";
                line += v.ToString("F" + decimals) + " ";
                if (line.Length >= lineLen)
                {
                    result += line + Environment.NewLine;
                    line = "";
                }
            }
            return result;
        }

        public static double[][] MakeAllData(int numInput, int numHidden, int numOutput, int numRows, int seed)
        {
            Random rnd = new Random(seed);
            int numWeights = (numInput * numHidden) + numHidden + (numHidden * numOutput) + numOutput;
            double[] weights = new double[numWeights]; // actually weights & biases
            for (int i = 0; i < numWeights; ++i)
                weights[i] = 20.0 * rnd.NextDouble() - 10.0; // [-10.0 to 10.0]

            Console.WriteLine("Generating weights and biases:");
            ShowVector(weights, 2, 10, true);

            double[][] result = new double[numRows][]; // allocate return-result
            for (int i = 0; i < numRows; ++i)
                result[i] = new double[numInput + numOutput]; // 1-of-N in last column

            Perceptron_e_o_s gnn = new Perceptron_e_o_s(numInput, numHidden, numOutput); // generating NN
            gnn.CargarPesosyUmbrales(weights);

            for (int r = 0; r < numRows; ++r) // for each row
            {
                // generate random inputs
                double[] inputs = new double[numInput];
                for (int i = 0; i < numInput; ++i)
                    inputs[i] = 20.0 * rnd.NextDouble() - 10.0; // [-10.0 to -10.0]

                // compute outputs
                double[] outputs = gnn.ProcesarEntradas(inputs);

                // translate outputs to 1-of-N
                double[] oneOfN = new double[numOutput]; // all 0.0

                int maxIndex = 0;
                double maxValue = outputs[0];
                for (int i = 0; i < numOutput; ++i)
                {
                    if (outputs[i] > maxValue)
                    {
                        maxIndex = i;
                        maxValue = outputs[i];
                    }
                }
                oneOfN[maxIndex] = 1.0;

                // place inputs and 1-of-N output values into curr row
                int c = 0; // column into result[][]
                for (int i = 0; i < numInput; ++i) // inputs
                    result[r][c++] = inputs[i];
                for (int i = 0; i < numOutput; ++i) // outputs
                    result[r][c++] = oneOfN[i];
            } // each row
            return result;
        } // MakeAllData

        public static double[][] MakeAllDataFor2Layer(int numInput, int numHidden1, int numHidden2, int numOutput, int numRows, int seed)
        {
            Random rnd = new Random(seed);
            int numWeights = (numInput * numHidden1) + numHidden1 + (numHidden1 * numHidden2) + numHidden2 + (numHidden2 * numOutput) + numOutput;
            double[] weights = new double[numWeights]; // actually weights & biases
            for (int i = 0; i < numWeights; ++i)
                weights[i] = 20.0 * rnd.NextDouble() - 10.0; // [-10.0 to 10.0]

            Console.WriteLine("Generating weights and biases:");
            ShowVector(weights, 2, 10, true);

            double[][] result = new double[numRows][]; // allocate return-result
            for (int i = 0; i < numRows; ++i)
                result[i] = new double[numInput + numOutput]; // 1-of-N in last column

            Perceptron_e_o_o_s gnn = new Perceptron_e_o_o_s(numInput, numHidden1, numHidden2, numOutput); // generating NN
            gnn.CargarPesosyUmbrales(weights);

            for (int r = 0; r < numRows; ++r) // for each row
            {
                // generate random inputs
                double[] inputs = new double[numInput];
                for (int i = 0; i < numInput; ++i)
                    inputs[i] = 20.0 * rnd.NextDouble() - 10.0; // [-10.0 to -10.0]

                // compute outputs
                double[] outputs = gnn.ProcesarEntradas(inputs);

                // translate outputs to 1-of-N
                double[] oneOfN = new double[numOutput]; // all 0.0

                int maxIndex = 0;
                double maxValue = outputs[0];
                for (int i = 0; i < numOutput; ++i)
                {
                    if (outputs[i] > maxValue)
                    {
                        maxIndex = i;
                        maxValue = outputs[i];
                    }
                }
                oneOfN[maxIndex] = 1.0;

                // place inputs and 1-of-N output values into curr row
                int c = 0; // column into result[][]
                for (int i = 0; i < numInput; ++i) // inputs
                    result[r][c++] = inputs[i];
                for (int i = 0; i < numOutput; ++i) // outputs
                    result[r][c++] = oneOfN[i];
            } // each row
            return result;
        }

        public static void SplitTrainTest(double[][] allData, double trainPct, int seed, out double[][] trainData, out double[][] testData)
        {
            Random rnd = new Random(seed);
            int totRows = allData.Length;
            int numTrainRows = (int)(totRows * trainPct); // usually 0.80
            int numTestRows = totRows - numTrainRows;
            trainData = new double[numTrainRows][];
            testData = new double[numTestRows][];

            double[][] copy = new double[allData.Length][]; // ref copy of data
            for (int i = 0; i < copy.Length; ++i)
                copy[i] = allData[i];

            for (int i = 0; i < copy.Length; ++i) // scramble order
            {
                int r = rnd.Next(i, copy.Length); // use Fisher-Yates
                double[] tmp = copy[r];
                copy[r] = copy[i];
                copy[i] = tmp;
            }
            for (int i = 0; i < numTrainRows; ++i)
                trainData[i] = copy[i];

            for (int i = 0; i < numTestRows; ++i)
                testData[i] = copy[i + numTrainRows];
        }

        #endregion

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            List<string> lineas = new List<string>();
            lineas.Add("\nBegin neural network back-propagation demo");

            backgroundWorker1.ReportProgress(1, lineas);

            int numInput = 4; // number features
            int numHidden = 5;
            int numOutput = 3; // number of classes for Y
            int numRows = 1000;
            int seed = 1; // gives nice demo

            lineas.Add("\nGenerating " + numRows + " artificial data items with " + numInput + " features");
            backgroundWorker1.ReportProgress(1, lineas);
            double[][] allData = BackPropProgram.MakeAllData(numInput, numHidden, numOutput, numRows, seed);
            lineas.Add("Done");

            lineas.Add("\nCreating train (80%) and test (20%) matrices");
            backgroundWorker1.ReportProgress(1, lineas);
            double[][] trainData;
            double[][] testData;
            BackPropProgram.SplitTrainTest(allData, 0.80, seed, out trainData, out testData);
            lineas.Add("Done\n");
            backgroundWorker1.ReportProgress(1, lineas);
            lineas.Add("Training data:");
            List<string> matrix = ShowMatrix(trainData, 4, 2, true);
            lineas.Add("Test data:");
            backgroundWorker1.ReportProgress(1, lineas);
            matrix = ShowMatrix(testData, 4, 2, true);
            foreach (string linea in matrix)
            {
                lineas.Add(linea);
            }
            backgroundWorker1.ReportProgress(1, lineas);
            lineas.Add("Creating a " + numInput + "-" + numHidden + "-" + numOutput + " neural network");
            NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);
            backgroundWorker1.ReportProgress(1, lineas);
            int maxEpochs = 1000;
            double learnRate = 0.05;
            double momentum = 0.01;
            lineas.Add("\nSetting maxEpochs = " + maxEpochs);
            lineas.Add("Setting learnRate = " + learnRate.ToString("F2"));
            lineas.Add("Setting momentum  = " + momentum.ToString("F2"));
            lineas.Add("");
            lineas.Add("\nStarting training");
            backgroundWorker1.ReportProgress(1, lineas);
            double[] weights = nn.Train(trainData, maxEpochs, learnRate, momentum);
            lineas.Add("Done");
            
            lineas.Add("\nFinal neural network model weights and biases:\n");
            lineas.Add(ShowVector(weights, 2, 10, true));
            backgroundWorker1.ReportProgress(1, lineas);
            
            double trainAcc = nn.Accuracy(trainData);
            lineas.Add("\nFinal accuracy on training data = " +
              trainAcc.ToString("F4"));
            backgroundWorker1.ReportProgress(1, lineas);
            
            double testAcc = nn.Accuracy(testData);
            lineas.Add("Final accuracy on test data     = " +
              testAcc.ToString("F4"));
            backgroundWorker1.ReportProgress(1, lineas);
            
            lineas.Add("\nEnd back-propagation demo\n");
            backgroundWorker1.ReportProgress(1, lineas);
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            List<string> lineas = (List<string>)e.UserState;
            textBox1.Text = String.Join(Environment.NewLine, lineas);
        }
    }
}