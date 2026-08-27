using System;
using System.Collections.Generic;

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

        public static int CalcularPuntaje(string categoria, int[] dados)
        {
            switch (categoria)
            {
                case "Unos": return SumaValor(dados, 1);
                case "Doses": return SumaValor(dados, 2);
                case "Treses": return SumaValor(dados, 3);
                case "Cuatros": return SumaValor(dados, 4);
                case "Cincos": return SumaValor(dados, 5);
                case "Seises": return SumaValor(dados, 6);
                case "Escalera": return Escalera(dados);
                case "Full": return Full(dados);
                case "Poker": return Poker(dados);
                case "Generala": return GeneralaCompleta(dados);
                case GENERALA_DOBLE: return GeneralaCompleta(dados) > 0 ? 100 : 0;
                default: return 0;
            }
        }

        private static int SumaValor(int[] dados, int valor)
        {
            int total = 0;
            foreach (int d in dados)
            {
                if (d == valor)
                {
                    total += valor;
                }
            }
            return total;
        }

        private static Dictionary<int, int> ContarRepeticiones(int[] dados)
        {
            Dictionary<int, int> conteo = new Dictionary<int, int>();
            foreach (int d in dados)
            {
                if (!conteo.ContainsKey(d))
                {
                    conteo[d] = 0;
                }
                conteo[d]++;
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
            Dictionary<int, int> conteo = ContarRepeticiones(dados);

            bool tieneTres = false;
            bool tieneDos = false;
            foreach (int cantidad in conteo.Values)
            {
                if (cantidad == 3) tieneTres = true;
                if (cantidad == 2) tieneDos = true;
            }

            if (tieneTres && tieneDos)
            {
                return 30;
            }
            return 0;
        }

        private static int Poker(int[] dados)
        {
            Dictionary<int, int> conteo = ContarRepeticiones(dados);

            foreach (int cantidad in conteo.Values)
            {
                if (cantidad >= 4)
                {
                    return 40;
                }
            }
            return 0;
        }

        private static int GeneralaCompleta(int[] dados)
        {
            Dictionary<int, int> conteo = ContarRepeticiones(dados);

            foreach (int cantidad in conteo.Values)
            {
                if (cantidad == 5)
                {
                    return 50;
                }
            }
            return 0;
        }
    }
}
