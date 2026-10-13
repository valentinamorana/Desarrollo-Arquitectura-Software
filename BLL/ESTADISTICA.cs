using System;
using System.Collections.Generic;

namespace BLL
{
    // Estadísticas de un jugador a partir de sus partidas terminadas.
    public class ESTADISTICA
    {
        private DAL.MP_PARTIDA mapperPartida = new DAL.MP_PARTIDA();
        private DAL.MP_USUARIO mapperUsuario = new DAL.MP_USUARIO();

        public BE.ESTADISTICA Calcular(BE.USUARIO usuario)
        {
            List<BE.PARTIDA> partidas = mapperPartida.ListarPorUsuario(usuario.ID);

            BE.ESTADISTICA estadistica = new BE.ESTADISTICA();
            estadistica.TiempoTotal = TimeSpan.Zero;
            estadistica.Historial = new List<BE.FILAHISTORIAL>();

            foreach (BE.PARTIDA p in partidas)
            {
                // El usuario pudo haber sido el jugador 1 o el 2 de esa partida.
                int idRival;
                int puntajePropio;
                int puntajeRival;
                if (p.IdJugador1 == usuario.ID)
                {
                    idRival = p.IdJugador2;
                    puntajePropio = p.PuntajeJugador1;
                    puntajeRival = p.PuntajeJugador2;
                }
                else
                {
                    idRival = p.IdJugador1;
                    puntajePropio = p.PuntajeJugador2;
                    puntajeRival = p.PuntajeJugador1;
                }

                // IdGanador = 0 significa empate.
                string resultado;
                if (p.IdGanador == 0)
                {
                    resultado = "Empate";
                    estadistica.Empatadas++;
                }
                else if (p.IdGanador == usuario.ID)
                {
                    resultado = "Ganada";
                    estadistica.Ganadas++;
                }
                else
                {
                    resultado = "Perdida";
                    estadistica.Perdidas++;
                }

                estadistica.TiempoTotal = estadistica.TiempoTotal + (p.FechaFin - p.FechaInicio);

                BE.FILAHISTORIAL fila = new BE.FILAHISTORIAL();
                fila.Fecha = p.FechaInicio.ToString("dd/MM/yyyy HH:mm");
                BE.USUARIO rival = mapperUsuario.BuscarPorId(idRival);
                if (rival != null)
                {
                    fila.Rival = rival.Nombre;
                }
                else
                {
                    fila.Rival = "-";
                }
                fila.PuntajePropio = puntajePropio;
                fila.PuntajeRival = puntajeRival;
                fila.Resultado = resultado;
                fila.RutaXml = p.RutaXml;
                estadistica.Historial.Add(fila);
            }

            // Promedio de victorias: ganadas sobre el total de partidas, en porcentaje.
            if (partidas.Count > 0)
            {
                estadistica.PromedioVictorias = estadistica.Ganadas * 100.0 / partidas.Count;
            }
            else
            {
                estadistica.PromedioVictorias = 0;
            }

            return estadistica;
        }
    }
}
