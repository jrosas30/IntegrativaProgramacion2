using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        List<CLASEEVALUA2juanRosas> lista;

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string rut = txtRut.Text;
            
            //instancia de la clase ValidacionRut 
            ValidacionRut rutValido = new ValidacionRut();

            rutValido.Rut = rut;
            string devuelveRut = rutValido.Rut.ToString();
            string nivel = comboBox1.Text;

            //validaciones de rut y nivel
            if (rutValido.Rut == "correcto" && nivel == "Facil")
            {
                FormBuscaminas buscaminas = new FormBuscaminas(10);
                buscaminas.ShowDialog();
                GuardarJugador(nombre);
            }
            else if (rutValido.Rut == "correcto" && nivel == "Medio")
            {
                FormBuscaminas buscaminas = new FormBuscaminas(15);
                buscaminas.ShowDialog();
                GuardarJugador(nombre);
            }
            else if (rutValido.Rut == "correcto" && nivel == "Dificil")
            {
                FormBuscaminas buscaminas = new FormBuscaminas(20);
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


    }
}
