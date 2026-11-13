using System;
using System.Collections.Generic;

namespace BLL
{
    public class BITACORA
    {
        public const int MAX_DESCRIPCION = 100;

        private DAL.IMP_LOG mapper;

        public BITACORA() : this(new DAL.MP_LOG())
        {
        }

        public BITACORA(DAL.IMP_LOG mapper)
        {
            this.mapper = mapper;
        }

        public void Registrar(int idUsuario, int idTipo, string descripcion)
        {
            if (descripcion == null)
            {
                descripcion = "";
            }
            if (descripcion.Length > MAX_DESCRIPCION)
            {
                descripcion = descripcion.Substring(0, MAX_DESCRIPCION);
            }

            BE.LOG log = new BE.LOG();
            log.IdUsuario = idUsuario;
            log.IdTipo = idTipo;
            log.Descripcion = descripcion;

            if (mapper.Insertar(log) == -1)
            {
                throw new Exception("No se pudo registrar en la bitácora: " + descripcion);
            }
        }

        public List<BE.LOG> Listar()
        {
            return mapper.Listar();
        }

        public static List<BE.LOG> Filtrar(List<BE.LOG> registros, string nombreUsuario, string nombreTipo,
            DateTime? desde, DateTime? hasta)
        {
            if (desde.HasValue && hasta.HasValue && desde.Value.Date > hasta.Value.Date)
            {
                DateTime auxiliar = desde.Value;
                desde = hasta;
                hasta = auxiliar;
            }

            List<BE.LOG> resultado = new List<BE.LOG>();
            foreach (BE.LOG registro in registros)
            {
                if (!string.IsNullOrEmpty(nombreUsuario) && registro.NombreUsuario != nombreUsuario)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(nombreTipo) && registro.NombreTipo != nombreTipo)
                {
                    continue;
                }
                if (desde.HasValue && registro.Fecha < desde.Value.Date)
                {
                    continue;
                }
                if (hasta.HasValue && registro.Fecha >= hasta.Value.Date.AddDays(1))
                {
                    continue;
                }
                resultado.Add(registro);
            }
            return resultado;
        }

        public static List<string> UsuariosDe(List<BE.LOG> registros)
        {
            List<string> usuarios = new List<string>();
            foreach (BE.LOG registro in registros)
            {
                if (!usuarios.Contains(registro.NombreUsuario))
                {
                    usuarios.Add(registro.NombreUsuario);
                }
            }
            usuarios.Sort();
            return usuarios;
        }

        public static List<string> TiposDe(List<BE.LOG> registros)
        {
            List<string> tipos = new List<string>();
            foreach (BE.LOG registro in registros)
            {
                if (!tipos.Contains(registro.NombreTipo))
                {
                    tipos.Add(registro.NombreTipo);
                }
            }
            tipos.Sort();
            return tipos;
        }
    }
}
