namespace CapaPresentasion
{
    partial class frmRecuperarContraseña
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNumeroDoc = new System.Windows.Forms.Label();
            this.txtRecuperar = new System.Windows.Forms.TextBox();
            this.btnRecuperarContra = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // lblNumeroDoc
            // 
            this.lblNumeroDoc.AutoSize = true;
            this.lblNumeroDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(45)))), ((int)(((byte)(144)))));
            this.lblNumeroDoc.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroDoc.Location = new System.Drawing.Point(35, 47);
            this.lblNumeroDoc.Name = "lblNumeroDoc";
            this.lblNumeroDoc.Size = new System.Drawing.Size(339, 26);
            this.lblNumeroDoc.TabIndex = 13;
            this.lblNumeroDoc.Text = "Ingrese su numero de documento:";
            this.lblNumeroDoc.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtRecuperar
            // 
            this.txtRecuperar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecuperar.Location = new System.Drawing.Point(57, 95);
            this.txtRecuperar.Name = "txtRecuperar";
            this.txtRecuperar.PasswordChar = '*';
            this.txtRecuperar.Size = new System.Drawing.Size(290, 26);
            this.txtRecuperar.TabIndex = 12;
            this.txtRecuperar.TextChanged += new System.EventHandler(this.txtClave_TextChanged);
            // 
            // btnRecuperarContra
            // 
            this.btnRecuperarContra.BackColor = System.Drawing.Color.Magenta;
            this.btnRecuperarContra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecuperarContra.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRecuperarContra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecuperarContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperarContra.ForeColor = System.Drawing.Color.White;
            this.btnRecuperarContra.IconChar = FontAwesome.Sharp.IconChar.Gitlab;
            this.btnRecuperarContra.IconColor = System.Drawing.Color.White;
            this.btnRecuperarContra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRecuperarContra.IconSize = 21;
            this.btnRecuperarContra.Location = new System.Drawing.Point(99, 150);
            this.btnRecuperarContra.Name = "btnRecuperarContra";
            this.btnRecuperarContra.Size = new System.Drawing.Size(203, 30);
            this.btnRecuperarContra.TabIndex = 14;
            this.btnRecuperarContra.Text = "Recuperar contraseña";
            this.btnRecuperarContra.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecuperarContra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRecuperarContra.UseVisualStyleBackColor = false;
            this.btnRecuperarContra.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // frmRecuperarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(45)))), ((int)(((byte)(144)))));
            this.ClientSize = new System.Drawing.Size(408, 265);
            this.Controls.Add(this.btnRecuperarContra);
            this.Controls.Add(this.lblNumeroDoc);
            this.Controls.Add(this.txtRecuperar);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmRecuperarContraseña";
            this.Text = "frmRecuperarContraseña";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton btnRecuperarContra;
        private System.Windows.Forms.Label lblNumeroDoc;
        private System.Windows.Forms.TextBox txtRecuperar;
    }
}