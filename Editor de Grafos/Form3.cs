using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public partial class Form3 : Form
    {
        private Form1 form;
        private int valor1;
        private int valor2;
        private int numGrafos;
        public Form3(Form1 f,String titulo,int numeroGrafos)
        {
            form = f;
            numGrafos = numeroGrafos;
            this.Text = titulo;
            InitializeComponent();
            activaOpciones();
        }
        public void activaOpciones()
        {
            numericUpDown1.Maximum = numGrafos;
            numericUpDown2.Maximum = numGrafos;
            this.Size = new Size(306, 139);
            /*label1.Visible = false;
            label2.Visible = false;
            con.Visible = false;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            valor1 = (int)numericUpDown1.Value;
            valor2 = (int)numericUpDown2.Value;
            if (form.isomorfismo(valor1 - 1, valor2 - 1))
            {
                MessageBox.Show("isomorfos");
            }
            else
                MessageBox.Show("No son isomorfos");  
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Grafo1
            valor1 = (int)numericUpDown1.Value;
            form.activaGrafo(valor1-1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Grafo2
            valor2 = (int)numericUpDown2.Value;
            form.activaGrafo(valor2 - 1);
        }
    }
}
