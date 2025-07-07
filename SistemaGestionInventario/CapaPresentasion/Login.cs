using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SistemaEntidades;
using SistemaNegocio;

namespace CapaPresentasion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            List<Usuario> TEST = new SN_Usuario().lister();

            Usuario ousuario = new SN_Usuario().lister().Where(u => u.documento == txtNoDocumento.Text && u.clave == txtClave.Text).FirstOrDefault();

            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            
        }
        private void frm_closing(object sender, FormClosingEventArgs e) {

            txtNoDocumento.Text = "";
            txtClave.Text = "";
            this.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
