using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_USUARIO : MAPPER<BE.USUARIO>, IMP_USUARIO
    {
        public override int Insertar(BE.USUARIO usuario)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@usu", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@pass", usuario.Contrasena));

            acceso.Abrir();
            int id;
            try
            {
                id = acceso.LeerEscalar("USUARIO_INSERTAR", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
            return id;
        }

        public bool Login(BE.USUARIO usuario)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@usu", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@pass", usuario.Contrasena));

            acceso.Abrir();
            DataTable tabla;
            try
            {
                tabla = acceso.Leer("USUARIO_LOGIN", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }

            if (tabla.Rows.Count == 0)
            {
                return false;
            }

            DataRow fila = tabla.Rows[0];
            usuario.ID = int.Parse(fila["ID"].ToString());
            usuario.Nombre = fila["Nombre"].ToString();
            return true;
        }
    }
}
