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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {

            cboEstado.Items.Add(new opcionCombo() { valor = 1, texto = "Activo" });
            cboEstado.Items.Add(new opcionCombo() { valor = 0, texto = "No activo" });

            cboEstado.DisplayMember = "texto";
            cboEstado.ValueMember = "valor";
            cboEstado.SelectedIndex = 0 ;

            List<Rol> listaRol = new SN_Rol().listar();

            foreach (Rol item in listaRol)
            {
                cboRol.Items.Add(new opcionCombo() { valor = item.idRol, texto = item.descripcion });
            }

            cboRol.DisplayMember = "texto";
            cboRol.ValueMember = "valor";
            cboRol.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new opcionCombo() { valor  = columna.Name, texto = columna.HeaderText });
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
            { idUsuario = Convert.ToInt32(txtId.Text), 
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
            else 
            { 
                bool resultado = new SN_Usuario().Editar(objUsuario, out Mensaje);

                if (resultado) 
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];

                    row.Cells["id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtNroDocumento.Text;
                    row.Cells["nombreCompleto"].Value = txtNombreCompleto.Text;
                    row.Cells["correo"].Value = txtCorreo.Text;
                    row.Cells["clave"].Value = txtContraseña.Text;
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
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32 (dgvData.Rows[indice].Cells["idRol"].Value))
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

                    bool respuesta = new SN_Usuario().Eliminar(objUsuario, out Mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(Mensaje,"Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        
    }
}
