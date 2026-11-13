using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormBitacora : Form
    {
        private const string TODOS = "(Todos)";

        private List<LOG> registros;
        private bool cargandoFiltros = false;

        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            try
            {
                registros = new BLL.BITACORA().Listar();
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudo cargar la bitácora:\n" + ex.Message, "Bitácora",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cargandoFiltros = true;
            cmbUsuario.Items.Add(TODOS);
            foreach (string usuario in BLL.BITACORA.UsuariosDe(registros))
            {
                cmbUsuario.Items.Add(usuario);
            }
            cmbEvento.Items.Add(TODOS);
            foreach (string tipo in BLL.BITACORA.TiposDe(registros))
            {
                cmbEvento.Items.Add(tipo);
            }
            LimpiarFiltros();
            cargandoFiltros = false;

            AplicarFiltro();
        }

        private void LimpiarFiltros()
        {
            cmbUsuario.SelectedIndex = 0;
            cmbEvento.SelectedIndex = 0;
            dtpDesde.Value = DateTime.Today;
            dtpDesde.Checked = false;
            dtpHasta.Value = DateTime.Today;
            dtpHasta.Checked = false;
        }

        private void Filtro_Changed(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (registros == null)
            {
                return;
            }

            cargandoFiltros = true;
            LimpiarFiltros();
            cargandoFiltros = false;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            if (registros == null || cargandoFiltros)
            {
                return;
            }

            string usuario = "";
            if (cmbUsuario.SelectedIndex > 0)
            {
                usuario = cmbUsuario.SelectedItem.ToString();
            }

            string tipo = "";
            if (cmbEvento.SelectedIndex > 0)
            {
                tipo = cmbEvento.SelectedItem.ToString();
            }

            DateTime? desde = null;
            if (dtpDesde.Checked)
            {
                desde = dtpDesde.Value;
            }

            DateTime? hasta = null;
            if (dtpHasta.Checked)
            {
                hasta = dtpHasta.Value;
            }

            List<LOG> visibles = BLL.BITACORA.Filtrar(registros, usuario, tipo, desde, hasta);

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = visibles;
            ConfigurarColumnas();

            lblCantidad.Text = visibles.Count + " de " + registros.Count + " registros";
        }

        private void ConfigurarColumnas()
        {
            if (!dgvBitacora.Columns.Contains("Fecha"))
            {
                return;
            }

            dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y hora";
            dgvBitacora.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            dgvBitacora.Columns["NombreUsuario"].HeaderText = "Usuario";
            dgvBitacora.Columns["NombreTipo"].HeaderText = "Evento";
            dgvBitacora.Columns["Descripcion"].HeaderText = "Descripción";

            dgvBitacora.Columns["IdUsuario"].Visible = false;
            dgvBitacora.Columns["IdTipo"].Visible = false;

            dgvBitacora.Columns["Fecha"].FillWeight = 120;
            dgvBitacora.Columns["NombreUsuario"].FillWeight = 90;
            dgvBitacora.Columns["NombreTipo"].FillWeight = 110;
            dgvBitacora.Columns["Descripcion"].FillWeight = 260;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
