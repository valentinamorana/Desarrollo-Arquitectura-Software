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

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (txtUsuario.Text == "" || txtContraseña.Text == "")
            {
                lblMensaje.Text = "Completá usuario y contraseña";
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
