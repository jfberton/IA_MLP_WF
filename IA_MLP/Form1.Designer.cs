namespace IA_MLP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tb_resultados = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_tasa_aprendizaje = new System.Windows.Forms.TextBox();
            this.tb_momento = new System.Windows.Forms.TextBox();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.cb_topologia = new System.Windows.Forms.ComboBox();
            this.cb_dataset = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_epocas = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(140, 209);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ejecutar corridas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Topología";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "DataSet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tasa de aprendizaje";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Momento";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(285, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 450);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tb_resultados);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(515, 397);
            this.panel3.TabIndex = 1;
            // 
            // tb_resultados
            // 
            this.tb_resultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_resultados.Location = new System.Drawing.Point(0, 0);
            this.tb_resultados.Multiline = true;
            this.tb_resultados.Name = "tb_resultados";
            this.tb_resultados.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_resultados.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_resultados.Size = new System.Drawing.Size(515, 397);
            this.tb_resultados.TabIndex = 2;
            this.tb_resultados.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(515, 53);
            this.panel2.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Datos de la ejecución del perceptrón";
            // 
            // tb_tasa_aprendizaje
            // 
            this.tb_tasa_aprendizaje.Location = new System.Drawing.Point(140, 143);
            this.tb_tasa_aprendizaje.Name = "tb_tasa_aprendizaje";
            this.tb_tasa_aprendizaje.Size = new System.Drawing.Size(139, 23);
            this.tb_tasa_aprendizaje.TabIndex = 4;
            this.tb_tasa_aprendizaje.Text = "0,5";
            // 
            // tb_momento
            // 
            this.tb_momento.Location = new System.Drawing.Point(140, 171);
            this.tb_momento.Name = "tb_momento";
            this.tb_momento.Size = new System.Drawing.Size(139, 23);
            this.tb_momento.TabIndex = 4;
            this.tb_momento.Text = "0,5";
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // cb_topologia
            // 
            this.cb_topologia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_topologia.FormattingEnabled = true;
            this.cb_topologia.Items.AddRange(new object[] {
            "100 - 05 - 3",
            "100 - 10 - 3",
            "100 - 05 - 05 - 3",
            "100 - 10 - 10 - 3"});
            this.cb_topologia.Location = new System.Drawing.Point(85, 53);
            this.cb_topologia.Name = "cb_topologia";
            this.cb_topologia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_topologia.Size = new System.Drawing.Size(194, 23);
            this.cb_topologia.TabIndex = 5;
            // 
            // cb_dataset
            // 
            this.cb_dataset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dataset.FormattingEnabled = true;
            this.cb_dataset.Items.AddRange(new object[] {
            "100 valores con 10 % distorción",
            "100 valores con 20 % distorción",
            "100 valores con 30 % distorción",
            "500 valores con 10 % distorción",
            "500 valores con 20 % distorción",
            "500 valores con 30 % distorción",
            "1000 valores con 10 % distorción",
            "1000 valores con 20 % distorción",
            "1000 valores con 30 % distorción",
            ""});
            this.cb_dataset.Location = new System.Drawing.Point(85, 85);
            this.cb_dataset.Name = "cb_dataset";
            this.cb_dataset.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_dataset.Size = new System.Drawing.Size(194, 23);
            this.cb_dataset.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Configuración de la ejecución";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "Máximo epocas";
            // 
            // tb_epocas
            // 
            this.tb_epocas.Location = new System.Drawing.Point(140, 114);
            this.tb_epocas.Name = "tb_epocas";
            this.tb_epocas.Size = new System.Drawing.Size(139, 23);
            this.tb_epocas.TabIndex = 4;
            this.tb_epocas.Text = "1000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_dataset);
            this.Controls.Add(this.cb_topologia);
            this.Controls.Add(this.tb_momento);
            this.Controls.Add(this.tb_epocas);
            this.Controls.Add(this.tb_tasa_aprendizaje);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Panel panel3;
        private TextBox tb_resultados;
        private Panel panel2;
        private Label label5;
        private TextBox tb_tasa_aprendizaje;
        private TextBox tb_momento;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private ComboBox cb_topologia;
        private ComboBox cb_dataset;
        private Label label6;
        private Label label7;
        private TextBox tb_epocas;
    }
}