using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormMovimientos : Form
    {
        private string rutaXml;
        private System.Drawing.Font fuenteNegrita;

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

            fuenteNegrita = new System.Drawing.Font(dgvMovimientos.Font, System.Drawing.FontStyle.Bold);
            dgvMovimientos.DataSource = movimientos;
            dgvMovimientos.Columns["Turno"].HeaderText = "Turno";
            dgvMovimientos.Columns["Jugador"].HeaderText = "Jugador";
            dgvMovimientos.Columns["Accion"].HeaderText = "Acción";
            dgvMovimientos.Columns["Dados"].HeaderText = "Dados";
            dgvMovimientos.Columns["Guardados"].HeaderText = "Dados guardados";
            dgvMovimientos.Columns["Categoria"].HeaderText = "Categoría / Resultado";
            dgvMovimientos.Columns["Puntaje"].HeaderText = "Puntaje";
            dgvMovimientos.Columns["Fecha"].HeaderText = "Fecha y hora";

            // Ancho relativo de cada columna (la grilla reparte el espacio con Fill).
            dgvMovimientos.Columns["Turno"].FillWeight = 45;
            dgvMovimientos.Columns["Jugador"].FillWeight = 90;
            dgvMovimientos.Columns["Accion"].FillWeight = 80;
            dgvMovimientos.Columns["Dados"].FillWeight = 80;
            dgvMovimientos.Columns["Guardados"].FillWeight = 80;
            dgvMovimientos.Columns["Categoria"].FillWeight = 110;
            dgvMovimientos.Columns["Puntaje"].FillWeight = 150;
            dgvMovimientos.Columns["Fecha"].FillWeight = 115;
        }

        // Las anotaciones van en negrita y el cierre de la partida resaltado, para
        // distinguirlos de las tiradas.
        private void dgvMovimientos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            MOVIMIENTO m = (MOVIMIENTO)dgvMovimientos.Rows[e.RowIndex].DataBoundItem;
            if (m.Accion == "Anotación")
            {
                e.CellStyle.Font = fuenteNegrita;
            }
            else if (m.Accion == "Fin de partida" || m.Accion == "Abandono")
            {
                e.CellStyle.Font = fuenteNegrita;
                e.CellStyle.BackColor = System.Drawing.Color.FromArgb(248, 214, 227);
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(173, 20, 87);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
