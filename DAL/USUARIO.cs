using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class USUARIO
    {
        public int Insertar(BE.USUARIO usuario)
        {
            ACCESO acceso = new ACCESO();
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
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@usu", usuario.Nombre));
            parametros.Add(acceso.CrearParametro("@pass", usuario.Contraseña));

            acceso.Abrir();
            bool ok = false;
            try
            {
                SqlDataReader reader = acceso.Leer("USUARIO_LOGIN", parametros);
                if (reader.Read())
                {
                    ok = true;
                    usuario.ID = reader.GetInt32(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Contraseña = reader.GetString(2);
                }
                reader.Close();
            }
            finally
            {
                acceso.Cerrar();
            }

            return ok;
        }

        public BE.USUARIO BuscarPorId(int id)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", id));

            acceso.Abrir();
            BE.USUARIO usuario = null;
            try
            {
                SqlDataReader reader = acceso.Leer("USUARIO_BUSCAR_POR_ID", parametros);
                if (reader.Read())
                {
                    usuario = new BE.USUARIO();
                    usuario.ID = reader.GetInt32(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Contraseña = reader.GetString(2);
                }
                reader.Close();
            }
            finally
            {
                acceso.Cerrar();
            }

            return usuario;
        }
    }
}
