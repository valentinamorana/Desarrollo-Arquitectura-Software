using System;

namespace BLL
{
    public class USUARIO
    {
        public const int MIN_NOMBRE = 3;
        public const int MAX_NOMBRE = 20;
        public const int MIN_CONTRASENA = 4;
        public const int MAX_CONTRASENA = 20;

        private DAL.IMP_USUARIO mapper;
        private BITACORA bitacora;

        public USUARIO() : this(new DAL.MP_USUARIO(), new BITACORA())
        {
        }

        public USUARIO(DAL.IMP_USUARIO mapper, BITACORA bitacora)
        {
            this.mapper = mapper;
            this.bitacora = bitacora;
        }

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
            if (usuario.Contrasena.Length < MIN_CONTRASENA || usuario.Contrasena.Length > MAX_CONTRASENA)
            {
                return "La contraseña tiene que tener entre " + MIN_CONTRASENA + " y " + MAX_CONTRASENA + " caracteres";
            }
            if (!TieneNumero(usuario.Contrasena))
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
                usuario.Contrasena = "";
                bitacora.Registrar(usuario.ID, BE.LOG.INICIO_SESION, "Inicio de sesión de " + usuario.Nombre);
            }
            return ok;
        }

        public void Logout(BE.USUARIO usuario)
        {
            bitacora.Registrar(usuario.ID, BE.LOG.CIERRE_SESION, "Cierre de sesión de " + usuario.Nombre);
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
    }
}
