using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class BitacoraTests
    {
        private BE.LOG CrearLog(string usuario, string tipo, DateTime fecha)
        {
            BE.LOG log = new BE.LOG();
            log.NombreUsuario = usuario;
            log.NombreTipo = tipo;
            log.Fecha = fecha;
            log.Descripcion = usuario + " " + tipo;
            return log;
        }

        private List<BE.LOG> CrearRegistros()
        {
            List<BE.LOG> registros = new List<BE.LOG>();
            registros.Add(CrearLog("juan", "Inicio de sesión", new DateTime(2026, 3, 1, 10, 0, 0)));
            registros.Add(CrearLog("ana", "Backup", new DateTime(2026, 3, 2, 12, 0, 0)));
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 3, 9, 30, 0)));
            registros.Add(CrearLog("ana", "Inicio de sesión", new DateTime(2026, 3, 4, 18, 0, 0)));
            return registros;
        }

        [TestMethod]
        public void Registrar_InsertaConLosDatosCorrectos()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);

            bitacora.Registrar(7, BE.LOG.INICIO_SESION, "Inicio de sesión de juan");

            Assert.AreEqual(1, mapper.Insertados.Count);
            Assert.AreEqual(7, mapper.Insertados[0].IdUsuario);
            Assert.AreEqual(BE.LOG.INICIO_SESION, mapper.Insertados[0].IdTipo);
            Assert.AreEqual("Inicio de sesión de juan", mapper.Insertados[0].Descripcion);
        }

        [TestMethod]
        public void Registrar_DescripcionNula_RegistraVacia()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);

            bitacora.Registrar(1, BE.LOG.BACKUP, null);

            Assert.AreEqual(1, mapper.Insertados.Count);
            Assert.AreEqual("", mapper.Insertados[0].Descripcion);
        }

        [TestMethod]
        public void Filtrar_FechasInvertidas_LasDaVuelta()
        {
            List<BE.LOG> registros = CrearRegistros();

            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(registros, "", "",
                new DateTime(2026, 3, 3), new DateTime(2026, 3, 2));

            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public void Registrar_DescripcionLarga_LaTrunca()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);
            string larga = new string('a', BLL.BITACORA.MAX_DESCRIPCION + 25);

            bitacora.Registrar(1, BE.LOG.BACKUP, larga);

            Assert.AreEqual(1, mapper.Insertados.Count);
            Assert.AreEqual(BLL.BITACORA.MAX_DESCRIPCION, mapper.Insertados[0].Descripcion.Length);
            Assert.AreEqual(larga.Substring(0, BLL.BITACORA.MAX_DESCRIPCION), mapper.Insertados[0].Descripcion);
        }

        [TestMethod]
        public void Registrar_DescripcionDelLargoMaximo_NoLaTrunca()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);
            string justa = new string('b', BLL.BITACORA.MAX_DESCRIPCION);

            bitacora.Registrar(1, BE.LOG.BACKUP, justa);

            Assert.AreEqual(justa, mapper.Insertados[0].Descripcion);
        }

        [TestMethod]
        public void Registrar_MapperDevuelveMenosUno_Lanza()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            mapper.ResultadoInsertar = -1;
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);

            Assert.ThrowsException<Exception>(() => bitacora.Registrar(1, BE.LOG.BACKUP, "Backup"));
            Assert.AreEqual(0, mapper.Insertados.Count);
        }

        [TestMethod]
        public void Listar_DevuelveLaListaDelMapper()
        {
            FakeMapperLog mapper = new FakeMapperLog();
            mapper.RegistrosParaListar = CrearRegistros();
            BLL.BITACORA bitacora = new BLL.BITACORA(mapper);

            List<BE.LOG> registros = bitacora.Listar();

            Assert.AreSame(mapper.RegistrosParaListar, registros);
            Assert.AreEqual(4, registros.Count);
        }

        [TestMethod]
        public void Filtrar_PorUsuario_DevuelveSoloLosDelUsuario()
        {
            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(CrearRegistros(), "juan", null, null, null);

            Assert.AreEqual(2, resultado.Count);
            foreach (BE.LOG log in resultado)
            {
                Assert.AreEqual("juan", log.NombreUsuario);
            }
        }

        [TestMethod]
        public void Filtrar_PorTipo_DevuelveSoloLosDelTipo()
        {
            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(CrearRegistros(), null, "Backup", null, null);

            Assert.AreEqual(2, resultado.Count);
            foreach (BE.LOG log in resultado)
            {
                Assert.AreEqual("Backup", log.NombreTipo);
            }
        }

        [TestMethod]
        public void Filtrar_PorRangoDeFechas_DevuelveLosDelRango()
        {
            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(CrearRegistros(), null, null,
                new DateTime(2026, 3, 2), new DateTime(2026, 3, 3));

            Assert.AreEqual(2, resultado.Count);
            Assert.AreEqual("ana", resultado[0].NombreUsuario);
            Assert.AreEqual("juan", resultado[1].NombreUsuario);
        }

        [TestMethod]
        public void Filtrar_DesdeConHora_IncluyeDesdeElInicioDelDia()
        {
            List<BE.LOG> registros = new List<BE.LOG>();
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 1, 23, 59, 59)));
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 2, 0, 0, 0)));
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 2, 8, 0, 0)));

            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(registros, null, null,
                new DateTime(2026, 3, 2, 15, 30, 0), null);

            Assert.AreEqual(2, resultado.Count);
            Assert.AreEqual(new DateTime(2026, 3, 2, 0, 0, 0), resultado[0].Fecha);
            Assert.AreEqual(new DateTime(2026, 3, 2, 8, 0, 0), resultado[1].Fecha);
        }

        [TestMethod]
        public void Filtrar_Hasta_IncluyeHastaElFinDelDia()
        {
            List<BE.LOG> registros = new List<BE.LOG>();
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 5, 23, 59, 0)));
            registros.Add(CrearLog("juan", "Backup", new DateTime(2026, 3, 6, 0, 0, 0)));

            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(registros, null, null,
                null, new DateTime(2026, 3, 5, 9, 0, 0));

            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual(new DateTime(2026, 3, 5, 23, 59, 0), resultado[0].Fecha);
        }

        [TestMethod]
        public void Filtrar_FiltrosCombinados_AplicaTodos()
        {
            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(CrearRegistros(), "ana", "Inicio de sesión",
                new DateTime(2026, 3, 1), new DateTime(2026, 3, 31));

            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("ana", resultado[0].NombreUsuario);
            Assert.AreEqual("Inicio de sesión", resultado[0].NombreTipo);
        }

        [TestMethod]
        public void Filtrar_FiltrosCombinadosSinCoincidencias_DevuelveVacio()
        {
            List<BE.LOG> resultado = BLL.BITACORA.Filtrar(CrearRegistros(), "juan", "Backup",
                new DateTime(2026, 3, 4), null);

            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        public void Filtrar_FiltrosVaciosONulos_DevuelveTodo()
        {
            List<BE.LOG> registros = CrearRegistros();

            Assert.AreEqual(4, BLL.BITACORA.Filtrar(registros, null, null, null, null).Count);
            Assert.AreEqual(4, BLL.BITACORA.Filtrar(registros, "", "", null, null).Count);
        }

        [TestMethod]
        public void UsuariosDe_DevuelveDistintosYOrdenados()
        {
            List<string> usuarios = BLL.BITACORA.UsuariosDe(CrearRegistros());

            Assert.AreEqual(2, usuarios.Count);
            Assert.AreEqual("ana", usuarios[0]);
            Assert.AreEqual("juan", usuarios[1]);
        }

        [TestMethod]
        public void TiposDe_DevuelveDistintosYOrdenados()
        {
            List<string> tipos = BLL.BITACORA.TiposDe(CrearRegistros());

            Assert.AreEqual(2, tipos.Count);
            Assert.AreEqual("Backup", tipos[0]);
            Assert.AreEqual("Inicio de sesión", tipos[1]);
        }

        [TestMethod]
        public void UsuariosDeYTiposDe_SinRegistros_DevuelvenVacio()
        {
            List<BE.LOG> registros = new List<BE.LOG>();

            Assert.AreEqual(0, BLL.BITACORA.UsuariosDe(registros).Count);
            Assert.AreEqual(0, BLL.BITACORA.TiposDe(registros).Count);
        }
    }
}
