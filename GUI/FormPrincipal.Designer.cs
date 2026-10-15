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
            this.btnReglas = new System.Windows.Forms.Button();
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
            this.ucDado1 = new GUI.ucDado();
            this.ucDado2 = new GUI.ucDado();
            this.ucDado3 = new GUI.ucDado();
            this.ucDado4 = new GUI.ucDado();
            this.ucDado5 = new GUI.ucDado();
            this.lblTirosRestantes = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblTurnoNombre = new System.Windows.Forms.Label();
            this.pnlPuntajes = new System.Windows.Forms.Panel();
            this.lblTotalJugador2 = new System.Windows.Forms.Label();
            this.lblVaGanando = new System.Windows.Forms.Label();
            this.lblTotalJugador1 = new System.Windows.Forms.Label();
            this.dgvPuntajes = new System.Windows.Forms.DataGridView();
            this.picDecoracion = new System.Windows.Forms.PictureBox();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.ofdRestore = new System.Windows.Forms.OpenFileDialog();
            this.pnlEncabezado.SuspendLayout();
            this.pnlJugadores.SuspendLayout();
            this.pnlJuego.SuspendLayout();
            this.pnlPuntajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntajes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDecoracion)).BeginInit();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.pnlEncabezado.Controls.Add(this.btnCerrarSesion);
            this.pnlEncabezado.Controls.Add(this.btnRestore);
            this.pnlEncabezado.Controls.Add(this.btnBackup);
            this.pnlEncabezado.Controls.Add(this.btnEstadisticas);
            this.pnlEncabezado.Controls.Add(this.btnReglas);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 70);
            this.pnlEncabezado.TabIndex = 0;
            //
            // btnCerrarSesion
            //
            this.btnCerrarSesion.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 2;
            this.btnCerrarSesion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(110, 10, 55);
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(140, 15, 70);
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
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
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
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
            this.btnBackup.BackColor = System.Drawing.Color.White;
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
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
            this.btnEstadisticas.BackColor = System.Drawing.Color.White;
            this.btnEstadisticas.FlatAppearance.BorderSize = 0;
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEstadisticas.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnEstadisticas.Location = new System.Drawing.Point(560, 15);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(120, 40);
            this.btnEstadisticas.TabIndex = 1;
            this.btnEstadisticas.Text = "Estadísticas";
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            //
            // btnReglas
            //
            this.btnReglas.BackColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.btnReglas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReglas.FlatAppearance.BorderSize = 0;
            this.btnReglas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(230, 180, 200);
            this.btnReglas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReglas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReglas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReglas.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnReglas.Location = new System.Drawing.Point(235, 20);
            this.btnReglas.Name = "btnReglas";
            this.btnReglas.Size = new System.Drawing.Size(125, 30);
            this.btnReglas.TabIndex = 5;
            this.btnReglas.Text = "¿Cómo jugar?";
            this.btnReglas.UseVisualStyleBackColor = false;
            this.btnReglas.Click += new System.EventHandler(this.btnReglas_Click);
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
            this.btnAbandonar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAbandonar.ForeColor = System.Drawing.Color.White;
            this.btnAbandonar.Location = new System.Drawing.Point(770, 14);
            this.btnAbandonar.Name = "btnAbandonar";
            this.btnAbandonar.Size = new System.Drawing.Size(175, 32);
            this.btnAbandonar.TabIndex = 4;
            this.btnAbandonar.Text = "Abandonar Partida";
            this.btnAbandonar.UseVisualStyleBackColor = false;
            this.btnAbandonar.Visible = false;
            this.btnAbandonar.Click += new System.EventHandler(this.btnAbandonar_Click);
            //
            // btnComenzarPartida
            //
            this.btnComenzarPartida.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnComenzarPartida.Enabled = false;
            this.btnComenzarPartida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComenzarPartida.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnComenzarPartida.ForeColor = System.Drawing.Color.White;
            this.btnComenzarPartida.Location = new System.Drawing.Point(770, 14);
            this.btnComenzarPartida.Name = "btnComenzarPartida";
            this.btnComenzarPartida.Size = new System.Drawing.Size(175, 32);
            this.btnComenzarPartida.TabIndex = 3;
            this.btnComenzarPartida.Text = "Comenzar Partida";
            this.btnComenzarPartida.UseVisualStyleBackColor = false;
            this.btnComenzarPartida.Click += new System.EventHandler(this.btnComenzarPartida_Click);
            this.btnComenzarPartida.EnabledChanged += new System.EventHandler(this.btnPrincipal_EnabledChanged);
            //
            // btnLoginJugador2
            //
            this.btnLoginJugador2.BackColor = System.Drawing.Color.FromArgb(253, 235, 242);
            this.btnLoginJugador2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnLoginJugador2.FlatAppearance.BorderSize = 2;
            this.btnLoginJugador2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginJugador2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoginJugador2.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnLoginJugador2.Location = new System.Drawing.Point(590, 14);
            this.btnLoginJugador2.Name = "btnLoginJugador2";
            this.btnLoginJugador2.Size = new System.Drawing.Size(170, 32);
            this.btnLoginJugador2.TabIndex = 2;
            this.btnLoginJugador2.Text = "Ingresar Jugador 2";
            this.btnLoginJugador2.UseVisualStyleBackColor = false;
            this.btnLoginJugador2.Click += new System.EventHandler(this.btnLoginJugador2_Click);
            //
            // lblJugador2
            //
            this.lblJugador2.AutoEllipsis = true;
            this.lblJugador2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblJugador2.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblJugador2.Location = new System.Drawing.Point(310, 18);
            this.lblJugador2.Name = "lblJugador2";
            this.lblJugador2.Size = new System.Drawing.Size(270, 22);
            this.lblJugador2.TabIndex = 1;
            this.lblJugador2.Text = "Jugador 2: (sin ingresar)";
            this.lblJugador2.UseMnemonic = false;
            //
            // lblJugador1
            //
            this.lblJugador1.AutoSize = true;
            this.lblJugador1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblJugador1.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblJugador1.Location = new System.Drawing.Point(15, 18);
            this.lblJugador1.Name = "lblJugador1";
            this.lblJugador1.Size = new System.Drawing.Size(120, 20);
            this.lblJugador1.TabIndex = 0;
            this.lblJugador1.Text = "Jugador 1: -";
            this.lblJugador1.UseMnemonic = false;
            //
            // pnlJuego
            //
            this.pnlJuego.BackColor = System.Drawing.Color.White;
            this.pnlJuego.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlJuego.Controls.Add(this.btnAnotar);
            this.pnlJuego.Controls.Add(this.cmbCategoria);
            this.pnlJuego.Controls.Add(this.btnTirarDados);
            this.pnlJuego.Controls.Add(this.ucDado5);
            this.pnlJuego.Controls.Add(this.ucDado4);
            this.pnlJuego.Controls.Add(this.ucDado3);
            this.pnlJuego.Controls.Add(this.ucDado2);
            this.pnlJuego.Controls.Add(this.ucDado1);
            this.pnlJuego.Controls.Add(this.lblTirosRestantes);
            this.pnlJuego.Controls.Add(this.lblTurnoNombre);
            this.pnlJuego.Controls.Add(this.lblTurno);
            this.pnlJuego.Location = new System.Drawing.Point(20, 160);
            this.pnlJuego.Name = "pnlJuego";
            this.pnlJuego.Size = new System.Drawing.Size(960, 190);
            this.pnlJuego.TabIndex = 2;
            //
            // btnAnotar
            //
            this.btnAnotar.BackColor = System.Drawing.Color.FromArgb(253, 235, 242);
            this.btnAnotar.Enabled = false;
            this.btnAnotar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnAnotar.FlatAppearance.BorderSize = 2;
            this.btnAnotar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnotar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnotar.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
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
            this.btnTirarDados.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
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
            this.btnTirarDados.EnabledChanged += new System.EventHandler(this.btnPrincipal_EnabledChanged);
            //
            // ucDado1
            //
            this.ucDado1.BackColor = System.Drawing.Color.White;
            this.ucDado1.Location = new System.Drawing.Point(15, 55);
            this.ucDado1.Name = "ucDado1";
            this.ucDado1.Size = new System.Drawing.Size(80, 112);
            this.ucDado1.TabIndex = 2;
            //
            // ucDado2
            //
            this.ucDado2.BackColor = System.Drawing.Color.White;
            this.ucDado2.Location = new System.Drawing.Point(105, 55);
            this.ucDado2.Name = "ucDado2";
            this.ucDado2.Size = new System.Drawing.Size(80, 112);
            this.ucDado2.TabIndex = 3;
            //
            // ucDado3
            //
            this.ucDado3.BackColor = System.Drawing.Color.White;
            this.ucDado3.Location = new System.Drawing.Point(195, 55);
            this.ucDado3.Name = "ucDado3";
            this.ucDado3.Size = new System.Drawing.Size(80, 112);
            this.ucDado3.TabIndex = 4;
            //
            // ucDado4
            //
            this.ucDado4.BackColor = System.Drawing.Color.White;
            this.ucDado4.Location = new System.Drawing.Point(285, 55);
            this.ucDado4.Name = "ucDado4";
            this.ucDado4.Size = new System.Drawing.Size(80, 112);
            this.ucDado4.TabIndex = 5;
            //
            // ucDado5
            //
            this.ucDado5.BackColor = System.Drawing.Color.White;
            this.ucDado5.Location = new System.Drawing.Point(375, 55);
            this.ucDado5.Name = "ucDado5";
            this.ucDado5.Size = new System.Drawing.Size(80, 112);
            this.ucDado5.TabIndex = 6;
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
            this.lblTurno.ForeColor = System.Drawing.Color.Black;
            this.lblTurno.Location = new System.Drawing.Point(15, 12);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(95, 25);
            this.lblTurno.TabIndex = 0;
            this.lblTurno.Text = "Turno de:";
            this.lblTurno.UseMnemonic = false;
            //
            // lblTurnoNombre
            //
            this.lblTurnoNombre.AutoSize = true;
            this.lblTurnoNombre.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTurnoNombre.ForeColor = System.Drawing.Color.FromArgb(233, 30, 99);
            this.lblTurnoNombre.Location = new System.Drawing.Point(110, 12);
            this.lblTurnoNombre.Name = "lblTurnoNombre";
            this.lblTurnoNombre.Size = new System.Drawing.Size(20, 25);
            this.lblTurnoNombre.TabIndex = 11;
            this.lblTurnoNombre.Text = "-";
            this.lblTurnoNombre.UseMnemonic = false;
            //
            // pnlPuntajes
            //
            this.pnlPuntajes.BackColor = System.Drawing.Color.White;
            this.pnlPuntajes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPuntajes.Controls.Add(this.picDecoracion);
            this.pnlPuntajes.Controls.Add(this.lblVaGanando);
            this.pnlPuntajes.Controls.Add(this.lblTotalJugador2);
            this.pnlPuntajes.Controls.Add(this.lblTotalJugador1);
            this.pnlPuntajes.Controls.Add(this.dgvPuntajes);
            this.pnlPuntajes.Location = new System.Drawing.Point(20, 360);
            this.pnlPuntajes.Name = "pnlPuntajes";
            this.pnlPuntajes.Size = new System.Drawing.Size(960, 355);
            this.pnlPuntajes.TabIndex = 3;
            //
            // picDecoracion
            //
            this.picDecoracion.BackColor = System.Drawing.Color.Transparent;
            this.picDecoracion.Image = global::GUI.Properties.Resources.dados_cubilete;
            this.picDecoracion.Location = new System.Drawing.Point(415, 135);
            this.picDecoracion.Name = "picDecoracion";
            this.picDecoracion.Size = new System.Drawing.Size(530, 210);
            this.picDecoracion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDecoracion.TabIndex = 3;
            this.picDecoracion.TabStop = false;
            //
            // lblVaGanando
            //
            this.lblVaGanando.AutoSize = true;
            this.lblVaGanando.Font = new System.Drawing.Font("Sitka Banner", 12F, System.Drawing.FontStyle.Bold);
            this.lblVaGanando.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblVaGanando.Location = new System.Drawing.Point(600, 100);
            this.lblVaGanando.Name = "lblVaGanando";
            this.lblVaGanando.Size = new System.Drawing.Size(0, 30);
            this.lblVaGanando.TabIndex = 4;
            this.lblVaGanando.UseMnemonic = false;
            //
            // lblTotalJugador2
            //
            this.lblTotalJugador2.AutoSize = true;
            this.lblTotalJugador2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador2.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblTotalJugador2.Location = new System.Drawing.Point(600, 60);
            this.lblTotalJugador2.Name = "lblTotalJugador2";
            this.lblTotalJugador2.Size = new System.Drawing.Size(160, 25);
            this.lblTotalJugador2.TabIndex = 2;
            this.lblTotalJugador2.Text = "Total Jugador 2: 0";
            this.lblTotalJugador2.UseMnemonic = false;
            //
            // lblTotalJugador1
            //
            this.lblTotalJugador1.AutoSize = true;
            this.lblTotalJugador1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador1.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblTotalJugador1.Location = new System.Drawing.Point(600, 20);
            this.lblTotalJugador1.Name = "lblTotalJugador1";
            this.lblTotalJugador1.Size = new System.Drawing.Size(160, 25);
            this.lblTotalJugador1.TabIndex = 1;
            this.lblTotalJugador1.Text = "Total Jugador 1: 0";
            this.lblTotalJugador1.UseMnemonic = false;
            //
            // dgvPuntajes
            //
            this.dgvPuntajes.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(252, 240, 245);
            this.dgvPuntajes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPuntajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.dgvPuntajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPuntajes.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvPuntajes.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.dgvPuntajes.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(90, 10, 45);
            this.dgvPuntajes.EnableHeadersVisualStyles = false;
            this.dgvPuntajes.GridColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.dgvPuntajes.Location = new System.Drawing.Point(15, 15);
            this.dgvPuntajes.Name = "dgvPuntajes";
            this.dgvPuntajes.ReadOnly = true;
            this.dgvPuntajes.RowHeadersVisible = false;
            this.dgvPuntajes.RowTemplate.Height = 27;
            this.dgvPuntajes.Size = new System.Drawing.Size(385, 330);
            this.dgvPuntajes.TabIndex = 0;
            this.dgvPuntajes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPuntajes_CellFormatting);
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
            // FormPrincipal
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this.pnlPuntajes);
            this.Controls.Add(this.pnlJuego);
            this.Controls.Add(this.pnlJugadores);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::GUI.Properties.Resources.generala;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
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
            ((System.ComponentModel.ISupportInitialize)(this.picDecoracion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnReglas;
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
        private System.Windows.Forms.Label lblTurnoNombre;
        private System.Windows.Forms.Label lblTirosRestantes;
        private GUI.ucDado ucDado1;
        private GUI.ucDado ucDado2;
        private GUI.ucDado ucDado3;
        private GUI.ucDado ucDado4;
        private GUI.ucDado ucDado5;
        private System.Windows.Forms.Button btnTirarDados;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnAnotar;
        private System.Windows.Forms.Panel pnlPuntajes;
        private System.Windows.Forms.DataGridView dgvPuntajes;
        private System.Windows.Forms.Label lblTotalJugador1;
        private System.Windows.Forms.Label lblTotalJugador2;
        private System.Windows.Forms.Label lblVaGanando;
        private System.Windows.Forms.PictureBox picDecoracion;
        private System.Windows.Forms.SaveFileDialog sfdBackup;
        private System.Windows.Forms.OpenFileDialog ofdRestore;
    }
}
