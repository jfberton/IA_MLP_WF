namespace IA_MLP
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.letra1 = new IA_MLP.Letra();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_cantidad = new System.Windows.Forms.TextBox();
            this.cb_topologia = new System.Windows.Forms.ComboBox();
            this.cb_letra = new System.Windows.Forms.ComboBox();
            this.cb_distorcion = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LetraColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.letra_enviada_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultadoReconocimientoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReconocioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salidaNeurona1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salidaNeurona2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salidaNeurona3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(261, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 434);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.letra1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 268);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(539, 166);
            this.panel4.TabIndex = 2;
            // 
            // letra1
            // 
            this.letra1.Location = new System.Drawing.Point(3, 6);
            this.letra1.Name = "letra1";
            this.letra1.Size = new System.Drawing.Size(150, 150);
            this.letra1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(539, 234);
            this.panel3.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LetraColumn,
            this.letra_enviada_column,
            this.ResultadoReconocimientoColumn,
            this.ReconocioColumn,
            this.salidaNeurona1,
            this.salidaNeurona2,
            this.salidaNeurona3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(539, 234);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(539, 34);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Topología";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Letra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cantidad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Distorción";
            // 
            // tb_cantidad
            // 
            this.tb_cantidad.Location = new System.Drawing.Point(134, 111);
            this.tb_cantidad.Name = "tb_cantidad";
            this.tb_cantidad.Size = new System.Drawing.Size(121, 23);
            this.tb_cantidad.TabIndex = 2;
            this.tb_cantidad.Text = "1";
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
            this.cb_topologia.Location = new System.Drawing.Point(77, 24);
            this.cb_topologia.Name = "cb_topologia";
            this.cb_topologia.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_topologia.Size = new System.Drawing.Size(178, 23);
            this.cb_topologia.TabIndex = 6;
            // 
            // cb_letra
            // 
            this.cb_letra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_letra.FormattingEnabled = true;
            this.cb_letra.Items.AddRange(new object[] {
            "b",
            "d",
            "f"});
            this.cb_letra.Location = new System.Drawing.Point(77, 53);
            this.cb_letra.Name = "cb_letra";
            this.cb_letra.Size = new System.Drawing.Size(178, 23);
            this.cb_letra.TabIndex = 7;
            // 
            // cb_distorcion
            // 
            this.cb_distorcion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_distorcion.FormattingEnabled = true;
            this.cb_distorcion.Items.AddRange(new object[] {
            "Sin distorcionar",
            "Distorción del 1 %",
            "Distorción del 2 %",
            "Distorción del 3 %",
            "Distorción del 4 %",
            "Distorción del 5 %",
            "Distorción del 6 %",
            "Distorción del 7 %",
            "Distorción del 8 %",
            "Distorción del 9 %",
            "Distorción del 10 %",
            "Distorción del 11 %",
            "Distorción del 12 %",
            "Distorción del 13 %",
            "Distorción del 14 %",
            "Distorción del 15 %",
            "Distorción del 16 %",
            "Distorción del 17 %",
            "Distorción del 18 %",
            "Distorción del 19 %",
            "Distorción del 20 %",
            "Distorción del 21 %",
            "Distorción del 22 %",
            "Distorción del 23 %",
            "Distorción del 24 %",
            "Distorción del 25 %",
            "Distorción del 26 %",
            "Distorción del 27 %",
            "Distorción del 28 %",
            "Distorción del 29 %",
            "Distorción del 30 %"});
            this.cb_distorcion.Location = new System.Drawing.Point(77, 82);
            this.cb_distorcion.Name = "cb_distorcion";
            this.cb_distorcion.Size = new System.Drawing.Size(178, 23);
            this.cb_distorcion.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Probar!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LetraColumn
            // 
            this.LetraColumn.HeaderText = "Nro";
            this.LetraColumn.Name = "LetraColumn";
            this.LetraColumn.ReadOnly = true;
            this.LetraColumn.Width = 50;
            // 
            // letra_enviada_column
            // 
            this.letra_enviada_column.HeaderText = "Letra enviada";
            this.letra_enviada_column.Name = "letra_enviada_column";
            this.letra_enviada_column.ReadOnly = true;
            // 
            // ResultadoReconocimientoColumn
            // 
            this.ResultadoReconocimientoColumn.HeaderText = "Letra reconocida";
            this.ResultadoReconocimientoColumn.Name = "ResultadoReconocimientoColumn";
            this.ResultadoReconocimientoColumn.ReadOnly = true;
            this.ResultadoReconocimientoColumn.Width = 150;
            // 
            // ReconocioColumn
            // 
            this.ReconocioColumn.HeaderText = "Reconoció";
            this.ReconocioColumn.Name = "ReconocioColumn";
            this.ReconocioColumn.ReadOnly = true;
            // 
            // salidaNeurona1
            // 
            this.salidaNeurona1.HeaderText = "Salida Neurona 1";
            this.salidaNeurona1.Name = "salidaNeurona1";
            this.salidaNeurona1.ReadOnly = true;
            // 
            // salidaNeurona2
            // 
            this.salidaNeurona2.HeaderText = "Salida Neurona 2";
            this.salidaNeurona2.Name = "salidaNeurona2";
            this.salidaNeurona2.ReadOnly = true;
            // 
            // salidaNeurona3
            // 
            this.salidaNeurona3.HeaderText = "Salida Neurona 3";
            this.salidaNeurona3.Name = "salidaNeurona3";
            this.salidaNeurona3.ReadOnly = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 434);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_distorcion);
            this.Controls.Add(this.cb_letra);
            this.Controls.Add(this.cb_topologia);
            this.Controls.Add(this.tb_cantidad);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private Panel panel3;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tb_cantidad;
        private ComboBox cb_topologia;
        private ComboBox cb_letra;
        private ComboBox cb_distorcion;
        private Button button1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn DistorcionColumn;
        private Letra letra1;
        private DataGridViewTextBoxColumn LetraColumn;
        private DataGridViewTextBoxColumn letra_enviada_column;
        private DataGridViewTextBoxColumn ResultadoReconocimientoColumn;
        private DataGridViewTextBoxColumn ReconocioColumn;
        private DataGridViewTextBoxColumn salidaNeurona1;
        private DataGridViewTextBoxColumn salidaNeurona2;
        private DataGridViewTextBoxColumn salidaNeurona3;
    }
}