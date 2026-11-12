using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormBitacora : Form
    {
        public FormBitacora()
        {
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            try
            {
                dgvBitacora.DataSource = BLL.BITACORA.Listar();
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudo cargar la bitácora:\n" + ex.Message, "Bitácora",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
