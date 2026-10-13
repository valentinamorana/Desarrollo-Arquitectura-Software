namespace DAL
{
    // Clase base de todos los mappers: cada MP_X traduce una entidad BE.X a su tabla.
    // Es genérica (T es la entidad) y abstracta: no se puede instanciar, y obliga a
    // cada mapper a implementar (override) el alta de su entidad. Además, cada uno
    // agrega las operaciones propias que necesita (login, cierre de partida, etc.).
    public abstract class MAPPER<T>
    {
        internal ACCESO acceso;

        // Devuelve el ID generado (o las filas afectadas), o -1 si falló.
        public abstract int Insertar(T objeto);
    }
}
