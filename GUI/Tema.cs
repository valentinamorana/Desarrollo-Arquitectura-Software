using System.Drawing;

namespace GUI
{
    // Colores de la aplicación en un solo lugar: para cambiar el tema, se cambian acá.
    public static class Tema
    {
        // Rosa principal: títulos, botones y totales.
        public static readonly Color Rosa = Color.FromArgb(173, 20, 87);

        // Rosa claro: fondos suaves y bordes.
        public static readonly Color RosaClaro = Color.FromArgb(248, 214, 227);

        // Bordó oscuro: textos sobre fondos claros.
        public static readonly Color Vino = Color.FromArgb(90, 10, 45);
    }
}
