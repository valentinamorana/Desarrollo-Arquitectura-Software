using System.Collections.Generic;

namespace BLL
{
    public class PARTIDA
    {
        private DAL.PARTIDA dal = new DAL.PARTIDA();

        public event delPartidaFinalizada PartidaFinalizada;

        public void Iniciar(BE.PARTIDA partida)
        {
            partida.RutaXml = MOVIMIENTOXML.GenerarRuta();
            partida.ID = dal.Insertar(partida);
            MOVIMIENTOXML.CrearArchivo(partida.RutaXml);

            BITACORA.Registrar(partida.IdJugador1, BITACORA.INICIO_PARTIDA, "Inicio de partida");
            BITACORA.Registrar(partida.IdJugador2, BITACORA.INICIO_PARTIDA, "Inicio de partida");
        }

        // La GUI le pasa la tabla de puntajes y BLL decide si la partida ya terminó,
        // en vez de que sea la GUI quien conozca esa regla.
        public bool EstaFinalizada(List<BE.FILAPUNTAJE> tabla)
        {
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                if (fila.PuntajeJugador1 == BE.FILAPUNTAJE.SIN_ANOTAR || fila.PuntajeJugador2 == BE.FILAPUNTAJE.SIN_ANOTAR)
                {
                    return false;
                }
            }
            return true;
        }

        public void Finalizar(BE.PARTIDA partida, bool abandonada = false)
        {
            dal.Finalizar(partida);

            string descripcion;
            if (abandonada)
            {
                descripcion = "Partida abandonada";
            }
            else
            {
                descripcion = "Fin de partida";
            }

            BITACORA.Registrar(partida.IdJugador1, BITACORA.FIN_PARTIDA, descripcion);
            BITACORA.Registrar(partida.IdJugador2, BITACORA.FIN_PARTIDA, descripcion);

            PartidaFinalizada(partida, abandonada);
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            return dal.ListarPorUsuario(idUsuario);
        }
    }
}
