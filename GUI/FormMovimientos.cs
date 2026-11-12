using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BE;

namespace GUI
{
    public partial class FormMovimientos : Form
    {
        private string rutaXml;
        private readonly ToolTip tooltipArchivo = new ToolTip();
        private System.Drawing.Font fuenteNegrita;

        public FormMovimientos(string rutaXml, string titulo)
        {
            InitializeComponent();
            this.rutaXml = rutaXml;
            lblTitulo.Text = titulo;
        }

        private void FormMovimientos_Load(object sender, EventArgs e)
        {
            lblArchivo.Text = "Archivo: " + Path.GetFileName(rutaXml);
            tooltipArchivo.SetToolTip(lblArchivo, rutaXml);

            List<MOVIMIENTO> movimientos;
            try
            {
                movimientos = BLL.MOVIMIENTOXML.LeerMovimientos(rutaXml);
            }
            catch (Exception ex)
            {
                Mensaje.Show("No se pudo leer el archivo XML de la partida (puede estar dañado):\n" + ex.Message,
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

            dgvMovimientos.Columns["Turno"].FillWeight = 45;
            dgvMovimientos.Columns["Jugador"].FillWeight = 90;
            dgvMovimientos.Columns["Accion"].FillWeight = 80;
            dgvMovimientos.Columns["Dados"].FillWeight = 80;
            dgvMovimientos.Columns["Guardados"].FillWeight = 80;
            dgvMovimientos.Columns["Categoria"].FillWeight = 110;
            dgvMovimientos.Columns["Puntaje"].FillWeight = 150;
            dgvMovimientos.Columns["Fecha"].FillWeight = 115;
        }

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
                e.CellStyle.ForeColor = Tema.Rosa;
            }
        }

        private void btnDescargarXml_Click(object sender, EventArgs e)
        {
            if (!BLL.MOVIMIENTOXML.Existe(rutaXml))
            {
                Mensaje.Show("No se encontró el archivo XML de esta partida.",
                    "Descargar XML", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Title = "Guardar XML de la partida";
                dialogo.Filter = "Archivo XML (*.xml)|*.xml";
                dialogo.FileName = Path.GetFileName(rutaXml);
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    File.Copy(rutaXml, dialogo.FileName, true);
                    Mensaje.Show("XML guardado en:\n" + dialogo.FileName,
                        "Descargar XML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Mensaje.Show("No se pudo guardar el XML:\n" + ex.Message,
                        "Descargar XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
