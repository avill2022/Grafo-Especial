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
    public partial class Form2 : Form
    {
        private Form1 f;
        private int i;
        private int[,] m;
        private int[,] back;
        private int tam;
        private string grado;
        private int igrado;
        public Form2(Form1 form)
        {
            f = form;
            i = 0;
            InitializeComponent();
        }
        public Form2(int[] P, string[] D,int tam)
        {
            InitializeComponent();
            string a = this.combierteArr(P,tam);
            this.dameMatriz(a, 0);
        }
        public Form2(int[,] matriz, int tam,int verticeOrigen)
        { 
            InitializeComponent();
            dameMatriz(convierteEnCadena(matriz, tam, verticeOrigen), 1);
        }
        public string convierteEnCadena(int[,] m,int tam,int verticeOrigen)
        {
            string cad = "";
            for (int i = 1; i < tam; i++)
            {
                for(int j=tam-1;j>-1;j--)            
                {
                    if(m[i,j]!=0)
                    {
                        if(j==0)
                            cad = cad+ " Peso Total: " + m[i,j].ToString();
                        else
                            if (m[i, j] == verticeOrigen)
                            cad = cad + m[i, j].ToString();
                        else
                            cad = cad + "->" + m[i, j].ToString();
                    }
                }
                cad = cad + "<br>";
            }
            return cad;
        }
        public Form2(Form1 form,int[,] matriz,int tam1)
        {
            f = form;
            i = 2;
            igrado = 1;
            m = matriz;
            back = m;
            grado = "M^";
            tam = tam1;
            InitializeComponent();
            this.dameMatriz(converteEnCadena(matriz), 0);
        }
        public Form2(Form1 form, string[,] matriz, int n)
        {
            f = form;
            i = 2;
            igrado = 1;
            back = m;
            grado = "\\";
            tam = n;         
            InitializeComponent();
            this.dameMatriz(converteEnCadena(matriz), 0);
        }
        public Form2(string cad)
        {
            InitializeComponent();
            this.dameMatriz(cad, 4);
        }
        public void dameMatriz(string Tabla,int matriz)
        {
            if (matriz == 0)
            {
                webBrowser1.DocumentText = "<Table border=1 width=100%>" + Tabla + "/Table>";                
            }
            else
            if (matriz == 1)
            {
                webBrowser1.DocumentText ="<br>"+ Tabla;
               
            }
            if (matriz == 2)
            {
                webBrowser1.DocumentText = "<Table border=1 width=100%>" + Tabla + "/Table>";

            }
            if (matriz == 3)
            {
               
                webBrowser1.DocumentText = "<Table border=1 width=100%>" + Tabla + "/Table>";

            }
            if (matriz == 4)
            {

                webBrowser1.DocumentText = "<br>"+Tabla;

            }
        }

        private void Mostrar_Nombres_Click(object sender, EventArgs e)
        {
            if (i == 2)
            {
                Mostrar_Nombres.Text = "Siguiente Matriz de caminos";
                int[,] auz = new int[tam,tam];
                for (int ii = 0; ii < tam; ii++)
                {
                    for (int j = 0; j < tam; j++)
                    {
                        auz[ii,j] = 0;
                        for (int k = 0; k < tam; k++)
                        {
                            auz[ii,j] = (auz[ii,j] + (m[ii,k] * back[k,j]));
                        }
                    }
                }
                m = auz;
                string cc = converteEnCadena(m);
                webBrowser1.DocumentText = "";
                dameMatriz(cc, 3);
            }
            else
            if (i == 0)
            {
                Mostrar_Nombres.Text = "Ocultar Nombres";
                i = 1;
                f.muestraNombresVertices(1);
            }
            else
            {
                if (i == 1)
                {
                    Mostrar_Nombres.Text = "Mostrar Nombres";
                    i = 0;
                    f.muestraNombresVertices(0);
                }                   
            }            
        }
        public string converteEnCadena(int[,] ma)
        {
            igrado += 1;
            string cad = "<Tr><Td>"+grado+igrado.ToString()+"</Td>";
            char c = 'A';
            for (int a = 0; a < tam; a++)
            {
                cad = cad + "<Td>" + c + "</Td>";
                c++;
            }
            cad = cad + "</Tr>";
            c = 'A';
            for (int i = 0; i < tam; i++)
            {
                cad = "<Tr>" + cad + "<Td>" + c + "</Td>";
                c++;
                for (int j = 0; j < tam; j++)
                {
                    cad = cad + "<Td>" + ma[i, j].ToString() + "</Td>";
                }
                cad = cad + "</Tr>";
            }
            return cad;
        }
        public string converteEnCadena(string[,] ma)
        {
            igrado += 1;
            string cad = "<Tr><Td>" + grado + igrado.ToString() + "</Td>";
            char c = 'A';
            for (int a = 1; a < tam; a++)
            {
                cad = cad + "<Td>" + c + "</Td>";
                c++;
            }
            cad = cad + "</Tr>";
            c = 'A';
            for (int i = 1; i < tam; i++)
            {
                cad = "<Tr>" + cad + "<Td>" + c + "</Td>";
                c++;
                for (int j = 1; j < tam; j++)
                {
                    cad = cad + "<Td>" + ma[i, j].ToString() + "</Td>";
                }
                cad = cad + "</Tr>";
            }
            return cad;
        }
        public string combierteArr(int[] arr,int tam)
        {
            string cad = "";

            for (int a = 1; a < tam+1; a++)
            {
                cad = cad + "<Td>" + arr[i] + "</Td>";
                i++;
            }
            cad = cad + "</Tr>";
            return cad;
        }
    }
}
