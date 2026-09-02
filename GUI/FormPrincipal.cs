using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormPrincipal : Form
    {
        private const int SIN_ANOTAR = -1;

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
            this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "generala.ico"));
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

            FILAPUNTAJE filaGenerala = null;
            foreach (FILAPUNTAJE f in tabla)
            {
                if (f.Categoria == "Generala")
                {
                    filaGenerala = f;
                }
            }

            bool generalaYaAnotada;
            if (jugadorActual == 1)
            {
                generalaYaAnotada = filaGenerala.PuntajeJugador1 != SIN_ANOTAR;
            }
            else
            {
                generalaYaAnotada = filaGenerala.PuntajeJugador2 != SIN_ANOTAR;
            }

            foreach (FILAPUNTAJE fila in tabla)
            {
                bool sinAnotar;
                if (jugadorActual == 1)
                {
                    sinAnotar = fila.PuntajeJugador1 == SIN_ANOTAR;
                }
                else
                {
                    sinAnotar = fila.PuntajeJugador2 == SIN_ANOTAR;
                }
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
            bool servido = tirosRestantes == 2;
            int puntaje = BLL.GENERALA.CalcularPuntaje(categoria, dados, servido);

            if (puntaje == 0)
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

            if (jugadorActual == 1)
            {
                jugadorActual = 2;
            }
            else
            {
                jugadorActual = 1;
            }

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
                if (fila.PuntajeJugador1 == SIN_ANOTAR || fila.PuntajeJugador2 == SIN_ANOTAR)
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
                total1 += SumarSiAnotado(fila.PuntajeJugador1);
                total2 += SumarSiAnotado(fila.PuntajeJugador2);
            }

            int idGanador;
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
                idGanador = 0;
                mensaje = "La partida terminó empatada!";
            }

            if (abandonada)
            {
                mensaje = "Partida abandonada. " + mensaje;
            }

            partidaActual.PuntajeJugador1 = total1;
            partidaActual.PuntajeJugador2 = total2;
            partidaActual.IdGanador = idGanador;

            string descripcionBitacora;
            if (abandonada)
            {
                descripcionBitacora = "Partida abandonada";
            }
            else
            {
                descripcionBitacora = "Fin de partida";
            }

            BLL.PARTIDA partidaBLL = new BLL.PARTIDA();
            partidaBLL.Finalizar(partidaActual, descripcionBitacora);

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
                FinalizarPartida(true);
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
            if (e.Value != null && e.Value.ToString() == SIN_ANOTAR.ToString())
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void CargarTablaVacia()
        {
            tabla = new List<FILAPUNTAJE>();
            foreach (string categoria in BLL.GENERALA.CATEGORIAS)
            {
                FILAPUNTAJE fila = new FILAPUNTAJE();
                fila.Categoria = categoria;
                fila.PuntajeJugador1 = SIN_ANOTAR;
                fila.PuntajeJugador2 = SIN_ANOTAR;
                tabla.Add(fila);
            }
            RefrescarGrilla();
        }

        private int SumarSiAnotado(int puntaje)
        {
            if (puntaje == SIN_ANOTAR)
            {
                return 0;
            }
            return puntaje;
        }

        private void ActualizarTotales()
        {
            int total1 = 0;
            int total2 = 0;
            foreach (FILAPUNTAJE fila in tabla)
            {
                total1 += SumarSiAnotado(fila.PuntajeJugador1);
                total2 += SumarSiAnotado(fila.PuntajeJugador2);
            }
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
        }

        private void DeshabilitarJuego()
        {
            btnTirarDados.Enabled = false;
            cmbCategoria.Enabled = false;
            btnAnotar.Enabled = false;
            DeshabilitarCheckboxesGuardar();
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

        private void btnReglas_Click(object sender, EventArgs e)
        {
            FormReglas formReglas = new FormReglas();
            formReglas.ShowDialog();
        }
    }
}
