namespace BLL
{
    public static class BACKUPRESTORE
    {
        public static string CarpetaBackupPorDefecto()
        {
            return DAL.BACKUPRESTORE.CarpetaBackupPorDefecto();
        }

        public static string HacerBackup(string ruta)
        {
            return DAL.BACKUPRESTORE.HacerBackup(ruta);
        }

        public static string HacerRestore(string ruta, out bool restaurada)
        {
            return DAL.BACKUPRESTORE.HacerRestore(ruta, out restaurada);
        }
    }
}
