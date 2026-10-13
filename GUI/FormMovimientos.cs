using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormMovimientos : Form
    {
        private string rutaXml;

        public FormMovimientos(string rutaXml, string titulo)
        {
            InitializeComponent();
            this.rutaXml = rutaXml;
            lblTitulo.Text = titulo;
        }

        private void FormMovimientos_Load(object sender, EventArgs e)
        {
            lblArchivo.Text = "Archivo: " + rutaXml;

            // Si el XML se editó a mano o quedó cortado, no se puede leer: se avisa y no se muestra nada.
            List<MOVIMIENTO> movimientos;
            try
            {
                movimientos = BLL.MOVIMIENTOXML.LeerMovimientos(rutaXml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo leer el archivo XML de la partida (puede estar dañado):\n" + ex.Message,
                    "Movimientos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvMovimientos.DataSource = movimientos;
            dgvMovimientos.Columns["Turno"].HeaderText = "Turno";
            dgvMovimientos.Columns["Jugador"].HeaderText = "Jugador";
            dgvMovimientos.Columns["Dados"].HeaderText = "Dados";
            dgvMovimientos.Columns["Categoria"].HeaderText = "Categoría";
            dgvMovimientos.Columns["Puntaje"].HeaderText = "Puntaje";
            dgvMovimientos.Columns["Fecha"].HeaderText = "Fecha y hora";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
