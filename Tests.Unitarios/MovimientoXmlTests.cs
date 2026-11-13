using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class MovimientoXmlTests
    {
        private string carpetaTemporal;
        private DAL.MOVIMIENTOXML archivo;

        [TestInitialize]
        public void Inicializar()
        {
            carpetaTemporal = Path.Combine(Path.GetTempPath(), "GeneralaTests_" + Guid.NewGuid().ToString("N"));
            archivo = new DAL.MOVIMIENTOXML(carpetaTemporal);
        }

        [TestCleanup]
        public void Limpiar()
        {
            if (Directory.Exists(carpetaTemporal))
            {
                Directory.Delete(carpetaTemporal, true);
            }
        }

        private string CrearPartida()
        {
            string ruta = archivo.GenerarRuta();
            archivo.CrearArchivo(ruta, "01/01/2026 10:00:00", "Ana", "Beto");
            return ruta;
        }

        [TestMethod]
        public void GenerarRuta_CarpetaInexistente_CreaLaCarpetaYDevuelveRutaAdentro()
        {
            Assert.IsFalse(Directory.Exists(carpetaTemporal));

            string ruta = archivo.GenerarRuta();

            Assert.IsTrue(Directory.Exists(carpetaTemporal));
            Assert.AreEqual(Path.GetFullPath(carpetaTemporal), Path.GetFullPath(Path.GetDirectoryName(ruta)));
            Assert.AreEqual(".xml", Path.GetExtension(ruta));
        }

        [TestMethod]
        public void CrearArchivo_CreaElArchivo()
        {
            string ruta = CrearPartida();

            Assert.IsTrue(File.Exists(ruta));
            Assert.IsTrue(archivo.Existe(ruta));
            Assert.AreEqual(0, archivo.Listar(ruta).Count);
        }

        [TestMethod]
        public void Listar_TiradaMovimientoYFin_DevuelveLosValoresEnOrden()
        {
            string ruta = CrearPartida();
            archivo.InsertarTirada(ruta, 1, "Ana", 1, "1,2,3,4,5", "1,3", "01/01/2026 10:00:01");
            archivo.InsertarMovimiento(ruta, 1, "Ana", "1,2,3,4,5", "Escalera", 20, "01/01/2026 10:00:02");
            archivo.InsertarFin(ruta, 11, "Ana", 20, "Beto", 20, "", "", "01/01/2026 10:30:00");

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);

            Assert.AreEqual(3, movimientos.Count);

            Assert.AreEqual("Tirada 1", movimientos[0].Accion);
            Assert.AreEqual(1, movimientos[0].Turno);
            Assert.AreEqual("Ana", movimientos[0].Jugador);
            Assert.AreEqual("1,2,3,4,5", movimientos[0].Dados);
            Assert.AreEqual("1,3", movimientos[0].Guardados);
            Assert.AreEqual("", movimientos[0].Categoria);
            Assert.AreEqual("", movimientos[0].Puntaje);
            Assert.AreEqual("01/01/2026 10:00:01", movimientos[0].Fecha);

            Assert.AreEqual("Anotación", movimientos[1].Accion);
            Assert.AreEqual("Ana", movimientos[1].Jugador);
            Assert.AreEqual("1,2,3,4,5", movimientos[1].Dados);
            Assert.AreEqual("", movimientos[1].Guardados);
            Assert.AreEqual("Escalera", movimientos[1].Categoria);
            Assert.AreEqual("20", movimientos[1].Puntaje);

            Assert.AreEqual("Fin de partida", movimientos[2].Accion);
            Assert.AreEqual(11, movimientos[2].Turno);
            Assert.AreEqual("", movimientos[2].Jugador);
            Assert.AreEqual("Empate", movimientos[2].Categoria);
            Assert.AreEqual("Ana 20 - Beto 20", movimientos[2].Puntaje);
        }

        [TestMethod]
        public void Listar_FinConGanador_CategoriaGanoJugador()
        {
            string ruta = CrearPartida();
            archivo.InsertarFin(ruta, 11, "Ana", 150, "Beto", 90, "Ana", "", "01/01/2026 10:30:00");

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);

            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("Fin de partida", movimientos[0].Accion);
            Assert.AreEqual("Ganó Ana", movimientos[0].Categoria);
            Assert.AreEqual("Ana 150 - Beto 90", movimientos[0].Puntaje);
        }

        [TestMethod]
        public void Listar_FinConAbandono_AccionAbandonoYJugadorQueAbandono()
        {
            string ruta = CrearPartida();
            archivo.InsertarFin(ruta, 3, "Ana", 40, "Beto", 10, "Ana", "Beto", "01/01/2026 10:30:00");

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);

            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("Abandono", movimientos[0].Accion);
            Assert.AreEqual("Beto", movimientos[0].Jugador);
            Assert.AreEqual("Ganó Ana", movimientos[0].Categoria);
            Assert.AreEqual(3, movimientos[0].Turno);
        }

        [TestMethod]
        public void QuitarUltimoMovimiento_UltimoEsMovimiento_LoQuita()
        {
            string ruta = CrearPartida();
            archivo.InsertarTirada(ruta, 1, "Ana", 1, "1,1,1,1,1", "", "f");
            archivo.InsertarMovimiento(ruta, 1, "Ana", "1,1,1,1,1", "Generala", 50, "f");

            archivo.QuitarUltimoMovimiento(ruta);

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);
            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("Tirada 1", movimientos[0].Accion);
        }

        [TestMethod]
        public void QuitarUltimoMovimiento_QuitaSoloElUltimo()
        {
            string ruta = CrearPartida();
            archivo.InsertarMovimiento(ruta, 1, "Ana", "1,1,1,1,1", "Unos", 5, "f");
            archivo.InsertarMovimiento(ruta, 2, "Beto", "2,2,2,2,2", "Doses", 10, "f");

            archivo.QuitarUltimoMovimiento(ruta);

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);
            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("Unos", movimientos[0].Categoria);
        }

        [TestMethod]
        public void QuitarUltimoMovimiento_UltimoEsTirada_NoHaceNada()
        {
            string ruta = CrearPartida();
            archivo.InsertarMovimiento(ruta, 1, "Ana", "1,1,1,1,1", "Unos", 5, "f");
            archivo.InsertarTirada(ruta, 2, "Beto", 1, "2,3,4,5,6", "", "f");

            archivo.QuitarUltimoMovimiento(ruta);

            List<BE.MOVIMIENTO> movimientos = archivo.Listar(ruta);
            Assert.AreEqual(2, movimientos.Count);
            Assert.AreEqual("Anotación", movimientos[0].Accion);
            Assert.AreEqual("Tirada 1", movimientos[1].Accion);
        }

        [TestMethod]
        public void QuitarUltimoMovimiento_SinElementos_NoHaceNada()
        {
            string ruta = CrearPartida();

            archivo.QuitarUltimoMovimiento(ruta);

            Assert.AreEqual(0, archivo.Listar(ruta).Count);
        }

        [TestMethod]
        public void Eliminar_ArchivoExistente_LoBorra()
        {
            string ruta = CrearPartida();

            archivo.Eliminar(ruta);

            Assert.IsFalse(File.Exists(ruta));
            Assert.IsFalse(archivo.Existe(ruta));
        }

        [TestMethod]
        public void Eliminar_ArchivoInexistente_NoLanza()
        {
            archivo.Eliminar(Path.Combine(carpetaTemporal, "no_existe.xml"));
            archivo.Eliminar("");
            archivo.Eliminar(null);
        }

        [TestMethod]
        public void Existe_RutaVaciaONula_DevuelveFalse()
        {
            Assert.IsFalse(archivo.Existe(""));
            Assert.IsFalse(archivo.Existe(null));
        }

        [TestMethod]
        public void Resolver_RutaExistente_DevuelveLaMismaRuta()
        {
            string ruta = CrearPartida();

            Assert.AreEqual(ruta, archivo.Resolver(ruta));
        }

        [TestMethod]
        public void Resolver_RutaOriginalInexistente_DevuelveMismoNombreEnLaCarpeta()
        {
            string ruta = CrearPartida();
            string nombre = Path.GetFileName(ruta);
            string rutaVieja = Path.Combine(Path.Combine(carpetaTemporal, "otra_maquina"), nombre);

            string resuelta = archivo.Resolver(rutaVieja);

            Assert.AreEqual(Path.Combine(carpetaTemporal, nombre), resuelta);
        }

        [TestMethod]
        public void Resolver_NoExisteEnNingunLado_DevuelveVacio()
        {
            archivo.GenerarRuta();

            Assert.AreEqual("", archivo.Resolver(Path.Combine(carpetaTemporal, "no_existe.xml")));
            Assert.AreEqual("", archivo.Resolver(""));
            Assert.AreEqual("", archivo.Resolver(null));
        }

        [TestMethod]
        public void Copiar_CopiaElArchivo()
        {
            string ruta = CrearPartida();
            archivo.InsertarMovimiento(ruta, 1, "Ana", "1,1,1,1,1", "Unos", 5, "f");
            string destino = Path.Combine(carpetaTemporal, "copia.xml");

            archivo.Copiar(ruta, destino);

            Assert.IsTrue(File.Exists(ruta));
            Assert.IsTrue(File.Exists(destino));
            List<BE.MOVIMIENTO> movimientos = archivo.Listar(destino);
            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("Unos", movimientos[0].Categoria);
        }

        [TestMethod]
        public void BllRegistrarTirada_EscribeDadosYGuardadosConIndicesDesdeUno()
        {
            BLL.MOVIMIENTOXML movimientoXml = new BLL.MOVIMIENTOXML(archivo);
            string ruta = movimientoXml.GenerarRuta();
            movimientoXml.CrearArchivo(ruta, "Ana", "Beto");

            movimientoXml.RegistrarTirada(ruta, 1, "Ana", 2, new int[] { 1, 2, 3, 4, 5 },
                new bool[] { true, false, true, false, false });
            movimientoXml.RegistrarMovimiento(ruta, 1, "Ana", new int[] { 6, 6, 6, 2, 2 }, "Full", 30);

            List<BE.MOVIMIENTO> movimientos = movimientoXml.LeerMovimientos(ruta);

            Assert.AreEqual(2, movimientos.Count);
            Assert.AreEqual("Tirada 2", movimientos[0].Accion);
            Assert.AreEqual("1,2,3,4,5", movimientos[0].Dados);
            Assert.AreEqual("1,3", movimientos[0].Guardados);
            Assert.AreEqual("6,6,6,2,2", movimientos[1].Dados);
            Assert.AreEqual("Full", movimientos[1].Categoria);
            Assert.AreEqual("30", movimientos[1].Puntaje);
        }

        [TestMethod]
        public void BllRegistrarTirada_SinGuardados_EscribeVacio()
        {
            BLL.MOVIMIENTOXML movimientoXml = new BLL.MOVIMIENTOXML(archivo);
            string ruta = movimientoXml.GenerarRuta();
            movimientoXml.CrearArchivo(ruta, "Ana", "Beto");

            movimientoXml.RegistrarTirada(ruta, 1, "Ana", 1, new int[] { 3, 3, 4, 5, 6 },
                new bool[] { false, false, false, false, false });

            List<BE.MOVIMIENTO> movimientos = movimientoXml.LeerMovimientos(ruta);

            Assert.AreEqual(1, movimientos.Count);
            Assert.AreEqual("", movimientos[0].Guardados);
        }
    }
}
