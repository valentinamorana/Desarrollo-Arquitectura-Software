namespace GUI
{
    partial class FormRegistro
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
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlTitulo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitulo
            //
            this.pnlTitulo.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(520, 90);
            this.pnlTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(520, 90);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Registrarme";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblNombre.Location = new System.Drawing.Point(50, 122);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(69, 21);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNombre.Location = new System.Drawing.Point(210, 117);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(260, 32);
            this.txtNombre.TabIndex = 2;
            //
            // lblContraseña
            //
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblContraseña.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblContraseña.Location = new System.Drawing.Point(50, 177);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(103, 21);
            this.lblContraseña.TabIndex = 3;
            this.lblContraseña.Text = "Contraseña";
            //
            // txtContraseña
            //
            this.txtContraseña.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtContraseña.Location = new System.Drawing.Point(210, 172);
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.PasswordChar = '●';
            this.txtContraseña.Size = new System.Drawing.Size(260, 32);
            this.txtContraseña.TabIndex = 4;
            //
            // lblConfirmar
            //
            this.lblConfirmar.AutoSize = true;
            this.lblConfirmar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblConfirmar.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblConfirmar.Location = new System.Drawing.Point(50, 232);
            this.lblConfirmar.Name = "lblConfirmar";
            this.lblConfirmar.Size = new System.Drawing.Size(96, 21);
            this.lblConfirmar.TabIndex = 5;
            this.lblConfirmar.Text = "Confirmar";
            //
            // txtConfirmar
            //
            this.txtConfirmar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtConfirmar.Location = new System.Drawing.Point(210, 227);
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.PasswordChar = '●';
            this.txtConfirmar.Size = new System.Drawing.Size(260, 32);
            this.txtConfirmar.TabIndex = 6;
            //
            // lblMensaje
            //
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblMensaje.ForeColor = System.Drawing.Color.Crimson;
            this.lblMensaje.Location = new System.Drawing.Point(50, 278);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(420, 40);
            this.lblMensaje.TabIndex = 7;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnGuardar
            //
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(85, 335);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(165, 52);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(253, 235, 242);
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCancelar.Location = new System.Drawing.Point(270, 335);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(165, 52);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // FormRegistro
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.ClientSize = new System.Drawing.Size(520, 440);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.txtContraseña);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRegistro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Registrarme";
            this.pnlTitulo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
