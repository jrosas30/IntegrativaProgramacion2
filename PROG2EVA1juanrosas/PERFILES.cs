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
    public partial class PERFILES : Form
    {
        public PERFILES()
        {
            InitializeComponent();

        }

        public void PERFILES_Load(object sender, EventArgs e)
        {
            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            MySqlConnection con = new MySqlConnection(conexion);
            con.Open();
            DataTable datos = new DataTable();
            string sentencia = "select * from PERFILESjuanrosas";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sentencia, conexion);
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            con.Close();
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

                if (rut == "" || nombre == "" || apellidoPaterno == "" || apellidoMaterno == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Debes completar todos los campos", "ERROR");
                }
                else
                {

                    int nivel;

                    if (!int.TryParse(comboBox1.Text, out nivel) || comboBox1.Text.Length != 1)
                    {
                        MessageBox.Show("El campo de nivel debe ser un número entero y maximo un caracter.");
                    }
                    else
                    {
                        nivel = int.Parse(comboBox1.Text);
                        string clave = $"{nombre.Substring(0, 1)}{apellidoPaterno.Substring(0, 1)}{apellidoMaterno.Substring(0, 1)}{rut}";

                        string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
                        MySqlConnection con = new MySqlConnection(conexion);
                        con.Open();
                        DataTable datos = new DataTable();
                        string sentencia = $"insert into PERFILESjuanrosas(Rut, Nombre, ApPat, ApMat, Clave, Nivel) " +
                            $"values('{rut}', '{nombre}', '{apellidoPaterno}', '{apellidoMaterno}', '{clave}', {nivel})";
                        MySqlDataAdapter adapter = new MySqlDataAdapter(sentencia, conexion);
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



        private void button2_Click(object sender, EventArgs e)
        {
            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            MySqlConnection con = new MySqlConnection(conexion);
            con.Open();
            string clave = txtClave.Text;
            string apellidoPaterno = txtApPat.Text;
            DataTable datos = new DataTable();
            string sentencia = $"select * from PERFILESjuanrosas where clave='{clave}' OR ApPat LIKE '%{apellidoPaterno}%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sentencia, conexion);
            adapter.Fill(datos);
            dataGridView1.DataSource = datos;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string rut = txtRut.Text;

            if (string.IsNullOrWhiteSpace(rut))
            {
                MessageBox.Show("Debe ingresar un valor de rut.", "Error");
                return; // Salir del método sin realizar la eliminación
            }

            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            using (MySqlConnection con = new MySqlConnection(conexion))
            {
                con.Open();

                // Verificar si el rut existe en la base de datos
                string verificacion = "SELECT COUNT(*) FROM PERFILESjuanrosas WHERE rut = @rut";
                using (MySqlCommand cmdVerificacion = new MySqlCommand(verificacion, con))
                {
                    cmdVerificacion.Parameters.AddWithValue("@rut", rut);
                    int count = Convert.ToInt32(cmdVerificacion.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("El rut no existe en la base de datos.", "Error");
                        return; // Salir del método sin realizar la eliminación
                    }
                }

                // Realizar la eliminación del registro
                string sentencia = $"DELETE FROM PERFILESjuanrosas WHERE rut = @rut";
                using (MySqlCommand cmd = new MySqlCommand(sentencia, con))
                {
                    cmd.Parameters.AddWithValue("@rut", rut);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro eliminado correctamente.", "Éxito");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro.", "Error");
                    }
                }

                con.Close();
            }

            this.PERFILES_Load(sender, e);
            txtRut.Text = "";
        }


        private void button4_Click(object sender, EventArgs e)
        {

            


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
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string rut = txtRut.Text;
            string nombre = txtNombre.Text;
            string ApellidoPat = txtApPat.Text;
            string ApellidoMat = txtApMat.Text;
            string nivel = comboBox1.Text;
            string clave = txtClave.Text;

            if (string.IsNullOrWhiteSpace(rut))
            {
                MessageBox.Show("El campo 'Rut' no puede estar vacío.", "Error");
                return; // Salir del método sin continuar con la actualización en la base de datos
            }

            string conexion = "Server=127.0.0.1;User=root;Database=programacion;password=''";
            using (MySqlConnection con = new MySqlConnection(conexion))
            {
                con.Open();

                // Verificar si el rut existe en la base de datos
                string verificacion = "SELECT COUNT(*) FROM PERFILESjuanrosas WHERE rut = @rut";
                using (MySqlCommand cmdVerificacion = new MySqlCommand(verificacion, con))
                {
                    cmdVerificacion.Parameters.AddWithValue("@rut", rut);
                    int count = Convert.ToInt32(cmdVerificacion.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("El rut no existe en la base de datos.", "Error");
                        return; // Salir del método sin continuar con la actualización en la base de datos
                    }
                }

                // Actualizar el registro en la base de datos solo si hay datos para actualizar
                if (!string.IsNullOrWhiteSpace(nombre) || !string.IsNullOrWhiteSpace(ApellidoPat) ||
                    !string.IsNullOrWhiteSpace(ApellidoMat) || !string.IsNullOrWhiteSpace(nivel))
                {
                    string sentencia = "UPDATE PERFILESjuanrosas SET ";

                    if (!string.IsNullOrWhiteSpace(nombre))
                        sentencia += "nombre = @nombre, ";
                    if (!string.IsNullOrWhiteSpace(ApellidoPat))
                        sentencia += "apPat = @apPat, ";
                    if (!string.IsNullOrWhiteSpace(ApellidoMat))
                        sentencia += "apMat = @apMat, ";
                    if (!string.IsNullOrWhiteSpace(nivel))
                        sentencia += "nivel = @nivel, ";

                    // Eliminar la última coma y espacio en blanco
                    sentencia = sentencia.TrimEnd(',', ' ');

                    sentencia += " WHERE rut = @rut";

                    using (MySqlCommand cmd = new MySqlCommand(sentencia, con))
                    {
                        if (!string.IsNullOrWhiteSpace(nombre))
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                        if (!string.IsNullOrWhiteSpace(ApellidoPat))
                            cmd.Parameters.AddWithValue("@apPat", ApellidoPat);
                        if (!string.IsNullOrWhiteSpace(ApellidoMat))
                            cmd.Parameters.AddWithValue("@apMat", ApellidoMat);
                        if (!string.IsNullOrWhiteSpace(nivel))
                            cmd.Parameters.AddWithValue("@nivel", nivel);
                        cmd.Parameters.AddWithValue("@rut", rut);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro actualizado correctamente.", "Éxito");
                            this.PERFILES_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar el registro.", "Error");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se proporcionaron datos para actualizar.", "Advertencia");
                }

                con.Close();
            }
        }

    }
}
