namespace GUI
{
    partial class FormAbandonar
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
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblPregunta = new System.Windows.Forms.Label();
            this.btnJugador1 = new System.Windows.Forms.Button();
            this.btnJugador2 = new System.Windows.Forms.Button();
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
            this.lblTitulo.Text = "Abandonar partida";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblMensaje
            //
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblMensaje.Location = new System.Drawing.Point(40, 105);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(440, 50);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblPregunta
            //
            this.lblPregunta.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPregunta.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblPregunta.Location = new System.Drawing.Point(40, 160);
            this.lblPregunta.Name = "lblPregunta";
            this.lblPregunta.Size = new System.Drawing.Size(440, 28);
            this.lblPregunta.TabIndex = 2;
            this.lblPregunta.Text = "¿Qué jugador abandona?";
            this.lblPregunta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnJugador1
            //
            this.btnJugador1.AutoEllipsis = true;
            this.btnJugador1.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnJugador1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJugador1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnJugador1.ForeColor = System.Drawing.Color.White;
            this.btnJugador1.Location = new System.Drawing.Point(40, 200);
            this.btnJugador1.Name = "btnJugador1";
            this.btnJugador1.Size = new System.Drawing.Size(210, 52);
            this.btnJugador1.TabIndex = 3;
            this.btnJugador1.Text = "Jugador 1";
            this.btnJugador1.UseVisualStyleBackColor = false;
            this.btnJugador1.Click += new System.EventHandler(this.btnJugador1_Click);
            //
            // btnJugador2
            //
            this.btnJugador2.AutoEllipsis = true;
            this.btnJugador2.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnJugador2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJugador2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnJugador2.ForeColor = System.Drawing.Color.White;
            this.btnJugador2.Location = new System.Drawing.Point(270, 200);
            this.btnJugador2.Name = "btnJugador2";
            this.btnJugador2.Size = new System.Drawing.Size(210, 52);
            this.btnJugador2.TabIndex = 4;
            this.btnJugador2.Text = "Jugador 2";
            this.btnJugador2.UseVisualStyleBackColor = false;
            this.btnJugador2.Click += new System.EventHandler(this.btnJugador2_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(253, 235, 242);
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCancelar.Location = new System.Drawing.Point(175, 272);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 45);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // FormAbandonar
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(520, 340);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnJugador2);
            this.Controls.Add(this.btnJugador1);
            this.Controls.Add(this.lblPregunta);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::GUI.Properties.Resources.generala;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbandonar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generala - Abandonar partida";
            this.pnlTitulo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Label lblPregunta;
        private System.Windows.Forms.Button btnJugador1;
        private System.Windows.Forms.Button btnJugador2;
        private System.Windows.Forms.Button btnCancelar;
    }
}
