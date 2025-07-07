namespace CapaPresentasion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.mspMenu = new System.Windows.Forms.MenuStrip();
            this.imtMenuUsuario = new FontAwesome.Sharp.IconMenuItem();
            this.imtMantenedor = new FontAwesome.Sharp.IconMenuItem();
            this.imtSubMenuCategoria = new FontAwesome.Sharp.IconMenuItem();
            this.imtSubMenuProducto = new FontAwesome.Sharp.IconMenuItem();
            this.imtVentas = new FontAwesome.Sharp.IconMenuItem();
            this.imtComprar = new FontAwesome.Sharp.IconMenuItem();
            this.imtClientes = new FontAwesome.Sharp.IconMenuItem();
            this.imtProovedores = new FontAwesome.Sharp.IconMenuItem();
            this.imtReportes = new FontAwesome.Sharp.IconMenuItem();
            this.imtAcercaDe = new FontAwesome.Sharp.IconMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.mspTitulo = new System.Windows.Forms.MenuStrip();
            this.pContenedor = new System.Windows.Forms.Panel();
            this.lblIndicaUsuario = new System.Windows.Forms.Label();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.imtSubMenuRegistrarVentas = new FontAwesome.Sharp.IconMenuItem();
            this.imtSubMenuVerDetalleVentas = new FontAwesome.Sharp.IconMenuItem();
            this.imtSubMenuRegistrarCompra = new FontAwesome.Sharp.IconMenuItem();
            this.imtSubMenuVerDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.mspMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mspMenu
            // 
            this.mspMenu.AutoSize = false;
            this.mspMenu.BackColor = System.Drawing.Color.Salmon;
            this.mspMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imtMenuUsuario,
            this.imtMantenedor,
            this.imtVentas,
            this.imtComprar,
            this.imtClientes,
            this.imtProovedores,
            this.imtReportes,
            this.imtAcercaDe});
            this.mspMenu.Location = new System.Drawing.Point(0, 84);
            this.mspMenu.Name = "mspMenu";
            this.mspMenu.Size = new System.Drawing.Size(853, 79);
            this.mspMenu.TabIndex = 0;
            this.mspMenu.Text = "menuStrip1";
            this.mspMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mspMenu_ItemClicked);
            // 
            // imtMenuUsuario
            // 
            this.imtMenuUsuario.AutoSize = false;
            this.imtMenuUsuario.ForeColor = System.Drawing.Color.White;
            this.imtMenuUsuario.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.imtMenuUsuario.IconColor = System.Drawing.Color.White;
            this.imtMenuUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtMenuUsuario.IconSize = 50;
            this.imtMenuUsuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtMenuUsuario.Name = "imtMenuUsuario";
            this.imtMenuUsuario.Size = new System.Drawing.Size(80, 70);
            this.imtMenuUsuario.Text = "Usuario";
            this.imtMenuUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtMenuUsuario.Click += new System.EventHandler(this.iconMenuItem1_Click);
            // 
            // imtMantenedor
            // 
            this.imtMantenedor.AutoSize = false;
            this.imtMantenedor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imtSubMenuCategoria,
            this.imtSubMenuProducto});
            this.imtMantenedor.ForeColor = System.Drawing.Color.White;
            this.imtMantenedor.IconChar = FontAwesome.Sharp.IconChar.ScrewdriverWrench;
            this.imtMantenedor.IconColor = System.Drawing.Color.White;
            this.imtMantenedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtMantenedor.IconSize = 50;
            this.imtMantenedor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtMantenedor.Name = "imtMantenedor";
            this.imtMantenedor.Size = new System.Drawing.Size(80, 70);
            this.imtMantenedor.Text = "Mantenedor";
            this.imtMantenedor.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtMantenedor.Click += new System.EventHandler(this.imtMantenedor_Click);
            // 
            // imtSubMenuCategoria
            // 
            this.imtSubMenuCategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuCategoria.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuCategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuCategoria.Name = "imtSubMenuCategoria";
            this.imtSubMenuCategoria.Size = new System.Drawing.Size(125, 22);
            this.imtSubMenuCategoria.Text = "Categoria";
            this.imtSubMenuCategoria.Click += new System.EventHandler(this.imtSubMenuCategoria_Click);
            // 
            // imtSubMenuProducto
            // 
            this.imtSubMenuProducto.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuProducto.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuProducto.Name = "imtSubMenuProducto";
            this.imtSubMenuProducto.Size = new System.Drawing.Size(125, 22);
            this.imtSubMenuProducto.Text = "Producto";
            this.imtSubMenuProducto.Click += new System.EventHandler(this.imtSubMenuProducto_Click);
            // 
            // imtVentas
            // 
            this.imtVentas.AutoSize = false;
            this.imtVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imtSubMenuRegistrarVentas,
            this.imtSubMenuVerDetalleVentas});
            this.imtVentas.ForeColor = System.Drawing.Color.White;
            this.imtVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.imtVentas.IconColor = System.Drawing.Color.White;
            this.imtVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtVentas.IconSize = 50;
            this.imtVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtVentas.Name = "imtVentas";
            this.imtVentas.Size = new System.Drawing.Size(80, 70);
            this.imtVentas.Text = "Ventas";
            this.imtVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // imtComprar
            // 
            this.imtComprar.AutoSize = false;
            this.imtComprar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imtSubMenuRegistrarCompra,
            this.imtSubMenuVerDetalleCompra});
            this.imtComprar.ForeColor = System.Drawing.Color.White;
            this.imtComprar.IconChar = FontAwesome.Sharp.IconChar.MoneyBillTransfer;
            this.imtComprar.IconColor = System.Drawing.Color.White;
            this.imtComprar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtComprar.IconSize = 50;
            this.imtComprar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtComprar.Name = "imtComprar";
            this.imtComprar.Size = new System.Drawing.Size(80, 70);
            this.imtComprar.Text = "Comprar";
            this.imtComprar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtComprar.Click += new System.EventHandler(this.imtComprar_Click);
            // 
            // imtClientes
            // 
            this.imtClientes.AutoSize = false;
            this.imtClientes.ForeColor = System.Drawing.Color.White;
            this.imtClientes.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.imtClientes.IconColor = System.Drawing.Color.White;
            this.imtClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtClientes.IconSize = 50;
            this.imtClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtClientes.Name = "imtClientes";
            this.imtClientes.Size = new System.Drawing.Size(80, 70);
            this.imtClientes.Text = "Clientes";
            this.imtClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtClientes.Click += new System.EventHandler(this.imtClientes_Click);
            // 
            // imtProovedores
            // 
            this.imtProovedores.AutoSize = false;
            this.imtProovedores.ForeColor = System.Drawing.Color.White;
            this.imtProovedores.IconChar = FontAwesome.Sharp.IconChar.AddressCard;
            this.imtProovedores.IconColor = System.Drawing.Color.White;
            this.imtProovedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtProovedores.IconSize = 50;
            this.imtProovedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtProovedores.Name = "imtProovedores";
            this.imtProovedores.Size = new System.Drawing.Size(80, 70);
            this.imtProovedores.Text = "Proovedores";
            this.imtProovedores.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtProovedores.Click += new System.EventHandler(this.imtProovedores_Click);
            // 
            // imtReportes
            // 
            this.imtReportes.AutoSize = false;
            this.imtReportes.ForeColor = System.Drawing.Color.White;
            this.imtReportes.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.imtReportes.IconColor = System.Drawing.Color.White;
            this.imtReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtReportes.IconSize = 50;
            this.imtReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtReportes.Name = "imtReportes";
            this.imtReportes.Size = new System.Drawing.Size(80, 70);
            this.imtReportes.Text = "Reportes";
            this.imtReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtReportes.Click += new System.EventHandler(this.imtReportes_Click);
            // 
            // imtAcercaDe
            // 
            this.imtAcercaDe.AutoSize = false;
            this.imtAcercaDe.ForeColor = System.Drawing.Color.White;
            this.imtAcercaDe.IconChar = FontAwesome.Sharp.IconChar.CircleInfo;
            this.imtAcercaDe.IconColor = System.Drawing.Color.White;
            this.imtAcercaDe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtAcercaDe.IconSize = 50;
            this.imtAcercaDe.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imtAcercaDe.Name = "imtAcercaDe";
            this.imtAcercaDe.Size = new System.Drawing.Size(80, 70);
            this.imtAcercaDe.Text = "Acerca de";
            this.imtAcercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imtAcercaDe.Click += new System.EventHandler(this.imtAcercaDe_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sistema de Inventario";
            // 
            // mspTitulo
            // 
            this.mspTitulo.AutoSize = false;
            this.mspTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mspTitulo.Location = new System.Drawing.Point(0, 0);
            this.mspTitulo.Name = "mspTitulo";
            this.mspTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.mspTitulo.Size = new System.Drawing.Size(853, 84);
            this.mspTitulo.TabIndex = 1;
            this.mspTitulo.Text = "menuStrip2";
            // 
            // pContenedor
            // 
            this.pContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContenedor.Location = new System.Drawing.Point(0, 163);
            this.pContenedor.Name = "pContenedor";
            this.pContenedor.Size = new System.Drawing.Size(853, 305);
            this.pContenedor.TabIndex = 3;
            this.pContenedor.Paint += new System.Windows.Forms.PaintEventHandler(this.pContenedor_Paint);
            // 
            // lblIndicaUsuario
            // 
            this.lblIndicaUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblIndicaUsuario.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndicaUsuario.ForeColor = System.Drawing.Color.White;
            this.lblIndicaUsuario.Location = new System.Drawing.Point(596, 23);
            this.lblIndicaUsuario.Name = "lblIndicaUsuario";
            this.lblIndicaUsuario.Size = new System.Drawing.Size(74, 23);
            this.lblIndicaUsuario.TabIndex = 4;
            this.lblIndicaUsuario.Text = "Usuario:";
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblUsuarios.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblUsuarios.Location = new System.Drawing.Point(666, 23);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(76, 19);
            this.lblUsuarios.TabIndex = 5;
            this.lblUsuarios.Text = "lblUsuarios";
            // 
            // imtSubMenuRegistrarVentas
            // 
            this.imtSubMenuRegistrarVentas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuRegistrarVentas.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuRegistrarVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuRegistrarVentas.Name = "imtSubMenuRegistrarVentas";
            this.imtSubMenuRegistrarVentas.Size = new System.Drawing.Size(180, 22);
            this.imtSubMenuRegistrarVentas.Text = "Registrar";
            this.imtSubMenuRegistrarVentas.Click += new System.EventHandler(this.imtSubMenuRegistrarVentas_Click);
            // 
            // imtSubMenuVerDetalleVentas
            // 
            this.imtSubMenuVerDetalleVentas.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuVerDetalleVentas.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuVerDetalleVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuVerDetalleVentas.Name = "imtSubMenuVerDetalleVentas";
            this.imtSubMenuVerDetalleVentas.Size = new System.Drawing.Size(180, 22);
            this.imtSubMenuVerDetalleVentas.Text = "Ver detalle";
            this.imtSubMenuVerDetalleVentas.Click += new System.EventHandler(this.imtSubMenuVerDetalleVentas_Click);
            // 
            // imtSubMenuRegistrarCompra
            // 
            this.imtSubMenuRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuRegistrarCompra.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuRegistrarCompra.Name = "imtSubMenuRegistrarCompra";
            this.imtSubMenuRegistrarCompra.Size = new System.Drawing.Size(180, 22);
            this.imtSubMenuRegistrarCompra.Text = "Registrar";
            this.imtSubMenuRegistrarCompra.Click += new System.EventHandler(this.imtSubMenuRegistrarCompra_Click);
            // 
            // imtSubMenuVerDetalleCompra
            // 
            this.imtSubMenuVerDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.imtSubMenuVerDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.imtSubMenuVerDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imtSubMenuVerDetalleCompra.Name = "imtSubMenuVerDetalleCompra";
            this.imtSubMenuVerDetalleCompra.Size = new System.Drawing.Size(180, 22);
            this.imtSubMenuVerDetalleCompra.Text = "Ver detalle";
            this.imtSubMenuVerDetalleCompra.Click += new System.EventHandler(this.imtSubMenuVerDetalleCompra_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 468);
            this.Controls.Add(this.lblUsuarios);
            this.Controls.Add(this.lblIndicaUsuario);
            this.Controls.Add(this.pContenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mspMenu);
            this.Controls.Add(this.mspTitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mspMenu;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.mspMenu.ResumeLayout(false);
            this.mspMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mspMenu;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconMenuItem imtMenuUsuario;
        private System.Windows.Forms.MenuStrip mspTitulo;
        private FontAwesome.Sharp.IconMenuItem imtMantenedor;
        private FontAwesome.Sharp.IconMenuItem imtVentas;
        private FontAwesome.Sharp.IconMenuItem imtComprar;
        private FontAwesome.Sharp.IconMenuItem imtClientes;
        private FontAwesome.Sharp.IconMenuItem imtProovedores;
        private FontAwesome.Sharp.IconMenuItem imtReportes;
        private FontAwesome.Sharp.IconMenuItem imtAcercaDe;
        private System.Windows.Forms.Panel pContenedor;
        private System.Windows.Forms.Label lblIndicaUsuario;
        private System.Windows.Forms.Label lblUsuarios;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuCategoria;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuProducto;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuRegistrarVentas;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuVerDetalleVentas;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuRegistrarCompra;
        private FontAwesome.Sharp.IconMenuItem imtSubMenuVerDetalleCompra;
    }
}

