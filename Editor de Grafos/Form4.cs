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
    public partial class Form4 : Form
    {
        private int valor1;
        private int valor2;
        private Form1 f;
        private int tipo;
        private int opcion;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(Form1 form,int op)
        {
            InitializeComponent();
            f = form;
            tipo = 1;
            opcion = op;
            if (op != 0)
            {
                numericUpDown2.Enabled = false;
                M.Enabled = false;
            }
        }

        private void Crear_Click(object sender, EventArgs e)
        {
            valor1 = (int)numericUpDown1.Value;
            valor2 = (int)numericUpDown2.Value;
            if(opcion == 0)
                f.creaKmn((int)numericUpDown1.Value, (int)numericUpDown2.Value,tipo);
            if (opcion == 1)
                f.creaKn((int)numericUpDown1.Value, tipo);
            if (opcion == 2)
                f.creaCn((int)numericUpDown1.Value, tipo);
            if(opcion == 3)
                f.creaWn((int)numericUpDown1.Value, tipo);
            this.Close();
        }

        private void Dirigido_CheckedChanged(object sender, EventArgs e)
        {
            tipo = 0;
            tipo = 1;
        }

        private void NoDirigido_CheckedChanged(object sender, EventArgs e)
        {
            tipo = 0;
        }


    }
}
