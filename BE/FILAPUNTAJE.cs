namespace BE
{
    public class FILAPUNTAJE
    {
        // -1 = todavía no se anotó
        public const int SIN_ANOTAR = -1;

        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
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
    }
}
