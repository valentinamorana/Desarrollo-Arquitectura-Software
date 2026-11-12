using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool volverAlLogin = true;
            while (volverAlLogin)
            {
                Application.ThreadException -= Application_ThreadException;
                Application.ThreadException += Application_ThreadException;

                USUARIO usuarioLogueado;
                using (FormLogin formLogin = new FormLogin())
                {
                    if (formLogin.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    usuarioLogueado = formLogin.UsuarioLogueado;
                }

                FormMenu menu = new FormMenu(usuarioLogueado);
                Application.Run(menu);
                volverAlLogin = menu.VolverAlLogin;
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Mensaje.Show(
                "Ocurrió un error inesperado: " + e.Exception.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
