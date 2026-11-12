using System.Windows.Forms;

namespace GUI
{
    public partial class ucDado : UserControl
    {
        private int valor;

        public ucDado()
        {
            InitializeComponent();
        }

        public int Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                if (valor == 0)
                {
                    lblValor.Text = "-";
                }
                else
                {
                    lblValor.Text = valor.ToString();
                }
            }
        }

        public bool Guardado
        {
            get { return chkGuardar.Checked; }
            set { chkGuardar.Checked = value; }
        }

        public bool PermitirGuardar
        {
            get { return chkGuardar.Enabled; }
            set { chkGuardar.Enabled = value; }
        }

        public void Limpiar()
        {
            Valor = 0;
            Guardado = false;
            PermitirGuardar = false;
        }
    }
}
