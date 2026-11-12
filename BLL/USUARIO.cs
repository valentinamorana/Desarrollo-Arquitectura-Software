using System;

namespace BLL
{
    public class USUARIO
    {
        public const int MIN_NOMBRE = 3;
        public const int MAX_NOMBRE = 20;
        public const int MIN_CONTRASEÑA = 4;
        public const int MAX_CONTRASEÑA = 20;

        private DAL.MP_USUARIO mapper = new DAL.MP_USUARIO();

        public string ValidarRegistro(BE.USUARIO usuario)
        {
            if (usuario.Nombre.Contains(" "))
            {
                return "El nombre de usuario no puede tener espacios";
            }
            if (usuario.Nombre.Length < MIN_NOMBRE || usuario.Nombre.Length > MAX_NOMBRE)
            {
                return "El nombre de usuario tiene que tener entre " + MIN_NOMBRE + " y " + MAX_NOMBRE + " caracteres";
            }
            if (usuario.Contraseña.Length < MIN_CONTRASEÑA || usuario.Contraseña.Length > MAX_CONTRASEÑA)
            {
                return "La contraseña tiene que tener entre " + MIN_CONTRASEÑA + " y " + MAX_CONTRASEÑA + " caracteres";
            }
            if (!TieneNumero(usuario.Contraseña))
            {
                return "La contraseña tiene que tener al menos un número";
            }
            return "";
        }

        private bool TieneNumero(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

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

        public bool Insertar(BE.USUARIO usuario)
        {
            string error = ValidarRegistro(usuario);
            if (error != "")
            {
                throw new Exception(error);
            }

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
