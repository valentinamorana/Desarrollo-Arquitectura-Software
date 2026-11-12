using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormPrincipal : Form
    {
        private USUARIO jugador1;
        private USUARIO jugador2;

        private BLL.PARTIDA partidaBLL = new BLL.PARTIDA();

        private ucDado[] ucDados;

        private bool volverAlLogin = false;

        public bool VolverAlLogin
        {
            get { return volverAlLogin; }
        }

        public USUARIO Jugador2
        {
            get { return jugador2; }
        }

        public FormPrincipal(USUARIO jugadorLogueado, USUARIO jugador2Logueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
            jugador2 = jugador2Logueado;
            ucDados = new ucDado[] { ucDado1, ucDado2, ucDado3, ucDado4, ucDado5 };
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            partidaBLL.PartidaFinalizada += PartidaBLL_PartidaFinalizada;

            lblJugador1.Text = "Jugador 1: " + jugador1.Nombre;
            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();

            btnPrincipal_EnabledChanged(btnComenzarPartida, EventArgs.Empty);
            btnPrincipal_EnabledChanged(btnTirarDados, EventArgs.Empty);
        }

        private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && btnTirarDados.Enabled)
            {
                btnTirarDados.PerformClick();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Enter && btnAnotar.Enabled && cmbCategoria.SelectedItem != null)
            {
                btnAnotar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        private void cmbCategoria_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            string categoria;
            if (e.Index >= 0)
            {
                categoria = cmbCategoria.Items[e.Index].ToString();
            }
            else if (cmbCategoria.SelectedItem != null)
            {
                categoria = cmbCategoria.SelectedItem.ToString();
            }
            else
            {
                return;
            }

            bool seleccionado = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            System.Drawing.Color colorTexto = seleccionado ? System.Drawing.SystemColors.HighlightText : cmbCategoria.ForeColor;

            using (System.Drawing.Brush pincel = new System.Drawing.SolidBrush(colorTexto))
            {
                e.Graphics.DrawString(categoria, e.Font, pincel, e.Bounds.Left + 4, e.Bounds.Top + 3);

                if (partidaBLL.PuedeAnotar())
                {
                    int puntaje = partidaBLL.CalcularPuntaje(categoria);
                    string texto;
                    if (puntaje > 0)
                    {
                        texto = puntaje + " pts";
                        if (partidaBLL.AplicaBonusServida(categoria))
                        {
                            texto += " (servida)";
                        }
                    }
                    else
                    {
                        texto = "0 (tachar)";
                    }

                    System.Drawing.SizeF medida = e.Graphics.MeasureString(texto, e.Font);
                    e.Graphics.DrawString(texto, e.Font, pincel, e.Bounds.Right - medida.Width - 6, e.Bounds.Top + 3);
                }
            }

            e.DrawFocusRectangle();
        }

        private void btnPrincipal_EnabledChanged(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            if (boton.Enabled)
            {
                boton.BackColor = Tema.Rosa;
            }
            else
            {
                boton.BackColor = System.Drawing.Color.FromArgb(240, 205, 220);
            }
        }

        private void MostrarError(Exception ex)
        {
            Mensaje.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void PartidaBLL_PartidaFinalizada(BE.PARTIDA partida, bool abandonada)
        {
            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();

            string mensaje = partidaBLL.MensajeFinal(abandonada);

            Mensaje.Show(mensaje, "Partida finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActualizarPantalla()
        {
            bool enCurso = partidaBLL.EnCurso;

            if (enCurso)
            {
                lblTurnoNombre.Text = partidaBLL.JugadorDelTurno().Nombre;
                lblTirosRestantes.Text = "Tiros restantes: " + partidaBLL.TirosRestantes;
                lblProgreso.Text = "Turno " + partidaBLL.TurnoDelJugador + " de 11";
            }
            else
            {
                lblTurnoNombre.Text = "-";
                lblTirosRestantes.Text = "Tiros restantes: -";
                lblProgreso.Text = "Partida sin iniciar";
            }
            lblTurnoNombre.Left = lblTurno.Right;

            bool turnoNuevo = !partidaBLL.PuedeAnotar();

            int[] dados = partidaBLL.Dados;
            for (int i = 0; i < ucDados.Length; i++)
            {
                if (turnoNuevo)
                {
                    ucDados[i].Limpiar();
                }
                else
                {
                    ucDados[i].Valor = dados[i];
                    ucDados[i].PermitirGuardar = partidaBLL.PuedeGuardarDados();
                }
            }

            ActualizarComboCategorias(turnoNuevo);

            btnTirarDados.Enabled = partidaBLL.PuedeTirar();
            btnAnotar.Enabled = partidaBLL.PuedeAnotar();
            btnTachar.Enabled = partidaBLL.PuedeAnotar();
            cmbCategoria.Enabled = enCurso;

            btnComenzarPartida.Visible = !enCurso;
            btnComenzarPartida.Enabled = !enCurso && jugador2 != null;
            btnLoginJugador2.Enabled = !enCurso;

            if (jugador2 == null)
            {
                lblJugador2.Text = "Jugador 2: (sin ingresar)";
                lblJugador2.ForeColor = System.Drawing.Color.FromArgb(150, 120, 135);
                btnLoginJugador2.Text = "Ingresar Jugador 2";
            }
            else
            {
                lblJugador2.Text = "Jugador 2: " + jugador2.Nombre + "  ✓";
                lblJugador2.ForeColor = Tema.Rosa;
                btnLoginJugador2.Text = "Cambiar Jugador 2";
            }
            btnAbandonar.Visible = enCurso;

            btnBackup.Enabled = !enCurso;
            btnRestore.Enabled = !enCurso;
        }

        private void ActualizarComboCategorias(bool turnoNuevo)
        {
            string seleccionada = null;
            if (cmbCategoria.SelectedItem != null)
            {
                seleccionada = cmbCategoria.SelectedItem.ToString();
            }

            cmbCategoria.Items.Clear();
            if (!partidaBLL.EnCurso)
            {
                return;
            }

            foreach (string categoria in partidaBLL.CategoriasDisponibles())
            {
                cmbCategoria.Items.Add(categoria);
            }

            if (!turnoNuevo && seleccionada != null && cmbCategoria.Items.Contains(seleccionada))
            {
                cmbCategoria.SelectedItem = seleccionada;
            }
            else
            {
                cmbCategoria.SelectedIndex = -1;
            }
        }

        private void RefrescarGrilla()
        {
            dgvPuntajes.DataSource = null;
            dgvPuntajes.DataSource = partidaBLL.Tabla;

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

            dgvPuntajes.Columns["Categoria"].Width = 155;
            dgvPuntajes.Columns["PuntajeJugador1"].Width = 113;
            dgvPuntajes.Columns["PuntajeJugador2"].Width = 113;
            dgvPuntajes.Columns["PuntajeJugador1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPuntajes.Columns["PuntajeJugador2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dgvPuntajes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString() == FILAPUNTAJE.SIN_ANOTAR.ToString())
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void ResaltarLider(int lider)
        {
            bool hayLider = partidaBLL.EnCurso && partidaBLL.AmbosAnotaron() && lider != 0;
            lblTotalJugador1.ForeColor = (hayLider && lider == 1) ? Tema.Rosa : Tema.Vino;
            lblTotalJugador2.ForeColor = (hayLider && lider == 2) ? Tema.Rosa : Tema.Vino;
        }

        private void ActualizarTotales()
        {
            string nombreJugador2;
            if (jugador2 != null)
            {
                nombreJugador2 = jugador2.Nombre;
            }
            else
            {
                nombreJugador2 = "Jugador 2";
            }

            lblTotalJugador1.Text = "Total " + jugador1.Nombre + ": " + partidaBLL.CalcularTotal(1);
            lblTotalJugador2.Text = "Total " + nombreJugador2 + ": " + partidaBLL.CalcularTotal(2);

            int lider = partidaBLL.JugadorQueVaGanando();
            ResaltarLider(lider);
            lblVaGanando.Text = partidaBLL.MensajeVaGanando();
        }

        private void btnLoginJugador2_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin(jugador1.Nombre);
            if (formLogin.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (jugador2 != null && jugador2.ID != formLogin.UsuarioLogueado.ID)
            {
                try
                {
                    BLL.USUARIO usuarioBLL = new BLL.USUARIO();
                    usuarioBLL.Logout(jugador2);
                }
                catch (Exception ex)
                {
                    MostrarError(ex);
                }
            }

            jugador2 = formLogin.UsuarioLogueado;
            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();

            btnComenzarPartida.Focus();
        }

        private void btnComenzarPartida_Click(object sender, EventArgs e)
        {
            if (jugador2 == null)
            {
                Mensaje.Show("Primero tiene que ingresar el Jugador 2");
                return;
            }

            try
            {
                partidaBLL.Iniciar(jugador1, jugador2);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                return;
            }

            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();
        }

        private void btnTirarDados_Click(object sender, EventArgs e)
        {
            bool[] guardar = new bool[ucDados.Length];
            bool hayDadosGuardados = false;
            for (int i = 0; i < ucDados.Length; i++)
            {
                guardar[i] = ucDados[i].Guardado;
                if (guardar[i])
                {
                    hayDadosGuardados = true;
                }
            }

            if (partidaBLL.PuedeAnotar() && !hayDadosGuardados)
            {
                DialogResult confirmar = Mensaje.Show(
                    "No guardaste ningún dado: se vuelven a tirar los 5.\n\n¿Querés tirar de nuevo?",
                    "Tirar dados", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirmar != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                partidaBLL.Tirar(guardar);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            ActualizarPantalla();
        }

        private void btnTachar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem == null)
            {
                Mensaje.Show("Elegí una categoría para tachar");
                return;
            }

            string categoria = cmbCategoria.SelectedItem.ToString();
            DialogResult confirmar = Mensaje.Show(
                "¿Tachar \"" + categoria + "\"?\n\nSe anota 0 y no vas a poder usarla después, aunque los dados den puntos.",
                "Tachar categoría", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirmar != DialogResult.Yes)
            {
                return;
            }

            try
            {
                partidaBLL.Tachar(categoria);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }

            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();
        }

        private void btnAnotar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem == null)
            {
                Mensaje.Show("Elegí una categoría para anotar");
                return;
            }

            string categoria = cmbCategoria.SelectedItem.ToString();

            try
            {
                string avisoTachada = partidaBLL.AvisoAntesDeAnotar(categoria);

                if (avisoTachada != "")
                {
                    DialogResult confirmar = Mensaje.Show(
                        avisoTachada + "\n\n¿Estás seguro?",
                        "Tachar categoría", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (confirmar != DialogResult.Yes)
                    {
                        return;
                    }
                }
                else if (partidaBLL.AplicaBonusServida(categoria))
                {
                    int puntaje = partidaBLL.CalcularPuntaje(categoria);
                    Mensaje.Show(
                        "¡Tirada servida! Lograste \"" + categoria + "\" en el primer tiro: puntaje doble (" + puntaje + ").",
                        "Servido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                partidaBLL.Anotar(categoria);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }

            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();
        }

        private bool PreguntarYAbandonar(string mensaje)
        {
            FormAbandonar formAbandonar = new FormAbandonar(jugador1, jugador2, mensaje);
            if (formAbandonar.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }

            bool abandonoOk = true;
            try
            {
                partidaBLL.Abandonar(formAbandonar.UsuarioQueAbandona);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
                abandonoOk = false;
            }
            ActualizarPantalla();
            return abandonoOk;
        }

        private void btnAbandonar_Click(object sender, EventArgs e)
        {
            PreguntarYAbandonar("La partida se registra con el puntaje actual y la gana el otro jugador.");
        }

        private void btnVolverMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (volverAlLogin)
            {
                return;
            }

            if (partidaBLL.EnCurso)
            {
                bool abandono = PreguntarYAbandonar(
                    "Hay una partida en curso. Para volver al menú, uno de los 2 jugadores tiene que abandonarla: la gana el otro.");
                if (!abandono)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

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
                    Mensaje.Show("Backup realizado correctamente en:\n" + sfdBackup.FileName);
                }
                else
                {
                    Mensaje.Show(
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
                DialogResult confirmar = Mensaje.Show(
                    "Esto va a reemplazar los datos actuales de la base por los del backup. ¿Confirmás?",
                    "Restaurar backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmar == DialogResult.Yes)
                {
                    bool restaurada;
                    string error = BLL.BACKUPRESTORE.HacerRestore(ofdRestore.FileName, out restaurada);
                    if (restaurada)
                    {
                        if (error == "")
                        {
                            Mensaje.Show(
                                "Restore realizado correctamente.\n\nComo los datos de la base cambiaron, " +
                                "hay que volver a iniciar sesión.",
                                "Restaurar backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Mensaje.Show(
                                "Los datos del backup se restauraron, pero después ocurrió un error:\n" + error +
                                "\n\nHay que volver a iniciar sesión. Si la aplicación no puede conectarse, " +
                                "ejecutá en SSMS: ALTER DATABASE GENERALA SET MULTI_USER",
                                "Restaurar backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        volverAlLogin = true;
                        this.Close();
                    }
                    else
                    {
                        Mensaje.Show(
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

        private void lblProgreso_Click(object sender, EventArgs e)
        {

        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
