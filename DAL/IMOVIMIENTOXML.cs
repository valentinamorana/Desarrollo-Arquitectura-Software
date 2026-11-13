using System.Collections.Generic;

namespace DAL
{
    public interface IMOVIMIENTOXML
    {
        string GenerarRuta();

        void CrearArchivo(string ruta, string fecha, string jugador1, string jugador2);

        void InsertarTirada(string ruta, int turno, string jugador, int numeroTirada, string dados, string guardados, string fecha);

        void InsertarMovimiento(string ruta, int turno, string jugador, string dados, string categoria, int puntaje, string fecha);

        void QuitarUltimoMovimiento(string ruta);

        void InsertarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono, string fecha);

        void Eliminar(string ruta);

        bool Existe(string ruta);

        string Resolver(string ruta);

        void Copiar(string origen, string destino);

        List<BE.MOVIMIENTO> Listar(string ruta);
    }
}
