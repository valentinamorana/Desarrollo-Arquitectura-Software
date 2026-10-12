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

        // Usuario que no puede ingresar en este login (el Jugador 1, cuando se loguea el Jugador 2).
        // Se rechaza antes de llamar a BLL.USUARIO.Login, así no queda un inicio de sesión en la bitácora.
        private string usuarioNoPermitido = "";

        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(string usuarioNoPermitido) : this()
        {
            this.usuarioNoPermitido = usuarioNoPermitido;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                lblMensaje.Text = "Completá usuario y contraseña";
                return;
            }

            // SQL Server compara los nombres sin distinguir mayúsculas ni espacios al final,
            // así que acá se compara igual.
            if (usuarioNoPermitido != "" && string.Equals(txtUsuario.Text.TrimEnd(), usuarioNoPermitido, StringComparison.OrdinalIgnoreCase))
            {
                lblMensaje.Text = "Tiene que ser un usuario distinto al Jugador 1";
                return;
            }

            USUARIO usuario = new USUARIO();
            usuario.Nombre = txtUsuario.Text;
            usuario.Contraseña = txtContraseña.Text;

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            if (usuarioBLL.Login(usuario))
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

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            FormRegistro formRegistro = new FormRegistro();
            if (formRegistro.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = formRegistro.NombreRegistrado;
                lblMensaje.Text = "Usuario creado. Ya podés ingresar.";
            }
        }
    }
}
