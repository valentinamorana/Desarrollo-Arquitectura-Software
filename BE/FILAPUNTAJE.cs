namespace BE
{
    public class FILAPUNTAJE
    {
        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        // -1 = todavía no se anotó
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
