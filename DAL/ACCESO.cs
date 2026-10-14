using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // internal: solo los mappers de la DAL hablan con la base. Ni la BLL ni la GUI
    // pueden crear un ACCESO y ejecutar SQL por su cuenta.
    internal class ACCESO
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;

        // Instancias de SQL Server que se prueban si la del App.config no responde:
        // con nombre (SQL Express) o la instancia por defecto (como en las compus de la facu).
        private static readonly string[] servidoresAlternativos = { @".\SQLEXPRESS", "." };

        // Servidor que respondió la primera vez; se reutiliza para no volver a probar.
        private static string servidorResuelto;

        // La cadena de conexión se lee del App.config de la GUI (connectionStrings "GENERALA"),
        // así se puede apuntar a otra instancia de SQL Server sin recompilar.
        // El Data Source se reemplaza por el servidor que efectivamente respondió.
        public static string CadenaConexion(string nombre)
        {
            SqlConnectionStringBuilder cadena = new SqlConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings[nombre].ConnectionString);
            cadena.DataSource = ResolverServidor();
            return cadena.ConnectionString;
        }

        // Prueba primero el Data Source del App.config y después las alternativas,
        // y se queda con el primero donde se pueda abrir la base GENERALA.
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
            candidatos.Add(configurado);
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
                    return servidorResuelto;
                }
                catch (SqlException)
                {
                    // No responde o no tiene la base: se prueba el siguiente.
                }
            }

            // Ninguno respondió: se usa el configurado, así el error que ve el usuario es el original.
            return configurado;
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
            GC.Collect();
        }

        // -------------------- Transacciones --------------------
        // La transacción la crea la conexión (BeginTransaction). Todos los comandos que se
        // ejecuten mientras esté abierta quedan dentro de ella, hasta el Commit o el Rollback.

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
            // Si SQL Server ya deshizo la transacción por su cuenta (por un error grave),
            // la transacción queda sin conexión y no hay nada para deshacer.
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

        // INSERT / UPDATE / DELETE. Devuelve las filas afectadas, o -1 si falló.
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

        // Un solo valor (por ejemplo el ID generado). Devuelve -1 si falló.
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

        // Varias filas, en modo desconectado: el adaptador llena un DataTable en memoria.
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
