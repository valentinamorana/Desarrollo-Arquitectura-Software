using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormRegistro : Form
    {
        private string nombreRegistrado;

        public string NombreRegistrado
        {
            get { return nombreRegistrado; }
            private set { nombreRegistrado = value; }
        }

        public FormRegistro()
        {
            InitializeComponent();
            this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "generala.ico"));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";

            if (txtNombre.Text == "" || txtContraseña.Text == "" || txtConfirmar.Text == "")
            {
                lblMensaje.Text = "Completá todos los campos";
                return;
            }

            if (txtContraseña.Text != txtConfirmar.Text)
            {
                lblMensaje.Text = "Las contraseñas no coinciden";
                return;
            }

            USUARIO usuario = new USUARIO();
            usuario.Nombre = txtNombre.Text;
            usuario.Contraseña = txtContraseña.Text;

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            if (!usuarioBLL.Insertar(usuario))
            {
                lblMensaje.Text = "Ese nombre de usuario ya existe";
                return;
            }

            NombreRegistrado = usuario.Nombre;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
