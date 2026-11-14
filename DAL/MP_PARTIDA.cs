using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_PARTIDA : MAPPER<BE.PARTIDA>, IMP_PARTIDA
    {
        public override int Insertar(BE.PARTIDA partida)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador1", partida.IdJugador1));
            parametros.Add(acceso.CrearParametro("@idJugador2", partida.IdJugador2));
            parametros.Add(acceso.CrearParametro("@idGanador", partida.IdGanador));
            parametros.Add(acceso.CrearParametro("@puntajeJugador1", partida.PuntajeJugador1));
            parametros.Add(acceso.CrearParametro("@puntajeJugador2", partida.PuntajeJugador2));
            parametros.Add(acceso.CrearParametro("@fechaInicio", partida.FechaInicio));
            parametros.Add(acceso.CrearParametro("@fechaFin", partida.FechaFin));
            parametros.Add(acceso.CrearParametro("@rutaXml", partida.RutaXml));

            acceso.Abrir();
            int id;
            try
            {
                id = acceso.LeerEscalar("PARTIDA_INSERTAR", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
            return id;
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));

            acceso.Abrir();
            DataTable tabla;
            try
            {
                tabla = acceso.Leer("PARTIDA_LISTAR_POR_USUARIO", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }

            List<BE.PARTIDA> partidas = new List<BE.PARTIDA>();
            foreach (DataRow fila in tabla.Rows)
            {
                BE.PARTIDA p = new BE.PARTIDA();
                p.ID = int.Parse(fila["ID"].ToString());
                p.IdJugador1 = int.Parse(fila["ID_JUGADOR1"].ToString());
                p.IdJugador2 = int.Parse(fila["ID_JUGADOR2"].ToString());
                p.IdGanador = int.Parse(fila["ID_GANADOR"].ToString());
                p.PuntajeJugador1 = int.Parse(fila["PUNTAJE_JUGADOR1"].ToString());
                p.PuntajeJugador2 = int.Parse(fila["PUNTAJE_JUGADOR2"].ToString());
                p.FechaInicio = Convert.ToDateTime(fila["FECHA_INICIO"]);
                p.FechaFin = Convert.ToDateTime(fila["FECHA_FIN"]);
                p.RutaXml = fila["RUTA_XML"].ToString();
                p.NombreJugador1 = fila["NOMBRE_JUGADOR1"].ToString();
                p.NombreJugador2 = fila["NOMBRE_JUGADOR2"].ToString();
                partidas.Add(p);
            }
            return partidas;
        }
    }
}
