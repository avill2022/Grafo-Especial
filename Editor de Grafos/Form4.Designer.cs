namespace Editor_de_Grafos
{
    partial class Form4
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Crear = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.N = new System.Windows.Forms.Label();
            this.M = new System.Windows.Forms.Label();
            this.Dirigido = new System.Windows.Forms.RadioButton();
            this.NoDirigido = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // Crear
            // 
            this.Crear.Location = new System.Drawing.Point(22, 70);
            this.Crear.Name = "Crear";
            this.Crear.Size = new System.Drawing.Size(177, 32);
            this.Crear.TabIndex = 0;
            this.Crear.Text = "Crear";
            this.Crear.UseVisualStyleBackColor = true;
            this.Crear.Click += new System.EventHandler(this.Crear_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(56, 16);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(56, 44);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 2;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // N
            // 
            this.N.AutoSize = true;
            this.N.Location = new System.Drawing.Point(19, 18);
            this.N.Name = "N";
            this.N.Size = new System.Drawing.Size(15, 13);
            this.N.TabIndex = 3;
            this.N.Text = "N";
            // 
            // M
            // 
            this.M.AutoSize = true;
            this.M.Location = new System.Drawing.Point(19, 51);
            this.M.Name = "M";
            this.M.Size = new System.Drawing.Size(16, 13);
            this.M.TabIndex = 4;
            this.M.Text = "M";
            // 
            // Dirigido
            // 
            this.Dirigido.AutoSize = true;
            this.Dirigido.Checked = true;
            this.Dirigido.Location = new System.Drawing.Point(22, 117);
            this.Dirigido.Name = "Dirigido";
            this.Dirigido.Size = new System.Drawing.Size(60, 17);
            this.Dirigido.TabIndex = 5;
            this.Dirigido.TabStop = true;
            this.Dirigido.Text = "Dirigido";
            this.Dirigido.UseVisualStyleBackColor = true;
            this.Dirigido.CheckedChanged += new System.EventHandler(this.Dirigido_CheckedChanged);
            // 
            // NoDirigido
            // 
            this.NoDirigido.AutoSize = true;
            this.NoDirigido.Location = new System.Drawing.Point(122, 117);
            this.NoDirigido.Name = "NoDirigido";
            this.NoDirigido.Size = new System.Drawing.Size(77, 17);
            this.NoDirigido.TabIndex = 6;
            this.NoDirigido.Text = "No Dirigido";
            this.NoDirigido.UseVisualStyleBackColor = true;
            this.NoDirigido.CheckedChanged += new System.EventHandler(this.NoDirigido_CheckedChanged);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 146);
            this.Controls.Add(this.NoDirigido);
            this.Controls.Add(this.Dirigido);
            this.Controls.Add(this.M);
            this.Controls.Add(this.N);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Crear);
            this.Name = "Form4";
            this.Text = "M";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Crear;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label N;
        private System.Windows.Forms.Label M;
        private System.Windows.Forms.RadioButton Dirigido;
        private System.Windows.Forms.RadioButton NoDirigido;
    }
}