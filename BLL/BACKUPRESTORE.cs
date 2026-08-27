namespace BLL
{
    public static class BACKUPRESTORE
    {
        public static bool HacerBackup(string ruta)
        {
            return DAL.BACKUPRESTORE.HacerBackup(ruta);
        }

        public static bool HacerRestore(string ruta)
        {
            return DAL.BACKUPRESTORE.HacerRestore(ruta);
        }
    }
}
