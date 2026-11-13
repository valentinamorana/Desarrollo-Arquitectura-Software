using System;
using System.Collections.Generic;
using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class PartidaTests
    {
        private FakeMapperPartida mapper;
        private FakeArchivoMovimientos xml;
        private FakeDado dado;
        private PARTIDA partida;
        private BE.USUARIO ana;
        private BE.USUARIO beto;

        private int finalizaciones;
        private bool ultimaFueAbandonada;

        [TestInitialize]
        public void Preparar()
        {
            mapper = new FakeMapperPartida();
            xml = new FakeArchivoMovimientos();
            dado = new FakeDado();
            partida = new PARTIDA(mapper, xml, dado);
            partida.PartidaFinalizada += Partida_PartidaFinalizada;

            ana = new BE.USUARIO();
            ana.ID = 1;
            ana.Nombre = "ana";
            beto = new BE.USUARIO();
            beto.ID = 2;
            beto.Nombre = "beto";

            finalizaciones = 0;
            ultimaFueAbandonada = false;
        }

        private void Partida_PartidaFinalizada(BE.PARTIDA p, bool abandonada)
        {
            finalizaciones++;
            ultimaFueAbandonada = abandonada;
        }

        private void JugarTurno(bool tachar)
        {
            partida.Tirar(new bool[5]);
            string categoria = partida.CategoriasDisponibles()[0];
            if (tachar)
            {
                partida.Tachar(categoria);
            }
            else
            {
                partida.Anotar(categoria);
            }
        }

        private int PuntajeEn(string categoria, int numeroJugador)
        {
            foreach (BE.FILAPUNTAJE fila in partida.Tabla)
            {
                if (fila.Categoria == categoria)
                {
                    if (numeroJugador == 1)
                    {
                        return fila.PuntajeJugador1;
                    }
                    return fila.PuntajeJugador2;
                }
            }
            return int.MinValue;
        }

        [TestMethod]
        public void Iniciar_MismoJugador_LanzaExcepcion()
        {
            Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, ana));
            Assert.AreEqual(0, mapper.Insertadas);
            Assert.AreEqual(0, xml.Creados.Count);
        }

        [TestMethod]
        public void Iniciar_Correcto_CreaXmlInsertaYEmpiezaElJugador1()
        {
            partida.Iniciar(ana, beto);

            Assert.IsTrue(partida.EnCurso);
            Assert.AreEqual(1, xml.Creados.Count);
            Assert.AreEqual(1, mapper.Insertadas);
            Assert.AreEqual("ana", partida.JugadorDelTurno().Nombre);
            Assert.AreEqual(PARTIDA.TIRADAS_POR_TURNO, partida.TirosRestantes);
            Assert.AreEqual(1, partida.TurnoDelJugador);
        }

        [TestMethod]
        public void Iniciar_ConUnaPartidaEnCurso_LanzaYNoCreaOtra()
        {
            partida.Iniciar(ana, beto);

            Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, beto));
            Assert.AreEqual(1, mapper.Insertadas);
            Assert.AreEqual(1, xml.Creados.Count);
            Assert.IsTrue(partida.EnCurso);
        }

        [TestMethod]
        public void Iniciar_DespuesDeTerminar_PermiteOtraPartida()
        {
            partida.Iniciar(ana, beto);
            partida.Abandonar(ana);

            partida.Iniciar(ana, beto);

            Assert.IsTrue(partida.EnCurso);
            Assert.AreEqual(2, mapper.Insertadas);
            Assert.AreEqual("ana", partida.JugadorDelTurno().Nombre);
        }

        [TestMethod]
        public void Iniciar_FallaElXml_NoInsertaEnLaBase()
        {
            xml.LanzarAlCrear = true;

            Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, beto));
            Assert.AreEqual(0, mapper.Insertadas);
            Assert.IsFalse(partida.EnCurso);
        }

        [TestMethod]
        public void Iniciar_LaBaseDevuelveError_EliminaElXml()
        {
            mapper.IdAInsertar = -1;

            Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, beto));
            Assert.AreEqual(1, xml.Eliminados.Count);
            Assert.AreEqual(xml.Creados[0], xml.Eliminados[0]);
            Assert.IsFalse(partida.EnCurso);
        }

        [TestMethod]
        public void Iniciar_LaBaseLanzaExcepcion_EliminaElXml()
        {
            mapper.LanzarAlInsertar = true;

            Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, beto));
            Assert.AreEqual(1, xml.Eliminados.Count);
            Assert.IsFalse(partida.EnCurso);
        }

        [TestMethod]
        public void Iniciar_FallaLaBaseYTambienBorrarElXml_LanzaElErrorDeLaBase()
        {
            mapper.LanzarAlInsertar = true;
            xml.LanzarAlEliminar = true;

            Exception error = Assert.ThrowsException<Exception>(() => partida.Iniciar(ana, beto));
            Assert.AreEqual("Falla simulada de la base al insertar", error.Message);
            Assert.IsFalse(partida.EnCurso);
        }

        [TestMethod]
        public void Tirar_DescuentaUnaTiradaYLaRegistraEnElXml()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(1, 2, 3, 4, 5);

            partida.Tirar(new bool[5]);

            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, partida.Dados);
            Assert.AreEqual(2, partida.TirosRestantes);
            Assert.AreEqual(1, xml.Contar("Tirada"));
            Assert.AreEqual("Tirada|1|ana|1|1,2,3,4,5|", xml.Elementos[0]);
        }

        [TestMethod]
        public void Tirar_PrimeraTirada_IgnoraLosDadosGuardados()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(6, 5, 4, 3, 2);

            partida.Tirar(new bool[] { true, true, true, true, true });

            CollectionAssert.AreEqual(new int[] { 6, 5, 4, 3, 2 }, partida.Dados);
        }

        [TestMethod]
        public void Tirar_DadosGuardados_SeConservan()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(1, 2, 3, 4, 5);
            partida.Tirar(new bool[5]);

            dado.Cargar(6, 6, 6);
            partida.Tirar(new bool[] { true, true, false, false, false });

            CollectionAssert.AreEqual(new int[] { 1, 2, 6, 6, 6 }, partida.Dados);
            Assert.AreEqual("Tirada|1|ana|2|1,2,6,6,6|1,2", xml.Elementos[1]);
        }

        [TestMethod]
        public void Tirar_SinTiradasRestantes_LanzaExcepcion()
        {
            partida.Iniciar(ana, beto);
            partida.Tirar(new bool[5]);
            partida.Tirar(new bool[5]);
            partida.Tirar(new bool[5]);

            Assert.IsFalse(partida.PuedeTirar());
            Assert.ThrowsException<Exception>(() => partida.Tirar(new bool[5]));
        }

        [TestMethod]
        public void Anotar_SinTirar_LanzaExcepcion()
        {
            partida.Iniciar(ana, beto);

            Assert.IsFalse(partida.PuedeAnotar());
            Assert.ThrowsException<Exception>(() => partida.Anotar("Unos"));
        }

        [TestMethod]
        public void Anotar_GuardaElPuntajeYPasaElTurnoAlOtroJugador()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(3, 3, 3, 1, 2);
            partida.Tirar(new bool[5]);

            int puntaje = partida.Anotar("Treses");

            Assert.AreEqual(9, puntaje);
            Assert.AreEqual(9, PuntajeEn("Treses", 1));
            Assert.AreEqual("beto", partida.JugadorDelTurno().Nombre);
            Assert.AreEqual(PARTIDA.TIRADAS_POR_TURNO, partida.TirosRestantes);
            Assert.IsFalse(partida.PuedeAnotar());
            Assert.AreEqual("Movimiento|1|ana|Treses|9", xml.Elementos[1]);
        }

        [TestMethod]
        public void Anotar_EscaleraServida_DuplicaElPuntaje()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(1, 2, 3, 4, 5);
            partida.Tirar(new bool[5]);

            Assert.IsTrue(partida.AplicaBonusServida("Escalera"));
            Assert.AreEqual(40, partida.Anotar("Escalera"));
        }

        [TestMethod]
        public void Anotar_EscaleraEnLaSegundaTirada_NoDuplica()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(6, 6, 6, 6, 1);
            partida.Tirar(new bool[5]);
            dado.Cargar(1, 2, 3, 4, 5);
            partida.Tirar(new bool[5]);

            Assert.IsFalse(partida.AplicaBonusServida("Escalera"));
            Assert.AreEqual(20, partida.Anotar("Escalera"));
        }

        [TestMethod]
        public void Tachar_AnotaCeroAunqueLosDadosDenPuntos()
        {
            partida.Iniciar(ana, beto);
            dado.Cargar(6, 6, 6, 6, 6);
            partida.Tirar(new bool[5]);

            Assert.AreEqual(0, partida.Tachar("Generala"));
            Assert.AreEqual(0, PuntajeEn("Generala", 1));
        }

        [TestMethod]
        public void Anotar_CategoriaYaUsada_LanzaExcepcion()
        {
            partida.Iniciar(ana, beto);
            partida.Tirar(new bool[5]);
            partida.Anotar("Unos");
            partida.Tirar(new bool[5]);
            partida.Anotar("Unos");

            partida.Tirar(new bool[5]);
            Assert.IsFalse(partida.CategoriasDisponibles().Contains("Unos"));
            Assert.ThrowsException<Exception>(() => partida.Anotar("Unos"));
        }

        [TestMethod]
        public void GeneralaDoble_SoloSeHabilitaDespuesDeAnotarLaGenerala()
        {
            partida.Iniciar(ana, beto);
            Assert.IsFalse(partida.CategoriasDisponibles().Contains(GENERALA.GENERALA_DOBLE));

            partida.Tirar(new bool[5]);
            partida.Anotar("Generala");
            JugarTurno(false);

            Assert.IsTrue(partida.CategoriasDisponibles().Contains(GENERALA.GENERALA_DOBLE));
        }

        [TestMethod]
        public void GeneralaDoble_ConLaGeneralaTachada_Vale0()
        {
            partida.Iniciar(ana, beto);
            partida.Tirar(new bool[5]);
            partida.Tachar("Generala");
            JugarTurno(false);

            dado.Cargar(4, 4, 4, 4, 4);
            partida.Tirar(new bool[5]);

            Assert.IsTrue(partida.GeneralaDobleSoloTachable(GENERALA.GENERALA_DOBLE));
            Assert.AreEqual(0, partida.CalcularPuntaje(GENERALA.GENERALA_DOBLE));
        }

        [TestMethod]
        public void TurnoDelJugador_AvanzaCadaVezQueAnota()
        {
            partida.Iniciar(ana, beto);
            JugarTurno(false);
            JugarTurno(false);

            Assert.AreEqual("ana", partida.JugadorDelTurno().Nombre);
            Assert.AreEqual(2, partida.TurnoDelJugador);
        }

        [TestMethod]
        public void PartidaCompleta_GanaQuienSumaMas()
        {
            partida.Iniciar(ana, beto);
            for (int ronda = 0; ronda < GENERALA.CATEGORIAS.Length; ronda++)
            {
                JugarTurno(true);
                JugarTurno(false);
            }

            Assert.IsFalse(partida.EnCurso);
            Assert.AreEqual(1, mapper.Finalizadas);
            Assert.AreEqual(beto.ID, mapper.UltimoGanador);
            Assert.AreEqual(0, mapper.UltimoPuntaje1);
            Assert.AreEqual(partida.CalcularTotal(2), mapper.UltimoPuntaje2);
            Assert.IsTrue(mapper.UltimoPuntaje2 > 0);
            Assert.AreEqual("Fin de partida", mapper.UltimaDescripcion);
            Assert.AreEqual(22, xml.Contar("Movimiento"));
            Assert.AreEqual(1, xml.Contar("Fin"));
            Assert.AreEqual(1, finalizaciones);
            Assert.IsFalse(ultimaFueAbandonada);
            Assert.AreEqual("beto ganó la partida!", partida.MensajeFinal(false));
        }

        [TestMethod]
        public void PartidaCompleta_MismoPuntaje_TerminaEmpatada()
        {
            partida.Iniciar(ana, beto);
            for (int ronda = 0; ronda < GENERALA.CATEGORIAS.Length; ronda++)
            {
                JugarTurno(true);
                JugarTurno(true);
            }

            Assert.IsFalse(partida.EnCurso);
            Assert.AreEqual(0, mapper.UltimoGanador);
            Assert.AreEqual("La partida terminó empatada!", partida.MensajeFinal(false));
        }

        [TestMethod]
        public void UltimaAnotacion_LaBaseNoGuarda_RevierteYNoDuplicaElXml()
        {
            partida.Iniciar(ana, beto);
            for (int ronda = 0; ronda < GENERALA.CATEGORIAS.Length - 1; ronda++)
            {
                JugarTurno(false);
                JugarTurno(false);
            }
            JugarTurno(false);

            mapper.ResultadoFinalizar = false;
            partida.Tirar(new bool[5]);
            string ultima = partida.CategoriasDisponibles()[0];

            Assert.ThrowsException<Exception>(() => partida.Anotar(ultima));
            Assert.IsTrue(partida.EnCurso);
            Assert.AreEqual("beto", partida.JugadorDelTurno().Nombre);
            Assert.AreEqual(BE.FILAPUNTAJE.SIN_ANOTAR, PuntajeEn(ultima, 2));
            Assert.AreEqual(21, xml.Contar("Movimiento"));
            Assert.AreEqual(0, xml.Contar("Fin"));
            Assert.AreEqual(0, finalizaciones);

            mapper.ResultadoFinalizar = true;
            partida.Anotar(ultima);

            Assert.IsFalse(partida.EnCurso);
            Assert.AreEqual(22, xml.Contar("Movimiento"));
            Assert.AreEqual(1, xml.Contar("Fin"));
            Assert.AreEqual(1, mapper.Finalizadas);
            Assert.AreEqual(1, finalizaciones);
        }

        [TestMethod]
        public void UltimaAnotacion_LaBaseLanzaExcepcion_SigueEnCurso()
        {
            partida.Iniciar(ana, beto);
            for (int ronda = 0; ronda < GENERALA.CATEGORIAS.Length - 1; ronda++)
            {
                JugarTurno(false);
                JugarTurno(false);
            }
            JugarTurno(false);

            mapper.LanzarAlFinalizar = true;
            partida.Tirar(new bool[5]);
            string ultima = partida.CategoriasDisponibles()[0];

            Assert.ThrowsException<Exception>(() => partida.Anotar(ultima));
            Assert.IsTrue(partida.EnCurso);
            Assert.IsTrue(partida.PuedeAnotar());
            Assert.AreEqual(21, xml.Contar("Movimiento"));
        }

        [TestMethod]
        public void PartidaCompleta_FallaElFinDelXml_IgualTerminaYAvisa()
        {
            partida.Iniciar(ana, beto);
            for (int ronda = 0; ronda < GENERALA.CATEGORIAS.Length - 1; ronda++)
            {
                JugarTurno(false);
                JugarTurno(false);
            }
            JugarTurno(false);

            xml.LanzarAlInsertarFin = true;
            partida.Tirar(new bool[5]);
            string ultima = partida.CategoriasDisponibles()[0];

            Assert.ThrowsException<Exception>(() => partida.Anotar(ultima));
            Assert.IsFalse(partida.EnCurso);
            Assert.AreEqual(1, mapper.Finalizadas);
            Assert.AreEqual(1, finalizaciones);
        }

        [TestMethod]
        public void Abandonar_LaGanaElOtroJugador()
        {
            partida.Iniciar(ana, beto);
            JugarTurno(false);

            partida.Abandonar(ana);

            Assert.IsFalse(partida.EnCurso);
            Assert.AreEqual(beto.ID, mapper.UltimoGanador);
            Assert.AreEqual("Partida abandonada por ana", mapper.UltimaDescripcion);
            Assert.AreEqual(1, xml.Contar("Fin"));
            Assert.IsTrue(xml.Elementos[xml.Elementos.Count - 1].EndsWith("|beto|ana"));
            Assert.AreEqual(1, finalizaciones);
            Assert.IsTrue(ultimaFueAbandonada);
            Assert.AreEqual("ana abandonó la partida. beto ganó la partida!", partida.MensajeFinal(true));
        }

        [TestMethod]
        public void Abandonar_LaBaseNoGuarda_SigueEnCurso()
        {
            partida.Iniciar(ana, beto);
            mapper.ResultadoFinalizar = false;

            Assert.ThrowsException<Exception>(() => partida.Abandonar(beto));
            Assert.IsTrue(partida.EnCurso);
            Assert.AreEqual(0, xml.Contar("Fin"));
            Assert.AreEqual(0, finalizaciones);
        }

        [TestMethod]
        public void Abandonar_UsuarioQueNoJuega_LanzaExcepcion()
        {
            partida.Iniciar(ana, beto);
            BE.USUARIO otro = new BE.USUARIO();
            otro.ID = 99;
            otro.Nombre = "otro";

            Assert.ThrowsException<Exception>(() => partida.Abandonar(otro));
            Assert.IsTrue(partida.EnCurso);
        }

        [TestMethod]
        public void Abandonar_SinPartidaEnCurso_NoHaceNada()
        {
            partida.Abandonar(ana);

            Assert.AreEqual(0, mapper.IntentosFinalizar);
            Assert.AreEqual(0, finalizaciones);
        }

        [TestMethod]
        public void MensajeVaGanando_IndicaQuienLideraYPorCuanto()
        {
            partida.Iniciar(ana, beto);
            Assert.AreEqual("", partida.MensajeVaGanando());

            dado.Cargar(5, 5, 5, 1, 2);
            partida.Tirar(new bool[5]);
            partida.Anotar("Cincos");
            dado.Cargar(5, 1, 2, 3, 3);
            partida.Tirar(new bool[5]);
            partida.Anotar("Cincos");

            Assert.AreEqual("ana va ganando por 10", partida.MensajeVaGanando());
            Assert.AreEqual(1, partida.JugadorQueVaGanando());
        }
    }
}
