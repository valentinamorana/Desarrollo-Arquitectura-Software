using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    internal class ACCESO
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;

        private static readonly string[] servidoresAlternativos = { @".\SQLEXPRESS", "." };

        private static string servidorResuelto;

        private static readonly string archivoServidor = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Generala", "servidor.txt");

        public static string CadenaConexion(string nombre)
        {
            SqlConnectionStringBuilder cadena = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings[nombre].ConnectionString);
            cadena.DataSource = ResolverServidor();
            return cadena.ConnectionString;
        }

        private static string ResolverServidor()
        {
            if (servidorResuelto != null)
            {
                return servidorResuelto;
            }

            SqlConnectionStringBuilder cadena = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["GENERALA"].ConnectionString);
            string configurado = cadena.DataSource;

            List<string> candidatos = new List<string>();
            string recordado = LeerServidorRecordado();
            if (recordado != null)
            {
                candidatos.Add(recordado);
            }
            if (!candidatos.Contains(configurado))
            {
                candidatos.Add(configurado);
            }
            foreach (string servidor in servidoresAlternativos)
            {
                if (!candidatos.Contains(servidor))
                {
                    candidatos.Add(servidor);
                }
            }

            cadena.ConnectTimeout = 5;
            foreach (string servidor in candidatos)
            {
                cadena.DataSource = servidor;
                try
                {
                    using (SqlConnection prueba = new SqlConnection(cadena.ConnectionString))
                    {
                        prueba.Open();
                    }
                    servidorResuelto = servidor;
                    if (servidor != recordado)
                    {
                        GuardarServidorRecordado(servidor);
                    }
                    return servidorResuelto;
                }
                catch (SqlException)
                {
                }
            }

            return configurado;
        }

        private static string LeerServidorRecordado()
        {
            try
            {
                if (File.Exists(archivoServidor))
                {
                    string servidor = File.ReadAllText(archivoServidor).Trim();
                    if (servidor != "")
                    {
                        return servidor;
                    }
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static void GuardarServidorRecordado(string servidor)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(archivoServidor));
                File.WriteAllText(archivoServidor, servidor);
            }
            catch (Exception)
            {
            }
        }

        public void Abrir()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = CadenaConexion("GENERALA");
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            transaccion = null;
        }

        public void IniciarTx()
        {
            transaccion = conexion.BeginTransaction();
        }

        public void AceptarTx()
        {
            transaccion.Commit();
            transaccion = null;
        }

        public void CancelarTx()
        {
            if (transaccion.Connection != null)
            {
                transaccion.Rollback();
            }
            transaccion = null;
        }

        private SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;
            if (transaccion != null)
            {
                cmd.Transaction = transaccion;
            }
            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }
            return cmd;
        }

        public int Escribir(string sql, List<SqlParameter> parametros = null)
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

        public DataTable Leer(string sql, List<SqlParameter> parametros = null)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = CrearComando(sql, parametros);

            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            return tabla;
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
