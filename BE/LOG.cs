namespace BE
{
    // Un registro de la bitácora (tabla LOG).
    public class LOG
    {
        // Tipos de evento: coinciden con los ID de la tabla TIPO_LOG.
        public const int INICIO_SESION = 1;
        public const int CIERRE_SESION = 2;
        public const int INICIO_PARTIDA = 3;
        public const int FIN_PARTIDA = 4;

        private int idUsuario;

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        private int idTipo;

        public int IdTipo
        {
            get { return idTipo; }
            set { idTipo = value; }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        // Datos que se leen para mostrar la bitácora (no se usan al grabar).
        private System.DateTime fecha;

        public System.DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string nombreUsuario;

        public string NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        private string nombreTipo;

        public string NombreTipo
        {
            get { return nombreTipo; }
            set { nombreTipo = value; }
        }
    }
}
