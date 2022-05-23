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
    public partial class Form6 : Form
    {
        public Form1 ff;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(Form1 f)
        {
            ff = f;
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(2);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(5);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(3);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(4);
        }

        private void N_CheckedChanged(object sender, EventArgs e)
        {
            ff.editaGrafo(0);
        }

        private void Color_Click(object sender, EventArgs e)
        {
            ff.editaGrafo(10);
        }
    }
}
