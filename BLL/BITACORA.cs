namespace BLL
{
    public static class BITACORA
    {
        public const int INICIO_SESION = 1;
        public const int CIERRE_SESION = 2;
        public const int INICIO_PARTIDA = 3;
        public const int FIN_PARTIDA = 4;

        public static void Registrar(int idUsuario, int idTipo, string descripcion)
        {
            DAL.LOG dal = new DAL.LOG();
            dal.Insertar(idUsuario, idTipo, descripcion);
        }
    }
}
