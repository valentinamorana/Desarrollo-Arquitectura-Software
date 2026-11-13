using System;
using System.Collections.Generic;

namespace Tests.Unitarios
{
    public class FakeMapperPartida : DAL.IMP_PARTIDA
    {
        public int IdAInsertar = 1;
        public bool LanzarAlInsertar = false;
        public bool ResultadoFinalizar = true;
        public bool LanzarAlFinalizar = false;

        public int Insertadas = 0;
        public int Finalizadas = 0;
        public int IntentosFinalizar = 0;
        public string UltimaDescripcion = "";
        public int UltimoGanador = -1;
        public int UltimoPuntaje1 = -1;
        public int UltimoPuntaje2 = -1;

        public List<BE.PARTIDA> PartidasParaListar = new List<BE.PARTIDA>();

        public int Insertar(BE.PARTIDA partida)
        {
            if (LanzarAlInsertar)
            {
                throw new Exception("Falla simulada de la base al insertar");
            }
            Insertadas++;
            return IdAInsertar;
        }

        public bool Finalizar(BE.PARTIDA partida, string descripcion)
        {
            IntentosFinalizar++;
            if (LanzarAlFinalizar)
            {
                throw new Exception("Falla simulada de la base al finalizar");
            }
            if (!ResultadoFinalizar)
            {
                return false;
            }
            Finalizadas++;
            UltimaDescripcion = descripcion;
            UltimoGanador = partida.IdGanador;
            UltimoPuntaje1 = partida.PuntajeJugador1;
            UltimoPuntaje2 = partida.PuntajeJugador2;
            return true;
        }

        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            return PartidasParaListar;
        }
    }

    public class FakeMapperUsuario : DAL.IMP_USUARIO
    {
        public int IdAInsertar = 1;
        public bool LoginCorrecto = true;
        public int IdDelLogin = 1;
        public int Insertados = 0;
        public int BusquedasPorId = 0;
        public List<BE.USUARIO> Usuarios = new List<BE.USUARIO>();

        public int Insertar(BE.USUARIO usuario)
        {
            Insertados++;
            return IdAInsertar;
        }

        public bool Login(BE.USUARIO usuario)
        {
            if (!LoginCorrecto)
            {
                return false;
            }
            usuario.ID = IdDelLogin;
            return true;
        }

        public BE.USUARIO BuscarPorId(int id)
        {
            BusquedasPorId++;
            foreach (BE.USUARIO usuario in Usuarios)
            {
                if (usuario.ID == id)
                {
                    return usuario;
                }
            }
            return null;
        }
    }

    public class FakeArchivoMovimientos : DAL.IMOVIMIENTOXML
    {
        public bool LanzarAlCrear = false;
        public bool LanzarAlInsertarFin = false;
        public bool LanzarAlEliminar = false;
        public List<string> Creados = new List<string>();
        public List<string> Eliminados = new List<string>();
        public List<string> Elementos = new List<string>();

        public string GenerarRuta()
        {
            return "partida_prueba.xml";
        }

        public void CrearArchivo(string ruta, string fecha, string jugador1, string jugador2)
        {
            if (LanzarAlCrear)
            {
                throw new Exception("Falla simulada al crear el XML");
            }
            Creados.Add(ruta);
        }

        public void InsertarTirada(string ruta, int turno, string jugador, int numeroTirada, string dados, string guardados, string fecha)
        {
            Elementos.Add("Tirada|" + turno + "|" + jugador + "|" + numeroTirada + "|" + dados + "|" + guardados);
        }

        public void InsertarMovimiento(string ruta, int turno, string jugador, string dados, string categoria, int puntaje, string fecha)
        {
            Elementos.Add("Movimiento|" + turno + "|" + jugador + "|" + categoria + "|" + puntaje);
        }

        public void QuitarUltimoMovimiento(string ruta)
        {
            if (Elementos.Count > 0 && Elementos[Elementos.Count - 1].StartsWith("Movimiento|"))
            {
                Elementos.RemoveAt(Elementos.Count - 1);
            }
        }

        public void InsertarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono, string fecha)
        {
            if (LanzarAlInsertarFin)
            {
                throw new Exception("Falla simulada al escribir el fin en el XML");
            }
            Elementos.Add("Fin|" + turnos + "|" + puntajeJugador1 + "|" + puntajeJugador2 + "|" + ganador + "|" + abandono);
        }

        public void Eliminar(string ruta)
        {
            if (LanzarAlEliminar)
            {
                throw new Exception("Falla simulada al eliminar el XML");
            }
            Eliminados.Add(ruta);
        }

        public bool Existe(string ruta)
        {
            return Creados.Contains(ruta) && !Eliminados.Contains(ruta);
        }

        public string Resolver(string ruta)
        {
            if (Existe(ruta))
            {
                return ruta;
            }
            return "";
        }

        public void Copiar(string origen, string destino)
        {
        }

        public List<BE.MOVIMIENTO> Listar(string ruta)
        {
            return new List<BE.MOVIMIENTO>();
        }

        public int Contar(string tipo)
        {
            int cantidad = 0;
            foreach (string elemento in Elementos)
            {
                if (elemento.StartsWith(tipo + "|"))
                {
                    cantidad++;
                }
            }
            return cantidad;
        }
    }

    public class FakeDado : BLL.IDADO
    {
        private Queue<int> valores = new Queue<int>();

        public void Cargar(params int[] nuevos)
        {
            foreach (int valor in nuevos)
            {
                valores.Enqueue(valor);
            }
        }

        public int Tirar()
        {
            if (valores.Count == 0)
            {
                return 1;
            }
            return valores.Dequeue();
        }
    }

    public class FakeMapperLog : DAL.IMP_LOG
    {
        public int ResultadoInsertar = 1;
        public List<BE.LOG> Insertados = new List<BE.LOG>();
        public List<BE.LOG> RegistrosParaListar = new List<BE.LOG>();

        public int Insertar(BE.LOG log)
        {
            if (ResultadoInsertar != -1)
            {
                Insertados.Add(log);
            }
            return ResultadoInsertar;
        }

        public List<BE.LOG> Listar()
        {
            return RegistrosParaListar;
        }
    }

    public class FakeBackupRestore : DAL.IBACKUPRESTORE
    {
        public string Carpeta = @"C:\Backups";
        public string ErrorBackup = "";
        public string ErrorRestore = "";
        public bool Restaurada = true;
        public List<string> Rutas = new List<string>();

        public string CarpetaBackupPorDefecto()
        {
            return Carpeta;
        }

        public string HacerBackup(string ruta)
        {
            Rutas.Add(ruta);
            return ErrorBackup;
        }

        public string HacerRestore(string ruta, out bool restaurada)
        {
            Rutas.Add(ruta);
            restaurada = Restaurada;
            return ErrorRestore;
        }
    }
}
