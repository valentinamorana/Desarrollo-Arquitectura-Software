using System;
using System.Drawing;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormMenu : Form
    {
        private static readonly Font fuenteDescripcion = new Font("Segoe UI", 10F);
        private static readonly Font fuenteDescripcionJugar = new Font("Segoe UI", 11.5F);
        private static readonly Color colorDescripcion = Color.FromArgb(130, 100, 115);
        private static readonly Image dadoJugar = Properties.Resources.generala_logo;

        private USUARIO jugador1;
        private USUARIO jugador2;

        private bool volverAlLogin = false;
        private bool sinLogout = false;

        public FormMenu(USUARIO jugadorLogueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
            lblBienvenida.Text = jugador1.Nombre;
            lblPie.Text = "Elegí una opción para empezar";
        }

        public bool VolverAlLogin
        {
            get { return volverAlLogin; }
        }

        private void Tarjeta_Paint(object sender, PaintEventArgs e)
        {
            Button boton = (Button)sender;
            bool esJugar = boton == btnJugar;

            if (!esJugar)
            {
                using (SolidBrush franja = new SolidBrush(Tema.Rosa))
                {
                    e.Graphics.FillRectangle(franja, 1, 1, boton.Width - 2, 5);
                }
            }

            string descripcion = boton.Tag as string;
            if (string.IsNullOrEmpty(descripcion))
            {
                return;
            }

            int arriba = boton.Padding.Top + boton.Font.Height + 8;

            if (esJugar)
            {
                DibujarDadoJugar(e.Graphics, boton);

                Rectangle areaJugar = new Rectangle(28, arriba, boton.Width - 56, boton.Height - arriba - 8);
                TextRenderer.DrawText(e.Graphics, descripcion, fuenteDescripcionJugar, areaJugar, Tema.RosaClaro,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.Top | TextFormatFlags.WordBreak);
                return;
            }

            Rectangle area = new Rectangle(boton.Padding.Left + 4, arriba,
                boton.Width - boton.Padding.Horizontal - 4, boton.Height - arriba - 8);
            TextRenderer.DrawText(e.Graphics, descripcion, fuenteDescripcion, area, colorDescripcion,
                TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak);
        }

        private void DibujarDadoJugar(Graphics g, Button boton)
        {
            const int tamanio = 44;
            const int separacion = 14;

            int anchoTitulo = TextRenderer.MeasureText(boton.Text, boton.Font).Width;
            int areaTitulo = boton.Width - boton.Padding.Horizontal;
            int izquierdaTitulo = boton.Padding.Left + (areaTitulo - anchoTitulo) / 2;

            int x = izquierdaTitulo - separacion - tamanio;
            int y = boton.Padding.Top + (boton.Font.Height - tamanio) / 2;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(dadoJugar, new Rectangle(x, y, tamanio, tamanio));
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            using (FormPrincipal formPrincipal = new FormPrincipal(jugador1, jugador2))
            {
                formPrincipal.ShowDialog(this);
                jugador2 = formPrincipal.Jugador2;
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            using (FormEstadisticas formEstadisticas = new FormEstadisticas(jugador1, jugador2))
            {
                formEstadisticas.ShowDialog(this);
            }
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            using (FormBitacora formBitacora = new FormBitacora())
            {
                formBitacora.ShowDialog(this);
            }
        }

        private void btnReglas_Click(object sender, EventArgs e)
        {
            using (FormReglas formReglas = new FormReglas())
            {
                formReglas.ShowDialog(this);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            BLL.BACKUPRESTORE backupRestore = new BLL.BACKUPRESTORE();
            sfdBackup.FileName = "GENERALA_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".bak";
            sfdBackup.InitialDirectory = backupRestore.CarpetaBackupPorDefecto();

            if (sfdBackup.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            string error;
            try
            {
                error = backupRestore.HacerBackup(sfdBackup.FileName, jugador1);
            }
            catch (Exception ex)
            {
                Mensaje.Show(
                    "El backup se realizó en:\n" + sfdBackup.FileName +
                    "\n\nPero no se pudo registrar en la bitácora: " + ex.Message +
                    "\n\nSi la base es de una versión anterior, borrá la base GENERALA y ejecutá BD/Script.sql.",
                    "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (error == "")
            {
                Mensaje.Show("Backup realizado correctamente en:\n" + sfdBackup.FileName);
            }
            else
            {
                Mensaje.Show(
                    "Ocurrió un error al hacer el backup:\n" + error +
                    "\n\nSi es un problema de permisos, probá guardarlo en la carpeta de backups de SQL Server.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            BLL.BACKUPRESTORE backupRestore = new BLL.BACKUPRESTORE();
            ofdRestore.InitialDirectory = backupRestore.CarpetaBackupPorDefecto();

            if (ofdRestore.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            DialogResult confirmar = Mensaje.Show(
                "Esto va a reemplazar los datos actuales de la base por los del backup. ¿Confirmás?",
                "Restaurar backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            bool restaurada;
            string error = backupRestore.HacerRestore(ofdRestore.FileName, jugador1, out restaurada);
            if (!restaurada)
            {
                Mensaje.Show(
                    "Ocurrió un error al restaurar el backup:\n" + error,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (error == "")
            {
                Mensaje.Show(
                    "Restore realizado correctamente.\n\nComo los datos de la base cambiaron, " +
                    "hay que volver a iniciar sesión.",
                    "Restaurar backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Mensaje.Show(
                    "Los datos del backup se restauraron, pero después ocurrió un error:\n" + error +
                    "\n\nHay que volver a iniciar sesión. Si la aplicación no puede conectarse, " +
                    "ejecutá en SSMS: ALTER DATABASE GENERALA SET MULTI_USER",
                    "Restaurar backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            volverAlLogin = true;
            sinLogout = true;
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult confirmar = Mensaje.Show(
                "¿Querés cerrar la sesión?",
                "Cerrar sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            volverAlLogin = true;
            this.Close();
        }

        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sinLogout)
            {
                return;
            }

            if (!volverAlLogin && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult confirmar = Mensaje.Show(
                    "¿Estás seguro de que querés salir?",
                    "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirmar != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            CerrarSesionDe(usuarioBLL, jugador1);
            if (jugador2 != null)
            {
                CerrarSesionDe(usuarioBLL, jugador2);
            }
        }

        private void CerrarSesionDe(BLL.USUARIO usuarioBLL, USUARIO usuario)
        {
            try
            {
                usuarioBLL.Logout(usuario);
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudo registrar el cierre de sesión de " + usuario.Nombre + ":\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
