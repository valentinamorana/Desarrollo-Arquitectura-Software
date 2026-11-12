using System;
using System.Collections.Generic;

namespace BE
{
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
