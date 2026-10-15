using System;
using System.Collections.Generic;

namespace BLL
{
    // Maneja una partida de principio a fin: turnos, tiradas, planilla de puntajes,
    // movimientos en XML, ganador y cierre. La GUI solo muestra este estado y le avisa
    // a esta clase lo que hace el usuario (tirar, anotar, abandonar).
    public class PARTIDA
    {
        public const int TIRADAS_POR_TURNO = 3;

        private DAL.MP_PARTIDA mapper = new DAL.MP_PARTIDA();

        private BE.PARTIDA partida;
        private BE.USUARIO jugador1;
        private BE.USUARIO jugador2;

        // Planilla: una fila por categoría, con el puntaje de cada jugador.
        private List<BE.FILAPUNTAJE> tabla;

        private int jugadorActual;      // 1 o 2
        private int tirosRestantes;
        private int[] dados;
        private int turno;              // cuántas categorías se anotaron en total (numera el XML)
        private bool enCurso = false;

        public event delPartidaFinalizada PartidaFinalizada;

        public PARTIDA()
        {
            tabla = CrearTablaVacia();
            dados = new int[5];
        }

        public bool EnCurso
        {
            get { return enCurso; }
        }

        public int TirosRestantes
        {
            get { return tirosRestantes; }
        }

        public int[] Dados
        {
            get { return dados; }
        }

        public List<BE.FILAPUNTAJE> Tabla
        {
            get { return tabla; }
        }

        // -------------------- Inicio --------------------

        public void Iniciar(BE.USUARIO j1, BE.USUARIO j2)
        {
            if (j1.ID == j2.ID)
            {
                throw new Exception("El Jugador 2 tiene que ser un usuario distinto al Jugador 1.");
            }

            BE.PARTIDA nueva = new BE.PARTIDA();
            nueva.IdJugador1 = j1.ID;
            nueva.IdJugador2 = j2.ID;
            nueva.RutaXml = MOVIMIENTOXML.GenerarRuta();

            // Graba la partida y el inicio en la bitácora en una sola transacción.
            nueva.ID = mapper.Insertar(nueva);
            if (nueva.ID == -1)
            {
                throw new Exception("No se pudo iniciar la partida en la base de datos. " +
                    "Si se restauró un backup, puede que alguno de los jugadores ya no exista: volvé a ingresar.");
            }

            MOVIMIENTOXML.CrearArchivo(nueva.RutaXml, j1.Nombre, j2.Nombre);

            partida = nueva;
            jugador1 = j1;
            jugador2 = j2;
            tabla = CrearTablaVacia();
            jugadorActual = 1;
            turno = 0;
            enCurso = true;
            IniciarTurno();
        }

        private void IniciarTurno()
        {
            dados = new int[5];
            tirosRestantes = TIRADAS_POR_TURNO;
        }

        private List<BE.FILAPUNTAJE> CrearTablaVacia()
        {
            List<BE.FILAPUNTAJE> nueva = new List<BE.FILAPUNTAJE>();
            foreach (string categoria in GENERALA.CATEGORIAS)
            {
                BE.FILAPUNTAJE fila = new BE.FILAPUNTAJE();
                fila.Categoria = categoria;
                fila.PuntajeJugador1 = BE.FILAPUNTAJE.SIN_ANOTAR;
                fila.PuntajeJugador2 = BE.FILAPUNTAJE.SIN_ANOTAR;
                nueva.Add(fila);
            }
            return nueva;
        }

        // -------------------- Turno --------------------

        public BE.USUARIO JugadorDelTurno()
        {
            if (jugadorActual == 1)
            {
                return jugador1;
            }
            return jugador2;
        }

        // Ya hubo al menos una tirada en este turno.
        private bool YaTiro()
        {
            return tirosRestantes < TIRADAS_POR_TURNO;
        }

        public bool PuedeTirar()
        {
            return enCurso && tirosRestantes > 0;
        }

        // Solo se puede anotar después de la primera tirada del turno.
        public bool PuedeAnotar()
        {
            return enCurso && YaTiro();
        }

        // Los dados se pueden marcar para guardar desde la primera tirada hasta que se anota:
        // antes de tirar no hay ningún dado para conservar. Después del último tiro marcarlos
        // no cambia nada (Tirar ya no se permite), pero no se bloquean.
        public bool PuedeGuardarDados()
        {
            return enCurso && YaTiro();
        }

        public void Tirar(bool[] guardar)
        {
            if (!PuedeTirar())
            {
                throw new Exception("No quedan tiradas en este turno.");
            }

            if (!YaTiro())
            {
                guardar = new bool[5];
            }

            int[] nuevos = GENERALA.Tirar(dados, guardar);
            int numeroTirada = TIRADAS_POR_TURNO - tirosRestantes + 1;

            // Primero el XML: si no se puede escribir, la tirada no cuenta y los dados quedan como estaban.
            MOVIMIENTOXML.RegistrarTirada(partida.RutaXml, turno + 1, JugadorDelTurno().Nombre, numeroTirada, nuevos, guardar);

            dados = nuevos;
            tirosRestantes--;
        }

        // "Servida": se anota en la primera tirada del turno, sin volver a tirar.
        private bool EsTiradaServida()
        {
            return tirosRestantes == TIRADAS_POR_TURNO - 1;
        }

        // -------------------- Anotación --------------------

        // Categorías que el jugador del turno todavía puede anotar.
        public List<string> CategoriasDisponibles()
        {
            bool generalaYaAnotada = PuntajeDe(BuscarFila("Generala"), jugadorActual) != BE.FILAPUNTAJE.SIN_ANOTAR;

            List<string> disponibles = new List<string>();
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                if (PuntajeDe(fila, jugadorActual) != BE.FILAPUNTAJE.SIN_ANOTAR)
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

        // Cuánto vale la categoría con los dados actuales, sin anotarla.
        // La GUI lo usa para avisar antes de anotar (tachada, servida).
        public int CalcularPuntaje(string categoria)
        {
            if (GeneralaDobleSoloTachable(categoria))
            {
                return 0;
            }
            return GENERALA.CalcularPuntaje(categoria, dados, EsTiradaServida());
        }

        // La Generala Doble solo vale si la Generala se anotó con puntos.
        // Si la Generala quedó tachada (0), la Generala Doble solo se puede tachar.
        public bool GeneralaDobleSoloTachable(string categoria)
        {
            return categoria == GENERALA.GENERALA_DOBLE && PuntajeDe(BuscarFila("Generala"), jugadorActual) <= 0;
        }

        // Si se anota ahora, ¿se cobra el doble por tirada servida?
        public bool AplicaBonusServida(string categoria)
        {
            return EsTiradaServida() && GENERALA.EsCombinacion(categoria) && CalcularPuntaje(categoria) > 0;
        }

        // Anota la categoría para el jugador del turno, la registra en el XML y pasa el turno.
        // Si con esto se completó la planilla, cierra la partida (dispara PartidaFinalizada).
        public int Anotar(string categoria)
        {
            if (!PuedeAnotar())
            {
                throw new Exception("Primero hay que tirar los dados.");
            }
            if (!CategoriasDisponibles().Contains(categoria))
            {
                throw new Exception("La categoría \"" + categoria + "\" no se puede anotar.");
            }

            int puntaje = CalcularPuntaje(categoria);

            // Primero el XML: si no se puede escribir, la planilla queda como estaba.
            MOVIMIENTOXML.RegistrarMovimiento(partida.RutaXml, turno + 1, JugadorDelTurno().Nombre, dados, categoria, puntaje);
            turno++;

            BE.FILAPUNTAJE fila = BuscarFila(categoria);
            if (jugadorActual == 1)
            {
                fila.PuntajeJugador1 = puntaje;
            }
            else
            {
                fila.PuntajeJugador2 = puntaje;
            }

            if (jugadorActual == 1)
            {
                jugadorActual = 2;
            }
            else
            {
                jugadorActual = 1;
            }

            if (EstaCompleta())
            {
                Finalizar();
            }
            else
            {
                IniciarTurno();
            }
            return puntaje;
        }

        public int CalcularTotal(int numeroJugador)
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

        // Quién va ganando según los puntos anotados hasta ahora: 1, 2, o 0 si van empatados.
        public int JugadorQueVaGanando()
        {
            int total1 = CalcularTotal(1);
            int total2 = CalcularTotal(2);
            if (total1 > total2)
            {
                return 1;
            }
            if (total2 > total1)
            {
                return 2;
            }
            return 0;
        }

        // Los 2 jugadores anotaron al menos una categoría: recién ahí tiene sentido decir quién va ganando.
        public bool AmbosAnotaron()
        {
            bool anotoJugador1 = false;
            bool anotoJugador2 = false;
            foreach (BE.FILAPUNTAJE fila in tabla)
            {
                if (fila.PuntajeJugador1 != BE.FILAPUNTAJE.SIN_ANOTAR) anotoJugador1 = true;
                if (fila.PuntajeJugador2 != BE.FILAPUNTAJE.SIN_ANOTAR) anotoJugador2 = true;
            }
            return anotoJugador1 && anotoJugador2;
        }

        // La partida termina cuando los 2 jugadores anotaron las 11 categorías.
        private bool EstaCompleta()
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

        private BE.FILAPUNTAJE BuscarFila(string categoria)
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
        private void Finalizar()
        {
            partida.PuntajeJugador1 = CalcularTotal(1);
            partida.PuntajeJugador2 = CalcularTotal(2);

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

            Cerrar(null);
        }

        // Abandona el jugador indicado (puede ser cualquiera de los 2, le toque o no el turno):
        // se guardan los puntajes parciales, pero la partida la gana siempre el otro
        // jugador, sin importar cómo venían los puntos.
        public void Abandonar(BE.USUARIO quienAbandona)
        {
            if (!enCurso)
            {
                return;
            }

            BE.USUARIO ganador;
            if (quienAbandona.ID == jugador1.ID)
            {
                ganador = jugador2;
            }
            else if (quienAbandona.ID == jugador2.ID)
            {
                ganador = jugador1;
            }
            else
            {
                throw new Exception("Solo puede abandonar uno de los 2 jugadores de la partida.");
            }

            partida.PuntajeJugador1 = CalcularTotal(1);
            partida.PuntajeJugador2 = CalcularTotal(2);
            partida.IdGanador = ganador.ID;

            Cerrar(quienAbandona);
        }

        // quienAbandona = null cuando la partida terminó completa.
        private void Cerrar(BE.USUARIO quienAbandona)
        {
            // La partida termina en pantalla aunque falle la base: no se puede seguir jugando
            // con la planilla completa. Si falla, se avisa con la excepción.
            enCurso = false;
            tirosRestantes = 0;

            bool abandonada = quienAbandona != null;
            string descripcion;
            string nombreAbandono;
            if (abandonada)
            {
                descripcion = "Partida abandonada por " + quienAbandona.Nombre;
                nombreAbandono = quienAbandona.Nombre;
            }
            else
            {
                descripcion = "Fin de partida";
                nombreAbandono = "";
            }

            // Resultado + fin de partida en la bitácora, en una sola transacción.
            if (!mapper.Finalizar(partida, descripcion))
            {
                throw new Exception("La partida terminó, pero no se pudo guardar el resultado en la base de datos.");
            }

            // Cierre en el XML: puntajes, ganador ("" si es empate) y quién abandonó.
            string nombreGanador = "";
            if (partida.IdGanador == jugador1.ID)
            {
                nombreGanador = jugador1.Nombre;
            }
            else if (partida.IdGanador == jugador2.ID)
            {
                nombreGanador = jugador2.Nombre;
            }
            MOVIMIENTOXML.RegistrarFin(partida.RutaXml, turno, jugador1.Nombre, partida.PuntajeJugador1,
                jugador2.Nombre, partida.PuntajeJugador2, nombreGanador, nombreAbandono);

            if (PartidaFinalizada != null)
            {
                PartidaFinalizada(partida, abandonada);
            }
        }
    }
}
