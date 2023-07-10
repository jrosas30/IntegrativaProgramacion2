using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PROG2EVA1juanrosas
{
    public partial class FormAcciones : Form
    {
        public FormAcciones()
        {
            InitializeComponent();
        }

        private void FormAcciones_Load(object sender, EventArgs e)
        {
            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            using (MySqlConnection con = new MySqlConnection(conexion))
            {
                // Realizar la consulta SELECT para obtener los datos de la tabla
                string consultaSelect = "SELECT * FROM ACCIONESjuanrosas";

                // Crear un MySqlDataAdapter para obtener los datos de la consulta SELECT
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSelect, con))
                {
                    // Crear un DataTable para almacenar los resultados de la consulta
                    DataTable tablaDatos = new DataTable();

                    // Llenar el DataTable con los datos del MySqlDataAdapter
                    adapter.Fill(tablaDatos);

                    // Mostrar el DataTable en el DataGridView
                    dataGridView1.DataSource = tablaDatos;
                }
            }
        }

        private void btnTraspasarTabla_Click(object sender, EventArgs e)
        {
            string rutaArchivo = @"C:\TXTS\VIGIAJUANROSAS.TXT";

            // Se crea la conexión a la base de datos
            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            using (MySqlConnection con = new MySqlConnection(conexion))
            {
                con.Open();

                // Se crea el objeto sr que instancia la clase StreamReader para leer el archivo de texto.
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string lectura = sr.ReadToEnd();
                    string[] lineas = lectura.Split(';');

                    // Se recorre cada línea del archivo
                    foreach (string linea in lineas)
                    {
                        string[] datos = linea.Split(',');

                        // Se verifica que la matriz contenga suficientes elementos antes de acceder a ellos
                        if (datos.Length >= 5)
                        {
                            string clave = datos[0];
                            string inicioSesion = datos[1];
                            string finSesion = datos[2];
                            string Accion = datos[3];
                            string AccionF = datos[4];

                            // Se construye la consulta SQL INSERT
                            string consultaInsert = $"INSERT INTO ACCIONESjuanrosas (Clave, InicioSesion, FinSesion, Accion, AccionF) " +
                                                    $"VALUES (@clave, @inicioSesion, @finSesion, @Accion, @AccionF)";

                            // Se crea un MySqlCommand para ejecutar la consulta INSERT
                            using (MySqlCommand comandoInsert = new MySqlCommand(consultaInsert, con))
                            {
                                // Se asignan los valores a los parámetros de la consulta
                                comandoInsert.Parameters.AddWithValue("@clave", clave);
                                comandoInsert.Parameters.AddWithValue("@inicioSesion", inicioSesion);
                                comandoInsert.Parameters.AddWithValue("@finSesion", finSesion);
                                comandoInsert.Parameters.AddWithValue("@Accion", Accion);
                                comandoInsert.Parameters.AddWithValue("@AccionF", AccionF);

                                // Se ejecuta la consulta INSERT
                                comandoInsert.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Después de insertar los datos, se ejecuta una consulta SELECT para obtener los datos de la tabla
                string consultaSelect = "SELECT * FROM ACCIONESjuanrosas";

                // Se crea un MySqlDataAdapter para obtener los datos de la consulta SELECT
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultaSelect, con))
                {
                    // Se crea un DataTable para almacenar los resultados de la consulta
                    DataTable tablaDatos = new DataTable();

                    // Se llena el DataTable con los datos del MySqlDataAdapter
                    adapter.Fill(tablaDatos);

                    // Se muestra el DataTable en el DataGridView
                    dataGridView1.DataSource = tablaDatos;
                }

                con.Close();
            }
        }

        
    }
}

