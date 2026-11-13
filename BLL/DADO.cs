using System;

namespace BLL
{
    public class DADO : IDADO
    {
        private static Random rnd = new Random();

        public int Tirar()
        {
            return rnd.Next(1, 7);
        }
    }
}
