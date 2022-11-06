using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IA_MLP
{
    public partial class Letra : UserControl
    {
        public Letra()
        {
            InitializeComponent();
        }

        public void MostrarLetra(double[] valores_letra, string letra, int distorcion)
        {
            this.label1.Text = string.Format("Letra {0}", letra);
            this.label2.Text = string.Format("Distorción {0} %", distorcion);
            for (int i = 1; i <= 100; i++)
            {
                int index = this.Controls.IndexOfKey("pictureBox" + i);
                if (index != -1)
                {
                    PictureBox pb = (PictureBox)this.Controls[index];
                    pb.BackColor = valores_letra[i - 1] == 0 ? Color.White : Color.Black;
                }
            }
        }
    }
}
