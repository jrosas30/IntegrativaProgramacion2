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
    public partial class VerDatos : Form
    {
        
        public VerDatos(string texto)
        {
            InitializeComponent();
            label1.Text = texto;
        }

        
        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
