using System;

namespace BE
{
    public class PARTIDA
    {
        public int ID { get; set; }
        public int IdJugador1 { get; set; }
        public int IdJugador2 { get; set; }
        public int? IdGanador { get; set; }
        public int PuntajeJugador1 { get; set; }
        public int PuntajeJugador2 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string RutaXml { get; set; }
    }
}
