using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Editor_de_Grafos
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Graphics pagina;
        private Bitmap bmp;
        private Pen pluma1;
        private Pen pluma2;

        private Vertice VerticeActivo;
        private Grafo grafoActivo;
        private Grafo grafoActivok;
        private Grafo k5;
        private Grafo k33;
        private Arista arista;
        private List<Grafo> ListaGrafos;
        private List<Grafo> ListaGKura;
        int k3;
        
        Color colorFondo = Color.DarkGray;
        int herramienta;
        bool KurActivo = false;
        Point p1, p2;
        int kura;
        string cad;

        Stream myStream;    
        String strArch;
       


        public Form1()
        {
            InitializeComponent();
            pluma1 = new Pen(Color.Black);
            pluma2 = new Pen(Color.Black);
            AdjustableArrowCap acc = new AdjustableArrowCap(5, 5, true);
            pluma2.CustomEndCap = acc;
            ListaGrafos = new List<Grafo>();
            ListaGKura = null;
            p1 = new Point();
            p2 = new Point();
            this.Width = 1600;
            this.Height = 800;
            bmp = new Bitmap(1600, 1024);
            pagina = Graphics.FromImage(bmp);
            grafoActivo = null;
            g = CreateGraphics();
            herramienta = 100;
            VerticeActivo = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EliminarGrafo.Enabled = false;
            Selecciona_Grafo.Enabled = false;
            MueveGrafo.Enabled = false;
            InsertaNodo.Enabled = false;
            EliminaNodo.Enabled = false;
            MueveNodo.Enabled = false;
            InsertaAristaDirigida.Enabled = false;
            InsertaAristaNoDirigida.Enabled = false;
            EliminaArista.Enabled = false;
            Contraccion.Enabled = false;
            SubdivicionG.Enabled = false;
            top.Enabled = false;
            Atras.Enabled = false;
        }
        private void botones()
        {
            Nuevo_Grafo.Enabled = true;
            if (ListaGrafos.Count == 0)
            {
                EliminarGrafo.Enabled = false;                
                InsertaNodo.Enabled = false;
                Selecciona_Grafo.Enabled = false;
                MueveGrafo.Enabled = false;
                EliminaNodo.Enabled = false;
                MueveNodo.Enabled = false;
                InsertaAristaDirigida.Enabled = false;
                InsertaAristaNoDirigida.Enabled = false;
                EliminaArista.Enabled = false;
                Contraccion.Enabled = false;
                top.Enabled = false;
                Atras.Enabled = false;
                SubdivicionG.Enabled = false;
            }
            else
            {
                if (ListaGrafos.Count == 1 || ListaGrafos.Count == 0)
                    Selecciona_Grafo.Enabled = false;
                else
                    Selecciona_Grafo.Enabled = true;
                EliminarGrafo.Enabled = true;
                InsertaNodo.Enabled = true;
                Contraccion.Enabled = false;
                SubdivicionG.Enabled = false;
                if (grafoActivo.getLista().Count == 0)
                {
                    MueveGrafo.Enabled = false;
                    EliminaNodo.Enabled = false;
                    MueveNodo.Enabled = false;
                    InsertaAristaDirigida.Enabled = false;
                    InsertaAristaNoDirigida.Enabled = false;
                    EliminaArista.Enabled = false;
                }
                else
                {
                    MueveGrafo.Enabled = true;
                    EliminaNodo.Enabled = true;
                    MueveNodo.Enabled = true;
                    if (grafoActivo.getLista().Count == 0)
                    {
                        InsertaAristaDirigida.Enabled = false;
                        InsertaAristaNoDirigida.Enabled = false;
                        EliminaArista.Enabled = false;                      
                    }
                    else
                    {
                        if (grafoActivo.Dirigido == false && grafoActivo.NoDirigido == false)
                        {
                            InsertaAristaDirigida.Enabled = true;
                            InsertaAristaNoDirigida.Enabled = true;
                        }
                        else
                        {
                            if (grafoActivo.Dirigido == true)
                            {
                                InsertaAristaDirigida.Enabled = true;
                                InsertaAristaNoDirigida.Enabled = false;
                            }
                            if (grafoActivo.NoDirigido == true)
                            {
                                InsertaAristaDirigida.Enabled = false;
                                InsertaAristaNoDirigida.Enabled = true;
                            }
                        }
                        if (grafoActivo.VerticesConAristas() == true)
                        {
                            EliminaArista.Enabled = true;
                        }
                        else
                        {
                            EliminaArista.Enabled = false;
                        }
                    }
                }
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {           
            if (e.Button == MouseButtons.Left)
            {
                switch(herramienta)
                {
                    case 6:
                        p2 = e.Location;
                        if (VerticeActivo!=null && herramienta == 6)
                        {
                            Cursor = Cursors.SizeAll;
                            VerticeActivo.Pc = e.Location;
                        }
                    break;
                    case 5:
                        if (grafoActivo != null)
                        {
                            if (VerticeActivo != null)
                                grafoActivo.muevete(VerticeActivo.Pc.X, VerticeActivo.Pc.Y, e.X, e.Y);
                        }
                    break;
                }
            }
            Form1_Paint(null, null);
            p2 = e.Location;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pagina.SmoothingMode = SmoothingMode.AntiAlias;
            pagina.Clear(colorFondo);
            if (KurActivo == false)
                imprimeGrafos(pagina);
            else
                grafoActivo.imprime(pagina, grafoActivo);
            
            switch (herramienta)
            { 
                case 1:
                    pagina.DrawEllipse(pluma1, p2.X - 20, p2.Y - 20, 40, 40);
                    break;
                case 3:
                    if (VerticeActivo!=null)
                        pagina.DrawLine(pluma2, VerticeActivo.Pc.X, VerticeActivo.Pc.Y, p2.X, p2.Y);
                    break;
                case 7:
                    if (VerticeActivo!=null)
                        pagina.DrawLine(pluma1, VerticeActivo.Pc.X, VerticeActivo.Pc.Y, p2.X, p2.Y);
                    break;

            }
            if (KurActivo == false)
                botones();
            else
                botonesKuratowsky();
            g.DrawImage(bmp, 0, 0);
        }
        private void imprimeGrafos(Graphics pagina)
        {
            if (ListaGrafos.Count != 0)
            {
                int indic = 1;
                foreach (Grafo n in ListaGrafos)
                {
                    n.imprime(pagina, grafoActivo);
                    n._indice = indic;
                    indic++;
                }
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (herramienta)
                {
                    case 1:
                        if (grafoActivo != null)
                            grafoActivo.AgregaNodo(new Vertice(e.X, e.Y));
                        break;
                    case 2:
                        if (grafoActivo != null)
                        {
                            VerticeActivo = buscaNodo(e);
                            if (VerticeActivo != null)
                            {
                                grafoActivo.EliminaAristas(VerticeActivo);
                                grafoActivo.getLista().Remove(VerticeActivo);
                            }
                            grafoActivo.setName();
                        }
                        break;
                    case 3:
                        if (grafoActivo != null)
                        {
                            VerticeActivo = buscaNodo(e);
                        }
                        break;
                    case 5:
                        if (grafoActivo != null)
                            VerticeActivo = buscaNodo(e);
                        break;
                    case 6:
                        if (grafoActivo != null)
                            VerticeActivo = buscaNodo(e);
                        break;
                    case 7:
                        if (grafoActivo != null)
                            VerticeActivo = buscaNodo(e);
                        break;
                    case 0:
                        grafoActivo = buscaGrafo(e);
                        break;
                    //Elimina Arista
                    case 4:
                        if (grafoActivo != null)
                        {
                            arista = grafoActivo.hayArista(p2);
                            if (arista != null)
                            {
                                grafoActivo.elimina_Arista(arista);
                                if (grafoActivo.VerticesConAristas() != true)
                                {
                                    grafoActivo.NoDirigido = false;
                                    grafoActivo.Dirigido = false;
                                }
                                if (KurActivo == true)
                                agregaKura();
                            }
                            if (KurActivo == true)
                            {
                                if (k3 == 0)
                                    if (isomo(grafoActivo, k5))
                                    {
                                        MessageBox.Show("No plano Isomofrico con K5");
                                    }
                                if (k3 == 1)
                                    if (isomo(grafoActivo, k33))
                                    {
                                        MessageBox.Show("No plano Isomofrico con K33");
                                    }
                            }
                        }
                        break;
                        //Contraccion
                    case 10:                      
                        arista = grafoActivo.hayArista(p2);
                        if (arista != null)
                        { 
                            Vertice nuevov = new Vertice(arista.getPuntoMedio().X, arista.getPuntoMedio().Y);
                            grafoActivo.AgregaNodo(nuevov);
                            if (grafoActivo != null)
                            {
                                Vertice v1 = arista._Vertice_Origen;
                                Vertice v2 = arista._Verticeo_Destino;
                                dirigeAristasSalientes(grafoActivo, arista._Vertice_Origen, arista._Verticeo_Destino, nuevov);
                                dirigeAristasEntrantes(grafoActivo, arista._Vertice_Origen, arista._Verticeo_Destino, nuevov);
                                grafoActivo.EliminaAristas(v1);
                                grafoActivo.getLista().Remove(v1);
                                grafoActivo.EliminaAristas(v2);
                                grafoActivo.getLista().Remove(v2);
                                agregaKura();
                            }
                        }
                        if (KurActivo == true)
                        {
                            if (k3 == 0)
                                if (isomo(grafoActivo, k5))
                                {
                                    MessageBox.Show("No plano Isomofrico con K5");
                                }
                            if (k3 == 1)
                                if (isomo(grafoActivo, k33))
                                {
                                    MessageBox.Show("No plano Isomofrico con K33");
                                }
                        }
                        break;
                    case 11:
                        //Subdivicion
                        arista = grafoActivo.hayArista(p2);
                        if (arista != null)
                        {
                            subdivicion(grafoActivo);
                            agregaKura();
                        }
                        if (KurActivo == true)
                        {
                            if (k3 == 0)
                                if (isomo(grafoActivo, k5))
                                {
                                    MessageBox.Show("No plano Isomofrico con K5");
                                }
                                else
                            if (k3 == 1)
                                if (isomo(grafoActivo, k33))
                                {
                                    MessageBox.Show("No plano Isomofrico con K33");
                                }
                        }
                        break;
                }
            }
            p1 = e.Location;
            Form1_Paint(null, null);
        }
        private void Atras_Click(object sender, EventArgs e)
        {
            if(kura>0)
            {
                ListaGKura.RemoveAt(kura);
                kura -= 1;
                grafoActivo = ListaGKura[kura];
            }
        }
        private void Kuratowskyinteractivo_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                k3 = 0;
                KurActivo = true;
                ListaGKura = new List<Grafo>();
                kura = 0;
                creak5yk33();
                grafoActivok = copiaGrafo(grafoActivo);
                ListaGKura.Add(grafoActivok);
            }
            else
                MessageBox.Show("No hay grafo");
        }
        public void agregaKura()
        {
            kura += 1;
            ListaGKura.Add(copiaGrafo(grafoActivo));
        }
        private void botonesKuratowsky()
        {
            EliminarGrafo.Enabled = false;
            Selecciona_Grafo.Enabled = false;
            EliminaNodo.Enabled = false;
            InsertaAristaDirigida.Enabled = false;
            InsertaAristaNoDirigida.Enabled = false;
            Nuevo_Grafo.Enabled = false;
            InsertaNodo.Enabled = false;
            Contraccion.Enabled = true;
            SubdivicionG.Enabled = true;
            top.Enabled = true;
            Atras.Enabled = true;

        }
        private Grafo copiaGrafo(Grafo g)
        {
            Grafo copiaG = new Grafo();
            foreach (Vertice v in g.getLista())
            {
                Vertice copiaV = new Vertice(v.Pc.X, v.Pc.Y, v.NAME, v.indiceVertice);
                copiaG.getLista().Add(copiaV);
            }

            foreach (Vertice v in g.getLista())
            {
                foreach (Arista a in v.getListaAristas())
                {
                    Vertice verO = regresaV(a._Vertice_Origen, copiaG);
                    Vertice verD = regresaV(a._Verticeo_Destino, copiaG);
                    Arista copiaA = new Arista(verO, verD, a.gettipoArista());
                    verO.getListaAristas().Add(copiaA);
                }
            }
            return copiaG;

        }
        private void subdivicion(Grafo g)
        {
            if (arista != null)
            {
                Vertice nuevov = new Vertice(arista.getPuntoMedio().X, arista.getPuntoMedio().Y);
                int tipo = 0;
                if (g.Dirigido == true)
                    tipo = 1;
                if (g != null)
                {
                    Vertice v1 = arista._Vertice_Origen;
                    Vertice v2 = arista._Verticeo_Destino;
                    Arista a1 = null;
                    Arista a2 = null;
                    foreach (Vertice n in g.getLista())
                    {
                        foreach (Arista a in n.getListaAristas())
                        {
                            if (a._Verticeo_Destino == v1 && a._Vertice_Origen == v2)
                            {
                                nuevov.getListaAristas().Add(new Arista(nuevov, v1, tipo));
                                a1 = a;
                            }
                            if (a._Verticeo_Destino == v2 && a._Vertice_Origen == v1)
                            {
                                nuevov.getListaAristas().Add(new Arista(nuevov, v2, tipo));
                                a2 = a;
                            }
                        }
                    }
                    if (a1 != null)
                        a1._Verticeo_Destino = nuevov;
                    if (a2 != null)
                        a2._Verticeo_Destino = nuevov;
                    g.AgregaNodo(nuevov);
                }
            }
        
        }
        private void dirigeAristasEntrantes(Grafo g, Vertice v1,Vertice v2,Vertice v3)
        {
            foreach (Vertice v in g.getLista())
            {
                foreach (Arista a in v.getListaAristas())
                {
                    if (a._Verticeo_Destino == v1)
                        a._Verticeo_Destino = v3;
                    if (a._Verticeo_Destino == v2)
                        a._Verticeo_Destino = v3;
                }
            }
        }
        private void dirigeAristasSalientes(Grafo g, Vertice v1, Vertice v2, Vertice nuevo)
        {
            int tipo=0;
            if (g.Dirigido == true)
                tipo = 1;
            
            foreach (Vertice v in g.getLista())
            {
                if (v == v1)
                {
                    foreach (Arista a in v.getListaAristas())
                    {
                        if(a._Verticeo_Destino!=v2)
                        nuevo.getListaAristas().Add(new Arista(nuevo,a._Verticeo_Destino,tipo));
                    }
                }
                if (v == v2)
                {
                    foreach (Arista a in v.getListaAristas())
                    {
                        if(a._Verticeo_Destino!=v1)
                            nuevo.getListaAristas().Add(new Arista(nuevo, a._Verticeo_Destino, tipo));
                    }
                }
            }
        }
      
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            p2 = e.Location;
            switch (herramienta)
            {
                case 3:
                    if (VerticeActivo!=null)
                    {
                        foreach (Vertice n in grafoActivo.getLista())
                        {
                            if (e.X < n.getPc().X + 10 && e.X > n.getPc().X - 10 && e.Y < n.getPc().Y + 10 && e.Y > n.getPc().Y - 10)
                            {
                                arista = new Arista(1);
                                arista._Vertice_Origen = VerticeActivo;
                                arista._Verticeo_Destino = n;
                                VerticeActivo._listaAristas.Add(arista);
                            }
                            Form1_Paint(null, null);
                        }
                    }
                    break;
                case 7:
                    if (VerticeActivo!=null)
                    {
                        foreach (Vertice n in grafoActivo.getLista())
                        {
                            if (e.X < n.getPc().X + 10 && e.X > n.getPc().X - 10 && e.Y < n.getPc().Y + 10 && e.Y > n.getPc().Y - 10)
                            {
                                arista = new Arista(0);
                                arista._Vertice_Origen = VerticeActivo;
                                arista._Verticeo_Destino = n;
                                VerticeActivo._listaAristas.Add(arista);
                                arista = new Arista(0);
                                arista._Verticeo_Destino = VerticeActivo;
                                arista._Vertice_Origen = n;
                                n._listaAristas.Add(arista);
                            }
                            Form1_Paint(null, null);
                        }
                    }
                    break;
            }
            
            if (e.Button == MouseButtons.Right)
            {
                
                if (grafoActivo != null)
                {
                    VerticeActivo = buscaNodo(e);
                    if (VerticeActivo != null)
                    {
                        EditaVertice.Show(e.X, e.Y);
                       
                    }
                    arista = grafoActivo.hayArista(p2);
                    if (arista != null)
                    {
                        editaArista.Show(e.X, e.Y);
                    }
                }
            }
            else
            {
                Cursor = Cursors.Arrow;
                if (VerticeActivo != null)
                    VerticeActivo = null;
            }
        }
        private Grafo buscaGrafo(MouseEventArgs e)
        {
            foreach (Grafo g in this.ListaGrafos)
            {
                foreach (Vertice n in g.getLista())
                {
                    if (e.X < n.getPc().X + 15 && e.X > n.getPc().X - 15 && e.Y < n.getPc().Y + 15 && e.Y > n.getPc().Y - 15)
                    {
                        return g;
                    }
                }
            }
            return null;
        }
        private Vertice buscaNodo(MouseEventArgs e)
        {
            foreach (Vertice n in grafoActivo.getLista())
            {
                if (e.X < n.getPc().X + 15 && e.X > n.getPc().X - 15 && e.Y < n.getPc().Y + 15 && e.Y > n.getPc().Y - 15)
                {
                    return n;
                }
            }
            return null;
        }

        private void Herramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Selecciona_Grafo":
                    herramienta = 0;
                    //Cursor = Cursors.Hand;
                    break;
                case "InsertaNodo":
                    herramienta = 1;
                    //Cursor = Cursors.Cross;
                    break;
                case "EliminaNodo":
                    herramienta = 2;
                    break;
                case "InsertaAristaDirigida":
                    herramienta = 3;
                    if (grafoActivo != null)
                    {
                        InsertaAristaNoDirigida.Enabled = false;
                        grafoActivo.Dirigido = true;
                    }
                    break;
                case "EliminaArista":
                    herramienta = 4;
                    break;
                case "MueveGrafo":
                    herramienta = 5;
                    break;
                case "MueveNodo":
                    herramienta = 6;
                    break;
                case "Nuevo_Grafo":
                    if (grafoActivo == null || grafoActivo.getLista().Count != 0)
                    {
                        grafoActivo = new Grafo();
                        ListaGrafos.Add(grafoActivo);
                    }                    
                break;
                case "EliminarGrafo":
                    if (MessageBox.Show("¿Desea Eliminar El Grafo?","Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ListaGrafos.Remove(grafoActivo);
                        if (ListaGrafos.Count != 0)
                        {
                            grafoActivo = ListaGrafos[0];
                        }
                        else
                        {
                            grafoActivo = null;
                        }
                    }
                    Form1_Paint(null, null);
                    break;
                case "InsertaAristaNoDirigida":
                    herramienta = 7;
                    if (grafoActivo != null)
                    {
                        InsertaAristaDirigida.Enabled = false;
                        grafoActivo.NoDirigido = true;
                    }
                    break;
            }
        }     

        public void muestraNombresVertices(int i)
        {
            if(i==1)
                grafoActivo.imprimeA = 2;
            if(i==0)
                grafoActivo.imprimeA = 0;
            Form1_Paint(null,null);
        }

        private int buscaInernos(Vertice v)
        {
            int num = 0;
            foreach(Vertice ver in grafoActivo.getLista())
            {
                foreach (Arista a in ver._listaAristas)
                {
                    if (a._Verticeo_Destino == v)
                        num += 1;
                }            
            }
            return num;
        }

        private void MatrizDeAdyacencia_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                Form2 nuevo = new Form2(this);
                nuevo.Text = "Matriz de Adyacencia";

                string nomVert = "<Tr><Td>\\</Td>";
                foreach (Vertice n in grafoActivo.getLista())
                {
                    nomVert = nomVert + "<Td>" + n.NAME + "</Td>";
                } nomVert = nomVert + "</Tr>";
                string matriz = nomVert;
                for (int i = 0; i < grafoActivo.getLista().Count; i++)
                {
                    Vertice vertice1 = grafoActivo.getLista()[i];
                    matriz = "<Tr>" + matriz + "<Td>" + vertice1.NAME + "</Td>";

                    for (int j = 0; j < grafoActivo.getLista().Count; j++)
                    {
                        Vertice vertice2 = grafoActivo.getLista()[j];

                        if (vertice1.tieneAristaCon(vertice2))
                            matriz = matriz + "<Td>1</Td>";
                        else
                            matriz = matriz + "<Td>0</Td>";
                    }
                    matriz = matriz + "</Tr>";
                }
                nuevo.dameMatriz(matriz, 0);
                nuevo.Show();
            }
        }

        private void ListaDeAdyacencia_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                Form2 nuevo = new Form2(this);
                nuevo.Text = "Lista de Adyacencia";
                string matriz = "";
                foreach (Vertice n in grafoActivo.getLista())
                {
                    matriz = matriz + n.NAME + ":{";
                    foreach (Arista a in n._listaAristas)
                    {
                        matriz = matriz + a._Verticeo_Destino.NAME;
                    }
                    matriz = matriz + "}<dt>";
                }
                nuevo.dameMatriz(matriz, 1);
                nuevo.Show();
            }
        }
        public void daNombres()
        {
            List<Arista> lisV = new List<Arista>();
            List<Arista> lisV2 = new List<Arista>();
            int numero = 1;
            if (grafoActivo.NoDirigido == true)
            {

                foreach (Vertice n in grafoActivo.getLista())
                {
                    foreach (Arista a in n._listaAristas)
                    {
                        a.numero = 0;
                        lisV.Add(a);
                    }
                }
                bool agrega = true;
                foreach (Arista a in lisV)
                {

                    if (lisV2.Count == 0)
                    {
                        lisV2.Add(a);
                    }
                    else
                    {
                        agrega = true;
                        foreach (Arista ari in lisV2)
                        {
                            if (agrega == true)
                            {
                                if (a._Vertice_Origen == ari._Verticeo_Destino)
                                {
                                    if (a._Verticeo_Destino == ari._Vertice_Origen)
                                        agrega = false;
                                }
                                else
                                {
                                    agrega = true;
                                }
                            }
                        }
                        if (agrega == true)
                            lisV2.Add(a);
                    }
                }
                Arista ar = new Arista(0);
                foreach (Arista a in lisV2)
                {
                    a.numero = numero;
                    numero += 1;
                }


            }
            else
            {
                foreach (Vertice n in grafoActivo.getLista())
                {
                    foreach (Arista a in n._listaAristas)
                    {
                        a.numero = 0;
                        lisV.Add(a);
                    }
                }
                foreach (Arista a in lisV)
                {
                    a.numero = numero;
                    numero += 1;
                }



            }
        }

        private void MatrizDeIncidencia_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                int numero = 1;
                string matriz = "";
                List<Arista> lisV = new List<Arista>();
                List<Arista> lisV2 = new List<Arista>();
                if (grafoActivo.NoDirigido == true)
                {

                    foreach (Vertice n in grafoActivo.getLista())
                    {
                        foreach (Arista a in n._listaAristas)
                        {
                            a.numero = 0;
                            lisV.Add(a);
                        }
                    }
                    bool agrega = true;
                    foreach (Arista a in lisV)
                    {

                        if (lisV2.Count == 0)
                        {
                            lisV2.Add(a);
                        }
                        else
                        {
                            agrega = true;
                            foreach (Arista ari in lisV2)
                            {
                                if (agrega == true)
                                {
                                    if (a._Vertice_Origen == ari._Verticeo_Destino)
                                    {
                                        if (a._Verticeo_Destino == ari._Vertice_Origen)
                                            agrega = false;
                                    }
                                    else
                                    {
                                        agrega = true;
                                    }
                                }
                            }
                            if (agrega == true)
                                lisV2.Add(a);
                        }
                    }
                    Arista ar = new Arista(0);
                    foreach (Arista a in lisV2)
                    {
                        a.numero = numero;
                        numero += 1;
                    }

                    string nomVert = "<Tr><Td>\\</Td>";
                    foreach (Arista a in lisV2)
                    {
                        nomVert = nomVert + "<Td>" + "e" + a.numero.ToString() + "</Td>";
                    }
                    nomVert = nomVert + "</Tr>";
                    matriz = nomVert;
                    foreach (Vertice v in this.grafoActivo.getLista())
                    {
                        matriz = "<Tr>" + matriz + "<Td>" + v.NAME + "</Td>";
                        foreach (Arista a in lisV2)
                        {
                            if (a._Vertice_Origen == v || a._Verticeo_Destino == v)
                                matriz = matriz + "<Td>1</Td>";
                            else
                                matriz = matriz + "<Td>0</Td>";
                        }
                        matriz = matriz + "</Tr>";
                    }
                }
                else
                {
                    foreach (Vertice n in grafoActivo.getLista())
                    {
                        foreach (Arista a in n._listaAristas)
                        {
                            a.numero = 0;
                            lisV.Add(a);
                        }
                    }
                    foreach (Arista a in lisV)
                    {
                        a.numero = numero;
                        numero += 1;
                    }
                    string nomVert = "<Tr><Td>\\</Td>";
                    foreach (Arista a in lisV)
                    {
                        nomVert = nomVert + "<Td>" + "e" + a.numero.ToString() + "</Td>";
                    }

                    nomVert = nomVert + "</Tr>";
                    matriz = nomVert;
                    foreach (Vertice v in this.grafoActivo.getLista())
                    {
                        matriz = "<Tr>" + matriz + "<Td>" + v.NAME + "</Td>";
                        foreach (Arista a in lisV)
                        {
                            if (a._Vertice_Origen == v || a._Verticeo_Destino == v)
                                matriz = matriz + "<Td>1</Td>";
                            else
                                matriz = matriz + "<Td>0</Td>";
                        }
                        matriz = matriz + "</Tr>";
                    }
                }
                Form2 nuevo = new Form2(this);
                nuevo.Text = "Matriz de Adyacencia";
                nuevo.dameMatriz(matriz, 0);
                nuevo.Show();
                Form1_Paint(null, null);
            }   
        }

        private void GradosDeLosNodos_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                string cadena = "";
                if (grafoActivo.NoDirigido == true)
                {
                    foreach (Vertice v in grafoActivo.getLista())
                    {
                        cadena = cadena + "deg(" + v.NAME + ")" + "=" + v._listaAristas.Count.ToString() + "<br>";
                    }
                }
                else
                {
                    int internos = 0;
                    foreach (Vertice v in grafoActivo.getLista())
                    {
                        internos = buscaInernos(v);
                        cadena = cadena + "deg+(" + v.NAME + ")" + "=" + v._listaAristas.Count.ToString() + " --> deg-(" + v.NAME + ")" + "=" + internos.ToString() + "<br>";
                    }
                }
                Form2 nuevo = new Form2(this);
                nuevo.Text = "Grado de cada Vertice";
                nuevo.dameMatriz(cadena, 1);
                nuevo.Show();
            }
        }

        private void NumeroDeNodosYAristas_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                int numero1 = 0;
                foreach (Vertice n in grafoActivo.getLista())
                {
                    numero1 += 1;
                }
                MessageBox.Show("Numero de Nodos: " + numero1.ToString());
                int numero = 0;
                int numerOrejas = 0;
                foreach (Vertice n in grafoActivo.getLista())
                {
                    foreach (Arista a in n._listaAristas)
                    {
                        if (a._Vertice_Origen == a._Verticeo_Destino)
                            numerOrejas += 1;
                        else
                            numero += 1;
                    }
                }
                if (grafoActivo.NoDirigido == true)
                {
                    numero = numero / 2;
                    numerOrejas = numerOrejas / 2;
                }
                numero = numero + numerOrejas;
                MessageBox.Show("El numero de aristas es: " + numero.ToString());
            }
        }

        private void ConteoDeCaminosEntreVertices_Click(object sender, EventArgs e)
        {           
            if (grafoActivo != null)
            {
                int[,] ma = convierteEnMatriz(grafoActivo);
                string nomVert = "<Tr><Td>M^1</Td>";
                foreach (Vertice n in grafoActivo.getLista())
                {
                    nomVert = nomVert + "<Td>" + n.NAME + "</Td>";
                } nomVert = nomVert + "</Tr>";
                string matriz = nomVert;
                for (int i = 0; i < grafoActivo.getLista().Count; i++)
                {
                    Vertice vertice1 = grafoActivo.getLista()[i];
                    matriz = "<Tr>" + matriz + "<Td>" + vertice1.NAME + "</Td>";

                    for (int j = 0; j < grafoActivo.getLista().Count; j++)
                    {
                        Vertice vertice2 = grafoActivo.getLista()[j];

                        if (vertice1.tieneAristaCon(vertice2))
                        {
                            ma[i, j] = 0;
                            matriz = matriz + "<Td>1</Td>";
                        }
                        else
                        {
                            ma[i, j] = 1;
                            matriz = matriz + "<Td>0</Td>";
                        }
                    }
                    matriz = matriz + "</Tr>";
                }
                Form2 nuevo = new Form2(this, ma, grafoActivo.getLista().Count);
                nuevo.Text = "Conteo de caminos entre vertices";
                nuevo.dameMatriz(matriz, 3);
                nuevo.Show();
            }
  
        }

        private void Nuevo_Click(object sender, EventArgs e)
        {
            ListaGrafos.Clear();
        }

        private void Abrir_Click(object sender, EventArgs e)
        {            
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            
            if (ListaGrafos.Count != 0)
            {
                if (MessageBox.Show("¿Antes No Desea Guardar el Grafo?", "Confirmación",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Guardar_Click(sender, e);
                }
                ListaGrafos.Clear();
                grafoActivo = null;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            ListaGrafos = (List<Grafo>)formatter.Deserialize(myStream);
                            grafoActivo = ListaGrafos[0];
                            strArch = openFileDialog1.FileName;                           
                            
                            if (grafoActivo != null)
                            {
                                if (grafoActivo.Dirigido == false && grafoActivo.NoDirigido == false)
                                {
                                    InsertaAristaNoDirigida.Enabled = true;
                                    InsertaAristaDirigida.Enabled = true;
                                }
                                else
                                if (grafoActivo.Dirigido == true)
                                {
                                    InsertaAristaNoDirigida.Enabled = false;
                                    InsertaAristaDirigida.Enabled = true;
                                }
                                else
                                if (grafoActivo.NoDirigido == true)
                                {
                                    InsertaAristaNoDirigida.Enabled = true;
                                    InsertaAristaDirigida.Enabled = false;
                                }
                            }
                            Form1_Paint(null, null);                                                      
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void GuardarComo_Click(object sender, EventArgs e)
        {
            if (myStream == null && ListaGrafos.Count == 0)
            {
                Guardar_Click(sender, e);
            }
            if (myStream != null || ListaGrafos.Count != 0)
            {
                myStream = new FileStream(strArch, FileMode.Create);

                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(myStream, ListaGrafos);
                }
                catch (SerializationException se)
                {
                    MessageBox.Show("La Serialización Falló. Motivo: " + se.Message);
                    throw;
                }
                finally
                {
                    myStream.Close();

                }
            }
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            BinaryFormatter formatter = new BinaryFormatter();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    formatter.Serialize(myStream, ListaGrafos);
                    strArch = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }
        }
        private void knm_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(this, 0);
            f.Show();
        }
        public void creaKmn(int n, int m, int tipo)
        {
            List<Vertice> v1 = new List<Vertice>();
            List<Vertice> v2 = new List<Vertice>();
            Grafo nuevo = new Grafo();
            if (tipo == 1)
                nuevo.Dirigido = true;
            else
                nuevo.NoDirigido = true;
            int x = 300;
            int y = 100;
            for (int i = 0; i < n; i++)
            {
                Vertice v = new Vertice(x, y);
                nuevo.getLista().Add(v);
                v1.Add(v);
                x += 100;
            }
            x = 300;
            y = 500;
            for (int i = 0; i < m; i++)
            {
                Vertice v = new Vertice(x, y);
                nuevo.getLista().Add(v);
                v2.Add(v);
                x += 100;
            }
            foreach (Vertice vn in v1)
            {
                foreach (Vertice nm in v2)
                {
                    Arista a = new Arista(vn, nm, tipo);
                    vn.getListaAristas().Add(a);
                }
            }
            foreach (Vertice vn in v2)
            {
                foreach (Vertice nm in v1)
                {
                    Arista a = new Arista(vn, nm, tipo);
                    vn.getListaAristas().Add(a);
                }
            }
            nuevo.setName();
            ListaGrafos.Add(nuevo);
            grafoActivo = nuevo;

        }
        private void kn_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(this, 1);
            f.Show();
        }
        public void creaKn(int n, int tipo)
        {
            int numeroV = n;
            if (numeroV > 1)
            {
                double teta = 0;
                double inc = (360 / numeroV) * Math.PI / 180;

                Grafo nuevo = new Grafo();
                if (tipo == 1)
                    nuevo.Dirigido = true;
                else
                    nuevo.NoDirigido = true;
                for (int i = 0; i < numeroV; i++)
                {
                    float x = this.Size.Width / 2 + (float)((Math.Cos(teta)) * (200));
                    float y = this.Size.Height / 2 + (float)((Math.Sin(teta)) * (200));
                    nuevo.AgregaNodo(new Vertice((int)x, (int)y));
                    teta += inc;
                }
                nuevo.complementarGrafo(tipo);
                ListaGrafos.Add(nuevo);

                grafoActivo = nuevo;
            }
        }
        private void Cn_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(this, 2);
            f.Show();
        }
        public void creaCn(int n, int tipo)
        {
            Vertice v1 = null, v2 = null;
            double teta = 0;
            int numeroV = n;


            if (numeroV > 1)
            {
                double inc = (360 / numeroV) * Math.PI / 180;

                Grafo nuevo = new Grafo();
                if (tipo == 1)
                    nuevo.Dirigido = true;
                else
                    nuevo.NoDirigido = true;

                for (int i = 0; i < numeroV; i++)
                {
                    float x = this.Size.Width / 2 + (float)((Math.Cos(teta)) * (200));
                    float y = this.Size.Height / 2 + (float)((Math.Sin(teta)) * (200));

                    v1 = new Vertice((int)x, (int)y);

                    if (v2 != null)
                    {
                        Arista arista = new Arista(v1, v2, tipo);
                        v1._listaAristas.Add(arista);


                        Arista arista2 = new Arista(v2, v1, tipo);
                        v2._listaAristas.Add(arista2);
                    }

                    nuevo.AgregaNodo(v1);
                    v2 = v1;
                    teta += inc;
                }
                Arista arista3 = new Arista(v1, nuevo.getLista()[0], tipo);
                v1._listaAristas.Add(arista3);

                Arista arista4 = new Arista(nuevo.getLista()[0], v1, tipo);
                nuevo.getLista()[0]._listaAristas.Add(arista4);
                ListaGrafos.Add(nuevo);
                grafoActivo = nuevo;
            }
        }
        private void Wn_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(this, 3);
            f.Show();
        }
        public void creaWn(int n, int tipo)
        {
            Vertice v1 = null, v2 = null;
            double teta = 0;
            int numeroV = n;

            if (numeroV > 1)
            {
                double inc = (360 / numeroV) * Math.PI / 180;
                Grafo nuevo = new Grafo();
                if (tipo == 1)
                    nuevo.Dirigido = true;
                else
                    nuevo.NoDirigido = true;
                for (int i = 0; i < numeroV; i++)
                {
                    float x = this.Size.Width / 2 + (float)((Math.Cos(teta)) * (200));
                    float y = this.Size.Height / 2 + (float)((Math.Sin(teta)) * (200));

                    v1 = new Vertice((int)x, (int)y);

                    if (v2 != null)
                    {
                        Arista arista = new Arista(v1, v2, tipo);
                        v1._listaAristas.Add(arista);


                        Arista arista2 = new Arista(v2, v1, tipo);
                        v2._listaAristas.Add(arista2);
                    }

                    nuevo.AgregaNodo(v1);
                    v2 = v1;
                    teta += inc;
                }
                Arista arista3 = new Arista(v1, nuevo.getLista()[0], tipo);
                v1._listaAristas.Add(arista3);

                Arista arista4 = new Arista(nuevo.getLista()[0], v1, tipo);
                nuevo.getLista()[0]._listaAristas.Add(arista4);

                Vertice vv = new Vertice(this.Size.Width / 2, this.Size.Height / 2);
                nuevo.AgregaNodo(vv);
                nuevo.complementarVertice(vv, tipo);
                ListaGrafos.Add(nuevo);
                grafoActivo = nuevo;
            }
        }
        private void ColorVertice_Click(object sender, EventArgs e)
        {
            if(VerticeActivo != null)
            {
                colorDialog1.ShowDialog();
                this.VerticeActivo.setColor(colorDialog1.Color);
            }
            VerticeActivo = null;
        }
        private int sumaColumna(int[,] matriz,int indice,int tamMatriz)
        {
            int suma = 0;
            for (int i = 0; i < tamMatriz;i++ )
            {
                suma += matriz[indice,i];
            }
                return suma;
        }
        private int sumaRenglon(int[,] matriz, int indice,int tamMatriz)
        {
            int suma = 0;
            for (int i = 0; i < tamMatriz; i++)
            {
                suma += matriz[i, indice];
            }
            return suma;
        }
        public bool comparaMatriz(int[,] matriz1, int[,] matriz2,int tamMatriz)
        {
            for (int i = 0; i < tamMatriz; i++)
            { 
                for(int j=0;j<tamMatriz;j++)
                {
                    if (matriz1[i, j] != matriz2[i, j])
                        return false;
                }
            }
                return true;
        }
        public List<int> sumaDondeHayUnos(int[,] U,int indice,int tamMatriz)
        {
            List<int> suma = new List<int>();
            for (int i = 0; i < tamMatriz; i++)
            {
                if (U[indice, i] != 0)
                {
                    suma.Add(sumaRenglon(U, i, tamMatriz));
                }
                else
                {
                    suma.Add(0);
                }
            }
                return suma;
        }
        public bool comparaListas(List<int> lista1,List<int> lista2,int tam)
        {
            lista1.Sort();
            lista2.Sort();
            for (int i = 0; i < tam; i++)
            {
                if (lista1[i] != lista2[i])
                    return false;
            }

                return true;
        }
        private void intercambiaFilasyColumnas(int[,] matrizGrafo, int indice1, int indice2,int tam)
        { 
            int[] aux = new int[tam];
            for (int i = 0; i < tam; i++)
            {
                aux[i] = matrizGrafo[indice1, i];
                matrizGrafo[indice1, i] = matrizGrafo[indice2, i]; 
                matrizGrafo[indice2, i]= aux[i];
            }
            for (int i = 0; i < tam; i++)
            {
                aux[i] = matrizGrafo[i, indice1];
                matrizGrafo[i, indice1] = matrizGrafo[i, indice2];
                matrizGrafo[i, indice2] = aux[i];
            }

        }
        public bool isomorfismo(int indice1,int indice2)
        {
            if (ListaGrafos[indice1] != null && ListaGrafos[indice2] != null)
            {
                if (ListaGrafos[indice1].getLista().Count == ListaGrafos[indice2].getLista().Count && ListaGrafos[indice1].numeroAristas() == ListaGrafos[indice2].numeroAristas())
                {
                    int tam = ListaGrafos[indice1].getLista().Count;
                    int indiceM1 = 0;
                    int indiceM2 = 1;
                    int[,] matrizGrafo1 = convierteEnMatriz(ListaGrafos[indice1]);
                    int[,] matrizGrafo2 = convierteEnMatriz(ListaGrafos[indice2]);
                    if (comparaMatriz(matrizGrafo1, matrizGrafo2, tam))
                    {
                        return true;
                    }
                    else
                    do
                    {                      
                        if (sumaColumna(matrizGrafo1, indiceM1, tam) == sumaColumna(matrizGrafo2, indiceM2, tam))
                        {

                            if (comparaListas(sumaDondeHayUnos(matrizGrafo1, indiceM1, tam), sumaDondeHayUnos(matrizGrafo2, indiceM2, tam), tam))
                            {
                                intercambiaFilasyColumnas(matrizGrafo2, indiceM1, indiceM2, tam);
                                if (comparaMatriz(matrizGrafo1, matrizGrafo2, tam))
                                {
                                    return true;
                                }
                                else
                                {
                                    indiceM1 += 1;
                                    indiceM2 = indiceM1 + 1;
                                }
                            }
                            else
                                indiceM2 += 1;
                        }
                        else
                            indiceM2 += 1;
                        if (indiceM2 == tam)
                            indiceM2 = 0;
                    } while (indiceM1!=indiceM2);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        private void Isomorfismo_Click(object sender, EventArgs e)
        {
            if(this.ListaGrafos.Count==1)
                MessageBox.Show("No existe grafo con el cual comparar");
            else
                if (this.ListaGrafos.Count == 2)
                { 
                    if (isomorfismo(0,1))
                    {
                        MessageBox.Show("isomorfos");
                    }
                    else
                        MessageBox.Show("No son isomorfos");     
                }
                else
                    if (this.ListaGrafos.Count > 2)
                    {
                        Form3 iso = new Form3(this, "Isomorfismo", ListaGrafos.Count);
                        iso.Show();
                    }
        }
        private bool isomo(Grafo g1,Grafo g2)
        {
            if (g1 != null && g2 != null)
            {
                if (g1.getLista().Count == g2.getLista().Count)
                {
                    int tam = g1.getLista().Count;
                    int indiceM1 = 0;
                    int indiceM2 = 1;
                    int[,] matrizGrafo1 = convierteEnMatriz(g1);
                    int[,] matrizGrafo2 = convierteEnMatriz(g2);
                    if (comparaMatriz(matrizGrafo1, matrizGrafo1, tam))
                    {
                        return true;
                    }
                    else
                        do
                        {
                            if (sumaColumna(matrizGrafo1, indiceM1, tam) == sumaColumna(matrizGrafo2, indiceM2, tam))
                            {

                                if (comparaListas(sumaDondeHayUnos(matrizGrafo1, indiceM1, tam), sumaDondeHayUnos(matrizGrafo2, indiceM2, tam), tam))
                                {
                                    intercambiaFilasyColumnas(matrizGrafo2, indiceM1, indiceM2, tam);
                                    if (comparaMatriz(matrizGrafo1, matrizGrafo1, tam))
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        indiceM1 += 1;
                                        indiceM2 = indiceM1 + 1;
                                    }
                                }
                                else
                                    indiceM2 += 1;
                            }
                            else
                                indiceM2 += 1;
                            if (indiceM2 == tam)
                                indiceM2 = 0;
                        } while (indiceM1 != indiceM2);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        private void CaminosYCircuitosEulerianos_Click(object sender, EventArgs e)
        {
            String camino = "";
            if (grafoActivo.Dirigido == true)
            {
                camino = grafoActivo.calculaEuler(g);
                MessageBox.Show("Camino: " + camino);
            }
            else
                if (grafoActivo.NoDirigido == true)
                {
                    if (grafoActivo != null && grafoActivo.tieneCircuito())
                    {
                        camino = grafoActivo.calculaEuler(g);
                        MessageBox.Show("Camino: " + camino);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No tiene Circuito de Euler");
                        if (grafoActivo != null && grafoActivo.tieneCamino())
                        {

                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("No tiene Camino de Euler");
                        }
                    }
                }
        }
        private void Bipartitas_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null && grafoActivo.esBipartita() )
            {
                MessageBox.Show("Es Bipartita");
                grafoActivo.setColorGrafo(Color.Yellow);
            }
        }
        private void númeroCromático_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
                grafoActivo.colorea();
        }
        private void Qn_Click(object sender, EventArgs e)
        {
            creaQn(1,2);
        }
        public void creaQn(int tipo,int n)
        {
            Grafo nuevo = new Grafo();
            if (tipo == 1)
                nuevo.Dirigido = true;
            else
                nuevo.NoDirigido = true;
            int x = 580;
            int y = 330;
            int tam = 50;
            
            List<Vertice> aux = null;

            List<Vertice> internos = new List<Vertice>();
            List<Vertice> externos = new List<Vertice>();
            creaInterno(internos, nuevo, x, y, tam);
            creaCuboExterno(externos, nuevo, x, y, tam);
            uneCuadros(internos, externos);
           


            this.ListaGrafos.Add(nuevo);
            grafoActivo = nuevo;

        }
        private void complementaG(List<Vertice> lis,Vertice aux)
        {
            Vertice v2 = null;
            foreach (Vertice v in lis)
            {
                if (v2 != null)
                {
                    Arista a1 = new Arista(v, v2, 0);
                    v.getListaAristas().Add(a1);

                    Arista a2 = new Arista(v2, v, 0);
                    v2.getListaAristas().Add(a2);
                }
                v2 = v;
            }

            Arista ari1 = new Arista(aux, v2, 0);
            aux.getListaAristas().Add(ari1);

            Arista ari2 = new Arista(v2, aux, 0);
            v2.getListaAristas().Add(ari2);
        }
        private void uneCuadros(List<Vertice> lis1, List<Vertice> lis2)
        {
            foreach (Vertice v1 in lis1)
            {
                foreach (Vertice v2 in lis2)
                {
                    if (v2.id2 == v1.id2)
                    {
                        Arista a1 = new Arista(v2, v1, 0);
                        v2.getListaAristas().Add(a1);
                    }
                }
            }
        }
        private void creaInterno(List<Vertice> internos, Grafo nuevo, int x, int y, int aumento)
        {
            Vertice v;
            Vertice aux = null;
            for (int j = 1; j < 5; j++)//internos
            {
                v = new Vertice(x, y);
                v.id2 = j;
                internos.Add(v);
                nuevo.AgregaNodo(v);
                if (j == 1)
                    aux = v;
                if (j == 1)
                    x += aumento;
                if (j == 2)
                    y += aumento;
                if (j == 3)
                    x -= aumento;
            }
            complementaG(internos, aux);
            y -= aumento;
        }
        private void creaCuboExterno(List<Vertice> externos, Grafo nuevo, int x, int y, int aumento)
        {
            Vertice v;
            Vertice aux = null;
            int xi, yi;
            xi = 0; yi = 0;
            for (int j = 1; j < 5; j++)//Externos
            {
                if (j == 1)
                {
                    xi = x;
                    yi = y;
                    xi += (aumento / 2);
                    yi -= aumento;
                }
                if (j == 2)
                {
                    xi = x;
                    yi = y;
                    xi += aumento;
                    yi += (aumento / 2);
                }
                if (j == 3)
                {
                    xi = x;
                    yi = y;
                    xi -= (aumento / 2);
                    yi += aumento;
                }
                if (j == 4)
                {
                    xi = x;
                    yi = y;
                    xi -= aumento;
                    yi -= (aumento / 2);
                }
                v = new Vertice(xi, yi);
                v.id2 = j;
                externos.Add(v);
                nuevo.AgregaNodo(v);
                if (j == 1)
                    aux = v;
                if (j == 1)
                    x += aumento;
                if (j == 2)
                    y += aumento;
                if (j == 3)
                    x -= aumento;
            }
            complementaG(externos, aux);
            y -= aumento;
        }
        private void ComponentesFuertementeConectados_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
                grafoActivo.compFuertes();
        }
        public void activaGrafo(int indice)
        { 
            grafoActivo = this.ListaGrafos[indice];
            Form1_Paint(null, null);
        }
        private void Corolarios_Click(object sender, EventArgs e)
        {
           /* if (grafoActivo != null)
            {
                int sumatoriadeg = 0;
                int numero = 0;
                int numerOrejas = 0;
                foreach (Vertice n in grafoActivo.getLista())
                {
                    sumatoriadeg += n.getListaAristas().Count;
                }
                sumatoriadeg = sumatoriadeg / 2;
                foreach (Vertice n in grafoActivo.getLista())
                {
                    foreach (Arista a in n._listaAristas)
                    {
                        if (a._Vertice_Origen == a._Verticeo_Destino)
                            numerOrejas += 1;
                        else
                            numero += 1;
                    }
                }
                if (grafoActivo.NoDirigido == true)
                {
                    numero = numero / 2;
                    numerOrejas = numerOrejas / 2;
                }
                numero = numero + numerOrejas;
                numero = 3 * numero - 3;
                if (sumatoriadeg <= numero)
                {
                    if (circuitos3(grafoActivo))
                    {
                        MessageBox.Show("Es Plano");
                    }
                    else
                    {
                        if (grafoActivo.getLista().Count >= 3)
                        {
                            sumatoriadeg = 0;
                            numero = 0;
                            numerOrejas = 0;
                            foreach (Vertice n in grafoActivo.getLista())
                            {
                                sumatoriadeg += n.getListaAristas().Count;
                            }
                            sumatoriadeg = sumatoriadeg / 2;
                            foreach (Vertice n in grafoActivo.getLista())
                            {
                                foreach (Arista a in n._listaAristas)
                                {
                                    if (a._Vertice_Origen == a._Verticeo_Destino)
                                        numerOrejas += 1;
                                    else
                                        numero += 1;
                                }
                            }
                            if (grafoActivo.NoDirigido == true)
                            {
                                numero = numero / 2;
                                numerOrejas = numerOrejas / 2;
                            }
                            numero = numero + numerOrejas;
                            numero = 2 * numero - 4;
                            if (sumatoriadeg <= numero)
                            {
                                MessageBox.Show("plano");
                            }
                            else
                            {
                                MessageBox.Show("No es plano");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No es plano");
                }
            }*/
            this.checaPlanaridadCorolarios(grafoActivo.getLista().Count, grafoActivo.getAristas().Count/*Aristas*/);
        }
        private void checaPlanaridadCorolarios(int numVertices, int numAristas)
        {
            if (this.corolario1(numVertices, numAristas) == true)
            {

                if (this.corolario2(numVertices, numAristas) == true)
                {
                    MessageBox.Show("El grafo es plano");
                }
                else
                    MessageBox.Show("El grafo no es plano");
            }
            else
                MessageBox.Show("El grafo no es plano");

        }
        private bool corolario1(int numVertices, int numAristas)
        {
            bool isPlano = false;

            if (numAristas <= 3 * (numVertices) - 6)
                isPlano = true;

            return isPlano;
        }
        private bool corolario2(int numVertices, int numAristas)
        {
            bool isPlano = false;

            if (!grafoActivo.tieneCamino() && numAristas > 3)
            {
                if (numAristas <= 2 * (numVertices) - 4)
                    isPlano = true;
            }
            return isPlano;
        }
        private bool circuitos3(Grafo g)
        {
            foreach (Vertice v in g.getLista())
            {
                if (nivel1(v, v))
                    return true;
            }
            return false;
        }
        private bool nivel1(Vertice v, Vertice vertc)
        {
            foreach(Arista a in v.getListaAristas())
            {
                if (nivel2(a._Verticeo_Destino, vertc))
                    return true;
            }
            return false;
        }
        private bool nivel2(Vertice v, Vertice vertc)
        {
            foreach (Arista a in v.getListaAristas())
            {
                if (nivel3(a._Verticeo_Destino, vertc))
                    return true;
            }
            return false;
        }
        private bool nivel3(Vertice v,Vertice vertc)
        {
            foreach (Arista a in v.getListaAristas())
            {
                if (a._Verticeo_Destino == vertc)
                {
                    return true;
                }
            }
            return false;
        }

        private Vertice regresaV(Vertice ver,Grafo g)
        {
            foreach (Vertice v in g.getLista())
            {
                if (v.NAME == ver.NAME)
                {
                    return v;
                }
            }
            return null;
        }

        private void SubdivicionG_Click(object sender, EventArgs e)
        {
            herramienta = 11;
        }

        private void Contraccion_Click(object sender, EventArgs e)
        {
            herramienta = 10;
        }
        private void creak5yk33()
        {
            int numeroV = 5;
            double teta = 0;
            double inc = (360 / numeroV) * Math.PI / 180;
            Grafo g = new Grafo();
            g.NoDirigido = true;
            for (int i = 0; i < numeroV; i++)
            {
                float x1 = this.Size.Width / 2 + (float)((Math.Cos(teta)) * (200));
                float y1 = this.Size.Height / 2 + (float)((Math.Sin(teta)) * (200));
                g.AgregaNodo(new Vertice((int)x1, (int)y1));
                teta += inc;
            }
            g.complementarGrafo(1);
            k5=g;
            List<Vertice> v1 = new List<Vertice>();
            List<Vertice> v2 = new List<Vertice>();
            Grafo nuevo = new Grafo();
            nuevo.NoDirigido = true;
            int x = 300;
            int y = 100;
            for (int i = 0; i < 3; i++)
            {
                Vertice v = new Vertice(x, y);
                nuevo.getLista().Add(v);
                v1.Add(v);
                x += 100;
            }
            x = 300;
            y = 500;
            for (int i = 0; i < 3; i++)
            {
                Vertice v = new Vertice(x, y);
                nuevo.getLista().Add(v);
                v2.Add(v);
                x += 100;
            }
            foreach (Vertice vn in v1)
            {
                foreach (Vertice nm in v2)
                {
                    Arista a = new Arista(vn, nm, 0);
                    vn.getListaAristas().Add(a);
                }
            }
            foreach (Vertice vn in v2)
            {
                foreach (Vertice nm in v1)
                {
                    Arista a = new Arista(vn, nm, 0);
                    vn.getListaAristas().Add(a);
                }
            }
            nuevo.setName();
            k33 = nuevo;
        }

        private void top_Click(object sender, EventArgs e)
        {
            KurActivo = false;
            Contraccion.Enabled = false;
            SubdivicionG.Enabled = false;
            top.Enabled = false;
            Atras.Enabled = false;
            if(ListaGrafos != null)
                if(ListaGrafos.Count!=0)
            grafoActivo = ListaGrafos.Last();
        }

        private void Kuratowskyautomático_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                if (grafoActivo.numeroAristas() == grafoActivo.getLista().Count)
                {
                    MessageBox.Show("Es Plano");
                }
                else
                {
                    if (grafoActivo.kuratowsky_k5())
                    {
                    }
                    else
                        if (grafoActivo.kuratowsky_k33())
                        {
                        }
                        else
                            MessageBox.Show("Es Plano");
                }
            }
            else
                MessageBox.Show("No hay grafo");
        }
        private void teoremaDeLosCuatroColores_Click(object sender, EventArgs e)
        {
            if (grafoActivo.colorea() <= 4)
            {
                MessageBox.Show("El grafo es plano");
            }
            else
            {
                MessageBox.Show("El grafo es No plano");
            }
        }
        private void regiones_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                int del = 0;
                foreach (Vertice v in grafoActivo.getLista())
                {
                    del += v._listaAristas.Count;
                }
                int E = del / 2; 
                int r = E - grafoActivo.getLista().Count +2;
                MessageBox.Show("El numero de regiones es"+r);
            }
        }
        private int[,] convierteEnMatriz(Grafo grafoActual)
        {
            if (grafoActual != null)
            {
                int[,] matriz = new int[grafoActivo.getLista().Count, grafoActivo.getLista().Count];
                for (int i = 0; i < grafoActual.getLista().Count; i++)
                {
                    Vertice vertice1 = grafoActual.getLista()[i];
                    for (int j = 0; j < grafoActual.getLista().Count; j++)
                    {
                        Vertice vertice2 = grafoActual.getLista()[j];

                        if (vertice1.tieneAristaCon(vertice2))
                            matriz[i, j] = 1;
                        else
                            matriz[i, j] = 0;
                    }
                }
                return matriz;
            }
            return null;
        }
        private string[,] convierteEnMatrizPonderada(Grafo grafoActual)
        {
            if (grafoActual != null)
            {
                    int n = grafoActual.getLista().Count + 1;
                    string[,] matriz = null;
                    Arista a = null;

                    int comp = grafoActual.componentes();
                    if (comp == 1)
                    {
                        if (grafoActual.tienePeso())
                        {
                            matriz = new string[n, n];
                            for (int i = 1; i < n; i++)
                            {
                                matriz[i, 0] = grafoActual.getLista()[i - 1].indiceVertice.ToString();
                                for (int j = 1; j < n; j++)
                                {
                                    matriz[0, j] = grafoActual.getLista()[j - 1].indiceVertice.ToString();
                                    if (i == j)
                                        matriz[i, j] = "0";
                                    else
                                    {
                                        a = grafoActual.getLista()[i - 1].dameAristaCon(grafoActual.getLista()[j - 1]);
                                        if (a != null)
                                        {
                                            matriz[i, j] = a.getPeso().ToString();
                                        }
                                        else
                                            matriz[i, j] = "#";
                                    }

                                }
                            }
                        }

                    }
                    else
                        System.Windows.Forms.MessageBox.Show("El grafo no esta conectado");
                    return matriz;
            }
            return null;
        }

        private void caminosMásCortosDesdeUnSoloOrigenDijkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                string[,] matriz = new string[grafoActivo.getLista().Count, grafoActivo.getLista().Count];
                matriz = convierteEnMatrizPonderada(grafoActivo);
                if (matriz != null)
                {
                    double d = Math.Sqrt((double)matriz.LongLength);
                    int a = (int)d;
                    Form5 f = new Form5(this, matriz, a);
                    f.Show();
                }
            }
        }
        public void Dijkstra(string[,] C,int indiceVertice,int tam)
        { 
            List<int> S = new List<int>();
            int[] VS = new int[tam-1];
            string[] D = new string[tam];
            int[] P = new int[tam];
            int[,] R = new int[tam, tam];
            int w;

            S.Add(indiceVertice);
            VmenosS(indiceVertice,VS,tam);
            for (int i = 1; i < tam; i++)
            {
                D[i] = C[indiceVertice, i];
                P[i] = indiceVertice;
            }
            for (int i = 1; i < tam - 1; i++)
            {
                w = VerticeMenorEn(D, VS, tam);
                verticeVS(D, C, P, w, VS, tam,indiceVertice);
                S.Add(w);
                    VS[w] = 0;
            }
            R = imprimeDijkstra(indiceVertice, P,D, tam);
            Form2 f = new Form2(R, tam,indiceVertice);
            f.Show();
        }
        public int[,] imprimeDijkstra(int indiceVertice,int[] P,string[] D,int tam)
        {
            int[,] C = new int[tam,tam];

            for (int i = 1; i < tam; i++)
            {
                if (D[i]!="#")
                C[i, 0] = int.Parse(D[i]);
                imprimeCaminos(indiceVertice, i, P, C, tam);
            }
            return C;
        }
  
        public void verticeVS(string[] D, string[,] C,int[] P,int w,int[] VS,int tam,int indice)
        {
            int V = 0;
            for (int v = 1; v < tam-1; v++)
            {
                if (VS[v] != 0)
                {
                    V = VS[v];
                    if (w != V)
                    {
                        if (minimo(D[V], D[w], C[w, V]))
                        {
                            int iD = int.Parse(D[w]);
                            int iC = int.Parse(C[w, V]);
                            int iDV = iD + iC;
                            D[V] = iDV.ToString();
                            P[V] = w;
                        }
                    }
                }
            }
        }
        public bool minimo(string DV, string DW,string CWV)
        {
            int d,dw, cwv;
            if (DW == "#")
                return false;
            else
                if (CWV == "#")
                    return false;
                else
                {
                    if (DV == "#")
                        return true;
                    else
                    {
                        d = int.Parse(DV);
                        dw = int.Parse(DW);
                        cwv = int.Parse(CWV);
                        if (d > dw + cwv)
                            return true;
                    }
                }
            return false;
        }
        public int VerticeMenorEn(string[] D,int[] VS,int tam)
        {
            int w = 9999;
            int W = 1;

            for (int i = 1; i < tam-1; i++)
            {
                if (D[VS[i]] != "#")
                {
                    if (VS[i]!=0)
                    if (int.Parse(D[VS[i]])!=0)
                        if (int.Parse(D[VS[i]]) < w)
                        {
                            w = int.Parse(D[VS[i]]);
                            W = i;
                        }
                }
            }
            return W;
        }
        public void VmenosS(int indiceVertice,int[] VS,int tam)
        {
            int i = 1;
            int j = 1;
            while (j < tam - 1)
            {
                if (indiceVertice != i)
                {
                    VS[j] = i;
                    j++;
                }
                i++;
            }
        }
        public void imprimeCaminos(int indiceVertice, int destino, int[] P, int[,] C, int tam)
        {
            int j = 1;
            int d = destino;
            C[destino, j] = d;
            do
            {
                j += 1;
                d = buscaDestinos(d, P, tam);
                C[destino, j] = d;
            } while (indiceVertice != d);
        }
        public int buscaDestinos(int destino, int[] P, int tam)
        {
            for (int i = 1; i < tam; i++)
            {
                if (i == destino)
                    return P[i];
            }
            return 0;
        }
        private void pesoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(this);
            f.Show();
        }
        public void insertaPeso(int peso)
        {
            if (arista != null)
            {
                if (grafoActivo.NoDirigido == true)
                {
                    Arista ari = null;
                    foreach (Arista a in arista._Verticeo_Destino.getListaAristas())
                    {
                        if (arista._Vertice_Origen == a._Verticeo_Destino)
                        {
                            ari = a;
                        }
                    }
                    if (ari != null)
                    {
                        ari.setPeso(peso);
                    }
                }
                arista.setPeso(peso);
                grafoActivo.imprimeA = 3;
                this.Form1_Paint(this, null);
            }
        }
        private void Floyd_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                string[,] matriz = new string[grafoActivo.getLista().Count, grafoActivo.getLista().Count];
                int[,] floyd = new int[grafoActivo.getLista().Count+1, grafoActivo.getLista().Count+1];
                int[,] P = new int[grafoActivo.getLista().Count+1, grafoActivo.getLista().Count+1];
                matriz = convierteEnMatrizPonderada(grafoActivo);
                
                if (matriz != null)
                {
                    double d = Math.Sqrt((double)matriz.LongLength);
                    int n = (int)d;
                    cad = "";
                    convierteFloyd(matriz, floyd, P);
                    for (int i = 1; i < n; i++)
                    {
                        cad = cad + "Caminos para: " + i.ToString() + " <br>";
                        for (int j = 1; j < n; j++)
                        {
                            caminos(i, j, P);
                        }
                    }

                    Form2 f = new Form2(cad);
                    f.Show();
                }
            }
        }
        private int[,] convierteFloyd(string[,] matriz, int[,] floyd, int[,] P)
        {
            double d = Math.Sqrt((double)matriz.LongLength);
            int n = (int)d;

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (matriz[i, j] == "#")
                        floyd[i, j] = 999999;
                    else
                        floyd[i, j] = int.Parse(matriz[i, j]);
                    P[i, j] = 0;
                }
            }
            for (int i = 0; i < n; i++)
                floyd[i, i] = 0;

            for (int k = 1; k < n; k++)
            {
                for (int i = 1; i < n; i++)
                {
                    for (int j = 1; j < n; j++)
                    {
                        if (floyd[i, k] + floyd[k, j] < floyd[i, j])
                        {
                            floyd[i, j] = floyd[i, k] + floyd[k, j];
                            P[i, j] = k;
                        }
                    }
                }
            }
            return floyd;
        }
        private void caminos(int origen, int destino,int[,] P)
        {
            if(warshall(origen-1,destino-1) == 1)
            if (origen != destino)
            {
                cad = cad + origen.ToString() + "->";
                caminos_r(origen, destino, P);
                cad = cad + destino.ToString() + "<br>";
            }
        }
        private void caminos_r(int origen, int destino, int[,] P)
        {
            int k = P[origen, destino];
            if (k == 0)
                return;
            else
            {
                caminos_r(origen, k, P);
                cad = cad + k.ToString() + "->";
                caminos_r(k, destino, P);
            }
                
        }
        private void CentroDeUnGrafoDirigido_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                string[,] matriz = new string[grafoActivo.getLista().Count, grafoActivo.getLista().Count];
                int[,] P = new int[grafoActivo.getLista().Count + 1, grafoActivo.getLista().Count + 1];
                int[,] floyd = new int[grafoActivo.getLista().Count + 1, grafoActivo.getLista().Count + 1];
                int[] F = new int[grafoActivo.getLista().Count + 1];
                matriz = convierteEnMatrizPonderada(grafoActivo);
                int col = 0;
                if (matriz != null)
                    floyd = convierteFloyd(matriz, floyd, P);

                if (floyd != null && matriz != null)
                {
                    int n = (int)Math.Sqrt(matriz.Length);
                    for (int i = 1; i < n; i++)
                    {
                        for (int j = 1; j < n; j++)
                        {
                            if (floyd[j, i] > col)
                                col = floyd[j, i];
                        }
                        F[i] = col;
                        col = 0;
                    }
                    col = F[1];
                    int ii = 1;
                    for (int i = 1; i < n; i++)
                    {
                        if (F[i] < col)
                        {
                            ii = i;
                            col = F[i];
                        }
                    }
                    if (col > 9999)
                        System.Windows.Forms.MessageBox.Show("El centro del grafo es " + ii.ToString() + " con  un peso de infinito");
                    else
                        System.Windows.Forms.MessageBox.Show("El centro del grafo es " + ii.ToString() + " con un peso de " + col.ToString());

                }
            }
        }
        private void componentesFuertes_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.compFuertes();
            }
        }
        private void grafosDirigidosAcíclicos_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                if (grafoActivo.Recorrido_En_Profundidad() == 0)
                {
                    MessageBox.Show("Grafo Aaciclico");
                }
                else
                    MessageBox.Show("Grafo No Aciclico");
                grafoActivo.restaura();
            }

        }
        private void bosqueAbarcadorEnProfundidad_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.Recorrido_En_Profundidad();
                Form1_Paint(this,null);
                MessageBox.Show("Arbol: azules, Retroceso: Amarillas, Cruzados: Rojas , Avance: verdes");
            }
        }
        private void Warshall_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                Form5 f = new Form5(this, grafoActivo.getLista().Count);
                f.Show();
            }
        }
        public int warshall(int origen,int destino)
        {
            int[,] warshall = new int[grafoActivo.getLista().Count + 1, grafoActivo.getLista().Count + 1];
            int n = grafoActivo.getLista().Count;

           warshall = convierteEnMatriz(grafoActivo);

            for (int i = 0; i < n; i++)
                warshall[i, i] = 0;

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (warshall[i, j] == 0)
                        {
                            if (warshall[i, k] == 1 && warshall[k, j] == 1)
                                warshall[i, j] = 1; 
                        }
                    }
                }
            }
            if (warshall[origen, destino] == 0)
                return 0;
            else
                return 1;
            /*    MessageBox.Show("No hay camino");
            else
                MessageBox.Show("Si hay camino");*/
        }
        private void pesoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5(this);
            f.Show();
        }
        private void clasificaciónTopológica_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.Recorrido_En_Profundidad();
                grafoActivo.restaura();
                Form2 f = new Form2(this);
                f.dameMatriz(grafoActivo.topologica, 4);
                f.Show();
            }
        }
        private void EditarGrafo_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6(this);
            f.Show();
        }
        public void editaGrafo(int opcion)
        {
            switch (opcion)
            { 
                case 1:
                    grafoActivo.imprimeNl = 2;
                    grafoActivo.setName();
                    break;
                case 2:
                    grafoActivo.imprimeNl = 1;
                    grafoActivo.setName();
                    break;
                case 3:
                    grafoActivo.imprimeA = 1;
                    break;
                case 4:
                    daNombres();
                    grafoActivo.imprimeA = 2;
                    break;
                case 5:
                    grafoActivo.imprimeA = 3;
                    break;
                case 0:
                    grafoActivo.imprimeA = 0;
                    grafoActivo.setName();
                    break;
                case 10:
                       colorDialog1.ShowDialog();
                       this.grafoActivo.setColorGrafo(colorDialog1.Color);
                    break;
            }
            this.Form1_Paint(this,null);
        }
        private void RecorridoEnProfundidadNoDirigidos_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.Recorrido_En_Profundidad();
                Form1_Paint(this, null);
                MessageBox.Show("Arbol: azules, Retroceso: Negro");
            }
        }
        private void RecorridoEnAmplitud_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.Recorrido_En_Amplitud();
                Form1_Paint(this, null);
                MessageBox.Show("Arbol: azules, Retroceso: Negro");
            }
        }
        private void AlgoritmoDePrim_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.prim();
            }
        }
        private void Kruskal_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                grafoActivo.kruskal();
            }
        }
        private void PuntosDeArticulación_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
            {
                if (grafoActivo.Puntos_De_Articulacion() == 0)
                    MessageBox.Show("Grafo Biconexo");
                Form1_Paint(this, null);
                
            }
        }
        private void NumeroCromático_Click(object sender, EventArgs e)
        {
            if (grafoActivo != null)
                grafoActivo.colorea();
        }
        private void teoremaCuatroColores_Click(object sender, EventArgs e)
        {
            if (grafoActivo.colorea() <= 4)
            {
                MessageBox.Show("El grafo es plano");
            }
            else
            {
                MessageBox.Show("El grafo es No plano");
            }
        }

        private void Selecciona_Grafo_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            k3 = 1;
        }

    }
}
