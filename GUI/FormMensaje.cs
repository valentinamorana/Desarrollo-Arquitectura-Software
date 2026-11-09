using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    // Cuadro de diálogo con la paleta de la app. Reemplaza a MessageBox para que los mensajes
    // se vean igual en cualquier PC (MessageBox toma el color de énfasis de Windows).
    public partial class FormMensaje : Form
    {
        public FormMensaje(string texto, string titulo, MessageBoxButtons botones, MessageBoxIcon icono, MessageBoxDefaultButton defecto)
        {
            InitializeComponent();

            this.Text = string.IsNullOrEmpty(titulo) ? "Generala" : titulo;
            lblTitulo.Text = this.Text;
            lblMensaje.Text = texto;

            Icon icono2 = IconoPara(icono);
            picIcono.Image = icono2 != null ? icono2.ToBitmap() : null;
            picIcono.Visible = icono2 != null;

            if (botones == MessageBoxButtons.YesNo)
            {
                btnPrimero.Text = "Sí";
                btnPrimero.DialogResult = DialogResult.Yes;
                btnSegundo.Text = "No";
                btnSegundo.DialogResult = DialogResult.No;
                btnSegundo.Visible = true;
                btnPrimero.Location = new Point(256, btnPrimero.Location.Y);
            }
            else
            {
                btnPrimero.Text = "Aceptar";
                btnPrimero.DialogResult = DialogResult.OK;
                btnSegundo.Visible = false;
            }

            // Igual que MessageBox: la tecla Enter activa el botón por defecto y Esc cancela.
            bool segundoPorDefecto = defecto == MessageBoxDefaultButton.Button2 && btnSegundo.Visible;
            this.AcceptButton = segundoPorDefecto ? (IButtonControl)btnSegundo : btnPrimero;
            this.CancelButton = btnSegundo.Visible ? (IButtonControl)btnSegundo : btnPrimero;

            // El alto se ajusta al largo del texto (el label se parte en varias líneas).
            int cuerpo = Math.Max(lblMensaje.PreferredHeight, 36);
            this.ClientSize = new Size(480, pnlTitulo.Height + 20 + cuerpo + 20 + pnlBotones.Height);
        }

        private static Icon IconoPara(MessageBoxIcon icono)
        {
            switch (icono)
            {
                case MessageBoxIcon.Error: return SystemIcons.Error;
                case MessageBoxIcon.Warning: return SystemIcons.Warning;
                case MessageBoxIcon.Question: return SystemIcons.Question;
                case MessageBoxIcon.Information: return SystemIcons.Information;
                default: return null;
            }
        }
    }
}
