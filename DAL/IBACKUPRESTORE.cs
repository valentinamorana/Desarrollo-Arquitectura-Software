namespace DAL
{
    public interface IBACKUPRESTORE
    {
        string CarpetaBackupPorDefecto();

        string HacerBackup(string ruta);

        string HacerRestore(string ruta, out bool restaurada);
    }
}
