namespace GUI
{
    partial class FormPrincipal
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlJugadores = new System.Windows.Forms.Panel();
            this.btnAbandonar = new System.Windows.Forms.Button();
            this.btnComenzarPartida = new System.Windows.Forms.Button();
            this.btnLoginJugador2 = new System.Windows.Forms.Button();
            this.lblJugador2 = new System.Windows.Forms.Label();
            this.lblJugador1 = new System.Windows.Forms.Label();
            this.pnlJuego = new System.Windows.Forms.Panel();
            this.btnAnotar = new System.Windows.Forms.Button();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.btnTirarDados = new System.Windows.Forms.Button();
            this.chkGuardar5 = new System.Windows.Forms.CheckBox();
            this.chkGuardar4 = new System.Windows.Forms.CheckBox();
            this.chkGuardar3 = new System.Windows.Forms.CheckBox();
            this.chkGuardar2 = new System.Windows.Forms.CheckBox();
            this.chkGuardar1 = new System.Windows.Forms.CheckBox();
            this.lblDado5 = new System.Windows.Forms.Label();
            this.lblDado4 = new System.Windows.Forms.Label();
            this.lblDado3 = new System.Windows.Forms.Label();
            this.lblDado2 = new System.Windows.Forms.Label();
            this.lblDado1 = new System.Windows.Forms.Label();
            this.lblTirosRestantes = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.pnlPuntajes = new System.Windows.Forms.Panel();
            this.lblTotalJugador2 = new System.Windows.Forms.Label();
            this.lblTotalJugador1 = new System.Windows.Forms.Label();
            this.dgvPuntajes = new System.Windows.Forms.DataGridView();
            this.pnlEncabezado.SuspendLayout();
            this.pnlJugadores.SuspendLayout();
            this.pnlJuego.SuspendLayout();
            this.pnlPuntajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntajes)).BeginInit();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.HotPink;
            this.pnlEncabezado.Controls.Add(this.btnCerrarSesion);
            this.pnlEncabezado.Controls.Add(this.btnRestore);
            this.pnlEncabezado.Controls.Add(this.btnBackup);
            this.pnlEncabezado.Controls.Add(this.btnEstadisticas);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 70);
            this.pnlEncabezado.TabIndex = 0;
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.BackColor = System.Drawing.Color.DeepPink;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(890, 15);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(95, 40);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Salir";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            //
            // btnRestore
            //
            this.btnRestore.BackColor = System.Drawing.Color.DeepPink;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.ForeColor = System.Drawing.Color.White;
            this.btnRestore.Location = new System.Drawing.Point(790, 15);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(90, 40);
            this.btnRestore.TabIndex = 3;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // btnBackup
            //
            this.btnBackup.BackColor = System.Drawing.Color.DeepPink;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.ForeColor = System.Drawing.Color.White;
            this.btnBackup.Location = new System.Drawing.Point(690, 15);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(90, 40);
            this.btnBackup.TabIndex = 2;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = false;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            //
            // btnEstadisticas
            //
            this.btnEstadisticas.BackColor = System.Drawing.Color.DeepPink;
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.ForeColor = System.Drawing.Color.White;
            this.btnEstadisticas.Location = new System.Drawing.Point(560, 15);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(120, 40);
            this.btnEstadisticas.TabIndex = 1;
            this.btnEstadisticas.Text = "Estadísticas";
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(197, 46);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "GENERALA";
            //
            // pnlJugadores
            //
            this.pnlJugadores.BackColor = System.Drawing.Color.White;
            this.pnlJugadores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlJugadores.Controls.Add(this.btnAbandonar);
            this.pnlJugadores.Controls.Add(this.btnComenzarPartida);
            this.pnlJugadores.Controls.Add(this.btnLoginJugador2);
            this.pnlJugadores.Controls.Add(this.lblJugador2);
            this.pnlJugadores.Controls.Add(this.lblJugador1);
            this.pnlJugadores.Location = new System.Drawing.Point(20, 90);
            this.pnlJugadores.Name = "pnlJugadores";
            this.pnlJugadores.Size = new System.Drawing.Size(960, 60);
            this.pnlJugadores.TabIndex = 1;
            //
            // btnAbandonar
            //
            this.btnAbandonar.BackColor = System.Drawing.Color.Crimson;
            this.btnAbandonar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbandonar.ForeColor = System.Drawing.Color.White;
            this.btnAbandonar.Location = new System.Drawing.Point(760, 14);
            this.btnAbandonar.Name = "btnAbandonar";
            this.btnAbandonar.Size = new System.Drawing.Size(185, 32);
            this.btnAbandonar.TabIndex = 4;
            this.btnAbandonar.Text = "Abandonar Partida";
            this.btnAbandonar.UseVisualStyleBackColor = false;
            this.btnAbandonar.Visible = false;
            this.btnAbandonar.Click += new System.EventHandler(this.btnAbandonar_Click);
            //
            // btnComenzarPartida
            //
            this.btnComenzarPartida.BackColor = System.Drawing.Color.DeepPink;
            this.btnComenzarPartida.Enabled = false;
            this.btnComenzarPartida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComenzarPartida.ForeColor = System.Drawing.Color.White;
            this.btnComenzarPartida.Location = new System.Drawing.Point(760, 14);
            this.btnComenzarPartida.Name = "btnComenzarPartida";
            this.btnComenzarPartida.Size = new System.Drawing.Size(185, 32);
            this.btnComenzarPartida.TabIndex = 3;
            this.btnComenzarPartida.Text = "Comenzar Partida";
            this.btnComenzarPartida.UseVisualStyleBackColor = false;
            this.btnComenzarPartida.Click += new System.EventHandler(this.btnComenzarPartida_Click);
            //
            // btnLoginJugador2
            //
            this.btnLoginJugador2.BackColor = System.Drawing.Color.White;
            this.btnLoginJugador2.FlatAppearance.BorderColor = System.Drawing.Color.DeepPink;
            this.btnLoginJugador2.FlatAppearance.BorderSize = 2;
            this.btnLoginJugador2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginJugador2.ForeColor = System.Drawing.Color.DeepPink;
            this.btnLoginJugador2.Location = new System.Drawing.Point(590, 14);
            this.btnLoginJugador2.Name = "btnLoginJugador2";
            this.btnLoginJugador2.Size = new System.Drawing.Size(160, 32);
            this.btnLoginJugador2.TabIndex = 2;
            this.btnLoginJugador2.Text = "Ingresar Jugador 2";
            this.btnLoginJugador2.UseVisualStyleBackColor = false;
            this.btnLoginJugador2.Click += new System.EventHandler(this.btnLoginJugador2_Click);
            //
            // lblJugador2
            //
            this.lblJugador2.AutoSize = true;
            this.lblJugador2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblJugador2.ForeColor = System.Drawing.Color.DeepPink;
            this.lblJugador2.Location = new System.Drawing.Point(310, 18);
            this.lblJugador2.Name = "lblJugador2";
            this.lblJugador2.Size = new System.Drawing.Size(240, 20);
            this.lblJugador2.TabIndex = 1;
            this.lblJugador2.Text = "Jugador 2: (sin ingresar)";
            //
            // lblJugador1
            //
            this.lblJugador1.AutoSize = true;
            this.lblJugador1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblJugador1.ForeColor = System.Drawing.Color.DeepPink;
            this.lblJugador1.Location = new System.Drawing.Point(15, 18);
            this.lblJugador1.Name = "lblJugador1";
            this.lblJugador1.Size = new System.Drawing.Size(120, 20);
            this.lblJugador1.TabIndex = 0;
            this.lblJugador1.Text = "Jugador 1: -";
            //
            // pnlJuego
            //
            this.pnlJuego.BackColor = System.Drawing.Color.White;
            this.pnlJuego.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlJuego.Controls.Add(this.btnAnotar);
            this.pnlJuego.Controls.Add(this.cmbCategoria);
            this.pnlJuego.Controls.Add(this.btnTirarDados);
            this.pnlJuego.Controls.Add(this.chkGuardar5);
            this.pnlJuego.Controls.Add(this.chkGuardar4);
            this.pnlJuego.Controls.Add(this.chkGuardar3);
            this.pnlJuego.Controls.Add(this.chkGuardar2);
            this.pnlJuego.Controls.Add(this.chkGuardar1);
            this.pnlJuego.Controls.Add(this.lblDado5);
            this.pnlJuego.Controls.Add(this.lblDado4);
            this.pnlJuego.Controls.Add(this.lblDado3);
            this.pnlJuego.Controls.Add(this.lblDado2);
            this.pnlJuego.Controls.Add(this.lblDado1);
            this.pnlJuego.Controls.Add(this.lblTirosRestantes);
            this.pnlJuego.Controls.Add(this.lblTurno);
            this.pnlJuego.Location = new System.Drawing.Point(20, 160);
            this.pnlJuego.Name = "pnlJuego";
            this.pnlJuego.Size = new System.Drawing.Size(960, 190);
            this.pnlJuego.TabIndex = 2;
            //
            // btnAnotar
            //
            this.btnAnotar.BackColor = System.Drawing.Color.White;
            this.btnAnotar.Enabled = false;
            this.btnAnotar.FlatAppearance.BorderColor = System.Drawing.Color.DeepPink;
            this.btnAnotar.FlatAppearance.BorderSize = 2;
            this.btnAnotar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnotar.ForeColor = System.Drawing.Color.DeepPink;
            this.btnAnotar.Location = new System.Drawing.Point(705, 138);
            this.btnAnotar.Name = "btnAnotar";
            this.btnAnotar.Size = new System.Drawing.Size(130, 32);
            this.btnAnotar.TabIndex = 14;
            this.btnAnotar.Text = "Anotar";
            this.btnAnotar.UseVisualStyleBackColor = false;
            this.btnAnotar.Click += new System.EventHandler(this.btnAnotar_Click);
            //
            // cmbCategoria
            //
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(475, 140);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(220, 28);
            this.cmbCategoria.TabIndex = 13;
            //
            // btnTirarDados
            //
            this.btnTirarDados.BackColor = System.Drawing.Color.DeepPink;
            this.btnTirarDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTirarDados.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTirarDados.ForeColor = System.Drawing.Color.White;
            this.btnTirarDados.Location = new System.Drawing.Point(475, 60);
            this.btnTirarDados.Name = "btnTirarDados";
            this.btnTirarDados.Size = new System.Drawing.Size(160, 50);
            this.btnTirarDados.TabIndex = 12;
            this.btnTirarDados.Text = "Tirar Dados";
            this.btnTirarDados.UseVisualStyleBackColor = false;
            this.btnTirarDados.Click += new System.EventHandler(this.btnTirarDados_Click);
            //
            // chkGuardar5
            //
            this.chkGuardar5.AutoSize = true;
            this.chkGuardar5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar5.Location = new System.Drawing.Point(399, 140);
            this.chkGuardar5.Name = "chkGuardar5";
            this.chkGuardar5.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar5.TabIndex = 11;
            this.chkGuardar5.Text = "Guardar";
            this.chkGuardar5.UseVisualStyleBackColor = true;
            //
            // chkGuardar4
            //
            this.chkGuardar4.AutoSize = true;
            this.chkGuardar4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar4.Location = new System.Drawing.Point(304, 140);
            this.chkGuardar4.Name = "chkGuardar4";
            this.chkGuardar4.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar4.TabIndex = 10;
            this.chkGuardar4.Text = "Guardar";
            this.chkGuardar4.UseVisualStyleBackColor = true;
            //
            // chkGuardar3
            //
            this.chkGuardar3.AutoSize = true;
            this.chkGuardar3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar3.Location = new System.Drawing.Point(209, 140);
            this.chkGuardar3.Name = "chkGuardar3";
            this.chkGuardar3.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar3.TabIndex = 9;
            this.chkGuardar3.Text = "Guardar";
            this.chkGuardar3.UseVisualStyleBackColor = true;
            //
            // chkGuardar2
            //
            this.chkGuardar2.AutoSize = true;
            this.chkGuardar2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar2.Location = new System.Drawing.Point(114, 140);
            this.chkGuardar2.Name = "chkGuardar2";
            this.chkGuardar2.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar2.TabIndex = 8;
            this.chkGuardar2.Text = "Guardar";
            this.chkGuardar2.UseVisualStyleBackColor = true;
            //
            // chkGuardar1
            //
            this.chkGuardar1.AutoSize = true;
            this.chkGuardar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar1.Location = new System.Drawing.Point(19, 140);
            this.chkGuardar1.Name = "chkGuardar1";
            this.chkGuardar1.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar1.TabIndex = 7;
            this.chkGuardar1.Text = "Guardar";
            this.chkGuardar1.UseVisualStyleBackColor = true;
            //
            // lblDado5
            //
            this.lblDado5.BackColor = System.Drawing.Color.MistyRose;
            this.lblDado5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado5.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblDado5.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDado5.Location = new System.Drawing.Point(375, 55);
            this.lblDado5.Name = "lblDado5";
            this.lblDado5.Size = new System.Drawing.Size(80, 80);
            this.lblDado5.TabIndex = 6;
            this.lblDado5.Text = "-";
            this.lblDado5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDado4
            //
            this.lblDado4.BackColor = System.Drawing.Color.MistyRose;
            this.lblDado4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado4.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblDado4.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDado4.Location = new System.Drawing.Point(285, 55);
            this.lblDado4.Name = "lblDado4";
            this.lblDado4.Size = new System.Drawing.Size(80, 80);
            this.lblDado4.TabIndex = 5;
            this.lblDado4.Text = "-";
            this.lblDado4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDado3
            //
            this.lblDado3.BackColor = System.Drawing.Color.MistyRose;
            this.lblDado3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado3.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblDado3.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDado3.Location = new System.Drawing.Point(195, 55);
            this.lblDado3.Name = "lblDado3";
            this.lblDado3.Size = new System.Drawing.Size(80, 80);
            this.lblDado3.TabIndex = 4;
            this.lblDado3.Text = "-";
            this.lblDado3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDado2
            //
            this.lblDado2.BackColor = System.Drawing.Color.MistyRose;
            this.lblDado2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado2.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblDado2.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDado2.Location = new System.Drawing.Point(105, 55);
            this.lblDado2.Name = "lblDado2";
            this.lblDado2.Size = new System.Drawing.Size(80, 80);
            this.lblDado2.TabIndex = 3;
            this.lblDado2.Text = "-";
            this.lblDado2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblDado1
            //
            this.lblDado1.BackColor = System.Drawing.Color.MistyRose;
            this.lblDado1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDado1.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblDado1.ForeColor = System.Drawing.Color.DeepPink;
            this.lblDado1.Location = new System.Drawing.Point(15, 55);
            this.lblDado1.Name = "lblDado1";
            this.lblDado1.Size = new System.Drawing.Size(80, 80);
            this.lblDado1.TabIndex = 2;
            this.lblDado1.Text = "-";
            this.lblDado1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTirosRestantes
            //
            this.lblTirosRestantes.AutoSize = true;
            this.lblTirosRestantes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTirosRestantes.ForeColor = System.Drawing.Color.DimGray;
            this.lblTirosRestantes.Location = new System.Drawing.Point(330, 15);
            this.lblTirosRestantes.Name = "lblTirosRestantes";
            this.lblTirosRestantes.Size = new System.Drawing.Size(130, 20);
            this.lblTirosRestantes.TabIndex = 1;
            this.lblTirosRestantes.Text = "Tiros restantes: -";
            //
            // lblTurno
            //
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTurno.ForeColor = System.Drawing.Color.Crimson;
            this.lblTurno.Location = new System.Drawing.Point(15, 12);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(120, 25);
            this.lblTurno.TabIndex = 0;
            this.lblTurno.Text = "Turno de: -";
            //
            // pnlPuntajes
            //
            this.pnlPuntajes.BackColor = System.Drawing.Color.White;
            this.pnlPuntajes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPuntajes.Controls.Add(this.lblTotalJugador2);
            this.pnlPuntajes.Controls.Add(this.lblTotalJugador1);
            this.pnlPuntajes.Controls.Add(this.dgvPuntajes);
            this.pnlPuntajes.Location = new System.Drawing.Point(20, 360);
            this.pnlPuntajes.Name = "pnlPuntajes";
            this.pnlPuntajes.Size = new System.Drawing.Size(960, 255);
            this.pnlPuntajes.TabIndex = 3;
            //
            // lblTotalJugador2
            //
            this.lblTotalJugador2.AutoSize = true;
            this.lblTotalJugador2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador2.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTotalJugador2.Location = new System.Drawing.Point(600, 60);
            this.lblTotalJugador2.Name = "lblTotalJugador2";
            this.lblTotalJugador2.Size = new System.Drawing.Size(160, 25);
            this.lblTotalJugador2.TabIndex = 2;
            this.lblTotalJugador2.Text = "Total Jugador 2: 0";
            //
            // lblTotalJugador1
            //
            this.lblTotalJugador1.AutoSize = true;
            this.lblTotalJugador1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador1.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTotalJugador1.Location = new System.Drawing.Point(600, 20);
            this.lblTotalJugador1.Name = "lblTotalJugador1";
            this.lblTotalJugador1.Size = new System.Drawing.Size(160, 25);
            this.lblTotalJugador1.TabIndex = 1;
            this.lblTotalJugador1.Text = "Total Jugador 1: 0";
            //
            // dgvPuntajes
            //
            this.dgvPuntajes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPuntajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.DeepPink;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvPuntajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPuntajes.EnableHeadersVisualStyles = false;
            this.dgvPuntajes.GridColor = System.Drawing.Color.MistyRose;
            this.dgvPuntajes.Location = new System.Drawing.Point(15, 15);
            this.dgvPuntajes.Name = "dgvPuntajes";
            this.dgvPuntajes.ReadOnly = true;
            this.dgvPuntajes.RowHeadersVisible = false;
            this.dgvPuntajes.Size = new System.Drawing.Size(560, 225);
            this.dgvPuntajes.TabIndex = 0;
            this.dgvPuntajes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPuntajes_CellFormatting);
            //
            // FormPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlPuntajes);
            this.Controls.Add(this.pnlJuego);
            this.Controls.Add(this.pnlJugadores);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlJugadores.ResumeLayout(false);
            this.pnlJugadores.PerformLayout();
            this.pnlJuego.ResumeLayout(false);
            this.pnlJuego.PerformLayout();
            this.pnlPuntajes.ResumeLayout(false);
            this.pnlPuntajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntajes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Panel pnlJugadores;
        private System.Windows.Forms.Label lblJugador1;
        private System.Windows.Forms.Label lblJugador2;
        private System.Windows.Forms.Button btnLoginJugador2;
        private System.Windows.Forms.Button btnComenzarPartida;
        private System.Windows.Forms.Button btnAbandonar;
        private System.Windows.Forms.Panel pnlJuego;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label lblTirosRestantes;
        private System.Windows.Forms.Label lblDado1;
        private System.Windows.Forms.Label lblDado2;
        private System.Windows.Forms.Label lblDado3;
        private System.Windows.Forms.Label lblDado4;
        private System.Windows.Forms.Label lblDado5;
        private System.Windows.Forms.CheckBox chkGuardar1;
        private System.Windows.Forms.CheckBox chkGuardar2;
        private System.Windows.Forms.CheckBox chkGuardar3;
        private System.Windows.Forms.CheckBox chkGuardar4;
        private System.Windows.Forms.CheckBox chkGuardar5;
        private System.Windows.Forms.Button btnTirarDados;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnAnotar;
        private System.Windows.Forms.Panel pnlPuntajes;
        private System.Windows.Forms.DataGridView dgvPuntajes;
        private System.Windows.Forms.Label lblTotalJugador1;
        private System.Windows.Forms.Label lblTotalJugador2;
    }
}
