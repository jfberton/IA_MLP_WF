using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_MLP
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            cb_topologia.SelectedIndex = 0;
            cb_letra.SelectedIndex = 0;
            cb_distorcion.SelectedIndex = 0;
        }

        string path_entrenamiento = string.Empty;
        double[] letra_sin_distorcionar;
        int porcentaje_distorcion;
        double[][] letras_distorcionadas;

        private void button1_Click(object sender, EventArgs e)
        {
            int cantidad_capas_ocultas = 0;
            int cantidad_neuronas_capa_oculta_1 = 0;
            int cantidad_neuronas_capa_oculta_2 = 0;
            switch (cb_topologia.SelectedItem.ToString())
            {
                case "100 - 05 - 3":
                    path_entrenamiento = Application.StartupPath + string.Format(@"Datasets\pesos_umbrales_100_5_3.txt");
                    cantidad_capas_ocultas = 1;
                    cantidad_neuronas_capa_oculta_1 = 5;
                    break;
                case "100 - 10 - 3":
                    path_entrenamiento = Application.StartupPath + string.Format(@"Datasets\pesos_umbrales_100_10_3.txt");
                    cantidad_capas_ocultas = 1;
                    cantidad_neuronas_capa_oculta_1 = 10;
                    break;
                case "100 - 05 - 05 - 3":
                    path_entrenamiento = Application.StartupPath + string.Format(@"Datasets\pesos_umbrales_100_5_5_3.txt");
                    cantidad_capas_ocultas = 2;
                    cantidad_neuronas_capa_oculta_1 = 5;
                    cantidad_neuronas_capa_oculta_2 = 5;
                    break;
                case "100 - 10 - 10 - 3":
                    path_entrenamiento = Application.StartupPath + string.Format(@"Datasets\pesos_umbrales_100_10_10_3.txt");
                    cantidad_capas_ocultas = 2;
                    cantidad_neuronas_capa_oculta_1 = 10;
                    cantidad_neuronas_capa_oculta_2 = 10;
                    break;
                default:
                    throw new Exception("Topologia no encontrada");
            }

            string data = System.IO.File.ReadAllText(path_entrenamiento).Replace("\r", "");
            
            string[] valores = data.Split('\n');
            List<double> pesos_y_umbrales_list = new List<double>();
            for (int i = 0; i < valores.Length; i++)
            {
                if (valores[i] != "" && valores[i] != "\r")
                    pesos_y_umbrales_list.Add(double.Parse(valores[i]));
            }
            double[] pesos_y_umbrales = pesos_y_umbrales_list.ToArray();
            
            letra_sin_distorcionar = ObtenerValoresLetra(cb_letra.SelectedItem.ToString());
            string distorcion = cb_distorcion.SelectedItem.ToString();

            if (distorcion == "Sin distorcionar")
            {
                letras_distorcionadas = new double[1][];
                letras_distorcionadas[0] = letra_sin_distorcionar;
            }
            else
            {
                porcentaje_distorcion = int.Parse(distorcion.Replace("Distorción del ", "").Replace(" %", ""));

                int cantidad_valores = 1;

                if (int.TryParse(tb_cantidad.Text, out cantidad_valores))
                {
                    letras_distorcionadas = ObtenerLetrasDistorcionadas(cantidad_valores);
                }
                else
                {
                    MessageBox.Show("Ingrese valor numérico en el campo cantidad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            if (cantidad_capas_ocultas == 1)
            {
                Perceptron_e_o_s nn = new Perceptron_e_o_s(100, cantidad_neuronas_capa_oculta_1, 3);
                nn.CargarPesosyUmbrales(pesos_y_umbrales);
                double[][] salidas_sin_corregir = new double[letras_distorcionadas.Length][];
                double[][] salidas_corregidas = new double[letras_distorcionadas.Length][];
                string[] reconocio = new string[letras_distorcionadas.Length];
                string[] correcto = new string[letras_distorcionadas.Length];

                for (int i = 0; i < letras_distorcionadas.Length; i++)
                {
                    salidas_sin_corregir[i] = nn.ProcesarEntradas(letras_distorcionadas[i]);
                    salidas_corregidas[i] = ObtenerResultadoCorregido(salidas_sin_corregir[i]);
                    if (salidas_corregidas[i][0] == 1)
                    {
                        reconocio[i] = "f";
                    }
                    else if (salidas_corregidas[i][1] == 1)
                    {
                        reconocio[i] = "d";
                    }
                    else if (salidas_corregidas[i][2] == 1)
                    {
                        reconocio[i] = "b";
                    }

                    if (reconocio[i] == cb_letra.SelectedItem.ToString())
                    {
                        correcto[i] = "Si";
                    }
                    else
                    {
                        correcto[i] = "No";
                    }
                }

                dataGridView1.Rows.Clear();
                for (int i = 0; i < letras_distorcionadas.Length; i++)
                {
                    dataGridView1.Rows.Add(
                                i + 1
                                , cb_letra.SelectedItem.ToString()
                                , reconocio[i]
                                , correcto[i]
                                , salidas_sin_corregir[i][0].ToString()
                                , salidas_sin_corregir[i][1].ToString()
                                , salidas_sin_corregir[i][2].ToString());
                }
            }
            else
            {
                Perceptron_e_o_o_s nn = new Perceptron_e_o_o_s(100, cantidad_neuronas_capa_oculta_1, cantidad_neuronas_capa_oculta_2, 3);
                nn.CargarPesosyUmbrales(pesos_y_umbrales);
                double[][] salidas_sin_corregir = new double[letras_distorcionadas.Length][];
                double[][] salidas_corregidas = new double[letras_distorcionadas.Length][];
                string[] reconocio = new string[letras_distorcionadas.Length];
                string[] correcto = new string[letras_distorcionadas.Length];

                for (int i = 0; i < letras_distorcionadas.Length; i++)
                {
                    salidas_sin_corregir[i] = nn.ProcesarEntradas(letras_distorcionadas[i]);
                    salidas_corregidas[i] = ObtenerResultadoCorregido(salidas_sin_corregir[i]);
                    if (salidas_corregidas[i][0] == 1)
                    {
                        reconocio[i] = "f";
                    }
                    else if (salidas_corregidas[i][1] == 1)
                    {
                        reconocio[i] = "d";
                    }
                    else if (salidas_corregidas[i][2] == 1)
                    {
                        reconocio[i] = "b";
                    }

                    if (reconocio[i] == cb_letra.SelectedItem.ToString())
                    {
                        correcto[i] = "Si";
                    }
                    else
                    {
                        correcto[i] = "No";
                    }
                }

                dataGridView1.Rows.Clear();
                for (int i = 0; i < letras_distorcionadas.Length; i++)
                {
                    dataGridView1.Rows.Add(
                                i + 1
                                , cb_letra.SelectedItem.ToString()
                                , reconocio[i]
                                , correcto[i]
                                , salidas_sin_corregir[i][0].ToString()
                                , salidas_sin_corregir[i][1].ToString()
                                , salidas_sin_corregir[i][2].ToString());
                }
            }
        }

        private double[] ObtenerResultadoCorregido(double[] resultadosDouble)
        {
            double[] ret = new double[3];
            double valorMaximo = 0;
            int indiceMaximo = 0;
            for (int i = 0; i < resultadosDouble.Length; i++)
            {
                if (resultadosDouble[i] > valorMaximo)
                {
                    valorMaximo = resultadosDouble[i];
                    indiceMaximo = i;
                }    
            }

            ret[indiceMaximo] = 1;
            return ret;
        }

        private double[][] ObtenerLetrasDistorcionadas(int cantidad_distorciones_solicitadas)
        {
            Random rnd = new Random(1);
            int[] cambiar_valor;

            double[][] letras_distorcionadas = new double[cantidad_distorciones_solicitadas][] ;
            for (int i = 0; i < cantidad_distorciones_solicitadas; i++)
            {
                //obtengo la letra sin distorcionar
                
                double[] letra_distorcionada = new double[letra_sin_distorcionar.Length];
                Array.Copy(letra_sin_distorcionar, letra_distorcionada, letra_sin_distorcionar.Length);
                int valores_cargados = 0;
                cambiar_valor = new int[porcentaje_distorcion];
                //seteo los valores del arreglo que contiene los indices random
                //en donde cambiar el pixel
                while (valores_cargados < porcentaje_distorcion)
                {
                    int indice_a_modificar = rnd.Next(0, 100);
                    if (!cambiar_valor.Contains(indice_a_modificar))
                    {
                        cambiar_valor[valores_cargados] = indice_a_modificar;
                        valores_cargados++;
                    }
                }

                for (int j = 0; j < cambiar_valor.Length; j++)
                {
                    if (letra_distorcionada[cambiar_valor[j]] == 0)
                    {
                        letra_distorcionada[cambiar_valor[j]] = 1;
                    }
                    else
                    {
                        letra_distorcionada[cambiar_valor[j]] = 0;
                    }
                }

                letras_distorcionadas[i] = letra_distorcionada;
            }

            return letras_distorcionadas;
        }

        private double[] ObtenerValoresLetra(string letra_buscada)
        {
            List<double[]> valores_letras = ObtenerValoresLetras();
            switch (letra_buscada)
            {
                case "b":
                    return valores_letras[0];
                case "d":
                    return valores_letras[1];
                case "f":
                    return valores_letras[2];
                default:
                    throw new Exception("Letra no encontrada");
            }
        }

        private List<double[]> ObtenerValoresLetras()
        {
            string path_letras = Application.StartupPath + @"Datasets\letras_originales.txt";
            string data = System.IO.File.ReadAllText(path_letras).Replace("\r", "");
            string[] rows = data.Split(Environment.NewLine.ToCharArray());
            List<double[]> ret = new List<double[]>();
            foreach (string row in rows)
            {
                if (row != "")
                {
                    string[] values = row.Split(';');
                    double[] values_double = new double[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        values_double[i] = double.Parse(values[i]);
                    }
                    ret.Add(values_double);
                }
            }

            return ret;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (letras_distorcionadas != null && letras_distorcionadas.Length >= e.RowIndex)
            {
                MostrarLetra(letras_distorcionadas[e.RowIndex]);
            }

        }

        public void MostrarLetra(double[] letra)
        {
            for (int i = 1; i <= 100; i++)
            {
                int index = panel4.Controls.IndexOfKey("pictureBox" + i);
                if (index != -1)
                {
                    PictureBox pb = (PictureBox)panel4.Controls[index];
                    pb.BackColor = letra[i-1] == 0 ? Color.White : Color.Black;
                }
            }

            letra1.MostrarLetra(letra, cb_letra.SelectedItem.ToString(), porcentaje_distorcion);
        }
    }
}
