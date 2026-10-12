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
            List<MOVIMIENTO> movimientos = BLL.MOVIMIENTOXML.LeerMovimientos(rutaXml);

            dgvMovimientos.DataSource = movimientos;
            dgvMovimientos.Columns["Turno"].HeaderText = "Turno";
            dgvMovimientos.Columns["Jugador"].HeaderText = "Jugador";
            dgvMovimientos.Columns["Dados"].HeaderText = "Dados";
            dgvMovimientos.Columns["Categoria"].HeaderText = "Categoría";
            dgvMovimientos.Columns["Puntaje"].HeaderText = "Puntaje";
            dgvMovimientos.Columns["Fecha"].HeaderText = "Fecha y hora";

            lblArchivo.Text = "Archivo: " + rutaXml;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
