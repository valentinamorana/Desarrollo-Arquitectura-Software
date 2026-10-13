namespace GUI
{
    partial class ucDado
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

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblValor = new System.Windows.Forms.Label();
            this.chkGuardar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            //
            // lblValor
            //
            this.lblValor.BackColor = System.Drawing.Color.FromArgb(248, 214, 227);
            this.lblValor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblValor.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblValor.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            this.lblValor.Location = new System.Drawing.Point(0, 0);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(80, 80);
            this.lblValor.TabIndex = 0;
            this.lblValor.Text = "-";
            this.lblValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // chkGuardar
            //
            this.chkGuardar.AutoSize = true;
            this.chkGuardar.Enabled = false;
            this.chkGuardar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGuardar.Location = new System.Drawing.Point(4, 85);
            this.chkGuardar.Name = "chkGuardar";
            this.chkGuardar.Size = new System.Drawing.Size(72, 24);
            this.chkGuardar.TabIndex = 1;
            this.chkGuardar.Text = "Guardar";
            this.chkGuardar.UseVisualStyleBackColor = true;
            //
            // ucDado
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chkGuardar);
            this.Controls.Add(this.lblValor);
            this.Name = "ucDado";
            this.Size = new System.Drawing.Size(80, 112);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.CheckBox chkGuardar;
    }
}
