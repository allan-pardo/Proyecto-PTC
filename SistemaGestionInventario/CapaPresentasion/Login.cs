using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

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
            var cn = new SN_Usuario();
            var ousuario = cn.Login(txtNoDocumento.Text.Trim(), txtClave.Text.Trim());

            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);
                form.FormClosing += frm_closing;
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("no se encontro el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void txtNoDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtNoDocumento.Text.Length >= 6)
            {
                e.Handled = true;
            }

        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true; 
            }
        }

        private void txtNoDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnIngresarLogin_Click(object sender, EventArgs e)
        {

        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOlviCuenta_Click(object sender, EventArgs e)
        {
            using (var modal = new frmRecuperarContraseña())
            {
                var result = modal.ShowDialog();

            }
        }
    }
}
