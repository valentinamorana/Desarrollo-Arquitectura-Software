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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnJugar = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.btnReglas = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblPie = new System.Windows.Forms.Label();
            this.pnlTitulo.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTitulo
            //
            this.pnlTitulo.BackColor = Tema.Rosa;
            this.pnlTitulo.Controls.Add(this.lblBienvenida);
            this.pnlTitulo.Controls.Add(this.lblSubtitulo);
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(640, 130);
            this.pnlTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(36, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(192, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GENERALA";
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.lblSubtitulo.Location = new System.Drawing.Point(40, 75);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(230, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Juego de dados para 2 jugadores";
            //
            // lblBienvenida
            //
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBienvenida.ForeColor = System.Drawing.Color.White;
            this.lblBienvenida.Location = new System.Drawing.Point(400, 44);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(200, 19);
            this.lblBienvenida.TabIndex = 2;
            this.lblBienvenida.Text = "Sesión iniciada";
            //
            // pnlBotones
            //
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.pnlBotones.Controls.Add(this.lblPie);
            this.pnlBotones.Controls.Add(this.btnCerrarSesion);
            this.pnlBotones.Controls.Add(this.btnReglas);
            this.pnlBotones.Controls.Add(this.btnBitacora);
            this.pnlBotones.Controls.Add(this.btnEstadisticas);
            this.pnlBotones.Controls.Add(this.btnJugar);
            this.pnlBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBotones.Location = new System.Drawing.Point(0, 130);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(640, 370);
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
            this.btnJugar.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnJugar.ForeColor = System.Drawing.Color.White;
            this.btnJugar.Location = new System.Drawing.Point(150, 30);
            this.btnJugar.Name = "btnJugar";
            this.btnJugar.Size = new System.Drawing.Size(340, 64);
            this.btnJugar.TabIndex = 1;
            this.btnJugar.Text = "▶  Jugar";
            this.btnJugar.UseVisualStyleBackColor = false;
            this.btnJugar.Click += new System.EventHandler(this.btnJugar_Click);
            //
            // btnEstadisticas
            //
            this.btnEstadisticas.BackColor = Tema.Rosa;
            this.btnEstadisticas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstadisticas.FlatAppearance.BorderSize = 0;
            this.btnEstadisticas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(120, 10, 60);
            this.btnEstadisticas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 60, 120);
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnEstadisticas.ForeColor = System.Drawing.Color.White;
            this.btnEstadisticas.Location = new System.Drawing.Point(150, 110);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(340, 52);
            this.btnEstadisticas.TabIndex = 2;
            this.btnEstadisticas.Text = "Estadísticas";
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            //
            // btnBitacora
            //
            this.btnBitacora.BackColor = Tema.Rosa;
            this.btnBitacora.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBitacora.FlatAppearance.BorderSize = 0;
            this.btnBitacora.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(120, 10, 60);
            this.btnBitacora.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 60, 120);
            this.btnBitacora.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBitacora.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnBitacora.ForeColor = System.Drawing.Color.White;
            this.btnBitacora.Location = new System.Drawing.Point(150, 178);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(340, 52);
            this.btnBitacora.TabIndex = 3;
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.UseVisualStyleBackColor = false;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            //
            // btnReglas
            //
            this.btnReglas.BackColor = Tema.Rosa;
            this.btnReglas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReglas.FlatAppearance.BorderSize = 0;
            this.btnReglas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(120, 10, 60);
            this.btnReglas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(200, 60, 120);
            this.btnReglas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReglas.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnReglas.ForeColor = System.Drawing.Color.White;
            this.btnReglas.Location = new System.Drawing.Point(150, 246);
            this.btnReglas.Name = "btnReglas";
            this.btnReglas.Size = new System.Drawing.Size(340, 52);
            this.btnReglas.TabIndex = 4;
            this.btnReglas.Text = "Reglas";
            this.btnReglas.UseVisualStyleBackColor = false;
            this.btnReglas.Click += new System.EventHandler(this.btnReglas_Click);
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.BackColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderColor = Tema.Vino;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 2;
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCerrarSesion.ForeColor = Tema.Vino;
            this.btnCerrarSesion.Location = new System.Drawing.Point(150, 318);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(340, 44);
            this.btnCerrarSesion.TabIndex = 5;
            this.btnCerrarSesion.Text = "Cerrar sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            //
            // lblPie
            //
            this.lblPie.AutoSize = true;
            this.lblPie.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPie.ForeColor = System.Drawing.Color.FromArgb(150, 120, 135);
            this.lblPie.Location = new System.Drawing.Point(150, 400);
            this.lblPie.Name = "lblPie";
            this.lblPie.Size = new System.Drawing.Size(0, 15);
            this.lblPie.TabIndex = 6;
            //
            // FormMenu
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 500);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Menú";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.pnlBotones.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnJugar;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnReglas;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblPie;
    }
}
