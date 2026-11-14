using System.Collections.Generic;

namespace DAL
{
    public interface IMP_PARTIDA
    {
        int Insertar(BE.PARTIDA partida);

        List<BE.PARTIDA> ListarPorUsuario(int idUsuario);
    }
}
