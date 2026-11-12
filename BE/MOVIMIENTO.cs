namespace BE
{
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
