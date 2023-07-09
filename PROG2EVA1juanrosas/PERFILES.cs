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

namespace PROG2EVA1juanrosas
{
    public partial class PERFILES : Form
    {
        public PERFILES()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rut = txtRut.Text;
            ValidacionRut validaRut = new ValidacionRut();
            validaRut.Rut = rut;
            string devuelveRut = validaRut.Rut.ToString();

            if (validaRut.Rut == "correcto")
            {
                
                string nombre = txtNombre.Text;
                string apellidoPaterno = txtApPat.Text;
                string apellidoMaterno = txtApMat.Text;

                if (rut == "" || nombre == "" || apellidoPaterno == "" || apellidoMaterno == "" || txtNivel.Text == "")
                {
                    MessageBox.Show("Debes completar todos los campos", "ERROR");
                }
                else
                {

                    int nivel;

                    if (!int.TryParse(txtNivel.Text, out nivel) || txtNivel.Text.Length != 1)
                    {
                        MessageBox.Show("El campo de nivel debe ser un número entero y maximo un caracter.");
                    }
                    else
                    {
                        nivel = int.Parse(txtNivel.Text);
                        string clave = $"{nombre.Substring(0, 1)}{apellidoPaterno.Substring(0, 1)}{apellidoMaterno.Substring(0, 1)}{rut}";

                        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True");
                        con.Open();

                        DataTable datos = new DataTable();
                        string sentencia = $"insert into PERFILESjuanrosas(Rut, Nombre, ApPat, ApMat, Clave, Nivel) " +
                            $"values('{rut}', '{nombre}', '{apellidoPaterno}', '{apellidoMaterno}', '{clave}', {nivel})";
                        SqlDataAdapter adapter = new SqlDataAdapter(sentencia, con);
                        adapter.Fill(datos);
                        con.Close();
                        lblSentencia.Text = sentencia;
                        this.PERFILES_Load(sender, e);
                    }


                }
            }
            else
            {
                MessageBox.Show(devuelveRut, "ERROR");
            }

            

        }

        public void PERFILES_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True");
            con.Open();

            DataTable datos = new DataTable();
            string sentencia = "select * from PERFILESjuanrosas";
            SqlDataAdapter adapter = new SqlDataAdapter(sentencia, con);
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True");
            con.Open();
            string apellidoPaterno = txtApPat.Text;
            DataTable datos = new DataTable();
            string sentencia = $"select * from PERFILESjuanrosas where ApPat='{apellidoPaterno}'";
            SqlDataAdapter adapter = new SqlDataAdapter(sentencia, con);
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True");
            con.Open();
            string rut = txtRut.Text;
            DataTable datos = new DataTable();
            string sentencia = $"delete from PERFILESjuanrosas where Rut='{rut}'";
            SqlDataAdapter adapter = new SqlDataAdapter(sentencia, con);
            adapter.Fill(datos);
            con.Close();
            this.PERFILES_Load(sender, e);
            txtRut.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {

            dataGridView1.Visible = false;
            dataGridView2.Visible = true;

            string rutaArchivo = @"C:\TXTS\VIGIAJUANROSAS.TXT";

            // Se crea el objeto sr que instancia la clase StreamReader para leer el archivo de texto.
            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string lectura = sr.ReadToEnd();
                string[] lineas = lectura.Split(';');

                // Se crea una conexión a la base de datos y se abre
                using (SqlConnection conexion = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True"))
                {
                    conexion.Open();

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
                                                    $"VALUES ('{clave}', '{inicioSesion}', '{finSesion}', '{Accion}', '{AccionF}')";

                            // Se crea un SqlCommand para ejecutar la consulta INSERT
                            using (SqlCommand comandoInsert = new SqlCommand(consultaInsert, conexion))
                            {
                                // Se ejecuta la consulta INSERT
                                comandoInsert.ExecuteNonQuery();
                            }
                        }
                    }

                    // Después de insertar los datos, se ejecuta una consulta SELECT para obtener los datos de la tabla
                    string consultaSelect = "SELECT * FROM ACCIONESjuanrosas";

                    // Se crea un SqlDataAdapter para obtener los datos de la consulta SELECT
                    using (SqlDataAdapter adapter = new SqlDataAdapter(consultaSelect, conexion))
                    {
                        // Se crea un DataTable para almacenar los resultados de la consulta
                        DataTable tablaDatos = new DataTable();

                        // Se llena el DataTable con los datos del SqlDataAdapter
                        adapter.Fill(tablaDatos);

                        // Se muestra el DataTable en el DataGridView
                        dataGridView2.DataSource = tablaDatos;
                    }
                }
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            string rutBuscado = txtRut.Text.Trim();

            if (!string.IsNullOrEmpty(rutBuscado))
            {
                string ruta = @"C:\TXTS\VIGIAJUANROSAS.TXT";
                string[] lineas = File.ReadAllLines(ruta);

                using (SqlConnection conexion = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\dreik\\source\\repos\\PROG2EVA1juanrosas\\PROG2EVA1juanrosas\\BDDPROG2juanrosas.mdf;Integrated Security=True"))
                {
                    //Se abre
                    conexion.Open();

                    foreach (string linea in lineas)
                    {
                        string[] campos = linea.Split(',');

                        if (campos.Length >= 1 && campos[0] == rutBuscado)
                        {
                            string clave = campos[0];
                            string inicioSesion = campos[1];
                            string finSesion = campos[2];
                            string accion = campos[3];
                            string accionF = campos[4];

                            string consultaInsert = $"INSERT INTO ACCIONESjuanrosas (Clave, InicioSesion, FinSesion, Accion, AccionF) " +
                                                    $"VALUES ('{clave}', '{inicioSesion}', '{finSesion}', '{accion}', '{accionF}')";

                            using (SqlCommand comandoInsert = new SqlCommand(consultaInsert, conexion))
                            {
                                comandoInsert.ExecuteNonQuery();
                            }
                        }
                    }

                    // Después de insertar los datos, se ejecuta una consulta SELECT para obtener los datos de la tabla
                    string consultaSelect = "SELECT * FROM ACCIONESjuanrosas";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(consultaSelect, conexion))
                    {
                        // Se crea un DataTable para almacenar los resultados de la consulta
                        DataTable tablaDatos = new DataTable();

                        // Se llena el DataTable con los datos del SqlDataAdapter
                        adapter.Fill(tablaDatos);

                        // Se muestra el DataTable en el DataGridView
                        dataGridView2.DataSource = tablaDatos;
                    }
                }

                MessageBox.Show("Registros almacenados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ingrese un rut válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Ocultar el DataGridView1 y mostrar el DataGridView2
            dataGridView1.Visible=false;
            dataGridView2.Visible=true;

            // Establecer la cadena de conexión a la base de datos
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\basesLeones\\BDDPROG2juanrosas.mdf;Integrated Security=True");
            // Establecer la cadena de conexión a la base de datos
            con.Open();

            DataTable datos = new DataTable();

            // Definir la consulta SQL
            string sentencia = "select * from ACCIONESjuanrosas";
            // Crear un SqlDataAdapter y ejecutar la consulta para llenar el DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(sentencia, con);
            adapter.Fill(datos);
            //Asignar el DataTable como origen de datos del DataGridView2
            dataGridView2.DataSource = datos;

            // Cerrar la conexión a la base de datos
            con.Close();
        }
    }
}
