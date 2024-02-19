using Entidades;
using System.Text.RegularExpressions;
using TrucoJuego;

namespace a
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Carta4RegexTRUE()
        {
            Jugador a = new Jugador();
            a.ComenzarJugador();
            Carta carta = new Carta();

            carta.CartaActual = "../../../../media/cartas/4 BASTO.png";
            a.Cartas[0] = carta;

            Assert.IsTrue(Regex.Match(a.Cartas[0].CartaActual, "4").ToString() != string.Empty);
        }
        [TestMethod]
        public void Carta4RegexFALSE()
        {
            Jugador a = new Jugador();
            a.ComenzarJugador();
            Carta carta = new Carta();

            carta.CartaActual = "../../../../media/cartas/1 BASTO.png";
            a.Cartas[0] = carta;

            Assert.IsTrue(Regex.Match(a.Cartas[0].CartaActual, "4").ToString() == string.Empty);
        }
    }
}