using System.Collections.Generic;

namespace DAL
{
    public interface IMP_PARTIDA
    {
        int Insertar(BE.PARTIDA partida);

        bool Finalizar(BE.PARTIDA partida, string descripcion);

        List<BE.PARTIDA> ListarPorUsuario(int idUsuario);
    }
}
