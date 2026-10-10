using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class BACKUPRESTORE
    {
        // El backup y el restore se hacen contra la base "master", porque no se puede
        // restaurar una base de datos mientras hay una conexión abierta contra ella misma.
        // Se usa la misma cadena del App.config, cambiando solo la base.
        private static string ConexionMaster()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ACCESO.CadenaConexion());
            builder.InitialCatalog = "master";
            return builder.ConnectionString;
        }

        // BACKUP/RESTORE no se pueden parametrizar con SqlParameter, así que la ruta se
        // concatena a mano. Se escapan las comillas simples para que un nombre de archivo
        // con un ' no pueda cerrar el string y meter SQL propio en el medio.
        private static string EscaparRuta(string ruta)
        {
            return ruta.Replace("'", "''");
        }

        // El BACKUP/RESTORE lo ejecuta el servicio de SQL Server, no el usuario de Windows,
        // así que en carpetas personales (Escritorio, Documentos) suele no tener permiso.
        // La carpeta de backups de la instancia siempre es accesible para el servicio.
        // Devuelve "" si no se pudo averiguar (versiones viejas de SQL Server).
        public static string CarpetaBackupPorDefecto()
        {
            string carpeta = "";
            SqlConnection conexion = new SqlConnection(ConexionMaster());
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT SERVERPROPERTY('InstanceDefaultBackupPath')", conexion);
                object resultado = cmd.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    carpeta = resultado.ToString();
                }
            }
            catch (Exception)
            {
                carpeta = "";
            }
            finally
            {
                conexion.Close();
            }
            return carpeta;
        }

        // Devuelve "" si salió bien, o el mensaje de error de SQL Server si falló.
        public static string HacerBackup(string ruta)
        {
            string error = "";
            SqlConnection conexion = new SqlConnection(ConexionMaster());
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "BACKUP DATABASE GENERALA TO DISK = '" + EscaparRuta(ruta) + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                conexion.Close();
            }
            return error;
        }

        // Devuelve "" si salió bien, o el mensaje de error de SQL Server si falló.
        public static string HacerRestore(string ruta)
        {
            string error = "";
            SqlConnection conexion = new SqlConnection(ConexionMaster());
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "ALTER DATABASE GENERALA SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "RESTORE DATABASE GENERALA FROM DISK = '" + EscaparRuta(ruta) + "' WITH REPLACE";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                // Pase lo que pase con el restore, la base tiene que volver a MULTI_USER:
                // si el RESTORE falla y esto no se ejecuta, la base queda inutilizable
                // para el resto de la aplicación hasta que alguien la destrabe a mano.
                try
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        SqlCommand cmdMultiUser = new SqlCommand("ALTER DATABASE GENERALA SET MULTI_USER", conexion);
                        cmdMultiUser.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (error == "")
                    {
                        error = ex.Message;
                    }
                }
                conexion.Close();
            }
            return error;
        }
    }
}
