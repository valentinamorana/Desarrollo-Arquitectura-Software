namespace GUI
{
    partial class FormLogin
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
            this.pnlMarca = new System.Windows.Forms.Panel();
            this.lblPie = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.pnlUsuario = new System.Windows.Forms.Panel();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.pnlLineaUsuario = new System.Windows.Forms.Panel();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.pnlContraseña = new System.Windows.Forms.Panel();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.pnlLineaContraseña = new System.Windows.Forms.Panel();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.lblSinCuenta = new System.Windows.Forms.Label();
            this.lnkRegistrarme = new System.Windows.Forms.LinkLabel();
            this.pnlMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlUsuario.SuspendLayout();
            this.pnlContraseña.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMarca
            //
            this.pnlMarca.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.pnlMarca.Controls.Add(this.lblPie);
            this.pnlMarca.Controls.Add(this.lblMarca);
            this.pnlMarca.Controls.Add(this.picLogo);
            this.pnlMarca.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMarca.Location = new System.Drawing.Point(0, 0);
            this.pnlMarca.Name = "pnlMarca";
            this.pnlMarca.Size = new System.Drawing.Size(360, 520);
            this.pnlMarca.TabIndex = 0;
            //
            // picLogo
            //
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Image = global::GUI.Properties.Resources.generala_logo;
            this.picLogo.Location = new System.Drawing.Point(110, 145);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(140, 140);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            //
            // lblMarca
            //
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(0, 300);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(360, 55);
            this.lblMarca.TabIndex = 1;
            this.lblMarca.Text = "GENERALA";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblPie
            //
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(230, 170, 195);
            this.lblPie.Location = new System.Drawing.Point(0, 480);
            this.lblPie.Name = "lblPie";
            this.lblPie.Size = new System.Drawing.Size(360, 25);
            this.lblPie.TabIndex = 3;
            this.lblPie.Text = "Desarrollo y Arquitectura de Software · UAI";
            this.lblPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTitulo
            //
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(90, 10, 45);
            this.lblTitulo.Location = new System.Drawing.Point(420, 70);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(360, 48);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Iniciar sesión";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(120, 110, 115);
            this.lblSubtitulo.Location = new System.Drawing.Point(422, 120);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(358, 44);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Ingresá con tu usuario para empezar a jugar.";
            //
            // lblUsuario
            //
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(150, 100, 120);
            this.lblUsuario.Location = new System.Drawing.Point(420, 180);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(60, 15);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "USUARIO";
            //
            // pnlUsuario
            //
            this.pnlUsuario.BackColor = System.Drawing.Color.White;
            this.pnlUsuario.Controls.Add(this.txtUsuario);
            this.pnlUsuario.Controls.Add(this.pnlLineaUsuario);
            this.pnlUsuario.Location = new System.Drawing.Point(420, 200);
            this.pnlUsuario.Name = "pnlUsuario";
            this.pnlUsuario.Size = new System.Drawing.Size(360, 36);
            this.pnlUsuario.TabIndex = 4;
            //
            // txtUsuario
            //
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(90, 10, 45);
            this.txtUsuario.Location = new System.Drawing.Point(2, 7);
            this.txtUsuario.MaxLength = 20;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(356, 21);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.Enter += new System.EventHandler(this.txtCampo_Enter);
            this.txtUsuario.Leave += new System.EventHandler(this.txtCampo_Leave);
            //
            // pnlLineaUsuario
            //
            this.pnlLineaUsuario.BackColor = System.Drawing.Color.FromArgb(230, 200, 212);
            this.pnlLineaUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineaUsuario.Location = new System.Drawing.Point(0, 34);
            this.pnlLineaUsuario.Name = "pnlLineaUsuario";
            this.pnlLineaUsuario.Size = new System.Drawing.Size(360, 2);
            this.pnlLineaUsuario.TabIndex = 1;
            //
            // lblContraseña
            //
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblContraseña.ForeColor = System.Drawing.Color.FromArgb(150, 100, 120);
            this.lblContraseña.Location = new System.Drawing.Point(420, 256);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(86, 15);
            this.lblContraseña.TabIndex = 5;
            this.lblContraseña.Text = "CONTRASEÑA";
            //
            // pnlContraseña
            //
            this.pnlContraseña.BackColor = System.Drawing.Color.White;
            this.pnlContraseña.Controls.Add(this.txtContraseña);
            this.pnlContraseña.Controls.Add(this.pnlLineaContraseña);
            this.pnlContraseña.Location = new System.Drawing.Point(420, 276);
            this.pnlContraseña.Name = "pnlContraseña";
            this.pnlContraseña.Size = new System.Drawing.Size(360, 36);
            this.pnlContraseña.TabIndex = 6;
            //
            // txtContraseña
            //
            this.txtContraseña.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtContraseña.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.txtContraseña.ForeColor = System.Drawing.Color.FromArgb(90, 10, 45);
            this.txtContraseña.Location = new System.Drawing.Point(2, 7);
            this.txtContraseña.MaxLength = 20;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '●';
            this.txtContraseña.Size = new System.Drawing.Size(356, 21);
            this.txtContraseña.TabIndex = 0;
            this.txtContraseña.Enter += new System.EventHandler(this.txtCampo_Enter);
            this.txtContraseña.Leave += new System.EventHandler(this.txtCampo_Leave);
            //
            // pnlLineaContraseña
            //
            this.pnlLineaContraseña.BackColor = System.Drawing.Color.FromArgb(230, 200, 212);
            this.pnlLineaContraseña.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineaContraseña.Location = new System.Drawing.Point(0, 34);
            this.pnlLineaContraseña.Name = "pnlLineaContraseña";
            this.pnlLineaContraseña.Size = new System.Drawing.Size(360, 2);
            this.pnlLineaContraseña.TabIndex = 1;
            //
            // lblMensaje
            //
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblMensaje.ForeColor = System.Drawing.Color.Crimson;
            this.lblMensaje.Location = new System.Drawing.Point(420, 322);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(360, 30);
            this.lblMensaje.TabIndex = 7;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnIngresar
            //
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(110, 10, 55);
            this.btnIngresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(140, 15, 70);
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(420, 360);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(360, 50);
            this.btnIngresar.TabIndex = 8;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            //
            // lblSinCuenta
            //
            this.lblSinCuenta.AutoSize = true;
            this.lblSinCuenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSinCuenta.ForeColor = System.Drawing.Color.FromArgb(120, 110, 115);
            this.lblSinCuenta.Location = new System.Drawing.Point(498, 432);
            this.lblSinCuenta.Name = "lblSinCuenta";
            this.lblSinCuenta.Size = new System.Drawing.Size(118, 19);
            this.lblSinCuenta.TabIndex = 9;
            this.lblSinCuenta.Text = "¿No tenés cuenta?";
            //
            // lnkRegistrarme
            //
            this.lnkRegistrarme.ActiveLinkColor = System.Drawing.Color.FromArgb(110, 10, 55);
            this.lnkRegistrarme.AutoSize = true;
            this.lnkRegistrarme.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lnkRegistrarme.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkRegistrarme.LinkColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lnkRegistrarme.Location = new System.Drawing.Point(618, 432);
            this.lnkRegistrarme.Name = "lnkRegistrarme";
            this.lnkRegistrarme.Size = new System.Drawing.Size(76, 19);
            this.lnkRegistrarme.TabIndex = 10;
            this.lnkRegistrarme.TabStop = true;
            this.lnkRegistrarme.Text = "Registrate";
            this.lnkRegistrarme.VisitedLinkColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lnkRegistrarme.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegistrarme_LinkClicked);
            //
            // FormLogin
            //
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 520);
            this.Controls.Add(this.lnkRegistrarme);
            this.Controls.Add(this.lblSinCuenta);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.pnlContraseña);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.pnlUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.pnlMarca);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::GUI.Properties.Resources.generala;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Iniciar Sesión";
            this.pnlMarca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlUsuario.ResumeLayout(false);
            this.pnlUsuario.PerformLayout();
            this.pnlContraseña.ResumeLayout(false);
            this.pnlContraseña.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMarca;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblPie;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel pnlUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Panel pnlLineaUsuario;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Panel pnlContraseña;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Panel pnlLineaContraseña;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label lblSinCuenta;
        private System.Windows.Forms.LinkLabel lnkRegistrarme;
    }
}
