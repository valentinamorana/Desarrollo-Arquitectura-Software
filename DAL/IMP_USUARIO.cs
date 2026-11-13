namespace DAL
{
    public interface IMP_USUARIO
    {
        int Insertar(BE.USUARIO usuario);

        bool Login(BE.USUARIO usuario);

        BE.USUARIO BuscarPorId(int id);
    }
}
