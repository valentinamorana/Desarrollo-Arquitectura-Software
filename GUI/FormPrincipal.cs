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
        private bool partidaEnCurso = false;

        private BLL.PARTIDA partidaBLL = new BLL.PARTIDA();

        public FormPrincipal(USUARIO jugadorLogueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            partidaBLL.PartidaFinalizada += PartidaBLL_PartidaFinalizada;

            lblJugador1.Text = "Jugador 1: " + jugador1.Nombre;
            lblJugador2.Text = "Jugador 2: (sin ingresar)";
            CargarTablaVacia();
            DeshabilitarJuego();
        }

        private void PartidaBLL_PartidaFinalizada(BE.PARTIDA partida, bool abandonada)
        {
            string mensaje;
            if (partida.IdGanador == jugador1.ID)
            {
                mensaje = jugador1.Nombre + " ganó la partida!";
            }
            else if (partida.IdGanador == jugador2.ID)
            {
                mensaje = jugador2.Nombre + " ganó la partida!";
            }
            else
            {
                mensaje = "La partida terminó empatada!";
            }

            if (abandonada)
            {
                mensaje = "Partida abandonada. " + mensaje;
            }

            MessageBox.Show(mensaje, "Partida finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            partidaEnCurso = false;
            DeshabilitarJuego();
            lblTurno.Text = "Turno de: -";
            lblTirosRestantes.Text = "Tiros restantes: -";
            btnComenzarPartida.Visible = true;
            btnComenzarPartida.Enabled = true;
            btnLoginJugador2.Enabled = true;
            btnAbandonar.Visible = false;
        }

        // -------------------- Alta de jugador 2 / inicio de partida --------------------

        private void btnLoginJugador2_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin(jugador1.Nombre);
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                // Control de respaldo: FormLogin ya rechaza al Jugador 1 antes de loguearlo.
                if (formLogin.UsuarioLogueado.ID == jugador1.ID)
                {
                    MessageBox.Show("El Jugador 2 tiene que ser un usuario distinto al Jugador 1");
                    return;
                }

                // Si entra un Jugador 2 distinto al de la partida anterior, al anterior
                // se le registra el cierre de sesión en la bitácora.
                if (jugador2 != null && jugador2.ID != formLogin.UsuarioLogueado.ID)
                {
                    BLL.USUARIO usuarioBLL = new BLL.USUARIO();
                    usuarioBLL.Logout(jugador2);
                }

                jugador2 = formLogin.UsuarioLogueado;
                lblJugador2.Text = "Jugador 2: " + jugador2.Nombre;
                btnComenzarPartida.Enabled = true;
                btnLoginJugador2.Enabled = false;
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

            partidaBLL.Iniciar(partidaActual);
            partidaEnCurso = true;

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

            // Todavía no se tiró ningún dado este turno: no hay nada que "guardar".
            // Si se dejaran habilitados, tildar un casillero acá haría que ese dado
            // se quede en 0 en la primera tirada en vez de tirarse.
            DeshabilitarCheckboxesGuardar();

            string nombreActual;
            if (jugadorActual == 1)
            {
                nombreActual = jugador1.Nombre;
            }
            else
            {
                nombreActual = jugador2.Nombre;
            }
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
                DeshabilitarCheckboxesGuardar();
            }
            else
            {
                HabilitarCheckboxesGuardar();
            }
        }

        private void ActualizarComboCategorias()
        {
            cmbCategoria.Items.Clear();

            foreach (string categoria in partidaBLL.CategoriasDisponibles(tabla, jugadorActual))
            {
                cmbCategoria.Items.Add(categoria);
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
            bool servido = tirosRestantes == 2;
            int puntaje = partidaBLL.Anotar(tabla, jugadorActual, categoria, dados, servido);

            if (puntaje == 0 && categoria == BLL.GENERALA.GENERALA_DOBLE && !partidaBLL.GeneralaConPuntos(tabla, jugadorActual))
            {
                MessageBox.Show(
                    "Como tu Generala quedó tachada, la Generala Doble no suma: se anota 0 y queda tachada.",
                    "Categoría tachada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (puntaje == 0)
            {
                MessageBox.Show(
                    "Los dados no alcanzan para puntuar en \"" + categoria + "\". Se anota 0 y la categoría queda tachada.",
                    "Categoría tachada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (servido && BLL.GENERALA.EsCombinacion(categoria))
            {
                MessageBox.Show(
                    "¡Tirada servida! Lograste \"" + categoria + "\" en el primer tiro: puntaje doble (" + puntaje + ").",
                    "Servido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            string nombreActual;
            if (jugadorActual == 1)
            {
                nombreActual = jugador1.Nombre;
            }
            else
            {
                nombreActual = jugador2.Nombre;
            }

            turnoContador++;
            BLL.MOVIMIENTOXML.RegistrarMovimiento(partidaActual.RutaXml, turnoContador, nombreActual, dados, categoria, puntaje);

            RefrescarGrilla();
            ActualizarTotales();

            if (jugadorActual == 1)
            {
                jugadorActual = 2;
            }
            else
            {
                jugadorActual = 1;
            }

            // El mensaje y el reseteo de la pantalla los hace PartidaBLL_PartidaFinalizada,
            // suscripto al evento que dispara BLL.PARTIDA al cerrar la partida.
            if (partidaBLL.EstaFinalizada(tabla))
            {
                partidaBLL.Finalizar(partidaActual, tabla);
            }
            else
            {
                IniciarTurno();
            }
        }

        // Abandona el jugador al que le toca el turno; el rival gana la partida.
        private USUARIO JugadorDelTurno()
        {
            if (jugadorActual == 1)
            {
                return jugador1;
            }
            return jugador2;
        }

        private USUARIO RivalDelTurno()
        {
            if (jugadorActual == 1)
            {
                return jugador2;
            }
            return jugador1;
        }

        private void btnAbandonar_Click(object sender, EventArgs e)
        {
            DialogResult confirmar = MessageBox.Show(
                "¿Seguro que " + JugadorDelTurno().Nombre + " quiere abandonar la partida? " +
                "Se registra con el puntaje actual y la gana " + RivalDelTurno().Nombre + ".",
                "Abandonar partida", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmar == DialogResult.Yes)
            {
                partidaBLL.Abandonar(partidaActual, tabla, JugadorDelTurno().ID);
            }
        }

        private void RefrescarGrilla()
        {
            dgvPuntajes.DataSource = null;
            dgvPuntajes.DataSource = tabla;

            string nombreColumna2;
            if (jugador2 != null)
            {
                nombreColumna2 = jugador2.Nombre;
            }
            else
            {
                nombreColumna2 = "Jugador 2";
            }

            dgvPuntajes.Columns["Categoria"].HeaderText = "Categoría";
            dgvPuntajes.Columns["PuntajeJugador1"].HeaderText = jugador1.Nombre;
            dgvPuntajes.Columns["PuntajeJugador2"].HeaderText = nombreColumna2;
        }

        private void dgvPuntajes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() == FILAPUNTAJE.SIN_ANOTAR.ToString())
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void CargarTablaVacia()
        {
            tabla = partidaBLL.CrearTablaVacia();
            RefrescarGrilla();
        }

        private void ActualizarTotales()
        {
            int total1 = partidaBLL.CalcularTotal(tabla, 1);
            int total2 = partidaBLL.CalcularTotal(tabla, 2);

            string nombreJugador2;
            if (jugador2 != null)
            {
                nombreJugador2 = jugador2.Nombre;
            }
            else
            {
                nombreJugador2 = "Jugador 2";
            }

            lblTotalJugador1.Text = "Total " + jugador1.Nombre + ": " + total1;
            lblTotalJugador2.Text = "Total " + nombreJugador2 + ": " + total2;
        }

        private void HabilitarJuego()
        {
            btnTirarDados.Enabled = true;
            cmbCategoria.Enabled = true;
            btnAnotar.Enabled = false;
            // Los checkboxes "Guardar" los habilita/deshabilita cada turno IniciarTurno()
            // y btnTirarDados_Click(), según si ya hay dados tirados para conservar.

            // Durante la partida no se puede hacer backup ni restore: un restore a mitad
            // de partida reemplazaría la base y la partida en curso quedaría inconsistente.
            btnBackup.Enabled = false;
            btnRestore.Enabled = false;
        }

        private void DeshabilitarJuego()
        {
            btnTirarDados.Enabled = false;
            cmbCategoria.Enabled = false;
            btnAnotar.Enabled = false;
            DeshabilitarCheckboxesGuardar();

            btnBackup.Enabled = true;
            btnRestore.Enabled = true;
        }

        private void HabilitarCheckboxesGuardar()
        {
            chkGuardar1.Enabled = true;
            chkGuardar2.Enabled = true;
            chkGuardar3.Enabled = true;
            chkGuardar4.Enabled = true;
            chkGuardar5.Enabled = true;
        }

        private void DeshabilitarCheckboxesGuardar()
        {
            chkGuardar1.Enabled = false;
            chkGuardar2.Enabled = false;
            chkGuardar3.Enabled = false;
            chkGuardar4.Enabled = false;
            chkGuardar5.Enabled = false;
        }

        // -------------------- Menú superior --------------------

        // El botón "Salir" solo cierra el form: el logout (y el abandono de una partida
        // en curso) los hace FormPrincipal_FormClosing, así también se registran si
        // la ventana se cierra con la X.
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (partidaEnCurso)
            {
                DialogResult confirmar = MessageBox.Show(
                    "Hay una partida en curso. Si salís, " + JugadorDelTurno().Nombre + " la abandona " +
                    "y la gana " + RivalDelTurno().Nombre + ". ¿Salir igual?",
                    "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                partidaBLL.Abandonar(partidaActual, tabla, JugadorDelTurno().ID);
            }

            BLL.USUARIO usuarioBLL = new BLL.USUARIO();
            usuarioBLL.Logout(jugador1);
            if (jugador2 != null)
            {
                usuarioBLL.Logout(jugador2);
            }
        }

        // BACKUP/RESTORE los ejecuta el servicio de SQL Server, que no suele tener permiso
        // sobre carpetas personales (Escritorio, Documentos). Por eso el diálogo arranca
        // en la carpeta de backups de la instancia, donde siempre puede leer y escribir.
        private string CarpetaInicialBackups()
        {
            string carpeta = BLL.BACKUPRESTORE.CarpetaBackupPorDefecto();
            if (carpeta != "" && System.IO.Directory.Exists(carpeta))
            {
                return carpeta;
            }
            return "";
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            sfdBackup.FileName = "GENERALA_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".bak";
            sfdBackup.InitialDirectory = CarpetaInicialBackups();

            if (sfdBackup.ShowDialog() == DialogResult.OK)
            {
                string error = BLL.BACKUPRESTORE.HacerBackup(sfdBackup.FileName);
                if (error == "")
                {
                    MessageBox.Show("Backup realizado correctamente en:\n" + sfdBackup.FileName);
                }
                else
                {
                    MessageBox.Show(
                        "Ocurrió un error al hacer el backup:\n" + error +
                        "\n\nSi es un problema de permisos, probá guardarlo en la carpeta de backups de SQL Server.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            ofdRestore.InitialDirectory = CarpetaInicialBackups();

            if (ofdRestore.ShowDialog() == DialogResult.OK)
            {
                DialogResult confirmar = MessageBox.Show(
                    "Esto va a reemplazar los datos actuales de la base por los del backup. ¿Confirmás?",
                    "Restaurar backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    string error = BLL.BACKUPRESTORE.HacerRestore(ofdRestore.FileName);
                    if (error == "")
                    {
                        MessageBox.Show("Restore realizado correctamente");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Ocurrió un error al restaurar el backup:\n" + error,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            FormEstadisticas formEstadisticas = new FormEstadisticas(jugador1, jugador2);
            formEstadisticas.ShowDialog();
        }

        private void btnReglas_Click(object sender, EventArgs e)
        {
            FormReglas formReglas = new FormReglas();
            formReglas.ShowDialog();
        }
    }
}
