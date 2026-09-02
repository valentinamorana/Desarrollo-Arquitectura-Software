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

        public void Finalizar(BE.PARTIDA partida)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", partida.ID));
            parametros.Add(acceso.CrearParametro("@puntajeJugador1", partida.PuntajeJugador1));
            parametros.Add(acceso.CrearParametro("@puntajeJugador2", partida.PuntajeJugador2));

            if (partida.IdGanador > 0)
            {
                parametros.Add(acceso.CrearParametro("@idGanador", partida.IdGanador));
            }
            else
            {
                parametros.Add(acceso.CrearParametroNulo("@idGanador"));
            }

            acceso.Abrir();
            try
            {
                acceso.Escribir("PARTIDA_FINALIZAR", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            ACCESO acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));

            acceso.Abrir();
            List<BE.PARTIDA> partidas = new List<BE.PARTIDA>();
            try
            {
                SqlDataReader reader = acceso.Leer("PARTIDA_LISTAR_POR_USUARIO", parametros);
                while (reader.Read())
                {
                    BE.PARTIDA p = new BE.PARTIDA();
                    p.ID = reader.GetInt32(0);
                    p.IdJugador1 = reader.GetInt32(1);
                    p.IdJugador2 = reader.GetInt32(2);
                    if (reader.IsDBNull(3))
                    {
                        p.IdGanador = 0;
                    }
                    else
                    {
                        p.IdGanador = reader.GetInt32(3);
                    }
                    p.PuntajeJugador1 = reader.GetInt32(4);
                    p.PuntajeJugador2 = reader.GetInt32(5);
                    p.FechaInicio = reader.GetDateTime(6);
                    p.FechaFin = reader.GetDateTime(7);
                    if (reader.IsDBNull(8))
                    {
                        p.RutaXml = "";
                    }
                    else
                    {
                        p.RutaXml = reader.GetString(8);
                    }
                    partidas.Add(p);
                }
                reader.Close();
            }
            finally
            {
                acceso.Cerrar();
            }
            return partidas;
        }
    }
}
