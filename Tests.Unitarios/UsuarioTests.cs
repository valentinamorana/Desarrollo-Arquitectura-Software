using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void UsuarioValido_NoDaError()
        {
            BLL.USUARIO usuario = new BLL.USUARIO();
            BE.USUARIO nuevo = new BE.USUARIO();
            nuevo.Nombre = "juan";
            nuevo.Contraseña = "clave1";
            Assert.AreEqual("", usuario.ValidarRegistro(nuevo));
        }

        [TestMethod]
        public void NombreConEspacios_DaError()
        {
            BLL.USUARIO usuario = new BLL.USUARIO();
            BE.USUARIO nuevo = new BE.USUARIO();
            nuevo.Nombre = "juan perez";
            nuevo.Contraseña = "clave1";
            Assert.AreNotEqual("", usuario.ValidarRegistro(nuevo));
        }

        [TestMethod]
        public void Contraseña_SinNumero_DaError()
        {
            BLL.USUARIO usuario = new BLL.USUARIO();
            BE.USUARIO nuevo = new BE.USUARIO();
            nuevo.Nombre = "juan";
            nuevo.Contraseña = "clavesinnumero";
            Assert.AreNotEqual("", usuario.ValidarRegistro(nuevo));
        }

        [TestMethod]
        public void Contraseña_MuyCorta_DaError()
        {
            BLL.USUARIO usuario = new BLL.USUARIO();
            BE.USUARIO nuevo = new BE.USUARIO();
            nuevo.Nombre = "juan";
            nuevo.Contraseña = "a1";
            Assert.AreNotEqual("", usuario.ValidarRegistro(nuevo));
        }

        private BE.USUARIO CrearUsuario(string nombre, string contraseña)
        {
            BE.USUARIO nuevo = new BE.USUARIO();
            nuevo.Nombre = nombre;
            nuevo.Contraseña = contraseña;
            return nuevo;
        }

        [TestMethod]
        public void Login_Correcto_RegistraElInicioDeSesionYBorraLaContraseña()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            mapper.IdDelLogin = 5;
            FakeMapperLog log = new FakeMapperLog();
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(log));
            BE.USUARIO usuario = CrearUsuario("juan", "clave1");

            Assert.IsTrue(usuarioBLL.Login(usuario));
            Assert.AreEqual(5, usuario.ID);
            Assert.AreEqual("", usuario.Contraseña);
            Assert.AreEqual(1, log.Insertados.Count);
            Assert.AreEqual(BE.LOG.INICIO_SESION, log.Insertados[0].IdTipo);
            Assert.AreEqual(5, log.Insertados[0].IdUsuario);
        }

        [TestMethod]
        public void Login_Incorrecto_NoRegistraNada()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            mapper.LoginCorrecto = false;
            FakeMapperLog log = new FakeMapperLog();
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(log));

            Assert.IsFalse(usuarioBLL.Login(CrearUsuario("juan", "mala1")));
            Assert.AreEqual(0, log.Insertados.Count);
        }

        [TestMethod]
        public void Logout_RegistraElCierreDeSesion()
        {
            FakeMapperLog log = new FakeMapperLog();
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(new FakeMapperUsuario(), new BLL.BITACORA(log));
            BE.USUARIO usuario = CrearUsuario("juan", "");
            usuario.ID = 3;

            usuarioBLL.Logout(usuario);

            Assert.AreEqual(1, log.Insertados.Count);
            Assert.AreEqual(BE.LOG.CIERRE_SESION, log.Insertados[0].IdTipo);
            Assert.AreEqual(3, log.Insertados[0].IdUsuario);
        }

        [TestMethod]
        public void Insertar_DatosInvalidos_LanzaYNoLlegaALaBase()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(new FakeMapperLog()));

            Assert.ThrowsException<System.Exception>(() => usuarioBLL.Insertar(CrearUsuario("juan perez", "clave1")));
            Assert.AreEqual(0, mapper.Insertados);
        }

        [TestMethod]
        public void Insertar_Correcto_AsignaElId()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            mapper.IdAInsertar = 8;
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(new FakeMapperLog()));
            BE.USUARIO usuario = CrearUsuario("juan", "clave1");

            Assert.IsTrue(usuarioBLL.Insertar(usuario));
            Assert.AreEqual(8, usuario.ID);
        }

        [TestMethod]
        public void Insertar_NombreRepetido_DevuelveFalse()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            mapper.IdAInsertar = 0;
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(new FakeMapperLog()));

            Assert.IsFalse(usuarioBLL.Insertar(CrearUsuario("juan", "clave1")));
        }

        [TestMethod]
        public void Insertar_FallaLaBase_Lanza()
        {
            FakeMapperUsuario mapper = new FakeMapperUsuario();
            mapper.IdAInsertar = -1;
            BLL.USUARIO usuarioBLL = new BLL.USUARIO(mapper, new BLL.BITACORA(new FakeMapperLog()));

            Assert.ThrowsException<System.Exception>(() => usuarioBLL.Insertar(CrearUsuario("juan", "clave1")));
        }
    }
}
