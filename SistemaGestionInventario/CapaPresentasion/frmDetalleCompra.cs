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
using SistemaDatos;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;

namespace CapaPresentasion
{
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new SN_Compra().ObtenerCompra(txtBusqueda.Text);

            if (oCompra.idCompra != 0)
            {

                txtNumDocumento.Text = oCompra.numeroDocumento;

                txtFecha.Text = oCompra.fechaRegistro;
                txtTipoDocumento.Text = oCompra.tipoDocumento;
                txtUsuario.Text = oCompra.oUsuario.nombreCompleto;
                txtDocProveedor.Text = oCompra.oProovedor.documento;
                txtRazonSocial.Text = oCompra.oProovedor.razonSocial;

                dgvData.Rows.Clear();
                foreach (Detalle_Compra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[] { dc.oProducto.nombre, dc.precioCompra, dc.cantidad, dc.montoTotal });
                }

                txtMontoTotal.Text = oCompra.montoTotal.ToString("0.00");

            }

        }

        private void btnBorrarBusqueda_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocProveedor.Text = "";
            txtRazonSocial.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void frmDetalleCompra_Load(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && txtBusqueda.Text.Length >= 50)

            { e.Handled = true; }
        }

        private void txtMontoTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtMontoTotal.Text.Length >= 8)
            {
                e.Handled = true;
            }
        }
    }
}
