using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class MP_PARTIDA : MAPPER<BE.PARTIDA>
    {
        // Crea la partida y registra el inicio en la bitácora de los 2 jugadores, todo en
        // una misma transacción: o se graban las 3 cosas o no se graba ninguna.
        // Devuelve el ID de la partida, o -1 si falló.
        public override int Insertar(BE.PARTIDA partida)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idJugador1", partida.IdJugador1));
            parametros.Add(acceso.CrearParametro("@idJugador2", partida.IdJugador2));
            parametros.Add(acceso.CrearParametro("@rutaXml", partida.RutaXml));

            acceso.Abrir();
            int id;
            try
            {
                acceso.IniciarTx();

                id = acceso.LeerEscalar("PARTIDA_INSERTAR", parametros);
                bool ok = id > 0;
                if (ok)
                {
                    ok = RegistrarEnBitacora(partida.IdJugador1, BE.LOG.INICIO_PARTIDA, "Inicio de partida");
                }
                if (ok)
                {
                    ok = RegistrarEnBitacora(partida.IdJugador2, BE.LOG.INICIO_PARTIDA, "Inicio de partida");
                }

                if (ok)
                {
                    acceso.AceptarTx();
                }
                else
                {
                    acceso.CancelarTx();
                    id = -1;
                }
            }
            finally
            {
                // Si algo tiró una excepción antes del Commit, al cerrar la conexión
                // SQL Server deshace solo la transacción que quedó abierta.
                acceso.Cerrar();
            }
            return id;
        }

        // Guarda el resultado de la partida y registra el fin en la bitácora de los 2 jugadores,
        // en una misma transacción. Devuelve false si no se pudo grabar (no queda nada a medias).
        public bool Finalizar(BE.PARTIDA partida, string descripcion)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@id", partida.ID));
            parametros.Add(acceso.CrearParametro("@puntajeJugador1", partida.PuntajeJugador1));
            parametros.Add(acceso.CrearParametro("@puntajeJugador2", partida.PuntajeJugador2));

            if (partida.IdGanador > 0)
            {
                parametros.Add(acceso.CrearParametro("@idGanador", partida.IdGanador));
            }
            else
            {
                parametros.Add(acceso.CrearParametroNulo("@idGanador"));
            }

            acceso.Abrir();
            bool ok;
            try
            {
                acceso.IniciarTx();

                ok = acceso.Escribir("PARTIDA_FINALIZAR", parametros) > 0;
                if (ok)
                {
                    ok = RegistrarEnBitacora(partida.IdJugador1, BE.LOG.FIN_PARTIDA, descripcion);
                }
                if (ok)
                {
                    ok = RegistrarEnBitacora(partida.IdJugador2, BE.LOG.FIN_PARTIDA, descripcion);
                }

                if (ok)
                {
                    acceso.AceptarTx();
                }
                else
                {
                    acceso.CancelarTx();
                }
            }
            finally
            {
                acceso.Cerrar();
            }
            return ok;
        }

        // Inserta en LOG usando la conexión (y la transacción) que ya está abierta.
        private bool RegistrarEnBitacora(int idUsuario, int idTipo, string descripcion)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@descripcion", descripcion));
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));
            parametros.Add(acceso.CrearParametro("@idTipo", idTipo));

            return acceso.Escribir("LOG_INSERTAR", parametros) > 0;
        }

        // Partidas terminadas en las que participó el usuario (como jugador 1 o 2).
        public List<BE.PARTIDA> ListarPorUsuario(int idUsuario)
        {
            acceso = new ACCESO();
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(acceso.CrearParametro("@idUsuario", idUsuario));

            acceso.Abrir();
            DataTable tabla;
            try
            {
                tabla = acceso.Leer("PARTIDA_LISTAR_POR_USUARIO", parametros);
            }
            finally
            {
                acceso.Cerrar();
            }

            List<BE.PARTIDA> partidas = new List<BE.PARTIDA>();
            foreach (DataRow fila in tabla.Rows)
            {
                BE.PARTIDA p = new BE.PARTIDA();
                p.ID = int.Parse(fila["ID"].ToString());
                p.IdJugador1 = int.Parse(fila["ID_JUGADOR1"].ToString());
                p.IdJugador2 = int.Parse(fila["ID_JUGADOR2"].ToString());

                // ID_GANADOR es NULL cuando la partida terminó empatada.
                if (fila["ID_GANADOR"] == DBNull.Value)
                {
                    p.IdGanador = 0;
                }
                else
                {
                    p.IdGanador = int.Parse(fila["ID_GANADOR"].ToString());
                }

                p.PuntajeJugador1 = int.Parse(fila["PUNTAJE_JUGADOR1"].ToString());
                p.PuntajeJugador2 = int.Parse(fila["PUNTAJE_JUGADOR2"].ToString());
                p.FechaInicio = Convert.ToDateTime(fila["FECHA_INICIO"]);
                p.FechaFin = Convert.ToDateTime(fila["FECHA_FIN"]);
                p.RutaXml = fila["RUTA_XML"].ToString();
                partidas.Add(p);
            }
            return partidas;
        }
    }
}
