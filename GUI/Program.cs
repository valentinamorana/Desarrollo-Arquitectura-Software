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

            // Se repite solo si FormPrincipal se cerró por un restore de la base,
            // que obliga a volver a loguearse (ver FormPrincipal.btnRestore_Click).
            bool volverAlLogin = true;
            while (volverAlLogin)
            {
                // Sin esto, un error de conexión a SQL Server (por ejemplo el servicio parado)
                // tira la excepción sin atajar hasta el tope y cierra toda la aplicación de golpe.
                // Se suscribe en cada vuelta porque WinForms descarta el handler cuando termina
                // un Application.Run; se desuscribe antes para no duplicarlo si no lo descartó.
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

                FormPrincipal formPrincipal = new FormPrincipal(usuarioLogueado);
                Application.Run(formPrincipal);
                volverAlLogin = formPrincipal.VolverAlLogin;
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(
                "Ocurrió un error inesperado: " + e.Exception.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
