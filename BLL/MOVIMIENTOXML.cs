using System;
using System.IO;
using System.Xml;

namespace BLL
{
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

        public static void CrearArchivo(string ruta)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Partida");
            raiz.SetAttribute("fecha", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            doc.AppendChild(raiz);
            doc.Save(ruta);
        }

        public static void RegistrarMovimiento(string ruta, int turno, string jugador, int[] dados, string categoria, int puntaje)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(ruta);

            string dadosTexto = "";
            for (int i = 0; i < dados.Length; i++)
            {
                if (i > 0)
                {
                    dadosTexto = dadosTexto + ",";
                }
                dadosTexto = dadosTexto + dados[i];
            }

            XmlElement movimiento = doc.CreateElement("Movimiento");
            movimiento.SetAttribute("turno", turno.ToString());
            movimiento.SetAttribute("jugador", jugador);
            movimiento.SetAttribute("dados", dadosTexto);
            movimiento.SetAttribute("categoria", categoria);
            movimiento.SetAttribute("puntaje", puntaje.ToString());
            movimiento.SetAttribute("fecha", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            doc.DocumentElement.AppendChild(movimiento);
            doc.Save(ruta);
        }
    }
}
