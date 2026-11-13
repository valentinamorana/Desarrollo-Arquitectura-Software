using System.Collections.Generic;

namespace DAL
{
    public interface IMP_LOG
    {
        int Insertar(BE.LOG log);

        List<BE.LOG> Listar();
    }
}
