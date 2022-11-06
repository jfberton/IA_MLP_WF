using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_MLP
{
    public class Perceptron_e_o_s
    {
        private int neuronas_c_entrada;
        private int neuronas_c_oculta;
        private int neuronas_c_salida;

        private double[] entradas;

        private double[][] pesos_c_entrada_a_c_oculta;
        private double[] umbrales_c_oculta;
        private double[] salidas_c_oculta;

        private double[][] pesos_c_oculta_a_c_salida;
        private double[] umbrales_c_salida;
        private double[] salidas_c_salida;

        public event EventHandler<ErrorEpoca> InformarErrorEpoca;
        public struct ErrorEpoca
        {
            public int Epoca { get; }
            public double Error { get; }

            public ErrorEpoca(int epoca, double error)
            {
                this.Epoca = epoca;
                this.Error = error;
            }
        }

        private Random rnd;

        public Perceptron_e_o_s(int neuronas_capa_entrada, int neuronas_capa_oculta, int neuronas_capa_salida)
        {
            this.neuronas_c_entrada = neuronas_capa_entrada;
            this.neuronas_c_oculta = neuronas_capa_oculta;
            this.neuronas_c_salida = neuronas_capa_salida;

            this.entradas = new double[neuronas_c_entrada];

            this.pesos_c_entrada_a_c_oculta = SetearValores(neuronas_c_entrada, neuronas_capa_oculta, 0.0);
            this.umbrales_c_oculta = new double[neuronas_c_oculta];
            this.salidas_c_oculta = new double[neuronas_c_oculta];

            this.pesos_c_oculta_a_c_salida = SetearValores(neuronas_c_oculta, neuronas_c_salida, 0.0);
            this.umbrales_c_salida = new double[neuronas_c_salida];
            this.salidas_c_salida = new double[neuronas_c_salida];

            this.rnd = new Random(0);
            this.RandomizarValoresDePesosyUmbrales();
        }

        private static double[][] SetearValores(int rows, int cols, double valor)
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = valor;
            return result;
        }

        private void RandomizarValoresDePesosyUmbrales() // helper for ctor
        {
            // initialize weights and biases to small random values
            int tamaño_ventor_pesos = (neuronas_c_entrada * neuronas_c_oculta) 
                + neuronas_c_oculta + (neuronas_c_oculta * neuronas_c_salida) 
                + neuronas_c_salida;
            
            double[] vector_pesos_umbrales = new double[tamaño_ventor_pesos];
            
            for (int i = 0; i < vector_pesos_umbrales.Length; ++i)
                vector_pesos_umbrales[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            
            this.CargarPesosyUmbrales(vector_pesos_umbrales);
        }

        /// <summary>
        /// Carga los pesos y umbrales desde un arreglo de doubles
        /// </summary>
        public void CargarPesosyUmbrales(double[] vector_pesos_umbrales)
        {
            //Carga los pesos y umbrales en las conexiones entre las neuronas de la red
            int tamaño_ventor_pesos_esperado = (neuronas_c_entrada * neuronas_c_oculta) + neuronas_c_oculta + (neuronas_c_oculta * neuronas_c_salida) + neuronas_c_salida;
            if (vector_pesos_umbrales.Length != tamaño_ventor_pesos_esperado)
                throw new Exception("El tamaño del vector de pesos y umbrales no coincide con la topología de la red");

            int k = 0; // points into weights param

            for (int i = 0; i < neuronas_c_entrada; ++i)
            {
                for (int j = 0; j < neuronas_c_oculta; ++j)
                {
                    pesos_c_entrada_a_c_oculta[i][j] = vector_pesos_umbrales[k];
                    k++;
                }
            }

            for (int i = 0; i < neuronas_c_oculta; ++i)
            {
                umbrales_c_oculta[i] = vector_pesos_umbrales[k];
                k++;
            }

            for (int i = 0; i < neuronas_c_oculta; ++i)
            {
                for (int j = 0; j < neuronas_c_salida; ++j)
                {
                    pesos_c_oculta_a_c_salida[i][j] = vector_pesos_umbrales[k];
                    k++;
                }
            }

            for (int i = 0; i < neuronas_c_salida; ++i)
            {
                umbrales_c_salida[i] = vector_pesos_umbrales[k];
                k++;
            }
        }

        /// <summary>
        /// Obtiene un vector que representa el estado de los pesos y umbrales de la red
        /// </summary>
        /// <returns></returns>
        public double[] ObtenerPesosUmbrales()
        {
            int tamaño_ventor_pesos = (neuronas_c_entrada * neuronas_c_oculta) + neuronas_c_oculta + (neuronas_c_oculta * neuronas_c_salida) + neuronas_c_salida;
            double[] result = new double[tamaño_ventor_pesos];

            int k = 0;
            for (int i = 0; i < pesos_c_entrada_a_c_oculta.Length; ++i)
            {
                for (int j = 0; j < pesos_c_entrada_a_c_oculta[0].Length; ++j)
                {
                    result[k] = pesos_c_entrada_a_c_oculta[i][j]; k++;
                }
            }

            for (int i = 0; i < umbrales_c_oculta.Length; ++i)
            {
                result[k] = umbrales_c_oculta[i]; k++;
            }

            for (int i = 0; i < pesos_c_oculta_a_c_salida.Length; ++i)
            {
                for (int j = 0; j < pesos_c_oculta_a_c_salida[0].Length; ++j)
                {
                    result[k] = pesos_c_oculta_a_c_salida[i][j]; k++;
                }
            }

            for (int i = 0; i < umbrales_c_salida.Length; ++i)
            {
                result[k] = umbrales_c_salida[i]; k++;
            }

            return result;
        }

        public double[] ProcesarEntradas(double[] entradas)
        {
            if (entradas.Length != neuronas_c_entrada)
                throw new Exception("El tamaño del vector de entradas no coincide con la cantidad de neuronas de la capa de ingreso a la red");

            double[] salida_capa_oculta = new double[neuronas_c_oculta];
            double[] salida_capa_salida = new double[neuronas_c_salida];

            for (int j = 0; j < neuronas_c_oculta; ++j) //trabajo sobre las entradas a la capa oculta
                for (int i = 0; i < neuronas_c_entrada; ++i)
                    salida_capa_oculta[j] += entradas[i] * this.pesos_c_entrada_a_c_oculta[i][j];

            for (int i = 0; i < neuronas_c_oculta; ++i) //agrego los umbrales a la sumatoria de entradas * pesos
                salida_capa_oculta[i] += umbrales_c_oculta[i];

            for (int i = 0; i < neuronas_c_oculta; ++i)
                this.salidas_c_oculta[i] = FuncionDeActivacion(salida_capa_oculta[i]);

            for (int j = 0; j < neuronas_c_salida; ++j)  //acarreo las salidas de la capa oculta hacia la capa de salida
                for (int i = 0; i < neuronas_c_oculta; ++i)
                    salida_capa_salida[j] += salidas_c_oculta[i] * pesos_c_oculta_a_c_salida[i][j];

            for (int i = 0; i < neuronas_c_salida; ++i)  //agrego los umbrales a la sumatoria de entradas * pesos
                salida_capa_salida[i] += umbrales_c_salida[i];

            double[] salida_final_capa_salida = Sigmoide(salida_capa_salida); //Aplico un afuncion diferente a la de la capa oculta, esto vimos que mejora los resultados
            Array.Copy(salida_final_capa_salida, salidas_c_salida, salida_final_capa_salida.Length);

            double[] retResult = new double[neuronas_c_salida];
            Array.Copy(salidas_c_salida, retResult, retResult.Length);
            return retResult;
        }

        /// <summary>
        /// Funcion de transferencia lineal f(x) = x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double FuncionDeActivacion(double x)
        {
            return x;
        }

        private static double DerivadaFuncionActivacion(double x)
        {
            return 1;
        }

        private static double[] Sigmoide(double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int i = 0; i < vector.Length; ++i)
                result[i] = 1.0 / (1.0 + Math.Exp(-vector[i]));

            return result;
        }

        public static double Derivada_sigmoide(double x)
        {
            double y = 1.0 / (1.0 + Math.Exp(-x)); //calculo el sigmoide de cada valor
            return y * (1 - y); //la derivada del sigmoide se calcula en funcion de si misma
        }


        public double[] Entrenar(double[][] datos_entrenamiento, int corridas_maximas, double tasa_de_aprendizaje, double momento)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] gradientes_pesos_oculta_salida = SetearValores(neuronas_c_oculta, neuronas_c_salida, 0.0);
            double[] gradientes_umbrales_salida = new double[neuronas_c_salida];

            double[][] gradientes_pesos_entrada_oculta = SetearValores(neuronas_c_entrada, neuronas_c_oculta, 0.0);
            double[] gradientes_umbrales_oculta = new double[neuronas_c_oculta];

            double[] signo_gradiente_salida = new double[neuronas_c_salida];
            double[] signo_gradiente_capa_oculta = new double[neuronas_c_oculta];

            // back-prop momentum specific arrays 
            double[][] delta_valores_previos_pesos_entrada_oculta = SetearValores(neuronas_c_entrada, neuronas_c_oculta, 0.0);
            double[] delta_valores_previos_umbrales_oculta = new double[neuronas_c_oculta];
            double[][] delta_valores_previos_pesos_oculta_salida = SetearValores(neuronas_c_oculta, neuronas_c_salida, 0.0);
            double[] delta_valores_previos_umbrales_salida = new double[neuronas_c_salida];

            int corridas = 0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados_salida = new double[neuronas_c_salida];
            double derivada = 0.0;
            double signo_error = 0.0;

            //establecimos un vector con los indices para mezclarlos y poder correr los entrenamientos de manera aleatoria sobre los datos proporcionados
            int[] secuencia = new int[datos_entrenamiento.Length];
            for (int i = 0; i < secuencia.Length; ++i)
                secuencia[i] = i;

            int errInterval = corridas_maximas / 10; //partimos las corridas maximas en 10 para ir comprobando el error
            while (corridas < corridas_maximas)
            {
                ++corridas;

                if (corridas % errInterval == 0 && corridas < corridas_maximas)
                {
                    double error = Error(datos_entrenamiento);

                    ErrorEpoca ee = new ErrorEpoca(corridas, error);
                    InformarErrorEpoca?.Invoke(this, ee);
                }

                MezclarDatos(secuencia);
                for (int ii = 0; ii < datos_entrenamiento.Length; ++ii)
                {
                    int idx = secuencia[ii];
                    Array.Copy(datos_entrenamiento[idx], valores_entrada, neuronas_c_entrada);
                    Array.Copy(datos_entrenamiento[idx], neuronas_c_entrada, valores_esperados_salida, 0, neuronas_c_salida);
                    ProcesarEntradas(valores_entrada);

                    // indices: i = inputs, j = hiddens, k = outputs

                    // 1. Obtener el signo del error en la salida
                    for (int k = 0; k < neuronas_c_salida; ++k)
                    {
                        signo_error = valores_esperados_salida[k] - salidas_c_salida[k];
                        derivada = Derivada_sigmoide(salidas_c_salida[k]);
                        signo_gradiente_salida[k] = signo_error * derivada;
                    }

                    // 2. Obtener los gradientes de los pesos de las conexiones capa oculta - capa salida utilizando
                    //    los signos del gradiente a la salida
                    for (int j = 0; j < neuronas_c_oculta; ++j)
                        for (int k = 0; k < neuronas_c_salida; ++k)
                            gradientes_pesos_oculta_salida[j][k] = signo_gradiente_salida[k] * salidas_c_oculta[j];

                    // 2b. Obtener los gradientes de los umbrales de las neuronas de la capa salida utilizando
                    //    los signos del gradiente a la salida
                    for (int k = 0; k < neuronas_c_salida; ++k)
                        gradientes_umbrales_salida[k] = signo_gradiente_salida[k] * 1.0;
                    /*--------------------------------------*/
                    // 3. Obtener los signos de error de los nodos de la capa oculta
                    for (int j = 0; j < neuronas_c_oculta; ++j)
                    {
                        derivada = DerivadaFuncionActivacion(salidas_c_oculta[j]); //<- 1 es porque la funcion de transferencia lineal derivada es 1;
                        // por cada neurona de la capa oculta necesito obtener los pesos de las conexiones con la capa de salida
                        // y multiplicarlos por el signo del error de la capa de salida
                        double sum = 0.0;
                        for (int k = 0; k < neuronas_c_salida; ++k)
                        {
                            sum += signo_gradiente_salida[k] * pesos_c_oculta_a_c_salida[j][k];
                        }
                        signo_gradiente_capa_oculta[j] = derivada * sum;
                    }

                    // 4. Obtener el valor del gradiente de los pesos entre la capa de ingreso y la oculta
                    for (int i = 0; i < neuronas_c_entrada; ++i)
                        for (int j = 0; j < neuronas_c_oculta; ++j)
                            gradientes_pesos_entrada_oculta[i][j] = signo_gradiente_capa_oculta[j] * entradas[i];

                    // 4b. Obtener el gradiente de los umbrales de la capa oculta
                    for (int j = 0; j < neuronas_c_oculta; ++j)
                        gradientes_umbrales_oculta[j] = signo_gradiente_capa_oculta[j] * 1.0;

                    // == Con estos valores actualizo los pesos y umbrales de las conexiones de la red ==

                    // Actualizo los pesos entre las conexiones de la capa de ingreso y la oculta
                    for (int i = 0; i < neuronas_c_entrada; ++i)
                    {
                        for (int j = 0; j < neuronas_c_oculta; ++j)
                        {
                            double delta = gradientes_pesos_entrada_oculta[i][j] * tasa_de_aprendizaje;
                            pesos_c_entrada_a_c_oculta[i][j] += delta;
                            pesos_c_entrada_a_c_oculta[i][j] += delta_valores_previos_pesos_entrada_oculta[i][j] * momento;
                            delta_valores_previos_pesos_entrada_oculta[i][j] = delta;
                        }
                    }

                    // Actualizo los umbrales de las neuronas de la capa oculta
                    for (int j = 0; j < neuronas_c_oculta; ++j)
                    {
                        double delta = gradientes_umbrales_oculta[j] * tasa_de_aprendizaje;
                        umbrales_c_oculta[j] += delta;
                        umbrales_c_oculta[j] += delta_valores_previos_umbrales_oculta[j] * momento;
                        delta_valores_previos_umbrales_oculta[j] = delta;
                    }

                    // Actualizo los pesos de las conexiones entre la capa oculta y la de salida
                    for (int j = 0; j < neuronas_c_oculta; ++j)
                    {
                        for (int k = 0; k < neuronas_c_salida; ++k)
                        {
                            double delta = gradientes_pesos_oculta_salida[j][k] * tasa_de_aprendizaje;
                            pesos_c_oculta_a_c_salida[j][k] += delta;
                            pesos_c_oculta_a_c_salida[j][k] += delta_valores_previos_pesos_oculta_salida[j][k] * momento;
                            delta_valores_previos_pesos_oculta_salida[j][k] = delta;
                        }
                    }

                    // Actualizo los pesos de los umbrales de las neuronas de la capa de salida
                    for (int k = 0; k < neuronas_c_salida; ++k)
                    {
                        double delta = gradientes_umbrales_salida[k] * tasa_de_aprendizaje;
                        umbrales_c_salida[k] += delta;
                        umbrales_c_salida[k] += delta_valores_previos_umbrales_salida[k] * momento;
                        delta_valores_previos_umbrales_salida[k] = delta;
                    }

                } // por cada item de entrenamiento

            } // fin del mientras

            double[] mejores_pesos_encontrados = ObtenerPesosUmbrales();
            return mejores_pesos_encontrados;
        } // fin de entrenamiento

        private void MezclarDatos(int[] datos_entrenamiento)
        {
            for (int i = 0; i < datos_entrenamiento.Length; ++i)
            {
                int r = this.rnd.Next(i, datos_entrenamiento.Length);
                int tmp = datos_entrenamiento[r];
                datos_entrenamiento[r] = datos_entrenamiento[i];
                datos_entrenamiento[i] = tmp;
            }
        }

        private double Error(double[][] datos_entrenamiento)
        {
            double suma_de_errores_al_cuadrado = 0.0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados = new double[neuronas_c_salida];

            for (int i = 0; i < datos_entrenamiento.Length; ++i)
            {
                Array.Copy(datos_entrenamiento[i], valores_entrada, neuronas_c_entrada);
                Array.Copy(datos_entrenamiento[i], neuronas_c_entrada, valores_esperados, 0, neuronas_c_salida);
                double[] yValues = this.ProcesarEntradas(valores_entrada);
                for (int j = 0; j < neuronas_c_salida; ++j)
                {
                    double err = valores_esperados[j] - yValues[j];
                    suma_de_errores_al_cuadrado += err * err;
                }
            }
            return suma_de_errores_al_cuadrado / datos_entrenamiento.Length;
        }

        public double Precision(double[][] datos_de_prueba)
        {
            // percentage correct using winner-takes all
            int correctos = 0;
            int incorrectos = 0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados = new double[neuronas_c_salida];
            double[] valores_obtenidos;

            for (int i = 0; i < datos_de_prueba.Length; ++i)
            {
                Array.Copy(datos_de_prueba[i], valores_entrada, neuronas_c_entrada);
                Array.Copy(datos_de_prueba[i], neuronas_c_entrada, valores_esperados, 0, neuronas_c_salida);
                valores_obtenidos = this.ProcesarEntradas(valores_entrada);

                int indice1_resultado_esperado = Indice_del_valor_1(valores_esperados);
                int indice1_resultado_obtenido = Indice_del_valor_1(valores_obtenidos);

                if (indice1_resultado_esperado == indice1_resultado_obtenido)
                    ++correctos;
                else
                    ++incorrectos;

            }
            return (correctos * 1.0) / (correctos + incorrectos);
        }

        public static int Indice_del_valor_1(double[] vector)
        {
            //tomo el vector de resultado obtenido y
            //para compararlo con el vector de resultado esperado
            //busco el maximo entre los resultados y a este indice le asigno el valor 1
            //ya que los valores posibles de resultado son:
            // 0 0 1 => b
            // 0 1 0 => d
            // 1 0 0 => f

            int indice = 0;
            double valor_maximo = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > valor_maximo)
                {
                    valor_maximo = vector[i];
                    indice = i;
                }
            }
            return indice;
        }

    }

    public class Perceptron_e_o_o_s
    {
        private int neuronas_c_entrada;
        private int neuronas_c_oculta_0;
        private int neuronas_c_oculta_1;
        private int neuronas_c_salida;

        private double[] entradas;

        private double[][] pesos_c_entrada_a_c_oculta_0;
        private double[] umbrales_c_oculta_0;
        private double[] salidas_c_oculta_0;

        private double[][] pesos_c_oculta_0_a_c_oculta_1;
        private double[] umbrales_c_oculta_1;
        private double[] salidas_c_oculta_1;


        private double[][] pesos_c_oculta_1_a_c_salida;
        private double[] umbrales_c_salida;
        private double[] salidas_c_salida;

        private Random rnd;

        public event EventHandler<ErrorEpoca> InformarErrorEpoca;
        public struct ErrorEpoca
        {
            public int Epoca { get; }
            public double Error { get; }

            public ErrorEpoca(int epoca, double error)
            {
                this.Epoca = epoca;
                this.Error = error;
            }
        }

        public Perceptron_e_o_o_s(int neuronas_capa_entrada, int neuronas_capa_oculta_0, int neuronas_capa_oculta_1, int neuronas_capa_salida)
        {
            this.neuronas_c_entrada = neuronas_capa_entrada;
            this.neuronas_c_oculta_0 = neuronas_capa_oculta_0;
            this.neuronas_c_oculta_1 = neuronas_capa_oculta_1;
            this.neuronas_c_salida = neuronas_capa_salida;

            this.entradas = new double[neuronas_c_entrada];

            this.pesos_c_entrada_a_c_oculta_0 = SetearValores(neuronas_capa_entrada, neuronas_capa_oculta_0, 0.0);
            this.umbrales_c_oculta_0 = new double[neuronas_c_oculta_0];
            this.salidas_c_oculta_0 = new double[neuronas_c_oculta_0];

            this.pesos_c_oculta_0_a_c_oculta_1 = SetearValores(neuronas_capa_oculta_0, neuronas_capa_oculta_1, 0.0);
            this.umbrales_c_oculta_1 = new double[neuronas_capa_oculta_1];
            this.salidas_c_oculta_1 = new double[neuronas_capa_oculta_1];

            this.pesos_c_oculta_1_a_c_salida = SetearValores(neuronas_capa_oculta_1, neuronas_c_salida, 0.0);
            this.umbrales_c_salida = new double[neuronas_c_salida];
            this.salidas_c_salida = new double[neuronas_c_salida];

            this.rnd = new Random(0);
            this.RandomizarValoresDePesosyUmbrales();
        }

        private static double[][] SetearValores(int rows, int cols, double v)
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = v;
            return result;
        }

        private int obtener_tamaño_vector_pesos_y_umbrales()
        {
            int tamaño_ventor_pesos = (neuronas_c_entrada * neuronas_c_oculta_0) + neuronas_c_oculta_0
                                    + (neuronas_c_oculta_0 * neuronas_c_oculta_1) + neuronas_c_oculta_1
                                    + (neuronas_c_oculta_1 * neuronas_c_salida) + neuronas_c_salida;

            return tamaño_ventor_pesos;
        }

        private void RandomizarValoresDePesosyUmbrales() // helper for ctor
        {
            double[] vector_pesos_umbrales = new double[obtener_tamaño_vector_pesos_y_umbrales()];

            for (int i = 0; i < vector_pesos_umbrales.Length; ++i)
                vector_pesos_umbrales[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            this.CargarPesosyUmbrales(vector_pesos_umbrales);
        }

        /// <summary>
        /// Carga los pesos y umbrales desde un arreglo de doubles
        /// </summary>
        public void CargarPesosyUmbrales(double[] vector_pesos_umbrales)
        {
            //Carga los pesos y umbrales en las conexiones entre las neuronas de la red
            if (vector_pesos_umbrales.Length != obtener_tamaño_vector_pesos_y_umbrales())
                throw new Exception("El tamaño del vector de pesos y umbrales no coincide con la topología de la red");

            int k = 0; // points into weights param

            for (int i = 0; i < neuronas_c_entrada; ++i)
            {
                for (int j = 0; j < neuronas_c_oculta_0; ++j)
                {
                    pesos_c_entrada_a_c_oculta_0[i][j] = vector_pesos_umbrales[k];
                    k++;
                }
            }

            for (int i = 0; i < neuronas_c_oculta_0; ++i)
            {
                umbrales_c_oculta_0[i] = vector_pesos_umbrales[k];
                k++;
            }

            for (int i = 0; i < neuronas_c_oculta_0; ++i)
            {
                for (int j = 0; j < neuronas_c_oculta_1; ++j)
                {
                    pesos_c_oculta_0_a_c_oculta_1[i][j] = vector_pesos_umbrales[k];
                    k++;
                }
            }

            for (int i = 0; i < neuronas_c_oculta_1; ++i)
            {
                umbrales_c_oculta_1[i] = vector_pesos_umbrales[k];
                k++;
            }

            for (int i = 0; i < neuronas_c_oculta_1; ++i)
            {
                for (int j = 0; j < neuronas_c_salida; ++j)
                {
                    pesos_c_oculta_1_a_c_salida[i][j] = vector_pesos_umbrales[k];
                    k++;
                }
            }

            for (int i = 0; i < neuronas_c_salida; ++i)
            {
                umbrales_c_salida[i] = vector_pesos_umbrales[k];
                k++;
            }
        }

        /// <summary>
        /// Obtiene un vector que representa el estado de los pesos y umbrales de la red
        /// </summary>
        /// <returns></returns>
        public double[] ObtenerPesosUmbrales()
        {
            double[] result = new double[obtener_tamaño_vector_pesos_y_umbrales()];

            int k = 0;
            for (int i = 0; i < pesos_c_entrada_a_c_oculta_0.Length; ++i)
            {
                for (int j = 0; j < pesos_c_entrada_a_c_oculta_0[0].Length; ++j)
                {
                    result[k] = pesos_c_entrada_a_c_oculta_0[i][j]; k++;
                }
            }

            for (int i = 0; i < umbrales_c_oculta_0.Length; ++i)
            {
                result[k] = umbrales_c_oculta_0[i]; k++;
            }

            for (int i = 0; i < pesos_c_oculta_0_a_c_oculta_1.Length; ++i)
            {
                for (int j = 0; j < pesos_c_oculta_0_a_c_oculta_1[0].Length; ++j)
                {
                    result[k] = pesos_c_oculta_0_a_c_oculta_1[i][j]; k++;
                }
            }

            for (int i = 0; i < umbrales_c_oculta_1.Length; ++i)
            {
                result[k] = umbrales_c_oculta_1[i]; k++;
            }

            for (int i = 0; i < pesos_c_oculta_1_a_c_salida.Length; ++i)
            {
                for (int j = 0; j < pesos_c_oculta_1_a_c_salida[0].Length; ++j)
                {
                    result[k] = pesos_c_oculta_1_a_c_salida[i][j]; k++;
                }
            }

            for (int i = 0; i < umbrales_c_salida.Length; ++i)
            {
                result[k] = umbrales_c_salida[i]; k++;
            }

            return result;
        }

        public double[] ProcesarEntradas(double[] entradas)
        {
            if (entradas.Length != neuronas_c_entrada)
                throw new Exception("El tamaño del vector de entradas no coincide con la cantidad de neuronas de la capa de ingreso a la red");

            double[] salida_capa_oculta_0 = new double[neuronas_c_oculta_0];
            double[] salida_capa_oculta_1 = new double[neuronas_c_oculta_1];
            double[] salida_capa_salida = new double[neuronas_c_salida];

            for (int j = 0; j < neuronas_c_oculta_0; ++j) //trabajo sobre las entradas a la capa oculta 0
                for (int i = 0; i < neuronas_c_entrada; ++i)
                    salida_capa_oculta_0[j] += entradas[i] * this.pesos_c_entrada_a_c_oculta_0[i][j];

            for (int i = 0; i < neuronas_c_oculta_0; ++i) //agrego los umbrales a la sumatoria de entradas * pesos
                salida_capa_oculta_0[i] += umbrales_c_oculta_0[i];

            for (int i = 0; i < neuronas_c_oculta_0; ++i)
                this.salidas_c_oculta_0[i] = FuncionDeActivacion(salida_capa_oculta_0[i]);


            for (int j = 0; j < neuronas_c_oculta_1; ++j) //trabajo sobre las salidas de capa oculta 0 a la capa oculta 1
                for (int i = 0; i < neuronas_c_oculta_0; ++i)
                    salida_capa_oculta_1[j] += salidas_c_oculta_0[i] * this.pesos_c_oculta_0_a_c_oculta_1[i][j];

            for (int i = 0; i < neuronas_c_oculta_1; ++i) //agrego los umbrales a la sumatoria de entradas * pesos
                salida_capa_oculta_0[i] += umbrales_c_oculta_1[i] * 1.0;

            for (int i = 0; i < neuronas_c_oculta_0; ++i)
                this.salidas_c_oculta_1[i] = FuncionDeActivacion(salida_capa_oculta_1[i]);

            for (int j = 0; j < neuronas_c_salida; ++j)  //acarreo las salidas de la capa oculta 1 hacia la capa de salida
                for (int i = 0; i < neuronas_c_oculta_1; ++i)
                    salida_capa_salida[j] += salidas_c_oculta_1[i] * pesos_c_oculta_1_a_c_salida[i][j];

            for (int i = 0; i < neuronas_c_salida; ++i)  //agrego los umbrales a la sumatoria de entradas * pesos
                salida_capa_salida[i] += umbrales_c_salida[i];

            double[] softOut = Sigmoide(salida_capa_salida); //Aplico un afuncion diferente a la de la capa oculta, esto vimos que mejora los resultados
            Array.Copy(softOut, salidas_c_salida, softOut.Length);

            double[] retResult = new double[neuronas_c_salida];
            Array.Copy(salidas_c_salida, retResult, retResult.Length);
            return retResult;
        }

        /// <summary>
        /// Funcion de transferencia lineal f(x) = x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double FuncionDeActivacion(double x)
        {
            return x;
        }

        private static double DerivadaFuncionActivacion(double x)
        {
            return 1;
        }

        private static double[] Sigmoide(double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int i = 0; i < vector.Length; ++i)
                result[i] = 1.0 / (1.0 + Math.Exp(-vector[i]));

            return result;
        }

        public static double Derivada_sigmoide(double x)
        {
            double y = 1.0 / (1.0 + Math.Exp(-x)); //calculo el sigmoide de cada valor
            return y * (1 - y); //la derivada del sigmoide se calcula en funcion de si misma
        }

        public double[] Entrenar(double[][] datos_entrenamiento, int corridas_maximas, double tasa_de_aprendizaje, double momento)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] gradientes_pesos_c_oculta_1_c_salida = SetearValores(neuronas_c_oculta_1, neuronas_c_salida, 0.0);
            double[] gradientes_umbrales_salida = new double[neuronas_c_salida];

            double[][] gradientes_pesos_c_oculta_0_oculta_1 = SetearValores(neuronas_c_oculta_0, neuronas_c_oculta_1, 0.0);
            double[] gradientes_umbrales_oculta_1 = new double[neuronas_c_oculta_1];

            double[][] gradientes_pesos_c_entrada_c_oculta_0 = SetearValores(neuronas_c_entrada, neuronas_c_oculta_0, 0.0);
            double[] gradientes_umbrales_oculta_0 = new double[neuronas_c_oculta_0];

            double[] signo_gradiente_salida = new double[neuronas_c_salida];
            double[] signo_gradiente_c_oculta_1 = new double[neuronas_c_oculta_1];
            double[] signo_gradiente_c_oculta_0 = new double[neuronas_c_oculta_0];

            // back-prop momentum specific arrays 
            double[][] delta_valores_previos_pesos_c_entrada_c_oculta_0 = SetearValores(neuronas_c_entrada, neuronas_c_oculta_0, 0.0);
            double[] delta_valores_previos_umbrales_c_oculta_0 = new double[neuronas_c_oculta_0];

            double[][] delta_valores_previos_pesos_c_oculta_0_c_oculta_1 = SetearValores(neuronas_c_oculta_0, neuronas_c_oculta_1, 0.0);
            double[] delta_valores_previos_umbrales_c_oculta_1 = new double[neuronas_c_oculta_1];

            double[][] delta_valores_previos_pesos_oculta_1_c_salida = SetearValores(neuronas_c_oculta_1, neuronas_c_salida, 0.0);
            double[] delta_valores_previos_umbrales_c_salida = new double[neuronas_c_salida];

            int corridas = 0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados_salida = new double[neuronas_c_salida];
            double derivada = 0.0;
            double signo_error = 0.0;

            //establecimos un vector con los indices para mezclarlos y poder correr los entrenamientos de manera aleatoria sobre los datos proporcionados
            int[] secuencia = new int[datos_entrenamiento.Length];
            for (int i = 0; i < secuencia.Length; ++i)
                secuencia[i] = i;

            int errInterval = corridas_maximas / 10; //partimos las corridas maximas en 10 para ir comprobando el error
            while (corridas < corridas_maximas)
            {
                ++corridas;

                if (corridas % errInterval == 0 && corridas < corridas_maximas)
                {
                    double error = Error(datos_entrenamiento);

                    ErrorEpoca ee = new ErrorEpoca(corridas, error);
                    InformarErrorEpoca?.Invoke(this, ee);
                }

                MezclarDatos(secuencia);
                for (int ii = 0; ii < datos_entrenamiento.Length; ++ii)
                {
                    int idx = secuencia[ii];
                    Array.Copy(datos_entrenamiento[idx], valores_entrada, neuronas_c_entrada);
                    Array.Copy(datos_entrenamiento[idx], neuronas_c_entrada, valores_esperados_salida, 0, neuronas_c_salida);
                    ProcesarEntradas(valores_entrada);

                    // 1. compute output node signals (assumes softmax)
                    for (int k = 0; k < neuronas_c_salida; ++k)
                    {
                        signo_error = valores_esperados_salida[k] - salidas_c_salida[k];  // Wikipedia uses (o-t)
                        derivada = Derivada_sigmoide(salidas_c_salida[k]); // derivada de la funcion de activacion
                        signo_gradiente_salida[k] = signo_error * derivada;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (int j = 0; j < neuronas_c_oculta_1; ++j)
                        for (int k = 0; k < neuronas_c_salida; ++k)
                            gradientes_pesos_c_oculta_1_c_salida[j][k] = signo_gradiente_salida[k] * salidas_c_oculta_1[j];

                    // 2b. compute output bias gradients using output signals
                    for (int k = 0; k < neuronas_c_salida; ++k)
                        gradientes_umbrales_salida[k] = signo_gradiente_salida[k] * 1.0;

                    // 3. compute hidden node signals
                    for (int j = 0; j < neuronas_c_oculta_1; ++j)
                    {
                        derivada = DerivadaFuncionActivacion(salidas_c_oculta_1[j]);
                        double sum = 0.0;
                        for (int k = 0; k < neuronas_c_salida; ++k)
                        {
                            sum += signo_gradiente_salida[k] * pesos_c_oculta_1_a_c_salida[j][k];
                        }
                        signo_gradiente_c_oculta_1[j] = derivada * sum;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (int j = 0; j < neuronas_c_oculta_0; ++j)
                        for (int k = 0; k < neuronas_c_oculta_1; ++k)
                            gradientes_pesos_c_oculta_0_oculta_1[j][k] = signo_gradiente_c_oculta_1[k] * salidas_c_oculta_0[j];

                    // 2b. compute output bias gradients using output signals
                    for (int k = 0; k < neuronas_c_salida; ++k)
                        gradientes_umbrales_oculta_1[k] = signo_gradiente_c_oculta_1[k] * 1.0;

                    // 3. compute hidden node signals
                    for (int j = 0; j < neuronas_c_oculta_0; ++j)
                    {
                        derivada = DerivadaFuncionActivacion(salidas_c_oculta_1[j]);
                        double sum = 0.0; // need sums of output signals times hidden-to-output weights
                        for (int k = 0; k < neuronas_c_oculta_1; ++k)
                        {
                            sum += signo_gradiente_c_oculta_1[k] * pesos_c_oculta_0_a_c_oculta_1[j][k];
                        }
                        signo_gradiente_c_oculta_0[j] = derivada * sum;
                    }

                    // 4. compute input-hidden weight gradients
                    for (int i = 0; i < neuronas_c_entrada; ++i)
                        for (int j = 0; j < neuronas_c_oculta_0; ++j)
                            gradientes_pesos_c_entrada_c_oculta_0[i][j] = signo_gradiente_c_oculta_0[j] * entradas[i];

                    // 4b. compute hidden node bias gradients
                    for (int j = 0; j < neuronas_c_oculta_0; ++j)
                        gradientes_umbrales_oculta_0[j] = signo_gradiente_c_oculta_0[j] * 1.0;

                    // == update weights and biases

                    // update input-to-hidden weights
                    for (int i = 0; i < neuronas_c_entrada; ++i)
                    {
                        for (int j = 0; j < neuronas_c_oculta_0; ++j)
                        {
                            double delta = gradientes_pesos_c_entrada_c_oculta_0[i][j] * tasa_de_aprendizaje;
                            pesos_c_entrada_a_c_oculta_0[i][j] += delta;
                            pesos_c_entrada_a_c_oculta_0[i][j] += delta_valores_previos_pesos_c_entrada_c_oculta_0[i][j] * momento;
                            delta_valores_previos_pesos_c_entrada_c_oculta_0[i][j] = delta;
                        }
                    }

                    // update hidden biases
                    for (int j = 0; j < neuronas_c_oculta_0; ++j)
                    {
                        double delta = gradientes_umbrales_oculta_0[j] * tasa_de_aprendizaje;
                        umbrales_c_oculta_0[j] += delta;
                        umbrales_c_oculta_0[j] += delta_valores_previos_umbrales_c_oculta_0[j] * momento;
                        delta_valores_previos_umbrales_c_oculta_0[j] = delta;
                    }



                    // update input-to-hidden weights
                    for (int i = 0; i < neuronas_c_oculta_0; ++i)
                    {
                        for (int j = 0; j < neuronas_c_oculta_1; ++j)
                        {
                            double delta = gradientes_pesos_c_oculta_0_oculta_1[i][j] * tasa_de_aprendizaje;
                            pesos_c_oculta_0_a_c_oculta_1[i][j] += delta;
                            pesos_c_oculta_0_a_c_oculta_1[i][j] += delta_valores_previos_pesos_c_oculta_0_c_oculta_1[i][j] * momento;
                            delta_valores_previos_pesos_c_oculta_0_c_oculta_1[i][j] = delta;
                        }
                    }

                    // update hidden biases
                    for (int j = 0; j < neuronas_c_oculta_1; ++j)
                    {
                        double delta = gradientes_umbrales_oculta_1[j] * tasa_de_aprendizaje;
                        umbrales_c_oculta_1[j] += delta;
                        umbrales_c_oculta_1[j] += delta_valores_previos_umbrales_c_oculta_1[j] * momento;
                        delta_valores_previos_umbrales_c_oculta_1[j] = delta;
                    }

                    // update hidden-to-output weights
                    for (int j = 0; j < neuronas_c_oculta_0; ++j)
                    {
                        for (int k = 0; k < neuronas_c_salida; ++k)
                        {
                            double delta = gradientes_pesos_c_oculta_1_c_salida[j][k] * tasa_de_aprendizaje;
                            pesos_c_oculta_1_a_c_salida[j][k] += delta;
                            pesos_c_oculta_1_a_c_salida[j][k] += delta_valores_previos_pesos_oculta_1_c_salida[j][k] * momento;
                            delta_valores_previos_pesos_oculta_1_c_salida[j][k] = delta;
                        }
                    }

                    // update output node biases
                    for (int k = 0; k < neuronas_c_salida; ++k)
                    {
                        double delta = gradientes_umbrales_salida[k] * tasa_de_aprendizaje;
                        umbrales_c_salida[k] += delta;
                        umbrales_c_salida[k] += delta_valores_previos_umbrales_c_salida[k] * momento;
                        delta_valores_previos_umbrales_c_salida[k] = delta;
                    }

                } // por cada item de entrenamiento

            } // fin del mientras

            double[] mejores_pesos_encontrados = ObtenerPesosUmbrales();
            return mejores_pesos_encontrados;
        } // fin de entrenamiento

        private void MezclarDatos(int[] sequence)
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = this.rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }

        private double Error(double[][] datos_entrenamiento)
        {
            double suma_de_errores_al_cuadrado = 0.0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados = new double[neuronas_c_salida];

            for (int i = 0; i < datos_entrenamiento.Length; ++i)
            {
                Array.Copy(datos_entrenamiento[i], valores_entrada, neuronas_c_entrada);
                Array.Copy(datos_entrenamiento[i], neuronas_c_entrada, valores_esperados, 0, neuronas_c_salida);
                double[] yValues = this.ProcesarEntradas(valores_entrada);
                for (int j = 0; j < neuronas_c_salida; ++j)
                {
                    double err = valores_esperados[j] - yValues[j];
                    suma_de_errores_al_cuadrado += err * err;
                }
            }
            return suma_de_errores_al_cuadrado / datos_entrenamiento.Length;
        }

        public double Precision(double[][] datos_de_prueba)
        {
            // percentage correct using winner-takes all
            int correctos = 0;
            int incorrectos = 0;
            double[] valores_entrada = new double[neuronas_c_entrada];
            double[] valores_esperados = new double[neuronas_c_salida];
            double[] valores_obtenidos;

            for (int i = 0; i < datos_de_prueba.Length; ++i)
            {
                Array.Copy(datos_de_prueba[i], valores_entrada, neuronas_c_entrada);
                Array.Copy(datos_de_prueba[i], neuronas_c_entrada, valores_esperados, 0, neuronas_c_salida);
                valores_obtenidos = this.ProcesarEntradas(valores_entrada);

                int indice1_resultado_esperado = Indice_del_valor_1(valores_esperados);
                int indice1_resultado_obtenido = Indice_del_valor_1(valores_obtenidos);

                if (indice1_resultado_esperado == indice1_resultado_obtenido)
                    ++correctos;
                else
                    ++incorrectos;

            }
            return (correctos * 1.0) / (correctos + incorrectos);
        }

        private static int Indice_del_valor_1(double[] vector)
        {
            //tomo el vector de resultado obtenido y
            //para compararlo con el vector de resultado esperado
            //busco el maximo entre los resultados y a este indice le asigno el valor 1
            //ya que los valores posibles de resultado son:
            // 0 0 1 => b
            // 0 1 0 => d
            // 1 0 0 => f

            int indice = 0;
            double valor_maximo = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > valor_maximo)
                {
                    valor_maximo = vector[i];
                    indice = i;
                }
            }
            return indice;
        }

    }

}
