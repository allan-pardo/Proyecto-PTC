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
using FontAwesome.Sharp;
using System.Security.Policy;


namespace CapaPresentasion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem Menuactivo = null;
        private static Form formularioActivo = null;

        public Inicio( Usuario objusuario = null)
        {
            if (objusuario == null) usuarioActual = new Usuario() {nombreCompleto = "ADMIN PREDEFINIDO", idUsuario = 1} ;
            else usuarioActual = objusuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermiso = new SN_Permiso().listar(usuarioActual.idUsuario);

            foreach (IconMenuItem iconMenu in Menu.MenuItems)
            {

                bool encontrado = listaPermiso.Any(m => m.nombreMenu == iconMenu.Name);

                if (encontrado == false)
                {
                    iconMenu.Visible = false;
                }
                else
                {
                    iconMenu.Visible = true;
                }
            }

            lblUsuarios.Text = usuarioActual.nombreCompleto;

        }

        private void abrirFormularios(IconMenuItem menu, Form formulario)
        {
            if (Menuactivo != null)
            {
                Menuactivo.BackColor = Color.Salmon;
            }

            menu.BackColor = Color.FromArgb(250, 60, 114);
            Menuactivo = menu;


            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(250, 60, 114);

            pContenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            abrirFormularios((IconMenuItem)sender, new frmUsuarios());
        }




        /*--------------Esta es las subs categorias de Mantenimiento -----------------*/

        private void imtSubMenuCategoria_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtMantenedor, new frmCategoria());
        }

        private void imtSubMenuProducto_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtMantenedor, new frmProducto());
        }

        /*-----------------------------------------------------------------------------*/

        /*--------------------Esta es las subs categorias de Ventas--------------------*/

        private void imtSubMenuRegistrarVentas_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtVentas, new frmVentas());
        }

        private void imtSubMenuVerDetalleVentas_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtVentas, new frmDetalleVenta());
        }

        /*-----------------------------------------------------------------------------*/

        /*--------------------Esta es las subs categorias de Compra--------------------*/
        private void imtComprar_Click(object sender, EventArgs e)
        {

        }
        private void imtSubMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtComprar, new frmCompras());
        }

        private void imtSubMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormularios(imtComprar, new frmDetalleCompra());
        }

        /*-----------------------------------------------------------------------------*/

        private void imtClientes_Click(object sender, EventArgs e)
        {
            abrirFormularios((IconMenuItem)sender, new frmClientes());
        }

        private void imtProovedores_Click(object sender, EventArgs e)
        {
            abrirFormularios((IconMenuItem)sender, new frmProovedores());
        }

        private void imtReportes_Click(object sender, EventArgs e)
        {
            abrirFormularios((IconMenuItem)sender, new frmProovedores());
        }

        private void pContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imtMantenedor_Click(object sender, EventArgs e)
        {

        }

        private void mspMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void imtAcercaDe_Click(object sender, EventArgs e)
        {

        }

        
    }
}
