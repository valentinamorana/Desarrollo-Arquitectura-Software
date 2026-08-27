using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormPrincipal : Form
    {
        private USUARIO jugador1;
        private USUARIO jugador2;

        private int[] dados = new int[5];
        private bool[] guardar = new bool[5];
        private int tirosRestantes;
        private int jugadorActual;

        private List<FILAPUNTAJE> tabla;
        private PARTIDA partidaActual;
        private int turnoContador;

        public FormPrincipal(USUARIO jugadorLogueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            lblJugador1.Text = "Jugador 1: " + jugador1.Nombre;
            lblJugador2.Text = "Jugador 2: (sin ingresar)";
            CargarTablaVacia();
            DeshabilitarJuego();
        }

        // -------------------- Alta de jugador 2 / inicio de partida --------------------

        private void btnLoginJugador2_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                if (formLogin.UsuarioLogueado.ID == jugador1.ID)
                {
                    MessageBox.Show("El Jugador 2 tiene que ser un usuario distinto al Jugador 1");
                    return;
                }

                jugador2 = formLogin.UsuarioLogueado;
                lblJugador2.Text = "Jugador 2: " + jugador2.Nombre;
                btnComenzarPartida.Enabled = true;
            }
        }

        private void btnComenzarPartida_Click(object sender, EventArgs e)
        {
            if (jugador2 == null)
            {
                MessageBox.Show("Primero tiene que ingresar el Jugador 2");
                return;
            }

            partidaActual = new PARTIDA();
            partidaActual.IdJugador1 = jugador1.ID;
            partidaActual.IdJugador2 = jugador2.ID;

            BLL.PARTIDA partidaBLL = new BLL.PARTIDA();
            partidaBLL.Iniciar(partidaActual);

            turnoContador = 0;
            jugadorActual = 1;
            CargarTablaVacia();
            ActualizarTotales();
            HabilitarJuego();
            IniciarTurno();

            btnComenzarPartida.Visible = false;
            btnLoginJugador2.Enabled = false;
            btnAbandonar.Visible = true;
        }

        // -------------------- Turno de juego --------------------

        private void IniciarTurno()
        {
            dados = new int[5];
            guardar = new bool[5];
            tirosRestantes = 3;

            lblDado1.Text = "-";
            lblDado2.Text = "-";
            lblDado3.Text = "-";
            lblDado4.Text = "-";
            lblDado5.Text = "-";

            chkGuardar1.Checked = false;
            chkGuardar2.Checked = false;
            chkGuardar3.Checked = false;
            chkGuardar4.Checked = false;
            chkGuardar5.Checked = false;

            string nombreActual = jugadorActual == 1 ? jugador1.Nombre : jugador2.Nombre;
            lblTurno.Text = "Turno de: " + nombreActual;
            lblTirosRestantes.Text = "Tiros restantes: " + tirosRestantes;

            btnTirarDados.Enabled = true;
            btnAnotar.Enabled = false;

            ActualizarComboCategorias();
        }

        private void btnTirarDados_Click(object sender, EventArgs e)
        {
            if (tirosRestantes <= 0)
            {
                return;
            }

            guardar[0] = chkGuardar1.Checked;
            guardar[1] = chkGuardar2.Checked;
            guardar[2] = chkGuardar3.Checked;
            guardar[3] = chkGuardar4.Checked;
            guardar[4] = chkGuardar5.Checked;

            dados = BLL.GENERALA.Tirar(dados, guardar);
            tirosRestantes--;

            lblDado1.Text = dados[0].ToString();
            lblDado2.Text = dados[1].ToString();
            lblDado3.Text = dados[2].ToString();
            lblDado4.Text = dados[3].ToString();
            lblDado5.Text = dados[4].ToString();

            lblTirosRestantes.Text = "Tiros restantes: " + tirosRestantes;
            btnAnotar.Enabled = true;

            if (tirosRestantes == 0)
            {
                btnTirarDados.Enabled = false;
            }
        }

        private void ActualizarComboCategorias()
        {
            cmbCategoria.Items.Clear();

            FILAPUNTAJE filaGenerala = tabla.Find(f => f.Categoria == "Generala");
            bool generalaYaAnotada = jugadorActual == 1
                ? filaGenerala.PuntajeJugador1 != null
                : filaGenerala.PuntajeJugador2 != null;

            foreach (FILAPUNTAJE fila in tabla)
            {
                bool sinAnotar = jugadorActual == 1 ? fila.PuntajeJugador1 == null : fila.PuntajeJugador2 == null;
                if (!sinAnotar)
                {
                    continue;
                }

                // "Generala Doble" solo se puede anotar después de haber anotado "Generala".
                if (fila.Categoria == BLL.GENERALA.GENERALA_DOBLE && !generalaYaAnotada)
                {
                    continue;
                }

                cmbCategoria.Items.Add(fila.Categoria);
            }

            if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }
        }

        private void btnAnotar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Elegí una categoría para anotar");
                return;
            }

            string categoria = cmbCategoria.SelectedItem.ToString();
            int puntaje = BLL.GENERALA.CalcularPuntaje(categoria, dados);
            string nombreActual = jugadorActual == 1 ? jugador1.Nombre : jugador2.Nombre;

            turnoContador++;
            BLL.MOVIMIENTOXML.RegistrarMovimiento(partidaActual.RutaXml, turnoContador, nombreActual, dados, categoria, puntaje);

            foreach (FILAPUNTAJE fila in tabla)
            {
                if (fila.Categoria == categoria)
                {
                    if (jugadorActual == 1)
                    {
                        fila.PuntajeJugador1 = puntaje;
                    }
                    else
                    {
                        fila.PuntajeJugador2 = puntaje;
                    }
                }
            }

            RefrescarGrilla();
            ActualizarTotales();

            jugadorActual = jugadorActual == 1 ? 2 : 1;

            if (JuegoTerminado())
            {
                FinalizarPartida();
            }
            else
            {
                IniciarTurno();
            }
        }

        private bool JuegoTerminado()
        {
            foreach (FILAPUNTAJE fila in tabla)
            {
                if (fila.PuntajeJugador1 == null || fila.PuntajeJugador2 == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void FinalizarPartida(bool abandonada = false)
        {
            int total1 = 0;
            int total2 = 0;
            foreach (FILAPUNTAJE fila in tabla)
            {
                total1 += fila.PuntajeJugador1 ?? 0;
                total2 += fila.PuntajeJugador2 ?? 0;
            }

            int? idGanador;
            string mensaje;
            if (total1 > total2)
            {
                idGanador = jugador1.ID;
                mensaje = jugador1.Nombre + " ganó la partida!";
            }
            else if (total2 > total1)
            {
                idGanador = jugador2.ID;
                mensaje = jugador2.Nombre + " ganó la partida!";
            }
            else
            {
                idGanador = null;
                mensaje = "La partida terminó empatada!";
            }

            if (abandonada)
            {
                mensaje = "Partida abandonada. " + mensaje;
            }

            partidaActual.PuntajeJugador1 = total1;
            partidaActual.PuntajeJugador2 = total2;
            partidaActual.IdGanador = idGanador;

            BLL.PARTIDA partidaBLL = new BLL.PARTIDA();
            partidaBLL.Finalizar(partidaActual, abandonada ? "Partida abandonada" : "Fin de partida");

            MessageBox.Show(mensaje, "Partida finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DeshabilitarJuego();
            lblTurno.Text = "Turno de: -";
            lblTirosRestantes.Text = "Tiros restantes: -";
            btnComenzarPartida.Visible = true;
            btnComenzarPartida.Enabled = true;
            btnLoginJugador2.Enabled = true;
            btnAbandonar.Visible = false;
        }

        private void btnAbandonar_Click(object sender, EventArgs e)
        {
            DialogResult confirmar = MessageBox.Show(
                "¿Seguro que querés abandonar la partida? Se va a registrar con el puntaje actual.",
                "Abandonar partida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmar == DialogResult.Yes)
            {
                FinalizarPartida(abandonada: true);
            }
        }

        private void RefrescarGrilla()
        {
            dgvPuntajes.DataSource = null;
            dgvPuntajes.DataSource = tabla;
        }

        private void CargarTablaVacia()
        {
            tabla = new List<FILAPUNTAJE>();
            foreach (string categoria in BLL.GENERALA.CATEGORIAS)
            {
                FILAPUNTAJE fila = new FILAPUNTAJE();
                fila.Categoria = categoria;
                tabla.Add(fila);
            }
            RefrescarGrilla();
        }

        private void ActualizarTotales()
        {
            int total1 = 0;
            int total2 = 0;
            foreach (FILAPUNTAJE fila in tabla)
            {
                total1 += fila.PuntajeJugador1 ?? 0;
                total2 += fila.PuntajeJugador2 ?? 0;
            }
            lblTotalJugador1.Text = "Total " + jugador1.Nombre + ": " + total1;
            lblTotalJugador2.Text = "Total " + (jugador2 != null ? jugador2.Nombre : "Jugador 2") + ": " + total2;
        }

        private void HabilitarJuego()
        {
            btnTirarDados.Enabled = true;
            chkGuardar1.Enabled = true;
            chkGuardar2.Enabled = true;
            chkGuardar3.Enabled = true;
            chkGuardar4.Enabled = true;
            chkGuardar5.Enabled = true;
            cmbCategoria.Enabled = true;
            btnAnotar.Enabled = false;
        }

        private void DeshabilitarJuego()
        {
            btnTirarDados.Enabled = false;
            chkGuardar1.Enabled = false;
            chkGuardar2.Enabled = false;
            chkGuardar3.Enabled = false;
            chkGuardar4.Enabled = false;
            chkGuardar5.Enabled = false;
            cmbCategoria.Enabled = false;
            btnAnotar.Enabled = false;
        }

        // -------------------- Menú superior --------------------

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            usuarioBLL.Logout(jugador1);
            if (jugador2 != null)
            {
                usuarioBLL.Logout(jugador2);
            }
            this.Close();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak";
            dialogo.FileName = "GENERALA.bak";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                bool ok = BLL.BACKUPRESTORE.HacerBackup(dialogo.FileName);
                if (ok)
                {
                    MessageBox.Show("Backup realizado correctamente");
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al hacer el backup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Backup de SQL Server (*.bak)|*.bak";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                DialogResult confirmar = MessageBox.Show(
                    "Esto va a reemplazar los datos actuales de la base por los del backup. ¿Confirmás?",
                    "Restaurar backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    bool ok = BLL.BACKUPRESTORE.HacerRestore(dialogo.FileName);
                    if (ok)
                    {
                        MessageBox.Show("Restore realizado correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al restaurar el backup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            FormEstadisticas formEstadisticas = new FormEstadisticas(jugador1, jugador2);
            formEstadisticas.ShowDialog();
        }
    }
}
