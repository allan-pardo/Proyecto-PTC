using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentasion.Utilidades;
using SistemaEntidades;
using SistemaNegocio;

namespace CapaPresentasion
{
    public partial class frmReporteCompras : Form
    {
        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {
            txtFechaFin.MaxDate = DateTime.Today;

            List<Proovedor> lista = new SN_Proovedor().Listar();

            cboProveedor.Items.Add(new opcionCombo() { valor = 0, texto = "TODOS" });
            foreach (Proovedor item in lista)
            {
                cboProveedor.Items.Add(new opcionCombo() { valor = item.idProovedor, texto = item.razonSocial });
            }
            cboProveedor.DisplayMember = "Texto";
            cboProveedor.ValueMember = "Valor";
            cboProveedor.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new opcionCombo() { valor = columna.Name, texto = columna.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscarResultado_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((opcionCombo)cboProveedor.SelectedItem).valor.ToString());

            List<ReporteCompra> lista = new List<ReporteCompra>();

            lista = new SN_Reporte().Compra(
                txtFechaInicio.Value.ToString(),
                txtFechaFin.Value.ToString(),
                idproveedor
                );


            dgvData.Rows.Clear();

            foreach (ReporteCompra rc in lista)
            {
                dgvData.Rows.Add(new object[] {
                    rc.fechaRegistro,
                    rc.tipoDocumento,
                    rc.numeroDocumento,
                    rc.montoTotal,
                    rc.usuarioRegistro,
                    rc.documentoProveedor,
                    rc.razonSocial,
                    rc.codigoProducto,
                    rc.nombreProducto,
                    rc.categoria,
                    rc.precioCompra,
                    rc.precioVenta,
                    rc.cantidad,
                    rc.subTotal
                });

            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((opcionCombo)cboBusqueda.SelectedItem).valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && txtBusqueda.Text.Length >= 50)
            {
                e.Handled = true;
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
