using System;

namespace BLL
{
    public static class BITACORA
    {
        public static void Registrar(int idUsuario, int idTipo, string descripcion)
        {
            BE.LOG log = new BE.LOG();
            log.IdUsuario = idUsuario;
            log.IdTipo = idTipo;
            log.Descripcion = descripcion;

            DAL.MP_LOG mapper = new DAL.MP_LOG();
            if (mapper.Insertar(log) == -1)
            {
                throw new Exception("No se pudo registrar en la bitácora: " + descripcion);
            }
        }

        public static System.Collections.Generic.List<BE.LOG> Listar()
        {
            DAL.MP_LOG mapper = new DAL.MP_LOG();
            return mapper.Listar();
        }

        public static System.Collections.Generic.List<BE.LOG> Filtrar(
            System.Collections.Generic.List<BE.LOG> registros, string nombreUsuario, string nombreTipo,
            DateTime? desde, DateTime? hasta)
        {
            System.Collections.Generic.List<BE.LOG> resultado = new System.Collections.Generic.List<BE.LOG>();
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
                if (desde.HasValue && registro.Fecha < desde.Value)
                {
                    continue;
                }
                if (hasta.HasValue && registro.Fecha >= hasta.Value)
                {
                    continue;
                }
                resultado.Add(registro);
            }
            return resultado;
        }
    }
}
