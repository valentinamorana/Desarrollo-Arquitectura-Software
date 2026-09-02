using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class LOG
    {
        public void Insertar(int idUsuario, int idTipo, string descripcion)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@descripcion", descripcion));
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));
            parametros.Add(acceso.CrearParametro("@idTipo", idTipo));

            acceso.Abrir();
            try
            {
                acceso.Escribir("LOG_INSERTAR", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }
    }
}
