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

        // -------------------- Planilla de puntajes --------------------
        // La GUI guarda la planilla (List<FILAPUNTAJE>) para mostrarla en la grilla,
        // pero todas las reglas sobre ella (qué se puede anotar, cuánto vale, totales,
        // ganador, fin de partida) las decide BLL. "numeroJugador" es 1 o 2.

        public List<BE.FILAPUNTAJE> CrearTablaVacia()
        {
            List<BE.FILAPUNTAJE> tabla = new List<BE.FILAPUNTAJE>();
            foreach (string categoria in GENERALA.CATEGORIAS)
            {
                BE.FILAPUNTAJE fila = new BE.FILAPUNTAJE();
                fila.Categoria = categoria;
                fila.PuntajeJugador1 = BE.FILAPUNTAJE.SIN_ANOTAR;
                fila.PuntajeJugador2 = BE.FILAPUNTAJE.SIN_ANOTAR;
                tabla.Add(fila);
            }
            return tabla;
        }

        public List<string> CategoriasDisponibles(List<BE.FILAPUNTAJE> tabla, int numeroJugador)
        {
            bool generalaYaAnotada = PuntajeDe(BuscarFila(tabla, "Generala"), numeroJugador) != BE.FILAPUNTAJE.SIN_ANOTAR;

            List<string> disponibles = new List<string>();
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                if (PuntajeDe(fila, numeroJugador) != BE.FILAPUNTAJE.SIN_ANOTAR)
                {
                    continue;
                }

                // "Generala Doble" solo se puede anotar después de haber anotado "Generala".
                if (fila.Categoria == GENERALA.GENERALA_DOBLE && !generalaYaAnotada)
                {
                    continue;
                }

                disponibles.Add(fila.Categoria);
            }
            return disponibles;
        }

        // Calcula el puntaje de la categoría elegida, lo anota en la planilla y lo devuelve.
        public int Anotar(List<BE.FILAPUNTAJE> tabla, int numeroJugador, string categoria, int[] dados, bool servido)
        {
            int puntaje = GENERALA.CalcularPuntaje(categoria, dados, servido);

            // La Generala Doble solo vale si la Generala se anotó con puntos.
            // Si la Generala quedó tachada (0), la Generala Doble solo se puede tachar.
            if (categoria == GENERALA.GENERALA_DOBLE && !GeneralaConPuntos(tabla, numeroJugador))
            {
                puntaje = 0;
            }

            BE.FILAPUNTAJE fila = BuscarFila(tabla, categoria);
            if (numeroJugador == 1)
            {
                fila.PuntajeJugador1 = puntaje;
            }
            else
            {
                fila.PuntajeJugador2 = puntaje;
            }
            return puntaje;
        }

        public bool GeneralaConPuntos(List<BE.FILAPUNTAJE> tabla, int numeroJugador)
        {
            return PuntajeDe(BuscarFila(tabla, "Generala"), numeroJugador) > 0;
        }

        public int CalcularTotal(List<BE.FILAPUNTAJE> tabla, int numeroJugador)
        {
            int total = 0;
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                int puntaje = PuntajeDe(fila, numeroJugador);
                if (puntaje != BE.FILAPUNTAJE.SIN_ANOTAR)
                {
                    total = total + puntaje;
                }
            }
            return total;
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

        private BE.FILAPUNTAJE BuscarFila(List<BE.FILAPUNTAJE> tabla, string categoria)
        {
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                if (fila.Categoria == categoria)
                {
                    return fila;
                }
            }
            return null;
        }

        private int PuntajeDe(BE.FILAPUNTAJE fila, int numeroJugador)
        {
            if (numeroJugador == 1)
            {
                return fila.PuntajeJugador1;
            }
            return fila.PuntajeJugador2;
        }

        // -------------------- Fin de partida --------------------

        // Fin normal: se completaron todas las categorías. Gana el de más puntos (o empate).
        public void Finalizar(BE.PARTIDA partida, List<BE.FILAPUNTAJE> tabla)
        {
            partida.PuntajeJugador1 = CalcularTotal(tabla, 1);
            partida.PuntajeJugador2 = CalcularTotal(tabla, 2);

            if (partida.PuntajeJugador1 > partida.PuntajeJugador2)
            {
                partida.IdGanador = partida.IdJugador1;
            }
            else if (partida.PuntajeJugador2 > partida.PuntajeJugador1)
            {
                partida.IdGanador = partida.IdJugador2;
            }
            else
            {
                partida.IdGanador = 0;
            }

            Cerrar(partida, false);
        }

        // Abandono: se guardan los puntajes parciales, pero la partida la gana siempre
        // el rival de quien abandonó, sin importar cómo venían los puntos.
        public void Abandonar(BE.PARTIDA partida, List<BE.FILAPUNTAJE> tabla, int idQuienAbandona)
        {
            partida.PuntajeJugador1 = CalcularTotal(tabla, 1);
            partida.PuntajeJugador2 = CalcularTotal(tabla, 2);

            if (idQuienAbandona == partida.IdJugador1)
            {
                partida.IdGanador = partida.IdJugador2;
            }
            else
            {
                partida.IdGanador = partida.IdJugador1;
            }

            Cerrar(partida, true);
        }

        private void Cerrar(BE.PARTIDA partida, bool abandonada)
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
