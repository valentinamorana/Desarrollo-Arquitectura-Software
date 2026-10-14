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

            // Largo del nombre y la contraseña, nombre sin espacios (reglas de la BLL).
            string error = usuarioBLL.ValidarRegistro(usuario);
            if (error != "")
            {
                lblMensaje.Text = error;
                return;
            }

            bool creado;
            try
            {
                creado = usuarioBLL.Insertar(usuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!creado)
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

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
