using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_USUARIO : MAPPER<BE.USUARIO>
    {
        public override int Insertar(BE.USUARIO usuario)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@usu", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@pass", usuario.Contraseña));

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
            parametros.Add(acceso.CrearParametro("@pass", usuario.Contraseña));

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
            usuario.Contraseña = fila["Contraseña"].ToString();
            return true;
        }

        public BE.USUARIO BuscarPorId(int id)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", id));

            acceso.Abrir();
            DataTable tabla;
            try
            {
                tabla = acceso.Leer("USUARIO_BUSCAR_POR_ID", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }

            if (tabla.Rows.Count == 0)
            {
                return null;
            }

            DataRow fila = tabla.Rows[0];
            BE.USUARIO usuario = new BE.USUARIO();
            usuario.ID = int.Parse(fila["ID"].ToString());
            usuario.Nombre = fila["Nombre"].ToString();
            usuario.Contraseña = fila["Contraseña"].ToString();
            return usuario;
        }
    }
}
