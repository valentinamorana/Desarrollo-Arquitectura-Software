using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ACCESO
    {
        private SqlConnection conexion;

        // La cadena de conexión se lee del App.config de la GUI (connectionStrings "GENERALA"),
        // así se puede apuntar a otra instancia de SQL Server sin recompilar.
        public static string CadenaConexion()
        {
            return ConfigurationManager.ConnectionStrings["GENERALA"].ConnectionString;
        }

        public void Abrir()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = CadenaConexion();
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        private SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;
            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }
            return cmd;
        }

        public int Escribir(string sql, List<SqlParameter> parametros)
        {
            SqlCommand cmd = CrearComando(sql, parametros);

            int filas = 0;
            try
            {
                filas = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                filas = -1;
            }
            return filas;
        }

        public int LeerEscalar(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = CrearComando(sql, parametros);

            int res = 0;
            try
            {
                res = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                res = -1;
            }
            return res;
        }

        public SqlDataReader Leer(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = CrearComando(sql, parametros);

            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.String;
            return parametro;
        }

        public SqlParameter CrearParametro(string nombre, int valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.Int32;
            return parametro;
        }

        public SqlParameter CrearParametroNulo(string nombre)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = DBNull.Value;
            parametro.DbType = DbType.Int32;
            return parametro;
        }
    }
}
