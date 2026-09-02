using System;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Sin esto, un error de conexión a SQL Server (por ejemplo el servicio parado)
            // tira la excepción sin atajar hasta el tope y cierra toda la aplicación de golpe.
            Application.ThreadException += Application_ThreadException;

            FormLogin formLogin = new FormLogin();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FormPrincipal(formLogin.UsuarioLogueado));
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
