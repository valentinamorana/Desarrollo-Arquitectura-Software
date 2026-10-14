using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BLL
{
    // Archivo XML de movimientos de una partida. Cada partida tiene su archivo, con:
    //   <Tirada>     cada tirada de dados (qué salió y qué dados se guardaron)
    //   <Movimiento> cada anotación de una categoría
    //   <Fin>        el cierre: puntajes, ganador y si fue abandono
    public static class MOVIMIENTOXML
    {
        public static string GenerarRuta()
        {
            string carpeta = AppDomain.CurrentDomain.BaseDirectory + "Partidas\\";
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            return carpeta + "Partida_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ".xml";
        }

        public static void CrearArchivo(string ruta, string jugador1, string jugador2)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Partida");
            raiz.SetAttribute("fecha", Ahora());
            raiz.SetAttribute("jugador1", jugador1);
            raiz.SetAttribute("jugador2", jugador2);
            doc.AppendChild(raiz);
            doc.Save(ruta);
        }

        // numeroTirada: 1, 2 o 3 dentro del turno. guardados: qué dados se conservaron de la tirada anterior.
        public static void RegistrarTirada(string ruta, int turno, string jugador, int numeroTirada, int[] dados, bool[] guardados)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlElement tirada = doc.CreateElement("Tirada");
            tirada.SetAttribute("turno", turno.ToString());
            tirada.SetAttribute("jugador", jugador);
            tirada.SetAttribute("numero", numeroTirada.ToString());
            tirada.SetAttribute("dados", DadosATexto(dados));
            tirada.SetAttribute("guardados", GuardadosATexto(guardados));
            tirada.SetAttribute("fecha", Ahora());

            doc.DocumentElement.AppendChild(tirada);
            doc.Save(ruta);
        }

        public static void RegistrarMovimiento(string ruta, int turno, string jugador, int[] dados, string categoria, int puntaje)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlElement movimiento = doc.CreateElement("Movimiento");
            movimiento.SetAttribute("turno", turno.ToString());
            movimiento.SetAttribute("jugador", jugador);
            movimiento.SetAttribute("dados", DadosATexto(dados));
            movimiento.SetAttribute("categoria", categoria);
            movimiento.SetAttribute("puntaje", puntaje.ToString());
            movimiento.SetAttribute("fecha", Ahora());

            doc.DocumentElement.AppendChild(movimiento);
            doc.Save(ruta);
        }

        // ganador: nombre del ganador, o "" si fue empate. abandono: quién abandonó, o "" si se terminó completa.
        public static void RegistrarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlElement fin = doc.CreateElement("Fin");
            fin.SetAttribute("turnos", turnos.ToString());
            fin.SetAttribute("jugador1", jugador1);
            fin.SetAttribute("puntajeJugador1", puntajeJugador1.ToString());
            fin.SetAttribute("jugador2", jugador2);
            fin.SetAttribute("puntajeJugador2", puntajeJugador2.ToString());
            fin.SetAttribute("ganador", ganador);
            if (abandono == "")
            {
                fin.SetAttribute("abandonada", "no");
            }
            else
            {
                fin.SetAttribute("abandonada", "si");
            }
            fin.SetAttribute("abandono", abandono);
            fin.SetAttribute("fecha", Ahora());

            doc.DocumentElement.AppendChild(fin);
            doc.Save(ruta);
        }

        public static bool Existe(string ruta)
        {
            return ruta != null && ruta != "" && File.Exists(ruta);
        }

        // Lee el XML de una partida y devuelve sus movimientos en orden, para consultarlos desde la app.
        // Los XML de versiones anteriores (solo con <Movimiento>) también se leen.
        public static List<BE.MOVIMIENTO> LeerMovimientos(string ruta)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            List<BE.MOVIMIENTO> movimientos = new List<BE.MOVIMIENTO>();
            foreach (XmlNode nodo in doc.DocumentElement.ChildNodes)
            {
                XmlElement elemento = nodo as XmlElement;
                if (elemento == null)
                {
                    continue;
                }

                BE.MOVIMIENTO m = new BE.MOVIMIENTO();
                m.Fecha = elemento.GetAttribute("fecha");

                if (elemento.Name == "Tirada")
                {
                    m.Turno = int.Parse(elemento.GetAttribute("turno"));
                    m.Jugador = elemento.GetAttribute("jugador");
                    m.Accion = "Tirada " + elemento.GetAttribute("numero");
                    m.Dados = elemento.GetAttribute("dados");
                    m.Guardados = elemento.GetAttribute("guardados");
                    m.Categoria = "";
                    m.Puntaje = "";
                }
                else if (elemento.Name == "Movimiento")
                {
                    m.Turno = int.Parse(elemento.GetAttribute("turno"));
                    m.Jugador = elemento.GetAttribute("jugador");
                    m.Accion = "Anotación";
                    m.Dados = elemento.GetAttribute("dados");
                    m.Guardados = "";
                    m.Categoria = elemento.GetAttribute("categoria");
                    m.Puntaje = elemento.GetAttribute("puntaje");
                }
                else if (elemento.Name == "Fin")
                {
                    m.Turno = int.Parse(elemento.GetAttribute("turnos"));
                    m.Dados = "";
                    m.Guardados = "";
                    m.Puntaje = elemento.GetAttribute("jugador1") + " " + elemento.GetAttribute("puntajeJugador1") + " - " +
                        elemento.GetAttribute("jugador2") + " " + elemento.GetAttribute("puntajeJugador2");

                    if (elemento.GetAttribute("abandonada") == "si")
                    {
                        m.Jugador = elemento.GetAttribute("abandono");
                        m.Accion = "Abandono";
                    }
                    else
                    {
                        m.Jugador = "";
                        m.Accion = "Fin de partida";
                    }

                    if (elemento.GetAttribute("ganador") == "")
                    {
                        m.Categoria = "Empate";
                    }
                    else
                    {
                        m.Categoria = "Ganó " + elemento.GetAttribute("ganador");
                    }
                }
                else
                {
                    continue;
                }

                movimientos.Add(m);
            }
            return movimientos;
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
