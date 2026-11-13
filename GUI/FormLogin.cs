using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormLogin : Form
    {
        private USUARIO usuarioLogueado;

        public USUARIO UsuarioLogueado
        {
            get { return usuarioLogueado; }
            private set { usuarioLogueado = value; }
        }

        private string usuarioNoPermitido = "";

        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(string usuarioNoPermitido) : this()
        {
            this.usuarioNoPermitido = usuarioNoPermitido;
            this.Text = "Generala - Ingresar Jugador 2";
            lblTitulo.Text = "Jugador 2";
            lblSubtitulo.Text = "Ingresá con un usuario distinto a " + usuarioNoPermitido + ".";
        }

        private void txtCampo_Enter(object sender, EventArgs e)
        {
            LineaDe(sender).BackColor = Tema.Rosa;
        }

        private void txtCampo_Leave(object sender, EventArgs e)
        {
            LineaDe(sender).BackColor = System.Drawing.Color.FromArgb(230, 200, 212);
        }

        private Panel LineaDe(object campo)
        {
            if (campo == txtUsuario)
            {
                return pnlLineaUsuario;
            }
            return pnlLineaContraseña;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            lblMensaje.ForeColor = System.Drawing.Color.Crimson;

            if (txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                lblMensaje.Text = "Completá usuario y contraseña";
                return;
            }

            if (usuarioNoPermitido != "" && txtUsuario.Text.TrimEnd().ToLower() == usuarioNoPermitido.ToLower())
            {
                lblMensaje.Text = "Tiene que ser un usuario distinto al Jugador 1";
                return;
            }

            USUARIO usuario = new USUARIO();
            usuario.Nombre = txtUsuario.Text;
            usuario.Contraseña = txtContraseña.Text;

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            bool ok;
            try
            {
                ok = usuarioBLL.Login(usuario);
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudo ingresar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ok)
            {
                UsuarioLogueado = usuario;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña incorrectos";
            }
        }

        private void lnkRegistrarme_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FormRegistro formRegistro = new FormRegistro())
            {
                if (formRegistro.ShowDialog(this) == DialogResult.OK)
                {
                    txtUsuario.Text = formRegistro.NombreRegistrado;
                    lblMensaje.ForeColor = System.Drawing.Color.SeaGreen;
                    lblMensaje.Text = "Usuario creado. Ya podés ingresar.";
                    txtContraseña.Focus();
                }
            }
        }
    }
}
