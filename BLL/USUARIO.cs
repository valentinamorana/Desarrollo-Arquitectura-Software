namespace BLL
{
    public class USUARIO
    {
        private DAL.USUARIO dal = new DAL.USUARIO();

        public bool Login(BE.USUARIO usuario)
        {
            bool ok = dal.Login(usuario);
            if (ok)
            {
                BITACORA.Registrar(usuario.ID, BITACORA.INICIO_SESION, "Inicio de sesión de " + usuario.Nombre);
            }
            return ok;
        }

        public void Logout(BE.USUARIO usuario)
        {
            BITACORA.Registrar(usuario.ID, BITACORA.CIERRE_SESION, "Cierre de sesión de " + usuario.Nombre);
        }

        public void Insertar(BE.USUARIO usuario)
        {
            usuario.ID = dal.Insertar(usuario);
        }

        public BE.USUARIO BuscarPorId(int id)
        {
            return dal.BuscarPorId(id);
        }
    }
}
