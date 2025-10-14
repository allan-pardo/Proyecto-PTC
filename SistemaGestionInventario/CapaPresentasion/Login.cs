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
using SistemaDatos;
using SistemaEntidades;
using SistemaNegocio;

namespace CapaPresentasion
{
    public partial class Login : Form
    {
        bool mostrarRegistro = true;
        bool mostrandoRegistro = true; // inicia en Registro
        int targetLeft = 0;
        const int paso = 14;

        public Login()
        {
            InitializeComponent();

        }

        private void btnMouseEnter (object sender, EventArgs e) 
        {
            ((Button)sender).ForeColor = Color.White;
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.Black;
        }

        private void txtEnter(object sender, EventArgs e)
        {
            TextBox tx = sender as TextBox;
            foreach (Control ctr in pRegistro.Controls)
            {
                if (ctr is Panel && ctr.Name == "p" + tx.Tag.ToString())
                {
                    ctr.BackColor = Color.FromArgb(255, 136, 136);
                }
            }
        }
        private void txtLeave(object sender, EventArgs e)
        {
            TextBox tx = sender as TextBox;
            foreach (Control ctr in pRegistro.Controls)
            {
                if (ctr is Panel && ctr.Name == "p" + tx.Tag.ToString())
                {
                    
                    ctr.BackColor = Color.Purple;
                }
            }
        }



        private void frm_closing(object sender, FormClosingEventArgs e) {

            txtNoDocumento.Text = "";
            txtClave.Text = "";
            this.Show();
        }

        private void ConfigurarRegistroExterno()
        {
            bool hayUsuarios = new SN_Usuario().HayUsuarios();

            if (hayUsuarios)
            {
                pRegistro.Visible = false;      
                pRegistro.Enabled = false;
                btnRegistrarse.Visible = false; 
                btnRegistrarse.Enabled = false;

                
                StartSlide(false);               
                pLogin.BringToFront();
            }
            else
            {
                
                pRegistro.Visible = true;
                pRegistro.Enabled = true;
                btnRegistrarse.Visible = true;
                btnRegistrarse.Enabled = true;

                StartSlide(true);                
                pRegistro.BringToFront();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ConfigurarRegistroExterno();
            pPrincipal.AutoScroll = false;

            // iguala anchos al viewport
            int viewportW = pPrincipal.ClientSize.Width;
            int viewportH = pPrincipal.ClientSize.Height;

            pRegistro.Size = new Size(325, 400);
            pLogin.Size = new Size(325, 400);

            // colocar en línea: Registro (izq), Login (der)
            pRegistro.Location = new Point(0, 1);
            pLogin.Location = new Point(pRegistro.Width, 0);

            // contenedor abarca ambos
            pContenedor.Size = new Size(pRegistro.Width + pLogin.Width, 400);
            pContenedor.Location = new Point(0, 0);   // ← muestra REGISTRO

            // suavizado opcional
            this.DoubleBuffered = true;
            pContenedor.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(pContenedor, true, null);

            // estado inicial
            mostrarRegistro = true;
            targetLeft = 0;
        }

        private void StartSlide(bool irARegistro)
        {
            // destino: 0 = Registro; -pRegistro.Width = Login
            targetLeft = irARegistro ? 0 : -pRegistro.Width;
            mostrarRegistro = irARegistro;

            if (irARegistro) pRegistro.BringToFront(); else pLogin.BringToFront();
            timer1.Start();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            int dir = (targetLeft > pContenedor.Left) ? 1 : -1; // +1 o -1
            pContenedor.Left += dir * paso;

            bool llego = (dir < 0 && pContenedor.Left <= targetLeft) ||
                         (dir > 0 && pContenedor.Left >= targetLeft);

            if (llego)
            {
                pContenedor.Left = targetLeft;
                timer1.Stop();
            }
        }

        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            StartSlide(true);
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            StartSlide(false);
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string doc = txtNoDocumento.Text.Trim();
            string pass = txtClave.Text; // sin Trim

            if (string.IsNullOrWhiteSpace(doc) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Ingresa documento y contraseña.");
                return;
            }

            var sn = new SN_Usuario();
            var u = sn.Login(doc, pass);   // hace BCrypt.Verify por dentro

            if (u != null)
            {
                var frm = new Inicio(u);      
                frm.FormClosing += frm_closing;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Documento o contraseña incorrectos.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnOlviCuenta_Click_1(object sender, EventArgs e)
        {
            using (var modal = new frmRecuperarContraseña())
            {
                var result = modal.ShowDialog();

            }
        }

        private void txtNoDocumento_TextChanged(object sender, EventArgs e)
        {

        }
        private const int MAX_DOC = 8;

        private void txtNoDocumento_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar) && txtNoDocumento.Text.Length >= MAX_DOC)
                e.Handled = true;
        }

        private void txtNoDocumento_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtClave_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (new SN_Usuario().HayUsuarios())
            {
                MessageBox.Show("El registro externo está deshabilitado porque ya existe un usuario.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartSlide(false); // vuelve a login
                return;
            }

            string Mensaje = string.Empty;

            
            var objUsuario = new Usuario
            {
                idUsuario = 0,
                nombreCompleto = txtNombreReg.Text.Trim(),
                documento = txtDocumentoReg.Text.Trim(),
                correo = txtCorreoReg.Text.Trim(),
                clave = txtClaveReg.Text,    
                oRol = new Rol { idRol = 1 },
                estado = true
            };

            
            if (objUsuario.oRol.idRol == 0)
                objUsuario.oRol.idRol = new SN_Rol().EnsureRol("Administrador"); 

            
            int idGenerado = new SN_Usuario().Registrar(objUsuario, out Mensaje);

            if (idGenerado > 0)
            {
                
                objUsuario.idUsuario = idGenerado;

                
                MessageBox.Show("Usuario creado. Ya puedes iniciar sesión.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                txtNoDocumento.Text = objUsuario.documento;
                StartSlide(irARegistro: false); // mostrar LOGIN
                txtClave.Focus();

                
                txtNombreReg.Clear();
                txtDocumentoReg.Clear();
                txtCorreoReg.Clear();
                txtClaveReg.Clear();
            }
            else
            {
                MessageBox.Show(Mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
