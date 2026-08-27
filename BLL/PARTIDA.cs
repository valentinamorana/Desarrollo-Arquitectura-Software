using System.Collections.Generic;

namespace BLL
{
    public class PARTIDA
    {
        private DAL.PARTIDA dal = new DAL.PARTIDA();

        public void Iniciar(BE.PARTIDA partida)
        {
            partida.RutaXml = MOVIMIENTOXML.GenerarRuta();
            partida.ID = dal.Insertar(partida);
            MOVIMIENTOXML.CrearArchivo(partida.RutaXml);

            BITACORA.Registrar(partida.IdJugador1, BITACORA.INICIO_PARTIDA, "Inicio de partida");
            BITACORA.Registrar(partida.IdJugador2, BITACORA.INICIO_PARTIDA, "Inicio de partida");
        }

        public void Finalizar(BE.PARTIDA partida, string descripcion = "Fin de partida")
        {
            dal.Finalizar(partida);

            BITACORA.Registrar(partida.IdJugador1, BITACORA.FIN_PARTIDA, descripcion);
            BITACORA.Registrar(partida.IdJugador2, BITACORA.FIN_PARTIDA, descripcion);
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            return dal.ListarPorUsuario(idUsuario);
        }
    }
}
