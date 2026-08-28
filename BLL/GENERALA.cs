using System;

namespace BLL
{
    public static class GENERALA
    {
        private static Random rnd = new Random();

        public const string GENERALA_DOBLE = "Generala Doble";

        public static readonly string[] CATEGORIAS = new string[]
        {
            "Unos", "Doses", "Treses", "Cuatros", "Cincos", "Seises",
            "Escalera", "Full", "Poker", "Generala", GENERALA_DOBLE
        };

        public static int[] Tirar(int[] dadosActuales, bool[] guardar)
        {
            int[] resultado = new int[5];
            for (int i = 0; i < 5; i++)
            {
                if (guardar[i])
                {
                    resultado[i] = dadosActuales[i];
                }
                else
                {
                    resultado[i] = rnd.Next(1, 7);
                }
            }
            return resultado;
        }

        // "Servido": la combinación salió en la primera tirada, sin repetir dados.
        // En ese caso Escalera/Full/Poker/Generala valen el doble.
        public static int CalcularPuntaje(string categoria, int[] dados, bool servido = false)
        {
            if (categoria == "Unos")
            {
                return SumaValor(dados, 1);
            }
            if (categoria == "Doses")
            {
                return SumaValor(dados, 2);
            }
            if (categoria == "Treses")
            {
                return SumaValor(dados, 3);
            }
            if (categoria == "Cuatros")
            {
                return SumaValor(dados, 4);
            }
            if (categoria == "Cincos")
            {
                return SumaValor(dados, 5);
            }
            if (categoria == "Seises")
            {
                return SumaValor(dados, 6);
            }
            if (categoria == "Escalera")
            {
                return AplicarServido(Escalera(dados), servido);
            }
            if (categoria == "Full")
            {
                return AplicarServido(Full(dados), servido);
            }
            if (categoria == "Poker")
            {
                return AplicarServido(Poker(dados), servido);
            }
            if (categoria == "Generala")
            {
                return AplicarServido(GeneralaCompleta(dados), servido);
            }
            if (categoria == GENERALA_DOBLE)
            {
                if (GeneralaCompleta(dados) > 0)
                {
                    return 100;
                }
                return 0;
            }
            return 0;
        }

        private static int AplicarServido(int puntaje, bool servido)
        {
            if (servido && puntaje > 0)
            {
                return puntaje * 2;
            }
            return puntaje;
        }

        // Categorías a las que les aplica el bonus de "servido" (las combinaciones, no los números sueltos).
        public static bool EsCombinacion(string categoria)
        {
            if (categoria == "Escalera" || categoria == "Full" || categoria == "Poker" || categoria == "Generala")
            {
                return true;
            }
            return false;
        }

        private static int SumaValor(int[] dados, int valor)
        {
            int total = 0;
            foreach (int d in dados)
            {
                if (d == valor)
                {
                    total = total + valor;
                }
            }
            return total;
        }

        // Cuenta cuántas veces salió cada valor de dado.
        // conteo[1] = cantidad de unos, conteo[2] = cantidad de doses, etc. (la posición 0 no se usa).
        private static int[] ContarRepeticiones(int[] dados)
        {
            int[] conteo = new int[7];
            foreach (int d in dados)
            {
                conteo[d] = conteo[d] + 1;
            }
            return conteo;
        }

        private static int Escalera(int[] dados)
        {
            int[] copia = (int[])dados.Clone();
            Array.Sort(copia);

            int[] menor = { 1, 2, 3, 4, 5 };
            int[] mayor = { 2, 3, 4, 5, 6 };

            bool esMenor = true;
            bool esMayor = true;
            for (int i = 0; i < 5; i++)
            {
                if (copia[i] != menor[i]) esMenor = false;
                if (copia[i] != mayor[i]) esMayor = false;
            }

            if (esMenor || esMayor)
            {
                return 20;
            }
            return 0;
        }

        private static int Full(int[] dados)
        {
            int[] conteo = ContarRepeticiones(dados);

            bool tieneTres = false;
            bool tieneDos = false;
            for (int valor = 1; valor <= 6; valor++)
            {
                if (conteo[valor] == 3) tieneTres = true;
                if (conteo[valor] == 2) tieneDos = true;
            }

            if (tieneTres && tieneDos)
            {
                return 30;
            }
            return 0;
        }

        private static int Poker(int[] dados)
        {
            int[] conteo = ContarRepeticiones(dados);

            for (int valor = 1; valor <= 6; valor++)
            {
                if (conteo[valor] >= 4)
                {
                    return 40;
                }
            }
            return 0;
        }

        private static int GeneralaCompleta(int[] dados)
        {
            int[] conteo = ContarRepeticiones(dados);

            for (int valor = 1; valor <= 6; valor++)
            {
                if (conteo[valor] == 5)
                {
                    return 50;
                }
            }
            return 0;
        }
    }
}
