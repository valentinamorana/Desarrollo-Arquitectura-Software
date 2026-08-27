using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormEstadisticas : Form
    {
        private USUARIO jugador1;
        private USUARIO jugador2;

        public FormEstadisticas(USUARIO jugador1, USUARIO jugador2)
        {
            InitializeComponent();
            this.jugador1 = jugador1;
            this.jugador2 = jugador2;
        }

        private void FormEstadisticas_Load(object sender, EventArgs e)
        {
            cmbJugador.Items.Add(jugador1.Nombre);
            if (jugador2 != null)
            {
                cmbJugador.Items.Add(jugador2.Nombre);
            }
            cmbJugador.SelectedIndex = 0;
        }

        private void cmbJugador_SelectedIndexChanged(object sender, EventArgs e)
        {
            USUARIO usuario;
            if (cmbJugador.SelectedIndex == 0)
            {
                usuario = jugador1;
            }
            else
            {
                usuario = jugador2;
            }
            CargarEstadisticas(usuario);
        }

        private void CargarEstadisticas(USUARIO usuario)
        {
            BLL.PARTIDA partidaBLL = new BLL.PARTIDA();
            List<PARTIDA> partidas = partidaBLL.ListarPorUsuario(usuario.ID);

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();

            int ganadas = 0;
            int perdidas = 0;
            int empatadas = 0;
            TimeSpan tiempoTotal = TimeSpan.Zero;

            List<FILAHISTORIAL> filas = new List<FILAHISTORIAL>();

            foreach (PARTIDA p in partidas)
            {
                bool esJugador1 = p.IdJugador1 == usuario.ID;

                int idRival;
                int puntajePropio;
                int puntajeRival;
                if (esJugador1)
                {
                    idRival = p.IdJugador2;
                    puntajePropio = p.PuntajeJugador1;
                    puntajeRival = p.PuntajeJugador2;
                }
                else
                {
                    idRival = p.IdJugador1;
                    puntajePropio = p.PuntajeJugador2;
                    puntajeRival = p.PuntajeJugador1;
                }

                string resultado;
                if (p.IdGanador == 0)
                {
                    resultado = "Empate";
                    empatadas++;
                }
                else if (p.IdGanador == usuario.ID)
                {
                    resultado = "Ganada";
                    ganadas++;
                }
                else
                {
                    resultado = "Perdida";
                    perdidas++;
                }

                tiempoTotal += p.FechaFin - p.FechaInicio;

                USUARIO rival = usuarioBLL.BuscarPorId(idRival);

                FILAHISTORIAL fila = new FILAHISTORIAL();
                fila.Fecha = p.FechaInicio.ToString("dd/MM/yyyy HH:mm");
                if (rival != null)
                {
                    fila.Rival = rival.Nombre;
                }
                else
                {
                    fila.Rival = "-";
                }
                fila.PuntajePropio = puntajePropio;
                fila.PuntajeRival = puntajeRival;
                fila.Resultado = resultado;
                filas.Add(fila);
            }

            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = filas;

            int totalPartidas = partidas.Count;
            double promedio;
            if (totalPartidas > 0)
            {
                promedio = ganadas * 100.0 / totalPartidas;
            }
            else
            {
                promedio = 0;
            }

            lblGanadas.Text = "Ganadas: " + ganadas;
            lblPerdidas.Text = "Perdidas: " + perdidas;
            lblEmpatadas.Text = "Empatadas: " + empatadas;
            lblPromedio.Text = "Promedio de victorias: " + promedio.ToString("0.0") + "%";
            lblTiempoTotal.Text = "Tiempo total jugado: " + (int)tiempoTotal.TotalHours + "h " + tiempoTotal.Minutes + "m";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
