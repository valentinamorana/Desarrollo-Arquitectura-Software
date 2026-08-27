namespace BE
{
    public class FILAHISTORIAL
    {
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string rival;

        public string Rival
        {
            get { return rival; }
            set { rival = value; }
        }

        private int puntajePropio;

        public int PuntajePropio
        {
            get { return puntajePropio; }
            set { puntajePropio = value; }
        }

        private int puntajeRival;

        public int PuntajeRival
        {
            get { return puntajeRival; }
            set { puntajeRival = value; }
        }

        private string resultado;

        public string Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }
    }
}
