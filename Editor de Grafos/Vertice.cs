using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Editor_de_Grafos
{
    [SerializableAttribute]
    class Vertice
    {
        public Point Pc;
        private string name;
        public string NAME { get { return name; } set { name = value; } }
        public int indiceVertice;
        private List<Arista> listaAristas;
        public List<Arista> _listaAristas { get { return listaAristas; } set { listaAristas = value; } }
        Font font;
        public int vCrom;
        private Color colorV;
        public int imprimeA;
        public bool marcado = false;
        public int id2=0;
        public int id3 = 0;
        public int bajo = 0;

        public Vertice(int x,int y)
        {
            Pc = new Point(x, y);
            font = new Font("Arial", 10);
            colorV = Color.Yellow;
            listaAristas = new List<Arista>();
            imprimeA = 0;
            vCrom = 0;
        }
        public Vertice(Point pos, List<Arista> aristas)
        {
            listaAristas = aristas;
            Pc = pos;
            vCrom=0;
        }
        public Vertice(int x, int y,String nombre, int indice)
        {
            Pc = new Point(x, y);
            this.name = nombre;
            this.indiceVertice = indice;
            font = new Font("Arial", 10);
            colorV = Color.Yellow;
            listaAristas = new List<Arista>();
            imprimeA = 0;
            vCrom = 0;
        }
        public void desmarcaAristas()
        {
            foreach (Arista a in this.getListaAristas())
            {
                a.camino = false;
            }
        }
        public int numAristas()
        {
            return this.getListaAristas().Count;
        }
        public Point getPc()
        {
            return Pc;
        }
        public void ImprimeVertice(Graphics g,Grafo activo)
        {
            SolidBrush brocha = new SolidBrush(colorV);
            g.FillEllipse(brocha, this.getPc().X - 15, this.getPc().Y - 15, 30, 30);
            g.DrawEllipse(Pens.Black, this.getPc().X - 15, this.getPc().Y - 15, 30, 30);
            g.DrawString(this.NAME, font, Brushes.Black, this.getPc().X - 5, this.getPc().Y - 5);
            g.DrawString(bajo.ToString(), font, Brushes.Black, this.getPc().X + 10, this.getPc().Y + 10);
            if(activo!=null)
                g.DrawEllipse(Pens.Black, this.getPc().X - 16, this.getPc().Y - 16, 32, 32);
            
            foreach (Arista ari in this.listaAristas)
            {
                if (imprimeA == 1)
                    ari.imprimeA = 1;
                else
                if (imprimeA == 2)
                    ari.imprimeA = 2;
                else
                if (imprimeA == 3)
                    ari.imprimeA = 3;
                else
                    ari.imprimeA = 0;
                ari.imprimeArista(g);
            }
        }
        public bool tieneAristaCon(Vertice vertice)
        {
            foreach (Arista a in this._listaAristas)
            {
                if (a._Verticeo_Destino == vertice)
                    return true;
            }
            return false;
        }
        public int pesoAristaDe(Vertice vertice)
        {
            foreach (Arista a in this._listaAristas)
            {
                if (a._Verticeo_Destino == vertice)
                    return a.getPeso();
            }
            return 0;
        }
        public Arista dameAristaCon(Vertice vertice)
        {
            foreach (Arista a in this.getListaAristas())
            {
                if (a._Verticeo_Destino == vertice)
                    return a;
            }
            return null;
        }
        public void setColor(int valor)
        {
            int r = valor, g = 0, b = 0;
            if (r >= 200 && r >= 0)
            {
                r = 150;
                g = valor - 200;
                valor = g;
            }
            if (g >= 200 && g >= 0)
            {
                g = 180;
                b = valor - 200;
                valor = b;
            }
            if (b >= 200 && b >= 0)
            {
                b = 200;
            }
            this.colorV = Color.FromArgb(r +50, g +50, b +50);

        }
        public void setColor(Color color)
        {
            this.colorV = color;
        }
        public void EliminaRelacion(Vertice vertice)
        {
            List<Arista> relaciones = new List<Arista>();
            foreach (Arista a in listaAristas)
            {
                if (a._Verticeo_Destino == vertice)
                {
                    relaciones.Add(a);
                }
            }
            foreach (Arista a in relaciones)
                listaAristas.Remove(a);

            relaciones.Clear();
        }
        public Arista dameAristaSinMarca()
        {
            List<Arista> lA = new List<Arista>();
            Arista arista = null;
            foreach (Arista a in this.listaAristas)
            {
                if (!a.camino)
                {
                    lA.Add(a);
                    arista = lA[0];
                }
            }

            if (arista != null)
            {
                foreach (Arista a2 in lA)
                {
                    if (arista._Verticeo_Destino._listaAristas.Count < a2._Verticeo_Destino._listaAristas.Count)
                        arista = a2;
                }
                foreach (Arista a2 in lA)
                {
                    if (arista._Verticeo_Destino.numMarcas() > a2._Verticeo_Destino.numMarcas())
                        arista = a2;
                }
            }
            return arista;
        }
        public int numMarcas()
        {
            int marcas = 0;
            foreach (Arista a in this.getListaAristas())
            {
                if (a.camino)
                    marcas++;
            }
            return marcas;
        }
        public List<Arista> getListaAristas()
        {
            return this.listaAristas;
        }
        public List<Arista> dameRelacionesSM()
        {
            List<Arista> lA = new List<Arista>();
            foreach (Arista a in this.getListaAristas())
            {
                if (a.camino==false)
                    lA.Add(a);
            }
            return lA;
        }

        public bool hayCaminoCon(Vertice v2)
        {
            List<Arista> pA = new List<Arista>();//El resultado esta en pA contiene las aristas que dibujan el camino
            List<Arista> aA; //Auxiliar de aristas para checar por cada vertice
            List<Vertice> aV1 = new List<Vertice>(); //auxiliar de vertices aV1 para comprobar en cada ciclo
            List<Vertice> aV2 = new List<Vertice>(); //Para almacenar los  vertices que pasaran a ser aV1 
            int watchdog = 0;


            List<Arista> auxA = new List<Arista>();

            aV1.Add(this);//Inicializamos con el primer vertice
            while (true)
            {
                //Recorremos la lista de Vertices  que tuvo relacion con el vertice anterior
                for (int i = 0; i < aV1.Count; i++)
                {
                    //Importante liberar esta lista cada ciclo
                    auxA.Clear();
                    //Pedimos relaciones con el vertice y las guardamos en aA
                    aA = aV1[i].dameRelacionesSM();
                    //Seleccionamos solo las del color q nos interesa                
                    foreach (Arista a in aA)
                    {
                        if (a.getColor() == Color.DodgerBlue)
                            auxA.Add(a);
                    }
                    //Y se las asignamos aA y continua todo normalmente
                    aA = auxA;

                    //Recorremos las relaciones por cada vertice 
                    foreach (Arista a in aA)
                    {
                        //Si un vertice a.v2 apunta a un destino
                        //Quiere decir que si se encontro el destino
                        if (a._Verticeo_Destino == v2 && a._Verticeo_Destino != this)
                        {
                            v2 = a._Vertice_Origen;
                            //Limpiamos los datos para evitar que se copie la basura de v2 a v1
                            aV1.Clear();
                            aV1.Add(this);
                            aV2.Clear();
                            //Copiamos aV1 en aV2
                            foreach (Vertice v in aV1)
                            {
                                aV2.Add(v);
                            }
                            ///////////////////
                            watchdog = 0;
                            break;

                        }
                        //Agregamos todos los vertices a aV2 para proximo ciclo
                        aV2.Add(a._Verticeo_Destino);
                    }
                }

                //Limpiamos el auxiliar de lista de vertices1 (aV1)  
                aV1.Clear();
                //Copiamos aV2 en aV1
                foreach (Vertice v in aV2)
                {
                    aV1.Add(v);
                }
                //Y limpiamos aV2
                aV2.Clear();

                watchdog++;
                if (watchdog > 10)
                    break;


                if (v2 == this)
                {
                    return true;
                }


            }
            return false;
        }
    }
}
