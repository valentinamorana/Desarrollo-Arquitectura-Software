using System;
using System.IO;

namespace BLL
{
    public class BACKUPRESTORE
    {
        private DAL.IBACKUPRESTORE backupRestore;
        private BITACORA bitacora;

        public BACKUPRESTORE() : this(new DAL.BACKUPRESTORE(), new BITACORA())
        {
        }

        public BACKUPRESTORE(DAL.IBACKUPRESTORE backupRestore, BITACORA bitacora)
        {
            this.backupRestore = backupRestore;
            this.bitacora = bitacora;
        }

        public string CarpetaBackupPorDefecto()
        {
            return backupRestore.CarpetaBackupPorDefecto();
        }

        public string HacerBackup(string ruta, BE.USUARIO usuario)
        {
            string error = backupRestore.HacerBackup(ruta);
            if (error == "")
            {
                bitacora.Registrar(usuario.ID, BE.LOG.BACKUP, "Backup de la base: " + Path.GetFileName(ruta));
            }
            return error;
        }

        public string HacerRestore(string ruta, BE.USUARIO usuario, out bool restaurada)
        {
            string error = backupRestore.HacerRestore(ruta, out restaurada);
            if (restaurada)
            {
                try
                {
                    bitacora.Registrar(usuario.ID, BE.LOG.RESTORE, "Restore de la base: " + Path.GetFileName(ruta));
                }
                catch (Exception)
                {
                }
            }
            return error;
        }
    }
}
