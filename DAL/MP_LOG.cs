using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_LOG : MAPPER<BE.LOG>
    {
        // Todos los registros de la bitácora, del más nuevo al más viejo.
        public List<BE.LOG> Listar()
        {
            acceso = new ACCESO();
            acceso.Abrir();
            DataTable tabla;
            try
            {
                tabla = acceso.Leer("BITACORA_LISTAR");
            }
            finally
            {
                acceso.Cerrar();
            }

            List<BE.LOG> registros = new List<BE.LOG>();
            foreach (DataRow fila in tabla.Rows)
            {
                BE.LOG log = new BE.LOG();
                log.IdUsuario = int.Parse(fila["ID_Usuario"].ToString());
                log.IdTipo = int.Parse(fila["ID_TIPO"].ToString());
                log.Descripcion = fila["Descripcion"].ToString();
                log.Fecha = Convert.ToDateTime(fila["Fecha"]);
                log.NombreUsuario = fila["NombreUsuario"].ToString();
                log.NombreTipo = fila["NombreTipo"].ToString();
                registros.Add(log);
            }
            return registros;
        }

        // Devuelve las filas insertadas (1), o -1 si falló.
        public override int Insertar(BE.LOG log)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@descripcion", log.Descripcion));
            parametros.Add(acceso.CrearParametro("@idUsuario", log.IdUsuario));
            parametros.Add(acceso.CrearParametro("@idTipo", log.IdTipo));

            acceso.Abrir();
            int res;
            try
            {
                res = acceso.Escribir("LOG_INSERTAR", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
            return res;
        }
    }
}
