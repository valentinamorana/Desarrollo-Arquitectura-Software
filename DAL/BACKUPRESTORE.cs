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
                cmd.CommandText = "BACKUP DATABASE GENERALA TO DISK = '" + ruta + "'";
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

                cmd.CommandText = "RESTORE DATABASE GENERALA FROM DISK = '" + ruta + "'";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "ALTER DATABASE GENERALA SET MULTI_USER";
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
    }
}
