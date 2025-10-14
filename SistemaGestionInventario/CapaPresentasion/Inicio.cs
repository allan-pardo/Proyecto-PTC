using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaDatos;

using SistemaEntidades;
using SistemaNegocio;
using FontAwesome.Sharp;
using System.Security.Policy;
using FontAwesome.Sharp.Pro;
using IconMenuItem = FontAwesome.Sharp.IconMenuItem;


namespace CapaPresentasion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem Menuactivo = null;
        private static Form formularioActivo = null;

        // set construido en Inicio_Load con los nombreMenu de BD
        private HashSet<string> _menus = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        
        public Inicio( Usuario objusuario = null)
        {
            usuarioActual = objusuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

            if (usuarioActual == null)
            {
                MessageBox.Show("No hay usuario en sesión.");
                Close();
                return;
            }

            var listaPermiso = new SN_Permiso().listar(usuarioActual.idUsuario);
            _menus = new HashSet<string>(listaPermiso.Select(p => p.nombreMenu), StringComparer.OrdinalIgnoreCase);

            lblUsuarios.Text = usuarioActual.nombreCompleto;
            AplicarPermisosUI();

        }
        private void AplicarPermisosUI()
        {

            // Oculta todo por defecto
            imtMenuUsuario.Visible = false;
            imtMantenedor.Visible = false;
            imtVentas.Visible = false;
            imtComprar.Visible = false;
            imtClientes.Visible = false;
            imtProovedores.Visible = false;
            imtReportes.Visible = false;

            // Submenús
            imtSubMenuCategoria.Visible = false;
            imtSubMenuProducto.Visible = false;
            imtSubMenuNegocio.Visible = false; // si lo quieres atado a Mantenedor
            imtSubMenuRegistrarVentas.Visible = false;
            imtSubMenuVerDetalleVentas.Visible = false;
            imtSubMenuRegistrarCompra.Visible = false;
            imtSubMenuVerDetalleCompra.Visible = false;
            imtSubMenuReporteVenta.Visible = false;
            imtSubMenuReporteCompra.Visible = false;

            // Habilitar según permisos (códigos de BD)
            imtMenuUsuario.Visible = _menus.Contains("menuUsuario");
            imtMantenedor.Visible = _menus.Contains("menuMantenedor");
            imtVentas.Visible = _menus.Contains("menuVentas");
            imtComprar.Visible = _menus.Contains("menuCompras");
            imtClientes.Visible = _menus.Contains("menuClientes");
            imtProovedores.Visible = _menus.Contains("menuProveedores");
            imtReportes.Visible = _menus.Contains("menuReportes");

            // Submenús: puedes agruparlos por el menú padre
            bool puedeMantener = _menus.Contains("menuMantenedor");
            imtSubMenuCategoria.Visible = puedeMantener;
            imtSubMenuProducto.Visible = puedeMantener;
            imtSubMenuNegocio.Visible = puedeMantener;

            bool puedeVentas = _menus.Contains("menuVentas");
            imtSubMenuRegistrarVentas.Visible = puedeVentas;
            imtSubMenuVerDetalleVentas.Visible = puedeVentas;

            bool puedeCompras = _menus.Contains("menuCompras");
            imtSubMenuRegistrarCompra.Visible = puedeCompras;
            imtSubMenuVerDetalleCompra.Visible = puedeCompras;

            bool puedeReportes = _menus.Contains("menuReportes");
            imtSubMenuReporteVenta.Visible = puedeReportes;
            imtSubMenuReporteCompra.Visible = puedeReportes;
        }

        private void abrirFormularios(IconMenuItem menu, Form formulario)
        {
            if (Menuactivo != null)
            {
                Menuactivo.BackColor = Color.Salmon;
            }

            menu.BackColor = Color.FromArgb(255, 136, 136);
            Menuactivo = menu;


            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.FromArgb(255, 136, 136);

            pContenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private bool Tiene(string codigoMenu) => _menus.Contains(codigoMenu);

        private void AbrirSi(string codigoMenu, IconMenuItem menu, Form form)
        {
            if (!Tiene(codigoMenu))
            {
                MessageBox.Show("No tienes permiso para esta opción.");
                return;
            }
            abrirFormularios(menu, form);
        }

        // Para submenús (ToolStripMenuItem): pásame el menú padre visible (IconMenuItem)
        private void AbrirSi(string codigoMenu, IconMenuItem menuPadre, ToolStripMenuItem subMenu, Form form)
        {
            if (!Tiene(codigoMenu))
            {
                MessageBox.Show("No tienes permiso para esta opción.");
                return;
            }
            abrirFormularios(menuPadre, form);
        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirSi("menuUsuario", (IconMenuItem)sender, new frmUsuarios());

        }




        /*--------------Esta es las subs categorias de Mantenimiento -----------------*/

        private void imtSubMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirSi("menuMantenedor", imtMantenedor, imtSubMenuCategoria, new frmCategoria());

        }

        private void imtSubMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirSi("menuMantenedor", imtMantenedor, imtSubMenuProducto, new frmProducto());
        }

        private void imtSubMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirSi("menuMantenedor", imtMantenedor, imtSubMenuNegocio, new frmNegocio());
        }

        /*-----------------------------------------------------------------------------*/

        /*--------------------Esta es las subs categorias de Ventas--------------------*/

        private void imtSubMenuRegistrarVentas_Click(object sender, EventArgs e)
        {
            AbrirSi("menuVentas", imtVentas, imtSubMenuRegistrarVentas, new frmVentas(usuarioActual));
        }

        private void imtSubMenuVerDetalleVentas_Click(object sender, EventArgs e)
        {
            AbrirSi("menuVentas", imtVentas, imtSubMenuVerDetalleVentas, new frmDetalleVenta());
        }

        /*-----------------------------------------------------------------------------*/

        /*--------------------Esta es las subs categorias de Compra--------------------*/
        private void imtComprar_Click(object sender, EventArgs e)
        {

        }
        private void imtSubMenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            AbrirSi("menuCompras", imtComprar, imtSubMenuRegistrarCompra, new frmCompras(usuarioActual));
        }

        private void imtSubMenuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            AbrirSi("menuCompras", imtComprar, imtSubMenuVerDetalleCompra, new frmDetalleCompra());
        }

        /*-----------------------------------------------------------------------------*/

        private void imtClientes_Click(object sender, EventArgs e)
        {
            AbrirSi("menuClientes", (IconMenuItem)sender, new frmClientes());
        }

        private void imtProovedores_Click(object sender, EventArgs e)
        {
            AbrirSi("menuProveedores", (IconMenuItem)sender, new frmProovedores());
        }



        /*--------------------Esta es las subs categorias de Reportes--------------------*/
        private void imtReportes_Click(object sender, EventArgs e)
        {
 
        }

        private void imtSubMenuReporteVenta_Click(object sender, EventArgs e)
        {
            AbrirSi("menuReportes", imtReportes, imtSubMenuReporteVenta, new frmReporteVentas());

        }

        private void imtSubMenuReporteCompra_Click(object sender, EventArgs e)
        {
            AbrirSi("menuReportes", imtReportes, imtSubMenuReporteCompra, new frmReporteCompras());
        }
        /*-----------------------------------------------------------------------------*/



        private void pContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void imtMantenedor_Click(object sender, EventArgs e)
        {

        }

        private void mspMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void mspTitulo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblUsuarios_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}
