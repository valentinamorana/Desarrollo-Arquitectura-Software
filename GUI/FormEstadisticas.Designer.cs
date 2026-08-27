namespace GUI
{
    partial class FormEstadisticas
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
            this.lblJugador = new System.Windows.Forms.Label();
            this.cmbJugador = new System.Windows.Forms.ComboBox();
            this.lblGanadas = new System.Windows.Forms.Label();
            this.lblPerdidas = new System.Windows.Forms.Label();
            this.lblEmpatadas = new System.Windows.Forms.Label();
            this.lblPromedio = new System.Windows.Forms.Label();
            this.lblTiempoTotal = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // pnlTitulo
            //
            this.pnlTitulo.BackColor = System.Drawing.Color.HotPink;
            this.pnlTitulo.Controls.Add(this.lblTitulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(0, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(700, 60);
            this.pnlTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(700, 60);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Estadísticas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblJugador
            //
            this.lblJugador.AutoSize = true;
            this.lblJugador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJugador.ForeColor = System.Drawing.Color.DeepPink;
            this.lblJugador.Location = new System.Drawing.Point(20, 78);
            this.lblJugador.Name = "lblJugador";
            this.lblJugador.Size = new System.Drawing.Size(70, 19);
            this.lblJugador.TabIndex = 1;
            this.lblJugador.Text = "Jugador:";
            //
            // cmbJugador
            //
            this.cmbJugador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJugador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbJugador.FormattingEnabled = true;
            this.cmbJugador.Location = new System.Drawing.Point(130, 74);
            this.cmbJugador.Name = "cmbJugador";
            this.cmbJugador.Size = new System.Drawing.Size(250, 28);
            this.cmbJugador.TabIndex = 2;
            this.cmbJugador.SelectedIndexChanged += new System.EventHandler(this.cmbJugador_SelectedIndexChanged);
            //
            // lblGanadas
            //
            this.lblGanadas.AutoSize = true;
            this.lblGanadas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGanadas.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.lblGanadas.Location = new System.Drawing.Point(20, 120);
            this.lblGanadas.Name = "lblGanadas";
            this.lblGanadas.Size = new System.Drawing.Size(90, 19);
            this.lblGanadas.TabIndex = 3;
            this.lblGanadas.Text = "Ganadas: 0";
            //
            // lblPerdidas
            //
            this.lblPerdidas.AutoSize = true;
            this.lblPerdidas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPerdidas.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.lblPerdidas.Location = new System.Drawing.Point(190, 120);
            this.lblPerdidas.Name = "lblPerdidas";
            this.lblPerdidas.Size = new System.Drawing.Size(87, 19);
            this.lblPerdidas.TabIndex = 4;
            this.lblPerdidas.Text = "Perdidas: 0";
            //
            // lblEmpatadas
            //
            this.lblEmpatadas.AutoSize = true;
            this.lblEmpatadas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmpatadas.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.lblEmpatadas.Location = new System.Drawing.Point(360, 120);
            this.lblEmpatadas.Name = "lblEmpatadas";
            this.lblEmpatadas.Size = new System.Drawing.Size(103, 19);
            this.lblEmpatadas.TabIndex = 5;
            this.lblEmpatadas.Text = "Empatadas: 0";
            //
            // lblPromedio
            //
            this.lblPromedio.AutoSize = true;
            this.lblPromedio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPromedio.ForeColor = System.Drawing.Color.DeepPink;
            this.lblPromedio.Location = new System.Drawing.Point(20, 155);
            this.lblPromedio.Name = "lblPromedio";
            this.lblPromedio.Size = new System.Drawing.Size(230, 19);
            this.lblPromedio.TabIndex = 6;
            this.lblPromedio.Text = "Promedio de victorias: 0%";
            //
            // lblTiempoTotal
            //
            this.lblTiempoTotal.AutoSize = true;
            this.lblTiempoTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTiempoTotal.ForeColor = System.Drawing.Color.DeepPink;
            this.lblTiempoTotal.Location = new System.Drawing.Point(360, 155);
            this.lblTiempoTotal.Name = "lblTiempoTotal";
            this.lblTiempoTotal.Size = new System.Drawing.Size(210, 19);
            this.lblTiempoTotal.TabIndex = 7;
            this.lblTiempoTotal.Text = "Tiempo total jugado: 0h 0m";
            //
            // dgvHistorial
            //
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(20, 195);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.Size = new System.Drawing.Size(650, 300);
            this.dgvHistorial.TabIndex = 8;
            //
            // btnCerrar
            //
            this.btnCerrar.BackColor = System.Drawing.Color.DeepPink;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(560, 505);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 36);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // FormEstadisticas
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(700, 560);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblTiempoTotal);
            this.Controls.Add(this.lblPromedio);
            this.Controls.Add(this.lblEmpatadas);
            this.Controls.Add(this.lblPerdidas);
            this.Controls.Add(this.lblGanadas);
            this.Controls.Add(this.cmbJugador);
            this.Controls.Add(this.lblJugador);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormEstadisticas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Estadísticas";
            this.Load += new System.EventHandler(this.FormEstadisticas_Load);
            this.pnlTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblJugador;
        private System.Windows.Forms.ComboBox cmbJugador;
        private System.Windows.Forms.Label lblGanadas;
        private System.Windows.Forms.Label lblPerdidas;
        private System.Windows.Forms.Label lblEmpatadas;
        private System.Windows.Forms.Label lblPromedio;
        private System.Windows.Forms.Label lblTiempoTotal;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Button btnCerrar;
    }
}
