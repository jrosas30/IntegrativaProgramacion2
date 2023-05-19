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

namespace PROG2EVA1juanrosas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] ultimosJugadores = new string[10]; //Arreglo de ultimos 10 jugadores

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string rut = txtRut.Text;
            
            //instancia de la clase ValidacionRut 
            ValidacionRut rutValido = new ValidacionRut();

            rutValido.Rut = rut;
            string devuelveRut = rutValido.Rut.ToString();
            string nivel = comboBox1.Text;
            FormBuscaminas buscaminas;

            
            //validaciones de rut y nivel
            if (rutValido.Rut == "correcto" && nivel == "Facil")
            {
                buscaminas = new FormBuscaminas(10, rut, DateTime.Now);
                buscaminas.ShowDialog();
                GuardarJugador(nombre);
            }
            else if (rutValido.Rut == "correcto" && nivel == "Medio")
            {
                buscaminas = new FormBuscaminas(15, rut, DateTime.Now);
                buscaminas.ShowDialog();
                GuardarJugador(nombre);
            }
            else if (rutValido.Rut == "correcto" && nivel == "Dificil")
            {
                buscaminas = new FormBuscaminas(20, rut, DateTime.Now);
                buscaminas.ShowDialog();
                GuardarJugador(nombre);
            }
            else
            {   
                MessageBox.Show(devuelveRut, "ERROR");
            }

            //muestra ultimos 10 jugadores del arreglo
            string jugadores = "";
            for (int i = 0; i < ultimosJugadores.Length; i++)
            {
                jugadores += ultimosJugadores[i] + "\n";
            }
            lblRes.Text = jugadores;
        }

        //metodo para guardar jugador
        public void GuardarJugador(string nombreJugador)
        {
            for (int i = ultimosJugadores.Length - 1; i > 0; i--)
            {
                ultimosJugadores[i] = ultimosJugadores[i - 1];
            }
            ultimosJugadores[0] = nombreJugador;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"C:\TXTS\VIGIAJUANROSAS.TXT");
            string lectura;
            string[] r;
            string texto;
            lectura = sr.ReadToEnd();
            r = lectura.Split(';');
            texto = "";
            for (int i = 0; i < r.Length; i++)
            {
                texto += $"{r[i]} \n";
            }

            VerDatos verDatos = new VerDatos(texto);
            verDatos.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"C:\TXTS\VIGIAJUANROSAS.TXT");
            string lectura;
            string[] r;
            string texto;
            lectura = sr.ReadToEnd();
            r = lectura.Split(' ');
            texto = "";
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i].Contains(txtRut.Text))
                {
                    texto += $"{r[i]} \n";
                }
            }
        }
    }
}
