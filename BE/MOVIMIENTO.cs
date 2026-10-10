namespace BE
{
    // Una jugada anotada, tal como queda grabada en el XML de la partida.
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

        private string dados;

        public string Dados
        {
            get { return dados; }
            set { dados = value; }
        }

        private string categoria;

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        private int puntaje;

        public int Puntaje
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
