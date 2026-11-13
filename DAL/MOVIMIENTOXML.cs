using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DAL
{
    public class MOVIMIENTOXML : IMOVIMIENTOXML
    {
        private string carpeta;

        public MOVIMIENTOXML() : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Partidas"))
        {
        }

        public MOVIMIENTOXML(string carpeta)
        {
            this.carpeta = carpeta;
        }

        public string GenerarRuta()
        {
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            return Path.Combine(carpeta, "Partida_" + DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + ".xml");
        }

        public void CrearArchivo(string ruta, string fecha, string jugador1, string jugador2)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Partida");
            raiz.SetAttribute("fecha", fecha);
            raiz.SetAttribute("jugador1", jugador1);
            raiz.SetAttribute("jugador2", jugador2);
            doc.AppendChild(raiz);
            doc.Save(ruta);
        }

        public void InsertarTirada(string ruta, int turno, string jugador, int numeroTirada, string dados, string guardados, string fecha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlElement tirada = doc.CreateElement("Tirada");
            tirada.SetAttribute("turno", turno.ToString());
            tirada.SetAttribute("jugador", jugador);
            tirada.SetAttribute("numero", numeroTirada.ToString());
            tirada.SetAttribute("dados", dados);
            tirada.SetAttribute("guardados", guardados);
            tirada.SetAttribute("fecha", fecha);

            doc.DocumentElement.AppendChild(tirada);
            doc.Save(ruta);
        }

        public void InsertarMovimiento(string ruta, int turno, string jugador, string dados, string categoria, int puntaje, string fecha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlElement movimiento = doc.CreateElement("Movimiento");
            movimiento.SetAttribute("turno", turno.ToString());
            movimiento.SetAttribute("jugador", jugador);
            movimiento.SetAttribute("dados", dados);
            movimiento.SetAttribute("categoria", categoria);
            movimiento.SetAttribute("puntaje", puntaje.ToString());
            movimiento.SetAttribute("fecha", fecha);

            doc.DocumentElement.AppendChild(movimiento);
            doc.Save(ruta);
        }

        public void QuitarUltimoMovimiento(string ruta)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            XmlNode ultimo = doc.DocumentElement.LastChild;
            while (ultimo != null && !(ultimo is XmlElement))
            {
                ultimo = ultimo.PreviousSibling;
            }

            if (ultimo != null && ultimo.Name == "Movimiento")
            {
                doc.DocumentElement.RemoveChild(ultimo);
                doc.Save(ruta);
            }
        }

        public void InsertarFin(string ruta, int turnos, string jugador1, int puntajeJugador1,
            string jugador2, int puntajeJugador2, string ganador, string abandono, string fecha)
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
            fin.SetAttribute("fecha", fecha);

            doc.DocumentElement.AppendChild(fin);
            doc.Save(ruta);
        }

        public void Eliminar(string ruta)
        {
            if (Existe(ruta))
            {
                File.Delete(ruta);
            }
        }

        public bool Existe(string ruta)
        {
            return ruta != null && ruta != "" && File.Exists(ruta);
        }

        public string Resolver(string ruta)
        {
            if (Existe(ruta))
            {
                return ruta;
            }

            string nombre = Path.GetFileName(ruta ?? "");
            if (nombre == "")
            {
                return "";
            }

            string alternativa = Path.Combine(carpeta, nombre);
            if (Existe(alternativa))
            {
                return alternativa;
            }
            return "";
        }

        public void Copiar(string origen, string destino)
        {
            File.Copy(origen, destino, true);
        }

        public List<BE.MOVIMIENTO> Listar(string ruta)
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
    }
}
