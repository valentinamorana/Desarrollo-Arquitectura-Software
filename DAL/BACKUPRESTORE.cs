using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class BACKUPRESTORE
    {
        private static string ConexionMaster()
        {
            return ACCESO.CadenaConexion("MASTER");
        }

        private static string EscaparRuta(string ruta)
        {
            return ruta.Replace("'", "''");
        }

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
                cmd.CommandText = "BACKUP DATABASE GENERALA TO DISK = '" + EscaparRuta(ruta) + "' WITH INIT";
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

        public static string HacerRestore(string ruta, out bool restaurada)
        {
            string error = "";
            restaurada = false;
            SqlConnection conexion = new SqlConnection(ConexionMaster());
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText =
                    "ALTER DATABASE GENERALA SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                    "RESTORE DATABASE GENERALA FROM DISK = '" + EscaparRuta(ruta) + "' WITH REPLACE";
                cmd.ExecuteNonQuery();
                restaurada = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
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

                SqlConnection.ClearAllPools();
            }
            return error;
        }
    }
}
