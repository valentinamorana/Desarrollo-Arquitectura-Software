using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class PARTIDA
    {
        public int Insertar(BE.PARTIDA partida)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador1", partida.IdJugador1));
            parametros.Add(acceso.CrearParametro("@idJugador2", partida.IdJugador2));
            parametros.Add(acceso.CrearParametro("@rutaXml", partida.RutaXml));

            acceso.Abrir();
            int id = acceso.LeerEscalar("PARTIDA_INSERTAR", parametros);
            acceso.Cerrar();

            return id;
        }

        public void Finalizar(BE.PARTIDA partida)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", partida.ID));
            parametros.Add(acceso.CrearParametro("@puntajeJugador1", partida.PuntajeJugador1));
            parametros.Add(acceso.CrearParametro("@puntajeJugador2", partida.PuntajeJugador2));

            if (partida.IdGanador.HasValue)
            {
                parametros.Add(acceso.CrearParametro("@idGanador", partida.IdGanador.Value));
            }
            else
            {
                parametros.Add(acceso.CrearParametroNulo("@idGanador"));
            }

            acceso.Abrir();
            acceso.Escribir("PARTIDA_FINALIZAR", parametros);
            acceso.Cerrar();
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));

            acceso.Abrir();
            SqlDataReader reader = acceso.Leer("PARTIDA_LISTAR_POR_USUARIO", parametros);

            List<BE.PARTIDA> partidas = new List<BE.PARTIDA>();
            while (reader.Read())
            {
                BE.PARTIDA p = new BE.PARTIDA();
                p.ID = reader.GetInt32(0);
                p.IdJugador1 = reader.GetInt32(1);
                p.IdJugador2 = reader.GetInt32(2);
                p.IdGanador = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                p.PuntajeJugador1 = reader.GetInt32(4);
                p.PuntajeJugador2 = reader.GetInt32(5);
                p.FechaInicio = reader.GetDateTime(6);
                p.FechaFin = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7);
                p.RutaXml = reader.IsDBNull(8) ? "" : reader.GetString(8);
                partidas.Add(p);
            }

            reader.Close();
            acceso.Cerrar();
            return partidas;
        }
    }
}
