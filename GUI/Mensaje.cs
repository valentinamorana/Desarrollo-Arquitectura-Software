using System;
using System.Windows.Forms;

namespace GUI
{
    // Mismas firmas que MessageBox.Show, pero con el formulario propio de la app.
    public static class Mensaje
    {
        public static DialogResult Show(string texto)
        {
            return Show(texto, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string texto, string titulo)
        {
            return Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string texto, string titulo, MessageBoxButtons botones)
        {
            return Show(texto, titulo, botones, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string texto, string titulo, MessageBoxButtons botones, MessageBoxIcon icono)
        {
            return Show(texto, titulo, botones, icono, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult Show(string texto, string titulo, MessageBoxButtons botones, MessageBoxIcon icono, MessageBoxDefaultButton defecto)
        {
            Form dueno = Form.ActiveForm;
            using (FormMensaje formulario = new FormMensaje(texto, titulo, botones, icono, defecto))
            {
                if (dueno != null)
                {
                    return formulario.ShowDialog(dueno);
                }
                return formulario.ShowDialog();
            }
        }
    }
}
