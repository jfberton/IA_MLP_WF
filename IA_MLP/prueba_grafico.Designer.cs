namespace IA_MLP
{
    partial class prueba_grafico
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(prueba_grafico));
            this.nChartControl1 = new Nevron.Chart.WinForm.NChartControl();
            this.SuspendLayout();
            // 
            // nChartControl1
            // 
            this.nChartControl1.AutoRefresh = false;
            this.nChartControl1.BackColor = System.Drawing.SystemColors.Control;
            this.nChartControl1.InputKeys = new System.Windows.Forms.Keys[0];
            this.nChartControl1.Location = new System.Drawing.Point(12, 12);
            this.nChartControl1.Name = "nChartControl1";
            this.nChartControl1.Size = new System.Drawing.Size(767, 426);
            this.nChartControl1.State = ((Nevron.Chart.WinForm.NState)(resources.GetObject("nChartControl1.State")));
            this.nChartControl1.TabIndex = 0;
            this.nChartControl1.Text = "nChartControl1";
            // 
            // prueba_grafico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nChartControl1);
            this.Name = "prueba_grafico";
            this.Text = "prueba_grafico";
            this.Load += new System.EventHandler(this.prueba_grafico_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Nevron.Chart.WinForm.NChartControl nChartControl1;
    }
}