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
    public partial class Form5 : Form
    {
        public int numero;
        private Form1 g;
        private int op;
        private string[,] s;
        private int t;

        public Form5(Form1 f)
        {
            g = f;
            op = 1;
            InitializeComponent();
            this.numericUpDown2.Visible = false;
            this.label2.Visible = false;
        }
      
        public Form5(Form1 f, string[,] C, int tam)
        {
            g = f;
            op = 2;
            s = C;
            t = tam;
            InitializeComponent();
            this.numericUpDown1.Maximum = tam-1;
            this.numericUpDown2.Visible = false;
            this.label2.Visible = false;
        }

        public Form5(Form1 f,int tam)
        {
            g = f;
            op = 3;
            t = tam;
            InitializeComponent();
            this.numericUpDown1.Maximum = tam;
            this.numericUpDown2.Maximum = tam;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numero = (int)numericUpDown1.Value;
            if(op==1)
                g.insertaPeso(numero);
            if (op == 2)
                g.Dijkstra(s, numero, t);
            if (op == 3)
            {
                if(g.warshall((int)numericUpDown1.Value-1, (int)numericUpDown2.Value-1)==0)
                    MessageBox.Show("No hay camino");
                else
                    MessageBox.Show("Si hay camino");
            }
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
