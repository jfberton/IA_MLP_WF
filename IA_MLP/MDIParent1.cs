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
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
        }

        private void entrenarToolStripMenuItem_Click(object sender, EventArgs e)
        {
                foreach(Form formulario in this.MdiChildren)
                {
                    if (formulario.Text == "Programa de entrenamiento")
                    {
                        formulario.Activate();
                        return;
                    }
                }

                Form1 childForm = new Form1();
                childForm.MdiParent = this;
                childForm.Text = "Programa de entrenamiento";
                childForm.Show();
        }

        private void probarToolStripMenuItem_Click(object sender, EventArgs e)
        {
                foreach (Form formulario in this.MdiChildren)
                {
                    if (formulario.Text == "Prueba de reconocimiento")
                    {
                        formulario.Activate();
                        return;
                    }
                }

                Form2 childForm = new Form2();
                childForm.MdiParent = this;
                childForm.Text = "Prueba de reconocimiento";
                childForm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
