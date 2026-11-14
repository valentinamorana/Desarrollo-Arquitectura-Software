namespace GUI
{
    partial class FormMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblSesion = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnJugar = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.btnReglas = new System.Windows.Forms.Button();
            this.lblAdministracion = new System.Windows.Forms.Label();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblPie = new System.Windows.Forms.Label();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.ofdRestore = new System.Windows.Forms.OpenFileDialog();
            this.pnlTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitulo
            //
            this.pnlTitulo.BackColor = Tema.Rosa;
            this.pnlTitulo.Controls.Add(this.lblBienvenida);
            this.pnlTitulo.Controls.Add(this.lblSesion);
            this.pnlTitulo.Controls.Add(this.lblSubtitulo);
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Controls.Add(this.picLogo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(1000, 120);
            this.pnlTitulo.TabIndex = 0;
            //
            // picLogo
            //
            this.picLogo.Image = global::GUI.Properties.Resources.generala_logo;
            this.picLogo.Location = new System.Drawing.Point(32, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(80, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(124, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(207, 51);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "GENERALA";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtitulo.ForeColor = Tema.RosaClaro;
            this.lblSubtitulo.Location = new System.Drawing.Point(129, 70);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(225, 19);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Juego de dados para 2 jugadores";
            //
            // lblSesion
            //
            this.lblSesion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSesion.ForeColor = Tema.RosaClaro;
            this.lblSesion.Location = new System.Drawing.Point(620, 34);
            this.lblSesion.Name = "lblSesion";
            this.lblSesion.Size = new System.Drawing.Size(348, 20);
            this.lblSesion.TabIndex = 3;
            this.lblSesion.Text = "Sesión iniciada como";
            this.lblSesion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // lblBienvenida
            //
            this.lblBienvenida.AutoEllipsis = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.ForeColor = System.Drawing.Color.White;
            this.lblBienvenida.Location = new System.Drawing.Point(620, 54);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(348, 32);
            this.lblBienvenida.TabIndex = 4;
            this.lblBienvenida.Text = "usuario";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlBotones
            //
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.pnlBotones.Controls.Add(this.lblPie);
            this.pnlBotones.Controls.Add(this.btnCerrarSesion);
            this.pnlBotones.Controls.Add(this.btnRestore);
            this.pnlBotones.Controls.Add(this.btnBackup);
            this.pnlBotones.Controls.Add(this.lblAdministracion);
            this.pnlBotones.Controls.Add(this.btnReglas);
            this.pnlBotones.Controls.Add(this.btnBitacora);
            this.pnlBotones.Controls.Add(this.btnEstadisticas);
            this.pnlBotones.Controls.Add(this.btnJugar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBotones.Location = new System.Drawing.Point(0, 120);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(1000, 500);
            this.pnlBotones.TabIndex = 1;
            //
            // btnJugar
            //
            this.btnJugar.BackColor = Tema.Vino;
            this.btnJugar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJugar.FlatAppearance.BorderSize = 0;
            this.btnJugar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(60, 5, 30);
            this.btnJugar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(120, 15, 60);
            this.btnJugar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJugar.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.btnJugar.ForeColor = System.Drawing.Color.White;
            this.btnJugar.Location = new System.Drawing.Point(32, 28);
            this.btnJugar.Name = "btnJugar";
            this.btnJugar.Padding = new System.Windows.Forms.Padding(90, 18, 28, 0);
            this.btnJugar.Size = new System.Drawing.Size(936, 108);
            this.btnJugar.TabIndex = 0;
            this.btnJugar.Tag = "Empezá una partida nueva: ingresá al Jugador 2 y a tirar los dados.";
            this.btnJugar.Text = "Jugar";
            this.btnJugar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnJugar.UseVisualStyleBackColor = false;
            this.btnJugar.Click += new System.EventHandler(this.btnJugar_Click);
            this.btnJugar.Paint += new System.Windows.Forms.PaintEventHandler(this.Tarjeta_Paint);
            //
            // btnEstadisticas
            //
            this.btnEstadisticas.BackColor = System.Drawing.Color.White;
            this.btnEstadisticas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstadisticas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(236, 205, 218);
            this.btnEstadisticas.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnEstadisticas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnEstadisticas.ForeColor = Tema.Vino;
            this.btnEstadisticas.Location = new System.Drawing.Point(32, 160);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Padding = new System.Windows.Forms.Padding(20, 22, 20, 0);
            this.btnEstadisticas.Size = new System.Drawing.Size(296, 140);
            this.btnEstadisticas.TabIndex = 1;
            this.btnEstadisticas.Tag = "Partidas ganadas, perdidas y empatadas, y los movimientos de cada partida.";
            this.btnEstadisticas.Text = "Estadísticas";
            this.btnEstadisticas.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            this.btnEstadisticas.Paint += new System.Windows.Forms.PaintEventHandler(this.Tarjeta_Paint);
            //
            // btnBitacora
            //
            this.btnBitacora.BackColor = System.Drawing.Color.White;
            this.btnBitacora.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBitacora.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(236, 205, 218);
            this.btnBitacora.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnBitacora.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnBitacora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBitacora.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnBitacora.ForeColor = Tema.Vino;
            this.btnBitacora.Location = new System.Drawing.Point(352, 160);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Padding = new System.Windows.Forms.Padding(20, 22, 20, 0);
            this.btnBitacora.Size = new System.Drawing.Size(296, 140);
            this.btnBitacora.TabIndex = 2;
            this.btnBitacora.Tag = "Inicios y cierres de sesión, partidas, backups y restores, con filtros.";
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnBitacora.UseVisualStyleBackColor = false;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            this.btnBitacora.Paint += new System.Windows.Forms.PaintEventHandler(this.Tarjeta_Paint);
            //
            // btnReglas
            //
            this.btnReglas.BackColor = System.Drawing.Color.White;
            this.btnReglas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReglas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(236, 205, 218);
            this.btnReglas.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnReglas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnReglas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReglas.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnReglas.ForeColor = Tema.Vino;
            this.btnReglas.Location = new System.Drawing.Point(672, 160);
            this.btnReglas.Name = "btnReglas";
            this.btnReglas.Padding = new System.Windows.Forms.Padding(20, 22, 20, 0);
            this.btnReglas.Size = new System.Drawing.Size(296, 140);
            this.btnReglas.TabIndex = 3;
            this.btnReglas.Tag = "Cómo se juega, las 11 categorías y cuánto vale cada una.";
            this.btnReglas.Text = "Reglas";
            this.btnReglas.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnReglas.UseVisualStyleBackColor = false;
            this.btnReglas.Click += new System.EventHandler(this.btnReglas_Click);
            this.btnReglas.Paint += new System.Windows.Forms.PaintEventHandler(this.Tarjeta_Paint);
            //
            // lblAdministracion
            //
            this.lblAdministracion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdministracion.ForeColor = System.Drawing.Color.FromArgb(150, 100, 120);
            this.lblAdministracion.Location = new System.Drawing.Point(32, 326);
            this.lblAdministracion.Name = "lblAdministracion";
            this.lblAdministracion.Size = new System.Drawing.Size(936, 20);
            this.lblAdministracion.TabIndex = 4;
            this.lblAdministracion.Text = "ADMINISTRACIÓN Y SESIÓN";
            this.lblAdministracion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnBackup
            //
            this.btnBackup.BackColor = System.Drawing.Color.White;
            this.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackup.FlatAppearance.BorderColor = Tema.Rosa;
            this.btnBackup.FlatAppearance.BorderSize = 2;
            this.btnBackup.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnBackup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = Tema.Rosa;
            this.btnBackup.Location = new System.Drawing.Point(146, 354);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(220, 48);
            this.btnBackup.TabIndex = 5;
            this.btnBackup.Text = "Hacer backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            //
            // btnRestore
            //
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestore.FlatAppearance.BorderColor = Tema.Rosa;
            this.btnRestore.FlatAppearance.BorderSize = 2;
            this.btnRestore.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = Tema.Rosa;
            this.btnRestore.Location = new System.Drawing.Point(390, 354);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(220, 48);
            this.btnRestore.TabIndex = 6;
            this.btnRestore.Text = "Restaurar backup";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.BackColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderColor = Tema.Vino;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 2;
            this.btnCerrarSesion.FlatAppearance.MouseDownBackColor = Tema.RosaClaro;
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 236, 243);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = Tema.Vino;
            this.btnCerrarSesion.Location = new System.Drawing.Point(634, 354);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(220, 48);
            this.btnCerrarSesion.TabIndex = 7;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            //
            // lblPie
            //
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(150, 120, 135);
            this.lblPie.Location = new System.Drawing.Point(32, 446);
            this.lblPie.Name = "lblPie";
            this.lblPie.Size = new System.Drawing.Size(936, 20);
            this.lblPie.TabIndex = 8;
            this.lblPie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // sfdBackup
            //
            this.sfdBackup.Filter = "Backup de SQL Server (*.bak)|*.bak";
            this.sfdBackup.Title = "Guardar backup";
            //
            // ofdRestore
            //
            this.ofdRestore.Filter = "Backup de SQL Server (*.bak)|*.bak";
            this.ofdRestore.Title = "Restaurar backup";
            //
            // FormMenu
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::GUI.Properties.Resources.generala;
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Menú";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.pnlBotones.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblSesion;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnJugar;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnReglas;
        private System.Windows.Forms.Label lblAdministracion;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblPie;
        private System.Windows.Forms.SaveFileDialog sfdBackup;
        private System.Windows.Forms.OpenFileDialog ofdRestore;
    }
}
