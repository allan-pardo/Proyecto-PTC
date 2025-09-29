using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaNegocio;

namespace CapaPresentasion
{
    public partial class frmRecuperarContraseña : Form
    {
        public frmRecuperarContraseña()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string documento = txtRecuperar.Text.Trim();
            btnRecuperarContra.Enabled = false;
            Cursor = Cursors.WaitCursor;

            var bw = new BackgroundWorker();
            bw.DoWork += (s, ev) =>
            {
                var servicio = new ServicioRecuperacionContraseña();
                ev.Result = servicio.RecuperarContra(documento);  // método SINCRÓNICO
            };
            bw.RunWorkerCompleted += (s, ev) =>
            {
                Cursor = Cursors.Default;
                btnRecuperarContra.Enabled = true;

                if (ev.Error != null)
                    MessageBox.Show("No se pudo completar la solicitud.\n" + ev.Error.Message, "Error");
                else
                    MessageBox.Show((string)ev.Result, "Recuperación");
            };
            bw.RunWorkerAsync();
        
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
