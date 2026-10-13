using System;
using System.Collections.Generic;

namespace BE
{
    // Resultado de las estadísticas de un jugador: lo calcula BLL.ESTADISTICA
    // y FormEstadisticas solo lo muestra.
    public class ESTADISTICA
    {
        private int ganadas;

        public int Ganadas
        {
            get { return ganadas; }
            set { ganadas = value; }
        }

        private int perdidas;

        public int Perdidas
        {
            get { return perdidas; }
            set { perdidas = value; }
        }

        private int empatadas;

        public int Empatadas
        {
            get { return empatadas; }
            set { empatadas = value; }
        }

        // Porcentaje de partidas ganadas sobre el total (0 a 100).
        private double promedioVictorias;

        public double PromedioVictorias
        {
            get { return promedioVictorias; }
            set { promedioVictorias = value; }
        }

        private TimeSpan tiempoTotal;

        public TimeSpan TiempoTotal
        {
            get { return tiempoTotal; }
            set { tiempoTotal = value; }
        }

        private List<FILAHISTORIAL> historial;

        public List<FILAHISTORIAL> Historial
        {
            get { return historial; }
            set { historial = value; }
        }
    }
}
