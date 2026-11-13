using System;
using System.Collections.Generic;

namespace BLL
{
    public class PARTIDA
    {
        public const int TIRADAS_POR_TURNO = 3;

        private DAL.IMP_PARTIDA mapper;
        private MOVIMIENTOXML movimientos;
        private IDADO dado;

        private BE.PARTIDA partida;
        private BE.USUARIO jugador1;
        private BE.USUARIO jugador2;

        private List<BE.FILAPUNTAJE> tabla;

        private int jugadorActual;
        private int tirosRestantes;
        private int[] dados;
        private int turno;
        private bool enCurso = false;

        public event delPartidaFinalizada PartidaFinalizada;

        public PARTIDA() : this(new DAL.MP_PARTIDA(), new DAL.MOVIMIENTOXML(), new DADO())
        {
        }

        public PARTIDA(DAL.IMP_PARTIDA mapper, DAL.IMOVIMIENTOXML archivoMovimientos, IDADO dado)
        {
            this.mapper = mapper;
            this.movimientos = new MOVIMIENTOXML(archivoMovimientos);
            this.dado = dado;
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

        public int TurnoDelJugador
        {
            get
            {
                int anotadas = 0;
                foreach (BE.FILAPUNTAJE fila in tabla)
                {
                    if (PuntajeDe(fila, jugadorActual) != BE.FILAPUNTAJE.SIN_ANOTAR)
                    {
                        anotadas++;
                    }
                }
                return Math.Min(anotadas + 1, GENERALA.CATEGORIAS.Length);
            }
        }

        public int[] Dados
        {
            get { return dados; }
        }

        public List<BE.FILAPUNTAJE> Tabla
        {
            get { return tabla; }
        }

        public void Iniciar(BE.USUARIO j1, BE.USUARIO j2)
        {
            if (enCurso)
            {
                throw new Exception("Ya hay una partida en curso.");
            }
            if (j1.ID == j2.ID)
            {
                throw new Exception("El Jugador 2 tiene que ser un usuario distinto al Jugador 1.");
            }

            string ruta = movimientos.GenerarRuta();
            movimientos.CrearArchivo(ruta, j1.Nombre, j2.Nombre);

            BE.PARTIDA nueva = new BE.PARTIDA();
            nueva.IdJugador1 = j1.ID;
            nueva.IdJugador2 = j2.ID;
            nueva.RutaXml = ruta;

            try
            {
                nueva.ID = mapper.Insertar(nueva);
            }
            catch
            {
                EliminarXmlSinFallar(ruta);
                throw;
            }

            if (nueva.ID == -1)
            {
                EliminarXmlSinFallar(ruta);
                throw new Exception("No se pudo iniciar la partida en la base de datos. " +
                    "Si se restauró un backup, puede que alguno de los jugadores ya no exista: volvé a ingresar.");
            }

            partida = nueva;
            jugador1 = j1;
            jugador2 = j2;
            tabla = CrearTablaVacia();
            jugadorActual = 1;
            turno = 0;
            enCurso = true;
            IniciarTurno();
        }

        private void EliminarXmlSinFallar(string ruta)
        {
            try
            {
                movimientos.Eliminar(ruta);
            }
            catch (Exception)
            {
            }
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

        public BE.USUARIO JugadorDelTurno()
        {
            if (jugadorActual == 1)
            {
                return jugador1;
            }
            return jugador2;
        }

        private bool YaTiro()
        {
            return tirosRestantes < TIRADAS_POR_TURNO;
        }

        public bool PuedeTirar()
        {
            return enCurso && tirosRestantes > 0;
        }

        public bool PuedeAnotar()
        {
            return enCurso && YaTiro();
        }

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

            int[] nuevos = GENERALA.Tirar(dados, guardar, dado);
            int numeroTirada = TIRADAS_POR_TURNO - tirosRestantes + 1;

            movimientos.RegistrarTirada(partida.RutaXml, turno + 1, JugadorDelTurno().Nombre, numeroTirada, nuevos, guardar);

            dados = nuevos;
            tirosRestantes--;
        }

        private bool EsTiradaServida()
        {
            return tirosRestantes == TIRADAS_POR_TURNO - 1;
        }

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

                if (fila.Categoria == GENERALA.GENERALA_DOBLE && !generalaYaAnotada)
                {
                    continue;
                }

                disponibles.Add(fila.Categoria);
            }
            return disponibles;
        }

        public int CalcularPuntaje(string categoria)
        {
            if (GeneralaDobleSoloTachable(categoria))
            {
                return 0;
            }
            return GENERALA.CalcularPuntaje(categoria, dados, EsTiradaServida());
        }

        public bool GeneralaDobleSoloTachable(string categoria)
        {
            return categoria == GENERALA.GENERALA_DOBLE && PuntajeDe(BuscarFila("Generala"), jugadorActual) <= 0;
        }

        public bool AplicaBonusServida(string categoria)
        {
            return EsTiradaServida() && GENERALA.EsCombinacion(categoria) && CalcularPuntaje(categoria) > 0;
        }

        public int Anotar(string categoria)
        {
            return RegistrarAnotacion(categoria, false);
        }

        public int Tachar(string categoria)
        {
            return RegistrarAnotacion(categoria, true);
        }

        private int RegistrarAnotacion(string categoria, bool tachar)
        {
            if (!PuedeAnotar())
            {
                throw new Exception("Primero hay que tirar los dados.");
            }
            if (!CategoriasDisponibles().Contains(categoria))
            {
                throw new Exception("La categoría \"" + categoria + "\" no se puede anotar.");
            }

            int puntaje = 0;
            if (!tachar)
            {
                puntaje = CalcularPuntaje(categoria);
            }

            movimientos.RegistrarMovimiento(partida.RutaXml, turno + 1, JugadorDelTurno().Nombre, dados, categoria, puntaje);
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

            int jugadorQueAnoto = jugadorActual;
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
                try
                {
                    Finalizar();
                }
                catch
                {
                    if (enCurso)
                    {
                        if (jugadorQueAnoto == 1)
                        {
                            fila.PuntajeJugador1 = BE.FILAPUNTAJE.SIN_ANOTAR;
                        }
                        else
                        {
                            fila.PuntajeJugador2 = BE.FILAPUNTAJE.SIN_ANOTAR;
                        }
                        jugadorActual = jugadorQueAnoto;
                        turno--;
                        try
                        {
                            movimientos.QuitarUltimoMovimiento(partida.RutaXml);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    throw;
                }
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

        public string MensajeFinal(bool abandonada)
        {
            string mensaje;
            if (partida.IdGanador == jugador1.ID)
            {
                mensaje = jugador1.Nombre + " ganó la partida!";
            }
            else if (partida.IdGanador == jugador2.ID)
            {
                mensaje = jugador2.Nombre + " ganó la partida!";
            }
            else
            {
                mensaje = "La partida terminó empatada!";
            }

            if (abandonada)
            {
                string quienAbandono;
                if (partida.IdGanador == jugador1.ID)
                {
                    quienAbandono = jugador2.Nombre;
                }
                else
                {
                    quienAbandono = jugador1.Nombre;
                }
                mensaje = quienAbandono + " abandonó la partida. " + mensaje;
            }
            return mensaje;
        }

        public string AvisoAntesDeAnotar(string categoria)
        {
            if (GeneralaDobleSoloTachable(categoria))
            {
                return "Como tu Generala quedó tachada, la Generala Doble no suma: se anota 0 y queda tachada.";
            }
            if (CalcularPuntaje(categoria) == 0)
            {
                return "Los dados no alcanzan para puntuar en \"" + categoria + "\": se anota 0 y la categoría queda tachada.";
            }
            return "";
        }

        public string MensajeVaGanando()
        {
            if (!EnCurso || !AmbosAnotaron())
            {
                return "";
            }

            int lider = JugadorQueVaGanando();
            int diferencia = Math.Abs(CalcularTotal(1) - CalcularTotal(2));
            if (lider == 1)
            {
                return jugador1.Nombre + " va ganando por " + diferencia;
            }
            if (lider == 2)
            {
                return jugador2.Nombre + " va ganando por " + diferencia;
            }
            return "Van empatados";
        }

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

        private void Cerrar(BE.USUARIO quienAbandona)
        {
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

            if (!mapper.Finalizar(partida, descripcion))
            {
                throw new Exception("La partida no se pudo guardar en la base de datos. Intentá de nuevo.");
            }

            enCurso = false;
            tirosRestantes = 0;

            string nombreGanador = "";
            if (partida.IdGanador == jugador1.ID)
            {
                nombreGanador = jugador1.Nombre;
            }
            else if (partida.IdGanador == jugador2.ID)
            {
                nombreGanador = jugador2.Nombre;
            }
            try
            {
                movimientos.RegistrarFin(partida.RutaXml, turno, jugador1.Nombre, partida.PuntajeJugador1,
                    jugador2.Nombre, partida.PuntajeJugador2, nombreGanador, nombreAbandono);
            }
            finally
            {
                AvisarFinalizacion(abandonada);
            }
        }

        private void AvisarFinalizacion(bool abandonada)
        {
            if (PartidaFinalizada != null)
            {
                PartidaFinalizada(partida, abandonada);
            }
        }
    }
}
