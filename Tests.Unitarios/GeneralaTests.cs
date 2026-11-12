using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Unitarios
{
    [TestClass]
    public class GeneralaTests
    {
        [TestMethod]
        public void Unos_SumaSoloLosUnos()
        {
            int[] dados = { 1, 1, 3, 4, 6 };
            Assert.AreEqual(2, GENERALA.CalcularPuntaje("Unos", dados));
        }

        [TestMethod]
        public void Escalera_MenorDeUnoACinco_Vale20()
        {
            int[] dados = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(20, GENERALA.CalcularPuntaje("Escalera", dados));
        }

        [TestMethod]
        public void Escalera_MayorDeDosASeis_Vale20()
        {
            int[] dados = { 2, 3, 4, 5, 6 };
            Assert.AreEqual(20, GENERALA.CalcularPuntaje("Escalera", dados));
        }

        [TestMethod]
        public void Escalera_SinSecuencia_Vale0()
        {
            int[] dados = { 1, 2, 3, 4, 6 };
            Assert.AreEqual(0, GENERALA.CalcularPuntaje("Escalera", dados));
        }

        [TestMethod]
        public void Full_TresYDos_Vale30()
        {
            int[] dados = { 2, 2, 2, 5, 5 };
            Assert.AreEqual(30, GENERALA.CalcularPuntaje("Full", dados));
        }

        [TestMethod]
        public void Poker_CuatroIguales_Vale40()
        {
            int[] dados = { 4, 4, 4, 4, 1 };
            Assert.AreEqual(40, GENERALA.CalcularPuntaje("Poker", dados));
        }

        [TestMethod]
        public void Generala_CincoIguales_Vale50()
        {
            int[] dados = { 6, 6, 6, 6, 6 };
            Assert.AreEqual(50, GENERALA.CalcularPuntaje("Generala", dados));
        }

        [TestMethod]
        public void GeneralaDoble_CincoIguales_Vale100()
        {
            int[] dados = { 3, 3, 3, 3, 3 };
            Assert.AreEqual(100, GENERALA.CalcularPuntaje(GENERALA.GENERALA_DOBLE, dados));
        }

        [TestMethod]
        public void Servida_DoblaLaEscalera()
        {
            int[] dados = { 1, 2, 3, 4, 5 };
            Assert.AreEqual(40, GENERALA.CalcularPuntaje("Escalera", dados, true));
        }

        [TestMethod]
        public void Servida_NoDobla_LosNumeros()
        {
            int[] dados = { 1, 1, 3, 4, 6 };
            Assert.AreEqual(2, GENERALA.CalcularPuntaje("Unos", dados, true));
        }

        [TestMethod]
        public void Tirar_GuardandoTodos_NoCambiaLosDados()
        {
            int[] actuales = { 1, 2, 3, 4, 5 };
            bool[] guardar = { true, true, true, true, true };
            CollectionAssert.AreEqual(actuales, GENERALA.Tirar(actuales, guardar));
        }
    }
}
