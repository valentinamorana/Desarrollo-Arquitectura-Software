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

        // Historial completo del jugador elegido; el filtro muestra solo una parte de él.
        private List<FILAHISTORIAL> historialCompleto;

        public FormEstadisticas(USUARIO jugador1, USUARIO jugador2)
        {
            InitializeComponent();
            this.jugador1 = jugador1;
            this.jugador2 = jugador2;
        }

        private void cmbResultado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        // Muestra en la grilla solo las partidas del resultado elegido. Los contadores de arriba no cambian.
        private void AplicarFiltro()
        {
            if (historialCompleto == null)
            {
                return;
            }

            // El filtro lo calcula BLL.ESTADISTICA; acá solo se muestra el resultado.
            BLL.ESTADISTICA.FILTRO_RESULTADO filtro = (BLL.ESTADISTICA.FILTRO_RESULTADO)cmbResultado.SelectedIndex;
            List<FILAHISTORIAL> visibles = BLL.ESTADISTICA.Filtrar(historialCompleto, filtro);

            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = visibles;

            dgvHistorial.Columns["Fecha"].HeaderText = "Fecha";
            dgvHistorial.Columns["Rival"].HeaderText = "Rival";
            dgvHistorial.Columns["PuntajePropio"].HeaderText = "Mi Puntaje";
            dgvHistorial.Columns["PuntajeRival"].HeaderText = "Puntaje Rival";
            dgvHistorial.Columns["Resultado"].HeaderText = "Resultado";
            dgvHistorial.Columns["RutaXml"].Visible = false;
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

        // Los cálculos los hace BLL.ESTADISTICA; acá solo se muestran.
        private void CargarEstadisticas(USUARIO usuario)
        {
            BLL.ESTADISTICA estadisticaBLL = new BLL.ESTADISTICA();
            ESTADISTICA estadistica;
            try
            {
                estadistica = estadisticaBLL.Calcular(usuario);
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudieron cargar las estadísticas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            historialCompleto = estadistica.Historial;
            AplicarFiltro();

            lblGanadas.Text = "Ganadas: " + estadistica.Ganadas;
            lblPerdidas.Text = "Perdidas: " + estadistica.Perdidas;
            lblEmpatadas.Text = "Empatadas: " + estadistica.Empatadas;
            lblPromedio.Text = "Promedio de victorias: " + estadistica.PromedioVictorias.ToString("0.0") + "%";
            lblTiempoTotal.Text = "Tiempo total jugado: " + (int)estadistica.TiempoTotal.TotalHours + "h " +
                estadistica.TiempoTotal.Minutes + "m " + estadistica.TiempoTotal.Seconds + "s";
        }

        // Consulta posterior del XML de movimientos de la partida elegida en el historial.
        private void btnVerMovimientos_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null)
            {
                Mensaje.Show("Elegí una partida del historial");
                return;
            }

            FILAHISTORIAL fila = (FILAHISTORIAL)dgvHistorial.CurrentRow.DataBoundItem;

            string rutaResuelta = BLL.MOVIMIENTOXML.Resolver(fila.RutaXml);
            if (rutaResuelta == "")
            {
                Mensaje.Show(
                    "No se encontró el archivo XML de esa partida:\n" + fila.RutaXml,
                    "Movimientos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string titulo = "Partida del " + fila.Fecha + " vs " + fila.Rival;
            FormMovimientos formMovimientos = new FormMovimientos(rutaResuelta, titulo);
            formMovimientos.ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
