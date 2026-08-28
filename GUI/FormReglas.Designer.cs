namespace GUI
{
    partial class FormReglas
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
            this.txtReglas = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
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
            this.pnlTitulo.Size = new System.Drawing.Size(560, 70);
            this.pnlTitulo.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(560, 70);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reglas del Juego";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // txtReglas
            //
            this.txtReglas.BackColor = System.Drawing.Color.White;
            this.txtReglas.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtReglas.ForeColor = System.Drawing.Color.FromArgb(60, 30, 45);
            this.txtReglas.Location = new System.Drawing.Point(20, 90);
            this.txtReglas.Multiline = true;
            this.txtReglas.Name = "txtReglas";
            this.txtReglas.ReadOnly = true;
            this.txtReglas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReglas.Size = new System.Drawing.Size(520, 400);
            this.txtReglas.TabIndex = 1;
            this.txtReglas.Text = "OBJETIVO\r\nCompletar las 10 categorías de la planilla con el mayor puntaje posible. " +
    "Gana quien suma más puntos al final; si empatan, la partida termina en empate." +
    "\r\n\r\nTURNO\r\nCada jugador tiene hasta 3 tiradas por turno. Después de cada tirad" +
    "a podés marcar \"Guardar\" en los dados que querés conservar antes de volver a t" +
    "irar.\r\n\r\nCATEGORÍAS\r\n• Unos a Seises: suma los dados que coinciden con ese núm" +
    "ero.\r\n• Escalera (20 pts): 1-2-3-4-5 o 2-3-4-5-6.\r\n• Full (30 pts): tres dados" +
    " iguales + dos iguales entre sí.\r\n• Poker (40 pts): cuatro o más dados iguales" +
    ".\r\n• Generala (50 pts): los cinco dados iguales.\r\n• Generala Doble (100 pts): " +
    "otra Generala, después de haber anotado la primera.\r\n\r\nSERVIDO\r\nSi lográs Esca" +
    "lera, Full, Poker o Generala en tu PRIMERA tirada (sin repetir ningún dado), e" +
    "l puntaje de esa categoría se duplica.\r\n\r\nCATEGORÍA TACHADA\r\nSi tus dados no a" +
    "lcanzan para ninguna categoría libre, tenés que elegir una igual: queda anota" +
    "da en 0 y no se puede volver a usar.\r\n\r\nFIN DE LA PARTIDA\r\nTermina cuando ambo" +
    "s jugadores completaron las 10 (u 11, con Generala Doble) categorías. También" +
    " se puede abandonar antes con el botón \"Abandonar Partida\", y se registra con" +
    " el puntaje parcial de ese momento.";
            //
            // btnCerrar
            //
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(420, 500);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 40);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            //
            // FormReglas
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(253, 245, 247);
            this.ClientSize = new System.Drawing.Size(560, 560);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtReglas);
            this.Controls.Add(this.pnlTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormReglas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generala - Reglas";
            this.pnlTitulo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtReglas;
        private System.Windows.Forms.Button btnCerrar;
    }
}
