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
using static System.Net.Mime.MediaTypeNames;

namespace CapaPresentasion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();

            this.MinimumSize = new Size(800, 600);
            this.MaximumSize = new Size(1920, 1080);
        }

        private void CargarUsuarioEnFormulario(Usuario u)
        {
            txtId.Text = u.idUsuario.ToString();
            txtNroDocumento.Text = u.documento;
            txtNombreCompleto.Text = u.nombreCompleto;
            txtCorreo.Text = u.correo;
            // Mostrar el HASH en el textbox:
            txtContraseña.Text = u.clave;            // <- el hash tal cual
                                                     // txtContraseña.UseSystemPasswordChar = false; // opcional si quieres VER el hash
                                                     // combos de rol/estado...
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new opcionCombo() { valor = 1, texto = "Activo" });
            cboEstado.Items.Add(new opcionCombo() { valor = 0, texto = "No activo" });

            cboEstado.DisplayMember = "texto";
            cboEstado.ValueMember = "valor";
            cboEstado.SelectedIndex = 0;


            List<Rol> listaRol = new SN_Rol().listar();

            List<opcionCombo> listaOpciones = new List<opcionCombo>();

            foreach (Rol item in listaRol)
            {
                listaOpciones.Add(new opcionCombo() { valor = item.idRol, texto = item.descripcion });
            }

            cboRol.DataSource = listaOpciones;
            cboRol.DisplayMember = "texto";
            cboRol.ValueMember = "valor";

            if (cboRol.Items.Count > 0)
                cboRol.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new opcionCombo() { valor = columna.Name, texto = columna.HeaderText });
                }

                cboBusqueda.DisplayMember = "texto";
                cboBusqueda.ValueMember = "valor";
                cboBusqueda.AutoCompleteMode = 0;

            }
            //mostrar todos los usuarios
            List<Usuario> listaUsuario = new SN_Usuario().listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {"",item.idUsuario,item.documento,item.nombreCompleto,item.correo,item.clave,
                item.oRol.idRol,
                item.oRol.descripcion,
                item.estado == true ? 1 : 0,
                item.estado == true ? "Activo" : "No activo"
                });
            }


        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Mensaje = string.Empty;

            Usuario objUsuario = new Usuario()
            {
                idUsuario = Convert.ToInt32(txtId.Text),
                documento = txtNroDocumento.Text,
                nombreCompleto = txtNombreCompleto.Text,
                correo = txtCorreo.Text,
                clave = txtContraseña.Text,
                oRol = new Rol() { idRol = Convert.ToInt32(((opcionCombo)cboRol.SelectedItem).valor) },

                estado = Convert.ToInt32(((opcionCombo)cboEstado.SelectedItem).valor) == 1 ? true : false
            };


            if (objUsuario.idUsuario == 0)
            {
                int idUsuarioGenerado = new SN_Usuario().Registrar(objUsuario, out Mensaje);

                if (idUsuarioGenerado != 0)
                {
                    dgvData.Rows.Add(new object[] {"",idUsuarioGenerado,txtNroDocumento.Text,txtNombreCompleto.Text,txtCorreo.Text,txtContraseña.Text,
                ((opcionCombo)cboRol.SelectedItem).valor.ToString() ,
                ((opcionCombo)cboRol.SelectedItem).texto.ToString(),

                ((opcionCombo)cboEstado.SelectedItem).valor.ToString() ,
                ((opcionCombo)cboEstado.SelectedItem).texto.ToString(),


            });

                    Limpiar();

                }
                else
                {
                    MessageBox.Show(Mensaje);
                }
            }
            else // EDITAR
            {
                // Pasa lo que haya en el textbox: hash, plano o vacío
                objUsuario.clave = txtContraseña.Text;

                bool resultado = new SN_Usuario().Editar(objUsuario, out Mensaje);

                if (resultado)
                {
                    var row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtNroDocumento.Text;
                    row.Cells["nombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["correo"].Value = txtCorreo.Text;

                    // No mostrar la contraseña (recomendado):
                    row.Cells["clave"].Value = objUsuario.clave;
                    // Si quieres ver el hash en la celda, podrías usar:
                    // row.Cells["clave"].Value = objUsuario.clave;

                    row.Cells["idRol"].Value = ((opcionCombo)cboRol.SelectedItem).valor.ToString();
                    row.Cells["Rol"].Value = ((opcionCombo)cboRol.SelectedItem).texto.ToString();
                    row.Cells["EstadoValor"].Value = ((opcionCombo)cboEstado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((opcionCombo)cboEstado.SelectedItem).texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(Mensaje);
                }

            }
        }



        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtNroDocumento.Text = "";
            txtNombreCompleto.Text = "";
            txtCorreo.Text = "";
            txtContraseña.Text = "";
            txtConfirmarContraseña.Text = "";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_check_20.Width;
                var h = Properties.Resources.icons8_check_20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;


                e.Graphics.DrawImage(Properties.Resources.icons8_check_20, new Rectangle(x, y, w, h));
                e.Handled = true;

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {

                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["id"].Value.ToString();
                    txtNroDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombreCompleto.Text = dgvData.Rows[indice].Cells["nombreCompleto"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["correo"].Value.ToString();
                    txtContraseña.Text = dgvData.Rows[indice].Cells["clave"].Value.ToString();
                    txtConfirmarContraseña.Text = dgvData.Rows[indice].Cells["clave"].Value.ToString();

                    foreach (opcionCombo oc in cboRol.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idRol"].Value))
                        {
                            int indice_Combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_Combo;
                            break;
                        }
                    }

                    foreach (opcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_Combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_Combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Deseas eliminar este usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Mensaje = string.Empty;

                    Usuario objUsuario = new Usuario()
                    {
                        idUsuario = Convert.ToInt32(txtId.Text)
                    };
                    //todavia no funciona
                    bool respuesta = new SN_Usuario().Eliminar(objUsuario, out Mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string columnafiltrar = ((opcionCombo)cboBusqueda.SelectedItem).valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {

                    if (row.Cells[columnafiltrar].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                    {
                        row.Visible = false;
                    }
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNroDocumento_TextChanged(object sender, EventArgs e)
        {


        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmarContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNroDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtNroDocumento.Text.Length >= 6)
            {
                e.Handled = true;
            }


        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && txtCorreo.Text.Length >= 40)
            {
                e.Handled = true;
            }
        }

        private void txtNombreCompleto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && txtNombreCompleto.Text.Length >= 30)
            {
                e.Handled = true;
            }

        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtContraseña.Text.Length >= 6)
            {
                e.Handled = true;
            }

        }

        private void txtConfirmarContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (!char.IsControl(e.KeyChar) && txtConfirmarContraseña.Text.Length >= 6)
            {
                e.Handled = true;
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && txtBusqueda.Text.Length >= 50)
            {
                e.Handled = true;
            }
        }

        private void txtNroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtNombreCompleto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtConfirmarContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V || e.KeyCode == Keys.X))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvData.Rows[e.RowIndex];

                // Opción A: reconstruir el objeto desde la fila (si tienes todas las columnas)
                var u = new Usuario
                {
                    idUsuario = Convert.ToInt32(fila.Cells["id"].Value),
                    documento = fila.Cells["Documento"].Value?.ToString(),
                    nombreCompleto = fila.Cells["nombreCompleto"].Value?.ToString(),
                    correo = fila.Cells["correo"].Value?.ToString(),
                    // si en la grilla NO muestras la clave, recupérala de BD:
                    // clave = fila.Cells["clave"].Value?.ToString(),
                    oRol = new Rol { idRol = Convert.ToInt32(fila.Cells["idRol"].Value) },
                    estado = fila.Cells["EstadoValor"].Value?.ToString() == "1"
                };

                // Opción B (recomendada): cargar desde BD por id o documento
                // var u = new SN_Usuario().ObtenerPorDocumento(fila.Cells["Documento"].Value.ToString());

                CargarUsuarioEnFormulario(u);
            }

        }
    }
}
