using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class BackupRestoreTests
    {
        private FakeBackupRestore fakeBackup;
        private FakeMapperLog fakeLog;
        private BLL.BACKUPRESTORE backupRestore;
        private BE.USUARIO usuario;

        [TestInitialize]
        public void Inicializar()
        {
            fakeBackup = new FakeBackupRestore();
            fakeLog = new FakeMapperLog();
            backupRestore = new BLL.BACKUPRESTORE(fakeBackup, new BLL.BITACORA(fakeLog));
            usuario = new BE.USUARIO();
            usuario.ID = 4;
            usuario.Nombre = "juan";
        }

        [TestMethod]
        public void HacerBackup_Exitoso_RegistraUnBackupEnLaBitacora()
        {
            string error = backupRestore.HacerBackup(@"C:\Backups\Generala_20260501.bak", usuario);

            Assert.AreEqual("", error);
            Assert.AreEqual(1, fakeBackup.Rutas.Count);
            Assert.AreEqual(@"C:\Backups\Generala_20260501.bak", fakeBackup.Rutas[0]);
            Assert.AreEqual(1, fakeLog.Insertados.Count);
            Assert.AreEqual(4, fakeLog.Insertados[0].IdUsuario);
            Assert.AreEqual(BE.LOG.BACKUP, fakeLog.Insertados[0].IdTipo);
        }

        [TestMethod]
        public void HacerBackup_Exitoso_DescripcionTieneElNombreDelArchivoSinLaRuta()
        {
            backupRestore.HacerBackup(@"C:\Backups\Generala_20260501.bak", usuario);

            string descripcion = fakeLog.Insertados[0].Descripcion;
            Assert.IsTrue(descripcion.Contains("Generala_20260501.bak"));
            Assert.IsFalse(descripcion.Contains(@"C:\Backups"));
        }

        [TestMethod]
        public void HacerBackup_ConError_DevuelveElErrorYNoRegistra()
        {
            fakeBackup.ErrorBackup = "No se pudo hacer el backup";

            string error = backupRestore.HacerBackup(@"C:\Backups\Generala.bak", usuario);

            Assert.AreEqual("No se pudo hacer el backup", error);
            Assert.AreEqual(0, fakeLog.Insertados.Count);
        }

        [TestMethod]
        public void HacerRestore_Exitoso_RegistraUnRestoreEnLaBitacora()
        {
            bool restaurada;

            string error = backupRestore.HacerRestore(@"C:\Backups\Generala_20260501.bak", usuario, out restaurada);

            Assert.AreEqual("", error);
            Assert.IsTrue(restaurada);
            Assert.AreEqual(1, fakeLog.Insertados.Count);
            Assert.AreEqual(4, fakeLog.Insertados[0].IdUsuario);
            Assert.AreEqual(BE.LOG.RESTORE, fakeLog.Insertados[0].IdTipo);
            Assert.IsTrue(fakeLog.Insertados[0].Descripcion.Contains("Generala_20260501.bak"));
            Assert.IsFalse(fakeLog.Insertados[0].Descripcion.Contains(@"C:\Backups"));
        }

        [TestMethod]
        public void HacerRestore_NoRestaurada_DevuelveElErrorYNoRegistra()
        {
            fakeBackup.Restaurada = false;
            fakeBackup.ErrorRestore = "El archivo no es un backup válido";
            bool restaurada;

            string error = backupRestore.HacerRestore(@"C:\Backups\Generala.bak", usuario, out restaurada);

            Assert.AreEqual("El archivo no es un backup válido", error);
            Assert.IsFalse(restaurada);
            Assert.AreEqual(0, fakeLog.Insertados.Count);
        }

        [TestMethod]
        public void HacerRestore_FallaLaBitacora_NoLanzaYQuedaRestaurada()
        {
            fakeLog.ResultadoInsertar = -1;
            bool restaurada;

            string error = backupRestore.HacerRestore(@"C:\Backups\Generala.bak", usuario, out restaurada);

            Assert.AreEqual("", error);
            Assert.IsTrue(restaurada);
            Assert.AreEqual(0, fakeLog.Insertados.Count);
        }

        [TestMethod]
        public void CarpetaBackupPorDefecto_DevuelveLaDelDal()
        {
            fakeBackup.Carpeta = @"D:\MisBackups";

            Assert.AreEqual(@"D:\MisBackups", backupRestore.CarpetaBackupPorDefecto());
        }
    }
}
