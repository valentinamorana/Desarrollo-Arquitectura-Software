using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class EstadisticaTests
    {
        private BE.USUARIO CrearUsuario(int id, string nombre)
        {
            BE.USUARIO usuario = new BE.USUARIO();
            usuario.ID = id;
            usuario.Nombre = nombre;
            return usuario;
        }

        private BE.PARTIDA CrearPartida(int idJugador1, string nombreJugador1, int puntaje1,
            int idJugador2, string nombreJugador2, int puntaje2, int idGanador, DateTime inicio, int minutos, string ruta)
        {
            BE.PARTIDA partida = new BE.PARTIDA();
            partida.IdJugador1 = idJugador1;
            partida.NombreJugador1 = nombreJugador1;
            partida.PuntajeJugador1 = puntaje1;
            partida.IdJugador2 = idJugador2;
            partida.NombreJugador2 = nombreJugador2;
            partida.PuntajeJugador2 = puntaje2;
            partida.IdGanador = idGanador;
            partida.FechaInicio = inicio;
            partida.FechaFin = inicio.AddMinutes(minutos);
            partida.RutaXml = ruta;
            return partida;
        }

        private FakeMapperPartida CrearMapperConPartidas()
        {
            FakeMapperPartida mapper = new FakeMapperPartida();
            mapper.PartidasParaListar.Add(CrearPartida(1, "juan", 200, 2, "ana", 150, 1,
                new DateTime(2026, 5, 1, 10, 0, 0), 30, "p1.xml"));
            mapper.PartidasParaListar.Add(CrearPartida(2, "ana", 180, 1, "juan", 120, 2,
                new DateTime(2026, 5, 2, 11, 0, 0), 20, "p2.xml"));
            mapper.PartidasParaListar.Add(CrearPartida(3, "beto", 100, 1, "juan", 100, 0,
                new DateTime(2026, 5, 3, 12, 0, 0), 15, "p3.xml"));
            mapper.PartidasParaListar.Add(CrearPartida(3, "beto", 90, 1, "juan", 160, 1,
                new DateTime(2026, 5, 4, 13, 0, 0), 25, "p4.xml"));
            return mapper;
        }

        private BE.FILAHISTORIAL CrearFila(string resultado)
        {
            BE.FILAHISTORIAL fila = new BE.FILAHISTORIAL();
            fila.Resultado = resultado;
            return fila;
        }

        private List<BE.FILAHISTORIAL> CrearHistorial()
        {
            List<BE.FILAHISTORIAL> historial = new List<BE.FILAHISTORIAL>();
            historial.Add(CrearFila("Ganada"));
            historial.Add(CrearFila("Perdida"));
            historial.Add(CrearFila("Empate"));
            historial.Add(CrearFila("Ganada"));
            return historial;
        }

        [TestMethod]
        public void Calcular_CuentaGanadasPerdidasYEmpatadas()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(2, resultado.Ganadas);
            Assert.AreEqual(1, resultado.Perdidas);
            Assert.AreEqual(1, resultado.Empatadas);
        }

        [TestMethod]
        public void Calcular_PromedioVictorias_EsPorcentaje()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(50.0, resultado.PromedioVictorias, 0.0001);
        }

        [TestMethod]
        public void Calcular_PromedioVictorias_UnaDeTres()
        {
            FakeMapperPartida mapper = new FakeMapperPartida();
            mapper.PartidasParaListar.Add(CrearPartida(1, "juan", 10, 2, "ana", 5, 1, new DateTime(2026, 5, 1), 10, ""));
            mapper.PartidasParaListar.Add(CrearPartida(1, "juan", 5, 2, "ana", 10, 2, new DateTime(2026, 5, 2), 10, ""));
            mapper.PartidasParaListar.Add(CrearPartida(1, "juan", 5, 2, "ana", 5, 0, new DateTime(2026, 5, 3), 10, ""));
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(mapper, new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(100.0 / 3.0, resultado.PromedioVictorias, 0.0001);
        }

        [TestMethod]
        public void Calcular_SinPartidas_TodoEnCero()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(new FakeMapperPartida(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(0, resultado.Ganadas);
            Assert.AreEqual(0, resultado.Perdidas);
            Assert.AreEqual(0, resultado.Empatadas);
            Assert.AreEqual(0.0, resultado.PromedioVictorias);
            Assert.AreEqual(TimeSpan.Zero, resultado.TiempoTotal);
            Assert.IsNotNull(resultado.Historial);
            Assert.AreEqual(0, resultado.Historial.Count);
        }

        [TestMethod]
        public void Calcular_TiempoTotal_SumaLasDuraciones()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(TimeSpan.FromMinutes(90), resultado.TiempoTotal);
        }

        [TestMethod]
        public void Calcular_UsuarioJugador1_RivalYPuntajesDelJugador2()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));
            BE.FILAHISTORIAL fila = resultado.Historial[0];

            Assert.AreEqual("ana", fila.Rival);
            Assert.AreEqual(200, fila.PuntajePropio);
            Assert.AreEqual(150, fila.PuntajeRival);
            Assert.AreEqual("Ganada", fila.Resultado);
            Assert.AreEqual("p1.xml", fila.RutaXml);
            Assert.AreEqual(new DateTime(2026, 5, 1, 10, 0, 0).ToString("dd/MM/yyyy HH:mm"), fila.Fecha);
        }

        [TestMethod]
        public void Calcular_UsuarioJugador2_RivalYPuntajesInvertidos()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));
            BE.FILAHISTORIAL fila = resultado.Historial[1];

            Assert.AreEqual("ana", fila.Rival);
            Assert.AreEqual(120, fila.PuntajePropio);
            Assert.AreEqual(180, fila.PuntajeRival);
            Assert.AreEqual("Perdida", fila.Resultado);
            Assert.AreEqual("p2.xml", fila.RutaXml);
        }

        [TestMethod]
        public void Calcular_Historial_ResultadosEnOrden()
        {
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(4, resultado.Historial.Count);
            Assert.AreEqual("Ganada", resultado.Historial[0].Resultado);
            Assert.AreEqual("Perdida", resultado.Historial[1].Resultado);
            Assert.AreEqual("Empate", resultado.Historial[2].Resultado);
            Assert.AreEqual("beto", resultado.Historial[2].Rival);
            Assert.AreEqual("Ganada", resultado.Historial[3].Resultado);
            Assert.AreEqual(160, resultado.Historial[3].PuntajePropio);
            Assert.AreEqual(90, resultado.Historial[3].PuntajeRival);
        }

        [TestMethod]
        public void Calcular_RivalSinNombre_MuestraGuion()
        {
            FakeMapperPartida mapper = new FakeMapperPartida();
            mapper.PartidasParaListar.Add(CrearPartida(1, "juan", 10, 0, "", 5, 1, new DateTime(2026, 5, 1), 10, ""));
            mapper.PartidasParaListar.Add(CrearPartida(0, null, 10, 1, "juan", 5, 0, new DateTime(2026, 5, 2), 10, ""));
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(mapper, new FakeMapperUsuario());

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual("-", resultado.Historial[0].Rival);
            Assert.AreEqual("-", resultado.Historial[1].Rival);
        }

        [TestMethod]
        public void Calcular_SinNombreEnLaConsulta_BuscaAlRivalPorId()
        {
            FakeMapperPartida mapper = new FakeMapperPartida();
            mapper.PartidasParaListar.Add(CrearPartida(1, "", 10, 2, "", 5, 1, new DateTime(2026, 5, 1), 10, ""));
            FakeMapperUsuario mapperUsuario = new FakeMapperUsuario();
            mapperUsuario.Usuarios.Add(CrearUsuario(2, "pedro"));
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(mapper, mapperUsuario);

            BE.ESTADISTICA resultado = estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual("pedro", resultado.Historial[0].Rival);
            Assert.AreEqual(1, mapperUsuario.BusquedasPorId);
        }

        [TestMethod]
        public void Calcular_ConNombreEnLaConsulta_NoBuscaAlRival()
        {
            FakeMapperUsuario mapperUsuario = new FakeMapperUsuario();
            BLL.ESTADISTICA estadistica = new BLL.ESTADISTICA(CrearMapperConPartidas(), mapperUsuario);

            estadistica.Calcular(CrearUsuario(1, "juan"));

            Assert.AreEqual(0, mapperUsuario.BusquedasPorId);
        }

        [TestMethod]
        public void Filtrar_Todas_DevuelveTodo()
        {
            List<BE.FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(CrearHistorial(), BLL.ESTADISTICA.FILTRO_RESULTADO.Todas);

            Assert.AreEqual(4, visibles.Count);
        }

        [TestMethod]
        public void Filtrar_Ganadas_DevuelveSoloGanadas()
        {
            List<BE.FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(CrearHistorial(), BLL.ESTADISTICA.FILTRO_RESULTADO.Ganadas);

            Assert.AreEqual(2, visibles.Count);
            foreach (BE.FILAHISTORIAL fila in visibles)
            {
                Assert.AreEqual("Ganada", fila.Resultado);
            }
        }

        [TestMethod]
        public void Filtrar_Perdidas_DevuelveSoloPerdidas()
        {
            List<BE.FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(CrearHistorial(), BLL.ESTADISTICA.FILTRO_RESULTADO.Perdidas);

            Assert.AreEqual(1, visibles.Count);
            Assert.AreEqual("Perdida", visibles[0].Resultado);
        }

        [TestMethod]
        public void Filtrar_Empates_DevuelveSoloEmpates()
        {
            List<BE.FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(CrearHistorial(), BLL.ESTADISTICA.FILTRO_RESULTADO.Empates);

            Assert.AreEqual(1, visibles.Count);
            Assert.AreEqual("Empate", visibles[0].Resultado);
        }

        [TestMethod]
        public void Filtrar_HistorialVacio_DevuelveVacio()
        {
            List<BE.FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(new List<BE.FILAHISTORIAL>(), BLL.ESTADISTICA.FILTRO_RESULTADO.Ganadas);

            Assert.AreEqual(0, visibles.Count);
        }
    }
}
