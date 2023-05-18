using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace PROG2EVA1juanrosas
{
    public partial class FormBuscaminas : Form
    {
        PictureBox[,] matriz = new PictureBox[30,30];
        int n;

        int TamanoMatriz1 = 10;
        int TamanoMatriz2 = 15;
        int TamanoMatriz3 = 20;

        public FormBuscaminas()
        {

        }

        public FormBuscaminas(int minas)
        {
            InitializeComponent();
 
            if (minas == 10)
            {
                crearMatriz(TamanoMatriz1, minas);
                colocarMinas(minas);
                this.n = TamanoMatriz1;
            }
            else if(minas == 15)
            {
                crearMatriz(TamanoMatriz2, minas);
                colocarMinas(minas);
                this.n = TamanoMatriz2;
            }
            else
            {
                crearMatriz(TamanoMatriz3, minas);
                colocarMinas(minas);
                this.n = TamanoMatriz3;
            }    
        }

        private void FormBuscaminas_Load(object sender, EventArgs e)
        {
            
        }

        public void crearMatriz(int d, int m)
        {       
            Random rnd = new Random();
            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < d; j++)
                {
                    matriz[i, j] = new PictureBox();
                    matriz[i, j].BackColor = Color.LightGray;
                    matriz[i, j].Width = 35;
                    matriz[i, j].Height = 35;
                    matriz[i, j].Location = new Point(40 * i, 40 * j);
                    matriz[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    matriz[i, j].Tag = 0; /*rnd.Next(0, d); */// se crean numeros aleatorios y son agregados a la matriz
                    matriz[i, j].MouseClick += new MouseEventHandler(hacer_click);
                    this.Controls.Add(matriz[i, j]);
                    panel1.Controls.Add(matriz[i, j]); // en esta linea se agrega la Matriz dentro del panel1
                }
            }
        }

        public void hacer_click(object sender, EventArgs e)
        {
            PictureBox picBox = (PictureBox)sender;
            abrirCasilla(picBox);
        }

        public void colocarMinas(int m)
        {
            Random rnd = new Random();
            int contador = 0;
            while (contador < m)
            {
                int filas = rnd.Next(0, m);
                int columnas = rnd.Next(0, m);
                if (matriz[filas, columnas].Tag.ToString() == "0")
                {
                    matriz[filas, columnas].Tag = "1";
                    contador++;
                }
            }
        }

        //Metodo para abrir casilla
        public void abrirCasilla(PictureBox picBox)
        {

            if (picBox.Tag.ToString() == "0")
            {
                int numero = contarMinasCerca(picBox); 
                if (numero > 0)
                {
                    if(numero == 1)
                    {
                        picBox.Image = Properties.Resources.c1;
                    }
                    else if(numero == 2)
                    {
                        picBox.Image = Properties.Resources.c2;
                    }
                    else if(numero == 3)
                    {
                        picBox.Image = Properties.Resources.c3;
                    }
                    else
                    {
                        picBox.Image = Properties.Resources.c4;
                    }
                }
                else
                {
                    picBox.BackColor = Color.White;
                }
            }
            else
            {
                picBox.Image = Properties.Resources.minas;
                MessageBox.Show("¡Perdiste! Haz clic en 'Aceptar' para reiniciar el juego.");
                ReiniciarJuego();
            }
        }


        public int contarMinasCerca(PictureBox picBox)
        {
            int contador = 0;
            int x = picBox.Location.X / 40;
            int y = picBox.Location.Y / 40;
            for (int i = Math.Max(0, x - 1); i <= Math.Min(n - 1, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(n - 1, y + 1); j++)
                {
                    if (matriz[i, j].Tag.ToString() == "1")
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }


        public void ReiniciarJuego()
        {
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {

        }
    }
}
