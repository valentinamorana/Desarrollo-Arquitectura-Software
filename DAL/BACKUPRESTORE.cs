using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class BACKUPRESTORE : IBACKUPRESTORE
    {
        private string ConexionMaster()
        {
            return ACCESO.CadenaConexion("MASTER");
        }

        private SqlParameter ParametroRuta(string ruta)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "@ruta";
            parametro.Value = ruta;
            parametro.DbType = DbType.String;
            return parametro;
        }

        public string CarpetaBackupPorDefecto()
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

            if (carpeta != "" && !Directory.Exists(carpeta))
            {
                carpeta = "";
            }
            return carpeta;
        }

        public string HacerBackup(string ruta)
        {
            string error = "";
            SqlConnection conexion = new SqlConnection(ConexionMaster());
            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "BACKUP DATABASE GENERALA TO DISK = @ruta WITH INIT";
                cmd.Parameters.Add(ParametroRuta(ruta));
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

        public string HacerRestore(string ruta, out bool restaurada)
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
                    "RESTORE DATABASE GENERALA FROM DISK = @ruta WITH REPLACE";
                cmd.Parameters.Add(ParametroRuta(ruta));
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
