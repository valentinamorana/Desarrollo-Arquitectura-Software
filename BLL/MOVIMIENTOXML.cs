using System;
using System.Collections.Generic;

namespace BLL
{
    // Movimientos de una partida en su archivo XML. El archivo lo lee y escribe
    // DAL.MOVIMIENTOXML; acá se arma qué datos lleva cada registro (tirada, anotación, fin).
    public static class MOVIMIENTOXML
    {
        public static string GenerarRuta()
        {
            return DAL.MOVIMIENTOXML.GenerarRuta();
        }

        public static void CrearArchivo(string ruta, string jugador1, string jugador2)
        {
            DAL.MOVIMIENTOXML.CrearArchivo(ruta, Ahora(), jugador1, jugador2);
        }

        // numeroTirada: 1, 2 o 3 dentro del turno. guardados: qué dados se conservaron de la tirada anterior.
        public static void RegistrarTirada(string ruta, int turno, string jugador, int numeroTirada, int[] dados, bool[] guardados)
        {
            DAL.MOVIMIENTOXML.InsertarTirada(ruta, turno, jugador, numeroTirada, DadosATexto(dados), GuardadosATexto(guardados), Ahora());
        }

        public static void RegistrarMovimiento(string ruta, int turno, string jugador, int[] dados, string categoria, int puntaje)
        {
            DAL.MOVIMIENTOXML.InsertarMovimiento(ruta, turno, jugador, DadosATexto(dados), categoria, puntaje, Ahora());
        }

        // ganador: nombre del ganador, o "" si fue empate. abandono: quién abandonó, o "" si se terminó completa.
        public static void RegistrarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono)
        {
            DAL.MOVIMIENTOXML.InsertarFin(ruta, turnos, jugador1, puntajeJugador1, jugador2, puntajeJugador2, ganador, abandono, Ahora());
        }

        public static bool Existe(string ruta)
        {
            return DAL.MOVIMIENTOXML.Existe(ruta);
        }

        // Lee el XML de una partida y devuelve sus movimientos en orden, para consultarlos desde la app.
        public static List<BE.MOVIMIENTO> LeerMovimientos(string ruta)
        {
            return DAL.MOVIMIENTOXML.Listar(ruta);
        }

        private static string Ahora()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // {3, 6, 6, 2, 2} -> "3,6,6,2,2"
        private static string DadosATexto(int[] dados)
        {
            string texto = "";
            for (int i = 0; i < dados.Length; i++)
            {
                if (i > 0)
                {
                    texto = texto + ",";
                }
                texto = texto + dados[i];
            }
            return texto;
        }

        // {true, false, true, false, false} -> "1,3" (posiciones de los dados guardados). "" si no se guardó ninguno.
        private static string GuardadosATexto(bool[] guardados)
        {
            string texto = "";
            for (int i = 0; i < guardados.Length; i++)
            {
                if (guardados[i])
                {
                    if (texto != "")
                    {
                        texto = texto + ",";
                    }
                    texto = texto + (i + 1);
                }
            }
            return texto;
        }
    }
}
