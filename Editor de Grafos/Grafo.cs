using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Editor_de_Grafos
{
    [SerializableAttribute]
    class Grafo
    {
        protected List<Vertice> nodos;
        private Font font;
        private char car;
        private int indice;
        public int _indice { get { return indice; } set { indice = _indice; } }
        public int imprimeA;
        public int imprimeNl;
        public bool Dirigido;
        public bool NoDirigido;
        private Color colorG;
        public string topologica;
        private int tiempo;
        private List<Vertice> vR = null;
        private List<Arista> lR = null;
        private List<Arista> Arbol;
        private List<Vertice> ArbolVertices;


        public Grafo()
        {
            nodos = new List<Vertice>();
            imprimeA = 0;
            imprimeNl = 1;//numeros o letras
            font = new Font("Arial", 10);
            colorG = Color.Yellow;
            Dirigido = false;
            NoDirigido = false;
            topologica = "";
        }
        public bool VerticesConAristas()
        {
            foreach (Vertice v in nodos)
            {
                if (v._listaAristas.Count != 0)
                    return true;
            }
            return false;
        }
        public int numeroAristas()
        {
            int num = 0;
            foreach (Vertice v in this.nodos)
            {
                num += v.numAristas();
            }
            return num / 2;
        }
        public virtual void AgregaNodo(Vertice _Nodo)
        {
            nodos.Add(_Nodo);
            setName();
        }
        public virtual List<Vertice> getLista()
        {
            return nodos;
        }
        public void setColorGrafo(Color color)
        {
            colorG = color;
            foreach (Vertice v in this.getLista())
            {
                v.setColor(color);
            }
        }
        public virtual void setName()
        {
            if (imprimeNl == 1)
            {
                car = 'A';
                foreach (Vertice n in nodos)
                {
                    n.NAME = car.ToString();
                    car++;
                }
            }
            else
            {
                indice = 1;
                foreach (Vertice n in nodos)
                {
                    n.NAME = indice.ToString();
                    indice++;
                }
            }
            indice = 0;
            foreach (Vertice n in nodos)
            {
                n.indiceVertice = indice;
                indice++;
            }
        }
        public void muevete(int x1/*Pos del Click*/, int y1, int x2, int y2/*Pos del movimiento del rato*/)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            foreach (Vertice v in this.nodos)
            {
                v.Pc.X += dx;
                v.Pc.Y += dy;
            }
        }
        public void imprime(Graphics g, Grafo activo)
        {
            SolidBrush brocha = new SolidBrush(colorG);
            if (this == activo)
            {
                foreach (Vertice n in nodos)
                {
                    if (imprimeA == 1)//imprime distancia
                        n.imprimeA = 1;
                    else
                        if (imprimeA == 2)//nombre de la arista
                            n.imprimeA = 2;
                        else
                            if (imprimeA == 3)//Peso
                                n.imprimeA = 3;
                            else
                                n.imprimeA = 0;
                    n.ImprimeVertice(g, activo);
                }
            }
            else
            {

                foreach (Vertice n in nodos)
                {
                    n.ImprimeVertice(g, null);
                }
            }
        }
        public void EliminaAristas(Vertice vertice)
        {
            foreach (Vertice n in this.getLista())
            {
                n.EliminaRelacion(vertice);
            }
        }
        public void complementarGrafo(int tipoGrafo)
        {
            foreach (Vertice v in this.nodos)
            {
                v._listaAristas.Clear();
            }
            foreach (Vertice v1 in this.nodos)
            {
                foreach (Vertice v2 in this.nodos)
                {
                    if (v1 != v2)
                    {
                        v1._listaAristas.Add(new Arista(v1, v2, tipoGrafo));
                    }
                }
            }
        }
        public void complementarVertice(Vertice v1, int tipoGrafo)
        {
            foreach (Vertice v2 in this.nodos)
            {
                if (v1 != v2)
                {
                    v1._listaAristas.Add(new Arista(v1, v2, tipoGrafo));
                }
            }
            foreach (Vertice v2 in this.nodos)
            {
                if (v1 != v2)
                {
                    v2._listaAristas.Add(new Arista(v2, v1, tipoGrafo));
                }
            }
        }
        public Arista hayArista(Point position)
        {
            List<Arista> lA = new List<Arista>();
            Arista arista = null;
            foreach (Vertice pV in nodos)
            {
                lA = pV._listaAristas;
                foreach (Arista a in lA)
                {
                    if (a.TocaArista(position))
                    {
                        arista = a;
                    }
                }
            }
            return arista;
        }
        public void elimina_Arista(Arista arista)
        {
            Arista ari1 = null;
            Arista ari2 = null;
            Arista ari3 = null;
            foreach (Vertice vertice in nodos)
            {
                foreach (Arista ari in vertice._listaAristas)
                {
                    if (ari == arista)
                    {
                        ari1 = ari;
                    }
                }
            }
            if (ari1 != null)
            {
                foreach (Arista ari in ari1._Vertice_Origen._listaAristas)
                {
                    if (ari._Vertice_Origen == ari1._Vertice_Origen && ari._Verticeo_Destino == ari1._Verticeo_Destino)
                    {
                        ari3 = ari;
                    }
                }
                foreach (Arista ari in ari1._Verticeo_Destino._listaAristas)
                {
                    if (ari._Vertice_Origen == ari1._Verticeo_Destino && ari._Verticeo_Destino == ari1._Vertice_Origen)
                    {
                        ari2 = ari;
                    }
                }
                foreach (Vertice vertice in nodos)
                {
                    vertice._listaAristas.Remove(ari3);
                    vertice._listaAristas.Remove(ari2);
                }
            }
        }
        public bool tieneCamino()
        {
            int num = 0;
            foreach (Vertice v in this.nodos)
            {
                if (v._listaAristas.Count % 2 != 0)
                {
                    num += 1;
                    if (num == 3)
                        return false;
                }
            }
            if (num == 2)
                return true;
            else
                return false;
        }
        public String calculaEuler(Graphics g)
        {
            Vertice v = nodos[0];
            Arista a = v.dameAristaSinMarca();

            String SubCircuito = "";
            String strCamino = "";

            while (a != null)
            {
                SubCircuito = SubCircuito + (Convert.ToString(v.NAME));
                a._Verticeo_Destino.dameAristaCon(v).camino = true;
                v = a._Verticeo_Destino;
                a.camino = true;
                a = v.dameAristaSinMarca();
                if (a == null)
                {   //Checar
                    SubCircuito = SubCircuito + ", ";
                    SubCircuito = SubCircuito + (Convert.ToString(v.NAME));
                    foreach (Vertice vt in nodos)
                    {
                        a = vt.dameAristaSinMarca();
                        if (a != null)
                        {
                            v = vt;
                            break;
                        }
                    }
                    if (strCamino == "")
                    {
                        strCamino = SubCircuito;
                        SubCircuito = "";
                    }
                    else
                    {
                        for (int i = 0; i < strCamino.Length; i++)
                        {
                            if (strCamino[i] == SubCircuito[1])
                            {

                                SubCircuito = SubCircuito.Remove(1, 3);

                                strCamino = strCamino.Insert(i + 2, SubCircuito);
                                SubCircuito = "";
                                break;
                            }
                        }

                    }
                }
                SubCircuito = SubCircuito + ", ";
            }

            foreach (Vertice vert in nodos)
            {
                List<Arista> aristas;
                aristas = vert._listaAristas;
                foreach (Arista arista in aristas)
                {
                    arista.camino = false;
                }
            }

            return strCamino;
        }
        public bool adyacentesColorDiferente(Vertice v)
        {
            List<Arista> lAdy = v.getListaAristas();
            foreach (Arista a in lAdy)
            {
                if (v.vCrom == a._Verticeo_Destino.vCrom)
                    return false;
            }
            return true;
        }


        private void buscaVerticeOrdenadoYMarcadoD(Vertice v)
        {
            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            foreach (Arista a in listA)
            {
                if (a._Verticeo_Destino.marcado == false)
                {
                    buscaVerticeOrdenadoYMarcadoD(a._Verticeo_Destino);
                }
            }
            v.id2 = tiempo;
            tiempo++;
        }
        public int colorea()
        {
            int var = componentes();
            if (var <= 1)
            {
                int vcrom = 0, color;
                List<Arista> lAdy;
                foreach (Vertice v in this.getLista())
                {
                    lAdy = v.getListaAristas();
                    foreach (Arista a in lAdy)
                    {

                        foreach (Vertice v2 in this.getLista())
                        {
                            if (!v.tieneAristaCon(v2) && v2.vCrom != 0)
                            {
                                vcrom = v2.vCrom;
                                v.vCrom = vcrom;
                                if (adyacentesColorDiferente(v))
                                    break;
                            }
                        }

                        v.vCrom = vcrom;
                        if (!adyacentesColorDiferente(v))
                        {
                            foreach (Arista a2 in lAdy)
                            {
                                if (a2._Verticeo_Destino.vCrom > vcrom)
                                    vcrom = a2._Verticeo_Destino.vCrom;
                            }
                            v.vCrom = vcrom + 1;
                        }
                    }
                }
                foreach (Vertice v in this.getLista())
                {
                    if (vcrom < v.vCrom)
                        vcrom = v.vCrom;
                }
                System.Windows.Forms.MessageBox.Show("El valor Cromatico es: " + Convert.ToString(vcrom));
                foreach (Vertice v in this.getLista())
                {
                    color = 765 / (vcrom + 1);
                    v.setColor(color * v.vCrom);
                }
                return vcrom;
            }
            else
                System.Windows.Forms.MessageBox.Show("Hay " + Convert.ToString(var) + " conjuntos que no se relacion");
            return 0;
        }
        public bool tieneCircuito()
        {
            foreach (Vertice v in this.getLista())
            {
                if (v._listaAristas.Count % 2 != 0)
                {
                    return false;
                }

                if (v._listaAristas.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool esBipartita()
        {
            foreach (Vertice v in this.getLista())
            {
                v.setColor(Color.Yellow);
            }
            //Inicializamos
            foreach (Vertice v in this.getLista())
            {
                v.id2 = 0;
            }
            //Checamos si hay mas de un conjunto
            int var = componentes();
            if (var > 1 || this.getLista().Count == 1)
            {
                System.Windows.Forms.MessageBox.Show("El grafo no esta conectado");
                return false;
            }
            //Inicializamos

            foreach (Vertice v in this.getLista())
            {
                if (v.indiceVertice % 2 == 0)
                    v.id2 = 1;
                else
                    v.id2 = 0;
            }

            List<Arista> lA;
            foreach (Vertice ve in this.getLista())
            {
                lA = ve.getListaAristas();
                foreach (Arista a in lA)
                {

                    if (a._Vertice_Origen.id2 == 2)
                    {
                        a._Verticeo_Destino.id2 = 1;
                    }
                    if (a._Vertice_Origen.id2 == 1)
                    {
                        a._Verticeo_Destino.id2 = 2;
                    }
                }
            }

            //Chequeo de que ninguna arista tenga vertices con mismo id2
            foreach (Vertice vert in this.getLista())
            {
                lA = vert.getListaAristas();
                foreach (Arista a in lA)
                {
                    if (a._Vertice_Origen.id2 == a._Verticeo_Destino.id2)
                    {
                        System.Windows.Forms.MessageBox.Show("No es Bipartita");
                        return false;
                    }
                }
            }

            foreach (Vertice v in this.getLista())
            {
                if (v.id2 == 2)
                    v.setColor(Color.Cyan);

            }

            return true;

        }
        public void compFuertes()
        {
            List<Arista> lA;
            tiempo = 1;
            foreach (Vertice v in this.nodos)
            {
                v.id2 = 0;
                v.marcado = false;
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
            foreach (Vertice v in this.nodos)
            {
                if (v.marcado == false)
                    buscaVerticeOrdenadoYMarcadoD(v);
            }
            List<Arista> lA2 = new List<Arista>();
            foreach (Vertice v in this.nodos)
            {
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    lA2.Add(a);
                }
                lA.Clear();
            }
            foreach (Arista a in lA2)
            {
                this.agregaArista(new Arista(a._Verticeo_Destino, a._Vertice_Origen, 1));
            }
            tiempo = 1;
            foreach (Vertice v in this.nodos)
            {
                v.id3 = 0;
                v.marcado = false;
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
            int componentes = 0;
            int iColor = 30;
            Vertice vert = this.nodos[0];
            while (!hayVerticeSinMArca())
            {
                componentes++;
                iColor = iColor + 80;
                bpf2(dameVertconMayorId2(), iColor);


            }
            System.Windows.Forms.MessageBox.Show("Se encontraron " + Convert.ToString(componentes) + " componenetes Fuertes");
        }
        public bool hayVerticeSinMArca()
        {
            int marcas = 0;
            foreach (Vertice v in nodos)
            {
                if (v.marcado == true)
                    marcas++;
            }
            if (marcas == nodos.Count)
                return true;
            else
                return false;
        }
        public void bpf2(Vertice v, int iColor)
        {
            v.marcado = true;
            v.id3 = tiempo;
            tiempo++;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            foreach (Arista a in listA)
            {
                if (a._Verticeo_Destino.marcado == false)
                {
                    bpf2(a._Verticeo_Destino, iColor);
                }
            }
            v.setColor(iColor);
        }
        public void agregaArista(Arista arista)
        {
            Vertice v1 = arista._Vertice_Origen;
            v1.getListaAristas().Add(arista);
        }
        public void respalda()
        {
            //Resplado vertices
            vR = new List<Vertice>(this.nodos);
            /****Respaldo Arista***************/
            List<Arista> lA;
            lR = new List<Arista>();
            foreach (Vertice v in this.nodos)
            {
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    lR.Add(a);
                }
            }
        }
        public void restaura()
        {
            if (this.nodos.Count != 0 && vR != null)
            {
                this.nodos.Clear();

                List<Arista> lA3;
                //Restauramos Vertices
                foreach (Vertice v in vR)
                {
                    v.setColor(Color.Yellow);
                    lA3 = v.getListaAristas();
                    lA3.Clear();
                    this.nodos.Add(v);
                }
                //Restauramos Aristas
                foreach (Arista a in lR)
                {
                    //Arista arista = new Arista(a.v1, a.v2);
                    //arista.setPeso(a.getPeso());
                    Arista arista = new Arista(a._Vertice_Origen, a._Verticeo_Destino, 1);
                    arista.setPeso(a.getPeso());

                    a._Vertice_Origen.getListaAristas().Add(arista);
                    //this.agregaArista(a);
                }
            }
        }
        public Vertice dameVertconMayorId2()
        {
            Vertice vertice = null;
            if (this.nodos.Count != 0)
            {
                foreach (Vertice v in this.nodos)
                {
                    if (v.marcado == false)
                    {
                        vertice = v;
                        break;
                    }
                }
                foreach (Vertice v in this.nodos)
                {
                    if (v.id2 > vertice.id2 && v.marcado == false)
                        vertice = v;
                }
            }
            return vertice;
        }
        public void DesmarcaAristas()
        {
            foreach (Vertice v in nodos)
            {
                v.desmarcaAristas();
            }
        }        
        List<Arista> ordena(List<Arista> listA)
        {
            Arista ar;
            List<Arista> lA = new List<Arista>();
            while (listA.Count != 0)
            {
                ar = listA[0];
                foreach (Arista a in listA)
                {
                    if (ar.getPeso() > a.getPeso())
                    {
                        ar = a;
                    }
                }
                lA.Add(ar);
                listA.Remove(ar);
            }
            return lA;
        }
        public bool kuratowsky_k33()
        {
            DesmarcaAristas();
            int n = nodos.Count;
            bool exit = false;
            Vertice c1v1, c1v2, c1v3;
            Vertice c2v1, c2v2, c2v3;

            for (int it1 = 0; it1 < n; it1++)
            {
                c1v1 = nodos[it1];
                for (int it2 = 0; it2 < n; it2++)
                {
                    c1v2 = nodos[it2];
                    for (int it3 = 0; it3 < n; it3++)
                    {
                        c1v3 = nodos[it3];
                        for (int cit1 = 0; cit1 < n; cit1++)
                        {
                            c2v1 = nodos[cit1];
                            for (int cit2 = 0; cit2 < n; cit2++)
                            {
                                c2v2 = nodos[cit2];
                                for (int cit3 = 0; cit3 < n; cit3++)
                                {
                                    c2v3 = nodos[cit3];
                                    if (c1v1.numAristas() >= 3 && c1v2.numAristas() >= 3 && c1v3.numAristas() >= 3 && c2v1.numAristas() >= 3 && c2v2.numAristas() >= 3 && c2v3.numAristas() >= 3)
                                        if (MarcaCamino(c1v1, c2v1) && MarcaCamino(c1v1, c2v2) && MarcaCamino(c1v1, c2v3) && MarcaCamino(c1v2, c2v1) && MarcaCamino(c1v2, c2v2) && MarcaCamino(c1v2, c2v3) && MarcaCamino(c1v3, c2v1) && MarcaCamino(c1v3, c2v2) && MarcaCamino(c1v3, c2v3) && c1v1.numMarcas() == 3 && c1v2.numMarcas() == 3 && c1v3.numMarcas() == 3 && c2v1.numMarcas() == 3 && c2v2.numMarcas() == 3 && c2v3.numMarcas() == 3)
                                        {
                                            exit = true;
                                            foreach (Vertice v in nodos)
                                            {

                                                if (v != c1v1 && v != c1v2 && v != c1v3 && v != c2v1 && v != c2v2 && v != c2v3)
                                                {
                                                    if (v.numMarcas() > 2)
                                                        exit = false;
                                                }
                                            }
                                            if (exit)
                                            {
                                                System.Windows.Forms.MessageBox.Show("No plano por K3,3");
                                                return true;
                                            }

                                        }
                                        else
                                            DesmarcaAristas();
                                }

                            }

                        }
                    }
                }
            }
            return false;
        }
        public bool kuratowsky_k5()
        {
            DesmarcaAristas();
            int n = nodos.Count;
            bool exit = false;
            Vertice v1, v2, v3, v4, v5;

            for (int it1 = 0; it1 < n; it1++)
            {
                v1 = nodos[it1];
                for (int it2 = 0; it2 < n; it2++)
                {
                    v2 = nodos[it2];
                    for (int it3 = 0; it3 < n; it3++)
                    {
                        v3 = nodos[it3];
                        for (int it4 = 0; it4 < n; it4++)
                        {
                            v4 = nodos[it4];
                            for (int it5 = 0; it5 < n; it5++)
                            {
                                v5 = nodos[it5];
                                if (v1.numAristas() > 3 && v2.numAristas() > 3 && v3.numAristas() > 3 &&
                                    v4.numAristas() > 3 && v5.numAristas() > 3)
                                {
                                    if (
                                        MarcaCamino(v1, v2) &&
                                        MarcaCamino(v1, v3) &&
                                        MarcaCamino(v1, v4) &&
                                        MarcaCamino(v1, v5) &&
                                        MarcaCamino(v2, v3) &&
                                        MarcaCamino(v2, v4) &&
                                        MarcaCamino(v2, v5) &&
                                        MarcaCamino(v3, v4) &&
                                        MarcaCamino(v3, v5) &&
                                        MarcaCamino(v4, v5) &&

                                        v1.numMarcas() == 4 &&
                                        v2.numMarcas() == 4 &&
                                        v3.numMarcas() == 4 &&
                                        v4.numMarcas() == 4 &&
                                        v5.numMarcas() == 4

                                    )
                                    {
                                        exit = true;
                                        foreach (Vertice v in nodos)
                                        {
                                            if (v != v1 && v != v2 && v != v3 && v != v4 && v != v5)
                                            {
                                                if (v.numMarcas() > 2)
                                                    exit = false;
                                            }
                                        }

                                        if (exit)
                                        {
                                            System.Windows.Forms.MessageBox.Show("No plano por K5");
                                            return true;
                                        }
                                    }
                                    else
                                        DesmarcaAristas();
                                }
                            }

                        }

                    }
                }
            }
            return false;

        }
        public void iniciaGrafo()
        {
            foreach (Vertice v in this.getLista())
            {
                v.marcado = false;
                v.id2 = 0;
                v.id3 = 0;
                foreach (Arista a in v.getListaAristas())
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
        }
        public bool tienePeso()
        {
            List<Arista> lA = new List<Arista>();
            foreach (Vertice v in this.getLista())
            {
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    if (a.getPeso() == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("No todas las aristas tienen peso");
                        return false;
                    }
                }

            }
            return true;
        }
        public void Recorrido_En_Profundidad_R(Vertice v)
        {
            v.marcado = true;
            v.id2 = tiempo;
            tiempo++;

            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            foreach (Arista a in listA)
            {
                if (a._Verticeo_Destino.marcado == false)
                {
                    a.setColor(Color.DodgerBlue);
                    Recorrido_En_Profundidad_R(a._Verticeo_Destino);
                }
            }
            this.topologica = this.topologica + v.NAME + "<-";
        }
        public int Recorrido_En_Profundidad()
        {
            respalda();
            List<Arista> lA;
            int retro = 0;
            tiempo = 0;
            foreach (Vertice v in this.getLista())
            {
                v.id2 = 0;
                v.marcado = false;
                lA = v.getListaAristas();
                foreach (Arista a in lA)
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
            foreach (Vertice v in this.getLista())
            {
                if (v.marcado == false)
                {
                    Recorrido_En_Profundidad_R(v);
                }
            }
            if (this.Dirigido == true)
            {
                //Coloreado de aristas que se eliminarian
                foreach (Vertice v in this.getLista())
                {
                    lA = v.getListaAristas();
                    foreach (Arista a in lA)
                    {
                        //Cruze
                        if (a._Vertice_Origen.id2 >= a._Verticeo_Destino.id2)
                            a.setColor(Color.Red);

                        //Arisas de Avance
                        if (a._Vertice_Origen.id2 <= a._Verticeo_Destino.id2 && a.getColor() == Color.Black)
                            a.setColor(Color.Green);
                    }
                }
                //Aristas de retroceso
                foreach (Vertice v in this.getLista())
                {
                    foreach (Arista a1 in v.getListaAristas())
                    {
                        if (a1._Vertice_Origen.id2 >= a1._Verticeo_Destino.id2 && a1.getColor() == Color.Red)
                        {
                            if (a1._Verticeo_Destino.hayCaminoCon(a1._Vertice_Origen))
                            {
                                a1.setColor(Color.Yellow);
                                retro++;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Vertice v in this.getLista())
                {
                    foreach (Arista a in v._listaAristas)
                    {
                        foreach (Arista va in a._Verticeo_Destino._listaAristas)
                        {
                            if (va._Verticeo_Destino == v)
                            {
                                if (va.getColor() != a.getColor())
                                    va.setColor(Color.DarkBlue);
                            }
                        }
                    }
                }
            }

            return retro;
        }
        public void Recorrido_En_Amplitud()
        {
            List<Arista> listArista = new List<Arista>();
            Vertice vertice = this.getLista()[0];
            vertice.marcado = true;
            foreach (Vertice v in this.getLista())
            {
                foreach (Arista a in v.getListaAristas())
                {
                    if (a._Verticeo_Destino.marcado != true)
                    {
                        a._Verticeo_Destino.marcado = true;
                        foreach (Arista w in a._Verticeo_Destino.getListaAristas())
                        {
                            if (w._Verticeo_Destino == v)
                            {
                                listArista.Add(w);
                            }
                        }
                        listArista.Add(a);
                    }
                }
            }
            foreach (Arista a in listArista)
            {
                a.setColor(Color.Red);
            }
        }


        #region Puntos de Articulacion

        public int Puntos_De_Articulacion()
        {
            tiempo = 1;
            Arbol = new List<Arista>();
            foreach (Vertice v in this.getLista())
            {
                v.id2 = 0;
                v.marcado = false;
                foreach (Arista a in v.getListaAristas())
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
            foreach (Vertice v in this.getLista())
            {
                if (v.marcado == false)
                    Puntos_De_Articulacion_R(v);
            }
            Vertice f;
            int raiz = 0;
            int puntos = 0;
            f = this.getLista()[0];
          /*  foreach (Arista a in f._listaAristas)
            {
                if (a.getColor() == Color.Red)
                {
                    raiz += 1;
                }
            }
            if (raiz >= 2)
            {
                f.setColor(Color.SeaGreen);
                //puntos += 1;
            }*/
            f = null;
            for (int i = 1; i < this.getLista().Count; i++)
            {
                f = this.getLista()[i];
                foreach (Arista a in f._listaAristas)
                {
                    if (a.getColor() == Color.Red)
                    {
                        if (a._Verticeo_Destino.bajo >= f.id2)
                        {
                            f.setColor(Color.SeaGreen);
                            //puntos += 1;
                        }
                    }
                }
            }
            //iniciaGrafo();
            return puntos;
        }
        public void Puntos_De_Articulacion_R(Vertice v)
        {
            v.marcado = true;
            v.id2 = tiempo;
            tiempo++;

            List<Arista> listA = ordena(v.dameRelacionesSM(), 0);
            foreach (Arista a in listA)
            {
                if (a._Verticeo_Destino.marcado == false)
                {
                    Arbol.Add(a);
                    a.setColor(Color.Red);
                    foreach (Arista w in a._Verticeo_Destino.getListaAristas())
                    {
                        if (w._Verticeo_Destino == v)
                        {
                            Arbol.Add(w);
                            w.setColor(Color.Red);
                        }
                    }

                    Puntos_De_Articulacion_R(a._Verticeo_Destino);
                }
            }
            v.bajo = minimo(v);
        }
        public int minimo(Vertice v)
        {
            int numero = v.id2;
            List<int> z = new List<int>();
            List<int> y = new List<int>();

            foreach (Arista a in v.getListaAristas())
            {
                if (a.getColor() == Color.Black)
                {
                    z.Add(a._Verticeo_Destino.id2);
                }
            }
            foreach (Arista a in v.getListaAristas())
            {
                if (a.getColor() == Color.Red)
                {
                    if (a._Verticeo_Destino.id2 > v.id2)
                    {
                        y.Add(a._Verticeo_Destino.bajo);
                    }
                }
            }
            foreach (int i in z)
            {
                if (i < numero)
                    numero = i;
            }
            foreach (int i in y)
            {
                if (i < numero)
                    numero = i;
            }
            return numero;
        }        
        private List<Arista> ordena(List<Arista> listA, int criterio)
        {
            Arista ar;
            List<Arista> lA = new List<Arista>();
            while (listA.Count != 0)
            {
                ar = listA[0];
                foreach (Arista a in listA)
                {
                    if (criterio == 0)
                    {
                        if (ar._Verticeo_Destino.indiceVertice > a._Verticeo_Destino.indiceVertice)
                        {
                            ar = a;
                        }
                    }
                    if (criterio == 1)
                    {
                        if (ar._Verticeo_Destino.id2 < a._Verticeo_Destino.id2)
                        {
                            ar = a;
                        }
                    }
                }
                lA.Add(ar);
                listA.Remove(ar);
            }
            return lA;
        }
        #endregion


        private void buscaVerticeOrdenadoYMarcado(Vertice v)
        {
            v.marcado = true;
            List<Arista> listA = ordena(v.dameRelacionesSM(), 1);
            foreach (Arista a in listA)
            {
                if (a._Verticeo_Destino.marcado == false)
                {
                    buscaVerticeOrdenadoYMarcado(a._Verticeo_Destino);
                }
            }
        }
        public int componentes()
        {
            int res = 0;
            tiempo = 1;
            foreach (Vertice v in this.getLista())
            {
                v.id2 = 0;
                v.marcado = false;
                foreach (Arista a in v.getListaAristas())
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }
            buscaVerticeOrdenadoYMarcadoD(this.getLista()[0]);
            tiempo = 1;
            foreach (Vertice v in this.getLista())
            {
                v.marcado = false;
                foreach (Arista a in v.getListaAristas())
                {
                    a.setColor(Color.Black);
                    a.camino = false;
                }
            }

            foreach (Vertice v in this.getLista())
            {
                if (v.marcado == false)
                {
                    res++;
                    buscaVerticeOrdenadoYMarcado(v);
                }
            }


            foreach (Vertice v in this.getLista())
            {
                v.marcado = false;
            }
            return res;
        }
        public Arista AristaMenorCosto(Vertice v)
        {
            Arista ari = null;
            Arista ari2 = null;
            Vertice ver = null;
            foreach (Arista a in v.getListaAristas())
            {
                if (a._Verticeo_Destino.marcado != true)
                {
                    ari = a;
                    ver = a._Verticeo_Destino;
                }
            }
            foreach (Arista a in v.getListaAristas())
            {
                if (a._Verticeo_Destino.marcado != true)
                {
                    if (a.getPeso() < ari.getPeso())
                    {
                        ari2 = a;
                        ver = a._Verticeo_Destino;
                    }
                }
            }
            if (ver != null)
                ArbolVertices.Add(ver);
            return ari2;
        }
        public void prim()
        {
            List<Arista> listA = new List<Arista>();

            List<Arista> lAux;
            int valido = 0;

            foreach (Vertice v in this.getLista())
            {

                foreach (Arista a in v.getListaAristas())
                {
                    valido++;
                    if (!listA.Contains(a) && a.getPeso() != 0)
                    {
                        listA.Add(a);
                    }
                }
            }
            foreach (Vertice v in this.getLista())
            {
                lAux = v.getListaAristas();
                lAux.Clear();
            }
            foreach (Vertice v in this.getLista())
            {
                v.marcado = false;
            }
            List<Arista> listResult = new List<Arista>();
            while (componentes() > 1 && listA.Count != 0)
            {
                Arista aMenor = listA[0];
                foreach (Arista a in listA)
                {
                    if (aMenor.getPeso() >= a.getPeso())
                    {
                        aMenor = a;
                    }
                }
                aMenor._Vertice_Origen.marcado = true;
                if (!MarcaCamino(aMenor._Verticeo_Destino, aMenor._Vertice_Origen))
                {
                    listResult.Add(aMenor);
                    Arista arista = new Arista(aMenor._Vertice_Origen, aMenor._Verticeo_Destino, 0);
                    agregaArista(arista);
                    arista = new Arista(aMenor._Verticeo_Destino, aMenor._Vertice_Origen, 0);
                    agregaArista(arista);

                }
                listA.Remove(aMenor);
            }
            imprimeA = 0;
        }
        public bool MarcaCamino(Vertice v1, Vertice v2)
        {
            List<Arista> pA = new List<Arista>();
            List<Arista> aA;
            List<Vertice> aV1 = new List<Vertice>();
            List<Vertice> aV2 = new List<Vertice>();
            int watchdog = 0;

            if (nodos.Contains(v1) && nodos.Contains(v2) && v1 != v2)
            {
                aV1.Add(v1);
                while (true)
                {
                    for (int i = 0; i < aV1.Count; i++)
                    {
                        aA = aV1[i].dameRelacionesSM();
                        foreach (Arista a in aA)
                        {
                            if (a._Verticeo_Destino == v2 && a._Verticeo_Destino != v1)
                            {
                                pA.Add(a._Verticeo_Destino.dameAristaCon(aV1[i]));
                                pA.Add(a);
                                v2 = a._Vertice_Origen;
                                aV1.Clear();
                                aV1.Add(v1);
                                aV2.Clear();
                                foreach (Vertice v in aV1)
                                {
                                    aV2.Add(v);
                                }
                                watchdog = 0;
                                break;
                            }
                            aV2.Add(a._Verticeo_Destino);
                        }
                    }
                    aV1.Clear();
                    foreach (Vertice v in aV2)
                    {
                        aV1.Add(v);
                    }
                    aV2.Clear();

                    watchdog++;
                    if (watchdog > 9)
                        break;
                    if (v2 == v1)
                    {
                        foreach (Arista a in pA)
                            a.camino = true;
                        return true;
                    }

                }
            }
            return false;
        }
        public int BuscaVerticeEnComponente(Vertice v, int[,] Componente)
        {
            for (int i = 0; i < this.getLista().Count; i++)
            {
                for (int j = 0; i < this.getLista().Count; i++)
                {
                    if (Componente[i, j] == v.id2)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
        public List<Arista> Quiksort()
        {
            List<Arista> lisa = new List<Arista>();
            foreach (Vertice v in this.getLista())
            {
                if (v.marcado != true)
                {
                    v.marcado = true;
                    foreach (Arista a in v.getListaAristas())
                    {
                        if (a._Verticeo_Destino.marcado != true)
                        {
                            lisa.Add(a);
                        }
                    }
                }
            }
            return lisa;
        }
        public void AcomodaComponentes(int Origen, int Destino, int[,] Componentes)
        {
            int j = 0;
            for (int i = 0; i < this.getLista().Count; i++)
            {
                if (Componentes[Destino, i] == 0)
                {
                    Componentes[Destino, i] = Componentes[Origen, j];
                    j += 1;
                }
            }
            for (int i = 0; i < this.getLista().Count; i++)
            {
                Componentes[Origen, i] = 0;
            }
        }
        private bool mismoComp(List<int>[] C, int u, int v)
        {
            bool value = false;

            for (int i = 0; i < C.Length; i++)
            {
                if (C[i].Count > 1)
                    if (C[i].Contains(u) && C[i].Contains(v))
                    {
                        value = true;
                        break;
                    }
            }
            return value;
        }
        private int enQueComp(List<int>[] C, int v)
        {
            int value = 1;

            for (int i = 0; i < C.Length; i++)
            {
                if (C[i].Contains(v))
                {
                    value = i;
                    break;
                }
            }

            return value;
        }
        public List<Arista> getAristas()
        {
            List<Arista> ColaAristas = Quiksort();
            return ColaAristas;
        }
        public int numAristas()
        {
            int num;
            List<Arista> ColaAristas = Quiksort();
            num = ColaAristas.Count;
            return num;
        }
        public void kruskal()
        {
            List<int>[] C = new List<int>[this.getLista().Count];
            List<Arista> ColaAristas = Quiksort();
            List<Arista> T = new List<Arista>();
            Arista aristaAux;
            int iDest;
            int iOrigen;

            ColaAristas = this.ordena(ColaAristas);

            for (int i = 0; i < C.Length; i++)
            {
                C[i] = new List<int>();
                C[i].Add(i);
            }

            while (T.Count < this.getLista().Count - 1)
            {
                aristaAux = ColaAristas[0];
                ColaAristas.RemoveAt(0);

                if (!this.mismoComp(C, int.Parse(aristaAux._Vertice_Origen.NAME) - 1, int.Parse(aristaAux._Verticeo_Destino.NAME) - 1))
                {
                    T.Add(aristaAux);
                    iOrigen = this.enQueComp(C, int.Parse(aristaAux._Vertice_Origen.NAME) - 1);
                    iDest = this.enQueComp(C, int.Parse(aristaAux._Verticeo_Destino.NAME) - 1);
                    C[iDest].AddRange(C[iOrigen]);
                    C[iOrigen].Clear();
                }
            }
            foreach (Arista a in T)
            {
                a.setColor(Color.Red);
            }
            foreach (Vertice v in this.getLista())
            {
                foreach (Arista a in v._listaAristas)
                {
                    foreach (Arista va in a._Verticeo_Destino._listaAristas)
                    {
                        if (va._Verticeo_Destino == v)
                        {
                            if (va.getColor() != a.getColor())
                                va.setColor(Color.Red);
                        }
                    }
                }
            }
        }
    }
}
