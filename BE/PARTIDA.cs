using System;

namespace BE
{
    public class PARTIDA
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int idJugador1;

        public int IdJugador1
        {
            get { return idJugador1; }
            set { idJugador1 = value; }
        }

        private int idJugador2;

        public int IdJugador2
        {
            get { return idJugador2; }
            set { idJugador2 = value; }
        }

        // 0 = todavía no hay ganador (empate o partida en curso)
        private int idGanador;

        public int IdGanador
        {
            get { return idGanador; }
            set { idGanador = value; }
        }

        private int puntajeJugador1;

        public int PuntajeJugador1
        {
            get { return puntajeJugador1; }
            set { puntajeJugador1 = value; }
        }

        private int puntajeJugador2;

        public int PuntajeJugador2
        {
            get { return puntajeJugador2; }
            set { puntajeJugador2 = value; }
        }

        private DateTime fechaInicio;

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        private DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        private string rutaXml;

        public string RutaXml
        {
            get { return rutaXml; }
            set { rutaXml = value; }
        }
    }
}
