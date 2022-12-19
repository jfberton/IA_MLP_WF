using System.IO.Pipes;

namespace IA_MLP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            cb_dataset.SelectedIndex = 0;
            cb_topologia.SelectedIndex = 0;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private int neuronas_capa_oculta1 = 0;
        private int neuronas_capa_oculta2 = 0;
        private int epocas = 0;
        private double tasa_de_aprendizaje = 0;
        private double momento = 0;
        private string path_dataset = "";
        private double[] pesos_y_umbrales;
        private double[] errores;
        private string path_entrenamiento;


        private void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;

            bool datos_entrada_ok = true;
            datos_entrada_ok = datos_entrada_ok && int.TryParse(tb_epocas.Text, out epocas);
            datos_entrada_ok = datos_entrada_ok && double.TryParse(tb_tasa_aprendizaje.Text, out tasa_de_aprendizaje);
            datos_entrada_ok = datos_entrada_ok && double.TryParse(tb_momento.Text, out momento);

            switch (cb_dataset.SelectedItem.ToString())
            {
                case "100 valores con 10 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets100validacion10.txt";
                    break;
                case "100 valores con 20 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets100validacion20.txt";
                    break;
                case "100 valores con 30 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets100validacion30.txt";
                    break;
                case "500 valores con 10 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets500validacion10.txt";
                    break;
                case "500 valores con 20 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets500validacion20.txt";
                    break;
                case "500 valores con 30 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets500validacion30.txt";
                    break;
                case "1000 valores con 10 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets1000validacion10.txt";
                    break;
                case "1000 valores con 20 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets1000validacion20.txt";
                    break;
                case "1000 valores con 30 % de prueba":
                    path_dataset = Application.StartupPath + @"Datasets\datasets1000validacion30.txt";
                    break;
                default:
                    break;
            }

            if (datos_entrada_ok)
            {
                switch (cb_topologia.SelectedItem.ToString())
                {
                    case "100 - 05 - 3":
                        neuronas_capa_oculta1 = 5;
                        if (backgroundWorker1.IsBusy != true)
                        {
                            backgroundWorker1.RunWorkerAsync();
                        }

                        break;
                    case "100 - 10 - 3":
                        neuronas_capa_oculta1 = 10;
                        if (backgroundWorker1.IsBusy != true)
                        {
                            backgroundWorker1.RunWorkerAsync();
                        }
                        break;
                    case "100 - 05 - 05 - 3":
                        neuronas_capa_oculta1 = 5;
                        neuronas_capa_oculta2 = 5;
                        if (backgroundWorker2.IsBusy != true)
                        {
                            backgroundWorker2.RunWorkerAsync();
                        }
                        break;
                    case "100 - 10 - 10 - 3":
                        neuronas_capa_oculta1 = 10;
                        neuronas_capa_oculta2 = 10;
                        if (backgroundWorker2.IsBusy != true)
                        {
                            backgroundWorker2.RunWorkerAsync();
                        }
                        break;

                    default:
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("Ocurrio un error en la carga de valores de ejecución, revise los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            tb_resultados.Text = string.Empty;
            path_entrenamiento = Application.StartupPath + string.Format(@"Datasets\pesos_umbrales_100_{0}_3.txt", neuronas_capa_oculta1);

            AgregarMostrarLineasBW1(new List<string>() { " - INICIANDO EL PROCESO DE ENTRENAMIENTO - " });

            AgregarMostrarLineasBW1(new List<string>() {"Cargando los datos del dataset..."});

            bool lectura_datos_correcta = true;

            List<double[]> trainDataList = new List<double[]>();
            List<double[]> testDataList = new List<double[]>();

            try
            {
                string data = System.IO.File.ReadAllText(path_dataset).Replace("\r", "");
                string[] rows = data.Split(Environment.NewLine.ToCharArray());


                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i] != "")
                    {
                        string[] rowData = rows[i].Split(';');

                        switch (rowData[0])
                        {
                            case "0": //train
                                trainDataList.Add(new double[rowData.Length - 1]);
                                for (int j = 1; j < rowData.Length; j++)
                                {
                                    trainDataList[trainDataList.Count - 1][j - 1] = double.Parse(rowData[j]);
                                }
                                break;

                            case "1"://test
                                testDataList.Add(new double[rowData.Length - 1]);
                                for (int j = 1; j < rowData.Length; j++)
                                {
                                    testDataList[testDataList.Count - 1][j - 1] = double.Parse(rowData[j]);
                                }
                                break;

                            default:
                                throw new Exception("Error en el formato del dataset");
                        }
                    }
                }
            }
            catch
            {
                lectura_datos_correcta = false;
            }

            double[][] trainData = trainDataList.ToArray();
            double[][] testData = testDataList.ToArray();

            if (lectura_datos_correcta)
            {
                AgregarMostrarLineasBW1(new List<string>() { "Datos cargados correctamente." });

                AgregarMostrarLineasBW1(new List<string>() { "Datos de entrenamiento:" });
                AgregarMostrarLineasBW1(ShowMatrix(trainData, 4, 0, true));
                AgregarMostrarLineasBW1(new List<string>() { "Datos de prueba:" });
                AgregarMostrarLineasBW1(ShowMatrix(testData, 4, 0, true));

                AgregarMostrarLineasBW1(new List<string>() { String.Format("Creando un Perceptron multicapa de 3 capas (100-{0}-3)", neuronas_capa_oculta1) });
                Perceptron_e_o_s nn = new Perceptron_e_o_s(100, neuronas_capa_oculta1, 3);
                nn.InformarErrorEpoca += HandleErrorEpocaBW1;

                AgregarMostrarLineasBW1(new List<string>() { string.Format("Máximo de epocas = {0}", epocas) });
                AgregarMostrarLineasBW1(new List<string>() { string.Format("Tasa de aprendizaje = {0}", tasa_de_aprendizaje.ToString("F2")) });
                AgregarMostrarLineasBW1(new List<string>() { string.Format("Momento = {0}", momento.ToString("F2")) });

                AgregarMostrarLineasBW1(new List<string>() { "\nComenzando el entrenamiento..." });
                pesos_y_umbrales = nn.Entrenar(trainData, epocas, tasa_de_aprendizaje, momento);
                errores = nn.errores;

                AgregarMostrarLineasBW1(new List<string>() { "Terminado!" });
                AgregarMostrarLineasBW1(new List<string>() { "Pesos y umbrales finales de la red:" });
                AgregarMostrarLineasBW1(new List<string>() { ShowVector(pesos_y_umbrales, 4, 10, true) });
                
                double error = nn.ultimo_error;
                AgregarMostrarLineasBW1(new List<string>() { string.Format("Error final sobre datos de entrenamiento = {0}", error.ToString("%#0.00")) });
                
                double trainAcc = nn.Precision(trainData);
                AgregarMostrarLineasBW1(new List<string>() { string.Format("Precisión final sobre datos de entrenamiento = {0}", trainAcc.ToString("%#0.00")) });

                double testAcc = nn.Precision(testData);
                AgregarMostrarLineasBW1(new List<string>() { string.Format("Precisión final sobre datos de prueba = {0}", testAcc.ToString("%#0.00")) });

                AgregarMostrarLineasBW1(new List<string>() { "Final del programa de entrenamiento" }, true);

                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                AgregarMostrarLineasBW1(new List<string>() { "Error al cargar los datos." });
            }
            
        }

        private void AgregarMostrarLineasBW1(List<string> lineas, bool termino = false)
        {
            List<string> lineas_a_mostrar = tb_resultados.Text.Split("\n").ToList();
            lineas_a_mostrar.AddRange(lineas);

            tb_resultados.Text = String.Join(Environment.NewLine, lineas_a_mostrar);

            backgroundWorker1.ReportProgress(1);
            if (termino)
                MessageBox.Show("Entrenamiento finalizado");
        }

        private void HandleErrorEpocaBW1(object? sender, Perceptron_e_o_s.ErrorEpoca e)
        {
            List<string> errorepoca = new List<string>() { string.Format("Epoca: {0}, Error: {1}", e.Epoca, e.Error) };
            AgregarMostrarLineasBW1(errorepoca);
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            tb_resultados.Text = string.Empty;
            string path_entrenamiento = Application.StartupPath + String.Format(@"Datasets\pesos_umbrales_100_{0}_{1}_3.txt", neuronas_capa_oculta1, neuronas_capa_oculta2);

            AgregarMostrarLineasBW2(new List<string>() { " - INICIANDO EL PROCESO DE ENTRENAMIENTO - " });

            AgregarMostrarLineasBW2(new List<string>() { "Cargando los datos del dataset..." });

            bool lectura_datos_correcta = true;
            
            List<double[]> trainDataList = new List<double[]>();
            List<double[]> testDataList = new List<double[]>();
            try
            {
                string data = System.IO.File.ReadAllText(path_dataset).Replace("\r", "");
                string[] rows = data.Split(Environment.NewLine.ToCharArray());

                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i] != "")
                    {
                        string[] rowData = rows[i].Split(';');
                        switch (rowData[0])
                        {
                            case "0"://train
                                trainDataList.Add(new double[rowData.Length - 1]);
                                for (int j = 1; j < rowData.Length; j++)
                                {
                                    trainDataList[trainDataList.Count - 1][j - 1] = double.Parse(rowData[j]);
                                }
                                break;
                            case "1"://test
                                testDataList.Add(new double[rowData.Length - 1]);
                                for (int j = 1; j < rowData.Length; j++)
                                {
                                    testDataList[testDataList.Count - 1][j - 1] = double.Parse(rowData[j]);
                                }
                                break;
                            default:
                                throw new Exception("Error en el formato del dataset");
                        }
                    }
                }
            }
            catch
            {
                lectura_datos_correcta = false;
            }

            if (lectura_datos_correcta)
            {
                AgregarMostrarLineasBW2(new List<string>() { "Datos cargados correctamente." });

                double[][] trainData = trainDataList.ToArray();
                double[][] testData = testDataList.ToArray();

                AgregarMostrarLineasBW2(new List<string>() { "Listo!" });

                AgregarMostrarLineasBW2(new List<string>() { "Datos de entrenamiento:" });
                AgregarMostrarLineasBW2(ShowMatrix(trainData, 4, 0, true));
                AgregarMostrarLineasBW2(new List<string>() { "Datos de prueba:" });
                AgregarMostrarLineasBW2(ShowMatrix(testData, 4, 0, true));

                AgregarMostrarLineasBW2(new List<string>() { String.Format("Creando un Perceptron multicapa de 4 capas (100-{0}-{1}-2)", neuronas_capa_oculta1, neuronas_capa_oculta2) });
                Perceptron_e_o_o_s nn = new Perceptron_e_o_o_s(100, neuronas_capa_oculta1, neuronas_capa_oculta2, 3);
                nn.InformarErrorEpoca += HandleErrorEpocaBW2;

                AgregarMostrarLineasBW2(new List<string>() { string.Format("Máximo de epocas = {0}", epocas) });
                AgregarMostrarLineasBW2(new List<string>() { string.Format("Tasa de aprendizaje = {0}", tasa_de_aprendizaje.ToString("F2")) });
                AgregarMostrarLineasBW2(new List<string>() { string.Format("Momento = {0}", momento.ToString("F2")) });

                AgregarMostrarLineasBW2(new List<string>() { "\nComenzando el entrenamiento..." });
                pesos_y_umbrales = nn.Entrenar(trainData, epocas, tasa_de_aprendizaje, momento);
                errores = nn.errores;

                AgregarMostrarLineasBW2(new List<string>() { "Terminado!" });
                AgregarMostrarLineasBW2(new List<string>() { "Pesos y umbrales finales de la red:" });
                AgregarMostrarLineasBW2(new List<string>() { ShowVector(pesos_y_umbrales, 4, 10, true) });

                double error = nn.ultimo_error;
                AgregarMostrarLineasBW2(new List<string>() { string.Format("Error final sobre datos de entrenamiento = {0}", error.ToString("%#0.00")) });

                double trainAcc = nn.Precision(trainData);
                AgregarMostrarLineasBW2(new List<string>() { string.Format("Precisión final sobre datos de entrenamiento = {0}", trainAcc.ToString("%#0.00")) });

                double testAcc = nn.Precision(testData);
                AgregarMostrarLineasBW2(new List<string>() { string.Format("Precisión final sobre datos de prueba = {0}", testAcc.ToString("%#0.00")) });

                AgregarMostrarLineasBW2(new List<string>() { "Final del programa de entrenamiento" }, true);

                button3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                AgregarMostrarLineasBW2(new List<string>() { "Error al cargar los datos del dataset" });
            }
        }
    
        private void AgregarMostrarLineasBW2(List<string> lineas, bool termino = false)
        {
            List<string> lineas_a_mostrar = tb_resultados.Text.Split("\n").ToList();
            lineas_a_mostrar.AddRange(lineas);
            
            tb_resultados.Text = String.Join(Environment.NewLine, lineas_a_mostrar);
            
            backgroundWorker2.ReportProgress(1);
            if(termino)
                MessageBox.Show("Entrenamiento finalizado");
        }

        private void HandleErrorEpocaBW2(object? sender, Perceptron_e_o_o_s.ErrorEpoca e)
        {
            List<string> errorepoca = new List<string>() { string.Format("Epoca: {0}, Error: {1}", e.Epoca, e.Error) };
            AgregarMostrarLineasBW2(errorepoca);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllLines(path_entrenamiento, pesos_y_umbrales.Select(d => d.ToString()));
            MessageBox.Show("Se guardaron los pesos y umbrales en el archivo \r" + path_entrenamiento, "Exito!", MessageBoxButtons.OK);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form formulario in this.MdiChildren)
            {
                if (formulario.Text == "Evolución del error")
                {
                    ((Grafico_Evolucion)formulario).ValoresError = errores;
                    ((Grafico_Evolucion)formulario).ActualizarGrafico();
                    formulario.Activate();
                    return;
                }
            }

            Grafico_Evolucion childForm = new Grafico_Evolucion();
            childForm.ValoresError = errores;
            childForm.ActualizarGrafico();
            childForm.MdiParent = this.MdiParent;
            childForm.Text = "Evolución del error";
            childForm.Show();
        }
    }
}