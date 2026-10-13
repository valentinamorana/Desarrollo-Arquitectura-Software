using System.Windows.Forms;

namespace GUI
{
    // Control de usuario: un dado (el número que salió) con su casilla "Guardar".
    // FormPrincipal usa 5 instancias en vez de repetir 5 labels y 5 checkboxes.
    public partial class ucDado : UserControl
    {
        private int valor;

        public ucDado()
        {
            InitializeComponent();
        }

        // 0 = todavía no se tiró en este turno (se muestra "-").
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

        // Si el jugador marcó el dado para conservarlo en la próxima tirada.
        public bool Guardado
        {
            get { return chkGuardar.Checked; }
            set { chkGuardar.Checked = value; }
        }

        // Habilita o deshabilita la casilla "Guardar".
        public bool PermitirGuardar
        {
            get { return chkGuardar.Enabled; }
            set { chkGuardar.Enabled = value; }
        }

        // Deja el dado como al empezar un turno: sin valor, sin guardar y sin poder guardarlo.
        public void Limpiar()
        {
            Valor = 0;
            Guardado = false;
            PermitirGuardar = false;
        }
    }
}
