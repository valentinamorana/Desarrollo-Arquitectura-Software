using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormMenu : Form
    {
        private USUARIO jugador1;
        private USUARIO jugador2;

        private bool volverAlLogin = false;
        private bool sinLogout = false;

        public FormMenu(USUARIO jugadorLogueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
            lblBienvenida.Text = "Jugador: " + jugador1.Nombre;
            lblPie.Text = "Elegí una opción para empezar";
        }

        public bool VolverAlLogin
        {
            get { return volverAlLogin; }
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            using (FormPrincipal formPrincipal = new FormPrincipal(jugador1, jugador2))
            {
                formPrincipal.ShowDialog(this);
                jugador2 = formPrincipal.Jugador2;
                if (formPrincipal.VolverAlLogin)
                {
                    volverAlLogin = true;
                    sinLogout = true;
                    this.Close();
                }
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
