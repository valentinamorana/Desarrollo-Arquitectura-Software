using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormReglas : Form
    {
        public FormReglas()
        {
            InitializeComponent();
            this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "generala.ico"));
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
