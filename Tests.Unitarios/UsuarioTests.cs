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
    }
}
