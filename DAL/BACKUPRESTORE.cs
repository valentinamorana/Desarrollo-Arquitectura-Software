using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class BACKUPRESTORE
    {
        // El backup y el restore se hacen contra la base "master", porque no se puede
        // restaurar una base de datos mientras hay una conexión abierta contra ella misma.
        private const string CONEXION_MASTER = @"Data Source=.\SQLEXPRESS; Initial Catalog=master; Integrated Security=SSPI;";

        // BACKUP/RESTORE no se pueden parametrizar con SqlParameter, así que la ruta se
        // concatena a mano. Se escapan las comillas simples para que un nombre de archivo
        // con un ' no pueda cerrar el string y meter SQL propio en el medio.
        private static string EscaparRuta(string ruta)
        {
            return ruta.Replace("'", "''");
        }

        public static bool HacerBackup(string ruta)
        {
            bool ok = true;
            SqlConnection conexion = new SqlConnection(CONEXION_MASTER);
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "BACKUP DATABASE GENERALA TO DISK = '" + EscaparRuta(ruta) + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ok = false;
            }
            finally
            {
                conexion.Close();
            }
            return ok;
        }

        public static bool HacerRestore(string ruta)
        {
            bool ok = true;
            SqlConnection conexion = new SqlConnection(CONEXION_MASTER);
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "ALTER DATABASE GENERALA SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "RESTORE DATABASE GENERALA FROM DISK = '" + EscaparRuta(ruta) + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                ok = false;
            }
            finally
            {
                // Pase lo que pase con el restore, la base tiene que volver a MULTI_USER:
                // si el RESTORE falla y esto no se ejecuta, la base queda inutilizable
                // para el resto de la aplicación hasta que alguien la destrabe a mano.
                try
                {
                    SqlCommand cmdMultiUser = new SqlCommand("ALTER DATABASE GENERALA SET MULTI_USER", conexion);
                    cmdMultiUser.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    ok = false;
                }
                conexion.Close();
            }
            return ok;
        }
    }
}
