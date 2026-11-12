using System;
using System.Collections.Generic;

namespace BLL
{
    public class ESTADISTICA
    {
        public enum FILTRO_RESULTADO
        {
            Todas = 0,
            Ganadas = 1,
            Perdidas = 2,
            Empates = 3
        }

        public static List<BE.FILAHISTORIAL> Filtrar(List<BE.FILAHISTORIAL> historial, FILTRO_RESULTADO filtro)
        {
            List<BE.FILAHISTORIAL> visibles = new List<BE.FILAHISTORIAL>();
            foreach (BE.FILAHISTORIAL fila in historial)
            {
                if (filtro == FILTRO_RESULTADO.Todas
                    || (filtro == FILTRO_RESULTADO.Ganadas && fila.Resultado == "Ganada")
                    || (filtro == FILTRO_RESULTADO.Perdidas && fila.Resultado == "Perdida")
                    || (filtro == FILTRO_RESULTADO.Empates && fila.Resultado == "Empate"))
                {
                    visibles.Add(fila);
                }
            }
            return visibles;
        }

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
