using System;
using System.Collections.Generic;

namespace BLL
{
    public class MOVIMIENTOXML
    {
        private DAL.IMOVIMIENTOXML archivo;

        public MOVIMIENTOXML() : this(new DAL.MOVIMIENTOXML())
        {
        }

        public MOVIMIENTOXML(DAL.IMOVIMIENTOXML archivo)
        {
            this.archivo = archivo;
        }

        public string GenerarRuta()
        {
            return archivo.GenerarRuta();
        }

        public void CrearArchivo(string ruta, string jugador1, string jugador2)
        {
            archivo.CrearArchivo(ruta, Ahora(), jugador1, jugador2);
        }

        public void RegistrarTirada(string ruta, int turno, string jugador, int numeroTirada, int[] dados, bool[] guardados)
        {
            archivo.InsertarTirada(ruta, turno, jugador, numeroTirada, DadosATexto(dados), GuardadosATexto(guardados), Ahora());
        }

        public void RegistrarMovimiento(string ruta, int turno, string jugador, int[] dados, string categoria, int puntaje)
        {
            archivo.InsertarMovimiento(ruta, turno, jugador, DadosATexto(dados), categoria, puntaje, Ahora());
        }

        public void QuitarUltimoMovimiento(string ruta)
        {
            archivo.QuitarUltimoMovimiento(ruta);
        }

        public void RegistrarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono)
        {
            archivo.InsertarFin(ruta, turnos, jugador1, puntajeJugador1, jugador2, puntajeJugador2, ganador, abandono, Ahora());
        }

        public void Eliminar(string ruta)
        {
            archivo.Eliminar(ruta);
        }

        public bool Existe(string ruta)
        {
            return archivo.Existe(ruta);
        }

        public string Resolver(string ruta)
        {
            return archivo.Resolver(ruta);
        }

        public void Copiar(string origen, string destino)
        {
            archivo.Copiar(origen, destino);
        }

        public List<BE.MOVIMIENTO> LeerMovimientos(string ruta)
        {
            return archivo.Listar(ruta);
        }

        private string Ahora()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private string DadosATexto(int[] dados)
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

        private string GuardadosATexto(bool[] guardados)
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
