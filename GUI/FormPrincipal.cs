using System;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormPrincipal : Form
    {
        private USUARIO jugador1;
        private USUARIO jugador2;

        // Todo el estado de la partida (turno, tiradas, dados, planilla) lo maneja BLL.PARTIDA.
        // Este form solo lo muestra y le avisa lo que hace el usuario.
        private BLL.PARTIDA partidaBLL = new BLL.PARTIDA();

        private ucDado[] ucDados;

        public FormPrincipal(USUARIO jugadorLogueado)
        {
            InitializeComponent();
            jugador1 = jugadorLogueado;
            ucDados = new ucDado[] { ucDado1, ucDado2, ucDado3, ucDado4, ucDado5 };
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            partidaBLL.PartidaFinalizada += PartidaBLL_PartidaFinalizada;

            lblJugador1.Text = "Jugador 1: " + jugador1.Nombre;
            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();

            // btnComenzarPartida arranca deshabilitado desde el Diseñador, antes de que
            // se conecte el EnabledChanged, así que el color inicial se aplica acá.
            btnPrincipal_EnabledChanged(btnComenzarPartida, EventArgs.Empty);
            btnPrincipal_EnabledChanged(btnTirarDados, EventArgs.Empty);
        }

        // Windows Forms pinta en gris el texto de un botón deshabilitado sin importar
        // su ForeColor. Sobre el fondo bordó no se lee, así que deshabilitado pasa a rosa claro.
        private void btnPrincipal_EnabledChanged(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            if (boton.Enabled)
            {
                boton.BackColor = System.Drawing.Color.FromArgb(173, 20, 87);
            }
            else
            {
                boton.BackColor = System.Drawing.Color.FromArgb(240, 205, 220);
            }
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // La BLL avisa con este evento que la partida terminó (completa o abandonada).
        private void PartidaBLL_PartidaFinalizada(BE.PARTIDA partida, bool abandonada)
        {
            RefrescarGrilla();
            ActualizarTotales();
            ActualizarPantalla();

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

            // Cuando se abandona, el que abandonó es el que no ganó.
            if (abandonada)
            {
                string quienAbandono;
                if (partida.IdGanador == jugador1.ID)
                {
                    quienAbandono = jugador2.Nombre;
                }
                else
                {
                    quienAbandono = jugador1.Nombre;
                }
                mensaje = quienAbandono + " abandonó la partida. " + mensaje;
            }

            MessageBox.Show(mensaje, "Partida finalizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // -------------------- Pantalla --------------------

        // Pone cada control según el estado de la partida que tiene la BLL.
        // Se llama después de cada acción del usuario.
        private void ActualizarPantalla()
        {
            bool enCurso = partidaBLL.EnCurso;

            if (enCurso)
            {
                lblTurnoNombre.Text = partidaBLL.JugadorDelTurno().Nombre;
                lblTirosRestantes.Text = "Tiros restantes: " + partidaBLL.TirosRestantes;
            }
            else
            {
                lblTurnoNombre.Text = "-";
                lblTirosRestantes.Text = "Tiros restantes: -";
            }
            // "Turno de:" va en negro y el nombre en rosa: son dos labels, el nombre pegado al primero.
            lblTurnoNombre.Left = lblTurno.Right;

            // Turno nuevo (o sin partida): todavía no se tiró, no hay dados para mostrar ni guardar.
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
            cmbCategoria.Enabled = enCurso;

            btnComenzarPartida.Visible = !enCurso;
            btnComenzarPartida.Enabled = !enCurso && jugador2 != null;
            btnLoginJugador2.Enabled = !enCurso;

            // Que se note si el Jugador 2 ya ingresó: nombre con ✓ y el botón pasa a "Cambiar".
            if (jugador2 == null)
            {
                lblJugador2.Text = "Jugador 2: (sin ingresar)";
                lblJugador2.ForeColor = System.Drawing.Color.FromArgb(150, 120, 135);
                btnLoginJugador2.Text = "Ingresar Jugador 2";
            }
            else
            {
                lblJugador2.Text = "Jugador 2: " + jugador2.Nombre + "  ✓";
                lblJugador2.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
                btnLoginJugador2.Text = "Cambiar Jugador 2";
            }
            btnAbandonar.Visible = enCurso;

            // Durante la partida no se puede hacer backup ni restore: un restore a mitad
            // de partida reemplazaría la base y la partida en curso quedaría inconsistente.
            btnBackup.Enabled = !enCurso;
            btnRestore.Enabled = !enCurso;
        }

        // Lista las categorías que el jugador del turno todavía puede anotar.
        // Entre tiradas del mismo turno se mantiene la categoría que tenía elegida.
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
            else if (cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
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

            // Anchos fijos que entran en la grilla sin barra horizontal; los puntajes, centrados.
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

            // Se recalcula cada vez que cambian los totales (después de cada anotación).
            // Hasta que los 2 jugadores anotaron al menos una vez no se muestra nada.
            int diferencia = Math.Abs(partidaBLL.CalcularTotal(1) - partidaBLL.CalcularTotal(2));
            int lider = partidaBLL.JugadorQueVaGanando();
            if (!partidaBLL.EnCurso || !partidaBLL.AmbosAnotaron())
            {
                lblVaGanando.Text = "";
            }
            else if (lider == 1)
            {
                lblVaGanando.Text = jugador1.Nombre + " va ganando por " + diferencia;
            }
            else if (lider == 2)
            {
                lblVaGanando.Text = nombreJugador2 + " va ganando por " + diferencia;
            }
            else
            {
                lblVaGanando.Text = "Van empatados";
            }
        }

        // -------------------- Alta de jugador 2 / inicio de partida --------------------

        private void btnLoginJugador2_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin(jugador1.Nombre);
            if (formLogin.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Si entra un Jugador 2 distinto al de la partida anterior, al anterior
            // se le registra el cierre de sesión en la bitácora.
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

            // Listo para jugar: con Enter o un clic arranca la partida.
            btnComenzarPartida.Focus();
        }

        private void btnComenzarPartida_Click(object sender, EventArgs e)
        {
            if (jugador2 == null)
            {
                MessageBox.Show("Primero tiene que ingresar el Jugador 2");
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

        // -------------------- Turno de juego --------------------

        private void btnTirarDados_Click(object sender, EventArgs e)
        {
            bool[] guardar = new bool[ucDados.Length];
            for (int i = 0; i < ucDados.Length; i++)
            {
                guardar[i] = ucDados[i].Guardado;
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

        private void btnAnotar_Click(object sender, EventArgs e)
        {
            if (cmbCategoria.SelectedItem == null)
            {
                MessageBox.Show("Elegí una categoría para anotar");
                return;
            }

            string categoria = cmbCategoria.SelectedItem.ToString();

            try
            {
                // Los avisos se muestran antes de anotar: si con esta anotación termina la
                // partida, después aparece el mensaje con el ganador.
                // Si la categoría va a quedar tachada (0 puntos), se pide confirmación:
                // con "No" no se anota nada y el jugador puede elegir otra categoría.
                int puntaje = partidaBLL.CalcularPuntaje(categoria);
                string avisoTachada = "";
                if (partidaBLL.GeneralaDobleSoloTachable(categoria))
                {
                    avisoTachada = "Como tu Generala quedó tachada, la Generala Doble no suma: se anota 0 y queda tachada.";
                }
                else if (puntaje == 0)
                {
                    avisoTachada = "Los dados no alcanzan para puntuar en \"" + categoria + "\": se anota 0 y la categoría queda tachada.";
                }

                if (avisoTachada != "")
                {
                    DialogResult confirmar = MessageBox.Show(
                        avisoTachada + "\n\n¿Estás seguro?",
                        "Tachar categoría", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (confirmar != DialogResult.Yes)
                    {
                        return;
                    }
                }
                else if (partidaBLL.AplicaBonusServida(categoria))
                {
                    MessageBox.Show(
                        "¡Tirada servida! Lograste \"" + categoria + "\" en el primer tiro: puntaje doble (" + puntaje + ").",
                        "Servido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Anota, pasa el turno y, si era la última categoría, cierra la partida
                // (la BLL dispara PartidaFinalizada).
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

        // Pregunta qué jugador abandona y abandona la partida; el otro la gana.
        // Devuelve false si se canceló (la partida sigue en curso).
        private bool PreguntarYAbandonar(string mensaje)
        {
            FormAbandonar formAbandonar = new FormAbandonar(jugador1, jugador2, mensaje);
            if (formAbandonar.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }

            try
            {
                partidaBLL.Abandonar(formAbandonar.UsuarioQueAbandona);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
            ActualizarPantalla();
            return true;
        }

        private void btnAbandonar_Click(object sender, EventArgs e)
        {
            PreguntarYAbandonar("La partida se registra con el puntaje actual y la gana el otro jugador.");
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
            // Para salir con una partida en curso, alguno de los 2 tiene que abandonarla.
            // Si se cancela, no se cierra la aplicación.
            if (partidaBLL.EnCurso)
            {
                bool abandono = PreguntarYAbandonar(
                    "Hay una partida en curso. Para salir, uno de los 2 jugadores tiene que abandonarla: la gana el otro.");
                if (!abandono)
                {
                    e.Cancel = true;
                    return;
                }
            }
            // Sin partida en curso, se confirma la salida (solo si la cierra el usuario,
            // no si se está apagando Windows).
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult confirmar = MessageBox.Show(
                    "¿Estás seguro de que querés salir?",
                    "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirmar != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }

            // Si falla la bitácora se avisa, pero la aplicación se cierra igual.
            try
            {
                BLL.USUARIO usuarioBLL = new BLL.USUARIO();
                usuarioBLL.Logout(jugador1);
                if (jugador2 != null)
                {
                    usuarioBLL.Logout(jugador2);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
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
