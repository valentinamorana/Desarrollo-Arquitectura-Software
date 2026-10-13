using System;

namespace BLL
{
    public class USUARIO
    {
        private DAL.MP_USUARIO mapper = new DAL.MP_USUARIO();

        public bool Login(BE.USUARIO usuario)
        {
            bool ok = mapper.Login(usuario);
            if (ok)
            {
                BITACORA.Registrar(usuario.ID, BE.LOG.INICIO_SESION, "Inicio de sesión de " + usuario.Nombre);
            }
            return ok;
        }

        public void Logout(BE.USUARIO usuario)
        {
            BITACORA.Registrar(usuario.ID, BE.LOG.CIERRE_SESION, "Cierre de sesión de " + usuario.Nombre);
        }

        // Devuelve false si el nombre de usuario ya existe.
        // Si falla la base de datos tira una excepción con el motivo.
        public bool Insertar(BE.USUARIO usuario)
        {
            int id = mapper.Insertar(usuario);
            if (id == -1)
            {
                throw new Exception("No se pudo registrar el usuario en la base de datos.");
            }
            if (id == 0)
            {
                return false;
            }
            usuario.ID = id;
            return true;
        }

        public BE.USUARIO BuscarPorId(int id)
        {
            return mapper.BuscarPorId(id);
        }
    }
}
