namespace BE
{
    // Un movimiento de la partida, tal como queda grabado en el XML:
    // una tirada, una anotación o el fin de la partida (columna Accion).
    public class MOVIMIENTO
    {
        private int turno;

        public int Turno
        {
            get { return turno; }
            set { turno = value; }
        }

        private string jugador;

        public string Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        // "Tirada 1", "Tirada 2", "Tirada 3", "Anotación" o "Fin de partida".
        private string accion;

        public string Accion
        {
            get { return accion; }
            set { accion = value; }
        }

        private string dados;

        public string Dados
        {
            get { return dados; }
            set { dados = value; }
        }

        // Posiciones (1 a 5) de los dados que se guardaron antes de la tirada.
        private string guardados;

        public string Guardados
        {
            get { return guardados; }
            set { guardados = value; }
        }

        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        // Texto: en una anotación es el puntaje; en el fin, el resultado (ej. "24 a 0").
        private string puntaje;

        public string Puntaje
        {
            get { return puntaje; }
            set { puntaje = value; }
        }

        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
