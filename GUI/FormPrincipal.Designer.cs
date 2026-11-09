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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnReglas = new System.Windows.Forms.Button();
            this.btnVolverMenu = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlJugadores = new System.Windows.Forms.Panel();
            this.btnAbandonar = new System.Windows.Forms.Button();
            this.btnComenzarPartida = new System.Windows.Forms.Button();
            this.btnLoginJugador2 = new System.Windows.Forms.Button();
            this.lblJugador2 = new System.Windows.Forms.Label();
            this.lblJugador1 = new System.Windows.Forms.Label();
            this.pnlJuego = new System.Windows.Forms.Panel();
            this.btnAnotar = new System.Windows.Forms.Button();
            this.btnTachar = new System.Windows.Forms.Button();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.btnTirarDados = new System.Windows.Forms.Button();
            this.ucDado5 = new GUI.ucDado();
            this.ucDado4 = new GUI.ucDado();
            this.ucDado3 = new GUI.ucDado();
            this.ucDado2 = new GUI.ucDado();
            this.ucDado1 = new GUI.ucDado();
            this.lblTirosRestantes = new System.Windows.Forms.Label();
            this.lblTurnoNombre = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblProgreso = new System.Windows.Forms.Label();
            this.pnlPuntajes = new System.Windows.Forms.Panel();
            this.picDecoracion = new System.Windows.Forms.PictureBox();
            this.lblVaGanando = new System.Windows.Forms.Label();
            this.lblTotalJugador2 = new System.Windows.Forms.Label();
            this.lblTotalJugador1 = new System.Windows.Forms.Label();
            this.dgvPuntajes = new System.Windows.Forms.DataGridView();
            this.sfdBackup = new System.Windows.Forms.SaveFileDialog();
            this.ofdRestore = new System.Windows.Forms.OpenFileDialog();
            this.pnlEncabezado.SuspendLayout();
            this.pnlJugadores.SuspendLayout();
            this.pnlJuego.SuspendLayout();
            this.pnlPuntajes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecoracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntajes)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.pnlEncabezado.Controls.Add(this.btnRestore);
            this.pnlEncabezado.Controls.Add(this.btnBackup);
            this.pnlEncabezado.Controls.Add(this.btnEstadisticas);
            this.pnlEncabezado.Controls.Add(this.btnReglas);
            this.pnlEncabezado.Controls.Add(this.btnVolverMenu);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(1000, 70);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.FlatAppearance.BorderSize = 0;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.btnBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.btnEstadisticas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.btnReglas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
            this.btnReglas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReglas.FlatAppearance.BorderSize = 0;
            this.btnReglas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this.btnReglas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnReglas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReglas.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReglas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnReglas.Location = new System.Drawing.Point(168, 20);
            this.btnReglas.Name = "btnReglas";
            this.btnReglas.Size = new System.Drawing.Size(106, 30);
            this.btnReglas.TabIndex = 5;
            this.btnReglas.Text = "¿Cómo jugar?";
            this.btnReglas.UseVisualStyleBackColor = false;
            this.btnReglas.Click += new System.EventHandler(this.btnReglas_Click);
            // 
            // 
            // btnVolverMenu
            // 
            this.btnVolverMenu.BackColor = Tema.Rosa;
            this.btnVolverMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolverMenu.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVolverMenu.FlatAppearance.BorderSize = 2;
            this.btnVolverMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolverMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVolverMenu.ForeColor = System.Drawing.Color.White;
            this.btnVolverMenu.Location = new System.Drawing.Point(830, 15);
            this.btnVolverMenu.Name = "btnVolverMenu";
            this.btnVolverMenu.Size = new System.Drawing.Size(155, 40);
            this.btnVolverMenu.TabIndex = 4;
            this.btnVolverMenu.Text = "Volver al menú";
            this.btnVolverMenu.UseVisualStyleBackColor = false;
            this.btnVolverMenu.Click += new System.EventHandler(this.btnVolverMenu_Click);
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 13);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(131, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Generala";
            this.lblTitulo.Click += new System.EventHandler(this.lblTitulo_Click);
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
            this.btnComenzarPartida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.btnComenzarPartida.EnabledChanged += new System.EventHandler(this.btnPrincipal_EnabledChanged);
            this.btnComenzarPartida.Click += new System.EventHandler(this.btnComenzarPartida_Click);
            // 
            // btnLoginJugador2
            // 
            this.btnLoginJugador2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(242)))));
            this.btnLoginJugador2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnLoginJugador2.FlatAppearance.BorderSize = 2;
            this.btnLoginJugador2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoginJugador2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoginJugador2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.lblJugador2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
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
            this.lblJugador1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.lblJugador1.Location = new System.Drawing.Point(15, 18);
            this.lblJugador1.Name = "lblJugador1";
            this.lblJugador1.Size = new System.Drawing.Size(93, 20);
            this.lblJugador1.TabIndex = 0;
            this.lblJugador1.Text = "Jugador 1: -";
            this.lblJugador1.UseMnemonic = false;
            // 
            // pnlJuego
            // 
            this.pnlJuego.BackColor = System.Drawing.Color.White;
            this.pnlJuego.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlJuego.Controls.Add(this.btnAnotar);
            this.pnlJuego.Controls.Add(this.btnTachar);
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
            this.pnlJuego.Controls.Add(this.lblProgreso);
            this.pnlJuego.Location = new System.Drawing.Point(20, 160);
            this.pnlJuego.Name = "pnlJuego";
            this.pnlJuego.Size = new System.Drawing.Size(960, 185);
            this.pnlJuego.TabIndex = 2;
            // 
            // btnAnotar
            // 
            this.btnAnotar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(235)))), ((int)(((byte)(242)))));
            this.btnAnotar.Enabled = false;
            this.btnAnotar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnAnotar.FlatAppearance.BorderSize = 2;
            this.btnAnotar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnotar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnotar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnAnotar.Location = new System.Drawing.Point(705, 128);
            this.btnAnotar.Name = "btnAnotar";
            this.btnAnotar.Size = new System.Drawing.Size(119, 32);
            this.btnAnotar.TabIndex = 14;
            this.btnAnotar.Text = "Anotar";
            this.btnAnotar.UseVisualStyleBackColor = false;
            this.btnAnotar.Click += new System.EventHandler(this.btnAnotar_Click);
            // 
            // btnTachar
            // 
            this.btnTachar.BackColor = System.Drawing.Color.White;
            this.btnTachar.Enabled = false;
            this.btnTachar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnTachar.FlatAppearance.BorderSize = 2;
            this.btnTachar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTachar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTachar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(10)))), ((int)(((byte)(45)))));
            this.btnTachar.Location = new System.Drawing.Point(830, 128);
            this.btnTachar.Name = "btnTachar";
            this.btnTachar.Size = new System.Drawing.Size(115, 32);
            this.btnTachar.TabIndex = 15;
            this.btnTachar.Text = "Tachar";
            this.btnTachar.UseVisualStyleBackColor = false;
            this.btnTachar.Click += new System.EventHandler(this.btnTachar_Click);
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.ItemHeight = 24;
            this.cmbCategoria.Location = new System.Drawing.Point(475, 128);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(220, 30);
            this.cmbCategoria.TabIndex = 13;
            this.cmbCategoria.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbCategoria_DrawItem);
            // 
            // btnTirarDados
            // 
            this.btnTirarDados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.btnTirarDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTirarDados.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTirarDados.ForeColor = System.Drawing.Color.White;
            this.btnTirarDados.Location = new System.Drawing.Point(475, 60);
            this.btnTirarDados.Name = "btnTirarDados";
            this.btnTirarDados.Size = new System.Drawing.Size(160, 50);
            this.btnTirarDados.TabIndex = 12;
            this.btnTirarDados.Text = "Tirar Dados";
            this.btnTirarDados.UseVisualStyleBackColor = false;
            this.btnTirarDados.EnabledChanged += new System.EventHandler(this.btnPrincipal_EnabledChanged);
            this.btnTirarDados.Click += new System.EventHandler(this.btnTirarDados_Click);
            // 
            // ucDado5
            // 
            this.ucDado5.BackColor = System.Drawing.Color.White;
            this.ucDado5.Guardado = false;
            this.ucDado5.Location = new System.Drawing.Point(375, 55);
            this.ucDado5.Name = "ucDado5";
            this.ucDado5.PermitirGuardar = false;
            this.ucDado5.Size = new System.Drawing.Size(80, 112);
            this.ucDado5.TabIndex = 6;
            this.ucDado5.Valor = 0;
            // 
            // ucDado4
            // 
            this.ucDado4.BackColor = System.Drawing.Color.White;
            this.ucDado4.Guardado = false;
            this.ucDado4.Location = new System.Drawing.Point(285, 55);
            this.ucDado4.Name = "ucDado4";
            this.ucDado4.PermitirGuardar = false;
            this.ucDado4.Size = new System.Drawing.Size(80, 112);
            this.ucDado4.TabIndex = 5;
            this.ucDado4.Valor = 0;
            // 
            // ucDado3
            // 
            this.ucDado3.BackColor = System.Drawing.Color.White;
            this.ucDado3.Guardado = false;
            this.ucDado3.Location = new System.Drawing.Point(195, 55);
            this.ucDado3.Name = "ucDado3";
            this.ucDado3.PermitirGuardar = false;
            this.ucDado3.Size = new System.Drawing.Size(80, 112);
            this.ucDado3.TabIndex = 4;
            this.ucDado3.Valor = 0;
            // 
            // ucDado2
            // 
            this.ucDado2.BackColor = System.Drawing.Color.White;
            this.ucDado2.Guardado = false;
            this.ucDado2.Location = new System.Drawing.Point(105, 55);
            this.ucDado2.Name = "ucDado2";
            this.ucDado2.PermitirGuardar = false;
            this.ucDado2.Size = new System.Drawing.Size(80, 112);
            this.ucDado2.TabIndex = 3;
            this.ucDado2.Valor = 0;
            // 
            // ucDado1
            // 
            this.ucDado1.BackColor = System.Drawing.Color.White;
            this.ucDado1.Guardado = false;
            this.ucDado1.Location = new System.Drawing.Point(15, 55);
            this.ucDado1.Name = "ucDado1";
            this.ucDado1.PermitirGuardar = false;
            this.ucDado1.Size = new System.Drawing.Size(80, 112);
            this.ucDado1.TabIndex = 2;
            this.ucDado1.Valor = 0;
            // 
            // lblTirosRestantes
            // 
            this.lblTirosRestantes.AutoSize = true;
            this.lblTirosRestantes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTirosRestantes.ForeColor = System.Drawing.Color.DimGray;
            this.lblTirosRestantes.Location = new System.Drawing.Point(330, 15);
            this.lblTirosRestantes.Name = "lblTirosRestantes";
            this.lblTirosRestantes.Size = new System.Drawing.Size(117, 20);
            this.lblTirosRestantes.TabIndex = 1;
            this.lblTirosRestantes.Text = "Tiros restantes: -";
            // 
            // lblTurnoNombre
            // 
            this.lblTurnoNombre.AutoSize = true;
            this.lblTurnoNombre.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTurnoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.lblTurnoNombre.Location = new System.Drawing.Point(110, 12);
            this.lblTurnoNombre.Name = "lblTurnoNombre";
            this.lblTurnoNombre.Size = new System.Drawing.Size(19, 25);
            this.lblTurnoNombre.TabIndex = 11;
            this.lblTurnoNombre.Text = "-";
            this.lblTurnoNombre.UseMnemonic = false;
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTurno.ForeColor = System.Drawing.Color.Black;
            this.lblTurno.Location = new System.Drawing.Point(15, 12);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(93, 25);
            this.lblTurno.TabIndex = 0;
            this.lblTurno.Text = "Turno de:";
            this.lblTurno.UseMnemonic = false;
            // 
            // lblProgreso
            // 
            this.lblProgreso.AutoSize = true;
            this.lblProgreso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProgreso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(10)))), ((int)(((byte)(45)))));
            this.lblProgreso.Location = new System.Drawing.Point(826, 12);
            this.lblProgreso.Name = "lblProgreso";
            this.lblProgreso.Size = new System.Drawing.Size(100, 19);
            this.lblProgreso.TabIndex = 30;
            this.lblProgreso.Text = "Turno 1 de 11";
            this.lblProgreso.Click += new System.EventHandler(this.lblProgreso_Click);
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
            this.lblVaGanando.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.lblVaGanando.Location = new System.Drawing.Point(600, 100);
            this.lblVaGanando.Name = "lblVaGanando";
            this.lblVaGanando.Size = new System.Drawing.Size(0, 23);
            this.lblVaGanando.TabIndex = 4;
            this.lblVaGanando.UseMnemonic = false;
            // 
            // lblTotalJugador2
            // 
            this.lblTotalJugador2.AutoSize = true;
            this.lblTotalJugador2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.lblTotalJugador2.Location = new System.Drawing.Point(600, 60);
            this.lblTotalJugador2.Name = "lblTotalJugador2";
            this.lblTotalJugador2.Size = new System.Drawing.Size(144, 21);
            this.lblTotalJugador2.TabIndex = 2;
            this.lblTotalJugador2.Text = "Total Jugador 2: 0";
            this.lblTotalJugador2.UseMnemonic = false;
            // 
            // lblTotalJugador1
            // 
            this.lblTotalJugador1.AutoSize = true;
            this.lblTotalJugador1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalJugador1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            this.lblTotalJugador1.Location = new System.Drawing.Point(600, 20);
            this.lblTotalJugador1.Name = "lblTotalJugador1";
            this.lblTotalJugador1.Size = new System.Drawing.Size(144, 21);
            this.lblTotalJugador1.TabIndex = 1;
            this.lblTotalJugador1.Text = "Total Jugador 1: 0";
            this.lblTotalJugador1.UseMnemonic = false;
            // 
            // dgvPuntajes
            // 
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.dgvPuntajes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvPuntajes.BackgroundColor = System.Drawing.Color.White;
            this.dgvPuntajes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(20)))), ((int)(((byte)(87)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPuntajes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvPuntajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(10)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPuntajes.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvPuntajes.EnableHeadersVisualStyles = false;
            this.dgvPuntajes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(214)))), ((int)(((byte)(227)))));
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this.pnlPuntajes);
            this.Controls.Add(this.pnlJuego);
            this.Controls.Add(this.pnlJugadores);
            this.Controls.Add(this.pnlEncabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::GUI.Properties.Resources.generala;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormPrincipal_KeyDown);
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlJugadores.ResumeLayout(false);
            this.pnlJugadores.PerformLayout();
            this.pnlJuego.ResumeLayout(false);
            this.pnlJuego.PerformLayout();
            this.pnlPuntajes.ResumeLayout(false);
            this.pnlPuntajes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecoracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPuntajes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Button btnVolverMenu;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnReglas;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Panel pnlJugadores;
        private System.Windows.Forms.Label lblJugador1;
        private System.Windows.Forms.Label lblJugador2;
        private System.Windows.Forms.Button btnLoginJugador2;
        private System.Windows.Forms.Button btnComenzarPartida;
        private System.Windows.Forms.Button btnAbandonar;
        private System.Windows.Forms.Panel pnlJuego;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label lblTurnoNombre;
        private System.Windows.Forms.Label lblProgreso;
        private System.Windows.Forms.Label lblTirosRestantes;
        private GUI.ucDado ucDado1;
        private GUI.ucDado ucDado2;
        private GUI.ucDado ucDado3;
        private GUI.ucDado ucDado4;
        private GUI.ucDado ucDado5;
        private System.Windows.Forms.Button btnTirarDados;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnAnotar;
        private System.Windows.Forms.Button btnTachar;
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
