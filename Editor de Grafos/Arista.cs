using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Editor_de_Grafos
{
    [SerializableAttribute] 
    class Arista
    {
        float CatOx1, CatAy1;
        float CatOx2, CatAy2;       
        private Vertice Vertice_Origen;
        public Vertice _Vertice_Origen { get { return Vertice_Origen; } set { Vertice_Origen = value; } }
        private Vertice Vertice_Destino;
        public Vertice _Verticeo_Destino { get { return Vertice_Destino; } set { Vertice_Destino = value; } }
        private Color colorA;
        public bool camino;
        private int aristaTipo; 
        private int distancia;
        private int peso;
        public int numero;
        public Point PuntoMedio;
        public int imprimeA;

        public Arista(int TipoArista) 
        {           
            numero = 0;
            imprimeA = 0;
            peso = 0;
            colorA = Color.Black;
            aristaTipo = TipoArista;
            camino = false;
        }
        public int gettipoArista()
        {
            return aristaTipo;
        }
        public int getPeso()
        {
            return peso;
        }
        
        public Arista(Vertice Origen, Vertice Destino, int TipoArista)
        {
            numero = 0;
            imprimeA = 0;
            peso = 0;
            colorA = Color.Black;
            Vertice_Origen = Origen;
            Vertice_Destino = Destino;
            aristaTipo = TipoArista;
            camino = false;
        }
        public Color getColor()
        {
            return this.colorA;
        }
        public Point getPuntoMedio()
        {
            return PuntoMedio;
        }
        public void calculaPuntoMedio()
        {
            PuntoMedio = new Point((Vertice_Origen.Pc.X + Vertice_Destino.Pc.X) / 2, (Vertice_Origen.Pc.Y + Vertice_Destino.Pc.Y) / 2);
        }
        
        public void calculaPeso()
        {
            if (Vertice_Origen == Vertice_Destino)
            {
                distancia = 0;
            }
            else
            {
                distancia = (int)Math.Sqrt(Math.Pow(Vertice_Destino.Pc.X - Vertice_Origen.Pc.X, 2) + Math.Pow(Vertice_Destino.Pc.Y - Vertice_Origen.Pc.Y, 2));
            }
        }
        public void Calcula_Flecha()
        {
            //origen
            double teta1 = Math.Atan2(Vertice_Destino.Pc.Y - Vertice_Origen.Pc.Y, Vertice_Destino.Pc.X - Vertice_Origen.Pc.X);
            CatOx1 = Vertice_Origen.Pc.X + (float)((Math.Cos(teta1)) * (50 / 2));
            CatAy1 = Vertice_Origen.Pc.Y + (float)((Math.Sin(teta1)) * (50 / 2));
            //Destino
            double teta2 = Math.Atan2(Vertice_Origen.Pc.Y - Vertice_Destino.Pc.Y, Vertice_Origen.Pc.X - Vertice_Destino.Pc.X);
            CatOx2 = Vertice_Destino.Pc.X + (float)((Math.Cos(teta2)) * (50 / 2));
            CatAy2 = Vertice_Destino.Pc.Y + (float)((Math.Sin(teta2)) * (50 / 2));
        }
        public  void imprimeArista(Graphics g)
        {
            Pen pluma = new Pen(colorA);
            if (aristaTipo == 1)
            {
                AdjustableArrowCap acc = new AdjustableArrowCap(5, 5, true);
                pluma.CustomEndCap = acc;
            }
            Calcula_Flecha();
            calculaPuntoMedio();
            calculaPeso();
            

            if (Vertice_Destino == Vertice_Origen)
            {
                g.DrawBezier(pluma, CatOx1- 10, CatAy1- 15, CatOx1 + 20, CatAy1 - 50, CatOx1 - 50, CatAy1 - 50, CatOx2- 25, CatAy2- 10);  
                if(imprimeA==1)
                    g.DrawString(this.distancia.ToString(), new Font("Arial", 10), new SolidBrush(colorA), this.Vertice_Destino.Pc.X - 7, this.Vertice_Destino.Pc.Y - 50);
                if(imprimeA==2)
                    if(numero!=0)
                    g.DrawString("e" + this.numero.ToString(), new Font("Arial", 10), new SolidBrush(colorA), this.Vertice_Destino.Pc.X - 7, this.Vertice_Destino.Pc.Y - 50);
                if (imprimeA == 3)
                    g.DrawString(this.peso.ToString(), new Font("Arial", 10), new SolidBrush(colorA), this.Vertice_Destino.Pc.X - 7, this.Vertice_Destino.Pc.Y - 50);
            }
            else
            {
                g.DrawLine(pluma,CatOx1,CatAy1,CatOx2,CatAy2);
                //dibujaLineaCurva(pluma, g, (int)CatOx1, (int)CatAy1, (int)CatOx2, (int)CatAy2);
                if (imprimeA == 1)
                    g.DrawString(this.distancia.ToString(), new Font("Arial", 10), new SolidBrush(colorA),this.PuntoMedio);
                if(imprimeA==2)
                    if (numero != 0)
                    g.DrawString("e" + this.numero.ToString(), new Font("Arial", 10), new SolidBrush(colorA), this.PuntoMedio);
                if (imprimeA == 3)
                    g.DrawString(this.peso.ToString(), new Font("Arial", 10), new SolidBrush(colorA), this.PuntoMedio);
            }
        }
        private void dibujaLineaCurva(Pen pluma,Graphics g,int x1,int y1,int x2,int y2)
        {
            /*Point point1 = new Point(x1, y1);
            Point point2 = new Point(x1, y1);
            //Point point3 = new Point(x1, y1);
            Point point4 = new Point(x2, y2);
            Point point5 = new Point(x2, y2);
            Point[] curvePoints = { point1, point2, /*point3, point4,point5 };
            g.DrawCurve(pluma, curvePoints);*/
            g.DrawBezier(pluma, x1, y1, x1, y1+40, x2, y2+40, x2, y2);  
            //g.DrawBezier(
        
        }
        private void DrawCurve(Pen pluma,Graphics g, int x, int y)
        {
            pluma.Color = Color.Red;
            Point point1 = new Point(x - 10, y - 10);
            Point point2 = new Point(x - 15, y - 23);
            Point point3 = new Point(x, y - 40);
            Point point4 = new Point(x + 15, y - 23);
            Point point5 = new Point(x + 10, y - 10);
            Point[] curvePoints = { point1, point2, point3, point4, point5 };
            g.DrawCurve(pluma, curvePoints);
            /*
            pluma.Color = Color.Red;
            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x1, y1);
            Point point3 = new Point(x1, y1);
            Point point4 = new Point(x2, y2);
            Point point5 = new Point(x2, y2);
            Point[] curvePoints = { point1, point2, point3, point4, point5 };
            g.DrawCurve(pluma, curvePoints);
             */
        }

        public void setPeso(int p)
        {
            peso = p;
        }

        public void setColor(Color c)
        {
            colorA = c;
        }

        public bool TocaArista(Point pos)
        {
            Rectangle mouse = new Rectangle(pos.X, pos.Y, 3, 3);
            Rectangle pix = new Rectangle(this.Vertice_Origen.Pc.X, this.Vertice_Origen.Pc.Y, 1, 1);

            int x0 = this.Vertice_Origen.Pc.X;
            int y0 = this.Vertice_Origen.Pc.Y;
            int x1 = this.Vertice_Destino.Pc.X;
            int y1 = this.Vertice_Destino.Pc.Y;

            int dx = this.Vertice_Destino.Pc.X - this.Vertice_Origen.Pc.X;
            int dy = this.Vertice_Destino.Pc.Y - this.Vertice_Origen.Pc.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            { 
                float m = (float)dy / (float)dx;
                float b = y0 - m * x0;
                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = (int)Math.Round(m * x0 + b);
                    pix.X = x0;
                    pix.Y = y0;

                    if (mouse.IntersectsWith(pix))
                        return true;
                }
            }
            else
                if (dy != 0)
                {                             
                    float m = (float)dx / (float)dy;  
                    float b = x0 - m * y0;
                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = (int)Math.Round(m * y0 + b);
                        pix.X = x0;
                        pix.Y = y0;

                        if (mouse.IntersectsWith(pix))
                            return true;
                    }
                }
            
            return false;
        }
    }
}
